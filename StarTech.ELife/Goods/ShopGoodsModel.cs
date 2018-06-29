using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Goods
{
    public class ShopGoodsModel
    {
        public ShopGoodsModel()
        { }
        #region Model
        private Guid _shopgoods_id;
        private string _shopid;
        private string _goodsid;
        private int? _shopgoods_amount;
        private decimal? _shopgoods_selfprice;
        private string _shopgoods_issell;
        private DateTime? _shopgoods_addtime;
        private string _goodscode;
        private int? _isformate = 0;
        private int? _salenum = 0;
        private int? _shopgoods_isset = 0;
        private int? _shopgoods_ispass = 0;
        /// <summary>
        /// 
        /// </summary>
        public Guid shopgoods_id
        {
            set { _shopgoods_id = value; }
            get { return _shopgoods_id; }
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
        public string goodsid
        {
            set { _goodsid = value; }
            get { return _goodsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? shopgoods_amount
        {
            set { _shopgoods_amount = value; }
            get { return _shopgoods_amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? shopgoods_selfPrice
        {
            set { _shopgoods_selfprice = value; }
            get { return _shopgoods_selfprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string shopgoods_isSell
        {
            set { _shopgoods_issell = value; }
            get { return _shopgoods_issell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? shopgoods_addtime
        {
            set { _shopgoods_addtime = value; }
            get { return _shopgoods_addtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string goodsCode
        {
            set { _goodscode = value; }
            get { return _goodscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isFormate
        {
            set { _isformate = value; }
            get { return _isformate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? saleNum
        {
            set { _salenum = value; }
            get { return _salenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? shopgoods_isSet
        {
            set { _shopgoods_isset = value; }
            get { return _shopgoods_isset; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? shopgoods_isPass
        {
            set { _shopgoods_ispass = value; }
            get { return _shopgoods_ispass; }
        }
        #endregion Model
    }

}
