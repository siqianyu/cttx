<%@ WebHandler Language="C#" Class="PostmanHandler" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using StarTech.DBUtility;
using StarTech;
using Startech.Member;

public class PostmanHandler : IHttpHandler
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "" : context.Request["id"].ToLower();
        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //*********************派送人员注册数据
        string postmanid = context.Request["postmanid"] == null ? "" : KillSqlIn.Url_ReplaceByNumber(HttpUtility.UrlDecode(context.Request["postmanid"]), 10);  //用户名
        string name = context.Request["name"] == null ? "" : KillSqlIn.Url_ReplaceByString(HttpUtility.UrlDecode(context.Request["name"]), 25);  //用户名
        string pwd = context.Request["pwd"] == null ? "" : KillSqlIn.Url_ReplaceByString(HttpUtility.UrlDecode(context.Request["pwd"]), 30);     //密码
        string truename = context.Request["tname"] == null ? "" : KillSqlIn.Url_ReplaceByString(HttpUtility.UrlDecode(context.Request["tname"]), 10); //真实姓名
        string tel = context.Request["tel"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByNumber(context.Request["tel"], 15)); //手机号码
        string marketid = context.Request["marketid"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByNumber(context.Request["marketid"], 10));  //农贸市场编号
        string status = context.Request["status"] == null ? "0" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByNumber(context.Request["status"], 1));  //农贸市场编号

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : context.Request["rows"];     //显示数量
        string page = context.Request["page"] == null ? "1" : context.Request["page"];      //当前页
        string sidx = context.Request["sidx"] == null ? "" : context.Request["sidx"];       //排序字段
        string sord = context.Request["sord"] == null ? "desc" : context.Request["sord"];   //排序规则



        string areaid = context.Request["areaid"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByNumber(context.Request["areaid"], 10));  //地区编号

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "info")
        {
            context.Response.Write(GetPostmanInfo(id));
        }
        else if (flag == "delete")
        {
            context.Response.Write(DeleteData(id));
        }
        else if (flag == "data")
        {
            context.Response.Write(GetAreaData(areaid));
        }
        else if (flag == "market")
        {
            context.Response.Write(GetMarketData(areaid));
        }
        else if (flag == "check")
        {
            //检测该用户是否存在
            context.Response.Write(CheckUserName(name));
        }
        else if (flag == "add")
        {
            //添加派送人
            context.Response.Write(AddPostMan(name, pwd, truename, tel, marketid, status));
        }
        else if (flag == "update")
        {
            //添加派送人
            context.Response.Write(UpdatePostMan(postmanid, name, pwd, truename, tel, marketid, status));
        }
    }

    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = " (select p.*,(select Market_name from  dbo.T_Base_Market where Market_id=p.postman_marketId) as marketname,(select area_name from  dbo.T_Base_Area r,T_Base_Market m where r.area_id=m.Area_id and m.Market_id=p.postman_marketId) as areaname from T_Postman_Info p) v ";
        string fields = "postman_id,areaname,marketname,postman_username,postman_trueName,postman_tel,postman_score,postman_status,postman_addtime, '操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "postman_id");
    }

    private string GetPostmanInfo(string id)
    {
        DataTable dt = DalBase.Util_GetList("select postman_username as name,postman_trueName as tname,postman_tel as tel,postman_status as status, postman_marketId as marketid,(select top 1 Market_name from dbo.T_Base_Market where Market_id=t.postman_marketId ) as mname from T_Postman_Info t where postman_id=" + id).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return JSONHelper.GetJSON(dt);
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 添加派送人
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pwd"></param>
    /// <param name="truename"></param>
    /// <param name="tel"></param>
    /// <param name="marketid"></param>
    /// <returns></returns>
    private string AddPostMan(string name, string pwd, string truename, string tel, string marketid, string status)
    {
        try
        {
            BllPostman bPostman = new BllPostman();
            DataTable dt = bPostman.GetList(" postman_username='" + name + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return "该用户已存在！";
            }

            ModPostman mPostman = new ModPostman();
            int postman_id = GetMaxPostManID();
            if (postman_id != 0)
            {

                mPostman.postman_id = postman_id;
                mPostman.postman_marketId = marketid;
                mPostman.postman_pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
                mPostman.postman_addtime = DateTime.Now;
                mPostman.postman_score = 0;
                mPostman.postman_status = status;
                mPostman.postman_tel = tel;
                mPostman.postman_trueName = truename;
                mPostman.postman_username = name;

                int i = bPostman.Add(mPostman);
                if (i > 0)
                {
                    return "添加成功！";
                }
                else
                {
                    return "添加失败！";
                }

            }
            else
            {
                return "服务器繁忙，请稍后再试！";
            }
        }
        catch (Exception ee)
        {
            return ee.Message;
        }

    }

    /// <summary>
    /// 修改派送人信息
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pwd"></param>
    /// <param name="truename"></param>
    /// <param name="tel"></param>
    /// <param name="marketid"></param>
    /// <returns></returns>
    private string UpdatePostMan(string id, string name, string pwd, string truename, string tel, string marketid, string status)
    {
        BllPostman bPostMan = new BllPostman();
        ModPostman mPostman = bPostMan.GetModel(Convert.ToInt32(id));
        if (mPostman != null)
        {
            mPostman.postman_marketId = marketid;
            if (pwd != "")
            {
                mPostman.postman_pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            }
            mPostman.postman_status = status;
            mPostman.postman_tel = tel;
            mPostman.postman_trueName = truename;

            int i = bPostMan.Update(mPostman);
            if (i > 0)
            {
                return "修改成功！";
            }
            else
            {
                return "修改失败！";
            }

        }
        else
        {
            return "服务器繁忙，请稍后再试！";
        }

    }

    /// <summary>
    /// 派送人最大的ID
    /// </summary>
    /// <returns></returns>
    private int GetMaxPostManID()
    {
        try
        {
            DataTable dt = DalBase.Util_GetList("select top 1 postman_id from dbo.T_Postman_Info order by postman_id desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["postman_id"]) + 1;
            }
            else
            {
                return 0;
            }
        }
        catch
        {
            return 0;
        }

    }

    /// <summary>
    /// 获取查询条件
    /// </summary>
    /// <param name="searchfilter"></param>
    /// <returns></returns>
    public string GetFilter(string searchfilter)
    {
        Hashtable hTable = JSONHelper.GetSearchFilter(searchfilter);
        string filter = " 1=1 ";

        if (hTable != null && hTable.Count > 0)
        {

            if (hTable.Contains("postman_trueName") && hTable["postman_trueName"].ToString().Trim() != "")
            {
                filter += " and postman_trueName like '%" + hTable["postman_trueName"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("postman_username") && hTable["postman_username"].ToString().Trim() != "")
            {
                filter += " and postman_username like '%" + hTable["postman_username"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("postman_status") && hTable["postman_status"].ToString().Trim() != "")
            {
                filter += " and postman_status= '" + hTable["postman_status"].ToString().Trim() + "'";
            }

        }
        return filter;
    }

    /// <summary>
    /// 省市区三级联动
    /// </summary>
    /// <param name="areaid"></param>
    /// <returns></returns>
    private string GetAreaData(string areaid)
    {
        string data = "<option value='0'>请选择</option>";
        try
        {

            DataTable dt = null;
            if (areaid == "")
            {
                //第一次加载
                dt = DalBase.Util_GetList("select area_id,area_name from T_Base_Area where area_level=2").Tables[0];
            }
            else if (areaid != "0")
            {
                //二级或三级
                dt = DalBase.Util_GetList("select area_id,area_name from T_Base_Area where area_pid='" + areaid + "'").Tables[0];
            }
            else
            {

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                data += "<option value='" + dr["area_id"].ToString() + "'>" + dr["area_name"].ToString() + "</option>";
            }

        }
        catch
        {
        }
        return data;
    }

    /// <summary>
    /// 返回 市场信息
    /// </summary>
    /// <param name="areaid"></param>
    /// <returns></returns>
    private string GetMarketData(string areaid)
    {
        string data = "<option value='0'>请选择</option>";
        try
        {

            DataTable dt = DalBase.Util_GetList("select Market_id,Market_name from dbo.T_Base_Market where Area_id='" + areaid + "'").Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                data += "<option value='" + dr["Market_id"].ToString() + "'>" + dr["Market_name"].ToString() + "</option>";
            }

        }
        catch
        {
        }
        return data;
    }

    /// <summary>
    /// 验证派送人是否已存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private string CheckUserName(string name)
    {
        BllPostman bPostman = new BllPostman();
        DataTable dt = bPostman.GetList(" postman_username='" + name + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 编辑数据源
    /// </summary>
    /// <param name="dt"></param>
    private void EditDataSource(ref DataTable dt)
    {

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["postman_status"] != null && dr["postman_status"].ToString() != "")
            {
                switch (dr["postman_status"].ToString())
                {
                    case "0":
                        dr["postman_status"] = "<font style='color:red'>禁用</font>";
                        break;
                    case "1":
                        dr["postman_status"] = "<font style='color:green'>正常</font>";
                        break;

                }
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
        if (ids.IndexOf(",") < 0)
        {
            int i = ado.ExecuteSqlNonQuery("delete from T_Postman_Info where postman_id='" + ids + "'");
            return i.ToString();
        }
        else
        {
            int i = ado.ExecuteSqlNonQuery("delete from T_Postman_Info where postman_id in('" + ids.Replace(",", "','") + "')");
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