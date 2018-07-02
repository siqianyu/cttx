using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;
using System.Data.SqlClient;

public partial class CTTXWeiXinRoot_CTTXUserRegPersonInfo : System.Web.UI.Page
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected string HeadImg = "";
    protected string MemberId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MemberId"] == null || Session["MemberId"] == "")
        {
            Response.Redirect("/Login.aspx?redirect_url=CTTXUserLogin.aspx");
        }
        else
        {
            MemberId = Session["MemberId"].ToString();
        }
        string strSql = "select top 1* from t_member_info where memberId=@memberId";
        DataTable dtUser = ado.ExecuteSqlDataset(strSql, new SqlParameter("@memberId", MemberId)).Tables[0];
        if (dtUser.Rows.Count > 0)
        {
            string headImg = dtUser.Rows[0]["HeadImg"].ToString();
            if (string.IsNullOrEmpty(headImg))
            {
                if (Session["wx_headimgurl"] != null && Session["wx_headimgurl"] != "")
                {
                    this.HeadImg = Session["wx_headimgurl"].ToString();
                    hidHeadImg.Value = headImg;
                }
            }
            else
            {
                this.HeadImg = headImg;
                hidHeadImg.Value = headImg;
            }
        }
    }
}