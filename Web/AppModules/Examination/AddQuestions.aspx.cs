using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using StarTech.ELife.Question;
using StarTech.DBUtility;
using System.Text;
using System.Data.OleDb;

public partial class Admin_AppModules_Question_AddQuestions : StarTech.Adapter.StarTechPage
{
    BllTestQueston bll = new BllTestQueston();
    ModelTestQueston model = new ModelTestQueston();
    private string _Nid; 
    protected string _pageTitle = string.Empty;
    //private int PageIndex = Utils.SafeRequest.GetQueryInt("PageIndex", 0);
    public string sjid;
    public string courseId;
    public string typeId;
    public int isshow;

    protected void Page_Load(object sender, EventArgs e)
    {
        this._Nid = Request["Nid"] == null ? "" : Request["Nid"];
        this.isshow = Request["isshow"] == null ? 0 : int.Parse(Request["isshow"]);
        this.sjid = Request["sjid"] == null ? "" : Request["sjid"];
        this.courseId = Request["courseId"] == null ? "" : Request["courseId"];
        this.typeId = Request["typeId"] == null ? "" : Request["typeId"];
        if (!IsPostBack)
        {
            if (Request.QueryString["courseId"] != null) { this.hidCategoryId.Value = Request.QueryString["courseId"]; }
            BindType();
            SHInit(_Nid);
            CheckPopedom();
            if (this.typeId != "") { this.ddlPCategory.SelectedValue = this.typeId; }
            if (this.isshow == 1) { this.btnSave.Visible = false; }
        }
    }

    protected void AddIntoSJ(string sjid, string tiid)
    {
        //DataTable dt = new NGShop.Bll.TableObject("T_Test_day").Util_GetList("*", "sysnumber='" + sjid + "'");
        //if (dt.Rows.Count > 0)
        //{
        //    string questions = dt.Rows[0]["questions"].ToString();
        //    if (questions.IndexOf(tiid) == -1)
        //    {
        //        questions += tiid + ",";
        //    }
        //    new NGShop.Bll.TableObject("T_Test_day").Util_UpdateBat("questions='" + questions + "'", "sysnumber='" + sjid + "'");
        //}
    }

    #region 权限判断
    /// <summary>
    /// 权限判断
    /// </summary>
    protected void CheckPopedom()
    {
        if (_Nid != "")
        {
            _pageTitle = "题目编辑";
            GetNewsInfo();
            this.ddlType.Enabled = false;
            BindALSubquestions(_Nid);
        }
        else
        {
            _pageTitle = "题目添加";
            //添加初始化
            if (Request["from"] != null && Request["from"] == "course")
            {
                //ModCourse mod = new BllCourse().GetModel(this.courseId);
                //if (mod != null)
                //{
                //    this.ddlPCategory.SelectedValue = mod.CourseType;
                //    foreach (ListItem item in this.CheckBoxList1.Items) { if (mod.CourseBYUserType.IndexOf(item.Value) > -1) { item.Selected = true; } }
                //}
            }
        }
    }
    #endregion

