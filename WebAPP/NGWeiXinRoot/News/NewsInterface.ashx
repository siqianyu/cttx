<%@ WebHandler Language="C#" Class="NewsInterface" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Data;
using StarTech.DBUtility;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web.DynamicData;

/// <summary>
/// 基于session验证类，防止越权访问和读取订单、视频等数据
/// </summary>
/// 
public class NewsInterface : IHttpHandler
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = Common.NullToEmpty(context.Request["flag"]);
        int CategoryId = Common.NullToZero(context.Request["categoryId"]);
        int noNewsID = Common.NullToZero(context.Request["nonewsid"]);
        string keyWord = context.Server.UrlDecode(Common.NullToEmpty(context.Request["k"]));
        int topNum = Common.NullToZero(context.Request["topnum"]);
        
        switch (flag.ToLower())
        {
            case "list_news":
                context.Response.Write(BindList(CategoryId, keyWord, noNewsID, topNum));
                break;
        }
    }

    protected string BindList(int CategoryId, string keyWord, int noNewsID,int topNum)
    {
        string html = "";
        string topInfo = topNum > 0 ? " top " + topNum + " " : "";
        string sql = "select " + topInfo + " * from T_News where [Approved]=1 ";
        if (CategoryId > 0) { sql += " and CategoryId=" + CategoryId + " "; }
        if (keyWord.Trim() != "") { sql += " and Title like '%" + keyWord.Trim() + "%'"; }
        if (noNewsID > 0) { sql += " and NewsID!=" + noNewsID + " "; }
        sql += " order by [ReleaseDate] desc";
        DataTable dt = ado.ExecuteSqlDataset(sql).Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            string sampleDesc = PubFunction.CheckStr(row["Body"].ToString());
            sampleDesc = sampleDesc.Replace("	", "").Replace("　", "");
            sampleDesc = sampleDesc.Length > 40 ? sampleDesc.Substring(0, 40) : sampleDesc;
            string ImgLink = "../Images/yqxkj.png";
            if (row["ImgLink"].ToString() != "") { ImgLink = row["ImgLink"].ToString(); }
            html += "<a href=\"NewsDetail.aspx?dirid=" + row["CategoryId"].ToString() + "&id=" + row["NewsID"].ToString() + "\" class=\"weui-media-box weui-media-box_appmsg\">";
            html += "<div class=\"weui-media-box__hd\">";
            html += "<img class=\"weui-media-box__thumb\" width=\"120\" height=\"100\" src=\"" + ImgLink + "\" >";
            html += "</div>";
            html += "<div class=\"weui-media-box__bd\">";
            html += "<h4 class=\"weui-media-box__title\" style=\"white-space:normal\">" + row["Title"] + "</h4>";
            html += "<p class=\"weui-media-box__desc\" style=\"white-space:nowrap\">" + sampleDesc + "</p>";
            html += "</div>";
            html += "</a>";
        }
        return html;
    }
    
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}