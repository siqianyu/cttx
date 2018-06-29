<%@ WebHandler Language="C#" Class="LoginDemo" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Data;
using StarTech.DBUtility;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web.DynamicData;

/// <summary>
/// 基于session验证类，防止越权访问和读取订单、视频等数据
/// </summary>
/// 
public class LoginDemo : IHttpHandler, IRequiresSessionState
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string flag = Common.NullToEmpty(context.Request["flag"]);
        string phone = Common.NullToEmpty(context.Request["phone"]);
        string wxopenid = Common.NullToEmpty(context.Request["wxopenid"]);
        string pwd = Common.NullToEmpty(context.Request["pwd"]);

        switch (flag.ToLower())
        {
            case "user_login":
                context.Response.Write(Login(phone, pwd, context));
                break;
        }
    }
    private string Login(string phone, string pwd, HttpContext context)
    {
        pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
        string strSql = @"select * from T_Member_Info where MemberName=@tel and Password=@Pwd";
        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@Pwd",pwd),
            new SqlParameter("@tel",phone)
        };
        DataTable dt = ado.ExecuteSqlDataset(strSql, p).Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["isUse"].ToString() == "0") { return "该账号已被禁用"; }
            context.Session["MemberId"] = dt.Rows[0]["MemberId"].ToString();
            return "1";
        }
        else
        {
            return "用户名或密码错误";
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}