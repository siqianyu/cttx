using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace StarTech.ELife.ShortMsg.AliSms
{
    public class AliSms
    {
        private const string GETURL = "http://dysmsapi.aliyuncs.com/";
        private const string Version = "2017-05-25";
        private string AccessKeyId;
        private string AccessKeySecret;
        public string PhoneNumbers = "";
        public string SignName = "";
        public string TemplateCode = "";
        public string TemplateParam = "";
        public AliSms(string keyid, string keysecret)
        {
            this.AccessKeyId = keyid;
            this.AccessKeySecret = keysecret;
        }
        public string Send()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("AccessKeyId", this.AccessKeyId);
            dictionary.Add("Action", "SendSms");
            dictionary.Add("Format", "JSON");
            dictionary.Add("PhoneNumbers", this.PhoneNumbers);
            dictionary.Add("SignatureMethod", "HMAC-SHA1");
            dictionary.Add("SignatureNonce", Guid.NewGuid().ToString());
            dictionary.Add("SignatureVersion", "1.0");
            dictionary.Add("SignName", this.SignName);
            dictionary.Add("TemplateCode", this.TemplateCode);
            dictionary.Add("TemplateParam", this.TemplateParam);
            dictionary.Add("Timestamp", AliTools.GetTimeStamp());
            dictionary.Add("Version", "2017-05-25");
            string source = AliTools.ComposeStringToSign("GET", dictionary);
            string value = AliTools.SignString(source, this.AccessKeySecret + "&");
            dictionary.Add("Signature", value);
            string url = "http://dysmsapi.aliyuncs.com/?" + AliTools.GetQueryString(dictionary);
            string value2 = AliTools.HttpGet(url);
            //Log.Write("[SMS]", url, "sms/");
            string result = "";
            JObject jObject = new JObject();
            try
            {
                jObject = (JObject)JsonConvert.DeserializeObject(value2);
            }
            catch (Exception Ex)
            {
                result = "Json Error";
                jObject = null;
                //Log.Write("[SMS]:", "【Data】:", Ex);
            }
            if (jObject != null)
            {
                if (jObject["Code"] != null)
                {
                    result = jObject["Code"].ToString();
                }
                else
                {
                    result = "Code Error";
                }
            }
            return result;
        }
    }
}
