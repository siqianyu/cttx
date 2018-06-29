using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;

public partial class NGWeiXinRoot_YqxkjNewsList2 : System.Web.UI.Page
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    public string dirName;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hid_dirId.Value = Common.NullToZero(Request["dirId"]).ToString();
        GetDirName(int.Parse(this.hid_dirId.Value));
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