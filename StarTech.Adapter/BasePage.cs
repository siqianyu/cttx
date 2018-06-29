using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;


namespace StarTech.Adapter
{
    /// <summary>
    /// 工作室会员基类
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        #region 用户属性

        /// <summary>
        /// 会员id
        /// </summary>
        public string MemberId
        {
            get
            {
                string memberId = HttpContext.Current.Session["MemberId"] == null ? "" : HttpContext.Current.Session["MemberId"].ToString();
                return memberId;
            }
        }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string MemberName
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("MemberName") == true) ? hTable["MemberName"].ToString() : "";
            }
        }        
        
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("TrueName") == true) ? hTable["TrueName"].ToString() : "";
            }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("HeadImg") == true) ? hTable["HeadImg"].ToString() : "";
            }
        }
        /// <summary>
        /// 会员类型
        /// </summary>
        public string MemberType
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("MemberType") == true) ? hTable["MemberType"].ToString() : "";
            }
        }

        /// <summary>
        /// 会员所属公司名称
        /// </summary>
        public string MemberCompanyName
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("memberCompanyName") == true) ? hTable["memberCompanyName"].ToString() : "";
            }
        }

        /// <summary>
        /// 会员区域
        /// </summary>
        public string MemberPost
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("MemberPost") == true) ? hTable["MemberPost"].ToString() : "";
            }
        }

        /// <summary>
        /// 是否为会年费会员
        /// </summary>
        public bool IsYearMember
        {
            get
            {
                DataSet ds = new IACenter().ExecuteSqlDataset("select *  from  T_Member_Level where levelId='" + this.MemberLevel + "' and moneyType='Year'");
                return (ds.Tables[0].Rows.Count>0) ? true : false;
            }
        }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string MemberLevelName
        {
            get
            {
                DataSet ds = new IACenter().ExecuteSqlDataset("select levelName from  T_Member_Level where levelId='" + this.MemberLevel + "'");
                return (ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0]["levelName"].ToString() : "";
            }
        }

        /// <summary>
        /// 会员等级类型（订购的套餐）
        /// </summary>
        public string MemberLevel
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("MemberLevel") == true) ? hTable["MemberLevel"].ToString() : "";
            }
        }


        /// <summary>
        /// 会员包年等级（包年开始时间）
        /// </summary>
        public DateTime LevelServiceStartTime
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                string st = (hTable.Contains("LevelServiceEndTime") == true) ? hTable["LevelServiceEndTime"].ToString() : "1900-01-01";
                return (st != "") ? DateTime.Parse(st) : DateTime.Parse("1900-01-01");
            }
        }

        /// <summary>
        /// 会员包年等级（包年结束时间）
        /// </summary>
        public DateTime LevelServiceEndTime
        {

            get
            {
                Hashtable hTable = this.GetUserInfo();
                string st = (hTable.Contains("LevelServiceEndTime") == true) ? hTable["LevelServiceEndTime"].ToString() : "1900-01-01";
                return (st != "") ? DateTime.Parse(st) : DateTime.Parse("1900-01-01");
            }
        }

        /// <summary>
        /// 会员包年等级（包年结束时间）
        /// </summary>
        public string memberCompanyType
        {

            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("memberCompanyType") == true) ? hTable["memberCompanyType"].ToString() : "";
            }
        }

        /// <summary>
        /// 可托管标准数
        /// </summary>
        public int TrustNumber
        {

            get
            {
                DataTable dtUser = new IACenter().ExecuteSqlDataset("select * from T_Member_Info where memberId=" + this.MemberId + "").Tables[0];
                if (dtUser.Rows.Count > 0)
                {
                    return int.Parse(dtUser.Rows[0]["TrustNumber"].ToString());
                }
                return 0;
            }
        }

        /// <summary>
        /// 已托管标准数
        /// </summary>
        public int HasTrustNumber
        {

            get
            {
                DataTable dtUser = new IACenter().ExecuteSqlDataset("select isnull(count(*),0) as total from T_Standard_TrustServices where memberid=" + this.MemberId + "").Tables[0];
                if (dtUser.Rows.Count > 0)
                {
                    return int.Parse(dtUser.Rows[0]["total"].ToString());
                }
                return 0;
            }
        }

        /// <summary>
        /// 可下载标准数
        /// </summary>
        public int DownloadNumber
        {

            get
            {
                DataTable dtUser = new IACenter().ExecuteSqlDataset("select * from T_Member_Info where memberId=" + this.MemberId + "").Tables[0];
                if (dtUser.Rows.Count > 0)
                {
                    return int.Parse(dtUser.Rows[0]["DownloadNumber"].ToString());
                }
                return 0;
            }
        }


        /// <summary>
        /// 会员邮箱
        /// </summary>
        public string MemberEmail
        {
            get
            {
                DataTable dtUser = new IACenter().ExecuteSqlDataset("select * from T_Member_Info where memberId=" + this.MemberId + "").Tables[0];
                if (dtUser.Rows.Count > 0)
                {
                    return dtUser.Rows[0]["email"].ToString();
                }
                else { return ""; }
            }
        }

        

        /// <summary>
        /// 会员手机
        /// </summary>
        public string MemberMobile
        {
            get
            {
                DataTable dtUser = new IACenter().ExecuteSqlDataset("select * from T_Member_Info where memberId=" + this.MemberId + "").Tables[0];
                if (dtUser.Rows.Count > 0)
                {
                    return dtUser.Rows[0]["mobile"].ToString();
                }
                else { return ""; }
            }
        }
        protected Hashtable GetUserInfo()
        {
            Hashtable hTable = new Hashtable();
            string memberId = HttpContext.Current.Session["MemberId"] == null ? "" : HttpContext.Current.Session["MemberId"].ToString();
            if (memberId == null || memberId.ToString() == "") { return hTable; }

            DataTable dtUser = new IACenter().ExecuteSqlDataset("select * from T_Member_Info where memberId=" + memberId + "").Tables[0];
            if (dtUser.Rows.Count > 0)
            {
                hTable.Add("MemberId", dtUser.Rows[0]["memberId"].ToString());//
                hTable.Add("MemberName", dtUser.Rows[0]["memberName"].ToString());//
                hTable.Add("TrueName", dtUser.Rows[0]["TrueName"].ToString());//
                hTable.Add("MemberLevel", dtUser.Rows[0]["memberLevel"].ToString());
                hTable.Add("HeadImg", dtUser.Rows[0]["HeadImg"].ToString());
                
            }
            return hTable;
        }
        #endregion

        #region 公共方法

        #endregion

        #region 验证相关
        protected override void OnInit(EventArgs e)
        {
            this.UserCheck();
            base.OnInit(e);
        }

        protected void UserCheck()
        {
            string memberId = HttpContext.Current.Session["MemberId"] == null ? "" : HttpContext.Current.Session["MemberId"].ToString();
            if (memberId == null || memberId.ToString() == "")
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }
        #endregion
    }
}
