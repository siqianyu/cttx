using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///ShareHelper 的摘要说明
/// </summary>
public class ShareHelper
{
	public ShareHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}


    /// <summary>
    /// 分享获得佣金
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public static int AddShareMember(string orderId)
    {
        StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
        DataTable dtOrder = adoHelper.ExecuteSqlDataset("select * from T_Order_Info where isPay=1 and orderId='" + orderId + "'").Tables[0];
        if (dtOrder.Rows.Count > 0)
        {
            double money = double.Parse(dtOrder.Rows[0]["OrderAllMoney"].ToString());
            string orderMemberId = dtOrder.Rows[0]["MemberId"].ToString();
            DataTable dtShareMember = adoHelper.ExecuteSqlDataset("select * from V_WXQRCodeShare_Log where newFirendMemberId='" + orderMemberId + "'").Tables[0];

            if (money >= 200 && dtShareMember.Rows.Count > 0)
            {
                //200奖励50，400奖励100
                int CouponValue = 0;
                if (money < 200)
                {
                    CouponValue = 1;
                }
                else if (money >= 200 && money < 400)
                {
                    CouponValue = 50;
                }
                else if (money >= 400)
                {
                    CouponValue = 100;
                }
                string remarks = "分享给“" + dtShareMember.Rows[0]["newFirendTrueName"].ToString() + "”获得的佣金";

                if (adoHelper.ExecuteSqlScalar("select MemberId from T_Member_ShareCash where MemberId='" + dtShareMember.Rows[0]["MemberId"].ToString() + "' and ShareFirendMemberId='" + orderMemberId + "'") == null)
                {
                    return AddShareCash(dtShareMember.Rows[0]["MemberId"].ToString(), CouponValue, remarks, orderMemberId, dtShareMember.Rows[0]["newFirendTrueName"].ToString());
                }
            }
        }

        return 0;
    }

    /// <summary>
    /// 分享获得佣金
    /// </summary>
    /// <param name="MemberId"></param>
    /// <param name="CouponValue"></param>
    /// <param name="Remark"></param>
    /// <param name="ShareFirendMemberId"></param>
    /// <param name="ShareFirendTrueName"></param>
    /// <returns></returns>
    public static int AddShareCash(string MemberId, int CouponValue, string Remark, string ShareFirendMemberId, string ShareFirendTrueName)
    {
        string CouponId = "D" + MemberId + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999);
        List<SqlParameter> plist = new List<SqlParameter>();
        string guid = Guid.NewGuid().ToString();
        plist.Add(new SqlParameter("@Sysnumber", guid));
        plist.Add(new SqlParameter("@MemberId", MemberId));
        plist.Add(new SqlParameter("@CouponId", CouponId));
        plist.Add(new SqlParameter("@CouponType", "代金券"));
        plist.Add(new SqlParameter("@CouponValue", CouponValue));
        plist.Add(new SqlParameter("@IsUsed", "0"));
        plist.Add(new SqlParameter("@Remark", Remark));
        plist.Add(new SqlParameter("@GetPlaceInfo", ""));
        plist.Add(new SqlParameter("@ShareFirendMemberId", ShareFirendMemberId));
        plist.Add(new SqlParameter("@ShareFirendTrueName", ShareFirendTrueName));
        plist.Add(new SqlParameter("@ShareFirendRegTime", DateTime.Now));
        plist.Add(new SqlParameter("@CreateTime", DateTime.Now));
        SqlParameter[] p = plist.ToArray();
        int result = StarTech.DBCommon.InsertData("T_Member_ShareCash", p);
        return result;


    }
}