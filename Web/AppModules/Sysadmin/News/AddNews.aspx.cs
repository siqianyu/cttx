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
using StarTech.ELife.News;
using Startech.Category;
using Startech.Utils;
using StarTech.DBUtility;

public partial class AppModules_Sysadmin_News_AddNews : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    private int PageIndex = 1;
    protected string _Pid = "";
    private string _typeId = "";
    protected string disImg = "0";
    NewsBll bll = new NewsBll();
    NewsModel model = new NewsModel();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], 30));
        _typeId = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["typeId"], 30));
        if (!IsPostBack)
        {
            InitFooter();   //底部默认信息
            ViewState["ImgLink"] = "";
            BindNewsCateoryDrp();
            if (_Pid != "SysError")
            {
                _pageTitle = "新闻信息编辑";
                GetNewsInfo();

            }
            else
            {
                _pageTitle = "新闻信息添加";
            }
            
        }
    }

    protected void InitFooter()
    {
        NG.CachHelper.Redis.RedisHelper redis = new NG.CachHelper.Redis.RedisHelper();
        this.fckBodyFooter.Text = redis.GetStringCash("News_Footer");
        redis.Close();
    }


    #region 绑定新闻类别下拉框
    /// <summary>
    /// 绑定新闻类别下拉框列表
    /// </summary>
    private void BindNewsCateoryDrp()
    {
        CategoryBLL bll = new CategoryBLL();

        ArrayList items = bll.GetSortedArticleCategoryItems(6, "2");
        IEnumerator e = items.GetEnumerator();
        while (e.MoveNext())
        {
            CategoryEntity item = (CategoryEntity)e.Current;
            this.ddlType.Items.Add(new ListItem(item.Name, item.Id));
        }
        if (!string.IsNullOrEmpty(_typeId) && !_typeId.Equals("null"))
        {
            this.ddlType.SelectedValue = _typeId;
        }
    }
    #endregion

    #region 新闻信息
    public void GetNewsInfo()
    {
        model = bll.GetModel(_Pid);
        if (model != null)
        {
            this.ddlType.SelectedValue = model.CategoryId.ToString();//新闻所属类别
            this.txtTitle.Text = model.Title;//新闻标题
            this.txtSubHead.Text = model.SubHead;//新闻副标题
            this.txtUnit.Text = model.PublicationUnit;//新闻作者
            this.txtFromSouce.Text = model.FromSource;//新闻来源
            //this.txtKeyWord.Text = model.KeyWord;//新闻关键字
            this.txtReleaseDate.Text = Convert.ToDateTime(model.ReleaseDate).ToString("yyyy-MM-dd");//发布日期
            //this.radioTopList.SelectedValue = model.IsTop.ToString();//是否置顶
            this.radioIndexCommentList.SelectedValue = model.IndexCommend.ToString();//是否首页推荐
            this.rbtIstop.SelectedValue = model.IsTop.ToString();//是否置顶           
            this.rbtHot.SelectedValue = model.HotPic.ToString();//是否显示‘news’
            this.txtHotDays.Text = model.HotDays.ToString();//'news'显示天数（默认为3天） 
            this.txtSort.Text = model.Sort.ToString();//排序编号，数字越小排序越靠前
            if (model.ArticleType.ToString() == "1")
            {
                this.picUpload.Visible = true;
            }
            this.fckBody.Text = model.Body;//新闻内容
            this.radioApproved.SelectedValue = model.Approved.ToString();//审核状态
            //显示图片
            if (model.ImgLink != "")
            {
                ViewState["ImgLink"] = model.ImgLink;
                this.uploadimage.Src = "~" + model.ImgLink;
            }
            else
            {
                ViewState["ImgLink"] = "";
                this.uploadimage.Src = "../../../Images/nopic.jpg";
            }
        }
    }
    #endregion


    #region 校验数据
    protected string Validates()
    {
        if (ddlType.SelectedValue.Trim() == "2")
        {
            return "请选择新闻类别!";
        }
        if (string.IsNullOrEmpty(txtTitle.Text))
        {
            return "新闻标题不能为空";
        }
        if (string.IsNullOrEmpty(fckBody.Text))
        {
            return "新闻内容不能为空!";
        }
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
        if (!string.IsNullOrEmpty(strError))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script>alert('" + strError + "');</script>");
            return;
        }
        model.CategoryId = Convert.ToInt32(this.ddlType.SelectedValue);
        model.Title = SafeRequest.GetFormString("txtTitle");
        model.SubHead = SafeRequest.GetFormString("txtSubHead");
        model.PublicationUnit = SafeRequest.GetFormString("txtUnit");
        model.FromSource = SafeRequest.GetFormString("txtFromSouce");
        // model.KeyWord = SafeRequest.GetFormString("txtKeyWord");
        model.ReleaseDate = !string.IsNullOrEmpty(this.txtReleaseDate.Text.Trim()) ? Convert.ToDateTime(this.txtReleaseDate.Text.Trim()) : System.DateTime.Now;
        model.IsTop = 0;    //SafeRequest.GetFormInt("radioTopList", 0);
        model.IndexCommend = 0;     //SafeRequest.GetFormInt("radioIndexCommentList", 0);
        model.HotPic = SafeRequest.GetFormString("radioHotList");
        model.HotDays = SafeRequest.GetFormInt("txtHotDays", 3);
        //model.ArticleType = SafeRequest.GetFormInt("radioArticleTypeList", 0);
        model.Body = this.fckBody.Text;
        
        //追加底部
        if (cbFooter.Checked)
        {
            if (model.Body.IndexOf("start_auto_footer") == -1 && model.Body.IndexOf("end_auto_footer") == -1)
            {
                string autoFooter = "<!--start_auto_footer-->" + this.fckBodyFooter.Text + "<!--end_auto_footer-->";
                model.Body = model.Body + autoFooter;
            }
           //写入缓存
            NG.CachHelper.Redis.RedisHelper redis = new NG.CachHelper.Redis.RedisHelper();
            redis.SetStringCash("News_Footer", this.fckBodyFooter.Text);
            redis.Close();
        }

        //   model.IsComment = 0;// SafeRequest.GetFormInt("radioIsComment", 0);
        model.Approved = SafeRequest.GetFormInt("radioApproved", 0);
        model.Sort = SafeRequest.GetFormInt("txtSort", 99);
        model.AddedDate = System.DateTime.Now;
        model.ExpireDate = System.DateTime.Now;
        model.ViewCount = 0;
        // model.IsScrool = 0;
        // model.AddedUserId = Convert.ToInt32(_userId);//默认登录进来人的编号
        model.AddedUserId = 1;
        //~/upload/News/
        #region 保存图片
        if (!this.picUpload.Value.Equals("") && this.picUpload.PostedFile.ContentLength>10)
        {
            string FileTZM = this.picUpload.PostedFile.FileName.Substring(this.picUpload.PostedFile.FileName.LastIndexOf(".") + 1);//得到文件的扩展名

            if (!IsPic(FileTZM.ToLower()))
            {
                JSUtility.Alert("上传图片格式不正确!");
                return;
            }
            if (this.picUpload.PostedFile.ContentLength > 1048576)
            {
                JSUtility.Alert("上传图片过大!");
                return;
            }
            Random rnd = new Random();
            string UpLoadFileTime = DateTime.Now.ToString("yyMMddHHmmss") + rnd.Next(9999).ToString("0000"); //生成一个新的数图片名称
            string fileName = UpLoadFileTime + "." + FileTZM;//产生上传图片的名称
            if (!Directory.Exists(Request.MapPath("~/upload/News/")))
            {
                Directory.CreateDirectory(Request.MapPath("~/upload/News/"));
            }
            string Url = Request.MapPath("~/upload/News/" + fileName);
            picUpload.PostedFile.SaveAs(Url);
            model.ImgLink = "/upload/News/" + fileName;
        }
        else
        {
            model.ImgLink = ViewState["ImgLink"].ToString();
        }
        #endregion


        if (_Pid != "")
        {
            model.NewsID = _Pid;
            if (bll.Update(model))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
        else
        {
            model.NewsID = IdCreator.CreateId("T_News", "NewsId");
            if (bll.Add(model))
            {
                JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
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
        string str = "update T_News set ImgLink='' where NewsID='" + this._Pid + "'";
        adohelper.ExecuteSqlNonQuery(str);
        this.uploadimage.Src = "../../../Images/nopic.jpg";
        this.btnDeleteImg.Visible = false;
        ViewState["ImgLink"] = "";
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