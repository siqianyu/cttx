using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using StarTech.DBUtility;
using System.Data.SqlClient;
/// <summary>
///PublicTools 的摘要说明
/// </summary>
public class PublicTools
{
    protected static AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance_Task");
    protected static AdoHelper hjjc_ado = AdoHelper.CreateHelper("DB_Instance");
	public PublicTools()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static void UpdateTaskSampleValue(string task_id, string sample_id, string item_id, string task_scene_id, string sample_value)
    {
        if (sample_value.Trim() == "") { return; }

        //更新样品表样品值
        string sql = "update t_mon_sample_item set  sample_item_value=@item_value,sample_item_value_revise=@item_value,sample_item_value_num=@item_value1,sample_item_value_input=@item_value where task_id=@task_id and sample_id=@sample_id and item_id=@item_id";
        SqlParameter[] parameter = { 
                        new SqlParameter("@item_value",SqlDbType.VarChar,50),
                        new SqlParameter("@item_value1",SqlDbType.Float),
                        new SqlParameter("@item_id",SqlDbType.VarChar,50),
                        new SqlParameter("@task_id",SqlDbType.UniqueIdentifier,50),
                        new SqlParameter("@sample_id",SqlDbType.UniqueIdentifier,50)
                    };
        parameter[0].Value = sample_value;
        if (IsNumber(sample_value) == true) { parameter[1].Value = sample_value; }
        else { parameter[1].Value = DBNull.Value; }
        parameter[2].Value = item_id;
        parameter[3].Value = new Guid(task_id);
        parameter[4].Value = new Guid(sample_id);
        adoHelper.ExecuteSqlNonQuery(sql, parameter);

        //string group_id="";
        //DataTable dt  = GetAnalyseItem(task_id, task_scene_id, item_id,ref group_id);
        //if (dt!=null && dt.Rows.Count>0)
        //{
        //    string edit = "update t_mon_analyse_item set item_value='" + sample_value + "' where group_id='"+ group_id +"' and sample_id='"+ sample_id +"' and item_id = '"+ item_id +"'";
        //    adoHelper.ExecuteSqlNonQuery(edit);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (Convert.ToString(dt.Rows[i]["sample_id"]).ToLower() == sample_id.ToLower())
        //        {
        //            InsertAnalyseItemLog(Convert.ToString(dt.Rows[i]["group_item_id"]));
        //        }
        //    }
        //}
    }

