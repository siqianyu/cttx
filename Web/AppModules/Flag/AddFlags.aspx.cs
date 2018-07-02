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
using StarTech.DBUtility;
using StarTech.ELife.Flag;

public partial class AppModules_Flag_AddFlags : StarTech.Adapter.StarTechPage
{
    FlagBll bll = new FlagBll();
    FlagModel model = new FlagModel();
    private string _id = "";// SafeRequest.GetQueryInt("CategoryId", 0).ToString();
    private string _rd = "";//SafeRequest.GetQueryInt("rd", 0).ToString();
    private string _userId = "1";//HttpContext.Current.Request.Cookies["__UserInfo"]["userId"];
    protected string _pageTitle = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        _id = KillSqlIn.Url_ReplaceByNumber(Request.QueryString["id"], 10);
        _rd = KillSqlIn.Url_ReplaceByNumber(Request.QueryString["rd"], 10);
        if (!IsPostBack)
        {
            //BindArticleCateoryDrp();
            CheckPopedom();
        }
    }

    #region 权限
    /// <summary>
    /// 权限判断
    /// </summary>
    protected void CheckPopedom()
    {
        if (_id != "0")
        {
            _pageTitle = "标签信息编辑";
            if (_rd == "1")
            {
                _pageTitle = "标签信息查看";
                this.btnSave.Visible = false;
            }
            GetFlagInfo();
        }
        else
        {
            _pageTitle = "标签添加";
            //txtAddDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    #endregion


    #region 修改时初始化页面
    /// <summary>
    /// 获得文章详细信息
    /// </summary>
    protected void GetFlagInfo()
    {
        model = new FlagBll().GetModel(this._id);
        if (model != null)
        {
            this.txtTitle.Text = model.flag_name;
            //this.txtSort.Text = model.Sort.ToString();
            //this.ddlParentCategory.SelectedValue = model.ParentCategoryId.ToString();
            //this.txtAddDate.Text = Convert.ToDateTime(model.AddedDate).ToString("yyyy-MM-dd");
        }
    }
    #endregion

    #region 保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        model.flag_name = KillSqlIn.Form_ReplaceByString(Request.Form["txtTitle"], 200);

        //string BackUrl = "CategoryTree.aspx";

        if (_id != "0")
        {
            model.flag_id = _id;
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
            model.if_use = 1;
            model.flag_id = IdCreator.CreateId("T_Base_Flag", "flag_id");
            bll.Add(model);
            JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());

            /*日志归档*/
            //string sql1 = @"select Title as title from dbo.T_Category  where CategoryId = (select top 1 CategoryId from T_Category order by CategoryId desc)";
            //string function = "添加";
            //PubFunction.InsertLog1("文章类别管理", sql1, function);
        }
    }
    #endregion
}