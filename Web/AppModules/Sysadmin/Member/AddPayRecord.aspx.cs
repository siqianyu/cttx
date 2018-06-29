using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Member.Member;
using StarTech.Util;
using System.Data;
using StarTech;

public partial class AppModules_Sysadmin_Member_AddMemberRecord : StarTech.Adapter.StarTechPage
{
    MemberCZRecordBLL bll = new MemberCZRecordBLL();
    MemberCZRecordModel model = new MemberCZRecordModel();

    private string _mid = SafeRequest.GetQueryString("mid");
    protected string _pageTitle = string.Empty;
    private int PageIndex = SafeRequest.GetQueryInt("PageIndex", 0);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AppModules_Sysadmin_Member_AddMemberRecord));
            CheckPopedom();
        }
    }

    #region 权限判断
    /// <summary>
    /// 权限判断
    /// </summary>
    protected void CheckPopedom()
    {
        if (_mid != "")
        {
            _pageTitle = "会员充值信息查看";
            GetMemberCZRecordInfo();
            this.palSH.Visible = true;
            this.btnSubmit.Visible = false;
        }
        else
        {
            _pageTitle = "会员充值信息添加";
            this.txtReleaseDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtAddPerson.Text = Request.Cookies["__UserInfo"]["nickname"].ToString();
            this.palSH.Visible = false;
            this.btnSubmit.Visible = true;
        }
    }
    #endregion

    #region 会员充值信息
    /// <summary>
    /// 获得领导详细信息
    /// </summary>
    protected void GetMemberCZRecordInfo()
    {
        model = bll.GetModel(_mid);
        if (model != null)
        {
            int id = Convert.ToInt32(model.memberId);
            string strname = new MemberInfoBLL().GetModel(id).memberName;
            this.txtName.Text = strname;
            this.txtMemberId.Value = model.memberId.ToString();
            this.txtMoney.Text = model.money.ToString();
            this.ddlMoneyType.SelectedValue = model.moneyType;
            this.txtRemarks.Text = model.remarks;
            this.txtReleaseDate.Value = Convert.ToDateTime(model.addTime).ToString("yyyy-MM-dd");//发布日期
            this.txtAddPerson.Text = model.addPerson;
            this.ltlshFlag.Text = model.shFlag == 0 ? "未审核" : "审核";
            this.ltlshPerson.Text = model.shPerson == "" ? "&nbsp;" : model.shPerson;
            this.ltlshTime.Text = model.shTime.ToString() == "" ? "&nbsp;" : Convert.ToDateTime(model.shTime).ToString("yyyy-MM-dd");
        }
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new MemberInfoBLL().GetList("memberName='" + this.txtMemberId.Value + "'");
        int id = 0;
        if (ds.Tables[0].Rows.Count > 0)
        {
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["memberId"].ToString());
        }
        model.memberId = id;// Convert.ToInt32(KillSqlIn.Form_ReplaceByString(this.txtMemberId.Value, 10));
        model.money = Convert.ToDecimal(KillSqlIn.Form_ReplaceByString(this.txtMoney.Text, 10));
        model.moneyType = KillSqlIn.Form_ReplaceByString(this.ddlMoneyType.SelectedValue, 20);
        model.remarks = KillSqlIn.Form_ReplaceByString(this.txtRemarks.Text, 200);
        model.addPerson = KillSqlIn.Form_ReplaceByString(this.txtAddPerson.Text, 20);
        model.addTime = Convert.ToDateTime(this.txtReleaseDate.Value.Trim());
        model.shFlag = 0;

        string BackUrl = "MemberCZRecordList.aspx?PageIndex=" + PageIndex + "";
        if (_mid != "")
        {
            model.sysnumber = _mid;
            bll.Update(model);

            /*日志归档*/
            string sql = @"select l.memberId as title from T_Member_AccountRecord l where sysnumber='" + this._mid + "'";
            PubFunction.InsertLog("业务管理", "会员列表", "会员充值列表", "修改", sql, _mid);

            JSUtility.AlertAndRedirect("修改成功!", BackUrl);
        }
        else
        {
            int i = bll.Add(model);
            /*日志归档*/
            string sql = @"select l.memberId as title from T_Member_AccountRecord l where sysnumber='" + i.ToString() + "'";
            PubFunction.InsertLog("业务管理", "会员列表", "会员充值列表", "添加", sql, i.ToString());
            JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
        }
    }
}
