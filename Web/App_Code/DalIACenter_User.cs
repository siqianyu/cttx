using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;


namespace StarTech.IACenter
{
    public class DalIACenter_User
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModIACenter_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into IACenter_User(");
            strSql.Append("userName,password,trueName,userType,isUse,isSuperAdmin,addTime,sex,age,tel,mobile,departId,orderBy)");
            strSql.Append(" values (");
            strSql.Append("@userName,@password,@trueName,@userType,@isUse,@isSuperAdmin,@addTime,@sex,@age,@tel,@mobile,@departId,@orderBy)");
            SqlParameter[] parameters = {
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,50),
					new SqlParameter("@trueName", SqlDbType.VarChar,50),
					new SqlParameter("@userType", SqlDbType.Int,4),
					new SqlParameter("@isUse", SqlDbType.Int,4),
					new SqlParameter("@isSuperAdmin", SqlDbType.Int,4),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@sex", SqlDbType.VarChar,50),
					new SqlParameter("@age", SqlDbType.Int,4),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@mobile", SqlDbType.VarChar,50),
					new SqlParameter("@departId", SqlDbType.Int,4),
					new SqlParameter("@orderBy", SqlDbType.Int,4)};
            parameters[0].Value = model.userName;
            parameters[1].Value = model.password;
            parameters[2].Value = model.trueName;
            parameters[3].Value = model.userType;
            parameters[4].Value = model.isUse;
            parameters[5].Value = model.isSuperAdmin;
            parameters[6].Value = model.addTime;
            parameters[7].Value = model.sex;
            parameters[8].Value = model.age;
            parameters[9].Value = model.tel;
            parameters[10].Value = model.mobile;
            parameters[11].Value = model.departId;
            parameters[12].Value = model.orderBy;

            return ado.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModIACenter_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IACenter_User set ");
            strSql.Append("userName=@userName,");
            strSql.Append("password=@password,");
            strSql.Append("trueName=@trueName,");
            strSql.Append("userType=@userType,");
            strSql.Append("isUse=@isUse,");
            strSql.Append("isSuperAdmin=@isSuperAdmin,");
            strSql.Append("addTime=@addTime,");
            strSql.Append("sex=@sex,");
            strSql.Append("age=@age,");
            strSql.Append("tel=@tel,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("departId=@departId,");
            strSql.Append("orderBy=@orderBy");
            strSql.Append(" where uniqueId=@uniqueId ");
            SqlParameter[] parameters = {
					new SqlParameter("@uniqueId", SqlDbType.Int,4),
					new SqlParameter("@userName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,50),
					new SqlParameter("@trueName", SqlDbType.VarChar,50),
					new SqlParameter("@userType", SqlDbType.Int,4),
					new SqlParameter("@isUse", SqlDbType.Int,4),
					new SqlParameter("@isSuperAdmin", SqlDbType.Int,4),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@sex", SqlDbType.VarChar,50),
					new SqlParameter("@age", SqlDbType.Int,4),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@mobile", SqlDbType.VarChar,50),
					new SqlParameter("@departId", SqlDbType.Int,4),
					new SqlParameter("@orderBy", SqlDbType.Int,4)};
            parameters[0].Value = model.uniqueId;
            parameters[1].Value = model.userName;
            parameters[2].Value = model.password;
            parameters[3].Value = model.trueName;
            parameters[4].Value = model.userType;
            parameters[5].Value = model.isUse;
            parameters[6].Value = model.isSuperAdmin;
            parameters[7].Value = model.addTime;
            parameters[8].Value = model.sex;
            parameters[9].Value = model.age;
            parameters[10].Value = model.tel;
            parameters[11].Value = model.mobile;
            parameters[12].Value = model.departId;
            parameters[13].Value = model.orderBy;

            return ado.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int uniqueId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete IACenter_User ");
            strSql.Append(" where uniqueId=@uniqueId ");
            SqlParameter[] parameters = {
					new SqlParameter("@uniqueId", SqlDbType.Int,4)};
            parameters[0].Value = uniqueId;

            return ado.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModIACenter_User GetModel(int uniqueId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from IACenter_User ");
            strSql.Append(" where uniqueId=@uniqueId ");
            SqlParameter[] parameters = {
					new SqlParameter("@uniqueId", SqlDbType.Int,4)};
            parameters[0].Value = uniqueId;

            ModIACenter_User model = new ModIACenter_User();
            DataSet ds = ado.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["uniqueId"].ToString() != "")
                {
                    model.uniqueId = int.Parse(ds.Tables[0].Rows[0]["uniqueId"].ToString());
                }
                model.userName = ds.Tables[0].Rows[0]["userName"].ToString();
                model.password = ds.Tables[0].Rows[0]["password"].ToString();
                model.trueName = ds.Tables[0].Rows[0]["trueName"].ToString();
                if (ds.Tables[0].Rows[0]["userType"].ToString() != "")
                {
                    model.userType = int.Parse(ds.Tables[0].Rows[0]["userType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isUse"].ToString() != "")
                {
                    model.isUse = int.Parse(ds.Tables[0].Rows[0]["isUse"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isSuperAdmin"].ToString() != "")
                {
                    model.isSuperAdmin = int.Parse(ds.Tables[0].Rows[0]["isSuperAdmin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addTime"].ToString() != "")
                {
                    model.addTime = DateTime.Parse(ds.Tables[0].Rows[0]["addTime"].ToString());
                }
                model.sex = ds.Tables[0].Rows[0]["sex"].ToString();
                if (ds.Tables[0].Rows[0]["age"].ToString() != "")
                {
                    model.age = int.Parse(ds.Tables[0].Rows[0]["age"].ToString());
                }
                model.tel = ds.Tables[0].Rows[0]["tel"].ToString();
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                if (ds.Tables[0].Rows[0]["departId"].ToString() != "")
                {
                    model.departId = int.Parse(ds.Tables[0].Rows[0]["departId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["orderBy"].ToString() != "")
                {
                    model.orderBy = int.Parse(ds.Tables[0].Rows[0]["orderBy"].ToString());
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
            strSql.Append(" FROM IACenter_User ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ado.ExecuteSqlDataset(strSql.ToString());
        }
        #endregion  成员方法
    
    }
}
