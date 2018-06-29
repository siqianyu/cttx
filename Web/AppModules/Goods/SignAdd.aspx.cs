using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Goods_SignAdd : StarTech.Adapter.StarTechPage
{
    protected string id = "";
    protected static DateTime addtime;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            id = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 10);
            if (!IsPostBack)
            {

                GetSign();
            }
        }
    }


    protected void GetSign()
    {
        string strSQL = "select * from T_Goods_Sign where signid="+id+";";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        txtSignName.Text = ds.Tables[0].Rows[0]["signName"].ToString();
        txtRemark.Text = ds.Tables[0].Rows[0]["remark"].ToString();
        addtime = Convert.ToDateTime(ds.Tables[0].Rows[0]["addtime"].ToString());
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        string signname = KillSqlIn.Form_ReplaceByString(txtSignName.Text,20);
        string remark = KillSqlIn.Form_ReplaceByString(txtRemark.Text, 20);
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        string strSQL = "";
        if (id == "")
        {
            DateTime atime = DateTime.Now;
            strSQL = "insert T_Goods_Sign values('" + signname + "','" + remark + "','" + atime + "');";
        }
        else
        {
            strSQL = "update T_Goods_Sign set signName='"+signname+"',remark='"+remark+"' where signid='"+id+"';";
        }
        int rows = adohelper.ExecuteSqlNonQuery(strSQL);
        if (rows > 0)
        {
            if (id == "")
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加成功');layer_close_refresh();</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改成功');layer_close_refresh();</script>");


        }
    }
}