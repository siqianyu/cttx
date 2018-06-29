<%@ WebHandler Language="C#" Class="NewsList" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using System.Collections;

public class NewsList : IHttpHandler
{

    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "0" : context.Request["id"].ToString();     //显示数量

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
        else if (flag == "topic")
        {
            context.Response.Write(GetTopic(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "deltopic")
        {
            context.Response.Write(DeleteTopic(id));
        }
        else
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
    }

    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "(select *,(select sum(viewcount) from V_NewsCategory ) as sumcnt from V_NewsCategory) as v";
        string fields = "NewsID,Title,CategoryName,Approved,ArticleType,ViewCount,sumcnt,ReleaseDate";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        //EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Split(','), "NewsID");
    }

    public string GetTopic(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "T_StandardTopic";
        string fields = "ID,Title,TopicContent,Url,ShareToPlatform,AddTime,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string sort = "order by AddTime";
        string filter = GetFilter(searchFilter);
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        //EditDataSource(ref dtSource);
        foreach (DataRow dr in dtSource.Rows)
        {
            string s = "";
            if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("NongY"))
            {
                s += "农业;";
            }
            if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("GongY"))
            {
                s += "工业;";
            }
            if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("FuWY"))
            {
                s += "服务业;";
            }
            if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("SheHGG"))
            {
                s += "社会公共管理;";
            }
            dr["ShareToPlatform"] = s;
        }
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "ID");
    }

    /// <summary>
    /// 删除先进标准数据(多个编号用逗号(|)隔开)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteTopic(string ids)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        if (ids.IndexOf("|") < 0)
        {
            int i = adoHelper.ExecuteSqlNonQuery("delete from T_StandardTopic where ID in('" + ids.Replace(",", "','") + "')");
            return i.ToString();
        }
        else
        {
            string[] idList = ids.Split(new char[] { '|' });

            for (int i = 0; i < idList.Length - 1; i++)
            {
                if (ids[i].ToString() != "")
                {
                    int id = Convert.ToInt32(idList[i].ToString());
                    int res = adoHelper.ExecuteSqlNonQuery("delete from T_StandardTopic where ID=" + id);
                }
            }
            return "true";
        }
    }

    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            if (hTable.Contains("Title") && hTable["Title"].ToString().Trim() != "") { filter += " and Title like '%" + hTable["Title"].ToString().Trim() + "%'"; }
            if (hTable.Contains("IsState") && hTable["IsState"].ToString().Trim() != "") { filter += " and IsState=" + hTable["IsState"].ToString().Trim() + ""; }
            if (hTable.Contains("CategoryId") && hTable["CategoryId"].ToString().Trim() != "") { filter += " and CategoryId=" + hTable["CategoryId"].ToString().Trim() + ""; }
        }
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