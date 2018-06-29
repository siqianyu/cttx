using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Startech.Utils;

namespace Startech.News
{
    public class TopicsDAL
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");

        public TopicsDAL()
		{}

		#region  成员方法

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(TopicsModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Topics(");
			strSql.Append("Title,Body,TopicsCategoryId,KeyWord,FromSource,Author,ViewCount,AddedDate,ReleaseDate)");
			strSql.Append(" values (");
			strSql.Append("@Title,@Body,@TopicsCategoryId,@KeyWord,@FromSource,@Author,@ViewCount,@AddedDate,@ReleaseDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.VarChar,200),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@TopicsCategoryId", SqlDbType.Int,4),
					new SqlParameter("@KeyWord", SqlDbType.VarChar,100),
					new SqlParameter("@FromSource", SqlDbType.VarChar,100),
					new SqlParameter("@Author", SqlDbType.VarChar,50),
					new SqlParameter("@ViewCount", SqlDbType.Int,4),
					new SqlParameter("@AddedDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Body;
			parameters[2].Value = model.TopicsCategoryId;
			parameters[3].Value = model.KeyWord;
			parameters[4].Value = model.FromSource;
			parameters[5].Value = model.Author;
			parameters[6].Value = model.ViewCount;
			parameters[7].Value = model.AddedDate;
			parameters[8].Value = model.ReleaseDate;

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
		public void Update(TopicsModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Topics set ");
			strSql.Append("Title=@Title,");
			strSql.Append("Body=@Body,");
			strSql.Append("TopicsCategoryId=@TopicsCategoryId,");
			strSql.Append("KeyWord=@KeyWord,");
			strSql.Append("FromSource=@FromSource,");
			strSql.Append("Author=@Author,");
			strSql.Append("ReleaseDate=@ReleaseDate,");
            strSql.Append("ViewCount=@ViewCount");
			strSql.Append(" where TopicsId=@TopicsId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicsId", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,200),
					new SqlParameter("@Body", SqlDbType.Text),
					new SqlParameter("@TopicsCategoryId", SqlDbType.Int,4),
					new SqlParameter("@KeyWord", SqlDbType.VarChar,100),
					new SqlParameter("@FromSource", SqlDbType.VarChar,100),
					new SqlParameter("@Author", SqlDbType.VarChar,50),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
                    new SqlParameter("@ViewCount",SqlDbType.Int,4)};
			parameters[0].Value = model.TopicsId;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Body;
			parameters[3].Value = model.TopicsCategoryId;
			parameters[4].Value = model.KeyWord;
			parameters[5].Value = model.FromSource;
			parameters[6].Value = model.Author;
			parameters[7].Value = model.ReleaseDate;
            parameters[8].Value = model.ViewCount;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int TopicsId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Topics ");
			strSql.Append(" where TopicsId=@TopicsId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicsId", SqlDbType.Int,4)};
			parameters[0].Value = TopicsId;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TopicsModel GetModel(int TopicsId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TopicsId,Title,Body,TopicsCategoryId,KeyWord,FromSource,Author,ViewCount,AddedDate,ReleaseDate from T_Topics ");
			strSql.Append(" where TopicsId=@TopicsId ");
			SqlParameter[] parameters = {
					new SqlParameter("@TopicsId", SqlDbType.Int,4)};
			parameters[0].Value = TopicsId;

            TopicsModel model = new TopicsModel();
			DataSet ds=adoHelper.ExecuteSqlDataset(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TopicsId"].ToString()!="")
				{
					model.TopicsId=int.Parse(ds.Tables[0].Rows[0]["TopicsId"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Body=ds.Tables[0].Rows[0]["Body"].ToString();
				if(ds.Tables[0].Rows[0]["TopicsCategoryId"].ToString()!="")
				{
					model.TopicsCategoryId=int.Parse(ds.Tables[0].Rows[0]["TopicsCategoryId"].ToString());
				}
				model.KeyWord=ds.Tables[0].Rows[0]["KeyWord"].ToString();
				model.FromSource=ds.Tables[0].Rows[0]["FromSource"].ToString();
				model.Author=ds.Tables[0].Rows[0]["Author"].ToString();
				if(ds.Tables[0].Rows[0]["ViewCount"].ToString()!="")
				{
					model.ViewCount=int.Parse(ds.Tables[0].Rows[0]["ViewCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddedDate"].ToString()!="")
				{
					model.AddedDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddedDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseDate"].ToString()!="")
				{
					model.ReleaseDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
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
			strSql.Append("select TopicsId,Title,Body,TopicsCategoryId,KeyWord,FromSource,Author,ViewCount,AddedDate,ReleaseDate ");
			strSql.Append(" FROM T_Topics ");
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
			strSql.Append(" TopicsId,Title,Body,TopicsCategoryId,KeyWord,FromSource,Author,ViewCount,AddedDate,ReleaseDate ");
			strSql.Append(" FROM T_Topics ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

        /// <summary>
        /// 更新点击率
        /// </summary>
        /// <param name="nid">新闻编号</param>
        public void UpdateHits(int nid)
        {
            string strSql = "Update T_Topics  set ViewCount=ViewCount+1 where TopicsId=" + nid + "";
            adoHelper.ExecuteSqlNonQuery(strSql);
        }

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		*/
        public DataSet GetTopicsList(string filed, string filter, string sort, int currentPage, int pageSize, out int count)
        {
            return PaginationUtility.GetPaginationList(filed, "V_Topics_TopicsCty", filter, sort, currentPage, pageSize, out count);
        }

		#endregion  成员方法
    }
}