    #region 新闻内容详细信息
    /// <summary>
    /// 获得新闻详细信息
    /// </summary>
    protected void GetNewsInfo()
    {
        string answer = "";
        string title = "";
        string questionType = "";
        string descript = "";
        DataTable dt = GetQuestionType(_Nid, ref questionType, ref answer, ref title, ref descript);
        SetTableValue("ck", "txt", answer, dt, this.tb);
        hidAnswer.Value = answer;
        ddlType.SelectedValue = questionType;
        txtTitle.Text = title;
        txtDescription.Text = descript;
        ModelTestQueston model = new ModelTestQueston();
        BllTestQueston bll = new BllTestQueston();
        model = bll.GetModel(_Nid);
        if (model != null)
        {
            foreach (ListItem item in this.CheckBoxList1.Items) { if (model.personFlag!=null&&model.personFlag.IndexOf(item.Value) > -1) { item.Selected = true; } }
            if (model.categoryFlag != null && model.categoryFlag != "")
            {
                foreach (string categoryId in model.categoryFlag.Split(','))
                {
                    foreach (ListItem item in this.ddlPCategory.Items) if (categoryId == item.Value) { item.Selected = true; }
                }
            }
            this.ddlLevelPoint.SelectedValue = model.LevelPoint.ToString();
            this.ddlPCategory_Show.Text = ShowCategorynames(model.categoryFlag);
            this.hidOrderBy.Text = model.orderBy.ToString();
            this.mainSysnumber.Value = model.mainQuestionSysnumber;
            if (this.mainSysnumber.Value != "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "a", "<script>selectType()</script>");
                this.div_al.InnerHtml = GetMainQuestionInfo(this.mainSysnumber.Value).questionTitle;
                this.div_al.Style["height"] = "100px";
                //this.btn_al.Visible = false;
            }
            if (model.questionType == "案例")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "b", "<script>$('#tr_options').hide();$('#tr_sh').hide();</script>");
            }
        }
    }
    //给table赋值
    private void SetTableValue(string cbkName, string txtName, string answer, DataTable dt, HtmlTable tb)
    {
        for (int k = 0; k <= dt.Rows.Count - 1; k++)
        {
            TextBox txt = ((TextBox)tb.FindControl(txtName + (k + 1)));
            if (txt != null)
            {
                txt.Text = dt.Rows[k]["AnswerValue"].ToString();
                string[] answers = answer.Split(',');
                string answerKye = dt.Rows[k]["AnswerKey"].ToString();
                foreach (string key in answers)
                {
                    if (key == answerKye)
                    {
                        ((HtmlInputCheckBox)tb.FindControl(cbkName + (k + 1))).Checked = true;
                    }
                }
            }
        }
    }
    private DataTable GetQuestionType(string sys, ref string type, ref string answer, ref string title, ref string descriptiong)
    {
        DataTable dtOption = null;
        DataTable dt = new BllTestQueston().GetList("sysnumber='" + sys + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            type = dt.Rows[0]["questionType"].ToString();
            answer = dt.Rows[0]["questionAnswer"].ToString();
            title = dt.Rows[0]["questionTitle"].ToString();
            descriptiong = dt.Rows[0]["description"].ToString();
            dtOption = new DalTestQuestonAnswer().GetList("questionSysnumber='" + sys + "' order by AnswerKey asc").Tables[0];
        }
        return dtOption;
    }
    #endregion

    #region 校验数据
    protected string Validates()
    {
        if (string.IsNullOrEmpty(txtTitle.Text))
        {
            return "题目不能为空";
        }
        return "";
    }
    #endregion

    public string GetTypes()
    {
        string s = "";
        foreach (ListItem item in this.CheckBoxList1.Items) { if (item.Selected) { s += item.Value + ","; } }
        return s;
    }

    public string GetCategorys()
    {
        return this.hidCategoryId.Value;
    }

    public ModelTestQueston GetMainQuestionInfo(string sysnumber)
    {
        return bll.GetModel(sysnumber);
    }

    #region 提交
    /// <summary>
    /// 确定提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strError = Validates();
        if (strError != "")
        {
            //JSUtility.Alert(strError);
            return;
        }
        model.LevelPoint = decimal.Parse(this.ddlLevelPoint.SelectedValue);
        model.ifCourseQuestion = 1;
        model.questionTitle = this.txtTitle.Text.Trim();
        model.questionType = ddlType.SelectedValue;
        model.Description = txtDescription.Text;
        model.Orner = "0";  //1属于每日一练表 0属于普通题目表；
        model.categoryFlag = GetCategorys();
        model.personFlag = GetTypes();
        model.createPerson = this.UserName;
        model.categorypath = getCategoryPath(model.categoryFlag);
        model.createTime = DateTime.Now;
        model.orderBy = Int32.Parse(this.hidOrderBy.Text.Trim());
        if (this.courseId != "") { model.courseId = this.courseId; }
        if (this.mainSysnumber.Value != "")
        {
            model.isSubQuestion = 1;
            model.mainQuestionSysnumber = this.mainSysnumber.Value;
            model.personFlag = GetMainQuestionInfo(model.mainQuestionSysnumber).personFlag;
        }
        else
        {
            model.isSubQuestion = 0;
            model.mainQuestionSysnumber = "";
        }


        if (_Nid != "")
        {
            string typeFlag = "";
            string answers = "";
            string title = "";
            string answer = "";
            string descript = "";
            DataTable dt = GetQuestionType(_Nid, ref typeFlag, ref answers, ref title, ref descript);
            SubUpdate(_Nid, this.tb, "ck", "txt", ref answer);
            model.sysnumber = _Nid;
            model.questionAnswer = answer;

            #region 原数据
            ModelTestQueston modelOld = bll.GetModel(_Nid);
            if (modelOld != null) { model.courseId = modelOld.courseId; }
            #endregion

            bll.Update(model);
            SaveSH(model.sysnumber);
            if (this.mainSysnumber.Value != "") { EditSameGroup(this.mainSysnumber.Value); }
            //跳转控制
            if (Request["from"] != null && Request["from"] == "course")
            {
                Response.Write("<script>alert('题目修改成功');</script>");
            }
            else
            {
                //状态
                if (Session["NewsStatus_Hashtable"] != null)
                {
                    Hashtable hTable = (Hashtable)Session["NewsStatus_Hashtable"];
                    hTable["ReadSessionStatus"] = "1";
                    Session["NewsStatus_Hashtable"] = hTable;
                }

                Response.Write("<script>alert('题目修改成功');</script>");
            }
        }
        else
        {
            model.sysnumber = Guid.NewGuid().ToString();
            string questionAnser = "";
            Sub(model.sysnumber, tb, "ck", "txt", ref questionAnser);
            model.questionAnswer = questionAnser;
            bll.Add(model);
            SaveSH(model.sysnumber);
            //加入所属试卷
            AddIntoSJ(this.sjid, model.sysnumber);


            Response.Write("<script>alert('题目添加成功，请继续添加');location.href='AddQuestions.aspx?courseId=" + this.courseId + "'</script>");

        }
    }

    /// <summary>
    /// 目录链
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string getCategoryPath(string id)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DBInstance");
        DataTable dtGoods = ado.ExecuteSqlDataset("select * from t_goods_info where goodsid='" + id + "'").Tables[0];
        if (dtGoods.Rows.Count > 0)
        {
            if (dtGoods.Rows[0]["JobType"].ToString() == "SubGoods")
            {
                DataTable dtGoodsTop = ado.ExecuteSqlDataset("select * from t_goods_info where goodsid='" + dtGoods.Rows[0]["CategoryId"].ToString() + "'").Tables[0];
                if (dtGoodsTop.Rows.Count > 0)
                {
                    return "," + dtGoodsTop.Rows[0]["CategoryId"].ToString() + "," + dtGoodsTop.Rows[0]["goodsid"].ToString() + "," + id;
                }
            }
            else
            {
                return "," + dtGoods.Rows[0]["CategoryId"].ToString() + "," + id;
            }
        }
        return "," + id;
    }

    private void Sub(string sysnumber, HtmlTable tb, string ckName, string txtName, ref string strAnswer)
    {
        string[] SelectOpiton = new string[] { "A", "B", "C", "D","E" };
        for (int i = 0; i <= SelectOpiton.Length; i++)
        {
            HtmlInputCheckBox cbk = tb.FindControl(ckName + (i + 1)) as HtmlInputCheckBox;
            if (cbk != null)
            {
                if (cbk.Checked)
                {
                    strAnswer = strAnswer + SelectOpiton[i].ToString() + ",";
                }

                AddQuestionAnswer(sysnumber, SelectOpiton[i], ((TextBox)tb.FindControl(txtName + (i + 1))).Text, i);
            }
        }
    }
    private void SubUpdate(string sysnumber, HtmlTable tb, string ckName, string txtName, ref string strAnswer)
    {
        string[] SelectOpiton = new string[] { "A", "B", "C", "D","E" };
        for (int i = 0; i <= SelectOpiton.Length; i++)
        {
            HtmlInputCheckBox cbk = tb.FindControl(ckName + (i + 1)) as HtmlInputCheckBox;
            if (cbk != null)
            {
                if (cbk.Checked)
                {
                    strAnswer = strAnswer + SelectOpiton[i].ToString() + ",";
                }

                UpdateQuestionAnswer(GetSys(sysnumber, SelectOpiton[i]), SelectOpiton[i], ((TextBox)tb.FindControl(txtName + (i + 1))).Text, sysnumber);
            }
        }
    }
    private string GetSys(string questionSysnumber, string selectOpiton)
    {
        string returnValue = "";
        DataTable dt = new DalTestQuestonAnswer().GetList("questionSysnumber='" + questionSysnumber + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["AnswerKey"].ToString() == selectOpiton)
                {
                    returnValue = dt.Rows[i]["sysnumber"].ToString();
                }
            }
        }
        return returnValue;
    }
    private void UpdateQuestionAnswer(string OptionSys, string key, string value, string QuestionSys)
    {
        ModelTestQuestonAnswer modelTestA = new DalTestQuestonAnswer().GetModel(OptionSys);
        if (modelTestA != null)
        {
            modelTestA.sysnumber = OptionSys;
            modelTestA.questionSysnumber = QuestionSys;
            modelTestA.AnswerKey = key;
            modelTestA.AnswerValue = value;
            new DalTestQuestonAnswer().Update(modelTestA);
        }
    }
    private void UpdateQuestion(string questionSys, string questionType, string questionTitle, string questionAnswer)
    {
        ModelTestQueston modelq = new DalTestQueston().GetModel(questionSys); ;
        modelq.sysnumber = questionSys;
        modelq.questionType = questionType;
        modelq.questionTitle = questionTitle;
        modelq.questionAnswer = questionAnswer;
        new DalTestQueston().Update(modelq);
    }
    #endregion
    private void AddQuestionAnswer(string questionSys, string questiongTitle, string isRight, int orderby)
    {
        ModelTestQuestonAnswer modelTestA = new ModelTestQuestonAnswer();
        modelTestA.sysnumber = Guid.NewGuid().ToString();
        modelTestA.questionSysnumber = questionSys;
        modelTestA.AnswerKey = questiongTitle;
        modelTestA.AnswerValue = isRight;
        modelTestA.OrderBy = orderby;
        new DalTestQuestonAnswer().Add(modelTestA);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //状态
        if (Session["NewsStatus_Hashtable"] != null)
        {
            Hashtable hTable = (Hashtable)Session["NewsStatus_Hashtable"];
            hTable["ReadSessionStatus"] = "1";
            Session["NewsStatus_Hashtable"] = hTable;
        }

        Response.Redirect("AddCourse.aspx?Nid=" + this.sjid + "", true);
    }

    protected void BindType()
    {
        //DataTable dtP = new BllCourseCategory().ListAllCategory();
        //this.ddlPCategory.DataTextField = "categoryName";
        //this.ddlPCategory.DataValueField = "categoryId";
        //this.ddlPCategory.DataSource = HiddenCategory(dtP);
        //this.ddlPCategory.DataBind();
        //this.ddlPCategory.Items.Insert(0, new ListItem("无", ""));


        //DataTable dt = new NGShop.Bll.TableObject("T_Base_UserType").Util_GetList("*", "1=1", "orderBy");
        //this.CheckBoxList1.DataTextField = "typeName";
        //this.CheckBoxList1.DataValueField = "typeId";
        //this.CheckBoxList1.DataSource = dt;
        //this.CheckBoxList1.DataBind();
        //this.CheckBoxList1.Items.Add(new ListItem("全部", "9999"));
    }

    public void BindALSubquestions(string alSysnumber)
    {
        DataTable dtSubs = new NGShop.Bll.TableObject(" T_Test_Queston").Util_GetList("*", "isSubQuestion=1 and mainQuestionSysnumber='" + alSysnumber + "'", "orderBy asc");
        this.rptSubQuestions.DataSource = dtSubs;
        this.rptSubQuestions.DataBind();
    }

    public string ShowCategorynames(string code)
    {
        return "";
        //string name = "";
        //BllCourseCategory bll = new BllCourseCategory();

        //if (code != "" && code !=null)
        //{
        //    foreach (string id in code.Split(','))
        //    {
        //        ModCourseCategory mode = bll.GetModel(id);
        //        if (mode != null)
        //        {
        //            name += mode.CategoryName + ",";
        //        }
        //    }

        //}
        //return name.TrimEnd(',');
    }

    protected void EditSameGroup(string mainQuestionSysnumber)
    {
        AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
        object objFlag = adohelper.ExecuteSqlScalar("select sameGroupFlag from T_Test_Queston where sysnumber='" + mainQuestionSysnumber + "'");
        if (objFlag != null)
        {
            adohelper.ExecuteSqlNonQuery("update T_Test_Queston set sameGroupFlag='" + objFlag.ToString() + "' where mainQuestionSysnumber='" + mainQuestionSysnumber + "'");
        }
    }



    //分类权限，动态隐藏
    protected bool HasTKAuth(string categoryid)
    {
        if (IsAdmin()) { return true; }
        DataTable dt = new NGShop.Bll.TableObject("t_base_tkset").Util_GetList("sysnumber", "usercode='" + this.UserName + "' and categoryid='" + categoryid + "'");
        return dt.Rows.Count > 0 ? true : false;
    }
    protected bool IsAdmin()
    {
        string groupId = ",";
        //Response.Write(groupId);
        if (groupId.IndexOf(",1,") > -1) { return true; } //超级管理员组
        return false;
    }
    protected DataTable HiddenCategory(DataTable dt)
    {
        if (IsAdmin()) { return dt; }
        DataTable authCate = new DataTable();
        authCate.Columns.Add("categoryId"); 
        authCate.Columns.Add("categoryName");

        foreach (DataRow row in dt.Rows)
        {
            if (HasTKAuth(row["categoryId"].ToString()) == true)
            {
                DataRow authRow = authCate.NewRow();
                authRow["categoryId"] = row["categoryId"];
                authRow["categoryName"] = row["categoryName"];
                authCate.Rows.Add(authRow);
            }
        }
        return authCate;
    }


    #region 审核功能
    /// <summary>
    /// 审核信息初始化
    /// </summary>
    protected void SHInit(string sysnumber)
    {
        AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
        DataSet ds = adohelper.ExecuteSqlDataset("select * from T_Test_Queston where sysnumber='" + sysnumber + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.ddlSH.SelectedValue = ds.Tables[0].Rows[0]["shFlag"].ToString();
            this.txtSHRemarks.Text = ds.Tables[0].Rows[0]["shReamrks"].ToString();
            this.txtSHPerson.Text = ds.Tables[0].Rows[0]["shPerson"].ToString();
            if (this.txtSHPerson.Text == "") { this.txtSHPerson.Text = this.UserName; }
            try
            {
                this.txtSHTime.Text = DateTime.Parse(ds.Tables[0].Rows[0]["shTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch { this.txtSHTime.Text = this.txtSHTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  }
        }
        else
        {
            this.txtSHPerson.Text = this.UserName;
            this.txtSHTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    /// <summary>
    /// 审核信息保存
    /// </summary>
    protected void SaveSH(string sysnumber)
    {
        AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
        string sql = "update T_Test_Queston set  shFlag=" + this.ddlSH.SelectedValue + ", shReamrks='" + this.txtSHRemarks.Text.Replace("'", "") + "', shPerson='" + this.UserName + "', shTime=getdate() where sysnumber='" + sysnumber + "'";
        adohelper.ExecuteSqlNonQuery(sql);
    }
    #endregion
}
