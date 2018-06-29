using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using Aspose.Cells;
using StarTech.Aspose;

public partial class MemberList : StarTech.Adapter.StarTechPage
{
    public string flag = "", type, title;
    protected void Page_Load(object sender, EventArgs e)
    {
        flag = Request.QueryString["flag"] == null ? "" : Request.QueryString["flag"].ToString();
        type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"].ToString();
        InitTopButton();
        if (!IsPostBack)
        {

            if (type == "Com" || type=="QY")
            {
                title = "企业会员";
            }
            else if (type == "XH")
            {
                title = "协会会员";
            }
            else if (type == "HZSIS")
            {
                title = "行政会员";
            }
            LoadMemLevel();
        }

    }
    #region 通用按钮栏设置
    /// <summary>
    /// 初始化按钮栏
    /// </summary>
    protected void InitTopButton()
    {
        if (flag == "choose")
        {
            this.Add1.MyButton.Visible = this.Edit1.MyButton.Visible = this.OutputExcel1.Visible = this.Delete1.MyButton.Visible = false;
        }
        //客户端脚本
        this.Add1.MyButton.OnClientClick = "button_actions('add','','" + type + "'); return false;";
        this.Edit1.MyButton.OnClientClick = "button_actions('edit',''); return false;";
        this.Edit1.MyButton.Visible = false;
        this.Delete1.MyButton.OnClientClick = "deleteAction(); return false;";
        this.Delete1.MyButton.Text = "批量删除";
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
        CreateExcel(DateTime.Now.ToShortDateString() + "会员列表");
        // btn_ExprotMemListByFilter(sender, e);
    }
    #endregion

    public void LoadMemLevel()
    {
        DataTable dt = DalBase.Util_GetList("select levelid,levelName from T_Member_Level where isSystemFlag=0 and ifToUsed=1").Tables[0];
        this.ddlMemLevel.DataSource = dt;
        this.ddlMemLevel.DataTextField = "levelName";
        this.ddlMemLevel.DataValueField = "levelid";
        this.ddlMemLevel.DataBind();
        this.ddlMemLevel.Items.Insert(0, new ListItem("请选择", "0"));
    }

