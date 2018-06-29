using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;
using StarTech.Util;

namespace StarTech.Adapter
{
    public class IACenter
    {
        private AdoHelper adoHelper = AdoHelper.CreateHelper(AppConfig.DBInstance);

        /// <summary>
        /// 根据组id组名称
        /// </summary>
        public string GetGroupNameById(int groupId)
        {
            DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_Group where uniqueId=" + groupId + "");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0) { return dt.Rows[0]["groupName"].ToString(); }
            else { return ""; }
        }

        /// <summary>
        /// 根据菜单id获取菜单名称
        /// </summary>
        public string GetMenuNameById(int menuId)
        {
            DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_Menu where uniqueId=" + menuId + "");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0) { return dt.Rows[0]["menuName"].ToString(); }
            else { return ""; }
        }

        /// <summary>
        /// 根据菜单id获取菜单子类
        /// </summary>
        public DataSet GetSubMenuById(int menuId)
        {
            DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_Menu where parentMenuId=" + menuId + "  order by orderIndex asc");
            return ds;
        }


        /// <summary>
        /// 根据组返回所有菜单信息
        /// </summary>
        public DataSet GetAllMenusByGroupIds(string groupIds)
        {
            string sql = "SELECT * FROM IACenter_Menu WHERE (uniqueId IN (SELECT menuid FROM IACenter_GroupByMenu WHERE groupid IN (" + groupIds + ")))  order by orderIndex asc";
            return adoHelper.ExecuteSqlDataset(sql);
        }

        /// <summary>
        /// 根据用户id返回所在的所有组ids
        /// </summary>
        public string GetGroupIdsByUserId(int userId)
        {
            string ids = "";
            DataSet ds = adoHelper.ExecuteSqlDataset("select groupId from IACenter_UserInGroup where userId=" + userId + "");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ids += row["groupId"].ToString() + ",";
            }
            if (ids != "") { ids = ids.TrimEnd(','); }
            return ids;
        }

        /// <summary>
        /// 根据用户id返回所有菜单信息
        /// </summary>
        public DataSet GetAllMenusByUserId(int userId)
        {
            string groupIds = GetGroupIdsByUserId(userId);
            if (groupIds == "") { return null; }
            else { return GetAllMenusByGroupIds(groupIds); }
        }


        /// <summary>
        /// 根据用户id判断菜单权限
        /// </summary>
        public bool CheckIsMyMenu(int menuId, int userId)
        {
            string groupIds = GetGroupIdsByUserId(userId);
            if (groupIds == "") { return false; }
            string sql = "SELECT menuid FROM IACenter_GroupByMenu WHERE groupid IN (" + groupIds + ") and menuid=" + menuId + "";
            DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
            if (dt.Rows.Count > 0) { return true; }
            else { return false; }
        }

        /// <summary>
        /// 根据组id判断菜单权限
        /// </summary>
        public bool CheckIsGroupMenu(int menuId, int groupId)
        {
            string sql = "SELECT menuid FROM IACenter_GroupByMenu WHERE groupid =" + groupId + " and menuid=" + menuId + "";
            DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
            if (dt.Rows.Count > 0) { return true; }
            else { return false; }
        }

                
        /// <summary>
        /// 设置组菜单权限
        /// </summary>
        public int SetGroupMenu(int menuId, int groupId, bool add)
        {
            if (add == true)
            {
                if (CheckIsGroupMenu(menuId, groupId) == false)
                {
                    return adoHelper.ExecuteSqlNonQuery("insert into IACenter_GroupByMenu(groupId,menuId) values(" + groupId + "," + menuId + ")");
                } 
                return 1;
            }
            else
            {
                return adoHelper.ExecuteSqlNonQuery("delete IACenter_GroupByMenu WHERE groupid =" + groupId + " and menuid=" + menuId + "");
            }
        }


