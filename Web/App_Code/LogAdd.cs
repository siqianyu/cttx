using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using StarTech.DBUtility;
using System.Data;

/// <summary>
///日志生成方案
/// </summary>
public class LogAdd
{

    

	public LogAdd()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 判断角色是否在线，是否可以继续操作
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static bool IsOnline(HttpContext context,ref string userId)
    {
        if (context.Session["UserId"] == null)
            return false;
        userId=context.Session["UserId"].ToString();
        return true;
    }

    /// <summary>
    /// 生成日志信息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="menuNameLevel"></param>
    /// <param name="actionType"></param>
    /// <param name="remark"></param>
    /// <param name="ip"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static int CreateLog(string userId, string menuNameLevel,string actionType,string remark,string ip,string url)
    {
        string id = Guid.NewGuid().ToString();
        DateTime addTime = DateTime.Now;
        string strSQL = "select userName,trueName from IACenter_User where uniqueId='"+userId+"';";
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = ado.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count<1)
            return -1;
        string userName = ds.Tables[0].Rows[0][0].ToString();
        string trueName = ds.Tables[0].Rows[0][1].ToString();
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into IACenter_UserActionLog(");
        strSql.Append("id,userName,userTrueName,menuNameLevel1,actionType,remarks1,ip,url,addTime)");
        strSql.Append(" values (@id,@userName,@userTrueName,@menuNameLevel1,@actionType,@remarks1,@ip,@url,@addTime");
        strSql.Append(")");
        SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar),
					new SqlParameter("@userName", SqlDbType.VarChar),
					new SqlParameter("@userTrueName", SqlDbType.VarChar),
					new SqlParameter("@menuNameLevel1", SqlDbType.VarChar),
					new SqlParameter("@actionType", SqlDbType.VarChar),
					new SqlParameter("@remarks1", SqlDbType.VarChar),
					new SqlParameter("@ip", SqlDbType.VarChar),
					new SqlParameter("@url", SqlDbType.VarChar),
					new SqlParameter("@addTime", SqlDbType.DateTime)
                                    };
        parameters[0].Value = id;
        parameters[1].Value = userName;
        parameters[2].Value = trueName;
        parameters[3].Value = menuNameLevel;
        parameters[4].Value = actionType;
        parameters[5].Value = remark;
        parameters[6].Value = ip;
        parameters[7].Value = url;
        parameters[8].Value = addTime;

        int row=ado.ExecuteSqlNonQuery(strSql.ToString(), parameters);

        return row;
    }
}