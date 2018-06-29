using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class NGWeiXinRoot_QRcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + token;
        string temp = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"kxm371\"}}}";
        string result = NG.WeiXin.NGWeiXinPubTools.GetPage(url, temp);
        this.TextBox1.Text = result;


        JObject ja = (JObject)JsonConvert.DeserializeObject(result);
        string ticket = ja["ticket"].ToString();
        string url2 = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + Server.UrlEncode(ticket);

        this.div_img.InnerHtml = "<img src='" + url2 + "'>";

    }
}