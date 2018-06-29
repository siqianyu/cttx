using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Goods
{
    public class GoodsFormateBll
    {
        GoodsFormateDal dal = new GoodsFormateDal();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(GoodsFormateModel model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(GoodsFormateModel model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public GoodsFormateModel GetModel(string sysnumber)
        {
            return dal.GetModel(sysnumber);
        }
    }
}
