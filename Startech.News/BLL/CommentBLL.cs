using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Startech.News
{
    public class CommentBLL
    {
        private static readonly CommentDAL dal = new CommentDAL();

        public CommentBLL()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CommentModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(CommentModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int CommentID)
        {
            dal.Delete(CommentID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CommentModel GetModel(int CommentID)
        {
            return dal.GetModel(CommentID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public DataSet GetArticleList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return new CommentDAL().GetArticleList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        #endregion  成员方法
    }
}
