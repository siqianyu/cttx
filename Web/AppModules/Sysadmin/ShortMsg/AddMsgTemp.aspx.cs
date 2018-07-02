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
using System.Collections.Generic;

public partial class AppModules_Sysadmin_ShortMsg_AddMsgTemp : StarTech.Adapter.StarTechPage
{
    /// <summary>
    /// 增加模板列表
    /// </summary>
    ShortMsgTempBll bll = new ShortMsgTempBll();
    ShortMsgTempModel model = new ShortMsgTempModel();
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
            BindSupplierCateoryDrp();
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
            _pageTitle = "模板信息编辑";
            if (_rd == "1")
            {
                _pageTitle = "模板信息查看";
                this.btnSave.Visible = false;
            }
            GetMsgSupInfo();
        }
        else
        {
            _pageTitle = "模板添加";
            //txtAddDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    #endregion

    protected void BindSupplierCateoryDrp()
    {
        ShortMsgSupBll supBll = new ShortMsgSupBll();
        List<ShortMsgSupModel> lists = supBll.GetModelList("isUse=1");
        ddlSupplierCategory.DataSource = lists;
        ddlSupplierCategory.DataValueField = "supplierId";
        ddlSupplierCategory.DataTextField = "supplierName";
        ddlSupplierCategory.DataBind();
    }
    #region 修改时初始化页面
    /// <summary>
    /// 获得文章详细信息
    /// </summary>
    protected void GetMsgSupInfo()
    {
        model = bll.GetModel(this._id);
        if (model != null)
        {
            this.txtCode.Text = model.templateCode;
            if (!string.IsNullOrEmpty(model.flag))
            {
                this.ddlParentCategory.Items.FindByValue(model.flag).Selected = true;
            }
            if (!string.IsNullOrEmpty(model.supplierId))
            {
                this.ddlSupplierCategory.Items.FindByValue(model.supplierId).Selected = true;
            }
            this.txtParam.Text = model.templateParam;
        }
    }
    #endregion

    protected void FillFeild()
    {
        if (_id != "0")
        {
            model = bll.GetModel(this._id);
            model.templateId = this._id;
        }
        else
        {
            model.templateId = IdCreator.CreateId("T_ShortMessage_Template", "templateId");
            model.isUse = 1;
        }
        model.flag = this.ddlParentCategory.SelectedValue; //标识
        model.supplierId = this.ddlSupplierCategory.SelectedValue;//供应商
        if (!string.IsNullOrEmpty(model.supplierId))
        {
            ShortMsgSupModel modelSup = new ShortMsgSupBll().GetModel(model.supplierId);
            if (modelSup!=null)
            {
                model.supplierFlag = modelSup.flag;
            }
        }
        model.templateCode = KillSqlIn.Form_ReplaceByString(Request.Form["txtCode"], 200);
        model.templateParam = KillSqlIn.Form_ReplaceByString(Request.Form["txtParam"], 200);
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