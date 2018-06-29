using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.ZXTS
{
    public class ZxModel
    {
        //咨询表
        public ZxModel()
        { }

        #region Model
        private int _id;
        private string _name;
        private string _tel;
        private string _address;
        private string _department;
        private string _email;
        private int? _type = 0;
        private string _content;
        private DateTime? _filltime = DateTime.Now;
        private DateTime? _replytime;
        private string _replypeople;
        private string _title;
        private string _replycontent;
        private int? _state = 0;
        private int? _ischeck = 0;
        private int? _platform;
        private string _memberid;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 咨询人
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 咨询人电话
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 咨询人email
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 咨询类别0：计量监督，1：标准化，2:质量管理，3：质量监督，4：特种设备，5：车辆管理，6：认证认可，7：其他相关
        /// </summary>
        public int? type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 咨询内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 咨询时间
        /// </summary>
        public DateTime? fillTime
        {
            set { _filltime = value; }
            get { return _filltime; }
        }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? replyTime
        {
            set { _replytime = value; }
            get { return _replytime; }
        }
        /// <summary>
        /// 回复人
        /// </summary>
        public string replyPeople
        {
            set { _replypeople = value; }
            get { return _replypeople; }
        }
        /// <summary>
        /// 咨询主题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string replyContent
        {
            set { _replycontent = value; }
            get { return _replycontent; }
        }
        /// <summary>
        /// 回复状态，0：未回复，1：已回复
        /// </summary>
        public int? state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 审核状态，0：未审核，1：已审核
        /// </summary>
        public int? isCheck
        {
            set { _ischeck = value; }
            get { return _ischeck; }
        }
        /// <summary>
        /// 咨询平台（1：企业频道，2：市民频道）
        /// </summary>
        public int? platform
        {
            set { _platform = value; }
            get { return _platform; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string memberid
        {
            set { _memberid = value; }
            get { return _memberid; }
        }
        #endregion Model
    }
}
