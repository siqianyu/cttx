using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using StarTech.DBUtility;
using System.Data;

/// <summary>
///ShopLog 的摘要说明
/// </summary>
public class ShopLog
{
    private static AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public ShopLog()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //


    }

    /// <summary>
    /// 增加一条数据
    /// </summary>
    public static int AddLog(string shopid, int type, string dec, string info)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into T_Shop_Log(");
        strSql.Append("shopid,log_type,log_dec,log_info,log_time)");
        strSql.Append(" values (");
        strSql.Append("@shopid,@log_type,@log_dec,@log_info,@log_time)");
        strSql.Append(";select @@IDENTITY");
        SqlParameter[] parameters = {
					new SqlParameter("@shopid", SqlDbType.VarChar,50),
					new SqlParameter("@log_type", SqlDbType.Int,4),
					new SqlParameter("@log_dec", SqlDbType.VarChar,50),
					new SqlParameter("@log_info", SqlDbType.VarChar,500),
					new SqlParameter("@log_time", SqlDbType.DateTime)};
        parameters[0].Value = shopid;
        parameters[1].Value = type;
        parameters[2].Value = dec;
        parameters[3].Value = info;
        parameters[4].Value = DateTime.Now;

        object obj = ado.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        if (obj == null)
        {
            return 0;
        }
        else
        {
            return Convert.ToInt32(obj);
        }
    }
}