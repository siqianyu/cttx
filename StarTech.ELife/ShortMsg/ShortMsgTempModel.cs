/**  版本信息模板在安装目录下，可自行修改。
* ShortMessage_Template.cs
*
* 功 能： N/A
* 类 名： ShortMessage_Template
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-26 17:33:07   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace StarTech.ELife.ShortMsg
{
	/// <summary>
	/// ShortMessage_Template:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShortMsgTempModel
	{
        public ShortMsgTempModel()
		{}
		#region Model
		private string _templateid;
		private string _flag;
		private string _templatecode;
		private string _templateparam;
		private string _supplierid;
		private string _supplierflag;
		private int? _isuse;
		/// <summary>
		/// 4位数Id
		/// </summary>
		public string templateId
		{
			set{ _templateid=value;}
			get{return _templateid;}
		}
		/// <summary>
		/// 标识 reg:注册 login:登陆
		/// </summary>
		public string flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 阿里云用此字段
		/// </summary>
		public string templateCode
		{
			set{ _templatecode=value;}
			get{return _templatecode;}
		}
		/// <summary>
		/// 其他接口用此字段
		/// </summary>
		public string templateParam
		{
			set{ _templateparam=value;}
			get{return _templateparam;}
		}
		/// <summary>
		/// 供应商ID
		/// </summary>
		public string supplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 供应商标识ali
		/// </summary>
		public string supplierFlag
		{
			set{ _supplierflag=value;}
			get{return _supplierflag;}
		}
		/// <summary>
		/// 是否启用 1、使用 0、禁用
		/// </summary>
		public int? isUse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		#endregion Model

	}
}

