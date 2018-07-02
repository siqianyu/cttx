using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace StarTech.ELife.ShortMsg.AliSms
{
    public  class AliTools
    {
        private const string SEPARATOR = "&";
        public static string SignString(string source, string accessSecret)
        {
            string result;
            using (KeyedHashAlgorithm keyedHashAlgorithm = KeyedHashAlgorithm.Create("HMACSHA1"))
            {
                keyedHashAlgorithm.Key = Encoding.UTF8.GetBytes(accessSecret.ToCharArray());
                result = Convert.ToBase64String(keyedHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(source.ToCharArray())));
            }
            return result;
        }
        private static IDictionary<string, string> SortDictionary(Dictionary<string, string> dic)
        {
            return new SortedDictionary<string, string>(dic, StringComparer.Ordinal);
        }
        public static string ComposeStringToSign(string method, Dictionary<string, string> queries)
        {
            IDictionary<string, string> dictionary = AliTools.SortDictionary(queries);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> current in dictionary)
            {
                stringBuilder.Append("&").Append(AliTools.PercentEncode(current.Key)).Append("=").Append(AliTools.PercentEncode(current.Value));
            }
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append(method);
            stringBuilder2.Append("&");
            stringBuilder2.Append(AliTools.PercentEncode("/"));
            stringBuilder2.Append("&");
            stringBuilder2.Append(AliTools.PercentEncode(stringBuilder.ToString().Substring(1)));
            return stringBuilder2.ToString();
        }
        public static string HttpGet(string Url)
        {
            string result = "";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                result = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }
        public static string GetQueryString(Dictionary<string, string> reqMap)
        {
            string text = "";
            foreach (string current in reqMap.Keys)
            {
                string text2 = text;
                text = string.Concat(new string[]
				{
					text2,
					current,
					"=",
					AliTools.percentEncode(reqMap[current]),
					"&"
				});
            }
            text = text.Substring(0, text.Length - 1);
            return text;
        }
        public static string percentEncode(string str)
        {
            return HttpUtility.UrlEncode(str, Encoding.UTF8).Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
        }
        public static string GetTimeStamp()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
        }
        public static string PercentEncode(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(value);
            byte[] array = bytes;
            for (int i = 0; i < array.Length; i++)
            {
                char c = (char)array[i];
                if (text.IndexOf(c) >= 0)
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    stringBuilder.Append("%").Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", new object[]
					{
						(int)c
					}));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
