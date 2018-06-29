using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using Startech.Utils;
using System.Data;
using System.Data.SqlClient;

namespace StarTech.Order.Seller
{
    public class GrabOrder
    {
        protected static AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        

        /// <summary>
        /// 1，创建临时抢单库（用户付款后，创建抢单库，将任务写入其中）
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public static int CreateWaitingDeal(string orderId)
        {
            orderId=KillSqlIn.Form_ReplaceByString(orderId,50);
            string strSQL = "select * from T_Order_Info where orderId='"+orderId+"';";
            strSQL += "select * from T_Order_InfoDetail where orderId='"+orderId+"';";
            string guid = Guid.NewGuid().ToString();
            SqlErr(strSQL, "CreateWaitingDeal");
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);

            //strSQL += "insert T_Order_WaitingDeal select '"+guid+"',";
            if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count < 1 || ds.Tables[1].Rows.Count < 1)
                return -1;
            strSQL = "";
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                string sysnumber = Guid.NewGuid().ToString()+""+i;
                string detailId = ds.Tables[1].Rows[i]["sysnumber"].ToString();
                string goodsName = ds.Tables[1].Rows[i]["goodsName"].ToString(); ;
                string goodsId = ds.Tables[1].Rows[i]["goodsId"].ToString(); ;
                int num = Convert.ToInt32(ds.Tables[1].Rows[i]["Quantity"].ToString());
                decimal price = Convert.ToDecimal(ds.Tables[1].Rows[i]["price"].ToString());
                decimal totalprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["OrderAllMoney"].ToString());
                DateTime payTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["payTime"].ToString());
                DateTime start = Convert.ToDateTime(ds.Tables[0].Rows[0]["ztTime"].ToString());
                DateTime end = Convert.ToDateTime(ds.Tables[0].Rows[0]["ztTimeEnd"].ToString());
                string marketId = ds.Tables[0].Rows[0]["marketId"].ToString();
                strSQL += "insert into T_Order_WaitingDeal values('"+sysnumber+"','"+detailId+"','"+orderId+"',0,'1991-1-1',0,'1991-1-1','','"+goodsName+"','"+goodsId+"',"+num+","+price+","+totalprice+",'"+payTime+"','"+start+"','"+end+"','"+marketId+"','');";

            }
            int rows=adoHelper.ExecuteSqlNonQuery(strSQL);
            return rows;

        }

        /// <summary>
        /// 2.获取该商家所出售的任务（商家登录后，率先获取商家可出售的任务）
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string goodsList(string shopId)
        {
            shopId = KillSqlIn.Form_ReplaceByString(shopId, 20);
            string strSQL = "select goodsId from T_Shop_Goods where shopid='"+shopId+"';";
            SqlErr(strSQL, "goodsList");
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
            string goodslist = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i != 0)
                    goodslist += ",";
                goodslist += ds.Tables[0].Rows[i][0];
            }
            return goodslist;
        }
        /// <summary>
        /// 3.返回信誉参数，根绝卖家的相关条件，比如开店时间，保证金，来确认信誉，越高的信誉返回越高的值，这个值将影响卖家抢单表的速度
        /// </summary>
        /// <returns></returns>
        public static float GetCredit(string shopId)
        {
            shopId = KillSqlIn.Form_ReplaceByString(shopId, 20);
            string strSQL = "select * from T_Shop_User where shopId='" + shopId + "';";
            SqlErr(strSQL, "GetCredit");
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
            float credit = 0.0f;
            if (ds == null || ds.Tables.Count < 1 && ds.Tables[0].Rows.Count < 1)
                return 0.0f;
            if (ds.Tables[0].Rows[0]["ShopMoney"] != DBNull.Value && Convert.ToDecimal(ds.Tables[0].Rows[0]["ShopMoney"]) > 0)
                credit += 0.5f;
            if (ds.Tables[0].Rows[0]["ApplyTime"] != DBNull.Value)
            {
                DateTime applytime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ApplyTime"]);
                int c=(DateTime.Now - applytime).Days/365;
                credit += 0.1f * c;
            }
            return credit;
        }


        /// <summary>
        /// 获取商家绑定会员
        /// </summary>
        /// <returns></returns>
        public static float GetShopMember(string orderId,string shopId)
        {
            string strSQL = "select * from T_Shop_MemberBind where memberId=(select memberId from T_Order_Info where orderId='"+orderId+"' and shopId='"+shopId+"');";
            SqlErr(strSQL, "GetShopMember");
            DataSet ds=adoHelper.ExecuteSqlDataset(strSQL);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                return 60;
            return 0;
        }


        /// <summary>
        /// 4.给卖家显示可以进行抢单的订单表，通过信誉度和卖家出售的任务种类，给他返回相应的数据
        /// </summary>
        /// <param name="shopId">卖家id</param>
        /// <param name="credit">信誉度</param>
        /// <param name="goodsList">买家出售的任务列表</param>
        /// <returns></returns>
        public static DataTable StartGrabOrder(string shopId,float credit,string goodsList)
        {
            
                shopId = KillSqlIn.Form_ReplaceByString(shopId, 10);
                goodsList = KillSqlIn.Form_ReplaceByString(goodsList, Int32.MaxValue);
                int ltime = Convert.ToInt32(30 - 30 * credit);
                string sqlGoodsList = "";
                for (int i = 0; i < goodsList.Split(',').Length; i++)
                {
                    if (i != 0)
                        sqlGoodsList += ",";
                    sqlGoodsList += "'" + goodsList.Split(',')[i] + "'";
                }
                string strSQL = "select w.*,pic=(select GoodsSmallPic from T_Goods_Info  g where g.goodsid=w.goodsId),unit=(select Uint from T_Goods_Info  g where g.goodsid=w.goodsId) from T_Order_WaitingDeal w where marketId=(select top 1 marketId from T_Shop_User u where u.shopid='" + shopId + "') and  dateadd(ss," + ltime + ",paytime)<='" + DateTime.Now + "' and sendEndTime>GETDATE() and orderId in (select orderId from T_Order_WaitingDeal where  goodsId in (" + sqlGoodsList + ") and isGrab=0  )  order by paytime desc;";
                SqlErr(strSQL, "StartGrabOrder");
                DataSet ds = adoHelper.ExecuteSqlDataset(strSQL); 

                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    return null;
                }
                //return ds.Tables[0];
                
                DataTable dt = ds.Tables[0];
                //dt.Columns.Add("goodsValue");
                DataTable dtValue = new DataTable();
                dtValue.Columns.Add("orderId");
                dtValue.Columns.Add("ordertotalPrice");
                dtValue.Columns.Add("goodsList");
                dtValue.Columns.Add("priceList");
                dtValue.Columns.Add("sendStartTime");
                dtValue.Columns.Add("sendEndTime");
                dtValue.Columns.Add("compatibility");
                dtValue.Columns.Add("payTime");
                dtValue.Columns.Add("getTime");
                dtValue.Columns.Add("detailId");
                dtValue.Columns.Add("statu");
                dtValue.Columns.Add("pic");
                dtValue.Columns.Add("unit");
                dtValue.Columns.Add("num");
                DataRow[] dr = dt.Select(" 1=1 ", "orderId desc");

                string ord = "";
                if (dr.Length > 0)
                {
                    ord = dr[0]["orderId"].ToString();
                }
                else
                    return null;
                List<string> goodsListOrder = new List<string>();
                List<string> goodsNamelist = new List<string>();
                List<string> detailIdList = new List<string>();
                List<string> goodsPriceList = new List<string>();
                List<string> statuList = new List<string>();
                List<string> picList = new List<string>();
                List<string> unitList = new List<string>();
                List<string> numList = new List<string>();
                string[] goodslist2 = goodsList.Split(',');

                for (int i = 0; i < dr.Length; i++)
                {

                    if (dr[i]["orderId"].ToString() == ord)
                    {
                        goodsListOrder.Add(dr[i]["goodsId"].ToString());
                        goodsNamelist.Add(dr[i]["goodsName"].ToString());
                        detailIdList.Add(dr[i]["detailId"].ToString());
                        goodsPriceList.Add(dr[i]["price"].ToString());
                        picList.Add(dr[i]["pic"].ToString());
                        unitList.Add(dr[i]["unit"].ToString());
                        numList.Add(dr[i]["num"].ToString());

                        if (dr[i]["isgrab"].ToString() == "1")
                        {
                            statuList.Add("0");
                        }
                        else if (!("," + goodsList + ",").Contains(("," + dr[i]["goodsId"].ToString().Trim() + ",")))
                        {
                            statuList.Add("-1");

                        }
                        else
                        {
                            statuList.Add("1");
                        }

                        if (i == dr.Length - 1)
                        {
                            DataRow dr2 = dtValue.NewRow();
                            float gv = GetGoodsValue(goodsListOrder, goodslist2);
                            string strName = "";
                            string strDetail = "";
                            string strPrice = "";
                            string strStatu = "";
                            string strPic = "";
                            string strUnit = "";
                            string strNum = "";
                            
                            DateTime getTime = Convert.ToDateTime(dr[i]["payTime"]).AddSeconds(30 - 30 * gv+GetShopMember(dr[i]["orderId"].ToString(),shopId));
                            for (int j = 0; j < goodsNamelist.Count; j++)
                            {
                                if (j != 0)
                                {
                                    strName += ",";
                                    strDetail += ",";
                                    strPrice += ",";
                                    strStatu += ",";
                                    strPic += ",";
                                    strUnit += ",";
                                    strNum += ",";
                                }
                                if (detailIdList.Count > j)
                                    strDetail += detailIdList[j];
                                else
                                    strDetail += "";
                                strName += goodsNamelist[j];
                                strPrice += goodsPriceList[j];
                                strStatu += statuList[j];
                                strPic +=picList[j];
                                strUnit +=unitList[j];
                                strNum += numList[j];
                            }
                            dr2.ItemArray = new object[]{ 
                    ord,
                    dr[i]["ordertotalPrice"],
                    strName,
                    strPrice,
                    dr[i]["sendStartTime"],
                    dr[i]["sendEndTime"],
                    gv,
                    dr[i]["payTime"],
                    getTime,
                    strDetail,
                    strStatu,
                    strPic,
                    strUnit,
                    strNum
                    };
                            goodsListOrder.Clear();
                            goodsNamelist.Clear();
                            detailIdList.Clear();
                            goodsPriceList.Clear();
                            statuList.Clear();
                            picList.Clear();
                            numList.Clear();
                            dtValue.Rows.Add(dr2);
                            ord = dr[i]["orderId"].ToString();
                        }
                    }
                    else
                    {
                        DataRow dr2 = dtValue.NewRow();
                        float gv = GetGoodsValue(goodsListOrder, goodslist2);
                        string strName = "";
                        string strDetail = "";
                        string strPrice = "";
                        string strStatu = "";
                        string strPic = "";
                        string strUnit = "";
                        string strNum = "";
                        DateTime getTime = Convert.ToDateTime(dr[i]["payTime"]).AddSeconds(30 - 30 * gv);
                        for (int j = 0; j < goodsNamelist.Count; j++)
                        {
                            if (j != 0)
                            {
                                strName += ",";
                                strPrice += ",";
                                strDetail += ",";
                                strStatu += ",";
                                strPic += ",";
                                strUnit += ",";
                                strNum += ",";
                            }
                            strName += goodsNamelist[j];
                            if (detailIdList.Count > j)
                                strDetail += detailIdList[j];
                            else
                                strDetail += "";

                            if (goodsPriceList.Count > j)
                                strPrice += goodsPriceList[j];
                            else
                                strPrice += "";
                            strStatu += statuList[j];
                            strPic +=picList[j];
                            strUnit += unitList[j];
                            strNum += numList[j];
                        }
                        dr2.ItemArray = new object[]{ 
                    ord,
                    dr[i-1]["ordertotalPrice"],
                    strName,
                    strPrice,
                    dr[i-1]["sendStartTime"],
                    dr[i-1]["sendEndTime"],
                    gv,
                    dr[i-1]["payTime"],
                    getTime,
                    strDetail,
                    strStatu,
                    strPic,
                    strUnit,
                    strNum
                    };
                        goodsListOrder.Clear();
                        goodsNamelist.Clear();
                        detailIdList.Clear();
                        goodsPriceList.Clear();
                        statuList.Clear();
                        picList.Clear();
                        unitList.Clear();
                        numList.Clear();
                        dtValue.Rows.Add(dr2);

                        goodsListOrder.Add(dr[i]["goodsId"].ToString());
                        goodsNamelist.Add(dr[i]["goodsName"].ToString());
                        detailIdList.Add(dr[i]["detailId"].ToString());
                        goodsPriceList.Add(dr[i]["price"].ToString());
                        picList.Add(dr[i]["pic"].ToString());
                        unitList.Add(dr[i]["unit"].ToString());
                        numList.Add(dr[i]["num"].ToString());

                        if (dr[i]["isgrab"].ToString() == "1")
                        {
                            statuList.Add("0");
                        }else if (!("," + goodsList + ",").Contains(("," + dr[i]["goodsId"].ToString().Trim() + ",")))
                        {
                            statuList.Add("-1");
                        }

                        else
                        {
                            statuList.Add("1");
                        }

                        ord = dr[i]["orderId"].ToString();

                        if (i == dr.Length - 1)
                        {
                            dr2 = dtValue.NewRow();
                            gv = GetGoodsValue(goodsListOrder, goodslist2);
                            strName = "";
                            strDetail = "";
                            strPrice = "";
                            strStatu = "";
                            strPic = "";
                            strUnit = "";
                            strNum = "";
                            getTime = Convert.ToDateTime(dr[i]["payTime"]).AddSeconds(30 - 30 * gv);
                            for (int j = 0; j < goodsNamelist.Count; j++)
                            {
                                if (j != 0)
                                {
                                    strName += ",";
                                    strDetail += ",";
                                    strPrice += ",";
                                    strStatu += ",";
                                    strPic += ",";
                                    strUnit += ",";
                                    strNum += ",";
                                }
                                if (detailIdList.Count > j)
                                    strDetail += detailIdList[j];
                                else
                                    strDetail += "";
                                strName += goodsNamelist[j];
                                strPrice += goodsPriceList[j];
                                strStatu += statuList[j];
                                strPic += picList[j];
                                strUnit += unitList[j];
                                strNum += numList[j];
                            }
                            dr2.ItemArray = new object[]{ 
                    ord,
                    dr[i]["ordertotalPrice"],
                    strName,
                    strPrice,
                    dr[i]["sendStartTime"],
                    dr[i]["sendEndTime"],
                    gv,
                    dr[i]["payTime"],
                    getTime,
                    strDetail,
                    strStatu,
                    strPic,
                    strUnit,
                    strNum
                    };
                            goodsListOrder.Clear();
                            goodsNamelist.Clear();
                            detailIdList.Clear();
                            goodsPriceList.Clear();
                            statuList.Clear();
                            picList.Clear();
                            unitList.Clear();
                            numList.Clear();
                            dtValue.Rows.Add(dr2);
                            ord = dr[i]["orderId"].ToString();
                        }

                    }
                }
                DataRow[] drResult = dtValue.Select("getTime<='" + DateTime.Now + "'", "paytime desc");
                //return drResult;
                DataTable dtResult = dtValue.Clone();
                for (int i = 0; i < drResult.Length; i++)
                {
                    dtResult.Rows.Add(drResult[i].ItemArray);
                }
                return dtResult;

        }


        /// <summary>
        /// 5.完成抢单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static int GrabOrderNow(string orderId, string shopId,string goodsList)
        {

            orderId = KillSqlIn.Form_ReplaceByString(orderId, 50);
            shopId = KillSqlIn.Form_ReplaceByString(shopId, 50);
            string strSQL = " BEGIN TRANSACTION ";
            // "update T_Order_WaitingDeal set isGrab=1 , GrabTime=getdate(), shopId='" + shopId + "' where orderId='" + orderId + "'  ;";
            string strGoods = "select sysnumber=newId(),goodsId,goodsName,orderId,detailId,grabTime,0,1991-1-1,shopId from T_Order_WaitingDeal where orderId='" + orderId + "' and isGrab=0;";
            SqlErr(strGoods, "GrabOrderNow");
            DataSet ds = adoHelper.ExecuteSqlDataset(strGoods);

            string canSellGoods = "";
            string canSellDetail = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string[] strG = goodsList.Split(',');
                //bool canSell = false;
                for (int j = 0; j < strG.Length; j++)
                {
                    if (strG[j] == ds.Tables[0].Rows[i]["goodsId"].ToString())
                    {

                        canSellGoods += ",'" + ds.Tables[0].Rows[i]["GoodsId"] + "'";
                        canSellDetail += ",'" + ds.Tables[0].Rows[i]["detailId"] + "'";
                        strSQL += "insert T_Order_GrabLog values('" + ds.Tables[0].Rows[i][0] + "','" + ds.Tables[0].Rows[i][1] + "','" + ds.Tables[0].Rows[i][2] + "','" + ds.Tables[0].Rows[i][3] + "','" + ds.Tables[0].Rows[i][4] + "','" + ds.Tables[0].Rows[i][5] + "'," + ds.Tables[0].Rows[i][6] + ",'" + ds.Tables[0].Rows[i][7] + "','" + ds.Tables[0].Rows[i][8] + "','抢单');";
                
                        //canSell = true;
                        continue;
                    }
                    else
                    {
                        //canSell = false;
                    }
                }
                //if (canSell)
                //{
                //    canSellGoods += ",'"+ds.Tables[0].Rows[i]["GoodsId"]+"'";
                //    canSellDetail += ",'" + ds.Tables[0].Rows[i]["detailId"] + "'";
                //    strSQL += "insert T_Order_GrabLog values('" + ds.Tables[0].Rows[i][0] + "','" + ds.Tables[0].Rows[i][1] + "','" + ds.Tables[0].Rows[i][2] + "','" + ds.Tables[0].Rows[i][3] + "','" + ds.Tables[0].Rows[i][4] + "','" + ds.Tables[0].Rows[i][5] + "'," + ds.Tables[0].Rows[i][6] + ",'" + ds.Tables[0].Rows[i][7] + "','" + ds.Tables[0].Rows[i][8] + "','抢单');";
                //}
            }
            if (canSellGoods!="" && canSellGoods[0] == ',')
                canSellGoods = canSellGoods.Substring(1);
            if (canSellDetail != "" && canSellDetail[0] == ',')
                canSellDetail = canSellDetail.Substring(1);

            if (canSellDetail == "")
                return 0;
            strSQL += "update T_Order_InfoDetail set ProviderInfo='" + shopId + "' where orderId='" + orderId + "' and sysnumber in (" + canSellDetail + ") and (ProviderInfo='' or ProviderInfo is null);";
            strSQL += "update T_Order_WaitingDeal set isGrab=1 , GrabTime=getdate(), shopId='" + shopId + "' where orderId='" + orderId + "' and detailId in (" + canSellDetail + ") and isGrab=0  ;";
            strSQL += "  COMMIT TRANSACTION ";
            SqlErr(strSQL, "GrabOrderNow");
            int row = adoHelper.ExecuteSqlNonQuery(strSQL);
            return row;
        }

        ///// <summary>
        ///// 5.完成抢单
        ///// </summary>
        ///// <param name="orderId"></param>
        ///// <param name="shopId"></param>
        ///// <returns></returns>
        //public static int GrabOrderNow(string orderId,string shopId)
        //{
        //    orderId = KillSqlIn.Form_ReplaceByString(orderId, 50);
        //    shopId = KillSqlIn.Form_ReplaceByString(shopId, 50);
        //    string strSQL = "update T_Order_WaitingDeal set isGrab=1 , GrabTime=getdate(), shopId='"+shopId+"' where orderId='"+orderId+"';";
        //    string strGoods = "select sysnumber=newId(),goodsId,goodsName,orderId,detailId,grabTime,0,1991-1-1,shopId from T_Order_WaitingDeal where orderId='" + orderId + "';";
        //    DataSet ds = adoHelper.ExecuteSqlDataset(strGoods);
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        strSQL += "insert T_Order_GrabLog values('" + ds.Tables[0].Rows[i][0] + "','" + ds.Tables[0].Rows[i][1] + "','" + ds.Tables[0].Rows[i][2] + "','" + ds.Tables[0].Rows[i][3] + "','" + ds.Tables[0].Rows[i][4] + "','" + ds.Tables[0].Rows[i][5] + "'," + ds.Tables[0].Rows[i][6] + ",'" + ds.Tables[0].Rows[i][7] + "','" + ds.Tables[0].Rows[i][8] + "','抢单');";
        //    }
        //    strSQL += "update T_Order_InfoDetail set ProviderInfo='"+shopId+"' where orderId='"+orderId+"';";
        //    int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        //    return row;
        //}

        /// <summary>
        /// 获取任务匹配度
        /// </summary>
        /// <param name="goodsList"></param>
        /// <param name="shopList"></param>
        /// <returns></returns>
        static float GetGoodsValue(List<string> goodsList,string[]shopList)
        {
            int c = 0;

            for (int i = 0; i < shopList.Length; i++)
            {
                if (goodsList.Contains(shopList[i]))
                {
                    c++;
                }
            }
            float num = goodsList.Count+0.0f;
            return c/num;
        }


        

        /// <summary>
        /// 6.取消已经抢到的订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static int CancelGrabOrder(string orderId,string shopId)
        {
            orderId = KillSqlIn.Form_ReplaceByString(orderId, 50);
            shopId = KillSqlIn.Form_ReplaceByString(shopId, 50);
            string strSQL = "update T_Order_WaitingDeal set isGrab=0 , GrabTime=getdate(), shopId='" + shopId + "' where orderId='" + orderId + "';";
            string strGoods = "select sysnumber=newId(),goodsId,goodsName,orderId,detailId,grabTime,0,1991-1-1,shopId from T_Order_WaitingDeal where orderId='" + orderId + "';";
            DataSet ds = adoHelper.ExecuteSqlDataset(strGoods);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strSQL += "insert T_Order_GrabLog values('" + ds.Tables[0].Rows[i][0] + "','" + ds.Tables[0].Rows[i][1] + "','" + ds.Tables[0].Rows[i][2] + "','" + ds.Tables[0].Rows[i][3] + "','" + ds.Tables[0].Rows[i][4] + "','" + ds.Tables[0].Rows[i][5] + "'," + ds.Tables[0].Rows[i][6] + ",'" + ds.Tables[0].Rows[i][7] + "','" + ds.Tables[0].Rows[i][8] + "','取消已抢订单');";
            }
            strSQL += "update T_Order_InfoDetail set ProviderInfo='' where orderId='" + orderId + "';";
            SqlErr(strSQL, "CancelGrabOrder");
            int row = adoHelper.ExecuteSqlNonQuery(strSQL);
            return row;
        }

        /// <summary>
        /// 7.卖家完成配货
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static int DisOrder(string orderId, string shopId)
        {
            orderId = KillSqlIn.Form_ReplaceByString(orderId, 50);
            shopId = KillSqlIn.Form_ReplaceByString(shopId, 50);

            string strSQL = " BEGIN TRANSACTION ";
            string strGoods = "select sysnumber=newId(),goodsId,goodsName,orderId,detailId,grabTime,1,'" + DateTime.Now + "',shopId,num from T_Order_WaitingDeal where orderId='" + orderId + "' and shopId='"+shopId+"';";
            DataSet ds = adoHelper.ExecuteSqlDataset(strGoods);

            string goodsList = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i != 0)
                    goodsList += ",";
                if (ds.Tables[0].Rows[i]["shopId"].ToString() != shopId)
                {
                    continue;
                }
                strSQL += "insert T_Order_GrabLog values('" + ds.Tables[0].Rows[i][0] + "','" + ds.Tables[0].Rows[i][1] + "','" + ds.Tables[0].Rows[i][2] + "','" + ds.Tables[0].Rows[i][3] + "','" + ds.Tables[0].Rows[i][4] + "','" + ds.Tables[0].Rows[i][5] + "'," + ds.Tables[0].Rows[i][6] + ",'" + ds.Tables[0].Rows[i][7] + "','" + ds.Tables[0].Rows[i][8] + "','已配货');";
                strSQL += "update T_Shop_Goods set shopgoods_amount=shopgoods_amount-" + ds.Tables[0].Rows[i]["num"] + " where goodsId='" + ds.Tables[0].Rows[i][1] + "' and ShopId='"+shopId+"';";
                strSQL += "update T_Order_WaitingDeal set isDis=1 where detailId='"+ds.Tables[0].Rows[i]["detailId"]+"';";

            }
            strSQL += "if((select count(sysnumber) from T_Order_InfoDetail where orderId='" + orderId + "')=(select count(sysnumber) from T_Order_WaitingDeal where orderId='" + orderId + "' and isDis=1)) update T_Order_Info set isDis=1,disTime='" + DateTime.Now + "' where orderId='" + orderId + "';";
            strSQL += " COMMIT TRANSACTION ";
            SqlErr(strSQL, "DisOrder");
            int row = adoHelper.ExecuteSqlNonQuery(strSQL);
            return row;
        }


        public static int SqlErr(string strSQL,string url) {
            string strSQL2 = "insert IACenter_SQLERROR values(@sql,getdate(),@url,'')";
            SqlParameter[] sp ={
                                   new SqlParameter("@sql",SqlDbType.VarChar),
                                   new SqlParameter("@url",SqlDbType.VarChar)
                              };
            sp[0].Value=strSQL;
            sp[1].Value = url;
            
            return adoHelper.ExecuteSqlNonQuery(strSQL2,sp);
        }
    }
}
