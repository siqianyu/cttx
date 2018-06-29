using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class Main : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.span_truename.InnerText = this.TrueName;
        //this.span_departname.InnerText = this.DepartName;
        this.ltTime.Text = DateTime.Now.ToString("yyyy年MM月dd日");
        BindRootMenu();
    }

    protected void BindRootMenu()
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = new StarTech.Adapter.IACenter().GetAllMenusByUserId(int.Parse(this.UserId));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            DataRow[] rows = ds.Tables[0].Select("parentMenuId=105");
            //DataSet ds = adoHelper.ExecuteSqlDataset("select * from IACenter_Menu where parentMenuId=0 order by orderIndex");
            string menus = "";
            string firstTab = "";
            for (int i = 0; i < rows.Length; i++)
            {
                DataRow row = rows[i];
                this.hidRootMenu.Value += "tab_" + row["uniqueId"] + ",";
                if (firstTab == "") { firstTab = "tab_" + row["uniqueId"] + "|" + row["menuName"]; }
                string css = i == 0 ? "Active" : "Normal";
                menus += "<a href=\"javascript:selectTab('tab_" + row["uniqueId"] + "','" + row["menuName"] + "');void(0);\" class=\"" + css + "\" id=\"tab_" + row["uniqueId"] + "\">" + row["menuName"] + "</a>";
            }
            this.ltRootMenu.Text = menus;
            if (this.hidRootMenu.Value != "") { this.hidRootMenu.Value = this.hidRootMenu.Value.TrimEnd(','); }
            if (firstTab != "") { ClientScript.RegisterStartupScript(this.GetType(), "a", "selectTab('" + firstTab.Split('|')[0] + "','" + firstTab.Split('|')[1] + "');", true); }
        }
    }
}