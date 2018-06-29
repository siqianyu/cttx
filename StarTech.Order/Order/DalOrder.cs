using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;

namespace StarTech.Order.Order
{
    public class DalOrder
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Order_Info(");
            strSql.Append("OrderId,OrderType,MemberId,MemberName,OrderTime,OrderAllMoney,GoodsAllMoney,Freight,TicketMoney,DiscountMoeny,TempChangeMoney,ReceivePerson,ReceiveAddressCode,ReceiveAddressDetail,PostCode,Email,Tel,Mobile,MemberOrderRemarks,AdminOrderRemarks,OrderAllWeight,IsPay,PayTime,PayInterfaceCode,PayMethod,IsSend,SendTime,SendNumber,SendCompany,SendMethod,SendMethodByReal,IsGet,GetTime,IsMemberCancel,MemberCancelRemarks,IsComplete,CompleteTime,IsInvoice,InvoiceCode,InvoiceType1,InvoiceType2,InvoiceTitle,InvoiceRemarks,IsCheckIfRealOrder,CheckIfRealOrderTime,CheckIfRealOrderPerson,CheckIfRealOrderRemarks,SellerId,SellerType,ztTime,ztTimeEnd,isComment,CommentTime,marketId,isDis,disTime,building_id,couponId,oldMoney,priceId)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@OrderType,@MemberId,@MemberName,@OrderTime,@OrderAllMoney,@GoodsAllMoney,@Freight,@TicketMoney,@DiscountMoeny,@TempChangeMoney,@ReceivePerson,@ReceiveAddressCode,@ReceiveAddressDetail,@PostCode,@Email,@Tel,@Mobile,@MemberOrderRemarks,@AdminOrderRemarks,@OrderAllWeight,@IsPay,@PayTime,@PayInterfaceCode,@PayMethod,@IsSend,@SendTime,@SendNumber,@SendCompany,@SendMethod,@SendMethodByReal,@IsGet,@GetTime,@IsMemberCancel,@MemberCancelRemarks,@IsComplete,@CompleteTime,@IsInvoice,@InvoiceCode,@InvoiceType1,@InvoiceType2,@InvoiceTitle,@InvoiceRemarks,@IsCheckIfRealOrder,@CheckIfRealOrderTime,@CheckIfRealOrderPerson,@CheckIfRealOrderRemarks,@SellerId,@SellerType,@ztTime,@ztTimeEnd,@isComment,@CommentTime,@marketId,@isDis,@disTime,@building_id,@couponId,@oldMoney,@priceId)");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.VarChar,50),
					new SqlParameter("@OrderType", SqlDbType.VarChar,50),
					new SqlParameter("@MemberId", SqlDbType.VarChar,50),
					new SqlParameter("@MemberName", SqlDbType.VarChar,50),
					new SqlParameter("@OrderTime", SqlDbType.DateTime),
					new SqlParameter("@OrderAllMoney", SqlDbType.Decimal,9),
					new SqlParameter("@GoodsAllMoney", SqlDbType.Decimal,9),
					new SqlParameter("@Freight", SqlDbType.Decimal,9),
					new SqlParameter("@TicketMoney", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountMoeny", SqlDbType.Decimal,9),
					new SqlParameter("@TempChangeMoney", SqlDbType.Decimal,9),
					new SqlParameter("@ReceivePerson", SqlDbType.VarChar,50),
					new SqlParameter("@ReceiveAddressCode", SqlDbType.VarChar,50),
					new SqlParameter("@ReceiveAddressDetail", SqlDbType.VarChar,500),
					new SqlParameter("@PostCode", SqlDbType.VarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@Tel", SqlDbType.VarChar,50),
					new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@MemberOrderRemarks", SqlDbType.VarChar,500),
					new SqlParameter("@AdminOrderRemarks", SqlDbType.VarChar,500),
					new SqlParameter("@OrderAllWeight", SqlDbType.Int,4),
					new SqlParameter("@IsPay", SqlDbType.Int,4),
					new SqlParameter("@PayTime", SqlDbType.DateTime),
					new SqlParameter("@PayInterfaceCode", SqlDbType.VarChar,50),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,50),
					new SqlParameter("@IsSend", SqlDbType.Int,4),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@SendNumber", SqlDbType.VarChar,50),
					new SqlParameter("@SendCompany", SqlDbType.VarChar,50),
					new SqlParameter("@SendMethod", SqlDbType.VarChar,50),
					new SqlParameter("@SendMethodByReal", SqlDbType.VarChar,50),
					new SqlParameter("@IsGet", SqlDbType.Int,4),
					new SqlParameter("@GetTime", SqlDbType.DateTime),
					new SqlParameter("@IsMemberCancel", SqlDbType.Int,4),
					new SqlParameter("@MemberCancelRemarks", SqlDbType.VarChar,500),
					new SqlParameter("@IsComplete", SqlDbType.Int,4),
					new SqlParameter("@CompleteTime", SqlDbType.DateTime),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@InvoiceCode", SqlDbType.VarChar,50),
					new SqlParameter("@InvoiceType1", SqlDbType.VarChar,50),
					new SqlParameter("@InvoiceType2", SqlDbType.VarChar,50),
					new SqlParameter("@InvoiceTitle", SqlDbType.VarChar,100),
					new SqlParameter("@InvoiceRemarks", SqlDbType.VarChar,500),
                    new SqlParameter("@IsCheckIfRealOrder", SqlDbType.Int,4),
					new SqlParameter("@CheckIfRealOrderTime", SqlDbType.DateTime),
					new SqlParameter("@CheckIfRealOrderPerson", SqlDbType.VarChar,50),
					new SqlParameter("@CheckIfRealOrderRemarks", SqlDbType.VarChar,500),
                    new SqlParameter("@SellerId",SqlDbType.VarChar,50),
                    new SqlParameter("@SellerType",SqlDbType.VarChar,50),
                    new SqlParameter("@ztTime",SqlDbType.DateTime),
                    new SqlParameter("@ztTimeEnd",SqlDbType.DateTime),
                    new SqlParameter("@isComment",SqlDbType.Int),
                    new SqlParameter("@CommentTime",SqlDbType.DateTime),
                    new SqlParameter("@marketId",SqlDbType.VarChar),
                    new SqlParameter("@isDis",SqlDbType.Int),
                    new SqlParameter("@disTime",SqlDbType.DateTime),
                    new SqlParameter("@building_id",SqlDbType.VarChar),
                    new SqlParameter("@couponId",SqlDbType.VarChar),
                    new SqlParameter("@oldMoney",SqlDbType.Decimal),
					new SqlParameter("@priceId",SqlDbType.VarChar,50)
			};

            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderType;
            parameters[2].Value = model.MemberId;
            parameters[3].Value = model.MemberName;
            parameters[4].Value = model.OrderTime;
            parameters[5].Value = model.OrderAllMoney;
            parameters[6].Value = model.GoodsAllMoney;
            parameters[7].Value = model.Freight;
            parameters[8].Value = model.TicketMoney;
            parameters[9].Value = model.DiscountMoeny;
            parameters[10].Value = model.TempChangeMoney;
            parameters[11].Value = model.ReceivePerson;
            parameters[12].Value = model.ReceiveAddressCode;
            parameters[13].Value = model.ReceiveAddressDetail;
            parameters[14].Value = model.PostCode;
            parameters[15].Value = model.Email;
            parameters[16].Value = model.Tel;
            parameters[17].Value = model.Mobile;
            parameters[18].Value = model.MemberOrderRemarks;
            parameters[19].Value = model.AdminOrderRemarks;
            parameters[20].Value = model.OrderAllWeight;
            parameters[21].Value = model.IsPay;
            parameters[22].Value = model.PayTime;
            parameters[23].Value = model.PayInterfaceCode;
            parameters[24].Value = model.PayMethod;
            parameters[25].Value = model.IsSend;
            parameters[26].Value = model.SendTime;
            parameters[27].Value = model.SendNumber;
            parameters[28].Value = model.SendCompany;
            parameters[29].Value = model.SendMethod;
            parameters[30].Value = model.SendMethodByReal;
            parameters[31].Value = model.IsGet;
            parameters[32].Value = model.GetTime;
            parameters[33].Value = model.IsMemberCancel;
            parameters[34].Value = model.MemberCancelRemarks;
            parameters[35].Value = model.IsComplete;
            parameters[36].Value = model.CompleteTime;
            parameters[37].Value = model.IsInvoice;
            parameters[38].Value = model.InvoiceCode;
            parameters[39].Value = model.InvoiceType1;
            parameters[40].Value = model.InvoiceType2;
            parameters[41].Value = model.InvoiceTitle;
            parameters[42].Value = model.InvoiceRemarks;
            parameters[43].Value = model.IsCheckIfRealOrder;
            parameters[44].Value = model.CheckIfRealOrderTime;
            parameters[45].Value = model.CheckIfRealOrderPerson;
            parameters[46].Value = model.CheckIfRealOrderRemarks;
            parameters[47].Value = model.SellerId;
            parameters[48].Value = model.SellerType;
            parameters[49].Value = model.ztTime;
            parameters[50].Value = model.ztTimeEnd;
            parameters[51].Value = model.isComment;
            parameters[52].Value = model.CommentTime;
            parameters[53].Value = model.marketId;
            parameters[54].Value = model.isDis;
            parameters[55].Value = model.DisTime;
            parameters[56].Value = model.building_id;
            parameters[57].Value = model.couponId;
            parameters[58].Value = model.oldMoney;
			parameters[59].Value = model.priceId;
			return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Order_Info set ");
            strSql.Append("OrderType=@OrderType,");
            strSql.Append("MemberId=@MemberId,");
            strSql.Append("MemberName=@MemberName,");
            strSql.Append("OrderTime=@OrderTime,");
            strSql.Append("OrderAllMoney=@OrderAllMoney,");
            strSql.Append("GoodsAllMoney=@GoodsAllMoney,");
            strSql.Append("Freight=@Freight,");
            strSql.Append("TicketMoney=@TicketMoney,");
            strSql.Append("DiscountMoeny=@DiscountMoeny,");
            strSql.Append("TempChangeMoney=@TempChangeMoney,");
            strSql.Append("ReceivePerson=@ReceivePerson,");
            strSql.Append("ReceiveAddressCode=@ReceiveAddressCode,");
            strSql.Append("ReceiveAddressDetail=@ReceiveAddressDetail,");
            strSql.Append("PostCode=@PostCode,");
            strSql.Append("Email=@Email,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Mobile=@Mobile,");
            strSql.Append("MemberOrderRemarks=@MemberOrderRemarks,");
            strSql.Append("AdminOrderRemarks=@AdminOrderRemarks,");
            strSql.Append("OrderAllWeight=@OrderAllWeight,");
            strSql.Append("IsPay=@IsPay,");
            strSql.Append("PayTime=@PayTime,");
            strSql.Append("PayInterfaceCode=@PayInterfaceCode,");
            strSql.Append("PayMethod=@PayMethod,");
            strSql.Append("IsSend=@IsSend,");
            strSql.Append("SendTime=@SendTime,");
            strSql.Append("SendNumber=@SendNumber,");
            strSql.Append("SendCompany=@SendCompany,");
            strSql.Append("SendMethod=@SendMethod,");
            strSql.Append("SendMethodByReal=@SendMethodByReal,");
            strSql.Append("IsGet=@IsGet,");
            strSql.Append("GetTime=@GetTime,");
            strSql.Append("IsMemberCancel=@IsMemberCancel,");
            strSql.Append("MemberCancelRemarks=@MemberCancelRemarks,");
            strSql.Append("IsComplete=@IsComplete,");
            strSql.Append("CompleteTime=@CompleteTime,");
            strSql.Append("IsInvoice=@IsInvoice,");
            strSql.Append("InvoiceCode=@InvoiceCode,");
            strSql.Append("InvoiceType1=@InvoiceType1,");
            strSql.Append("InvoiceType2=@InvoiceType2,");
            strSql.Append("InvoiceTitle=@InvoiceTitle,");
            strSql.Append("InvoiceRemarks=@InvoiceRemarks");
            strSql.Append("IsCheckIfRealOrder=@IsCheckIfRealOrder,");
            strSql.Append("CheckIfRealOrderTime=@CheckIfRealOrderTime,");
            strSql.Append("CheckIfRealOrderPerson=@CheckIfRealOrderPerson,");
            strSql.Append("CheckIfRealOrderRemarks=@CheckIfRealOrderRemarks,");
            strSql.Append("SellerId=@SellerId,");
            strSql.Append("SellerType=@SellerType,");
            strSql.Append("ztTime=@ztTime,");
            strSql.Append("ztTimeEnd=@ztTimeEnd,");
            strSql.Append("isComment=@isComment,");
            strSql.Append("CommentTime=@CommentTime,");
            strSql.Append("marketId=@marketId,");
            strSql.Append("isDis=@isDis,");
            strSql.Append("disTime=@disTime, ");
            strSql.Append("building_id=@building_id,");
            strSql.Append("couponId=@couponId,");
            strSql.Append("oldMoney=@oldMoney,");
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderType", SqlDbType.VarChar,50),
					new SqlParameter("@MemberId", SqlDbType.VarChar,50),
					new SqlParameter("@MemberName", SqlDbType.VarChar,50),
					new SqlParameter("@OrderTime", SqlDbType.DateTime),
					new SqlParameter("@OrderAllMoney", SqlDbType.Decimal,9),
					new SqlParameter("@GoodsAllMoney", SqlDbType.Decimal,9),
					new SqlParameter("@Freight", SqlDbType.Decimal,9),
					new SqlParameter("@TicketMoney", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountMoeny", SqlDbType.Decimal,9),
					new SqlParameter("@TempChangeMoney", SqlDbType.Decimal,9),
					new SqlParameter("@ReceivePerson", SqlDbType.VarChar,50),
					new SqlParameter("@ReceiveAddressCode", SqlDbType.VarChar,50),
					new SqlParameter("@ReceiveAddressDetail", SqlDbType.VarChar,500),
					new SqlParameter("@PostCode", SqlDbType.VarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@Tel", SqlDbType.VarChar,50),
					new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@MemberOrderRemarks", SqlDbType.VarChar,500),
					new SqlParameter("@AdminOrderRemarks", SqlDbType.VarChar,500),
					new SqlParameter("@OrderAllWeight", SqlDbType.Int,4),
					new SqlParameter("@IsPay", SqlDbType.Int,4),
					new SqlParameter("@PayTime", SqlDbType.DateTime),
					new SqlParameter("@PayInterfaceCode", SqlDbType.VarChar,50),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,50),
					new SqlParameter("@IsSend", SqlDbType.Int,4),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@SendNumber", SqlDbType.VarChar,50),
					new SqlParameter("@SendCompany", SqlDbType.VarChar,50),
					new SqlParameter("@SendMethod", SqlDbType.VarChar,50),
					new SqlParameter("@SendMethodByReal", SqlDbType.VarChar,50),
					new SqlParameter("@IsGet", SqlDbType.Int,4),
					new SqlParameter("@GetTime", SqlDbType.DateTime),
					new SqlParameter("@IsMemberCancel", SqlDbType.Int,4),
					new SqlParameter("@MemberCancelRemarks", SqlDbType.VarChar,500),
					new SqlParameter("@IsComplete", SqlDbType.Int,4),
					new SqlParameter("@CompleteTime", SqlDbType.DateTime),
					new SqlParameter("@IsInvoice", SqlDbType.Int,4),
					new SqlParameter("@InvoiceCode", SqlDbType.VarChar,50),
					new SqlParameter("@InvoiceType1", SqlDbType.VarChar,50),
					new SqlParameter("@InvoiceType2", SqlDbType.VarChar,50),
					new SqlParameter("@InvoiceTitle", SqlDbType.VarChar,100),
					new SqlParameter("@InvoiceRemarks", SqlDbType.VarChar,500),
                	new SqlParameter("@IsCheckIfRealOrder", SqlDbType.Int,4),
					new SqlParameter("@CheckIfRealOrderTime", SqlDbType.DateTime),
					new SqlParameter("@CheckIfRealOrderPerson", SqlDbType.VarChar,50),
					new SqlParameter("@CheckIfRealOrderRemarks", SqlDbType.VarChar,500),
                    new SqlParameter("@SellerId",SqlDbType.VarChar,50),
                    new SqlParameter("@SellerType",SqlDbType.VarChar,50),
                    new SqlParameter("@ztTime",SqlDbType.DateTime),
                    new SqlParameter("@ztTimeEnd",SqlDbType.DateTime),
                    new SqlParameter("@isComment",SqlDbType.Int),
                    new SqlParameter("@CommentTime",SqlDbType.DateTime),
                    new SqlParameter("@marketId",SqlDbType.VarChar),
                    new SqlParameter("@isDis",SqlDbType.Int),
                    new SqlParameter("@disTime",SqlDbType.DateTime),
                    new SqlParameter("@building_id",SqlDbType.VarChar),
                    new SqlParameter("@couponId",SqlDbType.VarChar),
                    new SqlParameter("@oldMoney",SqlDbType.Decimal),
					new SqlParameter("@OrderId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.OrderType;
            parameters[1].Value = model.MemberId;
            parameters[2].Value = model.MemberName;
            parameters[3].Value = model.OrderTime;
            parameters[4].Value = model.OrderAllMoney;
            parameters[5].Value = model.GoodsAllMoney;
            parameters[6].Value = model.Freight;
            parameters[7].Value = model.TicketMoney;
            parameters[8].Value = model.DiscountMoeny;
            parameters[9].Value = model.TempChangeMoney;
            parameters[10].Value = model.ReceivePerson;
            parameters[11].Value = model.ReceiveAddressCode;
            parameters[12].Value = model.ReceiveAddressDetail;
            parameters[13].Value = model.PostCode;
            parameters[14].Value = model.Email;
            parameters[15].Value = model.Tel;
            parameters[16].Value = model.Mobile;
            parameters[17].Value = model.MemberOrderRemarks;
            parameters[18].Value = model.AdminOrderRemarks;
            parameters[19].Value = model.OrderAllWeight;
            parameters[20].Value = model.IsPay;
            parameters[21].Value = model.PayTime;
            parameters[22].Value = model.PayInterfaceCode;
            parameters[23].Value = model.PayMethod;
            parameters[24].Value = model.IsSend;
            parameters[25].Value = model.SendTime;
            parameters[26].Value = model.SendNumber;
            parameters[27].Value = model.SendCompany;
            parameters[28].Value = model.SendMethod;
            parameters[29].Value = model.SendMethodByReal;
            parameters[30].Value = model.IsGet;
            parameters[31].Value = model.GetTime;
            parameters[32].Value = model.IsMemberCancel;
            parameters[33].Value = model.MemberCancelRemarks;
            parameters[34].Value = model.IsComplete;
            parameters[35].Value = model.CompleteTime;
            parameters[36].Value = model.IsInvoice;
            parameters[37].Value = model.InvoiceCode;
            parameters[38].Value = model.InvoiceType1;
            parameters[39].Value = model.InvoiceType2;
            parameters[40].Value = model.InvoiceTitle;
            parameters[41].Value = model.InvoiceRemarks;
            parameters[42].Value = model.IsCheckIfRealOrder;
            parameters[43].Value = model.CheckIfRealOrderTime;
            parameters[44].Value = model.CheckIfRealOrderPerson;
            parameters[45].Value = model.CheckIfRealOrderRemarks;
            parameters[46].Value = model.SellerId;
            parameters[47].Value = model.SellerType;
            parameters[48].Value = model.ztTime;
            parameters[49].Value = model.ztTimeEnd;
            parameters[50].Value = model.isComment;
            parameters[51].Value = model.CommentTime;
            parameters[52].Value = model.marketId;
            parameters[53].Value = model.isDis;
            parameters[54].Value = model.DisTime;
            parameters[55].Value = model.building_id;
            parameters[56].Value = model.couponId;
            parameters[57].Value = model.oldMoney;
            parameters[58].Value = model.OrderId;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);


        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModOrder GetModel(string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_Order_Info ");
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.VarChar,50)};
            parameters[0].Value = OrderId;

