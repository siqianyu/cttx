using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Startech.Category;
using Startech.Article;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_InfoManage_AddArticle : StarTech.Adapter.StarTechPage
{
    private string id, rd;
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {

        rd = Request.QueryString["rd"] == null ? "0" : KillSqlIn.Url_ReplaceByNumber(Request.QueryString["rd"].ToString(), 5);
        id = Request.QueryString["id"] == null ? "0" : KillSqlIn.Url_ReplaceByNumber(Request.QueryString["id"].ToString(), 5);
        if (!IsPostBack)
        {
            LoadPlatFormShare();
            LoadSubjectFormShare();
            LoadMarketFormShare();
            LoadTXPlatfrom();
            string share = ado.ExecuteSqlScalar("select ShareToPlatform from WTO_YJTB where ID=" + id).ToString();
            string share1 = ado.ExecuteSqlScalar("select ShareToSubject from WTO_YJTB where ID=" + id).ToString();
            string share2 = ado.ExecuteSqlScalar("select ShareToMarket from WTO_YJTB where ID=" + id).ToString();
            SelectPlatFormShare(share);
            SelectSubjectFormShare(share1);
            SelectMarketFormShare(share2);
        }
    }

    #region 平台共享
    public void LoadPlatFormShare()
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataTable dt = ado.ExecuteSqlDataset("select platformCode,platformName from T_Base_PlatformShare").Tables[0];
        this.ckPlatFormList.DataSource = dt;
        this.ckPlatFormList.DataValueField = "platformCode";
        this.ckPlatFormList.DataTextField = "platformName";
        this.ckPlatFormList.DataBind();
    }

    public string GetPlatFormShare()
    {
        string s = "";
        foreach (ListItem ck in this.ckPlatFormList.Items)
        {
            if (ck.Selected == true)
            {
                if (s == "")
                {
                    s += ck.Value;
                }
                else
                {
                    s += "," + ck.Value;
                }
            }
        }
        return s;
    }

    public void SelectPlatFormShare(string s)
    {
        if (s != null && s != "")
        {
            string[] list = s.Split(new char[] { ',' });
            if (list.Length >= 1)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    foreach (ListItem ck in this.ckPlatFormList.Items)
                    {
                        if (ck.Value == list[i])
                        {
                            ck.Selected = true;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region 体系平台 下拉列表类别树
    private void LoadTXPlatfrom()
    {
        this.ddlTxPlatform.DataSource = getddltree();
        ddlTxPlatform.DataTextField = "title";
        ddlTxPlatform.DataValueField = "stdcategoryid";
        ddlTxPlatform.DataBind();
        ddlTxPlatform.Items.Insert(0, new ListItem("无", "0"));

        if (id != "")
        {
            DataTable dt = DalBase.Util_GetList("select TXPTplatform from  WTO_YJTB where id=" + id).Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["TXPTplatform"] != null && dt.Rows[0]["TXPTplatform"].ToString() != "")
                {
                    this.ddlTxPlatform.SelectedValue = dt.Rows[0]["TXPTplatform"].ToString();
                }
            }
        }
    }

    public DataTable getddltree()
    {
        AdoHelper adohelp = AdoHelper.CreateHelper("DBTXInstance");
        DataTable dttree = new DataTable();
        dttree.Columns.Add("stdcategoryid", typeof(string));
        dttree.Columns.Add("title", typeof(string));
        DataTable dt = adohelp.ExecuteSqlDataset("select stdcategoryid,title from T_Local_StdCategory where parentid=0 order by sort asc").Tables[0];

        foreach (DataRow dr in dt.Rows)
        {
            dttree.Rows.Add(new object[] { dr["stdcategoryid"], "-" + dr["title"] });
            // getall(dr["stdcategoryid"].ToString(), dttree, 1);
        }
        return dttree;
    }

    private void getall(string stdcategoryid, DataTable dttree, int lev)
    {
        AdoHelper adohelp = AdoHelper.CreateHelper("DBTXInstance");
        DataTable dt = adohelp.ExecuteSqlDataset("select stdcategoryid,title from T_Local_StdCategory where parentid=" + stdcategoryid + " order by sort asc").Tables[0];
        string sp = "";
        for (int i = 0; i < lev; i++)
        {
            sp += "－－";
        }
        foreach (DataRow dr in dt.Rows)
        {
            dttree.Rows.Add(new object[] { dr["stdcategoryid"], sp + dr["title"] });
            getall(dr["stdcategoryid"].ToString(), dttree, lev + 1);
        }
    }

    #endregion



    #region 所属专题共享
    /// <summary>
    /// 所属专题
    /// </summary>
    public void LoadSubjectFormShare()
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataTable dt = ado.ExecuteSqlDataset("select * from dbo.T_Category where parentCategoryid =" + CategoryIdConfigReader.GetCategoryId("技术性贸易壁垒-产业专题研究")).Tables[0];
        this.ckSubjectFormList.DataSource = dt;
        this.ckSubjectFormList.DataValueField = "Categoryid";
        this.ckSubjectFormList.DataTextField = "title";
        this.ckSubjectFormList.DataBind();
    }

    /// <summary>
    /// 选择的专题
    /// </summary>
    public string GetSubjectFormShare()
    {
        string str = "";
        foreach (ListItem ck in ckSubjectFormList.Items)
        {
            if (ck.Selected == true)
            {
                if (str == "")
                {
                    str += ck.Text;
                }
                else
                {
                    str += "," + ck.Text;
                }
            }
        }
        return str;
    }

    /// <summary>
    /// 绑定以前选中的专题
    /// </summary>
    public void SelectSubjectFormShare(string str)
    {
        if (str != null && str != "")
        {
            string[] list = str.Split(new char[] { ',' });

            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    foreach (ListItem item in ckSubjectFormList.Items)
                    {
                        if (item.Text == list[i])
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }

    }

    #endregion

    #region 目标市场共享
    /// <summary>
    /// 目标市场研究
    /// </summary>
    public void LoadMarketFormShare()
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        DataTable dt = ado.ExecuteSqlDataset("select * from dbo.T_Category where parentCategoryid =" + CategoryIdConfigReader.GetCategoryId("技术性贸易壁垒-目标研究市场")).Tables[0];
        this.ckMarketFormList.DataSource = dt;
        this.ckMarketFormList.DataValueField = "Categoryid";
        this.ckMarketFormList.DataTextField = "title";
        this.ckMarketFormList.DataBind();
    }


    /// <summary>
    /// 选择的目标市场
    /// </summary>
    public string GetMarketFormShare()
    {
        string str = "";
        foreach (ListItem ck in ckMarketFormList.Items)
        {
            if (ck.Selected == true)
            {
                if (str == "")
                {
                    str += ck.Text;
                }
                else
                {
                    str += "," + ck.Text;
                }
            }
        }
        return str;
    }

    /// <summary>
    /// 绑定以前选中的目标市场
    /// </summary>
    public void SelectMarketFormShare(string str)
    {
        if (str != null && str != "")
        {
            string[] list = str.Split(new char[] { ',' });
            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    foreach (ListItem item in ckMarketFormList.Items)
                    {
                        if (item.Text == list[i])
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }

    }

    #endregion

    /// <summary>
    /// 提交数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            ado.ExecuteSqlNonQuery("update WTO_YJTB set ShareToPlatform='" + GetPlatFormShare() + "',txptPlatform='" + this.ddlTxPlatform.SelectedValue + "',ShareToSubject='" + GetSubjectFormShare() + "',ShareToMarket='" + GetMarketFormShare() + "' where ID=" + id);
            ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>layer_close_refresh()</script>");
        }
        catch (Exception ee)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ok", "<script>alert('设置失败')</script>");
        }
    }
}