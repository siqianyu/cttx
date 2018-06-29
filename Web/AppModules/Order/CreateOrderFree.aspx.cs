using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Order_CreateOrderFree : StarTech.Adapter.StarTechPage
{
    protected string OrderId = "";
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hidGoodsIds.Value = Request["GoodsIds"] == null ? "" : Request["GoodsIds"];

        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    /// <summary>
    /// 绑定信息
    /// </summary>
    protected void BindInfo()
    {
 
        DataSet ds = adoHelper.ExecuteSqlDataset("select * from T_Goods_Info  where [GoodsId] in('" + this.hidGoodsIds.Value.Replace(",", "','") + "');");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string goodsList = "<table>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                goodsList += "<tr>";
                goodsList += "<td><img src='" + ds.Tables[0].Rows[i]["GoodsSmallPic"] + "' width='50' height='50' /></td>";
                goodsList += "<td>" + ds.Tables[0].Rows[i]["GoodsName"] + "</td>";
                goodsList += "</tr>";
            }
            goodsList += "</table>";
            llGoods.Text = goodsList;
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        OrderHelper order = new OrderHelper();
        int total = 0;
        foreach (string gid in this.hidGoodsIds.Value.Split(','))
        {
            foreach (string mid in this.hidMemberIds.Value.Split(','))
            {
                if (gid != "" && mid != "")
                {
                    string r = order.AddOrderByFree(mid, gid, "");
                    if (r.IndexOf("error") == -1)
                    {
                        System.Threading.Thread.Sleep(1010);//防止订单重复
                        total++;
                    }
                }
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('成功创建" + total + "个订单');ayer_close();</script>");
    }
}