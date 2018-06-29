using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///PayLog 的摘要说明
/// </summary>
public class PayLog
{
	public PayLog()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public  static void PayLogWrite(string paylogId,string memberId,string orderId,string payStatu,string payType,decimal paymoney,DateTime  payTime,string payContext,string remark,DateTime payOutTime)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into T_Order_PayLog(");
        strSql.Append("paylogId,memberId,orderId,payStatu,payType,paymoney,payTime,payContext,remark,payOutTime)");
        strSql.Append(" values (");
        strSql.Append("@paylogId,@memberId,@orderId,@payStatu,@payType,@paymoney,@payTime,@payContext,@remark,@payOutTime)");
        SqlParameter[] parameters = {
					new SqlParameter("@paylogId", SqlDbType.VarChar,50),
					new SqlParameter("@memberId", SqlDbType.VarChar,50),
					new SqlParameter("@orderId", SqlDbType.VarChar,50),
					new SqlParameter("@payStatu", SqlDbType.VarChar,50),
					new SqlParameter("@payType", SqlDbType.VarChar,50),
					new SqlParameter("@paymoney", SqlDbType.Decimal,9),
					new SqlParameter("@payTime", SqlDbType.DateTime),
					new SqlParameter("@payContext", SqlDbType.VarChar,500),
					new SqlParameter("@remark", SqlDbType.VarChar,1000),
					new SqlParameter("@payOutTime", SqlDbType.DateTime)};
        parameters[0].Value = paylogId;
        parameters[1].Value = memberId;
        parameters[2].Value = orderId;
        parameters[3].Value = payStatu;
        parameters[4].Value = payType;
        parameters[5].Value = paymoney;
        parameters[6].Value = payTime;
        parameters[7].Value = payContext;
        parameters[8].Value = remark;
        parameters[9].Value = payOutTime;

        StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
        adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
    }
}