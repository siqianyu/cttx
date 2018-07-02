using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.ShortMsg.YunPianSms
{
    public class YunPianSms
    {
        private const string POSTURL = "https://sms.yunpian.com/v1/sms/send.json";
        private string AccessKeyId;
        public string mobile;
        public string text;//发送的内容
        public YunPianSms(string keyid)
        {
            this.AccessKeyId = keyid;
        }
        /// <summary>
        /// code=0为成功
        /// </summary>
        /// <returns></returns>
        public string Send()
        {
            //{"code":0,"msg":"OK","result":{"count":1,"fee":1,"sid":9339732980}}
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("apikey", this.AccessKeyId);
            dictionary.Add("mobile", this.mobile);
            dictionary.Add("text", this.text);
            string result = "";
            string parms = YunPianTools.BuildQuery(dictionary);
            string value2 = YunPianTools.HttpPost(POSTURL, parms);
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
                if (jObject["msg"] != null)
                {
                    result = jObject["msg"].ToString();
                    if (result != "OK")
                    {
                        result = value2;
                    }
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
