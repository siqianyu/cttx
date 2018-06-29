﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StarTech.ELife.Base
{
    public class BuildingBll
    {
        public BuildingDal dal = new BuildingDal();
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(BuildingModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BuildingModel model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新编号
        /// </summary>
        /// <param name="newId"></param>
        /// <param name="oldId"></param>
        /// <returns></returns>
        public bool UpdateId(string newId, string oldId)
        {
            return dal.UpdateId(newId, oldId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Building_id)
        {

            return dal.Delete(Building_id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Building_idlist)
        {
            return dal.DeleteList(Building_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BuildingModel GetModel(string Building_id)
        {

            return dal.GetModel(Building_id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public BuildingModel GetModelByCache(string Building_id)
        //{

        //    string CacheKey = "T_Base_BuildingModel-" + Building_id;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(Building_id);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (BuildingModel)objModel;
        //}

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
        public List<BuildingModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<BuildingModel> DataTableToList(DataTable dt)
        {
            List<BuildingModel> modelList = new List<BuildingModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                BuildingModel model;
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
