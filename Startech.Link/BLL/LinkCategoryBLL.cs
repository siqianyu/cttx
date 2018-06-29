using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Startech.Link
{
    public class LinkCategoryBLL
    {
        private readonly LinkCategoryDAL dal = new LinkCategoryDAL();

        public LinkCategoryBLL()
		{}

		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LinkCategoryModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(LinkCategoryModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ID)
		{
			
			return dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public LinkCategoryModel GetModel(int ID)
		{
			
			return dal.GetModel(ID);
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
        public DataSet GetLinkCategoryList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return dal.GetLinkCategoryList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        /// <summary>
        /// 添加类别权限
        /// </summary>
        /// <param name="categoryId">类别id</param>
        public void UpdateCategoryPermission(int categoryId)
        {
            dal.UpdateCategoryPermission(categoryId);
        }

        public DataSet GetAllCategoryItems()
        {
            return dal.GetAllCategoryItems();
        }

        #endregion  成员方法
    }
}
