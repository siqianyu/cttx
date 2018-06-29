using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;
using System.Configuration;
using Startech.Utils;

/// <summary>
///分类选择， 使用方案：设置或读取categoryID来设置或获取当前的分类
/// </summary>
public partial class Controls_CategorySelect : System.Web.UI.UserControl
{
    
    protected string selecthtml;
    public string categoryID;
    /// <summary>
    /// 仅显示衣食住行
    /// </summary>
    public bool onlyShop = false;
    AdoHelper adohelper = AdoHelper.CreateHelper(AppConfig.DBInstance);
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
            if (hfNowValue.Value != "")
            {
                string[] strPath = hfNowValue.Value.Split('|');
                categoryID = strPath[strPath.Length - 1];
            }
            if (onlyShop)
            {
                if (categoryID == null || categoryID == "")
                {
                    GetFirstCategory();
                }
                else
                {
                    GetNowCategory(categoryID);
                }
            }

            if (categoryID == null || categoryID == "")
            {
                GetFirstCategory();
            }
            else
            {
                GetNowCategory(categoryID);
            }
        //}
    }

    /// <summary>
    /// 上级分类链，以|隔开
    /// </summary>
    public string hfCode
    {
        get
        {
            return hfNowValue.Value;
        }
    
    }

    public bool OnlyShop
    {
        get
        {
            return onlyShop;
        }
        set
        {
            onlyShop = value;
        }
    }


    /// <summary>
    /// 常规选择
    /// </summary>
    void GetFirstCategory()
    {
        string strSQL = "select categoryId,categoryName from T_Menu_Category where categoryLevel=1";
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
        {
            selecthtml += "<div id='cateDIV'><select class='cateSelect' id='cateSelect1'><option value='-1'>未选择</option>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                selecthtml += "<option value='" + ds.Tables[0].Rows[i]["categoryID"] + "'>" + ds.Tables[0].Rows[i]["categoryName"] + "</option>";
            }
            selecthtml += "</select></div>";
        }

    }

    void GetFirstShopCategory()
    {
        string strCateShop = "'" + ConfigurationManager.ConnectionStrings["stringY"].ToString() + "','" + ConfigurationManager.ConnectionStrings["stringS"].ToString() + "','" + ConfigurationManager.ConnectionStrings["stringZ"].ToString() + "','" + ConfigurationManager.ConnectionStrings["stringX"].ToString() + "'";
        string strSQL = "select categoryId,categoryName from T_Menu_Category where categoryLevel=1 and categoryId in(" + strCateShop + ")";
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            selecthtml += "<div id='cateDIV'><select class='cateSelect' id='cateSelect1'><option value='-1'>未选择</option>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                selecthtml += "<option value='" + ds.Tables[0].Rows[i]["categoryID"] + "'>" + ds.Tables[0].Rows[i]["categoryName"] + "</option>";
            }
            selecthtml += "</select></div>";
        }
    }

    /// <summary>
    /// 获取当前ID的类别
    /// </summary>
    /// <param name="categoryID"></param>
    void GetNowCategory(string categoryID)
    {
        string strSQL = "select categoryPath from T_Menu_Category where categoryID='" + categoryID + "';";
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        if(ds==null || ds.Tables.Count<1 || ds.Tables[0].Rows.Count<1)
            return;
        string []strPath = ds.Tables[0].Rows[0][0].ToString().Split(',');
        hfNowValue.Value = ds.Tables[0].Rows[0][0].ToString().Replace(',', '|');
        selecthtml += "<div id='cateDIV'></div>";
    }

}