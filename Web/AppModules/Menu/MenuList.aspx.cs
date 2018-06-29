using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Menu_MenuList : StarTech.Adapter.StarTechPage
{
    protected string categoryId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["categoryId"] != null)
        {
            categoryId = "&categoryId=" + StarTech.KillSqlIn.Form_ReplaceByString(Request.QueryString["categoryId"], 20);
        }
    }
}