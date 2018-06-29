using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.News
{
    public class TopicsCategoryModel
    {
        public TopicsCategoryModel()
		{}
		#region Model
		private int _topicscategoryid;
		private string _title;
		private int? _sort;
		private int? _parentcategoryid;
		private int? _addeduserid;
		private DateTime? _addeddate;
		private string _imgurl;
		private string _remark;
        private int _typeid;
        private DateTime _enddate;

        /// <summary>
        /// 类别名称   0：专题类别  1：新闻类别
        /// </summary>
        public int TypeId
        {
            get { return _typeid; }
            set { _typeid = value; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int TopicsCategoryId
		{
			set{ _topicscategoryid=value;}
			get{return _topicscategoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParentCategoryId
		{
			set{ _parentcategoryid=value;}
			get{return _parentcategoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AddedUserId
		{
			set{ _addeduserid=value;}
			get{return _addeduserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddedDate
		{
			set{ _addeddate=value;}
			get{return _addeddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImgURL
		{
			set{ _imgurl=value;}
			get{return _imgurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model
    }
}
