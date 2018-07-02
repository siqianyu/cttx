<%@ WebHandler Language="C#" Class="FlagHandler" %>

using System;
using System.Web;
using StarTech;
using System.Data;
using System.Web.SessionState;

public class FlagHandler : IHttpHandler, IRequiresSessionState
{

    StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

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
        //string state = context.Request["state"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["state"], Int32.MaxValue);   //排序规则

        if (flag == "list")
        {
            string statu = KillSqlIn.Form_ReplaceByString(context.Request.QueryString["statu"], 20);
            context.Response.Write(List(page, "10", sidx, sord, searchfilter, statu));
        }
        else if (flag == "sharelist")
        {
            string statu = KillSqlIn.Form_ReplaceByString(context.Request.QueryString["statu"], 20);
            context.Response.Write(ShareList(page, "10", sidx, sord, searchfilter, statu));
        }
        else if (flag == "sharelistdetail")
        {
            string memberId2 = KillSqlIn.Form_ReplaceByString(context.Request.QueryString["memberId"], 20);
            context.Response.Write(ShareListDetailByMember(page, "10", sidx, sord, searchfilter, memberId2));
        }
        else if (flag == "sharecashlist")
        {
            string statu = KillSqlIn.Form_ReplaceByString(context.Request.QueryString["statu"], 20);
            context.Response.Write(ShareCashList(page, "10", sidx, sord, searchfilter, statu));
        }

        else if (flag == "use")
        {
            string statu = KillSqlIn.Form_ReplaceByString(context.Request.QueryString["statu"], 20);
            context.Response.Write(Disable(id, statu));
        }
        else if (flag == "cz")
        {
            string memberId = KillSqlIn.Form_ReplaceByString(context.Request.QueryString["memberId"], 20); ;
            decimal money = 0;
            decimal.TryParse(context.Request.QueryString["money"], out money);
            context.Response.Write(Recharge(memberId, money));
        }
        else if(flag=="delete")
        {
            context.Response.Write(DeleteData(id, context));
        }
    }

    /// <summary>
    /// 启用或禁用用户任务
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string Disable(string goodsid, string statu)
    {
        string gid = KillSqlIn.Form_ReplaceByString(goodsid, 20);
        string strSQL = "update T_Base_Flag set if_use='" + statu + "' where flag_id='" + gid + "';";
        int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
            return "true";

        return "false";
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
            string strSQL = "delete from T_Base_Flag where flag_id='" + ids + "'";
            result = adoHelper.ExecuteSqlNonQuery(strSQL) > 0 ? "true" : "false";
        }
        else
        {
            ids = ids.Replace("|", "','");
            string strSQL = "delete from T_Base_Flag where flag_id in('" + ids + "')";
            result = adoHelper.ExecuteSqlNonQuery(strSQL) > 0 ? "true" : "false";
        }
        return result;
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
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string statu)
    {
        string table = " T_Base_Flag";
        string fields = "flag_id,flag_name,if_use, '操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        if (statu != "")
        {

            //state = KillSqlIn.Form_ReplaceByString(state, 20);
            if (statu == "1")
                filter += " and if_use=1 ";
            if (statu == "0")
                filter += " and if_use=0 ";
        }
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; // bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        //if (orderBy.Equals("cmd_col")) { sort = " order by flag_id desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "flag_id");
    }


    public string GetFilter(string searchfilter)
    {
        System.Collections.Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("flag_name") && hTable["flag_name"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["flag_name"].ToString().Trim(), Int32.MaxValue);
                filter += " and flag_name like '%" + SafeStr + "%'";
            }
        }
        return filter;
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
    public string ShareList(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string statu)
    {
        string table = " (SELECT [MemberId],[MemberName],[TrueName],[Mobile],count(*) ShareTotal FROM [V_WXQRCodeShare_Log] group by [MemberId],[MemberName],[TrueName],[Mobile])v ";
        string fields = "MemberId,MemberName,Mobile,TrueName,ShareTotal,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);

        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; // bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by ShareTotal desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "MemberId");
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
    public string ShareCashList(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string statu)
    {
        string table = " View_Member_ShareCash ";
        string fields = "Sysnumber,TrueName,Tel,CouponId,CouponType,CouponValue,IsUsed,Remark,ShareFirendTrueName,CreateTime,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);

        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; // bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by CreateTime desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
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
    public string ShareListDetailByMember(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string memberId)
    {
        string table = " (SELECT * FROM [V_WXQRCodeShare_Log] where[MemberId]='" + memberId + "')v ";
        string fields = "sysnumber,MemberId,MemberName,Mobile,TrueName,logTime,firendNewNickname,newFirendMemberName,newFirendTrueName,newFirendMobile,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);

        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0; // bll.GetRecordCount(filter);
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by logTime desc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "sysnumber");
    }

    /// <summary>
    /// 账户充值
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="money"></param>
    /// <returns></returns>
    public string Recharge(string memberId, decimal money)
    {
        memberId = KillSqlIn.Form_ReplaceByString(memberId, 50);
        string strSQL = "BEGIN TRANSACTION update T_Moneybag_AccountInfo set account_money=account_money+" + money + " where member_Id='" + memberId + "';";
        string guid = Guid.NewGuid().ToString();
        string accid = "(select top 1 account_id from T_Moneybag_AccountInfo where member_id='" + memberId + "')";
        string userId = HttpContext.Current.Session["UserId"].ToString();
        strSQL += "insert T_Moneybag_AccountDetail values('" + guid + "'," + accid + ",'cz'," + money + ",'管理员充值','" + userId + "','" + DateTime.Now + "','sys','');";
        strSQL += " COMMIT TRANSACTION ";
        int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (rows > 0)
            return "success";
        return "fail";
    }

    public void EditDataSource(ref DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            ////update total
            //adoHelper.ExecuteSqlNonQuery("update T_Moneybag_AccountInfo set account_money=(select isnull(sum(money),0) from T_Moneybag_AccountDetail where  account_id=(select account_id from T_Moneybag_AccountInfo where member_id='" + dr["MemberId"] + "')) where member_id='" + dr["MemberId"] + "'");
            //object objMoney = adoHelper.ExecuteSqlScalar("select account_money from T_Moneybag_AccountInfo where member_id='" + dr["MemberId"] + "'");
            //dr["money"] = objMoney == null ? 0 : decimal.Parse(objMoney.ToString());

            //object objPass = adoHelper.ExecuteSqlScalar("select ifPass from T_Member_Pic where applyType='auth' and memberId='" + dr["MemberId"] + "' ");
            //dr["MemberStatus"] = objPass == null ? 0 : int.Parse(objPass.ToString());
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