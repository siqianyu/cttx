<%@ WebHandler Language="C#" Class="Menu" %>

using System;
using System.Web;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;
using StarTech.ELife.Menu;
using System.Collections;
using System.Web.SessionState;

public class Menu : IHttpHandler, IRequiresSessionState{

    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    MenuBll bll = new MenuBll();
    MenuModel model = new MenuModel();
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
        string page = context.Request["page"] == null ? "1" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["page"], Int32.MaxValue);      //当前页
        string sidx = context.Request["sidx"] == null ? "addTime" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["sidx"], Int32.MaxValue);       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["sord"], Int32.MaxValue);   //排序规则
        string categoryId = context.Request["categoryId"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["categoryId"], Int32.MaxValue);       //排序字段

        if (flag == "list")
        {
            string rows = context.Request["rows"] == null ? "15" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["rows"], Int32.MaxValue);     //显示数量

            context.Response.Write(List(page, rows, sidx, sord, searchfilter, categoryId));
        }
        else if (flag == "delete")
        {
            context.Response.Write(Delete(id));
        }
        else if (flag == "ilist")
        {
            string rows = context.Request["rows"] == null ? "5" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["rows"], Int32.MaxValue);     //显示数量

            context.Response.Write(IList(page, rows, sidx, sord, searchfilter));

        }
        else if (flag == "idelete") 
        {
            context.Response.Write(IDelete(id));
        }
    }

    /// <summary>
    /// 删除菜谱
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string Delete(string menuId)
    {
        string gid = KillSqlIn.Form_ReplaceByString(menuId, 20);
        string strSQL = "select menuName from T_Menu_Info where menuId='"+menuId+"';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            return "false";
        string menuName=ds.Tables[0].Rows[0][0].ToString();

        strSQL = "delete T_Menu_info where menuId='" + gid + "';";
        int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "删除菜谱《" + menuName + "》;", "删除", "", "", HttpContext.Current.Request.Url.ToString());
            return "true";
        }

        return "false";
    }

    /// <summary>
    /// 删除食材
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string IDelete(string menuId)
    {
        string gid = KillSqlIn.Form_ReplaceByString(menuId, 20);
        string strSQL = "delete T_Menu_item where itemId='" + gid + "';";
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
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string categoryId)
    {
        string table = " T_Menu_Info";
        string fields = "menuId,imgSrc,menuName,Technology,Flavor,CookingTime,Calorie,UserId,AddTime,isShow,orderby, '操作' as cmd_col ";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        if (categoryId != "")
        {
            filter += " and categoryId in (select categoryId from T_Menu_Category where categoryPath like '%" + categoryId + "%') ";
        }
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; //bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by addtime asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "MenuId");
    }

    /// <summary>
    /// 食材列表检索
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    public string IList(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = " (select itemId*1 as itemId,itemImgSrc,itemName,itemType,ifBuy,remark,orderBy,unit,GoodsId from T_Menu_Item) v";
        string fields = "itemId,itemImgSrc,itemName,itemType,ifBuy,remark,orderBy,unit,goodsId,'规格' as goodsFormate, '操作' as cmd_col ";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter2(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; //bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by orderby asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource2(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Replace("'规格' as ", "").Split(','), "itemId");
    }
    
    
    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("menuName") && hTable["menuName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["menuName"].ToString().Trim(), Int32.MaxValue);
                filter += " and menuName like '%" + SafeStr + "%'";
            }

            
        }
        return filter;
    }

    public string GetFilter2(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("itemName") && hTable["itemName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["itemName"].ToString().Trim(), Int32.MaxValue);
                filter += " and itemName like '%" + SafeStr + "%'";
            }
            if (hTable.Contains("ifbuy") && hTable["ifbuy"].ToString().Trim() != "" && hTable["ifbuy"].ToString().Trim() != "-1")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["ifbuy"].ToString().Trim(), Int32.MaxValue);
                filter += " and ifbuy=" + SafeStr + "";
            }
        }
        return filter;
    }
    public void EditDataSource(ref DataTable dt)
    {
        
    }
    
    public void EditDataSource2(ref DataTable dt)
    {
        //return;
        string strSQL = "";
        foreach (DataRow dr in dt.Rows)
        {
            strSQL += "select GoodsFormateNames,GoodsFormateValues,GoodsCode from T_Goods_Formate where GoodsId='" + dr["goodsId"] + "' and GoodsFormateNames is not null and GoodsFormateNames<>'';";
        }
        if (strSQL == "")
            return;
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL.ToString());


        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if (ds.Tables[i].Rows.Count > 0)
            {
                //dt.Rows[i]["goodsFormate"] = ds.Tables[i].Rows[0][0];
                string strFormate = "";

                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    //if (j != 0)
                    //    strFormate += "|";
                    string f1 = ds.Tables[i].Rows[j][0].ToString();
                    string f2 = ds.Tables[i].Rows[j][1].ToString();
                    for (int k = 0; k < f1.Split(',').Length; k++)
                    {
                        if (k != 0)
                            strFormate += ",";
                        else
                        {
                            string strCheck = "";
                            if (j == 0)
                                strCheck = "checked='checked'";
                            strFormate += "<span><input type='radio' code='"+ds.Tables[i].Rows[j]["GoodsCode"]+"' class='goodsf gf" + dt.Rows[i]["itemId"] + "' " + strCheck + "  name='gf" + dt.Rows[i]["GoodsId"] + "'/>";
                        }
                        strFormate += f1.Split(',')[k] + ":" + f2.Split(',')[k];
                        if (k == f1.Split(',').Length - 1)
                        {
                            strFormate += "</span><br/>";
                        }
                    }
                    //strFormate = "<span><input type='radio' class='goodsf'  name='gf" + ds.Tables[i].Rows[j]["GoodsCode"] + "'/>" + strFormate+"<br/>";
                }
                dt.Rows[i]["goodsFormate"] = ""+strFormate;
            }
            else
            {
                dt.Rows[i]["goodsFormate"] = "";
            }
        }
        
        ////foreach (DataRow dr in dt.Rows)
        ////{
        ////    dr["DisplayModeName"] = dr["DisplayMode"].ToString() == "1" ? "文字" : "图片";
        ////    dr["CategoryName"] = dr["CategoryId"].ToString() == "1004" ? "<font color='green'>首页幻灯片</font>" : dr["CategoryId"].ToString() == "1001" ? "<font color='green'>首页横幅广告1</font>" : dr["CategoryId"].ToString() == "1002" ? "<font color='green'>首页横幅广告2</font>" : dr["CategoryId"].ToString() == "1003" ? "<font color='green'>首页横幅广告3</font>" : dr["CategoryId"].ToString() == "1005" ? "<font color='green'>右幻灯片1</font>" : "<font color='green'>右幻灯片2</font>";
        ////}
    }
   
    
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}