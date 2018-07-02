using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NGWeiXinRoot_CTTXUserReg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["wx_openid"] != null && Session["wx_openid"] != "")
        {
            this.WXOpenId.Value = Session["wx_openid"].ToString();
            if (Session["wx_headimgurl"] != null && Session["wx_headimgurl"] != "")
            {
                this.HeadImg.Value = Session["wx_headimgurl"].ToString();
            }
        }
        else
        {
            //this.WXOpenId.Value = "wx_openid";
            //  Response.Write("wx_openid获取失败");
            //Response.End();
        }
        //if (Session["MemberId"] != null && Session["MemberId"] != "")
        //{
        //    this.MemberId.Value = Session["MemberId"].ToString();
        //}
    }
}