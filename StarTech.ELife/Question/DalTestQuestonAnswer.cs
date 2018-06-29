using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace StarTech.ELife.Question
{
    public partial class DalTestQuestonAnswer
    {
        public DalTestQuestonAnswer()
        { }
        #region  Method
        AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModelTestQuestonAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Test_QuestonAnswer(");
            strSql.Append("sysnumber,questionSysnumber,AnswerKey,AnswerValue,orderby)");
            strSql.Append(" values (");
            strSql.Append("@sysnumber,@questionSysnumber,@AnswerKey,@AnswerValue,@orderby)");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,255),
					new SqlParameter("@questionSysnumber", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerKey", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerValue", SqlDbType.VarChar,255),
                    new SqlParameter("@orderby", SqlDbType.Int,4),
                                          };
            parameters[0].Value = model.sysnumber;
            parameters[1].Value = model.questionSysnumber;
            parameters[2].Value = model.AnswerKey;
            parameters[3].Value = model.AnswerValue;
            parameters[4].Value = model.OrderBy;

            return adohelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ModelTestQuestonAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Test_QuestonAnswer set ");
            strSql.Append("questionSysnumber=@questionSysnumber,");
            strSql.Append("AnswerKey=@AnswerKey,");
            strSql.Append("AnswerValue=@AnswerValue");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@questionSysnumber", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerKey", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerValue", SqlDbType.VarChar,255),
					new SqlParameter("@sysnumber", SqlDbType.VarChar,255)};
            parameters[0].Value = model.questionSysnumber;
            parameters[1].Value = model.AnswerKey;
            parameters[2].Value = model.AnswerValue;
            parameters[3].Value = model.sysnumber;

            adohelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Test_QuestonAnswer ");
            strSql.Append(" where sysnumber=@sysnumber");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,255),
					new SqlParameter("@questionSysnumber", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerKey", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerValue", SqlDbType.VarChar,255)};
            parameters[0].Value = sysnumber;

            adohelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModelTestQuestonAnswer GetModel(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sysnumber,questionSysnumber,AnswerKey,AnswerValue from T_Test_QuestonAnswer ");
            strSql.Append(" where sysnumber=@sysnumber");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,255),
					new SqlParameter("@questionSysnumber", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerKey", SqlDbType.VarChar,255),
					new SqlParameter("@AnswerValue", SqlDbType.VarChar,255)};
            parameters[0].Value = sysnumber;
            ModelTestQuestonAnswer model = new ModelTestQuestonAnswer();
            DataSet ds = adohelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["sysnumber"] != null && ds.Tables[0].Rows[0]["sysnumber"].ToString() != "")
                {
                    model.sysnumber = ds.Tables[0].Rows[0]["sysnumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["questionSysnumber"] != null && ds.Tables[0].Rows[0]["questionSysnumber"].ToString() != "")
                {
                    model.questionSysnumber = ds.Tables[0].Rows[0]["questionSysnumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AnswerKey"] != null && ds.Tables[0].Rows[0]["AnswerKey"].ToString() != "")
                {
                    model.AnswerKey = ds.Tables[0].Rows[0]["AnswerKey"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AnswerValue"] != null && ds.Tables[0].Rows[0]["AnswerValue"].ToString() != "")
                {
                    model.AnswerValue = ds.Tables[0].Rows[0]["AnswerValue"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sysnumber,questionSysnumber,AnswerKey,AnswerValue ");
            strSql.Append(" FROM T_Test_QuestonAnswer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adohelper.ExecuteSqlDataset(strSql.ToString());
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
                    new SqlParameter("@PageSize", OleDbType.Integer),
                    new SqlParameter("@PageIndex", OleDbType.Integer),
                    new SqlParameter("@IsReCount", OleDbType.Boolean),
                    new SqlParameter("@OrderType", OleDbType.Boolean),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "T_Test_ QuestonAnswer";
            parameters[1].Value = "AnswerValue";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperOleDb.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}
