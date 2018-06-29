<%@ WebHandler Language="C#" Class="PropertySetHandler" %>

using System;
using System.Web;
using Startech.Utils;
using System.Collections;
using System.Data;

public class PropertySetHandler : IHttpHandler {

    StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
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
    }


    /// <summary>
    /// 删除任务
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string Delete(string propertyId)
    {
        string gid = KillSqlIn.Form_ReplaceByString(propertyId, 20);
        string strSQL = "delete T_Goods_MorePropertySet where propertyId='" + propertyId + "';";
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
        string table = " T_Goods_MorePropertySet";
        string fields = "propertyId,propertyName,propertyOptions,porpertyFlag,orderby, '操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;// bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by sort asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        //EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "propertyId");
    }


    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("propertyName") && hTable["propertyName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["propertyName"].ToString().Trim(), Int32.MaxValue);
                filter += " and propertyName like '%" + SafeStr + "%'";
            }
            
        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {
        //dt.Columns.Add("DisplayModeName");
        
        //dt.Columns.Add("CategoryName");
        //string strSQL = "";
        //foreach (DataRow dr in dt.Rows)
        //{
        //    strSQL += "select categoryName from T_Info_Category where Categoryid='" + dr["categoryid"] + "';";
        //}
        //DataSet ds = adoHelper.ExecuteSqlDataset(strSQL.ToString());
        //int i = 0;
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    dt.Rows[i]["CategoryName"] = dr[0];
        //}
        //foreach (DataRow dr in dt.Rows)
        //{
        //    dr["DisplayModeName"] = dr["DisplayMode"].ToString() == "1" ? "文字" : "图片";
        //    dr["CategoryName"] = dr["CategoryId"].ToString() == "1004" ? "<font color='green'>首页幻灯片</font>" : dr["CategoryId"].ToString() == "1001" ? "<font color='green'>首页横幅广告1</font>" : dr["CategoryId"].ToString() == "1002" ? "<font color='green'>首页横幅广告2</font>" : dr["CategoryId"].ToString() == "1003" ? "<font color='green'>首页横幅广告3</font>" : dr["CategoryId"].ToString() == "1005" ? "<font color='green'>右幻灯片1</font>" : "<font color='green'>右幻灯片2</font>";
        //}
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}