using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using StarTech.ELife;
using StarTech.ELife.Base;
using StarTech.ELife.Article;
using Startech.Category;
using Startech.Utils;
using StarTech.DBUtility;

public partial class AppModules_Sysadmin_Article_FreeCut : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    private int PageIndex = 1;
    protected string _Pid = "";
    private string _typeId = "";
    protected string disImg = "0";
    ArticleBll bll = new ArticleBll();
    ArticleModel model = new ArticleModel();
    MarketBll marketBll = new MarketBll();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], Int32.MaxValue, " |#|,|-".Split('|')));
        if (!IsPostBack)
        {
            BindNewsCateoryDrp();
            BindMarket();//绑定所有农贸市场
            if (_Pid != "")
            {
                _pageTitle = "文章信息编辑";
                GetArticleInfo();
                this.palCheck.Visible = false;
            }
            else
            {
                _pageTitle = "文章信息添加";
                this.palCheck.Visible = false;
                this.txtReleaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            ViewState["ImgLink"] = "";
        }
    }
    #region 绑定所有农贸市场
    public void BindMarket()
    {
        DataTable dt = marketBll.GetAllList().Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.ddlMarket.Items.Add(new ListItem(dt.Rows[i]["Market_name"].ToString(), dt.Rows[i]["Market_id"].ToString()));
        }
    }
    #endregion


    #region 绑定文章类别下拉框
    /// <summary>
    /// 绑定文章类别下拉框列表
    /// </summary>
    private void BindNewsCateoryDrp()
    {
        CategoryBLL bll = new CategoryBLL();

        ArrayList items = bll.GetSortedArticleCategoryItems(6, "176");
        IEnumerator e = items.GetEnumerator();
        while (e.MoveNext())
        {
            CategoryEntity item = (CategoryEntity)e.Current;
            this.ddlType.Items.Add(new ListItem(item.Name, item.Id));
        }
    }
    #endregion

    #region 文章信息
    public void GetArticleInfo()
    {
        if (!string.IsNullOrEmpty(_Pid)) model = bll.GetModel(Convert.ToInt32(_Pid));
        if (model != null)
        {
            this.ddlType.SelectedValue = model.CategoryId.ToString();//文章所属类别
            this.txtTitle.Text = model.Titie;//文章标题
            this.ddlMarket.SelectedValue = model.MarketId.ToString();
            //this.txtReleaseDate.Text = model.ReleaseDate.ToString("yyyy-MM-dd");//发布日期
            this.fckBody.Text = model.Body;//文章内容
            //this.radioApproved.SelectedValue = model.Approved.ToString();//审核状态
        }
    }
    #endregion


    #region 校验数据
    protected string Validates()
    {
       
        return "";
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

        string strError = Validates();
        if (strError != "")
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script>alert('" + strError + "');</script>");
            return;
        }
        model.CategoryId = Convert.ToInt32(this.ddlType.SelectedValue);
        model.MarketId = Convert.ToInt32(this.ddlMarket.SelectedValue);
        model.Titie = SafeRequest.GetFormString("txtTitle");
        model.ReleaseDate = System.DateTime.Now;
        model.AddedDate = System.DateTime.Now;
        model.ExpireDate = System.DateTime.Now;
        model.Body = SafeRequest.GetFormString("fckBody");
        model.Approved = 1;
        model.AddedUserId = Convert.ToInt32(this.UserId);//默认登录进来人的编号
        if (_Pid != "")
        {
            model.ArticleId = Convert.ToInt32(_Pid);
            if (bll.Update(model))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
        else
        {
            if (bll.Add(model) > 0)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('添加成功');layer_close_refresh();</script>");
            }
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

    #region 删除图片
    protected void btnDeleteImg_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region 判断上传图片格式是否正确
    public static bool IsPic(string uploadFileLastName)
    {
        string lastNameFilter = "jpeg,jpg,bmp,gif,png";
        string lastName = uploadFileLastName.Substring(uploadFileLastName.LastIndexOf('.') + 1);
        if (lastNameFilter.IndexOf(lastName) < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
}