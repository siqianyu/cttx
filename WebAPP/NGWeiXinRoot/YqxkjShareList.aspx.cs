using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class NGWeiXinRoot_YqxkjShareList : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["wx_openid"] != null)
        {
            BindList(Request["wx_openid"].ToString());
        }
    }

    protected void BindList(string wx_openid)
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_WXQRCodeShare_Log where myOpenId='" + wx_openid + "' order by logTime desc").Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            if (row["firendNewHeader"].ToString() == "") { row["firendNewHeader"] = "Images/yqxkj.png"; }
        }
        this.rptList.DataSource = dt;
        this.rptList.DataBind();
    }
}