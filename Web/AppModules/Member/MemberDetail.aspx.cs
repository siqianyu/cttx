using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using System.Data;

public partial class AppModules_Member_MemberDetail : StarTech.Adapter.StarTechPage
{
    StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
    public string memberId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            memberId = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 20);
            if (memberId == "")
                return;
            GetMember(memberId);
        }
    }

    protected void GetMember(string memberID)
    {
        string strSQL = "select * from T_Member_Info where memberid='" + memberID + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0)
        {
            lbMemberId.Text = ds.Tables[0].Rows[0]["MemberId"].ToString();
            lbMemberName.Text = ds.Tables[0].Rows[0]["MemberName"].ToString();
            lbTrueName.Text = ds.Tables[0].Rows[0]["TrueName"].ToString();
            lbEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            lbTel.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
            lbQQ.Text = ds.Tables[0].Rows[0]["QQ"].ToString();
            lbSex.Text = ds.Tables[0].Rows[0]["Sex"].ToString();
            lbAddress.Text = ds.Tables[0].Rows[0]["AddressCode"].ToString() + " " + ds.Tables[0].Rows[0]["AddressDetail"].ToString() + " " + ds.Tables[0].Rows[0]["PostCode"].ToString();
            lbBirthDay.Text = ds.Tables[0].Rows[0]["BirthDay"].ToString().Replace("0:00:00", "");
            lbRegisterTime.Text = ds.Tables[0].Rows[0]["RegisterTiem"].ToString();
            lbLastLoginTime.Text = ds.Tables[0].Rows[0]["LastLoginTime"].ToString();
            lbHeadImg.Text = "<img src='" + ds.Tables[0].Rows[0]["HeadImg"].ToString() + "' width='100px' height='100px'/>";
            lbIsUse.Text = ds.Tables[0].Rows[0]["IsUse"].ToString() == "1" ? "<span style='color:Green'>启用</span>" : "<span style='color:Red'>禁用</span>";
            //kxm@20160610
            lbPersonIDCode.Text = ds.Tables[0].Rows[0]["PersonIDCode"].ToString();
            ltPersonIDCodePhoto.Text = ListPics2(memberID);
            lbSpecialty.Text = ds.Tables[0].Rows[0]["Specialty"].ToString();
            lbSkill.Text = ds.Tables[0].Rows[0]["Skill"].ToString();
            lbSelfIntroduction.Text = ds.Tables[0].Rows[0]["SelfIntroduction"].ToString();
            ltPics.Text = ListPics(memberID);
            //age
            try
            {
                int age = DateTime.Now.Year - DateTime.Parse(ds.Tables[0].Rows[0]["BirthDay"].ToString()).Year;
                this.lbAge.Text = age.ToString();
            }
            catch { this.lbAge.Text = "0"; }

            //money
            object objMoney = adoHelper.ExecuteSqlScalar("select isnull(account_money,0) from T_Moneybag_AccountInfo where member_id='" + memberID + "'");
            this.lbMoney.Text = objMoney == null ? "0.00" : objMoney.ToString();
            this.div_money.InnerHtml = ListMoney(memberID);

        }
    }


    protected string ListPics(string memberId)
    {
        string s = "";
        DataSet ds = adoHelper.ExecuteSqlDataset("SELECT applyPic from T_Member_Pic where applyType='resume' and memberId='" + memberId + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            s += " <img src='" + row["applyPic"].ToString() + "' width='100px' height='100px' style='padding:5px;'/> ";
        }
        return s;
    }

    protected string ListPics2(string memberId)
    {
        string s = "";
        DataSet ds = adoHelper.ExecuteSqlDataset("SELECT * from T_Member_Pic where applyType='auth' and memberId='" + memberId + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow row = ds.Tables[0].Rows[0];
            s += " <img src='" + row["applyPic"].ToString() + "' width='100px' height='100px' style='padding:5px;'/> ";
            s += " <img src='" + row["applyPic2"].ToString() + "' width='100px' height='100px' style='padding:5px;'/> ";
            this.lbPersonIDCodePass.Text = row["ifPass"].ToString() == "1" ? "(通过)" : "<font color='red'>(未通过)</font>";
        }
        return s;
    }

    protected string ListMoney(string memberId)
    {
        string s = "";
        DataSet ds = adoHelper.ExecuteSqlDataset("select * from T_Moneybag_AccountDetail where account_id=(select account_id from T_Moneybag_AccountInfo where member_id='" + memberId + "') order by createtime desc");

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            s += "<tr><td>" + row["createtime"] + "</td><td>" + row["money"] + "</td><td>" + (row["detail_type"].ToString() == "xf" ? "提现" : "收入") + "</td></tr>";
        }
        if (s != "") { s = "<table width=360 style='border-collapse:collapse;' border='1' borderColor='#cccccc'>" + s + "</table>"; }
        return s;
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        adoHelper.ExecuteSqlNonQuery("update T_Member_Pic set ifPass=1 where applyType='auth' and memberId='" + memberId + "'");
        GetMember(memberId);
        AddLogMsg(memberId, "实名审核", "你的实名审核已经通过");
    }
    protected void btnCheck2_Click(object sender, EventArgs e)
    {
        adoHelper.ExecuteSqlNonQuery("update T_Member_Pic set ifPass=0 where applyType='auth' and memberId='" + memberId + "'");
        GetMember(memberId);
        AddLogMsg(memberId, "实名审核", "你的实名审核未通过，请修改信息");
    }

    //消息日志
    public string AddLogMsg(string memberId, string title, string remark)
    {
        string sysnumber = Guid.NewGuid().ToString();
        string sql = " INSERT INTO [T_Log_Message]([sysnumber],[MemberId],[type],[createTime],[title],[remark])";
        sql += "VALUES('" + sysnumber + "','" + memberId + "','log',getdate(),'" + title + "','" + remark + "')";
        return adoHelper.ExecuteSqlNonQuery(sql).ToString();
    }

    protected void btnResetPwd_Click(object sender, EventArgs e)
    {
        string pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("111111", "MD5");
        adoHelper.ExecuteSqlNonQuery("update T_Member_Info set Password='" + pwd + "' where memberId='" + memberId + "'");
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('登录密码重置成功，新密码为111111');</script>");
    }


    protected void btnResetPwd2_Click(object sender, EventArgs e)
    {
        string pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("111111", "MD5");
        adoHelper.ExecuteSqlNonQuery("update T_Member_Info set PasswordPay='" + pwd + "' where memberId='" + memberId + "'");
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('交易密码重置成功，新密码为111111');</script>");
    }
}