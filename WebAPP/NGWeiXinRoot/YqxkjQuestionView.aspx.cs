using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;

public partial class NGWeiXinRoot_YqxkjQuestionView : System.Web.UI.Page
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string source;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.hidMemberId.Value = Session["MemberId"] != null ? Session["MemberId"].ToString() : Request["memberId"];
            this.hidCourseId.Value = Common.NullToZero(Request["courseId"]).ToString();
            this.hidSource.Value = this.source = Common.NullToEmpty(Request["source"]).ToString();
            this.hidFromFree.Value = Common.NullToEmpty(Request["fromfree"]).ToString();
            this.hidCurId.Value = Common.NullToEmpty(Request["curId"]).ToString();
            BindTotal();
            BindAllQuestions();
            BindMyAnswer();
            this.hidNextId.Value = BindNextId(this.hidCurId.Value);
        }
    }

    public void BindMyAnswer()
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Test_ErrorRecord where MemberId='" + this.hidMemberId.Value + "' and CourseId='" + this.hidCourseId.Value + "' and QuestionId='" + this.hidCurId.Value + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.div_myanswer.InnerHtml = "我的答案：【" + dt.Rows[0]["userAnswer"].ToString() + "】，正确答案：" + dt.Rows[0]["questionAnswer"].ToString() + "";
            this.hidSNIndex.Value = dt.Rows[0]["showIndex"].ToString();
        }
    }

    //总题数
    public void BindTotal()
    {
        DataTable dt = ado.ExecuteSqlDataset("select QuestionId from T_Test_ErrorRecord where MemberId='" + this.hidMemberId.Value + "' and CourseId='" + this.hidCourseId.Value + "'").Tables[0];
        this.hidTotalCount.Value = dt.Rows.Count.ToString();
    }
    

    public string BindNextId(string curId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Test_ErrorRecord where MemberId='" + this.hidMemberId.Value + "' and CourseId='" + this.hidCourseId.Value + "' and IfPass<>1 order by showIndex").Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["QuestionId"].ToString() == curId)
                {
                    if (i + 1 < dt.Rows.Count) { return dt.Rows[i + 1]["QuestionId"].ToString(); }
                    else { return dt.Rows[0]["QuestionId"].ToString(); }
                }
            }
        }
        return "";
    }

    public void BindAllQuestions()
    {
        string ids = "";
        DataTable dt = ado.ExecuteSqlDataset("select sysnumber from T_Test_Queston where courseId = '" + this.hidCourseId.Value + "' and shFlag=1 and isnull(isAL,0)=0 order by questionTypeOrderBy,orderBy").Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            ids += row["sysnumber"].ToString() + ",";
        }
        if (ids != "") { ids = ids.TrimEnd(','); }
        this.hidAllQuestions.Value = ids;

        if (this.source == "mnlx")
        {
            //模拟练习
            dt = ado.ExecuteSqlDataset("select Questions from T_Test_day where courseType = '" + this.hidCourseId.Value + "' and shFlag=1 order by Addtime").Tables[0];
            if (dt.Rows.Count > 0)
            {
                ids = "";
                string sysnumbers = dt.Rows[0]["Questions"].ToString().TrimEnd(',').Replace(",", "','");
                DataTable dt2 = ado.ExecuteSqlDataset("select sysnumber from T_Test_Queston where sysnumber in('" + sysnumbers + "') and shFlag=1 and isnull(isAL,0)=0 order by questionTypeOrderBy,orderBy").Tables[0];
                foreach (DataRow row in dt2.Rows)
                {
                    ids += row["sysnumber"].ToString() + ",";
                }
                if (ids != "") { ids = ids.TrimEnd(','); }
                this.hidAllQuestions.Value = ids;

            }
        }
        //先创建我的题库记录
        QuestionHelper.CreateMyQuestions(this.hidMemberId.Value, this.hidCourseId.Value, this.hidAllQuestions.Value);

        //first
        if (this.hidCurId.Value == "")
        {
            object objFirst = ado.ExecuteSqlScalar("select top 1 QuestionId from T_Test_ErrorRecord where MemberId='" + this.hidMemberId.Value + "' and CourseId='" + this.hidCourseId.Value + "' order by showIndex");
            this.hidCurId.Value = objFirst == null ? "" : objFirst.ToString();
        }
    }

}