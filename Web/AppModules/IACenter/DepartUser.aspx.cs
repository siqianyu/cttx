using System;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.Util;

public partial class AppModules_Mail_DepartUser : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绑定数据
            BindData();
        }
    }

    //绑定数据
    private void BindData()
    {
        //清除所有数据
        DeptTree.Nodes.Clear();
        //绑定模式
        TreeNode node = new TreeNode("用户", "0");
        node.SelectAction = TreeNodeSelectAction.Expand;
        node.Expand();
        
        //部门数据
        DataSet dsDept = new BllTableObject("T_Base_Department").Util_GetList2("*", "1=1", "orderBy");
        //所有人员数据
        DataSet dsUser = new StarTech.Adapter.IACenter().ExecuteSqlDataset("select * from IACenter_User order by orderBy");
        //创建部门节点
        CreateDeptNode(node, dsDept.Tables[0], dsUser.Tables[0], "");
        DeptTree.Nodes.Add(node);
    }

    private void CreateDeptNode(TreeNode parentNode, DataTable dsDept, DataTable dsUser, string PathUnitName)
    {
        DataRow[] rowList = dsDept.Select(string.IsNullOrEmpty(parentNode.Value) ? "" : " isnull(departPid,0)=" + parentNode.Value + "","orderBy");
        foreach (DataRow row in rowList)
        {
            TreeNode node = new TreeNode();
            node.ToolTip = "点击添加";
            node.SelectAction = TreeNodeSelectAction.Expand;

            string DeptName = row["departName"].ToString();
            string DepartID = row["uniqueId"].ToString();


            string tempPathUnitName = PathUnitName == "" ? DeptName : DeptName + "/" + PathUnitName;
            node.Value = row["uniqueId"].ToString();
            //node.Text = "<a class='supman' href=\"javascript:AddTableFromTree('" + tempPathUnitName + "','" + DepartID + "')\">" + DeptName + "</a>";
            node.Text = DeptName;

            //绑定部门人员数据
            CreateUserNode(node, tempPathUnitName, dsUser);

            //递归绑定部门
            CreateDeptNode(node, dsDept, dsUser, tempPathUnitName);

            parentNode.ChildNodes.Add(node);
        }
    }

    private void CreateUserNode(TreeNode deptNode, string PathUnitName, DataTable dsUser)
    {


        DataRow[] rowList = dsUser.Select(" departId = '" + deptNode.Value + "'", "orderby asc");
        foreach (DataRow userRow in rowList)
        {
            TreeNode node2 = new TreeNode();
            node2.Value = userRow["uniqueId"].ToString();
            node2.ToolTip = "点击添加";
            node2.SelectAction = TreeNodeSelectAction.None;
            //路径名称显示
            string tempPathUnitName = userRow["trueName"].ToString() + "/" + PathUnitName;
            node2.Text = "<a class='supman' href=\"javascript:AddTableFromTree('" + tempPathUnitName + "','" + node2.Value + "')\">" + userRow["trueName"].ToString() + "</a>";
            deptNode.ChildNodes.Add(node2);
        }

    }

    //绑定下拉框数据
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        //绑定数据
        BindData();
    }
}
