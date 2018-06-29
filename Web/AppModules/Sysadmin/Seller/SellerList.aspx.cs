using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Sysadmin_Seller_SellerList : StarTech.Adapter.StarTechPage
{
    public string type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        InitTopButton();

        type = Request.QueryString["type"] == null ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["type"], 5);
    }

    #region 通用按钮栏设置
    /// <summary>
    /// 初始化按钮栏
    /// </summary>
    protected void InitTopButton()
    {
        //客户端脚本
        this.Add1.MyButton.OnClientClick = "button_actions('add'); return false;";
       // this.Add1.MyButton.Visible = false;
        this.Edit1.MyButton.Visible = false;
        this.Delete1.MyButton.OnClientClick = "deleteAction();return false;";
        this.Delete1.MyButton.Text = "批量删除";
        this.Delete1.MyButton.Visible = false;
        this.Show1.MyButton.OnClientClick = "return button_actions('show')";
        this.Show1.MyButton.Visible = false;

        //事件
        this.Show1.ShowClickEvent += new Sysadmin_Controls_Show.ShowClickHandler(Show1_ShowClickEvent);
        this.Add1.AddClickEvent += new Sysadmin_Controls_Add.AddClickHandler(Add1_AddClickEvent);
        this.Edit1.EditClickEvent += new Sysadmin_Controls_Edit.EditClickHandler(Edit1_EditClickEvent);
        this.Delete1.DeleteClickEvent += new Sysadmin_Controls_Delete.DeleteClickHandler(Delete1_DeleteClickEvent);
        // this.Search1.AddClickEvent += new Sysadmin_Controls_Search.AddClickHandler(Search1_AddClickEvent);
    }

    void Add1_AddClickEvent(object sender, EventArgs e)
    {

    }


    void Edit1_EditClickEvent(object sender, EventArgs e)
    {

    }

    void Delete1_DeleteClickEvent(object sender, EventArgs e)
    {

    }

    void Show1_ShowClickEvent(object sender, EventArgs e)
    {

    }

    void Search1_AddClickEvent(object sender, EventArgs e)
    {

    }
    #endregion
}