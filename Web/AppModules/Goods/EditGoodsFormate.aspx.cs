using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using StarTech.ELife.Goods;

using System.IO;
using StarTech.DBUtility;

public partial class ShopSeller_EditGoodsFormate : StarTech.Adapter.StarTechPage
{
    public string id;
    public string isShow;
    public string goodsId;
    protected string vipDs1;
    protected string vipDs2;

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ShopSeller_EditGoodsFormate));
        this.id = (Request["id"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["id"], 50);
        this.isShow = (Request["isShow"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByNumber(Request["isShow"], 10);
        this.goodsId = (Request["goodsId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["goodsId"], 50);
        //GetVipPrice();
        if (!IsPostBack)
        {
        }
        if (Request.QueryString["flag"] != null)
        {
            if (Request.QueryString["flag"] == "list")
            {
                string goodsid = Request.QueryString["goodsid"];
                Response.Clear();
                Response.Write(AjaxList(goodsid));
                Response.End();
            }
            if (Request.QueryString["flag"] == "del")
            {

                string a = Request.QueryString["sysnumber"]; 
                Response.Clear();
                Response.Write( AjaxDelete(a));
                Response.End();
               
            }
            if (Request.QueryString["flag"] == "save")
            {
                string a = Request.QueryString["sysnumber"]; 
                string b = Request.QueryString["info"];
                Response.Clear();
                Response.Write(AjaxSave(a,b));
                Response.End();
            }
        }

    }

    public string AjaxList(string goodsId)
    {
        string html = "";
        //DataTable dt = new TableObject("T_Goods_Formate").Util_GetList("*", "goodsId='" + goodsId + "' order by GoodsFormateValues");
        string strSQL = "select * from T_Goods_Formate where goodsid='" + goodsId + "' order by GoodsFormateValues;";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataTable dt = adohelper.ExecuteSqlDataset(strSQL).Tables[0];
        int flag = 1;
        foreach (DataRow row in dt.Rows)
        {
            html += "<div><input id='GoodsCode_" + flag + "' type='text' style='width:120px;' value='" + row["GoodsCode"].ToString() + "'>&nbsp;<input  id='GoodsFormateValues_" + flag + "' type='text' readonly='readonly' style='width:200px;' value='" + row["GoodsFormateValues"].ToString() + "'>&nbsp;<input id='price_" + flag + "' type='text'  style='width:80px;' class='price' value='" + row["price"].ToString() + "'>&nbsp;<input class='vipPrice1' id='vipprice1_" + flag + "' type='text'  style='width:80px;display:none;' value='" + row["vipprice1"].ToString() + "'/>&nbsp;<input id='vipprice2_" + flag + "' type='text'  style='width:80px;display:none;' class='vipPrice2' value='" + row["vipprice2"].ToString() + "'/>&nbsp; <input id='stock_" + flag + "' type='text'  style='width:80px;' value='" + row["stock"].ToString() + "'>&nbsp;<input type='button' value='保存'  onclick=\"saveOne('" + row["sysnumber"].ToString() + "','" + flag + "')\">&nbsp;<input type='button' value='删除' onclick=\"delOne('" + row["sysnumber"].ToString() + "')\"></div>";
            flag++;
        }
        return html;
    }

    public int AjaxDelete(string sysnumber)
    {
        string strSQL = "delete T_Goods_Formate where sysnumber='" + sysnumber + "';";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        return adohelper.ExecuteSqlNonQuery(strSQL);
    }


    public int AjaxSave(string sysnumber,string info)
    {
        string[] arr = info.Split('$');
        GoodsFormateBll bll = new GoodsFormateBll();
        GoodsFormateModel mod = bll.GetModel(sysnumber);
        mod.GoodsCode = arr[0];
        mod.Price = decimal.Parse(arr[1]);
        mod.Stock = int.Parse(arr[2]);
        return bll.Update(mod);
    }

    #region ajax
    [AjaxPro.AjaxMethod]
    public string Ajax_ListFormate2(string goodsId)
    {
        string html = "";
        //DataTable dt = new TableObject("T_Goods_Formate").Util_GetList("*", "goodsId='" + goodsId + "' order by GoodsFormateValues");
        string strSQL = "select * from T_Goods_Formate where goodsid='"+goodsId+"' order by GoodsFormateValues;";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataTable dt = adohelper.ExecuteSqlDataset(strSQL).Tables[0];
        int flag = 1;
        foreach (DataRow row in dt.Rows)
        {
            html += "<div><input id='GoodsCode_" + flag + "' type='text' style='width:120px;' value='" + row["GoodsCode"].ToString() + "'>&nbsp;<input  id='GoodsFormateValues_" + flag + "' type='text' readonly='readonly' style='width:200px;' value='" + row["GoodsFormateValues"].ToString() + "'>&nbsp;<input id='price_" + flag + "' type='text'  style='width:80px;' class='price' value='" + row["price"].ToString() + "'>&nbsp;<input class='vipPrice1' id='vipprice1_" + flag + "' type='text'  style='width:80px;' value='" + row["vipprice1"].ToString() + "'/>&nbsp;<input id='vipprice2_" + flag + "' type='text'  style='width:80px;' class='vipPrice2' value='" + row["vipprice2"].ToString() + "'/>&nbsp; <input id='stock_" + flag + "' type='text'  style='width:80px;' value='" + row["stock"].ToString() + "'>&nbsp;<input type='button' value='保存'  onclick=\"saveOne('" + row["sysnumber"].ToString() + "','" + flag + "')\">&nbsp;<input type='button' value='删除' onclick=\"delOne('" + row["sysnumber"].ToString() + "')\"></div>";
            flag++;
        }
        return html;
    }


    [AjaxPro.AjaxMethod]
    public int Ajax_Save(string sysnumber, string info)
    {
        string[] arr = info.Split('$');
        GoodsFormateBll bll = new GoodsFormateBll();
        GoodsFormateModel mod = bll.GetModel(sysnumber);
        mod.GoodsCode = arr[0];
        mod.Price = decimal.Parse(arr[1]);
        mod.Stock = int.Parse(arr[2]);
        return bll.Update(mod);
    }


    [AjaxPro.AjaxMethod]
    public int Ajax_Delete(string sysnumber)
    {
        string strSQL = "delete T_Goods_Formate where sysnumber='"+sysnumber+"';";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        return adohelper.ExecuteSqlNonQuery(strSQL);
        //return new TableObject("T_Goods_Formate").Util_DeleteBat("sysnumber='" + sysnumber + "'");
    }

    #endregion


    ///// <summary>
    ///// 获取会员折扣
    ///// </summary>
    //protected void GetVipPrice()
    //{
    //    string strSQL = "select * from T_Member_Level;";
    //    AdoHelper adohelper = StarTech.DBUtility.AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
    //    DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
    //    if (ds == null || ds.Tables.Count <= 0)
    //        return;
    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        if (ds.Tables[0].Rows[i]["levelname"].ToString() == "金牌")
    //            vipDs1 = ds.Tables[0].Rows[i]["shoppingDiscount"].ToString();
    //        if (ds.Tables[0].Rows[i]["levelname"].ToString() == "银牌")
    //            vipDs2 = ds.Tables[0].Rows[i]["shoppingDiscount"].ToString();
    //    }
    //}


    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        GetFormateInfo();
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');window.returnValue=1;window.close();</script>");
    }
    
    //protected void GetFormInfo(ref ModGoodsType mod)
    //{
    //   // mod.typeName = this.txtTitle.Value.Trim();
    //   // mod.orderby = this.ChangeToIntValue(this.txtOrder.Value.Trim());
    //    //mod.remarks = this.txtRemarks.Text.Trim();
    //}



    protected void GetFormateInfo()
    {
        string strSQL = "delete T_Goods_Formate where goodsId='" + goodsId + "';";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        adohelper.ExecuteSqlNonQuery(strSQL);
        //new TableObject("T_Goods_Formate").Util_DeleteBat("goodsId='" + this.goodsId + "'");
        GoodsFormateBll bll = new GoodsFormateBll();
        for (int i = 1; i < 100; i++)
        {
            if (Request["formate_code_" + i] == null) { break; }
            GoodsFormateModel mod = new GoodsFormateModel();
            mod.sysnumber = Guid.NewGuid().ToString();
            mod.GoodsCode = Request["formate_code_" + i];
            mod.GoodsId = this.goodsId;
            mod.Price = decimal.Parse(Request["formate_price_" + i]);
            mod.Stock = int.Parse(Request["formate_stock_" + i]);
            mod.GoodsFormateValues = Request["formate_info_" + i];
            mod.GoodsFormateNames = Request["formateAllNames"];
            bll.Add(mod);
        }
    }
}
