using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;
using Startech.Utils;

namespace Startech.News
{
    public class NewsDAL
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");
        AdoHelper adoHelper_hzzlw = AdoHelper.CreateHelper("DBInstance");

        public NewsDAL()
		{}
		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(NewsModel model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@SubHead", SqlDbType.VarChar,100),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@PublicationUnit", SqlDbType.VarChar,50),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IndexCommend", SqlDbType.Int,4),
					new SqlParameter("@ArticleType", SqlDbType.Int,4),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@ExpireDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@Approved", SqlDbType.Int,4),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@ImgLink", SqlDbType.VarChar,500),
					new SqlParameter("@IsState", SqlDbType.Int,4),
					new SqlParameter("@HotPic", SqlDbType.VarChar,100),
					new SqlParameter("@KeyWord", SqlDbType.VarChar,500),
					new SqlParameter("@HotDays", SqlDbType.Int,4),
					new SqlParameter("@FromSource", SqlDbType.VarChar,50),
					new SqlParameter("@IsComment", SqlDbType.Int,4),
					new SqlParameter("@IsScrool", SqlDbType.Int,4),
					new SqlParameter("@Period", SqlDbType.VarChar,100),
					new SqlParameter("@Leaderid", SqlDbType.Int,4),
                    new SqlParameter("@ShareToPlatform", SqlDbType.VarChar,50),
                    new SqlParameter("@ShareToSubject", SqlDbType.VarChar,1000),
					new SqlParameter("@ShareToMarket", SqlDbType.VarChar,1000)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.SubHead;
            parameters[3].Value = model.Body;
            parameters[4].Value = model.AddedUserId;
            parameters[5].Value = model.PublicationUnit;
            parameters[6].Value = model.AddedDate;
            parameters[7].Value = model.IsTop;
            parameters[8].Value = model.IndexCommend;
            parameters[9].Value = model.ArticleType;
            parameters[10].Value = model.ReleaseDate;
            parameters[11].Value = model.ExpireDate;
            parameters[12].Value = model.CategoryId;
            parameters[13].Value = model.Approved;
            parameters[14].Value = model.ViewCount;
            parameters[15].Value = model.ImgLink;
            parameters[16].Value = model.IsState;
            parameters[17].Value = model.HotPic;
            parameters[18].Value = model.KeyWord;
            parameters[19].Value = model.HotDays;
            parameters[20].Value = model.FromSource;
            parameters[21].Value = model.IsComment;
            parameters[22].Value = model.IsScrool;
            parameters[23].Value = model.Period;
            parameters[24].Value = model.Leaderid;
            parameters[25].Value = model.ShareToPlatform;
            parameters[26].Value = model.ShareToSubject;
            parameters[27].Value = model.ShareToMarket;

            adoHelper.ExecuteSPNonQuery("sp_News_Add", parameters);
            return (int)parameters[0].Value;
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(NewsModel model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@SubHead", SqlDbType.VarChar,100),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@AddedUserId", SqlDbType.Int,4),
					new SqlParameter("@PublicationUnit", SqlDbType.VarChar,50),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IndexCommend", SqlDbType.Int,4),
					new SqlParameter("@ArticleType", SqlDbType.Int,4),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@ExpireDate", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@Approved", SqlDbType.Int,4),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@ImgLink", SqlDbType.VarChar,500),
					new SqlParameter("@IsState", SqlDbType.Int,4),
					new SqlParameter("@HotPic", SqlDbType.VarChar,100),
					new SqlParameter("@KeyWord", SqlDbType.VarChar,500),
					new SqlParameter("@HotDays", SqlDbType.Int,4),
					new SqlParameter("@FromSource", SqlDbType.VarChar,50),
					new SqlParameter("@IsComment", SqlDbType.Int,4),
					new SqlParameter("@IsScrool", SqlDbType.Int,4),
					new SqlParameter("@Period", SqlDbType.VarChar,100),
					new SqlParameter("@Leaderid",SqlDbType.Int,4),
                    new SqlParameter("@ShareToPlatform", SqlDbType.VarChar,50),
                    new SqlParameter("@ShareToSubject", SqlDbType.VarChar,1000),
					new SqlParameter("@ShareToMarket", SqlDbType.VarChar,1000)};
            parameters[0].Value = model.NewsID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.SubHead;
            parameters[3].Value = model.Body;
            parameters[4].Value = model.AddedUserId;
            parameters[5].Value = model.PublicationUnit;
            parameters[6].Value = model.AddedDate;
            parameters[7].Value = model.IsTop;
            parameters[8].Value = model.IndexCommend;
            parameters[9].Value = model.ArticleType;
            parameters[10].Value = model.ReleaseDate;
            parameters[11].Value = model.ExpireDate;
            parameters[12].Value = model.CategoryId;
            parameters[13].Value = model.Approved;
            parameters[14].Value = model.ViewCount;
            parameters[15].Value = model.ImgLink;
            parameters[16].Value = model.IsState;
            parameters[17].Value = model.HotPic;
            parameters[18].Value = model.KeyWord;
            parameters[19].Value = model.HotDays;
            parameters[20].Value = model.FromSource;
            parameters[21].Value = model.IsComment;
            parameters[22].Value = model.IsScrool;
            parameters[23].Value = model.Period;
            parameters[24].Value = model.Leaderid;
            parameters[25].Value = model.ShareToPlatform;
            parameters[26].Value = model.ShareToSubject;
            parameters[27].Value = model.ShareToMarket;

            adoHelper.ExecuteSPNonQuery("sp_News_Update", parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int NewsID)
		{

            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.Int,4)};
            parameters[0].Value = NewsID;

            adoHelper.ExecuteSPNonQuery("sp_News_Delete", parameters);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public NewsModel GetModel(int NewsID)
		{

            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.Int,4)};
            parameters[0].Value = NewsID;

            NewsModel model = new NewsModel();
            DataSet ds = adoHelper.ExecuteSPDataset("sp_News_GetModel", parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["NewsID"].ToString()!="")
				{
					model.NewsID=int.Parse(ds.Tables[0].Rows[0]["NewsID"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.SubHead=ds.Tables[0].Rows[0]["SubHead"].ToString();
				model.Body=ds.Tables[0].Rows[0]["Body"].ToString();
				if(ds.Tables[0].Rows[0]["AddedUserId"].ToString()!="")
				{
					model.AddedUserId=int.Parse(ds.Tables[0].Rows[0]["AddedUserId"].ToString());
				}
				model.PublicationUnit=ds.Tables[0].Rows[0]["PublicationUnit"].ToString();
				if(ds.Tables[0].Rows[0]["AddedDate"].ToString()!="")
				{
					model.AddedDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsTop"].ToString()!="")
				{
					model.IsTop=int.Parse(ds.Tables[0].Rows[0]["IsTop"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IndexCommend"].ToString()!="")
				{
					model.IndexCommend=int.Parse(ds.Tables[0].Rows[0]["IndexCommend"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ArticleType"].ToString()!="")
				{
					model.ArticleType=int.Parse(ds.Tables[0].Rows[0]["ArticleType"].ToString());
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
				if(ds.Tables[0].Rows[0]["ViewCount"].ToString()!="")
				{
					model.ViewCount=int.Parse(ds.Tables[0].Rows[0]["ViewCount"].ToString());
				}
				model.ImgLink=ds.Tables[0].Rows[0]["ImgLink"].ToString();
				if(ds.Tables[0].Rows[0]["IsState"].ToString()!="")
				{
					model.IsState=int.Parse(ds.Tables[0].Rows[0]["IsState"].ToString());
				}
				model.HotPic=ds.Tables[0].Rows[0]["HotPic"].ToString();
				model.KeyWord=ds.Tables[0].Rows[0]["KeyWord"].ToString();
				if(ds.Tables[0].Rows[0]["HotDays"].ToString()!="")
				{
					model.HotDays=int.Parse(ds.Tables[0].Rows[0]["HotDays"].ToString());
				}
				model.FromSource=ds.Tables[0].Rows[0]["FromSource"].ToString();
				if(ds.Tables[0].Rows[0]["IsComment"].ToString()!="")
				{
					model.IsComment=int.Parse(ds.Tables[0].Rows[0]["IsComment"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsScrool"].ToString()!="")
				{
					model.IsScrool=int.Parse(ds.Tables[0].Rows[0]["IsScrool"].ToString());
				}
				model.Period=ds.Tables[0].Rows[0]["Period"].ToString();
                if (ds.Tables[0].Rows[0]["Leaderid"].ToString() != "")
                {
                    model.Leaderid = int.Parse(ds.Tables[0].Rows[0]["Leaderid"].ToString());
                }
                model.ShareToPlatform = ds.Tables[0].Rows[0]["ShareToPlatform"].ToString();
                model.ShareToSubject = ds.Tables[0].Rows[0]["ShareToSubject"].ToString();
                model.ShareToMarket = ds.Tables[0].Rows[0]["ShareToMarket"].ToString();
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
			strSql.Append(" FROM T_News ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

        /// <summary>
        /// 得到新闻类别数据列表
        /// </summary>
        public DataSet GetNewsList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return PaginationUtility.GetPaginationList(fields, "V_NewsT", filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        /// <summary>
        /// 全选审核通过
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveArticleAll(string newsIds)
        {
            string sql = String.Format("update T_News set Approved=1  where NewsId in ({0})", newsIds);
            adoHelper.ExecuteSqlNonQuery(sql);
        }

        /// <summary>
        /// 全选取消审核
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="state"></param>
        public void ApproveCellArticleAll(string newsIds)
        {
            string sql = String.Format("update T_News set Approved=0  where NewsId in ({0})", newsIds);
            adoHelper.ExecuteSqlNonQuery(sql);
        }
        /// <summary>
        /// 更新点击率
        /// </summary>
        /// <param name="nid">新闻编号</param>
        public void UpdateHits(int nid)
        {
            string strSql = "Update T_News  set ViewCount=ViewCount+1 where NewsId=" + nid + "";
             adoHelper.ExecuteSqlNonQuery(strSql);
        }
		#endregion  成员方法

        /// <summary>
        /// 返回特定条数的新闻
        /// </summary>
        /// <param name="num">个数</param>
        /// <param name="fields">返回字段</param>
        /// <param name="filter">条件</param>
        /// <param name="sort">排序</param>
        /// <returns>返回DataSet集合</returns>
        public DataSet GetNewsByTop(int num, string fields, string filter, string sort)
        {
            StringBuilder strSql = new StringBuilder();
            if (num > 0)
                strSql.Append("select top " + num + " " + fields);
            else
                strSql.Append("select " + fields);
            strSql.Append(" from T_News ");
            if (filter.Trim() != "")
                strSql.Append(" where " + filter);
            if (sort.Trim() != "")
                strSql.Append(" order by " + sort);

            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /// 返回杭州质量网特定条数的数据
        public DataSet GetNewsById_hzzlw(string strFilter, string strTable, string strWhere, string strOrder)
        {
            string strSql = string.Format(@"select " + strFilter + " from " + strTable + " where " + strWhere + " order by " + strOrder + "");
            return adoHelper_hzzlw.ExecuteSqlDataset(strSql);
        }
        /// <summary>
        /// 返回杭州质量网特定条数的数据
        /// </summary>
        /// <param name="num">个数</param>
        /// <param name="fields">返回字段</param>
        /// <param name="table">表名</param>
        /// <param name="filter">条件</param>
        /// <param name="sort">排序</param>
        /// <returns>返回DataSet集合</returns>
        public DataSet GetNewsTopById_hzzlw(int num, string fields, string table, string filter, string sort)
        {
            StringBuilder strSql = new StringBuilder();
            if (num > 0)
                strSql.Append("select top " + num + " " + fields);
            else
                strSql.Append("select " + fields);
            strSql.Append(" from " + table);
            if (filter.Trim() != "")
                strSql.Append(" where " + filter);
            if (sort.Trim() != "")
                strSql.Append(" order by " + sort);
            return adoHelper_hzzlw.ExecuteSqlDataset(strSql.ToString());
        }

        /// <summary>
        /// 更新新闻的浏览次数
        /// </summary>
        /// <param name="newsid">新闻编号</param>
        /// <returns></returns>
        public int Update_hzzlw(string newsid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_news set hits = hits + 1 where newsid='" + newsid + "'");
            return adoHelper_hzzlw.ExecuteSqlNonQuery(strSql.ToString());
        }
        public string GetTypeNameById_hzzlw(string strTypeId)
        {
            string strSql = string.Format(@"select DirNameChs from T_Directory where DirectoryID='" + strTypeId + "'");
            return adoHelper_hzzlw.ExecuteSqlScalar(strSql).ToString();
        }
        public DataSet GetNewsList_hzzlw(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return PaginationUtility.GetPaginationList_hzzlw(fields, "t_news", filter, sort, currentPageIndex, pageSize, out recordCount);
        }
        //代码遗失公告
        public DataSet GetCodeLost(int intTop)
        {
            string strSql = string.Format(@"Select top " + intTop + " * from HZ_DMYSGG order by ReleaseDate desc");
            return adoHelper_hzzlw.ExecuteSqlDataset(strSql);
        }
        public DataSet GetCodeLost_hzzlw(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return PaginationUtility.GetPaginationList_hzzlw(fields, "HZ_DMYSGG", filter, sort, currentPageIndex, pageSize, out recordCount);
        }
    }
}
