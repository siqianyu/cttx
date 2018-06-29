using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Menu
{
    public class MenuModel
    {
        private string _menuid;
        private string _menuname;
        private string _technology;
        private string _flavor;
        private string _cookingtime;
        private decimal? _calorie;
        private string _cookingskill;
        private string _userid;
        private DateTime? _addtime;
        private int? _isshow;
        private string _imgsrc;
        private string _smallimgsrc;
        private string _categoryId;
        //private string _remark;

        //public string remark
        //{
        //    set { _remark = value; }
        //    get { return _remark; }
        //}

        public int isTop { get; set; }
        public string signId { get; set; }
       

        public string categoryId
        {
            set { _categoryId = value; }
            get { return _categoryId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string menuId
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuName
        {
            set { _menuname = value; }
            get { return _menuname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Technology
        {
            set { _technology = value; }
            get { return _technology; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Flavor
        {
            set { _flavor = value; }
            get { return _flavor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CookingTime
        {
            set { _cookingtime = value; }
            get { return _cookingtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Calorie
        {
            set { _calorie = value; }
            get { return _calorie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CookingSkill
        {
            set { _cookingskill = value; }
            get { return _cookingskill; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isShow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string imgSrc
        {
            set { _imgsrc = value; }
            get { return _imgsrc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string smallImgSrc
        {
            set { _smallimgsrc = value; }
            get { return _smallimgsrc; }
        }
    }
}
