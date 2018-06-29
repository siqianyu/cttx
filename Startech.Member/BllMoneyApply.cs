using System;
using System.Data;
using System.Collections.Generic;
using Startech.Member;

namespace Startech.Member
{
    /// <summary>
    /// ModMoneyApply
    /// </summary>
    public class BllMoneyApply
    {
        private readonly DalMoneyApply dal = new DalMoneyApply();
        public BllMoneyApply()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModMoneyApply model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModMoneyApply model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int apply_id)
        {

            return dal.Delete(apply_id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModMoneyApply GetModel(int apply_id)
        {

            return dal.GetModel(apply_id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModMoneyApply GetModel(string shopid)
        {

            return dal.GetModel(shopid);
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }





        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

