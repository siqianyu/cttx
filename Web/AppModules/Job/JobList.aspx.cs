using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Job_JobList : StarTech.Adapter.StarTechPage
{
    protected string categoryId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["state"] != null)
        {
            categoryId = Request.QueryString["state"];
            if (categoryId == "1")
            {
                categoryId = "&statu=1";
            }
            else if (categoryId == "0")
            {
                categoryId = "&statu=0";
            }
        }
    }
}