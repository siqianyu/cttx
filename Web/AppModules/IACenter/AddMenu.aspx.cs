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
using StarTech.Util;


public partial class AppModules_IACenter_AddMenu : StarTech.Adapter.StarTechPage
{
    private string _menuId = HttpContext.Current.Request["MenuId"];
    private BllMenu _menu = new BllMenu();
    protected string _pageTitle = "添加菜单项";

    public ModMenu Model
    {
        get { return (ModMenu)ViewState["Model"]; }
        set { ViewState["Model"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDDL();
            if (this._menuId != null)
            {

                int menuId = Convert.ToInt32(this._menuId);
                ModMenu detail = _menu.GetModel(menuId);
                this.Model = detail;
                this.txtMenuName.Text = detail.menuName;
                this.txtMenuLink.Text = detail.menuTarget;
                this.txtSort.Text = detail.orderIndex.ToString();
                this.cbIsVisible.Checked = (detail.isShow == 1 ? true : false);
                this.ddlParentMenu.SelectedValue = detail.parentMenuId.ToString();
                this._pageTitle = "修改菜单项";
            }
            else
            {
                txtMenuName.Focus();
            }           
        }

    }



    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        ModMenu detail = new ModMenu();
        detail.menuName = this.txtMenuName.Text.Trim();
        detail.menuTarget = this.txtMenuLink.Text.Trim();
        detail.isShow = (this.cbIsVisible.Checked == true ? 1 : 0);
        detail.orderIndex = Convert.ToInt32(this.txtSort.Text.Trim());
        detail.parentMenuId = Convert.ToInt32(this.ddlParentMenu.SelectedValue);
        if (this._menuId != null)
        {
            int menuId = Convert.ToInt32(this._menuId);
            detail.uniqueId = menuId;
            this._menu.Update(detail);
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
        }
        else
        {
            this._menu.Add(detail);
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('添加成功');layer_close_refresh();</script>");
            //JSUtility.ReplaceOpenerParentWindow("menuTree.aspx");
        }
    }
    private void BindDDL()
    {
        ArrayList items = this._menu.AllMenuItemForUpdate();
        IEnumerator e = items.GetEnumerator();
        while (e.MoveNext())
        {
            MenuEntity item = (MenuEntity)(e.Current);
            ListItem li = new ListItem(item.Name, item.Id);
            this.ddlParentMenu.Items.Add(li);
        }
    }
}
