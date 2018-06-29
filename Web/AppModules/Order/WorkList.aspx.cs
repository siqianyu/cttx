using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_IACenter_WorkList : StarTech.Adapter.StarTechPage
{
    public string showtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.showtype = Request["type"] == null ? "" : Request["type"];
    }
}