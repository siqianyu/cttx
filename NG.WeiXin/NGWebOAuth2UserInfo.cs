using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NG.WeiXin
{
    /// <summary>
    ///NGWebOAuth2Ticket 的摘要说明
    /// </summary>
    public class NGWebOAuth2UserInfo
    {
        string _access_token;
        string _openid;
        string _nickname;
        string _headimgurl;

        /// <summary>  
        /// access_token   
        /// </summary>  
        public string access_token
        {
            get { return _access_token; }
            set { _access_token = value; }
        }

        /// <summary>  
        /// openid
        /// </summary>  
        public string openid
        {
            get { return _openid; }
            set { _openid = value; }
        }

        /// <summary>  
        /// nickname
        /// </summary>  
        public string nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        /// <summary>  
        /// headimgurl
        /// </summary>  
        public string headimgurl
        {
            get { return _headimgurl; }
            set { _headimgurl = value; }
        }
    }
}