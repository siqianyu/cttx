using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using StarTech.Order;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //StarTech.Order.Seller.GrabOrder.StartGrabOrder("100002", 1, "100302");
    }

    protected string GetRootMenus(string userId)
    {
        return "1";
        string ids = "";
        StarTech.Adapter.IACenter iacenter = new StarTech.Adapter.IACenter();
        DataSet dsMenus = iacenter.GetAllMenusByUserId(Int32.Parse(userId));
        if (dsMenus != null)
        {
            DataRow[] rowsMenus2 = dsMenus.Tables[0].Select("parentMenuId=0 and isAreaMenu=1");

            foreach (DataRow rowMenus2 in rowsMenus2)
            {
                ids += rowMenus2["uniqueId"].ToString() + ",";
            }
            return ids;
        }
        else { return ""; }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //验证码
        //if (Request["txtYZM"] == null || Session["VerifyChar"] == null) { return; }
        //if (Session["VerifyChar"].ToString().ToLower() != Request["txtYZM"].ToString().ToLower())
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<Script>alert('验证码错误！');</Script>");
        //    return;
        //}
        string username = this.txtUsername.Value.Trim().Replace("'", "");
        string pwd = this.txtPassword.Value;
        pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
        //Response.Write(pwd); Response.End();
        DataTable dtUser = new DataTable();
        int r = new StarTech.Adapter.IACenter().UserLogin(username, pwd, ref dtUser);
        if (r == 1)
        {
            //日志
            //iacenter.AddUserActionLog(dt.Rows[0]["UserName"].ToString(), dt.Rows[0]["TrueName"].ToString(), "", "", "", "登陆", "", "", "", Request.UserHostAddress, Request.Url.ToString());

            Session["UserId"] = dtUser.Rows[0]["uniqueId"].ToString();
            LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "用户‘" + dtUser.Rows[0]["userName"].ToString() + "’登录", "登录", "", "", HttpContext.Current.Request.Url.ToString());

            //string ids = GetRootMenus(dt.Rows[0]["uniqueId"].ToString());
            string ids = "1";
            if (ids != "")
            {
                Response.Redirect("Main.aspx?rootMenuId=" + ids.Split(',')[0] + "", true);
            }
            else
            {
                Response.Redirect("Main.aspx?rootMenuId=-1", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('用户名或密码错误!');</script>");
        }
    }
}
