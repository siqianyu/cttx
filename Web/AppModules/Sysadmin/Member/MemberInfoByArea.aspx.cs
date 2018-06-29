using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class AppModules_Sysadmin_Member_MemberTotal : StarTech.Adapter.StarTechPage
{
    public string area = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        InitTopButton();
        area = Request.QueryString["Area"] == null ? "" : Request.QueryString["Area"].ToString();
    }
    /// 初始化按钮栏
    /// </summary>
    protected void InitTopButton()
    {
        this.OutputExcel1.OutputExcelEvent += new Controls_OutputExcel.ExcelHandler(OutputExcel1_OutputExcelEvent);
    }

    private DataTable GetData(string where)
    {
        string filter = "1=1 ";
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from T_Member_Info  ");
        if (where != null && where != "")
        {
            strSql.Append(where);
        }
        strSql.Append(" order by membertype ");
        DataTable dt = DalBase.Util_GetList(strSql.ToString()).Tables[0];
        return dt;
    }
    private void OutputExcel1_OutputExcelEvent(object sender, EventArgs e)
    {
        DataTable dt = GetData(" where areaName like '%" + area + "%'");
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("用户名", typeof(string));
        dt1.Columns.Add("单位名称", typeof(string));
        dt1.Columns.Add("组织机构代码", typeof(string));
        dt1.Columns.Add("单位地址", typeof(string));
        dt1.Columns.Add("联系电话", typeof(string));
        dt1.Columns.Add("注册类别", typeof(string));
        dt1.Columns.Add("会员类型", typeof(string));
        foreach (DataRow row in dt.Rows)
        {
            DataRow dr = dt1.NewRow();
            dr[0] = row["memberName"];
            dr[1] = row["memberCompanyName"];
            dr[2] = row["memberCompanyCode"];
            dr[3] = row["address"];
            dr[4] = row["tel"];
            dr[5] = row["regType"];
            dr[6] = row["memberType"];
            dt1.Rows.Add(dr);
        }
        CreatExcel(dt1, DateTime.Now.ToShortDateString() + area + "会员统计列表");
    }

    #region 导出Excel
    public void CreatExcel(DataTable dt, string username)
    {
        StringWriter sw = new StringWriter();
        string rowStr = "";
        //取所有列名
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            rowStr = rowStr + dt.Columns[i] + "\t";
        }
        sw.WriteLine(rowStr);
        //取每行数据
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            rowStr = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                if (i == 2 || i == 4)
                {
                    rowStr = rowStr + (dt.Rows[j][i].ToString()) + "\t";
                }
                else if (i == 5)
                {
                    string temp = "普通注册";
                    if (dt.Rows[j][i].ToString().Trim() == "2")
                    {
                        temp = "消费券注册";
                    }
                    rowStr = rowStr + temp + "\t";
                }
                else if (i == 6)
                {
                    string strdemo = "";
                    if (dt.Rows[j][i].ToString().Trim() == "QY")
                    {
                        strdemo = "企业";
                    }
                    else if (dt.Rows[j][i].ToString().Trim() == "XH")
                    {
                        strdemo = "协会";
                    }
                    else if (dt.Rows[j][i].ToString().Trim() == "XZ")
                    {
                        strdemo = "行政";
                    }
                    rowStr = rowStr + strdemo + "\t";
                }
                else
                {
                    rowStr = rowStr + (dt.Rows[j][i].ToString()) + "\t";
                }
            }
            sw.WriteLine(rowStr);
        }
        sw.Close();
        string filename = username;
        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename) + ".xls");
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = Encoding.GetEncoding("GB2312");
        Response.Write(sw);
        Response.End();
    }
    #endregion
}