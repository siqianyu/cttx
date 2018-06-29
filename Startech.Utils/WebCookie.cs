using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Collections;

namespace Startech.Utils
{
    public class WebCookie
    {
        public static void SetAllCookies(Hashtable hTable)
        {
            HttpCookie newCookie = new HttpCookie("Wlyd_Cookies");
            newCookie.Expires = DateTime.Now.AddHours(1);
            foreach (DictionaryEntry entry in hTable)
            {
                newCookie[entry.Key.ToString()] = EncodeCookie(entry.Value.ToString());
            }
            HttpContext.Current.Response.Cookies.Add(newCookie);
        }

        public static string GetCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Wlyd_Cookies"];
            if (cookie != null)
            {
                string str = cookie[key];
                if (str != null && str != "")
                {
                    return DecodeCookie(str);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static void DelCookie()
        {
            HttpCookie newCookie = new HttpCookie("Wlyd_Cookies");
            newCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(newCookie);
        }


        public static string EncodeCookie(string str)
        {
            str = HttpContext.Current.Server.UrlDecode(str);
            //º”√‹
            string encryptKey = WebConfigurationManager.AppSettings["DESEncryptKey"];
            str = DES.Encode(str, encryptKey);
            return str;
        }

        public static string DecodeCookie(string str)
        {
            str = HttpContext.Current.Server.UrlDecode(str);
            //Ω‚√‹
            string encryptKey = WebConfigurationManager.AppSettings["DESEncryptKey"];
            str = DES.Decode(str, encryptKey);
            return str;
        }
    }
}
