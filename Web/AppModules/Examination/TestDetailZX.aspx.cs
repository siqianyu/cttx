using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NGShop.Bll;
using StarTech.ELife.Question;
using System.Data;
using System.Text.RegularExpressions;

using StarTech.DBUtility;

public partial class Member_TestDetailZX :  System.Web.UI.Page
{
    public string tesSys = HttpContext.Current.Request["Nid"] == null ? "" : HttpContext.Current.Request["Nid"];
    public string flag;
    public string view;
    public int totalnum = 0;
    public string allAnswers="";

    public string pd_num;
    public string dx_num;
    public string fx_num;
    public int al_num;
    public string MemberId = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Member_TestDetailZX));
        this.flag = Request["flag"] == null ? "test" : Request["flag"];
        this.view = Request["view"] == null ? "0" : Request["view"];//预览
        if (!IsPostBack)
        {
            hidID.Value = tesSys;

            DataTable dt = new BllTestday().GetList("sysnumber='" + tesSys + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.hidTaskId.Value = dt.Rows[0]["courseType"].ToString();
                DataTable dtCourse = new TableObject("T_Goods_Info").Util_GetList("GoodsToTypeId", "GoodsId='" + this.hidTaskId.Value + "'");
                this.hidCourseId.Value = dtCourse.Rows.Count > 0 ? dtCourse.Rows[0]["GoodsToTypeId"].ToString() : "-1";

                if (dt.Rows[0]["ZjMethod"].ToString() == "Person")
                {
                    //人工
                    this.hidRndQuestions.Value = dt.Rows[0]["Questions"].ToString().TrimEnd(',').TrimStart(',');
                }
                else
                {
                    //自动
                    CreateRndQuestions(tesSys);
                }
            }
            CreateTestQuestions();
        }
    }

    protected void CreateTestQuestions()
    {
        string title = "";
        string time = "";
        GetTestName(tesSys, ref title, ref time);
        //ltTitle.Text = title;
        ltTime.Text = time;

    }



    private string GetAnswer(string sysNum)
    {
        string value = "";
        DataTable dt = new BllTestQueston().GetList("sysnumber='" + sysNum + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["questionType"].ToString() == "PD")
            {
                //判断题显示对错
                DataTable dtA = new TableObject("T_Test_QuestonAnswer").Util_GetList("*", "questionSysnumber='" + sysNum + "' and AnswerKey='" + dt.Rows[0]["questionAnswer"].ToString().TrimEnd(',') + "'");
                return dtA.Rows.Count > 0 ? dtA.Rows[0]["AnswerValue"].ToString().ToUpper().Replace("A", "").Replace("B", "") : "";
            }
            else
            {
                value = dt.Rows[0]["questionAnswer"].ToString();
            }
        }
        return value.TrimEnd(',');
    }
    private string GetDesc(string sysNum)
    {
        string value = "";
        DataTable dt = new BllTestQueston().GetList("sysnumber='" + sysNum + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            value = dt.Rows[0]["description"].ToString();
        }
        return value;
    }
    private DataTable BindSelection(string questionSys)
    {
        DataTable dt = new DalTestQuestonAnswer().GetList("questionSysnumber='" + questionSys + "' order by orderby").Tables[0];
        return dt;
    }
    private void BindTest(Repeater rpt, string questionType, string sysNums)
    {
        DataTable dt = new BllTestQueston().GetList("sysnumber in('" + sysNums + "') and questionTitle<>'' and questionType in('" + questionType.Replace(",","','") + "')").Tables[0];
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), questionType, "<script>$('#div_" + questionType + "').hide();</script>");
        }
    }
    private string GetQuestions(string testSys)
    {
        string querstiongs = "";
        DataTable dtItem = new TableObject("t_test_day_item").Util_GetList("Questions", "testsysnumber='" + tesSys + "'");
        if (dtItem.Rows.Count > 0)
        {
            foreach (DataRow row in dtItem.Rows)
            {
                querstiongs += row["Questions"].ToString() + ",";
            }
            querstiongs = this.hidRndQuestions.Value + querstiongs.Replace(",,", ",");
            return querstiongs.Replace(",", "','");
        }
        else
        {
            DataTable dt = new BllTestday().GetList("sysnumber='" + tesSys + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                querstiongs = dt.Rows[0]["Questions"].ToString();
                return querstiongs.Replace(",", "','");
            }
        }
        return "";
    }

    private void GetTestName(string sysNum, ref string title, ref string time)
    {
        DataTable dt = new BllTestday().GetList("sysnumber='" + sysNum + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            DateTime tNow = DateTime.Now;
            DateTime starTime = Convert.ToDateTime(dt.Rows[0]["starTime"]);
            DateTime endTime = Convert.ToDateTime(dt.Rows[0]["endTime"]);
            TimeSpan tNowS = new TimeSpan(tNow.Ticks);
            TimeSpan starTimeS = new TimeSpan(starTime.Ticks);
            TimeSpan endTimeS = new TimeSpan(endTime.Ticks);
            int t1 = starTimeS.Subtract(tNowS).Seconds;
            int t2 = tNowS.Subtract(endTimeS).Seconds;
            //if (t1 > 0)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert('考试还没有开始！');location.href='KS2.aspx';</script>");
            //}
            //else if (t2 > 0)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert('考试已经结束！');location.href='KS2.aspx';</script>");
            //}
            //else
            //{
            //    
            //    
            //}
            title = dt.Rows[0]["Title"].ToString();
            time = Convert.ToDateTime(dt.Rows[0]["Addtime"]).ToString("yyyy-MM-dd");
        }

    }


    private static string getIp()
    {
        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
        else
            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    }

    #region 随机组卷题目
    public void CreateRndQuestions(string testSysnumber)
    {
        string rndQuestions = "";
        string zyQuestions = "";
        string courseQuestions = "";
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        //上级课程
        DataSet ds = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId='Current'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            string sql = "select top " + row["num"] + " sysnumber from T_Test_Queston where categoryPath like '%"+this.hidCourseId.Value+"%' and questionType='" + row["qType"] + "' and isnull(isSubQuestion,0)=0  order by newid()";
            //Response.Write(sql+";<br>");
            DataSet dsQ = ado.ExecuteSqlDataset(sql);
            int flagI = 0;
            string ids = "";
            foreach (DataRow rowQ in dsQ.Tables[0].Rows)
            {
                if (flagI > 100) { break; }
                ids += rowQ["sysnumber"].ToString() + ",";
                flagI++;
            }
            if (ids != "")
            {
                ids = ids.TrimEnd(',');
                zyQuestions += "," + ids;
            }
        }
        this.hidZYQuestions.Value = zyQuestions.TrimStart(',').TrimEnd(',');

        //当前任务
        DataSet dscourse = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId='Course'");
        foreach (DataRow row in dscourse.Tables[0].Rows)
        {
            string sql = "select top " + row["num"] + " sysnumber from T_Test_Queston where courseId='"+this.hidTaskId.Value+"' and  questionType='" + row["qType"] + "' and isnull(isSubQuestion,0)=0 and sysnumber not in('" + zyQuestions.Replace(",", "','") + "') order by newid()";
            //Response.Write(sql + ";<br>");
            DataSet dsQ = ado.ExecuteSqlDataset(sql);
            int flagI = 0;
            string ids = "";
            foreach (DataRow rowQ in dsQ.Tables[0].Rows)
            {
                if (flagI > 100) { break; }
                ids += rowQ["sysnumber"].ToString() + ",";
                flagI++;
            }
            if (ids != "")
            {
                ids = ids.TrimEnd(',');
                courseQuestions += "," + ids;
            }
        }
        this.hidCourseQuestions.Value = courseQuestions.TrimStart(',').TrimEnd(',');


        /**
        //大专业、公共
        DataSet ds2 = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId in('Parent','Common')");
        foreach (DataRow row in ds2.Tables[0].Rows)
        {
            string hasExsitsQuestions = zyQuestions + courseQuestions.Replace(",,", ",");
            string sql = "select top " + row["num"] + " sysnumber from T_Test_Queston where " + GetPersonFlagFilter(row["categoryId"].ToString()) + " and  questionType='" + row["qType"] + "' and isnull(isSubQuestion,0)=0 and isnull(courseId,'')='' and sysnumber not in('" + hasExsitsQuestions.Replace(",", "','") + "') order by newid()";
            //Response.Write(sql + ";<br>");
            DataSet dsQ = ado.ExecuteSqlDataset(sql);
            int flagI = 0;
            string ids = "";
            foreach (DataRow rowQ in dsQ.Tables[0].Rows)
            {
                if (flagI > 100) { break; }
                ids += rowQ["sysnumber"].ToString() + ",";
                flagI++;
            }
            if (ids != "")
            {
                ids = ids.TrimEnd(',');
                rndQuestions += "," + ids;
            }
        }
         */
         
        this.hidOtherQuestions.Value = rndQuestions.TrimStart(',').TrimEnd(',');

        this.hidRndQuestions.Value = zyQuestions + "," + courseQuestions.TrimStart(',').TrimEnd(',') + "," + rndQuestions.TrimStart(',').TrimEnd(',');

        new TableObject("T_Test_day").Util_UpdateBat("Questions='" + this.hidRndQuestions.Value.TrimEnd(',').TrimStart(',') + "'", "Sysnumber='" + testSysnumber + "'");

    }
    public string GetPersonFlagFilter(string flag)
    {
        string filter=" 1=1 ";
        if (flag == "Current")
        {
            string categoryIds = "";
            if (categoryIds == "")
            {
                return "1=1";
            }
            else
            {
                string[] idArr = categoryIds.Split(',');
                if (idArr.Length > 1)
                {
                    string tmp = "";
                    for (int i = 0; i < idArr.Length; i++)
                    {
                        if (i == 0) { tmp += " categoryPath like '" + idArr[i] + "%' "; }
                        else { tmp += " or  categoryPath like '" + idArr[i] + "%'"; }
                    }
                    filter += "and (" + tmp + ") ";
                }
                else
                {
                    filter += "and categoryPath like '" + idArr[0] + "%'";
                }
            }
            
        }
        else if (flag == "Parent")
        {
            return "1=1";
        }
        else if (flag == "Common")
        {
            return "1=1";
        }
        else if (flag == "Course")
        {
            string courseIds = "";
            if (courseIds == "")
            {
                return "1=1";
            }
            else
            {
              filter += "and courseId in('"+courseIds.Replace(",","','")+"')";
            }
        }
        return filter;
    }

    #endregion

    public int isUseIp()
    {
        return 1;
        //BllIp bll = new BllIp();
        //string ip = getIp();
        //if (ip != "127.0.0.1")
        //{
        //    DataSet ds = bll.GetList("ip='" + ip + "' and isuse=1");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //else
        //{
        //    return 1;
        //}
    }

    [AjaxPro.AjaxMethod]
    public int addTestResult(string result, string id, string userId)
    {
        //ModResult mod = new ModResult();
        //BllResult bll = new BllResult();
        //ModMemberInfo modM = new BllMemberInfo().GetModel(userId);
        //ModelTestday modTest = new BllTestday().GetModel(id);
        //if (modTest != null)
        //{
        //    mod.id = Guid.NewGuid().ToString();
        //    mod.ip = getIp();
        //    mod.addtime = DateTime.Now;
        //    mod.result = result;
        //    mod.userid = userId;
        //    mod.username = modM.TrueName;
        //    mod.deptid = modM.AddressCode;
        //    mod.deptname = modM.AddressDetail;
        //    mod.code = modM.MemberName;
        //    mod.testid = id;
        //    mod.testname = modTest.Title;
        //}
        //if (bll.Add(mod))
        //{
        //    return 1;
        //}
        //else
        //{
        //    return 0;
        //}
        return 1;
    }

    public int havKS()
    {


        return 0;

    }


    /// <summary>
    /// 普通题目
    /// </summary>
    /// <returns></returns>
    public string CreateCommon(string questionType)
    {
        string sysNums = GetQuestions(this.tesSys);
        string html = "";
        DataTable dt = new BllTestQueston().GetList("sysnumber in('" + sysNums + "') and questionTitle<>'' and questionType in('" + questionType.Replace(",", "','") + "')").Tables[0];
        DataRow[] rowItems = dt.Select("1=1");
        if (questionType == "判断题") { this.pd_num = rowItems.Length.ToString(); }
        else if (questionType == "单选题") { this.dx_num = rowItems.Length.ToString(); }
        else if (questionType == "多选题") { this.fx_num = rowItems.Length.ToString(); }

        if (rowItems.Length > 0)
        {
            for (int i = 0; i < rowItems.Length; i++)
            {
                this.allAnswers += rowItems[i]["sysnumber"].ToString() + "|" + rowItems[i]["questionAnswer"].ToString() + "$";
                html += "<p><em id='em_" + rowItems[i]["sysnumber"].ToString() + "' style=\"font-size: 14px;font-style: normal; color: blue; font-family: '微软雅黑'\"><b>" + (i + 1) + "：</b>" + rowItems[i]["questionTitle"] + "</em></p>";
                html += GetOptions(rowItems[i]["sysnumber"].ToString(), ((rowItems[i]["questionType"].ToString() == "多选题") ? "checkbox" : "radio"));
            }
        }
        else
        {
            if (questionType == "判断题")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "pd", "<script>$('#div_PD').hide();</script>");
            }
            else if (questionType == "单选题")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "dx", "<script>$('#div_DX').hide();</script>");
            }
            else if (questionType == "多选题")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "fx", "<script>$('#div_FX').hide();</script>");

            }
        }
        return html;
    }


    /// <summary>
    /// 案例题
    /// </summary>
    /// <returns></returns>
    public string CreateAL()
    {
        string sysNums = GetQuestions(this.tesSys);
        string html = "";
        DataTable dt = new BllTestQueston().GetList("sysnumber in('" + sysNums + "') and questionTitle<>'' and questionType in('不定项选择题')").Tables[0];
        DataRow[] rowHeaders = dt.Select("isnull(isSubQuestion,0)=0");
        
        if (rowHeaders.Length > 0)
        {
            int sn = 0;
            foreach (DataRow rowHeader in rowHeaders)
            {
                sn++;
                html += "<p style='padding-top:10px;padding-bottom:10px;'><em style=\"font-size: 14px;font-style: normal; color: red; font-family: '微软雅黑'\"><b>" + sn + "：</b>" + rowHeader["questionTitle"] + "</em></p>";
                DataTable dtItems = new BllTestQueston().GetList("isSubQuestion=1 and mainQuestionSysnumber='" + rowHeader["sysnumber"] + "'").Tables[0];
                DataRow[] rowItems = dtItems.Select("1=1");
                this.al_num += rowItems.Length;
                if (rowItems.Length > 0)
                {
                    for (int i = 0; i < rowItems.Length; i++)
                    {
                        this.allAnswers += rowItems[i]["sysnumber"].ToString() + "|" + rowItems[i]["questionAnswer"].ToString() + "$";
                        html += "<p><em id='em_" + rowItems[i]["sysnumber"].ToString() + "' style=\"font-size: 14px;font-style: normal; color: blue; font-family: '微软雅黑'\"><b>" + (i + 1) + "：</b>" + rowItems[i]["questionTitle"] + "</em></p>";
                        html += GetOptions(rowItems[i]["sysnumber"].ToString(), ((rowItems[i]["questionType"].ToString() == "不定项选择题") ? "checkbox" : "radio"));
                    }
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "al", "<script>$('#div_AL').hide();</script>");
        }
        return html;
    }

    public string GetOptions(string questionSys,string inputFlag)
    {
        string html = "";
        DataTable dt = new DalTestQuestonAnswer().GetList("questionSysnumber='" + questionSys + "' and isnull(AnswerValue,'')<>'' order by orderby").Tables[0];
        html += " <p><input type=\"hidden\"  id=\"" + questionSys + "\" class=\"jquery_options\" />";
        html += "<table onclick=\"getAnswer2('" + questionSys + "','" + inputFlag + "')\">";
        foreach (DataRow row in dt.Rows)
        {
            if (inputFlag == "radio")
            {
                html += "<tr><td><input type='radio' id='" + row["sysnumber"] + "' name='radio-" + questionSys + "' value='" + row["AnswerKey"] + "' /><label for='" + row["sysnumber"] + "'>" + row["AnswerKey"] + " " + row["AnswerValue"] + "</label></td></tr>";
            }
            else
            {
                html += "<tr><td><input type='checkbox' id='" + row["sysnumber"] + "' name='checkbox-" + questionSys + "' value='" + row["AnswerKey"] + "' /><label for='" + row["sysnumber"] + "'>" + row["AnswerKey"] + " " + row["AnswerValue"] + "</label></td></tr>";
            }
        }
        html += "</table>";
        return html;
    }
}
