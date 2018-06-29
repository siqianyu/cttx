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
    public class TsDal
    {
        public TsDal()
        { }
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_TSInfo(");
            strSql.Append("Subject,Type,SSName,SSAdd,SSPost,SSTel,ProductName,ValueInfo,ParticularInfo,Email,BSName,BSSAdd,BSPost,BSTel,FillTime,IsCheck,Answer,AnswerTime,ChickId,IsPub,isOpen,MemberId)");
            strSql.Append(" values (");
            strSql.Append("@Subject,@Type,@SSName,@SSAdd,@SSPost,@SSTel,@ProductName,@ValueInfo,@ParticularInfo,@Email,@BSName,@BSSAdd,@BSPost,@BSTel,@FillTime,@IsCheck,@Answer,@AnswerTime,@ChickId,@IsPub,@isOpen,@MemberId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Subject", SqlDbType.VarChar,100),
					new SqlParameter("@Type", SqlDbType.VarChar,50),
					new SqlParameter("@SSName", SqlDbType.VarChar,50),
					new SqlParameter("@SSAdd", SqlDbType.VarChar,100),
					new SqlParameter("@SSPost", SqlDbType.VarChar,50),
					new SqlParameter("@SSTel", SqlDbType.VarChar,50),
					new SqlParameter("@ProductName", SqlDbType.VarChar,100),
					new SqlParameter("@ValueInfo", SqlDbType.VarChar,100),
					new SqlParameter("@ParticularInfo", SqlDbType.Text),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@BSName", SqlDbType.VarChar,100),
					new SqlParameter("@BSSAdd", SqlDbType.VarChar,100),
					new SqlParameter("@BSPost", SqlDbType.VarChar,50),
					new SqlParameter("@BSTel", SqlDbType.VarChar,50),
					new SqlParameter("@FillTime", SqlDbType.DateTime),
					new SqlParameter("@IsCheck", SqlDbType.Int,4),
					new SqlParameter("@Answer", SqlDbType.Text),
					new SqlParameter("@AnswerTime", SqlDbType.DateTime),
					new SqlParameter("@ChickId", SqlDbType.VarChar,50),
					new SqlParameter("@IsPub", SqlDbType.Int,4),
					new SqlParameter("@isOpen", SqlDbType.Int,4),
					new SqlParameter("@MemberId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Subject;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.SSName;
            parameters[3].Value = model.SSAdd;
            parameters[4].Value = model.SSPost;
            parameters[5].Value = model.SSTel;
            parameters[6].Value = model.ProductName;
            parameters[7].Value = model.ValueInfo;
            parameters[8].Value = model.ParticularInfo;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.BSName;
            parameters[11].Value = model.BSSAdd;
            parameters[12].Value = model.BSPost;
            parameters[13].Value = model.BSTel;
            parameters[14].Value = model.FillTime;
            parameters[15].Value = model.IsCheck;
            parameters[16].Value = model.Answer;
            parameters[17].Value = model.AnswerTime;
            parameters[18].Value = model.ChickId;
            parameters[19].Value = model.IsPub;
            parameters[20].Value = model.isOpen;
            parameters[21].Value = model.MemberId;

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
        public bool Update(TsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_TSInfo set ");
            strSql.Append("Subject=@Subject,");
            strSql.Append("Type=@Type,");
            strSql.Append("SSName=@SSName,");
            strSql.Append("SSAdd=@SSAdd,");
            strSql.Append("SSPost=@SSPost,");
            strSql.Append("SSTel=@SSTel,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ValueInfo=@ValueInfo,");
            strSql.Append("ParticularInfo=@ParticularInfo,");
            strSql.Append("Email=@Email,");
            strSql.Append("BSName=@BSName,");
            strSql.Append("BSSAdd=@BSSAdd,");
            strSql.Append("BSPost=@BSPost,");
            strSql.Append("BSTel=@BSTel,");
            strSql.Append("FillTime=@FillTime,");
            strSql.Append("IsCheck=@IsCheck,");
            strSql.Append("Answer=@Answer,");
            strSql.Append("AnswerTime=@AnswerTime,");
            strSql.Append("ChickId=@ChickId,");
            strSql.Append("IsPub=@IsPub,");
            strSql.Append("isOpen=@isOpen,");
            strSql.Append("MemberId=@MemberId");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Subject", SqlDbType.VarChar,100),
					new SqlParameter("@Type", SqlDbType.VarChar,50),
					new SqlParameter("@SSName", SqlDbType.VarChar,50),
					new SqlParameter("@SSAdd", SqlDbType.VarChar,100),
					new SqlParameter("@SSPost", SqlDbType.VarChar,50),
					new SqlParameter("@SSTel", SqlDbType.VarChar,50),
					new SqlParameter("@ProductName", SqlDbType.VarChar,100),
					new SqlParameter("@ValueInfo", SqlDbType.VarChar,100),
					new SqlParameter("@ParticularInfo", SqlDbType.Text),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@BSName", SqlDbType.VarChar,100),
					new SqlParameter("@BSSAdd", SqlDbType.VarChar,100),
					new SqlParameter("@BSPost", SqlDbType.VarChar,50),
					new SqlParameter("@BSTel", SqlDbType.VarChar,50),
					new SqlParameter("@FillTime", SqlDbType.DateTime),
					new SqlParameter("@IsCheck", SqlDbType.Int,4),
					new SqlParameter("@Answer", SqlDbType.Text),
					new SqlParameter("@AnswerTime", SqlDbType.DateTime),
					new SqlParameter("@ChickId", SqlDbType.VarChar,50),
					new SqlParameter("@IsPub", SqlDbType.Int,4),
					new SqlParameter("@isOpen", SqlDbType.Int,4),
					new SqlParameter("@MemberId", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Subject;
            parameters[1].Value = model.Type;
            parameters[2].Value = model.SSName;
            parameters[3].Value = model.SSAdd;
            parameters[4].Value = model.SSPost;
            parameters[5].Value = model.SSTel;
            parameters[6].Value = model.ProductName;
            parameters[7].Value = model.ValueInfo;
            parameters[8].Value = model.ParticularInfo;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.BSName;
            parameters[11].Value = model.BSSAdd;
            parameters[12].Value = model.BSPost;
            parameters[13].Value = model.BSTel;
            parameters[14].Value = model.FillTime;
            parameters[15].Value = model.IsCheck;
            parameters[16].Value = model.Answer;
            parameters[17].Value = model.AnswerTime;
            parameters[18].Value = model.ChickId;
            parameters[19].Value = model.IsPub;
            parameters[20].Value = model.isOpen;
            parameters[21].Value = model.MemberId;
            parameters[22].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_TSInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_TSInfo ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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

        //审核
        public bool Approve(string Ids)
        {
            string sql = "UPDATE T_TSInfo SET IsPub =1 where ID in (" + Ids + ")";
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;
        }

        //取消审核
        public bool UnApprove(string Ids)
        {
            string sql = "UPDATE T_TSInfo SET IsPub =0 where ID in (" + Ids + ")";
            return adoHelper.ExecuteSqlNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TsModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Subject,Type,SSName,SSAdd,SSPost,SSTel,ProductName,ValueInfo,ParticularInfo,Email,BSName,BSSAdd,BSPost,BSTel,FillTime,IsCheck,Answer,AnswerTime,ChickId,IsPub,isOpen,MemberId from T_TSInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            TsModel model = new TsModel();
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
        public TsModel DataRowToModel(DataRow row)
        {
            TsModel model = new TsModel();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Subject"] != null)
                {
                    model.Subject = row["Subject"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = row["Type"].ToString();
                }
                if (row["SSName"] != null)
                {
                    model.SSName = row["SSName"].ToString();
                }
                if (row["SSAdd"] != null)
                {
                    model.SSAdd = row["SSAdd"].ToString();
                }
                if (row["SSPost"] != null)
                {
                    model.SSPost = row["SSPost"].ToString();
                }
                if (row["SSTel"] != null)
                {
                    model.SSTel = row["SSTel"].ToString();
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["ValueInfo"] != null)
                {
                    model.ValueInfo = row["ValueInfo"].ToString();
                }
                if (row["ParticularInfo"] != null)
                {
                    model.ParticularInfo = row["ParticularInfo"].ToString();
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["BSName"] != null)
                {
                    model.BSName = row["BSName"].ToString();
                }
                if (row["BSSAdd"] != null)
                {
                    model.BSSAdd = row["BSSAdd"].ToString();
                }
                if (row["BSPost"] != null)
                {
                    model.BSPost = row["BSPost"].ToString();
                }
                if (row["BSTel"] != null)
                {
                    model.BSTel = row["BSTel"].ToString();
                }
                if (row["FillTime"] != null && row["FillTime"].ToString() != "")
                {
                    model.FillTime = DateTime.Parse(row["FillTime"].ToString());
                }
                if (row["IsCheck"] != null && row["IsCheck"].ToString() != "")
                {
                    model.IsCheck = int.Parse(row["IsCheck"].ToString());
                }
                if (row["Answer"] != null)
                {
                    model.Answer = row["Answer"].ToString();
                }
                if (row["AnswerTime"] != null && row["AnswerTime"].ToString() != "")
                {
                    model.AnswerTime = DateTime.Parse(row["AnswerTime"].ToString());
                }
                if (row["ChickId"] != null)
                {
                    model.ChickId = row["ChickId"].ToString();
                }
                if (row["IsPub"] != null && row["IsPub"].ToString() != "")
                {
                    model.IsPub = int.Parse(row["IsPub"].ToString());
                }
                if (row["isOpen"] != null && row["isOpen"].ToString() != "")
                {
                    model.isOpen = int.Parse(row["isOpen"].ToString());
                }
                if (row["MemberId"] != null)
                {
                    model.MemberId = row["MemberId"].ToString();
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
            strSql.Append("select ID,Subject,Type,SSName,SSAdd,SSPost,SSTel,ProductName,ValueInfo,ParticularInfo,Email,BSName,BSSAdd,BSPost,BSTel,FillTime,IsCheck,Answer,AnswerTime,ChickId,IsPub,isOpen,MemberId ");
            strSql.Append(" FROM T_TSInfo ");
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
            strSql.Append(" ID,Subject,Type,SSName,SSAdd,SSPost,SSTel,ProductName,ValueInfo,ParticularInfo,Email,BSName,BSSAdd,BSPost,BSTel,FillTime,IsCheck,Answer,AnswerTime,ChickId,IsPub,isOpen,MemberId ");
            strSql.Append(" FROM T_TSInfo ");
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
            strSql.Append("select count(1) FROM T_TSInfo ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from T_TSInfo T ");
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
            parameters[0].Value = "T_TSInfo";
            parameters[1].Value = "ID";
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
