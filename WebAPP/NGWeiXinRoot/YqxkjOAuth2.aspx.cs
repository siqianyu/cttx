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

public partial class NGWeiXinRoot_YqxkjOAuth2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NG.WeiXin.NGWebOAuth2 webOAuth2 = new NG.WeiXin.NGWebOAuth2();

        NGWebOAuth2Ticket mod = webOAuth2.GetOpenidAndAccessToken("snsapi_userinfo");
        if (mod != null)
        {
            Response.Write("<br>--------------------------<br>");
            Response.Write("openid:" + mod.openid);
            Response.Write("<br>");
            Response.Write("access_token:" + mod.access_token);
            Response.Write("<br>");
            Session["wx_openid"] = mod.openid;
            Session["wx_access_token"] = mod.access_token;
            NG.WeiXin.Log.Debug(this.GetType().ToString(), "【YqxkjOAuth2.aspx】openid=" + mod.openid + "&access_token=" + mod.access_token + "");

            //采用openid登录
            int r = LoginByOpenId(mod.openid);
            if (r == 1)
            {
                Response.Redirect("YqxkjHome.aspx");
            }
            else
            {
                NGWebOAuth2UserInfo userInfo = webOAuth2.GetUserinfo(mod.access_token, mod.openid);
                if (userInfo != null)
                {
                    Response.Write("<br>--------------------------<br>");
                    Response.Write("nickname:" + userInfo.nickname);
                    Response.Write("<br>");
                    Response.Write("headimgurl:" + userInfo.headimgurl);
                    Response.Write("<br>");
                    Response.Write("openid:" + userInfo.openid);
                    Response.Write("<br>");
                    Session["wx_nickname"] = userInfo.nickname;
                    Session["wx_headimgurl"] = userInfo.headimgurl;
                    NG.WeiXin.Log.Debug(this.GetType().ToString(), "【YqxkjOAuth2.aspx】nickname=" + userInfo.nickname + "&headimgurl=" + userInfo.headimgurl + "");

                    //获取微信用户信息之后，重定向到用户注册界面
                    Response.Redirect("YqxkjUserReg.aspx");
                }
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
        NG.WeiXin.Log.Debug(this.GetType().ToString(), "【YqxkjOAuth2.aspx】【LoginByOpenId】WXOpenId=" + WXOpenId + ";dt.Rows.Count=" + dt.Rows.Count);
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