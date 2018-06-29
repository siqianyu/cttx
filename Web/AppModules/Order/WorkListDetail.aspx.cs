using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Order_WorkListDetail : StarTech.Adapter.StarTechPage
{
    protected string id = "";
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
            return;
        id = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 50);
        if (id == "")
            return;
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    /// <summary>
    /// 绑定信息
    /// </summary>
    protected void BindInfo()
    {
        string strSQL = "select * from V_WorkInfo where OrderId='" + id + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return;

        DataSet dsEnd = adoHelper.ExecuteSqlDataset("select * from T_Member_CompleteJob where orderid='" + id + "'");
        if (dsEnd.Tables[0].Rows.Count > 0)
        {
            this.lbPJ.Text = dsEnd.Tables[0].Rows[0]["CompleteInfo"].ToString();
            this.lbPJTime.Text = dsEnd.Tables[0].Rows[0]["CompleteTime"].ToString();
        }

        this.div_money.InnerHtml = ListLog(ds.Tables[0].Rows[0]["MemberId"].ToString(), ds.Tables[0].Rows[0]["GoodsId"].ToString());

    }

    protected string ListLog(string memberId,string goodsId)
    {
        string s = "";
        DataSet ds = adoHelper.ExecuteSqlDataset("select a.*,b.truename,b.membername from T_Member_JobRecord a,T_Member_Info b where a.MemberId=b.MemberId and a.MemberId='" + memberId + "' and GoodsId='" + goodsId + "' order by recordTime desc");

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            s += "<tr><td>" + row["recordTime"] + "</td><td>" + row["truename"].ToString() + "</td><td>" + row["membername"].ToString() + "</td><td>" + row["JobAddress"] + "</td><td>" + row["RecordInfo"].ToString() + "</td></tr>";
        }
        if (s != "") { s = "<table width=460 style='border-collapse:collapse;' border='1' borderColor='#cccccc'>" + s + "</table>"; }
        return s;
    }
   
}