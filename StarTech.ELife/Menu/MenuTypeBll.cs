using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StarTech.ELife.Menu
{
    public class MenuTypeBll
    {
        private readonly MenuTypeDal dal=new MenuTypeDal();




		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(MenuTypeModel model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MenuTypeModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string CategoryId)
		{
			
			return dal.Delete(CategoryId);
		}
        
        /// <summary>
        /// 删除一组id
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool Delete(int[] idList)
        {
            return dal.Delete(idList);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CategoryIdlist )
		{
			return dal.DeleteList(CategoryIdlist );
		}



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public MenuTypeModel GetModel(string CategoryId)
		{
			
			return dal.GetModel(CategoryId);
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
        public List<MenuTypeModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MenuTypeModel> DataTableToList(DataTable dt)
		{
			List<MenuTypeModel> modelList = new List<MenuTypeModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MenuTypeModel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new MenuTypeModel();
					if(dt.Rows[n]["CategoryId"]!=null && dt.Rows[n]["CategoryId"].ToString()!="")
					{
					model.CategoryId=dt.Rows[n]["CategoryId"].ToString();
					}
					if(dt.Rows[n]["CategoryName"]!=null && dt.Rows[n]["CategoryName"].ToString()!="")
					{
					model.CategoryName=dt.Rows[n]["CategoryName"].ToString();
					}
					if(dt.Rows[n]["CategoryLevel"]!=null && dt.Rows[n]["CategoryLevel"].ToString()!="")
					{
						model.CategoryLevel=int.Parse(dt.Rows[n]["CategoryLevel"].ToString());
					}
					if(dt.Rows[n]["PCategoryId"]!=null && dt.Rows[n]["PCategoryId"].ToString()!="")
					{
					model.PCategoryId=dt.Rows[n]["PCategoryId"].ToString();
					}
					if(dt.Rows[n]["CategoryPath"]!=null && dt.Rows[n]["CategoryPath"].ToString()!="")
					{
					model.CategoryPath=dt.Rows[n]["CategoryPath"].ToString();
					}
					if(dt.Rows[n]["RootId"]!=null && dt.Rows[n]["RootId"].ToString()!="")
					{
					model.RootId=dt.Rows[n]["RootId"].ToString();
					}
					if(dt.Rows[n]["CategoryToTypeId"]!=null && dt.Rows[n]["CategoryToTypeId"].ToString()!="")
					{
					model.CategoryToTypeId=dt.Rows[n]["CategoryToTypeId"].ToString();
					}
					if(dt.Rows[n]["CategoryFlag"]!=null && dt.Rows[n]["CategoryFlag"].ToString()!="")
					{
					model.CategoryFlag=dt.Rows[n]["CategoryFlag"].ToString();
					}
					if(dt.Rows[n]["Orderby"]!=null && dt.Rows[n]["Orderby"].ToString()!="")
					{
						model.Orderby=int.Parse(dt.Rows[n]["Orderby"].ToString());
					}
					if(dt.Rows[n]["Remarks"]!=null && dt.Rows[n]["Remarks"].ToString()!="")
					{
					model.Remarks=dt.Rows[n]["Remarks"].ToString();
					}
					if(dt.Rows[n]["Url"]!=null && dt.Rows[n]["Url"].ToString()!="")
					{
					model.Url=dt.Rows[n]["Url"].ToString();
					}
					if(dt.Rows[n]["icon"]!=null && dt.Rows[n]["icon"].ToString()!="")
					{
					model.icon=dt.Rows[n]["icon"].ToString();
					}
					modelList.Add(model);
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
    }
}
