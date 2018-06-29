using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.Order
{
  public class ModOrderDetail
    {
        private string _sysnumber;
        private string _orderid;
        private string _ordertype;
        private string _goodsid;
        private string _goodscode;
        private string _goodsname;
        private string _goodsformate;
        private string _goodspic;
        private int _quantity=0;
        private decimal _price;
        private decimal _allmoney;
        private int _oneweight=0;
        private int _allweight=0;
        private decimal _marketprice=0;
        private decimal _cbprice=0;
        private string _remarks;
      private decimal _FreightByWeight;
      private string _ProviderInfo;
      private string _datafrom;
        /// <summary>
        /// 
        /// </summary>
        public string sysnumber
        {
            set { _sysnumber = value; }
            get { return (_sysnumber == null) ? Guid.NewGuid().ToString() : _sysnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
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
        public string GoodsName
        {
            set { _goodsname = value; }
            get { return _goodsname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsFormate
        {
            set { _goodsformate = value; }
            get { return _goodsformate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsPic
        {
            set { _goodspic = value; }
            get { return _goodspic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AllMoney
        {
            set { _allmoney = value; }
            get { return _allmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OneWeight
        {
            set { _oneweight = value; }
            get { return _oneweight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AllWeight
        {
            set { _allweight = value; }
            get { return _allweight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MarketPrice
        {
            set { _marketprice = value; }
            get { return _marketprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CBPrice
        {
            set { _cbprice = value; }
            get { return _cbprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 
        /// </summary>
      public decimal FreightByWeight
        {
            set { _FreightByWeight = value; }
            get { return _FreightByWeight; }
        }
      public string ProviderInfo
      {
          set { _ProviderInfo = value; }
          get { return _ProviderInfo; }
      }
      public string DataFrom
      {
          set { _datafrom = value; }
          get { return _datafrom; }
      }

    }
}
