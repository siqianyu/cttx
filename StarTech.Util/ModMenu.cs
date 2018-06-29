using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.Util
{
    /// <summary>
    /// 实体类T_PermissionEntity 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ModMenu
    {
        public ModMenu()
		{}
		#region Model
		private int _uniqueid;
		private string _menuname;
		private string _menutarget;
		private int? _parentmenuid;
		private int? _orderindex;
		private int? _isshow;
		/// <summary>
		/// 
		/// </summary>
		public int uniqueId
		{
			set{ _uniqueid=value;}
			get{return _uniqueid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menuName
		{
			set{ _menuname=value;}
			get{return _menuname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menuTarget
		{
			set{ _menutarget=value;}
			get{return _menutarget;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? parentMenuId
		{
			set{ _parentmenuid=value;}
			get{return _parentmenuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? orderIndex
		{
			set{ _orderindex=value;}
			get{return _orderindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		#endregion Model

    }
}
