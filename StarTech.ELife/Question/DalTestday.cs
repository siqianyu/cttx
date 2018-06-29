using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace StarTech.ELife.Question
{
    /// <summary>
    /// 数据访问类:DalTestday
    /// </summary>
    public partial class DalTestday
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DBInstance");
        public DalTestday()
        { }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModelTestday model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Test_day(");
            strSql.Append("Sysnumber,Title,Remarks,Questions,Addtime,type,chapterId,courseType,personFlag,levelFlag,addMemberId,shFlag,startime,endtime,ZjMethod)");
            strSql.Append(" values (");
            strSql.Append("@Sysnumber,@Title,@Remarks,@Questions,@Addtime,@type,@chapterId,@courseType,@personFlag,@levelFlag,@addMemberId,@shFlag,@startime,@endtime,@ZjMethod)");
            SqlParameter[] parameters = {
					new SqlParameter("@Sysnumber", SqlDbType.NVarChar,255),
					new SqlParameter("@Title", SqlDbType.NVarChar,255),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,255),
					new SqlParameter("@Questions", SqlDbType.VarChar,8000),
					new SqlParameter("@Addtime", SqlDbType.DateTime),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@chapterId", SqlDbType.Int,4),
					new SqlParameter("@courseType", SqlDbType.VarChar,50),
					new SqlParameter("@personFlag", SqlDbType.VarChar,50),
					new SqlParameter("@levelFlag", SqlDbType.VarChar,50),
					new SqlParameter("@addMemberId", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.VarChar,50),
					new SqlParameter("@startime", SqlDbType.DateTime),
					new SqlParameter("@endtime", SqlDbType.DateTime),
                    new SqlParameter("@ZjMethod", SqlDbType.VarChar,50)
            
            };
            parameters[0].Value = model.Sysnumber;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Remarks;
            parameters[3].Value = model.Questions;
            parameters[4].Value = model.Addtime;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.chapterId;
            parameters[7].Value = model.courseType;
            parameters[8].Value = model.personFlag;
            parameters[9].Value = model.levelFlag;
            parameters[10].Value = model.addMemberId;
            parameters[11].Value = model.shFlag;
            parameters[12].Value = model.startime;
            parameters[13].Value = model.endtime;
            parameters[14].Value = model.ZjMethod;



            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ModelTestday model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Test_day set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Remarks=@Remarks,");
            strSql.Append("Questions=@Questions,");
            strSql.Append("Addtime=@Addtime,");
            strSql.Append("type=@type,");
            strSql.Append("chapterId=@chapterId,");
            strSql.Append("courseType=@courseType,");
            strSql.Append("personFlag=@personFlag,");
            strSql.Append("levelFlag=@levelFlag,");
            strSql.Append("addMemberId=@addMemberId,");
            strSql.Append("shFlag=@shFlag,");
            strSql.Append("startime=@startime,");
            strSql.Append("endtime=@endtime,ZjMethod=@ZjMethod");
            strSql.Append(" where Sysnumber=@Sysnumber ");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,255),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,255),
					new SqlParameter("@Questions", SqlDbType.VarChar,8000),
					new SqlParameter("@Addtime", SqlDbType.DateTime),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@chapterId", SqlDbType.Int,4),
					new SqlParameter("@courseType", SqlDbType.VarChar,50),
					new SqlParameter("@personFlag", SqlDbType.VarChar,50),
					new SqlParameter("@levelFlag", SqlDbType.VarChar,50),
					new SqlParameter("@addMemberId", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.VarChar,50),
					new SqlParameter("@startime", SqlDbType.DateTime),
					new SqlParameter("@endtime", SqlDbType.DateTime),
					new SqlParameter("@Sysnumber", SqlDbType.NVarChar,255),
                    new SqlParameter("@ZjMethod", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Remarks;
            parameters[2].Value = model.Questions;
            parameters[3].Value = model.Addtime;
            parameters[4].Value = model.Type;
            parameters[5].Value = model.chapterId;
            parameters[6].Value = model.courseType;
            parameters[7].Value = model.personFlag;
            parameters[8].Value = model.levelFlag;
            parameters[9].Value = model.addMemberId;
            parameters[10].Value = model.shFlag;
            parameters[11].Value = model.startime;
            parameters[12].Value = model.endtime;
            parameters[13].Value = model.Sysnumber;
            parameters[14].Value = model.ZjMethod;

            int r = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string Sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Test_day ");
            strSql.Append(" where Sysnumbers=@Sysnumbers");
            SqlParameter[] parameters = {
					new SqlParameter("@Sysnumber", SqlDbType.VarChar,255)};
            parameters[0].Value = Sysnumber;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string Sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Test_day ");
            strSql.Append(" where Sysnumbers in(@Sysnumbers)");
            SqlParameter[] parameters = {
					new SqlParameter("@Sysnumbers", SqlDbType.VarChar,255)};
            parameters[0].Value = Sysnumber;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModelTestday GetModel(string Sysnumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Test_day ");
            strSql.Append(" where Sysnumber=@Sysnumber");
            SqlParameter[] parameters = {
					new SqlParameter("@Sysnumber", SqlDbType.VarChar,255)};
            parameters[0].Value = Sysnumber;

            ModelTestday model = new ModelTestday();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Sysnumber"] != null && ds.Tables[0].Rows[0]["Sysnumber"].ToString() != "")
                {
                    model.Sysnumber = ds.Tables[0].Rows[0]["Sysnumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remarks"] != null && ds.Tables[0].Rows[0]["Remarks"].ToString() != "")
                {
                    model.Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Questions"] != null && ds.Tables[0].Rows[0]["Questions"].ToString() != "")
                {
                    model.Questions = ds.Tables[0].Rows[0]["Questions"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Addtime"] != null && ds.Tables[0].Rows[0]["Addtime"].ToString() != "")
                {
                    model.Addtime = DateTime.Parse(ds.Tables[0].Rows[0]["Addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.Type = Convert.ToInt32(ds.Tables[0].Rows[0]["type"]);
                }
                if (ds.Tables[0].Rows[0]["chapterId"] != null && ds.Tables[0].Rows[0]["chapterId"].ToString() != "")
                {
                    model.chapterId = Convert.ToInt32(ds.Tables[0].Rows[0]["chapterId"]);
                }
                model.courseType = ds.Tables[0].Rows[0]["courseType"].ToString();
                model.personFlag = ds.Tables[0].Rows[0]["personFlag"].ToString();
                model.levelFlag = ds.Tables[0].Rows[0]["levelFlag"].ToString();
                model.addMemberId = ds.Tables[0].Rows[0]["addMemberId"].ToString();
                model.shFlag = ds.Tables[0].Rows[0]["shFlag"].ToString();
                if (ds.Tables[0].Rows[0]["startime"] != null && ds.Tables[0].Rows[0]["startime"].ToString() != "")
                {
                    model.startime = DateTime.Parse(ds.Tables[0].Rows[0]["startime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endtime"] != null && ds.Tables[0].Rows[0]["endtime"].ToString() != "")
                {
                    model.endtime = DateTime.Parse(ds.Tables[0].Rows[0]["endtime"].ToString());
                }
                model.ZjMethod = ds.Tables[0].Rows[0]["ZjMethod"].ToString();

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
            strSql.Append(" FROM T_Test_day ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

        #endregion  Method
    }
}
