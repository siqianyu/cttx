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
using StarTech.Util;


public partial class AppModules_IACenter_AddUser : StarTech.Adapter.StarTechPage
{
    private string id;
    private string rd;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
        rd = Request.QueryString["rd"] == null ? "" : Request.QueryString["rd"].ToString();
        if (!IsPostBack)
        {
            // bindBM();
            if (id != "")
            {
                bind();
            }
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        StarTech.IACenter.ModIACenter_MarketUser mod = new StarTech.IACenter.ModIACenter_MarketUser();
        mod.userName = txtusername.Value;
        mod.password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtpwd.Value.Trim(), "MD5");
        mod.trueName = txttruename.Value;
        mod.sex = Ssex.Value;
        try
        {
            mod.age = int.Parse(txtage.Value);
        }
        catch { mod.age = 0; }
        mod.tel = txttel.Value;
        mod.mobile = txtsj.Value;
        mod.departId = 0;
        try
        {
            mod.orderBy = int.Parse(txtsort.Value);
        }
        catch { mod.orderBy = 0; }
        mod.addTime = DateTime.Now;
        mod.isUse = chkqy.Checked == false ? 0 : 1;
        mod.isSuperAdmin = chkadmin.Checked == false ? 0 : 1;

        //用户类型（领导：2，科室：3，镇街：4）
        // DataTable dtDepartment = new BllTableObject("T_Base_Department").Util_GetList("userInType", "uniqueId=" + mod.departId + "");
        mod.userType = 0;
        try
        {
            if (id == "")
            {
                if (new StarTech.IACenter.BllIACenter_MarketUser().Add(mod) > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('添加成功');layer_close_refresh();</script>");
                }
            }
            else
            {
                StarTech.IACenter.ModIACenter_MarketUser modOld = new StarTech.IACenter.BllIACenter_MarketUser().GetModel(int.Parse(id));
                if (modOld != null) { mod.password = modOld.password; }
                mod.uniqueId = int.Parse(id);
                if (new StarTech.IACenter.BllIACenter_MarketUser().Update(mod) > 0)
                {
                    new BllTableObject("IACenter_User").Util_UpdateBat("fax='" + this.txtcz.Value.Trim() + "',jobDesc='" + this.txtJob.Text.Trim() + "'", "uniqueId=" + mod.uniqueId + "");
                    ClientScript.RegisterStartupScript(this.GetType(), "u", "<script>alert('修改成功！');layer_close_refresh();</script>");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Source + "<br/>" + ex.Message);
        }

    }
    private void bind()
    {
        StarTech.IACenter.ModIACenter_MarketUser mod = new StarTech.IACenter.BllIACenter_MarketUser().GetModel(int.Parse(id));
        if (mod != null)
        {
            txtusername.Value = mod.userName;
            txttruename.Value = mod.trueName;
            Ssex.Value = mod.sex;
            txtage.Value = mod.age.ToString();
            txttel.Value = mod.tel;
            txtsj.Value = mod.mobile;
           // Sbm.Value = mod.departId.ToString();
            txtsort.Value = mod.orderBy.ToString();
            chkqy.Checked = mod.isUse == 1 ? true : false;
            chkadmin.Checked = mod.isSuperAdmin == 1 ? true : false;

            DataTable dt = new BllTableObject("IACenter_User").Util_GetList("*", "uniqueId=" + mod.uniqueId + "");
            if (dt.Rows.Count > 0)
            {
                this.txtJob.Text = dt.Rows[0]["jobDesc"].ToString();
                this.txtcz.Value = dt.Rows[0]["fax"].ToString();
            }
        }
        if (rd == "1")
        {
            this.SetReadOnlyPanel("readonly_input");
            ClientScript.RegisterStartupScript(this.GetType(), "rd", "<script>$('input[@type=checkbox]').each(function(){$(this).attr('disabled','disabled');});</script>");
            this.btnSave.Visible = false;
        }
    }
}
