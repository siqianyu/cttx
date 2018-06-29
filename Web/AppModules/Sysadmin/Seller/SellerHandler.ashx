<%@ WebHandler Language="C#" Class="PostmanHandler" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using StarTech.DBUtility;
using Startech.Member;

public class PostmanHandler : IHttpHandler
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    string type = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "" : context.Request["id"].ToLower();
        type = context.Request["type"] == null ? "" : context.Request["type"].ToLower();
        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : context.Request["rows"];     //显示数量
        string page = context.Request["page"] == null ? "1" : context.Request["page"];      //当前页
        string sidx = context.Request["sidx"] == null ? "ShopId" : context.Request["sidx"];       //排序字段
        string sord = context.Request["sord"] == null ? "" : context.Request["sord"];   //排序规则

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "delete")
        {
            context.Response.Write(DeleteData(id));
        }
        else if (flag == "init")
        {
            context.Response.Write(InitPassword(id));
        }
        else if (flag == "open")
        {
            context.Response.Write(OpenShop(id));
        }
    }

    /// <summary>
    /// 获取商家列表
    /// </summary>
    /// <param name="curPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="orderByRole"></param>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    public string List(string curPage, string pageSize, string orderBy, string orderByRole, string searchFilter)
    {
        string table = "(select a.*,b.CategoryPath,b.CategoryName from T_Shop_User a left join T_Goods_Category b on a.CategoryId=b.CategoryId )v";
        string fields = " ShopId,CompanyName,ShopName,CategoryName,LinkMan,Phone,AccoutsState,isOpen, '操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "ShopId");
    }

    /// <summary>
    /// 重置商家密码
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string InitPassword(string id)
    {
        BllShopUser bUser = new BllShopUser();
        ModShopUser mUser = bUser.GetModel(id);
        if (mUser != null)
        {
            mUser.Passwrod = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("111111", "MD5");
        }
        int i = bUser.Update(mUser);
        if (i > 0)
        {
            return "密码重置成功！";
        }
        else
        {
            return "密码重置失败！";
        }


    }

    /// <summary>
    /// 店铺开启
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string OpenShop(string id)
    {
        BllShopUser bUser = new BllShopUser();
        ModShopUser mUser = bUser.GetModel(id);
        if (mUser != null)
        {
            mUser.isOpen = 1;
        }
        int i = bUser.Update(mUser);
        if (i > 0)
        {
            return "店铺开启成功！";
        }
        else
        {
            return "店铺开启失败！";
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

        if (type != "")
        {
            switch (type)
            {
                case "0"://账户信息未审核
                    filter += " and (AccoutsState = 'Unchecked' or AccoutsState = '' or AccoutsState is null) ";
                    break;
                case "1"://店铺正常
                    filter += " and AccoutsState = 'Normal' and isOpen = 1";
                    break;
                case "2"://店铺关闭
                    filter += " and AccoutsState='Normal' and (isOpen = 0 or isOpen ='' or isOpen is null) ";
                    break;
                case "3"://审核不通过的店铺
                    filter += " and AccoutsState='Unpass' ";
                    break;
                case "4"://禁用的商家
                    filter += " and AccoutsState='Disable' ";
                    break;
            }

        }

        if (hTable != null && hTable.Count > 0)
        {

            if (hTable.Contains("CompanyName") && hTable["CompanyName"].ToString().Trim() != "")
            {
                filter += " and CompanyName like '%" + hTable["CompanyName"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("ShopName") && hTable["ShopName"].ToString().Trim() != "")
            {
                filter += " and ShopName like '%" + hTable["ShopName"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("LinkMan") && hTable["LinkMan"].ToString().Trim() != "")
            {
                filter += " and LinkMan like '%" + hTable["LinkMan"].ToString().Trim() + "%'";
            }
            if (hTable.Contains("isOpen") && hTable["isOpen"].ToString().Trim() != "")
            {
                filter += " and isOpen = '" + hTable["isOpen"].ToString().Trim() + "'";
            }
            if (hTable.Contains("AccoutsState") && hTable["AccoutsState"].ToString().Trim() != "")
            {
                filter += " and AccoutsState = '" + hTable["AccoutsState"].ToString().Trim() + "'";
            }
        }
        return filter;
    }

    /// <summary>
    /// 编辑数据源
    /// </summary>
    /// <param name="dt"></param>
    private void EditDataSource(ref DataTable dt)
    {

        foreach (DataRow dr in dt.Rows)
        {
            //账户状态
            if (dr["AccoutsState"] != null && dr["AccoutsState"].ToString() != "")
            {

                switch (dr["AccoutsState"].ToString())
                {
                    case "Normal":
                        dr["AccoutsState"] = "<font style='color:green'>正常</font>";
                        break;
                    case "Disable":
                        dr["AccoutsState"] = "<font style='color:red'>禁用</font>";
                        break;
                    case "Unchecked":
                        dr["AccoutsState"] = "<font style='color:red'>未审核</font>";
                        break;
                    case "Unpass":
                        dr["AccoutsState"] = "<font style='color:red'>不通过</font>";
                        break;
                    default:
                        dr["AccoutsState"] = "<font style='color:red'>未审核</font>";
                        break;
                }
            }
            else
            {
                dr["AccoutsState"] = "<font style='color:red'>未审核</font>";
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