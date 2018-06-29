<%@ WebHandler Language="C#" Class="PostmanHandler" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using StarTech.DBUtility;
using Startech.Member;
using System.Text;
using StarTech;
using System.Configuration;
using System.Data.SqlClient;

public class PostmanHandler : IHttpHandler
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string mainPath = ConfigurationManager.AppSettings["Source_NewsPic"];
    string type = string.Empty, callback = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        // context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/javascript";

        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string id = context.Request["id"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["id"].ToLower(), 10);
        string marketid = context.Request["marketid"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByNumber(context.Request["marketid"], 10));

        type = context.Request["type"] == null ? "" : context.Request["type"].ToLower();
        callback = context.Request["callback"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request["callback"], 20); //用于跨域调用
        //查询条件searchfilter,前台须encode下
        string searchfilter = context.Request["searchfilter"] == null ? "" : context.Server.UrlDecode(context.Request["searchfilter"]);

        //jggrid内部参数rows=10&page=2&sidx=id&sord=desc
        string rows = context.Request["rows"] == null ? "15" : context.Request["rows"];     //显示数量
        string page = context.Request["page"] == null ? "1" : context.Request["page"];      //当前页
        string sidx = context.Request["sidx"] == null ? "ShopId" : context.Request["sidx"];       //排序字段
        string sord = context.Request["sord"] == null ? "" : context.Request["sord"];   //排序规则


        //*********************商家注册数据

        string name = context.Request["name"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["name"], 50));  //用户名
        string pwd = context.Request["pwd"] == null ? "" : HttpUtility.UrlDecode(context.Request["pwd"]);     //密码
        string companyname = context.Request["cname"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["cname"], 80)); //公司名称
        string shopname = context.Request["sname"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["sname"], 50)); //店铺名称
        string linkman = context.Request["man"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["man"], 30));  //联系人 真实姓名
        string phone = context.Request["phone"] == null ? "" : context.Request["phone"];  //联系电话、手机
        string charter = context.Request["charter"] == null ? "" : context.Request["charter"];  //营业执照
        string area = context.Request["area"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["area"], 200)); //所在区域
        string market = context.Request["market"] == null ? "" : KillSqlIn.Url_ReplaceByNumber(context.Request["market"], 10); //所在区域
        string isown = context.Request["isown"] == null ? "" : KillSqlIn.Url_ReplaceByNumber(context.Request["isown"], 1); //是否自营
        string address = context.Request["address"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["address"], 200));//详细地址
        string clist = context.Request["clist"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["clist"], 200));//详细地址

        /**** 商家信息 营业执照上传 ********/
        string shopid = context.Request.Form["shopid"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.Form["shopid"].ToString(), 10);
        string method = context.Request.Form["method"] == null ? "" : KillSqlIn.Url_ReplaceByString(context.Request.Form["method"].ToString(), 10);
        HttpPostedFile file = context.Request.Files["Filedata"]; //接收图片数据

        if (method == "charter" && shopid != "" && file != null)
        {
            SaveCharpterImage(shopid, file);  //营业执照上传 图片上传
        }

        //****************商家保证金申请
        string money = context.Request["money"] == null ? "0" : context.Request["money"];     //显示数量
        string mark = context.Request["mark"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByString(context.Request["mark"], 500));  //用户名


        string areaid = context.Request["areaid"] == null ? "" : HttpUtility.UrlDecode(KillSqlIn.Url_ReplaceByNumber(context.Request["areaid"], 10));  //地区编号

        if (flag == "list")
        {
            context.Response.Write(List(page, rows, sidx, sord, searchfilter));
        }
        else if (flag == "userinfo")
        {
            context.Response.Write(GetShopUserInfo(id));
        }
        else if (flag == "apply")
        {
            context.Response.Write(ShopUserApply(name, pwd, companyname, shopname, linkman, phone, charter, area, market, address, file, context, clist, isown));
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
        else if (flag == "data")
        {
            context.Response.Write(GetAreaData(areaid));
        }
        else if (flag == "cate")
        {
            context.Response.Write(GetCategory());
        }
        else if (flag == "market")
        {
            context.Response.Write(GetMarketData(areaid));
        }
        else if (flag == "areainfo")
        {
            context.Response.Write(GetAreaInfo(marketid));
        }

        else if (flag == "checkname")
        {
            context.Response.Write(CheckUserName(name));
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
        string table = "(select a.*,b.CategoryPath,b.CategoryName,(select Market_name from  dbo.T_Base_Market where Market_id=a.MarketId) as marketname,(select area_name from  dbo.T_Base_Area r,T_Base_Market m where r.area_id=m.Area_id and m.Market_id=a.MarketId) as areaname from T_Shop_User a left join T_Goods_Category b on a.CategoryId=b.CategoryId )v ";
        string fields = " ShopId,areaname,marketname,CompanyName,ShopName,CategoryName,LinkMan,Phone,AccoutsState,isOpen, '操作' as cmd_col";//字段顺序和必须前台jggrid设置的一样
        string filter = GetFilter(searchFilter);
        string sort = "order by " + orderBy + " " + orderByRole + "";
        int totalRecords = 0;
        DataTable dtSource = new PaginationUtility().GetPaginationList(fields, table, filter, sort, Int32.Parse(curPage), Int32.Parse(pageSize), out totalRecords);
        EditDataSource(ref dtSource);
        int totalPages = JSONHelper.GetTotalPages(totalRecords, Int32.Parse(pageSize));
        return JSONHelper.ToJGGridJson(curPage, totalPages.ToString(), totalRecords.ToString(), dtSource, fields.Replace("'操作' as ", "").Split(','), "ShopId");
    }

    /// <summary>
    /// 验证用户是否已经存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private string CheckUserName(string name)
    {
        try
        {
            DataTable dt = DalBase.Util_GetList("select shopid from T_Shop_User where username='" + name + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        catch
        {
            return "0";
        }
    }

    /// <summary>
    /// 根据id返回用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetShopUserInfo(string id)
    {
        try
        {
            StringBuilder res = new StringBuilder();
            if (id != "")
            {

                DataTable dt = DalBase.Util_GetList("select * from T_Shop_User where ShopId='" + id + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return GetCallback(JSONHelper.GetJSON(dt));
                }
                else
                {
                    return GetCallback("[{\"error\":\"未找到该用户信息\"}]");
                }
            }
            else
            {
                return GetCallback("[{\"error\":\"参数错误，获取用户失败\"}]");
            }
        }
        catch (Exception ee)
        {
            return GetCallback("[{\"error\":\"服务器繁忙，请稍后重试\"}]");
        }

    }

    /// <summary>
    /// 商家注册
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Passwrod"></param>
    /// <param name="CompanyName"></param>
    /// <param name="ShopName"></param>
    /// <param name="LinkMan"></param>
    /// <param name="Phone"></param>
    /// <param name="Charter"></param>
    /// <param name="Address"></param>
    /// <param name="area"></param>
    /// <returns></returns>
    private string ShopUserApply(string UserName, string Passwrod, string CompanyName, string ShopName, string LinkMan, string Phone, string Charter, string area, string Marketid, string Address, HttpPostedFile file, HttpContext context, string clist, string isown)
    {
        try
        {
            if (CheckUserName(UserName) == "1")
            {
                return "该商家已存在！";
            }
            BllShopUser bUer = new BllShopUser();
            ModShopUser mUser = new ModShopUser();
            string shipid = GetShopId();
            if (shipid == "")
            {
                return "服务器繁忙，请稍后再试！";
            }
            mUser.ShopId = shipid;
            mUser.UserName = UserName;
            mUser.Passwrod = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Passwrod, "MD5");
            mUser.CompanyName = CompanyName;
            mUser.ShopName = ShopName;
            mUser.LinkMan = LinkMan;
            mUser.Phone = Phone;
            mUser.Address = Address;
            mUser.area = area;
            mUser.MarketId = Marketid;
            mUser.ApplyTime = DateTime.Now;
            mUser.AccoutsState = "Unchecked";
            mUser.isOpen = 0;
            mUser.isOwnSeller = (isown == "1") ? 1 : 0;
            mUser.CategoryId = clist.Replace("$", ",").TrimEnd(',');

            if (bUer.Add(mUser) > 0)
            {
                return "资料已提交，请完善商家资料并审核！";
            }
            else
            {
                return "资料提交失败，请稍后再试！";
            }
        }
        catch (Exception ee)
        {
            return ee.Message;
        }
    }




    /// <summary>
    /// 获取新注册商家编号
    /// </summary>
    /// <returns></returns>
    private string GetShopId()
    {
        try
        {
            DataTable dt = DalBase.Util_GetList("select shopid from T_Shop_User order by shopid desc").Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows[0]["shopid"].ToString() != "")
            {
                return (Convert.ToInt32(dt.Rows[0]["shopid"].ToString()) + 1).ToString();
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
            dr["CategoryName"] = GetCategoryName(dr["shopid"].ToString());
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

    private string GetCategoryName(string shopid)
    {
        string names = string.Empty;
        try
        {
            DataTable dt = DalBase.Util_GetList(" select CategoryId from T_Shop_User where shopid='" + shopid + "'").Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows[0]["CategoryId"].ToString() != "")
            {
                DataTable dt2 = DalBase.Util_GetList(" select CategoryName from dbo.T_Goods_Category where CategoryId in('" + dt.Rows[0]["CategoryId"].ToString().Replace(",", "','") + "')").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        names += dt2.Rows[i]["CategoryName"].ToString() + ",";
                    }
                }
            }
        }
        catch
        {

        }
        if (names != "")
        {
            names.TrimEnd(',');
        }
        return names;
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

    public string GetCallback(string res)
    {
        if (callback != "")
        {
            return callback + "(" + res + ");";
        }
        else
        {
            return res;
        }
    }

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

    private string GetAreaInfo(string marketid)
    {
        try
        {
            DataTable dt = DalBase.Util_GetList("select Market_name,Area_id,(select area_name from T_Base_Area where area_id=m.Area_id) as area_name from dbo.T_Base_Market m  where Market_id='" + marketid + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return JSONHelper.GetJSON(dt);
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


    /// <summary>
    /// 获取图片数据
    /// </summary>
    /// <param name="PostedFile"></param>
    /// <returns></returns>
    public void SaveCharpterImage(string shopid, HttpPostedFile PostedFile)
    {
        #region 保存图片
        if (PostedFile != null)
        {
            string UploadFileLastName = PostedFile.FileName.Substring(PostedFile.FileName.LastIndexOf(".") + 1);//得到文件的扩展名

            if (!PubFunction.IsPic(UploadFileLastName))
            {
                JSUtility.Alert("上传图片格式不正确!");
            }
            Random rnd = new Random();
            string UpLoadFileTime = DateTime.Now.ToString("HHmmss") + rnd.Next(9999).ToString("0000"); //生成一个新的数图片名称
            string fileName = UpLoadFileTime + "." + UploadFileLastName;//产生上传图片的名称

            string SaveFile = DateTime.Now.ToString("yyyy/MM/dd/").Replace("-", "/");

            #region 设置保存的路径
            string SevedDirectory = System.Web.VirtualPathUtility.Combine(mainPath, SaveFile);
            string phydic = System.Web.HttpContext.Current.Server.MapPath(SevedDirectory);

            if (!System.IO.Directory.Exists(phydic))
            {
                System.IO.Directory.CreateDirectory(phydic);
            }
            #endregion

            PostedFile.SaveAs(phydic + "" + fileName);
            string url = (mainPath + SaveFile + fileName).Replace("~", "");
            int i = SaveChater(shopid, url, "yyzz");
            if (i > 0)
            {
                ado.ExecuteSqlNonQuery("update T_Shop_User set Charter='" + url + "' where shopid='" + shopid + "'");
            }

        }


        #endregion
    }

    /// <summary>
    /// 获取顶级任务分类
    /// </summary>
    /// <returns></returns>
    private string GetCategory()
    {
        DataTable dt = DalBase.Util_GetList("select CategoryId as id,CategoryName as name from  T_Info_Category where CategoryLevel=1").Tables[0];
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
    /// 保存上传的营业执照图片
    /// </summary>
    /// <param name="ShopId"></param>
    /// <param name="Charter"></param>
    /// <param name="PostImageType"></param>
    /// <returns></returns>
    private int SaveChater(string ShopId, string Charter, string PostImageType)
    {
        try
        {

            DataTable dt = DalBase.Util_GetList("select id from T_Shop_ApplyImage where shopid='" + ShopId + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return ado.ExecuteSqlNonQuery("update T_Shop_ApplyImage set Charter='" + Charter + "' where shopid='" + ShopId + "'");
            }
            else
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into T_Shop_ApplyImage (");
                strSql.Append("id,ShopId,Charter,AddTime,PostImageType)");
                strSql.Append(" values (");
                strSql.Append("@id,@ShopId,@Charter,@AddTime,@PostImageType)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.VarChar,100),
					new SqlParameter("@ShopId", SqlDbType.VarChar,100),
					new SqlParameter("@Charter", SqlDbType.VarChar,100),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@PostImageType", SqlDbType.VarChar,100)};
                parameters[0].Value = GetMaxID();
                parameters[1].Value = ShopId;
                parameters[2].Value = Charter;
                parameters[3].Value = DateTime.Now;
                parameters[4].Value = "yyzz";

                object o = ado.ExecuteSqlNonQuery(strSql.ToString(), parameters);
                if (o != null)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
        }
        catch
        {
            return 0;
        }

    }

    /// <summary>
    /// 商家营业执照表 编号
    /// </summary>
    /// <returns></returns>
    private string GetMaxID()
    {
        try
        {
            DataTable dt = DalBase.Util_GetList("select top 1 id from T_Shop_ApplyImage order by addtime desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                int i = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                return (i + 1).ToString();
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}