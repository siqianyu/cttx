using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NGWeiXinRoot_SavePic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string atoken = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string path = HttpContext.Current.Server.MapPath("Source/" + this.TextBox1.Text + ".jpg");
        NG.WeiXin.NGWeiXinPubTools.DownloadPic(atoken, this.TextBox1.Text, path);
    }
}