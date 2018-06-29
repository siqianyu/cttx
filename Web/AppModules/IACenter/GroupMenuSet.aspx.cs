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

public partial class AppModules_IACenter_GroupMenuSet : StarTech.Adapter.StarTechPage
{
    public string id;
    //public string def

    protected void Page_Load(object sender, EventArgs e)
    {
        this.id = (Request["id"] == null) ? "" : Request["id"];

        if (!IsPostBack)
        {
            if (this.id != "")
            {
                BindMenusTree();
                BindData();
            }
        }
    }

    

    #region 显示数据源
    // 绑定列表
    protected void BindData()
    {
        //string filter = GetFilter();
        //string tableViews = "IACenter_Group";
        //string sort = " order by uniqueId desc";
        //int pageIndex = this.Pagination1.PageIndex;
        //int pageSize = this.Pagination1.PageSize;
        //int count;
        //DataTable dt = GetArticleList("*", tableViews, filter, sort, pageIndex, pageSize, out count);
        //EditDataSource(ref dt);
        //this.Repeater1.DataSource = dt;
        //this.Repeater1.DataBind();
        //this.Pagination1.RecordCount = count;
    }

    //筛选
    protected string GetFilter()
    {
        string filter = " 1=1 ";
        return filter;
    }

    //编辑数据源
    protected void EditDataSource(ref DataTable dt)
    {

    }
    #endregion

    protected void BindMenusTree()
    {
        this.ltGroupName.Text = this.GetGroupNameById(Int32.Parse(this.id));
        BindTreeView();
    }

    private void BindTreeView()
    {
        //清空树
        tvModuleTree.Nodes.Clear();
        DataTable dt = new BllTableObject("IACenter_Menu").Util_GetList("*", "1=1");

        TreeNode treenote = new TreeNode("系统菜单");
        tvModuleTree.Nodes.Add(treenote);
        if (dt.Rows.Count > 0)
        {
            string filter = " parentMenuId=0 ";
            DataRow[] rows = dt.Select(filter, "orderIndex asc");
            foreach (DataRow row in rows)
            {
                TreeNode tn = new TreeNode();
                string id = row["uniqueId"].ToString();
                tn.Text = row["menuName"].ToString();
                tn.Value = row["uniqueId"].ToString();
                tn.ShowCheckBox = false;
                tn.NavigateUrl = "GroupMenuSetIframe.aspx?menuId=" + id + "&groupId=" + this.id;
                tn.Target = "mainList";
                tn.Expanded = true;
                BindNode(tn, dt, id);
                treenote.ChildNodes.Add(tn);
            }
        }
    }


    private void BindNode(TreeNode parentNode, DataTable dt, string sysID)
    {
        string filter = "parentMenuId=" + sysID + "";
        DataRow[] rows = dt.Select(filter, "orderIndex asc");
        foreach (DataRow row in rows)
        {
            TreeNode child = new TreeNode();
            string id = row["uniqueId"].ToString();
            child.Text = row["menuName"].ToString();
            child.Value = row["uniqueId"].ToString();
            //child.ToolTip = row["uniqueId"].ToString();
            child.ShowCheckBox = false;
            child.NavigateUrl = "GroupMenuSetIframe.aspx?menuId=" + id + "&groupId=" + this.id;
            child.Target = "mainList";
            parentNode.ChildNodes.Add(child);
            BindNode(child, dt, id);
        }
    }
}
