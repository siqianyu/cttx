using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Goods_GoodsList : StarTech.Adapter.StarTechPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hid_pgoodsid.Value = (Request["pGoodsId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["pGoodsId"], 50);
            this.hid_categoryId.Value = (Request["categoryId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["categoryId"], 50);
        }
    }


    /// <summary>
    /// 将未登录到农贸市场的任务同步到农贸市场
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btMarket_Click(object sender, EventArgs e)
    {
        string strShop = "select shopId from T_Shop_User where isdefault=1;";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataSet ds=adohelper.ExecuteSqlDataset(strShop);
        string strInsert = "";
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            return;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string strGoods = "select * from T_Goods_Info where goodsId not in (select goodsId from T_Shop_Goods where shopId='"+ds.Tables[0].Rows[i][0]+"');";
            DataSet ds2 = adohelper.ExecuteSqlDataset(strGoods);
            if (ds2 == null || ds2.Tables.Count < 1 || ds2.Tables[0].Rows.Count < 1)
                continue;
            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
            {
                string newGuid = Guid.NewGuid().ToString();
                string shopId = ds.Tables[0].Rows[i][0].ToString();
                string goodsId = ds2.Tables[0].Rows[j]["GoodsId"].ToString();
                string num = ds2.Tables[0].Rows[j]["Sotck"].ToString();
                string price = ds2.Tables[0].Rows[j]["SalePrice"].ToString();
                string isSell = ds2.Tables[0].Rows[j]["IsSale"].ToString();
                DateTime addt = DateTime.Now;
                string goodsCode = ds2.Tables[0].Rows[j]["GoodsCode"].ToString();
                if (num == "")
                    num = "0";
                if (price == "")
                    price = "0";
                if (isSell == "")
                    isSell = "0";
                strInsert += "insert into T_Shop_Goods values('" + newGuid + "','" + shopId + "','" + goodsId + "'," + num + "," + price + "," + isSell + ",'" + addt + "','" + goodsCode + "',0,0,0,0);";
         

            }
        }

        int rows = 0;
        if (strInsert != "")
            rows=adohelper.ExecuteSqlNonQuery(strInsert);

        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('同步任务共" + rows + "件');</script>");
               
    }
}