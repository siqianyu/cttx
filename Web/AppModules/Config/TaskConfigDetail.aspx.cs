using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using System.Data;
using StarTech.DBUtility;
using System.Data.SqlClient;

public partial class AppModules_Member_MemberDetail : StarTech.Adapter.StarTechPage
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                task_id.Value = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 20);
                if (task_id.Value == "")
                    return;
                GetInfo(task_id.Value);
            }
        }
        
    }

    protected void GetInfo(string id)
    {
        string strSQL = "select * from t_task_config where task_id='" + id + "';";
        DataSet ds = ado.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0)
        {
            task_sn.Value = ds.Tables[0].Rows[0]["task_sn"].ToString();
            task_name.Value = ds.Tables[0].Rows[0]["task_name"].ToString();
            task_para.Value = ds.Tables[0].Rows[0]["task_para"].ToString();
            remarks.Value = ds.Tables[0].Rows[0]["remarks"].ToString();
            if_use.SelectedValue = ds.Tables[0].Rows[0]["if_use"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@task_sn", task_sn.Value));
            p.Add(new SqlParameter("@task_name", task_name.Value));
            p.Add(new SqlParameter("@task_para", task_para.Value));
            p.Add(new SqlParameter("@remarks", remarks.Value));
            p.Add(new SqlParameter("@if_use", if_use.SelectedValue));

            if (string.IsNullOrWhiteSpace(task_id.Value))
            {
                string id = IdCreator.CreateId("t_task_config", "task_id");
                //add
                p.Add(new SqlParameter("@task_id", id));
                p.Add(new SqlParameter("@task_code", "cd" + new Random().Next(1000, 9999)));

                string sql = "insert into t_task_config(task_id,task_code,task_sn,task_name,task_para,remarks,if_use) values(@task_id,@task_code,@task_sn,@task_name,@task_para,@remarks,@if_use);";
                int result=ado.ExecuteSqlNonQuery(sql, p.ToArray());

                if (result == 1)
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('新增成功');layer_close_refresh();</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('新增失败');</script>");
            }
            else
            {
                //update
                p.Add(new SqlParameter("@task_id", task_id.Value));

                string sql = "update t_task_config set task_sn=@task_sn,task_name=@task_name,task_para=@task_para,remarks=@remarks,if_use=@if_use where task_id=@task_id;";
                int result=ado.ExecuteSqlNonQuery(sql, p.ToArray());

                if(result==1)
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改失败');</script>");
            }
        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('提交失败');</script>");
        }
    }
}