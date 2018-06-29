using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using StarTech.DBUtility;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// DalBase 的摘要说明
/// </summary>
public class DalBase
{
    public static readonly string DBInstance = "DB_Instance"; //数据库实例
    public static readonly string DBInstance_hzzlw = "DB_InstanceHzqts"; //杭州质量网数据库实例
    public static readonly string DBInstance_hzsis = "DB_InstanceHzisz"; //杭州标准服务网数据库实例  

    #region 通用批量操作
    /// <summary>
    /// 批量更新数据
    /// </summary>
    public static int Util_UpdateBat(string updateDesc, string updateFilter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string sql = "update " + tableName + " set " + updateDesc + " where " + updateFilter + "";
        return adoHelper.ExecuteSqlNonQuery(sql);
    }

    /// <summary>
    /// 批量删除数据
    /// </summary>
    public static int Util_DeleteBat(string filter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string sql = "delete " + tableName + "  where " + filter + "";
        return adoHelper.ExecuteSqlNonQuery(sql);
    }

    /// <summary>
    /// 判断某条数据是否存在
    /// </summary>
    public static bool Util_CheckIsExsitData(string filter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string sql = "select count(*) count1 from " + tableName + "  where " + filter + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        if (ds.Tables[0].Rows[0]["count1"].ToString() == "0") { return false; }
        return true;
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataSet Util_GetList(string strSql)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        DataSet ds = adoHelper.ExecuteSqlDataset(strSql);
        return ds;
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataSet Util_GetList(string fields, string fielter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string sql = "select " + fields + "  from " + tableName + " where " + fielter + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds;
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataTable Util_GetList(string fields, string fielter, string sort, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string sql = "select " + fields + "  from " + tableName + " where " + fielter + " order by " + sort + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds.Tables[0];
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataTable Util_GetList(string fields, string fielter, string sort, int top, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string sql = "select top " + top + " " + fields + "  from " + tableName + " where " + fielter + " order by " + sort + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds.Tables[0];
    }
    /// <summary>
    /// 获得数据列表并且排序
    /// </summary>
    public static DataTable Util_GetGroupByList(string groupbyDesc, string fielter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string sql = "select " + groupbyDesc + "  from " + tableName + " where " + fielter + " group by  " + groupbyDesc + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds.Tables[0];
    }

    /// <summary>    
    /// 统计某个字段的总和
    /// </summary>
    public static DataTable Util_SumFields(string fields, string fielter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string fieldsDesc = "";
        string[] fieldsArr = fields.Split(',');
        for (int i = 0; i < fieldsArr.Length; i++)
        {
            fieldsDesc += "isnull(sum(" + fieldsArr[i] + "),0) as " + fieldsArr[i] + ",";
        }
        if (fieldsDesc != "")
        {
            fieldsDesc = fieldsDesc.Substring(0, fieldsDesc.Length - 1);
            string sql = "select " + fieldsDesc + " from " + tableName + " ";
            if (fielter != "") { sql += " where " + fielter; }
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }
        else { return null; }
    }

    /// <summary>
    /// 填充DataSet
    /// </summary>
    public static int Util_UpdateSet(DataSet ds, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        return adoHelper.UpdateDataset(ds, tableName);
    }
    #endregion

    #region 杭州质量网数据操作通用方法
    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataSet Util_hzzlw_GetList(string strSql)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        DataSet ds = adoHelper.ExecuteSqlDataset(strSql);
        return ds;
    }
    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataSet Util_hzzlw_GetList(string fields, string fielter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        string sql = "select " + fields + "  from " + tableName + " where " + fielter + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds;
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataTable Util_hzzlw_GetList(string fields, string fielter, string sort, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        string sql = "select " + fields + "  from " + tableName + " where " + fielter + " order by " + sort + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds.Tables[0];
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataTable Util_hzzlw_GetList(string fields, string fielter, string sort, int top, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        string sql = "select top " + top + " " + fields + "  from " + tableName + " where " + fielter + " order by " + sort + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds.Tables[0];
    }
    /// <summary>
    /// 获得数据列表并且排序
    /// </summary>
    public static DataTable Util_hzzlw_GetGroupByList(string groupbyDesc, string fielter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        string sql = "select " + groupbyDesc + "  from " + tableName + " where " + fielter + " group by  " + groupbyDesc + "";
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        return ds.Tables[0];
    }
    /// <summary>
    /// 批量删除数据
    /// </summary>
    public static int Util_hzzlw_DeleteBat(string filter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        string sql = "delete " + tableName + "  where " + filter + "";
        return adoHelper.ExecuteSqlNonQuery(sql);
    }
    /// <summary>
    /// 批量更新数据
    /// </summary>
    public static int Util_hzzlw_UpdateBat(string updateDesc, string updateFilter, string tableName)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        string sql = "update " + tableName + " set " + updateDesc + " where " + updateFilter + "";
        return adoHelper.ExecuteSqlNonQuery(sql);
    }

    public static DataSet GetWTOList_Detail(int YJId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        SqlParameter[] parameters = {
					new SqlParameter("@YJID", SqlDbType.BigInt,8)
				};
        parameters[0].Value = YJId;
        return adoHelper.ExecuteSPDataset("UP_WTO_CKZK_GetList", parameters);
    }
    public static string GetTypeName_Hzzlw(string strTypeId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzzlw);
        string strSql = string.Format(@"select DirNameChs from T_Directory where DirectoryID='" + strTypeId + "'");
        return adoHelper.ExecuteSqlScalar(strSql).ToString();
    }
    #endregion

    #region 杭州标准服务网数据操作通用方法
    /// <summary>
    /// 获得数据列表
    /// </summary>
    public static DataSet Util_hzsis_GetList(string strSql)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance_hzsis);
        DataSet ds = adoHelper.ExecuteSqlDataset(strSql);
        return ds;
    }

    #endregion

    #region 民生调查
    public static int UpdateOptin(int titleId, int optionId, string strOption)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string strSql = " ";
        switch (strOption)
        {
            case "A":
                strSql = "update  T_Form_Comment set OptionA=OptionA+1 where titleId=" + titleId + " and id=" + optionId + "";
                break;
            case "B":
                strSql = "update  T_Form_Comment set OptionB=OptionB+1   where titleId=" + titleId + " and id=" + optionId + "";
                break;
            case "C":
                strSql = "update  T_Form_Comment  set OptionC=OptionC+1  where titleId=" + titleId + " and id=" + optionId + "";
                break;
            case "D":
                strSql = "update  T_Form_Comment  set OptionD=OptionD+1  where titleId=" + titleId + " and id=" + optionId + "";
                break;
            default:
                strSql = "update  T_Form_Comment  set OptionA=OptionA+1  where titleId=" + titleId + " and id=" + optionId + "";
                break;
        }
        return adoHelper.ExecuteSqlNonQuery(strSql);
    }
    public static DataSet GetCommentList(string strWhere, string strSort)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper(DBInstance);
        string strSql = "select * from  T_Form_Comment";
        if (strWhere.Trim() != "")
        {
            strSql += " where " + strWhere;
        }
        if (strSort.Trim() != "")
        {
            strSql += " order by  " + strSort;
        }
        return adoHelper.ExecuteSqlDataset(strSql);
    }

    #endregion
}
