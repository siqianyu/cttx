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
    public class BuildingToMarketDal
    {
        public BuildingToMarketDal()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(BuildingToMarketModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Base_BuildingToMarket(");
            strSql.Append("BuildingToMarket_id,Building_id,Market_id,orderby,MinPrice,MaxPrice,Price,Distance)");
            strSql.Append(" values (");
            strSql.Append("@BuildingToMarket_id,@Building_id,@Market_id,@orderby,@MinPrice,@MaxPrice,@Price,@Distance)");
            SqlParameter[] parameters = {
					new SqlParameter("@BuildingToMarket_id", SqlDbType.VarChar,50),
					new SqlParameter("@Building_id", SqlDbType.VarChar,50),
					new SqlParameter("@Market_id", SqlDbType.VarChar,50),
					new SqlParameter("@orderby", SqlDbType.Int,4),
					new SqlParameter("@MinPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MaxPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Distance", SqlDbType.Int,4)};
            parameters[0].Value = model.BuildingToMarket_id;
            parameters[1].Value = model.Building_id;
            parameters[2].Value = model.Market_id;
            parameters[3].Value = model.orderby;
            parameters[4].Value = model.MinPrice;
            parameters[5].Value = model.MaxPrice;
            parameters[6].Value = model.Price;
            parameters[7].Value = model.Distance;

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
        public bool Update(BuildingToMarketModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Base_BuildingToMarket set ");
            strSql.Append("BuildingToMarket_id=@BuildingToMarket_id,");
            strSql.Append("Building_id=@Building_id,");
            strSql.Append("Market_id=@Market_id,");
            strSql.Append("orderby=@orderby,");
            strSql.Append("MinPrice=@MinPrice,");
            strSql.Append("MaxPrice=@MaxPrice,");
            strSql.Append("Price=@Price,");
            strSql.Append("Distance=@Distance");
            strSql.Append(" where BuildingToMarket_id=@BuildingToMarket_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuildingToMarket_id", SqlDbType.VarChar,50),
					new SqlParameter("@Building_id", SqlDbType.VarChar,50),
					new SqlParameter("@Market_id", SqlDbType.VarChar,50),
					new SqlParameter("@orderby", SqlDbType.Int,4),
					new SqlParameter("@MinPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MaxPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Distance", SqlDbType.Int,4)};
            parameters[0].Value = model.BuildingToMarket_id;
            parameters[1].Value = model.Building_id;
            parameters[2].Value = model.Market_id;
            parameters[3].Value = model.orderby;
            parameters[4].Value = model.MinPrice;
            parameters[5].Value = model.MaxPrice;
            parameters[6].Value = model.Price;
            parameters[7].Value = model.Distance;

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
        public bool Delete(string BuildingToMarket_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Base_BuildingToMarket ");
            strSql.Append(" where BuildingToMarket_id=@BuildingToMarket_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuildingToMarket_id", SqlDbType.VarChar,50)			};
            parameters[0].Value = BuildingToMarket_id;

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
        public bool DeleteList(string BuildingToMarket_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Base_BuildingToMarket ");
            strSql.Append(" where BuildingToMarket_id in (" + BuildingToMarket_idlist + ")  ");
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
        public BuildingToMarketModel GetModel(string BuildingToMarket_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BuildingToMarket_id,Building_id,Market_id,orderby,MinPrice,MaxPrice,Price,Distance from T_Base_BuildingToMarket ");
            strSql.Append(" where BuildingToMarket_id=@BuildingToMarket_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuildingToMarket_id", SqlDbType.VarChar,50)			};
            parameters[0].Value = BuildingToMarket_id;

            BuildingToMarketModel model = new BuildingToMarketModel();
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
        public BuildingToMarketModel DataRowToModel(DataRow row)
        {
            BuildingToMarketModel model = new BuildingToMarketModel();
            if (row != null)
            {
                if (row["BuildingToMarket_id"] != null)
                {
                    model.BuildingToMarket_id = row["BuildingToMarket_id"].ToString();
                }
                if (row["Building_id"] != null)
                {
                    model.Building_id = row["Building_id"].ToString();
                }
                if (row["Market_id"] != null)
                {
                    model.Market_id = row["Market_id"].ToString();
                }
                if (row["orderby"] != null && row["orderby"].ToString() != "")
                {
                    model.orderby = int.Parse(row["orderby"].ToString());
                }
                if (row["MinPrice"] != null && row["MinPrice"].ToString() != "")
                {
                    model.MinPrice = decimal.Parse(row["MinPrice"].ToString());
                }
                if (row["MaxPrice"] != null && row["MaxPrice"].ToString() != "")
                {
                    model.MaxPrice = decimal.Parse(row["MaxPrice"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["Distance"] != null && row["Distance"].ToString() != "")
                {
                    model.Distance = int.Parse(row["Distance"].ToString());
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
            strSql.Append("select BuildingToMarket_id,Building_id,Market_id,orderby,MinPrice,MaxPrice,Price,Distance ");
            strSql.Append(" FROM T_Base_BuildingToMarket ");
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
            strSql.Append(" BuildingToMarket_id,Building_id,Market_id,orderby,MinPrice,MaxPrice,Price,Distance ");
            strSql.Append(" FROM T_Base_BuildingToMarket ");
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
            strSql.Append("select count(1) FROM T_Base_BuildingToMarket ");
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
                strSql.Append("order by T.BuildingToMarket_id desc");
            }
            strSql.Append(")AS Row, T.*  from T_Base_BuildingToMarket T ");
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
            parameters[0].Value = "T_Base_BuildingToMarket";
            parameters[1].Value = "BuildingToMarket_id";
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
