using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using System.Data;
using System.Data.SqlClient;
using StarTech.DBUtility;

public partial class AppModules_Member_MemberDetail : StarTech.Adapter.StarTechPage
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            task_id.Value = KillSqlIn.Form_ReplaceByString(Request.QueryString["task_id"], 20);
            if (Request.QueryString["id"] != null)
            {
                node_id.Value = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 20);
                if (node_id.Value == "")
                    return;
                GetInfo(node_id.Value);
            }
        }

    }

    protected void GetInfo(string id)
    {
        string strSQL = "select * from t_task_node_config where node_id='" + id + "';";
        DataSet ds = ado.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0)
        {
            node_sn.Value = ds.Tables[0].Rows[0]["node_sn"].ToString();
            node_name.Value = ds.Tables[0].Rows[0]["node_name"].ToString();
            yb_value.Value = ds.Tables[0].Rows[0]["yb_value"].ToString();
            jyz_value_set.Value = ds.Tables[0].Rows[0]["jyz_value_set"].ToString();
            cs_value_set.Value = ds.Tables[0].Rows[0]["cs_value_set"].ToString();
            rq_value_set.Value = ds.Tables[0].Rows[0]["rq_value_set"].ToString();
            node_para.Value = ds.Tables[0].Rows[0]["node_para"].ToString();
            remarks.Value = ds.Tables[0].Rows[0]["remarks"].ToString();
            if_use.SelectedValue = ds.Tables[0].Rows[0]["if_use"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<SqlParameter> p = new List<SqlParameter>();
            p.Add(new SqlParameter("@node_sn", node_sn.Value));
            p.Add(new SqlParameter("@node_name", node_name.Value));
            p.Add(new SqlParameter("@yb_value", yb_value.Value));
            p.Add(new SqlParameter("@jyz_value_set", jyz_value_set.Value));
            p.Add(new SqlParameter("@cs_value_set", cs_value_set.Value));
            p.Add(new SqlParameter("@rq_value_set", rq_value_set.Value));
            p.Add(new SqlParameter("@node_para", node_para.Value));
            p.Add(new SqlParameter("@remarks", remarks.Value));
            p.Add(new SqlParameter("@if_use", if_use.SelectedValue));

            if (string.IsNullOrWhiteSpace(node_id.Value))
            {
                string id = IdCreator.CreateId("t_task_node_config", "node_id");
                //add
                p.Add(new SqlParameter("@node_id", id));
                p.Add(new SqlParameter("@node_code", "cd" + new Random().Next(1000, 9999)));
                p.Add(new SqlParameter("@task_id", task_id.Value));

                string sql = "insert into t_task_node_config(node_id,task_id,node_code,node_sn,node_name,node_para,remarks,if_use) values(@node_id,@task_id,@node_code,@node_sn,@node_name,@node_para,@remarks,@if_use);";
                int result = ado.ExecuteSqlNonQuery(sql, p.ToArray());

                if (result == 1)
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('新增成功');layer_close_refresh();</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('新增失败');</script>");
            }
            else
            {
                //update
                p.Add(new SqlParameter("@node_id", node_id.Value));
                string sql = "update t_task_node_config set node_sn=@node_sn,node_name=@node_name,node_para=@node_para,remarks=@remarks,if_use=@if_use where node_id=@node_id;";
                int result = ado.ExecuteSqlNonQuery(sql, p.ToArray());

                if (result == 1)
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改失败');</script>");
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('提交失败');</script>");
        }
    }
}