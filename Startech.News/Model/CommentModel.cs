using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.News
{
    public class CommentModel
    {
        public CommentModel()
        { }
        #region Model
        private int _commentid;
        private int _newsid;
        private string _commentname;
        private string _commentcontent;
        /// <summary>
        /// 
        /// </summary>
        public int CommentID
        {
            set { _commentid = value; }
            get { return _commentid; }
        }
        /// <summary>
        /// 新闻编号
        /// </summary>
        public int NewsID
        {
            set { _newsid = value; }
            get { return _newsid; }
        }
        /// <summary>
        /// 留言人姓名
        /// </summary>
        public string CommentName
        {
            set { _commentname = value; }
            get { return _commentname; }
        }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string CommentContent
        {
            set { _commentcontent = value; }
            get { return _commentcontent; }
        }
        #endregion Model
    }
}
