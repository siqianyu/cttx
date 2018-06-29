using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarTech.ELife.Base
{
  public   class GoodsModel
    {
            public GoodsModel()
            { }
            #region Model
            private string _serviceid;
            private string _servicename;
            private string _servicecontext;
            private int? _orderby = 99;
            private string _remark;
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
            public string serviceName
            {
                set { _servicename = value; }
                get { return _servicename; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string serviceContext
            {
                set { _servicecontext = value; }
                get { return _servicecontext; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? orderby
            {
                set { _orderby = value; }
                get { return _orderby; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string remark
            {
                set { _remark = value; }
                get { return _remark; }
            }
            #endregion Model

        }
    }
