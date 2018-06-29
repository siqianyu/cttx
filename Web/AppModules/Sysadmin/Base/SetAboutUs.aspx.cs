using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using StarTech;
using StarTech.ELife.Base;
using System.Text;

public partial class AppModules_Sysadmin_Base_SetBToMPrice : StarTech.Adapter.StarTechPage
{
    AdoHelper adohelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (System.IO.File.Exists(Server.MapPath("~\\aboutUs.txt")))
            {
                string str = System.IO.File.ReadAllText(Server.MapPath("~\\aboutUs.txt"));
                string[] info = str.Split(new string[] { "_$$_" }, StringSplitOptions.RemoveEmptyEntries);
                if (info.Length == 2)
                {
                    txtVersion.Text = info[0];
                    txtAboutUs.Text = info[1];
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.IO.File.WriteAllText(Server.MapPath("~\\aboutUs.txt"), txtVersion.Text + "_$$_" + txtAboutUs.Text, Encoding.UTF8);
        this.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('提交成功');</script>");
    }
}