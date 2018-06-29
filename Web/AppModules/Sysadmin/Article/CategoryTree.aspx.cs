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
using Startech.Category;
using Startech.Article;
using StarTech.Util;

public partial class Sysadmin_Article_CategoryTree : StarTech.Adapter.StarTechPage
{
    public int _count = 0;
    private CategoryBLL _category = new CategoryBLL();
    protected string _pageTitle = "新闻类别列表";
    //CustomPrincipal p = CustomPrincipal.CurrentRequestPrincipal;

    protected void Page_Load(object sender, EventArgs e)
    {
        InitTopButton();
        if (!IsPostBack)
        {
            this.treeCategory.Attributes.Add("onclick", "CheckEvent1()");
            //绑定新闻类别列表，1：文章类 2：新闻类
            this.treeCategory.Nodes.Add(this._category.GetCategoryTree(1));
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
        //this.Add1.MyButton.Attributes.Add("onclick", "return buttonAction('add')");
        //this.Edit1.MyButton.Attributes.Add("onclick", "return buttonAction('edit')");
        //this.Show1.MyButton.Attributes.Add("onclick", "return buttonAction('Show')");
        //this.Delete1.MyButton.Attributes.Add("onclick", "return buttonAction('delete')");
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
        ArrayList selectedNodes = this._category.GetSelectedTreeNodes(this.treeCategory.Nodes[0]);
        if (selectedNodes.Count == 0) JSUtility.Alert("请选择一个节点!");
        else if (selectedNodes.Count == 1)
        {
            string CategoryId = ((TreeNode)selectedNodes[0]).Value;
            Response.Redirect(String.Format("CopyMenu.aspx?CategoryId={0}", CategoryId), true);
        }
        else JSUtility.Alert("只能选择一个节点!");
    }



    // 修改
    void Edit1_EditClickEvent(object sender, EventArgs e)
    {
        ArrayList selectedNodes = this._category.GetSelectedTreeNodes(this.treeCategory.Nodes[0]);
        if (selectedNodes.Count == 0) JSUtility.Alert("请选择一个要编辑的节点!");
        else if (selectedNodes.Count == 1)
        {
            string CategoryId = ((TreeNode)selectedNodes[0]).Value;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "<Script language=JavaScript>edit_method(" + CategoryId + ")</Script>");
        }
        else JSUtility.Alert("只能选择一个要编辑的节点!");
    }



    //查看
    void Show1_ShowClickEvent(object sender, EventArgs e)
    {
        ArrayList selectedNodes = this._category.GetSelectedTreeNodes(this.treeCategory.Nodes[0]);
        if (selectedNodes.Count == 0) JSUtility.Alert("请选择一个要编辑的节点!");
        else if (selectedNodes.Count == 1)
        {
            string CategoryId = ((TreeNode)selectedNodes[0]).Value;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "<Script language=JavaScript>show_method(" + CategoryId + ")</Script>");

        }
        else JSUtility.Alert("只能选择一个要编辑的节点!");
    }

    //删除
    void Delete1_DeleteClickEvent(object sender, EventArgs e)
    {
        ArrayList selectedNodes = this._category.GetSelectedTreeNodes(this.treeCategory.Nodes[0]);
        if (selectedNodes.Count == 0)
            JSUtility.Alert("请选择要删除的页节点!");
        else
        {
            int[] selectedIds = new int[selectedNodes.Count];
            for (int i = 0; i < selectedNodes.Count; i++)
            {
                selectedIds[i] = Convert.ToInt32(((TreeNode)selectedNodes[i]).Value);
                /*日志归档*/
                string sql = @"select * from dbo.T_Category where CategoryId = (" + selectedIds[i] + ")";
                //  string function = "删除新闻类别";
                //PubFunction.InsertLog1(null, sql, function);
            }

            foreach (int id in selectedIds)
            {
                if (new BllTableObject("T_Article").Util_CheckIsExsitData("CategoryId=" + id + ""))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('该类别已包含的新闻内容，请到【新闻列表】里删除相应的内容!');</script>");
                    return;
                }
            }

            //如果该节点为根节点，且有子节点则不允许删除
            bool flag = this._category.DeleteCategoryParent(((TreeNode)selectedNodes[0]).Value);
            if (flag)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('该节点为根节点，且有子节点则不允许删除!');</script>");
                return;
            }

            /*删除数据*/
            bool sucess = this._category.DeleteCategory(selectedIds);

            if (sucess)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('删除菜单项成功!');</script>");

            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('包含子节点的菜单项无法删除,请先删除子节点!');</script>");
            this.treeCategory.Nodes.Clear();
            //绑定新闻类别列表，1：新闻类。
            this.treeCategory.Nodes.Add(this._category.GetCategoryTree(1));
        }


    }

    #endregion

}
