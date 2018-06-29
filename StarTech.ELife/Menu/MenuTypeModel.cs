using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Menu
{
    public class MenuTypeModel
    {
        private string _categoryid;
		private string _categoryname;
		private int? _categorylevel;
		private string _pcategoryid;
		private string _categorypath;
		private string _rootid;
		private string _categorytotypeid;
		private string _categoryflag;
		private int? _orderby;
		private string _remarks;
		private string _url;
		private string _icon;
		/// <summary>
		/// 
		/// </summary>
		public string CategoryId
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CategoryName
		{
			set{ _categoryname=value;}
			get{return _categoryname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CategoryLevel
		{
			set{ _categorylevel=value;}
			get{return _categorylevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PCategoryId
		{
			set{ _pcategoryid=value;}
			get{return _pcategoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CategoryPath
		{
			set{ _categorypath=value;}
			get{return _categorypath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RootId
		{
			set{ _rootid=value;}
			get{return _rootid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CategoryToTypeId
		{
			set{ _categorytotypeid=value;}
			get{return _categorytotypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CategoryFlag
		{
			set{ _categoryflag=value;}
			get{return _categoryflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Orderby
		{
			set{ _orderby=value;}
			get{return _orderby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
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
		public string icon
		{
			set{ _icon=value;}
			get{return _icon;}
		}
    }
}
