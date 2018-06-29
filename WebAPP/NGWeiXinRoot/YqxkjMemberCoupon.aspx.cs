using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;

public partial class NGWeiXinRoot_YqxkjMemberCoupon : StarTech.Adapter.BasePage
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hid_memberId.Value = this.MemberId;
        ListUse();
    }

    protected void ListUse()
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_Member_Coupon where MemberId='" + this.MemberId + "' and isnull(IsUsed,0)=0 and EndTime>getdate() order by CouponValue desc").Tables[0];
        foreach (DataRow row in dt.Rows)
        {
          
        }
        this.rptListUse.DataSource = dt;
        this.rptListUse.DataBind();
    }
}