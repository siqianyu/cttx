using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using StarTech;
using StarTech.ELife;
using StarTech.ELife.Base;


public partial class AppModules_Sysadmin_Base_AddArea : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected string _pageTitle = string.Empty;
    protected string _Pid = "";
    private string _rd = "";
    AreaBll bll = new AreaBll();
    AreaModel model = new AreaModel();



    protected void Page_Load(object sender, EventArgs e)
    {
        _Pid = Server.UrlDecode(KillSqlIn.Url_ReplaceByString(Request.QueryString["id"], 30, " |#|,".Split('|')));
        BindPArea();
        if (!IsPostBack)
        {
            if (_Pid != "SysError")
            {
                _pageTitle = "区域信息编辑";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "区域信息添加";
            }
            
        }
    }

    #region 绑定所有区域
    public void BindPArea()
    {
        DataTable dt = bll.GetAllList().Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.ddlPid.Items.Add(new ListItem(dt.Rows[i]["area_name"].ToString(), dt.Rows[i]["area_id"].ToString()));
            //ddlPid.DataSource = dt;
            //ddlPid.DataTextField = "area_name";
            //ddlPid.DataValueField = "area_id";
            //ddlPid.DataBind();
        }
            
        
    }
    #endregion

    #region 区域信息
    public void GetAreaInfo()
    {
        model = bll.GetModel(_Pid);
        if (model != null)
        {
            txtAreaId.Text = model.area_id;
            txtName.Text = model.area_name;
            if (!model.area_pid.Equals("0")) ddlPid.SelectedValue = model.area_pid;
            txtSort.Text = model.orderby.ToString();
            object objHot = adohelper.ExecuteSqlScalar("select ishot from T_Base_Area where area_id='" + model.area_id + "'");
            this.cbHot.Checked = (objHot.ToString() == "1") ? true : false;
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
            if (!_Pid.Equals(model.area_id)) bll.UpdateId(model.area_id, _Pid);//先更新编号
            if (bll.Update(model))
            {
                adohelper.ExecuteSqlNonQuery("update T_Base_Area set ishot=" + (this.cbHot.Checked ? "1" : "0") + " where area_id='" + model.area_id + "'");
                this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
        else
        {
            model.area_id = IdCreator.CreateId("T_Base_Area", "area_id");
            if (bll.Add(model))
            {
                adohelper.ExecuteSqlNonQuery("update T_Base_Area set ishot=" + (this.cbHot.Checked ? "1" : "0") + " where area_id='" + model.area_id + "'");
                JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
            }
        }
    }
    #endregion

    #region 得到Model
    public AreaModel GetModel()
    {
        model = new AreaModel();
        model.area_id = KillSqlIn.Form_ReplaceByString(this.txtAreaId.Text, 100);
        model.area_name = KillSqlIn.Form_ReplaceByString(this.txtName.Text,200);
        model.area_pid = ddlPid.SelectedValue.Equals("0") ? "0" : ddlPid.SelectedValue;
        model.area_level = ddlPid.SelectedValue.Equals("0") ? 1 : bll.GetModel(model.area_pid).area_level + 1;
        model.orderby = string.IsNullOrEmpty(txtSort.Text) ? 0 : Convert.ToInt32(txtSort.Text, 10);
        return model;
    }

    #endregion
}