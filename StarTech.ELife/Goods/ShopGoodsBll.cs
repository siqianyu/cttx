using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StarTech.ELife.Goods
{
    public class ShopGoodsBll
    {
        private readonly ShopGoodsDal dal=new ShopGoodsDal();
		 
		#region  BasicMethod
		 

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ShopGoodsModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(ShopGoodsModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(Guid shopgoods_id)
		{
			
			return dal.Delete(shopgoods_id);
		}
	 
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ShopGoodsModel GetModel(Guid shopgoods_id)
		{
			
			return dal.GetModel(shopgoods_id);
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
		public List<ShopGoodsModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ShopGoodsModel> DataTableToList(DataTable dt)
		{
			List<ShopGoodsModel> modelList = new List<ShopGoodsModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ShopGoodsModel model;
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
    }
}
