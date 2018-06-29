using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Startech.Utils;
using StarTech.DBUtility;
using System.Web;

namespace Startech.Link
{
    public class LinkCategoryDAL
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");

        public LinkCategoryDAL()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LinkCategoryModel model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,8),
					new SqlParameter("@Category", SqlDbType.VarChar,50),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Category;
            parameters[2].Value = model.AddedDate;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.Type;

            adoHelper.ExecuteSPNonQuery ("sp_LinkCategory_Add", parameters);
            return (int)parameters[0].Value;
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(LinkCategoryModel model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,8),
					new SqlParameter("@Category", SqlDbType.VarChar,50),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Category;
            parameters[2].Value = model.AddedDate;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.Type;

            return adoHelper.ExecuteSPNonQuery("sp_LinkCategory_Update", parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ID)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

           return adoHelper .ExecuteSPNonQuery("sp_LinkCategory_Delete", parameters);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public LinkCategoryModel GetModel(int ID)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;

            LinkCategoryModel model = new LinkCategoryModel();
            DataSet ds = adoHelper .ExecuteSPDataset("sp_LinkCategory_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                if (ds.Tables[0].Rows[0]["AddedDate"].ToString() != "")
                {
                    model.AddedDate = DateTime.Parse(ds.Tables[0].Rows[0]["AddedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM T_LinkCategory ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

        /// <summary>
        /// 得到友情连接类别数据列表
        /// </summary>
        public DataSet GetLinkCategoryList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return PaginationUtility.GetPaginationList(fields, "T_LinkCategory", filter, sort, currentPageIndex, pageSize, out recordCount);
        }

		#endregion  成员方法

        #region 添加类别权限
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
        public void UpdateCategoryPermission(int categoryId)
        {
            string roleId = GetRoleId();
            if (roleId != "")
            {
                string sql = "insert into T_LinkCtyPermission(LinkCtyId,roleId) values(@cId,@rId)";
                SqlParameter[] para ={ new SqlParameter("@cId", categoryId),
                                       new SqlParameter("@rId",roleId)};
                adoHelper.ExecuteSqlNonQuery(sql, para);
            }
        }

        public DataSet GetAllCategoryItems()
        {
           // string roleId = GetRoleId();
 
                //string query = @"select a.* from dbo.T_LinkCategory a,T_LinkCtyPermission b where a.ID=b.LinkCtyId and roleId=@rId order by a.sort desc";
                string query = "select * from t_linkCategory";
                //SqlParameter[] para ={ new SqlParameter("@rId", SqlDbType.Int) };
                //para[0].Value = roleId;
                return adoHelper.ExecuteSqlDataset(query);
         
       
        }
        #endregion
    }
}
