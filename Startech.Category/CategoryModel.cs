using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Category
{
    /// <summary>
    /// 实体类T_Category 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class CategoryModel
    {
        public CategoryModel()
        { }
        #region Model
        private int _categoryid;
        private string _categoryname;
        private int? _sort;
        private int? _type;
        private int? _parentcategoryid;
        private int? _addeduserid;
        private DateTime? _addeddate;
        private string _url;
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentCategoryId
        {
            set { _parentcategoryid = value; }
            get { return _parentcategoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AddedUserId
        {
            set { _addeduserid = value; }
            get { return _addeduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddedDate
        {
            set { _addeddate = value; }
            get { return _addeddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        #endregion Model

    }
}
