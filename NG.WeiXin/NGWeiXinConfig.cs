using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NG.WeiXin
{
    /// <summary>
    ///NGWeiXinConfig 的摘要说明
    /// </summary>
    public static class NGWeiXinConfig
    {
        public static string appID = System.Configuration.ConfigurationSettings.AppSettings["WeiXin_AppId"];    //"wx2605abb0be2aad0a";

        public static string appsecret = System.Configuration.ConfigurationSettings.AppSettings["WeiXin_AppSecret"]; // "278c57764bafb2fb49260158d265531b";
    }
}