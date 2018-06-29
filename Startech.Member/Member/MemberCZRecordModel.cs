using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Member.Member
{
    public class MemberCZRecordModel
    {
        public MemberCZRecordModel()
		{}
		#region Model
		private string _sysnumber;
		private int? _memberid;
		private decimal? _money;
		private string _moneytype;
		private string _remarks;
        private DateTime? _addtime;
		private string _addperson;
		private int? _shflag;
		private string _shperson;
		private DateTime? _shtime;
		/// <summary>
		/// Guid
		/// </summary>
		public string sysnumber
		{
			set{ _sysnumber=value;}
			get{return _sysnumber;}
		}
		/// <summary>
		/// 会员编号
		/// </summary>
		public int? memberId
		{
			set{ _memberid=value;}
			get{return _memberid;}
		}
		/// <summary>
		/// 充值金额
		/// </summary>
		public decimal? money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 充值类型：现金：XJ,赠送：ZS,消费券：XFQ
		/// </summary>
		public string moneyType
		{
			set{ _moneytype=value;}
			get{return _moneytype;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime? addTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 添加人
		/// </summary>
        public string addPerson
		{
			set{ _addperson=value;}
			get{return _addperson;}
		}
		/// <summary>
		/// 审核状态:1:审核，0:未审核
		/// </summary>
		public int? shFlag
		{
			set{ _shflag=value;}
			get{return _shflag;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string shPerson
		{
			set{ _shperson=value;}
			get{return _shperson;}
		}
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? shTime
		{
			set{ _shtime=value;}
			get{return _shtime;}
		}
		#endregion Model
    }
}
