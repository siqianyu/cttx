using System;
using System.Collections.Generic;
using System.Text;


namespace StarTech.Order
{
    public class BllOrderDetail
    {
        private DalOrderDetail dal = new DalOrderDetail();
       
       /// <summary>
        /// 增加一条数据
        /// </summary>
       public int Add(ModOrderDetail model)
       {
           return dal.Add(model);
       }

                   
       /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModOrderDetail model)
        {
            return dal.Update(model);
        }

            
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModOrderDetail GetModel(string sysnumber)
        {
            return dal.GetModel(sysnumber);
        }
    }
}
