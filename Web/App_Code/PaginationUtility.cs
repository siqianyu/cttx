using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using StarTech.DBUtility;
using System.Data.SqlClient;

/// <summary>
///PaginationUtility 的摘要说明
/// </summary>
public class PaginationUtility
{
    public PaginationUtility()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public DataTable GetPaginationList(string fields, string viewtablesql, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
    {
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        SqlParameter[] paras = new SqlParameter[7];
        paras[0] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
        paras[0].Direction = ParameterDirection.Output;
        paras[1] = new SqlParameter("@QueryStr", viewtablesql);
        paras[2] = new SqlParameter("@FdShow", fields);
        paras[3] = new SqlParameter("@FdOrder", sort);
        paras[4] = new SqlParameter("@Where", "where " + filter);
        paras[5] = new SqlParameter("@PageCurrent", currentPageIndex);
        paras[6] = new SqlParameter("@PageSize", pageSize);
        DataSet list = helper.ExecuteSPDataset("sp_list", (IDataParameter[])paras);
        recordCount = Convert.ToInt32(paras[0].Value);
        return list.Tables[0];
    }
}