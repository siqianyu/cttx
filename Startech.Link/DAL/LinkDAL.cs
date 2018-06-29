using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using  Startech.Utils;
using StarTech.DBUtility;

namespace Startech.Link
{
   public class LinkDAL
    {
       AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");

       public LinkDAL()
		{}

		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(LinkModel model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@LinkId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.VarChar,50),
					new SqlParameter("@Link", SqlDbType.VarChar,100),
					new SqlParameter("@Image", SqlDbType.VarChar,100),
					new SqlParameter("@DisplayMode", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Link;
            parameters[3].Value = model.Image;
            parameters[4].Value = model.DisplayMode;
            parameters[5].Value = model.Sort;
            parameters[6].Value = model.CategoryId;

            return adoHelper.ExecuteSPNonQuery("sp_Link_Add", parameters);
            return (int)parameters[0].Value;
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
       public int Update(LinkModel model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@LinkId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.VarChar,50),
					new SqlParameter("@Link", SqlDbType.VarChar,100),
					new SqlParameter("@Image", SqlDbType.VarChar,100),
					new SqlParameter("@DisplayMode", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
            parameters[0].Value = model.LinkId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Link;
            parameters[3].Value = model.Image;
            parameters[4].Value = model.DisplayMode;
            parameters[5].Value = model.Sort;
            parameters[6].Value = model.CategoryId;

            return adoHelper .ExecuteSPNonQuery ("sp_Link_Update", parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int LinkId)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@LinkId", SqlDbType.BigInt)};
            parameters[0].Value = LinkId;

            return adoHelper .ExecuteSPNonQuery ("sp_Link_Delete", parameters);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
       public LinkModel GetModel(int LinkId)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@LinkId", SqlDbType.BigInt)};
            parameters[0].Value = LinkId;

            LinkModel model = new LinkModel();
            DataSet ds =  adoHelper.ExecuteSPDataset("sp_Link_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LinkId"].ToString() != "")
                {
                    model.LinkId = long.Parse(ds.Tables[0].Rows[0]["LinkId"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Link = ds.Tables[0].Rows[0]["Link"].ToString();
                model.Image = ds.Tables[0].Rows[0]["Image"].ToString();
                if (ds.Tables[0].Rows[0]["DisplayMode"].ToString() != "")
                {
                    model.DisplayMode = int.Parse(ds.Tables[0].Rows[0]["DisplayMode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
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
			strSql.Append("SELECT * ");
			strSql.Append(" FROM T_Link ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

        /// <summary>
        /// 得到友情连接类别数据列表
        /// </summary>
        public DataSet GetLinkList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return PaginationUtility.GetPaginationList(fields, "V_Link_LinkCty", filter, sort, currentPageIndex, pageSize, out recordCount);
        }

		#endregion  成员方法
    }
}
