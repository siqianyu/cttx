using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Startech.Member.Member
{
    public class MemberInfoBLL
    {
        private readonly MemberInfoDAL dal=new MemberInfoDAL();
        public MemberInfoBLL()
		{}
		#region  成员方法
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(MemberInfoModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(MemberInfoModel model)
        {
            return dal.Update(model);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int memberId)
		{
			dal.Delete(memberId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public MemberInfoModel GetModel(int memberId)
		{
			return dal.GetModel(memberId);
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

        #region 自定义方法
        public int CheckUserName(string name)
        {
            return dal.CheckUserName(name);
        }
        public string CheckUserPwd(string name, string pwd)
        {
            return dal.CheckUserPwd(name,pwd);
        }
        public int UpdatePwd(string name, string pwd)
        {
            return dal.UpdatePwd(name, pwd);
        }
        public void Buy(double moeny, int id, int type)
        {
            dal.Buy(moeny, id, type);
        }
        #endregion
    }
}
