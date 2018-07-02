/**  版本信息模板在安装目录下，可自行修改。
* ShortMessage_Template.cs
*
* 功 能： N/A
* 类 名： ShortMessage_Template
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-26 17:33:07   N/A    初版
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
namespace StarTech.ELife.ShortMsg
{
    /// <summary>
    /// 数据访问类:ShortMessage_Template
    /// </summary>
    public partial class ShortMsgTempDal
    {

        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        public ShortMsgTempDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ShortMsgTempModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ShortMessage_Template(");
            strSql.Append("templateId,flag,templateCode,templateParam,supplierId,supplierFlag,isUse)");
            strSql.Append(" values (");
            strSql.Append("@templateId,@flag,@templateCode,@templateParam,@supplierId,@supplierFlag,@isUse)");
            SqlParameter[] parameters = {
					new SqlParameter("@templateId", SqlDbType.VarChar,50),
					new SqlParameter("@flag", SqlDbType.NVarChar,50),
					new SqlParameter("@templateCode", SqlDbType.NVarChar,50),
					new SqlParameter("@templateParam", SqlDbType.NText),
					new SqlParameter("@supplierId", SqlDbType.VarChar,50),
					new SqlParameter("@supplierFlag", SqlDbType.VarChar,50),
					new SqlParameter("@isUse", SqlDbType.Int,4)};
            parameters[0].Value = model.templateId;
            parameters[1].Value = model.flag;
            parameters[2].Value = model.templateCode;
            parameters[3].Value = model.templateParam;
            parameters[4].Value = model.supplierId;
            parameters[5].Value = model.supplierFlag;
            parameters[6].Value = model.isUse;

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
        public bool Update(ShortMsgTempModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ShortMessage_Template set ");
            strSql.Append("flag=@flag,");
            strSql.Append("templateCode=@templateCode,");
            strSql.Append("templateParam=@templateParam,");
            strSql.Append("supplierId=@supplierId,");
            strSql.Append("supplierFlag=@supplierFlag,");
            strSql.Append("isUse=@isUse");
            strSql.Append(" where templateId=@templateId");
            SqlParameter[] parameters = {
					new SqlParameter("@templateId", SqlDbType.VarChar,50),
					new SqlParameter("@flag", SqlDbType.NVarChar,50),
					new SqlParameter("@templateCode", SqlDbType.NVarChar,50),
					new SqlParameter("@templateParam", SqlDbType.NText),
					new SqlParameter("@supplierId", SqlDbType.VarChar,50),
					new SqlParameter("@supplierFlag", SqlDbType.VarChar,50),
					new SqlParameter("@isUse", SqlDbType.Int,4)};
            parameters[0].Value = model.templateId;
            parameters[1].Value = model.flag;
            parameters[2].Value = model.templateCode;
            parameters[3].Value = model.templateParam;
            parameters[4].Value = model.supplierId;
            parameters[5].Value = model.supplierFlag;
            parameters[6].Value = model.isUse;

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
        public bool Delete(string templateId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_ShortMessage_Template ");
            strSql.Append(" where  templateId=@templateId");
            SqlParameter[] parameters = {
                        new SqlParameter("@templateId",SqlDbType.VarChar,50)
			};
            parameters[0].Value = templateId;
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
        public ShortMsgTempModel GetModel(string templateId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 templateId,flag,templateCode,templateParam,supplierId,supplierFlag,isUse from T_ShortMessage_Template ");
            strSql.Append(" where  templateId=@templateId");
            SqlParameter[] parameters = {
                        new SqlParameter("@templateId",SqlDbType.VarChar,50)
			};
            parameters[0].Value = templateId;
            ShortMsgTempModel model = new ShortMsgTempModel();
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
        public ShortMsgTempModel DataRowToModel(DataRow row)
        {
            ShortMsgTempModel model = new ShortMsgTempModel();
            if (row != null)
            {
                if (row["templateId"] != null)
                {
                    model.templateId = row["templateId"].ToString();
                }
                if (row["flag"] != null)
                {
                    model.flag = row["flag"].ToString();
                }
                if (row["templateCode"] != null)
                {
                    model.templateCode = row["templateCode"].ToString();
                }
                if (row["templateParam"] != null)
                {
                    model.templateParam = row["templateParam"].ToString();
                }
                if (row["supplierId"] != null)
                {
                    model.supplierId = row["supplierId"].ToString();
                }
                if (row["supplierFlag"] != null)
                {
                    model.supplierFlag = row["supplierFlag"].ToString();
                }
                if (row["isUse"] != null && row["isUse"].ToString() != "")
                {
                    model.isUse = int.Parse(row["isUse"].ToString());
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
            strSql.Append("select templateId,flag,templateCode,templateParam,supplierId,supplierFlag,isUse ");
            strSql.Append(" FROM T_ShortMessage_Template ");
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
            strSql.Append(" templateId,flag,templateCode,templateParam,supplierId,supplierFlag,isUse ");
            strSql.Append(" FROM T_ShortMessage_Template ");
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
            strSql.Append("select count(1) FROM T_ShortMessage_Template ");
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
            strSql.Append(")AS Row, T.*  from T_ShortMessage_Template T ");
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
            parameters[0].Value = "T_ShortMessage_Template";
            parameters[1].Value = "";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return adoHelper.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

