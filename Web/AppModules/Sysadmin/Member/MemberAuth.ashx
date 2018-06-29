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
using System.Data.SqlClient;

public class MemberHandler : IHttpHandler
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    string type = string.Empty, code = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "" : context.Request["id"].ToLower();
        string count = context.Request["count"] == null ? "" : context.Request["count"].ToLower();
        string undo = context.Request["undo"] == null ? "" : context.Request["undo"].ToString();
        string undoinfo = context.Request["undoinfo"] == null ? "" : context.Request["undoinfo"].ToString();
        string submenu = context.Request["ids"] == null ? "" : context.Request["ids"].ToString();
        string subinfo = context.Request["names"] == null ? "" : context.Request["names"].ToString();

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
        else if (flag == "query")
        {
            context.Response.Write(GetInfo(id));
        }
        else if (flag == "update")
        {
            context.Response.Write(UpdateSubAccount(id, submenu, subinfo, undo, undoinfo));
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
        string table = "T_Member_SubInfo";
        string fields = "id,flag,subinfo";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by id";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "id");
    }


    /// <summary>
    /// 检索权限
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    /// 
    public string GetInfo(string id)
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select submenu from T_Member_SubInfo where id=" + id).Tables[0];
        return JSONHelper.GetJSON(dt);
    }

    /// <summary>
    /// 修改权限
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    /// 
    public string UpdateSubAccount(string id, string submenu, string subinfo, string undo, string undoinfo)
    {
        try
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Member_SubInfo set ");
            strSql.Append("submenu=@submenu,");
            strSql.Append("subinfo=@subinfo, ");
            strSql.Append("undo=@undo,");
            strSql.Append("undoinfo=@undoinfo, ");
            strSql.Append("undourl=@undourl ");
            strSql.Append("where id=@id");
            SqlParameter[] parameters = {
                   new SqlParameter("@submenu", SqlDbType.Text),
                    new SqlParameter("@subinfo", SqlDbType.Text),
                    new SqlParameter("@undo", SqlDbType.Text),
                    new SqlParameter("@undoinfo", SqlDbType.Text),
                     new SqlParameter("@undourl", SqlDbType.Text),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = submenu.TrimEnd(new char[] { ';' });
            parameters[1].Value = subinfo.TrimEnd(new char[] { ';' });
            parameters[2].Value = undo.TrimEnd(new char[] { ';' });
            parameters[3].Value = undoinfo.TrimEnd(new char[] { ';' });
            parameters[4].Value = GetUndoUrl(undo.TrimEnd(new char[] { ';' }));
            parameters[5].Value = id;

            int i = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (i > 0)
            {
                return "设置成功";
            }
            else
            {
                return "设置失败";
            }
        }
        catch (Exception ee)
        {
            return "设置失败,原因:" + ee.Message;
        }
    }

    private string GetUndoUrl(string undo)
    {
        string urls = "";
        if (undo != "")
        {
            undo = undo.TrimEnd(';');
            string[] ss = undo.Split(';');
            for (int i = 0; i < ss.Length; i++)
            {
                string url = GetUrlByUndo(ss[i]);
                if (url != "")
                {
                    urls += url + ";";
                }
            }
        }
        return urls;
    }

    private string GetUrlByUndo(string undo)
    {
        try
        {
            DataTable dt = DalBase.Util_GetList("select url from T_Member_Menu where mid='" + undo + "' and  [target]='newgzs' ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["url"].ToString() == "/" || dt.Rows[0]["url"].ToString().Contains("#"))
                {
                    return "";
                }
                else
                {
                    return dt.Rows[0]["url"].ToString();
                }
            }
            else
            {
                return "";
            }
        }

        catch
        {
            return "";
        }
    }


    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " flag<>'' and [target]='newgzs' ";
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {

        foreach (DataRow row in dt.Rows)
        {
            if (row["flag"].ToString() == "wjh")
            {
                row["flag"] = "未激活会员";
            }
            else if (row["flag"].ToString() == "unhz")
            {
                row["flag"] = "非杭州企业会员";
            }
            else if (row["flag"].ToString() == "person")
            {
                row["flag"] = "个人用户";
            }
            else if (row["flag"].ToString() == "xh")
            {
                row["flag"] = "协会会员";
            }
            else if (row["flag"].ToString() == "xz")
            {
                row["flag"] = "行政会员";
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