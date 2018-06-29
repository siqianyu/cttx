using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.ELife.Question;
using StarTech.DBUtility;
using System.Text;
using System.Data.OleDb;
using System.Data;
using NGShop.Bll;

public partial class Admin_AppModules_Question_AddCourseZX : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
    BllTestday bll = new BllTestday();
    ModelTestday model = new ModelTestday();
    public string _Nid =  HttpContext.Current.Request["Nid"] == null ? "" : HttpContext.Current.Request["Nid"];
    protected string _pageTitle = string.Empty;
    private int PageIndex = HttpContext.Current.Request["PageIndex"] == null ? 0 : int.Parse(HttpContext.Current.Request["PageIndex"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Admin_AppModules_Question_AddCourseZX));
        if (!IsPostBack)
        {
            this.hid_goodsId.Value = Request["goodsId"] == null ? "" : Request["goodsId"];
            BindType();
            if (Request["from"] != null && Request["from"] == "course")
            {
                this.txtTitle.Enabled = false;
                this.txtRemark.Enabled = false;
                this.txtReleaseDate.Enabled = false;
                
            }
            CheckPopedom();
        }
    }
    protected void CheckPopedom()
    {
        if (_Nid != "")
        {
            _pageTitle = "题目编辑";
            ModelTestday model = new BllTestday().GetModel(_Nid);
            ViewState["model"] = model;
            txtTitle.Text = model.Title;
            txtRemark.Text = model.Remarks;
            txtReleaseDate.Text = model.Addtime.ToString("yyyy-MM-dd");
            this.ddlLevel.SelectedValue = model.levelFlag;
            this.ddlSH.SelectedValue = model.shFlag;

            txtStarTime.Text = Convert.ToDateTime(model.startime).ToString("yyyy-MM-dd");
            ddlStarH.SelectedValue = Convert.ToDateTime(model.startime).ToString("HH");
            ddlStarM.SelectedValue = Convert.ToDateTime(model.startime).ToString("mm");
            txtEndTime.Text = Convert.ToDateTime(model.endtime).ToString("yyyy-MM-dd");
            ddlEndH.SelectedValue = Convert.ToDateTime(model.endtime).ToString("HH");
            ddlEndM.SelectedValue = Convert.ToDateTime(model.endtime).ToString("mm");
            this.rdZJFS.SelectedValue = model.ZjMethod;
            this.rdLX.SelectedValue = model.Type.ToString();


            BindQuetion(model.Questions);

        }
        else
        {
            _pageTitle = "题目添加";
            this.txtEndTime.Text = this.txtStarTime.Text = txtReleaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    private void BindQuetion(string Questions)
    {
        if (!string.IsNullOrEmpty(Questions))
        {
            DataTable dt = new BllTestQueston().GetList(" sysnumber in ('" + Questions.Replace(",", "','") + "')").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (_Nid != "")
        {
            ModelTestday model = (ModelTestday)ViewState["model"];
            model.Title = txtTitle.Text.ToString();
            model.Remarks = txtRemark.Text.ToString();
            model.Addtime = Convert.ToDateTime(txtReleaseDate.Text);
            model.levelFlag = this.ddlLevel.SelectedValue;
            model.shFlag = this.ddlSH.SelectedValue;
            model.startime =Convert.ToDateTime( txtStarTime.Text + " " + ddlStarH.SelectedValue + ":" + ddlStarM.SelectedValue + ":00");
            model.endtime = Convert.ToDateTime(txtEndTime.Text + " " + ddlEndH.SelectedValue + ":" + ddlEndM.SelectedValue + ":00");
            model.ZjMethod = this.rdZJFS.SelectedValue;
            model.Type = int.Parse(this.rdLX.SelectedValue);
            bll.Update(model);

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');layer_close_refresh();</script>");

        }
        else
        {
            model.Sysnumber = Guid.NewGuid().ToString();
            model.Title = txtTitle.Text.ToString();
            model.Remarks = txtRemark.Text.ToString();
            model.Addtime = Convert.ToDateTime(txtReleaseDate.Text);
            model.levelFlag = this.ddlLevel.SelectedValue;
            model.shFlag = this.ddlSH.SelectedValue;
            model.Type = int.Parse(this.rdLX.SelectedValue);
            model.courseType = this.hid_goodsId.Value;
            model.startime = Convert.ToDateTime(txtStarTime.Text + " " + ddlStarH.SelectedValue + ":" + ddlStarM.SelectedValue + ":00");
            model.endtime = Convert.ToDateTime(txtEndTime.Text + " " + ddlEndH.SelectedValue + ":" + ddlEndM.SelectedValue + ":00");
            model.ZjMethod = this.rdZJFS.SelectedValue;
            if (bll.Add(model) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');layer_close_refresh();</script>");
            }

        }

    }

    public string GetTypes()
    {
        string s = "";
        return s;
    }

    public string GetQuesType(string type)
    {
        string value = "";
        switch (type)
        {
            case "PD": value = "判断题";
                break;
            case "DX": value = "单选题";
                break;
            case "FX": value = "复选题";
                break;
            default: value = "";
                break;
        }
        return value;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request["from"] != null && Request["from"] == "course")
        {
            Response.Redirect("../Course/CourseListTM.aspx");
        }
        else
        {
            Response.Redirect("CouseTestZX.aspx");
        }
    }
    [AjaxPro.AjaxMethod]
    public string DeleteQues(string testId, string questionSys)
    {
        string value = "";
        ModelTestday model = new BllTestday().GetModel(testId);
        if (!string.IsNullOrEmpty(model.Questions))
        {
            string newQuestions = model.Questions.Replace(questionSys + ",", "");
            value = adohelper.ExecuteSqlNonQuery("update T_Test_day set Questions='" + newQuestions + "' where Sysnumber='" + testId + "'").ToString();
        }
        return value;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string allQuestions = "";
        DataTable dt = new TableObject("T_Test_Day").Util_GetList("*", "Sysnumber='" + _Nid + "'");
        DataTable dtTM = new TableObject("T_Test_Queston").Util_GetList("*", "categoryFlag='" + dt.Rows[0]["chapterId"] + "'");
        foreach (DataRow row in dtTM.Rows)
        {
            string questionSysnumber = row["sysnumber"].ToString();
            allQuestions += questionSysnumber + ",";
            DataTable dtDA = new TableObject(" T_Question_AnswerItems").Util_GetList("*", "questionSysnumber='" + questionSysnumber + "'");
            int orderby = 0;
            string rightDA = "";
            foreach (DataRow rowDA in dtDA.Rows)
            {
                ModelTestQuestonAnswer modelTestA = new ModelTestQuestonAnswer();
                modelTestA.sysnumber = rowDA["sysnumber"].ToString();
                modelTestA.questionSysnumber = questionSysnumber;
                modelTestA.AnswerKey = GetENG(orderby);
                modelTestA.AnswerValue = rowDA["anserItemDesc"].ToString();
                modelTestA.OrderBy = orderby;
                new DalTestQuestonAnswer().Add(modelTestA);
                orderby++;
                if (rowDA["isAnswer"].ToString() == "1") { rightDA += modelTestA.AnswerKey + ","; }
            }
            new TableObject("T_Test_Queston").Util_UpdateBat("questionAnswer='" + rightDA + "'", "sysnumber='" + rightDA + "'");
        }
        new TableObject("T_Test_Day").Util_UpdateBat("Questions='" + allQuestions + "'", "Sysnumber='" + _Nid + "'");
    }

    protected string GetENG(int i)
    {
        if (i == 0) { return "A"; }
        if (i == 1) { return "B"; }
        if (i == 2) { return "C"; }
        if (i == 3) { return "D"; }
        if (i == 4) { return "E"; }
        else { return "F"; }
    }

    protected void BindType()
    {
        //DataTable dtP = new TableObject("T_Base_CourseCategory").Util_GetList("*", "isnull(pcategoryId,'')=''", "orderBy asc");
        //this.ddlPCategory.DataTextField = "categoryName";
        //this.ddlPCategory.DataValueField = "categoryId";
        //this.ddlPCategory.DataSource = dtP;
        //this.ddlPCategory.DataBind();
        //this.ddlPCategory.Items.Insert(0,new ListItem("无", ""));

        //DataTable dt = new TableObject("T_Base_UserType").Util_GetList("*", "1=1", "orderBy");
        //this.CheckBoxList1.DataTextField = "typeName";
        //this.CheckBoxList1.DataValueField = "typeId";
        //this.CheckBoxList1.DataSource = dt;
        //this.CheckBoxList1.DataBind();
        //this.CheckBoxList1.Items.Add(new ListItem("全部", "9999"));
    }
    protected void rdZJFS_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
