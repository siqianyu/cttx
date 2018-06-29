using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

public partial class demo_ProductPage : System.Web.UI.Page
{

    /// <summary>
    /// 调用js获取收货地址时需要传入的参数
    /// 格式：json串
    /// 包含以下字段：
    ///     appid：公众号id
    ///     scope: 填写“jsapi_address”，获得编辑地址权限
    ///     signType:签名方式，目前仅支持SHA1
    ///     addrSign: 签名，由appid、url、timestamp、noncestr、accesstoken参与签名
    ///     timeStamp：时间戳
    ///     nonceStr: 随机字符串
    /// </summary>
    public static string wxEditAddrParam { get; set; }

    public string pay_url;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.out_trade_no.Value = Request.QueryString["out_trade_no"] == null ? "" : Request.QueryString["out_trade_no"];   //本地订单号

        Log.Info(this.GetType().ToString(), "page load");
        if (!IsPostBack)
        {
            JsApiPay jsApiPay = new JsApiPay(this);
            try
            {
                //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                jsApiPay.GetOpenidAndAccessToken();

                //获取收货地址js函数入口参数
                //wxEditAddrParam = jsApiPay.GetEditAddressParameters();
                ViewState["openid"] = jsApiPay.openid;
            }
            catch (Exception ex)
            {
                //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
                Button1.Visible = false;
                Label1.Visible = false;
            }

            if (ViewState["openid"] != null && ViewState["openid"].ToString() != "")
            {
                //根据订单号获取订单金额
                int total_fee = (int)(GetMoeny(this.out_trade_no.Value) * 100);
                //total_fee=1;
                this.pay_url = "http://www.yiqixkj.com/PayInterface/WXPpay/JsApiPayPage.aspx?openid=" + ViewState["openid"].ToString() + "&out_trade_no=" + this.out_trade_no.Value + "&total_fee=" + total_fee;

            }
        }
    }

    protected decimal GetMoeny(string out_trade_no)
    {
        //return 1;
        StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
        object objMoney = adoHelper.ExecuteSqlScalar(" select top 1 OrderAllMoney from T_Order_Info where OrderId='" + out_trade_no + "'");
         if (objMoney != null)
         {
             return decimal.Parse(objMoney.ToString());
         }
         return 0;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string total_fee = (GetMoeny(this.out_trade_no.Value) * 100).ToString();
        if (ViewState["openid"] != null)
        {
            string openid = ViewState["openid"].ToString();
            string url = "http://www.yiqixkj.com/PayInterface/WXPpay/JsApiPayPage.aspx?openid=" + openid + "&out_trade_no=" + this.out_trade_no.Value + "&total_fee=" + total_fee;
            Response.Redirect(url);
        }
        else
        {
            //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面缺少参数，请返回重试" + "</span>");
            Button1.Visible = false;
            Label1.Visible = false;
        }
    }
}
