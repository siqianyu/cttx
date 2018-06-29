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
using Startech.Category;
using StarTech.Util;
using StarTech;

public partial class Sysadmin_Article_AddCategory : StarTech.Adapter.StarTechPage
{
    CategoryModel model = new CategoryModel();
    CategoryBLL bll = new CategoryBLL();

    private string _ACid ="";// SafeRequest.GetQueryInt("CategoryId", 0).ToString();
    private string _rd = "";//SafeRequest.GetQueryInt("rd", 0).ToString();
    private string _userId ="1";//HttpContext.Current.Request.Cookies["__UserInfo"]["userId"];
    protected string _pageTitle = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        _ACid = KillSqlIn.Url_ReplaceByNumber(Request.QueryString["CategoryId"], 10);
        _rd = KillSqlIn.Url_ReplaceByNumber(Request.QueryString["rd"], 10);
        if (!IsPostBack)
        {
            BindArticleCateoryDrp();
            CheckPopedom();
        }
    }

    #region 权限
    /// <summary>
    /// 权限判断
    /// </summary>
    protected void CheckPopedom()
    {
        if (_ACid != "0")
        {
            _pageTitle = "新闻类别信息编辑";
            if (_rd == "1")
            {
                _pageTitle = "新闻类别信息查看";
                this.btnSave.Visible = false;
            }
            GetArticleCategoryInfo();
        }
        else
        {
            _pageTitle = "新闻类别信息添加";
            txtAddDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    #endregion

    #region 绑定类别
    /// <summary>
    /// 绑定文章类别下拉框列表
    /// </summary>
    private void BindArticleCateoryDrp()
    {
        ArrayList items = bll.GetSortedArticleCategoryItems(6, "1");//1:文章类别 2:新闻类别
        IEnumerator e = items.GetEnumerator();
        while (e.MoveNext())
        {
            CategoryEntity item = (CategoryEntity)e.Current;
            this.ddlParentCategory.Items.Add(new ListItem(item.Name, item.Id));
        }
    }
    #endregion

    #region 修改时初始化页面
    /// <summary>
    /// 获得文章详细信息
    /// </summary>
    protected void GetArticleCategoryInfo()
    {
        int ACid = Int32.Parse(_ACid);
        model = bll.GetModel(ACid);
        if (model != null)
        {
            this.txtTitle.Text = model.CategoryName;
            this.txtSort.Text = model.Sort.ToString();
            this.ddlParentCategory.SelectedValue = model.ParentCategoryId.ToString();
            this.txtAddDate.Text = Convert.ToDateTime(model.AddedDate).ToString("yyyy-MM-dd");
        }
    }
    #endregion

    #region 保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        model.CategoryName = KillSqlIn.Form_ReplaceByString(Request.Form["txtTitle"], 200);
        model.Sort = Convert.ToInt32(KillSqlIn.Form_ReplaceByNumber(Request.Form["txtSort"], 4));
        model.AddedUserId = Convert.ToInt32(_userId);
        model.AddedDate = Convert.ToDateTime(this.txtAddDate.Text);
        model.ParentCategoryId = Convert.ToInt32(this.ddlParentCategory.SelectedValue);
        model.Type = 0;//0:文章  1:新闻

        //string BackUrl = "CategoryTree.aspx";

        if (_ACid != "0")
        {
            model.CategoryId = Convert.ToInt32(_ACid);
            bll.Update(model);
            ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('修改成功!');layer_close_refresh();</script>");
            //JSUtility.AlertAndRedirect("修改成功!", BackUrl);

            /*日志归档*/
            //string sql1 = @"select Title as title from dbo.T_Category  where CategoryId = (" + _ACid + ")";
            //string function = "修改";
            //PubFunction.InsertLog1("文章类别管理", sql1, function);
        }
        else
        {
            model.AddedDate = DateTime.Now;
            int id = bll.Add(model);
            bll.UpdateCategoryPermission(id.ToString());
            JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());

            /*日志归档*/
            //string sql1 = @"select Title as title from dbo.T_Category  where CategoryId = (select top 1 CategoryId from T_Category order by CategoryId desc)";
            //string function = "添加";
            //PubFunction.InsertLog1("文章类别管理", sql1, function);
        }
    }
    #endregion

}
