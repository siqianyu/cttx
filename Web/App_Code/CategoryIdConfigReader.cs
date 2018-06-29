using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Data;

/// <summary>
///网站栏目配置文件读取类
/// </summary>
public class CategoryIdConfigReader
{
	public CategoryIdConfigReader()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 读取栏目编号
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
   public static string GetCategoryId(string categoryName)
   {
       XmlDocument xd = new XmlDocument();
       xd.Load(HttpContext.Current.Server.MapPath("~/CategoryIdConfig.xml"));
       XmlNodeList nodeList = xd.SelectNodes("/categoryconfig/categoryitem");
      
       for (int j = 0; j < nodeList.Count; j++)
       {
           if (nodeList[j].Attributes.Count>0)
           {
               if (categoryName.ToLower() == nodeList[j].Attributes[0].Value.ToLower().Trim())
               {
                   return nodeList[j].InnerText;
               }
           }

       }
       return "0";
   }

}