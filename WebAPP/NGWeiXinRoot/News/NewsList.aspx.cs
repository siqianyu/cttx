using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using System.Web.Configuration;

public partial class NGWeiXinRoot_News_NewsList : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    public string CategoryInfo;
    public string ZYK_CategoryID;//资源库

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ZYK_CategoryID = WebConfigurationManager.AppSettings["ZYK_CategoryID"];//资源库
        BindDir(this.ZYK_CategoryID);
    }

    protected void BindDir(string ParentCategoryId)
    {
        string html = "";
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_Category where [ParentCategoryId]=" + ParentCategoryId + " order by [Sort] asc").Tables[0];
        string firstId = "";
        foreach (DataRow row in dt.Rows)
        {
            if (firstId == "") { firstId = row["CategoryId"].ToString(); }
            html += "<div id=\"tab_" + row["CategoryId"].ToString() + "\" class=\"weui-navbar__item\" onclick=\"list_news(" + row["CategoryId"].ToString() + ")\">";
            html += row["categoryname"].ToString();
            html += "</div>";
        }
        this.CategoryInfo = html;
        this.hidFirstDirId.Value = firstId;
    }
}