using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Ad
{
    public class AdModel
    {
        //广告表
        public AdModel()
        { }

        #region Model
        private long _adid;
        private string _title;
        private string _link;
        private string _image;
        private int? _displaymode;
        private int? _sort;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private int? _categoryid;
        private string _video;
        private string _addperson;
        /// <summary>
        /// 
        /// </summary>
        public long AdId
        {
            set { _adid = value; }
            get { return _adid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Image
        {
            set { _image = value; }
            get { return _image; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DisplayMode
        {
            set { _displaymode = value; }
            get { return _displaymode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string video
        {
            set { _video = value; }
            get { return _video; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddPerson
        {
            set { _addperson = value; }
            get { return _addperson; }
        }
        #endregion Model
    }
}
