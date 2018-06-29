<%@ WebHandler Language="C#" Class="NewsList" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using System.Collections;

public class NewsList : IHttpHandler
{

    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string type = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
         type = context.Request.QueryString["type"] == null ? "0" : context.Request.QueryString["type"];
        string id = context.Request.QueryString["id"] == null ? "0" : context.Request.QueryString["id"];
        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : context.Request["rows"];     //显示数量
        string page = context.Request["page"] == null ? "1" : context.Request["page"];      //当前页
        string sidx = context.Request["sidx"] == null ? "" : context.Request["sidx"];       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : context.Request["sord"];   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, type, searchfilter));
        }
        else if (flag == "delete")
        {
            context.Response.Write(DeleteData(id));
        }
    }

    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string type, string searchFilter)
    {
        string table = "(select ID,TBBH,TBCY,TBBT,FillTime,type,ViewCount,(select sum(ViewCount) from WTO_YJTB where type=" + type + " ) as sumcnt,ProTypeName=(select typeName from t_report_keywords b where b.sysnumber = a.protypeName  ) from WTO_YJTB a)c";
        string fields = "ID,TBBH,TBCY,TBBT,FillTime,ProTypeName,ViewCount,sumcnt,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter, type);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        //EditDataSource(ref dtSource);

        for (int i = 0; i < dtSource.Rows.Count; i++)
        {
            if (dtSource.Rows[i]["ViewCount"].ToString() == "")
            {
                dtSource.Rows[i]["ViewCount"] = 0;
            }
            if (dtSource.Rows[i]["sumcnt"].ToString() == "")
            {
                dtSource.Rows[i]["sumcnt"] = 0;
            }
        }
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "ID");
    }

    public string GetFilter(string searchfilter, string type)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 and type=" + type;
        if (hTable != null && hTable.Count > 0)
        {
            if (hTable["ProTypeName"].ToString().Replace('-', ' ').Trim() != "请选择")
            {
                if (hTable.Contains("ProTypeName") && hTable["ProTypeName"].ToString().Trim() != "") { filter += " and ProTypeName like '%" + hTable["ProTypeName"].ToString().Trim() + "%'"; }

            }
        }
        return filter;
    }

    //public void EditDataSource(ref DataTable dt)
    //{

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        string s = "";
    //        if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("NongY"))
    //        {
    //            s += "农业;";
    //        }
    //        if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("GongY"))
    //        {
    //            s += "工业;";
    //        }
    //        if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("FuWY"))
    //        {
    //            s += "服务业;";
    //        }
    //        if (dr["ShareToPlatform"] != null && dr["ShareToPlatform"].ToString().Contains("SheHGG"))
    //        {
    //            s += "社会公共管理;";
    //        }
    //        dr["ShareToPlatform"] = s;
    //    }
    //}


    /// <summary>
    /// 删除数据(多个编号用逗号(|)隔开)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteData(string ids)
    {
        if (ids.IndexOf("|") < 0)
        {
            int i = ado.ExecuteSqlNonQuery("delete from WTO_YJTB where ID in('" + ids.Replace(",", "','") + "')");
            return i.ToString();
        }
        else
        {
            string[] idList = ids.Split(new char[] { '|' });

            for (int i = 0; i < idList.Length - 1; i++)
            {
                if (ids[i].ToString() != "")
                {
                    string id = idList[i].ToString();
                    int res = ado.ExecuteSqlNonQuery("delete from WTO_YJTB where ID='" + id + "'");
                }
            }
            return "true";
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