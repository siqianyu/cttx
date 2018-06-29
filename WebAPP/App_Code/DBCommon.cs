using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;

namespace StarTech
{
    /// <summary>
    /// 公共类
    /// </summary>
    public static class DBCommon
    {
        private static AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");

        /// <summary>
        /// 解析成浮点类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Double ParseToDouble(object o)
        {
            try
            {
                return Convert.ToDouble(o);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 解析成int类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int ParseToInt(object o)
        {
            try
            {
                return Convert.ToInt32(o);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 解析成string类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ParseToString(object o)
        {
            try
            {
                return o.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 将DataTable格式化成JGGrid控件指定的格式
        /// </summary>
        /// <param name="curPage"></param>
        /// <param name="totalPage"></param>
        /// <param name="totalRecords"></param>
        /// <param name="dt"></param>
        /// <param name="fields"></param>
        /// <param name="keyField"></param>
        /// <returns></returns>
        public static string ToJGGridJson(string curPage, string totalPage, string totalRecords, DataTable dt, string[] fields, string keyField)
        {
            string s = "{";
            s += "\"page\":\"" + curPage + "\",";
            s += "\"total\":" + totalPage + ",";
            s += "\"records\":\"" + totalRecords + "\",";
            s += "\"rows\":[";
            s += GetJGGridRows(dt, fields, keyField);
            s += "],";
            s += "\"userdata\":{}";
            s += "}";
            return s;
        }

        /// <summary>
        /// JGGrid格式化中rows的格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fields"></param>
        /// <param name="keyField"></param>
        /// <returns></returns>
        public static string GetJGGridRows(DataTable dt, string[] fields, string keyField)
        {
            string s = "";
            foreach (DataRow row in dt.Rows)//循环行
            {
                string item = "";
                foreach (string field in fields)//循环当前行字段
                {
                    string txt = row[field.Trim()].ToString().Replace("\\", "\\\\").Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("\"", "’");

                    item += "\"" + txt + "\",";
                }
                if (item != "") { item = item.TrimEnd(','); }

                s += "{\"" + keyField + "\":\"" + row[keyField] + "\",\"cell\":[" + item + "]},";
            }
            if (s != "") { s = s.TrimEnd(','); }
            return s;
        }

        /// <summary>
        /// 计算总页码
        /// </summary>
        /// <param name="totalRecords"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int GetTotalPages(int totalRecords, int pageSize)
        {
            if (totalRecords <= pageSize) { return 1; }
            int totalPage = totalRecords / pageSize;
            if (totalRecords % pageSize > 0) { totalPage++; }
            return totalPage;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="ado">数据库选项</param>
        /// <param name="fields">字段</param>
        /// <param name="viewtablesql">表名</param>
        /// <param name="filter">查询sql语句</param>
        /// <param name="sort">排序sql语句</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="recordCount">数据总量</param>
        /// <returns></returns>
        public static DataTable GetPaginationList(AdoHelper ado, string fields, string viewtablesql, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            SqlParameter[] paras = new SqlParameter[7];
            paras[0] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            paras[0].Direction = ParameterDirection.Output;
            paras[1] = new SqlParameter("@QueryStr", viewtablesql);
            paras[2] = new SqlParameter("@FdShow", fields);
            paras[3] = new SqlParameter("@FdOrder", sort);
            paras[4] = new SqlParameter("@Where", "where " + filter);
            paras[5] = new SqlParameter("@PageCurrent", currentPageIndex);
            paras[6] = new SqlParameter("@PageSize", pageSize);
            DataSet list = ado.ExecuteSPDataset("sp_list", (IDataParameter[])paras);
            recordCount = ParseToInt(paras[0].Value);
            return list.Tables[0];
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="p">要修改的字段（参数值），数组的第一个值作为主键条件</param>
        /// <returns></returns>
        public static int UpdateData(string tableName, SqlParameter[] p)
        {
            if (p == null || p.Length == 0 || string.IsNullOrEmpty(tableName))
                return 0;

            string strSql = "update " + tableName + " set ";
            string key = p[0].ParameterName;
            string pName = "";
            for (int i = 1; i < p.Length; i++)
            {
                pName = p[i].ParameterName;
                strSql += pName.Substring(1) + "=" + pName + ",";
            }

            strSql = strSql.TrimEnd(new char[] { ',' });

            strSql += " where " + key.Substring(1) + "=" + key;

            try
            {
                return ado.ExecuteSqlNonQuery(strSql, p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取新的ID号
        /// </summary>
        /// <param name="tableName">表明</param>
        /// <param name="key">ID字段名</param>
        /// <returns></returns>
        public static int GetNewID(string tableName, string key)
        {
            return DBCommon.ParseToInt(ado.ExecuteSqlScalar("select top 1 " + key + " from " + tableName + " order by " + key + " desc")) + 1;//先获取id
        }

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="p">字段（参数值）</param>
        /// <returns></returns>
        public static int InsertData(string tableName, SqlParameter[] p)
        {
            if (p == null || p.Length == 0 || string.IsNullOrEmpty(tableName))
                return 0;

            string keyNames = "", paramNames = "";
            for (int i = 0; i < p.Length; i++)
            {
                keyNames += p[i].ParameterName.Substring(1) + ",";
                paramNames += p[i].ParameterName + ",";
            }
            keyNames = keyNames.TrimEnd(new char[] { ',' });
            paramNames = paramNames.TrimEnd(new char[] { ',' });

            string strSql = "insert into " + tableName + "(" + keyNames + ") values(" + paramNames + ") ";

            try
            {
                return ado.ExecuteSqlNonQuery(strSql, p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="p">字段（参数值）</param>
        /// <returns></returns>
        public static int DeleteDataReally(string tableName, SqlParameter p)
        {
            if (p == null || string.IsNullOrEmpty(tableName))
                return 0;

            string strSql = "delete from " + tableName + " where " + p.ParameterName.Substring(1) + "=" + p.ParameterName;

            return ado.ExecuteSqlNonQuery(strSql, p);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="p">字段（参数值）</param>
        /// <returns></returns>
        public static int DeleteData(string tableName, SqlParameter p)
        {
            if (p == null || string.IsNullOrEmpty(tableName))
                return 0;

            string strSql = "update " + tableName + " set is_delete=1 where " + p.ParameterName.Substring(1) + "=" + p.ParameterName;

            return ado.ExecuteSqlNonQuery(strSql, p);
        }

        /// <summary>
        /// 删除记录(数据库删除)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="p">字段（参数值）</param>
        /// <returns></returns>
        public static int DeleteData(string tableName, SqlParameter p, bool truedelete)
        {
            if (p == null || string.IsNullOrEmpty(tableName))
                return 0;

            string strSql = "delete " + tableName + "  where " + p.ParameterName.Substring(1) + "=" + p.ParameterName;

            return ado.ExecuteSqlNonQuery(strSql, p);
        }

        /// <summary>
        /// 记录详情
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="p">字段（参数值）</param>
        /// <returns></returns>
        public static DataTable DataDetail(string tableName, SqlParameter p)
        {
            if (p == null || string.IsNullOrEmpty(tableName))
                return new DataTable();

            string strSql = "select * from " + tableName + " where (is_delete is null or is_delete=0) and " + p.ParameterName.Substring(1) + "=" + p.ParameterName;

            return ado.ExecuteSqlDataset(strSql, p).Tables[0];
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="p">字段（参数值）</param>
        /// <returns></returns>
        public static DataTable GetDataList(string tableName, string where)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(where))
                return new DataTable();

            string strSql = "select * from " + tableName + " where " + where;

            return ado.ExecuteSqlDataset(strSql).Tables[0];
        }

        /// <summary>
        /// 读取字典表
        /// </summary>
        /// <param name="dict_type">字典类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> SelectDictData(string dict_type)
        {
            if (dict_type == null)
                return new Dictionary<string, string>();

            SqlParameter p = new SqlParameter("@dict_type", dict_type);
            string strSql = "select * from t_base_common_dict where dict_type=@dict_type order by order_num";
            DataTable dt = ado.ExecuteSqlDataset(strSql, p).Tables[0];
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dict.Add(ParseToString(dt.Rows[i]["dict_value"]), ParseToString(dt.Rows[i]["dict_text"]));
            }
            return dict;
        }


        /// <summary>
        /// 委托单监测类别（字典数据）
        /// </summary>
        /// <param name="dict_type">字典类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> SelectContractMonType()
        {
            DataTable dt = ado.ExecuteSqlDataset("select * from t_base_monitor_type order by monitor_order_num").Tables[0];
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dict.Add(ParseToString(dt.Rows[i]["monitor_type_id"]), ParseToString(dt.Rows[i]["monitor_name"]) + "（" + dt.Rows[i]["monitor_code"] + "）");
            }
            return dict;
        }


        /// <summary>
        /// 样品种类（字典数据）
        /// </summary>
        /// <param name="dict_type">字典类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> SelectEntityType()
        {
            DataTable dt = ado.ExecuteSqlDataset("select * from t_base_entity where isnull(is_use,0)=1 and isnull(is_delete,0)=0  order by entity_id").Tables[0];
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dict.Add(ParseToString(dt.Rows[i]["entity_id"]), ParseToString(dt.Rows[i]["entity_name"]));
            }
            return dict;
        }
    }
}
