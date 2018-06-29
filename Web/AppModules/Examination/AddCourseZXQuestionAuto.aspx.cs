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

public partial class Admin_AppModules_Question_AddCourseZXQuestionAuto : StarTech.Adapter.StarTechPage
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
        this.zjmethod = Request["zjmethod"] == null ? "Auto" : Request["zjmethod"];
        
        if (!IsPostBack)
        {
            this.hid_goodsId.Value = Request["goodsId"] == null ? "" : Request["goodsId"];
            InitSetInfo();
            CheckPopedom();
            BindGoods();
        }
    }

    protected void BindGoods()
    {
        DataTable dt = new TableObject("T_Goods_Info").Util_GetList("GoodsName", "GoodsId='" + this.hid_goodsId.Value + "'");
        this.txtGoods.Text = dt.Rows.Count > 0 ? dt.Rows[0]["GoodsName"].ToString() : "";
    }

    protected void CheckPopedom()
    {
        if (_Nid != "")
        {
            _pageTitle = "题目管理";
            ModelTestday model = new BllTestday().GetModel(_Nid);
            ViewState["model"] = model;
            txtTitle.Text = model.Title;
           
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
        DataSet ds = ado.ExecuteSqlDataset("select categoryId,Questions,qtype,num from T_Test_day_item where testSysnumber='" + testSysnumber + "'");
        int total = 0;
        string info = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                int one = int.Parse(row["num"].ToString());
        
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
        if (this.zjmethod == "Auto222")
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
        DataSet ds = ado.ExecuteSqlDataset("select categoryId from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId not in('Current','Parent','Course','Common')  group by categoryId");
        foreach (DataRow row in ds.Tables[0].Rows) { list.Add(row["categoryId"].ToString()); }
        return list;
    }

    #region set
    protected void btnSave_Click(object sender, EventArgs e)
    {

        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        ado.ExecuteSqlNonQuery("delete from T_Test_day_item where testSysnumber='" + this._Nid + "' and categoryId in('Current','Parent','Course','Common')");

        //check
        string errorCheck = "";
        for (int i = 1; i <= 4; i++)
        {
            Control category = this.FindControl("ly" + i);
            Control sf = this.FindControl("sf" + i);
            Control a1 = this.FindControl("a1" + i);
            Control a2 = this.FindControl("a2" + i);
            Control a3a4 = this.FindControl("a3a4" + i);
            Control x = this.FindControl("x" + i);
            Control al = this.FindControl("al" + i);

            if (category != null && sf != null && a1 != null && a2 != null && a3a4 != null && x != null && al != null)
            {
                TextBox ddl_category = (TextBox)category;
                TextBox txt_sf = (TextBox)sf;
                TextBox txt_a1 = (TextBox)a1;
                TextBox txt_a2 = (TextBox)a2;
                TextBox txt_a3a4 = (TextBox)a3a4;
                TextBox txt_x = (TextBox)x;
                TextBox txt_al = (TextBox)al;


                if (ddl_category.Text == "Current" || ddl_category.Text == "Course")
                {
                    if (AddItem(this._Nid, GetNumber(txt_sf.Text), ddl_category.Text, "", "判断题", i) == false) { return; }
                    if (AddItem(this._Nid, GetNumber(txt_a1.Text), ddl_category.Text, "", "单选题", i) == false) { return; }
                    if (AddItem(this._Nid, GetNumber(txt_a2.Text), ddl_category.Text, "", "多选题", i) == false) { return; }
                    if (AddItem(this._Nid, GetNumber(txt_a3a4.Text), ddl_category.Text, "", "不定项选择题", i) == false) { return; }
                    if (AddItem(this._Nid, GetNumber(txt_x.Text), ddl_category.Text, "", "简答题", i) == false) { return; }
                    if (AddItem(this._Nid, GetNumber(txt_al.Text), ddl_category.Text, "", "计算分析题", i) == false) { return; }
                }
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "ttt", "<script>alert('保存成功');location.href='AddCourseZXQuestionAuto.aspx?zjmethod=Auto&Nid=" + this._Nid + "&goodsId="+this.hid_goodsId.Value+"'</script>");
    }

    protected int GetNumber(string s)
    {
        int i = 0;
        try { i = Int32.Parse(s); }
        catch { i = 0; }
        return i;
    }

    protected bool AddItem(string testSysnumber, int num, string categoryId, string Questions, string qType, int orderBy)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        if (ado.ExecuteSqlScalar("select sysnumber from T_Test_day_item where testSysnumber='" + testSysnumber + "' and categoryId='" + categoryId + "' and qType='" + qType + "'") == null)
        {
            string sql = "insert into  T_Test_day_item(sysnumber, testSysnumber, num, categoryId, Questions,qType,orderBy)";
            sql += " values('" + Guid.NewGuid().ToString() + "','" + testSysnumber + "'," + num + ",'" + categoryId + "','" + Questions + "','" + qType + "'," + orderBy + ")";
            ado.ExecuteSqlNonQuery(sql);
        }
        else
        {
            ado.ExecuteSqlNonQuery("update T_Test_day_item set num=" + num + ",Questions='" + Questions + "' where testSysnumber='" + testSysnumber + "' and categoryId='" + categoryId + "' and qType='" + qType + "' ");
        }
        return true;
    }

    protected void InitSetInfo()
    {

        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Test_day_item where testSysnumber='" + this._Nid + "'and categoryId in('Current','Parent','Course','Common')").Tables[0];
        if (dt.Rows.Count == 0) { return; }
        for (int i = 1; i <= 4; i++)
        {
            Control category = this.FindControl("ly" + i);
            Control sf = this.FindControl("sf" + i);
            Control a1 = this.FindControl("a1" + i);
            Control a2 = this.FindControl("a2" + i);
            Control a3a4 = this.FindControl("a3a4" + i);
            Control x = this.FindControl("x" + i);
            Control al = this.FindControl("al" + i);

            if (category != null && sf != null && a1 != null && a2 != null && a3a4 != null && x != null && al != null)
            {
                TextBox ddl_category = (TextBox)category;
                TextBox txt_sf = (TextBox)sf;
                TextBox txt_a1 = (TextBox)a1;
                TextBox txt_a2 = (TextBox)a2;
                TextBox txt_a3a4 = (TextBox)a3a4;
                TextBox txt_x = (TextBox)x;
                TextBox txt_al = (TextBox)al;

                if (dt.Select("orderBy=" + i + "").Length > 0)
                {
                    if (dt.Select("orderBy=" + i + " and qType='判断题'").Length > 0) { txt_sf.Text = dt.Select("orderBy=" + i + " and qType='判断题'")[0]["num"].ToString(); }
                    if (dt.Select("orderBy=" + i + " and qType='单选题'").Length > 0) { txt_a1.Text = dt.Select("orderBy=" + i + " and qType='单选题'")[0]["num"].ToString(); }
                    if (dt.Select("orderBy=" + i + " and qType='多选题'").Length > 0) { txt_a2.Text = dt.Select("orderBy=" + i + " and qType='多选题'")[0]["num"].ToString(); }
                    if (dt.Select("orderBy=" + i + " and qType='不定项选择题'").Length > 0) { txt_a3a4.Text = dt.Select("orderBy=" + i + " and qType='不定项选择题'")[0]["num"].ToString(); }
                    if (dt.Select("orderBy=" + i + " and qType='简答题'").Length > 0) { txt_x.Text = dt.Select("orderBy=" + i + " and qType='简答题'")[0]["num"].ToString(); }
                    if (dt.Select("orderBy=" + i + " and qType='计算分析题'").Length > 0) { txt_al.Text = dt.Select("orderBy=" + i + " and qType='计算分析题'")[0]["num"].ToString(); }
                }
            }
        }
    
    }

    #endregion
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("CouseTestZX.aspx?goodsId=" + this.hid_goodsId.Value, true);
    }
}
