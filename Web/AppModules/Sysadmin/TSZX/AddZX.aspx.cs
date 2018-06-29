using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using StarTech;
using System.IO;
using StarTech.ELife.ZXTS;

public partial class AppModules_Sysadmin_Base_AddZX : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    private int PageIndex = 1;
    protected string _Pid = "";
    private string _rd = "";
    protected string disImg = "0";
    ZxBll bll = new ZxBll();
    ZxModel model = new ZxModel();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], 30));
        _rd = KillSqlIn.Url_ReplaceByString(Request.QueryString["rd"], 10);
        if (!IsPostBack)
        {
            if (_Pid != "SysError")
            {
                _pageTitle = "咨询信息编辑";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "咨询信息添加";
            }
            if (_rd.Equals("1"))
            {
                this.btnSubmit.Visible = false;
            }
            
        }
    }

    #region 咨询信息
    public void GetAreaInfo()
    {
        if (_Pid != "SysError")
        {
            model = _Pid.Contains("|") ? bll.GetModel(Convert.ToInt32(_Pid.Replace("|", ""))) : bll.GetModel(Convert.ToInt32(_Pid));
        }
        if (model != null)
        {
            this.txtSubHead.Text = model.name;  //投诉主题
            this.txtUnit.Text = model.title;   //投诉人姓名
            this.txtFromSouce.Text = model.content;
            this.txtteleph.Text = Convert.ToDateTime(model.fillTime).ToString("yyyy-MM-dd");
            this.txtCode.Text = model.tel;
            this.txtContent.Text = model.replyPeople;
            if (model.state == 1)
            {
                this.txtReleaseDate.Text = Convert.ToDateTime(model.replyTime).ToString("yyyy-MM-dd");
            }
            else
            {
                this.txtReleaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (model.replyContent != null && model.replyContent != "")
            {
                this.fckBody.Text = model.replyContent;
            }
            else
            {
                this.fckBody.Text = "";
            }
        }
    }
    #endregion


    #region 提交
    /// <summary>
    /// 确定提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        model = bll.GetModel(Convert.ToInt32(_Pid));
        model.replyContent = this.fckBody.Text;
        model.replyPeople = KillSqlIn.Form_ReplaceByString(this.txtContent.Text.Trim(), 200);
        if (this.txtReleaseDate.Text.Trim() == "")
        {
            model.replyTime = DateTime.Now;
        }
        else
        {
            model.replyTime = DateTime.Parse(this.txtReleaseDate.Text.Trim());
        }
        model.state = 1;
        if (_Pid != "SysError")
        {
            model.id = Convert.ToInt32(_Pid);
        }
        if (_Pid != "SysError")
        {
            if (bll.Update(model))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('保存信息成功');layer_close_refresh();</script>");
            }
        }
    }
    #endregion

}