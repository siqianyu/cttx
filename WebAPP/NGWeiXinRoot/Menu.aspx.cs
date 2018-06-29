using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class NGWeiXinRoot_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //创建菜单栏
        FileStream fs1 = new FileStream(Server.MapPath("Menu.txt") + "", FileMode.Open);
        StreamReader sr = new StreamReader(fs1, Encoding.GetEncoding("GBK"));
        string menu = sr.ReadToEnd();
        sr.Close();
        fs1.Close();
        string access_token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string result = NG.WeiXin.NGWeiXinPubTools.GetPage("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token, menu);
        this.TextBox1.Text = result;
    }
}