using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Base
{
    public class AreaModel
    {
        //区域基础表
        public AreaModel()
        { }

        #region Model
        private string _area_id;
        private string _area_name;
        private string _area_pid;
        private int? _area_level;
        private int? _orderby;
        /// <summary>
        /// 区域编号
        /// </summary>
        public string area_id
        {
            set { _area_id = value; }
            get { return _area_id; }
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string area_name
        {
            set { _area_name = value; }
            get { return _area_name; }
        }
        /// <summary>
        /// 区域父级编号
        /// </summary>
        public string area_pid
        {
            set { _area_pid = value; }
            get { return _area_pid; }
        }
        /// <summary>
        /// 所属层级（西湖区：3
        /// </summary>
        public int? area_level
        {
            set { _area_level = value; }
            get { return _area_level; }
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
