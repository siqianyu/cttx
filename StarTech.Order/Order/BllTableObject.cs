using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;

namespace StarTech.Order.Order
{
    /// <summary>
    /// 抽象基类
    /// </summary>
    public class BllTableObject
    {
        private string _tableMame;

        public BllTableObject(string tableName)
        {
            this._tableMame = tableName;
        }

        public string CurrentTableName()
        {
            if (this._tableMame == null || this._tableMame == "")
            {
                throw new Exception("表名不能为空");
            }
            return this._tableMame;
        }

        #region 通用批量操作
        /// <summary>
        /// 批量更新数据
        /// </summary>
        public int Util_UpdateBat(string updateDesc, string updateFilter)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sql = "update " + CurrentTableName() + " set " + updateDesc + " where " + updateFilter + "";
            return adoHelper.ExecuteSqlNonQuery(sql);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int Util_DeleteBat(string filter)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sql = "delete " + CurrentTableName() + "  where " + filter + "";
            return adoHelper.ExecuteSqlNonQuery(sql);
        }

        /// <summary>
        /// 判断某条数据是否存在
        /// </summary>
        public bool Util_CheckIsExsitData(string filter)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sql = "select count(*) count1 from " + CurrentTableName() + "  where " + filter + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            if (ds.Tables[0].Rows[0]["count1"].ToString() == "0") { return false; }
            return true;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Util_GetList(string fields, string fielter)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sql = "select " + fields + "  from " + CurrentTableName() + " where " + fielter + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Util_GetList(string fields, string fielter, string sort)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sql = "select " + fields + "  from " + CurrentTableName() + " where " + fielter + " order by " + sort + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Util_GetList(string fields, string fielter, string sort, int top)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sql = "select top " + top + " " + fields + "  from " + CurrentTableName() + " where " + fielter + " order by " + sort + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得数据列表并且排序
        /// </summary>
        public DataTable Util_GetGroupByList(string groupbyDesc, string fielter)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sql = "select " + groupbyDesc + "  from " + CurrentTableName() + " where " + fielter + " group by  " + groupbyDesc + "";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            return ds.Tables[0];
        }

        /// <summary>    
        /// 统计某个字段的总和
        /// </summary>
        public DataTable Util_SumFields(string fields, string fielter)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string fieldsDesc = "";
            string[] fieldsArr = fields.Split(',');
            for (int i = 0; i < fieldsArr.Length; i++)
            {
                fieldsDesc += "nvl(sum(" + fieldsArr[i] + "),0) " + fieldsArr[i] + ",";
            }
            if (fieldsDesc != "")
            {
                fieldsDesc = fieldsDesc.Substring(0, fieldsDesc.Length - 1);
                string sql = "select " + fieldsDesc + " from " + CurrentTableName() + " ";
                if (fielter != "") { sql += " where " + fielter; }
                DataSet ds = adoHelper.ExecuteSqlDataset(sql);
                return ds.Tables[0];
            }
            else { return null; }
        }
        #endregion
    }
}
