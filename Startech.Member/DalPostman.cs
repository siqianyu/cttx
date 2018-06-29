using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace Startech.Member
{
    /// <summary>
    /// 数据访问类:Postman
    /// </summary>
    public class DalPostman
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        public DalPostman()
        { }
        #region  BasicMethod

     

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModPostman model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Postman_Info(");
            strSql.Append("postman_id,postman_username,postman_pwd,postman_trueName,Postman_headImg,postman_tel,postman_marketId,postman_deliverBuildingId,postman_score,postman_status,postman_addtime)");
            strSql.Append(" values (");
            strSql.Append("@postman_id,@postman_username,@postman_pwd,@postman_trueName,@Postman_headImg,@postman_tel,@postman_marketId,@postman_deliverBuildingId,@postman_score,@postman_status,@postman_addtime)");
            SqlParameter[] parameters = {
					new SqlParameter("@postman_id", SqlDbType.Int,4),
					new SqlParameter("@postman_username", SqlDbType.NVarChar,50),
					new SqlParameter("@postman_pwd", SqlDbType.VarChar,50),
					new SqlParameter("@postman_trueName", SqlDbType.NVarChar,10),
					new SqlParameter("@Postman_headImg", SqlDbType.VarChar,100),
					new SqlParameter("@postman_tel", SqlDbType.VarChar,25),
					new SqlParameter("@postman_marketId", SqlDbType.VarChar,25),
					new SqlParameter("@postman_deliverBuildingId", SqlDbType.VarChar,150),
					new SqlParameter("@postman_score", SqlDbType.Int,4),
					new SqlParameter("@postman_status", SqlDbType.NVarChar,10),
					new SqlParameter("@postman_addtime", SqlDbType.DateTime)};
            parameters[0].Value = model.postman_id;
            parameters[1].Value = model.postman_username;
            parameters[2].Value = model.postman_pwd;
            parameters[3].Value = model.postman_trueName;
            parameters[4].Value = model.Postman_headImg;
            parameters[5].Value = model.postman_tel;
            parameters[6].Value = model.postman_marketId;
            parameters[7].Value = model.postman_deliverBuildingId;
            parameters[8].Value = model.postman_score;
            parameters[9].Value = model.postman_status;
            parameters[10].Value = model.postman_addtime;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModPostman model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Postman_Info set ");
            strSql.Append("postman_username=@postman_username,");
            strSql.Append("postman_pwd=@postman_pwd,");
            strSql.Append("postman_trueName=@postman_trueName,");
            strSql.Append("Postman_headImg=@Postman_headImg,");
            strSql.Append("postman_tel=@postman_tel,");
            strSql.Append("postman_marketId=@postman_marketId,");
            strSql.Append("postman_deliverBuildingId=@postman_deliverBuildingId,");
            strSql.Append("postman_score=@postman_score,");
            strSql.Append("postman_status=@postman_status,");
            strSql.Append("postman_addtime=@postman_addtime");
            strSql.Append(" where postman_id=@postman_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@postman_username", SqlDbType.NVarChar,50),
					new SqlParameter("@postman_pwd", SqlDbType.VarChar,50),
					new SqlParameter("@postman_trueName", SqlDbType.NVarChar,10),
					new SqlParameter("@Postman_headImg", SqlDbType.VarChar,100),
					new SqlParameter("@postman_tel", SqlDbType.VarChar,25),
					new SqlParameter("@postman_marketId", SqlDbType.VarChar,25),
					new SqlParameter("@postman_deliverBuildingId", SqlDbType.VarChar,150),
					new SqlParameter("@postman_score", SqlDbType.Int,4),
					new SqlParameter("@postman_status", SqlDbType.NVarChar,10),
					new SqlParameter("@postman_addtime", SqlDbType.DateTime),
					new SqlParameter("@postman_id", SqlDbType.Int,4)};
            parameters[0].Value = model.postman_username;
            parameters[1].Value = model.postman_pwd;
            parameters[2].Value = model.postman_trueName;
            parameters[3].Value = model.Postman_headImg;
            parameters[4].Value = model.postman_tel;
            parameters[5].Value = model.postman_marketId;
            parameters[6].Value = model.postman_deliverBuildingId;
            parameters[7].Value = model.postman_score;
            parameters[8].Value = model.postman_status;
            parameters[9].Value = model.postman_addtime;
            parameters[10].Value = model.postman_id;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int postman_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Postman_Info ");
            strSql.Append(" where postman_id=@postman_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@postman_id", SqlDbType.Int,4)			};
            parameters[0].Value = postman_id;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
       
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModPostman GetModel(int postman_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 postman_id,postman_username,postman_pwd,postman_trueName,Postman_headImg,postman_tel,postman_marketId,postman_deliverBuildingId,postman_score,postman_status,postman_addtime from T_Postman_Info ");
            strSql.Append(" where postman_id=@postman_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@postman_id", SqlDbType.Int,4)			};
            parameters[0].Value = postman_id;

            ModPostman model = new ModPostman();
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
        public ModPostman DataRowToModel(DataRow row)
        {
            ModPostman model = new ModPostman();
            if (row != null)
            {
                if (row["postman_id"] != null && row["postman_id"].ToString() != "")
                {
                    model.postman_id = int.Parse(row["postman_id"].ToString());
                }
                if (row["postman_username"] != null)
                {
                    model.postman_username = row["postman_username"].ToString();
                }
                if (row["postman_pwd"] != null)
                {
                    model.postman_pwd = row["postman_pwd"].ToString();
                }
                if (row["postman_trueName"] != null)
                {
                    model.postman_trueName = row["postman_trueName"].ToString();
                }
                if (row["Postman_headImg"] != null)
                {
                    model.Postman_headImg = row["Postman_headImg"].ToString();
                }
                if (row["postman_tel"] != null)
                {
                    model.postman_tel = row["postman_tel"].ToString();
                }
                if (row["postman_marketId"] != null)
                {
                    model.postman_marketId = row["postman_marketId"].ToString();
                }
                if (row["postman_deliverBuildingId"] != null)
                {
                    model.postman_deliverBuildingId = row["postman_deliverBuildingId"].ToString();
                }
                if (row["postman_score"] != null && row["postman_score"].ToString() != "")
                {
                    model.postman_score = int.Parse(row["postman_score"].ToString());
                }
                if (row["postman_status"] != null)
                {
                    model.postman_status = row["postman_status"].ToString();
                }
                if (row["postman_addtime"] != null && row["postman_addtime"].ToString() != "")
                {
                    model.postman_addtime = DateTime.Parse(row["postman_addtime"].ToString());
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
            strSql.Append("select postman_id,postman_username,postman_pwd,postman_trueName,Postman_headImg,postman_tel,postman_marketId,postman_deliverBuildingId,postman_score,postman_status,postman_addtime ");
            strSql.Append(" FROM T_Postman_Info ");
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
            strSql.Append(" postman_id,postman_username,postman_pwd,postman_trueName,Postman_headImg,postman_tel,postman_marketId,postman_deliverBuildingId,postman_score,postman_status,postman_addtime ");
            strSql.Append(" FROM T_Postman_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }

     
        


        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
