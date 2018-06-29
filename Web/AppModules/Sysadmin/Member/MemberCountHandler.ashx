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
    string area = "";
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "" : context.Request["id"].ToLower();
        area = context.Request["area"] == null ? "" : context.Request["area"].ToLower();

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
        else if (flag == "listcount")
        {
            context.Response.Write(ListCount(page, rows, sidx, sord, searchfilter));
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
    /// 
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = " T_Member_Info";
        string fields = "memberId,memberName,memberCompanyName,memberCompanyCode,address,tel,memberType,RegType,regTime,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "memberId");
    }



    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = "1=1 ";
        if (area.Trim().Equals("其他"))
        {
            filter += @"areaName not like '下沙'
        and areaName not like '上城'
        and areaName not like '下城'
        and areaName not like '江干'
        and areaName not like '西湖'
        and areaName not like '萧山'
        and areaName not like '滨江'
        and areaName not like '临安'
        and areaName not like '桐庐'
        and areaName not like '拱墅'
        and areaName not like '余杭'
        and areaName not like '建德'
        and areaName not like '富阳'
        and areaName not like '市局'
        and areaName not like '余杭'";
        }
        else if (area != "")
        {
            filter += "and areaName like '%" + area + "%'";
        }

        if (hTable != null && hTable.Count > 0)
        {
            if (hTable.Contains("MemberType") && hTable["MemberType"].ToString().Trim() != "") { filter += " and MemberType like '%" + hTable["MemberType"].ToString().Trim() + "%'"; }
            if (hTable.Contains("RegType") && hTable["RegType"].ToString().Trim() != "") { filter += " and RegType like '%" + hTable["RegType"].ToString().Trim() + "%'"; }

        }
        return filter;
    }

    public DataTable EditDataSource(ref DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            if (dt.Columns.Contains("RegType") && row["RegType"].ToString().Contains("PT") == true)
            {
                row["RegType"] = "普通";
            }
            else if (dt.Columns.Contains("RegType") && row["RegType"].ToString().Contains("XFQ") == true)
            {
                row["RegType"] = "消费券";
            }

            if (dt.Columns.Contains("memberType") && row["memberType"].ToString() == "QY")
            {
                row["memberType"] = "企业";
            }
            else if (dt.Columns.Contains("memberType") && row["memberType"].ToString() == "XH")
            {
                row["memberType"] = "协会";
            }
            else if (dt.Columns.Contains("memberType") && row["memberType"].ToString() == "XZ")
            {
                row["memberType"] = "行政";
            }
        }
        return dt;
    }



    public string ListCount(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        //string table = " T_Member_Info";
        string fields = "areaName,com,gov,person,formreg,xfqreg,total,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        //string filter = GetFilter(searchFilter);
        //string sort = "";
        //int totalRecords = 0;
        //DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        DataTable dt = GetCount();

        int totalPages = JSONHelper.GetTotalPages(dt.Rows.Count, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), dt.Rows.Count.ToString(), dt, fields.Replace("'操作' as ", "").Split(','), "areaName");
    }

    public DataTable GetCount()
    {
        string sql = @"select distinct areaName from T_Member_Info where areaName like '%上城%' or areaName like '%下城%' or areaName like '%西湖%' 
or areaName like '%江干%'  or areaName like '%拱墅%' or areaName like '%滨江%' or areaName like '%萧山%' or areaName like '%余杭%'
or areaName like '%临安%' or areaName like '%富阳%' or areaName like '%桐庐%' or areaName like '%建德%' or areaName like '%淳安%' 
or areaName like '%下沙%' or areaName like '%市局%' order by areaName ";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("上城"))
            {
                row["areaName"] = "上城";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("下城"))
            {
                row["areaName"] = "下城";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("西湖"))
            {
                row["areaName"] = "西湖";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("江干"))
            {
                row["areaName"] = "江干";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("上城"))
            {
                row["areaName"] = "上城";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("拱墅"))
            {
                row["areaName"] = "拱墅";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("滨江"))
            {
                row["areaName"] = "滨江";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("萧山"))
            {
                row["areaName"] = "萧山";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("余杭"))
            {
                row["areaName"] = "余杭";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("临安"))
            {
                row["areaName"] = "临安";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("富阳"))
            {
                row["areaName"] = "富阳";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("建德"))
            {
                row["areaName"] = "建德";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("桐庐"))
            {
                row["areaName"] = "桐庐";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("淳安"))
            {
                row["areaName"] = "淳安";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("下沙"))
            {
                row["areaName"] = "下沙";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("市局"))
            {
                row["areaName"] = "市局";
            }
        }

        dt.Columns.Add(new DataColumn("com", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("gov", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("person", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("formreg", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("xfqreg", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("total", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("cmd_col", System.Type.GetType("System.String")));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            dr["com"] = CountComNum(dr["areaName"].ToString());
            dr["gov"] = CountGovNum(dr["areaName"].ToString());
            dr["person"] = CountPerNum(dr["areaName"].ToString());
            dr["formreg"] = CountPTRegNum(dr["areaName"].ToString());
            dr["xfqreg"] = CountXFQRegNum(dr["areaName"].ToString());
            dr["total"] = CountMemberNum(dr["areaName"].ToString());
            dr["total"] = CountMemberNum(dr["areaName"].ToString());
        }
        return dt;
    }

    /// <summary>
    /// 统计各类型的会员
    /// </summary>
    /// <param name="areaName"></param>
    /// <returns></returns>

    public string CountComNum(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and memberType ='QY'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    public string CountGovNum(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and memberType= 'XH'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    public string CountPerNum(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and memberType ='XZ'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    // 普通会员注册数量
    public string CountPTRegNum(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and RegType='PT'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    // 消费券会员注册数量
    public string CountXFQRegNum(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and RegType='XFQ'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    public string CountMemberNum(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    #region 单位及城区会员统计
    /// <summary>
    /// 获取城区数据
    /// </summary>
    /// <returns></returns>
    private DataTable GetDataArea()
    {
        string filter = "1=1 ";
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select distinct areaName from T_Member_Info where areaName like '%上城%' or areaName like '%下城%' or areaName like '%西湖%' or areaName like '%江干%'  or areaName like '%拱墅%'or areaName like '%滨江%' or areaName like '%萧山%' or areaName like '%余杭%' or areaName like '%临安%'  or areaName like '%富阳%' or areaName like '%桐庐%' or areaName like '%建德%' or areaName like '%淳安%' or areaName like '%下沙%'  or areaName like '%市局%'   ");
        strSql.Append(" order by areaName ");
        DataTable dt = adoHelper.ExecuteSqlDataset(strSql.ToString()).Tables[0];
        return dt;
    }

    //单位
    public DataTable GetCountDataByArea(string area)
    {
        string sql = @"select distinct (select count(*) from T_Member_Info where areaName like '%" + area + "%' and membertype ='Com') as count1 ,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and membertype like 'Gov') as count2,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and membertype like 'Person') as count3,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and regType ='PT') as count4,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and regType ='XFQ') as count5 from T_Member_Info";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
        return dt;
    }
    #endregion
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}