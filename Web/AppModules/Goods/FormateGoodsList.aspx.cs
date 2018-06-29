using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Goods_FormateGoodsList : StarTech.Adapter.StarTechPage
{
    protected string strTypeList = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        string flag = "";
        if (Request.QueryString["flag"] == null)
        {
            strTypeList=GetAllFormate("");
        }
        else
        {
            flag = Request.QueryString["flag"];
            if (flag == "select")
            {
                string typeid = Request.QueryString["typeid"];
                Response.Clear();
                Response.Write(GetAllFormate(typeid));
                Response.End();
                return;
            }
            if (flag == "add")
            {
                string typeid = Request.QueryString["typeid"];
                string cateList = Request.QueryString["catelist"];
                Response.Clear();
                Response.Write(AddFormate(typeid,cateList));
                Response.End();
                return;

            }
            
        }
    }

    /// <summary>
    /// 获取基础分类
    /// </summary>
    /// <param name="typeid"></param>
    /// <returns></returns>
    protected string GetAllFormate(string typeid)
    {
        string strSQL = "select * from T_Goods_MorePropertySet order by porpertyFlag;";
        if (typeid != "")
        {
            strSQL += "select * from T_Goods_Category_MoreProperty where categoryId='"+KillSqlIn.Form_ReplaceByString(typeid,10)+"';";
        }
        AdoHelper adohelper = StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance);
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        bool istype = false;
        if (ds == null)
            return "";
        List<string> strType = new List<string>();
      
        if (ds.Tables.Count > 1)
        {
            istype = true;
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                strType.Add(ds.Tables[1].Rows[i]["morePropertyId"].ToString());
            }
        }
        string returnStr = "";
        int index = 0;
        int indextype = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (indextype == 0 && ds.Tables[0].Rows[i]["porpertyFlag"].ToString() == "select")
            {
                indextype = 1;
                index = 0;
                returnStr += "<br/><p style='width:100%;font-weight:bold;'>可选择属性</p>";
            }
            else if (i == 0)
            {
                returnStr += "<p style='width:100%;font-weight:bold;'>仅显示属性</p>";
            }
            if (istype)
            {
                if (index == 5)
                {
                    returnStr += "<br/>";
                    index = -1;
                }
                string isCheck="";
                if(strType.Contains(ds.Tables[0].Rows[i]["propertyId"].ToString()))
                {
                    isCheck="checked='checked'";
                }
                returnStr += "<span style='float:left;'><input tid='" + KillSqlIn.Form_ReplaceByString(ds.Tables[0].Rows[i]["propertyId"].ToString(), 10) + "' " + isCheck + " type='checkbox' id='fck" + KillSqlIn.Form_ReplaceByString(ds.Tables[0].Rows[i]["propertyId"].ToString(), 10) + "'>" + KillSqlIn.Form_ReplaceByString(ds.Tables[0].Rows[i]["propertyName"].ToString(), 20) + "</span>";
                
            }
            else
            {
                if (index == 5)
                {
                    returnStr += "<br/>";
                    index = -1;
                }
                returnStr += "<span style='float:left;'><input tid='" + KillSqlIn.Form_ReplaceByString(ds.Tables[0].Rows[i]["propertyId"].ToString(), 10) + "'  type='checkbox' id='fck" + KillSqlIn.Form_ReplaceByString(ds.Tables[0].Rows[i]["propertyId"].ToString(), 10) + "'>" + KillSqlIn.Form_ReplaceByString(ds.Tables[0].Rows[i]["propertyName"].ToString(), 20) + "</span>";

            }
            index++;
        }
            return returnStr;
    }

    /// <summary>
    /// 绑定分类
    /// </summary>
    /// <param name="typeid"></param>
    /// <param name="cateList"></param>
    /// <returns></returns>
    protected string AddFormate(string typeid, string cateList)
    {
        if (cateList == "")
            return "none";
        string[] categoryList = cateList.Split(',');
        string strSQL = "delete T_Goods_Category_MoreProperty where categoryId='"+KillSqlIn.Form_ReplaceByString(typeid,10)+"';";
        AdoHelper adohelper = StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance);
        for (int i = 0; i < categoryList.Length; i++)
        {
            strSQL += "insert T_Goods_Category_MoreProperty values('"+Guid.NewGuid().ToString()+"','"+typeid+"','"+KillSqlIn.Form_ReplaceByString(categoryList[i],20)+"');";
        }
        int row = adohelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
            return "success";
        return "fail";
    }

}