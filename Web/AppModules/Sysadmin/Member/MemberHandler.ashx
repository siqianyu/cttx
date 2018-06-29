<%@ WebHandler Language="C#" Class="MemberHandler" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;

public class MemberHandler : IHttpHandler
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    string type = string.Empty, code = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        type = context.Request["type"] == null ? "" : context.Request["type"].ToUpper();
        code = context.Request["code"] == null ? "" : context.Request["code"].ToUpper();
        string id = context.Request["id"] == null ? "" : context.Request["id"].ToLower();
        string count = context.Request["count"] == null ? "" : context.Request["count"].ToLower();

        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : context.Request["rows"];     //显示数量
        string page = context.Request["page"] == null ? "1" : context.Request["page"];      //当前页
        string sidx = context.Request["sidx"] == null ? "" : context.Request["sidx"];       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : context.Request["sord"];   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "delete")
        {
            context.Response.Write(DeleteData(id));
        }
        else if (flag == "count")
        {
            context.Response.Write(MemberCount(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "hy")
        {
            context.Response.Write(HyList(page, rows, sidx, sord, searchfilter));
        }

        else if (flag == "log")
        {
            context.Response.Write(Log(page, rows, sidx, sord, searchfilter));
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
    /// 
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "(select memberId,memberName,memberCompanyName,memberLevel,memberTrueName,memberType,shFlag,memberStatus,regTime,(select levelname from T_Member_Level where levelid=t.memberLevel) as levelname from  T_Member_Info t) as v";
        string fields = "memberId,memberName,memberCompanyName,memberTrueName,levelname,shFlag,memberStatus,regTime,'操作' as cmd_col,memberLevel";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by  memberId";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "memberId");
    }

    /// <summary>
    /// 会员统计
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    /// 
    public string MemberCount(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = " (select memberCompanyType,count(*) as cnt,(select HyName from T_Base_HY where HyCode=memberCompanyType) as hyName from t_member_info where memberCompanyType in (select HyCode from T_Base_HY)  group by memberCompanyType ) as v";
        string fields = "memberCompanyType,hyName,cnt,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by cnt desc";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        for (int i = 0; i < dtSource.Rows.Count; i++)
        {
            dtSource.Rows[i]["cnt"] = CountNum(dtSource.Rows[i]["memberCompanyType"].ToString());
        }
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "memberCompanyType");
    }


    /// <summary>
    /// 某个行业会员列表
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    /// 
    public string HyList(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "(select memberId,memberName,memberCompanyName,memberCompanyType,memberTrueName,memberType,shFlag,memberStatus,regTime,(select levelname from T_Member_Level where levelid=t.memberLevel) as levelname from  T_Member_Info t) as v";
        string fields = "memberId,memberName,memberCompanyName,memberTrueName,levelname,shFlag,memberStatus,regTime,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by  memberId";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "memberId");
    }

    /// <summary>
    /// 用户操作日志
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    /// 
    public string Log(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "(select *,(select userName from IACenter_User where uniqueid=t_member_log.memberId) as person,(select memberCompanyName from T_Member_Info where memberId=t_member_log.target) as member from t_member_log) as v";
        string fields = "memberId,person,member,operation,decoration,time";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by  time desc";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        //EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "memberId");
    }

    public string CountNum(string hy)
    {
        try
        {
            string sql = @"select count(*) from T_Member_Info where memberCompanyType like '%" + hy + "%'";
            return adoHelper.ExecuteSqlScalar(sql).ToString();
        }
        catch
        {
            return "0";
        }
    }



    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = "1=1 ";
        if (type != "")
        {
            filter += "and memberType='" + type + "'";
        }
        if (code != "")
        {
            filter += "and memberCompanyType like '%" + code + "%'";
        }
        if (hTable != null && hTable.Count > 0)
        {
            if (hTable.Contains("memberName") && hTable["memberName"].ToString().Trim() != "") { filter += " and memberName like '%" + hTable["memberName"].ToString().Trim() + "%'"; }
            if (hTable.Contains("memberCompanyName") && hTable["memberCompanyName"].ToString().Trim() != "") { filter += " and memberCompanyName like '%" + hTable["memberCompanyName"].ToString().Trim() + "%'"; }
            if (hTable.Contains("memberTrueName") && hTable["memberTrueName"].ToString().Trim() != "") { filter += " and memberTrueName like '%" + hTable["memberTrueName"].ToString().Trim() + "%'"; }
            if (hTable.Contains("memberLevel") && hTable["memberLevel"].ToString().Trim() != "0") { filter += " and memberLevel = '" + hTable["memberLevel"].ToString().Trim() + "'"; }
            if (hTable.Contains("hyName") && hTable["hyName"].ToString().Trim() != "") { filter += " and hyName like '%" + hTable["hyName"].ToString().Trim() + "%'"; }
            if (hTable.Contains("person") && hTable["person"].ToString().Trim() != "") { filter += " and person like '%" + hTable["person"].ToString().Trim() + "%'"; }
            if (hTable.Contains("operation") && hTable["operation"].ToString().Trim() != "") { filter += " and operation like '%" + hTable["operation"].ToString().Trim() + "%'"; }
            if (hTable.Contains("member") && hTable["member"].ToString().Trim() != "") { filter += " and member like '%" + hTable["member"].ToString().Trim() + "%'"; }
        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {
        DataColumn col = new DataColumn("shFlagRes", typeof(string));
        dt.Columns.Add(col);
        foreach (DataRow row in dt.Rows)
        {
            if (dt.Columns.Contains("memberStatus") && row["memberStatus"].ToString().Contains("ZC") == true)
            {
                row["memberStatus"] = "<font style='color:green'>正常</font>";
            }
            else if (dt.Columns.Contains("memberStatus") && row["memberStatus"].ToString().Contains("JY") == true)
            {
                row["memberStatus"] = "<font style='color:red'>禁用</font>";
            }
            else if (dt.Columns.Contains("memberStatus") && row["memberStatus"].ToString().Contains("WJH") == true)
            {
                row["memberStatus"] = "<font style='color:red'>未激活</font>";
            }
            if (dt.Columns.Contains("levelname") && row["levelname"].ToString() == "")
            {
                row["levelname"] = "<font style='color:red'>暂不明确</font>";
            }

            //if (dt.Columns.Contains("memberType") && row["memberType"].ToString() == "QY")
            //{
            //    row["memberType"] = "企业";
            //}
            //else if (dt.Columns.Contains("memberType") && row["memberType"].ToString() == "XH")
            //{
            //    row["memberType"] = "协会";
            //}
            //else if (dt.Columns.Contains("memberType") && row["memberType"].ToString() == "XZ")
            //{
            //    row["memberType"] = "行政";
            //}

            if (dt.Columns.Contains("shFlag") && row["shFlag"].ToString() == "1")
            {
                row["shFlagRes"] = "通过";
            }
            else
            {
                row["shFlagRes"] = "未通过";
            }
        }
    }


    /// <summary>
    /// 删除数据(多个编号用逗号(|)隔开)
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteData(string ids)
    {
        if (ids.IndexOf("|") < 0)
        {
            int i = adoHelper.ExecuteSqlNonQuery("delete from T_Member_Info where memberId in('" + ids.Replace(",", "','") + "')");
            return i.ToString();
        }
        else
        {
            string[] idList = ids.Split(new char[] { '|' });

            for (int i = 0; i < idList.Length - 1; i++)
            {
                if (ids[i].ToString() != "")
                {
                    int id = Convert.ToInt32(idList[i].ToString());
                    int res = adoHelper.ExecuteSqlNonQuery("delete from T_Member_Info where memberId=" + id);
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