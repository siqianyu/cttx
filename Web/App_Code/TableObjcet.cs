using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;

namespace NGShop.Bll
{
    public class TableObject
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        private string tableName;

        public TableObject(string _tableName)
        {
            this.tableName = _tableName;
        }

        #region 通用批量操作
        /// <summary>
        /// 批量更新数据
        /// </summary>
        public int Util_UpdateBat(string updateDesc, string updateFilter)
        {
            string sql = "update " + this.tableName + " set " + updateDesc + " where " + updateFilter + "";
            return adoHelper.ExecuteSqlNonQuery(sql);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int Util_DeleteBat(string filter)
        {

            string sql = "delete " + this.tableName + "  where " + filter + "";
            return adoHelper.ExecuteSqlNonQuery(sql);
        }

        /// <summary>
        /// 判断某条数据是否存在
        /// </summary>
        public bool Util_CheckIsExsitData(string filter)
        {

            string sql = "select count(*) count1 from " + this.tableName + "  where " + filter + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            if (ds.Tables[0].Rows[0]["count1"].ToString() == "0") { return false; }
            return true;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Util_GetList(string fields, string fielter)
        {

            string sql = "select " + fields + "  from " + this.tableName + " where " + fielter + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Util_GetList(string fields, string fielter, string sort)
        {

            string sql = "select " + fields + "  from " + this.tableName + " where " + fielter + " order by " + sort + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Util_GetList(string fields, string fielter, string sort, int top)
        {

            string sql = "select top " + top + " " + fields + "  from " + this.tableName + " where " + fielter + " order by " + sort + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得数据列表并且排序
        /// </summary>
        public DataTable Util_GetGroupByList(string groupbyDesc, string fielter)
        {

            string sql = "select " + groupbyDesc + "  from " + this.tableName + " where " + fielter + " group by  " + groupbyDesc + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>    
        /// 统计某个字段的总和
        /// </summary>
        public DataTable Util_SumFields(string fields, string fielter)
        {

            string fieldsDesc = "";
            string[] fieldsArr = fields.Split(',');
            for (int i = 0; i < fieldsArr.Length; i++)
            {
                fieldsDesc += "isnull(sum(" + fieldsArr[i] + "),0) " + fieldsArr[i] + ",";
            }
            if (fieldsDesc != "")
            {
                fieldsDesc = fieldsDesc.Substring(0, fieldsDesc.Length - 1);
                string sql = "select " + fieldsDesc + " from " + this.tableName + " ";
                if (fielter != "") { sql += " where " + fielter; }
                DataSet ds = adoHelper.ExecuteSqlDataset(sql);
                return ds.Tables[0];
            }
            else { return null; }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        public int ExecuteSqlNonQuery(string sql)
        {
            return adoHelper.ExecuteSqlNonQuery(sql);
        }
        #endregion
    }
}
