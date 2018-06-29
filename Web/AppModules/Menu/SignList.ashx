<%@ WebHandler Language="C#" Class="SignList" %>

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


public class SignList : IHttpHandler {

    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest (HttpContext context) {
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

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "delete")
        {
            context.Response.Write(Delete(id));
        }
        else if (flag == "bindlist")
        {
            context.Response.Write(BList(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "binddelete")
        {
            context.Response.Write(BDelete(id));
        }
            
        
    }


    /// <summary>
    /// 删除标签
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string Delete(string goodsid)
    {
        string gid = KillSqlIn.Form_ReplaceByString(goodsid, 20);
        string strSQL = "delete T_Menu_Sign where signid='" + gid + "';";
        int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
            return "true";

        return "false";
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
        string table = " T_Menu_Sign ";
        string fields = "signId,signName,remark,addTime,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;// bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by addTime desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        //EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "SignId");
    }


    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("SignName") && hTable["SignName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["SignName"].ToString().Trim(), Int32.MaxValue);
                filter += " and SignName like '%" + SafeStr + "%'";
            }
        }
        return filter;
    }


    /// <summary>
    /// 删除标签绑定
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string BDelete(string goodsid)
    {
        string gid = KillSqlIn.Form_ReplaceByString(goodsid, 20);
        string strSQL = "delete T_Menu_SignBind where signid='" + gid + "';";
        int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
            return "true";

        return "false";
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
    public string BList(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = " T_Goods_Info ";
        string fields = "goodsId,goodsName,goodsCode,'标签内容' as sign,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter2(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;// bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by orderby desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Replace("'标签内容' as ","").Split(','), "goodsId");
    }

    public string GetFilter2(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            //if (hTable.Contains("SignName") && hTable["SignName"].ToString().Trim() != "")
            //{
            //    SafeStr = KillSqlIn.Form_ReplaceByString(hTable["SignName"].ToString().Trim(), Int32.MaxValue);
            //    filter += " and SignName like '%" + SafeStr + "%'";
            //}
            if (hTable.Contains("MenuName") && hTable["MenuName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["MenuName"].ToString().Trim(), Int32.MaxValue);
                filter += " and MenuName like '%" + SafeStr + "%'";
            }
        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {

        string strSQL = "";
        foreach (DataRow dr in dt.Rows)
        {
            strSQL += "select * from T_Menu_Sign where signid in (select signid from T_Menu_SignBind where goodsid='" + dr["goodsid"] + "');";
        }
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL.ToString());

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            string str="";
            for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
            {
                if (j != 0)
                    str += ",";
                str += ds.Tables[i].Rows[j]["signname"].ToString();
            }
            if (ds.Tables[i].Rows.Count > 0)
            {
                dt.Rows[i]["sign"] = ds.Tables[i].Rows[0][0];
            }
            else
            {
                dt.Rows[i]["sign"] = "";
            }
        }

    }
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}