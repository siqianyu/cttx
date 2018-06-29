using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using StarTech.Util;
using StarTech.DBUtility;

namespace StarTech.ELife.ZXTS
{
    public class ZxDal
    {
        public ZxDal()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZxModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ReferOnline(");
            strSql.Append("name,tel,address,department,email,type,content,fillTime,replyTime,replyPeople,title,replyContent,state,isCheck,platform,memberid)");
            strSql.Append(" values (");
            strSql.Append("@name,@tel,@address,@department,@email,@type,@content,@fillTime,@replyTime,@replyPeople,@title,@replyContent,@state,@isCheck,@platform,@memberid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,200),
					new SqlParameter("@department", SqlDbType.VarChar,100),
					new SqlParameter("@email", SqlDbType.VarChar,100),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@content", SqlDbType.NChar,500),
					new SqlParameter("@fillTime", SqlDbType.DateTime),
					new SqlParameter("@replyTime", SqlDbType.DateTime),
					new SqlParameter("@replyPeople", SqlDbType.VarChar,50),
					new SqlParameter("@title", SqlDbType.VarChar,500),
					new SqlParameter("@replyContent", SqlDbType.NText),
					new SqlParameter("@state", SqlDbType.Int,4),
					new SqlParameter("@isCheck", SqlDbType.Int,4),
					new SqlParameter("@platform", SqlDbType.Int,4),
					new SqlParameter("@memberid", SqlDbType.VarChar,50)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.tel;
            parameters[2].Value = model.address;
            parameters[3].Value = model.department;
            parameters[4].Value = model.email;
            parameters[5].Value = model.type;
            parameters[6].Value = model.content;
            parameters[7].Value = model.fillTime;
            parameters[8].Value = model.replyTime;
            parameters[9].Value = model.replyPeople;
            parameters[10].Value = model.title;
            parameters[11].Value = model.replyContent;
            parameters[12].Value = model.state;
            parameters[13].Value = model.isCheck;
            parameters[14].Value = model.platform;
            parameters[15].Value = model.memberid;

            object obj = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
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
        public bool Update(ZxModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ReferOnline set ");
            strSql.Append("name=@name,");
            strSql.Append("tel=@tel,");
            strSql.Append("address=@address,");
            strSql.Append("department=@department,");
            strSql.Append("email=@email,");
            strSql.Append("type=@type,");
            strSql.Append("content=@content,");
            strSql.Append("fillTime=@fillTime,");
            strSql.Append("replyTime=@replyTime,");
            strSql.Append("replyPeople=@replyPeople,");
            strSql.Append("title=@title,");
            strSql.Append("replyContent=@replyContent,");
            strSql.Append("state=@state,");
            strSql.Append("isCheck=@isCheck,");
            strSql.Append("platform=@platform,");
            strSql.Append("memberid=@memberid");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,200),
					new SqlParameter("@department", SqlDbType.VarChar,100),
					new SqlParameter("@email", SqlDbType.VarChar,100),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@content", SqlDbType.NChar,500),
					new SqlParameter("@fillTime", SqlDbType.DateTime),
					new SqlParameter("@replyTime", SqlDbType.DateTime),
					new SqlParameter("@replyPeople", SqlDbType.VarChar,50),
					new SqlParameter("@title", SqlDbType.VarChar,500),
					new SqlParameter("@replyContent", SqlDbType.NText),
					new SqlParameter("@state", SqlDbType.Int,4),
					new SqlParameter("@isCheck", SqlDbType.Int,4),
					new SqlParameter("@platform", SqlDbType.Int,4),
					new SqlParameter("@memberid", SqlDbType.VarChar,50),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.tel;
            parameters[2].Value = model.address;
            parameters[3].Value = model.department;
            parameters[4].Value = model.email;
            parameters[5].Value = model.type;
            parameters[6].Value = model.content;
            parameters[7].Value = model.fillTime;
            parameters[8].Value = model.replyTime;
            parameters[9].Value = model.replyPeople;
            parameters[10].Value = model.title;
            parameters[11].Value = model.replyContent;
            parameters[12].Value = model.state;
            parameters[13].Value = model.isCheck;
            parameters[14].Value = model.platform;
            parameters[15].Value = model.memberid;
            parameters[16].Value = model.id;

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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_ReferOnline ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_ReferOnline ");
            strSql.Append(" where id in (" + idlist + ")  ");
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

        //批量审核
        public bool Approve(string Ids)
        {
            string sql = "UPDATE T_ReferOnline SET isCheck =1 where ID in (" + Ids + ")";
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;

        }

        //批量取消审核
        public bool UnApprove(string Ids)
        {
            string sql = "UPDATE T_ReferOnline SET isCheck =0 where ID in (" + Ids + ")";
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZxModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,tel,address,department,email,type,content,fillTime,replyTime,replyPeople,title,replyContent,state,isCheck,platform,memberid from T_ReferOnline ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            ZxModel model = new ZxModel();
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
        public ZxModel DataRowToModel(DataRow row)
        {
            ZxModel model = new ZxModel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.tel = row["tel"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["department"] != null)
                {
                    model.department = row["department"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["type"] != null && row["type"].ToString() != "")
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["fillTime"] != null && row["fillTime"].ToString() != "")
                {
                    model.fillTime = DateTime.Parse(row["fillTime"].ToString());
                }
                if (row["replyTime"] != null && row["replyTime"].ToString() != "")
                {
                    model.replyTime = DateTime.Parse(row["replyTime"].ToString());
                }
                if (row["replyPeople"] != null)
                {
                    model.replyPeople = row["replyPeople"].ToString();
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["replyContent"] != null)
                {
                    model.replyContent = row["replyContent"].ToString();
                }
                if (row["state"] != null && row["state"].ToString() != "")
                {
                    model.state = int.Parse(row["state"].ToString());
                }
                if (row["isCheck"] != null && row["isCheck"].ToString() != "")
                {
                    model.isCheck = int.Parse(row["isCheck"].ToString());
                }
                if (row["platform"] != null && row["platform"].ToString() != "")
                {
                    model.platform = int.Parse(row["platform"].ToString());
                }
                if (row["memberid"] != null)
                {
                    model.memberid = row["memberid"].ToString();
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
            strSql.Append("select id,name,tel,address,department,email,type,content,fillTime,replyTime,replyPeople,title,replyContent,state,isCheck,platform,memberid ");
            strSql.Append(" FROM T_ReferOnline ");
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
            strSql.Append(" id,name,tel,address,department,email,type,content,fillTime,replyTime,replyPeople,title,replyContent,state,isCheck,platform,memberid ");
            strSql.Append(" FROM T_ReferOnline ");
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
            strSql.Append("select count(1) FROM T_ReferOnline ");
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
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from T_ReferOnline T ");
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
            parameters[0].Value = "T_ReferOnline";
            parameters[1].Value = "id";
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
