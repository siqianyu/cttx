using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using StarTech.DBUtility;
using System.Data;

namespace StarTech.ELife.Menu
{
    public class MenuTypeDal
    {

        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(MenuTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Menu_Category(");
            strSql.Append("CategoryId,CategoryName,CategoryLevel,PCategoryId,CategoryPath,RootId,CategoryToTypeId,CategoryFlag,Orderby,Remarks,Url,icon)");
            strSql.Append(" values (");
            strSql.Append("@CategoryId,@CategoryName,@CategoryLevel,@PCategoryId,@CategoryPath,@RootId,@CategoryToTypeId,@CategoryFlag,@Orderby,@Remarks,@Url,@icon)");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryLevel", SqlDbType.Int,4),
					new SqlParameter("@PCategoryId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryPath", SqlDbType.VarChar,50),
					new SqlParameter("@RootId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryToTypeId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryFlag", SqlDbType.VarChar,50),
					new SqlParameter("@Orderby", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.VarChar,500),
					new SqlParameter("@Url", SqlDbType.VarChar,500),
					new SqlParameter("@icon", SqlDbType.VarChar,500)};
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.CategoryName;
            parameters[2].Value = model.CategoryLevel;
            parameters[3].Value = model.PCategoryId;
            parameters[4].Value = model.CategoryPath;
            parameters[5].Value = model.RootId;
            parameters[6].Value = model.CategoryToTypeId;
            parameters[7].Value = model.CategoryFlag;
            parameters[8].Value = model.Orderby;
            parameters[9].Value = model.Remarks;
            parameters[10].Value = model.Url;
            parameters[11].Value = model.icon;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MenuTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Menu_Category set ");
            strSql.Append("CategoryName=@CategoryName,");
            strSql.Append("CategoryLevel=@CategoryLevel,");
            strSql.Append("PCategoryId=@PCategoryId,");
            strSql.Append("CategoryPath=@CategoryPath,");
            strSql.Append("RootId=@RootId,");
            strSql.Append("CategoryToTypeId=@CategoryToTypeId,");
            strSql.Append("CategoryFlag=@CategoryFlag,");
            strSql.Append("Orderby=@Orderby,");
            strSql.Append("Remarks=@Remarks,");
            strSql.Append("Url=@Url,");
            strSql.Append("icon=@icon");
            strSql.Append(" where CategoryId=@CategoryId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryLevel", SqlDbType.Int,4),
					new SqlParameter("@PCategoryId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryPath", SqlDbType.VarChar,50),
					new SqlParameter("@RootId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryToTypeId", SqlDbType.VarChar,50),
					new SqlParameter("@CategoryFlag", SqlDbType.VarChar,50),
					new SqlParameter("@Orderby", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.VarChar,500),
					new SqlParameter("@Url", SqlDbType.VarChar,500),
					new SqlParameter("@icon", SqlDbType.VarChar,500),
					new SqlParameter("@CategoryId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.CategoryLevel;
            parameters[2].Value = model.PCategoryId;
            parameters[3].Value = model.CategoryPath;
            parameters[4].Value = model.RootId;
            parameters[5].Value = model.CategoryToTypeId;
            parameters[6].Value = model.CategoryFlag;
            parameters[7].Value = model.Orderby;
            parameters[8].Value = model.Remarks;
            parameters[9].Value = model.Url;
            parameters[10].Value = model.icon;
            parameters[11].Value = model.CategoryId;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Menu_Category ");
            strSql.Append(" where CategoryId=@CategoryId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.VarChar,50)};
            parameters[0].Value = CategoryId;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除叶节点菜单项集，非叶节点无法删除
        /// </summary>
        /// <param name="menuIds">菜单项标识集</param>
        /// <returns>全部删除 返回true ,否则false</returns>
        public bool Delete(int[] menuIds)
        {
            int count = 0;
            foreach (int menuId in menuIds)
            {
                if (Delete(menuId.ToString())) count++;
            }
            return count == menuIds.Length;
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Menu_Category ");
            strSql.Append(" where CategoryId in (" + CategoryIdlist + ")  ");
            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MenuTypeModel GetModel(string CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CategoryId,CategoryName,CategoryLevel,PCategoryId,CategoryPath,RootId,CategoryToTypeId,CategoryFlag,Orderby,Remarks,Url,icon from T_Menu_Category ");
            strSql.Append(" where CategoryId=@CategoryId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.VarChar,50)};
            parameters[0].Value = CategoryId;

            MenuTypeModel model = new MenuTypeModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CategoryId"] != null && ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {
                    model.CategoryId = ds.Tables[0].Rows[0]["CategoryId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CategoryName"] != null && ds.Tables[0].Rows[0]["CategoryName"].ToString() != "")
                {
                    model.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CategoryLevel"] != null && ds.Tables[0].Rows[0]["CategoryLevel"].ToString() != "")
                {
                    model.CategoryLevel = int.Parse(ds.Tables[0].Rows[0]["CategoryLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PCategoryId"] != null && ds.Tables[0].Rows[0]["PCategoryId"].ToString() != "")
                {
                    model.PCategoryId = ds.Tables[0].Rows[0]["PCategoryId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CategoryPath"] != null && ds.Tables[0].Rows[0]["CategoryPath"].ToString() != "")
                {
                    model.CategoryPath = ds.Tables[0].Rows[0]["CategoryPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RootId"] != null && ds.Tables[0].Rows[0]["RootId"].ToString() != "")
                {
                    model.RootId = ds.Tables[0].Rows[0]["RootId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CategoryToTypeId"] != null && ds.Tables[0].Rows[0]["CategoryToTypeId"].ToString() != "")
                {
                    model.CategoryToTypeId = ds.Tables[0].Rows[0]["CategoryToTypeId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CategoryFlag"] != null && ds.Tables[0].Rows[0]["CategoryFlag"].ToString() != "")
                {
                    model.CategoryFlag = ds.Tables[0].Rows[0]["CategoryFlag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Orderby"] != null && ds.Tables[0].Rows[0]["Orderby"].ToString() != "")
                {
                    model.Orderby = int.Parse(ds.Tables[0].Rows[0]["Orderby"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remarks"] != null && ds.Tables[0].Rows[0]["Remarks"].ToString() != "")
                {
                    model.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Url"] != null && ds.Tables[0].Rows[0]["Url"].ToString() != "")
                {
                    model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["icon"] != null && ds.Tables[0].Rows[0]["icon"].ToString() != "")
                {
                    model.icon = ds.Tables[0].Rows[0]["icon"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CategoryId,CategoryName,CategoryLevel,PCategoryId,CategoryPath,RootId,CategoryToTypeId,CategoryFlag,Orderby,Remarks,Url,icon ");
            strSql.Append(" FROM T_Menu_Category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }
    }
}
