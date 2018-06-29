using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using StarTech.Util;
using StarTech.DBUtility;

namespace StarTech.ELife.Ad
{
    public class AdDal
    {
        public AdDal()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(AdModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Ad(");
            strSql.Append("Title,Link,Image,DisplayMode,sort,StartTime,EndTime,CategoryId,video,AddPerson)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Link,@Image,@DisplayMode,@sort,@StartTime,@EndTime,@CategoryId,@video,@AddPerson)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.VarChar,50),
					new SqlParameter("@Link", SqlDbType.VarChar,100),
					new SqlParameter("@Image", SqlDbType.VarChar,100),
					new SqlParameter("@DisplayMode", SqlDbType.Int,4),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@video", SqlDbType.VarChar,150),
					new SqlParameter("@AddPerson", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Link;
            parameters[2].Value = model.Image;
            parameters[3].Value = model.DisplayMode;
            parameters[4].Value = model.sort;
            parameters[5].Value = model.StartTime;
            parameters[6].Value = model.EndTime;
            parameters[7].Value = model.CategoryId;
            parameters[8].Value = model.video;
            parameters[9].Value = model.AddPerson;

            object obj = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AdModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Ad set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Link=@Link,");
            strSql.Append("Image=@Image,");
            strSql.Append("DisplayMode=@DisplayMode,");
            strSql.Append("sort=@sort,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("video=@video,");
            strSql.Append("AddPerson=@AddPerson");
            strSql.Append(" where AdId=@AdId");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.VarChar,50),
					new SqlParameter("@Link", SqlDbType.VarChar,100),
					new SqlParameter("@Image", SqlDbType.VarChar,100),
					new SqlParameter("@DisplayMode", SqlDbType.Int,4),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@video", SqlDbType.VarChar,150),
					new SqlParameter("@AddPerson", SqlDbType.VarChar,50),
					new SqlParameter("@AdId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Link;
            parameters[2].Value = model.Image;
            parameters[3].Value = model.DisplayMode;
            parameters[4].Value = model.sort;
            parameters[5].Value = model.StartTime;
            parameters[6].Value = model.EndTime;
            parameters[7].Value = model.CategoryId;
            parameters[8].Value = model.video;
            parameters[9].Value = model.AddPerson;
            parameters[10].Value = model.AdId;

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
        public bool Delete(long AdId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Ad ");
            strSql.Append(" where AdId=@AdId");
            SqlParameter[] parameters = {
					new SqlParameter("@AdId", SqlDbType.BigInt)
			};
            parameters[0].Value = AdId;

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
        public bool DeleteList(string AdIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Ad ");
            strSql.Append(" where AdId in (" + AdIdlist + ")  ");
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
        public AdModel GetModel(long AdId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 AdId,Title,Link,Image,DisplayMode,sort,StartTime,EndTime,CategoryId,video,AddPerson from T_Ad ");
            strSql.Append(" where AdId=@AdId");
            SqlParameter[] parameters = {
					new SqlParameter("@AdId", SqlDbType.BigInt)
			};
            parameters[0].Value = AdId;

            AdModel model = new AdModel();
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
        public AdModel DataRowToModel(DataRow row)
        {
            AdModel model = new AdModel();
            if (row != null)
            {
                if (row["AdId"] != null && row["AdId"].ToString() != "")
                {
                    model.AdId = long.Parse(row["AdId"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Link"] != null)
                {
                    model.Link = row["Link"].ToString();
                }
                if (row["Image"] != null)
                {
                    model.Image = row["Image"].ToString();
                }
                if (row["DisplayMode"] != null && row["DisplayMode"].ToString() != "")
                {
                    model.DisplayMode = int.Parse(row["DisplayMode"].ToString());
                }
                if (row["sort"] != null && row["sort"].ToString() != "")
                {
                    model.sort = int.Parse(row["sort"].ToString());
                }
                if (row["StartTime"] != null && row["StartTime"].ToString() != "")
                {
                    model.StartTime = DateTime.Parse(row["StartTime"].ToString());
                }
                if (row["EndTime"] != null && row["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(row["EndTime"].ToString());
                }
                if (row["CategoryId"] != null && row["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if (row["video"] != null)
                {
                    model.video = row["video"].ToString();
                }
                if (row["AddPerson"] != null)
                {
                    model.AddPerson = row["AddPerson"].ToString();
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
            strSql.Append("select AdId,Title,Link,Image,DisplayMode,sort,StartTime,EndTime,CategoryId,video,AddPerson ");
            strSql.Append(" FROM T_Ad ");
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
            strSql.Append(" AdId,Title,Link,Image,DisplayMode,sort,StartTime,EndTime,CategoryId,video,AddPerson ");
            strSql.Append(" FROM T_Ad ");
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
            strSql.Append("select count(1) FROM T_Ad ");
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
                strSql.Append("order by T.AdId desc");
            }
            strSql.Append(")AS Row, T.*  from T_Ad T ");
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
            parameters[0].Value = "T_Ad";
            parameters[1].Value = "AdId";
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
