using System;
using System.Collections.Generic;
using System.Text;
using Startech.Utils;
using StarTech.DBUtility;

namespace StarTech.Order.Seller
{


    public class PostMan
    {

        static AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        /// <summary>
        /// 派送员拒绝
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="postmanId"></param>
        /// <param name="context"></param>
        /// <param name="isDis"></param>
        /// <returns></returns>
        public static string PostManRefuse(string OrderId,string postmanId, string context, int isDis)
        {
            OrderId = KillSqlIn.Form_ReplaceByString(OrderId, 50);
            context = KillSqlIn.Form_ReplaceByString(context, 500);
            postmanId = KillSqlIn.Form_ReplaceByString(postmanId, 500);

            string strSQL = "update T_Order_info set issend=0 , isPostmanConfirm=-1,isdis=" + isDis + ",postmanid ='" + postmanId + "',ConfirmTime ='" + DateTime.Now + "',ConfirmRemark='" + context + "' where orderId='" + OrderId + "';";
            
            

            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
          
            if (rows > 0)
            {
                //adoHelper.ExecuteSqlNonQuery("update T_Order_WaitingDeal set isdis=0 where orderId='" + OrderId + "' ");
                return "1";
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 派送员签收
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="postmanId"></param>
        /// <param name="context"></param>
        /// <param name="isDis"></param>
        /// <returns></returns>
        public static int PostManComplete(string OrderId, string postmanId, string context)
        {
            OrderId = KillSqlIn.Form_ReplaceByString(OrderId, 50);
            context = KillSqlIn.Form_ReplaceByString(context, 500);
            postmanId = KillSqlIn.Form_ReplaceByString(postmanId, 500);

            string strSQL = "update T_Order_info set issend=1, SendTime=getdate(), isPostmanConfirm=1,postmanid ='" + postmanId + "',ConfirmTime ='" + DateTime.Now + "',ConfirmRemark='" + context + "' where orderId='" + OrderId + "';";

            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            if (rows > 0)
            {
                //adoHelper.ExecuteSqlNonQuery("update T_Order_WaitingDeal set isdis=0 where orderId='" + OrderId + "' ");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 拒绝签收
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="refuseContext"></param>
        /// <returns></returns>
        public static int RefuseGet(string OrderId, string refuseContext)
        {
            OrderId = KillSqlIn.Form_ReplaceByString(OrderId, 50);
            refuseContext = KillSqlIn.Form_ReplaceByString(refuseContext, 500);
            string strSQL = "update T_Order_info set isdis=0,isRefuseGet=1,isPostmanConfirm=0,refuseTime='" + DateTime.Now + "',refuseContext='" + refuseContext + "' where orderId='" + OrderId + "';";
            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            if (rows > 0)
            {
                adoHelper.ExecuteSqlNonQuery("update T_Order_WaitingDeal set isdis=0 where orderId='" + OrderId + "' ");
                return 1;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 完成签收
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="refuseContext"></param>
        /// <returns></returns>
        public static int Complete(string OrderId, string completeContext)
        {
            OrderId = KillSqlIn.Form_ReplaceByString(OrderId, 50);
            completeContext = KillSqlIn.Form_ReplaceByString(completeContext, 500);
            string strSQL = "update T_Order_info set isGet=1,gettime='" + DateTime.Now + "',GetRemarks='【派送员确认】" + completeContext + "' where orderId='" + OrderId + "';";
            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            if (rows > 0)
                return 1;
            else
                return 0;
        }
    }
}
