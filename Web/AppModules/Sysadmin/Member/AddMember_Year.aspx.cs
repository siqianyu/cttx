using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Member.Member;
using CodeService;
using StarTech;

public partial class AddMember : StarTech.Adapter.StarTechPage
{

    string type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"].ToString();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AddMember));
    }
    [AjaxPro.AjaxMethod]
    public string checkjgdm(string jgdm)
    {
        SearchCodeInfo search = new SearchCodeInfo();
        string ticket = search.GetAuthorizationTicket("xingzhen", "xz20100722");
        JgdmInfo info = search.GetJgdmDetail(ticket, jgdm.Replace("-", "").Replace("－", ""));
        if (info.State == "000001")
        {
            return info.jgmc;
        }
        else
        {
            return "验证失败";
        }
    }
    [AjaxPro.AjaxMethod]
    public string checkUsername(string userName)
    {
        string temp = string.Empty;
        temp = new MemberInfoBLL().CheckUserName(userName).ToString();
        return temp;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SearchCodeInfo search = new SearchCodeInfo();
        string ticket = search.GetAuthorizationTicket("xingzhen", "xz20100722");
        JgdmInfo info = search.GetJgdmDetail(ticket, txtjgdm.Text.Trim().Replace("-", "").Replace("－", ""));
        if (info.State == "000001")
        {
            MemberInfoModel model = new MemberInfoModel();

            if (checkUsername(KillSqlIn.Form_ReplaceByString(this.txtname.Text, 20)) == "0")
            {
                model.memberName = KillSqlIn.Form_ReplaceByString(this.txtname.Text, 20);
                model.password = KillSqlIn.Form_ReplaceByString(this.txtpw.Text, 20);//ValidateUtil.MD5Encrypt(this.txtTwoPwd.Value);不用MD5加密
                model.memberLevel = "20";
                model.memberType = type;
                model.memberCompanyType = "";
                model.areaName = "";
                model.memberCompanyName = info.jgmc;
                model.memberCompanyCode = txtjgdm.Text;
                model.memberTrueName = "";
                model.sex = "";
                model.tel = "";
                model.fax = "";
                model.mobile = "";
                model.address = "";
                model.post = "";
                model.email = "";
                model.regTime = System.DateTime.Now;
                model.shFlag = 1;
                model.shTime = DateTime.Now;
                model.shPerson = "SYS";
                model.unPassReason = "";
                model.memberStatus = "ZC";//正常：ZC  禁用：JY
                model.buyMoneyAccount = 0;
                model.buyMoneyAccountUsed = 0;
                model.freeMoenyAccount = 0;
                model.freeMoenyAccountUsed = 0;
                int i = new MemberInfoBLL().Add(model);
                if (i > 0)
                {
                    Session["MemberId"] = i.ToString();
                    Response.Write("<script>alert('注册成功！');layer_close();</script>");
                }
                else
                {
                    Response.Write("<script>alert('注册失败！');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('该用户名已存在！');layer_close();</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('组织机构代码错误！');</script>");
            return;
        }
    }
}
