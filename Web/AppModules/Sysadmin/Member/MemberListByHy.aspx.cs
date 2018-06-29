using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class MemberList : StarTech.Adapter.StarTechPage
{
    public string flag = "", code, title;
    protected void Page_Load(object sender, EventArgs e)
    {

        InitTopButton();
        if (!IsPostBack)
        {
            flag = Request.QueryString["flag"] == null ? "" : Request.QueryString["flag"].ToString();
            code = Request.QueryString["code"] == null ? "" : Request.QueryString["code"].ToString();
        }
    }
    #region 通用按钮栏设置
    /// <summary>
    /// 初始化按钮栏
    /// </summary>
    protected void InitTopButton()
    {

        this.OutputExcel1.OutputExcelEvent += new Controls_OutputExcel.ExcelHandler(OutputExcel1_OutputExcelEvent);
        this.OutputExcel1.Visible = false;
        // this.Search1.AddClickEvent += new Sysadmin_Controls_Search.AddClickHandler(Search1_AddClickEvent);
    }



    void OutputExcel1_OutputExcelEvent(object sender, EventArgs e)
    {
        CreateExcel(DateTime.Now.ToShortDateString() + "年费会员列表");
    }
    #endregion

    #region 导出Excel
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="name">文件名称</param>
    private void CreateExcel(string name)
    {
        //DataTable thisTable = DalBase.Util_GetList("select * from T_Member_Info where exists(select levelId from T_Member_Level where levelId=memberLevel and moneyType='Year')").Tables[0];
        //if (thisTable != null)
        //{
        //    StringWriter sw = new StringWriter();
        //    sw.WriteLine("用户名\t公司名称\t可托管标准数\t可下载标准数\t有效时间\t会员状态");
        //    foreach (DataRow dr in thisTable.Rows)
        //    {
        //        sw.WriteLine(dr["memberName"] + "\t" + dr["memberCompanyName"].ToString() + "\t"
        //            + dr["TrustNumber"].ToString() + "\t" + dr["DownloadNumber"] + "\t" + string.Format("{0:yyyy-MM-dd}", dr["levelServiceStartTime"]) + "至" + string.Format("{0:yyyy-MM-dd}", dr["levelServiceEndTime"]) + "\t"
        //            + (dr["memberStatus"].ToString() == "ZC" ? "正常" : "禁用"));
        //    }
        //    sw.Close();
        //    Response.Charset = "GB2312";
        //    string filename = name;
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename) + ".xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        //    Response.Write(sw);
        //    Response.End();
        //}

        DataTable thisTable = DalBase.Util_GetList("select * from T_Member_Info " + SetFilter()).Tables[0];

        if (thisTable != null)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("会员名称\t性别\t真实姓名\t会员类型\t单位名称\t联系电话\t会员状态\t注册时间\t现金账户\t赠送的账户");
            foreach (DataRow dr in thisTable.Rows)
            {
                sw.WriteLine(dr["memberName"] + "\t" + dr["sex"].ToString() + "\t" + dr["memberTrueName"].ToString() + "\t" + GetMemberType(dr["memberType"].ToString()) + "\t" + dr["memberCompanyName"].ToString() + "\t" + dr["tel"].ToString() + "\t" + GetMemberStatus(dr["memberStatus"].ToString()) + "\t" + string.Format("{0:yyyy-MM-dd}", dr["regTime"]) + "\t" + (dr["buyMoneyAccount"].ToString()) + dr["freeMoenyAccount"].ToString());
            }
            sw.Close();
            Response.Charset = "GB2312";
            string filename = name;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename) + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Write(sw);
            Response.End();
        }
    }

    public string GetMemberType(string strMemType)
    {
        string strValue = "";
        if (strMemType == "Gov")
            strValue = "政府";
        if (strMemType == "Com")
            strValue = "企业";
        if (strMemType == "Person")
            strValue = "个人";
        return strValue;
    }
    public string GetMemberStatus(string strMemType)
    {
        string strValue = "";
        if (strMemType == "ZC")
            strValue = "正常";
        else if (strMemType == "Com")
            strValue = "停用";
        return strValue;
    }

    private string SetFilter()
    {
        string filter = "where 1=1";
        if (txtMemberName.Value.Trim().Length > 0)
            filter += " and memberName like '%" + txtMemberName.Value.Trim() + "%'";
        if (txtTrueName.Value.Trim().Length > 0)
            filter += " and memberTrueName like '%" + txtTrueName.Value.Trim() + "%'";
        return filter.ToString();
    }
    #endregion

}