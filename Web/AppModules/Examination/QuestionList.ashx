<%@ WebHandler Language="C#" Class="QuestionList" %>

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
using System.Web.SessionState;

public class QuestionList : IHttpHandler, IRequiresSessionState
{
    
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    StarTech.ELife.Goods.GoodsBll bll = new StarTech.ELife.Goods.GoodsBll();
    StarTech.ELife.Goods.GoodsModel model = new StarTech.ELife.Goods.GoodsModel();
    string userid = "";
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        if (!LogAdd.IsOnline(context, ref userid))
            return;
        
        string flag = context.Request["flag"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["flag"].ToLower(), Int32.MaxValue);
        string id = context.Request["id"] == null ? "" : context.Request.QueryString["id"].ToLower();
        string lang = context.Request["lang"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["lang"].ToLower(), Int32.MaxValue);
        string QikanId = context.Request["qikanId"] == null ? "0" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["qikanId"].ToLower(), Int32.MaxValue);
        string testSysnumber = context.Request["testSysnumber"] == null ? "" : context.Request.QueryString["testSysnumber"].ToLower();
        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Server.UrlDecode(context.Request.QueryString["searchfilter"]), Int32.MaxValue);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "10" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["rows"], Int32.MaxValue);     //显示数量
        string page = context.Request["page"] == null ? "1" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["page"], Int32.MaxValue);      //当前页
        string sidx = context.Request["sidx"] == null ? "" : context.Request.QueryString["sidx"];  //排序字段
        string sord = context.Request["sord"] == null ? "desc" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["sord"], Int32.MaxValue);   //排序规则
        string categoryId = context.Request["categoryId"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["categoryId"], Int32.MaxValue);   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, "20", sidx, sord, searchfilter,categoryId));
        }
        else if (flag == "listdialog")
        {
            context.Response.Write(ListDialog(page, "1000", sidx, sord, searchfilter, categoryId, testSysnumber));
        }
        else if (flag == "listdialog_select")
        {
            context.Response.Write(ListDialogSelect(page, "1000", sidx, sord, searchfilter, categoryId, testSysnumber));
        }  
            
        else if (flag == "delete")
        {
            context.Response.Write(Delete(id));
        }else if (flag == "deletebat")
        {
            context.Response.Write(DeleteAll(id));
        }
            
        else if (flag == "allsh")
        {
            context.Response.Write(AllSH(id));
        }
        else if (flag == "addsj")
        {
            context.Response.Write(AddSJ(testSysnumber, id));
        }
        else if (flag == "delsj")
        {
            context.Response.Write(DelSJ(testSysnumber, id));
        }
        else if (flag == "slide")
        {
            string goodsid = context.Request["goodsId"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.QueryString["goodsId"], Int32.MaxValue);   //排序规则
            context.Response.Write(SlideList(goodsid));
        }
        else if (flag == "get_al")
        {
            context.Response.Write(GetAL(id));
        }
    }

    /// <summary>
    /// 获取幻灯片
    /// </summary>
    /// <param name="goodsList"></param>
    /// <returns></returns>
    public string SlideList(string goodsId)
    {
        string imgList = "";
        string strSQL = "select * from T_Goods_Pic where GoodsId='"+goodsId+"' order by orderby asc;";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 0)
            return "";
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count < 0)
            return "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            imgList += "<img src='"+ds.Tables[0].Rows[i]["picpath"]+"' width='100px' height='100px' style='margin:0px 2px'/>";
        }
        return imgList;
    }

    /// <summary>
    /// 获取案列
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetAL(string sysnumber)
    {
        string strSQL = "select * from T_Test_Queston where sysnumber='" + sysnumber + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 0)
            return "";
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count < 0)
            return "";

        return dt.Rows[0]["questionTitle"].ToString();
    }
    
    /// <summary>
    /// 删除任务
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string Delete(string sysnumber)
    {
        string strSQL = "delete T_Test_Queston where sysnumber='" + sysnumber + "';";
        int row=adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "删除习题《" + sysnumber + "》;", "删除", "", "", HttpContext.Current.Request.Url.ToString());
            return "true";
        }
        return "false";
    }

    /// <summary>
    /// 删除任务
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string DeleteAll(string sysnumber)
    {
        string strSQL = "delete T_Test_Queston where sysnumber in('" + sysnumber.Replace(",","','") + "');";
        int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            //LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "删除习题《" + sysnumber + "》;", "删除", "", "", HttpContext.Current.Request.Url.ToString());
            return "true";
        }
        return "false";
    }
    
    /// <summary>
    /// 全部审核
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string AllSH(string courseId)
    {
        string strSQL = "update  T_Test_Queston  set shFlag=1 where courseId='" + courseId + "';";
        int row = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            //LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "删除习题《" + sysnumber + "》;", "删除", "", "", HttpContext.Current.Request.Url.ToString());
            return "true";
        }
        return "false";
    }


    /// <summary>
    /// 加入试卷
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string AddSJ(string testSysnumber, string ids)
    {
        DataTable dtOld = adoHelper.ExecuteSqlDataset("select questionSysnumber from T_Test_day_Questions where testSysnumber='" + testSysnumber + "'").Tables[0];
        foreach (string id in ids.Split('|'))
        {
            if (id != "" && dtOld.Select("questionSysnumber='" + id + "'").Length == 0)
            {
                string sql = "INSERT INTO [T_Test_day_Questions]([testSysnumber],[questionSysnumber])VALUES('" + testSysnumber + "','" + id + "')";
                adoHelper.ExecuteSqlNonQuery(sql);
            }
        }
        UpdateTotalQuestions(testSysnumber);
        return "true";
    }
    
    /// <summary>
    /// 移除试卷
    /// </summary>
    /// <param name="goodsid"></param>
    /// <returns></returns>
    public string DelSJ(string testSysnumber, string ids)
    {
        DataTable dtOld = adoHelper.ExecuteSqlDataset("select questionSysnumber from T_Test_day_Questions where testSysnumber='" + testSysnumber + "'").Tables[0];
        foreach (string id in ids.Split('|'))
        {
            if (id != "")
            {
                string sql = "delete [T_Test_day_Questions] where [testSysnumber]='" + testSysnumber + "' and [questionSysnumber]='" + id + "'";
                adoHelper.ExecuteSqlNonQuery(sql);
            }
        }
        UpdateTotalQuestions(testSysnumber);
        return "true";
    }

    public void UpdateTotalQuestions(string testSysnumber)
    {
        string ids = "";
        DataTable dtOld = adoHelper.ExecuteSqlDataset("select questionSysnumber from T_Test_day_Questions where testSysnumber='" + testSysnumber + "'").Tables[0];
        foreach (DataRow row in dtOld.Rows)
        {
            ids += row["questionSysnumber"] + ",";
        }
        adoHelper.ExecuteSqlNonQuery("update T_Test_day set Questions='" + ids.TrimEnd(',') + "' where Sysnumber='" + testSysnumber + "'");
    }


    /// <summary>
    /// 调整题目类型排序规则，方便显示
    /// </summary>
    public void questionTypeOrderBy()
    {
        string sql = "update T_Test_Queston set questionTypeOrderBy=1 where questionType='判断题' and isnull(questionTypeOrderBy,0)=0;";
        sql += "update T_Test_Queston set questionTypeOrderBy=2 where questionType='单选题' and isnull(questionTypeOrderBy,0)=0;";
        sql += "update T_Test_Queston set questionTypeOrderBy=3 where questionType='多选题' and isnull(questionTypeOrderBy,0)=0;";
        sql += "update T_Test_Queston set questionTypeOrderBy=4 where questionType='不定项选择题' and isnull(questionTypeOrderBy,0)=0;";
        sql += "update T_Test_Queston set questionTypeOrderBy=11 where questionType='简答题' and isnull(questionTypeOrderBy,0)=0;";
        sql += "update T_Test_Queston set questionTypeOrderBy=12 where questionType='计算分析题' and isnull(questionTypeOrderBy,0)=0;";
        sql += "update T_Test_Queston set questionTypeOrderBy=13 where questionType='综合题' and isnull(questionTypeOrderBy,0)=0;";
        sql += "update T_Test_Queston set mainQuestionSysnumber='' where mainQuestionSysnumber is null;";
        sql += "update T_Test_Queston set questionTypeOrderBy=9  where isnull(mainQuestionSysnumber,'')<>'' and questionTypeOrderBy<>9;";
        sql += "update T_Test_Queston set questionTypeOrderBy=9999  where questionTypeOrderBy is null;";
        
        adoHelper.ExecuteSqlNonQuery(sql);
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
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter,string categoryId)
    {
        questionTypeOrderBy();
        string table = " T_Test_Queston ";
        string fields = "sysnumber,questionType,questionTitle,levelPoint,courseId,createTime,shFlag,'' passInfo,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        if (categoryId != "")
        {
            filter += " and categoryPath like '%" + categoryId + "%'";
        }
        string sort = "order by questionTypeOrderBy,orderBy";
        int totalRecords = 0;
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by sort asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource_Pass(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Replace("'' passInfo", "passInfo").Split(','), "sysnumber");
    }
    /// <summary>
    /// 列表检索（选择框）
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    public string ListDialogSelect(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string categoryId, string testSysnumber)
    {
        string table = " (SELECT a.*,b.GoodsName  FROM T_Test_Queston a left join T_Goods_Info b on a.courseId=b.GoodsId) vv";
        string fields = "sysnumber,GoodsName,questionType,questionTitle,mainQuestionSysnumber,levelPoint,courseId,createTime,shFlag,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        filter += " and shFlag=1 and isnull(isAL,0)=0  ";
        if (categoryId != "")
        {
            filter += " and categoryPath like '%" + categoryId + "%'";
        }
        if (testSysnumber != "")
        {
            filter += " and sysnumber in(select questionSysnumber from T_Test_day_Questions where testSysnumber='" + testSysnumber + "')";
        }
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by sort asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "sysnumber");
    }
    
    
    /// <summary>
    /// 列表检索（选择框）
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    public string ListDialog(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter, string categoryId, string testSysnumber)
    {
        string table = " (SELECT a.*,b.GoodsName  FROM T_Test_Queston a left join T_Goods_Info b on a.courseId=b.GoodsId) vv ";
        string fields = "sysnumber,GoodsName,questionType,questionTitle,mainQuestionSysnumber,levelPoint,courseId,createTime,shFlag,'操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        filter += " and shFlag=1 and isnull(isAL,0)=0 ";
        if (categoryId != "")
        {
            filter += " and categoryPath like '%" + categoryId + "%'";
        }
        if (testSysnumber != "")
        {
            //排查已经存在的
            filter += " and sysnumber not in(select questionSysnumber from T_Test_day_Questions where testSysnumber='" + testSysnumber + "')";
        }
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        int start = (Convert.ToInt32(curPage) - 1) * Convert.ToInt32(pageSize) + 1;
        int end = Convert.ToInt32(curPage) * Convert.ToInt32(pageSize);
        if (orderBy.Equals("cmd_col")) { sort = " order by sort asc "; }
        //DataTable dtSource = bll.GetListByPage(filter, orderBy + " " + orderByRole, start, end).Tables[0];
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "sysnumber");
    }

    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";
        if (hTable != null && hTable.Count > 0)
        {
            string SafeStr = "";
            if (hTable.Contains("questionTitle") && hTable["questionTitle"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["questionTitle"].ToString().Trim(), Int32.MaxValue);
                filter += " and questionTitle like '%" + SafeStr + "%'";
            } if (hTable.Contains("questionType") && hTable["questionType"].ToString().Trim() != "")
            {
                SafeStr = KillSqlIn.Form_ReplaceByString(hTable["questionType"].ToString().Trim(), Int32.MaxValue);
                filter += " and questionType like '%" + SafeStr + "%'";
            }
        }
        return filter;
    }

    public void EditDataSource_Pass(ref DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {

            DataSet ds = adoHelper.ExecuteSqlDataset("select goodsname from T_Goods_Info where GoodsId='" + dr["courseId"] + "'");
            dr["courseId"] = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["goodsname"] : "";

            DataSet dsPass = adoHelper.ExecuteSqlDataset("select * from View_Test_ErrorRecord_Percent where QuestionId='" + dr["sysnumber"] + "'");
            if (dsPass.Tables[0].Rows.Count > 0)
            {
                dr["passInfo"] = dsPass.Tables[0].Rows[0]["PassPecent"].ToString() + "%(" + dsPass.Tables[0].Rows[0]["TotalPass"].ToString() + "/" + dsPass.Tables[0].Rows[0]["TotalNum"].ToString() + ")";
            }
        }
    }
    
    public void EditDataSource(ref DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {

            DataSet ds = adoHelper.ExecuteSqlDataset("select goodsname from T_Goods_Info where GoodsId='" + dr["courseId"] + "'");
            dr["courseId"] = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["goodsname"] : "";
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

    
}