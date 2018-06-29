using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Startech.Category;

namespace Startech.Article
{
    public class ArticleBLL
    {
        private readonly ArticleDAL dal = new ArticleDAL();

        public ArticleBLL()
		{}

		#region  成员方法
        public int Add(ArticleModel model)
        {
            ArticleDAL article = new ArticleDAL();

            int categoryId = model.CategoryId;
            Startech.Category.CategoryModel Ctymodel = new CategoryDAL().GetCategoryDetail(categoryId);
            if (Ctymodel.Type == 1)
                return new ArticleDAL().Add(model);
            else
            {
                int count = article.GetRecordCount(String.Format("CategoryId={0}", categoryId));
                if (count == 0)
                    return new ArticleDAL().Add(model);
                else return -2;
            }

        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
        //public void  Add(ArticleModel model)
        //{
        //    dal.Add(model);
        //}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(ArticleModel model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ArticleId)
		{
			
			dal.Delete(ArticleId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ArticleModel GetModel(int ArticleId)
		{
			
			return dal.GetModel(ArticleId);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 得到文章数据列表
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetArticleList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return new ArticleDAL().GetArticleList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        /// <summary>
        /// 全选审核通过
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveArticleAll(string articleIds)
        {
            ArticleDAL article = new ArticleDAL();
            article.ApproveArticleAll(articleIds);
        }
        /// <summary>
        /// 全选取消审核
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveCellArticleAll(string articleIds)
        {
            ArticleDAL article = new ArticleDAL();
            article.ApproveCellArticleAll(articleIds);
        }

		#endregion  成员方法
    }
}
