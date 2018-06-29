using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Link
{
    /// <summary>
    /// 实体类LinkCategoryModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class LinkCategoryModel
    {
        public LinkCategoryModel()
        { }
        #region Model
        private int _id;
        private string _category;
        private DateTime? _addeddate;
        private int? _sort;
        private int? _type;
        /// <summary>
        /// 编号id
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddedDate
        {
            set { _addeddate = value; }
            get { return _addeddate; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// ，类型
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion Model

    }
}
