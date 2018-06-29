<%@ WebHandler Language="C#" Class="WXJSSDKInterface" %>

using System;
using System.Web;

public class WXJSSDKInterface : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //接收参数
        string flag = Common.NullToEmpty(context.Request["flag"]);
        string goodsid = Common.NullToEmpty(context.Request["goodsid"]);
        string couponid = Common.NullToEmpty(context.Request["couponid"]);
        int studytime = Common.NullToZero(context.Request["studytime"]);
        string email = Common.NullToEmpty(context.Request["email"]);
        string truename = HttpContext.Current.Server.UrlDecode(Common.NullToEmpty(context.Request["truename"]));
        string phone = Common.NullToEmpty(context.Request["phone"]);
        string areacode = Common.NullToEmpty(context.Request["areacode"]);
        string wxopenid = Common.NullToEmpty(context.Request["wxopenid"]);
        string pwd = Common.NullToEmpty(context.Request["pwd"]);
        string targeturl = Common.NullToEmpty(context.Request["targetUrl"]);


        context.Response.ContentType = "text/plain";

        switch (flag.ToLower())
        {
            case "jsapi_token"://购买课程
                context.Response.Write(JsApiToken(targeturl));
                break;
            
        }
        
    }

    protected string JsApiToken(string url)
    {
        string jsapi_ticket = NG.WeiXin.NGJSApiTicketTools.GetExistJSApi_Ticket();
        string noncestr = NG.WeiXin.NGWeiXinPubTools.GenerateNonceStr();
        string timestamp = NG.WeiXin.NGWeiXinPubTools.GenerateTimeStamp();
        string sign = NG.WeiXin.NGJSApiTicketTools.GetJSApi_Sign(jsapi_ticket, noncestr, timestamp, url);
        return noncestr + "$" + timestamp + "$" + sign;
    }
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}