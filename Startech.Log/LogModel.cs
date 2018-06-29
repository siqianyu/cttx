using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Log
{
    public class LogModel
    {
        public LogModel()
		{}
		#region Model
		private int _logid;
		private string _applicationname;
		private string _firstitem;
		private string _seconditem;
		private string _actiontype;
		private string _description;
		private int _userid;
		private DateTime _operationdate;
		private string _url;
		private string _ip;
		/// <summary>
		/// 
		/// </summary>
		public int LogId
		{
			set{ _logid=value;}
			get{return _logid;}
		}
		/// <summary>
		/// 应用程序名称
		/// </summary>
		public string ApplicationName
		{
			set{ _applicationname=value;}
			get{return _applicationname;}
		}
		/// <summary>
		/// 一级栏目名称
		/// </summary>
		public string FirstItem
		{
			set{ _firstitem=value;}
			get{return _firstitem;}
		}
		/// <summary>
		/// 二级栏目名称
		/// </summary>
		public string SecondItem
		{
			set{ _seconditem=value;}
			get{return _seconditem;}
		}
		/// <summary>
		/// 操作名称
		/// </summary>
		public string ActionType
		{
			set{ _actiontype=value;}
			get{return _actiontype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime OperationDate
		{
			set{ _operationdate=value;}
			get{return _operationdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP
		{
			set{ _ip=value;}
			get{return _ip;}
		}
		#endregion Model
    }
}
