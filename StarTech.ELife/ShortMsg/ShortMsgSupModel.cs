/**  版本信息模板在安装目录下，可自行修改。
* ShortMessage_Supplier.cs
*
* 功 能： N/A
* 类 名： ShortMessage_Supplier
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-26 17:33:07   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
namespace StarTech.ELife.ShortMsg
{
    /// <summary>
    /// ShortMessage_Supplier:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ShortMsgSupModel
    {
        public ShortMsgSupModel()
        { }
        #region Model
        private string _supplierid;
        private string _flag;
        private string _suppliername;
        private string _smssign;
        private string _accesskeyid;
        private string _accesskeysecret;
        private int? _isuse;
        private int? _isdefault;
        private int? _sort;
        /// <summary>
        /// 4位数
        /// </summary>
        public string supplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 标识 ali
        /// </summary>
        public string flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 提供商名称
        /// </summary>
        public string supplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 签名 ：远鉴才通
        /// </summary>
        public string smsSign
        {
            set { _smssign = value; }
            get { return _smssign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string accessKeyID
        {
            set { _accesskeyid = value; }
            get { return _accesskeyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string accessKeySecret
        {
            set { _accesskeysecret = value; }
            get { return _accesskeysecret; }
        }
        /// <summary>
        /// 是否启用 1、使用 0、禁用
        /// </summary>
        public int? isUse
        {
            set { _isuse = value; }
            get { return _isuse; }
        }
        /// <summary>
        /// 是否默认 1、是 0、否
        /// </summary>
        public int? isDefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }
        public int? sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        public List<ShortMsgTempModel> tempList = new List<ShortMsgTempModel>();
        #endregion Model

    }
}

