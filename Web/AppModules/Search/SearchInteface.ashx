<%@ WebHandler Language="C#" Class="SearchInteface" %>

using System;
using System.Web;
using System.Globalization;
using StarTech.DBUtility;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using StarTech.ELife.Ad;
using Startech.Category;
using System.Text;
using System.Web.UI;
using StarTech;

public class SearchInteface : IHttpHandler
{

    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    AdBll bll = new AdBll();
    AdModel model = new AdModel();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["flag"].ToLower(), Int32.MaxValue);
        string id = context.Request["id"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["id"].ToLower(), Int32.MaxValue);
        string lang = context.Request["lang"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["lang"].ToLower(), Int32.MaxValue);
        string QikanId = context.Request["qikanId"] == null ? "0" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["qikanId"].ToLower(), Int32.MaxValue);

        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Server.UrlDecode(context.Request.QueryString["searchfilter"]), Int32.MaxValue);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["rows"], Int32.MaxValue);     //显示数量
        string page = context.Request["page"] == null ? "1" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["page"], Int32.MaxValue);      //当前页
        string sidx = context.Request["sidx"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["sidx"], Int32.MaxValue);       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["sord"], Int32.MaxValue);   //排序规则

        if (flag == "list_examination")
        {
            context.Response.Write(List_Examination(page, rows, sidx, sord, searchfilter));
        }
      

    }

    /// <summary>
    /// 答题情况统计
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    public string List_Examination(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = " View_Test_ErrorRecord_CountShow ";
        string fields = "MemberId,TrueName,Tel,MemberName,CourseId,GoodsName,CreateTime,TotalNum,TotalPass,'操作' as cmd_col ";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter_Examination(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; //bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by CreateTime desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource_Examination(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "MemberId");
    }

    public string GetFilter_Examination(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";

            if (hTable.Contains("TrueName") && hTable["TrueName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["TrueName"].ToString().Trim(), Int32.MaxValue);
                filter += " and TrueName like '%" + SafeStr + "%'";
            } 
            if (hTable.Contains("Tel") && hTable["Tel"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["Tel"].ToString().Trim(), Int32.MaxValue);
                filter += " and Tel like '%" + SafeStr + "%'";
            }
            if (hTable.Contains("GoodsName") && hTable["GoodsName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["GoodsName"].ToString().Trim(), Int32.MaxValue);
                filter += " and GoodsName like '%" + SafeStr + "%'";
            }
            
        }
        return filter;
    }

    public void EditDataSource_Examination(ref DataTable dt)
    {

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["TrueName"].ToString() == "") { dr["TrueName"] = "试用会员"; }
        }
    }

    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}