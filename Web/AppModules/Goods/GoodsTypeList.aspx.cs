using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.ELife.Goods;
using System.Data;
using System.Collections;
using Startech.Utils;

public partial class AppModules_Goods_GoodsTypeList : StarTech.Adapter.StarTechPage
{        
    protected GoodsTypeBll bll = new GoodsTypeBll();

    protected void Page_Load(object sender, EventArgs e)
    {
        InitTopButton();
        if (!IsPostBack)
        {
            BindTreeView();
        }
    }
    /// <summary>
    /// 绑定分类信息
    /// </summary>
    private void BindTreeView()
    {
        //清空树
        treeMenu.Nodes.Clear();
        DataSet ds = bll.GetList("1=1");
        if (ds == null || ds.Tables.Count < 1)
            return;
        DataTable dt = ds.Tables[0];
        TreeNode treenote = new TreeNode("分类管理");
        treeMenu.Nodes.Add(treenote);
        if (dt.Rows.Count > 0)
        {
            string filter = " PCategoryId='' ";
            DataRow[] rows = dt.Select(filter, "orderBy asc");
            foreach (DataRow row in rows)
            {
                TreeNode tn = new TreeNode();
                string id = row["categoryId"].ToString();
                tn.Text = row["categoryName"].ToString();
                tn.Value = row["categoryId"].ToString();
                tn.ShowCheckBox = true;
                //tn.NavigateUrl = "GroupMenuSetIframe.aspx?menuId=" + id + "";
                tn.Target = "mainList";
                tn.Expanded = true;
                BindNode(tn, dt, id);
                treenote.ChildNodes.Add(tn);
            }
        }
    }


    private void BindNode(TreeNode parentNode, DataTable dt, string sysID)
    {
        string filter = "PCategoryId='" + sysID + "'";
        DataRow[] rows = dt.Select(filter, "orderBy asc");
        foreach (DataRow row in rows)
        {
            TreeNode child = new TreeNode();
            string id = row["categoryId"].ToString();
            child.Text = row["categoryName"].ToString();
            child.Value = row["categoryId"].ToString();
            //child.ToolTip = row["uniqueId"].ToString();
            child.ShowCheckBox = true;
            //child.NavigateUrl = "GroupMenuSetIframe.aspx?menuId=" + id + "";
            child.Target = "mainList";
            parentNode.ChildNodes.Add(child);
            BindNode(child, dt, id);
        }
    }


    #region 通用按钮栏设置
    /// <summary>
    /// 初始化按钮栏
    /// </summary>
    protected void InitTopButton()
    {
        //显示
        //this.TopButtons1.GetImageButton("add").Visible = true;
        //this.TopButtons1.GetImageButton("edit").Visible = true;
        //this.TopButtons1.GetImageButton("delete").Visible = true;
        //this.TopButtons1.GetImageButton("search").Visible = true;
        //客户端脚本
        this.Add1.MyButton.Attributes.Add("onclick", "return buttonAction('add')");
        this.Edit1.MyButton.Attributes.Add("onclick", "return buttonAction('edit')");
        this.Show1.MyButton.Attributes.Add("onclick", "return buttonAction('Show')");
        this.Delete1.MyButton.Attributes.Add("onclick", "return buttonAction('delete')");
        //this.TopButtons1.GetImageButton("add").Attributes.Add("onclick", "return buttonAction('add')");
        //this.TopButtons1.GetImageButton("edit").Attributes.Add("onclick", "return buttonAction('edit')"); ;
        //this.TopButtons1.GetImageButton("delete").Attributes.Add("onclick", "return buttonAction('delete')");
        //this.TopButtons1.GetImageButton("search").Attributes.Add("onclick", "return buttonAction('copy')");

        //事件
        this.Add1.AddClickEvent += new Sysadmin_Controls_Add.AddClickHandler(Add1_AddClickEvent);
        this.Edit1.EditClickEvent += new Sysadmin_Controls_Edit.EditClickHandler(Edit1_EditClickEvent);
        this.Delete1.DeleteClickEvent += new Sysadmin_Controls_Delete.DeleteClickHandler(Delete1_DeleteClickEvent);
        this.Show1.ShowClickEvent += new Sysadmin_Controls_Show.ShowClickHandler(Show1_ShowClickEvent);
        JSUtility.ShowConfirm((WebControl)this.Delete1.MyButton, "确定要删除该项？");

        //权限
        //this.TopButtons1.GetImageButton("add").Visible = this.ValidateAccess("F100302");
        //this.TopButtons1.GetImageButton("edit").Visible = this.ValidateAccess("F100303");
        //this.TopButtons1.GetImageButton("delete").Visible = this.ValidateAccess("F100305");
    }

