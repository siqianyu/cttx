<%@ WebHandler Language="C#" Class="DelLink" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using System.Collections;

public class DelLink : IHttpHandler 
{
    
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();
        string LinkId = context.Request["LinkId"] == null ? "-1" : context.Request["LinkId"].ToLower();

        
        if (flag == "delete" && LinkId != "")
        {
            context.Response.Write(DeleteLink(LinkId));
        }
        
    }



    /// <summary>
    /// 删除友情链接数据
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteLink(string ids)
    {
        if (ids.IndexOf("|") < 0)
        {
            int i = ado.ExecuteSqlNonQuery("delete from T_Link where LinkId in('" + ids.Replace(",", "','") + "')");
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
                    int res = ado.ExecuteSqlNonQuery("delete from T_Link where LinkId=" + id);
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