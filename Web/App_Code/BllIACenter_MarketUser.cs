using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StarTech.IACenter
{
    public class BllIACenter_MarketUser
    {
        private DalIACenter_MarketUser dal = new DalIACenter_MarketUser();
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModIACenter_MarketUser model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModIACenter_MarketUser model)
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
        public ModIACenter_MarketUser GetModel(int uniqueId)
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
