using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NG.WeiXin
{
    public class NGWeiXinUser
    {

        /// <summary>
        /// 获取微信用户信息（nickname,sex,headimgurl,remark）读取：userObj["nickname"].ToString()
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static JObject GetUserInfoByOpenId(string openid)
        {
            try
            {
                string atoken = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
                string str = " https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + atoken + "&openid=" + openid + "&lang=zh_CN";
                string data = NG.WeiXin.NGWeiXinPubTools.SendGetHttpRequest(str, "GET");
                JObject userObj = (JObject)JsonConvert.DeserializeObject(data);
                return userObj;
            }
            catch { return null; }
        }
    }
}
