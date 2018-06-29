using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.ELife.Question
{
    public partial class ModelTestQuestonAnswer
    {
        public ModelTestQuestonAnswer()
        { }
        #region Model
        private string _sysnumber;
        private string _questionsysnumber;
        private string _answerkey;
        private string _answervalue;
        private int _orderby;
        public int OrderBy
        {
            set { _orderby = value; }
            get { return _orderby; }
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
        public string questionSysnumber
        {
            set { _questionsysnumber = value; }
            get { return _questionsysnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnswerKey
        {
            set { _answerkey = value; }
            get { return _answerkey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnswerValue
        {
            set { _answervalue = value; }
            get { return _answervalue; }
        }
        #endregion Model

    }
}
