using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using StarTech.DBUtility;
using StarTech.Util;

public partial class gzs_MemberInfo_AddSubMember : StarTech.Adapter.StarTechPage
{
    public string id = string.Empty;
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadMenu();

        id = SafeRequest.GetQueryString("id");
    }


    /// <summary>
    /// 绑定一级菜单
    /// </summary>
    private void LoadMenu()
    {
        DataTable dt = DalBase.Util_GetList("select * from T_Member_Menu where pid='0' and [target]='newgzs'").Tables[0];
        this.Menu.DataSource = dt;
        this.Menu.DataBind();
    }

    /// <summary>
    /// 绑定二级菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        //判断里层repeater处于外层repeater的哪个位置（ AlternatingItemTemplate，FooterTemplate，
        //HeaderTemplate，，ItemTemplate，SeparatorTemplate）
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rep = e.Item.FindControl("SubMenu") as Repeater;//找到里层的repeater对象
            DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
            string mid = rowv["mid"].ToString(); //获取填充子类的id 
            DataTable dt = DalBase.Util_GetList("select * from T_Member_Menu where pid='" + mid + "' and [target]='newgzs'").Tables[0];
            rep.DataSource = dt;
            rep.DataBind();
        }

    }

}