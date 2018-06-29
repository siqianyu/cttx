<%@ WebHandler Language="C#" Class="GoodsHandler" %>

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
using System.Web.SessionState;

public class GoodsHandler : IHttpHandler, IRequiresSessionState
{
    
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    StarTech.ELife.Goods.GoodsBll bll = new StarTech.ELife.Goods.GoodsBll();
    StarTech.ELife.Goods.GoodsModel model = new StarTech.ELife.Goods.GoodsModel();
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
        string categoryId = context.Request["categoryId"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["categoryId"], Int32.MaxValue);   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, "10", sidx, sord, searchfilter,categoryId));
        }
        else if (flag == "delete")
        {
            context.Response.Write(Delete(id));
        }
        else if (flag == "slide")
        {
            string goodsid = context.Request["goodsId"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["goodsId"], Int32.MaxValue);   //排序规则
            context.Response.Write(SlideList(goodsid));
        }
    }

    /// <summary>
    /// 获取幻灯片
    /// </summary>
    /// <param name="goodsList"></param>
    /// <returns></returns>
    public string SlideList(string goodsId)
    {
        string imgList = "";
        string strSQL = "select * from T_Goods_Pic where GoodsId='"+goodsId+"' order by orderby asc;";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 0)
            return "";
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count < 0)
            return "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            imgList += "<img src='"+ds.Tables[0].Rows[i]["picpath"]+"' width='100px' height='100px' style='margin:0px 2px'/>";
        }
        return imgList;
    }
    
    /// <summary>
    /// 删除任务
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string Delete(string goodsid)
    {
        string gid = KillSqlIn.Form_ReplaceByString(goodsid, 20);
        if (adoHelper.ExecuteSqlScalar("select GoodsName from T_Goods_Info where GoodsToTypeId='" + gid + "' ") != null) 
        {
            return "false";
        }
        string strSQL = "select GoodsName from T_Goods_info where goodsid='" + gid + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            return "false";
        string GoodsName = ds.Tables[0].Rows[0][0].ToString();
        strSQL = "delete T_Goods_info where goodsid='"+gid+"';";
        int row=adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "删除课程《" + GoodsName + "》;", "删除", "", "", HttpContext.Current.Request.Url.ToString());

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
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter,string categoryId)
    {
        string table = " (select a.*,(select count(1) from T_Goods_Info where GoodsToTypeId=a.GoodsId) PLCount from T_Goods_Info a) vv";
        string fields = "GoodsId,CategoryId,GoodsSmallPic,GoodsName,PLCount,SalePrice,IsSale,JobAddress,AddTime,JobStartTime,JobEndTime,orderby, '操作' as cmd_col, '分类名' as CategoryName,uint";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        filter += " and JobType='Goods'";
        if (categoryId != "")
        {
            filter += " and categoryId in (select categoryId from T_Info_Category where categoryPath like '%"+categoryId+"%') ";
        }
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by sort asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Replace("'分类名' as ","").Split(','), "GoodsId");
    }


    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("GoodsName") && hTable["GoodsName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["GoodsName"].ToString().Trim(), Int32.MaxValue);
                filter += " and GoodsName like '%" + SafeStr + "%'";
            }
            

        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {

            DataSet ds = adoHelper.ExecuteSqlDataset("select categoryName from T_Info_Category where Categoryid='" + dr["categoryid"] + "'");
            dr["CategoryName"] = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["categoryName"] : "";
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

    
}