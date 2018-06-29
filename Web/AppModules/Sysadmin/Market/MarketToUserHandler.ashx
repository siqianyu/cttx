<%@ WebHandler Language="C#" Class="List1Handler" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using StarTech;



public class List1Handler : IHttpHandler
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["flag"].ToLower(), 8);
        string id = context.Request["id"] == null ? "" : context.Request["id"].ToLower();


        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : KillSqlIn.Url_ReplaceByNumber(context.Request["rows"], 5);     //显示数量
        string page = context.Request["page"] == null ? "1" : KillSqlIn.Url_ReplaceByNumber(context.Request["page"], 5);      //当前页
        string sidx = context.Request["sidx"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["sidx"], 10);       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : KillSqlIn.Url_ReplaceByString(context.Request["sord"], 20);   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "delete")
        {
            context.Response.Write(DeleteData(id));
        }

    }

    /// <summary>
    /// 列表检索
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "  V_Market_User_Config ";
        string fields = "id,area_name,market_id,market_name,truename,mobile,addtime,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "id");
    }

    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {

            if (hTable.Contains("marketname") && hTable["marketname"].ToString().Trim() != "") { filter += " and market_name like '%" + hTable["marketname"].ToString().Trim() + "%'"; }
            if (hTable.Contains("truename") && hTable["truename"].ToString().Trim() != "") { filter += " and truename like '%" + hTable["truename"].ToString().Trim() + "%'"; }
        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {

    }


    /// <summary>
    /// 删除数据(多个编号用逗号(|)隔开)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteData(string ids)
    {
        int res = 0;
        if (ids.IndexOf(",") < 0)
        {
            res = adoHelper.ExecuteSqlNonQuery("delete from T_Base_MarketToUser where id='" + ids + "'");

        }
        else
        {
            ids = ids.TrimEnd(',');
            res = adoHelper.ExecuteSqlNonQuery("delete from T_Base_MarketToUser where id in('" + ids.Replace(",", "','") + "')");

        }
        
        if (res > 0)
        {
            return "true";
        }
        else
        {
            return "false";
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}