using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;
using System.Text;

namespace Startech.News
{
    public class CommentDAL
    {
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");

        public int Add(CommentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Comment(");
            strSql.Append("NewsID,CommentName,CommentContent)");
            strSql.Append(" values (");
            strSql.Append("@NewsID,@CommentName,@CommentContent)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.Int,4),
					new SqlParameter("@CommentName", SqlDbType.VarChar,20),
					new SqlParameter("@CommentContent", SqlDbType.VarChar,500)};
            parameters[0].Value = model.NewsID;
            parameters[1].Value = model.CommentName;
            parameters[2].Value = model.CommentContent;

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
        public int Update(CommentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Comment set ");
            strSql.Append("NewsID=@NewsID,");
            strSql.Append("CommentName=@CommentName,");
            strSql.Append("CommentContent=@CommentContent");
            strSql.Append(" where CommentID=@CommentID");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4),
					new SqlParameter("@NewsID", SqlDbType.Int,4),
					new SqlParameter("@CommentName", SqlDbType.VarChar,20),
					new SqlParameter("@CommentContent", SqlDbType.VarChar,500)};
            parameters[0].Value = model.CommentID;
            parameters[1].Value = model.NewsID;
            parameters[2].Value = model.CommentName;
            parameters[3].Value = model.CommentContent;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int CommentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Comment ");
            strSql.Append(" where CommentID=@CommentID");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)
				};
            parameters[0].Value = CommentID;
            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CommentModel GetModel(int CommentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Comment ");
            strSql.Append(" where NewsID=@CommentID");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentID", SqlDbType.Int,4)};
            parameters[0].Value = CommentID;
            CommentModel model = new CommentModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            model.CommentID = CommentID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["NewsID"].ToString() != "")
                {
                    model.NewsID = int.Parse(ds.Tables[0].Rows[0]["NewsID"].ToString());
                }
                model.CommentName = ds.Tables[0].Rows[0]["CommentName"].ToString();
                model.CommentContent = ds.Tables[0].Rows[0]["CommentContent"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        public DataSet GetArticleList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return Startech.Utils.PaginationUtility.GetPaginationList(fields, "T_Comment", filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Comment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CommentID ");
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }
        #endregion  成员方法
    }
}
