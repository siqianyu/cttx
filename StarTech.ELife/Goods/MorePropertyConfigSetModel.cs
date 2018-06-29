using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.ELife.Goods
{
   public class MorePropertyConfigSetModel
    {
        #region Model
       private string _propertyid;
        private string _tablename;
        private string _morefiledname;
        private string _propertyname;
        private string _porpertyflag;
        private string _propertyoptions;
        private int _orderby;
        private string _remarks;
        /// <summary>
        /// 
        /// </summary>
        public string propertyId
        {
            set { _propertyid = value; }
            get { return _propertyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string moreFiledName
        {
            set { _morefiledname = value; }
            get { return _morefiledname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string propertyName
        {
            set { _propertyname = value; }
            get { return _propertyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string porpertyFlag
        {
            set { _porpertyflag = value; }
            get { return _porpertyflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string propertyOptions
        {
            set { _propertyoptions = value; }
            get { return _propertyoptions; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int orderBy
        {
            set { _orderby = value; }
            get { return _orderby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model
    }
}
