using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.News
{
    [Serializable]
    public class NewsModel
    {
        public NewsModel()
        { }
        #region Model
        private int _newsid;
        private string _title;
        private string _subhead;
        private string _body;
        private int? _addeduserid;
        private string _publicationunit;
        private DateTime? _addeddate;
        private int? _istop;
        private int? _indexcommend;
        private int? _articletype;
        private DateTime? _releasedate;
        private DateTime? _expiredate;
        private int? _categoryid;
        private int? _approved;
        private int? _viewcount;
        private string _imglink;
        private int? _isstate;
        private string _hotpic;
        private string _keyword;
        private int? _hotdays;
        private string _fromsource;
        private int? _iscomment;
        private int? _isscrool;
        private string _period;
        private int _leaderid;
        private string _ShareToPlatform;
        private string _ShareToSubject;
        private string _ShareToMarket;
        /// <summary>
        /// 
        /// </summary>
        public int NewsID
        {
            set { _newsid = value; }
            get { return _newsid; }
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
        public string SubHead
        {
            set { _subhead = value; }
            get { return _subhead; }
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
        public int? AddedUserId
        {
            set { _addeduserid = value; }
            get { return _addeduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PublicationUnit
        {
            set { _publicationunit = value; }
            get { return _publicationunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddedDate
        {
            set { _addeddate = value; }
            get { return _addeddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IndexCommend
        {
            set { _indexcommend = value; }
            get { return _indexcommend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ArticleType
        {
            set { _articletype = value; }
            get { return _articletype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReleaseDate
        {
            set { _releasedate = value; }
            get { return _releasedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExpireDate
        {
            set { _expiredate = value; }
            get { return _expiredate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Approved
        {
            set { _approved = value; }
            get { return _approved; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ViewCount
        {
            set { _viewcount = value; }
            get { return _viewcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgLink
        {
            set { _imglink = value; }
            get { return _imglink; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsState
        {
            set { _isstate = value; }
            get { return _isstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HotPic
        {
            set { _hotpic = value; }
            get { return _hotpic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KeyWord
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HotDays
        {
            set { _hotdays = value; }
            get { return _hotdays; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromSource
        {
            set { _fromsource = value; }
            get { return _fromsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsComment
        {
            set { _iscomment = value; }
            get { return _iscomment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsScrool
        {
            set { _isscrool = value; }
            get { return _isscrool; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Period
        {
            set { _period = value; }
            get { return _period; }
        }
        /// <summary>
        /// ¡Ïµº±‡∫≈
        /// </summary>
        public int Leaderid
        {
            set { _leaderid = value; }
            get { return _leaderid; }
        }
        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        public string ShareToPlatform
        {
            set { _ShareToPlatform = value; }
            get { return _ShareToPlatform; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShareToSubject
        {
            set { _ShareToSubject = value; }
            get { return _ShareToSubject; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShareToMarket
        {
            set { _ShareToMarket = value; }
            get { return _ShareToMarket; }
        }
        
    }
}
