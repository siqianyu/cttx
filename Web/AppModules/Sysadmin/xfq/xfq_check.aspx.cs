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
using Startech.Member.Member;
using StarTech;

public partial class AppModules_xfq_xfq_check : StarTech.Adapter.StarTechPage
{
    private string memberid;
    protected void Page_Load(object sender, EventArgs e)
    {
        memberid = Request.QueryString["memberid"] == null ? "" : KillSqlIn.Url_ReplaceByNumber(Request.QueryString["memberid"].ToString(), 10);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string type = ((Button)sender).CommandArgument;
        if (type == "0")
        {
            string id = txtid.Text.Trim();
            string pw = txtpw.Text.Trim();
            DataTable dt = DalBase.Util_GetList("select * from N_Stamp where StampNo='" + id + "' and Password='" + pw + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                lituse.Text = dt.Rows[0]["IsUsed"].ToString() == "0" ? "未使用" : "已使用";
                litgqsj.Text = Convert.ToDateTime(dt.Rows[0]["StampOutTime"].ToString()).ToString("yyyy-MM-dd");
                litje.Text = (float.Parse(dt.Rows[0]["StampMoney"].ToString()) / 100).ToString();
                Panel1.Visible = true;
                Panel2.Visible = false;
                txtid.ReadOnly = false;
                txtpw.ReadOnly = false;
                txtid.Style.Add("border", "none");
                txtpw.Style.Add("border", "none");
                btnSubmit.Text = "确定";
                btnSubmit.OnClientClick = "return CheckForm()";
                btnSubmit.CommandArgument = "1";
                bindhy();
                bindtc();
            }
            else
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
        }
        else if (type == "1")
        {
            string[] arry = RadioButtonList1.SelectedValue.Split('$');
            if (DalBase.Util_UpdateBat("memberLevel='" + arry[2] + "',TrustNumber=" + txttg.Text.Trim() + ",DownloadNumber=" + txtxz.Text.Trim() + ",levelServiceStartTime='" + txtStart.Value + "',levelServiceEndTime='" + txtEnd.Value + "'", "memberId=" + memberid, "T_Member_Info") > 0)
            {
                DalBase.Util_UpdateBat("IsUsed=1", "StampNo='" + txtid.Text.Trim() + "' and Password='" + txtpw.Text.Trim() + "'", "N_Stamp");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('设置成功！');layer_close();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('设置失败，请重试！');</script>");
            }
        }
    }

    private void bindhy()
    {
        if (memberid != "")
        {
            MemberInfoModel mod = new MemberInfoBLL().GetModel(int.Parse(memberid));
            if (mod != null)
            {
                lithym.Text = mod.memberName;
                litgsmc.Text = mod.memberCompanyName;
                lityx.Text = mod.levelServiceStartTime.ToString("yyyy-MM-dd") + "到" + mod.levelServiceEndTime.ToString("yyyy-MM-dd");
            }
        }
    }
    private void bindtc()
    {
        DataTable dt = DalBase.Util_GetList("select * from T_Member_Level where isSystemFlag=0").Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            RadioButtonList1.Items.Add(new ListItem(dr["levelName"].ToString() + "（可托管标准数：" + dr["trustStandardNumber"].ToString() + "，可下载标准数：" + dr["DownStdFileNumber"].ToString() + "）", dr["trustStandardNumber"].ToString() + "$" + dr["DownStdFileNumber"].ToString() + "$" + dr["levelId"].ToString()));
        }
    }
}
