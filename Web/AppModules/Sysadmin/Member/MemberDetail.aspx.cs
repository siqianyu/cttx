using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Member.Member;
using StarTech;
using System.Data;
using StarTech.DBUtility;


public partial class MemberDetail : StarTech.Adapter.StarTechPage
{
    private string PageIndex;
    private int userid, rd;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageIndex = Request.QueryString["PageIndex"] == null ? "" : Request.QueryString["PageIndex"].ToString();
        userid = Request.QueryString["userid"] == null ? 0 : Convert.ToInt32(Request.QueryString["userid"].ToString());
        rd = Request.QueryString["rd"] == null ? 0 : Convert.ToInt32(Request.QueryString["rd"].ToString());
        if (!IsPostBack)
        {
            if (userid == 0)
            {
                this.txtshTime.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (rd == 1)
            {
                this.Button2.Visible = false;
            }
            loadMemberType();
            Bind();
        }
    }

    private void Bind()
    {
        MemberInfoBLL memberBll = new MemberInfoBLL();
        MemberInfoModel model = memberBll.GetModel(userid);
        if (model != null)
        {
            litUserName.Text = model.memberName;
            this.litPassword.Text = model.password;
            this.litbuyMoneyAccount.Text = model.buyMoneyAccount.ToString();
            this.litfreeMoenyAccount.Text = model.freeMoenyAccount.ToString();
            litMemberType.Text = GetMemType(model.memberType);
            litAreaName.Text = model.areaName;
            //本地 会员原始数据企业类型为类型值，需要对应获取类型名称
            litmemberCompanyType.Text = GetHy(model.memberCompanyType);
            //外网 会员数据类型企业类型为中文名称，不需要处理
            // litmemberCompanyType.Text = model.memberCompanyType;

            SelectType(model.memberCompanyType);
            litmemberCompanyName.Text = model.memberCompanyName;
            litmemberCompanyCode.Text = model.memberCompanyCode;
            litmemberTrueName.Text = model.memberTrueName;
            litSex.Text = model.sex;
            litaddress.Text = model.address;
            litPhone.Text = model.tel;
            litMobile.Text = model.mobile;
            litFax.Text = model.fax;
            litPost.Text = model.post;
            litEmail.Text = model.email;
            litRegDate.Text = Convert.ToDateTime(model.regTime).ToString("yyyy-MM-dd");
            rdlshFlag.SelectedValue = model.shFlag.ToString();
            rdlmemberStatus.SelectedValue = model.memberStatus;
            txtshTime.Value = model.shTime.ToString() != "2011-01-01 00:00:00.000" ? Convert.ToDateTime(model.shTime).ToString("yyyy-MM-dd") : System.DateTime.Now.ToString("yyyy-MM-dd");
            txtshPerson.Text = model.shPerson;
            txtunPassReason.Text = model.unPassReason;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        MemberInfoModel _mod = new MemberInfoModel();
        MemberInfoBLL bll = new MemberInfoBLL();
        _mod = bll.GetModel(userid);
        if (_mod != null)
        {
            _mod.shFlag = Convert.ToInt32(this.rdlshFlag.SelectedValue);
            _mod.shPerson = this.txtshPerson.Text;
            _mod.shTime = Convert.ToDateTime(this.txtshTime.Value);
            _mod.unPassReason = this.txtunPassReason.Text;
            _mod.memberStatus = this.rdlmemberStatus.SelectedValue;
            _mod.memberCompanyType = GetHyType();
            if ((bll.Update(_mod)) > 0)
            {
                string BackUrl = "MemberList.aspx?PageIndex=" + PageIndex;
                /*日志归档*/
                string sql = @"select memberName as title from T_Member_Info u where memberId=" + userid + "";
                PubFunction.InsertLog("业务管理", "会员管理", "会员列表", "修改", sql, userid.ToString());

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('操作成功!');layer_close();</script>");

            }
        }
    }

    #region 企业类型
    public void loadMemberType()
    {
        DataTable dt = DalBase.Util_GetList("select HyName,HyCode from t_base_hy order by hycode desc").Tables[0];
        this.ddlMemberType.DataSource = dt;
        this.ddlMemberType.DataTextField = "HyName";
        this.ddlMemberType.DataValueField = "HyCode";
        this.ddlMemberType.DataBind();
        // ddlMemberType.Items.Insert(0, new ListItem("请选择", "0"));
    }

    private string GetHyType()
    {
        string s = "";
        foreach (ListItem ck in this.ddlMemberType.Items)
        {
            if (ck.Selected == true)
            {
                //本地会员类型保存为类型编号
                if (s == "")
                {
                    s += ck.Value;
                }
                else
                {
                    s += "," + ck.Value;
                }
                //外网会员类型为中文名称，为按照原有格式，现保存类型名称
                //if (s == "")
                //{
                //    s += ck.Text;
                //}
                //else
                //{
                //    s += "," + ck.Text;
                //}
            }
        }
        return s;
    }

    private void SelectType(string type)
    {
        foreach (ListItem item in this.ddlMemberType.Items)
        {
            //外网会员原始数据企业类型为中文类型名称，本地为类型值
            //if (type.IndexOf(item.Text) > -1)
            //{
            //    item.Selected = true;
            //}
            //本地会员原始数据企业类型为类型值
            if (type.IndexOf(item.Value) > -1)
            {
                item.Selected = true;
            }
        }
    }

    public string GetMemType(string type)
    {
        switch (type.ToUpper())
        {
            case "COM":
            case "QY":
                return "企业会员";
            case "XH":
                return "协会会员";
            case "HZSIS":
                return "行政会员";
            default:
                return "暂不明确";
        }
    }

    public string GetHy(string hy)
    {
        DataTable dt = DalBase.Util_GetList("select HyName from t_base_hy where HyCode='" + hy + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["HyName"].ToString();
        }
        else
        {
            return "暂不明确";
        }
    }
    #endregion
}
