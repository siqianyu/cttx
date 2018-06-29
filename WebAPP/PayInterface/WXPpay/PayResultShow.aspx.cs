using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PayResultShow : System.Web.UI.Page
{
    public string back_url;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PayPage_BackUrl"] != null && Session["PayPage_BackUrl"] != "")
        {
            back_url = Session["PayPage_BackUrl"].ToString();
        }
        else
        {
            back_url = "/NGWeiXinRoot/YqxkjHome.aspx";
        }
    }
}