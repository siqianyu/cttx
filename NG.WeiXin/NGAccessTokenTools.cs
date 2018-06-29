using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;

namespace NG.WeiXin
{
    /// <summary>
    ///NGAccessTokenTools 的摘要说明
    /// </summary>
    public  class NGAccessTokenTools
    {
        public NGAccessTokenTools()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string GetExistAccess_Token()
        {

            string Token = string.Empty;
            DateTime YouXRQ;
            // 读取XML文件中的数据，并显示出来 ，注意文件路径  
            string filepath = HttpContext.Current.Server.MapPath("NGAccessTokenMode.xml");

            StreamReader str = new StreamReader(filepath, System.Text.Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            str.Close();
            str.Dispose();
            Token = xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
            YouXRQ = Convert.ToDateTime(xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText);

            if (DateTime.Now > YouXRQ)
            {
                DateTime _youxrq = DateTime.Now;
                NGAccessToken mode = GetAccess_token();
                xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText = mode.access_token;
                //设置有效期（秒）
                //_youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
                _youxrq = _youxrq.AddSeconds(60);
                xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText = _youxrq.ToString();
                xml.Save(filepath);
                Token = mode.access_token;
            }
            return Token;
        }
        public static NGAccessToken GetAccess_token()
        {
            string appid = NGWeiXinConfig.appID;
            string secret = NGWeiXinConfig.appsecret;
            string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
            NGAccessToken mode = new NGAccessToken();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                string content = reader.ReadToEnd();
                Log.Debug("NGAccessTokenTools", "GetAccess_token response : " + content);
                //Response.Write(content);  
                //在这里对Access_token 赋值  
                NGAccessToken token = new NGAccessToken();
                token = NGJSONHelper.ParseFromJson<NGAccessToken>(content);
                mode.access_token = token.access_token;
                mode.expires_in = token.expires_in;
            }
            return mode;
        }
    }
}