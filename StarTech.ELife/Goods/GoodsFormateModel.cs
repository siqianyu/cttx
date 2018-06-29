using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Goods
{
    public class GoodsFormateModel
    {
        private string _sysnumber = "newid";
        private string _goodsid;
        private string _goodscode;
        private string _goodsformatenames;
        private string _goodsformatevalues;
        private decimal? _price;
        private int? _stock;
        private decimal? _postage;
        private decimal? _vipprice1;
        private decimal? _vipprice2;
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
        public string GoodsId
        {
            set { _goodsid = value; }
            get { return _goodsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsCode
        {
            set { _goodscode = value; }
            get { return _goodscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsFormateNames
        {
            set { _goodsformatenames = value; }
            get { return _goodsformatenames; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsFormateValues
        {
            set { _goodsformatevalues = value; }
            get { return _goodsformatevalues; }
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
        public int? Stock
        {
            set { _stock = value; }
            get { return _stock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Postage
        {
            set { _postage = value; }
            get { return _postage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? vipPrice1
        {
            set { _vipprice1 = value; }
            get { return _vipprice1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? vipPrice2
        {
            set { _vipprice2 = value; }
            get { return _vipprice2; }
        }
    }
}
