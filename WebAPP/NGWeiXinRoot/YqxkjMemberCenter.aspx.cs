using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class NGWeiXinRoot_YqxkjMemberCenter : StarTech.Adapter.BasePage
{
    public string BZZX_CategoryID;//帮助中心
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hid_memberId.Value = this.MemberId;
        this.img_header.Src = this.HeadImg == "" ? "Images/header.jpg" : this.HeadImg;
        this.BZZX_CategoryID = WebConfigurationManager.AppSettings["BZZX_CategoryID"];//帮助中心
    }
}