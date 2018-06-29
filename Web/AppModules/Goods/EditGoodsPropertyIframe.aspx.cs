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
using System.IO;
using StarTech.ELife.Goods;
using Startech.Utils;
using StarTech.DBUtility;

public partial class ShopSeller_EditGoodsPropertyIframe : StarTech.Adapter.StarTechPage
{
    public string id;
    public string isShow;


    protected void Page_Load(object sender, EventArgs e)
    {
        this.id = (Request["goodsId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["goodsId"], 50);
        this.isShow = (Request["isShow"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByNumber(Request["isShow"], 10);
        

        if (!IsPostBack)
        {
            CreateInfo(this.id);

            if (this.isShow == "1")
            {
                //this.SetReadOnlyPanel();
                this.btnSave.Visible = false;
            }
        }
    }
   

    protected void CreateInfo(string goodsId)
    {
        DataTable dt = new GoodsBll().GetMorePropertyInfo(goodsId);
        if (dt != null && dt.Rows.Count > 0)
        {
            string html = "";
            string allIds = "";
            string strSQL = "select * from T_Goods_MoreProperty where goodsid='" + goodsId + "';;";
            AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
            DataTable dtValue = adohelper.ExecuteSqlDataset(strSQL).Tables[0];
            //DataTable dtValue = new TableObject("T_Goods_MoreProperty").Util_GetList("*", "goodsid='" + goodsId + "'");
            foreach (DataRow row in dt.Rows)
            {
                if (row["porpertyFlag"].ToString() == "input")
                {
                    string v = (dtValue.Select("PropertyId='" + row["propertyId"].ToString() + "'").Length > 0) ? dtValue.Select("PropertyId='" + row["propertyId"].ToString() + "'")[0]["PropertyValue"].ToString() : "";
                    string uid = "MorePropertyId_" + row["propertyId"].ToString();
                    html += "<tr><td class='td_title'  style='width:100px;'>" + row["propertyName"].ToString() + "：</td><td class='td_info1' style='width:330px;'><input type='text' id='" + uid + "' name='" + uid + "' value='" + v + "' style='width:300px;height:16px;'></td></tr>";

                    allIds += uid + ",";
                }
            }
            if (allIds != "") { allIds = allIds.TrimEnd(','); }
            html += "<input type='hidden' name='MorePropertyId_AllIds' value='" + allIds + "'>";
            this.ltHtml.Text = html;
        }
        else
        {
            this.btn_1.Visible = false;
            this.btn_2.Visible = true;
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request["MorePropertyId_AllIds"] != null && Request["MorePropertyId_AllIds"] != "")
        {
            StarTech.DBUtility.AdoHelper ado = StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance);
            ado.ExecuteSqlNonQuery("delete  T_Goods_MoreProperty where goodsId='" + this.id + "'");
            foreach (string s in Request["MorePropertyId_AllIds"].Split(','))
            {
                if (Request[s] != null)
                {
                    string k = s.Replace("MorePropertyId_", "");
                    string v = Request[s];
                    string sqladd = "insert into T_Goods_MoreProperty(sysnumber,GoodsId,PropertyId,PropertyValue)";
                    sqladd += "values('" + Guid.NewGuid().ToString() + "','" + this.id + "','" + k + "','" + v + "')";
                    ado.ExecuteSqlNonQuery(sqladd);
                }
            }
            UpdateGoodsTable(this.id);
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');window.returnValue=1;window.close();</script>");
    }


    /// <summary>
    /// 更新任务表中的自定义集合字段(把所有自定义字段拼接成一个大字符串保存)
    /// </summary>
    protected void UpdateGoodsTable(string goodsId)
    {
        string info = "";
        StarTech.DBUtility.AdoHelper ado = StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance);
        DataSet ds = ado.ExecuteSqlDataset("select * from T_Goods_MoreProperty where goodsId='" + this.id + "'");
        DataSet dsSet = ado.ExecuteSqlDataset("select * from T_Goods_MorePropertySet");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            DataRow[] rowsSet = dsSet.Tables[0].Select("PropertyId='" + row["PropertyId"].ToString() + "'");
            if (rowsSet.Length > 0)
            {
                info += "{" + row["PropertyId"].ToString() + "$" + rowsSet[0]["propertyName"].ToString() + "$" + row["PropertyValue"].ToString().Replace("'", "’") + "}";
            }
        }
        ado.ExecuteSqlNonQuery("update T_Goods_Info set MorePropertys='" + info + "' where goodsId='" + goodsId + "'");
    }
}
