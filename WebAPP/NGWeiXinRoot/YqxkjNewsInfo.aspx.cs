using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;

public partial class NGWeiXinRoot_YqxkjNewsInfo : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    public string title;
    public string content;
    public string time;
    public string img;
    public string dirName;
    protected void Page_Load(object sender, EventArgs e)
    {
        int NewsID = 0;
        try { NewsID = int.Parse(Request["id"]); }
        catch { }
        this.hidNewsId.Value = NewsID.ToString();

        int CategoryId = 0;
        try { CategoryId = int.Parse(Request["dirid"]); }
        catch { }
        this.hid_dirId.Value = CategoryId.ToString();

        BindInfo(NewsID);
        GetDirName(CategoryId);
    }

    protected void BindInfo(int NewsID)
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select * from T_News where [Approved]=1 and NewsID=" + NewsID + "").Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.title = dt.Rows[0]["Title"].ToString();
            this.content = dt.Rows[0]["Body"].ToString();
            this.time = DateTime.Parse(dt.Rows[0]["ReleaseDate"].ToString()).ToString("yyyy-MM-dd");
            this.img = dt.Rows[0]["ImgLink"].ToString() == "" ? "" : "<img src='" + dt.Rows[0]["ImgLink"].ToString() + "'>";
        }
    }

    protected void GetDirName(int CategoryId)
    {
        DataTable dt = adoHelper.ExecuteSqlDataset("select categoryname from T_Category where CategoryId=" + CategoryId + "").Tables[0];
         if (dt.Rows.Count > 0)
         {
             this.dirName = dt.Rows[0]["categoryname"].ToString();
         }
    }
}