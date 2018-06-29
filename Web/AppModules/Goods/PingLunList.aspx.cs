using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_IACenter_PingLunList : StarTech.Adapter.StarTechPage
{
    public string goodsid;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.goodsid = Request["goodsid"] == null ? "" : Request["goodsid"];
    }
}