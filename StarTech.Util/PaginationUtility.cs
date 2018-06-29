using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace StarTech.Util
{
    public class PaginationUtility
    {
        private PaginationUtility()
        { }
        public static DataSet GetPaginationList(string fields, string viewtablesql, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            AdoHelper helper = AdoHelper.CreateHelper("DBInstance");
            SqlParameter[] paras = new SqlParameter[7];
            paras[0] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            paras[0].Direction = ParameterDirection.Output;
            paras[1] = new SqlParameter("@QueryStr", viewtablesql);
            paras[2] = new SqlParameter("@FdShow", fields);
            paras[3] = new SqlParameter("@FdOrder", "order by " + sort);
            paras[4] = new SqlParameter("@Where", "where " + filter);
            paras[5] = new SqlParameter("@PageCurrent", currentPageIndex + 1);
            paras[6] = new SqlParameter("@PageSize", pageSize);
            DataSet list = helper.ExecuteSPDataset("sp_list", (IDataParameter[])paras);
            recordCount = Convert.ToInt32(paras[0].Value);
            return list;
        }
        public static DataSet GetPaginationList_hzzlw(string fields, string viewtablesql, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            AdoHelper adoHelper_hzzlw = AdoHelper.CreateHelper("DB_InstanceHzqts");
            SqlParameter[] paras = new SqlParameter[7];
            paras[0] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            paras[0].Direction = ParameterDirection.Output;
            paras[1] = new SqlParameter("@QueryStr", viewtablesql);
            paras[2] = new SqlParameter("@FdShow", fields);
            paras[3] = new SqlParameter("@FdOrder", "order by " + sort);
            paras[4] = new SqlParameter("@Where", "where " + filter);
            paras[5] = new SqlParameter("@PageCurrent", currentPageIndex + 1);
            paras[6] = new SqlParameter("@PageSize", pageSize);
            DataSet list = adoHelper_hzzlw.ExecuteSPDataset("sp_list", (IDataParameter[])paras);
            recordCount = Convert.ToInt32(paras[0].Value);
            return list;
        }
    }
}
