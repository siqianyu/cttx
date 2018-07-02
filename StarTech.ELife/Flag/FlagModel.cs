/**  版本信息模板在安装目录下，可自行修改。
* T_Base_Flag.cs
*
* 功 能： N/A
* 类 名： T_Base_Flag
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-25 18:20:04   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace StarTech.ELife.Flag
{
	/// <summary>
	/// T_Base_Flag:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FlagModel
	{
        public FlagModel()
		{}
		#region Model
		private string _flag_id;
		private string _flag_name;
		private int? _if_use;
		/// <summary>
		/// 标签id
		/// </summary>
		public string flag_id
		{
			set{ _flag_id=value;}
			get{return _flag_id;}
		}
		/// <summary>
		/// 标签名称
		/// </summary>
		public string flag_name
		{
			set{ _flag_name=value;}
			get{return _flag_name;}
		}
		/// <summary>
		/// 是否启用
		/// </summary>
		public int? if_use
		{
			set{ _if_use=value;}
			get{return _if_use;}
		}
		#endregion Model

	}
}