        /// <summary>
        /// 设置组按钮权限
        /// </summary>
        public int SetGroupButtons(int menuId, int groupId, string buttons, bool add)
        {
            if (add == true)
            {
                DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_GroupByMenuByButton WHERE groupid =" + groupId + " and menuid=" + menuId + "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return adoHelper.ExecuteSqlNonQuery("update IACenter_GroupByMenuByButton set buttonIds='" + buttons + "' WHERE groupid =" + groupId + " and menuid=" + menuId + "");
                }
                else
                {
                    return adoHelper.ExecuteSqlNonQuery("insert into IACenter_GroupByMenuByButton(groupId,menuId,buttonIds) values(" + groupId + "," + menuId + ",'" + buttons + "')");
                }
            }
            else
            {
                return adoHelper.ExecuteSqlNonQuery("delete IACenter_GroupByMenuByButton WHERE groupid =" + groupId + " and menuid=" + menuId + "");
            }
        }


        /// <summary>
        /// 获取当前系统定义的按钮
        /// </summary>
        public DataSet GetSystemButtons()
        {
            return adoHelper.ExecuteSqlDataset("select * from IACenter_Button");
        }


        /// <summary>
        /// 根据组和菜单返回所有按钮信息
        /// </summary>
        public string GetAllButtons(int menuId, string groupIds)
        {
            string ids = "";
            string sql = "select buttonIds from IACenter_GroupByMenuByButton where menuId=" + menuId + " and groupId in(" + groupIds + ")";
            DataSet ds = adoHelper.ExecuteSqlDataset(sql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ids += row["buttonIds"].ToString() + ",";
            }
            if (ids != "") 
            {
                ids = ids.TrimEnd(',');
                string[] idArr = ids.Split(',');
                string tmp = ",";
                foreach (string s in idArr)
                {
                    if (tmp.IndexOf("," + s + ",") == -1)
                    {
                        tmp += s + ",";
                    }
                }
                return tmp.TrimEnd(',').TrimStart(',');
            }
            else { return ""; }
        }

        /// <summary>
        /// 用户登陆(-1:username error,-2:password error,1:success)
        /// </summary>
        public int UserLogin(string username, string password, ref DataTable returnUserInfo)
        {
            DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_User where userName='" + username + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["password"].ToString() == password) { returnUserInfo = ds.Tables[0]; return 1; }
                else { return -2; }
            }
            else { return -1; }
        }

        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        public DataTable GetUserInfoByUserId(int userId)
        {
            DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_User where uniqueId=" + userId + "");
            return ds.Tables[0];
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        public int UpdateUserInfo(int userId, string updateDesc)
        {
            string sql = "update IACenter_User set " + updateDesc + " where uniqueId=" + userId + "";
            return adoHelper.ExecuteSqlNonQuery(sql);
        }

        /// <summary>
        /// 数据库通用操作
        /// </summary>
        public DataSet ExecuteSqlDataset(string sql)
        {
            return adoHelper.ExecuteSqlDataset(sql);
        }

        /// <summary>
        ///用户操作日志
        /// </summary>
        public void AddUserActionLog(string userName,string trueName,string menuNameLevel1, string menuNameLevel2, string menuNameLevel3, string actionType, string actionTicket, string remarks1, string remarks2,string ip,string url)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into IACenter_UserActionLog(");
            //strSql.Append("userName,userTrueName,menuNameLevel1,menuNameLevel2,menuNameLevel3,actionType,actionTicket,remarks1,remarks2,ip,url,addTime)");
            //strSql.Append(" values (");
            //strSql.Append(":userName,:userTrueName,:menuNameLevel1,:menuNameLevel2,:menuNameLevel3,:actionType,:actionTicket,:remarks1,:remarks2,:ip,:url,:addTime)");
            //OracleParameter[] parameters = {
            //        new OracleParameter(":userName", OracleType.VarChar,50),
            //        new OracleParameter(":userTrueName", OracleType.VarChar,50),
            //        new OracleParameter(":menuNameLevel1", OracleType.VarChar,50),
            //        new OracleParameter(":menuNameLevel2", OracleType.VarChar,50),
            //        new OracleParameter(":menuNameLevel3", OracleType.VarChar,50),
            //        new OracleParameter(":actionType", OracleType.VarChar,50),
            //        new OracleParameter(":actionTicket", OracleType.VarChar,50),
            //        new OracleParameter(":remarks1", OracleType.VarChar,8000),
            //        new OracleParameter(":remarks2", OracleType.Clob),
            //        new OracleParameter(":ip", OracleType.VarChar,50),
            //        new OracleParameter(":url", OracleType.VarChar,500),
            //        new OracleParameter(":addTime", OracleType.DateTime)};
            //parameters[0].Value = userName;
            //parameters[1].Value = trueName;
            //parameters[2].Value = menuNameLevel1;
            //parameters[3].Value = menuNameLevel2;
            //parameters[4].Value = menuNameLevel3;
            //parameters[5].Value = actionType;
            //parameters[6].Value = actionTicket;
            //parameters[7].Value = (remarks1 == "") ? "无" : remarks1;
            //parameters[8].Value = (remarks2 == "") ? "无" : remarks2;
            //parameters[9].Value = ip;
            //parameters[10].Value = url;
            //parameters[11].Value = DateTime.Now;
            //adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
        }
    }
}
