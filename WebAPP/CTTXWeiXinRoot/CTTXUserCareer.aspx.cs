using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CTTXWeiXinRoot_CTTXUserCareer : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        if (Session["wx_openid"] == null || Session["wx_openid"] == "")
        {
            if (Session["MemberId"] == null || Session["MemberId"] == "")
            {
                Response.Redirect("/Login.aspx?redirect_url=CTTXUserLogin.aspx");
            }
            else
            {
                this.hidMemberId.Value = Session["MemberId"].ToString();
            }
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}