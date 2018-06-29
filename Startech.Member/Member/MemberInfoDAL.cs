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
    public class MemberInfoDAL
    {
        public MemberInfoDAL()
        { }

        public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Member_Info(");
            strSql.Append("memberName,password,memberStatus,memberInPlatfroms,memberType,memberCompanyType,areaName,memberCompanyName,memberCompanyCode,memberTrueName,sex,tel,fax,mobile,address,post,email,regTime,regType,shFlag,shTime,shPerson,unPassReason,buyMoneyAccount,buyMoneyAccountUsed,freeMoenyAccount,freeMoenyAccountUsed,memberLevel,levelServiceStartTime,levelServiceEndTime,ishangzhou,iscompany,companyinfo,url_qy,url_business,downloadNumber,trustNumber)");
            strSql.Append(" values (");
            strSql.Append("@memberName,@password,@memberStatus,@memberInPlatfroms,@memberType,@memberCompanyType,@areaName,@memberCompanyName,@memberCompanyCode,@memberTrueName,@sex,@tel,@fax,@mobile,@address,@post,@email,@regTime,@regType,@shFlag,@shTime,@shPerson,@unPassReason,@buyMoneyAccount,@buyMoneyAccountUsed,@freeMoenyAccount,@freeMoenyAccountUsed,@memberLevel,@levelServiceStartTime,@levelServiceEndTime,@ishangzhou,@iscompany,@companyinfo,@url_qy,@url_business,@downloadNumber,@trustNumber)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@memberName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,50),
					new SqlParameter("@memberStatus", SqlDbType.VarChar,50),
					new SqlParameter("@memberInPlatfroms", SqlDbType.VarChar,50),
					new SqlParameter("@memberType", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyType", SqlDbType.VarChar,50),
					new SqlParameter("@areaName", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyName", SqlDbType.VarChar,100),
					new SqlParameter("@memberCompanyCode", SqlDbType.VarChar,50),
					new SqlParameter("@memberTrueName", SqlDbType.VarChar,50),
					new SqlParameter("@sex", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@fax", SqlDbType.VarChar,50),
					new SqlParameter("@mobile", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,500),
					new SqlParameter("@post", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@regTime", SqlDbType.DateTime),
					new SqlParameter("@regType", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.Int,4),
					new SqlParameter("@shTime", SqlDbType.DateTime),
					new SqlParameter("@shPerson", SqlDbType.VarChar,50),
					new SqlParameter("@unPassReason", SqlDbType.VarChar,500),
					new SqlParameter("@buyMoneyAccount", SqlDbType.Decimal,9),
					new SqlParameter("@buyMoneyAccountUsed", SqlDbType.Decimal,9),
					new SqlParameter("@freeMoenyAccount", SqlDbType.Decimal,9),
					new SqlParameter("@freeMoenyAccountUsed", SqlDbType.Decimal,9),
					new SqlParameter("@memberLevel", SqlDbType.VarChar,50),
					new SqlParameter("@levelServiceStartTime", SqlDbType.DateTime),
					new SqlParameter("@levelServiceEndTime", SqlDbType.DateTime),
                    new SqlParameter("@ishangzhou", SqlDbType.Int,4),                    
                    new SqlParameter("@iscompany", SqlDbType.Int,4),
                    new SqlParameter("@companyinfo", SqlDbType.VarChar,500),
                     new SqlParameter("@url_qy", SqlDbType.VarChar,500),
                      new SqlParameter("@url_business", SqlDbType.VarChar,500),
                       new SqlParameter("@downloadNumber", SqlDbType.Int,4),
                       new SqlParameter("@trustNumber", SqlDbType.Int,4)   
                                        };
            parameters[0].Value = model.memberName;
            parameters[1].Value = model.password;
            parameters[2].Value = model.memberStatus;
            parameters[3].Value = model.memberInPlatfroms;
            parameters[4].Value = model.memberType;
            parameters[5].Value = model.memberCompanyType;
            parameters[6].Value = model.areaName;
            parameters[7].Value = model.memberCompanyName;
            parameters[8].Value = model.memberCompanyCode;
            parameters[9].Value = model.memberTrueName;
            parameters[10].Value = model.sex;
            parameters[11].Value = model.tel;
            parameters[12].Value = model.fax;
            parameters[13].Value = model.mobile;
            parameters[14].Value = model.address;
            parameters[15].Value = model.post;
            parameters[16].Value = model.email;
            parameters[17].Value = model.regTime;
            parameters[18].Value = model.regType;
            parameters[19].Value = model.shFlag;
            parameters[20].Value = model.shTime;
            parameters[21].Value = model.shPerson;
            parameters[22].Value = model.unPassReason;
            parameters[23].Value = model.buyMoneyAccount;
            parameters[24].Value = model.buyMoneyAccountUsed;
            parameters[25].Value = model.freeMoenyAccount;
            parameters[26].Value = model.freeMoenyAccountUsed;
            parameters[27].Value = model.memberLevel;
            parameters[28].Value = model.levelServiceStartTime;
            parameters[29].Value = model.levelServiceEndTime;
            parameters[30].Value = model.ishangzhou;
            parameters[31].Value = model.iscompany;
            parameters[32].Value = model.companyInfo;
            parameters[33].Value = model.url_qy;
            parameters[34].Value = model.url_business;
            parameters[35].Value = model.downloadNumber;
            parameters[36].Value = model.trustNumber;
            

            object obj = adoHelper.ExecuteSqlScalar(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public int Update(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Member_Info set ");
            strSql.Append("memberName=@memberName,");
            strSql.Append("password=@password,");
            strSql.Append("memberStatus=@memberStatus,");
            strSql.Append("memberInPlatfroms=@memberInPlatfroms,");
            strSql.Append("memberType=@memberType,");
            strSql.Append("memberCompanyType=@memberCompanyType,");
            strSql.Append("areaName=@areaName,");
            strSql.Append("memberCompanyName=@memberCompanyName,");
            strSql.Append("memberCompanyCode=@memberCompanyCode,");
            strSql.Append("memberTrueName=@memberTrueName,");
            strSql.Append("sex=@sex,");
            strSql.Append("tel=@tel,");
            strSql.Append("fax=@fax,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("address=@address,");
            strSql.Append("post=@post,");
            strSql.Append("email=@email,");
            strSql.Append("regTime=@regTime,");
            strSql.Append("regType=@regType,");
            strSql.Append("shFlag=@shFlag,");
            strSql.Append("shTime=@shTime,");
            strSql.Append("shPerson=@shPerson,");
            strSql.Append("unPassReason=@unPassReason,");
            strSql.Append("buyMoneyAccount=@buyMoneyAccount,");
            strSql.Append("buyMoneyAccountUsed=@buyMoneyAccountUsed,");
            strSql.Append("freeMoenyAccount=@freeMoenyAccount,");
            strSql.Append("freeMoenyAccountUsed=@freeMoenyAccountUsed,");
            strSql.Append("memberLevel=@memberLevel,");
            strSql.Append("levelServiceStartTime=@levelServiceStartTime,");
            strSql.Append("levelServiceEndTime=@levelServiceEndTime,");
            strSql.Append("companyinfo=@companyinfo,");
            strSql.Append("url_qy=@url_qy,");
            strSql.Append("url_business=@url_business, ");
            strSql.Append("downloadNumber=@downloadNumber, ");
            strSql.Append("trustNumber=@trustNumber ");
            strSql.Append(" where memberId=@memberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@memberId", SqlDbType.Int,4),
					new SqlParameter("@memberName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,50),
					new SqlParameter("@memberStatus", SqlDbType.VarChar,50),
					new SqlParameter("@memberInPlatfroms", SqlDbType.VarChar,50),
					new SqlParameter("@memberType", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyType", SqlDbType.VarChar,50),
					new SqlParameter("@areaName", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyName", SqlDbType.VarChar,100),
					new SqlParameter("@memberCompanyCode", SqlDbType.VarChar,50),
					new SqlParameter("@memberTrueName", SqlDbType.VarChar,50),
					new SqlParameter("@sex", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@fax", SqlDbType.VarChar,50),
					new SqlParameter("@mobile", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,500),
					new SqlParameter("@post", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@regTime", SqlDbType.DateTime),
					new SqlParameter("@regType", SqlDbType.VarChar,50),
					new SqlParameter("@shFlag", SqlDbType.Int,4),
					new SqlParameter("@shTime", SqlDbType.DateTime),
					new SqlParameter("@shPerson", SqlDbType.VarChar,50),
					new SqlParameter("@unPassReason", SqlDbType.VarChar,500),
					new SqlParameter("@buyMoneyAccount", SqlDbType.Decimal,9),
					new SqlParameter("@buyMoneyAccountUsed", SqlDbType.Decimal,9),
					new SqlParameter("@freeMoenyAccount", SqlDbType.Decimal,9),
					new SqlParameter("@freeMoenyAccountUsed", SqlDbType.Decimal,9),
					new SqlParameter("@memberLevel", SqlDbType.VarChar,50),
					new SqlParameter("@levelServiceStartTime", SqlDbType.DateTime),
					new SqlParameter("@levelServiceEndTime", SqlDbType.DateTime),
                     new SqlParameter("@companyinfo", SqlDbType.VarChar,500),
                     new SqlParameter("@url_qy", SqlDbType.VarChar,500),
                      new SqlParameter("@url_business", SqlDbType.VarChar,500),
                    new SqlParameter("@downloadNumber", SqlDbType.Int,4),
                     new SqlParameter("@trustNumber", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.memberId;
            parameters[1].Value = model.memberName;
            parameters[2].Value = model.password;
            parameters[3].Value = model.memberStatus;
            parameters[4].Value = model.memberInPlatfroms;
            parameters[5].Value = model.memberType;
            parameters[6].Value = model.memberCompanyType;
            parameters[7].Value = model.areaName;
            parameters[8].Value = model.memberCompanyName;
            parameters[9].Value = model.memberCompanyCode;
            parameters[10].Value = model.memberTrueName;
            parameters[11].Value = model.sex;
            parameters[12].Value = model.tel;
            parameters[13].Value = model.fax;
            parameters[14].Value = model.mobile;
            parameters[15].Value = model.address;
            parameters[16].Value = model.post;
            parameters[17].Value = model.email;
            parameters[18].Value = model.regTime;
            parameters[19].Value = model.regType;
            parameters[20].Value = model.shFlag;
            parameters[21].Value = model.shTime;
            parameters[22].Value = model.shPerson;
            parameters[23].Value = model.unPassReason;
            parameters[24].Value = model.buyMoneyAccount;
            parameters[25].Value = model.buyMoneyAccountUsed;
            parameters[26].Value = model.freeMoenyAccount;
            parameters[27].Value = model.freeMoenyAccountUsed;
            parameters[28].Value = model.memberLevel;
            parameters[29].Value = model.levelServiceStartTime;
            parameters[30].Value = model.levelServiceEndTime;
            parameters[31].Value = model.companyInfo;
            parameters[32].Value = model.url_qy;
            parameters[33].Value = model.url_business;
            parameters[34].Value = model.downloadNumber;
            parameters[35].Value = model.trustNumber;
            
            return adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int memberId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Member_Info ");
            strSql.Append(" where memberId=@memberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@memberId", SqlDbType.Int,4)};
            parameters[0].Value = memberId;

            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberInfoModel GetModel(int memberId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Member_Info ");
            strSql.Append(" where memberId=@memberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@memberId", SqlDbType.Int,4)};
            parameters[0].Value = memberId;

            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = adoHelper.ExecuteSqlDataset(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["memberId"].ToString() != "")
                {
                    model.memberId = int.Parse(ds.Tables[0].Rows[0]["memberId"].ToString());
                }
                model.memberName = ds.Tables[0].Rows[0]["memberName"].ToString();
                model.password = ds.Tables[0].Rows[0]["password"].ToString();
                model.memberStatus = ds.Tables[0].Rows[0]["memberStatus"].ToString();
                model.memberType = ds.Tables[0].Rows[0]["memberType"].ToString();
                model.memberCompanyType = ds.Tables[0].Rows[0]["memberCompanyType"].ToString();
                model.areaName = ds.Tables[0].Rows[0]["areaName"].ToString();
                model.memberCompanyName = ds.Tables[0].Rows[0]["memberCompanyName"].ToString();
                model.memberCompanyCode = ds.Tables[0].Rows[0]["memberCompanyCode"].ToString();
                model.memberTrueName = ds.Tables[0].Rows[0]["memberTrueName"].ToString();
                model.sex = ds.Tables[0].Rows[0]["sex"].ToString();
                model.tel = ds.Tables[0].Rows[0]["tel"].ToString();
                model.fax = ds.Tables[0].Rows[0]["fax"].ToString();
                model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                model.address = ds.Tables[0].Rows[0]["address"].ToString();
                model.post = ds.Tables[0].Rows[0]["post"].ToString();
                model.email = ds.Tables[0].Rows[0]["email"].ToString();
                if (ds.Tables[0].Rows[0]["regTime"].ToString() != "")
                {
                    model.regTime = DateTime.Parse(ds.Tables[0].Rows[0]["regTime"].ToString());
                }
                //model.regType = ds.Tables[0].Rows[0]["regType"].ToString();
                if (ds.Tables[0].Rows[0]["shFlag"].ToString() != "")
                {
                    model.shFlag = int.Parse(ds.Tables[0].Rows[0]["shFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["shTime"].ToString() != "")
                {
                    model.shTime = DateTime.Parse(ds.Tables[0].Rows[0]["shTime"].ToString());
                }
                model.shPerson = ds.Tables[0].Rows[0]["shPerson"].ToString();
                model.unPassReason = ds.Tables[0].Rows[0]["unPassReason"].ToString();
                if (ds.Tables[0].Rows[0]["buyMoneyAccount"].ToString() != "")
                {
                    model.buyMoneyAccount = decimal.Parse(ds.Tables[0].Rows[0]["buyMoneyAccount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["buyMoneyAccountUsed"].ToString() != "")
                {
                    model.buyMoneyAccountUsed = decimal.Parse(ds.Tables[0].Rows[0]["buyMoneyAccountUsed"].ToString());
                }
                if (ds.Tables[0].Rows[0]["freeMoenyAccount"].ToString() != "")
                {
                    model.freeMoenyAccount = decimal.Parse(ds.Tables[0].Rows[0]["freeMoenyAccount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["freeMoenyAccountUsed"].ToString() != "")
                {
                    model.freeMoenyAccountUsed = decimal.Parse(ds.Tables[0].Rows[0]["freeMoenyAccountUsed"].ToString());
                }
                model.memberLevel = ds.Tables[0].Rows[0]["memberLevel"].ToString();
                if (ds.Tables[0].Rows[0]["levelServiceStartTime"].ToString() != "")
                {
                    model.levelServiceStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["levelServiceStartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["levelServiceEndTime"].ToString() != "")
                {
                    model.levelServiceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["levelServiceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["iscompany"].ToString() != "")
                {
                    model.iscompany = int.Parse(ds.Tables[0].Rows[0]["iscompany"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ishangzhou"].ToString() != "")
                {
                    model.ishangzhou = int.Parse(ds.Tables[0].Rows[0]["ishangzhou"].ToString());
                }

                model.companyInfo = ds.Tables[0].Rows[0]["companyInfo"].ToString();
                model.url_qy = ds.Tables[0].Rows[0]["url_qy"].ToString();
                model.url_business = ds.Tables[0].Rows[0]["url_business"].ToString();
                if (ds.Tables[0].Rows[0]["downloadNumber"].ToString() != "")
                {
                    model.downloadNumber = int.Parse(ds.Tables[0].Rows[0]["downloadNumber"].ToString());
                }
                if (ds.Tables[0].Rows[0]["trustNumber"].ToString() != "")
                {
                    model.trustNumber = int.Parse(ds.Tables[0].Rows[0]["trustNumber"].ToString());
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
            strSql.Append(" FROM T_Member_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            return PaginationUtility.GetPaginationList(fields, "T_Member_Info", filter, sort, currentPageIndex, pageSize, out recordCount);
        }

        #endregion  成员方法

        #region 自定义方法
        public void Buy(double moeny, int id, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update T_Member_Info set  ");
            if (type == 0)
            {
                strSql.Append(" freeMoneyAccountUsed=freeMoneyAccountUsed+@money ");
            }
            else if (type == 1)
            {
                strSql.Append(" buyMoneyAccountUsed=buyMoneyAccountUsed+@money ");
            }
            strSql.Append(" where memberId=@memberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@memberId", SqlDbType.Int,4),
                    new SqlParameter("@money", SqlDbType.Decimal,10),
            };
            parameters[0].Value = id;
            parameters[1].Value = moeny;
            adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
        public int CheckUserName(string name)
        {
            string strSql = "select memberid from T_Member_Info where membername='" + name + "'";
            object obj = adoHelper.ExecuteSqlScalar(strSql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public string CheckUserPwd(string name, string pwd)
        {
            string strSql = "select password from T_Member_Info where membername='" + name + "' and password='" + pwd + "'";
            object obj = adoHelper.ExecuteSqlScalar(strSql);
            if (obj == null)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }
        public int UpdatePwd(string name, string pwd)
        {
            string strSql = "update T_Member_Info set password='" + pwd + "' where membername='" + name + "'";
            object obj = adoHelper.ExecuteSqlScalar(strSql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        #endregion
    }
}