            ModOrder model = new ModOrder();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                model.OrderType = ds.Tables[0].Rows[0]["OrderType"].ToString();
                model.MemberId = ds.Tables[0].Rows[0]["MemberId"].ToString();
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                if (ds.Tables[0].Rows[0]["OrderTime"].ToString() != "")
                {
                    model.OrderTime = DateTime.Parse(ds.Tables[0].Rows[0]["OrderTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderAllMoney"].ToString() != "")
                {
                    model.OrderAllMoney = decimal.Parse(ds.Tables[0].Rows[0]["OrderAllMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GoodsAllMoney"].ToString() != "")
                {
                    model.GoodsAllMoney = decimal.Parse(ds.Tables[0].Rows[0]["GoodsAllMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Freight"].ToString() != "")
                {
                    model.Freight = decimal.Parse(ds.Tables[0].Rows[0]["Freight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TicketMoney"].ToString() != "")
                {
                    model.TicketMoney = decimal.Parse(ds.Tables[0].Rows[0]["TicketMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DiscountMoeny"].ToString() != "")
                {
                    model.DiscountMoeny = decimal.Parse(ds.Tables[0].Rows[0]["DiscountMoeny"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TempChangeMoney"].ToString() != "")
                {
                    model.TempChangeMoney = decimal.Parse(ds.Tables[0].Rows[0]["TempChangeMoney"].ToString());
                }
                model.ReceivePerson = ds.Tables[0].Rows[0]["ReceivePerson"].ToString();
                model.ReceiveAddressCode = ds.Tables[0].Rows[0]["ReceiveAddressCode"].ToString();
                model.ReceiveAddressDetail = ds.Tables[0].Rows[0]["ReceiveAddressDetail"].ToString();
                model.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                model.MemberOrderRemarks = ds.Tables[0].Rows[0]["MemberOrderRemarks"].ToString();
                model.AdminOrderRemarks = ds.Tables[0].Rows[0]["AdminOrderRemarks"].ToString();
                if (ds.Tables[0].Rows[0]["OrderAllWeight"].ToString() != "")
                {
                    model.OrderAllWeight = int.Parse(ds.Tables[0].Rows[0]["OrderAllWeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsPay"].ToString() != "")
                {
                    model.IsPay = int.Parse(ds.Tables[0].Rows[0]["IsPay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayTime"].ToString() != "")
                {
                    model.PayTime = DateTime.Parse(ds.Tables[0].Rows[0]["PayTime"].ToString());
                }
                model.PayInterfaceCode = ds.Tables[0].Rows[0]["PayInterfaceCode"].ToString();
                model.PayMethod = ds.Tables[0].Rows[0]["PayMethod"].ToString();
                if (ds.Tables[0].Rows[0]["IsSend"].ToString() != "")
                {
                    model.IsSend = int.Parse(ds.Tables[0].Rows[0]["IsSend"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SendTime"].ToString() != "")
                {
                    model.SendTime = DateTime.Parse(ds.Tables[0].Rows[0]["SendTime"].ToString());
                }
                model.SendNumber = ds.Tables[0].Rows[0]["SendNumber"].ToString();
                model.SendCompany = ds.Tables[0].Rows[0]["SendCompany"].ToString();
                model.SendMethod = ds.Tables[0].Rows[0]["SendMethod"].ToString();
                model.SendMethodByReal = ds.Tables[0].Rows[0]["SendMethodByReal"].ToString();
                if (ds.Tables[0].Rows[0]["IsGet"].ToString() != "")
                {
                    model.IsGet = int.Parse(ds.Tables[0].Rows[0]["IsGet"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GetTime"].ToString() != "")
                {
                    model.GetTime = DateTime.Parse(ds.Tables[0].Rows[0]["GetTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsMemberCancel"].ToString() != "")
                {
                    model.IsMemberCancel = int.Parse(ds.Tables[0].Rows[0]["IsMemberCancel"].ToString());
                }
                model.MemberCancelRemarks = ds.Tables[0].Rows[0]["MemberCancelRemarks"].ToString();
                if (ds.Tables[0].Rows[0]["IsComplete"].ToString() != "")
                {
                    model.IsComplete = int.Parse(ds.Tables[0].Rows[0]["IsComplete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CompleteTime"].ToString() != "")
                {
                    model.CompleteTime = DateTime.Parse(ds.Tables[0].Rows[0]["CompleteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsInvoice"].ToString() != "")
                {
                    model.IsInvoice = int.Parse(ds.Tables[0].Rows[0]["IsInvoice"].ToString());
                }
                model.InvoiceCode = ds.Tables[0].Rows[0]["InvoiceCode"].ToString();
                model.InvoiceType1 = ds.Tables[0].Rows[0]["InvoiceType1"].ToString();
                model.InvoiceType2 = ds.Tables[0].Rows[0]["InvoiceType2"].ToString();
                model.InvoiceTitle = ds.Tables[0].Rows[0]["InvoiceTitle"].ToString();
                model.InvoiceRemarks = ds.Tables[0].Rows[0]["InvoiceRemarks"].ToString();
                if (ds.Tables[0].Rows[0]["IsCheckIfRealOrder"].ToString() != "")
                {
                    model.IsCheckIfRealOrder = int.Parse(ds.Tables[0].Rows[0]["IsCheckIfRealOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckIfRealOrderTime"].ToString() != "")
                {
                    model.CheckIfRealOrderTime = DateTime.Parse(ds.Tables[0].Rows[0]["CheckIfRealOrderTime"].ToString());
                }
                model.CheckIfRealOrderPerson = ds.Tables[0].Rows[0]["CheckIfRealOrderPerson"].ToString();
                model.CheckIfRealOrderRemarks = ds.Tables[0].Rows[0]["CheckIfRealOrderRemarks"].ToString();
                if (ds.Tables[0].Rows[0]["SellerId"].ToString() != "")
                {
                    model.SellerId = ds.Tables[0].Rows[0]["SellerId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SellerType"].ToString() != "")
                {
                    model.SellerType = ds.Tables[0].Rows[0]["SellerType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ztTime"].ToString() != "")
                {
                    model.ztTime = DateTime.Parse(ds.Tables[0].Rows[0]["ztTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ztTimeEnd"].ToString() != "")
                {
                    model.ztTimeEnd = DateTime.Parse(ds.Tables[0].Rows[0]["ztTimeEnd"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isComment"].ToString() != "")
                {
                    model.isComment = int.Parse(ds.Tables[0].Rows[0]["isComment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CommentTime"].ToString() != "")
                {
                    model.CommentTime = DateTime.Parse(ds.Tables[0].Rows[0]["CommentTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["marketId"].ToString() != "")
                {
                    model.marketId = (ds.Tables[0].Rows[0]["marketId"].ToString()).ToString();
                }
                if (ds.Tables[0].Rows[0]["isDis"].ToString() != "")
                {
                    model.isDis = Convert.ToInt32(ds.Tables[0].Rows[0]["isDis"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DisTime"].ToString() != "")
                {
                    model.DisTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["disTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["building_id"].ToString() != "")
                {
                    model.building_id = ds.Tables[0].Rows[0]["building_id"].ToString();
                }
                if (ds.Tables[0].Rows[0]["oldMoney"].ToString() != "")
                {
                    model.oldMoney = Convert.ToDecimal(ds.Tables[0].Rows[0]["oldMoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["couponId"].ToString() != "")
                {
                    model.couponId = ds.Tables[0].Rows[0]["couponId"].ToString();
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
