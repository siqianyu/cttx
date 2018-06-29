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

public partial class AppModules_IACenter_AppayCash : StarTech.Adapter.StarTechPage
{
    public string showtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.showtype = Request["showtype"] == null ? "" : Request["showtype"];

        if (this.showtype == "99")
        {
            this.div_total.Visible = true;
        }
    }





    protected void btnExcel_Click(object sender, EventArgs e)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string filter = " 1=1 ";
        if (this.showtype == "1") { filter += " and IfDeal='1'and isnull(IsPayByUnline,'0')='0'"; }
        else if (this.showtype == "-1") { filter += " and IfDeal='-1'"; }
        else if (this.showtype == "0") { filter += " and IfDeal='0'"; }
        else if (this.showtype == "99") { filter += " and IsPayByUnline='1'"; }

        if (this.txtDateBegin.Value != "") { filter += " and ApplayTime >= '" + this.txtDateBegin.Value + "'"; }
        if (this.txtDateEnd.Value != "") { filter += " and ApplayTime < '" + DateTime.Parse(this.txtDateEnd.Value).AddDays(1) + "'"; }
        if (this.txtName.Value.Trim() != "")
        {
            filter += " and TrueName like '%" + this.txtName.Value.Trim() + "%'";
        }
        if (this.txtMemberId.Value.Trim() != "")
        {
            filter += " and MemberId in (select memberid from t_member_info where membername like '%" + this.txtMemberId.Value.Trim() + "%' )";
        } string sql = "select * from T_Member_ApplayCashBank where " + filter;

        DataSet ds = adoHelper.ExecuteSqlDataset(sql);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            object obj = adoHelper.ExecuteSqlScalar("select membername from t_member_info where memberid='" + dr["MemberId"] + "'");
            dr["MemberId"] = obj == null ? "无效会员" : obj.ToString();
        }
        /*
        StringWriter sw = new StringWriter();
        //MemberId,TrueName,CardNumber,Moeny,ApplayTime,IfDeal,DealRemarks
        sw.WriteLine("会员名\t姓名\t卡号\t金额\t申请时间\t备注");
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sw.WriteLine(dr["MemberId"] + "\t" + dr["TrueName"] + "\t" + dr["CardNumber"] + "\t" + dr["Moeny"] + "\t" + dr["ApplayTime"] + "\t" + dr["DealRemarks"]);
        }
        sw.Close();
        Response.AddHeader("Content-Disposition", "attachment; filename=已支付的申请.xls");
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(sw);
        Response.End();
        */

        ExcelReportIframe1_ReportEvent(ds.Tables[0], "已支付的申请");
    }




    protected void ExcelReportIframe1_ReportEvent(DataTable dt, string excelfilename)
    {

        string ReportFields = "MemberId$会员名,TrueName$姓名,CardNumber$卡号,Moeny$金额,ApplayTime$申请时间,DealRemarks$备注";
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