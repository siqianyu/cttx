/**  版本信息模板在安装目录下，可自行修改。
* T_Base_Flag.cs
*
* 功 能： N/A
* 类 名： T_Base_Flag
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-25 18:20:04   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using StarTech.DBUtility;//Please add references
using StarTech.ELife.Flag;
namespace StarTech.ELife.Flag
{
    /// <summary>
    /// 数据访问类:T_Base_Flag
    /// </summary>
    public partial class FlagDal
    {
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        public FlagDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FlagModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Base_Flag(");
            strSql.Append("flag_id,flag_name,if_use)");
            strSql.Append(" values (");
            strSql.Append("@flag_id,@flag_name,@if_use)");
            SqlParameter[] parameters = {
					new SqlParameter("@flag_id", SqlDbType.NVarChar,50),
					new SqlParameter("@flag_name", SqlDbType.NVarChar,100),
					new SqlParameter("@if_use", SqlDbType.Int,4)};
            parameters[0].Value = model.flag_id;
            parameters[1].Value = model.flag_name;
            parameters[2].Value = model.if_use;

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
        public bool Update(FlagModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Base_Flag set ");
            strSql.Append("flag_name=@flag_name");
            strSql.Append(" where flag_id=@flag_id");
            SqlParameter[] parameters = {
					new SqlParameter("@flag_id", SqlDbType.NVarChar,50),
					new SqlParameter("@flag_name", SqlDbType.NVarChar,100)
             };
            parameters[0].Value = model.flag_id;
            parameters[1].Value = model.flag_name;

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
        public bool Delete(string flag_id)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Base_Flag ");
            strSql.Append(" where flag_id=@flag_id");
            SqlParameter[] parameters = {
                new SqlParameter("@flag_id", SqlDbType.VarChar,50),
			};
            parameters[0].Value = flag_id;
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
        /// 得到一个对象实体
        /// </summary>
        public FlagModel GetModel(string flag_id)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 flag_id,flag_name,if_use from T_Base_Flag ");
            strSql.Append(" where flag_id=@flag_id");
            SqlParameter[] parameters = {
                new SqlParameter("@flag_id", SqlDbType.VarChar,50),
			};
            parameters[0].Value = flag_id;
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
        public FlagModel DataRowToModel(DataRow row)
        {
            FlagModel model = new FlagModel();
            if (row != null)
            {
                if (row["flag_id"] != null)
                {
                    model.flag_id = row["flag_id"].ToString();
                }
                if (row["flag_name"] != null)
                {
                    model.flag_name = row["flag_name"].ToString();
                }
                if (row["if_use"] != null && row["if_use"].ToString() != "")
                {
                    model.if_use = int.Parse(row["if_use"].ToString());
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
            strSql.Append("select flag_id,flag_name,if_use ");
            strSql.Append(" FROM T_Base_Flag ");
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
            strSql.Append(" flag_id,flag_name,if_use ");
            strSql.Append(" FROM T_Base_Flag ");
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
            strSql.Append("select count(1) FROM T_Base_Flag ");
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
                strSql.Append("order by T. desc");
            }
            strSql.Append(")AS Row, T.*  from T_Base_Flag T ");
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
            parameters[0].Value = "T_Base_Flag";
            parameters[1].Value = "";
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

