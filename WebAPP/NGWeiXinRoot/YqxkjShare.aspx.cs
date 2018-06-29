using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class NGWeiXinRoot_YqxkjShare : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["wx_openid"] != null && Request["wx_openid"] != "")
        {
            this.WXOpenId.Value = Request["wx_openid"].ToString();
            DataTable dtUser = GetUserInfo(this.WXOpenId.Value);
            if (dtUser.Rows.Count > 0)
            {
                this.ltUser.Text = dtUser.Rows[0]["TrueName"].ToString();
            }
        }
        else
        {
            //this.WXOpenId.Value = "wx_openid";
            Response.Write("wx_openid获取失败");
            //Response.End();
        }
    }

    protected DataTable GetUserInfo(string WXOpenId)
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_Member_Info where WXOpenId='" + WXOpenId + "'").Tables[0];
        return dt;
    }
}