using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.ELife.Base;

public partial class AppModules_Sysadmin_Base_AreaList : StarTech.Adapter.StarTechPage
{
    AreaBll bll = new AreaBll();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindPArea();//绑定所有区域
    }

    #region 绑定所有区域
    public void BindPArea()
    {
        DataSet ds = bll.GetAllList();
        ddlPid.DataSource = ds;
        ddlPid.DataTextField = "area_name";
        ddlPid.DataValueField = "area_id";

        ddlPid.DataBind();
        ddlPid.Items.Insert(0, "--请选择--");
    }
    #endregion
}