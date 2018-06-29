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
using System.Collections;
using NGShop.Bll;

public partial class Admin_AppModules_Question_AddCourseZXQuestion : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
    BllTestday bll = new BllTestday();
    ModelTestday model = new ModelTestday();
    public string _Nid = HttpContext.Current.Request["Nid"] == null ? "" : HttpContext.Current.Request["Nid"];
    protected string _pageTitle = string.Empty;
    private int PageIndex = HttpContext.Current.Request["PageIndex"] == null ? 0 : int.Parse(HttpContext.Current.Request["PageIndex"]);
    public string zjmethod;
    public string personFlag;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.zjmethod = Request["zjmethod"] == null ? "" : Request["zjmethod"];
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Admin_AppModules_Question_AddCourseZXQuestion));
        if (!IsPostBack)
        {
         
            CheckPopedom();
        }
    }
    protected void CheckPopedom()
    {
        if (_Nid != "")
        {
            _pageTitle = "题目管理";
            ModelTestday model = new BllTestday().GetModel(_Nid);
            ViewState["model"] = model;
            txtTitle.Text = model.Title;
            txtLevel.Text = model.personFlag.TrimEnd(',');
            this.personFlag = model.personFlag.TrimEnd(',');
            BindQuetion(model.Questions);
            BindTotalNum(_Nid);
        }
        else
        {
            _pageTitle = "题目管理";
          
        }
    }
    private void BindQuetion(string Questions)
    {

    }

    protected void BindTotalNum(string testSysnumber)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = ado.ExecuteSqlDataset("select categoryId,Questions,qtype from T_Test_day_item where testSysnumber='" + testSysnumber + "'");
        int total = 0;
        string info = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                string question = row["Questions"].ToString().TrimEnd(',').TrimStart(',');
                int one = question.Length > 1 ? question.Split(',').Length : 0;
                if (row["qtype"].ToString() == "A3/A4型题")
                {
                    object objNum = ado.ExecuteSqlScalar("select count(*) as c from T_Test_Queston where  isSubQuestion=1 and mainQuestionSysnumber in('" + question.Replace(",", "','") + "') ");
                    one = objNum == null ? 0 : int.Parse(objNum.ToString());
                }
                total += one;
                //info += GetCategoryName(row["categoryId"].ToString()) + " " + one + "；";
            }
            this.txtQuestionTotalNum.Text = total + "";
        }
        else
        {
            this.txtQuestionTotalNum.Text = "0";
        }
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
    public string DeleteQues(string testId, string questionSys, string groupsysnumber)
    {

        object objQ = adohelper.ExecuteSqlScalar("select Questions from T_Test_day_item where sysnumber='" + groupsysnumber + "'");
        if (objQ != null)
        {
            string newids = objQ.ToString().Replace(questionSys, "").Replace(",,", ",");
            adohelper.ExecuteSqlNonQuery("update T_Test_day_item set Questions='" + newids + "',num=num-1 where sysnumber='" + groupsysnumber + "' ");
        }

        return "1";
    }

    [AjaxPro.AjaxMethod]
    public string InsertQues(string questionSys, string groupsysnumber)
    {

        object objQ = adohelper.ExecuteSqlScalar("select Questions from T_Test_day_item where sysnumber='" + groupsysnumber + "'");
        if (objQ != null)
        {
            int n = questionSys.TrimEnd(',').Split(',').Length;
            string newids = objQ.ToString().TrimEnd(',') + "," + questionSys.TrimEnd(',');
            adohelper.ExecuteSqlNonQuery("update T_Test_day_item set Questions='" + newids + "',num=num+" + n + " where sysnumber='" + groupsysnumber + "' ");
        }

        return "1";
    }

    [AjaxPro.AjaxMethod]
    public string DeleteItems(string testId, string categoryId)
    {

        int value = adohelper.ExecuteSqlNonQuery("delete T_Test_day_item  where testSysnumber='" + testId + "' and categoryId='" + categoryId + "' ");

        return value.ToString();
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

    public string CreateHtml(string testSysnumber)
    {
        ArrayList groupList = GetItemGroup(testSysnumber);
        if (this.zjmethod == "Auto")
        {
            string s = "";
            foreach (object obj in groupList)
            {
                s += "<table  class='table_1'><tr><td><b>" + GetCategoryName(obj.ToString()) + "</td></tr>";
                DataTable dt = new TableObject("T_Test_day_item").Util_GetList("*", "testSysnumber='" + testSysnumber + "' and categoryId='" + obj.ToString() + "'", "qtype asc");
                foreach (DataRow row in dt.Rows)
                {
                    s += "<tr><td><b style='color:blue'>【" + row["qtype"] + "】(" + row["num"] + ")</b></td></tr>";
                }
                s += "</table>";
            }
            return s;
        }
        else
        {
            string html = "";
            foreach (object obj in groupList)
            {
                html += "<table  class='table_1'><tr><td><b>" + GetCategoryName(obj.ToString()) + "</td></tr>";
                DataTable dt = new TableObject("T_Test_day_item").Util_GetList("*", "testSysnumber='" + testSysnumber + "' and categoryId='" + obj.ToString() + "'", "qtype asc");
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Questions"].ToString() != "")
                    {
                        int i = 1;
                        html += "<tr><td><b style='color:blue'>【" + row["qtype"] + "】(" + row["num"] + ")<input type='button' value='新增' onclick=\"addQuestion('" + row["sysnumber"] + "');\"></b></td></tr>";
                        html += "<tr><td>";
                        html += "<table  class='table_1'>";
                        DataTable dtQuestion = new BllTestQueston().GetList("isnull(isSubQuestion,0)=0 and sysnumber in ('" + row["Questions"].ToString().Replace(",", "','") + "')").Tables[0];
                        foreach (DataRow rowQ in dtQuestion.Rows)
                        {
                            html += "<tr><td>" + i + "</td><td>" + rowQ["questionTitle"] + "</td><td width='90'><input type='button' value='查看' onclick=\"showQuestion('" + rowQ["sysnumber"] + "');\"> <input type='button' value='删除' onclick=\"deleteQuestion('" + rowQ["sysnumber"] + "','" + row["sysnumber"] + "');\"></td></tr>";
                            i++;
                        }

                        html += "</table>";
                        html += "</td></tr>";
                    }
                }
                html += "</table>";
            }


            return html;
        }
    }



    public string GetCategoryName(string id)
    {
        return "";
        //ModCourseCategory mod = new BllCourseCategory().GetModel(id);
        //return mod == null ? "" : mod.CategoryName;
    }

    public ArrayList GetItemGroup(string testSysnumber)
    {
        ArrayList list = new ArrayList();
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = ado.ExecuteSqlDataset("select categoryId from T_Test_day_item where testSysnumber='" + testSysnumber + "' group by categoryId");
        foreach (DataRow row in ds.Tables[0].Rows) { list.Add(row["categoryId"].ToString()); }
        return list;
    }
}
