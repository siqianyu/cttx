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

public partial class AppModules_Sysadmin_Base_AddBToM : StarTech.Adapter.StarTechPage
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
        BindBuilding();//绑定所有小区/大厦
        BindMarket();//绑定所有农贸市场
        if (!IsPostBack)
        {
            if (_Pid != "SysError")
            {
                _pageTitle = "关系信息编辑";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "关系信息添加";
            }
            
        }
    }

    #region 绑定所有小区/大厦
    public void BindBuilding()
    {
        DataTable dt = buildingBll.GetAllList().Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //this.ddlBuilding.Items.Add(new ListItem(dt.Rows[i]["Building_name"].ToString(), dt.Rows[i]["Building_id"].ToString()));
        }
    }
    #endregion

    #region 绑定所有农贸市场
    public void BindMarket()
    {
        DataTable dt = marketBll.GetAllList().Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.ddlMarket.Items.Add(new ListItem(dt.Rows[i]["Market_name"].ToString(), dt.Rows[i]["Market_id"].ToString()));
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
                this.txtBuilding.Text = buildingBll.GetModel(model.Building_id).Building_name;
                this.hidBuildingsId.Value = model.Building_id;
                this.ddlMarket.SelectedValue = model.Market_id;
                this.txtSort.Text = model.orderby.ToString();
                ViewState["id"] = model.BuildingToMarket_id;
                ViewState["Building_id"] = model.Building_id;
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
        if (_Pid != "SysError")
        {
            if (!this.hidBuildingsId.Value.Equals(ViewState["Building_id"].ToString()))//修改了小区
            {
                if (this.hidBuildingsId.Value.Contains("|"))//修改为多个小区，实为添加
                {
                    string[] ids = this.hidBuildingsId.Value.Split('|');
                    foreach (string s in ids)
                    {
                        if (bll.GetList(" Building_id = '" + s + "' and Market_id = '" + model.Market_id + "' ").Tables[0].Rows.Count <= 0)
                        {
                            model.BuildingToMarket_id = System.Guid.NewGuid().ToString();
                            model.Building_id = s;
                            if (bll.Add(model))
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
                            }
                        }
                    }
                }
                else//修改为一个小区
                {
                    if (bll.GetList(" Building_id = '" + this.hidBuildingsId.Value + "' and Market_id = '" + model.Market_id + "' ").Tables[0].Rows.Count <= 0)
                    {//修改的新关系不存在
                        model.BuildingToMarket_id = ViewState["id"].ToString();
                        model.Building_id = this.hidBuildingsId.Value;
                        if (bll.Update(model))
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
                        }
                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('该关系已经存在!');</script>");
                    }
                }
            }
            else
            {
                model.BuildingToMarket_id = ViewState["id"].ToString();
                model.Building_id = this.hidBuildingsId.Value;
                if (bll.Update(model))
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
                }
            }
        }
        else
        {
            string BuildingsId = this.hidBuildingsId.Value;
            if (BuildingsId.Contains("|"))
            {
                string[] ids = BuildingsId.Split('|');
                foreach (string s in ids)
                {
                    if (bll.GetList(" Building_id = '" + s + "' and Market_id = '" + model.Market_id + "' ").Tables[0].Rows.Count <= 0)
                    {
                        model.BuildingToMarket_id = System.Guid.NewGuid().ToString();
                        model.Building_id = s;
                        if (bll.Add(model))
                        {
                            JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
                        }
                    }
                }
            }
            else
            {
                model.BuildingToMarket_id = System.Guid.NewGuid().ToString();
                model.Building_id = BuildingsId;
                if (bll.Add(model))
                {
                    JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
                }
            }
        }
    }
    #endregion




    #region 得到Model
    public BuildingToMarketModel GetModel()
    {
        model = new BuildingToMarketModel();
        model.Market_id = this.ddlMarket.SelectedValue;
        model.orderby = string.IsNullOrEmpty(this.txtSort.Text) ? 0 : Convert.ToInt32(KillSqlIn.Form_ReplaceByString(this.txtSort.Text, 10));
        return model;
    }

    #endregion
}