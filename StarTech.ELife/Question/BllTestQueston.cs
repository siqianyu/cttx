using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StarTech.ELife.Question
{
    public partial class BllTestQueston
    {
        private readonly DalTestQueston dal = new DalTestQueston();
        public BllTestQueston()
        { }
        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModelTestQueston model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ModelTestQueston model)
        {
            dal.Update(model);
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
        public ModelTestQueston GetModel(string sysnumber)
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

        #endregion  Method
    }
}
