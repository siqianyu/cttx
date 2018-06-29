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

public partial class AppModules_Sysadmin_Base_AddTS : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    private int PageIndex = 1;
    protected string _Pid = "";
    private string _rd = "";
    protected string disImg = "0";
    TsBll bll = new TsBll();
    TsModel model = new TsModel();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], 30));
        if (!IsPostBack)
        {
            if (_Pid != "SysError")
            {
                _pageTitle = "广告信息编辑";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "广告信息添加";
            }
            
        }
    }


    #region 信息
    public void GetAreaInfo()
    {
        if (_Pid != "SysError")
        {
            model = _Pid.Contains("|") ? bll.GetModel(Convert.ToInt32(_Pid.Replace("|", ""))) : bll.GetModel(Convert.ToInt32(_Pid));
        }
        if (model != null)
        {
            this.txtSubHead.Text = model.Subject;  //投诉主题
            this.txtUnit.Text = model.SSName;   //投诉人姓名
            this.txtFromSouce.Text = model.SSAdd;
            this.txtteleph.Text = model.SSTel;
            this.txtCode.Text = model.SSPost;
            this.txtEmail.Text = model.Email;
            this.txtProduct.Text = model.ProductName;
            this.txtContent.Text = GetTextFromHtml(model.ParticularInfo.Trim());
            if (model.AnswerTime != null && model.AnswerTime.ToString() != "" && model.AnswerTime > new DateTime(2014, 1, 1))
                txtReleaseDate.Text = Convert.ToDateTime(model.AnswerTime).ToString("yyyy-MM-dd");
            else
                txtReleaseDate.Text = DateTime.Now.ToShortDateString();

            if (model.Answer != null && model.Answer != "")
            {
                this.fckBody.Text = model.Answer;
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
        //model.Title = KillSqlIn.Form_ReplaceByString(this.txtTitle.Text, 100);
        //model.DisplayMode = Convert.ToInt32(this.RadioButtonList1.SelectedValue);
        //model.CategoryId = Convert.ToInt32(this.DrCategory.SelectedValue);
        //model.Link = KillSqlIn.Form_ReplaceByString(this.txtLink.Text.Trim(), 100);
        //model.sort = Convert.ToInt32(KillSqlIn.Form_ReplaceByString(this.txtSort.Text, 10));
        model = bll.GetModel(Convert.ToInt32(_Pid));
        model.ID = Convert.ToInt32(_Pid);
        model.IsCheck = 1;
        model.AnswerTime = Convert.ToDateTime(this.txtReleaseDate.Text.Trim());
        model.Answer = this.fckBody.Text;
        if (bll.Update(model))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('您已成功回复该投诉信息');layer_close_refresh();</script>");
        }
    }
    #endregion


    #region html代码转换成文本
    private string GetTextFromHtml(string str)
    {
        string dst = str;
        dst = dst.Replace("&lt;", "<");
        dst = dst.Replace("&rt;", ">");
        dst = dst.Replace("&quot;", "\"");
        dst = dst.Replace("&#039;", "'");
        dst = dst.Replace("&nbsp;", " ");
        dst = dst.Replace("<br>", "\r\n");
        dst = dst.Replace("<br>", "\r");
        dst = dst.Replace("<br>", "\n");

        return dst;
    }
    #endregion
}