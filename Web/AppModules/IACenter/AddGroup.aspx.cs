using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using StarTech.Util;
using System.IO;

public partial class AppModules_IACenter_AddGroup : StarTech.Adapter.StarTechPage
{
    public string id;
    public string isShow;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.id = (Request["id"] == null) ? "" : Request["id"];
        this.isShow = (Request["isShow"] == null) ? "" : Request["isShow"];

        if (!IsPostBack)
        {
            InitForm();

            if (this.isShow == "1")
            {
                this.btnSave.Visible = false;
            }
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if(this.txtTitle.Value=="")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('请输入用户组名称');</script>");
            return;

        }

        StarTech.DBUtility.AdoHelper ado = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
        if (this.id == "")
        {
            
            string sql = "insert into IACenter_Group(groupName) values('" + this.txtTitle.Value.Trim() + "');select @@IDENTITY";
            
            object obj = ado.ExecuteSqlScalar(sql);
            if (obj != null)
            {
                int groupId = Int32.Parse(obj.ToString());
                AddUserToGroup(groupId, this.SelectPanel1.Value);
                ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('添加成功');layer_close_refresh();</script>");
            }
        }
        else
        {
            string sql = "update IACenter_Group set groupName='" + this.txtTitle.Value.Trim() + "' where uniqueId=" + this.id + "";
            if (ado.ExecuteSqlNonQuery(sql) > 0)
            {
                AddUserToGroup(Int32.Parse(this.id), this.SelectPanel1.Value);
                ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
    }

    protected void AddUserToGroup(int groupId, string userIds)
    {
        StarTech.DBUtility.AdoHelper ado = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
        ado.ExecuteSqlNonQuery("delete IACenter_UserInGroup where groupId=" + groupId + "");
        if (userIds == "") { return; }

        string[] userIdsArr = userIds.Split(',');
        foreach (string id in userIdsArr)
        {
           
            ado.ExecuteSqlNonQuery("insert into IACenter_UserInGroup(userId,groupId) values(" + id + "," + groupId + ")");
        }
    }

    protected void InitForm()
    {
        if (this.id != "")
        {
            DataTable dt = new BllTableObject("IACenter_Group").Util_GetList("*","uniqueId=" + this.id + "");
            if (dt.Rows.Count > 0)
            {
                this.txtTitle.Value = dt.Rows[0]["groupName"].ToString();
                //users
                DataTable dtUsers = new BllTableObject("IACenter_UserInGroup").Util_GetList("*", "groupId=" + this.id + "");
                DataTable dtUser = new BllTableObject("IACenter_User").Util_GetList("*", "1=1");
                string values = "";
                string texts = "";
                foreach (DataRow row in dtUsers.Rows) 
                {
                    values += row["userid"].ToString() + ",";
                    DataRow[] rowUserInfo = dtUser.Select("uniqueId='" + row["userid"].ToString() + "'");
                    if (rowUserInfo.Length > 0) { texts += rowUserInfo[0]["truename"].ToString() + ","; } else { texts += ","; }
                }
                if (values.Length > 0) { values = values.TrimEnd(','); }
                if (texts.Length > 0) { texts = texts.TrimEnd(','); }
                this.SelectPanel1.Value = values;
                this.SelectPanel1.Text = texts;
            }
        }
    }
}
