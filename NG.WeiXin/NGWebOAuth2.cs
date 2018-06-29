using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LitJson;
namespace NG.WeiXin
{
   public class NGWebOAuth2
    {


        /**
          * 
          * 网页授权获取用户基本信息的全部过程
          * 详情请参看网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
          * 第一步：利用url跳转获取code
          * 第二步：利用code去获取openid和access_token
          * 
          * scope为snsapi_base
            https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx520c15f417810387&redirect_uri=https%3A%2F%2Fchong.qq.com%2Fphp%2Findex.php%3Fd%3D%26c%3DwxAdapter%26m%3DmobileDeal%26showwxpaytitle%3D1%26vb2ctag%3D4_2030_5_1194_60&response_type=code&scope=snsapi_base&state=123#wechat_redirect
            scope为snsapi_userinfo
            https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxf0e81c3bee622d60&redirect_uri=http%3A%2F%2Fnba.bluewebgame.com%2Foauth_response.php&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect
          */
       public NGWebOAuth2Ticket GetOpenidAndAccessToken(string scope_flag)
       {

           if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["code"]))
           {
               //获取code码，以获取openid和access_token
               string code = HttpContext.Current.Request.QueryString["code"];
               Log.Debug(this.GetType().ToString(), "Get code : " + code);
               NGWebOAuth2Ticket mod = GetOpenidAndAccessTokenFromCode(code);
               return mod;
           }
           else
           {
               //构造网页授权获取code的URL
               string host = HttpContext.Current.Request.Url.Host;
               string path = HttpContext.Current.Request.Path;
               int port = HttpContext.Current.Request.Url.Port;
               string myquery = "";
               string redirect_uri = HttpUtility.UrlEncode("http://" + host + path + myquery);

               string appid = NGWeiXinConfig.appID;
               string response_type = "code";
               string scope = "snsapi_base";
               if (scope_flag == "snsapi_userinfo") { scope = "snsapi_userinfo"; }
               string state = "STATE" + "#wechat_redirect";
               string data = "appid=" + appid + "&redirect_uri=" + redirect_uri + "&response_type=" + response_type + "&scope=" + scope + "&state" + state;
               string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data;
               Log.Debug(this.GetType().ToString(), "Will Redirect to URL : " + url);
               try
               {
                   //触发微信返回code码         
                   HttpContext.Current.Response.Redirect(url);//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
               }
               catch (System.Threading.ThreadAbortException ex)
               {
               }
           }
           return null;
       }


       /**
       * 
       * 通过code换取网页授权access_token和openid的返回数据，正确时返回的JSON数据包如下：
       * {
       *  "access_token":"ACCESS_TOKEN",
       *  "expires_in":7200,
       *  "refresh_token":"REFRESH_TOKEN",
       *  "openid":"OPENID",
       *  "scope":"SCOPE",
       *  "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
       * }
       * 其中access_token可用于获取共享收货地址
       * openid是微信支付jsapi支付接口统一下单时必须的参数
       * 更详细的说明请参考网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
       * @失败时抛异常WxPayException
        * https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
       */
       public NGWebOAuth2Ticket GetOpenidAndAccessTokenFromCode(string code)
       {
           try
           {
               //构造获取openid及access_token的url

               string appid = NGWeiXinConfig.appID;
               string secret = NGWeiXinConfig.appsecret;
               string grant_type = "authorization_code";
               string data = "appid=" + appid + "&secret=" + secret + "&code=" + code + "&grant_type=" + grant_type + "";
               string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data;

               //请求url以获取数据
               string result = HttpService.Get(url);

               Log.Debug(this.GetType().ToString(), "GetOpenidAndAccessTokenFromCode response : " + result);

               //保存access_token
               JsonData jd = JsonMapper.ToObject(result);
               string access_token = (string)jd["access_token"];

               //获取用户openid
               string openid = (string)jd["openid"];

               NGWebOAuth2Ticket mod = new NGWebOAuth2Ticket();
               mod.access_token = access_token;
               mod.openid = openid;

               Log.Debug(this.GetType().ToString(), "Get openid : " + openid);
               Log.Debug(this.GetType().ToString(), "Get access_token : " + access_token);
               return mod;
           }
           catch (Exception ex)
           {
               Log.Error(this.GetType().ToString(), ex.ToString());
               return null;
           }
       }



       /**
      * 
      * 第四步：拉取用户信息(需scope为 snsapi_userinfo)
      * 如果网页授权作用域为snsapi_userinfo，则此时开发者可以通过access_token和openid拉取用户信息了。
            {    
        *    "openid":" OPENID",
            " nickname": NICKNAME,
            "sex":"1",
            "province":"PROVINCE"
            "city":"CITY",
            "country":"COUNTRY",
            "headimgurl":    "http://thirdwx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46",
            "privilege":[ "PRIVILEGE1" "PRIVILEGE2"     ],
            "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
            }
      * 更详细的说明请参考网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
       *https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
      */
       public NGWebOAuth2UserInfo GetUserinfo(string access_token, string openid)
       {
           try
           {
               //构造获取openid及access_token的url
               string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN";

               //请求url以获取数据
               string result = HttpService.Get(url);
               Log.Debug(this.GetType().ToString(), "GetUserinfo response : " + result);

               //保存access_token
               JsonData jd = JsonMapper.ToObject(result);
               //获取用户openid
               string nickname = (string)jd["nickname"];
               string headimgurl = (string)jd["headimgurl"];
               string openid2 = (string)jd["openid"];

               NGWebOAuth2UserInfo mod = new NGWebOAuth2UserInfo();
               mod.nickname = nickname;
               mod.headimgurl = headimgurl;
               mod.access_token = access_token;
               mod.openid = openid2;

               Log.Debug(this.GetType().ToString(), "Get openid : " + openid);
               Log.Debug(this.GetType().ToString(), "Get access_token : " + access_token);
               return mod;
           }
           catch (Exception ex)
           {
               Log.Error(this.GetType().ToString(), ex.ToString());
               return null;
           }
       }
    }
}
