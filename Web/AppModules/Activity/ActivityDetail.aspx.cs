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
                activity_id.Value = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 20);
                if (activity_id.Value == "")
                    return;
                GetInfo(activity_id.Value);
            }
        }
        
    }

    protected void GetInfo(string id)
    {
        string strSQL = "select * from t_activity_info where activity_id='" + id + "';";
        DataSet ds = ado.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0)
        {
            activity_title.Value = ds.Tables[0].Rows[0]["activity_title"].ToString();
            activity_desc.Value = ds.Tables[0].Rows[0]["activity_desc"].ToString();
            manager_id.Value = ds.Tables[0].Rows[0]["manager_id"].ToString();
            job_ids.Value = ds.Tables[0].Rows[0]["job_ids"].ToString();
            activity_person.Value = ds.Tables[0].Rows[0]["activity_person"].ToString();
            remarks.Value = ds.Tables[0].Rows[0]["remarks"].ToString();
            activity_status.SelectedValue = ds.Tables[0].Rows[0]["activity_status"].ToString();
            if_approved.SelectedValue = ds.Tables[0].Rows[0]["if_approved"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@activity_title", activity_title.Value));
            p.Add(new SqlParameter("@activity_desc", activity_desc.Value));
            p.Add(new SqlParameter("@activity_status", activity_status.SelectedValue));
            p.Add(new SqlParameter("@remarks", remarks.Value));
            p.Add(new SqlParameter("@if_approved", if_approved.SelectedValue));
            p.Add(new SqlParameter("@manager_id", manager_id.Value));
            p.Add(new SqlParameter("@job_ids", job_ids.Value));
            p.Add(new SqlParameter("@activity_person", activity_person.Value));

            if (string.IsNullOrWhiteSpace(activity_id.Value))
            {
                string id = IdCreator.CreateId("t_activity_info", "activity_id");
                //add
                p.Add(new SqlParameter("@activity_id", id));
                p.Add(new SqlParameter("@create_time", DateTime.Now));
                p.Add(new SqlParameter("@create_person", this.TrueName));

                string sql = "insert into t_activity_info(activity_id,activity_title,activity_desc,activity_status,create_time,create_person,if_approved,manager_id,job_ids,activity_person,remarks) values(@activity_id,@activity_title,@activity_desc,@activity_status,@create_time,@create_person,@if_approved,@manager_id,@job_ids,@activity_person,@remarks);";
                int result=ado.ExecuteSqlNonQuery(sql, p.ToArray());

                if (result == 1)
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('新增成功');layer_close_refresh();</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('新增失败');</script>");
            }
            else
            {
                //update
                p.Add(new SqlParameter("@activity_id", activity_id.Value));

                string sql = "update t_activity_info set activity_title=@activity_title,activity_desc=@activity_desc,activity_status=@activity_status,remarks=@remarks,if_approved=@if_approved,manager_id=@manager_id,job_ids=@job_ids,activity_person=@activity_person where activity_id=@activity_id;";
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