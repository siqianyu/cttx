using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Member.Member;
using CodeService;
using StarTech;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using StarTech.DBUtility;

public partial class AddMember : StarTech.Adapter.StarTechPage
{
    string type = string.Empty;
    int id, rd;
    public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(AddXHMember));
        type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"].ToString();
        id = Request.QueryString["id"] == null ? 0 : Convert.ToInt32(Request.QueryString["id"].ToString());
        rd = Request.QueryString["rd"] == null ? 0 : Convert.ToInt32(Request.QueryString["rd"].ToString());
        if (!IsPostBack)
        {
            loadMemberType();
            if (rd == 1)
            {
                this.btnSubmit.Visible = false;
                this.Button2.Visible = false;
            }
            if (type == "XH")
            {
                this.panel_xh.Visible = true;
                if (id != 0)
                {
                    MemberInfoBLL mBll = new MemberInfoBLL();
                    MemberInfoModel mModel = mBll.GetModel(id);
                    this.txtAddress.Text = mModel.address;
                    this.txtXHName.Text = mModel.memberCompanyName;
                    this.txtMemberName.Text = mModel.memberName;
                    // this.ddlMemberType.SelectedValue = mModel.memberCompanyType;
                    SelectType(mModel.memberCompanyType);
                    this.txtPwd.Text = mModel.password;
                    this.txtTel.Text = mModel.tel;
                    this.txtTrueName.Text = mModel.memberTrueName;

                    this.txtMemberName.ReadOnly = true;
                }
            }
            else if (type == "HZSIS")
            {
                this.panel_xz.Visible = true;
                if (id != 0)
                {
                    MemberInfoBLL mBll = new MemberInfoBLL();
                    MemberInfoModel mModel = mBll.GetModel(id);

                    this.txtXZMember.Text = mModel.memberName;
                    this.txtXZName.Text = mModel.memberCompanyName;
                    this.txtXZPwd.Text = mModel.password;
                    this.txtXZTel.Text = mModel.tel;
                    this.txtXZTrueName.Text = mModel.memberTrueName;
                    this.txtXZType.Text = mModel.memberCompanyType;

                    this.txtXZMember.ReadOnly = true;
                }
            }
        }
    }
    public string checkUsername(string userName)
    {
        string temp = string.Empty;
        temp = new MemberInfoBLL().CheckUserName(userName).ToString();
        return temp;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (type == "XH")
        {
            if (id == 0)
            {
                AddXHMember();
            }
            else
            {
                Update_AddXHMember();
            }
        }
        else if (type == "HZSIS")
        {
            if (id == 0)
            {
                AddXZMember();
            }
            else
            {
                Update_AddXZMember();
            }
        }
    }

    private void AddXHMember()
    {
        if (checkUsername(this.txtMemberName.Text.Trim()) == "1")
        {
            Response.Write("<script>alert('该协会会员已存在！');</script>");
        }
        else
        {
            try
            {
                MemberInfoModel model = new MemberInfoModel();
                model.memberName = KillSqlIn.Form_ReplaceByString(this.txtMemberName.Text, 20);
                model.password = KillSqlIn.Form_ReplaceByString(this.txtPwd.Text, 20);//ValidateUtil.MD5Encrypt(this.txtTwoPwd.Value);不用MD5加密
                model.memberLevel = "0";
                model.memberType = type;
              //  model.memberCompanyType = this.ddlMemberType.SelectedValue;
                model.memberCompanyType = GetHyType();
                model.areaName = "";
                model.memberCompanyName = txtXHName.Text;
                model.memberCompanyCode = "";
                model.memberTrueName = txtTrueName.Text;
                model.sex = "";
                model.tel = txtTel.Text;
                model.fax = "";
                model.mobile = "";
                model.address = this.txtAddress.Text;
                model.post = "";
                model.email = "";
                model.regTime = System.DateTime.Now;
                model.shFlag = 1;
                model.shTime = DateTime.Now;
                model.shPerson = " ";
                model.unPassReason = "";
                model.memberStatus = "ZC";//正常：ZC  禁用：JY
                model.buyMoneyAccount = 0;
                model.buyMoneyAccountUsed = 0;
                model.freeMoenyAccount = 0;
                model.freeMoenyAccountUsed = 0;
                int i = new MemberInfoBLL().Add(model);
                if (i > 0)
                {
                    Response.Write("<script>alert('添加成功！');layer_close();</script>");
                }
                else
                {
                    Response.Write("<script>alert('添加失败！');</script>");
                }

            }
            catch (Exception ee)
            {
                Response.Write("<script>alert('出错了, 原因：'" + ee.Message + "'');</script>");
            }
        }
    }

    private void Update_AddXHMember()
    {
        try
        {
            MemberInfoModel model = new MemberInfoModel();
            model.memberId = id;
            model.memberName = KillSqlIn.Form_ReplaceByString(this.txtMemberName.Text, 20);
            model.password = KillSqlIn.Form_ReplaceByString(this.txtPwd.Text, 20);//ValidateUtil.MD5Encrypt(this.txtTwoPwd.Value);不用MD5加密
            model.memberType = type;
          //  model.memberCompanyType = this.ddlMemberType.SelectedValue;
            model.memberCompanyType = GetHyType();
            model.memberCompanyName = txtXHName.Text;
            model.memberTrueName = txtTrueName.Text;
            model.tel = txtTel.Text;
            model.address = this.txtAddress.Text;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Member_Info set ");
            strSql.Append("memberName=@memberName,");
            strSql.Append("password=@password,");
            strSql.Append("memberType=@memberType,");
            strSql.Append("memberCompanyType=@memberCompanyType,");
            strSql.Append("memberCompanyName=@memberCompanyName,");
            strSql.Append("memberTrueName=@memberTrueName,");
            strSql.Append("tel=@tel,");
            strSql.Append("address=@address");
            strSql.Append(" where memberId=@memberId ");
            SqlParameter[] parameters = {
					new SqlParameter("@memberId", SqlDbType.Int,4),
					new SqlParameter("@memberName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,50),
					new SqlParameter("@memberType", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyType", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyName", SqlDbType.VarChar,100),
					new SqlParameter("@memberTrueName", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,500)};
            parameters[0].Value = model.memberId;
            parameters[1].Value = model.memberName;
            parameters[2].Value = model.password;
            parameters[3].Value = model.memberType;
            parameters[4].Value = model.memberCompanyType;
            parameters[5].Value = model.memberCompanyName;
            parameters[6].Value = model.memberTrueName;
            parameters[7].Value = model.tel;
            parameters[8].Value = model.address;

            int i = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (i > 0)
            {
                Response.Write("<script>alert('修改成功！');layer_close();</script>");
            }
            else
            {
                Response.Write("<script>alert('修改失败！');</script>");
            }
        }
        catch (Exception ee)
        {
            Response.Write("<script>alert('出错了');</script>");
        }
    }

    private void AddXZMember()
    {
        if (checkUsername(this.txtMemberName.Text.Trim()) == "1")
        {
            Response.Write("<script>alert('该行政会员已存在！');</script>");
        }
        else
        {
            try
            {
                MemberInfoModel model = new MemberInfoModel();
                model.memberName = KillSqlIn.Form_ReplaceByString(this.txtXZMember.Text, 20);
                model.password = KillSqlIn.Form_ReplaceByString(this.txtXZPwd.Text, 20);//ValidateUtil.MD5Encrypt(this.txtTwoPwd.Value);不用MD5加密
                model.memberType = type;
                model.memberCompanyType = this.txtXZType.Text;//部门名称
                model.memberCompanyName = this.txtXZName.Text;
                model.memberTrueName = txtXZTrueName.Text;
                model.tel = txtXZTel.Text;
                model.mobile = txtXZTel.Text;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update T_Member_Info set ");
                strSql.Append("memberName=@memberName,");
                strSql.Append("password=@password,");
                strSql.Append("memberType=@memberType,");
                strSql.Append("memberCompanyType=@memberCompanyType,");
                strSql.Append("memberCompanyName=@memberCompanyName,");
                strSql.Append("memberTrueName=@memberTrueName,");
                strSql.Append("tel=@tel");
                strSql.Append(" where memberId=@memberId ");
                SqlParameter[] parameters = {
					new SqlParameter("@memberId", SqlDbType.Int,4),
					new SqlParameter("@memberName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,50),
					new SqlParameter("@memberType", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyType", SqlDbType.VarChar,50),
					new SqlParameter("@memberCompanyName", SqlDbType.VarChar,100),
					new SqlParameter("@memberTrueName", SqlDbType.VarChar,50),
					new SqlParameter("@tel", SqlDbType.VarChar,50)};
                parameters[0].Value = model.memberId;
                parameters[1].Value = model.memberName;
                parameters[2].Value = model.password;
                parameters[3].Value = model.memberType;
                parameters[4].Value = model.memberCompanyType;
                parameters[5].Value = model.memberCompanyName;
                parameters[6].Value = model.memberTrueName;
                parameters[7].Value = model.tel;

                int i = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
                if (i > 0)
                {
                    Response.Write("<script>alert('添加成功！');layer_close();</script>");
                }
                else
                {
                    Response.Write("<script>alert('添加失败！');</script>");
                }
            }
            catch (Exception ee)
            {
                Response.Write("<script>alert('出错了, 原因：'" + ee.Message + "'');</script>");
            }
        }
    }

    private void Update_AddXZMember()
    {
        try
        {
            MemberInfoModel model = new MemberInfoModel();
            model.memberId = id;
            model.memberName = KillSqlIn.Form_ReplaceByString(this.txtXZMember.Text, 20);
            model.password = KillSqlIn.Form_ReplaceByString(this.txtXZPwd.Text, 20);//ValidateUtil.MD5Encrypt(this.txtTwoPwd.Value);不用MD5加密
            model.memberLevel = "0";
            model.memberType = type;
            model.memberCompanyType = this.txtXZType.Text;//部门名称
            model.areaName = "";
            model.memberCompanyName = this.txtXZName.Text;
            model.memberCompanyCode = "";
            model.memberTrueName = txtXZTrueName.Text;
            model.sex = "";
            model.tel = txtXZTel.Text;
            model.fax = "";
            model.mobile = "";
            model.address = "";
            model.post = "";
            model.email = "";
            model.regTime = System.DateTime.Now;
            model.shFlag = 1;
            model.shTime = DateTime.Now;
            model.shPerson = " ";
            model.unPassReason = "";
            model.memberStatus = "ZC";//正常：ZC  禁用：JY
            model.buyMoneyAccount = 0;
            model.buyMoneyAccountUsed = 0;
            model.freeMoenyAccount = 0;
            model.freeMoenyAccountUsed = 0;
            int i = new MemberInfoBLL().Update(model);
            if (i > 0)
            {
                Response.Write("<script>alert('修改成功！');layer_close();</script>");
            }
            else
            {
                Response.Write("<script>alert('修改失败！');</script>");
            }
        }
        catch (Exception ee)
        {
            Response.Write("<script>alert('出错了, 原因：'" + ee.Message + "'');</script>");
        }
    }


    #region

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
                if (s == "")
                {
                    s += ck.Value;
                }
                else
                {
                    s += "," + ck.Value;
                }
            }
        }
        return s;
    }

    private void SelectType(string type)
    {
        foreach (ListItem item in this.ddlMemberType.Items)
        {
            if (type.IndexOf(item.Value) > -1)
            {
                item.Selected = true;
            }
        }
    }


    #endregion
}
