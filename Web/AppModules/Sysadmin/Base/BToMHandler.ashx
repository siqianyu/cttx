<%@ WebHandler Language="C#" Class="BToMHandler" %>

using System;
using System.Web;
using System.Globalization;
using StarTech.DBUtility;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using StarTech.ELife.Base;
using Startech.Category;
using System.Text;
using System.Web.UI;
using StarTech;

public class BToMHandler : IHttpHandler
{

    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    BuildingToMarketBll bll = new BuildingToMarketBll();
    BuildingToMarketModel model = new BuildingToMarketModel();
    BuildingBll buildingBll = new BuildingBll();
    BuildingModel buildingModel = new BuildingModel();
    MarketBll marketBll = new MarketBll();
    MarketModel marketModel = new MarketModel();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["flag"].ToLower(), Int32.MaxValue);
        string id = context.Request["id"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["id"].ToLower(), Int32.MaxValue);
        string lang = context.Request["lang"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["lang"].ToLower(), Int32.MaxValue);
        string BuildingsId = context.Request["buildingsId"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["buildingsId"].ToLower(), Int32.MaxValue);

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
            context.Response.Write(DeleteData(id, context));
        }
        else if (flag == "approve")
        {
            string approveResult = context.Request.QueryString["result"] == null ? "0" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["result"], Int32.MaxValue);
            //context.Response.Write(Approve(id, approveResult));
        }
        else if (flag == "download")
        {
            context.Response.Write(showFile(id, lang, context));
        }
        else if (flag == "isexist")
        {
            context.Response.Write(IdIsExist(id, lang, context));
        }
        else if (flag == "sbn")
        {
            context.Response.Write(SelectBuildingsName(BuildingsId));
        }
        else
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
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
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        //string table = " V_QikanInfo_Category";
        string fields = "BuildingToMarket_id,BuildingName,MarketName,orderby";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by orderby asc "; }
        DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        //DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "orderby");
    }

    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("BuildingName") && hTable["BuildingName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["BuildingName"].ToString().Trim(), Int32.MaxValue);
                string buildingsId = string.Empty;
                DataTable dt = buildingBll.GetList(" Building_name like '%" + SafeStr + "%' ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        buildingsId += "'" + dr["Building_id"].ToString() + "'" + ",";
                    }
                    filter += " and Building_id in (" + buildingsId.Remove(buildingsId.LastIndexOf(",")) + ") ";
                }
                else
                {
                    filter += " and Building_id = '" + SafeStr + "' ";
                }
            }
            if (hTable.Contains("MarketName") && hTable["MarketName"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["MarketName"].ToString().Trim(), Int32.MaxValue);
                string MarketsId = string.Empty;
                DataTable dt = marketBll.GetList(" Market_name like '%" + SafeStr + "%' ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MarketsId += "'" + dr["Market_id"].ToString() + "'" + ",";
                    }
                    filter += " and Market_id in (" + MarketsId.Remove(MarketsId.LastIndexOf(",")) + ") ";
                }
                else
                {
                    filter += " and Market_id = '" + SafeStr + "' ";
                }
            }
            
            //if (hTable.Contains("Author") && hTable["Author"].ToString().Trim() != "")
            //{
            //    SafeStr = KillSqlIn.Form_ReplaceByString(hTable["Author"].ToString().Trim(), Int32.MaxValue);
            //    filter += " and EDITOR like '%" + SafeStr + "%'";
            //}
            //if (hTable.Contains("Qikan") && hTable["Qikan"].ToString().Trim() != "")
            //{
            //    SafeStr = KillSqlIn.Form_ReplaceByString(hTable["Qikan"].ToString().Trim(), Int32.MaxValue);
            //    filter += " and (PARENT = '" + SafeStr + "' or IDX = '" + SafeStr + "')";
            //}
            //if (hTable.Contains("ddlApproved") && hTable["ddlApproved"].ToString().Trim() != "")
            //{
            //    SafeStr = KillSqlIn.Form_ReplaceByString(hTable["ddlApproved"].ToString().Trim(), Int32.MaxValue);
            //    filter += " and Approved = '" + SafeStr + "'";
            //}
            //if (hTable.Contains("AddDateBegin") && hTable["AddDateBegin"].ToString().Trim() != "")
            //{
            //    SafeStr = KillSqlIn.Form_ReplaceByString(hTable["AddDateBegin"].ToString().Trim(), Int32.MaxValue);
            //    filter += " and AddDate >= '" + SafeStr + "'";
            //}
            //if (hTable.Contains("AddDateEnd") && hTable["AddDateEnd"].ToString().Trim() != "")
            //{
            //    SafeStr = KillSqlIn.Form_ReplaceByString(hTable["AddDateEnd"].ToString().Trim(), Int32.MaxValue);
            //    filter += " and AddDate <= '" + Convert.ToDateTime(SafeStr).AddDays(1).Date.ToString() + "' ";
            //}
        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {
        dt.Columns.Add("BuildingName");
        foreach (DataRow dr in dt.Rows)
        {
            dr["BuildingName"] = dr["Building_id"].ToString().Equals("0") ? "无" : buildingBll.GetModel(dr["Building_id"].ToString()).Building_name;
        }
        dt.Columns.Add("MarketName");
        foreach (DataRow dr in dt.Rows)
        {
            dr["MarketName"] = dr["Market_id"].ToString().Equals("0") ? "无" : marketBll.GetModel(dr["Market_id"].ToString()).Market_name;
        }
    }


    /// <summary>
    /// 删除数据(多个编号用逗号(|)隔开)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteData(string ids, HttpContext context)
    {
        string result = string.Empty;
        if (ids.IndexOf("|") < 0)
        {
            result = bll.Delete(ids) ? "true" : "false";
        }
        else
        {
            string[] idList = ids.Split(new char[] { '|' });

            for (int i = 0; i < idList.Length - 1; i++)
            {
                if (ids[i].ToString() != "")
                {
                    result = bll.Delete(idList[i]) ? "true" : "false";
                }
            }
        }
        return result;
    }


    public bool DeleteFile(string fileUrl, HttpContext context)
    {
        bool flag = false;
        string Url = context.Request.MapPath("~" + fileUrl);
        if (File.Exists(Url))
        {
            File.Delete(Url);
            flag = true;
        }
        return flag;
    }

    
    /// <summary>
    /// 审核数据(多个编号用逗号(|)隔开)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    //public string Approve(string ids,string result)
    //{
    //    NewsBLL bll = new NewsBLL();
    //    string s="";
    //    if (ids.IndexOf("|") < 0)
    //    {
    //        if (result == "1")
    //        {
    //            s = bll.ApproveArticleAll(ids) > 0 ? "审核已通过！" : "审核未通过！"; //adoHelper.ExecuteSqlNonQuery("delete from T_News where NewsID in('" + ids.Replace(",", "','") + "')");
    //        }
    //        else
    //        {
    //            s = bll.ApproveCellArticleAll(ids) > 0 ? "取消审核成功！" : "取消审核失败！";
    //        }
    //    }
    //    else
    //    {
    //        string[] idList = ids.Split(new char[] { '|' });

    //        for (int i = 0; i < idList.Length - 1; i++)
    //        {
    //            if (ids[i].ToString() != "")
    //            {
    //                int id = Convert.ToInt32(idList[i].ToString());

    //                if (result == "1")
    //                {
    //                    s = bll.ApproveArticle(id) > 0 ? "审核已通过！" : "审核未通过！"; //adoHelper.ExecuteSqlNonQuery("delete from T_News where NewsID in('" + ids.Replace(",", "','") + "')");
    //                }
    //                else
    //                {
    //                    s = bll.ApproveCellArticle(id) > 0 ? "取消审核成功！" : "取消审核失败！";
    //                }
    //            }
    //        }
    //    }
    //    return s;
    //}


    #region 判断关系是否存在
    public string IdIsExist(string id, string lang, HttpContext context)
    {
        string result = string.Empty;
        if (!string.IsNullOrEmpty(id))
        {
            model = bll.GetModel(id);
            if (model == null)
            {
                result = "false";
            }
            else
            {
                result = "true";
            }
        }
        return result;
    }
    #endregion

    #region 查询多个小区/大厦名称
    public string SelectBuildingsName(string ids)
    {
        string result = string.Empty;
        if (!string.IsNullOrEmpty(ids))
        {
            string[] id = ids.Split('|');
            foreach (string s in id)
            {
                result += buildingBll.GetModel(s).Building_name + ",";
            }
        }
        return result.Remove(result.LastIndexOf(","));
    }
    #endregion

    public string showFile(string standardid, string type, HttpContext context)
    {
        // HttpContext context = null;
        string StdPath = System.Configuration.ConfigurationManager.AppSettings["StdPath"].ToString();
        bool flag = true;
        string stdid = standardid;

        string fileName = "";
        string FilePath = "";
        try
        {
            string sql = "select * from T_DownFile where StardardID='" + standardid + "'";
            DataRow dr = adoHelper.ExecuteSqlDataset(sql).Tables[0].Rows[0];
            if (type == "C")
            {
                fileName = dr["SourceFilename"].ToString();
                FilePath = dr["FilePath"].ToString().Replace("~/upload/Standard/", "");
            }
            else if (type == "E")
            {
                fileName = dr["EngSourceFilename"].ToString();
                FilePath = dr["EngFilePath"].ToString().Replace("~/upload/Standard/", "");
            }


            FileStream fs = new FileStream(StdPath + FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            context.Response.ContentType = "application/octet-stream;";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            context.Response.AddHeader("Content-Length", fs.Length.ToString());
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            context.Response.BinaryWrite(buffer);
            return "";
        }
        catch (Exception exc)
        {
            flag = false;
            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.ContentType = "text/html";
            context.Response.Write("<script>alert('文件不存在!');history.back()</script>");
            return "";
        }
        if (flag)
        {
            context.Response.End();
        }
    }
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}