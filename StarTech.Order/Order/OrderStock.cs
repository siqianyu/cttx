using System;
using System.Collections.Generic;
using System.Text;
using Startech.Utils;
using System.Data;
using StarTech.DBUtility;

namespace StarTech.Order.Order
{
    public class OrderStock
    {
        private static AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        /// <summary>
        /// 农贸市场减少库存
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static int UpdateMarketStock(string orderId)
        {
            orderId=KillSqlIn.Form_ReplaceByString(orderId, 50);

            string strSQL = "select d.*,i.marketId from T_Order_infoDetail d,T_Order_Info i where d.orderid=i.orderid and i.orderId='"+orderId+"';";
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                return 0;
            strSQL = " begin TRANSACTION ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strSQL += "update T_Shop_Price set stock=stock-" + ds.Tables[0].Rows[i]["Quantity"]+" where goodsid='"+ds.Tables[0].Rows[i]["goodsId"]+"' and marketId='"+ds.Tables[0].Rows[i]["marketId"]+"';";
            }
            strSQL += " commit TRANSACTION";
            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            return rows;
        }

        /// <summary>
        /// 农贸市场恢复库存
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static int BackMarketStock(string orderId)
        {
            orderId = KillSqlIn.Form_ReplaceByString(orderId, 50);

            string strSQL = "select d.*,i.marketId from T_Order_infoDetail d,T_Order_Info i where d.orderid=i.orderid and i.orderId='" + orderId + "';";
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                return 0;
            strSQL = " begin TRANSACTION ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strSQL += "update T_Shop_Price set stock=stock+" + ds.Tables[0].Rows[i]["Quantity"] + " where goodsid='" + ds.Tables[0].Rows[i]["goodsId"] + "' and marketId='" + ds.Tables[0].Rows[i]["marketId"] + "';";
            }
            strSQL += " commit TRANSACTION";
            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            return rows;
        }

        /// <summary>
        /// 商家减少库存
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static int UpdateShopStock(string orderId,string shopId)
        {
            orderId = KillSqlIn.Form_ReplaceByString(orderId, 50);
            shopId = KillSqlIn.Form_ReplaceByString(shopId, 50);
            string strSQL = "select * from T_Order_WaitingDeal where orderId='" + orderId + "' and shopid='" + shopId + "';";
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                return 0;
            strSQL = " begin TRANSACTION ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strSQL += "update T_Shop_Goods set stock=stock-" + ds.Tables[0].Rows[i]["Quantity"] + " where goodsid='" + ds.Tables[0].Rows[i]["goodsId"] + "' and shopId='"+shopId+"';";
            }
            strSQL += " commit TRANSACTION";
            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            return rows;
        }

        /// <summary>
        /// 商家恢复库存
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static int BackShopStock(string orderId,string shopId)
        {
            orderId = KillSqlIn.Form_ReplaceByString(orderId, 50);
            shopId = KillSqlIn.Form_ReplaceByString(shopId, 50);

            string strSQL = "select * from T_Order_WaitingDeal where orderId='" + orderId + "' and shopid='"+shopId+"';";
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                return 0;
            strSQL = " begin TRANSACTION ";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strSQL += "update T_Shop_Goods set stock=stock+" + ds.Tables[0].Rows[i]["Quantity"] + " where goodsid='" + ds.Tables[0].Rows[i]["goodsId"] + "' and shopId='"+shopId+"';";
            }
            strSQL += " commit TRANSACTION";
            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            return rows;
        }
    }
}
