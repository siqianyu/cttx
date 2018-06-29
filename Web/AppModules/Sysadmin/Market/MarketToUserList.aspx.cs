using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;


public partial class AppModules_IACenter_ListUser : StarTech.Adapter.StarTechPage
{
   protected void Page_Load(object sender, EventArgs e)
    {
        InitTopButton();
        //bindstdcategory();
        //bindstdorg();
    }
    #region 通用按钮栏设置
    /// <summary>
    /// 初始化按钮栏
    /// </summary>
    protected void InitTopButton()
    {
        //客户端脚本
        this.Add1.MyButton.OnClientClick = "button_actions('add'); return false;";
        this.Add1.MyButton.Visible = true;
        this.Edit1.MyButton.OnClientClick = "deleteAction(); return false;";
        this.Edit1.MyButton.Visible = false;
        this.Delete1.MyButton.OnClientClick = "return deleteAction()";
        this.Delete1.MyButton.Text = "批量删除";
        this.Delete1.MyButton.Visible = true;
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

    #region 标准类别
    //private void bindstdcategory()
    //{

    //    stdcategory.DataSource = getddltree();
    //    stdcategory.DataTextField = "title";
    //    stdcategory.DataValueField = "stdcategoryid";
    //    stdcategory.DataBind();
    //    stdcategory.Items.Insert(0, new ListItem("--请选择--", ""));
    //}
    public DataTable getddltree()
    {
        DataTable dttree = new DataTable();
        dttree.Columns.Add("stdcategoryid", typeof(string));
        dttree.Columns.Add("title", typeof(string));
        DataTable dt = DalBase.Util_GetList("select *  FROM T_Local_StdCategory where parentid=0 order by sort asc").Tables[0];

        foreach (DataRow dr in dt.Rows)
        {
            dttree.Rows.Add(new object[] { dr["stdcategoryid"], "-" + dr["title"] });
            getall(dr["stdcategoryid"].ToString(), dttree, 1);
        }
        return dttree;
    }

    private void getall(string stdcategoryid, DataTable dttree, int lev)
    {
        DataTable dt = DalBase.Util_GetList("select *  FROM T_Local_StdCategory where parentid=" + stdcategoryid + " order by sort asc").Tables[0];
        string sp = "";
        for (int i = 0; i < lev; i++)
        {
            sp += "－－";
        }
        foreach (DataRow dr in dt.Rows)
        {
            dttree.Rows.Add(new object[] { dr["stdcategoryid"], sp + dr["title"] });
            getall(dr["stdcategoryid"].ToString(), dttree, lev + 1);
        }
    }
    #endregion

    //#region 标准组织
    //private void bindstdorg()
    //{
    //    DataTable dt = DalBase.Util_GetList("select * from t_base_stdorgtype order by sort asc").Tables[0];
    //    stdorgtype.DataSource = dt;
    //    stdorgtype.DataTextField = "name";
    //    stdorgtype.DataValueField = "id";
    //    stdorgtype.DataBind();
    //    stdorgtype.Items.Insert(0, new ListItem("--请选择--", ""));
    //}
    //#endregion
}