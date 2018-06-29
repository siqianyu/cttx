using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.Order;
using StarTech.DBUtility;

namespace StarTech.Order
{
    public class DalOrderDetail
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModOrderDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Order_InfoDetail(");
            strSql.Append("sysnumber,OrderId,OrderType,GoodsId,GoodsCode,GoodsName,GoodsFormate,GoodsPic,Quantity,Price,AllMoney,OneWeight,AllWeight,MarketPrice,CBPrice,Remarks,FreightByWeight,ProviderInfo,DataFrom)");
            strSql.Append(" values (");
            strSql.Append("@sysnumber,@OrderId,@OrderType,@GoodsId,@GoodsCode,@GoodsName,@GoodsFormate,@GoodsPic,@Quantity,@Price,@AllMoney,@OneWeight,@AllWeight,@MarketPrice,@CBPrice,@Remarks,@FreightByWeight,@ProviderInfo,@DataFrom)");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@OrderId", SqlDbType.VarChar,50),
					new SqlParameter("@OrderType", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsName", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsFormate", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsPic", SqlDbType.VarChar,500),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@AllMoney", SqlDbType.Decimal,9),
					new SqlParameter("@OneWeight", SqlDbType.Int,4),
					new SqlParameter("@AllWeight", SqlDbType.Int,4),
					new SqlParameter("@MarketPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CBPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remarks", SqlDbType.VarChar,500),
                    new SqlParameter("@FreightByWeight", SqlDbType.Decimal,9),
                    new SqlParameter("@ProviderInfo", SqlDbType.VarChar,500),
                    new SqlParameter("@DataFrom", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.sysnumber;
            parameters[1].Value = model.OrderId;
            parameters[2].Value = model.OrderType;
            parameters[3].Value = model.GoodsId;
            parameters[4].Value = model.GoodsCode;
            parameters[5].Value = model.GoodsName;
            parameters[6].Value = model.GoodsFormate;
            parameters[7].Value = model.GoodsPic;
            parameters[8].Value = model.Quantity;
            parameters[9].Value = model.Price;
            parameters[10].Value = model.AllMoney;
            parameters[11].Value = model.OneWeight;
            parameters[12].Value = model.AllWeight;
            parameters[13].Value = model.MarketPrice;
            parameters[14].Value = model.CBPrice;
            parameters[15].Value = model.Remarks;
            parameters[16].Value = model.FreightByWeight;
            parameters[17].Value = model.ProviderInfo;
            parameters[18].Value = model.DataFrom;


            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModOrderDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Order_InfoDetail set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("OrderType=@OrderType,");
            strSql.Append("GoodsId=@GoodsId,");
            strSql.Append("GoodsCode=@GoodsCode,");
            strSql.Append("GoodsName=@GoodsName,");
            strSql.Append("GoodsFormate=@GoodsFormate,");
            strSql.Append("GoodsPic=@GoodsPic,");
            strSql.Append("Quantity=@Quantity,");
            strSql.Append("Price=@Price,");
            strSql.Append("AllMoney=@AllMoney,");
            strSql.Append("OneWeight=@OneWeight,");
            strSql.Append("AllWeight=@AllWeight,");
            strSql.Append("MarketPrice=@MarketPrice,");
            strSql.Append("CBPrice=@CBPrice,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.VarChar,50),
					new SqlParameter("@OrderType", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsName", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsFormate", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsPic", SqlDbType.VarChar,500),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@AllMoney", SqlDbType.Decimal,9),
					new SqlParameter("@OneWeight", SqlDbType.Int,4),
					new SqlParameter("@AllWeight", SqlDbType.Int,4),
					new SqlParameter("@MarketPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CBPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Remarks", SqlDbType.VarChar,500),
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderType;
            parameters[2].Value = model.GoodsId;
            parameters[3].Value = model.GoodsCode;
            parameters[4].Value = model.GoodsName;
            parameters[5].Value = model.GoodsFormate;
            parameters[6].Value = model.GoodsPic;
            parameters[7].Value = model.Quantity;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.AllMoney;
            parameters[10].Value = model.OneWeight;
            parameters[11].Value = model.AllWeight;
            parameters[12].Value = model.MarketPrice;
            parameters[13].Value = model.CBPrice;
            parameters[14].Value = model.Remarks;
            parameters[15].Value = model.sysnumber;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);


        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModOrderDetail GetModel(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_Order_InfoDetail ");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)};
            parameters[0].Value = sysnumber;

            ModOrderDetail model = new ModOrderDetail();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.sysnumber = ds.Tables[0].Rows[0]["sysnumber"].ToString();
                model.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                model.OrderType = ds.Tables[0].Rows[0]["OrderType"].ToString();
                model.GoodsId = ds.Tables[0].Rows[0]["GoodsId"].ToString();
                model.GoodsCode = ds.Tables[0].Rows[0]["GoodsCode"].ToString();
                model.GoodsName = ds.Tables[0].Rows[0]["GoodsName"].ToString();
                model.GoodsFormate = ds.Tables[0].Rows[0]["GoodsFormate"].ToString();
                model.GoodsPic = ds.Tables[0].Rows[0]["GoodsPic"].ToString();
                if (ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
                {
                    model.Quantity = int.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AllMoney"].ToString() != "")
                {
                    model.AllMoney = decimal.Parse(ds.Tables[0].Rows[0]["AllMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OneWeight"].ToString() != "")
                {
                    model.OneWeight = int.Parse(ds.Tables[0].Rows[0]["OneWeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AllWeight"].ToString() != "")
                {
                    model.AllWeight = int.Parse(ds.Tables[0].Rows[0]["AllWeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MarketPrice"].ToString() != "")
                {
                    model.MarketPrice = decimal.Parse(ds.Tables[0].Rows[0]["MarketPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBPrice"].ToString() != "")
                {
                    model.CBPrice = decimal.Parse(ds.Tables[0].Rows[0]["CBPrice"].ToString());
                }
                model.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

    }
}
