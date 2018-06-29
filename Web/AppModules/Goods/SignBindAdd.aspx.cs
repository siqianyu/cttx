using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using Startech.Utils;

public partial class AppModules_Goods_SignBindAdd : StarTech.Adapter.StarTechPage
{
    protected static string goodsid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                goodsid = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"],20);
                string strSQL = "select  * from T_Goods_info where goodsid='"+goodsid+"';";
                strSQL += "select * from T_Goods_Sign;";
                strSQL+="select * from T_Goods_SignBind where goodsid='"+goodsid+"';";
                AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
                DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
                llName.Text = ds.Tables[0].Rows[0]["goodsName"].ToString();
                llCode.Text = ds.Tables[0].Rows[0]["goodsCode"].ToString();
                List<string> signList = new List<string>();
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    signList.Add(ds.Tables[2].Rows[i]["signid"].ToString());
                }
                string llText="";
                for(int i=0;i<ds.Tables[1].Rows.Count;i++)
                {
                    if(signList.Contains(ds.Tables[1].Rows[i]["signid"].ToString()))
                    {
                        llText += "<input checked='checked' type='checkbox' signname='" + ds.Tables[1].Rows[i]["signname"].ToString() + "' signid='" + ds.Tables[1].Rows[i]["signid"].ToString() + "' class='ckSign' />";
                    }else
                    {
                        llText += "<input type='checkbox' signname='" + ds.Tables[1].Rows[i]["signname"].ToString() + "' signid='" + ds.Tables[1].Rows[i]["signid"].ToString() + "' class='ckSign' />";
                    }
                    llText+=ds.Tables[1].Rows[i]["signname"].ToString();
                    if((i+1)%6==0)
                    {
                        llText+="<br/>";
                    }
                }
                llSign.Text=llText;
            }
        }
    }


    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        string []signList = hfSign.Value.Split(',');
        string strSQL = "delete T_Goods_SignBind where goodsid='"+goodsid+"'";
        for (int i = 0; i < signList.Length; i++)
        {
            string signid = KillSqlIn.Form_ReplaceByString(signList[i],20).Trim();
            strSQL += "insert T_Goods_SignBind values('"+goodsid+"','"+signid+"','');";
        }
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        int row = adohelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加成功');</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加失败');</script>");
        }
    }
}