using System;
using System.Collections.Generic;
using System.Text;
using Startech.Member;
using System.Data;

namespace Startech.Member
{

    /// <summary>
    /// Postman
    /// </summary>
    public partial class BllPostman
    {
        private DalPostman dal = new DalPostman();
        public BllPostman()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModPostman model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModPostman model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int postman_id)
        {

            return dal.Delete(postman_id);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModPostman GetModel(int postman_id)
        {

            return dal.GetModel(postman_id);
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ModPostman> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ModPostman> DataTableToList(DataTable dt)
        {
            List<ModPostman> modelList = new List<ModPostman>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ModPostman model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }


        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
