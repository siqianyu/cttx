using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Startech.Category;

namespace StarTech.ELife.Article
{
    public class ArticleBll
    {
        public ArticleBll()
        { }
        public ArticleDal dal = new ArticleDal();

        #region  成员方法
        //public int Add(ArticleModel model)
        //{
        //    int categoryId = model.CategoryId;
        //    CategoryModel Ctymodel = new CategoryDAL().GetCategoryDetail(categoryId);
        //    if (Ctymodel.Type == 1)
        //        return dal.Add(model);
        //    else
        //    {
        //        int count = dal.GetRecordCount(String.Format("CategoryId={0}", categoryId));
        //        if (count == 0)
        //            return dal.Add(model);
        //        else return -2;
        //    }

        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ArticleModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ArticleModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ArticleId)
        {

            return dal.Delete(ArticleId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ArticleIdlist)
        {
            return dal.DeleteList(ArticleIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ArticleModel GetModel(int ArticleId)
        {

            return dal.GetModel(ArticleId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public ArticleModel GetModelByCache(int ArticleId)
        //{

        //    string CacheKey = "T_ArticleModel-" + ArticleId;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(ArticleId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (ArticleModel)objModel;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ArticleModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ArticleModel> DataTableToList(DataTable dt)
        {
            List<ArticleModel> modelList = new List<ArticleModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ArticleModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}



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
            return dal.GetArticleList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public bool Approve(string articleId)
        {
            return dal.Approve(articleId);
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public bool UnApprove(string articleId)
        {
            return dal.UnApprove(articleId);
        }


        #endregion  成员方法
    }
}
