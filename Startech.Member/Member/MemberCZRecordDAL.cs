using System;
using System.Collections.Generic;
using System.Text;
using StarTech.DBUtility;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using StarTech.Util;


namespace Startech.Member.Member
{
    public class MemberCZRecordDAL
    {
        public MemberCZRecordDAL()
		{}
        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(MemberCZRecordModel model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into T_Member_AccountRecord(");
			strSql.Append("memberId,money,moneyType,remarks,addTime,addPerson,shFlag)");
			strSql.Append(" values (");
			strSql.Append("@memberId,@money,@moneyType,@remarks,@addTime,@addPerson,@shFlag)");
			SqlParameter[] parameters = {
					new SqlParameter("@memberId", SqlDbType.Int,4),
					new SqlParameter("@money", SqlDbType.Decimal,9),
					new SqlParameter("@moneyType", SqlDbType.VarChar,50),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@addPerson", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.Int,4)};
			parameters[0].Value = model.memberId;
			parameters[1].Value = model.money;
			parameters[2].Value = model.moneyType;
			parameters[3].Value = model.remarks;
			parameters[4].Value = model.addTime;
			parameters[5].Value = model.addPerson;
			parameters[6].Value = model.shFlag;

            object obj = adoHelper.ExecuteSqlScalar(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(MemberCZRecordModel model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update T_Member_AccountRecord set ");
			strSql.Append("memberId=@memberId,");
			strSql.Append("money=@money,");
			strSql.Append("moneyType=@moneyType,");
			strSql.Append("remarks=@remarks,");
			strSql.Append("addTime=@addTime,");
			strSql.Append("addPerson=@addPerson,");
			strSql.Append("shFlag=@shFlag,");
			strSql.Append("shPerson=@shPerson,");
			strSql.Append("shTime=@shTime");
			strSql.Append(" where sysnumber=@sysnumber ");
			SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@memberId", SqlDbType.Int,4),
					new SqlParameter("@money", SqlDbType.Decimal,9),
					new SqlParameter("@moneyType", SqlDbType.VarChar,50),
					new SqlParameter("@remarks", SqlDbType.VarChar,500),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@addPerson", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.Int,4),
					new SqlParameter("@shPerson", SqlDbType.VarChar,50),
					new SqlParameter("@shTime", SqlDbType.DateTime)};
			parameters[0].Value = model.sysnumber;
			parameters[1].Value = model.memberId;
			parameters[2].Value = model.money;
			parameters[3].Value = model.moneyType;
			parameters[4].Value = model.remarks;
			parameters[5].Value = model.addTime;
			parameters[6].Value = model.addPerson;
			parameters[7].Value = model.shFlag;
			parameters[8].Value = model.shPerson;
			parameters[9].Value = model.shTime;

            object obj = adoHelper.ExecuteSqlScalar(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string sysnumber)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete T_Member_AccountRecord ");
			strSql.Append(" where sysnumber=@sysnumber ");
			SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)};
			parameters[0].Value = sysnumber;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MemberCZRecordModel GetModel(string sysnumber)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * from T_Member_AccountRecord ");
			strSql.Append(" where sysnumber=@sysnumber ");
			SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50)};
			parameters[0].Value = sysnumber;

            MemberCZRecordModel model = new MemberCZRecordModel();
			DataSet ds=adoHelper.ExecuteSqlDataset(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.sysnumber=ds.Tables[0].Rows[0]["sysnumber"].ToString();
				if(ds.Tables[0].Rows[0]["memberId"].ToString()!="")
				{
					model.memberId=int.Parse(ds.Tables[0].Rows[0]["memberId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["money"].ToString()!="")
				{
					model.money=decimal.Parse(ds.Tables[0].Rows[0]["money"].ToString());
				}
				model.moneyType=ds.Tables[0].Rows[0]["moneyType"].ToString();
				model.remarks=ds.Tables[0].Rows[0]["remarks"].ToString();
                model.addPerson = ds.Tables[0].Rows[0]["addPerson"].ToString();
                if (ds.Tables[0].Rows[0]["addTime"].ToString() != "")
				{
                    model.addTime = DateTime.Parse(ds.Tables[0].Rows[0]["addTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["shFlag"].ToString()!="")
				{
					model.shFlag=int.Parse(ds.Tables[0].Rows[0]["shFlag"].ToString());
				}
				model.shPerson=ds.Tables[0].Rows[0]["shPerson"].ToString();
				if(ds.Tables[0].Rows[0]["shTime"].ToString()!="")
				{
					model.shTime=DateTime.Parse(ds.Tables[0].Rows[0]["shTime"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM T_Member_AccountRecord ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return adoHelper.ExecuteSqlDataset(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		*/
        public DataSet GetPageList(string fields, string filter, string sort, int currentPageIndex, int pageSize, out int recordCount)
        {
            return PaginationUtility.GetPaginationList(fields, "V_Member_MemberCZ", filter, sort, currentPageIndex, pageSize, out recordCount);
        }

		#endregion  成员方法

        #region 自定义方法
        //审核会员充值信息
        public int UpdateMemberCZ(string strid, string person, string time)
        {
            string strSql = "update T_Member_AccountRecord set shFlag=1,shPerson='" + person + "',shTime='" + time + "' where sysnumber='" + strid + "'";
            object obj = adoHelper.ExecuteSqlScalar(strSql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        //获取某充值类型
        public string GetMemberMoneyType(string strid)
        {
            string strSql = "select moneyType from T_Member_AccountRecord where sysnumber='" + strid + "'";
            object obj =  adoHelper.ExecuteSqlScalar(strSql);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        //根据充值类型同步改变会员表现金总额
        public int UpdateMemberCount(string type, string count, string id)
        {
            string strSql = "";
            if (type == "XJ")
            {
                strSql = "update T_Member_Info set buyMoneyAccount=buyMoneyAccount+" + count + " where memberId="+ id +"";
            }
            else
            {
                strSql = "update T_Member_Info set freeMoenyAccount=freeMoenyAccount+" + count + " where memberId=" + id + "";
            }
            object obj = adoHelper.ExecuteSqlScalar(strSql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion
    }
}
