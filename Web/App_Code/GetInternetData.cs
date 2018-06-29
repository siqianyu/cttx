using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Text;


    /// <summary>
    /// GetInternetData 的摘要说明
    /// </summary>
    public class GetInternetData
    {
        public GetInternetData()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 用WebRequest进行采集
        /// </summary>
        public static string getCode(string url,Encoding encode)
        {
            string returnStr = "";
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(resStream,encode);
                returnStr = sr.ReadToEnd();
                resStream.Close();
                sr.Close();
            }
            catch (Exception ee)
            {
                returnStr = ee.Message;
            }
            return returnStr;
        }

        /// <summary>
        /// 用WebClient进行采集(可能有乱码现象)
        /// </summary>
        public static string getCode2(string url)
        {
            string returnStr = "";
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = CredentialCache.DefaultCredentials;
                Byte[] pageData = wc.DownloadData(url);
                returnStr = Encoding.Default.GetString(pageData);
            }
            catch (Exception ee)
            {
                returnStr = ee.Message;
            }
            return returnStr;
        }

        /// <summary>
        /// 用WebRequest进行下载文件
        /// </summary>
        public static string download(string url,string savePath)
        {
            string returnStr = "";
            try
            {
                WebRequest wReq = WebRequest.Create(url);
                WebResponse wResp = wReq.GetResponse();
                Stream respStream = wResp.GetResponseStream();
                long length = wResp.ContentLength;
                BinaryReader br = new BinaryReader(respStream);
                FileStream fs = File.Create(savePath);
                fs.Write(br.ReadBytes((int)length), 0, (int)length);
                br.Close();
                fs.Close();
            }
            catch (Exception ee)
            {
                returnStr = ee.Message;
            }
            return returnStr;
        }

        public static string init(string str)
        {
            str = str.ToLower();
            str = str.Replace("\"", "'");
            str = str.Replace(" ", "");
            str = str.Replace("\r\n", "");
            return str;
        }

        /// <summary>
        /// 获取页面的链接正则
        /// </summary>
        public static string getHref(string htmlCode)
        {
            string MatchVale = "";
            string Reg = @"<a[^>]*?href=['""]?([^'"">]+)[^>]*>";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }

        /// <summary>
        /// 获取页面的a正则
        /// </summary>
        public static string getAList(string htmlCode)
        {
            string MatchVale = "";
            //string Reg = "<table.*>.*</table>";

            string Reg = "<a[^>]*>[\\s\\S]*?";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }


        /// <summary>
        /// 获取页面的li正则
        /// </summary>
        public static string getLiList(string htmlCode)
        {
            string MatchVale = "";
            //string Reg = "<table.*>.*</table>";

            string Reg = "<li[^>]*>[\\s\\S]*?<\\/li>";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }




        /// <summary>
        /// 获取页面的table正则
        /// </summary>
        public static string getTableList(string htmlCode)
        {
            string MatchVale = "";
            //string Reg = "<table.*>.*</table>";

            string Reg = "<table[^>]*>[\\s\\S]*?<\\/table>";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }

        /// <summary>
        /// 获取页面的tr正则
        /// </summary>
        public static string getTrList(string htmlCode)
        {
            string MatchVale = "";
            //string Reg = @"<tr.*?\/tr>";
            string Reg = "<tr[^>]*>[\\s\\S]*?<\\/tr>";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }

        /// <summary>
        /// 获取页面的tr正则
        /// </summary>
        public static string getTdList(string htmlCode)
        {
            string MatchVale = "";
            string Reg = "<td[^>]*>[\\s\\S]*?<\\/td>";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }

        /// <summary>
        /// 获取img正则
        /// </summary>
        public static string getImgList(string htmlCode)
        {
            htmlCode = htmlCode.ToLower();
            string MatchVale = "";
            string Reg = @"<img.*?>";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }

        /// <summary>
        /// 获取img src地址正则
        /// </summary>
        public static string getImgSrcList(string htmlCode)
        {
            htmlCode = htmlCode.ToLower().Replace("\"", "'").Replace(" ", "");
            string MatchVale = "";
            string Reg = @"src='.*?'";
            foreach (Match m in Regex.Matches(htmlCode, Reg))
            {
                MatchVale += (m.Value) + "_$$_";
            }
            return MatchVale;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        public static string getInnerContent(string str, string startStr, string endStr)
        {
            try
            {
                int startPos = str.IndexOf(startStr) + startStr.Length;
                str = str.Substring(startPos, str.Length - startPos);
                int endPos = str.IndexOf(endStr);
                string returnStr = str.Substring(0, endPos);
                return returnStr;
            }
            catch { return ""; }
        }

        /// <summary>
        /// 替换HTML源代
        /// </summary>
        public static string RemoveHTML(string HtmlCode)
        {
            string MatchVale = HtmlCode;
            foreach (Match s in Regex.Matches(HtmlCode, "<.+?>"))
            {
                MatchVale = MatchVale.Replace(s.Value, "");
            }
            return MatchVale;
        }
    }

