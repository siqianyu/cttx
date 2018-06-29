<%@ WebHandler Language="C#" Class="Service" %>

using System;
using System.Web;
using System.Data;
using StarTech.DBUtility;
using StarTech;
using System.Web.SessionState;

public class Service : IHttpHandler, IRequiresSessionState {

    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance"); 
    string userid = "";
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        if (!LogAdd.IsOnline(context, ref userid))
            return;

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
        
        if (flag == "list")
        {
            context.Response.Write(List(page, "10", sidx, sord, searchfilter));
        }
        else if (flag == "delete")
        {
            context.Response.Write(Delete(id));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    

    /// <summary>
    /// 删除服务
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string Delete(string serviceId)
    {
        serviceId = KillSqlIn.Form_ReplaceByString(serviceId, 50);
        string strSQL = "select serviceName from T_Goods_Service where serviceId='" + serviceId + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            return "false";
        string GoodsName = ds.Tables[0].Rows[0][0].ToString();
        
        strSQL = "delete T_Goods_Service where serviceid='" + serviceId + "';";
        strSQL += "delete T_Goods_ServiceDetail where serviceid='" + serviceId + "';";
        
        int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "删除任务服务《" + GoodsName + "》;", "删除", "", "", HttpContext.Current.Request.Url.ToString());

            return "true";
        }
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
        string table = " T_Goods_Service ";
        string fields = "serviceId,serviceName,serviceContext,'选项' as serviceValue,'操作' as cmd_col";
        //"GoodsId,CategoryId,GoodsSmallPic,GoodsName,GoodsCode,SalePrice,MarketPrice,AddTime,Sotck,isSale,orderby, '操作' as cmd_col, '分类名' as CategoryName,uint";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; //bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by orderBy asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Replace("'选项' as ", "").Split(','), "serviceId");
    }

    public string GetFilter(string searchfilter)
    {
        System.Collections.Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("serviceName") && hTable["serviceName"].ToString().Trim() != "")
            {
                SafeStr = Startech.Utils.KillSqlIn.Form_ReplaceByString(hTable["serviceName"].ToString().Trim(), Int32.MaxValue);
                filter += " and serviceName like '%" + SafeStr + "%'";
            }


        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {

        string strSQL = "";
        foreach (DataRow dr in dt.Rows)
        {
            strSQL += "select * from T_Goods_servicedetail where serviceId='" + dr["serviceId"] + "';";
        }
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL.ToString());

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if (ds.Tables[i].Rows.Count > 0)
            {
                string v="";
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    if(j!=0)
                        v+=",";
                   v+= ds.Tables[i].Rows[j]["value"];
                }
                dt.Rows[i]["serviceValue"] = v;
            }
            else
            {
                dt.Rows[i]["serviceValue"] = "";
            }
        }

    }
}