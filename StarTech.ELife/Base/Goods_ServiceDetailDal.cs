using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;

namespace StarTech.ELife.Base
{
    public class Goods_ServiceDetailDal
    {
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        public Goods_ServiceDetailDal()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Goods_ServiceDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods_ServiceDetail(");
            strSql.Append("sysnumber,serviceId,value,Price,Remark,isDefault)");
            strSql.Append(" values (");
            strSql.Append("@sysnumber,@serviceId,@value,@Price,@Remark,@isDefault)");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@serviceId", SqlDbType.VarChar,50),
					new SqlParameter("@value", SqlDbType.VarChar,50),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
                    new SqlParameter("@isDefault",SqlDbType.Int,4)};
            parameters[0].Value = model.sysnumber;
            parameters[1].Value = model.serviceId;
            parameters[2].Value = model.value;
            parameters[3].Value = model.Price;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.IsDefault;
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
        public bool Update(Goods_ServiceDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Goods_ServiceDetail set ");
            strSql.Append("serviceId=@serviceId,");
            strSql.Append("value=@value,");
            strSql.Append("Price=@Price,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("isDefault=@isDefault");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@serviceId", SqlDbType.VarChar,200),
					new SqlParameter("@value", SqlDbType.VarChar,50),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
                    new SqlParameter("@isDefault",SqlDbType.Int,4),
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)};
            parameters[0].Value = model.serviceId;
            parameters[1].Value = model.value;
            parameters[2].Value = model.Price;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.IsDefault;
            parameters[5].Value = model.sysnumber;

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
        public bool Delete(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods_ServiceDetail ");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)			};
            parameters[0].Value = sysnumber;

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
        public bool DeleteList(string sysnumberlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods_ServiceDetail ");
            strSql.Append(" where sysnumber in (" + sysnumberlist + ")  ");
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

        public Goods_ServiceDetailModel GetModel1(string serviceId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_Goods_ServiceDetail ");
            strSql.Append(" where serviceId=@serviceId ");
            SqlParameter[] parameters = {
					new SqlParameter("@serviceId", SqlDbType.VarChar,200)			};
            parameters[0].Value = serviceId;

            Goods_ServiceDetailModel model = new Goods_ServiceDetailModel();
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
        public Goods_ServiceDetailModel GetModel(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 sysnumber,serviceId,value,Price,Remark,isDefault from T_Goods_ServiceDetail ");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)			};
            parameters[0].Value = sysnumber;

            Goods_ServiceDetailModel model = new Goods_ServiceDetailModel();
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
        public Goods_ServiceDetailModel DataRowToModel(DataRow row)
        {
            Goods_ServiceDetailModel model = new Goods_ServiceDetailModel();
            if (row != null)
            {
                if (row["sysnumber"] != null)
                {
                    model.sysnumber = row["sysnumber"].ToString();
                }
                if (row["serviceId"] != null)
                {
                    model.serviceId = row["serviceId"].ToString();
                }
                if (row["value"] != null)
                {
                    model.value = row["value"].ToString();
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["IsDefault"] != null)
                {
                    model.IsDefault = int.Parse(row["IsDefault"].ToString());
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
            strSql.Append("select sysnumber,serviceId,value,Price,Remark,isDefault ");
            strSql.Append(" FROM T_Goods_ServiceDetail ");
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
            strSql.Append(" sysnumber,serviceId,value,Price,Remark,IsDefault ");
            strSql.Append(" FROM T_Goods_ServiceDetail ");
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
            strSql.Append("select count(1) FROM T_Goods_ServiceDetail ");
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
                strSql.Append("order by T.sysnumber desc");
            }
            strSql.Append(")AS Row, T.*  from T_Goods_ServiceDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }




        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
