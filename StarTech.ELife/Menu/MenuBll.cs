using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StarTech.ELife.Menu
{
    public class MenuBll
    {

        private MenuDal dal = new MenuDal();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MenuModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MenuModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string menuId)
        {

            return dal.Delete(menuId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MenuModel GetModel(string menuId)
        {

            return dal.GetModel(menuId);
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
        public List<MenuModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MenuModel> DataTableToList(DataTable dt)
        {
            List<MenuModel> modelList = new List<MenuModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MenuModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new MenuModel();
                    if (dt.Rows[n]["menuId"] != null && dt.Rows[n]["menuId"].ToString() != "")
                    {
                        model.menuId = dt.Rows[n]["menuId"].ToString();
                    }
                    if (dt.Rows[n]["menuName"] != null && dt.Rows[n]["menuName"].ToString() != "")
                    {
                        model.menuName = dt.Rows[n]["menuName"].ToString();
                    }
                    if (dt.Rows[n]["Technology"] != null && dt.Rows[n]["Technology"].ToString() != "")
                    {
                        model.Technology = dt.Rows[n]["Technology"].ToString();
                    }
                    if (dt.Rows[n]["Flavor"] != null && dt.Rows[n]["Flavor"].ToString() != "")
                    {
                        model.Flavor = dt.Rows[n]["Flavor"].ToString();
                    }
                    if (dt.Rows[n]["CookingTime"] != null && dt.Rows[n]["CookingTime"].ToString() != "")
                    {
                        model.CookingTime = dt.Rows[n]["CookingTime"].ToString();
                    }
                    if (dt.Rows[n]["Calorie"] != null && dt.Rows[n]["Calorie"].ToString() != "")
                    {
                        model.Calorie = decimal.Parse(dt.Rows[n]["Calorie"].ToString());
                    }
                    if (dt.Rows[n]["CookingSkill"] != null && dt.Rows[n]["CookingSkill"].ToString() != "")
                    {
                        model.CookingSkill = dt.Rows[n]["CookingSkill"].ToString();
                    }
                    if (dt.Rows[n]["UserId"] != null && dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = dt.Rows[n]["UserId"].ToString();
                    }
                    if (dt.Rows[n]["AddTime"] != null && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Rows[n]["isShow"] != null && dt.Rows[n]["isShow"].ToString() != "")
                    {
                        model.isShow = int.Parse(dt.Rows[n]["isShow"].ToString());
                    }
                    if (dt.Rows[n]["imgSrc"] != null && dt.Rows[n]["imgSrc"].ToString() != "")
                    {
                        model.imgSrc = dt.Rows[n]["imgSrc"].ToString();
                    }
                    if (dt.Rows[n]["smallImgSrc"] != null && dt.Rows[n]["smallImgSrc"].ToString() != "")
                    {
                        model.smallImgSrc = dt.Rows[n]["smallImgSrc"].ToString();
                    }
                    if (dt.Rows[n]["categoryId"] != null && dt.Rows[n]["categoryId"].ToString() != "")
                    {
                        model.smallImgSrc = dt.Rows[n]["categoryId"].ToString();
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
