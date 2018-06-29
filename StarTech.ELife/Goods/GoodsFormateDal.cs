using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using StarTech.DBUtility;

namespace StarTech.ELife.Goods
{
    public class GoodsFormateDal
    {

        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(GoodsFormateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods_Formate(");
            strSql.Append("sysnumber,GoodsId,GoodsCode,GoodsFormateNames,GoodsFormateValues,Price,Stock,vipPrice1,vipPrice2,postage)");
            strSql.Append(" values (");
            strSql.Append("@sysnumber,@GoodsId,@GoodsCode,@GoodsFormateNames,@GoodsFormateValues,@Price,@Stock,@vipPrice1,@vipPrice2,@postage)");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsFormateNames", SqlDbType.NVarChar,500),
					new SqlParameter("@GoodsFormateValues", SqlDbType.VarChar,500),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Stock", SqlDbType.Int,4),
                    new SqlParameter("@vipPrice1",SqlDbType.Decimal,18),
                    new SqlParameter("@vipPrice2",SqlDbType.Decimal,18),
                    new SqlParameter("@postage",SqlDbType.Decimal,18)
                                        };
            parameters[0].Value = model.sysnumber;
            parameters[1].Value = model.GoodsId;
            parameters[2].Value = model.GoodsCode;
            parameters[3].Value = model.GoodsFormateNames;
            parameters[4].Value = model.GoodsFormateValues;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.Stock;
            parameters[7].Value = model.vipPrice1;
            parameters[8].Value = model.vipPrice2;
            parameters[9].Value = model.Postage;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(GoodsFormateModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Goods_Formate set ");
            strSql.Append("GoodsId=@GoodsId,");
            strSql.Append("GoodsCode=@GoodsCode,");
            strSql.Append("GoodsFormateNames=@GoodsFormateNames,");
            strSql.Append("GoodsFormateValues=@GoodsFormateValues,");
            strSql.Append("Price=@Price,");
            strSql.Append("Stock=@Stock,");
            strSql.Append("vipPrice1=@vipPrice1,");
            strSql.Append("vipPrice2=@vipPrice2,");
            strSql.Append("postage=@postage");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsFormateNames", SqlDbType.NVarChar,500),
					new SqlParameter("@GoodsFormateValues", SqlDbType.VarChar,500),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Stock", SqlDbType.Int,4),
                    new SqlParameter("@vipPrice1",SqlDbType.Decimal,18),
                    new SqlParameter("@vipPrice2",SqlDbType.Decimal,18),
                    new SqlParameter("@postage",SqlDbType.Decimal,18),
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)};
            parameters[0].Value = model.GoodsId;
            parameters[1].Value = model.GoodsCode;
            parameters[2].Value = model.GoodsFormateNames;
            parameters[3].Value = model.GoodsFormateValues;
            parameters[4].Value = model.Price;
            parameters[5].Value = model.Stock;
            parameters[6].Value = model.vipPrice1;
            parameters[7].Value = model.vipPrice2;
            parameters[8].Value = model.Postage;
            parameters[9].Value = model.sysnumber;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);


        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public GoodsFormateModel GetModel(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_Goods_Formate ");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)};
            parameters[0].Value = sysnumber;

            GoodsFormateModel model = new GoodsFormateModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.sysnumber = ds.Tables[0].Rows[0]["sysnumber"].ToString();
                model.GoodsId = ds.Tables[0].Rows[0]["GoodsId"].ToString();
                model.GoodsCode = ds.Tables[0].Rows[0]["GoodsCode"].ToString();
                model.GoodsFormateNames = ds.Tables[0].Rows[0]["GoodsFormateNames"].ToString();
                model.GoodsFormateValues = ds.Tables[0].Rows[0]["GoodsFormateValues"].ToString();

                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Stock"].ToString() != "")
                {
                    model.Stock = int.Parse(ds.Tables[0].Rows[0]["Stock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["vipPrice1"].ToString() != "")
                {
                    model.vipPrice1 = Convert.ToDecimal(ds.Tables[0].Rows[0]["vipPrice1"]);
                }
                if (ds.Tables[0].Rows[0]["vipPrice2"].ToString() != "")
                {
                    model.vipPrice2 = Convert.ToDecimal(ds.Tables[0].Rows[0]["vipPrice2"]);
                }
                if (ds.Tables[0].Rows[0]["postage"].ToString() != "")
                {
                    model.Postage = Convert.ToDecimal(ds.Tables[0].Rows[0]["postage"]);
                }
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
