using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Order_AppayCashSet : StarTech.Adapter.StarTechPage
{
    protected string id = "";
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        
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
        this.lbKuoMoney.Text = (WebConfig.GetKouMoneySet() * 100).ToString();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        decimal m = decimal.Parse(this.lbKuoMoney.Text);
        if (m > 0) { m = m / 100; }
        string sql = "update T_Web_Config set KouMoney=" + m + "";
        adoHelper.ExecuteSqlNonQuery(sql);
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('提交成功');location.href=location.href;</script>");
    }
}