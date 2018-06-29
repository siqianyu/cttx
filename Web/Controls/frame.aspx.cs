using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_frame : StarTech.Adapter.StarTechPage
{
    public string url;
    //protected override void OnInit(EventArgs e)
    //{
    //    if (Session["qlygUserId"] == null || Session["qlygUserId"].ToString()=="")
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "go", "<script>document.getElementById('fram').src='/AppModules/Company/CompanyInfo_qlyg.aspx?backurl=" + (Request.QueryString["url"] == null ? "" : Request.QueryString["url"].ToString()) + "';</script>");
    //        //Response.Redirect("/Controls/frame.aspx?url=/AppModules/Company/CompanyInfo_qlyg.aspx?backurl=" + (Request.QueryString["url"] == null ? "" : Request.QueryString["url"].ToString()));
    //    }
    //    base.OnInit(e);
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        url = Request.QueryString["url"] == null ? "" : Request.QueryString["url"].ToString();
        if (Session["qlygUserId"] != null && Session["qlygUserId"].ToString() == "-1")
        {
            url = "/AppModules/Company/CompanyInfo_qlyg.aspx?backurl=" + url;
        }
    }
}
