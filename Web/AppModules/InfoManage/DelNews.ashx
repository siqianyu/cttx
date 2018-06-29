<%@ WebHandler Language="C#" Class="DelNews" %>

using System;
using System.Web;
using StarTech.DBUtility;
using System.Data;
using System.Collections;

public class DelNews : IHttpHandler 
{


    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        string NewsId = context.Request["newsid"] == null ? "" : context.Request["newsid"].ToLower();
        string ArticleId=context.Request["ArticleId"] == null ? "" : context.Request["ArticleId"].ToLower();
        string CategoryId = context.Request["CategoryId"] == null ? "" : context.Request["CategoryId"].ToLower();
        string flag = context.Request["flag"] == null ? "" : context.Request["flag"].ToLower();

            if (flag == "delete"&&NewsId!="")
            {
                context.Response.Write(DeleteData(NewsId));
            }

            if (flag == "delete" && ArticleId != "")
            { 
               context.Response.Write(DeleteArticle(ArticleId));
            }

            if (flag == "delete" && CategoryId != "")
            {

                context.Response.Write(DeleteCategory(CategoryId));
            }
        
    }



    /// <summary>
    /// 删除新闻数据
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteData(string ids)
    {
        if (ids.IndexOf("|") < 0)
        {
            int i = ado.ExecuteSqlNonQuery("delete from T_News where Newsid in('" + ids.Replace(",", "','") + "')");
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
                    int res = ado.ExecuteSqlNonQuery("delete from T_News where NewsId="+id);
                }
            }
            return "true";
        }
    }



    /// <summary>
    /// 删除文章数据
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteArticle(string ids)
    {
        if (ids.IndexOf("|") < 0)
        {
            int i = ado.ExecuteSqlNonQuery("delete from T_Article where ArticleId in('" + ids.Replace(",", "','") + "')");
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
                    int res = ado.ExecuteSqlNonQuery("delete from T_Article where ArticleId=" + id);
                }
            }
            return "true";
        }
    }


    /// <summary>
    /// 删除新闻类别数据
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public string DeleteCategory(string ids)
    {
        if (ids.IndexOf("|") < 0)
        {
            int i = ado.ExecuteSqlNonQuery("delete from T_Category where CategoryId in('" + ids.Replace(",", "','") + "')");
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
                    int res = ado.ExecuteSqlNonQuery("delete from T_Category where CategoryId=" + id);
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