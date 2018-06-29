<%@ WebHandler Language="C#" Class="PayLog" %>

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
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections.Generic;

public class PayLog : IHttpHandler
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    AdBll bll = new AdBll();
    AdModel model = new AdModel();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["flag"].ToLower(), Int32.MaxValue);
        string id = context.Request["id"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["id"].ToLower(), Int32.MaxValue);
        string lang = context.Request["lang"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["lang"].ToLower(), Int32.MaxValue);
        string QikanId = context.Request["qikanId"] == null ? "0" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["qikanId"].ToLower(), Int32.MaxValue);
        string mid = context.Request["memberid"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["memberid"].ToLower(), Int32.MaxValue);
        string num = context.Request["num"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["num"].ToLower(), Int32.MaxValue);
        string val = context.Request["val"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["val"].ToLower(), Int32.MaxValue);
        
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
        else if (flag == "listgroup")
        {
            context.Response.Write(ListGroup(page, rows, sidx, sord, searchfilter, mid));
        }
        else if (flag == "approve")
        {
            //string approveResult = context.Request.QueryString["result"] == null ? "0" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["result"], Int32.MaxValue);
            //context.Response.Write(Approve(id, approveResult));
        }
        else if (flag == "download")
        {
          //  context.Response.Write(showFile(id, lang, context));
        }
        else if (flag == "togroup")
        {
            context.Response.Write(ToGroup(mid, id, int.Parse( num), int.Parse(val)));
        }
        else
        {
           // context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
    }


    /// <summary>
    /// 合并优惠券
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="couponId"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public string ToGroup(string memberId, string couponId, int num, int val)
    {
        DataTable dtBase = adoHelper.ExecuteSqlDataset("select * from T_Base_Coupon where CouponId='" + couponId + "'").Tables[0];
        double CouponValue = double.Parse(dtBase.Rows[0]["CouponValue"].ToString());

        double total = 0;
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_Member_Coupon where MemberId='" + memberId + "' and IsUsed=0 and CouponValue=" + val + "").Tables[0];
        foreach (DataRow row in dt.Rows)
        {
            total += double.Parse(row["CouponValue"].ToString());
        }
        if (CouponValue > total) { return "-1"; }

        int topTotalNum = (int)CouponValue / val * num;
        DataTable dtTopTotal = adoHelper.ExecuteSqlDataset("select top " + topTotalNum + " * from T_Member_Coupon where MemberId='" + memberId + "' and IsUsed=0 and CouponValue=" + val + " order by CouponId").Tables[0];
        if (dtTopTotal.Rows.Count == topTotalNum)
        {
            for (int i = 0; i < num; i++)
            {
                int topNum = (int)CouponValue / val;
                DataTable dtTop = adoHelper.ExecuteSqlDataset("select top " + topNum + " * from T_Member_Coupon where MemberId='" + memberId + "' and IsUsed=0 and CouponValue=" + val + " order by CouponId").Tables[0];
                if (dtTop.Rows.Count == topNum)
                {
                    //add
                    List<SqlParameter> plist = new List<SqlParameter>();
                    string guid = Guid.NewGuid().ToString();
                    string CouponId = memberId + DateTime.Now.ToString("yyMMddHHmmss") + i + "H";
                    plist.Add(new SqlParameter("@Sysnumber", guid));
                    plist.Add(new SqlParameter("@MemberId", memberId));
                    plist.Add(new SqlParameter("@CouponId", CouponId));
                    plist.Add(new SqlParameter("@CouponType", "抵用券"));
                    plist.Add(new SqlParameter("@CouponValue", dtBase.Rows[0]["CouponValue"]));
                    plist.Add(new SqlParameter("@StartTime", dtBase.Rows[0]["StartTime"]));
                    plist.Add(new SqlParameter("@EndTime", dtBase.Rows[0]["EndTime"]));
                    plist.Add(new SqlParameter("@IsUsed", "0"));
                    plist.Add(new SqlParameter("@Remark", dtBase.Rows[0]["Context"]));
                    plist.Add(new SqlParameter("@GetPlaceInfo", "合并抵用券"));
                    plist.Add(new SqlParameter("@minPrice", dtBase.Rows[0]["minPrice"]));
                    plist.Add(new SqlParameter("@maxPrice", dtBase.Rows[0]["maxPrice"]));
                    plist.Add(new SqlParameter("@CreateTime", DateTime.Now));
                    SqlParameter[] p = plist.ToArray();
                    int result = StarTech.DBCommon.InsertData("T_Member_Coupon", p);
                    if (result > 0)
                    {
                        string sql = "";
                        foreach (DataRow rowTop in dtTop.Rows)
                        {
                            sql += "update T_Member_Coupon set IsUsed=1,Remark='已合并',GetPlaceInfo='已合并',GroupCouponId='" + CouponId + "' where CouponId='" + rowTop["CouponId"] + "';";
                        }
                        adoHelper.ExecuteSqlNonQuery(sql);

                    }
                }
            }
            return "1";
        }
        return "0";
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
        string table = " (select a.*,b.TrueName,b.Mobile from T_Member_Coupon a,T_Member_Info b where a.MemberId=b.MemberId)vv ";
        //string fields = "AdId,title,Link,DisplayModeName,CategoryName,sort";//字段顺序和必须前台jggrid设置的一样
        string fields = "Sysnumber,MemberId,TrueName,Mobile,CouponId,CouponType,CouponValue,StartTime,EndTime,IsUsed,Remark,'操作' as cmd_col ";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; //bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by StartTime desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "Sysnumber");
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
    public string ListGroup(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string mid)
    {
        string table = " (select a.*,b.TrueName,b.Mobile from T_Member_Coupon a,T_Member_Info b where a.MemberId=b.MemberId)vv ";
        //string fields = "AdId,title,Link,DisplayModeName,CategoryName,sort";//字段顺序和必须前台jggrid设置的一样
        string fields = "Sysnumber,MemberId,TrueName,Mobile,CouponId,CouponType,CouponValue,StartTime,EndTime,IsUsed,Remark,'操作' as cmd_col ";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter) + " and MemberId='" + mid + "' and IsUsed=0";
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; //bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by StartTime desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "Sysnumber");
    }

    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            if (hTable.Contains("TrueName") && hTable["TrueName"].ToString().Trim() != "")
            {
                filter += " and TrueName like '%" + hTable["TrueName"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("Mobile") && hTable["Mobile"].ToString().Trim() != "")
            {
                filter += " and Mobile like '%" + hTable["Mobile"].ToString().Trim() + "%'";
            }
        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {
       
    }

    public string GetGoodsData(string orderId)
    {
        string s = "";
        DataSet ds = adoHelper.ExecuteSqlDataset("select GoodsName from T_Order_InfoDetail where OrderId='" + orderId + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            s += row["GoodsName"] + ";";
        }
        return s;
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
            result = bll.Delete(Convert.ToInt32(ids)) ? "true" : "false";
        }
        else
        {
            string[] idList = ids.Split(new char[] { '|' });

            for (int i = 0; i < idList.Length - 1; i++)
            {
                if (ids[i].ToString() != "")
                {
                    result = bll.Delete(Convert.ToInt32(idList[i])) ? "true" : "false";
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


    #region 判断区域编号是否存在
    public string IdIsExist(string id, string lang, HttpContext context)
    {
        string result = string.Empty;
        if (!string.IsNullOrEmpty(id))
        {
            //model = bll.GetModel(id);
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