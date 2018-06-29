using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AppModules_Order_App : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
    //http://192.168.2.41:2334/ServiceInterface/Goods/OrderList.ashx?flag=create&memberid=1&key=35f864bb-2b02-4992-a903-6cbc7d7568c8&marketId=1000000001
        string strSQL = "select memberId from T_Member_Info where tel='"+txtTel.Text+"';";
        string member=txtMemberId.Text;
        if (txtMemberId.Text == "" && txtTel.Text!="")
        {
            StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
            DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
            member = ds.Tables[0].Rows[0][0].ToString();
        }
        string strUrl = "http://elife2.hzst.com/ServiceInterface/Goods/OrderList.ashx?flag=create&memberid="+member+"&key=35f864bb-2b02-4992-a903-6cbc7d7568c8&marketId="+txtMarketId.Text+"";
        Response.Redirect(strUrl);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strUrl = "http://elife2.hzst.com/ServiceInterface/Index/Index.ashx?flag=index&marketId=" + txtMarketId.Text + "";
        Response.Redirect(strUrl);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string strUrl = "http://elife2.hzst.com/ServiceInterface/Goods/GoodsHandler.ashx?flag=goodsindextop&marketId=" + txtMarketId.Text + "";
        Response.Redirect(strUrl);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string strUrl = "http://elife2.hzst.com/ServiceInterface/Goods/GoodsHandler.ashx?flag=goodsdetail&goodsid="+this.txtGoodsID.Text+"&marketId=" + txtMarketId.Text + "";
        Response.Redirect(strUrl);
    }
}