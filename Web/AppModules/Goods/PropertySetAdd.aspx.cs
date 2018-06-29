using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using Startech.Utils;
using StarTech.ELife.Goods;

public partial class AppModules_Goods_PropertySetAdd : StarTech.Adapter.StarTechPage
{
    public string id;
    public string isShow;
    MorePropertyConfigSetBll bll = new MorePropertyConfigSetBll();
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        this.id = (Request["id"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["id"], 50);
        this.isShow = (Request["isShow"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByNumber(Request["isShow"], 10);


        if (!IsPostBack)
        {
            InitForm();

            if (this.isShow == "1")
            {
                //this.SetReadOnlyPanel();
                this.btnSave.Visible = false;
            }
        }
    }

    protected void InitForm()
    {
        if (this.id != "")
        {
            //DataSet ds = adoHelper.ExecuteSqlDataset("select * from T_Goods_MorePropertySet where id='"+KillSqlIn.Form_ReplaceByString(id,20)+"';");
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    this.txtName.Value = ds.Tables[0].Rows[0]["propertyName"].ToString();
            //    this.txtValues.Text = ds.Tables[0].Rows[0]["propertyOptions"].ToString();
            //    this.txtOrder.Value = ds.Tables[0].Rows[0]["orderBy"].ToString();
            //    this.rdFlag.SelectedValue = ds.Tables[0].Rows[0]["porpertyFlag"].ToString();
            //    this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
            //}
            MorePropertyConfigSetModel mod = bll.GetModel(this.id);
            if (mod != null)
            {

                this.txtName.Value = mod.propertyName;
                this.txtValues.Text = mod.propertyOptions;
                this.txtOrder.Value = mod.orderBy.ToString();
                this.rdFlag.SelectedValue = mod.porpertyFlag;
                this.txtRemarks.Text = mod.remarks;
            }


        }
        else
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (this.id == "")
        {
            MorePropertyConfigSetModel mod = new MorePropertyConfigSetModel();
            mod.propertyId = IdCreator.CreateId("T_Goods_MorePropertySet", "propertyId");
            GetFormInfo(ref mod);
            if (bll.Add(mod) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');location.href('MorePropertySetList.aspx');</script>");
            }
        }
        else
        {
            MorePropertyConfigSetModel mod = bll.GetModel(this.id);
            GetFormInfo(ref mod);
            if (bll.Update(mod) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');location.href('MorePropertySetList.aspx');</script>");
            }
        }
    }

    protected void GetFormInfo(ref MorePropertyConfigSetModel mod)
    {

        mod.propertyName = this.txtName.Value.Trim();
        mod.propertyOptions = this.txtValues.Text.Trim(); 
        int orderby = 0;
        int.TryParse(this.txtOrder.Value.Trim(), out orderby);
        mod.orderBy = orderby;
        mod.porpertyFlag = this.rdFlag.SelectedValue;
        mod.remarks = this.txtRemarks.Text;
    }
}