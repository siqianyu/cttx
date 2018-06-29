using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;

public partial class NGWeiXinRoot_YqxkjQuestionRecordPanel : System.Web.UI.Page
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string source;
    public string allIndex;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.hidMemberId.Value = Session["MemberId"] != null ? Session["MemberId"].ToString() : Request["memberId"];
            this.hidCourseId.Value = Common.NullToZero(Request["courseId"]).ToString();
            this.hidSource.Value = this.source = Common.NullToEmpty(Request["source"]).ToString();
            this.hidFromFree.Value = Common.NullToEmpty(Request["fromfree"]).ToString();
            this.hidCurId.Value = Common.NullToEmpty(Request["curId"]).ToString();
            //this.allIndex = BindAll(this.hidMemberId.Value, this.hidCourseId.Value);
        }
    }

    //显示我的题库信息
    public string BindAll(string memberId, string courseId)
    {
        string html = "";
        DataSet ds = ado.ExecuteSqlDataset("select * from T_Test_ErrorRecord where MemberId='" + memberId + "' and CourseId='" + courseId + "' order by showIndex");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            string background = "background:#ccc;";
            if (row["IfPass"].ToString() == "1") { background = "background:#56ca5a;"; }
            if (row["IfPass"].ToString() == "0") { background = "background:#ff0000;"; }
            html += "<button style=\"margin:1em;width:2.4em;padding:0;" + background + "\" onclick=\"to_show('" + row["QuestionId"] + "');return false;\">" + row["showIndex"] + "</button>";
        }
        return html;
    }

    public string ListRecord(string questionType)
    {        
        string html = "";
        string sql = "select a.*,b.questionType from T_Test_ErrorRecord a,T_Test_Queston b where a.QuestionId=b.sysnumber and  a.MemberId='" + this.hidMemberId.Value + "' and a.CourseId='" + this.hidCourseId.Value + "' and b.questionType='" + questionType + "' order by a.showIndex";
        DataSet ds = ado.ExecuteSqlDataset(sql);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            string background = "";
            if (row["IfPass"].ToString() == "1") { background = "class=\"answer-right\""; }
            if (row["IfPass"].ToString() == "0") { background = "class=\"answer-wrong\""; }
            html += "<li " + background + " onclick=\"to_show('" + row["QuestionId"] + "');return false;\">" + row["showIndex"] + "</li>";
        }
        return html;
        
    }

    //computer
    public string ComputerAll()
    {
        double total = 0;
        string sql = "select a.*,b.questionType from T_Test_ErrorRecord a,T_Test_Queston b where a.QuestionId=b.sysnumber and  a.MemberId='" + this.hidMemberId.Value + "' and a.CourseId='" + this.hidCourseId.Value + "'  order by a.showIndex";
        DataSet ds = ado.ExecuteSqlDataset(sql);
        foreach (DataRow row in ds.Tables[0].Rows)
        {

            if (row["IfPass"].ToString() == "1")
            {
                if (row["questionType"].ToString() == "单选题") { total += 1.5; }
                if (row["questionType"].ToString() == "多选题") { total += 2; }
                if (row["questionType"].ToString() == "判断题") { total += 1; }
                if (row["questionType"].ToString() == "不定项选择题") { total += 2; }
            }
            if (row["IfPass"].ToString() == "0")
            {
                if (row["questionType"].ToString() == "判断题") { total -= 0.5; }
                if (row["questionType"].ToString() == "不定项选择题") 
                {
                    string questionAnswer = row["questionAnswer"].ToString();
                    string userAnswer = row["userAnswer"].ToString();
                    if (questionAnswer.IndexOf(userAnswer) > -1)
                    {
                        double n = 2.0 / ((double)questionAnswer.Length);       //每小题得分
                        double x = userAnswer.Length * n;                       //总计得分
                        total += x; 
                    }
                }
            }

        }
        string s = total.ToString("0.0");
        if (s.IndexOf(".0") > -1) { s = s.TrimEnd('0').TrimEnd('.'); }
        return s;
    }
}