using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using StarTech.Order.Order;
using StarTech.Order;

/// <summary>
///订单辅助类
/// </summary>
public class OrderHelper
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");

    /// <summary>
    /// 订单主表T_Order_Info
    /// </summary>
    public string AddOrderByFree(string MemberId, string GoodsId, string CouponId)
    {
        //判断重复下单
        if (ado.ExecuteSqlScalar("select orderId from T_Order_Info where  MemberId='" + MemberId + "' and IsPay=1 and orderId in (select orderId from T_Order_InfoDetail where [GoodsId]='" + GoodsId + "')") != null)
        {
            return "error|重复下单";
        }

        //读取报价表和其他信息
        DataTable dtPrice = ado.ExecuteSqlDataset("select * from T_Goods_Info where GoodsId='" + GoodsId + "'").Tables[0];
        if (dtPrice.Rows.Count == 0) { return "error|数据异常"; }
        decimal OrderAllMoney = decimal.Parse(dtPrice.Rows[0]["SalePrice"].ToString());     //（重要）订单价格
        string JobType = dtPrice.Rows[0]["JobType"].ToString();

        //会员表
        DataTable dtMember = ado.ExecuteSqlDataset("select * from T_Member_Info where MemberId='" + MemberId + "'").Tables[0];
        if (dtMember.Rows.Count == 0) { return "error|数据异常"; }
        string MemberName = dtMember.Rows[0]["MemberName"].ToString();
        string TrueName = dtMember.Rows[0]["TrueName"].ToString();
        string AddressCode = dtMember.Rows[0]["AddressCode"].ToString();
        string AddressDetail = dtMember.Rows[0]["AddressDetail"].ToString();
        string PostCode = dtMember.Rows[0]["PostCode"].ToString();

        //优惠券信息
        decimal TicketMoney = 0;
        if (CouponId != "")
        {
            object objCouponValue = ado.ExecuteSqlScalar("select CouponValue from T_Member_Coupon where MemberId='" + MemberId + "' and CouponId='" + CouponId + "' and isnull(IsUsed,0)=0 and EndTime>getdate()");
            TicketMoney = objCouponValue == null ? 0 : decimal.Parse(objCouponValue.ToString());
        }

        //订单主表（订单主信息，包括人员信息和价格信息等）
        ModOrder mod = new ModOrder();

        //订单基本信息
        mod.OrderId = DateTime.Now.ToString("yyyyMMddHHmmss") + MemberId + new Random().Next(1000, 9999).ToString();
        mod.OrderType = JobType;                                         //（重要）订单类型，和工作类型关联：全包（QB）、钟点工（DG）
        mod.OrderTime = DateTime.Now;
        mod.SendMethod = "off";
        mod.PayMethod = "线下支付";


        //雇员信息
        mod.MemberId = MemberId;                                        //（重要）雇员编号
        mod.MemberName = MemberName;
        mod.MemberOrderRemarks = "后台下单";                                    //订单备注（备用）
        mod.ReceivePerson = TrueName;
        mod.ReceiveAddressCode = AddressCode;
        mod.ReceiveAddressDetail = AddressDetail;
        mod.PostCode = PostCode;



        //优惠券信息
        mod.couponId = CouponId;                                        //（重要）优惠券编号
        mod.oldMoney = OrderAllMoney;                                   //（重要）订单原始价格（优惠券抵用前）
        mod.TicketMoney = TicketMoney;                                  //（重要）优惠券金额

        //价格信息
        mod.OrderAllMoney = OrderAllMoney - TicketMoney;                              //（重要）订单价格
        mod.GoodsAllMoney = OrderAllMoney - TicketMoney;
        mod.OrderAllWeight = 0;


        //支付信息
        mod.IsPay = 1;                                                  //（重要）是否支付
        mod.PayInterfaceCode = "";                                      //第三方支付接口提交的编号，查账用
        mod.PayMethod = "后台下单";                                 //支付方式（支付宝ZFB，微信WX等）
        mod.PayTime = DateTime.Now;                                     //支付时间


        //创建订单
        if (new BllOrder().Add(mod) > 0)
        {
            //明细信息
            AddOrderDetail(mod.OrderId, mod.OrderType, GoodsId);
        }

        return mod.OrderId;
    }


    /// <summary>
    /// 订单明细T_Order_InfoDetail
    /// </summary>
    public int AddOrderDetail(string OrderId, string OrderType, string GoodsId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Goods_Info where GoodsId='" + GoodsId + "'").Tables[0];
        BllOrderDetail bllDetail = new BllOrderDetail();
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            ModOrderDetail mod = new ModOrderDetail();
            mod.OrderId = OrderId;
            mod.OrderType = OrderType;
            mod.GoodsId = row["GoodsId"].ToString();
            mod.GoodsName = row["GoodsName"].ToString();
            mod.GoodsCode = row["GoodsCode"].ToString();
            mod.GoodsFormate = "";
            mod.Price = decimal.Parse(row["SalePrice"].ToString());
            mod.GoodsPic = row["GoodsSmallPic"].ToString();
            mod.ProviderInfo = row["ProviderInfo"].ToString();
            mod.DataFrom = row["DataFrom"].ToString();
            mod.Quantity = 1;
            mod.AllMoney = mod.Price * mod.Quantity;
            mod.AllWeight = mod.OneWeight * mod.Quantity;
            i += bllDetail.Add(mod);
        }
        return i;
    }
}