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

public partial class AppModules_IACenter_CouponGroupList : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            DataTable dt = adoHelper.ExecuteSqlDataset("select CouponId,CouponValue,EndTime from T_Base_Coupon where CouponType='抵用券'").Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                this.ddlCoupon.Items.Add(new ListItem("【抵用券】金额(" + row["CouponValue"].ToString() + ")有效期(" + DateTime.Parse(row["EndTime"].ToString()).ToString("yy-MM-dd") + ")", row["CouponId"].ToString()));
            }
        }
    }



    protected void btnExcel_Click(object sender, EventArgs e)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string filter = " 1=1 ";
       
        //if (this.txtName.Value.Trim() != "")
        //{
        //    filter += " and TrueName like '%" + this.txtName.Value.Trim() + "%'";
        //}
        //if (this.txtMemberId.Value.Trim() != "")
        //{
        //    filter += " and MemberId in (select memberid from t_member_info where membername like '%" + this.txtMemberId.Value.Trim() + "%' )";
        //} 

        string sql = "select orderId,paymoney,payTime,OrderType,OrderTime,ReceivePerson,Tel,payGoodsDetail from V_Order_PayLog where " + filter;

        DataSet ds = adoHelper.ExecuteSqlDataset(sql);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            dr["OrderType"] = dr["OrderType"].ToString() == "QB" ? "全包" : "点工";
            dr["payGoodsDetail"] = GetGoodsData(dr["orderId"].ToString());
        }

        ExcelReportIframe1_ReportEvent(ds.Tables[0], "支付记录");


        /*
        StringWriter sw = new StringWriter();
        //MemberId,TrueName,CardNumber,Moeny,ApplayTime,IfDeal,DealRemarks
        sw.WriteLine("订单编号\t支付金额\t支付时间\t订单类型\t下单时间\t支付人\t电话\t订单描述");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sw.WriteLine(dr["orderId"] + "\t" + dr["paymoney"] + "\t" + dr["payTime"] + "\t" + dr["OrderType"] + "\t" + dr["OrderTime"] + "\t" + dr["ReceivePerson"] + "\t" + dr["Tel"] + "\t" + dr["payGoodsDetail"]);
        }
        sw.Close();
        Response.AddHeader("Content-Disposition", "attachment; filename=支付记录.xls");
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(sw);
        Response.End();
         * */
    }

    public string GetGoodsData(string orderId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string s = "";
        DataSet ds = adoHelper.ExecuteSqlDataset("select GoodsName from T_Order_InfoDetail where OrderId='" + orderId + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            s += row["GoodsName"] + ";";
        }
        return s;
    }



    protected void ExcelReportIframe1_ReportEvent(DataTable dt, string excelfilename)
    {

        string ReportFields = "orderId$订单编号,paymoney$支付金额,payTime$支付时间,OrderType$订单类型,OrderTime$下单时间,ReceivePerson$支付人,Tel$电话,payGoodsDetail$订单描述";
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