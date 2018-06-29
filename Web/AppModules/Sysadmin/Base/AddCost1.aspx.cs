using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using StarTech;
using StarTech.ELife.Base;
using System.Data;

public partial class AppModules_Sysadmin_Base_AddCost1 : System.Web.UI.Page
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    Goods_ServiceDetailModel model = new Goods_ServiceDetailModel();
    Goods_ServiceDetailBll bll = new Goods_ServiceDetailBll();
    protected string _pageTitle = string.Empty;
    protected string _Pid = "";
    private string _rd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], 40);
        if (!IsPostBack)
        {
            if (_Pid != "SysError")
            {
              
                _pageTitle = "加工费设置";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "加工费设置";
            }

        }
    }
    public void GetAreaInfo()
    {
        GoodsModel model1 = new GoodsModel();
        GoodsBll bll1 = new GoodsBll();
        model = bll.GetModel1(_Pid);
     
        model1 = bll1.GetModel(_Pid);   
        this.txtname.Text = model1.serviceName;
        if (model != null)
        {
            //this.txtID.Text = model.sysnumber;
            this.txtPrice.Text = model.Price.ToString();
            this.txtValue.Text = model.value;
            //this.txtsysnumber.Visible =false ;
            //this.txtID.Visible = true;
            if (model.IsDefault == 1)
            {
                this.CheckDefault.SelectedValue = "1";
            }
            else 
            {
                this.CheckDefault.SelectedValue = "0";
            }
        }
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      
        

        model = bll.GetModel1(_Pid);
        model.value = KillSqlIn.Form_ReplaceByString(this.txtValue.Text, 200);
        model.Price = Convert.ToDecimal(this.txtPrice.Text);
        model.IsDefault = Convert.ToInt32(this.CheckDefault.SelectedValue);
        
        if (model!=null)
        {
            
            if (bll.Update(model))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
        else
        {
            model = new Goods_ServiceDetailModel();
            model.sysnumber = Guid.NewGuid().ToString();
            model.serviceId = _Pid;
            model.value = KillSqlIn.Form_ReplaceByString(this.txtValue.Text, 200);
            model.Price = Convert.ToDecimal(this.txtPrice.Text);
            model.IsDefault = Convert.ToInt32(this.CheckDefault.SelectedValue);
            if (bll.Add(model) != null)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('设置成功');layer_close_refresh();</script>");
            }
        }

    }
    
}