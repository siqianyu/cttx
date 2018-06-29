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

public partial class Components_TopButtons : System.Web.UI.UserControl
{
    public string upImg;
    public string downImg;

    protected void Page_Load(object sender, EventArgs e)
    {
        upImg = this.ResolveUrl("~/Images/jt_up.jpg");
        downImg = this.ResolveUrl("~/Images/jt_down.jpg");
        SetButtonSkinColor();
    }

    public string ControlPanelID
    {
        set { ViewState["ControlPanelID"] = value; }
        get { return (ViewState["ControlPanelID"] == null) ? "" : ViewState["ControlPanelID"].ToString(); }
    }

    public string ButtonSkinColor
    {
        set { ViewState["ButtonSkinColor"] = value; }
        get { return (ViewState["ButtonSkinColor"] == null) ? "" : ViewState["ButtonSkinColor"].ToString(); }
    }

    public void SetButtonSkinColor()
    {
        if (ButtonSkinColor != "" && ButtonSkinColor != "blue")
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType().Name == "ImageButton")
                {
                    ImageButton btn = (ImageButton)control;
                    btn.ImageUrl = btn.ImageUrl.Replace("/blue/", "/" + ButtonSkinColor + "/");
                }

            }
        }
    }

    #region 按钮访问权限(结合权限系统)
    /// <summary>
    /// 验证
    /// </summary>
    public void SetButtonsPermissions(int menuId, string groupIds)
    {
        string ids = new StarTech.Adapter.IACenter().GetAllButtons(menuId, groupIds);
        ids = "," + ids + ",";

        //IACenter_Button
        //1	新增	add
        //2	删除	delete
        //3	修改	edit
        //4	查询	search
        //5	查看	show
        //6	权限设置	pemissionset
        //7	审核	approved
        //8	取消审核 	unapproved

        if (ids.IndexOf(",1,") == -1) { this.btnAdd.Visible = false; }
        if (ids.IndexOf(",2,") == -1) { this.btnDelete.Visible = false; }
        if (ids.IndexOf(",3,") == -1) { this.btnEdit.Visible = false; }
        if (ids.IndexOf(",4,") == -1) { this.btnSearch.Visible = false; }
        if (ids.IndexOf(",5,") == -1) { this.btnShow.Visible = false; }
        //if (ids.IndexOf(",6,") == -1) { this.btnAdd.Visible = false; }
        if (ids.IndexOf(",7,") == -1) { this.btnSH.Visible = false; }
        if (ids.IndexOf(",8,") == -1) { this.btnNSH.Visible = false; }

    }
    #endregion

    /// <summary>
    /// checkbox当前选择的值(多个用逗号(,)隔开)
    /// </summary>
    public string CheckBoxValues
    {
        get { return this.hidCheckBoxValues.Value; }
    }

    public Image GetImage(string imgName)
    {
        switch (imgName)
        {
            default:
                return null;
        }
    }

    public ImageButton GetImageButton(string btnName)
    {
        switch (btnName)
        {
            case "add":
                return this.btnAdd;
            case "edit":
                return this.btnEdit;
            case "search":
                return this.btnSearch;
            case "delete":
                return this.btnDelete;
            case "show":
                return this.btnShow;
            case "back":
                return this.btnBack;
            case "sh":
                return this.btnSH;//审核
            case "nsh":
                return this.btnNSH;//取消审核
            case "patrol":
                return this.imgtnPatrol;//检查结果
            case "ZS":
                return this.btnZS;//证书管理
            case "SB":
                return this.imgbtnSB;//上报申请
            case "sc":
                return this.btnSC;
            case "dxtx":
                return this.btnSendMessage;//短信提醒
            case "dqtx":
                return this.btnOverTime;//到期提醒
            case "nstx":
                return this.btnYearCheck;//年审提醒
            case "set":
                return this.btnSet;//设置
            case "pset":
                return this.btnPersonSet;//人员设置
            case "cset":
                return this.btnConpanySet;//企业设置
            default:
                return null;
        }
    }

    public delegate void AddClickHandler(object sender, ImageClickEventArgs e);
    public event AddClickHandler AddClickEvent;
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (AddClickEvent != null) { AddClickEvent(sender, e); }
    }

    public delegate void EditClickHandler(object sender, ImageClickEventArgs e);
    public event EditClickHandler EditClickEvent;
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (EditClickEvent != null) { EditClickEvent(sender, e); }
    }
    //检查结果
    public delegate void PatrolClickHandler(object sender, ImageClickEventArgs e);
    public event PatrolClickHandler PatrolClickEvent;
    protected void btnPatrol_Click(object sender, ImageClickEventArgs e)
    {
        if (PatrolClickEvent != null) { PatrolClickEvent(sender, e); }
    }

    //证书管理
    public delegate void ZSClickHandler(object sender, ImageClickEventArgs e);
    public event ZSClickHandler ZSClickEvent;
    protected void btnZS_Click(object sender, ImageClickEventArgs e)
    {
        if (ZSClickEvent != null) { ZSClickEvent(sender, e); }
    }

    //上报申请
    public delegate void SBClickHandler(object sender, ImageClickEventArgs e);
    public event SBClickHandler SBClickEvent;
    protected void btnSB_Click(object sender, ImageClickEventArgs e)
    {
        if (SBClickEvent != null) { SBClickEvent(sender, e); }
    }

    public delegate void DeleteClickHandler(object sender, ImageClickEventArgs e);
    public event DeleteClickHandler DeleteClickEvent;
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        if (DeleteClickEvent != null) { DeleteClickEvent(sender, e); }
    }

    public delegate void SearchClickHandler(object sender, ImageClickEventArgs e);
    public event SearchClickHandler SearchClickEvent;
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (SearchClickEvent != null) { SearchClickEvent(sender, e); }
    }

    public delegate void FlushClickHandler(object sender, ImageClickEventArgs e);
    public event FlushClickHandler FlushClickEvent;
    protected void btnFlush_Click(object sender, ImageClickEventArgs e)
    {
        if (FlushClickEvent != null) { FlushClickEvent(sender, e); }
    }

    public delegate void ExcelClickHandler(object sender, ImageClickEventArgs e);
    public event ExcelClickHandler ExcelClickEvent;
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (ExcelClickEvent != null) { ExcelClickEvent(sender, e); }
    }

    public delegate void ShowClickHandler(object sender, ImageClickEventArgs e);
    public event ShowClickHandler ShowClickEvent;
    protected void btnShow_Click(object sender, ImageClickEventArgs e)
    {
        if (ShowClickEvent != null) { ShowClickEvent(sender, e); }
    }

    public delegate void BackClickHandler(object sender, ImageClickEventArgs e);
    public event BackClickHandler BackClickEvent;
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        if (BackClickEvent != null) { BackClickEvent(sender, e); }
    }

    public delegate void ShClickHandler(object sender, ImageClickEventArgs e);
    public event ShClickHandler ShClickEvent;
    /// <summary>
    /// 审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSH_Click(object sender, ImageClickEventArgs e)
    {
        if (ShClickEvent != null) { ShClickEvent(sender, e); }
    }

    public delegate void NShClickHandler(object sender, ImageClickEventArgs e);
    public event NShClickHandler NShClickEvent;
    /// <summary>
    /// 取消审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNSH_Click(object sender, ImageClickEventArgs e)
    {
        if (NShClickEvent != null) { NShClickEvent(sender, e); }
    }


    public delegate void CRClickHandler(object sender, ImageClickEventArgs e);
    public event CRClickHandler CRClickEvent;

    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCR_Click(object sender, ImageClickEventArgs e)
    {
        if (CRClickEvent != null) { CRClickEvent(sender, e); }
    }



    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SaveClickHandler(object sender, ImageClickEventArgs e);
    public event SaveClickHandler SaveClickEvent;
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (SaveClickEvent != null)
        {
            SaveClickEvent(sender, e);
        }
    }

    /// <summary>
    /// 作废
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ZFClickHandler(object sender, ImageClickEventArgs e);
    public event ZFClickHandler ZFClickEvent;
    protected void btnZF_Click(object sender, ImageClickEventArgs e)
    {
        if (ZFClickEvent != null)
        {
            ZFClickEvent(sender, e);
        }
    }

    /// <summary>
    /// 取消作废
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void NZFClickHandler(object sender, ImageClickEventArgs e);
    public event NZFClickHandler NZFClickEvent;
    protected void btnNZF_Click(object sender, ImageClickEventArgs e)
    {
        if (NZFClickEvent != null)
        {
            NZFClickEvent(sender, e);
        }
    }

    //短信提醒
    public delegate void SendMessageClickHandler(object sender, ImageClickEventArgs e);
    public event SendMessageClickHandler SendMessageClickEvent;
    protected void btnSendMessage_Click(object sender, ImageClickEventArgs e)
    {
        if (SendMessageClickEvent != null) { SendMessageClickEvent(sender, e); }
    }

    public delegate void SClickHandler(object sender, ImageClickEventArgs e);
    public event SClickHandler SClickEvent;
    protected void btnSC_Click(object sender, ImageClickEventArgs e)
    {
        if (SClickEvent != null)
        {
            SClickEvent(sender, e);
        }
    }

    //到期提醒
    public delegate void OverTimeClickHandler(object sender, ImageClickEventArgs e);
    public event OverTimeClickHandler OverTimeClickEvent;
    protected void btnOverTime_Click(object sender, ImageClickEventArgs e)
    {
        if (OverTimeClickEvent != null) { OverTimeClickEvent(sender, e); }
    }

    //年审提醒
    public delegate void YearCheckClickHandler(object sender, ImageClickEventArgs e);
    public event YearCheckClickHandler YearCheckClickEvent;
    protected void btnYearCheck_Click(object sender, ImageClickEventArgs e)
    {
        if (YearCheckClickEvent != null) { YearCheckClickEvent(sender, e); }
    }

    //设置
    public delegate void SetClickHandler(object sender, ImageClickEventArgs e);
    public event SetClickHandler SetClickEvent;
    protected void btnSet_Click(object sender, ImageClickEventArgs e)
    {
        if (SetClickEvent != null) { SetClickEvent(sender, e); }
    }
    //人员设置
    public delegate void PSetClickHandler(object sender, ImageClickEventArgs e);
    public event PSetClickHandler PSetClickEvent;
    protected void btnPersonSet_Click(object sender, ImageClickEventArgs e)
    {
        if (PSetClickEvent != null) { PSetClickEvent(sender, e); }
    }
    //企业设置
    public delegate void CSetClickHandler(object sender, ImageClickEventArgs e);
    public event CSetClickHandler CSetClickEvent;
    protected void btnConpanySet_Click(object sender, ImageClickEventArgs e)
    {
        if (CSetClickEvent != null) { CSetClickEvent(sender, e); }
    }
}


