using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_OutputExcel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //对象调用
    public Button MyButton
    {
        get { return this.btnExcel; }
    }

    //方法调用
    public delegate void ExcelHandler(object sender, EventArgs e);
    public event ExcelHandler OutputExcelEvent;
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (OutputExcelEvent != null) { OutputExcelEvent(sender, e); }
    }

    //权限控制Code
    public string AuthCode
    {
        set { ViewState["AutoCode"] = value; }
        get { return (ViewState["AutoCode"] == null) ? "" : ViewState["AutoCode"].ToString(); }
    }

    //权限控制开关
    public int AuthFlag
    {
        set { ViewState["AuthFlag"] = value; }
        get { return (ViewState["AuthFlag"] == null) ? 0 : Int32.Parse(ViewState["AuthFlag"].ToString()); }
    }

    /*权限控制函数由引用页面决定*/
}