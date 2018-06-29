using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Member.Member;
using CodeService;
using StarTech;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using StarTech.DBUtility;
using Startech.Member;

public partial class AddMember : System.Web.UI.Page
{
    public string id = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"] == null ? "" : StarTech.KillSqlIn.Url_ReplaceByNumber(Request["id"], 10);
        if (!IsPostBack)
        {
            LoadCate();
        }
    }

    private void LoadCate()
    {
        DataTable dt = DalBase.Util_GetList("select CategoryId as id,CategoryName as name from  T_Info_Category where CategoryLevel=1").Tables[0];
        this.catelist.DataSource = dt;
        this.catelist.DataTextField = "name";
        this.catelist.DataValueField = "id";
        this.catelist.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (id != "")
        {


            string list = string.Empty;
            foreach (ListItem item in catelist.Items)
            {
                if (item.Selected)
                {
                    list += item.Value + ",";
                }
            }

            if (list == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请选择任务分类!');</script>");
            }
            else
            {
                BllShopUser bUer = new BllShopUser();
                ModShopUser mUser = bUer.GetModel(id);
                mUser.CategoryId = list.TrimEnd(',');
                int i = bUer.Update(mUser);
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改成功!');layer_close();</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改失败!');</script>");
                }
            }
        }

    }
}
