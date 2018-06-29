using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_DivDialog : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 弹出层对话框width
    /// </summary>
    public int DialogWidth
    {
        set { ViewState["DialogWidth"] = value; }
        get { return ViewState["DialogWidth"] == null ? 500 : int.Parse(ViewState["DialogWidth"].ToString()); }
    }
    /// <summary>
    /// 弹出层对话框height
    /// </summary>
    public int DialogHeight
    {
        set { ViewState["DialogHeight"] = value; }
        get { return ViewState["DialogHeight"] == null ? 400 : int.Parse(ViewState["DialogHeight"].ToString()); }
    }

    /// <summary>
    /// 弹出层对话框title信息
    /// </summary>
    public string DialogTitle
    {
        set { ViewState["DialogTitle"] = value; }
        get { return ViewState["DialogTitle"] == null ? "选择" : ViewState["DialogTitle"].ToString(); }
    }

    /// <summary>
    /// 弹出层对话框id编号
    /// </summary>
    public string DialogId
    {
        set { ViewState["DialogId"] = value; }
        get { return ViewState["DialogId"] == null ? "DivDialogId001" : ViewState["DialogId"].ToString(); }
    }

    /// <summary>
    /// 弹出层Iframe地址
    /// </summary>
    public string DialogIFrameSrc
    {
        set { ViewState["DialogIFrameSrc"] = value; }
        get { return ViewState["DialogIFrameSrc"] == null ? "about:blank" : ViewState["DialogIFrameSrc"].ToString(); }
    }
}