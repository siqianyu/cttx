using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NG.WeiXin
{
    /// <summary>
    ///NGWebOAuth2Ticket 的摘要说明
    /// </summary>
    public class NGWebOAuth2Ticket
    {
        string _access_token;
        string _openid;

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
    }
}