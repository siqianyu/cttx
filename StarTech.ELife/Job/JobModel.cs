/**  版本信息模板在安装目录下，可自行修改。
* Job.cs
*
* 功 能： N/A
* 类 名： Job
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-25 20:01:49   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace StarTech.ELife.Job
{
	/// <summary>
	/// Job:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class JobModel
	{
        public JobModel()
		{}
		#region Model
		private string _job_id;
		private string _job_name;
		private int? _if_leader;
		/// <summary>
		/// 岗位id
		/// </summary>
		public string job_id
		{
			set{ _job_id=value;}
			get{return _job_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string job_name
		{
			set{ _job_name=value;}
			get{return _job_name;}
		}
		/// <summary>
		/// 是否为第一岗位（1是、0否）
		/// </summary>
		public int? if_leader
		{
			set{ _if_leader=value;}
			get{return _if_leader;}
		}
		#endregion Model

	}
}

