using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;

namespace Startech.Member
{
    public class DalMoneyApply
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        public DalMoneyApply()
        {

        }

        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ModMoneyApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Shop_MoneyApply(");
            strSql.Append("shopid,marketid,apply_name,apply_money,apply_ispost,apply_posttime,apply_postcheck,apply_postchecktime,apply_isback,apply_backtime,apply_backcheck,apply_backchecktime,apply_mark)");
            strSql.Append(" values (");
            strSql.Append("@shopid,@marketid,@apply_name,@apply_money,@apply_ispost,@apply_posttime,@apply_postcheck,@apply_postchecktime,@apply_isback,@apply_backtime,@apply_backcheck,@apply_backchecktime,@apply_mark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@shopid", SqlDbType.VarChar,50),
					new SqlParameter("@marketid", SqlDbType.VarChar,50),
					new SqlParameter("@apply_name", SqlDbType.VarChar,50),
					new SqlParameter("@apply_money", SqlDbType.Decimal,9),
					new SqlParameter("@apply_ispost", SqlDbType.VarChar,50),
					new SqlParameter("@apply_posttime", SqlDbType.DateTime),
					new SqlParameter("@apply_postcheck", SqlDbType.VarChar,50),
					new SqlParameter("@apply_postchecktime", SqlDbType.DateTime),
					new SqlParameter("@apply_isback", SqlDbType.VarChar,50),
					new SqlParameter("@apply_backtime", SqlDbType.DateTime),
					new SqlParameter("@apply_backcheck", SqlDbType.VarChar,50),
					new SqlParameter("@apply_backchecktime", SqlDbType.DateTime),
					new SqlParameter("@apply_mark", SqlDbType.VarChar,500)};
            parameters[0].Value = model.shopid;
            parameters[1].Value = model.marketid;
            parameters[2].Value = model.apply_name;
            parameters[3].Value = model.apply_money;
            parameters[4].Value = model.apply_ispost;
            parameters[5].Value = model.apply_posttime;
            parameters[6].Value = model.apply_postcheck;
            parameters[7].Value = model.apply_postchecktime;
            parameters[8].Value = model.apply_isback;
            parameters[9].Value = model.apply_backtime;
            parameters[10].Value = model.apply_backcheck;
            parameters[11].Value = model.apply_backchecktime;
            parameters[12].Value = model.apply_mark;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ModMoneyApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Shop_MoneyApply set ");
            strSql.Append("shopid=@shopid,");
            strSql.Append("marketid=@marketid,");
            strSql.Append("apply_name=@apply_name,");
            strSql.Append("apply_money=@apply_money,");
            strSql.Append("apply_ispost=@apply_ispost,");
            strSql.Append("apply_posttime=@apply_posttime,");
            strSql.Append("apply_postcheck=@apply_postcheck,");
            strSql.Append("apply_postchecktime=@apply_postchecktime,");
            strSql.Append("apply_isback=@apply_isback,");
            strSql.Append("apply_backtime=@apply_backtime,");
            strSql.Append("apply_backcheck=@apply_backcheck,");
            strSql.Append("apply_backchecktime=@apply_backchecktime,");
            strSql.Append("apply_mark=@apply_mark");
            strSql.Append(" where apply_id=@apply_id");
            SqlParameter[] parameters = {
					new SqlParameter("@shopid", SqlDbType.VarChar,50),
					new SqlParameter("@marketid", SqlDbType.VarChar,50),
					new SqlParameter("@apply_name", SqlDbType.VarChar,50),
					new SqlParameter("@apply_money", SqlDbType.Decimal,9),
					new SqlParameter("@apply_ispost", SqlDbType.VarChar,50),
					new SqlParameter("@apply_posttime", SqlDbType.DateTime),
					new SqlParameter("@apply_postcheck", SqlDbType.VarChar,50),
					new SqlParameter("@apply_postchecktime", SqlDbType.DateTime),
					new SqlParameter("@apply_isback", SqlDbType.VarChar,50),
					new SqlParameter("@apply_backtime", SqlDbType.DateTime),
					new SqlParameter("@apply_backcheck", SqlDbType.VarChar,50),
					new SqlParameter("@apply_backchecktime", SqlDbType.DateTime),
					new SqlParameter("@apply_mark", SqlDbType.VarChar,500),
					new SqlParameter("@apply_id", SqlDbType.Int,4)};
            parameters[0].Value = model.shopid;
            parameters[1].Value = model.marketid;
            parameters[2].Value = model.apply_name;
            parameters[3].Value = model.apply_money;
            parameters[4].Value = model.apply_ispost;
            parameters[5].Value = model.apply_posttime;
            parameters[6].Value = model.apply_postcheck;
            parameters[7].Value = model.apply_postchecktime;
            parameters[8].Value = model.apply_isback;
            parameters[9].Value = model.apply_backtime;
            parameters[10].Value = model.apply_backcheck;
            parameters[11].Value = model.apply_backchecktime;
            parameters[12].Value = model.apply_mark;
            parameters[13].Value = model.apply_id;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int apply_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Shop_MoneyApply ");
            strSql.Append(" where apply_id=@apply_id");
            SqlParameter[] parameters = {
					new SqlParameter("@apply_id", SqlDbType.Int,4)
			};
            parameters[0].Value = apply_id;

            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ModMoneyApply GetModel(int apply_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 apply_id,shopid,marketid,apply_name,apply_money,apply_ispost,apply_posttime,apply_postcheck,apply_postchecktime,apply_isback,apply_backtime,apply_backcheck,apply_backchecktime,apply_mark from T_Shop_MoneyApply ");
            strSql.Append(" where apply_id=@apply_id");
            SqlParameter[] parameters = {
					new SqlParameter("@apply_id", SqlDbType.Int,4)
			};
            parameters[0].Value = apply_id;

            ModMoneyApply model = new ModMoneyApply();
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
        public ModMoneyApply GetModel(string shopid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 apply_id,shopid,marketid,apply_name,apply_money,apply_ispost,apply_posttime,apply_postcheck,apply_postchecktime,apply_isback,apply_backtime,apply_backcheck,apply_backchecktime,apply_mark from T_Shop_MoneyApply ");
            strSql.Append(" where shopid=@shopid");
            SqlParameter[] parameters = {
					new SqlParameter("@shopid", SqlDbType.VarChar,50)
			};
            parameters[0].Value = shopid;

            ModMoneyApply model = new ModMoneyApply();
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
        public ModMoneyApply DataRowToModel(DataRow row)
        {
            ModMoneyApply model = new ModMoneyApply();
            if (row != null)
            {
                if (row["apply_id"] != null && row["apply_id"].ToString() != "")
                {
                    model.apply_id = int.Parse(row["apply_id"].ToString());
                }
                if (row["shopid"] != null)
                {
                    model.shopid = row["shopid"].ToString();
                }
                if (row["marketid"] != null)
                {
                    model.marketid = row["marketid"].ToString();
                }
                if (row["apply_name"] != null)
                {
                    model.apply_name = row["apply_name"].ToString();
                }
                if (row["apply_money"] != null && row["apply_money"].ToString() != "")
                {
                    model.apply_money = decimal.Parse(row["apply_money"].ToString());
                }
                if (row["apply_ispost"] != null)
                {
                    model.apply_ispost = row["apply_ispost"].ToString();
                }
                if (row["apply_posttime"] != null && row["apply_posttime"].ToString() != "")
                {
                    model.apply_posttime = DateTime.Parse(row["apply_posttime"].ToString());
                }
                if (row["apply_postcheck"] != null)
                {
                    model.apply_postcheck = row["apply_postcheck"].ToString();
                }
                if (row["apply_postchecktime"] != null && row["apply_postchecktime"].ToString() != "")
                {
                    model.apply_postchecktime = DateTime.Parse(row["apply_postchecktime"].ToString());
                }
                if (row["apply_isback"] != null)
                {
                    model.apply_isback = row["apply_isback"].ToString();
                }
                if (row["apply_backtime"] != null && row["apply_backtime"].ToString() != "")
                {
                    model.apply_backtime = DateTime.Parse(row["apply_backtime"].ToString());
                }
                if (row["apply_backcheck"] != null)
                {
                    model.apply_backcheck = row["apply_backcheck"].ToString();
                }
                if (row["apply_backchecktime"] != null && row["apply_backchecktime"].ToString() != "")
                {
                    model.apply_backchecktime = DateTime.Parse(row["apply_backchecktime"].ToString());
                }
                if (row["apply_mark"] != null)
                {
                    model.apply_mark = row["apply_mark"].ToString();
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
            strSql.Append("select apply_id,shopid,marketid,apply_name,apply_money,apply_ispost,apply_posttime,apply_postcheck,apply_postchecktime,apply_isback,apply_backtime,apply_backcheck,apply_backchecktime,apply_mark ");
            strSql.Append(" FROM T_Shop_MoneyApply ");
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
            strSql.Append(" apply_id,shopid,marketid,apply_name,apply_money,apply_ispost,apply_posttime,apply_postcheck,apply_postchecktime,apply_isback,apply_backtime,apply_backcheck,apply_backchecktime,apply_mark ");
            strSql.Append(" FROM T_Shop_MoneyApply ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
        }


        #endregion  BasicMethod
    }
}

