using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Article
{
    public class ArticleModel
    {
        public ArticleModel()
		{}

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
        private string _ShareToPlatform;
		/// <summary>
		/// 
		/// </summary>
		public int ArticleId
		{
			set{ _articleid=value;}
			get{return _articleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Titie
		{
			set{ _titie=value;}
			get{return _titie;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Body
		{
			set{ _body=value;}
			get{return _body;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AddedUserId
		{
			set{ _addeduserid=value;}
			get{return _addeduserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddedDate
		{
			set{ _addeddate=value;}
			get{return _addeddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ReleaseDate
		{
			set{ _releasedate=value;}
			get{return _releasedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ExpireDate
		{
			set{ _expiredate=value;}
			get{return _expiredate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CategoryId
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Approved
		{
			set{ _approved=value;}
			get{return _approved;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FileId
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}

        /// <summary>
        /// 
        /// </summary>
        public string ShareToPlatform
        {
            set { _ShareToPlatform = value; }
            get { return _ShareToPlatform; }
        }
		#endregion Model
    }

    /// <summary>
    /// 实体类T_Category 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class CategoryModel
    {
        public CategoryModel()
        { }
        #region Model
        private int _categoryid;
        private string _title;
        private int? _sort;
        private int? _type;
        private int? _parentcategoryid;
        private int? _addeduserid;
        private DateTime? _addeddate;
        private string _url;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentCategoryId
        {
            set { _parentcategoryid = value; }
            get { return _parentcategoryid; }
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
        public DateTime? AddedDate
        {
            set { _addeddate = value; }
            get { return _addeddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        #endregion Model

    }
}
