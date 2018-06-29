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
using StarTech.DBUtility;

public partial class Admin_AppModules_NewsListByCategory : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            this.tvModuleTree.Attributes.Add("onclick", "CheckEvent1()");
            BindTreeView();
        }

    }

    

    private void BindTreeView()
    {
        //清空树
        tvModuleTree.Nodes.Clear();
        DataTable dt = adohelper.ExecuteSqlDataset("select * from T_Category").Tables[0];

        TreeNode treenote = new TreeNode("栏目分类");
        tvModuleTree.Nodes.Add(treenote);
        if (dt.Rows.Count > 0)
        {
            string filter = " ParentCategoryId=2 ";
            DataRow[] rows = dt.Select(filter, "Sort asc");
            foreach (DataRow row in rows)
            {
                TreeNode tn = new TreeNode();
                string id = row["CategoryId"].ToString();
                tn.Text = row["categoryname"].ToString();
                tn.Value = row["CategoryId"].ToString();
                //tn.ShowCheckBox = true;
                tn.NavigateUrl = "NewsList.aspx?typeId=" + id + "";
                tn.Target = "mainList";
                tn.Expanded = true;
                BindNode(tn, dt, id);
                treenote.ChildNodes.Add(tn);
            }
        }
    }


    private void BindNode(TreeNode parentNode, DataTable dt, string sysID)
    {
        string filter = "ParentCategoryId=" + sysID + "";
        DataRow[] rows = dt.Select(filter, "Sort asc");
        foreach (DataRow row in rows)
        {
            TreeNode child = new TreeNode();
            string id = row["categoryId"].ToString();
            child.Text = row["categoryName"].ToString();
            child.Value = row["categoryId"].ToString();
            //child.ToolTip = row["uniqueId"].ToString();
            //child.ShowCheckBox = true;
            child.NavigateUrl = "NewsList.aspx?typeId=" + id + "";
            child.Target = "mainList";
            parentNode.ChildNodes.Add(child);
            BindNode(child, dt, id);
        }
    }
}
