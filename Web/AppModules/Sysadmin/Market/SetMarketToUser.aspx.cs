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
using System.Text;
using System.Data.SqlClient;
using StarTech.DBUtility;


public partial class AppModules_IACenter_AddUser : StarTech.Adapter.StarTechPage
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindMarket();
            BindMarketUser();
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        Add(this.ddlMarket.SelectedValue, this.ddlMarketUser.SelectedValue);
    }

    /// <summary>
    /// 农贸市场数据
    /// </summary>
    private void BindMarket()
    {
        DataTable dt = DalBase.Util_GetList("select Market_id,Market_name from T_Base_Market").Tables[0];
        this.ddlMarket.DataSource = dt;
        this.ddlMarket.DataTextField = "Market_name";
        this.ddlMarket.DataValueField = "Market_id";
        this.ddlMarket.DataBind();
    }


    /// <summary>
    /// 市场管理员数据
    /// </summary>
    private void BindMarketUser()
    {
        DataTable dt = DalBase.Util_GetList("select uniqueid,truename from IACenter_Market_User where isuse=1").Tables[0];
        this.ddlMarketUser.DataSource = dt;
        this.ddlMarketUser.DataTextField = "truename";
        this.ddlMarketUser.DataValueField = "uniqueid";
        this.ddlMarketUser.DataBind();
    }


    /// <summary>
    /// 增加一条数据
    /// </summary>
    public void Add(string mid, string uid)
    {

        if (!CheckUserFromMarketConfig(mid, uid))
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Base_MarketToUser(");
            strSql.Append("market_id,marketuser_id,addTime)");
            strSql.Append(" values (");
            strSql.Append("@market_id,@marketuser_id,@addTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@market_id", SqlDbType.VarChar,50),
					new SqlParameter("@marketuser_id", SqlDbType.Int,8),
					new SqlParameter("@addTime", SqlDbType.DateTime)};
            parameters[0].Value = mid;
            parameters[1].Value = uid;
            parameters[2].Value = DateTime.Now;

            int i = ado.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (i > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "u", "<script>alert('配置成功！');layer_close_refresh();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "u", "<script>alert('配置失败！');</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "u", "<script>alert('管理人员<" + this.ddlMarketUser.SelectedItem.ToString() + ">已经是<" + this.ddlMarket.SelectedItem.ToString() + ">的管理员了，无需重复配置！');layer_close_refresh();</script>");
        }
    }

    /// <summary>
    /// 检验该市场管理员是否已经为对应农贸市场的管理员了
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    private bool CheckUserFromMarketConfig(string mid, string uid)
    {
        DataTable dt = DalBase.Util_GetList("select id from T_Base_MarketToUser where market_id='" + mid + "' and marketuser_id=" + uid).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
