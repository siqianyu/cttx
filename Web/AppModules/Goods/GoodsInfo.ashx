<%@ WebHandler Language="C#" Class="HttpHandler_GoodsInfo" %>

using System;
using System.Web;
using System.Data;
using StarTech.ELife.Goods;
using StarTech.DBUtility;


public class HttpHandler_GoodsInfo : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string goodsId = context.Request["goodsId"] == null ? "" : context.Request["goodsId"];
        string method = context.Request["method"] == null ? "" : context.Request["method"].ToLower();
        if (method == "goodspics")
        {
            //context.Response.Write(ListGoodsPics(goodsId));
        }
        else if (method == "moreproperty")
        {
            context.Response.Write(ListMorePropertyInfo(goodsId));
        }
        else if (method == "listformate")
        {
            context.Response.Write(ListFormate(goodsId));
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    


    /// <summary>
    /// 属性
    /// </summary>
    /// <param name="goodsId"></param>
    /// <returns></returns>
    public string ListMorePropertyInfo(string goodsId)
    {
        string html = "";
        DataTable dt = new GoodsBll().GetMorePropertyInfo(goodsId);
        if (dt != null && dt.Rows.Count > 0)
        {
            //DataTable dtValue = new TableObject("T_Goods_MoreProperty").Util_GetList("*", "goodsid='" + goodsId + "'");
            string strSQL = "select * from T_Goods_MoreProperty where goodsid='"+goodsId+"';";
            AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
            DataTable dtValue = adohelper.ExecuteSqlDataset(strSQL).Tables[0];
            int flag = 2;
            foreach (DataRow row in dt.Rows)
            {
                if (row["porpertyFlag"].ToString() == "input")
                {
                    if (flag % 2 == 0) { html += "<tr>"; }
                    flag++;
                    string v = (dtValue.Select("PropertyId='" + row["propertyId"].ToString() + "'").Length > 0) ? dtValue.Select("PropertyId='" + row["propertyId"].ToString() + "'")[0]["PropertyValue"].ToString() : "";
                    html += "<td class='Ltd'>" + row["propertyName"].ToString() + "：</td><td class='Rtd'>&nbsp;&nbsp;" + v + "</td>";
                    if (flag % 2 == 0) { html += "</tr>"; }
                }
            }
        }
        if (html != "" && html.EndsWith("</tr>") == false) { html += "<td  class='Ltd'>&nbsp;&nbsp;</td><td class='Rtd'>&nbsp;&nbsp;</td></tr>"; }
        return html;
    }

   /// <summary>
   /// 规格信息
   /// </summary>
   /// <param name="goodsId"></param>
   /// <returns></returns>
    public string ListFormate(string goodsId)
    {
        //DataTable dt = new TableObject("T_Goods_Formate").Util_GetList("*", "goodsId='" + goodsId + "'", "GoodsFormateValues");
        string strSQL = "select * from T_Goods_Formate where goodsid='" + goodsId + "';";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataTable dt = adohelper.ExecuteSqlDataset(strSQL).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string html = "<tr><td  style='background:#f5f6fa;padding:1px;' >货号</td><td style='background:#f5f6fa;padding:1px;' >规格组合信息</td><td style='background:#f5f6fa;padding:1px;' >价格</td><td style='background:#f5f6fa;padding:1px;' >库存数量</td></tr>";
            foreach (DataRow row in dt.Rows)
            {
                html += "<tr><td style='background:#fdfdff;padding:1px;'>" + row["GoodsCode"].ToString() + "</td><td style='background:#fdfdff;padding:1px;'>&nbsp;&nbsp;" + row["GoodsFormateValues"].ToString() + "</td><td  style='background:#fdfdff;padding:1px;'>" + row["price"].ToString() + "</td><td  style='background:#fdfdff;padding:1px;'>" + row["stock"].ToString() + "</td></tr>";
            }
            return html;
        }
        else
        {
            return "";
        }
    }
}