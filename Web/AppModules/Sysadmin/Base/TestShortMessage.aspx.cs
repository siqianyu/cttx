using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech;
using System.Net;
using System.Text;
using System.IO;

public partial class AppModules_Sysadmin_Base_TestShortMessage : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btSend_Click(object sender, EventArgs e)
    {
        string mobile =KillSqlIn.Form_ReplaceByString(txtPhone.Text,20);
        string url = "http://elife2.hzst.com/ServiceInterface/Member/Mobile.ashx?flag=yzm&mobile="+mobile;
        Response.Redirect(url);
        return;

        //WebRequest r = WebRequest.Create(url);
        //WebResponse response = r.GetResponse();
        //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
        //string str = reader.ReadToEnd();
        //reader.Close();
        //reader.Dispose();
        //response.Close();
        //txtYZM.Text = str;
    }
}