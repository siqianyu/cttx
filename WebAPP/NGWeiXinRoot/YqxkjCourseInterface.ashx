<%@ WebHandler Language="C#" Class="YqxkjCourseInterface" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using StarTech.DBUtility;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web.DynamicData;
public class YqxkjCourseInterface : IHttpHandler
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        string flag = Common.NullToEmpty(context.Request["flag"]);
        string id = Common.NullToEmpty(context.Request["id"]);
        string email = Common.NullToEmpty(context.Request["email"]);
        string truename = HttpContext.Current.Server.UrlDecode(Common.NullToEmpty(context.Request["truename"]));
        string phone = Common.NullToEmpty(context.Request["phone"]);
        string areacode = Common.NullToEmpty(context.Request["areacode"]);
        
        string wxopenid = Common.NullToEmpty(context.Request["wxopenid"]);
        string pwd = Common.NullToEmpty(context.Request["pwd"]);
        string code = Common.NullToEmpty(context.Request["code"]);
        string mid = Common.NullToEmpty(context.Request["mid"]);
        string dirid = Common.NullToZero(context.Request["dirid"]).ToString();
        string questionId = Common.NullToEmpty(context.Request["questionid"]);
        string courseId = Common.NullToEmpty(context.Request["courseid"]);
        string mytype = HttpContext.Current.Server.UrlDecode(Common.NullToEmpty(context.Request["mytype"]));
        string userAnswer = Common.NullToEmpty(context.Request["useranswer"]);
        switch (flag.ToLower())
        {
            //课程列表
            case "list_course":
                context.Response.Write(ListCourse(dirid));
                break;
            //新闻列表
            case "list_news":
                context.Response.Write(ListNews(dirid));
                break;//新闻列表
            case "list_news2":
                context.Response.Write(ListNews2(dirid));
                break;
            //二级分类
            case "list_subclass":
                context.Response.Write(ListSubCategory(dirid));
                break;
            //课后练习  
            case "get_question":
                context.Response.Write(GetQuestion(id));
                break;
            //错题练习  
            case "list_error_question":
                context.Response.Write(ListErrorQuestion(mid, courseId));
                break; 
            //错题记录
            case "record_error":
                context.Response.Write(RecordError(mid, courseId, questionId, code, userAnswer));
                break;  
            //得分计算
            case "computer_record":
                context.Response.Write(ComputerQuestionRecord(mytype, courseId, mid));
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
        }
    }

    
    /// <summary>
    /// 获取错误题目
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="courseId"></param>
    /// <returns></returns>
    protected string ListErrorQuestion(string memberId, string courseId)
    {
        string ids = "";
        DataTable dt = ado.ExecuteSqlDataset("select QuestionId from T_Test_ErrorRecord where CourseId = '" + courseId + "' and MemberId='" + memberId + "' and isnull(IfPass,0)=0").Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            if (ids.IndexOf(row["QuestionId"].ToString()) == -1)
            {
                ids += row["QuestionId"].ToString() + ",";
            }
        }
        if (ids != "") { ids = ids.TrimEnd(','); }
        return ids;
    }

    /// <summary>
    /// 错题记录
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="courseId"></param>
    /// <param name="questionId"></param>
    /// <returns></returns>
    protected int RecordError(string memberId, string courseId, string questionId, string IsError,string userAnswer)
    {
        if (ado.ExecuteSqlScalar("select QuestionId from T_Test_ErrorRecord where [MemberId]='" + memberId + "' and [CourseId]='" + courseId + "' and [QuestionId]='" + questionId + "'") == null)
        {
            string sql = "INSERT INTO [T_Test_ErrorRecord]([MemberId],[CourseId],[QuestionId],[CreateTime],[IfPass],[userAnswer])";
            sql += " VALUES('" + memberId + "','" + courseId + "','" + questionId + "',getdate(),0,'" + userAnswer + "')";
            ado.ExecuteSqlNonQuery(sql);
        }
        if (IsError == "0")
        {
            return ado.ExecuteSqlNonQuery("update T_Test_ErrorRecord set IfPass=1,userAnswer='" + userAnswer + "' where [MemberId]='" + memberId + "' and [CourseId]='" + courseId + "' and [QuestionId]='" + questionId + "'");
        }
        else
        {
            return ado.ExecuteSqlNonQuery("update T_Test_ErrorRecord set IfPass=0,userAnswer='" + userAnswer + "' where [MemberId]='" + memberId + "' and [CourseId]='" + courseId + "' and [QuestionId]='" + questionId + "'");
        }
        return 1;
    }

    /// <summary>
    /// 显示全部新闻信息
    /// </summary>
    /// <returns></returns>
    protected string ListNews(string CategoryId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_News where CategoryId=" + CategoryId + " and [Approved]=1  order by [ReleaseDate] desc").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            string sampleDesc = PubFunction.CheckStr(row["Body"].ToString());
            sampleDesc = sampleDesc.Replace("	", "").Replace("　", "");
            sampleDesc = sampleDesc.Length > 100 ? sampleDesc.Substring(0, 100) : sampleDesc;
            string ImgLink = "../Images/yqxkj.png";
            if (row["ImgLink"].ToString() != "") { ImgLink = row["ImgLink"].ToString(); }

            html += "<li onclick=\"location.href='YqxkjNewsInfo.aspx?dirid=" + row["CategoryId"].ToString() + "&id=" + row["NewsID"].ToString() + "'\">";
            html += "<div class=\"course-img left\"><img width='100' height='80' src='" + ImgLink + "'></div>";
            html += "<p>";
            html += "<b class=\"news-tit\">" + row["Title"].ToString() + "</b>";
            html += "<span class=\"news-des\">" + sampleDesc + "</span>";
            html += "</p>";
            html += "</li>";
        }
        return html;
    }

    /// <summary>
    /// 显示全部新闻信息
    /// </summary>
    /// <returns></returns>
    protected string ListNews2(string CategoryId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_News where CategoryId=" + CategoryId + " and [Approved]=1  order by [ReleaseDate] desc").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<li><i></i><a href=\"YqxkjNewsInfo.aspx?dirid=" + row["CategoryId"].ToString() + "&id=" + row["NewsID"].ToString() + "\">" + row["Title"].ToString() + "</a></li>";
        }
        return html;
    }
    
    /// <summary>
    /// 显示全部课程信息
    /// </summary>
    /// <returns></returns>
    protected string ListCourse(string CategoryId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select GoodsId,GoodsName,GoodsSmallPic,GoodsSampleDesc,SalePrice,TotalSaleCount,(select count(1) from T_Goods_Info where GoodsToTypeId=a.GoodsId and IsSale=1)TaskCount from V_Goods_Info a where CategoryPath like '%" + CategoryId + "%' and JobType='Goods' and IsSale=1").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<li onclick=\"location.href='YqxkjCourseDetail.aspx?id=" + row["GoodsId"].ToString() + "'\">";
            html += "<div class=\"course-img left\"><img src=\"" + row["GoodsSmallPic"].ToString() + "\"></div>";
				html += "<p>";
                html += "<b>" + row["GoodsName"].ToString() + "</b>";
                html += "<span class=\"course-des\">" + row["GoodsSampleDesc"].ToString() + "</span>";
                html += "<span>" + row["TaskCount"].ToString() + "讲  |  <i class=\"over\">" + row["TotalSaleCount"].ToString() + "人学习中</i></span>";
				html += "</p>";
			html += "</li>";
        }
        return html;
    }

    /// <summary>
    /// 显示全部课程信息
    /// </summary>
    /// <returns></returns>
    protected string GetCourseInfo(string CategoryId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select GoodsId,GoodsName,GoodsSmallPic,GoodsSampleDesc,SalePrice,TotalSaleCount from V_Goods_Info where CategoryPath like '%" + CategoryId + "%' and JobType='Goods' and IsSale=1").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<li onclick=\"location.href='YqxkjCourseDetail.aspx?id=" + row["GoodsId"].ToString() + "'\">";
            html += "<div class=\"course-img left\"><img src=\"" + row["GoodsSmallPic"].ToString() + "\"></div>";
            html += "<p>";
            html += "<b>" + row["GoodsName"].ToString() + "</b>";
            html += "<span class=\"course-des\">" + row["GoodsSampleDesc"].ToString() + "</span>";
            html += "<span>" + row["TotalSaleCount"].ToString() + "讲  |  " + row["TotalSaleCount"].ToString() + "人学习中  |  <i class=\"over\">已完结</i></span>";
            html += "</p>";
            html += "</li>";
        }
        return html;
    }

    /// <summary>
    /// 显示下级分类
    /// </summary>
    /// <returns></returns>
    protected string ListSubCategory(string PCategoryId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select CategoryId,CategoryName from T_Info_Category where PCategoryId='" + PCategoryId + "' order by Orderby").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            DataSet dsCount = ado.ExecuteSqlDataset("select count(1) from T_Goods_Info where [JobType]='Goods' and CategoryId = '" + row["CategoryId"] + "' and IsSale=1;select count(1) from [T_Test_Queston] where [courseId] in(select GoodsId from T_Goods_Info where [JobType]='Goods' and CategoryId = '" + row["CategoryId"] + "' and IsSale=1)");
            
            html += "<li class=\"nav1\" onclick=\"location.href='YqxkjCourseList.aspx?dirId=" + row["CategoryId"] + "'\">";
            html += "<b>" + row["CategoryName"] + "</b><span>" + dsCount.Tables[0].Rows[0][0].ToString() + "课  | " + dsCount.Tables[1].Rows[0][0].ToString() + "练习</span>";
			html += "</li>";
            //<li class="nav2" onclick="location.href='#'">
            //    <b>中级职称</b><span>21讲  | 320练习</span>
            //</li>
            //<li class="nav3" onclick="location.href='#'">
            //    <b>高级职称</b><span>21讲  | 320练习</span>
            //</li>
        }
        return html;
    }


    /// <summary>
    /// 获取练习题
    /// </summary>
    /// <param name="questionId"></param>
    /// <returns></returns>
    protected string GetQuestion(string questionId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Test_Queston where sysnumber='" + questionId + "'").Tables[0];
        string html1 = "";
        string html2 = "";
        if (dt.Rows.Count > 0)
        {
            //所属案例
            string mainQuestionTitle = "";
            if (dt.Rows[0]["mainQuestionSysnumber"].ToString() != "")
            {
                DataTable dtAL = ado.ExecuteSqlDataset("select * from T_Test_Queston where isAL=1 and sysnumber='" + dt.Rows[0]["mainQuestionSysnumber"].ToString() + "'").Tables[0];
                mainQuestionTitle = dtAL.Rows.Count > 0 ? dtAL.Rows[0]["questionTitle"].ToString() : "";
            }
            
            string rightAnswer = dt.Rows[0]["questionAnswer"].ToString().TrimEnd(',').Replace(",", "");
            html1 += "【" + dt.Rows[0]["questionType"].ToString() + "】" + mainQuestionTitle + dt.Rows[0]["questionTitle"].ToString() + "$$" + rightAnswer + "$$" + dt.Rows[0]["description"].ToString();
            DataTable dsAnswer = ado.ExecuteSqlDataset("select * from T_Test_QuestonAnswer where questionSysnumber='" + questionId + "' order by orderby").Tables[0];

            foreach (DataRow rowAnswer in dsAnswer.Rows)
            {
                if (rowAnswer["AnswerValue"].ToString() != "")
                {
                    string cssAnswer = rightAnswer.IndexOf(rowAnswer["AnswerKey"].ToString()) > -1 ? "right" : "wrong";
                    string cssAnswer2 = rightAnswer.IndexOf(rowAnswer["AnswerKey"].ToString()) > -1 ? " icon-duigou" : "icon-17cuowu";
                    html2 += "<p class=\"" + cssAnswer + "\" id='" + rowAnswer["AnswerKey"] + "' onclick=\"select_answer('" + rowAnswer["AnswerKey"] + "')\"><b>" + rowAnswer["AnswerKey"] + "</b><i class=\"icon iconfont " + cssAnswer2 + "\"></i><span>" + rowAnswer["AnswerValue"] + "</span></p>";
                }
            }
        }
        return html1 + "$$" + html2 + "$$" + dt.Rows[0]["questionType"].ToString();
    }

    /// <summary>
    /// 得分计算
    /// </summary>
    protected string ComputerQuestionRecord(string hidMorePropertys, string courseId, string memberId)
    {
        return QuestionHelper.ComputerQuestionRecord(hidMorePropertys, courseId, memberId);
    }

    private string Register(string phone, string pwd, string truename, string code)
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
                strSql = @"insert into T_Member_Info(MemberId,MemberName,Password,Mobile,Tel,LevelId,LevelName,TrueName) values(@MemberId,@Tel,@Pwd,@Tel,@Tel,'1000','普通会员',@TrueName)";
                SqlParameter[] p3 = new SqlParameter[] {
                    new SqlParameter("@MemberId",memberId),
                    new SqlParameter("@Pwd",pwd),
                    new SqlParameter("@tel",phone),
                    new SqlParameter("@TrueName",truename)
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