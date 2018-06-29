using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.ELife.Question
{
    public partial class ModelTestQueston
    {
        public ModelTestQueston()
        { }
        #region Model
        private string _sysnumber;
        private string _questiontype;
        private string _questiontitle;
        private string _questionanswer;
        private string _orwner;
        private string _descriptiong;
        private string _personflag;
        private string _levelflag;
        private string _categoryflag;
        private string _categorypath;
        private int? _ifcoursequestion = 0;
        private DateTime? _createtime;
        private string _createperson;
        private int? _shflag = 0;
        private int? _orderBy = 0;
        private string _courseId;
        private int? _isSubQuestion = 0;
        private string _mainQuestionSysnumber;
        private decimal _levelPoint;

        public decimal LevelPoint
        {
            get { return _levelPoint; }
            set { _levelPoint = value; }
        }

        public string Description
        {
            set { _descriptiong = value; }
            get { return _descriptiong; }
        }
        public string Orner
        {
            set { _orwner = value; }
            get { return _orwner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sysnumber
        {
            set { _sysnumber = value; }
            get { return _sysnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string questionType
        {
            set { _questiontype = value; }
            get { return _questiontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string questionTitle
        {
            set { _questiontitle = value; }
            get { return _questiontitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string questionAnswer
        {
            set { _questionanswer = value; }
            get { return _questionanswer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string personFlag
        {
            set { _personflag = value; }
            get { return _personflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string levelFlag
        {
            set { _levelflag = value; }
            get { return _levelflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string categoryFlag
        {
            set { _categoryflag = value; }
            get { return _categoryflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string categorypath
        {
            set { _categorypath = value; }
            get { return _categorypath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ifCourseQuestion
        {
            set { _ifcoursequestion = value; }
            get { return _ifcoursequestion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createPerson
        {
            set { _createperson = value; }
            get { return _createperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? shFlag
        {
            set { _shflag = value; }
            get { return _shflag; }
        }/// <summary>
        /// 
        /// </summary>
        public int? orderBy
        {
            set { _orderBy = value; }
            get { return _orderBy; }
        } 
        /// <summary>
        /// 
        /// </summary>
        public string courseId
        {
            set { _courseId = value; }
            get { return _courseId; }
        }

        public int? isSubQuestion
        {
            set { _isSubQuestion = value; }
            get { return _isSubQuestion; }
        } 

        /// <summary>
        /// 
        /// </summary>
        public string mainQuestionSysnumber
        {
            set { _mainQuestionSysnumber = value; }
            get { return _mainQuestionSysnumber; }
        }
        #endregion Model

    }
}
