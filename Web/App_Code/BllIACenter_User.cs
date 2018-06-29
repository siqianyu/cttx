using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StarTech.IACenter
{
    public class BllIACenter_User
    {
        private DalIACenter_User dal = new DalIACenter_User();
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModIACenter_User model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModIACenter_User model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int uniqueId)
        {
            return dal.Delete(uniqueId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModIACenter_User GetModel(int uniqueId)
        {
            return dal.GetModel(uniqueId);
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

        #endregion  成员方法
        
    }
}
