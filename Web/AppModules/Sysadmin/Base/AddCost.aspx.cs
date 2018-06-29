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

public partial class AppModules_Sysadmin_Base_AddCost : System.Web.UI.Page
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    GoodsBll bll = new GoodsBll();
    GoodsModel model = new GoodsModel();
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
              
                _pageTitle = "加工费编辑";
                GetAreaInfo();
            }
            else
            {
                _pageTitle = "加工费添加";
            }

        }
    }
    public void GetAreaInfo()
    {
        model = bll.GetModel(_Pid);
        if (model != null)
        {
            this.txtName.Text = model.serviceName;
            this.txtMethod.Text = model.serviceContext;
            this.txtSort.Text = model.orderby.ToString();
        }
    }
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
                if (bll.Update(model))
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
                }
            }
            else
            {
                if (bll.Add(model) != null)
                {
                    JSUtility.AlertAndRedirect("添加成功,请继续添加!", Request.RawUrl.ToString());
                }
            }
        
    }
    public GoodsModel GetModel()
    {
        model = new GoodsModel();
        model.serviceId = Guid.NewGuid().ToString();
        model.serviceName = KillSqlIn.Form_ReplaceByString(this.txtName.Text, 50);
        model.serviceContext = KillSqlIn.Form_ReplaceByString(this.txtMethod.Text, 200);
        model.orderby = string.IsNullOrEmpty(txtSort.Text) ? 0 : Convert.ToInt32(txtSort.Text, 10);
        return model;
    }
  
}