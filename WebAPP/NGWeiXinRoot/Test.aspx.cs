using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NGWeiXinRoot_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string atoken = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        this.TextBox1.Text = atoken;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string atoken = NG.WeiXin.NGJSApiTicketTools.GetExistJSApi_Ticket();
        this.TextBox2.Text = atoken;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string jsapi_ticket = NG.WeiXin.NGJSApiTicketTools.GetExistJSApi_Ticket();
        string noncestr=NG.WeiXin.NGWeiXinPubTools.GenerateNonceStr();
        string timestamp=NG.WeiXin.NGWeiXinPubTools.GenerateTimeStamp();
        string url=Request.Url.ToString();
        string sign = NG.WeiXin.NGJSApiTicketTools.GetJSApi_Sign(jsapi_ticket, noncestr, timestamp, url);
        this.TextBox3.Text = "【jsapi_ticket】" + jsapi_ticket + "【noncestr】" + noncestr + "【timestamp】" + timestamp + "【url】" + url + "【sign】" + sign;
    }
}