using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using StarTech.Util;
using StarTech.DBUtility;


namespace StarTech.ELife.Base
{
    public class AreaDal
    {
        public AreaDal()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(AreaModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Base_Area(");
            strSql.Append("area_id,area_name,area_pid,area_level,orderby)");
            strSql.Append(" values (");
            strSql.Append("@area_id,@area_name,@area_pid,@area_level,@orderby)");
            SqlParameter[] parameters = {
					new SqlParameter("@area_id", SqlDbType.VarChar,50),
					new SqlParameter("@area_name", SqlDbType.VarChar,200),
					new SqlParameter("@area_pid", SqlDbType.VarChar,50),
					new SqlParameter("@area_level", SqlDbType.Int,4),
					new SqlParameter("@orderby", SqlDbType.Int,4)};
            parameters[0].Value = model.area_id;
            parameters[1].Value = model.area_name;
            parameters[2].Value = model.area_pid;
            parameters[3].Value = model.area_level;
            parameters[4].Value = model.orderby;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AreaModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Base_Area set ");
            strSql.Append("area_id=@area_id,");
            strSql.Append("area_name=@area_name,");
            strSql.Append("area_pid=@area_pid,");
            strSql.Append("area_level=@area_level,");
            strSql.Append("orderby=@orderby");
            strSql.Append(" where area_id=@area_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@area_id", SqlDbType.VarChar,50),
					new SqlParameter("@area_name", SqlDbType.VarChar,200),
					new SqlParameter("@area_pid", SqlDbType.VarChar,50),
					new SqlParameter("@area_level", SqlDbType.Int,4),
					new SqlParameter("@orderby", SqlDbType.Int,4)};
            parameters[0].Value = model.area_id;
            parameters[1].Value = model.area_name;
            parameters[2].Value = model.area_pid;
            parameters[3].Value = model.area_level;
            parameters[4].Value = model.orderby;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新编号
        /// </summary>
        /// <param name="newId"></param>
        /// <param name="oldID"></param>
        /// <returns></returns>
        public bool UpdateId(string newId, string oldID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Base_Area set ");
            strSql.Append("area_id=@newArea_id");
            strSql.Append(" where area_id=@oldArea_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@newArea_id", SqlDbType.VarChar,50),
					new SqlParameter("@oldArea_id", SqlDbType.VarChar,50)};
            parameters[0].Value = newId;
            parameters[1].Value = oldID;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string area_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Base_Area ");
            strSql.Append(" where area_id=@area_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@area_id", SqlDbType.VarChar,50)			};
            parameters[0].Value = area_id;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string area_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Base_Area ");
            strSql.Append(" where area_id in (" + area_idlist + ")  ");
            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AreaModel GetModel(string area_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 area_id,area_name,area_pid,area_level,orderby from T_Base_Area ");
            strSql.Append(" where area_id=@area_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@area_id", SqlDbType.VarChar,50)			};
            parameters[0].Value = area_id;

            AreaModel model = new AreaModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AreaModel DataRowToModel(DataRow row)
        {
            AreaModel model = new AreaModel();
            if (row != null)
            {
                if (row["area_id"] != null)
                {
                    model.area_id = row["area_id"].ToString();
                }
                if (row["area_name"] != null)
                {
                    model.area_name = row["area_name"].ToString();
                }
                if (row["area_pid"] != null)
                {
                    model.area_pid = row["area_pid"].ToString();
                }
                if (row["area_level"] != null && row["area_level"].ToString() != "")
                {
                    model.area_level = int.Parse(row["area_level"].ToString());
                }
                if (row["orderby"] != null && row["orderby"].ToString() != "")
                {
                    model.orderby = int.Parse(row["orderby"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select area_id,area_name,area_pid,area_level,orderby ");
            strSql.Append(" FROM T_Base_Area ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" area_id,area_name,area_pid,area_level,orderby ");
            strSql.Append(" FROM T_Base_Area ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_Base_Area ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = adoHelper.ExecuteSqlScalar(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.area_id desc");
            }
            strSql.Append(")AS Row, T.*  from T_Base_Area T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "T_Base_Area";
            parameters[1].Value = "area_id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
