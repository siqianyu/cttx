using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Startech.Utils;
using Startech.Link;
using System.Data;
using System.Collections;

public partial class AppModules_Link_AddLink : StarTech.Adapter.StarTechPage
{

    LinkBLL bll = new LinkBLL();
    LinkModel model = new LinkModel();
    public string _type = "1";

    private string _Lid ,rd;
    protected string _pageTitle = string.Empty;
    private int PageIndex = SafeRequest.GetQueryInt("PageIndex", 0);
    public string mainPath = ConfigurationManager.AppSettings["LinkPicUrl"];
    protected void Page_Load(object sender, EventArgs e)
    {

        rd = Request.QueryString["rd"] == null ? "0" : KillSqlIn.Url_ReplaceByNumber(Request.QueryString["rd"].ToString(), 5);
        _Lid = Request.QueryString["LinkId"] == null ? "0" : KillSqlIn.Url_ReplaceByNumber(Request.QueryString["LinkId"].ToString(), 5);
        if (!IsPostBack)
        {
            BindLinkCateory();
            CheckPopedom();

            if (rd == "1")
            {
                SetReadOnlyPanel("readonly_input");
                LinkButton1.Visible = false;
            }
        }

    }


    #region
    public void SetReadOnlyPanel(string style)
    {
        string js = "<script>$(document).ready(function(){";
        js += "$('input[type=text]').each(function(){$(this).attr('className','" + style + "');$(this).attr('readonly','readonly');});";
        js += "$('textarea').each(function(){$(this).css('border','0px');$(this).attr('readonly','readonly');});";
        js += "$('select').each(function(){$(this).attr('disabled','disabled');});";
        js += "$('input[type=radio]').each(function(){$(this).attr('disabled','disabled');})";
        js += "});</script>";
        ClientScript.RegisterStartupScript(this.GetType(), this.GetType().ToString(), js);
    }
    #endregion



    #region 权限判断
    protected void CheckPopedom()
    {
        if (_Lid != "0")
        {
            _pageTitle = "友情连接信息编辑";
            GetLinkInfo();
        }
        else
        {
            _pageTitle = "友情连接信息添加";
            ViewState["ImgLink"] = "";
        }
    }
    #endregion

    #region 获得产品详细信息
    protected void GetLinkInfo()
    {
        int Lid = Int32.Parse(_Lid);
        model = bll.GetModel(Lid);
        if (model != null)
        {
            this._type = model.DisplayMode.ToString();
            this.txtTitle.Text = model.Title;
            this.RadioButtonList1.SelectedValue = model.DisplayMode.ToString();
            this.DrCategory.SelectedValue = model.CategoryId.ToString();
            this.txtLink.Text = model.Link;
            this.txtSort.Text = model.Sort.ToString();
            //显示图片
            if (model.Image != "")
            {
                ViewState["ImgLink"] = model.Image;
                this.uploadimage.ImageUrl = mainPath + model.Image;
            }
            else
            {
                ViewState["ImgLink"] = "";
                this.uploadimage.ImageUrl = "../../images/nopic.jpg";
            }
        }
    }
    #endregion

    //保存
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        model.Title = SafeRequest.GetFormString("txtTitle");
        model.DisplayMode = SafeRequest.GetFormInt("RadioButtonList1", 0);
        model.CategoryId = SafeRequest.GetFormInt("DrCategory", 0);
        model.Link = txtLink.Text.Trim();
        model.Sort = SafeRequest.GetFormInt("txtSort", 0);

        #region 保存图片
        if (!this.picUpload.Value.Equals(""))
        {
            string UploadFileLastName = this.picUpload.PostedFile.FileName.Substring(this.picUpload.PostedFile.FileName.LastIndexOf(".") + 1);//得到文件的扩展名

            if (!PubFunction.IsPic(UploadFileLastName))
            {
                JSUtility.Alert("上传图片格式不正确!");
                return;
            }
            Random rnd = new Random();
            string UpLoadFileTime = DateTime.Now.ToString("HHmmss") + rnd.Next(9999).ToString("0000"); //生成一个新的数图片名称
            string fileName = UpLoadFileTime + "." + UploadFileLastName;//产生上传图片的名称

            string SaveFile = DateTime.Now.ToString("yyyy/MM/dd/").Replace("-", "/");

            #region 设置保存的路径
            string SevedDirectory = System.Web.VirtualPathUtility.Combine(mainPath, SaveFile);
            string phydic = MapPath(SevedDirectory);

            if (!System.IO.Directory.Exists(phydic))
            {
                System.IO.Directory.CreateDirectory(phydic);
            }
            #endregion

            this.picUpload.PostedFile.SaveAs(phydic + "" + fileName);
            string fckimg = string.Empty;
            string source = string.Empty;
            fckimg = "<a href='" + this.ResolveUrl(SevedDirectory + fileName) + "' target='_blank'  id='upannexx'>" + fileName + "</a>";
            source = this.ResolveUrl(SevedDirectory + fileName);

            model.Image = SaveFile + fileName;
        }
        else
        {
            model.Image = ViewState["ImgLink"].ToString();
        }

        #endregion

        string BackUrl = "LinkList.aspx?PageIndex=" + PageIndex;

        if (_Lid != "0")
        {
            model.LinkId = Convert.ToInt32(_Lid);
            bll.Update(model);
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");

            //状态
            if (Session["LinkStatus_Hashtable"] != null)
            {
                Hashtable hTable = (Hashtable)Session["LinkStatus_Hashtable"];
                hTable["ReadSessionStatus"] = "1";
                Session["LinkStatus_Hashtable"] = hTable;
            }

            /*日志归档*/
            string sql = @"select l.title from T_link l where linkid=" + this._Lid + "";
            PubFunction.InsertLog("其它管理", "友情链接", "友情链接列表", "修改", sql, _Lid);
        }
        else
        {
            int i = bll.Add(model);
            JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
 

            /*日志归档*/
            string sql = @"select l.title from T_link l where linkid=" + i.ToString() + "";
            PubFunction.InsertLog("其它管理", "友情链接", "友情链接列表", "添加", sql, i.ToString());
        }
    }

    /// <summary>
    /// 绑定友情连接类别下拉框
    /// </summary>
    private void BindLinkCateory()
    {
        LinkCategoryBLL bll = new LinkCategoryBLL();
        DataSet ds = bll.GetAllCategoryItems();
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.DrCategory.DataTextField = "Category";
            this.DrCategory.DataValueField = "Id";
            this.DrCategory.DataSource = ds;
            this.DrCategory.DataBind();
            ListItem item = new ListItem("==请选择类别==", "0");
            this.DrCategory.Items.Insert(0, item);
        }
    }

}