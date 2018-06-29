using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_MP4Player : System.Web.UI.Page
{
    public string path;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.path = this.hidVideoPath.Value = Request["path"];
        }
    }
}