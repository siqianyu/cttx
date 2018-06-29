using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class NGWeiXinRoot_News_NewsDetail : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    public string title;
    public string content;
    public string time;
    public string img;
    protected void Page_Load(object sender, EventArgs e)
    {
        int NewsID = 0;
        try { NewsID = int.Parse(Request["id"]); }
        catch { }
        this.hidNewsId.Value = NewsID.ToString();

        int CategoryId = 0;
        try { CategoryId = int.Parse(Request["dirid"]); }
        catch { }
        this.hidFirstDirId.Value = CategoryId.ToString();

        BindInfo(NewsID);
    }

    protected void BindInfo(int NewsID)
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_News where [Approved]=1 and NewsID=" + NewsID + "").Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.title = dt.Rows[0]["Title"].ToString();
            this.content = dt.Rows[0]["Body"].ToString();
            if (this.content.ToLower().IndexOf("<pre>") > -1 && this.content.ToLower().IndexOf("</pre>") > -1)
            {
                this.content = this.content.Replace("<pre>", "").Replace("</pre>", "").Replace("<PRE>", "").Replace("</PRE>", "");
                this.content = this.content.Replace("<span>", "<p>").Replace("</span>", "</p>").Replace("<SPAN>", "<p>").Replace("</SPAN>", "</p>");
            }
            this.time = DateTime.Parse(dt.Rows[0]["ReleaseDate"].ToString()).ToString("yyyy-MM-dd");
            this.img = dt.Rows[0]["ImgLink"].ToString() == "" ? "" : "<img src='" + dt.Rows[0]["ImgLink"].ToString() + "'>";
        }
    }

}