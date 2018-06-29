<%@ WebHandler Language="C#" Class="OrderList" %>

using System;
using System.Web;
using StarTech;
using System.Data;
using StarTech.DBUtility;

public class OrderList : IHttpHandler {

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
        string rows = context.Request["rows"] == null ? "10" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["rows"], Int32.MaxValue);     //显示数量
        string page = context.Request["page"] == null ? "1" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["page"], Int32.MaxValue);      //当前页
        string sidx = context.Request["sidx"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["sidx"], Int32.MaxValue);       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["sord"], Int32.MaxValue);   //排序规则
        string statu = context.Request["statu"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["statu"], Int32.MaxValue);   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, "10", sidx, sord, searchfilter, statu));
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
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string statu)
    {
        string table = " T_Order_Info";
        string fields = "orderId,OrderType,memberName,ReceivePerson,orderTime,orderAllMoney,'任务列表' as goodsList,MemberOrderRemarks,'操作' as cmd_col ";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        //if (categoryId != "")
        //{
        //    filter += " and categoryId in (select categoryId from T_Info_Category where categoryPath like '%" + categoryId + "%') ";
        //}
        if (statu == "complete")
        {
            //完成
            filter += " and isComplete=1 ";
        }
        if (statu == "pay")
        {
            //已支付
            filter += " and ispay=1 and issend=0 ";
        }
        if (statu == "nopay")
        {
            //未支付
            filter += " and ispay=0 ";
        }
        if (statu == "send")
        {
            //已发货
            filter += " and issend=1 and isComplete=0 ";
        }
        
        
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords =0; //bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by ordertime desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Replace("'任务列表' as ", "").Split(','), "OrderId");
    }


    public void EditDataSource(ref DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            dr["goodsList"] = GetGoodsInfoByOrderId(dr["orderid"].ToString());
            dr["OrderType"] = dr["OrderType"].ToString() == "QB" ? "课程" : "课程";

        }
    }

    public string GetMarketById(string marketId)
    {
        object obj = adoHelper.ExecuteSqlScalar("select Market_name from T_Base_Market where  Market_id='" + marketId + "'");
        return obj == null ? "" : obj.ToString();
    }

    public string GetGoodsInfoByOrderId(string orderId)
    {
        string strSQL = "select goodsName from T_order_infoDetail where orderid='" + orderId + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL.ToString());
        string strGoods = "";
        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        {
            if (j != 0)
                strGoods += ",";
            strGoods += ds.Tables[0].Rows[j]["goodsName"].ToString();
        }
        return strGoods;
    }

    public string GetFilter(string searchfilter)
    {
        System.Collections.Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("OrderId") && hTable["OrderId"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["OrderId"].ToString().Trim(), Int32.MaxValue);
                filter += " and OrderId like '%" + SafeStr + "%'";
            }
            if (hTable.Contains("txtDateBegin") && hTable["txtDateBegin"].ToString().Trim() != "") { filter += " and OrderTime >= '" + hTable["txtDateBegin"].ToString() + "'"; }
            if (hTable.Contains("txtDateEnd") && hTable["txtDateEnd"].ToString().Trim() != "") { filter += " and OrderTime < '" + DateTime.Parse(hTable["txtDateEnd"].ToString()).AddDays(1) + "'"; }

            if (hTable.Contains("ReceivePerson") && hTable["ReceivePerson"].ToString().Trim() != "") { filter += " and ReceivePerson like '%" + hTable["ReceivePerson"].ToString().Trim() + "%'"; }

        }
        return filter;
    }
    
    
    //public 
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}