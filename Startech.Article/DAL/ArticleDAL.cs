using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Startech.Utils;

namespace Startech.Article
{
    public class ArticleDAL
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");

        public ArticleDAL()
		{}

		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ArticleModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Article(");
            strSql.Append("Titie,Body,AddedUserId,AddedDate,ReleaseDate,ExpireDate,CategoryId,Approved,FileId,ShareToPlatform)");
			strSql.Append(" values (");
            strSql.Append("@Titie,@Body,@AddedUserId,@AddedDate,@ReleaseDate,@ExpireDate,@CategoryId,@Approved,@FileId,@ShareToPlatform)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Titie", SqlDbType.VarChar,100),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@ExpireDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@Approved", SqlDbType.Int,4),
					new SqlParameter("@FileId", SqlDbType.Int,4),
                     new SqlParameter("@ShareToPlatform", SqlDbType.VarChar,50)};
			parameters[0].Value = model.Titie;
			parameters[1].Value = model.Body;
			parameters[2].Value = model.AddedUserId;
			parameters[3].Value = model.AddedDate;
			parameters[4].Value = model.ReleaseDate;
			parameters[5].Value = model.ExpireDate;
			parameters[6].Value = model.CategoryId;
			parameters[7].Value = model.Approved;
			parameters[8].Value = model.FileId;
            parameters[9].Value = model.ShareToPlatform;

            object o = adoHelper.ExecuteSqlScalar(strSql.ToString(), parameters);
            if (o != null)
                return Convert.ToInt32(o);
            else
                return 0;
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(ArticleModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Article set ");
			strSql.Append("Titie=@Titie,");
			strSql.Append("Body=@Body,");
			strSql.Append("AddedUserId=@AddedUserId,");
			strSql.Append("AddedDate=@AddedDate,");
			strSql.Append("ReleaseDate=@ReleaseDate,");
			strSql.Append("ExpireDate=@ExpireDate,");
			strSql.Append("CategoryId=@CategoryId,");
			strSql.Append("Approved=@Approved,");
			strSql.Append("FileId=@FileId,");
            strSql.Append("ShareToPlatform=@ShareToPlatform");
			strSql.Append(" where ArticleId=@ArticleId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ArticleId", SqlDbType.Int,4),
					new SqlParameter("@Titie", SqlDbType.VarChar,100),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@ExpireDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@Approved", SqlDbType.Int,4),
					new SqlParameter("@FileId", SqlDbType.Int,4),
                    new SqlParameter("@ShareToPlatform", SqlDbType.VarChar,50)};
			parameters[0].Value = model.ArticleId;
			parameters[1].Value = model.Titie;
			parameters[2].Value = model.Body;
			parameters[3].Value = model.AddedUserId;
			parameters[4].Value = model.AddedDate;
			parameters[5].Value = model.ReleaseDate;
			parameters[6].Value = model.ExpireDate;
			parameters[7].Value = model.CategoryId;
			parameters[8].Value = model.Approved;
			parameters[9].Value = model.FileId;
            parameters[10].Value = model.ShareToPlatform;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ArticleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_Article ");
			strSql.Append(" where ArticleId=@ArticleId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ArticleId", SqlDbType.Int,4)};
			parameters[0].Value = ArticleId;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ArticleModel GetModel(int ArticleId)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ArticleId,Titie,Body,AddedUserId,AddedDate,ReleaseDate,ExpireDate,CategoryId,Approved,FileId,ShareToPlatform from T_Article ");
			strSql.Append(" where ArticleId=@ArticleId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ArticleId", SqlDbType.Int,4)};
			parameters[0].Value = ArticleId;

            ArticleModel model = new ArticleModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ArticleId"].ToString()!="")
				{
					model.ArticleId=int.Parse(ds.Tables[0].Rows[0]["ArticleId"].ToString());
				}
				model.Titie=ds.Tables[0].Rows[0]["Titie"].ToString();
				model.Body=ds.Tables[0].Rows[0]["Body"].ToString();
				if(ds.Tables[0].Rows[0]["AddedUserId"].ToString()!="")
				{
					model.AddedUserId=int.Parse(ds.Tables[0].Rows[0]["AddedUserId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddedDate"].ToString()!="")
				{
					model.AddedDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseDate"].ToString()!="")
				{
					model.ReleaseDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ExpireDate"].ToString()!="")
				{
					model.ExpireDate=DateTime.Parse(ds.Tables[0].Rows[0]["ExpireDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Approved"].ToString()!="")
				{
					model.Approved=int.Parse(ds.Tables[0].Rows[0]["Approved"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FileId"].ToString()!="")
				{
					model.FileId=int.Parse(ds.Tables[0].Rows[0]["FileId"].ToString());
				}
                model.ShareToPlatform = ds.Tables[0].Rows[0]["ShareToPlatform"].ToString();
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
            strSql.Append("select ArticleId,Titie,Body,AddedUserId,AddedDate,ReleaseDate,ExpireDate,CategoryId,Approved,FileId,ShareToPlatform ");
			strSql.Append(" FROM T_Article ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

        /// <summary>
        /// 得到文章数据列表
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetArticleList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return Startech.Utils.PaginationUtility.GetPaginationList(fields, "V_Article", filter, sort, currentPageIndex, pageSize, out recordCount);
        }


        /// <summary>
        /// 全选审核通过
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveArticleAll(string articleIds)
        {
            string sql = String.Format("update T_Article set Approved=1  where ArticleId in ({0})", articleIds);
            AdoHelper.CreateHelper("DBInstance").ExecuteSqlNonQuery(sql);
        }

        /// <summary>
        /// 全选取消审核
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveCellArticleAll(string articleIds)
        {
            string sql = String.Format("update T_Article set Approved=0  where ArticleId in ({0})", articleIds);
            AdoHelper.CreateHelper("DBInstance").ExecuteSqlNonQuery(sql);
        }

        public int GetRecordCount(string filter)
        {
            filter = filter.Trim();
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) from T_Article");
            if (filter != String.Empty)
                sql.AppendFormat(" where {0}", filter);
            return Convert.ToInt32(adoHelper.ExecuteSqlScalar(sql.ToString()));
        }

		#endregion  成员方法
    }
}
