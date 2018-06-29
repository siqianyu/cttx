using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Startech.Link
{
    public class LinkBLL
    {
        private readonly LinkDAL dal = new LinkDAL();

        public LinkBLL()
		{}

		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(LinkModel model)
        {
            return dal.Add(model);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(LinkModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int LinkId)
		{
			return 
			dal.Delete(LinkId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public LinkModel GetModel(int LinkId)
		{
			return dal.GetModel(LinkId);
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
        public DataSet GetLinkList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return dal.GetLinkList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }

		#endregion  成员方法
    }
}
