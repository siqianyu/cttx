using Startech.Utils;
using StarTech.DBUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Member_MemberList : StarTech.Adapter.StarTechPage
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        string flag = Common.NullToEmpty(Request["flag"]);
        switch (flag)
        {
            case "list":
                GetList();
                break;
            case "delete":
                Delete();
                break;
        }
    }

    private void GetList()
    {
        string searchfilter = Request["searchfilter"] == null ? "" : KillSqlIn.Url_ReplaceByString(Server.UrlDecode(Request.QueryString["searchfilter"]), Int32.MaxValue);
        
        string rows = Request["rows"] == null ? "10" : KillSqlIn.Url_ReplaceByString(Request.QueryString["rows"], Int32.MaxValue);     //显示数量
        string page = Request["page"] == null ? "1" : KillSqlIn.Url_ReplaceByString(Request.QueryString["page"], Int32.MaxValue);      //当前页
        string sidx = Request["sidx"] == null ? "" : KillSqlIn.Url_ReplaceByString(Request.QueryString["sidx"], Int32.MaxValue);       //排序字段
        string sord = Request["sord"] == null ? "desc" : KillSqlIn.Url_ReplaceByString(Request.QueryString["sord"], Int32.MaxValue);   //排序规则

        string table = " t_task_config ";
        string fields = "task_id,task_code,task_sn,task_name,remarks,if_use, '' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchfilter);

        string sort = "order by " + sidx + " " + sord + "";
        int totalRecords = 0; // bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows) + 1;
        int end = Convert.ToInt32(page) * Convert.ToInt32(rows);
        if (sidx.Equals("cmd_col")) { sort = " order by task_id asc "; }
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(page), Int32.Parse(rows), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(rows));
        string json = JSONHelper.ToJGGridJson(page, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'' as ", "").Split(','), "task_id");
        Response.Write(json);
        Response.End();
    }

    private string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("task_name") && hTable["task_name"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["task_name"].ToString().Trim(), Int32.MaxValue);
                filter += " and task_name like '%" + SafeStr + "%'";
            }
            if (hTable.Contains("if_use") && hTable["if_use"].ToString().Trim() != "")
            {
                SafeStr = Common.NullToZero(hTable["if_use"]).ToString();
                filter += " and if_use = " + SafeStr;
            }

        }
        return filter;
    }

    private void EditDataSource(ref DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            //int if_use = Common.NullToZero(dr["if_use"]);
            //dr["if_use_str"] = if_use == 0 ? "否" : "是";
        }
    }

    private void Delete()
    {
        string id = Common.NullToEmpty(Request["id"]);
        string[] ids = id.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string strSql = "delete from t_task_config where task_id='{0}';";
        for (int i = 0; i < ids.Length; i++)
        {
            ado.ExecuteSqlNonQuery(string.Format(strSql, ids[i]));
        }
        Response.Write("true");
        Response.End();
    }
}