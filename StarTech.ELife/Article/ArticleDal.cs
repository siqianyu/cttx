using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using StarTech.Util;
using StarTech.DBUtility;

namespace StarTech.ELife.Article
{
    public class ArticleDal
    {
        public ArticleDal()
        { }

        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ArticleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Article(");
            strSql.Append("Titie,Body,AddedUserId,AddedDate,ReleaseDate,ExpireDate,CategoryId,Approved,FileId,MarketId,Site)");
            strSql.Append(" values (");
            strSql.Append("@Titie,@Body,@AddedUserId,@AddedDate,@ReleaseDate,@ExpireDate,@CategoryId,@Approved,@FileId,@MarketId,@Site)");
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
					new SqlParameter("@MarketId", SqlDbType.Int,4),
					new SqlParameter("@Site", SqlDbType.Int,4)};
            parameters[0].Value = model.Titie;
            parameters[1].Value = model.Body;
            parameters[2].Value = model.AddedUserId;
            parameters[3].Value = model.AddedDate;
            parameters[4].Value = model.ReleaseDate;
            parameters[5].Value = model.ExpireDate;
            parameters[6].Value = model.CategoryId;
            parameters[7].Value = model.Approved;
            parameters[8].Value = model.FileId;
            parameters[9].Value = model.MarketId;
            parameters[10].Value = model.Site;


            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ArticleModel model)
        {
            StringBuilder strSql = new StringBuilder();
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
            strSql.Append("MarketId=@MarketId,");
            strSql.Append("Site=@Site");
            strSql.Append(" where ArticleId=@ArticleId");
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
					new SqlParameter("@MarketId", SqlDbType.Int,4),
					new SqlParameter("@Site", SqlDbType.Int,4),
					new SqlParameter("@ArticleId", SqlDbType.Int,4)};
            parameters[0].Value = model.Titie;
            parameters[1].Value = model.Body;
            parameters[2].Value = model.AddedUserId;
            parameters[3].Value = model.AddedDate;
            parameters[4].Value = model.ReleaseDate;
            parameters[5].Value = model.ExpireDate;
            parameters[6].Value = model.CategoryId;
            parameters[7].Value = model.Approved;
            parameters[8].Value = model.FileId;
            parameters[9].Value = model.MarketId;
            parameters[10].Value = model.Site;
            parameters[11].Value = model.ArticleId;


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
        public bool Delete(int ArticleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Article ");
            strSql.Append(" where ArticleId=@ArticleId");
            SqlParameter[] parameters = {
					new SqlParameter("@ArticleId", SqlDbType.Int,4)
			};
            parameters[0].Value = ArticleId;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string ArticleIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Article ");
            strSql.Append(" where ArticleId in (" + ArticleIdlist + ")  ");
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
        public ArticleModel GetModel(int ArticleId)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ArticleId,Titie,Body,AddedUserId,AddedDate,ReleaseDate,ExpireDate,CategoryId,Approved,FileId,MarketId,Site from T_Article ");
            strSql.Append(" where ArticleId=@ArticleId");
            SqlParameter[] parameters = {
					new SqlParameter("@ArticleId", SqlDbType.Int,4)
			};
            parameters[0].Value = ArticleId;

            ArticleModel model = new ArticleModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ArticleModel DataRowToModel(DataRow row)
		{
			ArticleModel model=new ArticleModel();
			if (row != null)
			{
				if(row["ArticleId"]!=null && row["ArticleId"].ToString()!="")
				{
					model.ArticleId=int.Parse(row["ArticleId"].ToString());
				}
				if(row["Titie"]!=null)
				{
					model.Titie=row["Titie"].ToString();
				}
				if(row["Body"]!=null)
				{
					model.Body=row["Body"].ToString();
				}
				if(row["AddedUserId"]!=null && row["AddedUserId"].ToString()!="")
				{
					model.AddedUserId=int.Parse(row["AddedUserId"].ToString());
				}
				if(row["AddedDate"]!=null && row["AddedDate"].ToString()!="")
				{
					model.AddedDate=DateTime.Parse(row["AddedDate"].ToString());
				}
				if(row["ReleaseDate"]!=null && row["ReleaseDate"].ToString()!="")
				{
					model.ReleaseDate=DateTime.Parse(row["ReleaseDate"].ToString());
				}
				if(row["ExpireDate"]!=null && row["ExpireDate"].ToString()!="")
				{
					model.ExpireDate=DateTime.Parse(row["ExpireDate"].ToString());
				}
				if(row["CategoryId"]!=null && row["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(row["CategoryId"].ToString());
				}
				if(row["Approved"]!=null && row["Approved"].ToString()!="")
				{
					model.Approved=int.Parse(row["Approved"].ToString());
				}
				if(row["FileId"]!=null && row["FileId"].ToString()!="")
				{
					model.FileId=int.Parse(row["FileId"].ToString());
				}
                if (row["MarketId"] != null && row["MarketId"].ToString() != "")
                {
                    model.MarketId = int.Parse(row["MarketId"].ToString());
                }
                if (row["Site"] != null && row["Site"].ToString() != "")
                {
                    model.Site = int.Parse(row["Site"].ToString());
                }
			}
			return model;
		}


        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArticleId,Titie,Body,AddedUserId,AddedDate,ReleaseDate,ExpireDate,CategoryId,Approved,FileId,MarketId,Site ");
            strSql.Append(" FROM T_Article ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

        /// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ArticleId,Titie,Body,AddedUserId,AddedDate,ReleaseDate,ExpireDate,CategoryId,Approved,FileId,MarketId,Site ");
            strSql.Append(" FROM T_Article ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

        /// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ArticleId desc");
			}
			strSql.Append(")AS Row, T.*  from T_Article T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
        /// 审核通过
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public bool Approve(string articleId)
        {
            string sql = String.Format("update T_Article set Approved=1  where ArticleId in ({0})", articleId);
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public bool UnApprove(string articleId)
        {
            string sql = String.Format("update T_Article set Approved=0  where ArticleId in ({0})", articleId);
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM T_Article ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            object obj = adoHelper.ExecuteSqlScalar(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}

        #endregion  成员方法
    }
}
