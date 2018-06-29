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

public partial class AppModules_IACenter_GroupMenuSetIframe : StarTech.Adapter.StarTechPage
{
    public string menuId;
    public string groupId;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.menuId = (Request["menuId"] == null) ? "" : Request["menuId"];
        this.groupId = (Request["groupId"] == null) ? "" : Request["groupId"];

        if (!IsPostBack)
        {
            if (this.groupId != "" && this.menuId != "")
            {
                BindData();
            }
        }
    }

    

    #region 显示数据源
    protected void BindData()
    {
        StarTech.Adapter.IACenter iaCenter = new StarTech.Adapter.IACenter();
        this.ltMenuName.Text = iaCenter.GetMenuNameById(Int32.Parse(this.menuId));
        this.cbxMenu.Checked = iaCenter.CheckIsGroupMenu(Int32.Parse(this.menuId), Int32.Parse(this.groupId));

        DataSet ds = iaCenter.GetSystemButtons();
        this.cbxButtons.DataSource = ds;
        this.cbxButtons.DataTextField = "buttonName";
        this.cbxButtons.DataValueField = "uniqueId";
        this.cbxButtons.DataBind();
        string buttons = iaCenter.GetAllButtons(Int32.Parse(this.menuId), this.groupId);
        buttons = ","+buttons+",";
        foreach (ListItem item in this.cbxButtons.Items)
        {
            if (buttons.IndexOf("," + item.Value + ",") > -1) { item.Selected = true; }
        }
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        StarTech.Adapter.IACenter iaCenter = new StarTech.Adapter.IACenter();
        //menu
        iaCenter.SetGroupMenu(Int32.Parse(this.menuId), Int32.Parse(this.groupId), this.cbxMenu.Checked);
        if (this.cbxToSub.Checked)
        {
            DataSet subMenus = iaCenter.GetSubMenuById(Int32.Parse(this.menuId));
            foreach (DataRow row in subMenus.Tables[0].Rows)
            {
                iaCenter.SetGroupMenu(Int32.Parse(row["uniqueId"].ToString()), Int32.Parse(this.groupId), this.cbxMenu.Checked);
            }
        }

        //button
        string buttons = "";
        foreach (ListItem item in this.cbxButtons.Items)
        {
            if (item.Selected == true) { buttons += item.Value + ","; }
        }
        if (buttons != "") { buttons = buttons.TrimEnd(','); }
        bool addButtonsFlag = (buttons == "") ? false : true;
        iaCenter.SetGroupButtons(Int32.Parse(this.menuId), Int32.Parse(this.groupId), buttons, addButtonsFlag);
        if (this.cbxToSub.Checked)
        {
            DataSet subMenus = iaCenter.GetSubMenuById(Int32.Parse(this.menuId));
            foreach (DataRow row in subMenus.Tables[0].Rows)
            {
                iaCenter.SetGroupButtons(Int32.Parse(row["uniqueId"].ToString()), Int32.Parse(this.groupId), buttons, addButtonsFlag);
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功!');</script>");
    }
}
