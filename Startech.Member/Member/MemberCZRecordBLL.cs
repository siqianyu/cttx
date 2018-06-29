using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Startech.Member.Member
{
    public class MemberCZRecordBLL
    {
        private readonly MemberCZRecordDAL dal = new MemberCZRecordDAL();
        public MemberCZRecordBLL()
		{}
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(MemberCZRecordModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(MemberCZRecordModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string sysnumber)
		{
			dal.Delete(sysnumber);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public MemberCZRecordModel GetModel(string sysnumber)
		{
			return dal.GetModel(sysnumber);
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
        public DataSet GetPageList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return dal.GetPageList(fields, filter, sort, currentPageIndex, pageSize, out recordCount);
        }

		#endregion  成员方法
    }
}
