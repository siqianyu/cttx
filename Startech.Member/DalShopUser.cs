using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;
using StarTech.Util;

namespace Startech.Member
{
    public class DalShopUser
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModShopUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Shop_User(");
            strSql.Append("ShopId,CompanyName,ShopName,LinkMan,Phone,Charter,CategoryId,Address,ApplyTime,Mark,UserName,Passwrod,LoginTime,AccoutsState,ShopLogo,ShopDescribe,area,Email,QQ,AlipayName,ShopLogoL,isOpen,shopMoney,orderby,MarketId,isOwnSeller)");
            strSql.Append(" values (");
            strSql.Append("@ShopId,@CompanyName,@ShopName,@LinkMan,@Phone,@Charter,@CategoryId,@Address,@ApplyTime,@Mark,@UserName,@Passwrod,@LoginTime,@AccoutsState,@ShopLogo,@ShopDescribe,@area,@Email,@QQ,@AlipayName,@ShopLogoL,@isOpen,@shopMoney,@orderby,@MarketId,@isOwnSeller)");
            SqlParameter[] parameters = {
					new SqlParameter("@ShopId", SqlDbType.VarChar,50),
					new SqlParameter("@CompanyName", SqlDbType.VarChar,200),
					new SqlParameter("@ShopName", SqlDbType.VarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.VarChar,20),
					new SqlParameter("@Phone", SqlDbType.VarChar,20),
					new SqlParameter("@Charter", SqlDbType.VarChar,500),
					new SqlParameter("@CategoryId", SqlDbType.VarChar,500),
					new SqlParameter("@Address", SqlDbType.VarChar,200),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@Mark", SqlDbType.VarChar,2000),
					new SqlParameter("@UserName", SqlDbType.VarChar,200),
					new SqlParameter("@Passwrod", SqlDbType.VarChar,200),
					new SqlParameter("@LoginTime", SqlDbType.DateTime),
					new SqlParameter("@AccoutsState", SqlDbType.VarChar,50),
					new SqlParameter("@ShopLogo", SqlDbType.VarChar,500),
					new SqlParameter("@ShopDescribe", SqlDbType.VarChar,500),
					new SqlParameter("@area", SqlDbType.VarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,100),
					new SqlParameter("@QQ", SqlDbType.VarChar,50),
                    new SqlParameter("@AlipayName",SqlDbType.VarChar,500),
                    new SqlParameter("@ShopLogoL",SqlDbType.VarChar,500),
                    new SqlParameter("@isOpen",SqlDbType.Int,4),
                    new SqlParameter("@shopMoney",SqlDbType.Decimal,18),
                    new SqlParameter("@orderby",SqlDbType.Int,4),
                    new SqlParameter("@MarketId",SqlDbType.VarChar,50),
                     new SqlParameter("@isOwnSeller",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ShopId;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.ShopName;
            parameters[3].Value = model.LinkMan;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Charter;
            parameters[6].Value = model.CategoryId;
            parameters[7].Value = model.Address;
            parameters[8].Value = model.ApplyTime;
            parameters[9].Value = model.Mark;
            parameters[10].Value = model.UserName;
            parameters[11].Value = model.Passwrod;
            parameters[12].Value = model.LoginTime;
            parameters[13].Value = model.AccoutsState;
            parameters[14].Value = model.ShopLogo;
            parameters[15].Value = model.ShopDescribe;
            parameters[16].Value = model.area;
            parameters[17].Value = model.Email;
            parameters[18].Value = model.QQ;
            parameters[19].Value = model.alipayName;
            parameters[20].Value = model.ShopLogoL;
            parameters[21].Value = model.isOpen;
            parameters[22].Value = model.ShopMoney;
            parameters[23].Value = model.orderby;
            parameters[24].Value = model.MarketId;
            parameters[25].Value = model.isOwnSeller;


            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModShopUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Shop_User set ");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("ShopName=@ShopName,");
            strSql.Append("LinkMan=@LinkMan,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Charter=@Charter,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("Address=@Address,");
            strSql.Append("ApplyTime=@ApplyTime,");
            strSql.Append("Mark=@Mark,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Passwrod=@Passwrod,");
            strSql.Append("LoginTime=@LoginTime,");
            strSql.Append("AccoutsState=@AccoutsState,");
            strSql.Append("ShopLogo=@ShopLogo,");
            strSql.Append("ShopDescribe=@ShopDescribe,");
            strSql.Append("area=@area,");
            strSql.Append("Email=@Email,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("alipayName=@alipayName,");
            strSql.Append("ShopLogoL=@ShopLogoL,");
            strSql.Append("isOpen=@isOpen,");
            strSql.Append("shopMoney=@shopMoney,");
            strSql.Append("orderby=@orderby,");
            strSql.Append("MarketId=@MarketId,");
            strSql.Append("isOwnSeller=@isOwnSeller");
            strSql.Append(" where ShopId=@ShopId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyName", SqlDbType.VarChar,200),
					new SqlParameter("@ShopName", SqlDbType.VarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.VarChar,20),
					new SqlParameter("@Phone", SqlDbType.VarChar,20),
					new SqlParameter("@Charter", SqlDbType.VarChar,500),
					new SqlParameter("@CategoryId", SqlDbType.VarChar,50),
					new SqlParameter("@Address", SqlDbType.VarChar,200),
					new SqlParameter("@ApplyTime", SqlDbType.DateTime),
					new SqlParameter("@Mark", SqlDbType.VarChar,2000),
					new SqlParameter("@UserName", SqlDbType.VarChar,200),
					new SqlParameter("@Passwrod", SqlDbType.VarChar,200),
					new SqlParameter("@LoginTime", SqlDbType.DateTime),
					new SqlParameter("@AccoutsState", SqlDbType.VarChar,50),
					new SqlParameter("@ShopLogo", SqlDbType.VarChar,500),
					new SqlParameter("@ShopDescribe", SqlDbType.VarChar,500),
					new SqlParameter("@area", SqlDbType.VarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,100),
					new SqlParameter("@QQ", SqlDbType.VarChar,50),
                    new SqlParameter("@alipayName",SqlDbType.VarChar,500),
                    new SqlParameter("@ShopLogoL",SqlDbType.VarChar,500),
                    new SqlParameter("@isOpen",SqlDbType.Int,4),
                    new SqlParameter("@shopMoney",SqlDbType.Decimal,18),
                    new SqlParameter("@orderby",SqlDbType.Int,4),
                    new SqlParameter("@MarketId",SqlDbType.VarChar,50),
                    new SqlParameter("@isOwnSeller",SqlDbType.Int,4),
					new SqlParameter("@ShopId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CompanyName;
            parameters[1].Value = model.ShopName;
            parameters[2].Value = model.LinkMan;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Charter;
            parameters[5].Value = model.CategoryId;
            parameters[6].Value = model.Address;
            parameters[7].Value = model.ApplyTime;
            parameters[8].Value = model.Mark;
            parameters[9].Value = model.UserName;
            parameters[10].Value = model.Passwrod;
            parameters[11].Value = model.LoginTime;
            parameters[12].Value = model.AccoutsState;
            parameters[13].Value = model.ShopLogo;
            parameters[14].Value = model.ShopDescribe;
            parameters[15].Value = model.area;
            parameters[16].Value = model.Email;
            parameters[17].Value = model.QQ;
            parameters[18].Value = model.alipayName;
            parameters[19].Value = model.ShopLogoL;
            parameters[20].Value = model.isOpen;
            parameters[21].Value = model.ShopMoney;
            parameters[22].Value = model.orderby;
            parameters[23].Value = model.MarketId;
            parameters[24].Value = model.isOwnSeller;
            parameters[25].Value = model.ShopId;



            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModShopUser GetModel(string ShopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ShopId,CompanyName,ShopName,LinkMan,Phone,Charter,CategoryId,Address,ApplyTime,Mark,UserName,Passwrod,LoginTime,AccoutsState,ShopLogo,ShopDescribe,area,Email,QQ,alipayName,ShopLogoL,isOpen,shopMoney,orderby,MarketId,isOwnSeller from T_Shop_User ");
            strSql.Append(" where ShopId=@ShopId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ShopId", SqlDbType.VarChar,50)			};
            parameters[0].Value = ShopId;

            ModShopUser model = new ModShopUser();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ShopId"] != null && ds.Tables[0].Rows[0]["ShopId"].ToString() != "")
                {
                    model.ShopId = ds.Tables[0].Rows[0]["ShopId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompanyName"] != null && ds.Tables[0].Rows[0]["CompanyName"].ToString() != "")
                {
                    model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShopName"] != null && ds.Tables[0].Rows[0]["ShopName"].ToString() != "")
                {
                    model.ShopName = ds.Tables[0].Rows[0]["ShopName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkMan"] != null && ds.Tables[0].Rows[0]["LinkMan"].ToString() != "")
                {
                    model.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Phone"] != null && ds.Tables[0].Rows[0]["Phone"].ToString() != "")
                {
                    model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Charter"] != null && ds.Tables[0].Rows[0]["Charter"].ToString() != "")
                {
                    model.Charter = ds.Tables[0].Rows[0]["Charter"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CategoryId"] != null && ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {
                    model.CategoryId = ds.Tables[0].Rows[0]["CategoryId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null && ds.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ApplyTime"] != null && ds.Tables[0].Rows[0]["ApplyTime"].ToString() != "")
                {
                    model.ApplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Mark"] != null && ds.Tables[0].Rows[0]["Mark"].ToString() != "")
                {
                    model.Mark = ds.Tables[0].Rows[0]["Mark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Passwrod"] != null && ds.Tables[0].Rows[0]["Passwrod"].ToString() != "")
                {
                    model.Passwrod = ds.Tables[0].Rows[0]["Passwrod"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LoginTime"] != null && ds.Tables[0].Rows[0]["LoginTime"].ToString() != "")
                {
                    model.LoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LoginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccoutsState"] != null && ds.Tables[0].Rows[0]["AccoutsState"].ToString() != "")
                {
                    model.AccoutsState = ds.Tables[0].Rows[0]["AccoutsState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShopLogo"] != null && ds.Tables[0].Rows[0]["ShopLogo"].ToString() != "")
                {
                    model.ShopLogo = ds.Tables[0].Rows[0]["ShopLogo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShopDescribe"] != null && ds.Tables[0].Rows[0]["ShopDescribe"].ToString() != "")
                {
                    model.ShopDescribe = ds.Tables[0].Rows[0]["ShopDescribe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["area"] != null && ds.Tables[0].Rows[0]["area"].ToString() != "")
                {
                    model.area = ds.Tables[0].Rows[0]["area"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QQ"] != null && ds.Tables[0].Rows[0]["QQ"].ToString() != "")
                {
                    model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["alipayName"] != null && ds.Tables[0].Rows[0]["alipayName"].ToString() != "")
                {
                    model.alipayName = ds.Tables[0].Rows[0]["alipayName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShopLogoL"] != null && ds.Tables[0].Rows[0]["ShopLogoL"].ToString() != "")
                {
                    model.ShopLogoL = ds.Tables[0].Rows[0]["ShopLogoL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isOpen"] != null && ds.Tables[0].Rows[0]["isOpen"].ToString() != "")
                {
                    model.isOpen = Convert.ToInt32(ds.Tables[0].Rows[0]["isOpen"]);
                }
                if (ds.Tables[0].Rows[0]["shopMoney"] != null && ds.Tables[0].Rows[0]["shopMoney"].ToString() != "")
                {
                    model.ShopMoney = Convert.ToDecimal(ds.Tables[0].Rows[0]["shopMoney"]);
                }
                if (ds.Tables[0].Rows[0]["orderby"] != null && ds.Tables[0].Rows[0]["orderby"].ToString() != "")
                {
                    model.orderby = Convert.ToInt32(ds.Tables[0].Rows[0]["orderby"]);
                }
                if (ds.Tables[0].Rows[0]["MarketId"] != null && ds.Tables[0].Rows[0]["MarketId"].ToString() != "")
                {
                    model.MarketId = ds.Tables[0].Rows[0]["MarketId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isOwnSeller"] != null && ds.Tables[0].Rows[0]["isOwnSeller"].ToString() != "")
                {
                    model.isOwnSeller = Convert.ToInt32(ds.Tables[0].Rows[0]["isOwnSeller"]);
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
            strSql.Append("select ShopId,CompanyName,ShopName,LinkMan,Phone,Charter,CategoryId,Address,ApplyTime,Mark,UserName,Passwrod,LoginTime,AccoutsState,ShopLogo,ShopDescribe,area,Email,QQ,alipayName,ShopLogoL,isOpen,shopMoney,orderby,MarketId,isOwnSeller ");
            strSql.Append(" FROM T_Shop_User ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }
    }
}
