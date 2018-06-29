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
    public class MarketDal
    {
        public MarketDal()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MarketModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Base_Market(");
            strSql.Append("Market_id,Market_name,Area_id,Map_x,Map_y,orderby)");
            strSql.Append(" values (");
            strSql.Append("@Market_id,@Market_name,@Area_id,@Map_x,@Map_y,@orderby)");
            SqlParameter[] parameters = {
					new SqlParameter("@Market_id", SqlDbType.VarChar,50),
					new SqlParameter("@Market_name", SqlDbType.VarChar,200),
					new SqlParameter("@Area_id", SqlDbType.VarChar,50),
					new SqlParameter("@Map_x", SqlDbType.VarChar,100),
					new SqlParameter("@Map_y", SqlDbType.VarChar,100),
					new SqlParameter("@orderby", SqlDbType.Int,4)};
            parameters[0].Value = model.Market_id;
            parameters[1].Value = model.Market_name;
            parameters[2].Value = model.Area_id;
            parameters[3].Value = model.Map_x;
            parameters[4].Value = model.Map_y;
            parameters[5].Value = model.orderby;

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
        public bool Update(MarketModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Base_Market set ");
            strSql.Append("Market_id=@Market_id,");
            strSql.Append("Market_name=@Market_name,");
            strSql.Append("Area_id=@Area_id,");
            strSql.Append("Map_x=@Map_x,");
            strSql.Append("Map_y=@Map_y,");
            strSql.Append("orderby=@orderby");
            strSql.Append(" where Market_id=@Market_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Market_id", SqlDbType.VarChar,50),
					new SqlParameter("@Market_name", SqlDbType.VarChar,200),
					new SqlParameter("@Area_id", SqlDbType.VarChar,50),
					new SqlParameter("@Map_x", SqlDbType.VarChar,100),
					new SqlParameter("@Map_y", SqlDbType.VarChar,100),
					new SqlParameter("@orderby", SqlDbType.Int,4)};
            parameters[0].Value = model.Market_id;
            parameters[1].Value = model.Market_name;
            parameters[2].Value = model.Area_id;
            parameters[3].Value = model.Map_x;
            parameters[4].Value = model.Map_y;
            parameters[5].Value = model.orderby;

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
        /// <param name="oldId"></param>
        /// <returns></returns>
        public bool UpdateId(string newId, string oldId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Base_Market set ");
            strSql.Append("Market_id=@newMarket_id");
            strSql.Append(" where Market_id=@oldMarket_id ");
             SqlParameter[] parameters = {
					new SqlParameter("@newMarket_id", SqlDbType.VarChar,50),
                    new SqlParameter("@oldMarket_id", SqlDbType.VarChar,50)};
             parameters[0].Value = newId;
             parameters[1].Value = oldId;
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
        public bool Delete(string Market_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Base_Market ");
            strSql.Append(" where Market_id=@Market_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Market_id", SqlDbType.VarChar,50)			};
            parameters[0].Value = Market_id;

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
        public bool DeleteList(string Market_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Base_Market ");
            strSql.Append(" where Market_id in (" + Market_idlist + ")  ");
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
        public MarketModel GetModel(string Market_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Market_id,Market_name,Area_id,Map_x,Map_y,orderby from T_Base_Market ");
            strSql.Append(" where Market_id=@Market_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Market_id", SqlDbType.VarChar,50)			};
            parameters[0].Value = Market_id;

            MarketModel model = new MarketModel();
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
        public MarketModel DataRowToModel(DataRow row)
        {
            MarketModel model = new MarketModel();
            if (row != null)
            {
                if (row["Market_id"] != null)
                {
                    model.Market_id = row["Market_id"].ToString();
                }
                if (row["Market_name"] != null)
                {
                    model.Market_name = row["Market_name"].ToString();
                }
                if (row["Area_id"] != null)
                {
                    model.Area_id = row["Area_id"].ToString();
                }
                if (row["Map_x"] != null)
                {
                    model.Map_x = row["Map_x"].ToString();
                }
                if (row["Map_y"] != null)
                {
                    model.Map_y = row["Map_y"].ToString();
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
            strSql.Append("select Market_id,Market_name,Area_id,Map_x,Map_y,orderby ");
            strSql.Append(" FROM T_Base_Market ");
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
            strSql.Append(" Market_id,Market_name,Area_id,Map_x,Map_y,orderby ");
            strSql.Append(" FROM T_Base_Market ");
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
            strSql.Append("select count(1) FROM T_Base_Market ");
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
                strSql.Append("order by T.Market_id desc");
            }
            strSql.Append(")AS Row, T.*  from T_Base_Market T ");
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
            parameters[0].Value = "T_Base_Market";
            parameters[1].Value = "Market_id";
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
