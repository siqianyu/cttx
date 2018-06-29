using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;

public partial class NGWeiXinRoot_Push : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetOpenid();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string touser = this.DropDownList1.SelectedValue;
        string token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();

        string template_id = "kPv2MKW57TSPUBGAC2rrZTDImyhUTkT8GohQKp1Ew9Y";
        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token;
        string temp = "{\"touser\": \"" + touser + "\"," +
                      "\"template_id\": \"" + template_id + "\", " +
                      "\"topcolor\": \"#FF0000\", " +
                      "\"url\":\"http://wx.qq.com/\"," +
                      "\"data\": " +
                      "{\"first\": {\"value\": \"通知\"}," +
                      "\"keyword1\": { \"value\": \"通知一下" + DateTime.Now + "\"}," +
                      "\"keyword2\": { \"value\": \" \"}," +
                      "\"remark\": {\"value\": \"推送测试\" }}}";
        string result = NG.WeiXin.NGWeiXinPubTools.GetPage(url, temp);
        this.TextBox1.Text = result;
    }

    public void GetOpenid()
    {
        string atoken = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string strUrl = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + atoken;
        string datas = NG.WeiXin.NGWeiXinPubTools.SendGetHttpRequest(strUrl, "GET");

        JObject ja = (JObject)JsonConvert.DeserializeObject(datas);
        int totalnum = int.Parse(ja["count"].ToString());
        for (int i = 0; i < totalnum; i++)
        {
            string openid = ja["data"]["openid"][i].ToString();
            string str = " https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + atoken + "&openid=" + openid + "&lang=zh_CN";
            string data = NG.WeiXin.NGWeiXinPubTools.SendGetHttpRequest(str, "GET");
            JObject ja1 = (JObject)JsonConvert.DeserializeObject(data);
            string UserName = ja1["nickname"].ToString();
            string remark = ja1["remark"].ToString();
            ListItem item = new ListItem(UserName, openid);
            this.DropDownList1.Items.Add(item);
        }
    }
}