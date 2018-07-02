/**  版本信息模板在安装目录下，可自行修改。
* ShortMessage_Log.cs
*
* 功 能： N/A
* 类 名： ShortMessage_Log
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-25 21:38:53   N/A    初版
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
    /// 数据访问类:ShortMessage_Log
    /// </summary>
    public partial class ShortMsgDal
    {
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        public ShortMsgDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ShortMsgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ShortMessage_Log(");
            strSql.Append("sysnumber,tel,yzm,statu,sendTime,outSendTime,remark,template,smsSign,result,sendText,supplier)");
            strSql.Append(" values (");
            strSql.Append("@sysnumber,@tel,@yzm,@statu,@sendTime,@outSendTime,@remark,@template,@smsSign,@result,@sendText,@supplier)");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@yzm", SqlDbType.VarChar,10),
					new SqlParameter("@statu", SqlDbType.VarChar,20),
					new SqlParameter("@sendTime", SqlDbType.DateTime),
					new SqlParameter("@outSendTime", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.VarChar,500),
					new SqlParameter("@template", SqlDbType.VarChar,50),
					new SqlParameter("@smsSign", SqlDbType.NVarChar,100),
                    new SqlParameter("@result", SqlDbType.NVarChar,200),
                    new SqlParameter("@sendText", SqlDbType.NText),
					new SqlParameter("@supplier", SqlDbType.VarChar,50)	
                                        };
            parameters[0].Value = model.sysnumber;
            parameters[1].Value = model.tel;
            parameters[2].Value = model.yzm;
            parameters[3].Value = model.statu;
            parameters[4].Value = model.sendTime;
            parameters[5].Value = model.outSendTime;
            parameters[6].Value = model.remark;
            parameters[7].Value = model.template;
            parameters[8].Value = model.smsSign;
            parameters[9].Value = model.result;
            parameters[10].Value = model.sendText;
            parameters[11].Value = model.supplier;
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
        public bool Update(ShortMsgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ShortMessage_Log set ");
            strSql.Append("tel=@tel,");
            strSql.Append("yzm=@yzm,");
            strSql.Append("statu=@statu,");
            strSql.Append("sendTime=@sendTime,");
            strSql.Append("outSendTime=@outSendTime,");
            strSql.Append("remark=@remark,");
            strSql.Append("template=@template,");
            strSql.Append("smsSign=@smsSign,");
            strSql.Append("result=@result,");
            strSql.Append("sendText=@sendText,");
            strSql.Append("supplier=@supplier");
            strSql.Append(" where sysnumber=@sysnumber");
            SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@yzm", SqlDbType.VarChar,10),
					new SqlParameter("@statu", SqlDbType.VarChar,20),
					new SqlParameter("@sendTime", SqlDbType.DateTime),
					new SqlParameter("@outSendTime", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.VarChar,500),
					new SqlParameter("@template", SqlDbType.VarChar,50),
					new SqlParameter("@smsSign", SqlDbType.NVarChar,100),
                    new SqlParameter("@result", SqlDbType.NVarChar,200),
                    new SqlParameter("@sendText", SqlDbType.NText),
					new SqlParameter("@supplier", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.sysnumber;
            parameters[1].Value = model.tel;
            parameters[2].Value = model.yzm;
            parameters[3].Value = model.statu;
            parameters[4].Value = model.sendTime;
            parameters[5].Value = model.outSendTime;
            parameters[6].Value = model.remark;
            parameters[7].Value = model.template;
            parameters[8].Value = model.smsSign;
            parameters[9].Value = model.result;
            parameters[10].Value = model.sendText;
            parameters[11].Value = model.supplier;
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
        public bool Delete(string sysnumber)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_ShortMessage_Log ");
            strSql.Append(" where sysnumber=@sysnumber");
            SqlParameter[] parameters = {
                          new SqlParameter("@sysnumber",SqlDbType.VarChar,60)
			};
            parameters[0].Value = sysnumber;
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
        public ShortMsgModel GetModel(string sysnumber)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 sysnumber,tel,yzm,statu,sendTime,outSendTime,remark,template,smsSign,result,sendText,supplier from T_ShortMessage_Log ");
            strSql.Append(" where sysnumber=@sysnumber");
            SqlParameter[] parameters = {
                          new SqlParameter("@sysnumber",SqlDbType.VarChar,60)
			};
            parameters[0].Value = sysnumber;
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
        public ShortMsgModel DataRowToModel(DataRow row)
        {
            ShortMsgModel model = new ShortMsgModel();
            if (row != null)
            {
                if (row["sysnumber"] != null)
                {
                    model.sysnumber = row["sysnumber"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.tel = row["tel"].ToString();
                }
                if (row["yzm"] != null)
                {
                    model.yzm = row["yzm"].ToString();
                }
                if (row["statu"] != null)
                {
                    model.statu = row["statu"].ToString();
                }
                if (row["sendTime"] != null && row["sendTime"].ToString() != "")
                {
                    model.sendTime = DateTime.Parse(row["sendTime"].ToString());
                }
                if (row["outSendTime"] != null && row["outSendTime"].ToString() != "")
                {
                    model.outSendTime = DateTime.Parse(row["outSendTime"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["template"] != null)
                {
                    model.template = row["template"].ToString();
                }
                if (row["smsSign"] != null)
                {
                    model.smsSign = row["smsSign"].ToString();
                }
                if (row["result"] != null)
                {
                    model.result = row["result"].ToString();
                }
                if (row["sendText"] != null)
                {
                    model.sendText = row["sendText"].ToString();
                }
                if (row["supplier"] != null)
                {
                    model.supplier = row["supplier"].ToString();
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
            strSql.Append("select sysnumber,tel,yzm,statu,sendTime,outSendTime,remark,template,smsSign,result,sendText,supplier ");
            strSql.Append(" FROM T_ShortMessage_Log ");
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
            strSql.Append(" sysnumber,tel,yzm,statu,sendTime,outSendTime,remark,template,smsSign,result,sendText,supplier ");
            strSql.Append(" FROM T_ShortMessage_Log ");
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
            strSql.Append("select count(1) FROM T_ShortMessage_Log ");
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
            strSql.Append(")AS Row, T.*  from T_ShortMessage_Log T ");
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
            parameters[0].Value = "T_ShortMessage_Log";
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

