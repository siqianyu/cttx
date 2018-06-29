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
using StarTech.ELife.Ad;

public partial class AppModules_Sysadmin_Base_AddAd : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    private int PageIndex = 1;
    protected string _Pid = "";
    protected string disImg = "0";
    AdBll bll = new AdBll();
    AdModel model = new AdModel();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], 30, " |#|,".Split('|')));
        BindPArea();
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

    #region 绑定所有广告类别
    public void BindPArea()
    {
        DataTable dt = bll.GetAllList().Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //this.ddlPid.Items.Add(new ListItem(dt.Rows[i]["area_name"].ToString(), dt.Rows[i]["area_id"].ToString()));
        }
            
        
    }
    #endregion

    #region 广告信息
    public void GetAreaInfo()
    {
        model = bll.GetModel(Convert.ToInt32(_Pid));
        if (model != null)
        {
            this.txtTitle.Text = model.Title;
            this.txtLink.Text = model.Link;
            this.txtSort.Text = model.sort.ToString();
            if (!string.IsNullOrEmpty(model.Image))
            {
                ViewState["Image"] = model.Image;
                this.uploadimage.ImageUrl = "~" + model.Image;
                disImg = "1";
            }
            else
            {
                ViewState["Image"] = "";
                this.uploadimage.ImageUrl = "../images/nopic.jpg";
            }
            this.DrCategory.SelectedValue = model.CategoryId.ToString();
            this.RadioButtonList1.SelectedValue = model.DisplayMode.ToString();
            this.txtStartTime.Text = Convert.ToDateTime(model.StartTime).ToString("yyyy-MM-dd");
            this.txtEndTime.Text = Convert.ToDateTime(model.EndTime).ToString("yyyy-MM-dd");
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
        model.Title = KillSqlIn.Form_ReplaceByString(this.txtTitle.Text, 100);
        model.DisplayMode = Convert.ToInt32(this.RadioButtonList1.SelectedValue);
        model.CategoryId = Convert.ToInt32(this.DrCategory.SelectedValue);
        model.Link = KillSqlIn.Form_ReplaceByString(this.txtLink.Text.Trim(), 100);
        model.sort = Convert.ToInt32(KillSqlIn.Form_ReplaceByString(this.txtSort.Text, 10));
        model.StartTime = !string.IsNullOrEmpty(this.txtStartTime.Text) ? Convert.ToDateTime(this.txtStartTime.Text) : System.DateTime.Now;
        model.EndTime = !string.IsNullOrEmpty(this.txtEndTime.Text) ? Convert.ToDateTime(this.txtEndTime.Text) : System.DateTime.Now.AddDays(7);
        #region 保存图片
        if (!this.picUpload.Value.Equals(""))
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
            string UpLoadFileTime = DateTime.Now.ToString("HHmmss") + rnd.Next(9999).ToString("0000"); //生成一个新的数图片名称
            string fileName = UpLoadFileTime + "." + FileTZM;//产生上传图片的名称
            if (!Directory.Exists(Request.MapPath("~/upload/Link/")))
            {
                Directory.CreateDirectory(Request.MapPath("~/upload/Link/"));
            }
            string Url = Request.MapPath("~/upload/Link/" + fileName);
            picUpload.PostedFile.SaveAs(Url);
            model.Image = "/upload/Link/" + fileName;
        }
        else
        {
            model.Image = ViewState["Image"] == null ? "" : ViewState["Image"].ToString();
        }
        #endregion
        if (_Pid != "SysError")
        {
            model.AdId = Convert.ToInt32(_Pid);
            if (bll.Update(model))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
        else
        {
            if (bll.Add(model) != 0)
            {
                JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
            }
        }
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