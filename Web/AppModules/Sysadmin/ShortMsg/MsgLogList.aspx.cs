using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Sysadmin_ShortMsg_MsgLogList: StarTech.Adapter.StarTechPage
{
    protected string categoryId = "";
    /// <summary>
    /// 短信供应商列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["state"] != null)
        {
            categoryId = Request.QueryString["state"];
            if (categoryId == "1")
            {
                categoryId = "&statu=1";
            }
            else if (categoryId == "0")
            {
                categoryId = "&statu=0";
            }
        }
    }
}