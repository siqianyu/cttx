using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using System.Text;
using System.IO;

public partial class AppModules_Sysadmin_Member_MemberTotal : StarTech.Adapter.StarTechPage
{
    public AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Bind(0);
        }
    }

    #region 通用按钮栏设置


    //导出Excel
    void TopButtons1_ExcelClickEvent(object sender, EventArgs e)
    {
        DataTable dt = GetDataArea();
        DataTable dt1 = new DataTable();

        dt1.Columns.Add("城区", typeof(string));
        dt1.Columns.Add("单位", typeof(string));
        dt1.Columns.Add("政府", typeof(string));
        dt1.Columns.Add("个人", typeof(string));
        dt1.Columns.Add("普通注册", typeof(string));
        dt1.Columns.Add("消费券注册", typeof(string));
        dt1.Columns.Add("会员统计", typeof(string));

        foreach (DataRow row in dt.Rows)
        {
            DataRow dr = dt1.NewRow();
            dr[0] = row["areaName"];
            DataTable dt2 = GetCountDataByArea(row[0].ToString());
            dr[1] = dt2.Rows[0]["count1"];
            dr[2] = dt2.Rows[0]["count2"];
            dr[3] = dt2.Rows[0]["count3"];
            dr[4] = dt2.Rows[0]["count4"];
            dr[5] = dt2.Rows[0]["count5"];
            dr[6] = dt2.Rows[0]["count5"];
            dt1.Rows.Add(dr);
        }
        CreateExcel(dt1, "会员信息统计");
    }
    #endregion

    #region 绑定列表数据
    /// <summary>
    /// 绑定会员统计列表
    /// </summary>
    /// <param name="pageIndex"></param>
    private void Bind(int pageIndex)
    {
        string sql = @"select distinct areaName from T_Member_Info where areaName like '%上城%' or areaName like '%下城%' or areaName like '%西湖%' 
or areaName like '%江干%'  or areaName like '%拱墅%' or areaName like '%滨江%' or areaName like '%萧山%' or areaName like '%余杭%'
or areaName like '%临安%' or areaName like '%富阳%' or areaName like '%桐庐%' or areaName like '%建德%' or areaName like '%淳安%' 
or areaName like '%下沙%' or areaName like '%市局%' order by areaName ";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
        EditDataSource(ref dt);
      
    }

    public void EditDataSource(ref DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("上城"))
            {
                row["areaName"] = "上城";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("下城"))
            {
                row["areaName"] = "下城";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("西湖"))
            {
                row["areaName"] = "西湖";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("江干"))
            {
                row["areaName"] = "江干";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("上城"))
            {
                row["areaName"] = "上城";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("拱墅"))
            {
                row["areaName"] = "拱墅";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("滨江"))
            {
                row["areaName"] = "滨江";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("萧山"))
            {
                row["areaName"] = "萧山";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("余杭"))
            {
                row["areaName"] = "余杭";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("临安"))
            {
                row["areaName"] = "临安";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("富阳"))
            {
                row["areaName"] = "富阳";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("建德"))
            {
                row["areaName"] = "建德";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("桐庐"))
            {
                row["areaName"] = "桐庐";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("淳安"))
            {
                row["areaName"] = "淳安";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("下沙"))
            {
                row["areaName"] = "下沙";
            }
            else if (dt.Columns.Contains("areaName") && row["areaName"].ToString().Contains("市局"))
            {
                row["areaName"] = "市局";
            }
        }
    }


    #endregion

    #region 统计各类型的会员
    public string GetTypeCount(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and memberType ='QY'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    public string GetTypeCount1(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and memberType= 'XH'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    public string GetTypeCount2(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and memberType ='XZ'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    // 普通会员注册数量
    public string GetTypeCount3(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and RegType='PT'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    // 消费券会员注册数量
    public string GetTypeCount4(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%' and RegType='XFQ'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }

    public string GetTypeCount5(string areaName)
    {
        string sql = @"select count(*) from T_Member_Info where areaName like '%" + areaName + "%'";
        return adoHelper.ExecuteSqlScalar(sql).ToString();
    }
    #endregion

    #region 单位及城区会员统计
    /// <summary>
    /// 获取城区数据
    /// </summary>
    /// <returns></returns>
    private DataTable GetDataArea()
    {
        string filter = "1=1 ";
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select distinct areaName from T_Member_Info where areaName like '%上城%' or areaName like '%下城%' or areaName like '%西湖%' or areaName like '%江干%'  or areaName like '%拱墅%'or areaName like '%滨江%' or areaName like '%萧山%' or areaName like '%余杭%' or areaName like '%临安%'  or areaName like '%富阳%' or areaName like '%桐庐%' or areaName like '%建德%' or areaName like '%淳安%' or areaName like '%下沙%'  or areaName like '%市局%'   ");
        strSql.Append(" order by areaName ");
        DataTable dt = adoHelper.ExecuteSqlDataset(strSql.ToString()).Tables[0];
        return dt;
    }

    //单位
    public DataTable GetCountDataByArea(string area)
    {
        string sql = @"select distinct (select count(*) from T_Member_Info where areaName like '%" + area + "%' and membertype ='Com') as count1 ,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and membertype like 'Gov') as count2,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and membertype like 'Person') as count3,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and regType ='PT') as count4,(select count(*)  from T_Member_Info where areaName like '%" + area + "%' and regType ='XFQ') as count5 from T_Member_Info";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
        return dt;
    }
    #endregion

    #region 导出Excel
    public void CreateExcel(System.Data.DataTable dt, string username)
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
                if (i == 6)
                {
                    int countall = int.Parse(dt.Rows[j][i - 1].ToString().Trim()) + int.Parse(dt.Rows[j][i - 2].ToString().Trim());
                    rowStr = rowStr + countall + "\t";
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

    protected void linkbtn_Command(object sender, CommandEventArgs e)
    {
        string area = Server.HtmlEncode(e.CommandArgument.ToString());
        Response.Redirect("MemberInfoByArea.aspx?Area=" + area + "");
    }
}
