using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Goods_ServiceAdd : System.Web.UI.Page
{
    protected string serviceId = "";
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            serviceId = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 50);
                string strSQL = "select * from T_Goods_Service where serviceId='" + serviceId + "';";
                strSQL += "select * from T_Goods_ServiceDetail where serviceId='" + serviceId + "';";
                ds = adoHelper.ExecuteSqlDataset(strSQL);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                    return;
            if (!IsPostBack)
            {

                BindData();
            }
        }
    }

    protected void BindData()
    {

        txtName.Text = ds.Tables[0].Rows[0]["serviceName"].ToString();
        txtContext.Text = ds.Tables[0].Rows[0]["serviceContext"].ToString();
        txtOrder.Text = ds.Tables[0].Rows[0]["orderBy"].ToString();
        txtRemark.Text = ds.Tables[0].Rows[0]["remark"].ToString();

        if (ds.Tables.Count < 2 || ds.Tables[1].Rows.Count < 1)
            return;
        string valueList = "";
        string priceList = "";
        string defaultList = "";
        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        {
            //txtName.Text = ds.Tables[0].Rows[0]["serviceName"].ToString();
            //txtName.Text = ds.Tables[0].Rows[0]["serviceName"].ToString();
            if (i != 0)
            {
                valueList += ",";
                priceList += ",";
                defaultList += ",";
            }
            valueList += ds.Tables[1].Rows[i]["value"];
            priceList += ds.Tables[1].Rows[i]["Price"];
            defaultList += ds.Tables[1].Rows[i]["isdefault"];
        }
        txtValue.Text = valueList;
        txtPrice.Text = priceList;
        txtDefault.Text = defaultList;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string serviceName= KillSqlIn.Form_ReplaceByString(txtName.Text,50);
        string serviceContext =KillSqlIn.Form_ReplaceByString(txtContext.Text,50);
        int orderBy=0;
        int.TryParse(txtOrder.Text,out orderBy);
        string remark =KillSqlIn.Form_ReplaceByString(txtRemark.Text,50);

        string []valueList = txtValue.Text.Split(',');
        string[] priceList = txtPrice.Text.Split(',');
        string[] defaultList = txtDefault.Text.Split(',');
        string strSQL = "";


        if (serviceId == "")
        {
            var guid = Guid.NewGuid().ToString();
            if (valueList.Length != priceList.Length || valueList.Length != defaultList.Length)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('服务选项，价格以及默认值存在不匹配');</script>");
                return;
            }
            strSQL = " BEGIN TRANSACTION  ";
            strSQL += "insert T_Goods_Service values('"+guid+"','"+serviceName+"','"+serviceContext+"',"+orderBy+",'"+remark+"');";
            for (int i = 0; i < valueList.Length; i++)
            {
                //txtName.Text = ds.Tables[0].Rows[0]["serviceName"].ToString();
                //txtName.Text = ds.Tables[0].Rows[0]["serviceName"].ToString();
                string value=KillSqlIn.Form_ReplaceByString(valueList[i],50);
                decimal d = 0;
                decimal.TryParse(priceList[i],out d);
                int dd = 0;
                int.TryParse(defaultList[i],out dd);
                strSQL += "insert T_Goods_ServiceDetail values('" + guid + i + "','" + guid + "','" + value + "','" + d + "',''," + dd + ");";
            }

            strSQL += " COMMIT TRANSACTION ";
            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
            if (rows > 0)
            {
                LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "添加任务服务《" + serviceName + "》", "添加", "", "", HttpContext.Current.Request.Url.ToString());

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加成功');</script>");
            }

        }
        else
        {
            if (valueList.Length != priceList.Length || valueList.Length != defaultList.Length)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('服务选项，价格以及默认值不匹配');</script>");
                return;
            }
            strSQL = " BEGIN TRANSACTION  ";
            strSQL += "update  T_Goods_Service set serviceName='" + serviceName + "',serviceContext='" + serviceContext + "',orderBy=" + orderBy + ",remark='" + remark + "' where serviceId='"+serviceId+"';";
            for (int i = 0; i < valueList.Length; i++)
            {
                //txtName.Text = ds.Tables[0].Rows[0]["serviceName"].ToString();
                //txtName.Text = ds.Tables[0].Rows[0]["serviceName"].ToString();
                string value = KillSqlIn.Form_ReplaceByString(valueList[i], 50);
                decimal d = 0;
                decimal.TryParse(priceList[i], out d);
                int dd = 0;
                int.TryParse(defaultList[i], out dd);
                //strSQL += "insert T_Goods_ServiceDetail values('" + guid + i + "','" + guid + "','" + value + "','" + d + "',''," + dd + ");";
                if (ds.Tables[1].Rows.Count > i)
                    strSQL += "update T_Goods_ServiceDetail set value='" + value + "',price=" + d + ",isDefault=" + dd + " where sysnumber='" + ds.Tables[1].Rows[i]["sysnumber"] + "';";
                else
                    strSQL += "insert T_Goods_ServiceDetail values('" + ds.Tables[0].Rows[0]["serviceId"] + i + "','" + ds.Tables[0].Rows[0]["serviceId"] + "','" + value + "','" + d + "',''," + dd + ");";
            }
            strSQL += " COMMIT TRANSACTION ";

            int rows = adoHelper.ExecuteSqlNonQuery(strSQL);

            if (rows > 0)
            {
                LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "修改任务服务《" + serviceName + "》", "添加", "", "", HttpContext.Current.Request.Url.ToString());

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改成功');</script>");
            }
        }
    }
}