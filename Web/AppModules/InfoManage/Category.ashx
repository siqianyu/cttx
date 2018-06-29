<%@ WebHandler Language="C#" Class="Category" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using System.Collections;

 
public class Category : IHttpHandler 
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();

        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : context.Request["rows"];     //显示数量
        string page = context.Request["page"] == null ? "1" : context.Request["page"];      //当前页
        string sidx = context.Request["sidx"] == null ? "" : context.Request["sidx"];       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : context.Request["sord"];   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }

        else
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
    }



    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "T_Category";
        string fields = "CategoryId";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        //EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Split(','), "CategoryId");
    }

    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        //if (hTable != null && hTable.Count > 0)
        //{
        //    if (hTable.Contains("Title") && hTable["Title"].ToString().Trim() != "") { filter += " and Title like '%" + hTable["Title"].ToString().Trim() + "%'"; }
        //    if (hTable.Contains("IsState") && hTable["IsState"].ToString().Trim() != "") { filter += " and IsState=" + hTable["IsState"].ToString().Trim() + ""; }
        //}
        return filter;
    }
    
    
    public bool IsReusable
    {
        get 
        {
            return false;
        }
    }

}