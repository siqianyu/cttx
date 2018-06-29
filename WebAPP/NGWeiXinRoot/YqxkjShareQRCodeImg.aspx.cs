using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class NGWeiXinRoot_YqxkjShareQRCodeImg : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["fromuser"] != null && Request["imgpath"] != null)
        {
            this.ltUser.Text = Server.UrlDecode(Request["fromuser"].ToString());
            this.img_qrcode.Src = Server.UrlDecode(Request["imgpath"].ToString());
        }
    }
}