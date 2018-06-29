using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Article
{
    public class ArticleModel
    {
        //说明文章表
        public ArticleModel()
        { }

        #region Model
        private int _articleid;
        private string _titie;
        private string _body;
        private int _addeduserid;
        private DateTime _addeddate;
        private DateTime _releasedate;
        private DateTime _expiredate;
        private int _categoryid;
        private int _approved;
        private int _fileid;
        private int? _marketid;
        private int? _site;
        /// <summary>
        /// 
        /// </summary>
        public int ArticleId
        {
            set { _articleid = value; }
            get { return _articleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Titie
        {
            set { _titie = value; }
            get { return _titie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AddedUserId
        {
            set { _addeduserid = value; }
            get { return _addeduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddedDate
        {
            set { _addeddate = value; }
            get { return _addeddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReleaseDate
        {
            set { _releasedate = value; }
            get { return _releasedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpireDate
        {
            set { _expiredate = value; }
            get { return _expiredate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Approved
        {
            set { _approved = value; }
            get { return _approved; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int FileId
        {
            set { _fileid = value; }
            get { return _fileid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MarketId
        {
            set { _marketid = value; }
            get { return _marketid; }
        }
        /// <summary>
        /// 0 ios平台  1安卓平台
        /// </summary>
        public int? Site
        {
            set { _site = value; }
            get { return _site; }
        }
        #endregion Model
    }
}
