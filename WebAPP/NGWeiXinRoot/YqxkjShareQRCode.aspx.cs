using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Drawing.Imaging;

public partial class NGWeiXinRoot_YqxkjShareQRCode : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        //上个页面要获取到wx_openid，并且保存到Session里
        if (Session["wx_openid"] != null && Session["wx_openid"].ToString() != "")
        {
            this.WXOpenId.Value = Session["wx_openid"].ToString();
            DataTable dtUser = GetUserInfo(this.WXOpenId.Value);
            if (dtUser.Rows.Count > 0)
            {
                this.ltUser.Text = dtUser.Rows[0]["TrueName"].ToString();
                CreateQRCode(this.WXOpenId.Value);
            }
        }
        else
        {
            //this.WXOpenId.Value = "wx_openid";
            Response.Write("wx_openid获取失败");
            //Response.End();
        }
    }

    protected DataTable GetUserInfo(string WXOpenId)
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_Member_Info where WXOpenId='" + WXOpenId + "'").Tables[0];
        return dt;
    }

    public void CreateQRCode(string WXOpenId)
    {
        string token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + token;
        string temp = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"" + WXOpenId + "\"}}}";
        string result = NG.WeiXin.NGWeiXinPubTools.GetPage(url, temp);
        NG.WeiXin.Log.Debug(this.GetType().ToString(), "【YqxkjShareQRCode.aspx-->CreateQRCode】url=" + url + ";result=" + result + "");

        JObject ja = (JObject)JsonConvert.DeserializeObject(result);
        if (ja["ticket"] != null)
        {
            string ticket = ja["ticket"].ToString();
            string url2 = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + Server.UrlEncode(ticket);
            this.img_qrcode.Src = url2;
            //保存图片
            string path = DownloadImg(url2, WXOpenId);
            if (path == "") { path = url2; }
            Response.Redirect("YqxkjShareQRCodeImg.aspx?fromuser=" + Server.UrlEncode(this.ltUser.Text) + "&imgpath=" + Server.UrlEncode(path) + "");
        }
        else
        {
            Response.Write("对不起，服务器繁忙，请稍后重试...");
            Response.End();
        }
    }

    public string DownloadImg(string url, string WXOpenId)
    {
        string path = HttpContext.Current.Server.MapPath("/Source/QRCode/" + WXOpenId + ".png");
        if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

        WebRequest request = (WebRequest)HttpWebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "image/png";
        using (WebResponse response = request.GetResponse())
        {
            if (response != null)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                    img.Save(path, ImageFormat.Png);
                    return "/Source/QRCode/" + WXOpenId + ".png";
                }
            }
        }
        return "";
    }

}