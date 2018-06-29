using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Order_OrderDetail : StarTech.Adapter.StarTechPage
{
    protected string OrderId = "";
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["orderid"] == null)
            return;
        OrderId = KillSqlIn.Form_ReplaceByString(Request.QueryString["orderid"], 50);
        if (OrderId == "")
            return;
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
        string strSQL = "select * from T_Order_Info where orderId='" + OrderId + "';";
        strSQL += "select * from T_Order_InfoDetail  where orderId='" + OrderId + "';";
        
        
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null)
            return;
        
        string strSQL2 = "";
        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        {
            strSQL2 += "select shopName from T_Shop_User where shopId='" + ds.Tables[1].Rows[i]["ProviderInfo"] + "'";
        }
        DataSet dsShop = adoHelper.ExecuteSqlDataset(strSQL2);
        List<string> shopNameList = new List<string>();
        for (int i = 0; i < dsShop.Tables.Count; i++)
        {
            if (dsShop.Tables[i].Rows.Count > 0)
            {
                shopNameList.Add(dsShop.Tables[i].Rows[0][0].ToString());
            }
            else
            {
                shopNameList.Add("");
            }
        }
        lbRemarks.Text = ds.Tables[0].Rows[0]["MemberOrderRemarks"].ToString();
        lbOrderId.Text = ds.Tables[0].Rows[0]["orderId"].ToString();
        lbMember.Text = ds.Tables[0].Rows[0]["memberName"].ToString();
        lbOrderTime.Text = ds.Tables[0].Rows[0]["OrderTime"].ToString();
        lbMoney.Text = ds.Tables[0].Rows[0]["OrderAllMoney"].ToString();
        lbReceiveAddress.Text = ds.Tables[0].Rows[0]["ReceivePerson"].ToString() + " " + ds.Tables[0].Rows[0]["ReceiveAddressCode"].ToString().Replace("|","") + " " + ds.Tables[0].Rows[0]["ReceiveAddressDetail"].ToString() + " " + ds.Tables[0].Rows[0]["PostCode"].ToString();
        llFreight.Text = ds.Tables[0].Rows[0]["Freight"].ToString();
        llRemark.Text = GetMemberPrice(ds.Tables[0].Rows[0]["orderId"].ToString());
        this.txtPayLogInfo.Text = PayLog(ds.Tables[0].Rows[0]["orderId"].ToString());
        string statu = "";

        string strPost = "select * from T_Postman_Task where orderId='" + ds.Tables[0].Rows[0]["orderId"].ToString()+"';";
        DataSet dsPost = adoHelper.ExecuteSqlDataset(strPost);
        string postMan = "";
        if (dsPost != null && dsPost.Tables.Count > 0 && dsPost.Tables[0].Rows.Count > 0)
        {
            postMan = dsPost.Tables[0].Rows[0]["postman_name"].ToString();
        }

        if (ds.Tables[0].Rows[0]["ispay"].ToString() != "1" && ds.Tables[0].Rows[0]["isComplete"].ToString() != "1")
        {
            statu = "未付款";
        }
        else if (ds.Tables[0].Rows[0]["ispay"].ToString() == "1" && ds.Tables[0].Rows[0]["isComplete"].ToString() != "1")
        {
            statu = "已付款" + " \r\n付款时间:" + ds.Tables[0].Rows[0]["paytime"].ToString();
        }
        else if (ds.Tables[0].Rows[0]["isComplete"].ToString() == "1")
        {
            statu = "已完成" + " \r\n付款时间:" + ds.Tables[0].Rows[0]["paytime"].ToString() + "\r\n完成时间：" + ds.Tables[0].Rows[0]["CompleteTime"].ToString() + "";
        }



        lbStatu.Text = statu;


        //if ((ds.Tables[0].Rows[0]["SellerId"] != DBNull.Value && ds.Tables[0].Rows[0]["SellerId"].ToString() != ""))
        //{
        //    strSQL = "select shopName from T_Shop_User where shopid='" + ds.Tables[0].Rows[0]["SellerId"].ToString() + "';";
        //    DataSet ds2 = adoHelper.ExecuteSqlDataset(strSQL);
        //    if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //    {
        //        lbSeller.Text = ds2.Tables[0].Rows[0][0].ToString();
        //    }
        //    //lbSeller.Text = ds.Tables[0].Rows[0]["orderId"].ToString();
        //}
        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            string goodsList = "<table>";
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {

                goodsList += "<tr>";
                goodsList += "<td><img src='"+ds.Tables[1].Rows[i]["goodsPic"]+"' width='50' height='50' /></td>";
                goodsList += "<td>" + ds.Tables[1].Rows[i]["goodsName"] +" "+ds.Tables[1].Rows[i]["goodsformate"]+ "</td>";
                goodsList += "<td>" + ds.Tables[1].Rows[i]["quantity"] + "</td>";
                goodsList += "<td>" + ds.Tables[1].Rows[i]["allMoney"] + "</td>";
                goodsList += "<td>"+shopNameList[i]+"</td>";
                //goodsList += "<td></td>";
                //goodsList += "<td></td>";
                goodsList += "</tr>";
            }
            goodsList += "</table>";
            llGoods.Text = goodsList;
        }
    }


    protected string PayLog(string orderId)
    {
        string sql = @"SELECT TOP 1000 [paylogId]
                      ,[memberId]
                      ,[orderId]
                      ,[payStatu]
                      ,[payType]
                      ,[paymoney]
                      ,[payTime]
                      ,[payContext]
                      ,[remark]
                      ,[payOutTime]
                        FROM [T_Order_PayLog] where payStatu='pay' and orderId='" + orderId + "'";
      DataTable dt=  adoHelper.ExecuteSqlDataset(sql).Tables[0];
      return dt.Rows.Count > 0 ? dt.Rows[0]["remark"].ToString() : "";
    }

    /// <summary>
    /// 报价详情
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    protected string GetMemberPrice(string orderId)
    {
        string s = "";
        string sql = @"SELECT [PriceId]
      ,[JobType]
      ,[GoodsId]
      ,[MemberMobile]
      ,[MemberPrice]
      ,[CreateTime]
      ,[IsLast]
      ,[IsEmployerSelect]
      ,[EmployerSelectTime]
      ,[IsMemberConfirm]
      ,[MemberConfirmTime]
      ,b.TrueName
      FROM T_Goods_MemberPrice a,T_Member_Info b
      where a.MemberId=b.MemberId and a.GoodsId in(select GoodsId from T_Order_InfoDetail where OrderId='" + orderId + "')";
        sql += " order by a.MemberId";

        //Response.Write(sql);
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            s += "<table>";
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                s += "<tr><td>" + row["TrueName"] + "</td><td>" + row["CreateTime"] + "</td><td>" + row["MemberPrice"] + "</td><td>" + ((row["IsLast"].ToString() == "1") ? "最新" : "历史") + "</td><td>" + (row["IsEmployerSelect"].ToString() == "1" ? "选择" : "") + "</td></tr>";
            }
            s += "</table>";
        }
        return s;
    }
}