using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NG.WeiXin
{
    /// <summary>
    ///NGJSApiTicket 的摘要说明
    /// </summary>
    public class NGJSApiTicket
    {
        /*
        "errcode":0,
        "errmsg":"ok",
        "ticket":"bxLdikRXVbTPdHSM05e5u5sUoXNKd8-41ZO3MhKoyN5OfkWITDGgnr2fwJ0m9E8NYzWKVZvdVtaUgWvsdshFKA",
        "expires_in":7200
         */

        string _ticket;
        string _expires_in;

        /// <summary>  
        /// 获取到的凭证   
        /// </summary>  
        public string ticket
        {
            get { return _ticket; }
            set { _ticket = value; }
        }

        /// <summary>  
        /// 凭证有效时间，单位：秒  
        /// </summary>  
        public string expires_in
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }
    }
}