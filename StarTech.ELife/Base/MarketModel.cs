using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Base
{
    public class MarketModel
    {
        //农贸市场基础表
        public MarketModel()
        { }

        #region Model
        private string _market_id;
        private string _market_name;
        private string _area_id;
        private string _map_x;
        private string _map_y;
        private int? _orderby;
        /// <summary>
        /// 农贸市场编号（6位唯一编号组成，非自增）
        /// </summary>
        public string Market_id
        {
            set { _market_id = value; }
            get { return _market_id; }
        }
        /// <summary>
        /// 农贸市场名称
        /// </summary>
        public string Market_name
        {
            set { _market_name = value; }
            get { return _market_name; }
        }
        /// <summary>
        /// 所属区域编号
        /// </summary>
        public string Area_id
        {
            set { _area_id = value; }
            get { return _area_id; }
        }
        /// <summary>
        /// X轴坐标（经度）
        /// </summary>
        public string Map_x
        {
            set { _map_x = value; }
            get { return _map_x; }
        }
        /// <summary>
        /// Y轴坐标（纬度）
        /// </summary>
        public string Map_y
        {
            set { _map_y = value; }
            get { return _map_y; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int? orderby
        {
            set { _orderby = value; }
            get { return _orderby; }
        }
        #endregion Model
    }
}
