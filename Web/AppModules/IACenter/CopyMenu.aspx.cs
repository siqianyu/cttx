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


public partial class AppModules_IACenter_CopyMenu : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ltTargetId.Text = (Request.QueryString["MenuId"] == null) ? "0" : Request.QueryString["MenuId"];
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            DataSet dsSource = adoHelper.ExecuteSqlDataset("select * from IACenter_Menu where uniqueId=" + this.ltTargetId.Text + " ");
            this.Literal1.Text = dsSource.Tables[0].Rows.Count > 0 ? dsSource.Tables[0].Rows[0]["menuName"].ToString() : "";
        }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        int r = 0;
        if (this.ltSourceId.Text.Trim() != "" && this.ltSourceId.Text.Trim() != "0" && this.ltTargetId.Text != "0")
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            string sqlSource = "select * from IACenter_Menu where parentMenuId=" + this.ltSourceId.Text.Trim() + "";
            DataSet dsSource = adoHelper.ExecuteSqlDataset(sqlSource);
            foreach (DataRow row in dsSource.Tables[0].Rows)
            {
                string sql = "insert into IACenter_Menu(menuName, menuTarget, parentMenuId, orderIndex, isShow) values('" + row["menuName"] + "','" + row["menuTarget"] + "'," + this.ltTargetId.Text + "," + row["orderIndex"] + "," + row["isShow"] + ") ";
                r += adoHelper.ExecuteSqlNonQuery(sql);
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "a", "<script>alert('成功复制" + r + "条记录');location.href('MenuTree.aspx');</script>");
        this.btnCopy.Enabled = false;
    }
    protected void ltSourceId_TextChanged(object sender, EventArgs e)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        DataSet dsSource = adoHelper.ExecuteSqlDataset("select * from IACenter_Menu where uniqueId=" + this.ltSourceId.Text + " ");
        this.Literal2.Text = dsSource.Tables[0].Rows.Count > 0 ? dsSource.Tables[0].Rows[0]["menuName"].ToString() : "";
    }
}
