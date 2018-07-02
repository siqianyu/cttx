/**  版本信息模板在安装目录下，可自行修改。
* ShortMessage_Log.cs
*
* 功 能： N/A
* 类 名： ShortMessage_Log
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018-06-25 21:38:53   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace StarTech.ELife.ShortMsg
{
    /// <summary>
    /// ShortMessage_Log:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ShortMsgModel
    {
        public ShortMsgModel()
        { }
        #region Model
        private string _sysnumber;
        private string _tel;
        private string _yzm;
        private string _statu;
        private DateTime? _sendtime;
        private DateTime? _outsendtime;
        private string _remark;
        private string _template;
        private string _smssign;
        private string _result;
        private string _sendtext;
        private string _supplier;

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
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string yzm
        {
            set { _yzm = value; }
            get { return _yzm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string statu
        {
            set { _statu = value; }
            get { return _statu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? sendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? outSendTime
        {
            set { _outsendtime = value; }
            get { return _outsendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 阿里云模板
        /// </summary>
        public string template
        {
            set { _template = value; }
            get { return _template; }
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string smsSign
        {
            set { _smssign = value; }
            get { return _smssign; }
        }
        /// <summary>
        /// 返回结果
        /// </summary>
        public string result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string sendText
        {
            set { _sendtext = value; }
            get { return _sendtext; }
        }
        /// <summary>
        /// 短信提供商
        /// </summary>
        public string supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        public string flag;
        public string accessKeyID;
        public string accessKeySecret;
        public string templateParam;
        
        #endregion Model


    }
}

