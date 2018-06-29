using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using StarTech.DBUtility;
using System.Data;

public partial class NGWeiXinRoot_YqxkjHome : System.Web.UI.Page
{
    //首页8个模块入口
    public string KJJC_ID;
    public string SFKS_ID;
    public string SCKC_ID;
    public string GLKJ_ID;
    public string CWZX_ID;
    public string GLZX_ID;
    public string SWKJ_ID;
    public string WEID_ID;
    //底部3个帮助中心入口
    public string BZZX_CategoryID;//帮助中心
    public string KFSH_CategoryID;//客服&售后
    public string SWHZ_CategoryID;//商务合作

    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");

    protected void Page_Load(object sender, EventArgs e)
    {
        //首页8个模块入口
        this.KJJC_ID = WebConfigurationManager.AppSettings["KJJC_ID"];
        this.SFKS_ID = WebConfigurationManager.AppSettings["SFKS_ID"];
        this.SCKC_ID = WebConfigurationManager.AppSettings["SCKC_ID"];
        this.GLKJ_ID = WebConfigurationManager.AppSettings["GLKJ_ID"];
        this.CWZX_ID = WebConfigurationManager.AppSettings["CWZX_ID"];
        this.GLZX_ID = WebConfigurationManager.AppSettings["GLZX_ID"];
        this.SWKJ_ID = WebConfigurationManager.AppSettings["SWKJ_ID"];
        this.WEID_ID = WebConfigurationManager.AppSettings["WEID_ID"];
        //底部3个帮助中心入口
        this.BZZX_CategoryID = WebConfigurationManager.AppSettings["BZZX_CategoryID"];//帮助中心
        this.KFSH_CategoryID = WebConfigurationManager.AppSettings["KFSH_CategoryID"];//客服&售后
        this.SWHZ_CategoryID = WebConfigurationManager.AppSettings["SWHZ_CategoryID"];//商务合作
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