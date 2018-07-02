using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NG.WeiXin;
using System.Data.SqlClient;
using System.Data;
using StarTech.DBUtility;

public partial class NGWeiXinRoot_CTTXOAuth2Center : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //统一授权中心，先保存授权之后要跳转的页面
        if (Request["local_redirect_url"] != null)
        {
            Session["local_redirect_url"] = Request["local_redirect_url"];
        }

        NG.WeiXin.Log.Debug(this.GetType().ToString(), "【CTTXOAuth2Center.aspx】Session[local_redirect_url]=" + Session["local_redirect_url"] + "");

        //重定向到微信服务器授权，微信服务器处理之后再次重定向到当前页面
        NG.WeiXin.NGWebOAuth2 webOAuth2 = new NG.WeiXin.NGWebOAuth2();
        NGWebOAuth2Ticket mod = webOAuth2.GetOpenidAndAccessToken("");
        if (mod != null)
        {
            Session["wx_openid"] = mod.openid;
            Session["wx_access_token"] = mod.access_token;
            NG.WeiXin.Log.Debug(this.GetType().ToString(), "【YqxkjOAuth2Center.aspx】openid=" + mod.openid + "&access_token=" + mod.access_token + "");

            //采用openid登录
            int r = LoginByOpenId(mod.openid);
            if (r == 1)
            {
                if (Session["local_redirect_url"] != null)
                {
                    Response.Redirect(Session["local_redirect_url"].ToString());
                }
                else
                {
                    Response.Redirect("CTTXHome.aspx?r=" + DateTime.Now.Ticks);
                }
            }
            else
            {
                //重定向到用户注册界面
                Response.Redirect("CTTXOAuth2.aspx?r=" + DateTime.Now.Ticks);
            }
        }
    }

   /// <summary>
   /// 通过微信openid登录
   /// </summary>
   /// <param name="WXOpenId"></param>
   /// <returns></returns>
    private int LoginByOpenId(string WXOpenId)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        string strSql = @"select * from T_Member_Info where WXOpenId=@WXOpenId";
        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@WXOpenId",WXOpenId)
        };
        

        DataTable dt = ado.ExecuteSqlDataset(strSql, p).Tables[0];
        NG.WeiXin.Log.Debug(this.GetType().ToString(), "【YqxkjOAuth2Center.aspx-->LoginByOpenId】WXOpenId=" + WXOpenId + ";dt.Rows.Count=" + dt.Rows.Count);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["isUse"].ToString() == "0") { return -1; }
            Session["MemberId"] = dt.Rows[0]["MemberId"].ToString();
            return 1;
        }
        else
        {
            return 0;
        }
    }
}