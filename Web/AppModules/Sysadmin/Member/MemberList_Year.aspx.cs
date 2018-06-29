using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class MemberList_Year : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitTopButton();
    }
    #region 通用按钮栏设置
    /// <summary>
    /// 初始化按钮栏
    /// </summary>
    protected void InitTopButton()
    {
        //客户端脚本
        this.Add1.MyButton.OnClientClick = "button_actions('add'); return false;";
        this.Add1.MyButton.Visible = true;
        this.Edit1.MyButton.OnClientClick = "deleteAction(); return false;";
        this.Edit1.MyButton.Visible = false;
        this.Delete1.MyButton.OnClientClick = "return deleteAction()";
        this.Delete1.MyButton.Text = "批量删除";
        this.Delete1.MyButton.Visible = true;
        this.Show1.MyButton.OnClientClick = "return button_actions('show')";
        this.Show1.MyButton.Visible = false;


        //事件
        this.Show1.ShowClickEvent += new Sysadmin_Controls_Show.ShowClickHandler(Show1_ShowClickEvent);
        this.Add1.AddClickEvent += new Sysadmin_Controls_Add.AddClickHandler(Add1_AddClickEvent);
        this.Edit1.EditClickEvent += new Sysadmin_Controls_Edit.EditClickHandler(Edit1_EditClickEvent);
        this.Delete1.DeleteClickEvent += new Sysadmin_Controls_Delete.DeleteClickHandler(Delete1_DeleteClickEvent);
        this.OutputExcel1.OutputExcelEvent += new Controls_OutputExcel.ExcelHandler(OutputExcel1_OutputExcelEvent);
        // this.Search1.AddClickEvent += new Sysadmin_Controls_Search.AddClickHandler(Search1_AddClickEvent);
    }

    void Add1_AddClickEvent(object sender, EventArgs e)
    {

    }


    void Edit1_EditClickEvent(object sender, EventArgs e)
    {

    }

    void Delete1_DeleteClickEvent(object sender, EventArgs e)
    {

    }

    void Show1_ShowClickEvent(object sender, EventArgs e)
    {

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
        DataTable thisTable = DalBase.Util_GetList("select * from T_Member_Info where exists(select levelId from T_Member_Level where levelId=memberLevel and moneyType='Year'" + SetFilter() + ")").Tables[0];
        if (thisTable != null)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("用户名\t公司名称\t可托管标准数\t可下载标准数\t有效时间\t会员状态");
            foreach (DataRow dr in thisTable.Rows)
            {
                sw.WriteLine(dr["memberName"] + "\t" + dr["memberCompanyName"].ToString() + "\t"
                    + dr["TrustNumber"].ToString() + "\t" + dr["DownloadNumber"] + "\t" + string.Format("{0:yyyy-MM-dd}", dr["levelServiceStartTime"]) + "至" + string.Format("{0:yyyy-MM-dd}", dr["levelServiceEndTime"]) + "\t"
                    + (dr["memberStatus"].ToString() == "ZC" ? "正常" : "禁用"));
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
    private string SetFilter()
    {
        string filter = "";
        if (this.txtMemberName.Value.Trim().Length > 0)
            filter += " and memberName like '%" + txtMemberName.Value.Trim() + "%'";
        if (this.txtCompanyName.Value.Trim().Length > 0)
            filter += " and memberCompanyName like '%" + txtCompanyName.Value.Trim() + "%'";
        return filter.ToString();
    }
    #endregion

}