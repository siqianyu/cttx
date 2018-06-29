using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace StarTech.ELife.Question
{
    public partial class DalTestQueston
    {
        public DalTestQueston()
        { }
        #region  Method
        AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModelTestQueston model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Test_Queston(");
            strSql.Append("sysnumber,questionType,questionTitle,questionAnswer,orwner,description,personFlag,levelFlag,categoryFlag,categorypath,ifCourseQuestion,createTime,createPerson,shFlag,orderBy,courseId,isSubQuestion,mainQuestionSysnumber,levelPoint)");
            strSql.Append(" values (");
            strSql.Append("@sysnumber,@questionType,@questionTitle,@questionAnswer,@orwner,@description,@personFlag,@levelFlag,@categoryFlag,@categorypath,@ifCourseQuestion,@createTime,@createPerson,@shFlag,@orderBy,@courseId,@isSubQuestion,@mainQuestionSysnumber,@levelPoint)");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.NVarChar,255),
					new SqlParameter("@questionType", SqlDbType.NVarChar,255),
					new SqlParameter("@questionTitle", SqlDbType.NVarChar,4000),
					new SqlParameter("@questionAnswer", SqlDbType.NVarChar,550),
					new SqlParameter("@orwner", SqlDbType.NVarChar,550),
					new SqlParameter("@description", SqlDbType.NVarChar,4000),
					new SqlParameter("@personFlag", SqlDbType.VarChar,50),
					new SqlParameter("@levelFlag", SqlDbType.VarChar,50),
					new SqlParameter("@categoryFlag", SqlDbType.VarChar,50),
					new SqlParameter("@categorypath", SqlDbType.VarChar,500),
					new SqlParameter("@ifCourseQuestion", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@createPerson", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.Int,4),
                    new SqlParameter("@orderBy", SqlDbType.Int,4),
                    new SqlParameter("@courseId", SqlDbType.VarChar,50),
                    new SqlParameter("@isSubQuestion", SqlDbType.Int,4),
                    new SqlParameter("@mainQuestionSysnumber", SqlDbType.VarChar,50),
                     new SqlParameter("@levelPoint", SqlDbType.Decimal)
                    
                
            };
            parameters[0].Value = model.sysnumber;
            parameters[1].Value = model.questionType;
            parameters[2].Value = model.questionTitle;
            parameters[3].Value = model.questionAnswer;
            parameters[4].Value = model.Orner;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.personFlag;
            parameters[7].Value = model.levelFlag;
            parameters[8].Value = model.categoryFlag;
            parameters[9].Value = model.categorypath;
            parameters[10].Value = model.ifCourseQuestion;
            parameters[11].Value = model.createTime;
            parameters[12].Value = model.createPerson;
            parameters[13].Value = model.shFlag;
            parameters[14].Value = model.orderBy;
            parameters[15].Value = model.courseId;
            parameters[16].Value = model.isSubQuestion;
            parameters[17].Value = model.mainQuestionSysnumber;
            parameters[18].Value = model.LevelPoint;
            return adohelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ModelTestQueston model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Test_Queston set ");
            strSql.Append("questionType=@questionType,");
            strSql.Append("questionTitle=@questionTitle,");
            strSql.Append("questionAnswer=@questionAnswer,");
            strSql.Append("orwner=@orwner,");
            strSql.Append("description=@description,");
            strSql.Append("personFlag=@personFlag,");
            strSql.Append("levelFlag=@levelFlag,");
            strSql.Append("categoryFlag=@categoryFlag,");
            strSql.Append("categorypath=@categorypath,");
            strSql.Append("ifCourseQuestion=@ifCourseQuestion,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createPerson=@createPerson,");
            strSql.Append("shFlag=@shFlag,orderBy=@orderBy,courseId=@courseId,isSubQuestion=@isSubQuestion,mainQuestionSysnumber=@mainQuestionSysnumber,levelPoint=@levelPoint");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@questionType", SqlDbType.NVarChar,255),
					new SqlParameter("@questionTitle", SqlDbType.NVarChar,4000),
					new SqlParameter("@questionAnswer", SqlDbType.NVarChar,550),
					new SqlParameter("@orwner", SqlDbType.NVarChar,550),
					new SqlParameter("@description", SqlDbType.NVarChar,4000),
					new SqlParameter("@personFlag", SqlDbType.VarChar,50),
					new SqlParameter("@levelFlag", SqlDbType.VarChar,50),
					new SqlParameter("@categoryFlag", SqlDbType.VarChar,50),
					new SqlParameter("@categorypath", SqlDbType.VarChar,500),
					new SqlParameter("@ifCourseQuestion", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@createPerson", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.Int,4),
                    new SqlParameter("@orderBy", SqlDbType.Int,4),
                    new SqlParameter("@courseId", SqlDbType.VarChar,50),
                    new SqlParameter("@isSubQuestion", SqlDbType.Int,4),
                    new SqlParameter("@mainQuestionSysnumber", SqlDbType.VarChar,50),
                    new SqlParameter("@levelPoint", SqlDbType.Decimal),
					new SqlParameter("@sysnumber", SqlDbType.NVarChar,255)
            };
            parameters[0].Value = model.questionType;
            parameters[1].Value = model.questionTitle;
            parameters[2].Value = model.questionAnswer;
            parameters[3].Value = model.Orner;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.personFlag;
            parameters[6].Value = model.levelFlag;
            parameters[7].Value = model.categoryFlag;
            parameters[8].Value = model.categorypath;
            parameters[9].Value = model.ifCourseQuestion;
            parameters[10].Value = model.createTime;
            parameters[11].Value = model.createPerson;
            parameters[12].Value = model.shFlag;
            parameters[13].Value = model.orderBy;
            parameters[14].Value = model.courseId;
            parameters[15].Value = model.isSubQuestion;
            parameters[16].Value = model.mainQuestionSysnumber;
            parameters[17].Value = model.LevelPoint;
            parameters[18].Value = model.sysnumber;


            adohelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Test_Queston ");
            strSql.Append(" where sysnumber=@sysnumber");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,255)};
            parameters[0].Value = sysnumber;

            adohelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Test_Queston ");
            strSql.Append(" where sysnumber in(@Sysnumber)");
            SqlParameter[] parameters = {
					new SqlParameter("@Sysnumber", SqlDbType.VarChar,255)};
            parameters[0].Value = sysnumber;
            string sys = sysnumber;
            return adohelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModelTestQueston GetModel(string sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_Test_Queston ");
            strSql.Append(" where sysnumber=@sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,255)};
            parameters[0].Value = sysnumber;

            ModelTestQueston model = new ModelTestQueston();
            DataSet ds = adohelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["sysnumber"] != null && ds.Tables[0].Rows[0]["sysnumber"].ToString() != "")
                {
                    model.sysnumber = ds.Tables[0].Rows[0]["sysnumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["questionType"] != null && ds.Tables[0].Rows[0]["questionType"].ToString() != "")
                {
                    model.questionType = ds.Tables[0].Rows[0]["questionType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["questionTitle"] != null && ds.Tables[0].Rows[0]["questionTitle"].ToString() != "")
                {
                    model.questionTitle = ds.Tables[0].Rows[0]["questionTitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["questionAnswer"] != null && ds.Tables[0].Rows[0]["questionAnswer"].ToString() != "")
                {
                    model.questionAnswer = ds.Tables[0].Rows[0]["questionAnswer"].ToString();
                }
                if (ds.Tables[0].Rows[0]["orwner"] != null && ds.Tables[0].Rows[0]["orwner"].ToString() != "")
                {
                    model.Orner= ds.Tables[0].Rows[0]["orwner"].ToString();
                }
                if (ds.Tables[0].Rows[0]["description"] != null && ds.Tables[0].Rows[0]["description"].ToString() != "")
                {
                    model.Description = ds.Tables[0].Rows[0]["description"].ToString();
                }

                if (ds.Tables[0].Rows[0]["personFlag"] != null && ds.Tables[0].Rows[0]["personFlag"].ToString()!="")
                {
                    model.personFlag = ds.Tables[0].Rows[0]["personFlag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["levelFlag"] != null&&ds.Tables[0].Rows[0]["levelFlag"].ToString()!="")
                {
                    model.levelFlag = ds.Tables[0].Rows[0]["levelFlag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["categoryFlag"] != null && ds.Tables[0].Rows[0]["categoryFlag"].ToString() != "")
                {
                    model.categoryFlag = ds.Tables[0].Rows[0]["categoryFlag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["categorypath"] != null && ds.Tables[0].Rows[0]["categorypath"].ToString()!="")
                {
                    model.categorypath = ds.Tables[0].Rows[0]["categorypath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ifCourseQuestion"] != null && ds.Tables[0].Rows[0]["ifCourseQuestion"].ToString() != "")
                {
                    model.ifCourseQuestion = int.Parse(ds.Tables[0].Rows[0]["ifCourseQuestion"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createTime"] != null && ds.Tables[0].Rows[0]["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(ds.Tables[0].Rows[0]["createTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["createPerson"] != null&&ds.Tables[0].Rows[0]["createPerson"].ToString()!="")
                {
                    model.createPerson = ds.Tables[0].Rows[0]["createPerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["shFlag"] != null && ds.Tables[0].Rows[0]["shFlag"].ToString() != "")
                {
                    model.shFlag = int.Parse(ds.Tables[0].Rows[0]["shFlag"].ToString());
                } 
                if (ds.Tables[0].Rows[0]["orderBy"] != null && ds.Tables[0].Rows[0]["orderBy"].ToString() != "")
                {
                    model.orderBy = int.Parse(ds.Tables[0].Rows[0]["orderBy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["courseId"] != null && ds.Tables[0].Rows[0]["courseId"].ToString() != "")
                {
                    model.courseId = ds.Tables[0].Rows[0]["courseId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isSubQuestion"] != null && ds.Tables[0].Rows[0]["isSubQuestion"].ToString() != "")
                {
                    model.isSubQuestion = int.Parse(ds.Tables[0].Rows[0]["isSubQuestion"].ToString());
                }
                if (ds.Tables[0].Rows[0]["mainQuestionSysnumber"] != null && ds.Tables[0].Rows[0]["mainQuestionSysnumber"].ToString() != "")
                {
                    model.mainQuestionSysnumber = ds.Tables[0].Rows[0]["mainQuestionSysnumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LevelPoint"] != null && ds.Tables[0].Rows[0]["LevelPoint"].ToString() != "")
                {
                    model.LevelPoint = decimal.Parse(ds.Tables[0].Rows[0]["LevelPoint"].ToString());
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
            strSql.Append("select * ");
            strSql.Append(" FROM T_Test_Queston ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adohelper.ExecuteSqlDataset(strSql.ToString());
        }

        #endregion  Method
    }
}
