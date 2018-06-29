<%@ WebHandler Language="C#" Class="GetCategory" %>

using System;
using System.Web;
using System.Data;

public class GetCategory : IHttpHandler {


    StarTech.DBUtility.AdoHelper adohelper = StarTech.DBUtility.AdoHelper.CreateHelper(Startech.Utils.AppConfig.DBInstance);
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request.Params["index"] == "update")
        {
            GetNowCategory(context);
            return;
        }
        
        
        GetNextCategory(context);
    }

    /// <summary>
    /// 获取当前选项
    /// </summary>
    /// <param name="context"></param>
    void GetNowCategory(HttpContext context)
    {
        string[] strPath = context.Request.Params["strPath"].ToString().Split('|');
        string strSQL = "select categoryID,categoryName from T_Info_Category where categorylevel=1;";
        for (int i = 0; i < strPath.Length; i++)
        {
            strSQL += "select categoryID,categoryName from T_Info_Category where pcategoryID='" + strPath[i] + "';";
        }
        string check = "";
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            return;
        string selecthtml = "";
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if (ds.Tables[i].Rows.Count <= 0)
                break;
            selecthtml += "<select  class='cateSelect' id='cateSelect" + (i + 1) + "'><option value='-1'>未选择</option>";
            for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
            {
                if (strPath.Length > i && ds.Tables[i].Rows[j]["categoryid"].ToString() == strPath[i])
                {
                    check = "selected='selected' ";
                }
                else
                {
                    check = "";
                }
                selecthtml += "<option value='" + ds.Tables[i].Rows[j]["categoryid"] + "' "+check+">" + ds.Tables[i].Rows[j]["categoryname"] + "</option>";
            }
            selecthtml += "</select>";

        }
        context.Response.Write(selecthtml);
    }
    

    /// <summary>
    /// 获取下一级分类
    /// </summary>
    /// <param name="context"></param>
    void GetNextCategory(HttpContext context)
    {
        string strSQL = "select * from T_Info_Category where pcategoryid='"+StarTech.KillSqlIn.Form_ReplaceByString(context.Request.Params["categoryID"],10)+"'";
        DataSet ds=adohelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
        {
            context.Response.Write("");
            return;
        }
        string selecthtml = "<select class='cateSelect' id='cateSelect"+ds.Tables[0].Rows[0]["categorylevel"]+"'><option value='-1'>未选择</option>";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            selecthtml += "<option value='" + ds.Tables[0].Rows[i]["categoryID"] + "'>" + ds.Tables[0].Rows[i]["categoryName"] + "</option>";
        }
        selecthtml += "</select>";
        context.Response.Write(selecthtml);
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}