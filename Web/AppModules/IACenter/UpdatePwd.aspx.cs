using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using StarTech.DBUtility;

public partial class AppModules_Sysadmin_Member_UpdatePwd : StarTech.Adapter.StarTechPage
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtOrignalPwd.Text.Trim(), "MD5");
        string newPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtNewPwd.Text.Trim(), "MD5");
        string uid = Session["UserId"].ToString();
        if (Session["UserId"] != null && Session["UserId"].ToString() != "")
        {
            DataTable dt = DalBase.Util_GetList("select * from IACenter_User where password='" + pwd + "' and uniqueId='" + Session["UserId"].ToString() + "' ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                int i = ado.ExecuteSqlNonQuery("update IACenter_User set password='" + newPwd + "' where uniqueId='" + Session["UserId"].ToString() + "'");
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "lk", "<script>alert('密码修改成功');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "lk", "<script>alert('修改失败');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "lk", "<script>alert('原始密码错误，修改失败');</script>");
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "lk", "<script>alert('修改失败');</script>");
        }
    }
}