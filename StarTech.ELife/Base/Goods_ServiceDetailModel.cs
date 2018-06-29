using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Base
{
    public class Goods_ServiceDetailModel
    {
        public Goods_ServiceDetailModel()
        { }
        #region Model
        private string _sysnumber;
        private string _serviceid;
        private string _value;
        private decimal? _price;
        private string _remark;
        private int _isDefault;
        /// <summary>
        /// 
        /// </summary>
        public string sysnumber
        {
            set { _sysnumber = value; }
            get { return _sysnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string serviceId
        {
            set { _serviceid = value; }
            get { return _serviceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }  
        public int IsDefault
        {
            set { _isDefault = value; }
            get { return _isDefault; }
        }
        #endregion Model
      
    }
}