    #region 导出Excel
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="name">文件名称</param>
    private void CreateExcel(string name)
    {
        DataTable thisTable = DalBase.Util_GetList("select *,(select levelName from T_Member_Level where levelId= T_Member_Info.memberLevel) as levelName from  T_Member_Info " + SetFilter()).Tables[0];

        if (thisTable != null)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("会员名称\t性别\t真实姓名\t会员类型\t会员级别\t单位名称\t联系电话\t会员状态\t注册时间\t现金账户\t赠送的账户");
            foreach (DataRow dr in thisTable.Rows)
            {
                sw.WriteLine(dr["memberName"] + "\t" + dr["sex"].ToString() + "\t" + dr["memberTrueName"].ToString() + "\t" + GetMemberType(dr["memberType"].ToString()) + "\t" + dr["levelName"].ToString() + "\t" + dr["memberCompanyName"].ToString() + "\t" + dr["tel"].ToString() + "\t" + GetMemberStatus(dr["memberStatus"].ToString()) + "\t" + string.Format("{0:yyyy-MM-dd}", dr["regTime"]) + "\t" + (dr["buyMoneyAccount"].ToString()) + dr["freeMoenyAccount"].ToString());
            }
            sw.Close();
            Response.Charset = "GB2312";
            string filename = name;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Write(sw);
            Response.End();
        }
    }

    public string GetMemberType(string strMemType)
    {
        string strValue = "";
        if (strMemType == "Gov" )
            strValue = "政府";
        if (strMemType == "Com" || strMemType == "QY")
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
        string filter = "where 1=1 ";
        if (type != "")
        {
            filter += " and memberType='" + type + "'";
        }
        if (txtMemberName.Value.Trim().Length > 0)
            filter += " and memberName like '%" + txtMemberName.Value.Trim() + "%'";
        if (txtTrueName.Value.Trim().Length > 0)
            filter += " and memberTrueName like '%" + txtTrueName.Value.Trim() + "%'";
        if (txtCompanyName.Value.Trim().Length > 0)
            filter += " and txtCompanyName like '%" + txtTrueName.Value.Trim() + "%'";
        if (ddlMemLevel.SelectedValue != "0")
        {
            filter += " and memberLevel ='" + ddlMemLevel.SelectedValue + "'";
        }

        return filter.ToString();
    }

    #endregion

    #region  按条件 Excel导出会员信息
    /// <summary>
    ///  按条件 Excel导出会员信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_ExprotMemListByFilter(object sender, EventArgs e)
    {
        string filter = SetFilter();
        string tableViews = "(select *,(select levelName from T_Member_Level where levelId= T_Member_Info.memberLevel) as levelName,(select TypeName from T_Base_MemberType where TypeCode=memberType ) as TypeName  from  T_Member_Info ) v ";
        string sort = " order by memberid";
        string fields = "*";
        string sql = "select " + fields + " from " + tableViews + filter + sort;

        DataTable dt = DalBase.Util_GetList(sql).Tables[0];
        EditDataSourceExcel(ref dt);
        string fileName = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("-", "").Replace(" ", "").Replace(":", "") + "会员信息表" + ".xls";
        string savedPath = "~/upload/";
        string phyPath = System.Web.HttpContext.Current.Server.MapPath(savedPath);
        if (!System.IO.Directory.Exists(phyPath))
        {
            System.IO.Directory.CreateDirectory(phyPath);
        }
        string savedFileName = Path.Combine(phyPath, fileName);

        STMsExcel excel = new STMsExcel();
        excel.ClearAndAddSheet("会员信息");//表格 sheet1名称

        //表格首行标题样式
        excel.Merge(0, 0, 1, 26);//合并第一行 前8列
        excel.SetValue(0, 0, "会员信息表");//表格首行大标题
        Aspose.Cells.Style styleTitle = excel.CreateStyle();
        styleTitle.Font.Color = Color.Black;
        styleTitle.Font.IsBold = true;
        styleTitle.Font.Size = 15;
        styleTitle.BackgroundColor = Color.FromArgb(106, 182, 180);
        styleTitle.HorizontalAlignment = TextAlignmentType.Center;
        styleTitle.ForegroundColor = Color.FromArgb(0x99, 0xcc, 0xff);
        //设置首行上下左右边框
        styleTitle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        styleTitle.Borders[BorderType.TopBorder].Color = Color.Black;
        styleTitle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        styleTitle.Borders[BorderType.BottomBorder].Color = Color.Black;
        styleTitle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        styleTitle.Borders[BorderType.LeftBorder].Color = Color.Black;
        styleTitle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        styleTitle.Borders[BorderType.RightBorder].Color = Color.Black;
        excel.SetRowHeight(0, 30);
        excel.SetRowStyle(0, styleTitle);


        //表格 字段标题 样式
        Aspose.Cells.Style style = excel.CreateStyle();
        style.Font.Color = Color.Black;
        style.BackgroundColor = Color.Gray;
        style.Font.IsBold = true;
        style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style.Borders[BorderType.TopBorder].Color = Color.Black;
        style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style.Borders[BorderType.BottomBorder].Color = Color.Black;
        style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style.Borders[BorderType.LeftBorder].Color = Color.Black;
        style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style.Borders[BorderType.RightBorder].Color = Color.Black;
        style.HorizontalAlignment = TextAlignmentType.Center;//水平居中
        excel.SetRowStyle(1, style);
        excel.SetRowHeight(1, 20);


        //设置列宽
        excel.SetColumnWidth(0, 8);//自动编号
        excel.SetColumnWidth(1, 20);
        excel.SetColumnWidth(2, 10);
        excel.SetColumnWidth(3, 30);
        excel.SetColumnWidth(4, 15);
        excel.SetColumnWidth(5, 15);
        excel.SetColumnWidth(6, 10);
        excel.SetColumnWidth(7, 10);
        excel.SetColumnWidth(8, 15);
        excel.SetColumnWidth(9, 10);
        excel.SetColumnWidth(10, 15);
        excel.SetColumnWidth(11, 20);
        excel.SetColumnWidth(12, 10);
        excel.SetColumnWidth(13, 15);
        excel.SetColumnWidth(14, 20);
        excel.SetColumnWidth(15, 10);
        excel.SetColumnWidth(16, 10);
        excel.SetColumnWidth(13, 15);
        excel.SetColumnWidth(14, 10);
        excel.SetColumnWidth(15, 10);
        excel.SetColumnWidth(16, 10);
        excel.SetColumnWidth(17, 15);
        excel.SetColumnWidth(18, 20);
        excel.SetColumnWidth(19, 20);
        excel.SetColumnWidth(20, 20);
        excel.SetColumnWidth(21, 20);
        excel.SetColumnWidth(22, 30);
        excel.SetColumnWidth(23, 10);
        excel.SetColumnWidth(24, 20);
        excel.SetColumnWidth(25, 30);
        excel.SetColumnWidth(26, 10);

        //表格字段名称
        excel.SetValue("A" + 2, "序号");//自动编号
        excel.SetValue("B" + 2, "用户名称");
        excel.SetValue("C" + 2, "会员类型");
        excel.SetValue("D" + 2, "所属行业");
        excel.SetValue("E" + 2, "会员级别");
        excel.SetValue("F" + 2, "可托管标准");
        excel.SetValue("G" + 2, "剩余托管数");
        excel.SetValue("H" + 2, "可下载标准");
        excel.SetValue("I" + 2, "剩余下载数");
        excel.SetValue("J" + 2, "充值账户金额");
        excel.SetValue("K" + 2, "已用金额");
        excel.SetValue("L" + 2, "免费账户金额");
        excel.SetValue("M" + 2, "已用金额");
        excel.SetValue("N" + 2, "所在地区");
        excel.SetValue("O" + 2, "公司名称");
        excel.SetValue("P" + 2, "联系人");
        excel.SetValue("Q" + 2, "性别");
        excel.SetValue("R" + 2, "联系电话");
        excel.SetValue("S" + 2, "传真");
        excel.SetValue("T" + 2, "手机");
        excel.SetValue("U" + 2, "地址");
        excel.SetValue("V" + 2, "邮政编码");
        excel.SetValue("W" + 2, "电子邮箱");
        excel.SetValue("X" + 2, "注册时间");
        excel.SetValue("Y" + 2, "会员开始日期");
        excel.SetValue("Z" + 2, "会员截止日期");

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Aspose.Cells.Style style1 = excel.CreateStyle();
                style1.HorizontalAlignment = TextAlignmentType.Left;//水平居左
                style1.BackgroundColor = Color.FromArgb(225, 253, 254);
                style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.TopBorder].Color = Color.Black;
                style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.BottomBorder].Color = Color.Black;
                style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.LeftBorder].Color = Color.Black;
                style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style1.Borders[BorderType.RightBorder].Color = Color.Black;
                excel.SetRowStyle(i + 2, style1);
                excel.SetRowHeight(i + 2, 20);



                excel.SetValue("A" + (3 + i), Convert.ToString(dt.Rows[i]["add_sn"]));//自动编号
                excel.SetValue("B" + (3 + i), Convert.ToString(dt.Rows[i]["memberName"]));
                excel.SetValue("C" + (3 + i), Convert.ToString(dt.Rows[i]["TypeName"]));
                excel.SetValue("D" + (3 + i), Convert.ToString(dt.Rows[i]["memberCompanyType"]));
                excel.SetValue("E" + (3 + i), Convert.ToString(dt.Rows[i]["levelName"]));
                excel.SetValue("F" + (3 + i), Convert.ToString(dt.Rows[i]["TrustNumber"]));
                excel.SetValue("G" + (3 + i), Convert.ToString(dt.Rows[i]["TrustNumber"]));
                excel.SetValue("H" + (3 + i), Convert.ToString(dt.Rows[i]["DownloadNumber"]));
                excel.SetValue("I" + (3 + i), Convert.ToString(dt.Rows[i]["DownloadNumber"]));
                excel.SetValue("J" + (3 + i), Convert.ToString(dt.Rows[i]["buyMoneyAccount"]));
                excel.SetValue("K" + (3 + i), Convert.ToString(dt.Rows[i]["buyMoneyAccountUsed"]));
                excel.SetValue("L" + (3 + i), Convert.ToString(dt.Rows[i]["freeMoenyAccount"]));
                excel.SetValue("M" + (3 + i), Convert.ToString(dt.Rows[i]["freeMoenyAccountUsed"]));
                excel.SetValue("N" + (3 + i), Convert.ToString(dt.Rows[i]["areaName"]));
                excel.SetValue("O" + (3 + i), Convert.ToString(dt.Rows[i]["memberCompanyName"]));
                excel.SetValue("P" + (3 + i), Convert.ToString(dt.Rows[i]["memberTrueName"]));
                excel.SetValue("Q" + (3 + i), Convert.ToString(dt.Rows[i]["sex"]));
                excel.SetValue("R" + (3 + i), Convert.ToString(dt.Rows[i]["tel"]));
                excel.SetValue("S" + (3 + i), Convert.ToString(dt.Rows[i]["fax"]));
                excel.SetValue("T" + (3 + i), Convert.ToString(dt.Rows[i]["mobile"]));
                excel.SetValue("U" + (3 + i), Convert.ToString(dt.Rows[i]["address"]));
                excel.SetValue("V" + (3 + i), Convert.ToString(dt.Rows[i]["post"]));
                excel.SetValue("W" + (3 + i), Convert.ToString(dt.Rows[i]["email"]));
                excel.SetValue("X" + (3 + i), Convert.ToString(dt.Rows[i]["regType"]));
                excel.SetValue("Y" + (3 + i), Convert.ToString(dt.Rows[i]["levelServiceStartTime"]));
                excel.SetValue("Z" + (3 + i), Convert.ToString(dt.Rows[i]["levelServiceEndTime"]));
            }
        }
        excel.Save(fileName, Aspose.Cells.FileFormatType.Excel97To2003, Aspose.Cells.SaveType.OpenInBrowser, Response);
        FileInfo file = new FileInfo(savedFileName);
        file.Delete();
    }

    public void EditDataSourceExcel(ref DataTable dt)
    {
        dt.Columns.Add("add_sn", typeof(string));

        int sn = 1;
        foreach (DataRow dr in dt.Rows)
        {
            dr["add_sn"] = sn;
            sn++;
        }
    }
    #endregion

}