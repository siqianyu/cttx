using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Goods
{
    public class GoodsModel
    {
        private string _goodsid;
        private string _categoryid;
        private string _goodstotypeid;
        private string _brandid;
        private string _goodsname;
        private string _goodscode;
        private string _uint;
        private int? _weight;
        private string _goodssmallpic;
        private string _goodssampledesc;
        private string _goodsdesc;
        private string _goodsdesc2;
        private string _goodsdesc3;
        private string _goodsdesc4;
        private string _goodsdesc5;
        private int? _isrec;
        private int? _ishot;
        private int? _isnew;
        private int? _isspe;
        private decimal? _saleprice;
        private decimal? _marketprice;
        private decimal? _cbprice;
        private int? _issale;
        private int? _sotck;
        private int? _minsalenumber;
        private int? _maxsalenumber;
        private int? _orderby;
        private int? _viewcount;
        private int? _totalsalecount = 0;
        private string _remarks;
        private DateTime? _addtime;
        private string _areainfo;
        private string _bookinfo;
        private string _providerinfo;
        private string _datafrom;
        private string _morepropertys;
        private int? _ifsh;
        private string _shperson;
        private DateTime? _shtime;
        private string _shmark;
        private int? _isoldgoods = 0;
        private string _oldgoodslevel;
        private int? _salecount;
        private decimal? _postage;
        private int? _pingluncount = 0;
        private decimal? _vipprice1;
        private decimal? _vipprice2;
        private string _signid;
        private string _serviceId;
        private string _jobtype;
        private DateTime _jobstarttime;
        private DateTime _jobendtime;
        private string _jobaddress;
        private string _jobsquare;
        private string _jobbypersontype;


        public string ServiceId { get; set; }
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
        public string CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsToTypeId
        {
            set { _goodstotypeid = value; }
            get { return _goodstotypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BrandId
        {
            set { _brandid = value; }
            get { return _brandid; }
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
        public string GoodsCode
        {
            set { _goodscode = value; }
            get { return _goodscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Uint
        {
            set { _uint = value; }
            get { return _uint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSmallPic
        {
            set { _goodssmallpic = value; }
            get { return _goodssmallpic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSampleDesc
        {
            set { _goodssampledesc = value; }
            get { return _goodssampledesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsDesc
        {
            set { _goodsdesc = value; }
            get { return _goodsdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsDesc2
        {
            set { _goodsdesc2 = value; }
            get { return _goodsdesc2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsDesc3
        {
            set { _goodsdesc3 = value; }
            get { return _goodsdesc3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsDesc4
        {
            set { _goodsdesc4 = value; }
            get { return _goodsdesc4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsDesc5
        {
            set { _goodsdesc5 = value; }
            get { return _goodsdesc5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsRec
        {
            set { _isrec = value; }
            get { return _isrec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsHot
        {
            set { _ishot = value; }
            get { return _ishot; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsNew
        {
            set { _isnew = value; }
            get { return _isnew; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsSpe
        {
            set { _isspe = value; }
            get { return _isspe; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SalePrice
        {
            set { _saleprice = value; }
            get { return _saleprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MarketPrice
        {
            set { _marketprice = value; }
            get { return _marketprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CBPrice
        {
            set { _cbprice = value; }
            get { return _cbprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsSale
        {
            set { _issale = value; }
            get { return _issale; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sotck
        {
            set { _sotck = value; }
            get { return _sotck; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MinSaleNumber
        {
            set { _minsalenumber = value; }
            get { return _minsalenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MaxSaleNumber
        {
            set { _maxsalenumber = value; }
            get { return _maxsalenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Orderby
        {
            set { _orderby = value; }
            get { return _orderby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ViewCount
        {
            set { _viewcount = value; }
            get { return _viewcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TotalSaleCount
        {
            set { _totalsalecount = value; }
            get { return _totalsalecount; }
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
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaInfo
        {
            set { _areainfo = value; }
            get { return _areainfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BookInfo
        {
            set { _bookinfo = value; }
            get { return _bookinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProviderInfo
        {
            set { _providerinfo = value; }
            get { return _providerinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DataFrom
        {
            set { _datafrom = value; }
            get { return _datafrom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MorePropertys
        {
            set { _morepropertys = value; }
            get { return _morepropertys; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IfSh
        {
            set { _ifsh = value; }
            get { return _ifsh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShPerson
        {
            set { _shperson = value; }
            get { return _shperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ShTime
        {
            set { _shtime = value; }
            get { return _shtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShMark
        {
            set { _shmark = value; }
            get { return _shmark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsOldGoods
        {
            set { _isoldgoods = value; }
            get { return _isoldgoods; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldGoodsLevel
        {
            set { _oldgoodslevel = value; }
            get { return _oldgoodslevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? saleCount
        {
            set { _salecount = value; }
            get { return _salecount; }
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
        public int? PingLunCount
        {
            set { _pingluncount = value; }
            get { return _pingluncount; }
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
        /// <summary>
        /// 
        /// </summary>
        public string signId
        {
            set { _signid = value; }
            get { return _signid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string serviceId
        {
            set { _serviceId = value; }
            get { return _serviceId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JobType
        {
            set { _jobtype = value; }
            get { return _jobtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime JobStartTime
        {
            set { _jobstarttime = value; }
            get { return _jobstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime JobEndTime
        {
            set { _jobendtime = value; }
            get { return _jobendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JobAddress
        {
            set { _jobaddress = value; }
            get { return _jobaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JobSquare
        {
            set { _jobsquare = value; }
            get { return _jobsquare; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JobByPersonType
        {
            set { _jobbypersontype = value; }
            get { return _jobbypersontype; }
        }
    }
}
