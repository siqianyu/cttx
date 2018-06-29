using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using StarTech.Util;
using StarTech.DBUtility;

namespace StarTech.ELife.News
{
    public class NewsDal
    {
        public NewsDal()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_News(");
            strSql.Append("NewsID,Title,SubHead,Body,AddedUserId,PublicationUnit,AddedDate,IsTop,IndexCommend,ArticleType,ReleaseDate,ExpireDate,CategoryId,Approved,ViewCount,ImgLink,IsState,HotPic,KeyWord,HotDays,FromSource,IsComment,IsScrool,Period,Sort,lookNum)");
            strSql.Append(" values (");
            strSql.Append("@NewsID,@Title,@SubHead,@Body,@AddedUserId,@PublicationUnit,@AddedDate,@IsTop,@IndexCommend,@ArticleType,@ReleaseDate,@ExpireDate,@CategoryId,@Approved,@ViewCount,@ImgLink,@IsState,@HotPic,@KeyWord,@HotDays,@FromSource,@IsComment,@IsScrool,@Period,@Sort,@lookNum)");
            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.VarChar,50),
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
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@lookNum", SqlDbType.Int,4)};
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
            parameters[24].Value = model.Sort;
            parameters[25].Value = model.lookNum;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(NewsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_News set ");
            strSql.Append("NewsID=@NewsID,");
            strSql.Append("Title=@Title,");
            strSql.Append("SubHead=@SubHead,");
            strSql.Append("Body=@Body,");
            strSql.Append("AddedUserId=@AddedUserId,");
            strSql.Append("PublicationUnit=@PublicationUnit,");
            strSql.Append("AddedDate=@AddedDate,");
            strSql.Append("IsTop=@IsTop,");
            strSql.Append("IndexCommend=@IndexCommend,");
            strSql.Append("ArticleType=@ArticleType,");
            strSql.Append("ReleaseDate=@ReleaseDate,");
            strSql.Append("ExpireDate=@ExpireDate,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("Approved=@Approved,");
            strSql.Append("ViewCount=@ViewCount,");
            strSql.Append("ImgLink=@ImgLink,");
            strSql.Append("IsState=@IsState,");
            strSql.Append("HotPic=@HotPic,");
            strSql.Append("KeyWord=@KeyWord,");
            strSql.Append("HotDays=@HotDays,");
            strSql.Append("FromSource=@FromSource,");
            strSql.Append("IsComment=@IsComment,");
            strSql.Append("IsScrool=@IsScrool,");
            strSql.Append("Period=@Period,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("lookNum=@lookNum");
            strSql.Append(" where NewsID=@NewsID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.VarChar,50),
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
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@lookNum", SqlDbType.Int,4)};
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
            parameters[24].Value = model.Sort;
            parameters[25].Value = model.lookNum;

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
        public bool Delete(string NewsID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_News ");
            strSql.Append(" where NewsID=@NewsID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.VarChar,50)			};
            parameters[0].Value = NewsID;

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
        public bool DeleteList(string NewsIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_News ");
            strSql.Append(" where NewsID in (" + NewsIDlist + ")  ");
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
        /// 审核通过
        /// </summary>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        public bool Approve(string NewsId)
        {
            string sql = "update T_News set Approved=1  where NewsId = " + NewsId;
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        public bool UnApprove(string NewsId)
        {
            string sql = "update T_News set Approved=0  where NewsId = " + NewsId;
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NewsModel GetModel(string NewsID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 NewsID,Title,SubHead,Body,AddedUserId,PublicationUnit,AddedDate,IsTop,IndexCommend,ArticleType,ReleaseDate,ExpireDate,CategoryId,Approved,ViewCount,ImgLink,IsState,HotPic,KeyWord,HotDays,FromSource,IsComment,IsScrool,Period,Sort,lookNum from T_News ");
            strSql.Append(" where NewsID=@NewsID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewsID", SqlDbType.VarChar,50)			};
            parameters[0].Value = NewsID;

            NewsModel model = new NewsModel();
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
        public NewsModel DataRowToModel(DataRow row)
        {
            NewsModel model = new NewsModel();
            if (row != null)
            {
                if (row["NewsID"] != null)
                {
                    model.NewsID = row["NewsID"].ToString();
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["SubHead"] != null)
                {
                    model.SubHead = row["SubHead"].ToString();
                }
                if (row["Body"] != null)
                {
                    model.Body = row["Body"].ToString();
                }
                if (row["AddedUserId"] != null && row["AddedUserId"].ToString() != "")
                {
                    model.AddedUserId = int.Parse(row["AddedUserId"].ToString());
                }
                if (row["PublicationUnit"] != null)
                {
                    model.PublicationUnit = row["PublicationUnit"].ToString();
                }
                if (row["AddedDate"] != null && row["AddedDate"].ToString() != "")
                {
                    model.AddedDate = DateTime.Parse(row["AddedDate"].ToString());
                }
                if (row["IsTop"] != null && row["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(row["IsTop"].ToString());
                }
                if (row["IndexCommend"] != null && row["IndexCommend"].ToString() != "")
                {
                    model.IndexCommend = int.Parse(row["IndexCommend"].ToString());
                }
                if (row["ArticleType"] != null && row["ArticleType"].ToString() != "")
                {
                    model.ArticleType = int.Parse(row["ArticleType"].ToString());
                }
                if (row["ReleaseDate"] != null && row["ReleaseDate"].ToString() != "")
                {
                    model.ReleaseDate = DateTime.Parse(row["ReleaseDate"].ToString());
                }
                if (row["ExpireDate"] != null && row["ExpireDate"].ToString() != "")
                {
                    model.ExpireDate = DateTime.Parse(row["ExpireDate"].ToString());
                }
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["Approved"] != null && row["Approved"].ToString() != "")
                {
                    model.Approved = int.Parse(row["Approved"].ToString());
                }
                if (row["ViewCount"] != null && row["ViewCount"].ToString() != "")
                {
                    model.ViewCount = int.Parse(row["ViewCount"].ToString());
                }
                if (row["ImgLink"] != null)
                {
                    model.ImgLink = row["ImgLink"].ToString();
                }
                if (row["IsState"] != null && row["IsState"].ToString() != "")
                {
                    model.IsState = int.Parse(row["IsState"].ToString());
                }
                if (row["HotPic"] != null)
                {
                    model.HotPic = row["HotPic"].ToString();
                }
                if (row["KeyWord"] != null)
                {
                    model.KeyWord = row["KeyWord"].ToString();
                }
                if (row["HotDays"] != null && row["HotDays"].ToString() != "")
                {
                    model.HotDays = int.Parse(row["HotDays"].ToString());
                }
                if (row["FromSource"] != null)
                {
                    model.FromSource = row["FromSource"].ToString();
                }
                if (row["IsComment"] != null && row["IsComment"].ToString() != "")
                {
                    model.IsComment = int.Parse(row["IsComment"].ToString());
                }
                if (row["IsScrool"] != null && row["IsScrool"].ToString() != "")
                {
                    model.IsScrool = int.Parse(row["IsScrool"].ToString());
                }
                if (row["Period"] != null)
                {
                    model.Period = row["Period"].ToString();
                }
                if (row["Sort"] != null && row["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(row["Sort"].ToString());
                }
                if (row["lookNum"] != null && row["lookNum"].ToString() != "")
                {
                    model.lookNum = int.Parse(row["lookNum"].ToString());
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
            strSql.Append("select NewsID,Title,SubHead,Body,AddedUserId,PublicationUnit,AddedDate,IsTop,IndexCommend,ArticleType,ReleaseDate,ExpireDate,CategoryId,Approved,ViewCount,ImgLink,IsState,HotPic,KeyWord,HotDays,FromSource,IsComment,IsScrool,Period,Sort,lookNum ");
            strSql.Append(" FROM T_News ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" NewsID,Title,SubHead,Body,AddedUserId,PublicationUnit,AddedDate,IsTop,IndexCommend,ArticleType,ReleaseDate,ExpireDate,CategoryId,Approved,ViewCount,ImgLink,IsState,HotPic,KeyWord,HotDays,FromSource,IsComment,IsScrool,Period,Sort,lookNum ");
            strSql.Append(" FROM T_News ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_News ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.NewsID desc");
            }
            strSql.Append(")AS Row, T.*  from T_News T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "T_News";
            parameters[1].Value = "NewsID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
