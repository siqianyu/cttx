<%@ WebHandler Language="C#" Class="MemberHandler" %>

using System;
using System.Web;
using StarTech;
using System.Data;
using System.Web.SessionState;

public class MemberHandler : IHttpHandler, IRequiresSessionState
{

    StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string flag = context.Request["flag"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["flag"].ToLower(), Int32.MaxValue);
        string id = context.Request["id"] == null ? "" : context.Request["id"];

        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Server.UrlDecode(context.Request["searchfilter"]), Int32.MaxValue);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "10" : KillSqlIn.Url_ReplaceByString(context.Request["rows"], Int32.MaxValue);     //显示数量
        string page = context.Request["page"] == null ? "1" : KillSqlIn.Url_ReplaceByString(context.Request["page"], Int32.MaxValue);      //当前页
        string sidx = context.Request["sidx"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["sidx"], Int32.MaxValue);       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : KillSqlIn.Url_ReplaceByString(context.Request["sord"], Int32.MaxValue);   //排序规则

        switch (flag.ToLower())
        {
            case "addwords":
                string words = context.Request["words"] == null ? "" : context.Request["words"];
                context.Response.Write(AddWords(words));
                break;
            case "wordslist":
                context.Response.Write(WordsList(page, rows));
                break;
            case "deletewords":
                context.Response.Write(DeleteWords(id));
                break;
        }
    }
    
    #region Category
    public string AddWords(string words)
    {
        string strSql = string.Format("insert into T_Base_CommentWords(word) values('{0}')", words);
        return adoHelper.ExecuteSqlNonQuery(strSql).ToString();
    }


    /// <summary>
    /// 列表检索
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public string WordsList(string curPage, string pageSize)
    {
        string table = " T_Base_CommentWords ";
        string fields = "id,word";//字段顺序和必须前台jggrid设置的一样
        int totalRecords = 0;
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, "1=1", "", Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Split(','), "id");
    }

    public string DeleteWords(string id)
    {
        string[] ids = id.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string strSql = "delete from T_Base_CommentWords where id='{0}';";
        for (int i = 0; i < ids.Length; i++)
        {
            adoHelper.ExecuteSqlNonQuery(string.Format(strSql, ids[i]));
        }
        return "1";
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