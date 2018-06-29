using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Menu_ItemListMenu : StarTech.Adapter.StarTechPage
{
    protected int index = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["index"] != "")
        {
            int.TryParse(Request.QueryString["index"], out index);
        }
    }
}