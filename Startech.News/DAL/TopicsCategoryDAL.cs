using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Startech.Utils;
using System.Web;

namespace Startech.News
{
    public class TopicsCategoryDAL
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");

        public TopicsCategoryDAL()
		{}

		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TopicsCategoryModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_TopicsCategory(");
			strSql.Append("Title,Sort,ParentCategoryId,AddedUserId,AddedDate,ImgURL,Remark,TypeId,EndDate)");
			strSql.Append(" values (");
            strSql.Append("@Title,@Sort,@ParentCategoryId,@AddedUserId,@AddedDate,@ImgURL,@Remark,@TypeId,@EndDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@ImgURL", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.Text),
            	new SqlParameter("@TypeId", SqlDbType.Int,4),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Sort;
			parameters[2].Value = model.ParentCategoryId;
			parameters[3].Value = model.AddedUserId;
			parameters[4].Value = model.AddedDate;
			parameters[5].Value = model.ImgURL;
			parameters[6].Value = model.Remark;
            parameters[7].Value = model.TypeId;
            parameters[8].Value = model.EndDate;

            object obj = adoHelper.ExecuteSqlScalar(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(TopicsCategoryModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_TopicsCategory set ");
			strSql.Append("Title=@Title,");
			strSql.Append("Sort=@Sort,");
			strSql.Append("ParentCategoryId=@ParentCategoryId,");
			strSql.Append("AddedUserId=@AddedUserId,");
			strSql.Append("AddedDate=@AddedDate,");
			strSql.Append("ImgURL=@ImgURL,");
			strSql.Append("Remark=@Remark,");
            strSql.Append("EndDate=@EndDate");
			strSql.Append(" where TopicsCategoryId=@TopicsCategoryId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicsCategoryId", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ParentCategoryId", SqlDbType.Int,4),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@ImgURL", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.Text),
            new SqlParameter("@EndDate", SqlDbType.DateTime) };
			parameters[0].Value = model.TopicsCategoryId;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Sort;
			parameters[3].Value = model.ParentCategoryId;
			parameters[4].Value = model.AddedUserId;
			parameters[5].Value = model.AddedDate;
			parameters[6].Value = model.ImgURL;
			parameters[7].Value = model.Remark;
            parameters[8].Value = model.EndDate;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int TopicsCategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_TopicsCategory ");
			strSql.Append(" where TopicsCategoryId=@TopicsCategoryId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicsCategoryId", SqlDbType.Int,4)};
			parameters[0].Value = TopicsCategoryId;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TopicsCategoryModel GetModel(int TopicsCategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from T_TopicsCategory ");
			strSql.Append(" where TopicsCategoryId=@TopicsCategoryId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicsCategoryId", SqlDbType.Int,4)};
			parameters[0].Value = TopicsCategoryId;

            TopicsCategoryModel model = new TopicsCategoryModel();
			DataSet ds=adoHelper.ExecuteSqlDataset(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TopicsCategoryId"].ToString()!="")
				{
					model.TopicsCategoryId=int.Parse(ds.Tables[0].Rows[0]["TopicsCategoryId"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentCategoryId"].ToString()!="")
				{
					model.ParentCategoryId=int.Parse(ds.Tables[0].Rows[0]["ParentCategoryId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddedUserId"].ToString()!="")
				{
					model.AddedUserId=int.Parse(ds.Tables[0].Rows[0]["AddedUserId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddedDate"].ToString()!="")
				{
					model.AddedDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddedDate"].ToString());
				}
                if (ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
                }
				model.ImgURL=ds.Tables[0].Rows[0]["ImgURL"].ToString();
				model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM T_TopicsCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM T_TopicsCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        */
        public DataSet GetTopicsCategoryList(string filed, string filter, string sort, int currentPage, int pageSize, out int count)
        {
            return PaginationUtility.GetPaginationList(filed, "T_TopicsCategory", filter, sort, currentPage, pageSize, out count);
        }

		#endregion  成员方法

        #region 自定义方法
        //获得登录者编号
        public string GetRoleId()
        {
            string userId = HttpContext.Current.Request.Cookies["__UserInfo"]["userId"].ToString();
            string sql = "select roleId from T_User where userId=@userId";
            SqlParameter[] para ={ new SqlParameter("@userId", SqlDbType.Int) };
            para[0].Value = userId;
            object obj = adoHelper.ExecuteSqlScalar(sql, para);
            if (obj != null)
                return obj.ToString();
            else
                return "";
        }
        //得到所有专题类别
        public DataSet GetAllCategoryItems()
        {
            string roleId = GetRoleId();
            if (roleId != "")
            {
                string query = @"select a.* from dbo.T_TopicsCategory a,T_TopicsCtyPermission b 
where  a.TopicsCategoryId =b.TopicsCategoryId and roleId=@rId order by a.sort desc ";
                SqlParameter[] para ={ new SqlParameter("@rId", SqlDbType.Int) };
                para[0].Value = roleId;
                return adoHelper.ExecuteSqlDataset(query, para);
            }
            return null;
        }
        //添加类别权限
        public void UpdateCategoryPermission(string categoryId)
        {
            string roleId = GetRoleId();
            if (roleId != "")
            {
                string sql = "insert into T_TopicsCtyPermission(TopicsCategoryId,roleId) values(@cId,@rId)";
                SqlParameter[] para ={ new SqlParameter("@cId", categoryId),
                                     new SqlParameter("@rId",roleId)};
                adoHelper.ExecuteSqlNonQuery(sql, para);
            }
        }
        //删除
        public bool DeleteCategoryParent(string categoryId)
        {
            string strSql = @"select * from  dbo.T_TopicsCategory where ParentCategoryId in (
select TopicsCategoryId from  dbo.T_TopicsCategory where TopicsCategoryId=" + categoryId + ")";
            DataSet ds = AdoHelper.CreateHelper("DBInstance").ExecuteSqlDataset(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
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
            SqlParameter[] parm ={
                  new SqlParameter("@TopicsCategoryId",SqlDbType.Int,4),
                  new SqlParameter("@Return",SqlDbType.Int,4)
            };
            parm[0].Value = categoryId;
            parm[1].Direction = ParameterDirection.Output;
            adoHelper.ExecuteSPNonQuery("sp_TopicsCategory_Del", parm);
            return Convert.ToBoolean(parm[1].Value);
        }
        #endregion
    }
}
