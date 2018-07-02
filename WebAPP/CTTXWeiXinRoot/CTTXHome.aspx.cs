using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using StarTech.DBUtility;
using System.Data;

public partial class NGWeiXinRoot_CTTXHome : StarTech.Adapter.BasePage
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    public string TopCourse()
    {
        DataTable dt = ado.ExecuteSqlDataset("select top 4 GoodsId,GoodsName,GoodsSmallPic,GoodsSampleDesc,SalePrice,TotalSaleCount from T_Goods_Info where IsRec=1 and JobType='Goods' and IsSale=1 order by Orderby").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<li><a href='YqxkjCourseDetail.aspx?id=" + row["GoodsId"].ToString() + "'>";
            html += "<b><img src='" + row["GoodsSmallPic"].ToString() + "'></b>";
            html += "<span>" + row["GoodsName"].ToString() + "</span>";
            html += "</a></li>";
        }
        return html;
    }

    public string ListHelp(string CategoryID)
    {
        DataTable dt = ado.ExecuteSqlDataset("select top 3  NewsID,Title,CategoryId from T_News where CategoryId=" + CategoryID + " and Approved=1 order by ReleaseDate asc").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<a href='YqxkjNewsList2.aspx?dirId=" + row["CategoryId"] + "'>" + row["Title"] + "</a>";
        }
        return html;
    }

    public string ListAD()
    {
        DataTable dt = ado.ExecuteSqlDataset("SELECT [Link],[Image] FROM [T_Ad] where ISNULL([Image],'')<>'' order by sort asc").Tables[0];
        string html = "";
        string css = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<li onclick=\"location.href='" + row["Link"] + "'\" " + css + "><img src=\"" + row["Image"] + "\"></li>";
            if (css == "") { css = "style=\"display:none\""; }
        }
        return html;
    }
}