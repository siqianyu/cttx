using System;
using System.Collections.Generic;
using System.Text;

namespace StarTech.Order.Order
{
    public class ModOrder
    {
        #region Model
        private string _orderid;
        private string _ordertype;
        private string _memberid;
        private string _membername;
        private DateTime _ordertime = DateTime.Now;
        private decimal _orderallmoney = 0;
        private decimal _goodsallmoney = 0;
        private decimal _freight = 0;
        private decimal _ticketmoney = 0;
        private decimal _discountmoeny = 0;
        private decimal _tempchangemoney = 0;
        private string _receiveperson;
        private string _receiveaddresscode;
        private string _receiveaddressdetail;
        private string _postcode;
        private string _email;
        private string _tel;
        private string _mobile;
        private string _memberorderremarks;
        private string _adminorderremarks;
        private int _orderallweight = 0;
        private int _ispay = 0;
        private DateTime _paytime = DateTime.Parse("1900-01-01");
        private string _payinterfacecode;
        private string _paymethod;
        private int _issend = 0;
        private DateTime _sendtime = DateTime.Parse("1900-01-01");
        private string _sendnumber;
        private string _sendcompany;
        private string _sendmethod;
        private string _sendmethodbyreal;
        private int _isget = 0;
        private DateTime _gettime = DateTime.Parse("1900-01-01");
        private int _ismembercancel = 0;
        private string _membercancelremarks;
        private int _iscomplete = 0;
        private DateTime _completetime = DateTime.Parse("1900-01-01");
        private int _isinvoice = 0;
        private string _invoicecode;
        private string _invoicetype1;
        private string _invoicetype2;
        private string _invoicetitle;
        private string _invoiceremarks;
        private int _ischeckifrealorder = 0;
        private DateTime _checkifrealordertime = DateTime.Parse("1900-01-01");
        private string _checkifrealorderperson;
        private string _checkifrealorderremarks;
        private DateTime zttime = DateTime.Parse("1900-01-01");
        private DateTime zttimeEnd = DateTime.Parse("1900-01-01");
        private int _iscomment;
        private DateTime _commenttime = DateTime.Parse("1900-01-01");
        private DateTime disTime=DateTime.Parse("1900-1-1-1");
	    private string _priceId;

		public string priceId { get; set; }

		public string building_id { get; set; }
        public int isDis { get; set; }

        public DateTime DisTime { get { return disTime; } set { disTime = value; } }

        public string marketId { get; set; }

        public string couponId { get; set; }

        public decimal oldMoney { get; set; }

        public DateTime ztTime { 
            get { return zttime; } 
            set { zttime = value; } 
        }

        public DateTime CommentTime
        {
            get { return _commenttime; }
            set { _commenttime = value; }
        }

        public int isComment
        {
            get { return _iscomment;}
            set {_iscomment=value;}
        }

        public DateTime ztTimeEnd
        {
            get { return zttimeEnd; }
            set { zttimeEnd = value; }
        }

        public string SellerId { get; set; }
        public string SellerType { get; set; }
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
        public string MemberId
        {
            set { _memberid = value; }
            get { return _memberid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MemberName
        {
            set { _membername = value; }
            get { return _membername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrderAllMoney
        {
            set { _orderallmoney = value; }
            get { return _orderallmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodsAllMoney
        {
            set { _goodsallmoney = value; }
            get { return _goodsallmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Freight
        {
            set { _freight = value; }
            get { return _freight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TicketMoney
        {
            set { _ticketmoney = value; }
            get { return _ticketmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DiscountMoeny
        {
            set { _discountmoeny = value; }
            get { return _discountmoeny; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TempChangeMoney
        {
            set { _tempchangemoney = value; }
            get { return _tempchangemoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceivePerson
        {
            set { _receiveperson = value; }
            get { return _receiveperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveAddressCode
        {
            set { _receiveaddresscode = value; }
            get { return _receiveaddresscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveAddressDetail
        {
            set { _receiveaddressdetail = value; }
            get { return _receiveaddressdetail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MemberOrderRemarks
        {
            set { _memberorderremarks = value; }
            get { return _memberorderremarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdminOrderRemarks
        {
            set { _adminorderremarks = value; }
            get { return _adminorderremarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrderAllWeight
        {
            set { _orderallweight = value; }
            get { return _orderallweight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsPay
        {
            set { _ispay = value; }
            get { return _ispay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PayTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayInterfaceCode
        {
            set { _payinterfacecode = value; }
            get { return _payinterfacecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayMethod
        {
            set { _paymethod = value; }
            get { return _paymethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsSend
        {
            set { _issend = value; }
            get { return _issend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendNumber
        {
            set { _sendnumber = value; }
            get { return _sendnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendCompany
        {
            set { _sendcompany = value; }
            get { return _sendcompany; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendMethod
        {
            set { _sendmethod = value; }
            get { return _sendmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendMethodByReal
        {
            set { _sendmethodbyreal = value; }
            get { return _sendmethodbyreal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsGet
        {
            set { _isget = value; }
            get { return _isget; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime GetTime
        {
            set { _gettime = value; }
            get { return _gettime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsMemberCancel
        {
            set { _ismembercancel = value; }
            get { return _ismembercancel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MemberCancelRemarks
        {
            set { _membercancelremarks = value; }
            get { return _membercancelremarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsComplete
        {
            set { _iscomplete = value; }
            get { return _iscomplete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CompleteTime
        {
            set { _completetime = value; }
            get { return _completetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsInvoice
        {
            set { _isinvoice = value; }
            get { return _isinvoice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceCode
        {
            set { _invoicecode = value; }
            get { return _invoicecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceType1
        {
            set { _invoicetype1 = value; }
            get { return _invoicetype1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceType2
        {
            set { _invoicetype2 = value; }
            get { return _invoicetype2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceTitle
        {
            set { _invoicetitle = value; }
            get { return _invoicetitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceRemarks
        {
            set { _invoiceremarks = value; }
            get { return _invoiceremarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsCheckIfRealOrder
        {
            set { _ischeckifrealorder = value; }
            get { return _ischeckifrealorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CheckIfRealOrderTime
        {
            set { _checkifrealordertime = value; }
            get { return _checkifrealordertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckIfRealOrderPerson
        {
            set { _checkifrealorderperson = value; }
            get { return _checkifrealorderperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckIfRealOrderRemarks
        {
            set { _checkifrealorderremarks = value; }
            get { return _checkifrealorderremarks; }
        }
        #endregion Model
    }
}
