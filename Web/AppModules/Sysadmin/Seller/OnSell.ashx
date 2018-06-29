<%@ WebHandler Language="C#" Class="PostmanHandler" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using StarTech.DBUtility;
using Startech.Member;
using System.Text;
using StarTech;
using System.Configuration;
using System.Data.SqlClient;

public class PostmanHandler : IHttpHandler
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string mainPath = ConfigurationManager.AppSettings["Source_NewsPic"];
    string type = string.Empty, callback = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        // context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/javascript";

        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["id"].ToLower(), 10);
        string marketid = context.Request["marketid"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByNumber(context.Request["marketid"], 10));

        type = context.Request["type"] == null ? "" : context.Request["type"].ToLower();
        callback = context.Request["callback"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["callback"], 20); //用于跨域调用
        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : context.Request["rows"];     //显示数量
        string page = context.Request["page"] == null ? "1" : context.Request["page"];      //当前页
        string sidx = context.Request["sidx"] == null ? "ShopId" : context.Request["sidx"];       //排序字段
        string sord = context.Request["sord"] == null ? "" : context.Request["sord"];   //排序规则




        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }


    }

    /// <summary>
    /// 获取商家列表
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "(select (select top 1 Area_id from T_Base_Market where Market_id=u.MarketId) as areaid, (select top 1 area_name from  dbo.T_Base_Area r,T_Base_Market m where r.area_id=m.Area_id and m.Market_id=u.MarketId) as areaname,(select top 1 Market_name from  dbo.T_Base_Market where Market_id=u.MarketId) as marketname,u.ShopName,(select top 1 GoodsName from  T_Goods_Info where GoodsCode=g.goodsCode) as GoodsName,g.*,u.MarketId from T_Shop_Goods g,T_Shop_User u where g.shopid=u.ShopId )v ";
        string fields = " shopgoods_id,areaname,marketname,shopname,goodscode,goodsname,shopgoods_amount,shopgoods_selfprice,shopgoods_addtime, '操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "shopgoods_id");
    }



    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <param name="searchfilter"></param>
    /// <returns></returns>
    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";

        if (hTable != null && hTable.Count > 0)
        {

            if (hTable.Contains("Areaid") && hTable["Areaid"].ToString().Trim() != "" && hTable["Areaid"].ToString().Trim() != "0" && hTable["Areaid"].ToString().Trim().ToLower() != "null")
            {
                filter += " and Areaid = '" + hTable["Areaid"].ToString().Trim() + "'";
            }
            if (hTable.Contains("ShopName") && hTable["ShopName"].ToString().Trim() != "0" && hTable["ShopName"].ToString().Trim() != "")
            {
                filter += " and ShopName like '%" + hTable["ShopName"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("GoodsName") && hTable["GoodsName"].ToString().Trim() != "0" && hTable["GoodsName"].ToString().Trim() != "")
            {
                filter += " and goodsname like '%" + hTable["GoodsName"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("Marketid") && hTable["Marketid"].ToString().Trim() != "0" && hTable["Marketid"].ToString().Trim() != "" && hTable["Marketid"].ToString().Trim().ToLower() != "null")
            {
                filter += " and Marketid = '" + hTable["Marketid"].ToString().Trim() + "'";
            }


        }
        return filter;
    }

    /// <summary>
    /// 编辑数据源
    /// </summary>
    /// <param name="dt"></param>
    private void EditDataSource(ref DataTable dt)
    {


    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}