using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Startech.News
{
    public class NewsBLL
    {
        private readonly NewsDAL dal = new NewsDAL();

        public NewsBLL()
		{}

		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(NewsModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(NewsModel model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int NewsID)
		{
			
			dal.Delete(NewsID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public NewsModel GetModel(int NewsID)
		{
			
			return dal.GetModel(NewsID);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetNewsList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return dal.GetNewsList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        /// <summary>
        /// 全选审核通过
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveArticleAll(string newsIds)
        {
            dal.ApproveArticleAll(newsIds);
        }
        
        /// <summary>
        /// 全选取消审核
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveCellArticleAll(string newsIds)
        {
            dal.ApproveCellArticleAll(newsIds);
        }
          /// <summary>
        /// 更新点击率
        /// </summary>
        /// <param name="nid">新闻编号</param>
        public void UpdateHits(int nid)
        {
             dal.UpdateHits(nid);
        }
		#endregion  成员方法

        /// <summary>
        /// 返回特定条数的新闻
        /// </summary>
        /// <param name="num">个数</param>
        /// <param name="fields">返回字段</param>
        /// <param name="filter">条件</param>
        /// <param name="sort">排序</param>
        /// <returns>返回DataSet集合</returns>
        public DataSet GetNewsByTop(int num, string fields, string filter, string sort)
        {
            return dal.GetNewsByTop(num, fields, filter, sort);
        }

        /// <summary>
        /// 返回杭州质量网特定条数的数据
        /// </summary>
        /// <param name="num">个数</param>
        /// <param name="fields">返回字段</param>
        /// <param name="table">表名</param>
        /// <param name="filter">条件</param>
        /// <param name="sort">排序</param>
        /// <returns>返回DataSet集合</returns>
        public DataSet GetNewsTopById_hzzlw(int num, string fields, string table, string filter, string sort)
        {
            return dal.GetNewsTopById_hzzlw(num, fields, table, filter, sort);
        }

        /// <summary>
        /// 更新新闻的浏览次数
        /// </summary>
        /// <param name="newsid">新闻编号</param>
        /// <returns></returns>
        public int Update_hzzlw(string newsid)
        {
            return dal.Update_hzzlw(newsid);
        }
        //代码遗失公告
        public DataSet GetCodeLost(int intTop)
        {
            return dal.GetCodeLost(intTop);
        }
        public DataSet GetCodeLost_hzzlw(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return dal.GetCodeLost_hzzlw(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }
    }
}
