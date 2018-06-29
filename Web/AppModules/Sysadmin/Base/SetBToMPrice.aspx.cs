using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using StarTech;
using StarTech.ELife.Base;

public partial class AppModules_Sysadmin_Base_SetBToMPrice : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    protected string _Pid = "";
    private string _rd = "";
    BuildingToMarketBll bll = new BuildingToMarketBll();
    BuildingToMarketModel model = new BuildingToMarketModel();
    BuildingBll buildingBll = new BuildingBll();
    BuildingModel buildingModel = new BuildingModel();
    MarketBll marketBll = new MarketBll();
    MarketModel marketModel = new MarketModel();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], Int32.MaxValue, " |#|,|-".Split('|')));
        BindBuildingAndMarket(_Pid);//绑定所有小区/大厦和农贸市场
        if (!IsPostBack)
        {
            if (_Pid != "SysError")
            {
                _pageTitle = "配送费设置";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "配送费设置";
            }
            
        }
    }

    #region 绑定小区/大厦
    public void BindBuildingAndMarket(string Id)
    {
        if (!string.IsNullOrEmpty(Id))
        {
            model = bll.GetModel(Id);
            if (model != null)
            {
                this.txtBuilding.Text = buildingBll.GetModel(model.Building_id).Building_name;
                this.txtMarket.Text = marketBll.GetModel(model.Market_id).Market_name;
            }
        }
        
    }
    #endregion

 



    #region 关系信息
    public void GetAreaInfo()
    {
        if (!string.IsNullOrEmpty(_Pid))
        {
            model = bll.GetModel(_Pid);
            if (model != null)
            {
                this.txtMinPrice.Text = model.MinPrice.ToString();
                this.txtMaxPrice.Text = model.MaxPrice.ToString();
                this.txtPrice.Text = model.Price.ToString();
                this.txtDistance.Text = model.Distance.ToString();
                ViewState["id"] = model.BuildingToMarket_id;
            }
        }
    }
    #endregion


    #region 提交
    /// <summary>
    /// 确定提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        model = GetModel();
        if (bll.Update(model))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('设置成功');layer_close_refresh();</script>");
        }
    }
    #endregion




    #region 得到Model
    public BuildingToMarketModel GetModel()
    {
        model = new BuildingToMarketModel();
        if (!string.IsNullOrEmpty(_Pid))
        {
            BuildingToMarketModel oldModel = bll.GetModel(_Pid);
            model.BuildingToMarket_id = oldModel.BuildingToMarket_id;
            model.Building_id = oldModel.Building_id;
            model.Market_id = oldModel.Market_id;
            model.orderby = oldModel.orderby;
        }
        model.MinPrice = !string.IsNullOrEmpty(this.txtMinPrice.Text) ? Convert.ToDecimal(this.txtMinPrice.Text) : 0;
        model.MaxPrice = !string.IsNullOrEmpty(this.txtMaxPrice.Text) ? Convert.ToDecimal(this.txtMaxPrice.Text) : 0;
        model.Price = !string.IsNullOrEmpty(this.txtPrice.Text) ? Convert.ToDecimal(this.txtPrice.Text) : 0;
        model.Distance = !string.IsNullOrEmpty(this.txtDistance.Text) ? Convert.ToInt32(this.txtDistance.Text) : 0;
        return model;
    }

    #endregion
}