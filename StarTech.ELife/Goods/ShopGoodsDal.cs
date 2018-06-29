using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace StarTech.ELife.Goods
{
    public class ShopGoodsDal
    {
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ShopGoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Shop_Goods(");
            strSql.Append("shopgoods_id,shopid,goodsid,shopgoods_amount,shopgoods_selfPrice,shopgoods_isSell,shopgoods_addtime,goodsCode,isFormate,saleNum,shopgoods_isSet,shopgoods_isPass)");
            strSql.Append(" values (");
            strSql.Append("@shopgoods_id,@shopid,@goodsid,@shopgoods_amount,@shopgoods_selfPrice,@shopgoods_isSell,@shopgoods_addtime,@goodsCode,@isFormate,@saleNum,@shopgoods_isSet,@shopgoods_isPass)");
            SqlParameter[] parameters = {
					new SqlParameter("@shopgoods_id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@shopid", SqlDbType.VarChar,50),
					new SqlParameter("@goodsid", SqlDbType.VarChar,50),
					new SqlParameter("@shopgoods_amount", SqlDbType.Int,4),
					new SqlParameter("@shopgoods_selfPrice", SqlDbType.Decimal,9),
					new SqlParameter("@shopgoods_isSell", SqlDbType.VarChar,50),
					new SqlParameter("@shopgoods_addtime", SqlDbType.DateTime),
					new SqlParameter("@goodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@isFormate", SqlDbType.Int,4),
					new SqlParameter("@saleNum", SqlDbType.Int,4),
					new SqlParameter("@shopgoods_isSet", SqlDbType.Int,4),
					new SqlParameter("@shopgoods_isPass", SqlDbType.Int,4)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.shopid;
            parameters[2].Value = model.goodsid;
            parameters[3].Value = model.shopgoods_amount;
            parameters[4].Value = model.shopgoods_selfPrice;
            parameters[5].Value = model.shopgoods_isSell;
            parameters[6].Value = model.shopgoods_addtime;
            parameters[7].Value = model.goodsCode;
            parameters[8].Value = model.isFormate;
            parameters[9].Value = model.saleNum;
            parameters[10].Value = model.shopgoods_isSet;
            parameters[11].Value = model.shopgoods_isPass;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ShopGoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Shop_Goods set ");
            strSql.Append("shopid=@shopid,");
            strSql.Append("goodsid=@goodsid,");
            strSql.Append("shopgoods_amount=@shopgoods_amount,");
            strSql.Append("shopgoods_selfPrice=@shopgoods_selfPrice,");
            strSql.Append("shopgoods_isSell=@shopgoods_isSell,");
            strSql.Append("shopgoods_addtime=@shopgoods_addtime,");
            strSql.Append("goodsCode=@goodsCode,");
            strSql.Append("isFormate=@isFormate,");
            strSql.Append("saleNum=@saleNum,");
            strSql.Append("shopgoods_isSet=@shopgoods_isSet,");
            strSql.Append("shopgoods_isPass=@shopgoods_isPass");
            strSql.Append(" where shopgoods_id=@shopgoods_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@shopid", SqlDbType.VarChar,50),
					new SqlParameter("@goodsid", SqlDbType.VarChar,50),
					new SqlParameter("@shopgoods_amount", SqlDbType.Int,4),
					new SqlParameter("@shopgoods_selfPrice", SqlDbType.Decimal,9),
					new SqlParameter("@shopgoods_isSell", SqlDbType.VarChar,50),
					new SqlParameter("@shopgoods_addtime", SqlDbType.DateTime),
					new SqlParameter("@goodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@isFormate", SqlDbType.Int,4),
					new SqlParameter("@saleNum", SqlDbType.Int,4),
					new SqlParameter("@shopgoods_isSet", SqlDbType.Int,4),
					new SqlParameter("@shopgoods_isPass", SqlDbType.Int,4),
					new SqlParameter("@shopgoods_id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.shopid;
            parameters[1].Value = model.goodsid;
            parameters[2].Value = model.shopgoods_amount;
            parameters[3].Value = model.shopgoods_selfPrice;
            parameters[4].Value = model.shopgoods_isSell;
            parameters[5].Value = model.shopgoods_addtime;
            parameters[6].Value = model.goodsCode;
            parameters[7].Value = model.isFormate;
            parameters[8].Value = model.saleNum;
            parameters[9].Value = model.shopgoods_isSet;
            parameters[10].Value = model.shopgoods_isPass;
            parameters[11].Value = model.shopgoods_id;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(Guid shopgoods_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Shop_Goods ");
            strSql.Append(" where shopgoods_id=@shopgoods_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@shopgoods_id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = shopgoods_id;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ShopGoodsModel GetModel(Guid shopgoods_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 shopgoods_id,shopid,goodsid,shopgoods_amount,shopgoods_selfPrice,shopgoods_isSell,shopgoods_addtime,goodsCode,isFormate,saleNum,shopgoods_isSet,shopgoods_isPass from T_Shop_Goods ");
            strSql.Append(" where shopgoods_id=@shopgoods_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@shopgoods_id", SqlDbType.UniqueIdentifier,16)			};
            parameters[0].Value = shopgoods_id;

            ShopGoodsModel model = new ShopGoodsModel();
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
        public ShopGoodsModel DataRowToModel(DataRow row)
        {
            ShopGoodsModel model = new ShopGoodsModel();

            if (row != null)
            {
                if (row["shopgoods_id"] != null && row["shopgoods_id"].ToString() != "")
                {
                    model.shopgoods_id = new Guid(row["shopgoods_id"].ToString());
                }
                if (row["shopid"] != null)
                {
                    model.shopid = row["shopid"].ToString();
                }
                if (row["goodsid"] != null)
                {
                    model.goodsid = row["goodsid"].ToString();
                }
                if (row["shopgoods_amount"] != null && row["shopgoods_amount"].ToString() != "")
                {
                    model.shopgoods_amount = int.Parse(row["shopgoods_amount"].ToString());
                }
                if (row["shopgoods_selfPrice"] != null && row["shopgoods_selfPrice"].ToString() != "")
                {
                    model.shopgoods_selfPrice = decimal.Parse(row["shopgoods_selfPrice"].ToString());
                }
                if (row["shopgoods_isSell"] != null)
                {
                    model.shopgoods_isSell = row["shopgoods_isSell"].ToString();
                }
                if (row["shopgoods_addtime"] != null && row["shopgoods_addtime"].ToString() != "")
                {
                    model.shopgoods_addtime = DateTime.Parse(row["shopgoods_addtime"].ToString());
                }
                if (row["goodsCode"] != null)
                {
                    model.goodsCode = row["goodsCode"].ToString();
                }
                if (row["isFormate"] != null && row["isFormate"].ToString() != "")
                {
                    model.isFormate = int.Parse(row["isFormate"].ToString());
                }
                if (row["saleNum"] != null && row["saleNum"].ToString() != "")
                {
                    model.saleNum = int.Parse(row["saleNum"].ToString());
                }
                if (row["shopgoods_isSet"] != null && row["shopgoods_isSet"].ToString() != "")
                {
                    model.shopgoods_isSet = int.Parse(row["shopgoods_isSet"].ToString());
                }
                if (row["shopgoods_isPass"] != null && row["shopgoods_isPass"].ToString() != "")
                {
                    model.shopgoods_isPass = int.Parse(row["shopgoods_isPass"].ToString());
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
            strSql.Append("select shopgoods_id,shopid,goodsid,shopgoods_amount,shopgoods_selfPrice,shopgoods_isSell,shopgoods_addtime,goodsCode,isFormate,saleNum,shopgoods_isSet,shopgoods_isPass ");
            strSql.Append(" FROM T_Shop_Goods ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        #endregion  BasicMethod
    }
}