    //添加
    void Add1_AddClickEvent(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "<Script language=JavaScript>add_method()</Script>");

    }
    // copy
    void TopButtons1_SearchClickEvent(object sender, EventArgs e)
    {
        ArrayList selectedNodes = TreeNodeUtil.GetSelectedTreeNodes(this.treeMenu.Nodes[0]);
        if (selectedNodes.Count == 0) JSUtility.Alert("请选择一个节点!");
        else if (selectedNodes.Count == 1)
        {
            string menuId = ((TreeNode)selectedNodes[0]).Value;
            Response.Redirect(String.Format("CopyMenu.aspx?MenuId={0}", menuId), true);
        }
        else JSUtility.Alert("只能选择一个节点!");
    }



    // 修改
    void Edit1_EditClickEvent(object sender, EventArgs e)
    {
        ArrayList selectedNodes = TreeNodeUtil.GetSelectedTreeNodes(this.treeMenu.Nodes[0]);
        if (selectedNodes.Count == 0) JSUtility.Alert("请选择一个要编辑的节点!");
        else if (selectedNodes.Count == 1)
        {
            string menuId = ((TreeNode)selectedNodes[0]).Value;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "<Script language=JavaScript>edit_method(" + menuId + ")</Script>");
        }
        else JSUtility.Alert("只能选择一个要编辑的节点!");
    }



    //查看
    void Show1_ShowClickEvent(object sender, EventArgs e)
    {
        ArrayList selectedNodes = TreeNodeUtil.GetSelectedTreeNodes(this.treeMenu.Nodes[0]);
        if (selectedNodes.Count == 0) JSUtility.Alert("请选择一个要编辑的节点!");
        else if (selectedNodes.Count == 1)
        {
            string menuId = ((TreeNode)selectedNodes[0]).Value;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "<Script language=JavaScript>show_method(" + menuId + ")</Script>");

        }
        else JSUtility.Alert("只能选择一个要编辑的节点!");
    }

    //删除
    void Delete1_DeleteClickEvent(object sender, EventArgs e)
    {
        ArrayList selectedNodes = TreeNodeUtil.GetSelectedTreeNodes(this.treeMenu.Nodes[0]);
        if (selectedNodes.Count == 0)
            JSUtility.Alert("请选择要删除的页节点!");
        else
        {
            int[] selectedIds = new int[selectedNodes.Count];
            string typeList = "";
            for (int i = 0; i < selectedNodes.Count; i++)
            {
                selectedIds[i] = Convert.ToInt32(((TreeNode)selectedNodes[i]).Value);
                typeList += "《" + ((TreeNode)selectedNodes[i]).Text+"》";
                /*日志归档*/
                //  string sql = @"select l.Description as title from T_Permission l where PermissionId=" + selectedIds[i].ToString() + "";
                //  PubFunction.InsertLog("系统管理", "菜单管理", "菜单列表", "删除", sql, selectedIds[i].ToString());
            }
            bool sucess = this.bll.Delete(selectedIds);
            if (sucess)
            {

                LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "删除任务分类" + typeList + "", "删除", "", "", HttpContext.Current.Request.Url.ToString());

                JSUtility.Alert("删除菜单项成功!");
            }
            else
                JSUtility.Alert("包含子节点的菜单项无法删除,子节点已删除!");
            this.treeMenu.Nodes.Clear();
            //this.treeMenu.Nodes.Add(this._menu.GetMenuTree(false));
            BindTreeView();
        }


    }

    #endregion

    
}
