using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Startech.Member
{
    public class BllShopUser
    {
        private DalShopUser dal = new DalShopUser();
        /// <summary>
        /// 增加一个数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(ModShopUser model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(ModShopUser model)
        {
            return dal.Update(model);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModShopUser GetModel(string ShopId)
        {
            return dal.GetModel(ShopId);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
    }
}
