<%@ WebHandler Language="C#" Class="WeiXinPayCheck" %>

using System;
using System.Web;

public class WeiXinPayCheck : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        if (context.Request["orderId"] != null && context.Request["orderId"] != null)
        {
            context.Response.Write(QueryOrder(context.Request["orderId"]));
        }
    }

    //查询订单
    private string QueryOrder(string out_trade_no)
    {
        //查询本地支付情况
        if (CheckIsPay(out_trade_no) == true) {
            //用户分享获取相应的奖励(有可能是微信后台通知，改变了支付状态，导致无法执行用户分享流程)
            ShareHelper.AddShareMember(out_trade_no);
            return "SUCCESS"; 
        }
        
        WxPayAPI.WxPayData req = new WxPayAPI.WxPayData();
        req.SetValue("out_trade_no", out_trade_no);
        WxPayAPI.WxPayData res = WxPayAPI.WxPayApi.OrderQuery(req);
        //查询日志
        PayLog.PayLogWrite(Guid.NewGuid().ToString(), "0", out_trade_no, "orderquery", "wx", 0, DateTime.Now, out_trade_no, res.ToXml(), DateTime.Now);
        try
        {
            if (res.GetValue("trade_state").ToString() == "SUCCESS")
            {
                UpdatePay(out_trade_no, res.ToXml());
            }
            return res.GetValue("trade_state").ToString();
        }
        catch { return "Error"; }
    }

    /// <summary>
    /// 更新本地订单
    /// </summary>
    /// <param name="out_trade_no"></param>
    /// <param name="remarks"></param>
    protected void UpdatePay(string out_trade_no, string remarks)
    {
        StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");

        object objMoney = adoHelper.ExecuteSqlScalar("select OrderAllMoney from T_Order_Info where orderId='" + out_trade_no + "'");
        if (objMoney != null)
        {
            int i = adoHelper.ExecuteSqlNonQuery("update T_Order_Info set isPay=1 , payTime=getdate() where orderId='" + out_trade_no + "'");
            if (i > 0)
            {
                //更新优惠券为已经使用
                adoHelper.ExecuteSqlNonQuery("update T_Member_Coupon set IsUsed=1 where CouponId=(select CouponId from T_Order_Info where orderId='" + out_trade_no + "')");
                
                //用户分享获取相应的奖励
                ShareHelper.AddShareMember(out_trade_no);
                
                //日志记录
                PayLog.PayLogWrite(Guid.NewGuid().ToString(), "0", out_trade_no, "pay", "wx", decimal.Parse(objMoney.ToString()), DateTime.Now, out_trade_no, remarks, DateTime.Now);
            }
        }
    }

    /// <summary>
    /// 查询本地支付情况
    /// </summary>
    /// <param name="out_trade_no"></param>
    /// <returns></returns>
    protected bool CheckIsPay(string out_trade_no)
    {
        StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
        object obj = adoHelper.ExecuteSqlScalar("select orderId from T_Order_Info where isPay=1 and orderId='" + out_trade_no + "'");
        return obj == null ? false : true;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}