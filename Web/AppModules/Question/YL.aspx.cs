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
using StarTech.DBUtility;

public partial class Admin_AppModules_Question_YL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("MemberCookieInfo");
        cookie.Values.Add("MemberId", "0");
        cookie.Expires = DateTime.Now.AddHours(8);
        Response.Cookies.Add(cookie);

        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber = '" + Request["Nid"] + "'");
        string ids = "";
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ids += row["Questions"].ToString() + ",";
        }
        ids = ids.Replace(",,", ",");
        ado.ExecuteSqlNonQuery("update   T_Test_day set Questions='" + ids + "' where Sysnumber ='" + Request["Nid"] + "'");
        Response.Write("<script>location.href='/Member/TestDetailByPrint.aspx?Nid=" + Request["Nid"] + "&view=1'</script>");
    }
}
