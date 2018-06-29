using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.IACenter
{
    public class ModIACenter_MarketUser
    {
        #region Model
        private int _uniqueid;
        private string _username;
        private string _password="123456";
        private string _truename;
        private int _usertype=2;
        private int _isuse=1;
        private int _issuperadmin=0;
        private DateTime _addtime=DateTime.Now;
        private string _sex;
        private int _age=0;
        private string _tel;
        private string _mobile;
        private int _departid;
        private int _orderby=0;
        /// <summary>
        /// 
        /// </summary>
        public int uniqueId
        {
            set { _uniqueid = value; }
            get { return _uniqueid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string trueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 用户类型(企业:1,领导:2,科室:3,镇街:4)
        /// </summary>
        public int userType
        {
            set { _usertype = value; }
            get { return _usertype; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int isUse
        {
            set { _isuse = value; }
            get { return _isuse; }
        }
        /// <summary>
        /// 是否为超级用户
        /// </summary>
        public int isSuperAdmin
        {
            set { _issuperadmin = value; }
            get { return _issuperadmin; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime addTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 姓别
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int age
        {
            set { _age = value; }
            get { return _age; }
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
        /// 手机
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 所属部门(关联部门表)
        /// </summary>
        public int departId
        {
            set { _departid = value; }
            get { return _departid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int orderBy
        {
            set { _orderby = value; }
            get { return _orderby; }
        }
        #endregion Model
    }
}
