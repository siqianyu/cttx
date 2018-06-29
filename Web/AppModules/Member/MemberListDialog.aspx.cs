using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Member_MemberListDialog : StarTech.Adapter.StarTechPage
{
    protected string categoryId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["state"] != null)
        {
            categoryId = "&statu=1";
        }
    }
}