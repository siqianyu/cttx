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
using StarTech.ELife.ShortMsg;

public partial class AppModules_Sysadmin_ShortMsg_AddMsgSup : StarTech.Adapter.StarTechPage
{
    /// <summary>
    /// 增加供应商列表
    /// </summary>
    ShortMsgSupBll bll = new ShortMsgSupBll();
    ShortMsgSupModel model = new ShortMsgSupModel();
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
            _pageTitle = "供应商信息编辑";
            if (_rd == "1")
            {
                _pageTitle = "供应商信息查看";
                this.btnSave.Visible = false;
            }
            GetMsgSupInfo();
        }
        else
        {
            _pageTitle = "供应商添加";
            //txtAddDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    #endregion


    #region 修改时初始化页面
    /// <summary>
    /// 获得文章详细信息
    /// </summary>
    protected void GetMsgSupInfo()
    {
        model = bll.GetModel(this._id);
        if (model != null)
        {
            this.txtTitle.Text = model.supplierName;
            if (!string.IsNullOrEmpty(model.flag))
            {
                this.ddlParentCategory.Items.FindByValue(model.flag).Selected = true;
            }
            this.txtKey.Text = model.accessKeyID;
            this.txtSecret.Text = model.accessKeySecret;
            this.txtSmsSign.Text = model.smsSign;
            this.txtSort.Text = model.sort.ToString();
            //this.txtSort.Text = model.Sort.ToString();
            //this.ddlParentCategory.SelectedValue = model.ParentCategoryId.ToString();
            //this.txtAddDate.Text = Convert.ToDateTime(model.AddedDate).ToString("yyyy-MM-dd");
        }
    }
    #endregion

    protected void FillFeild()
    {
        if (_id != "0")
        {
            model = bll.GetModel(this._id);
            model.supplierId = this._id;
        }
        else
        {
            model.supplierId = IdCreator.CreateId("T_ShortMessage_Supplier", "supplierId");
            model.isUse = 0;
            model.isDefault = 0;
        }
        model.flag = this.ddlParentCategory.SelectedValue;
        model.supplierName = KillSqlIn.Form_ReplaceByString(Request.Form["txtTitle"], 200);
        model.accessKeyID = KillSqlIn.Form_ReplaceByString(Request.Form["txtKey"], 200);
        model.accessKeySecret = KillSqlIn.Form_ReplaceByString(Request.Form["txtSecret"], 200);
        model.smsSign = KillSqlIn.Form_ReplaceByString(Request.Form["txtSmsSign"], 200);
        model.sort = int.Parse(KillSqlIn.Form_ReplaceByString(Request.Form["txtSort"], int.MaxValue));
    }
    #region 保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        FillFeild();
        if (_id != "0")
        {
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