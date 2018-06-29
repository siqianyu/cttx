using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.ELife.Question
{
    /// <summary>
    /// ModelTestday:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ModelTestday
    {
        public ModelTestday()
        { }
        #region Model
        private string _title;
        private string _remarks;
        private string _questions;
        private DateTime _addtime;
        private string _sysnumber;
        private int _type;
        private int _chapterId;
        private string _courseType;
        private string _personFlag;
        private string _levelFlag;
        private string _addMemberId;
        private string _shFlag;
        private DateTime? _startime;
        private DateTime? _endtime;
        private string _zjmothod;

        public string courseType
        {
            set { _courseType = value; }
            get { return _courseType; }
        }
        public string ZjMethod
        {
            set { _zjmothod = value; }
            get { return _zjmothod; }
        }
        public string personFlag
        {
            set { _personFlag = value; }
            get { return _personFlag; }
        }


        public string levelFlag
        {
            set { _levelFlag = value; }
            get { return _levelFlag; }
        }

        public string addMemberId
        {
            set { _addMemberId = value; }
            get { return _addMemberId; }
        }


        public string shFlag
        {
            set { _shFlag = value; }
            get { return _shFlag; }
        }


        public int chapterId
        {
            set { _chapterId = value; }
            get { return _chapterId; }
        }

        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Questions
        {
            set { _questions = value; }
            get { return _questions; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sysnumber
        {
            set { _sysnumber = value; }
            get { return _sysnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? startime
        {
            set { _startime = value; }
            get { return _startime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endtime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        #endregion Model

    }
}
