<%@ WebHandler Language="C#" Class="LeftMenuTree" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;

public class LeftMenuTree : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request["pid"] != null && context.Request["userid"] != null)
        {
            context.Response.Write(BindSubMenu(context.Request["pid"], context.Request["userid"]));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    protected string BindSubMenu(string pId, string userId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = new StarTech.Adapter.IACenter().GetAllMenusByUserId(int.Parse(userId));
        //DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_Menu where parentMenuId=" + pId + " order by orderIndex");
        DataRow[] rows = ds.Tables[0].Select("parentMenuId=" + pId + "");

        string s = "";
        for (int i = 0; i < rows.Length; i++)
        {
            DataRow row = rows[i];
            string css = i == 0 ? "Act current" : "Act";
            s += "<p id='p_menu_" + row["uniqueId"] + "' class='" + css + "' onclick=\"leftmenuclick('p_menu_" + row["uniqueId"] + "')\">" + row["menuName"] + "</p>";

            DataRow[] rows2 = ds.Tables[0].Select("parentMenuId=" + row["uniqueId"] + "");
            if (rows2.Length > 0)
            {
                s += "<ul>";
                foreach (DataRow row2 in rows2)
                {
                    s += "<li><a href='" + row2["menuTarget"] + "' target='Main'>" + row2["menuName"] + "</a></li>";
                }
                s += "</ul>";
            }
        }
        return s;
    }

}