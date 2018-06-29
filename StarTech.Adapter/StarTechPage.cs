using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace StarTech.Adapter
{
    /// <summary>
    /// 政府基类
    /// </summary>
    public class StarTechPage: System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.UserCheck();
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnError(EventArgs e)
        {
            StarTechPage_Error();
            base.OnError(e);
        }

        protected void StarTechPage_Error()
        {

            //得到系统上一个异常
            Exception currentError = Server.GetLastError();
            string errMsg = "==========================<br>#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br>==========================<br>";
            errMsg += "#错误发生位置： " + Request.Url.ToString() + "<br>" +
                "#错误消息： " + currentError.Message.ToString() + "<br>" +
                "#Stack Trace：<br>" +
                currentError.ToString() +
                "<br><br><br>";
            //在页面中显示错误
            //Response.Write(errMsg);
            WriteLog(errMsg);
            Response.Write("<div style='font-size:12px'>正在处理中,请稍等...</div><div style='display:none'>" + errMsg + "</div>");
            //new IACenter().AddUserActionLog("system", "系统", "", "", "", "程序错误", "", "", errMsg.Replace("\r\n", "<br>"), HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.ToString());

            //清除异常
            Server.ClearError();
        }

        public void WriteLog(string text)
        {
            string mailText = text;
            string fileName = DateTime.Now.ToString("yyyyMMdd") + "log.htm";
            string dir = Server.MapPath("~/log/");
            if (!System.IO.Directory.Exists(dir)) { System.IO.Directory.CreateDirectory(dir); }
            string path = dir + fileName;
            if (!System.IO.File.Exists(path))
            {
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
                }
            }
            System.IO.StreamWriter sr = System.IO.File.AppendText(path);
            sr.WriteLine(mailText);
            sr.Close();
        }

        protected void UserCheck()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == "")
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }


        /// <summary>
        /// 菜单权限
        /// </summary>
        public void CheckMenuPermissions(int menuId)
        {
            bool r = new IACenter().CheckIsMyMenu(menuId, Int32.Parse(this.UserId));
            if (r == false) { Response.Redirect("/Login.aspx", true); }
        }



        #region 用户属性
        public string UserId
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("UserId") == true) ? hTable["UserId"].ToString() : "";
            }
        }

        public string UserName
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("UserName") == true) ? hTable["UserName"].ToString() : "";
            }
        }

        public string TrueName
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("TrueName") == true) ? hTable["TrueName"].ToString() : "";
            }
        }

        public string UserType
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("UserType") == true) ? hTable["UserType"].ToString() : "";
            }
        }

        public string DepartCode
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("DepartCode") == true) ? hTable["DepartCode"].ToString() : "";
            }
        }

        public string DepartName
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("DepartName") == true) ? hTable["DepartName"].ToString() : "";
            }
        }

        public string AreaCode
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("AreaCode") == true) ? hTable["AreaCode"].ToString() : "";
            }
        }

        public string AreaName
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("AreaName") == true) ? hTable["AreaName"].ToString() : "";
            }
        }

        /// <summary>
        /// 是否为领导，领导可以查看所有的数据（前提是菜单权限，相对于部门而言）
        /// </summary>
        public bool IsLeader
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                if (hTable.Contains("IsLeader") == true)
                {
                    return (hTable["IsLeader"].ToString() == "1") ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 是否为超级管理员，超级管理员可以查看所有的数据（前提菜单权限，相对于部门而言）
        /// </summary>
        public bool IsSuperAdmin
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                if (hTable.Contains("IsSuperAdmin") == true)
                {
                    return (hTable["IsSuperAdmin"].ToString() == "1") ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 分页数量
        /// </summary>
        public string PageSize
        {
            get
            {
                Hashtable hTable = this.GetUserInfo();
                return (hTable.Contains("PageSize") == true) ? hTable["PageSize"].ToString() : "15";
            }
        }


        protected Hashtable GetUserInfo()
        {
            Hashtable hTable = new Hashtable();
            if (Session["UserInfo"] != null)
            {
                hTable = (Hashtable)Session["UserInfo"];
            }
            else
            {

                DataTable dtUser = new IACenter().GetUserInfoByUserId(int.Parse(Session["UserId"].ToString()));
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    hTable.Add("UserId", dtUser.Rows[0]["uniqueId"].ToString());
                    hTable.Add("UserName", dtUser.Rows[0]["userName"].ToString());
                    hTable.Add("TrueName", dtUser.Rows[0]["trueName"].ToString());
                    hTable.Add("DepartCode", dtUser.Rows[0]["departId"].ToString());
                    hTable.Add("DepartName", dtUser.Rows[0]["departId"].ToString());
                }
                Session["UserInfo"] = hTable;
            }
            return hTable;
        }
        #endregion

        //编辑框只读界面样式(jquery)
        public void SetReadOnlyPanel(string style)
        {
            string js = "<script>$(document).ready(function(){";
            js += "$('input[@type=text]').each(function(){$(this).attr('className','" + style + "');$(this).attr('readonly','readonly');});";
            js += "$('textarea').each(function(){$(this).css('border','0px');$(this).attr('readonly','readonly');});";
            js += "$('select').each(function(){$(this).attr('disabled','disabled');});";

            js += "});</script>";
            ClientScript.RegisterStartupScript(this.GetType(), this.GetType().ToString(), js);
        }

        //编辑框只读界面样式(jquery)
        public void SetReadOnlyPanel()
        {
            string js = "<script>$(document).ready(function(){";
            js += "$('input[@type=text]').each(function(){$(this).css('border','0px');$(this).attr('readonly','readonly');});";
            js += "$('textarea').each(function(){$(this).css('border','0px');$(this).attr('readonly','readonly');});";
            js += "$('select').each(function(){$(this).attr('disabled','disabled');});";
            js += "$('input[@type=radio]').each(function(){$(this).attr('disabled','disabled');});";
            js += "});</script>";
            ClientScript.RegisterStartupScript(this.GetType(), this.GetType().ToString(), js);
        }


        #region 菜单、按钮权限管理
        /// <summary>
        /// 根据组id组名称
        /// </summary>
        public string GetGroupNameById(int groupId)
        {
            return new IACenter().GetGroupNameById(groupId);
        }


        /// <summary>
        /// 根据组返回所有菜单信息
        /// </summary>
        public DataSet GetAllMenusByGroupIds(string groupIds)
        {
            return new IACenter().GetAllMenusByGroupIds(groupIds);
        }

        /// <summary>
        /// 根据用户id返回所在的所有组ids
        /// </summary>
        public string GetGroupIdsByUserId(int userId)
        {
            return new IACenter().GetGroupIdsByUserId(userId);
        }

        /// <summary>
        /// 根据用户id返回所有菜单信息
        /// </summary>
        public DataSet GetAllMenusByUserId(int userId)
        {
            return new IACenter().GetAllMenusByUserId(userId);
        }


        /// <summary>
        /// 根据用户id判断菜单权限
        /// </summary>
        public bool CheckIsMyMenu(int menuId, int userId)
        {
            return new IACenter().CheckIsMyMenu(menuId, userId);
        }


        /// <summary>
        /// 根据组和菜单返回所有按钮信息
        /// </summary>
        public string GetAllButtons(int menuId, string groupIds)
        {
            return new IACenter().GetAllButtons(menuId, groupIds);
        }
                
        /// <summary>
        /// 根据菜单id获取菜单子类
        /// </summary>
        public DataSet GetSubMenuById(int menuId)
        {
            return new IACenter().GetSubMenuById(menuId);
        }

        //按钮权限
        public bool ValidateAccess(string code)
        {
            return true;
        }

        //用户操作日志
        public void AddUserActionLog(string menuNameLevel1, string menuNameLevel2, string menuNameLevel3, string actionType, string actionTicket, string remarks1, string remarks2)
        {
            new IACenter().AddUserActionLog(this.UserName, this.TrueName, menuNameLevel1, menuNameLevel2, menuNameLevel3, actionType, actionTicket, remarks1, remarks2, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.ToString());
        }
        #endregion

        /// <summary>
        /// 获取当前用户相应的首页
        /// </summary>
        public string GetMyIndexPage()
        {
           DataSet ds= new IACenter().ExecuteSqlDataset("select * from t_platform_base_depart where trim(departcode)='" + this.DepartCode.Trim() + "'");
           if (ds.Tables[0].Rows.Count > 0)
           {
               return ds.Tables[0].Rows[0]["indexpagename"].ToString().Trim() == "" ? "Index_FJ.aspx" : ds.Tables[0].Rows[0]["indexpagename"].ToString();
           }
           else { return "Index_FJ.aspx"; }
        }
    }
}
