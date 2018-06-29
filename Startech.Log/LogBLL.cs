using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Collections;

namespace Startech.Log
{
    public class LogBLL
    {
        /// <summary>
        /// 添加日志(登陆后,用户票据存在时使用)
        /// </summary>
        /// <param name="description">日志内容描述</param>
        /// <returns>成功,返回日志标识,否则,返回-1</returns>
        public static int AddLog(string description)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            string url = HttpContext.Current.Request.Path;
            //int userId = Convert.ToInt32(HttpContext.Current.Request.Cookies["__UserInfo"]["userId"]);
            int userId = 1;
            return LogBLL.AddLog(description, ipAddress, url, userId);
        }
        /// <summary>
        /// 添加日志 (登陆前使用)
        /// </summary>
        /// <param name="description"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int AddLog(string description, int userId)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            string url = HttpContext.Current.Request.Path;
            return LogBLL.AddLog(description, ipAddress, url, userId);
        }

        public static int AddLog(string description, string ip, string url, int userId)
        {
            return new LogDAL().Add(userId, ip, url, description);
        }

        /// <summary>
        /// 用户操作后插入日志
        /// </summary>
        /// <param name="model"></param>
        public void InsertLog(Startech.Log.LogModel model)
        {
            model.UserId = HttpContext.Current.Session["UserId"] == null ? 0 : int.Parse(HttpContext.Current.Session["UserId"].ToString());
            model.OperationDate = DateTime.Now;
            model.Url = HttpContext.Current.Request.Path;
            model.IP = HttpContext.Current.Request.UserHostAddress;

            new LogDAL().InsertLog(model);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="logIdList"></param>
        public void Delete(int[] logIdList)
        {
            new LogDAL().Delete(logIdList);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="logId"></param>
        public bool DeleteLog(int logId)
        {
            return new LogDAL().DeleteLog(logId);
        }

        /// <summary>
        /// 根据条件删除记录
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public bool Delete(string strWhere)
        {
            return new LogDAL().Delete(strWhere);
        }

        /// <summary>
        /// 检索日志记录
        /// </summary>
        /// <param name="where">检索条件</param>
        /// <param name="order">排序条件</param>
        /// <returns>满足条件的日志记录记录集</returns>
        //public DataSet Select(string where, string order)
        //{
        //    return new LogDAL().Select(where, order);
        //}
        public DataSet GetLogList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return new LogDAL().GetLogList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }
    }
}
