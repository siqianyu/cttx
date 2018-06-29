using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Member
{
    public class ModMoneyApply
    {
        public ModMoneyApply()
        {
        }

        #region Model
        private int _apply_id;
        private string _shopid;
        private string _marketid;
        private string _apply_name;
        private decimal? _apply_money;
        private string _apply_ispost;
        private DateTime? _apply_posttime;
        private string _apply_postcheck;
        private DateTime? _apply_postchecktime;
        private string _apply_isback;
        private DateTime? _apply_backtime;
        private string _apply_backcheck;
        private DateTime? _apply_backchecktime;
        private string _apply_mark;
        /// <summary>
        /// 
        /// </summary>
        public int apply_id
        {
            set { _apply_id = value; }
            get { return _apply_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string shopid
        {
            set { _shopid = value; }
            get { return _shopid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string marketid
        {
            set { _marketid = value; }
            get { return _marketid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string apply_name
        {
            set { _apply_name = value; }
            get { return _apply_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? apply_money
        {
            set { _apply_money = value; }
            get { return _apply_money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string apply_ispost
        {
            set { _apply_ispost = value; }
            get { return _apply_ispost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? apply_posttime
        {
            set { _apply_posttime = value; }
            get { return _apply_posttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string apply_postcheck
        {
            set { _apply_postcheck = value; }
            get { return _apply_postcheck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? apply_postchecktime
        {
            set { _apply_postchecktime = value; }
            get { return _apply_postchecktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string apply_isback
        {
            set { _apply_isback = value; }
            get { return _apply_isback; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? apply_backtime
        {
            set { _apply_backtime = value; }
            get { return _apply_backtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string apply_backcheck
        {
            set { _apply_backcheck = value; }
            get { return _apply_backcheck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? apply_backchecktime
        {
            set { _apply_backchecktime = value; }
            get { return _apply_backchecktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string apply_mark
        {
            set { _apply_mark = value; }
            get { return _apply_mark; }
        }
        #endregion Model
    }
}
