using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.ELife.Goods;
using StarTech;

public partial class AppModules_Goods_GoodsListType : StarTech.Adapter.StarTechPage
{

    protected GoodsTypeBll bll = new GoodsTypeBll();
    protected string categoryId = "?r=" + new Random().Next();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["categoryId"] != null)
        {
            categoryId += "&categoryId="+KillSqlIn.Form_ReplaceByString(Request.QueryString["categoryId"],20);
        }
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
        TreeNode treenote = new TreeNode("分类");
        treenote.NavigateUrl = "GoodsList.aspx";
        treenote.Target = "categoryGoodsList";
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
                
                tn.NavigateUrl = "GoodsList.aspx?categoryId=" + id + "";
                tn.Target = "categoryGoodsList";
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
            
            //child.NavigateUrl = "GroupMenuSetIframe.aspx?menuId=" + id + "";
            child.Target = "categoryGoodsList";
            child.NavigateUrl = "GoodsList.aspx?categoryId=" + id + "";
            parentNode.ChildNodes.Add(child);
            BindNode(child, dt, id);
        }
    }

    protected void treeMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        //string value=((TreeView)sender).SelectedValue;
        //url = "goodsList.aspx?r=" + new Random().Next()+"&categoryId="+value;
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('!!!'); GetList(" + url + ");</script>");
    }
}