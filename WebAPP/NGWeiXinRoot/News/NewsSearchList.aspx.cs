using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class NGWeiXinRoot_News_NewsSearchList : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    public string CategoryInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hidKeyword.Value = Request["k"] == null ? "" : Server.UrlDecode(Request["k"]);
        this.searchInput.Value = this.hidKeyword.Value;
      
    }

    protected void BindDir()
    {
        string html = "";
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_Category where [ParentCategoryId]=2 order by [Sort] asc").Tables[0];
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