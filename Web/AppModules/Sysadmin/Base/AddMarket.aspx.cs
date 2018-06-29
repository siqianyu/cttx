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

public partial class AppModules_Sysadmin_Base_AddMarket : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    protected string _Pid = "";
    private string _rd = "";
    MarketBll bll = new MarketBll();
    MarketModel model = new MarketModel();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], 30);
        BindPArea();
        if (!IsPostBack)
        {
            if (_Pid != "SysError")
            {
                _pageTitle = "市场信息编辑";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "市场信息添加";
            }
            
        }
    }

    #region 绑定所有区域
    public void BindPArea()
    {
        AreaBll areaBll = new AreaBll();
        DataTable dt = areaBll.GetAllList().Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.ddlAreaId.Items.Add(new ListItem(dt.Rows[i]["area_name"].ToString(), dt.Rows[i]["area_id"].ToString()));
        }
            
        
    }
    #endregion

    #region 市场信息
    public void GetAreaInfo()
    {
        model = bll.GetModel(_Pid);
        if (model != null)
        {
            txtMarketId.Text = model.Market_id;
            txtMarketName.Text = model.Market_name;
            if (!model.Area_id.Equals("0")) ddlAreaId.SelectedValue = model.Area_id;
            txtMapX.Text = model.Map_x;
            txtMapY.Text = model.Map_y;
            txtSort.Text = model.orderby.ToString();
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
            if (!_Pid.Equals(model.Market_id)) bll.UpdateId(model.Market_id, _Pid);//先更新编号
            if (bll.Update(model))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
        else
        {
            model.Market_id = IdCreator.CreateId("T_Base_Market", "Market_id");
            if (bll.Add(model))
            {
                JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
            }
        }
    }
    #endregion

    #region 得到Model
    public MarketModel GetModel()
    {
        model = new MarketModel();
        model.Market_id = KillSqlIn.Form_ReplaceByString(this.txtMarketId.Text, 100);
        model.Market_name = KillSqlIn.Form_ReplaceByString(this.txtMarketName.Text,200);
        model.Area_id = ddlAreaId.SelectedValue.Equals("0") ? "0" : ddlAreaId.SelectedValue;
        model.Map_x = KillSqlIn.Form_ReplaceByString(this.txtMapX.Text, 100);
        model.Map_y = KillSqlIn.Form_ReplaceByString(this.txtMapY.Text, 100);
        model.orderby = string.IsNullOrEmpty(txtSort.Text) ? 0 : Convert.ToInt32(txtSort.Text, 10);
        return model;
    }

    #endregion
}