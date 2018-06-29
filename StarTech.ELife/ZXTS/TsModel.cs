using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.ZXTS
{
    public class TsModel
    {
        //投诉表
        public TsModel()
        { }

        #region Model
        private int _id;
        private string _subject;
        private string _type;
        private string _ssname;
        private string _ssadd;
        private string _sspost;
        private string _sstel;
        private string _productname;
        private string _valueinfo;
        private string _particularinfo;
        private string _email;
        private string _bsname;
        private string _bssadd;
        private string _bspost;
        private string _bstel;
        private DateTime? _filltime;
        private int? _ischeck;
        private string _answer;
        private DateTime? _answertime;
        private string _chickid;
        private int? _ispub;
        private int _isopen;
        private string _memberid;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSName
        {
            set { _ssname = value; }
            get { return _ssname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSAdd
        {
            set { _ssadd = value; }
            get { return _ssadd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSPost
        {
            set { _sspost = value; }
            get { return _sspost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSTel
        {
            set { _sstel = value; }
            get { return _sstel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ValueInfo
        {
            set { _valueinfo = value; }
            get { return _valueinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParticularInfo
        {
            set { _particularinfo = value; }
            get { return _particularinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BSName
        {
            set { _bsname = value; }
            get { return _bsname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BSSAdd
        {
            set { _bssadd = value; }
            get { return _bssadd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BSPost
        {
            set { _bspost = value; }
            get { return _bspost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BSTel
        {
            set { _bstel = value; }
            get { return _bstel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FillTime
        {
            set { _filltime = value; }
            get { return _filltime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsCheck
        {
            set { _ischeck = value; }
            get { return _ischeck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AnswerTime
        {
            set { _answertime = value; }
            get { return _answertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChickId
        {
            set { _chickid = value; }
            get { return _chickid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsPub
        {
            set { _ispub = value; }
            get { return _ispub; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MemberId
        {
            set { _memberid = value; }
            get { return _memberid; }
        }
        #endregion Model

    }
}
