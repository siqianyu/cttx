using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NG.WeiXin;

public partial class NGWeiXinRoot_OAuth2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NG.WeiXin.NGWebOAuth2 webOAuth2 = new NG.WeiXin.NGWebOAuth2();

        NGWebOAuth2Ticket mod = webOAuth2.GetOpenidAndAccessToken("snsapi_userinfo");
        if (mod != null)
        {
            Response.Write("<br>--------------------------<br>");
            Response.Write("openid:" + mod.openid);
            Response.Write("<br>");
            Response.Write("access_token:" + mod.access_token);
            Response.Write("<br>");
            Session["openid"] = mod.openid;
            Session["access_token"] = mod.access_token;
        }

        NGWebOAuth2UserInfo userInfo = webOAuth2.GetUserinfo(mod.access_token, mod.openid);
        if (userInfo != null)
        {
            Response.Write("<br>--------------------------<br>");
            Response.Write("nickname:" + userInfo.nickname);
            Response.Write("<br>");
            Response.Write("headimgurl:" + userInfo.headimgurl);
            Response.Write("<br>");
            Response.Write("openid:" + userInfo.openid);
            Response.Write("<br>");
        }
    }
}