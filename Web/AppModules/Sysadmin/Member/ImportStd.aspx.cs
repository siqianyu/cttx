using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.Aspose;
using StarTech.DBUtility;
using Aspose.Cells;
using System.Data;
using System.Data.SqlClient;

public partial class AppModules_Sysadmin_Member_ImportStd : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请选择要上传的文件!');</script>");
            return;
        }
        string fileName = FileUpload1.FileName;

        string fileExit = fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));

        if (fileExit.ToUpper() != ".XLS" && fileExit.ToUpper() != ".XLSX")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('文件类型不正确,请选择Excel文件!');</script>");
            return;
        }

        System.IO.Stream sr = FileUpload1.FileContent;
        Import(sr);
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('数据导入成功!');</script>");
    }


    public void Import(System.IO.Stream sr)
    {
        STMsExcel excel = new STMsExcel();
        excel.Open(sr);
        excel.SetActiveSheet(0);

        DataTable dt = excel.ExportDataTable(0,0,100000,6);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToString(dt.Rows[i][0]) == "")
            {
                break;
            }
            if (Insert(Convert.ToString(dt.Rows[i][0]), Convert.ToString(dt.Rows[i][1]), Convert.ToString(dt.Rows[i][2]), Convert.ToString(dt.Rows[i][3]), Convert.ToString(dt.Rows[i][5]), Convert.ToString(dt.Rows[i][4])) < 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('导入失败,标准号" + Convert.ToString(dt.Rows[i][1]) + "');</script>");
                return;
            }
        }
    }

    public int Insert(string companyName, string stdname, string stdcode, string bacode, string exedate,string pubdate)
    {
        string sql = "insert into T_Interface_Search(sysnumber,fnumber,companyName,stdName,stdCode,stdPubTime,stdUseTime,addTime) values (@sysnumber,@fnumber,@companyName,@stdName,@stdCode,@stdPubTime,@stdUseTime,@addTime)";

        SqlParameter[] parameter = {
                         new SqlParameter("@sysnumber",SqlDbType.VarChar,50),
                         new SqlParameter("@fnumber",SqlDbType.VarChar,200),
                         new SqlParameter("@companyName",SqlDbType.VarChar,500),
                         new SqlParameter("@stdName",SqlDbType.VarChar,500),
                         new SqlParameter("@stdCode",SqlDbType.VarChar,500),
                         new SqlParameter("@stdPubTime",SqlDbType.DateTime),
                         new SqlParameter("@stdUseTime",SqlDbType.DateTime),
                          new SqlParameter("@addTime",SqlDbType.DateTime)
                         };
        parameter[0].Value = Guid.NewGuid().ToString();
        parameter[1].Value = bacode;
        parameter[2].Value = companyName;
        parameter[3].Value = stdname;
        parameter[4].Value = stdcode;
        try
        {
            parameter[5].Value = Convert.ToDateTime(pubdate);
        }
        catch
        {
            parameter[5].Value = DBNull.Value;
        }
        try
        {
            parameter[6].Value = Convert.ToDateTime(exedate);
        }
        catch
        {
            parameter[6].Value = DBNull.Value;
        }
        parameter[7].Value = DateTime.Now;

        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");

        return ado.ExecuteSqlNonQuery(sql, parameter);
    }
}