using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StarTech.ELife.Base
{
   public class Goods_ServiceDetailBll
    {
       private readonly Goods_ServiceDetailDal dal = new Goods_ServiceDetailDal();
       /// <summary>
       /// 增加一条数据
       /// </summary>
       public bool Add(Goods_ServiceDetailModel model)
       {
           return dal.Add(model);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool Update(Goods_ServiceDetailModel model)
       {
           return dal.Update(model);
       }

       /// <summary>
       /// 删除一条数据
       /// </summary>
       public bool Delete(string sysnumber)
       {

           return dal.Delete(sysnumber);
       }
       
       /// <summary>
       /// 删除一条数据
       /// </summary>
       public bool DeleteList(string sysnumberlist)
       {
           return dal.DeleteList(sysnumberlist);
       }

       /// <summary>
       /// 得到一个对象实体
       /// </summary>
       public Goods_ServiceDetailModel GetModel(string sysnumber)
       {

           return dal.GetModel(sysnumber);
       }
       public Goods_ServiceDetailModel GetModel1(string serviceId)
       {
           return dal.GetModel1(serviceId);
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
       public List<Goods_ServiceDetailModel> GetModelList(string strWhere)
       {
           DataSet ds = dal.GetList(strWhere);
           return DataTableToList(ds.Tables[0]);
       }
       /// <summary>
       /// 获得数据列表
       /// </summary>
       public List<Goods_ServiceDetailModel> DataTableToList(DataTable dt)
       {
           List<Goods_ServiceDetailModel> modelList = new List<Goods_ServiceDetailModel>();
           int rowsCount = dt.Rows.Count;
           if (rowsCount > 0)
           {
               Goods_ServiceDetailModel model;
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
    }
}
