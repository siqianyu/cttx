<%@ WebHandler Language="C#" Class="GetGoodsHandler" %>

using System;
using System.Web;

public class GetGoodsHandler : IHttpHandler {

    StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string flag = context.Request["flag"] == null ? "" : StarTech.KillSqlIn.Url_ReplaceByString(context.Request.QueryString["flag"], 20);
        if (flag == "goods")
        {
            string shopId = context.Request["shopId"] == null ? "" : StarTech.KillSqlIn.Url_ReplaceByString(context.Request.QueryString["shopId"], 20);
            context.Response.Write(GetGoods(shopId));
        }
    }

    public string GetGoods(string shopId)
    {
        string strSQL = "declare @total int;"
        + "declare @num int;"
        + "set @num=0;"
        + "select @total=COUNT(1) from T_GOods_Info;"
        + "while(@num<@total) "
        + " begin declare @n int; "
        + " set @n=@total-@num;"
        + " declare @sql nvarchar(1000);"
        + "declare @gid nvarchar(50);"
        + "declare @gcode nvarchar(50);"
        + "declare @stock int;"
        + "declare @price decimal(10,2);"
+ "set @sql='select top 1 @gid=goodsId from(select top '+convert(varchar(50),@n)+N' * from V_Goods_Info order by GoodsId desc) v order by GoodsId asc; ';"

+ " Exec sp_executesql  @sql,N'@gid nvarchar(50) output',@gid output; "

+ " set @sql='select top 1 @gcode=goodsCode from(select top '+convert(varchar(50),@n)+N' * from V_Goods_Info order by GoodsId desc) v order by GoodsId asc; ';"

+ " Exec sp_executesql  @sql,N'@gcode nvarchar(50) output',@gcode output;"

+ " set @sql='select top 1 @stock=Sotck from(select top '+convert(varchar(50),@n)+N' * from V_Goods_Info order by GoodsId desc) v order by GoodsId asc; ';"

+ " Exec sp_executesql  @sql,N'@stock int output',@stock output;"

+ " set @sql='select top 1 @price=saleprice from(select top '+convert(varchar(50),@n)+N' * from V_Goods_Info order by GoodsId desc) v order by GoodsId asc; ';"

+ " Exec sp_executesql  @sql,N'@price decimal(10,2) output',@price output;"

+ " if((select count(*) from T_Shop_Goods where goodsid=@gid and shopid='"+shopId+"')<1)"

+" insert into T_Shop_Goods values(NEWID(),'" + shopId + "',@gid,@stock,@price,1,GETDATE(),@gcode,0,0,0,0);"

+" set @num=@num+1;"

+" end";
        return strSQL;
        int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
        if (rows > 0)
            return "success";
        return "fail";
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}