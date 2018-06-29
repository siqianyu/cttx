using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Web.Security;

namespace NG.WeiXin
{
    /// <summary>
    ///NGJSApiTicketTools 的摘要说明
    /// </summary>
    public  class NGJSApiTicketTools
    {
        public static string GetExistJSApi_Ticket()
        {

            string Token = string.Empty;
            DateTime YouXRQ;
            // 读取XML文件中的数据，并显示出来 ，注意文件路径  
            string filepath = HttpContext.Current.Server.MapPath("NGJSApiTicketMode.xml");

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
                NGJSApiTicket mode = GetJSApi_Ticket();
                xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText = mode.ticket;
                _youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
                xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText = _youxrq.ToString();
                xml.Save(filepath);
                Token = mode.ticket;
            }
            return Token;
        }
        public static NGJSApiTicket GetJSApi_Ticket()
        {
            string access_token = NGAccessTokenTools.GetExistAccess_Token();
            string strUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + access_token + "&type=jsapi";

            NGJSApiTicket mode = new NGJSApiTicket();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                string content = reader.ReadToEnd();

                Log.Debug("NGJSApiTicketTools", "GetJSApi_Ticket response : " + content);
                //Response.Write(content);  
                //在这里对SApiTicket 赋值  
                NGJSApiTicket token = new NGJSApiTicket();
                token = NGJSONHelper.ParseFromJson<NGJSApiTicket>(content);
                mode.ticket = token.ticket;
                mode.expires_in = token.expires_in;
            }
            return mode;
        }




        /// <summary>
        /// 生成JSApi签名
        /// </summary>
        /// <param name="jsapi_ticket"></param>
        /// <param name="timestamp"></param>
        /// <param name="noncestr"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetJSApi_Sign(string jsapi_ticket, string noncestr, string timestamp, string url)
        {
            /*
             noncestr=Wm3WZYTPz0wzccnW
             jsapi_ticket=sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg
             timestamp=1414587457
             url=http://mp.weixin.qq.com?params=value
             */

            string strSource = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;

            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "SHA1").ToLower();

            Log.Debug("NGJSApiTicketTools", "GetJSApi_Sign response : " + strSource + ";sign=" + sign);

            return sign;
        }
    }
}