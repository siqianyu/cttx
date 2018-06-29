using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.ELife.Goods;
using StarTech;
using StarTech.DBUtility;

public partial class AppModules_Examination_GoodsListType : StarTech.Adapter.StarTechPage
{
    public DataTable dtAll;
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
            this.dtAll = GetAll();
            BindTreeView();
        }
    }

    /// <summary>
    /// 绑定分类信息
    /// </summary>
    private void BindTreeView()
    {
        DataTable dtGoods = GetAllGoods();
        //清空树
        treeMenu.Nodes.Clear();
        DataSet ds = bll.GetList("1=1");
        if (ds == null || ds.Tables.Count < 1)
            return;
        DataTable dt = ds.Tables[0];
        TreeNode treenote = new TreeNode("习题分类");
        treenote.NavigateUrl = "QuestionList.aspx";
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

                tn.NavigateUrl = "QuestionList.aspx?categoryId=" + id + "";
                tn.Target = "categoryGoodsList";
                tn.Expanded = true;
                //BindNode(tn, dt, id);
                BindNode2(tn, dt, id);
                //BindNodeByGoods(tn, dtGoods, id);
                treenote.ChildNodes.Add(tn);
            }
        }
    }

    private void BindNode2(TreeNode parentNode, DataTable dt, string sysID)
    {
        DataTable dtGoods = GetAllGoods();
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
            child.NavigateUrl = "QuestionList.aspx?categoryId=" + id + "";
            parentNode.ChildNodes.Add(child);
            BindNodeByGoods(child, dtGoods, id);
        }
        if (rows.Length == 0) { BindNodeByGoods(parentNode, dtGoods, sysID); }
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
            child.NavigateUrl = "QuestionList.aspx?categoryId=" + id + "";
            parentNode.ChildNodes.Add(child);
            BindNode(child, dt, id);
        }
    }


    private void BindNodeByGoods(TreeNode parentNode, DataTable dt, string sysID)
    {
        string filter = "CategoryId='" + sysID + "'";
        DataRow[] rows = dt.Select(filter, "Orderby asc");
        foreach (DataRow row in rows)
        {
            TreeNode child = new TreeNode();
            string id = row["GoodsId"].ToString();
            child.Text = row["GoodsName"].ToString()+ "(" + CountNum(id, true ) + ")";
            child.Value = row["GoodsId"].ToString();
            //child.ToolTip = row["uniqueId"].ToString();

            //child.NavigateUrl = "GroupMenuSetIframe.aspx?menuId=" + id + "";
            child.Target = "categoryGoodsList";
            child.NavigateUrl = "QuestionList.aspx?flag=course&categoryId=" + id + "";
            parentNode.ChildNodes.Add(child);
            BindNodeByGoods(child, dt, id);
        }
    }



    protected void treeMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        //string value=((TreeView)sender).SelectedValue;
        //url = "goodsList.aspx?r=" + new Random().Next()+"&categoryId="+value;
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('!!!'); GetList(" + url + ");</script>");
    }

    protected DataTable GetAll()
    {
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        return adohelper.ExecuteSqlDataset("select sysnumber,courseId,categoryPath from T_Test_Queston ").Tables[0];
    }
    protected DataTable GetAllGoods()
    {
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        return adohelper.ExecuteSqlDataset("select GoodsId,CategoryId,GoodsToTypeId,GoodsName,Orderby from [T_Goods_Info] ").Tables[0];
    }
    protected int CountNum(string cid, bool containkhlx)
    {
        if (containkhlx == false)
        {
            DataRow[] rows = this.dtAll.Select(" isnull(courseId,'')='' and categoryPath like '%" + cid + "%'");
            return rows.Length;
        }
        else
        {
            DataRow[] rows = this.dtAll.Select(" categoryPath like '%" + cid + "%'");
            return rows.Length;
        }

    }
}