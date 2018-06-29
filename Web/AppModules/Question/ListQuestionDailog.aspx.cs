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
//using NGWeb.News;
//using NGWeb.Utils;
using System.Text;
//using NGWeb.Category;
using System.Collections.Generic;
using StarTech.DBUtility;
using StarTech.ELife.Question;

public partial class Admin_AppModules_Question_ListQuestionDailog : StarTech.Adapter.StarTechPage
{
    public string questionType;
    public int PageIndex = 0; 

    protected void Page_Load(object sender, EventArgs e)
    {
        this.questionType = Request["questionType"] == null ? "A3/A4型题" : Server.UrlDecode(Request["questionType"]);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Admin_AppModules_Question_ListQuestionDailog));
        PageIndex = Request["PageIndex"] == null ? 0 : int.Parse(Request["PageIndex"]);
    
        if (!IsPostBack)
        {
            BindType();
            BindNewsCateoryDrp();
            //还原状态
            //ReadSessionStatus();
            BindNews(PageIndex);
        }
    }

    #region 通用按钮栏设置
    /// <summary>
    /// 初始化按钮栏
    /// </summary>

    void TopButtons1_ShClickEvent(object sender, EventArgs e)
    {

    }
    //添加
    void TopButtons1_AddClickEvent(object sender, EventArgs e)
    {
        //Response.Redirect("AddQuestion.aspx", true);
    }

    //修改
    void TopButtons1_EditClickEvent(object sender, EventArgs e)
    {
        string ids = "";
        if (ids != "")
        {

            Response.Redirect("AddQuestions.aspx?Nid=" + ids + "", true);
        }
    }

    //保存状态start
    protected void SaveSessionStatus()
    {
        //Hashtable hTable = new Hashtable();
        //hTable["ReadSessionStatus"] = "0";
        //hTable["PageIndex"] = this.pageBar.PageIndex;
        //hTable["txtTitle"] = this.txtTitle.Text;
        //Session["NewsStatus_Hashtable"] = hTable;
    }
    //保存状态start
    protected void ReadSessionStatus()
    {
        //if (Session["NewsStatus_Hashtable"] != null)
        //{
        //    Hashtable hTable = (Hashtable)Session["NewsStatus_Hashtable"];
        //    if (hTable.ContainsKey("ReadSessionStatus") == true && hTable["ReadSessionStatus"].ToString() == "1")
        //    {
        //        if (hTable.ContainsKey("PageIndex")) { PageIndex = Int32.Parse(hTable["PageIndex"].ToString()); }
        //        if (hTable.ContainsKey("txtTitle")) { this.txtTitle.Text = hTable["txtTitle"].ToString(); }
        //        hTable["ReadSessionStatus"] = "0";
        //        Session["NewsStatus_Hashtable"] = hTable;
        //    }
        //}
    }

    //删除
    void TopButtons1_DeleteClickEvent(object sender, EventArgs e)
    {

    }

    //查询
    void TopButtons1_SearchClickEvent(object sender, EventArgs e)
    {
        this.pageBar.PageIndex = 0;
        BindNews(this.pageBar.PageIndex);
    }
    #endregion

    #region 搜素条件
    /// <summary>
    ///搜索条件 
    /// </summary>
    private string SetFilter()
    {
        StringBuilder filter = new StringBuilder("isnull(isSubQuestion,0)=0");
        string title = this.txtTitle.Text.Trim();
        if (title != String.Empty) { filter.AppendFormat(" and questionTitle like '%{0}%'", title); }
        filter.AppendFormat(" and questionType='" + this.questionType + "'");
        if (ddlPCategory.SelectedValue != "")
        {
            filter.AppendFormat(" and categoryflag like '" + ddlPCategory.SelectedValue.Trim() + "%'");
        }
        //Response.Write(filter);
        ViewState["Filter"] = filter.ToString();
        return filter.ToString();
    }
    #endregion

    #region 绑定列表数据
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    /// <param name="pageIndex"></param>
    private void BindNews(int pageIndex)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string filter = SetFilter();
        string sort = "createtime desc";
        this.pageBar.PageIndex = pageIndex;
        int pageSize = this.pageBar.PageSize;
        int count = 0;
        DataTable dtAll = adoHelper.ExecuteSqlDataset("select * from T_Test_Queston where " + filter + " order by " + sort).Tables[0];
        DataTable dt = PubFunction.LocalPaging(dtAll, "1=1", sort, pageSize, pageIndex, ref count);
        this.gvArticleList.DataSource = dt;
        this.gvArticleList.DataBind();
        this.pageBar.RecordCount = count;
        //保存状态
        SaveSessionStatus();
    }
    #endregion

    #region 分页
    protected void pageBar_PageIndexChanged(object sender, PageIndexChangedEventArguments e)
    {
        BindNews(this.pageBar.PageIndex);
    }
    #endregion

    #region 绑定新闻类别下拉框
    /// <summary>
    /// 绑定新闻类别下拉框列表
    /// </summary>
    private void BindNewsCateoryDrp()
    {
    }
    #endregion

    #region 新闻类型
    /// <summary>
    /// 新闻类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected string ShowPic(string type)
    {
        if (type == "1")
        {
            return "图片新闻";
        }
        else
        {
            return "普通新闻";
        }

    }
    protected string GetTypeName(string type)
    {
        return type;
    }
    //删除按钮
    [AjaxPro.AjaxMethod]
    public string Delete(string sysNums)
    {
        AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
        string sys = sysNums.Replace(",", "','");
        int result = adohelper.ExecuteSqlNonQuery("delete from T_Test_Queston where sysnumber in ('" + sys + "') and orwner <> '1'");
        string sysN = sysNums.Split(',')[0].ToString();
        int a = adohelper.ExecuteSqlNonQuery("delete from T_Test_QuestonAnswer where questionSysnumber='" + sysNums.Split(',')[0].ToString() + "'");
        return result.ToString();
    }

    public string categoryname(string code, string ifCourseQuestion)
    {
        return "";
        /*
        if (ifCourseQuestion == "1")
        {
            //课后练习
            ModCourse mod = new BllCourse().GetModel(code);
            code = (mod == null) ? "" : mod.CourseType;
        }
        string name = "";
        BllCourseCategory bll = new BllCourseCategory();
        
        if (code != "")
        {
            foreach (string id in code.Split(','))
            {
                ModCourseCategory mode = bll.GetModel(id);
                if (mode != null)
                {
                    name += mode.CategoryName + ",";
                }
            }

        }
        return name.TrimEnd(',');
         * */
    }

    protected void BindType()
    {
        //DataTable dtP = new BllCourseCategory().ListAllCategory();
        //this.ddlPCategory.DataTextField = "categoryName";
        //this.ddlPCategory.DataValueField = "categoryId";
        //this.ddlPCategory.DataSource = dtP;
        //this.ddlPCategory.DataBind();
        //this.ddlPCategory.Items.Insert(0, new ListItem("全部", ""));


        //this.ddlPCategory2.DataTextField = "categoryName";
        //this.ddlPCategory2.DataValueField = "categoryId";
        //this.ddlPCategory2.DataSource = dtP;
        //this.ddlPCategory2.DataBind();
        //this.ddlPCategory2.Items.Insert(0, new ListItem("全部", ""));

        DataTable dt = new NGShop.Bll.TableObject("T_Base_UserType").Util_GetList("*", "1=1", "orderBy");
        this.CheckBoxList1.DataTextField = "typeName";
        this.CheckBoxList1.DataValueField = "typeId";
        this.CheckBoxList1.DataSource = dt;
        this.CheckBoxList1.DataBind();
        this.CheckBoxList1.Items.Add(new ListItem("全部", "9999"));
    }
    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        ModelTestQueston model = new ModelTestQueston();
        model.sysnumber = Guid.NewGuid().ToString();
        model.questionTitle = this.al.Value;
        model.questionType = this.questionType;
        model.Description = "";
        model.Orner = "0";  
        model.categoryFlag = ddlPCategory2.SelectedValue;
        model.personFlag = GetTypes();
        model.createPerson = this.UserName;
        model.categorypath = getCategoryPath(ddlPCategory2.SelectedValue);
        model.createTime = DateTime.Now;
        model.orderBy = 0;
        new BllTestQueston().Add(model);
        BindNews(0);
    }

    public string getCategoryPath(string id)
    {
        return "";
        //string path = "";
        ////ModCourseCategory mod = new ModCourseCategory();
        ////BllCourseCategory bll = new BllCourseCategory();
        //mod = bll.GetModel(id);
        //if (mod != null)
        //{
        //    path = mod.CategoryPath;
        //    if (path == "") { path = mod.CategoryId; }
        //}
        //return path;
    }

    public string GetTypes()
    {
        string s = "";
        foreach (ListItem item in this.CheckBoxList1.Items) { if (item.Selected) { s += item.Value + ","; } }
        return s;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.pageBar.PageIndex = 0;
        BindNews(this.pageBar.PageIndex);
    }
}
