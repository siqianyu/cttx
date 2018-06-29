<%@ WebHandler Language="C#" Class="YqxkjUserRegAndLogin" %>

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

public class YqxkjUserRegAndLogin : IHttpHandler, IRequiresSessionState {
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string flag = Common.NullToEmpty(context.Request["flag"]);
        string email = Common.NullToEmpty(context.Request["email"]);
        string truename = HttpContext.Current.Server.UrlDecode(Common.NullToEmpty(context.Request["truename"]));
        string phone = Common.NullToEmpty(context.Request["phone"]);
        string areacode = Common.NullToEmpty(context.Request["areacode"]);

        string wxopenid = Common.NullToEmpty(context.Request["wxopenid"]);
        string pwd = Common.NullToEmpty(context.Request["pwd"]);
        if (pwd == "") { pwd = "111111"; }
        string code = Common.NullToEmpty(context.Request["code"]);
        string mid = Common.NullToEmpty(context.Request["mid"]);

        switch (flag.ToLower())
        {
            case "user_reg":
                context.Response.Write(Register(phone, pwd, truename,wxopenid, code));
                break;
            case "reg2":
                context.Response.Write(Register2(phone, code, mid));
                break;
            case "findpwd":
                context.Response.Write(FindPwd(phone, pwd, code));
                break;
            case "login":
                context.Response.Write(Login(phone, pwd));
                break;
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

    private string Register(string phone, string pwd, string truename,string wxopenid,string code)
    {
        string strSql = @"select top 1 yzm from T_ShortMessage_Log where tel=@tel and outSendTime>getdate() order by sendTime desc";
        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@tel",phone)
        };
        object yzm = ado.ExecuteSqlScalar(strSql, p);
        if (code.Equals(yzm) || true)
        {
            strSql = @"select * from T_Member_Info where MemberName=@tel";
            SqlParameter[] p2 = new SqlParameter[] {
                new SqlParameter("@tel",phone)
            };
            int exists = ado.ExecuteSqlDataset(strSql, p2).Tables[0].Rows.Count;
            if (exists > 0)
            {
                return "该手机号码已经注册过";
            }
            else
            {
                string memberId = IdCreator.CreateId("T_Member_Info", "MemberId");
                pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
                strSql = @"insert into T_Member_Info(MemberId,MemberName,Password,Mobile,Tel,LevelId,LevelName,TrueName,WXOpenId) values(@MemberId,@Tel,@Pwd,@Tel,@Tel,'1000','普通会员',@TrueName,@WXOpenId)";
                SqlParameter[] p3 = new SqlParameter[] {
                    new SqlParameter("@MemberId",memberId),
                    new SqlParameter("@Pwd",pwd),
                    new SqlParameter("@tel",phone),
                    new SqlParameter("@TrueName",truename),
                    new SqlParameter("@WXOpenId",wxopenid)
                    
                };
                int result = ado.ExecuteSqlNonQuery(strSql, p3);
                if (result == 1)
                {
                    /*
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie("memberInfo",memberId+"$$"+pwd));
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie("memberInfoMore", "$$" + phone));
                     * */

                    HttpCookie cookie = new HttpCookie("memberInfo");//初使化并设置Cookie的名称
                    cookie.Expires = DateTime.Now.AddYears(1);//设置过期时间
                    cookie.Value = memberId + "$$" + pwd;
                    HttpContext.Current.Response.AppendCookie(cookie);

                    HttpCookie cookie2 = new HttpCookie("memberInfoMore");//初使化并设置Cookie的名称
                    cookie2.Expires = DateTime.Now.AddYears(1);//设置过期时间
                    cookie2.Value = "$$" + phone;
                    HttpContext.Current.Response.AppendCookie(cookie2);
                }
                return result.ToString();
            }
        }
        else
        {
            return "验证码错误";
        }
    }

    private string Register2(string phone, string code, string mid)
    {
        string strSqlyzm = @"select top 1 yzm from T_ShortMessage_Log where tel=@tel and outSendTime>getdate() order by sendTime desc";
        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@tel",phone)
        };
        string yzm = ado.ExecuteSqlScalar(strSqlyzm, p).ToString();
        if (yzm != code) { return "验证码错误"; }

        string strSql = @"update T_Member_Info set MemberName=@MemberName,Mobile=@MemberName where MemberId=@MemberId";
        SqlParameter[] p3 = new SqlParameter[] {
                    new SqlParameter("@MemberName",phone),
                    new SqlParameter("@MemberId",mid)
                };
        int result = ado.ExecuteSqlNonQuery(strSql, p3);
        return result.ToString();
    }

    private string FindPwd(string phone, string pwd, string code)
    {
        string strSql = @"select top 1 yzm from T_ShortMessage_Log where tel=@tel and outSendTime>getdate() order by sendTime desc";
        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@tel",phone)
        };
        string yzm = "";
        try
        {
            yzm = ado.ExecuteSqlScalar(strSql, p).ToString();
        }
        catch { return "验证码错误"; }

        if (code.Equals(yzm))
        {
            strSql = @"select * from T_Member_Info where MemberName=@tel";
            SqlParameter[] p2 = new SqlParameter[] {
                new SqlParameter("@tel",phone)
            };
            int exists = ado.ExecuteSqlDataset(strSql, p2).Tables[0].Rows.Count;
            if (exists == 0)
            {
                return "该用户不存在";
            }
            else
            {
                pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
                strSql = @"update T_Member_Info set Password=@Pwd where MemberName=@Tel";
                SqlParameter[] p3 = new SqlParameter[] {
                    new SqlParameter("@Pwd",pwd),
                    new SqlParameter("@tel",phone)
                };
                int result = ado.ExecuteSqlNonQuery(strSql, p3);
                if (result == 1)
                {
                    string memberId = "";
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie("memberInfo", memberId + "$$" + pwd));
                }
                return "密码重置成功";
            }
        }
        else
        {
            return "验证码错误";
        }
    }

    private string Login(string phone, string pwd)
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
            /*
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("memberInfo", dt.Rows[0]["MemberId"].ToString() + "$$" + pwd));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("memberInfoMore", dt.Rows[0]["truename"].ToString() + "$$" + dt.Rows[0]["mobile"].ToString()));
            **/

            HttpCookie cookie = new HttpCookie("memberInfo");//初使化并设置Cookie的名称
            cookie.Expires = DateTime.Now.AddYears(1);//设置过期时间
            cookie.Value = dt.Rows[0]["MemberId"].ToString() + "$$" + pwd;
            HttpContext.Current.Response.AppendCookie(cookie);

            HttpCookie cookie2 = new HttpCookie("memberInfoMore");//初使化并设置Cookie的名称
            cookie2.Expires = DateTime.Now.AddYears(1);//设置过期时间
            cookie2.Value = dt.Rows[0]["truename"].ToString() + "$$" + dt.Rows[0]["mobile"].ToString();
            HttpContext.Current.Response.AppendCookie(cookie2);

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