using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Member.Member
{
    public class MemberInfoModel
    {
        public MemberInfoModel()
        { }
        #region Model
        private int _memberid;
        private string _membername;
        private string _password;
        private string _memberstatus;
        private string _memberinplatfroms;
        private string _membertype;
        private string _membercompanytype;
        private string _areaname;
        private string _membercompanyname;
        private string _membercompanycode;
        private string _membertruename;
        private string _sex;
        private string _tel;
        private string _fax;
        private string _mobile;
        private string _address;
        private string _post;
        private string _email;
        private DateTime _regtime = DateTime.Now;
        private string _regtype;
        private int _shflag = 0;
        private DateTime _shtime = DateTime.Parse("1900-01-01");
        private string _shperson;
        private string _unpassreason;
        private decimal _buymoneyaccount = 0;
        private decimal _buymoneyaccountused = 0;
        private decimal _freemoenyaccount = 0;
        private decimal _freemoenyaccountused = 0;
        private string _memberlevel;
        private DateTime _levelservicestarttime = DateTime.Now;
        private DateTime _levelserviceendtime = DateTime.Now;
        private int _ishangzhou = 0;
        private int _iscompany = 0;
        private string _companyinfo;
        private string _url_qy;
        private string _url_business;
        private int _downloadNumber;
        private int _trustNumber;
        private int _ischeckcardid;
        /// <summary>
        /// 会员编号（Guid）
        /// </summary>
        public int memberId
        {
            set { _memberid = value; }
            get { return _memberid; }
        }
        /// <summary>
        /// 会员名
        /// </summary>
        public string memberName
        {
            set { _membername = value; }
            get { return _membername; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 会员状态：正常：ZC,禁用：JY
        /// </summary>
        public string memberStatus
        {
            set { _memberstatus = value; }
            get { return _memberstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string memberInPlatfroms
        {
            set { _memberinplatfroms = value; }
            get { return _memberinplatfroms; }
        }
        /// <summary>
        /// 会员类型：政府：Gov,企业：Com,个人：Person
        /// </summary>
        public string memberType
        {
            set { _membertype = value; }
            get { return _membertype; }
        }
        /// <summary>
        /// 会员的企业类别（直接写中文）
        /// </summary>
        public string memberCompanyType
        {
            set { _membercompanytype = value; }
            get { return _membercompanytype; }
        }
        /// <summary>
        /// 所在地区（直接写中文）
        /// </summary>
        public string areaName
        {
            set { _areaname = value; }
            get { return _areaname; }
        }
        /// <summary>
        /// 会员的企业名称
        /// </summary>
        public string memberCompanyName
        {
            set { _membercompanyname = value; }
            get { return _membercompanyname; }
        }
        /// <summary>
        /// 会员的企业组织机构代码
        /// </summary>
        public string memberCompanyCode
        {
            set { _membercompanycode = value; }
            get { return _membercompanycode; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string memberTrueName
        {
            set { _membertruename = value; }
            get { return _membertruename; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 发送短信用
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string post
        {
            set { _post = value; }
            get { return _post; }
        }
        /// <summary>
        /// email
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime regTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        /// <summary>
        ///  注册类型 1会员注册 2消费券注册
        /// </summary>
        public string regType
        {
            set { _regtype = value; }
            get { return _regtype; }
        }
        /// <summary>
        /// 审核标示：1通过，0未通过
        /// </summary>
        public int shFlag
        {
            set { _shflag = value; }
            get { return _shflag; }
        }

        /// <summary>
        /// 是否验证了法人身份证:1 是 否则 不是
        /// </summary>
        public int ischeckcardid
        {
            set { _ischeckcardid = value; }
            get { return _ischeckcardid; }
        }

        /// <summary>
        /// 下载数量
        /// </summary>
        public int downloadNumber
        {
            set { _downloadNumber = value; }
            get { return _downloadNumber; }
        }

        /// <summary>
        /// 托管数量
        /// </summary>
        public int trustNumber
        {
            set { _trustNumber = value; }
            get { return _trustNumber; }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime shTime
        {
            set { _shtime = value; }
            get { return _shtime; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string shPerson
        {
            set { _shperson = value; }
            get { return _shperson; }
        }
        /// <summary>
        /// 未通过审核原因
        /// </summary>
        public string unPassReason
        {
            set { _unpassreason = value; }
            get { return _unpassreason; }
        }
        /// <summary>
        /// 现金账户（用现金充值）
        /// </summary>
        public decimal buyMoneyAccount
        {
            set { _buymoneyaccount = value; }
            get { return _buymoneyaccount; }
        }
        /// <summary>
        /// 现金账户（用现金充值）累计使用记录
        /// </summary>
        public decimal buyMoneyAccountUsed
        {
            set { _buymoneyaccountused = value; }
            get { return _buymoneyaccountused; }
        }
        /// <summary>
        /// 赠送的账户（如消费券等）
        /// </summary>
        public decimal freeMoenyAccount
        {
            set { _freemoenyaccount = value; }
            get { return _freemoenyaccount; }
        }
        /// <summary>
        /// 赠送的账户（如消费券等）累计使用记录
        /// </summary>
        public decimal freeMoenyAccountUsed
        {
            set { _freemoenyaccountused = value; }
            get { return _freemoenyaccountused; }
        }
        /// <summary>
        /// 会员等级（备用字段：默认1）
        /// </summary>
        public string memberLevel
        {
            set { _memberlevel = value; }
            get { return _memberlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime levelServiceStartTime
        {
            set { _levelservicestarttime = value; }
            get { return _levelservicestarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime levelServiceEndTime
        {
            set { _levelserviceendtime = value; }
            get { return _levelserviceendtime; }
        }

        /// <summary>
        /// 是否杭州会员
        /// </summary>
        public int ishangzhou
        {
            set { _ishangzhou = value; }
            get { return _ishangzhou; }
        }

        /// <summary>
        /// 是否企业
        /// </summary>
        public int iscompany
        {
            set { _iscompany = value; }
            get { return _iscompany; }
        }

        /// <summary>
        /// 企业简介
        /// </summary>
        public string companyInfo
        {
            set { _companyinfo = value; }
            get { return _companyinfo; }
        }

        /// <summary>
        /// 企业网址
        /// </summary>
        public string url_qy
        {
            set { _url_qy = value; }
            get { return _url_qy; }
        }

        /// <summary>
        /// 电商网址
        /// </summary>
        public string url_business
        {
            set { _url_business = value; }
            get { return _url_business; }
        }

        #endregion Model


    }
}
