using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
////using NGWeb.News;
////using NGWeb.Utils;
using System.Text;
//using Startech.System;
////using NGWeb.Category;
using System.Collections.Generic;
using StarTech.DBUtility;
using NGShop.Bll;

public partial class Admin_AppModules_Question_CouseTestZX : StarTech.Adapter.StarTechPage
{
    //NewsModel model = new NewsModel();
    //NewsBLL bll = new NewsBLL();
    public int PageIndex =  0;
    //CustomPrincipal p = CustomPrincipal.CurrentRequestPrincipal;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            this.hid_goodsId.Value = Request["goodsId"] == null ? "" : Request["goodsId"];
            this.hid_categoryId.Value = Request["categoryId"] == null ? "" : Request["categoryId"];
            this.hid_pgoodsid.Value = Request["pgoodsId"] == null ? "" : Request["pgoodsId"];
            BindNewsCateoryDrp();
  

        }
    }


    #region 搜素条件
    /// <summary>
    ///搜索条件 
    /// </summary>
    private string SetFilter()
    {
        StringBuilder filter = new StringBuilder("1=1 and courseType='" + this.hid_goodsId.Value + "'");


        ViewState["Filter"] = filter.ToString();
        return filter.ToString();
    }
    #endregion

    //编辑数据源
    protected void EditDataSource(ref DataTable dt)
    {
        dt.Columns.Add("add_typename");
        //DataTable dtCate = new TableObject(" T_Base_CourseCategory").Util_GetList("*", "1=1");
        foreach (DataRow row in dt.Rows)
        {
            //DataRow[] rowsCate = dtCate.Select("CategoryId='" + row["CourseType"].ToString() + "'");
            //row["add_typename"] = rowsCate.Length > 0 ? rowsCate[0]["CategoryName"].ToString() : "";
        }
    }
    

    #region 绑定新闻类别下拉框
    /// <summary>
    /// 绑定新闻类别下拉框列表
    /// </summary>
    private void BindNewsCateoryDrp()
    {

    }
    #endregion

    #region 新闻类型
    /// <summary>
    /// 新闻类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected string ShowPic(string type)
    {
        if (type == "1")
        {
            return "图片新闻";
        }
        else
        {
            return "普通新闻";
        }

    }
    protected string GetTypeName(string type)
    {
        string returnValue = "";
        switch (type)
        {
            case "DX": returnValue = "单选题";
                break;
            case "FX": returnValue = "复选题";
                break;
            case "PD": returnValue = "判断题目";
                break;
        }
        return returnValue;
    }
    #endregion

    //删除按钮
    [AjaxPro.AjaxMethod]
    public string Delete(string sysNums)
    {
        AdoHelper adohelper = AdoHelper.CreateHelper("DBInstance");
        string sys = "'" + sysNums.Replace(",", "','") + "'";
        DataTable dt = adohelper.ExecuteSqlDataset("select * from T_Test_day where sysnumber='" + sysNums.Split(',')[0].ToString() + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            string questions = dt.Rows[0]["Questions"].ToString().Replace(",", "','");
            adohelper.ExecuteSqlNonQuery("update T_Test_Queston set orwner='0' where sysnumber in ('" + questions + "')");
        }
        string sqlDel = "delete from T_Test_day  where Sysnumber in(" + sys + ")";
        int result = adohelper.ExecuteSqlNonQuery(sqlDel);
        //int result = new NGShop.Dal.DalTestday().DeleteList(sys);

        return result.ToString();
    }
}
