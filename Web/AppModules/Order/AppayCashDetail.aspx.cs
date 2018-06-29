using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Order_AppayCashDetail : StarTech.Adapter.StarTechPage
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
        string strSQL = "select * from T_Member_ApplayCashBank where sysnumber='" + id + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return;


        object obj = adoHelper.ExecuteSqlScalar("select membername from t_member_info where memberid='" + ds.Tables[0].Rows[0]["MemberId"].ToString() + "'");
        lbMemberId.Text = obj == null ? "" : obj.ToString();
        ViewState["MemberId"] = ds.Tables[0].Rows[0]["MemberId"].ToString();

        lbCardNumber.Text = ds.Tables[0].Rows[0]["BankInfo"].ToString();
        lbApplayTime.Text = ds.Tables[0].Rows[0]["ApplayTime"].ToString();
        lbMoney.Text = ds.Tables[0].Rows[0]["Moeny"].ToString();
        this.lbKuoMoney.Text = GetKouMoney(id);
        ddlIfDeal.SelectedValue = ds.Tables[0].Rows[0]["IfDeal"].ToString();
        lbDealAdmin.Text = ds.Tables[0].Rows[0]["DealAdmin"].ToString();
        lbDealTime.Text = ds.Tables[0].Rows[0]["DealTime"].ToString();
        if (ddlIfDeal.SelectedValue == "0") { lbDealTime.Visible = false; }
        if (ddlIfDeal.SelectedValue == "1") { ddlIfDeal.Enabled = false; }
        lbDealRemarks.Text = ds.Tables[0].Rows[0]["DealRemarks"].ToString();

        object objYE = adoHelper.ExecuteSqlScalar("select account_money from T_Moneybag_AccountInfo where member_id='" + ds.Tables[0].Rows[0]["MemberId"].ToString() + "'");
        lbYE.Text = objYE == null ? "0" : objYE.ToString();

        
        if (ds.Tables[0].Rows[0]["IsPayByUnline"].ToString() == "1") { this.btnCheck.Visible = this.btnSave.Visible = false; }
        if (ds.Tables[0].Rows[0]["IfDeal"].ToString() == "1" && ds.Tables[0].Rows[0]["IsPayByUnline"].ToString() != "1") { this.btnSave.Visible = false; this.btnCheck.Visible = true; }
        if (ds.Tables[0].Rows[0]["IfDeal"].ToString() == "-1" && ds.Tables[0].Rows[0]["IsPayByUnline"].ToString() != "1") { this.btnSave.Visible = false; this.btnCheck.Visible = false; }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((decimal.Parse(lbMoney.Text) > decimal.Parse(lbYE.Text)) && ddlIfDeal.SelectedValue == "1")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('余额不足');location.href=location.href;</script>");
            return;
        }
        string sql = "update T_Member_ApplayCashBank set DealAdmin='" + this.UserName + "',DealTime=getdate(), IfDeal=" + ddlIfDeal.SelectedValue + ",DealRemarks='" + lbDealRemarks.Text.Replace("'", "’") + "' where sysnumber='" + id + "'";
        if (this.ddlIfDeal.SelectedValue == "0")
        {
            sql = "update T_Member_ApplayCashBank set DealAdmin='" + this.UserName + "',DealTime=getdate(),DealRemarks='" + lbDealRemarks.Text.Replace("'", "’") + "' where sysnumber='" + id + "'";
        }
        
        adoHelper.ExecuteSqlNonQuery(sql);

        if (this.ddlIfDeal.SelectedValue == "-1")
        {
            //电子钱包记录
            MoneyBagTools.DelMoneyBagDetail(ViewState["MemberId"].ToString(), id);
            Common.AddLogMsg(ViewState["MemberId"].ToString(), "提现审核失败", lbDealRemarks.Text.Replace("'", "’"), "#", 0);
        }

        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('提交成功');location.href=location.href;</script>");
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        string sql = "update T_Member_ApplayCashBank set IsPayByUnline='1',DealAdmin='" + this.UserName + "',DealTime=getdate(),DealRemarks='" + lbDealRemarks.Text.Replace("'", "’") + "' where sysnumber='" + id + "'";
        if (adoHelper.ExecuteSqlNonQuery(sql) > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('提交成功');location.href=location.href;</script>");
        }
    }

    protected string GetKouMoney(string id)
    {
        string sql = "select money from T_Moneybag_AccountDetail where detail_type='xf2' and from_source_id='" + id + "'";
        object obj = adoHelper.ExecuteSqlScalar(sql);
        return obj == null ? "0" : obj.ToString();
    }
}