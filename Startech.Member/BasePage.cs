using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Web.Configuration;
namespace Startech.Member
{
    public class BasePage:System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.UserCheck();
            base.OnInit(e);
        }
        protected void UserCheck()
        {
            if (Session["memberid"] == null || Session["memberid"].ToString() == "")
            {

                Response.Write("<script>window.location.href='/default.aspx';</script>");
                Response.End();
            }
        }
        public string Memberid
        {
            get
            {
                if (Session["memberid"] != null && Session["memberid"].ToString() != "")
                {
                    return Session["memberid"].ToString();
                }
                return "";
            }
        }
        public string memberName
        {
            get
            {
                if (Session["memberName"] != null && Session["memberName"].ToString() != "")
                {
                    return Session["memberName"].ToString();
                }
                return "";
            }
        }
        public string memberTrueNam
        {
            get
            {
                if (Session["memberTrueName"] != null && Session["memberTrueName"].ToString() != "")
                {
                    return Session["memberTrueName"].ToString();
                }
                return "";
            }
        }
    }
}
