using System;
using System.Collections.Generic;
using System.Text;

namespace Startech.Member
{
    public class ModPostman
    {
        public ModPostman()
        { }
        #region Model
        private int _postman_id;
        private string _postman_username;
        private string _postman_pwd;
        private string _postman_truename;
        private string _postman_headimg;
        private string _postman_tel;
        private string _postman_marketid;
        private string _postman_deliverbuildingid;
        private int? _postman_score;
        private string _postman_status;
        private DateTime? _postman_addtime;
        /// <summary>
        /// 
        /// </summary>
        public int postman_id
        {
            set { _postman_id = value; }
            get { return _postman_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postman_username
        {
            set { _postman_username = value; }
            get { return _postman_username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postman_pwd
        {
            set { _postman_pwd = value; }
            get { return _postman_pwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postman_trueName
        {
            set { _postman_truename = value; }
            get { return _postman_truename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Postman_headImg
        {
            set { _postman_headimg = value; }
            get { return _postman_headimg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postman_tel
        {
            set { _postman_tel = value; }
            get { return _postman_tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postman_marketId
        {
            set { _postman_marketid = value; }
            get { return _postman_marketid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postman_deliverBuildingId
        {
            set { _postman_deliverbuildingid = value; }
            get { return _postman_deliverbuildingid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? postman_score
        {
            set { _postman_score = value; }
            get { return _postman_score; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string postman_status
        {
            set { _postman_status = value; }
            get { return _postman_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? postman_addtime
        {
            set { _postman_addtime = value; }
            get { return _postman_addtime; }
        }
        #endregion Model

    }
}
