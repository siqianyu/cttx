using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using System.IO;
using System.Collections;

public partial class AppModules_Order_OrderList : StarTech.Adapter.StarTechPage
{
    protected string categoryId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["statu"] != null)
        {
            categoryId = "&statu="+StarTech.KillSqlIn.Form_ReplaceByString(Request.QueryString["statu"],20);
        }
    }



    protected void btnExcel_Click(object sender, EventArgs e)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string filter = " 1=1 ";
        if (this.txtDateBegin.Value != "") { filter += " and OrderTime >= '" + this.txtDateBegin.Value + "'"; }
        if (this.txtDateEnd.Value != "") { filter += " and OrderTime < '" + DateTime.Parse(this.txtDateEnd.Value).AddDays(1) + "'"; }
        //if (this.txtName.Value.Trim() != "")
        //{
        //    filter += " and TrueName like '%" + this.txtName.Value.Trim() + "%'";
        //}
        //if (this.txtMemberId.Value.Trim() != "")
        //{
        //    filter += " and MemberId in (select memberid from t_member_info where membername like '%" + this.txtMemberId.Value.Trim() + "%' )";
        //} 
        string statu = Request.QueryString["statu"];
        string filename = "全部";
        if (statu == "complete")
        {
            //完成
            filter += " and isComplete=1 ";
            filename = "已完成";
        }
        if (statu == "pay")
        {
            //已支付
            filter += " and ispay=1 and issend=0 ";
            filename = "已支付";
        }
        if (statu == "nopay")
        {
            //未支付
            filter += " and ispay=0 ";
            filename = "未支付";
        }
        if (statu == "send")
        {
            //已发货
            filter += " and issend=1 and isComplete=0 ";
            filename = "已发货";
        }




        string sql = "select orderId,OrderType,memberName,orderTime,orderAllMoney,'任务列表' as goodsList from T_Order_Info where " + filter;

        DataSet ds = adoHelper.ExecuteSqlDataset(sql);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            dr["goodsList"] = GetGoodsInfoByOrderId(dr["orderid"].ToString());
            dr["OrderType"] = dr["OrderType"].ToString() == "QB" ? "全包" : "点工";
        }


        ExcelReportIframe1_ReportEvent(ds.Tables[0], filename + "订单信息");


        /*
        StringWriter sw = new StringWriter();
        //MemberId,TrueName,CardNumber,Moeny,ApplayTime,IfDeal,DealRemarks
        sw.WriteLine("订单号\t订单类型\t接单用户名\t下单时间\t交易额\t任务内容");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sw.WriteLine(dr["orderId"] + "\t" + dr["OrderType"] + "\t" + dr["memberName"] + "\t" + dr["orderTime"] + "\t" + dr["orderAllMoney"] + "\t" + dr["goodsList"]);
        }
        sw.Close();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "订单信息.xls");
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(sw);
        Response.End();
         * */
    }

    public string GetGoodsInfoByOrderId(string orderId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string strSQL = "select goodsName from T_order_infoDetail where orderid='" + orderId + "';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL.ToString());
        string strGoods = "";
        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        {
            if (j != 0)
                strGoods += ",";
            strGoods += ds.Tables[0].Rows[j]["goodsName"].ToString();
        }
        return strGoods;
    }


    protected void ExcelReportIframe1_ReportEvent(DataTable dt, string excelfilename)
    {

        string ReportFields = "orderId$订单号,OrderType$订单类型,memberName$接单用户名,orderTime$下单时间,orderAllMoney$交易额,goodsList$任务内容";
        //创建字典
        ArrayList list = new ArrayList();
        string[] arr = ReportFields.Split(',');
        for (int i = 0; i < arr.Length; i++)
        {
            list.Add(arr[i]);
        }

        //创建要统计总数的数据
        Hashtable hTable = null;
        string ReportFieldsToCount = "";
        if (ReportFieldsToCount != "")
        {
            hTable = new Hashtable();
            string[] arrCount = ReportFieldsToCount.Split(',');
            for (int i = 0; i < arrCount.Length; i++)
            {
                hTable[arrCount[i]] = 0;
            }
        }

        //创建要显示为数字的数据
        string ReportFieldsToShowNumber = "";
        string[] toShowNumberArr = null;
        if (ReportFieldsToShowNumber != "")
        {
            toShowNumberArr = ReportFieldsToShowNumber.Split(',');
        }

        //导出
        System.IO.StringWriter sw = StarTech.AbcSettlement.BLL.ExcelHelper.CreateExcelTableByXml(dt, list, hTable);
        string filename = HttpUtility.UrlEncode("" + excelfilename + ".xls");
        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + "");
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Response.Write(sw);
        Response.End();

    }
}