    public static void UpdateTaskSampleValue(string task_id, string sample_id, string item_id, string task_scene_id, string sample_value,string user_name_start,string user_name_end,string EditdepartName)
    {
        if (sample_value.Trim() == "") { return; }

        //更新样品表样品值
        string sql = "update t_mon_sample_item set  sample_item_value=@item_value,sample_item_value_revise=@item_value,sample_item_value_num=@item_value1,sample_item_value_input=@item_value where task_id=@task_id and sample_id=@sample_id and item_id=@item_id";
        SqlParameter[] parameter = { 
                        new SqlParameter("@item_value",SqlDbType.VarChar,50),
                        new SqlParameter("@item_value1",SqlDbType.Float),
                        new SqlParameter("@item_id",SqlDbType.VarChar,50),
                        new SqlParameter("@task_id",SqlDbType.UniqueIdentifier,50),
                        new SqlParameter("@sample_id",SqlDbType.UniqueIdentifier,50)
                    };
        parameter[0].Value = sample_value;
        if (IsNumber(sample_value) == true) { parameter[1].Value = sample_value; }
        else { parameter[1].Value = DBNull.Value; }
        parameter[2].Value = item_id;
        parameter[3].Value = new Guid(task_id);
        parameter[4].Value = new Guid(sample_id);
        adoHelper.ExecuteSqlNonQuery(sql, parameter);

        string group_id = "";
        DataTable dt = GetAnalyseItem(task_id, task_scene_id, item_id, ref group_id);
        if (dt != null && dt.Rows.Count > 0)
        {
            string edit = "update t_mon_analyse_item set item_value='" + sample_value + "' where group_id='" + group_id + "' and sample_id='" + sample_id + "' and item_id = '" + item_id + "'";
            adoHelper.ExecuteSqlNonQuery(edit);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["sample_id"]).ToLower() == sample_id.ToLower())
                {
                    InsertAnalyseItemLog(Convert.ToString(dt.Rows[i]["group_item_id"]), user_name_start, user_name_end, EditdepartName);
                }
            }
        }
    }


    public static bool IsNumber(string s)
    {
        if (s == "") { return false; }
        try
        {
            decimal d = decimal.Parse(s);
            return true;
        }
        catch { return false; }
    }

    public static DataTable GetAnalyseItem(string task_id, string task_scene_id, string item_id, ref string group_id)
    {
        DataTable dt = new DataTable();
        SqlParameter[] para = {
            new SqlParameter("@taskId",SqlDbType.UniqueIdentifier,50),
            new SqlParameter("@taskSceneId",SqlDbType.UniqueIdentifier,50),
            new SqlParameter("@itemIdList",SqlDbType.VarChar,4000)
        };
        Guid tempGuid = new Guid(task_id);
        para[0].Value = tempGuid;
        tempGuid = new Guid(task_scene_id);
        para[1].Value = tempGuid;
        para[2].Value = item_id;
        dt = adoHelper.ExecuteSPDataset("p_get_analyse_group_item_scene", para).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            group_id = Convert.ToString(dt.Rows[0]["group_id"]);
        }
        return dt;
    }

    public static void InsertAnalyseItemLog(string group_item_id,string start_name,string end_name,string EditdepartName)
    {
        //exec p_insert_mon_analyse_item_log @group_item_id='B5C417D2-A8A0-47CB-A46E-83B46DD03B28',@analyse_person=N'',@analyse_time=''2014-05-26 13:10:31:313'',@edit_person=N'',@edit_time=''2014-05-26 13:10:31:313'',@edit_person_department=N'',@action_type=N'修改',@remark=N''
        SqlParameter[] parmeters = { 
            new SqlParameter("@group_item_id",SqlDbType.UniqueIdentifier),
            new SqlParameter("@analyse_person",SqlDbType.VarChar,350),   
            new SqlParameter("@analyse_time",SqlDbType.DateTime),   
            new SqlParameter("@edit_person",SqlDbType.VarChar,50),   
            new SqlParameter("@edit_time",SqlDbType.DateTime),   
            new SqlParameter("@edit_person_department",SqlDbType.VarChar,50),   
            new SqlParameter("@action_type",SqlDbType.VarChar,50),   
            new SqlParameter("@remark",SqlDbType.VarChar,4000)
       };
        parmeters[0].Value = new Guid(group_item_id);
        parmeters[1].Value = start_name;
        parmeters[2].Value = DateTime.Now;
        parmeters[3].Value = end_name;
        parmeters[4].Value = DateTime.Now;
        parmeters[5].Value = EditdepartName;
        parmeters[6].Value = "修改";
        parmeters[7].Value = "";
        adoHelper.ExecuteSPNonQuery("p_insert_mon_analyse_item_log",parmeters);
        //v_mon_analyse_item_log
    }
    public static void InsertLog(string task_id, string item_id, string task_scene_id, string user_name, string user_code, string edittime, string start_value, string end_value)
    {
 
    }

    public static DataTable GetEnvironment(string item_name, string item_id)
    {
        DataTable dt = new DataTable();
        string sql = "select * from T_Environment where item_name = '" + item_name + "' and createtime between '"+ DateTime.Now.ToString("yyyy/MM/dd")+" 00:00:00' and '"+ DateTime.Now.ToString("yyyy-MM-dd") +" 23:59:59'";
        dt = hjjc_ado.ExecuteSqlDataset(sql).Tables[0];
        return dt;
    }




    public static void InsertEnvironment(string wd,string sd,string hzstID)
    {
        string selBase = "select * from t_hzst_analyse where hzstid='"+ hzstID +"'";

        string item_name = "";
        string item_id = "";

        DataTable dt = hjjc_ado.ExecuteSqlDataset(selBase).Tables[0];
        if (dt.Rows.Count > 0)
        {
            item_name = Convert.ToString(dt.Rows[0]["item_name"]);
            item_id = Convert.ToString(dt.Rows[0]["item_id"]);
        }

        string sql_count = "select count(1) from T_Environment where item_name = '" + item_name + "' and createtime between '" + DateTime.Now.ToString("yyyy/MM/dd") + " 00:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";
        int nCount =Convert.ToInt32(hjjc_ado.ExecuteSqlScalar(sql_count));
        if (nCount == 0)
        {
            string sql = "insert T_Environment (sysnumber,temperature,humidity,item_name,item_id,createtime) values('"+ Guid.NewGuid().ToString() +"','"+ wd +"','" + sd + "','"+ item_name +"','"+ item_id +"','"+ DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") +"')";
            hjjc_ado.ExecuteSqlNonQuery(sql);
        }
    }
}