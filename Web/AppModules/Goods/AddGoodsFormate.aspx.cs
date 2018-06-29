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
using Startech.Utils;

public partial class AppModules_Goods_AddGoodsFormate :System.Web.UI.Page
{
    public string id;
    public string isShow;
    public string goodsId;
    public decimal postage;
    protected string vipDs1;
    protected string vipDs2;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AppModules_Goods_AddGoodsFormate));
        this.id = (Request["id"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["id"], 50);
        this.isShow = (Request["isShow"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByNumber(Request["isShow"], 10);
        this.goodsId = (Request["goodsId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["goodsId"], 50);
        //GetVipPrice();
        if (!IsPostBack)
        {
            InitForm();
            InitFormate2(this.goodsId);

        }

        if (Request.QueryString["flag"] != null)
        {
            string goodsid = Request.QueryString["goodsid"];
            string iteminfo = Server.UrlDecode(Request.QueryString["iteminfo"]);
            Response.Clear();
            Response.Write(ajaxList(goodsid, iteminfo));
            Response.End();
        }
    }

    ///// <summary>
    ///// 获取会员折扣
    ///// </summary>
    //protected void GetVipPrice()
    //{
    //    string strSQL = "select * from T_Member_Level;";
    //    AdoHelper adohelper = StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance);
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

    public string ajaxList(string goodsId, string itemInfo)
    {
        //最多支持3组规格信息
        string html = "";
        string f1 = "";
        string f2 = "";
        string f3 = "";
        string propertyNames = "";
        GoodsModel modGoods = new GoodsBll().GetModel(goodsId);
        ArrayList list = new ArrayList();
        if (modGoods != null)
        {
            itemInfo = itemInfo.TrimEnd('|').Replace(",,", ",");
            foreach (string item in itemInfo.Split('|'))
            {
                string[] propertyArr = item.Split('$');
                if (f1 == "") { f1 = propertyArr[1].TrimEnd(',').TrimStart(','); propertyNames += propertyArr[0] + ","; }
                else if (f2 == "") { f2 = propertyArr[1].TrimEnd(',').TrimStart(','); propertyNames += propertyArr[0] + ","; }
                else if (f3 == "") { f3 = propertyArr[1].TrimEnd(',').TrimStart(','); propertyNames += propertyArr[0] + ","; }
            }
            #region computer
            string computer = "";
            foreach (string fv1 in f1.Split(','))
            {
                computer = fv1 + ",";
                if (f2 != "")
                {
                    foreach (string fv2 in f2.Split(','))
                    {
                        computer = fv1 + "," + fv2 + ",";
                        if (f3 != "")
                        {
                            foreach (string fv3 in f3.Split(','))
                            {
                                computer += fv3 + ",";
                                list.Add(computer.TrimEnd(','));
                                computer = fv1 + "," + fv2 + ",";
                            }
                        }
                        else
                        {
                            list.Add(computer.TrimEnd(','));
                            computer = fv1 + ",";
                        }
                    }
                }
                else
                {
                    list.Add(computer.TrimEnd(','));
                }
            }
            #endregion

            if (list != null && list.Count > 0)
            {
                int flagI = 1;
                foreach (object s in list)
                {
                    html += "<div class='parDiv' postage='" + modGoods.Postage + "'><input id='formate_code_" + flagI + "' name='formate_code_" + flagI + "' type='text' style='width:120px;' value='" + modGoods.GoodsCode + "-" + flagI + "'>&nbsp;<input id='formate_info_" + flagI + "' name='formate_info_" + flagI + "' type='text' style='width:200px;' value='" + s.ToString() + "'>&nbsp;<input class='price' id='formate_price_" + flagI + "' name='formate_price_" + flagI + "' type='text' style='width:80px;' value='" + modGoods.SalePrice + "'>&nbsp;<input class='vipPrice1' id='formate_vip1_" + flagI + "' name='formate_vip1_" + flagI + "'  value='" + modGoods.vipPrice1 + "' style='width:80px;display:none' />&nbsp;<input class='vipPrice2' id='formate_vip2_" + flagI + "' value='" + modGoods.vipPrice2 + "' name='formate_vip2_" + flagI + "' style='width:80px;display:none;' />&nbsp;<input id='formate_stock_" + flagI + "' name='formate_stock_" + flagI + "' type='text' style='width:80px;' value='" + modGoods.Sotck + "'></div>";
                    flagI++;
                }
            }

        }
        return html + "<input type='hidden' value='" + propertyNames.TrimEnd(',') + "' name='formateAllNames'>";
    }


    #region ajax
    [AjaxPro.AjaxMethod]
    public string Ajax_ListFormate(string goodsId, string itemInfo)
    {
        //最多支持3组规格信息
        string html = "";
        string f1 = "";
        string f2 = "";
        string f3 = "";
        string propertyNames = "";
        GoodsModel modGoods = new GoodsBll().GetModel(goodsId);
        ArrayList list = new ArrayList();
        if (modGoods != null)
        {
            itemInfo=itemInfo.TrimEnd('|').Replace(",,",",");
            foreach (string item in itemInfo.Split('|'))
            {
                string[] propertyArr = item.Split('$');
                if (f1 == "") { f1 = propertyArr[1].TrimEnd(',').TrimStart(','); propertyNames += propertyArr[0] + ","; }
                else if (f2 == "") { f2 = propertyArr[1].TrimEnd(',').TrimStart(','); propertyNames += propertyArr[0] + ","; }
                else if (f3 == "") { f3 = propertyArr[1].TrimEnd(',').TrimStart(','); propertyNames += propertyArr[0] + ","; }
            }
            #region computer
            string computer = "";
            foreach (string fv1 in f1.Split(','))
            {
                computer = fv1 + ",";
                if (f2 != "")
                {
                    foreach (string fv2 in f2.Split(','))
                    {
                        computer = fv1 + "," + fv2 + ",";
                        if (f3 != "")
                        {
                            foreach (string fv3 in f3.Split(','))
                            {
                                computer += fv3 + ",";
                                list.Add(computer.TrimEnd(','));
                                computer = fv1 + "," + fv2 + ",";
                            }
                        }
                        else
                        {
                            list.Add(computer.TrimEnd(','));
                            computer = fv1 + ",";
                        }
                    }
                }
                else
                {
                    list.Add(computer.TrimEnd(','));
                }
            }
            #endregion

            if (list != null && list.Count > 0)
            {
                int flagI = 1;
                foreach (object s in list)
                {
                    html += "<div class='parDiv' postage='"+modGoods.Postage+"'><input id='formate_code_" + flagI + "' name='formate_code_" + flagI + "' type='text' style='width:120px;' value='" + modGoods.GoodsCode + "-" + flagI + "'>&nbsp;<input id='formate_info_" + flagI + "' name='formate_info_" + flagI + "' type='text' style='width:200px;' value='" + s.ToString() + "'>&nbsp;<input class='price' id='formate_price_" + flagI + "' name='formate_price_" + flagI + "' type='text' style='width:80px;' value='" + modGoods.SalePrice + "'>&nbsp;<input class='vipPrice1' id='formate_vip1_" + flagI + "' name='formate_vip1_" + flagI + "'  value='" + modGoods.vipPrice1 + "' style='width:80px' />&nbsp;<input class='vipPrice2' id='formate_vip2_" + flagI + "' value='" + modGoods.vipPrice2 + "' name='formate_vip2_" + flagI + "' style='width:80px' />&nbsp;<input id='formate_stock_" + flagI + "' name='formate_stock_" + flagI + "' type='text' style='width:80px;' value='" + modGoods.Sotck + "'></div>";
                    flagI++;
                }
            }

        }
        return html + "<input type='hidden' value='" + propertyNames.TrimEnd(',') + "' name='formateAllNames'>";
    }
    #endregion

    protected void InitForm()
    {
        if (this.id != "")
        {
            GoodsTypeModel mod = new GoodsTypeBll().GetModel(this.id);

            if (mod != null)
            {
               // this.txtTitle.Value = mod.typeName;
                //this.txtOrder.Value = mod.orderby.ToString();
                //this.txtRemarks.Text = mod.remarks;
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        GetFormateInfo();
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');window.returnValue=1;window.close();</script>");
    }
    
    protected void GetFormInfo(ref GoodsModel mod)
    {
       // mod.typeName = this.txtTitle.Value.Trim();
       // mod.orderby = this.ChangeToIntValue(this.txtOrder.Value.Trim());
        //mod.remarks = this.txtRemarks.Text.Trim();
    }

    protected void InitFormate(string goodsId)
    {
        GoodsModel modGoods = new GoodsBll().GetModel(goodsId);
        postage = modGoods.Postage.Value;
        if (modGoods != null)
        {
            string html = "";
            string html2 = "";
            string strSQL = "select * from T_Goods_MoreProperty where typeId='" + modGoods.GoodsToTypeId + "' and flag='select' order by orderBy asc;";
            AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
            DataTable dt = adohelper.ExecuteSqlDataset(strSQL).Tables[0];
            //DataTable dt = new TableObject("T_Goods_TypeProperty").Util_GetList("*", "typeId='" + modGoods.GoodsToTypeId + "' and flag='select'", "orderBy asc");
            foreach (DataRow row in dt.Rows)
            {
                string uid = "MorePropertyId_" + row["propertyId"].ToString();
                html2 += uid + "$" + row["propertyName"].ToString() + ",";
                if (row["propertyValues"].ToString() != "")
                {
                    string vs = "";
                    foreach (string s in row["propertyValues"].ToString().Split(','))
                    {
                        string vid = Guid.NewGuid().ToString();
                        vs += "<input type='checkbox' id='" + vid + "' onclick=\"selectPropertyValue('" + uid + "','" + vid + "','" + s + "')\" >" + s + "&nbsp;&nbsp;";
                    }
                    html += "<div>" + row["propertyName"].ToString() + "：" + vs + "<input type='hidden' id='" + uid + "' name='" + uid + "'></div>";
                }
            }
            if (html2 != "") { html2 = "<input type='hidden' id='MorePropertyInfo' name='MorePropertyInfo' value='" + html2.TrimEnd(',') + "'>"; }
            this.ltHtml.Text = html + html2;
        }
    }

    protected void InitFormate2(string goodsId)
    {
        string html = "";
        string html2 = "";
        DataTable dt = new GoodsBll().GetMorePropertyInfo(goodsId);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["porpertyFlag"].ToString() == "select")
                {

                    string uid = "MorePropertyId_" + row["propertyId"].ToString();
                    html2 += uid + "$" + row["propertyName"].ToString() + ",";
                    if (row["propertyOptions"].ToString() != "")
                    {
                        string vs = "";
                        foreach (string s in row["propertyOptions"].ToString().Split(','))
                        {
                            string vid = Guid.NewGuid().ToString();
                            vs += "<input type='checkbox' id='" + vid + "' onclick=\"selectPropertyValue('" + uid + "','" + vid + "','" + s + "')\" >" + s + "&nbsp;&nbsp;";
                        }
                        html += "<div>" + row["propertyName"].ToString() + "：" + vs + "<input type='hidden' id='" + uid + "' name='" + uid + "'></div>";
                    }
                }
            }
            if (html2 != "") { html2 = "<input type='hidden' id='MorePropertyInfo' name='MorePropertyInfo' value='" + html2.TrimEnd(',') + "'>"; }
            this.ltHtml.Text = html + html2;
        }
    }

    protected void GetFormateInfo()
    {
        //new TableObject("T_Goods_Formate").Util_DeleteBat("goodsId='" + this.goodsId + "'");
        string strSQL = "delete T_Goods_Formate where goodsId='" + goodsId + "';";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        adohelper.ExecuteSqlNonQuery(strSQL);
        GoodsFormateBll bll = new GoodsFormateBll();
        for (int i = 1; i < 100; i++)
        {
            if (Request["formate_code_" + i] == null) { break; }
            GoodsModel modGoods = new GoodsBll().GetModel(goodsId);
            postage = modGoods.Postage.Value;
            GoodsFormateModel mod = new GoodsFormateModel();
            mod.sysnumber = Guid.NewGuid().ToString();
            mod.GoodsCode = Request["formate_code_" + i];
            mod.GoodsId = this.goodsId;
            mod.Price = decimal.Parse( Request["formate_price_" + i]);
            mod.Stock = int.Parse(Request["formate_stock_" + i]);
            mod.GoodsFormateValues = Request["formate_info_" + i];
            //mod.Postage = this.postage;
            //mod.vipPrice1 = Convert.ToDecimal(Request["formate_vip1_" + i]);
            //mod.vipPrice2 = Convert.ToDecimal(Request["formate_vip2_" + i]);
            mod.GoodsFormateNames = Request["formateAllNames"];
            bll.Add(mod);
        }
    }
}
