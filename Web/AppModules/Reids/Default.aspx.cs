using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppModules_Reids_Default : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        NG.CachHelper.Redis.RedisHelper redis = new NG.CachHelper.Redis.RedisHelper();
        string v = this.fckBodyFooter.Text;
        redis.SetStringCash("News_Footer", v);
        redis.Close();
    }
}