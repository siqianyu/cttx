using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StarTech.DBUtility;

namespace StarTech.ELife.Goods
{
    public class GoodsBll
    {
        private readonly GoodsDal dal=new GoodsDal();




		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(GoodsModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(GoodsModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string GoodsId)
		{
			
			return dal.Delete(GoodsId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string GoodsIdlist )
		{
			return dal.DeleteList(GoodsIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public GoodsModel GetModel(string GoodsId)
		{
			
			return dal.GetModel(GoodsId);
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
		public List<GoodsModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<GoodsModel> DataTableToList(DataTable dt)
		{
			List<GoodsModel> modelList = new List<GoodsModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				GoodsModel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new GoodsModel();
					if(dt.Rows[n]["GoodsId"]!=null && dt.Rows[n]["GoodsId"].ToString()!="")
					{
					model.GoodsId=dt.Rows[n]["GoodsId"].ToString();
					}
					if(dt.Rows[n]["CategoryId"]!=null && dt.Rows[n]["CategoryId"].ToString()!="")
					{
					model.CategoryId=dt.Rows[n]["CategoryId"].ToString();
					}
					if(dt.Rows[n]["GoodsToTypeId"]!=null && dt.Rows[n]["GoodsToTypeId"].ToString()!="")
					{
					model.GoodsToTypeId=dt.Rows[n]["GoodsToTypeId"].ToString();
					}
					if(dt.Rows[n]["BrandId"]!=null && dt.Rows[n]["BrandId"].ToString()!="")
					{
					model.BrandId=dt.Rows[n]["BrandId"].ToString();
					}
					if(dt.Rows[n]["GoodsName"]!=null && dt.Rows[n]["GoodsName"].ToString()!="")
					{
					model.GoodsName=dt.Rows[n]["GoodsName"].ToString();
					}
					if(dt.Rows[n]["GoodsCode"]!=null && dt.Rows[n]["GoodsCode"].ToString()!="")
					{
					model.GoodsCode=dt.Rows[n]["GoodsCode"].ToString();
					}
					if(dt.Rows[n]["Uint"]!=null && dt.Rows[n]["Uint"].ToString()!="")
					{
					model.Uint=dt.Rows[n]["Uint"].ToString();
					}
					if(dt.Rows[n]["Weight"]!=null && dt.Rows[n]["Weight"].ToString()!="")
					{
						model.Weight=int.Parse(dt.Rows[n]["Weight"].ToString());
					}
					if(dt.Rows[n]["GoodsSmallPic"]!=null && dt.Rows[n]["GoodsSmallPic"].ToString()!="")
					{
					model.GoodsSmallPic=dt.Rows[n]["GoodsSmallPic"].ToString();
					}
					if(dt.Rows[n]["GoodsSampleDesc"]!=null && dt.Rows[n]["GoodsSampleDesc"].ToString()!="")
					{
					model.GoodsSampleDesc=dt.Rows[n]["GoodsSampleDesc"].ToString();
					}
					if(dt.Rows[n]["GoodsDesc"]!=null && dt.Rows[n]["GoodsDesc"].ToString()!="")
					{
					model.GoodsDesc=dt.Rows[n]["GoodsDesc"].ToString();
					}
					if(dt.Rows[n]["GoodsDesc2"]!=null && dt.Rows[n]["GoodsDesc2"].ToString()!="")
					{
					model.GoodsDesc2=dt.Rows[n]["GoodsDesc2"].ToString();
					}
					if(dt.Rows[n]["GoodsDesc3"]!=null && dt.Rows[n]["GoodsDesc3"].ToString()!="")
					{
					model.GoodsDesc3=dt.Rows[n]["GoodsDesc3"].ToString();
					}
					if(dt.Rows[n]["GoodsDesc4"]!=null && dt.Rows[n]["GoodsDesc4"].ToString()!="")
					{
					model.GoodsDesc4=dt.Rows[n]["GoodsDesc4"].ToString();
					}
					if(dt.Rows[n]["GoodsDesc5"]!=null && dt.Rows[n]["GoodsDesc5"].ToString()!="")
					{
					model.GoodsDesc5=dt.Rows[n]["GoodsDesc5"].ToString();
					}
					if(dt.Rows[n]["IsRec"]!=null && dt.Rows[n]["IsRec"].ToString()!="")
					{
						model.IsRec=int.Parse(dt.Rows[n]["IsRec"].ToString());
					}
					if(dt.Rows[n]["IsHot"]!=null && dt.Rows[n]["IsHot"].ToString()!="")
					{
						model.IsHot=int.Parse(dt.Rows[n]["IsHot"].ToString());
					}
					if(dt.Rows[n]["IsNew"]!=null && dt.Rows[n]["IsNew"].ToString()!="")
					{
						model.IsNew=int.Parse(dt.Rows[n]["IsNew"].ToString());
					}
					if(dt.Rows[n]["IsSpe"]!=null && dt.Rows[n]["IsSpe"].ToString()!="")
					{
						model.IsSpe=int.Parse(dt.Rows[n]["IsSpe"].ToString());
					}
					if(dt.Rows[n]["SalePrice"]!=null && dt.Rows[n]["SalePrice"].ToString()!="")
					{
						model.SalePrice=decimal.Parse(dt.Rows[n]["SalePrice"].ToString());
					}
					if(dt.Rows[n]["MarketPrice"]!=null && dt.Rows[n]["MarketPrice"].ToString()!="")
					{
						model.MarketPrice=decimal.Parse(dt.Rows[n]["MarketPrice"].ToString());
					}
					if(dt.Rows[n]["CBPrice"]!=null && dt.Rows[n]["CBPrice"].ToString()!="")
					{
						model.CBPrice=decimal.Parse(dt.Rows[n]["CBPrice"].ToString());
					}
					if(dt.Rows[n]["IsSale"]!=null && dt.Rows[n]["IsSale"].ToString()!="")
					{
						model.IsSale=int.Parse(dt.Rows[n]["IsSale"].ToString());
					}
					if(dt.Rows[n]["Sotck"]!=null && dt.Rows[n]["Sotck"].ToString()!="")
					{
						model.Sotck=int.Parse(dt.Rows[n]["Sotck"].ToString());
					}
					if(dt.Rows[n]["MinSaleNumber"]!=null && dt.Rows[n]["MinSaleNumber"].ToString()!="")
					{
						model.MinSaleNumber=int.Parse(dt.Rows[n]["MinSaleNumber"].ToString());
					}
					if(dt.Rows[n]["MaxSaleNumber"]!=null && dt.Rows[n]["MaxSaleNumber"].ToString()!="")
					{
						model.MaxSaleNumber=int.Parse(dt.Rows[n]["MaxSaleNumber"].ToString());
					}
					if(dt.Rows[n]["Orderby"]!=null && dt.Rows[n]["Orderby"].ToString()!="")
					{
						model.Orderby=int.Parse(dt.Rows[n]["Orderby"].ToString());
					}
					if(dt.Rows[n]["ViewCount"]!=null && dt.Rows[n]["ViewCount"].ToString()!="")
					{
						model.ViewCount=int.Parse(dt.Rows[n]["ViewCount"].ToString());
					}
					if(dt.Rows[n]["TotalSaleCount"]!=null && dt.Rows[n]["TotalSaleCount"].ToString()!="")
					{
						model.TotalSaleCount=int.Parse(dt.Rows[n]["TotalSaleCount"].ToString());
					}
					if(dt.Rows[n]["Remarks"]!=null && dt.Rows[n]["Remarks"].ToString()!="")
					{
					model.Remarks=dt.Rows[n]["Remarks"].ToString();
					}
					if(dt.Rows[n]["AddTime"]!=null && dt.Rows[n]["AddTime"].ToString()!="")
					{
						model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
					}
					if(dt.Rows[n]["AreaInfo"]!=null && dt.Rows[n]["AreaInfo"].ToString()!="")
					{
					model.AreaInfo=dt.Rows[n]["AreaInfo"].ToString();
					}
					if(dt.Rows[n]["BookInfo"]!=null && dt.Rows[n]["BookInfo"].ToString()!="")
					{
					model.BookInfo=dt.Rows[n]["BookInfo"].ToString();
					}
					if(dt.Rows[n]["ProviderInfo"]!=null && dt.Rows[n]["ProviderInfo"].ToString()!="")
					{
					model.ProviderInfo=dt.Rows[n]["ProviderInfo"].ToString();
					}
					if(dt.Rows[n]["DataFrom"]!=null && dt.Rows[n]["DataFrom"].ToString()!="")
					{
					model.DataFrom=dt.Rows[n]["DataFrom"].ToString();
					}
					if(dt.Rows[n]["MorePropertys"]!=null && dt.Rows[n]["MorePropertys"].ToString()!="")
					{
					model.MorePropertys=dt.Rows[n]["MorePropertys"].ToString();
					}
					if(dt.Rows[n]["IfSh"]!=null && dt.Rows[n]["IfSh"].ToString()!="")
					{
						model.IfSh=int.Parse(dt.Rows[n]["IfSh"].ToString());
					}
					if(dt.Rows[n]["ShPerson"]!=null && dt.Rows[n]["ShPerson"].ToString()!="")
					{
					model.ShPerson=dt.Rows[n]["ShPerson"].ToString();
					}
					if(dt.Rows[n]["ShTime"]!=null && dt.Rows[n]["ShTime"].ToString()!="")
					{
						model.ShTime=DateTime.Parse(dt.Rows[n]["ShTime"].ToString());
					}
					if(dt.Rows[n]["ShMark"]!=null && dt.Rows[n]["ShMark"].ToString()!="")
					{
					model.ShMark=dt.Rows[n]["ShMark"].ToString();
					}
					if(dt.Rows[n]["IsOldGoods"]!=null && dt.Rows[n]["IsOldGoods"].ToString()!="")
					{
						model.IsOldGoods=int.Parse(dt.Rows[n]["IsOldGoods"].ToString());
					}
					if(dt.Rows[n]["OldGoodsLevel"]!=null && dt.Rows[n]["OldGoodsLevel"].ToString()!="")
					{
					model.OldGoodsLevel=dt.Rows[n]["OldGoodsLevel"].ToString();
					}
					if(dt.Rows[n]["saleCount"]!=null && dt.Rows[n]["saleCount"].ToString()!="")
					{
						model.saleCount=int.Parse(dt.Rows[n]["saleCount"].ToString());
					}
					if(dt.Rows[n]["Postage"]!=null && dt.Rows[n]["Postage"].ToString()!="")
					{
						model.Postage=decimal.Parse(dt.Rows[n]["Postage"].ToString());
					}
					if(dt.Rows[n]["PingLunCount"]!=null && dt.Rows[n]["PingLunCount"].ToString()!="")
					{
						model.PingLunCount=int.Parse(dt.Rows[n]["PingLunCount"].ToString());
					}
					if(dt.Rows[n]["vipPrice1"]!=null && dt.Rows[n]["vipPrice1"].ToString()!="")
					{
						model.vipPrice1=decimal.Parse(dt.Rows[n]["vipPrice1"].ToString());
					}
					if(dt.Rows[n]["vipPrice2"]!=null && dt.Rows[n]["vipPrice2"].ToString()!="")
					{
						model.vipPrice2=decimal.Parse(dt.Rows[n]["vipPrice2"].ToString());
					}
					if(dt.Rows[n]["signId"]!=null && dt.Rows[n]["signId"].ToString()!="")
					{
					model.signId=dt.Rows[n]["signId"].ToString();
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 根据goodsid编号获取自定义属性集合(新型),判断是否null返回
        /// </summary>

        public DataTable GetMorePropertyInfo(string goodsId)
        {
            GoodsModel mod = dal.GetModel(goodsId);
            if (mod != null)
            {
                GoodsTypeModel modCate = new GoodsTypeBll().GetModel(mod.CategoryId);
                if (modCate != null)
                {
                    //DataTable dt = new TableObject("T_Goods_MorePropertySet").Util_GetList("*", "propertyId in(select morePropertyId from T_Goods_Category_MoreProperty where categoryId='" + modCate.CategoryId + "')");
                    string strSQL = "select * from T_Goods_MorePropertySet where propertyId in(select morePropertyId from T_Goods_Category_MoreProperty where categoryId='" + modCate.CategoryId + "');";
                    AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
                    DataTable dt = adohelper.ExecuteSqlDataset(strSQL).Tables[0];
                    return dt;
                }
                else { return null; }
            }
            else { return null; }
        }
    }
}
