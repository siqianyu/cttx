using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;

namespace StarTech.ELife.Base
{
    public class GoodsDal
    {
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(GoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods_Service(");
            strSql.Append("serviceId,serviceName,serviceContext,orderby,remark)");
            strSql.Append(" values (");
            strSql.Append("@serviceId,@serviceName,@serviceContext,@orderby,@remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@serviceId", SqlDbType.VarChar,50),
					new SqlParameter("@serviceName", SqlDbType.VarChar,50),
					new SqlParameter("@serviceContext", SqlDbType.VarChar,100),
					new SqlParameter("@orderby", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.VarChar,500)};
            parameters[0].Value = model.serviceId;
            parameters[1].Value = model.serviceName;
            parameters[2].Value = model.serviceContext;
            parameters[3].Value = model.orderby;
            parameters[4].Value = model.remark;
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
        public bool Update(GoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Goods_Service set ");
            strSql.Append("serviceName=@serviceName,");
            strSql.Append("serviceContext=@serviceContext,");
            strSql.Append("orderby=@orderby,");
            strSql.Append("remark=@remark");
            strSql.Append(" where serviceId=@serviceId");
            SqlParameter[] parameters = {
					new SqlParameter("@serviceName", SqlDbType.VarChar,50),
					new SqlParameter("@serviceContext", SqlDbType.VarChar,100),
					new SqlParameter("@orderby", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.VarChar,500),
					new SqlParameter("@serviceId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.serviceName;
            parameters[1].Value = model.serviceContext;
            parameters[2].Value = model.orderby;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.serviceId;

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
        public bool Delete(string serviceId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods_Service ");
            strSql.Append(" where serviceId=@serviceId");
            SqlParameter[] parameters = {
					new SqlParameter("@serviceId", SqlDbType.VarChar,50)
			};
            parameters[0].Value = serviceId;

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
        public bool DeleteList(string serviceIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods_Service ");
            strSql.Append(" where serviceId in (" + serviceIdlist + ")  ");
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
        public GoodsModel GetModel(string serviceId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 serviceId,serviceName,serviceContext,orderby,remark from T_Goods_Service ");
            strSql.Append(" where serviceId=@serviceId");
            SqlParameter[] parameters = {
					new SqlParameter("@serviceId", SqlDbType.VarChar,50)
			};
            parameters[0].Value = serviceId;

            GoodsModel model = new GoodsModel();
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
        public GoodsModel DataRowToModel(DataRow row)
        {
            GoodsModel model = new GoodsModel();
            if (row != null)
            {
                if (row["serviceId"] != null && row["serviceId"].ToString() != "")
                {
                    model.serviceId = row["serviceId"].ToString();
                }
                if (row["serviceName"] != null)
                {
                    model.serviceName = row["serviceName"].ToString();
                }
                if (row["serviceContext"] != null)
                {
                    model.serviceContext = row["serviceContext"].ToString();
                }
                if (row["orderby"] != null && row["orderby"].ToString() != "")
                {
                    model.orderby = int.Parse(row["orderby"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
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
            strSql.Append("select serviceId,serviceName,serviceContext,orderby,remark ");
            strSql.Append(" FROM T_Goods_Service ");
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
            strSql.Append(" serviceId,serviceName,serviceContext,orderby,remark ");
            strSql.Append(" FROM T_Goods_Service ");
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
            strSql.Append("select count(1) FROM T_Goods_Service ");
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
                strSql.Append("order by T.serviceId desc");
            }
            strSql.Append(")AS Row, T.*  from T_Goods_Service T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

    }
}
