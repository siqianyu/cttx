using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Base
{
    public class BuildingToMarketModel
    {
        //小区对应农贸市场关系表
        public BuildingToMarketModel()
        { }

        #region Model
        private string _buildingtomarket_id;
        private string _building_id;
        private string _market_id;
        private int? _orderby;
        private decimal? _minprice;
        private decimal? _maxprice;
        private decimal? _price;
        private int? _distance;
        /// <summary>
        /// Guid唯一编号
        /// </summary>
        public string BuildingToMarket_id
        {
            set { _buildingtomarket_id = value; }
            get { return _buildingtomarket_id; }
        }
        /// <summary>
        /// 小区编号（8位唯一编号组成，非自增）
        /// </summary>
        public string Building_id
        {
            set { _building_id = value; }
            get { return _building_id; }
        }
        /// <summary>
        /// 农贸市场编号（6位唯一编号组成，非自增）
        /// </summary>
        public string Market_id
        {
            set { _market_id = value; }
            get { return _market_id; }
        }
        /// <summary>
        /// 排序（排序越小，推荐度越高）
        /// </summary>
        public int? orderby
        {
            set { _orderby = value; }
            get { return _orderby; }
        }
        /// <summary>
        /// 起步价
        /// </summary>
        public decimal? MinPrice
        {
            set { _minprice = value; }
            get { return _minprice; }
        }
        /// <summary>
        /// 封顶价
        /// </summary>
        public decimal? MaxPrice
        {
            set { _maxprice = value; }
            get { return _maxprice; }
        }
        /// <summary>
        /// 每500g单价
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 小区到农贸市场的距离
        /// </summary>
        public int? Distance
        {
            set { _distance = value; }
            get { return _distance; }
        }
        #endregion Model

    }
}
