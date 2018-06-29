using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using StarTech.Adapter;

namespace Startech.Category
{
    /// <summary>
    /// 数据访问类CategoryDAL。
    /// </summary>
    public class CategoryDAL
    {
        public CategoryDAL()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        #region  成员方法
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(CategoryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Category(");
            //strSql.Append("CategoryId,CategoryName,Sort,Type,ParentCategoryId,AddedUserId,AddedDate,Url)");
            strSql.Append("CategoryName,Sort,Type,ParentCategoryId,AddedUserId,AddedDate,Url)");
            strSql.Append(" values (");
            strSql.Append("@CategoryName,@Sort,@Type,@ParentCategoryId,@AddedUserId,@AddedDate,@Url)");
            strSql.Append(" SET @CategoryId = @@IDENTITY");
            SqlParameter[] parameters = {
                              new SqlParameter("@CategoryId", SqlDbType.Int,4),
                                     new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@Url", SqlDbType.VarChar,100)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.CategoryName;
            parameters[2].Value = model.Sort;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.ParentCategoryId;
            parameters[5].Value = model.AddedUserId;
            parameters[6].Value = model.AddedDate;
            parameters[7].Value = model.Url;
            object obj = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(parameters[0].Value);
            }

        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(CategoryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Category set ");
            strSql.Append("CategoryName=@CategoryName,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("Type=@Type,");
            strSql.Append("ParentCategoryId=@ParentCategoryId,");
            strSql.Append("AddedUserId=@AddedUserId,");
            strSql.Append("AddedDate=@AddedDate,");
            strSql.Append("Url=@Url");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@Url", SqlDbType.VarChar,100),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.Sort;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.ParentCategoryId;
            parameters[4].Value = model.AddedUserId;
            parameters[5].Value = model.AddedDate;
            parameters[6].Value = model.Url;
            parameters[7].Value = model.CategoryId;

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
        /// 得到一个对象实体
        /// </summary>
        public CategoryModel GetModel(int CategoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_CATEGORY ");
            strSql.Append(" where CATEGORYID=@CATEGORYID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
            parameters[0].Value = CategoryId;

            CategoryModel model = new CategoryModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
                }
                model.CategoryName = ds.Tables[0].Rows[0]["categoryname"].ToString();
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentCategoryId"].ToString() != "")
                {
                    model.ParentCategoryId = int.Parse(ds.Tables[0].Rows[0]["ParentCategoryId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddedUserId"].ToString() != "")
                {
                    model.AddedUserId = int.Parse(ds.Tables[0].Rows[0]["AddedUserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddedDate"].ToString() != "")
                {
                    model.AddedDate = DateTime.Parse(ds.Tables[0].Rows[0]["AddedDate"].ToString());
                }
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
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
            strSql.Append("select CategoryId,Title,Sort,Type,ParentCategoryId,AddedUserId,AddedDate,Url ");
            strSql.Append(" FROM T_Category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /// <summary>
        /// 有权限的类别菜单
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllCategoryItems()
        {
            //权限先注释，等以后载用
            string uniqueId = GetGroupID().ToString();
            if (uniqueId != "")
            {
                string query = @"
      select a.categoryId,a.parentCategoryId,a.sort,a.title from dbo.T_Category  
      a,T_CategoryPermission b where  a.categoryId
        =b.categoryId and uniqueId=@rId order by a.sort desc ";
                SqlParameter[] para = { new SqlParameter("@rId", SqlDbType.Int) };
                para[0].Value = uniqueId;
                return adoHelper.ExecuteSqlDataset(query, para);
            }
            return null;
        }

        public DataSet GetCategoryItems()
        {
            string query2 = "select a.categoryId,a.parentCategoryId,a.sort,a.CategoryName from dbo.T_Category a ";

            return adoHelper.ExecuteSqlDataset(query2);
        }
        public int GetGroupID()
        {

            IACenter iacenter = new IACenter();
            string userId = HttpContext.Current.Request.Cookies["__UserInfo"]["userId"].ToString();// "1";
            if (userId != "")
            {
                return int.Parse(iacenter.GetGroupIdsByUserId(int.Parse(userId)));
            }
            else
                return 0;

        }
        /// <summary>
        /// 删除类别表信息
        /// </summary>
        /// <param name="categoryIds">类别编号集合</param>
        /// <returns></returns>
        public bool DeleteCategory(int[] categoryIds)
        {
            int count = 0;
            foreach (int categoryId in categoryIds)
            {
                if (DeleteCategory(categoryId)) count++;
            }
            return count == categoryIds.Length;
        }

        /// <summary>
        /// 删除类别表信息
        /// </summary>
        /// <param name="categoryId">类别编号</param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Category ");
            strSql.Append(" where CategoryId=@CategoryId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)			};
            parameters[0].Value = categoryId;

            int rows = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                string sql = "delete from t_categorypermission where categoryid=@CategoryId";
                SqlParameter[] par = { new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
                par[0].Value = categoryId;
                adoHelper.ExecuteSqlNonQuery(sql, par);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除父目录信息
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteCategoryParent(string categoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from  T_Category ");
            strSql.Append(" where ParentCategoryId in (select CategoryId from T_Category ");
            strSql.AppendFormat(" where CategoryId= {0})", categoryId);

            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 如果该节点为二级结点，则不允许删除
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public bool DeleteCategorySecond(string categoryId, string parentcategoryId)
        {
            string strSql = @"select ParentCategoryId from dbo.T_Category where CategoryId='" + categoryId + "'";
            DataSet ds = AdoHelper.CreateHelper("DBInstance").ExecuteSqlDataset(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (parentcategoryId == ds.Tables[0].Rows[0]["ParentCategoryId"].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        //添加类别权限
        public void UpdateCategoryPermission(string categoryId)
        {
            string uniqueId = "1";// GetRoleId();
            if (uniqueId != "")
            {
                string sql = "insert into T_CategoryPermission(categoryId,uniqueId) values(@cId,@rId)";
                SqlParameter[] para ={ new SqlParameter("@cId", categoryId),
                                     new SqlParameter("@rId",uniqueId)};
                adoHelper.ExecuteSqlNonQuery(sql, para);
            }
        }

        public CategoryModel GetCategoryDetail(int categoryId)
        {
            return GetCategoryDetailFromDataRow(GetCategoryDataRow(categoryId));
        }
        public DataRow GetCategoryDataRow(int categoryId)
        {
            string query = String.Format("Select * from T_Category where CategoryId={0}", categoryId);
            DataSet ds = adoHelper.ExecuteSqlDataset(query);
            if (ds.Tables[0].Rows.Count == 1) return ds.Tables[0].Rows[0];
            return null;
        }
        private CategoryModel GetCategoryDetailFromDataRow(DataRow info)
        {
            if (info != null)
            {
                CategoryModel detail = new CategoryModel();
                detail.CategoryId = (int)info["CategoryId"];
                detail.CategoryName = info["categoryname"].ToString();
                detail.Sort = (int)info["Sort"];
                detail.Type = (int)info["Type"];
                detail.ParentCategoryId = (int)info["ParentCategoryId"];
                detail.AddedUserId = (int)info["AddedUserId"];
                detail.AddedDate = Convert.ToDateTime(info["AddedDate"]);
                detail.Url = info["Url"].ToString();
                return detail;
            }
            return null;
        }

        #endregion  成员方法
    }
}

