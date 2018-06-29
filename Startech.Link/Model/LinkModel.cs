using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Link
{
    /// <summary>
    /// 实体类LinkModel 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class LinkModel
    {
        public LinkModel()
        { }
        #region Model
        private long _linkid;
        private string _title;
        private string _link;
        private string _image;
        private int? _displaymode;
        private int? _sort;
        private int? _categoryid;
        /// <summary>
        /// 编号id
        /// </summary>
        public long LinkId
        {
            set { _linkid = value; }
            get { return _linkid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Image
        {
            set { _image = value; }
            get { return _image; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int? DisplayMode
        {
            set { _displaymode = value; }
            get { return _displaymode; }
        }
        /// <summary>
        ///排序
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 类别id
        /// </summary>
        public int? CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        #endregion Model

    }
}
