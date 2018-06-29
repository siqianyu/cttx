using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using StarTech;
using System.Data;

public partial class AppModules_InfoManage_ShareKeyword : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetLoadKeyword();
    }

    protected void GetLoadKeyword()
    {
        string strSQL = "Update T_News set ShareToMarket=''  where ShareToMarket is null;";
        strSQL += "Update T_News set ShareToSubject=''  where ShareToSubject is null;";
        strSQL += "Update T_News set ShareToPlatform=''  where ShareToPlatform is null;";
        strSQL += "select * from T_ShareNews;";
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = helper.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int index = Convert.ToInt32(ds.Tables[0].Rows[i]["ShareIndex"]);
                int type = Convert.ToInt32(ds.Tables[0].Rows[i]["ShareType"]);
                string ky = ds.Tables[0].Rows[i]["KeywordList"].ToString();
                if (index == 1)
                {
                    if (type == 1)
                        TextBox1.Text = ky;
                    if (type == 2)
                        TextBox2.Text = ky;
                    if (type == 3)
                        TextBox3.Text = ky;
                    if (type == 1)
                        TextBox4.Text = ky;
                }
                if (index == 2)
                {

                    if (type == 1)
                        TextBox5.Text = ky;
                    if (type == 2)
                        TextBox6.Text = ky;
                    if (type == 3)
                        TextBox7.Text = ky;
                    if (type == 4)
                        TextBox8.Text = ky;
                    if (type == 5)
                        TextBox9.Text = ky;
                    if (type == 6)
                        TextBox10.Text = ky;
                    if (type == 7)
                        TextBox11.Text = ky;
                    if (type == 8)
                        TextBox12.Text = ky;
                    if (type == 9)
                        TextBox13.Text = ky;
                }
                if (index == 3)
                {
                    if (type == 1)
                        TextBox14.Text = ky;
                    if (type == 2)
                        TextBox15.Text = ky;
                    if (type == 3)
                        TextBox16.Text = ky;
                }
            }
        }
    }
    #region 全部事件

    protected void Button1_Click(object sender, EventArgs e)
    {
        ShareNow(1, 1, this.TextBox1.Text);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ShareNow(1, 2, this.TextBox2.Text);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ShareNow(1, 3, this.TextBox3.Text);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ShareNow(1, 4, this.TextBox4.Text);
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        ShareNow(2, 1, this.TextBox5.Text);
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        ShareNow(2, 2, this.TextBox6.Text);
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        ShareNow(2, 3, this.TextBox7.Text);
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        ShareNow(2, 4, this.TextBox8.Text);
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        ShareNow(2, 5, this.TextBox9.Text);
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        ShareNow(2, 6, this.TextBox10.Text);
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        ShareNow(2, 7, this.TextBox11.Text);
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        ShareNow(2, 8, this.TextBox12.Text);
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        ShareNow(2, 9, this.TextBox13.Text);
    }
    protected void Button14_Click(object sender, EventArgs e)
    {
        ShareNow(3, 1, this.TextBox14.Text);
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        ShareNow(3, 2, this.TextBox15.Text);
    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        ShareNow(3, 3, this.TextBox16.Text);
    }

    #endregion

    /// <summary>
    /// 同步分享
    /// </summary>
    /// <param name="index"></param>
    /// <param name="type"></param>
    /// <param name="keywordList"></param>
    void ShareNow(int index,int type,string keywordList)
    {
        try
        {
            string notKy = "";
            string set = GetUpdateFilter(index, type, ref notKy);
            string kyList = keywordList.Replace("，", ",");
            string strSQL = " update T_News set " + set + " where " + notKy + " and (" + GetLikeFilter(kyList, "Title")+");";
            strSQL += " update T_Jsfg set " + set + " where " + notKy + " and (" + GetLikeFilter(kyList, "Title") + ");";
            strSQL += " update WTO_YJTB set " + set + " where " + notKy + " and (" + GetLikeFilter(kyList, "TBBT") + ");";
            AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
            int row = helper.ExecuteSqlNonQuery(strSQL);
            UpdateData(index,type,kyList);
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "<Script language='JavaScript'>alert('同步完成，共同步" + row + "条资讯！');</Script>");

        }
        catch
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "<Script language='JavaScript'>alert('同步出现异常！');</Script>");
        }
    }

     ///<summary>
     ///改动设置
     ///</summary>
     ///<param name="index">1平台 2专题 3市场</param>
     ///<param name="type"></param>
     ///<returns></returns>
    string GetUpdateFilter(int index,int type,ref string notKy)
    {
        string set = "";
        if (index == 1) 
        {
            if (type == 1)
            {
                set = " ShareToPlatform=ShareToPlatform+',FuWY' ";
                notKy = "(ShareToPlatform not like '%FuWY%' or ShareToPlatform is null )";
            }
            if (type == 2)
            {
                set = " ShareToPlatform=ShareToPlatform+',GongY' ";
                notKy = "(ShareToPlatform not like '%GongY%' or ShareToPlatform is null )";
            }
            if (type == 3)
            {
                set = " ShareToPlatform=ShareToPlatform+',NongY' ";
                notKy = "(ShareToPlatform not like '%NongY%' or ShareToPlatform is null )";
            }
            if (type == 4)
            {
                set = " ShareToPlatform=ShareToPlatform+',SheHGG' ";
                notKy = "(ShareToPlatform not like '%SheHGG%' or ShareToPlatform is null )";
            }
        } 
        else if (index == 2) 
        {
            //								

            if (type == 1)
            {
                set = " ShareToSubject=ShareToSubject+',LED' ";
                notKy = "(ShareToSubject not like '%LED%' or ShareToSubject is null )";
            }
            if (type == 2)
            {
                set = " ShareToSubject=ShareToSubject+',纺织服装' ";
                notKy = "(ShareToSubject not like '%纺织服装%' or ShareToSubject is null )";
            }
            if (type == 3)
            {
                set = " ShareToSubject=ShareToSubject+',纺织服装标签' ";
                notKy = "(ShareToSubject not like '%纺织服装标签%' or ShareToSubject is null )";
            }
            if (type == 4)
            {
                set = " ShareToSubject=ShareToSubject+',食品与农产品' ";
                notKy = "(ShareToSubject not like '%食品与农产品%' or ShareToSubject is null )";
            }
            if (type == 5)
            {
                set = " ShareToSubject=ShareToSubject+',食品标签' ";
                notKy = "(ShareToSubject not like '%食品标签%' or ShareToSubject is null )";
            }
            if (type == 6)
            {
                set = " ShareToSubject=ShareToSubject+',玩具' ";
                notKy = "(ShareToPlatform not like '%玩具%' or ShareToSubject is null )";
            }
            if (type == 7)
            {
                set = " ShareToSubject=ShareToSubject+',电动工具' ";
                notKy = "(ShareToSubject not like '%电动工具%' or ShareToSubject is null )";
            }
            if (type == 8)
            {
                set = " ShareToSubject=ShareToSubject+',通讯设备' ";
                notKy = "(ShareToSubject not like '%通讯设备%' or ShareToSubject is null )";
            }
            if (type == 9)
            {
                set = " ShareToSubject=ShareToSubject+',电动汽车' ";
                notKy = "(ShareToSubject not like '%电动汽车%' or ShareToSubject is null )";
            }

        } 
        else if (index == 3) 
        {
            //北美	欧洲	日韩
            if (type == 1)
            {
                set = " ShareToMarket=ShareToMarket+',北美' ";
                notKy = "(ShareToMarket not like '%北美%' or ShareToMarket is null )";
            }
            if (type == 2)
            {
                set = " ShareToMarket=ShareToMarket+',欧洲' ";
                notKy = "(ShareToMarket not like '%欧洲%' or ShareToMarket is null )";
            }
            if (type == 3)
            {
                set = " ShareToMarket=ShareToMarket+',日韩' ";
                notKy = "(ShareToMarket not like '%日韩%' or ShareToMarket is null )";
            }
        }
        return set;
    }
    

    /// <summary>
    /// 关键字匹配
    /// </summary>
    /// <param name="str"></param>
    /// <param name="filed"></param>
    /// <returns></returns>
    public string GetLikeFilter(string str, string filed)
    {
        string f = "";
        string[] arr = str.Split(',');
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == arr.Length - 1) { f += " " + filed + " like '%" + arr[i].Trim() + "%' "; }
            else { f += " " + filed + " like '%" + arr[i].Trim() + "%' or "; }
        }
        return f;
    }

    /// <summary>
    /// 保存关键字信息
    /// </summary>
    void UpdateData(int index,int type,string keywordList)
    {

        string sqlKy = KillSqlIn.Form_ReplaceByString(keywordList, 300);
        string strSQL = " update T_News set ShareToMarket=substring(ShareToMarket,2,500) where (patindex('%,%',ShareToMarket))=1 ;";
        strSQL += " update T_News set ShareToSubject=substring(ShareToSubject,2,500) where (patindex('%,%',ShareToSubject))=1 ;";
        strSQL += "update T_News set ShareToPlatform=substring(ShareToPlatform,2,500) where (patindex('%,%',ShareToPlatform))=1";
        strSQL += " if((select count(*) from T_ShareNews where ShareIndex="+index+" and ShareType="+type+")=0) ";
        strSQL += " insert T_ShareNews values("+index+","+type+",'"+sqlKy+"','');";
        strSQL += " else ";
        strSQL += " update T_ShareNews Set ShareIndex=" + index + ",ShareType=" + type + ",KeywordList='" + sqlKy + "' where  ShareIndex=" + index + " and ShareType=" + type + ";";
        AdoHelper helper = AdoHelper.CreateHelper("DB_Instance");
        int row = helper.ExecuteSqlNonQuery(strSQL);
    }

}