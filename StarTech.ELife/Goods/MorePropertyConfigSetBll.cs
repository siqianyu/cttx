using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.ELife.Goods
{
    public class MorePropertyConfigSetBll
    {
        private MorePropertyConfigSetDal dal = new MorePropertyConfigSetDal();
       
       /// <summary>
        /// 增加一条数据
        /// </summary>
       public int Add(MorePropertyConfigSetModel model)
       {
           return dal.Add(model);
       }

                   
       /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(MorePropertyConfigSetModel model)
        {
            return dal.Update(model);
        }

            
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MorePropertyConfigSetModel GetModel(string propertyId)
        {
            return dal.GetModel(propertyId);
        }
    }
}
