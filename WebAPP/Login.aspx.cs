using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    public string RedirectUrl = "CTTXWeiXinRoot/CTTXOAuth2Center.aspx?local_redirect_url=";
    string url = "CTTXHome.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        string tmp = Request.QueryString["redirect_url"];
        if (!string.IsNullOrEmpty(tmp))
        {
            url = tmp;
        }
        RedirectUrl = RedirectUrl + url;
        Response.Redirect(RedirectUrl);
    }
}