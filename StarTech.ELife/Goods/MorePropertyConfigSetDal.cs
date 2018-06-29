using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;
using StarTech.Util;

namespace StarTech.ELife.Goods
{
    public class MorePropertyConfigSetDal
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper(AppConfig.DBInstance);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MorePropertyConfigSetModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods_MorePropertySet(");
            strSql.Append("propertyId,tableName,moreFiledName,propertyName,porpertyFlag,propertyOptions,orderBy,remarks)");
            strSql.Append(" values (");
            strSql.Append("@propertyId,@tableName,@moreFiledName,@propertyName,@porpertyFlag,@propertyOptions,@orderBy,@remarks)");
            SqlParameter[] parameters = {
					new SqlParameter("@propertyId", SqlDbType.VarChar,50),
					new SqlParameter("@tableName", SqlDbType.VarChar,50),
					new SqlParameter("@moreFiledName", SqlDbType.VarChar,50),
					new SqlParameter("@propertyName", SqlDbType.VarChar,50),
					new SqlParameter("@porpertyFlag", SqlDbType.VarChar,50),
					new SqlParameter("@propertyOptions", SqlDbType.VarChar,2000),
					new SqlParameter("@orderBy", SqlDbType.Int,4),
					new SqlParameter("@remarks", SqlDbType.VarChar,2000)};
            parameters[0].Value = model.propertyId;
            parameters[1].Value = model.tableName;
            parameters[2].Value = model.moreFiledName;
            parameters[3].Value = model.propertyName;
            parameters[4].Value = model.porpertyFlag;
            parameters[5].Value = model.propertyOptions;
            parameters[6].Value = model.orderBy;
            parameters[7].Value = model.remarks;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(MorePropertyConfigSetModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Goods_MorePropertySet set ");
            strSql.Append("tableName=@tableName,");
            strSql.Append("moreFiledName=@moreFiledName,");
            strSql.Append("propertyName=@propertyName,");
            strSql.Append("porpertyFlag=@porpertyFlag,");
            strSql.Append("propertyOptions=@propertyOptions,");
            strSql.Append("orderBy=@orderBy,");
            strSql.Append("remarks=@remarks");
            strSql.Append(" where propertyId=@propertyId ");
            SqlParameter[] parameters = {
					new SqlParameter("@tableName", SqlDbType.VarChar,50),
					new SqlParameter("@moreFiledName", SqlDbType.VarChar,50),
					new SqlParameter("@propertyName", SqlDbType.VarChar,50),
					new SqlParameter("@porpertyFlag", SqlDbType.VarChar,50),
					new SqlParameter("@propertyOptions", SqlDbType.VarChar,2000),
					new SqlParameter("@orderBy", SqlDbType.Int,4),
					new SqlParameter("@remarks", SqlDbType.VarChar,2000),
					new SqlParameter("@propertyId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.tableName;
            parameters[1].Value = model.moreFiledName;
            parameters[2].Value = model.propertyName;
            parameters[3].Value = model.porpertyFlag;
            parameters[4].Value = model.propertyOptions;
            parameters[5].Value = model.orderBy;
            parameters[6].Value = model.remarks;
            parameters[7].Value = model.propertyId;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);


        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MorePropertyConfigSetModel GetModel(string propertyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Goods_MorePropertySet ");
            strSql.Append(" where propertyId=@propertyId  ");
            SqlParameter[] parameters = {
					new SqlParameter("@propertyId", SqlDbType.VarChar,50)};
            parameters[0].Value =propertyId;

            MorePropertyConfigSetModel model = new MorePropertyConfigSetModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.propertyId = ds.Tables[0].Rows[0]["propertyId"].ToString();
                model.tableName = ds.Tables[0].Rows[0]["tableName"].ToString();
                model.moreFiledName = ds.Tables[0].Rows[0]["moreFiledName"].ToString();
                model.propertyName = ds.Tables[0].Rows[0]["propertyName"].ToString();
                model.porpertyFlag = ds.Tables[0].Rows[0]["porpertyFlag"].ToString();
                model.propertyOptions = ds.Tables[0].Rows[0]["propertyOptions"].ToString();
                if (ds.Tables[0].Rows[0]["orderBy"].ToString() != "")
                {
                    model.orderBy = int.Parse(ds.Tables[0].Rows[0]["orderBy"].ToString());
                }
                model.remarks = ds.Tables[0].Rows[0]["remarks"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

    }
}
