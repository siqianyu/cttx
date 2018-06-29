using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.News
{
    public class TopicsModel
    {
        public TopicsModel()
		{}

		#region Model
		private int _topicsid;
		private string _title;
		private string _body;
		private int? _topicscategoryid;
		private string _keyword;
		private string _fromsource;
		private string _author;
		private int? _viewcount;
		private DateTime? _addeddate;
		private DateTime? _releasedate;

		/// <summary>
		/// 
		/// </summary>
		public int TopicsId
		{
			set{ _topicsid=value;}
			get{return _topicsid;}
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
		public string Body
		{
			set{ _body=value;}
			get{return _body;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TopicsCategoryId
		{
			set{ _topicscategoryid=value;}
			get{return _topicscategoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyWord
		{
			set{ _keyword=value;}
			get{return _keyword;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FromSource
		{
			set{ _fromsource=value;}
			get{return _fromsource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ViewCount
		{
			set{ _viewcount=value;}
			get{return _viewcount;}
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
		public DateTime? ReleaseDate
		{
			set{ _releasedate=value;}
			get{return _releasedate;}
		}
		#endregion Model
    }
}
