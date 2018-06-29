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
using Startech.Member.Member;

public class MemberHandler : IHttpHandler
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "" : context.Request["id"].ToLower();
        string lang = context.Request["lang"] == null ? "" : context.Request["lang"].ToLower();

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
        else if (flag == "check")
        {
            context.Response.Write(CheckPayRecord(id, context));
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
        string table = " V_Member_MemberCZ";
        string fields = "sysnumber,memberName,money,moneyType,addTime,addPerson,shFlag,shPerson,shTime,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "sysnumber");
    }

    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = "1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            if (hTable.Contains("memberName") && hTable["memberName"].ToString().Trim() != "") { filter += " and memberName like '%" + hTable["memberName"].ToString().Trim() + "%'"; }
            if (hTable.Contains("moneyType") && hTable["moneyType"].ToString().Trim() != "") { filter += " and moneyType like '%" + hTable["moneyType"].ToString().Trim() + "%'"; }

        }
        return filter;
    }

    public void EditDataSource(ref DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            if (row["moneyType"].ToString() == "XJ")
            {
                row["moneyType"] = "现金";
            }
            else if (row["moneyType"].ToString() == "ZS")
            {
                row["moneyType"] = "赠送";
            }
            else if (row["moneyType"].ToString() == "XFQ")
            {
                row["moneyType"] = "消费券";
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
        int i = adoHelper.ExecuteSqlNonQuery("delete from T_Member_AccountRecord where sysnumber in('" + ids.Replace(",", "','") + "')");
        return "true";
    }


    //审核
    public string CheckPayRecord(string ids, HttpContext context)
    {
        try
        {
            if (ids != "")
            {
                MemberCZRecordBLL cbll = new MemberCZRecordBLL();
                MemberCZRecordDAL dal = new MemberCZRecordDAL();
                string[] strIds = ids.Split(',');
                foreach (string id in strIds)
                {
                    string strType = dal.GetMemberMoneyType(id);
                    MemberCZRecordModel model = cbll.GetModel(id);
                    if (model != null)
                    {
                        if (model.shFlag != 1)
                        {
                            int j = dal.UpdateMemberCount(strType, model.money.ToString(), model.memberId.ToString());

                            int i = dal.UpdateMemberCZ(id, context.Request.Cookies["__UserInfo"]["nickname"].ToString(), System.DateTime.Now.ToString());
                        }
                    }

                    /*日志归档*/
                    string sql = @"select moneyType as title from T_Member_AccountRecord u where sysnumber='" + id + "'";
                    PubFunction.InsertLog("业务管理", "会员管理", "会员充值列表", "审核", sql, id);
                }
            }
            return "true";
        }
        catch (Exception ee)
        {
            return ee.Message;
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