using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using StarTech.DBUtility;
using System.Data;

namespace StarTech.ELife.Goods
{
    public class GoodsDal
    {
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(GoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Goods_Info(");
            strSql.Append("GoodsId,CategoryId,GoodsToTypeId,BrandId,GoodsName,GoodsCode,Uint,Weight,GoodsSmallPic,GoodsSampleDesc,GoodsDesc,GoodsDesc2,GoodsDesc3,GoodsDesc4,GoodsDesc5,IsRec,IsHot,IsNew,IsSpe,SalePrice,MarketPrice,CBPrice,IsSale,Sotck,MinSaleNumber,MaxSaleNumber,Orderby,ViewCount,TotalSaleCount,Remarks,AddTime,AreaInfo,BookInfo,ProviderInfo,DataFrom,MorePropertys,IfSh,ShPerson,ShTime,ShMark,IsOldGoods,OldGoodsLevel,saleCount,Postage,PingLunCount,vipPrice1,vipPrice2,signId,serviceId,JobType,JobStartTime,JobEndTime,JobAddress,JobSquare,JobByPersonType)");
            strSql.Append(" values (");
            strSql.Append("@GoodsId,@CategoryId,@GoodsToTypeId,@BrandId,@GoodsName,@GoodsCode,@Uint,@Weight,@GoodsSmallPic,@GoodsSampleDesc,@GoodsDesc,@GoodsDesc2,@GoodsDesc3,@GoodsDesc4,@GoodsDesc5,@IsRec,@IsHot,@IsNew,@IsSpe,@SalePrice,@MarketPrice,@CBPrice,@IsSale,@Sotck,@MinSaleNumber,@MaxSaleNumber,@Orderby,@ViewCount,@TotalSaleCount,@Remarks,@AddTime,@AreaInfo,@BookInfo,@ProviderInfo,@DataFrom,@MorePropertys,@IfSh,@ShPerson,@ShTime,@ShMark,@IsOldGoods,@OldGoodsLevel,@saleCount,@Postage,@PingLunCount,@vipPrice1,@vipPrice2,@signId,@serviceId,@JobType,@JobStartTime,@JobEndTime,@JobAddress,@JobSquare,@JobByPersonType)");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsToTypeId", SqlDbType.VarChar,50),
					new SqlParameter("@BrandId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsName", SqlDbType.VarChar,200),
					new SqlParameter("@GoodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@Uint", SqlDbType.VarChar,50),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@GoodsSmallPic", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsSampleDesc", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsDesc", SqlDbType.Text),
					new SqlParameter("@GoodsDesc2", SqlDbType.Text),
					new SqlParameter("@GoodsDesc3", SqlDbType.Text),
					new SqlParameter("@GoodsDesc4", SqlDbType.Text),
					new SqlParameter("@GoodsDesc5", SqlDbType.Text),
					new SqlParameter("@IsRec", SqlDbType.Int,4),
					new SqlParameter("@IsHot", SqlDbType.Int,4),
					new SqlParameter("@IsNew", SqlDbType.Int,4),
					new SqlParameter("@IsSpe", SqlDbType.Int,4),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,9),
					new SqlParameter("@MarketPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CBPrice", SqlDbType.Decimal,9),
					new SqlParameter("@IsSale", SqlDbType.Int,4),
					new SqlParameter("@Sotck", SqlDbType.Int,4),
					new SqlParameter("@MinSaleNumber", SqlDbType.Int,4),
					new SqlParameter("@MaxSaleNumber", SqlDbType.Int,4),
					new SqlParameter("@Orderby", SqlDbType.Int,4),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@TotalSaleCount", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.VarChar,500),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@AreaInfo", SqlDbType.VarChar,500),
					new SqlParameter("@BookInfo", SqlDbType.VarChar,500),
					new SqlParameter("@ProviderInfo", SqlDbType.VarChar,500),
					new SqlParameter("@DataFrom", SqlDbType.VarChar,50),
					new SqlParameter("@MorePropertys", SqlDbType.VarChar,4000),
					new SqlParameter("@IfSh", SqlDbType.Int,4),
					new SqlParameter("@ShPerson", SqlDbType.VarChar,50),
					new SqlParameter("@ShTime", SqlDbType.DateTime),
					new SqlParameter("@ShMark", SqlDbType.VarChar,500),
					new SqlParameter("@IsOldGoods", SqlDbType.Int,4),
					new SqlParameter("@OldGoodsLevel", SqlDbType.VarChar,50),
					new SqlParameter("@saleCount", SqlDbType.Int,4),
					new SqlParameter("@Postage", SqlDbType.Decimal,9),
					new SqlParameter("@PingLunCount", SqlDbType.Int,4),
					new SqlParameter("@vipPrice1", SqlDbType.Decimal,9),
					new SqlParameter("@vipPrice2", SqlDbType.Decimal,9),
					new SqlParameter("@signId", SqlDbType.VarChar,1000),
					new SqlParameter("@serviceId", SqlDbType.VarChar,500),
					new SqlParameter("@JobType", SqlDbType.VarChar,500),
					new SqlParameter("@JobStartTime", SqlDbType.DateTime),
					new SqlParameter("@JobEndTime", SqlDbType.DateTime),
					new SqlParameter("@JobAddress", SqlDbType.VarChar,2000),
					new SqlParameter("@JobSquare", SqlDbType.VarChar,500),
					new SqlParameter("@JobByPersonType", SqlDbType.VarChar,500)};
            parameters[0].Value = model.GoodsId;
            parameters[1].Value = model.CategoryId;
            parameters[2].Value = model.GoodsToTypeId;
            parameters[3].Value = model.BrandId;
            parameters[4].Value = model.GoodsName;
            parameters[5].Value = model.GoodsCode;
            parameters[6].Value = model.Uint;
            parameters[7].Value = model.Weight;
            parameters[8].Value = model.GoodsSmallPic;
            parameters[9].Value = model.GoodsSampleDesc;
            parameters[10].Value = model.GoodsDesc;
            parameters[11].Value = model.GoodsDesc2;
            parameters[12].Value = model.GoodsDesc3;
            parameters[13].Value = model.GoodsDesc4;
            parameters[14].Value = model.GoodsDesc5;
            parameters[15].Value = model.IsRec;
            parameters[16].Value = model.IsHot;
            parameters[17].Value = model.IsNew;
            parameters[18].Value = model.IsSpe;
            parameters[19].Value = model.SalePrice;
            parameters[20].Value = model.MarketPrice;
            parameters[21].Value = model.CBPrice;
            parameters[22].Value = model.IsSale;
            parameters[23].Value = model.Sotck;
            parameters[24].Value = model.MinSaleNumber;
            parameters[25].Value = model.MaxSaleNumber;
            parameters[26].Value = model.Orderby;
            parameters[27].Value = model.ViewCount;
            parameters[28].Value = model.TotalSaleCount;
            parameters[29].Value = model.Remarks;
            parameters[30].Value = model.AddTime;
            parameters[31].Value = model.AreaInfo;
            parameters[32].Value = model.BookInfo;
            parameters[33].Value = model.ProviderInfo;
            parameters[34].Value = model.DataFrom;
            parameters[35].Value = model.MorePropertys;
            parameters[36].Value = model.IfSh;
            parameters[37].Value = model.ShPerson;
            parameters[38].Value = model.ShTime;
            parameters[39].Value = model.ShMark;
            parameters[40].Value = model.IsOldGoods;
            parameters[41].Value = model.OldGoodsLevel;
            parameters[42].Value = model.saleCount;
            parameters[43].Value = model.Postage;
            parameters[44].Value = model.PingLunCount;
            parameters[45].Value = model.vipPrice1;
            parameters[46].Value = model.vipPrice2;
            parameters[47].Value = model.signId;
            parameters[48].Value = model.serviceId;
            parameters[49].Value = model.JobType;
            parameters[50].Value = model.JobStartTime;
            parameters[51].Value = model.JobEndTime;
            parameters[52].Value = model.JobAddress;
            parameters[53].Value = model.JobSquare;
            parameters[54].Value = model.JobByPersonType;
            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(GoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Goods_Info set ");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("GoodsToTypeId=@GoodsToTypeId,");
            strSql.Append("BrandId=@BrandId,");
            strSql.Append("GoodsName=@GoodsName,");
            strSql.Append("GoodsCode=@GoodsCode,");
            strSql.Append("Uint=@Uint,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("GoodsSmallPic=@GoodsSmallPic,");
            strSql.Append("GoodsSampleDesc=@GoodsSampleDesc,");
            strSql.Append("GoodsDesc=@GoodsDesc,");
            strSql.Append("GoodsDesc2=@GoodsDesc2,");
            strSql.Append("GoodsDesc3=@GoodsDesc3,");
            strSql.Append("GoodsDesc4=@GoodsDesc4,");
            strSql.Append("GoodsDesc5=@GoodsDesc5,");
            strSql.Append("IsRec=@IsRec,");
            strSql.Append("IsHot=@IsHot,");
            strSql.Append("IsNew=@IsNew,");
            strSql.Append("IsSpe=@IsSpe,");
            strSql.Append("SalePrice=@SalePrice,");
            strSql.Append("MarketPrice=@MarketPrice,");
            strSql.Append("CBPrice=@CBPrice,");
            strSql.Append("IsSale=@IsSale,");
            strSql.Append("Sotck=@Sotck,");
            strSql.Append("MinSaleNumber=@MinSaleNumber,");
            strSql.Append("MaxSaleNumber=@MaxSaleNumber,");
            strSql.Append("Orderby=@Orderby,");
            strSql.Append("ViewCount=@ViewCount,");
            strSql.Append("TotalSaleCount=@TotalSaleCount,");
            strSql.Append("Remarks=@Remarks,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("AreaInfo=@AreaInfo,");
            strSql.Append("BookInfo=@BookInfo,");
            strSql.Append("ProviderInfo=@ProviderInfo,");
            strSql.Append("DataFrom=@DataFrom,");
            strSql.Append("MorePropertys=@MorePropertys,");
            strSql.Append("IfSh=@IfSh,");
            strSql.Append("ShPerson=@ShPerson,");
            strSql.Append("ShTime=@ShTime,");
            strSql.Append("ShMark=@ShMark,");
            strSql.Append("IsOldGoods=@IsOldGoods,");
            strSql.Append("OldGoodsLevel=@OldGoodsLevel,");
            strSql.Append("saleCount=@saleCount,");
            strSql.Append("Postage=@Postage,");
            strSql.Append("PingLunCount=@PingLunCount,");
            strSql.Append("vipPrice1=@vipPrice1,");
            strSql.Append("vipPrice2=@vipPrice2,");
            strSql.Append("signId=@signId,");
            strSql.Append("serviceId=@serviceId,");
            strSql.Append("JobType=@JobType,");
            strSql.Append("JobStartTime=@JobStartTime,");
            strSql.Append("JobEndTime=@JobEndTime,");
            strSql.Append("JobAddress=@JobAddress,");
            strSql.Append("JobSquare=@JobSquare,");
            strSql.Append("JobByPersonType=@JobByPersonType");
            strSql.Append(" where GoodsId=@GoodsId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsToTypeId", SqlDbType.VarChar,50),
					new SqlParameter("@BrandId", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsName", SqlDbType.VarChar,200),
					new SqlParameter("@GoodsCode", SqlDbType.VarChar,50),
					new SqlParameter("@Uint", SqlDbType.VarChar,50),
					new SqlParameter("@Weight", SqlDbType.Int,4),
					new SqlParameter("@GoodsSmallPic", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsSampleDesc", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsDesc", SqlDbType.Text),
					new SqlParameter("@GoodsDesc2", SqlDbType.Text),
					new SqlParameter("@GoodsDesc3", SqlDbType.Text),
					new SqlParameter("@GoodsDesc4", SqlDbType.Text),
					new SqlParameter("@GoodsDesc5", SqlDbType.Text),
					new SqlParameter("@IsRec", SqlDbType.Int,4),
					new SqlParameter("@IsHot", SqlDbType.Int,4),
					new SqlParameter("@IsNew", SqlDbType.Int,4),
					new SqlParameter("@IsSpe", SqlDbType.Int,4),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,9),
					new SqlParameter("@MarketPrice", SqlDbType.Decimal,9),
					new SqlParameter("@CBPrice", SqlDbType.Decimal,9),
					new SqlParameter("@IsSale", SqlDbType.Int,4),
					new SqlParameter("@Sotck", SqlDbType.Int,4),
					new SqlParameter("@MinSaleNumber", SqlDbType.Int,4),
					new SqlParameter("@MaxSaleNumber", SqlDbType.Int,4),
					new SqlParameter("@Orderby", SqlDbType.Int,4),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@TotalSaleCount", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.VarChar,500),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@AreaInfo", SqlDbType.VarChar,500),
					new SqlParameter("@BookInfo", SqlDbType.VarChar,500),
					new SqlParameter("@ProviderInfo", SqlDbType.VarChar,500),
					new SqlParameter("@DataFrom", SqlDbType.VarChar,50),
					new SqlParameter("@MorePropertys", SqlDbType.VarChar,4000),
					new SqlParameter("@IfSh", SqlDbType.Int,4),
					new SqlParameter("@ShPerson", SqlDbType.VarChar,50),
					new SqlParameter("@ShTime", SqlDbType.DateTime),
					new SqlParameter("@ShMark", SqlDbType.VarChar,500),
					new SqlParameter("@IsOldGoods", SqlDbType.Int,4),
					new SqlParameter("@OldGoodsLevel", SqlDbType.VarChar,50),
					new SqlParameter("@saleCount", SqlDbType.Int,4),
					new SqlParameter("@Postage", SqlDbType.Decimal,9),
					new SqlParameter("@PingLunCount", SqlDbType.Int,4),
					new SqlParameter("@vipPrice1", SqlDbType.Decimal,9),
					new SqlParameter("@vipPrice2", SqlDbType.Decimal,9),
					new SqlParameter("@signId", SqlDbType.VarChar,1000),
					new SqlParameter("@serviceId", SqlDbType.VarChar,500),
					new SqlParameter("@JobType", SqlDbType.VarChar,500),
					new SqlParameter("@JobStartTime", SqlDbType.DateTime),
					new SqlParameter("@JobEndTime", SqlDbType.DateTime),
					new SqlParameter("@JobAddress", SqlDbType.VarChar,2000),
					new SqlParameter("@JobSquare", SqlDbType.VarChar,500),
					new SqlParameter("@JobByPersonType", SqlDbType.VarChar,500),
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.GoodsToTypeId;
            parameters[2].Value = model.BrandId;
            parameters[3].Value = model.GoodsName;
            parameters[4].Value = model.GoodsCode;
            parameters[5].Value = model.Uint;
            parameters[6].Value = model.Weight;
            parameters[7].Value = model.GoodsSmallPic;
            parameters[8].Value = model.GoodsSampleDesc;
            parameters[9].Value = model.GoodsDesc;
            parameters[10].Value = model.GoodsDesc2;
            parameters[11].Value = model.GoodsDesc3;
            parameters[12].Value = model.GoodsDesc4;
            parameters[13].Value = model.GoodsDesc5;
            parameters[14].Value = model.IsRec;
            parameters[15].Value = model.IsHot;
            parameters[16].Value = model.IsNew;
            parameters[17].Value = model.IsSpe;
            parameters[18].Value = model.SalePrice;
            parameters[19].Value = model.MarketPrice;
            parameters[20].Value = model.CBPrice;
            parameters[21].Value = model.IsSale;
            parameters[22].Value = model.Sotck;
            parameters[23].Value = model.MinSaleNumber;
            parameters[24].Value = model.MaxSaleNumber;
            parameters[25].Value = model.Orderby;
            parameters[26].Value = model.ViewCount;
            parameters[27].Value = model.TotalSaleCount;
            parameters[28].Value = model.Remarks;
            parameters[29].Value = model.AddTime;
            parameters[30].Value = model.AreaInfo;
            parameters[31].Value = model.BookInfo;
            parameters[32].Value = model.ProviderInfo;
            parameters[33].Value = model.DataFrom;
            parameters[34].Value = model.MorePropertys;
            parameters[35].Value = model.IfSh;
            parameters[36].Value = model.ShPerson;
            parameters[37].Value = model.ShTime;
            parameters[38].Value = model.ShMark;
            parameters[39].Value = model.IsOldGoods;
            parameters[40].Value = model.OldGoodsLevel;
            parameters[41].Value = model.saleCount;
            parameters[42].Value = model.Postage;
            parameters[43].Value = model.PingLunCount;
            parameters[44].Value = model.vipPrice1;
            parameters[45].Value = model.vipPrice2;
            parameters[46].Value = model.signId;
            parameters[47].Value = model.serviceId;
            parameters[48].Value = model.JobType;
            parameters[49].Value = model.JobStartTime;
            parameters[50].Value = model.JobEndTime;
            parameters[51].Value = model.JobAddress;
            parameters[52].Value = model.JobSquare;
            parameters[53].Value = model.JobByPersonType;
            parameters[54].Value = model.GoodsId;

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
        public bool Delete(string GoodsId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods_Info ");
            strSql.Append(" where GoodsId=@GoodsId ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50)};
            parameters[0].Value = GoodsId;

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
        public bool DeleteList(string GoodsIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Goods_Info ");
            strSql.Append(" where GoodsId in (" + GoodsIdlist + ")  ");
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
        public GoodsModel GetModel(string GoodsId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_Goods_Info ");
            strSql.Append(" where GoodsId=@GoodsId ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsId", SqlDbType.VarChar,50)};
            parameters[0].Value = GoodsId;

            GoodsModel model = new GoodsModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["GoodsId"] != null && ds.Tables[0].Rows[0]["GoodsId"].ToString() != "")
                {
                    model.GoodsId = ds.Tables[0].Rows[0]["GoodsId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CategoryId"] != null && ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {
                    model.CategoryId = ds.Tables[0].Rows[0]["CategoryId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsToTypeId"] != null && ds.Tables[0].Rows[0]["GoodsToTypeId"].ToString() != "")
                {
                    model.GoodsToTypeId = ds.Tables[0].Rows[0]["GoodsToTypeId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BrandId"] != null && ds.Tables[0].Rows[0]["BrandId"].ToString() != "")
                {
                    model.BrandId = ds.Tables[0].Rows[0]["BrandId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsName"] != null && ds.Tables[0].Rows[0]["GoodsName"].ToString() != "")
                {
                    model.GoodsName = ds.Tables[0].Rows[0]["GoodsName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsCode"] != null && ds.Tables[0].Rows[0]["GoodsCode"].ToString() != "")
                {
                    model.GoodsCode = ds.Tables[0].Rows[0]["GoodsCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Uint"] != null && ds.Tables[0].Rows[0]["Uint"].ToString() != "")
                {
                    model.Uint = ds.Tables[0].Rows[0]["Uint"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Weight"] != null && ds.Tables[0].Rows[0]["Weight"].ToString() != "")
                {
                    model.Weight = int.Parse(ds.Tables[0].Rows[0]["Weight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GoodsSmallPic"] != null && ds.Tables[0].Rows[0]["GoodsSmallPic"].ToString() != "")
                {
                    model.GoodsSmallPic = ds.Tables[0].Rows[0]["GoodsSmallPic"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsSampleDesc"] != null && ds.Tables[0].Rows[0]["GoodsSampleDesc"].ToString() != "")
                {
                    model.GoodsSampleDesc = ds.Tables[0].Rows[0]["GoodsSampleDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsDesc"] != null && ds.Tables[0].Rows[0]["GoodsDesc"].ToString() != "")
                {
                    model.GoodsDesc = ds.Tables[0].Rows[0]["GoodsDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsDesc2"] != null && ds.Tables[0].Rows[0]["GoodsDesc2"].ToString() != "")
                {
                    model.GoodsDesc2 = ds.Tables[0].Rows[0]["GoodsDesc2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsDesc3"] != null && ds.Tables[0].Rows[0]["GoodsDesc3"].ToString() != "")
                {
                    model.GoodsDesc3 = ds.Tables[0].Rows[0]["GoodsDesc3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsDesc4"] != null && ds.Tables[0].Rows[0]["GoodsDesc4"].ToString() != "")
                {
                    model.GoodsDesc4 = ds.Tables[0].Rows[0]["GoodsDesc4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GoodsDesc5"] != null && ds.Tables[0].Rows[0]["GoodsDesc5"].ToString() != "")
                {
                    model.GoodsDesc5 = ds.Tables[0].Rows[0]["GoodsDesc5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsRec"] != null && ds.Tables[0].Rows[0]["IsRec"].ToString() != "")
                {
                    model.IsRec = int.Parse(ds.Tables[0].Rows[0]["IsRec"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsHot"] != null && ds.Tables[0].Rows[0]["IsHot"].ToString() != "")
                {
                    model.IsHot = int.Parse(ds.Tables[0].Rows[0]["IsHot"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsNew"] != null && ds.Tables[0].Rows[0]["IsNew"].ToString() != "")
                {
                    model.IsNew = int.Parse(ds.Tables[0].Rows[0]["IsNew"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSpe"] != null && ds.Tables[0].Rows[0]["IsSpe"].ToString() != "")
                {
                    model.IsSpe = int.Parse(ds.Tables[0].Rows[0]["IsSpe"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SalePrice"] != null && ds.Tables[0].Rows[0]["SalePrice"].ToString() != "")
                {
                    model.SalePrice = decimal.Parse(ds.Tables[0].Rows[0]["SalePrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MarketPrice"] != null && ds.Tables[0].Rows[0]["MarketPrice"].ToString() != "")
                {
                    model.MarketPrice = decimal.Parse(ds.Tables[0].Rows[0]["MarketPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CBPrice"] != null && ds.Tables[0].Rows[0]["CBPrice"].ToString() != "")
                {
                    model.CBPrice = decimal.Parse(ds.Tables[0].Rows[0]["CBPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSale"] != null && ds.Tables[0].Rows[0]["IsSale"].ToString() != "")
                {
                    model.IsSale = int.Parse(ds.Tables[0].Rows[0]["IsSale"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sotck"] != null && ds.Tables[0].Rows[0]["Sotck"].ToString() != "")
                {
                    model.Sotck = int.Parse(ds.Tables[0].Rows[0]["Sotck"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinSaleNumber"] != null && ds.Tables[0].Rows[0]["MinSaleNumber"].ToString() != "")
                {
                    model.MinSaleNumber = int.Parse(ds.Tables[0].Rows[0]["MinSaleNumber"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxSaleNumber"] != null && ds.Tables[0].Rows[0]["MaxSaleNumber"].ToString() != "")
                {
                    model.MaxSaleNumber = int.Parse(ds.Tables[0].Rows[0]["MaxSaleNumber"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Orderby"] != null && ds.Tables[0].Rows[0]["Orderby"].ToString() != "")
                {
                    model.Orderby = int.Parse(ds.Tables[0].Rows[0]["Orderby"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ViewCount"] != null && ds.Tables[0].Rows[0]["ViewCount"].ToString() != "")
                {
                    model.ViewCount = int.Parse(ds.Tables[0].Rows[0]["ViewCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TotalSaleCount"] != null && ds.Tables[0].Rows[0]["TotalSaleCount"].ToString() != "")
                {
                    model.TotalSaleCount = int.Parse(ds.Tables[0].Rows[0]["TotalSaleCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remarks"] != null && ds.Tables[0].Rows[0]["Remarks"].ToString() != "")
                {
                    model.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AddTime"] != null && ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AreaInfo"] != null && ds.Tables[0].Rows[0]["AreaInfo"].ToString() != "")
                {
                    model.AreaInfo = ds.Tables[0].Rows[0]["AreaInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BookInfo"] != null && ds.Tables[0].Rows[0]["BookInfo"].ToString() != "")
                {
                    model.BookInfo = ds.Tables[0].Rows[0]["BookInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProviderInfo"] != null && ds.Tables[0].Rows[0]["ProviderInfo"].ToString() != "")
                {
                    model.ProviderInfo = ds.Tables[0].Rows[0]["ProviderInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DataFrom"] != null && ds.Tables[0].Rows[0]["DataFrom"].ToString() != "")
                {
                    model.DataFrom = ds.Tables[0].Rows[0]["DataFrom"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MorePropertys"] != null && ds.Tables[0].Rows[0]["MorePropertys"].ToString() != "")
                {
                    model.MorePropertys = ds.Tables[0].Rows[0]["MorePropertys"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IfSh"] != null && ds.Tables[0].Rows[0]["IfSh"].ToString() != "")
                {
                    model.IfSh = int.Parse(ds.Tables[0].Rows[0]["IfSh"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShPerson"] != null && ds.Tables[0].Rows[0]["ShPerson"].ToString() != "")
                {
                    model.ShPerson = ds.Tables[0].Rows[0]["ShPerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShTime"] != null && ds.Tables[0].Rows[0]["ShTime"].ToString() != "")
                {
                    model.ShTime = DateTime.Parse(ds.Tables[0].Rows[0]["ShTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShMark"] != null && ds.Tables[0].Rows[0]["ShMark"].ToString() != "")
                {
                    model.ShMark = ds.Tables[0].Rows[0]["ShMark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsOldGoods"] != null && ds.Tables[0].Rows[0]["IsOldGoods"].ToString() != "")
                {
                    model.IsOldGoods = int.Parse(ds.Tables[0].Rows[0]["IsOldGoods"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OldGoodsLevel"] != null && ds.Tables[0].Rows[0]["OldGoodsLevel"].ToString() != "")
                {
                    model.OldGoodsLevel = ds.Tables[0].Rows[0]["OldGoodsLevel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["saleCount"] != null && ds.Tables[0].Rows[0]["saleCount"].ToString() != "")
                {
                    model.saleCount = int.Parse(ds.Tables[0].Rows[0]["saleCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Postage"] != null && ds.Tables[0].Rows[0]["Postage"].ToString() != "")
                {
                    model.Postage = decimal.Parse(ds.Tables[0].Rows[0]["Postage"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PingLunCount"] != null && ds.Tables[0].Rows[0]["PingLunCount"].ToString() != "")
                {
                    model.PingLunCount = int.Parse(ds.Tables[0].Rows[0]["PingLunCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["vipPrice1"] != null && ds.Tables[0].Rows[0]["vipPrice1"].ToString() != "")
                {
                    model.vipPrice1 = decimal.Parse(ds.Tables[0].Rows[0]["vipPrice1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["vipPrice2"] != null && ds.Tables[0].Rows[0]["vipPrice2"].ToString() != "")
                {
                    model.vipPrice2 = decimal.Parse(ds.Tables[0].Rows[0]["vipPrice2"].ToString());
                }
                if (ds.Tables[0].Rows[0]["signId"] != null && ds.Tables[0].Rows[0]["signId"].ToString() != "")
                {
                    model.signId = ds.Tables[0].Rows[0]["signId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["serviceId"] != null && ds.Tables[0].Rows[0]["serviceId"].ToString() != "")
                {
                    model.ServiceId = ds.Tables[0].Rows[0]["serviceId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JobType"] != null)
                {
                    model.JobType = ds.Tables[0].Rows[0]["JobType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JobStartTime"] != null && ds.Tables[0].Rows[0]["JobStartTime"].ToString() != "")
                {
                    model.JobStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["JobStartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JobEndTime"] != null && ds.Tables[0].Rows[0]["JobEndTime"].ToString() != "")
                {
                    model.JobEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["JobEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JobAddress"] != null)
                {
                    model.JobAddress = ds.Tables[0].Rows[0]["JobAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JobSquare"] != null)
                {
                    model.JobSquare = ds.Tables[0].Rows[0]["JobSquare"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JobByPersonType"] != null)
                {
                    model.JobByPersonType = ds.Tables[0].Rows[0]["JobByPersonType"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Goods_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_Goods_Info ");
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
                strSql.Append("order by T.addtime desc");
            }
            strSql.Append(")AS Row, T.*  from T_Goods_Info T ");
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
