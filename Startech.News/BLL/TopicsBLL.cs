using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Startech.News
{
    public class TopicsBLL
    {
        private readonly TopicsDAL dal = new TopicsDAL();

        public TopicsBLL()
		{}
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(TopicsModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(TopicsModel model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int TopicsId)
		{
			
			dal.Delete(TopicsId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public TopicsModel GetModel(int TopicsId)
		{
			
			return dal.GetModel(TopicsId);
		}

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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

        public void UpdateHits(int nid)
        {
            dal.UpdateHits(nid);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetTopicsList(string fields, string filter, string sort, int currentPage, int pageSize, out int count)
        {
            return dal.GetTopicsList(fields, filter, sort, currentPage, pageSize, out count);
        }

		#endregion  成员方法
    }
}
