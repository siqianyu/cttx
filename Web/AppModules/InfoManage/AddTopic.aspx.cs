using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Startech.Utils;
using StarTech.DBUtility;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

public partial class AppModules_InfoManage_AddTopic : StarTech.Adapter.StarTechPage
{
    private string id, rd;
    public string mainPath = ConfigurationManager.AppSettings["Source_NewsPic"];
    protected void Page_Load(object sender, EventArgs e)
    {
        rd = Request.QueryString["rd"] == null ? "0" : KillSqlIn.Url_ReplaceByNumber(Request.QueryString["rd"].ToString(), 5);
        id = Request.QueryString["id"] == null ? "0" : KillSqlIn.Url_ReplaceByNumber(Request.QueryString["id"].ToString(), 5);
        if (!IsPostBack)
        {
            LoadPlatFormShare();
            LoadTXPlatfrom();
            LoadTopicInfo();


            if (rd == "1")
            {
                LinkButton1.Visible = false;
            }
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
            DataTable dt = DalBase.Util_GetList("select TXPTplatform from T_News where newsid=" + id).Tables[0];
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


    #region 获取专题详细信息
    /// <summary>
    /// 获得文章详细信息
    /// </summary>
    public void LoadTopicInfo()
    {
        DataTable dt = DalBase.Util_GetList("select * from T_StandardTopic where ID=" + id).Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.txtTitle.Text = dt.Rows[0]["Title"].ToString();
            this.txtContent.Text = dt.Rows[0]["TopicContent"].ToString();
            if (dt.Rows[0]["Image"].ToString() != "")
            {
                Session["img"] = dt.Rows[0]["Image"].ToString();
                this.topicImg.Src = mainPath + dt.Rows[0]["Image"].ToString();
                if (dt.Rows[0]["Image"].ToString().StartsWith("/") || dt.Rows[0]["Image"].ToString().ToLower().StartsWith("http:"))
                {
                    this.topicImg.Src = dt.Rows[0]["Image"].ToString();
                }
            }
            else
            {
                Session["img"] = "";
            }
            this.txtUrl.Text = dt.Rows[0]["Url"].ToString();
            this.txtSort.Text = dt.Rows[0]["Sort"].ToString();
            if (dt.Rows[0]["IsNew"].ToString() == "1")
            {
                this.rbtnYes.Checked = true;
                this.rbtnNo.Checked = false;
            }
            else
            {
                this.rbtnYes.Checked = false;
                this.rbtnNo.Checked = true;
            }
            this.txtKeyWord.Text = dt.Rows[0]["newskeyword"].ToString();
            this.ddlTxPlatform.SelectedValue = dt.Rows[0]["txptplatform"].ToString();
            SelectPlatFormShare(dt.Rows[0]["ShareToPlatform"].ToString());
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
        if (id == "0")
        {
            AddTopic();
        }
        else
        {
            UpdateTopic();
        }

    }

    private void AddTopic()
    {
        try
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_StandardTopic(");
            strSql.Append("Title,TopicContent,Image,Url,ShareToPlatform,Sort,IsNew,AddTime,newskeyword,txptplatform)");
            strSql.Append(" values (");
            strSql.Append("@Title,@TopicContent,@Image,@Url,@ShareToPlatform,@Sort,@IsNew,@AddTime,@newskeyword,@txptplatform)");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@TopicContent", SqlDbType.Text),
					new SqlParameter("@Image",SqlDbType.VarChar,100),
					new SqlParameter("@Url", SqlDbType.VarChar,100),
					new SqlParameter("@ShareToPlatform",SqlDbType.VarChar,100),
                    new SqlParameter("@Sort",SqlDbType.Int,32),
                    new SqlParameter("@IsNew",SqlDbType.Int,32),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@newskeyword", SqlDbType.NVarChar,100),
                    new SqlParameter("@txptplatform", SqlDbType.Int,32)};
            parameters[0].Value = this.txtTitle.Text.Trim();
            parameters[1].Value = this.txtContent.Text.Trim();
            parameters[2].Value = getImg();
            parameters[3].Value = this.txtUrl.Text.Trim();
            parameters[4].Value = GetPlatFormShare();
            parameters[5].Value = this.txtSort.Text == "" ? 99 : Convert.ToInt32(this.txtSort.Text);

            if (this.rbtnYes.Checked == true)
            {
                parameters[6].Value = 1;
            }
            else
            {
                parameters[6].Value = 0;
            }
            parameters[7].Value = DateTime.Now;
            parameters[8].Value = this.txtKeyWord.Text.Trim();
            parameters[9].Value = this.ddlTxPlatform.SelectedValue;

            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            object o = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (Convert.ToInt32(o) == 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加成功');layer_close_refresh();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加失败了'); ';</script>");
            }
        }
        catch (Exception ee)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加失败，原因:" + ee.Message + "') </script>");
        }
    }
    private void UpdateTopic()
    {
        try
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_StandardTopic ");
            strSql.Append(" set Title=@Title,TopicContent=@TopicContent,Image=@Image,Url=@Url,ShareToPlatform=@ShareToPlatform,Sort=@Sort,IsNew=@IsNew,AddTime=@AddTime,newskeyword=@newskeyword,txptplatform=@txptplatform where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@TopicContent", SqlDbType.Text),
					new SqlParameter("@Image",SqlDbType.VarChar,100),
					new SqlParameter("@Url", SqlDbType.VarChar,100),
					new SqlParameter("@ShareToPlatform",SqlDbType.VarChar,100),
                    new SqlParameter("@Sort",SqlDbType.Int,32),
                    new SqlParameter("@IsNew",SqlDbType.Int,32),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.VarChar,10),
                     new SqlParameter("@newskeyword", SqlDbType.NVarChar,100),
                      new SqlParameter("@txptplatform", SqlDbType.Int,32)
};
            parameters[0].Value = this.txtTitle.Text.Trim();
            parameters[1].Value = this.txtContent.Text.Trim();
            parameters[2].Value = getImg();
            parameters[3].Value = this.txtUrl.Text.Trim();
            parameters[4].Value = GetPlatFormShare();
            parameters[5].Value = this.txtSort.Text == "" ? 99 : Convert.ToInt32(this.txtSort.Text);

            if (this.rbtnYes.Checked == true)
            {
                parameters[6].Value = 1;
            }
            else
            {
                parameters[6].Value = 0;
            }
            parameters[7].Value = DateTime.Now;
            parameters[8].Value = id;
            parameters[9].Value = this.txtKeyWord.Text.Trim();
            parameters[10].Value = this.ddlTxPlatform.SelectedValue;

            AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
            object o = adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
            if (Convert.ToInt32(o) == 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改失败了'); ';</script>");
            }
        }
        catch (Exception ee)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改失败，原因:" + ee.Message + "') </script>");
        }
    }


    #region 获取图片

    public string getImg()
    {
        #region 保存图片
        if (!this.picUpload.Value.Equals(""))
        {
            string UploadFileLastName = this.picUpload.PostedFile.FileName.Substring(this.picUpload.PostedFile.FileName.LastIndexOf(".") + 1);//得到文件的扩展名

            if (!PubFunction.IsPic(UploadFileLastName))
            {
                JSUtility.Alert("上传图片格式不正确!");
                return Session["img"].ToString();
            }
            Random rnd = new Random();
            string UpLoadFileTime = DateTime.Now.ToString("HHmmss") + rnd.Next(9999).ToString("0000"); //生成一个新的数图片名称
            string fileName = UpLoadFileTime + "." + UploadFileLastName;//产生上传图片的名称

            string SaveFile = DateTime.Now.ToString("yyyy/MM/dd/").Replace("-", "/");

            #region 设置保存的路径
            string SevedDirectory = System.Web.VirtualPathUtility.Combine(mainPath, SaveFile);
            string phydic = MapPath(SevedDirectory);

            if (!System.IO.Directory.Exists(phydic))
            {
                System.IO.Directory.CreateDirectory(phydic);
            }
            #endregion

            this.picUpload.PostedFile.SaveAs(phydic + "" + fileName);
            string fckimg = string.Empty;
            string source = string.Empty;
            fckimg = "<a href='" + this.ResolveUrl(SevedDirectory + fileName) + "' target='_blank'  id='upannexx'>" + fileName + "</a>";
            source = this.ResolveUrl(SevedDirectory + fileName);
            return SaveFile + fileName;
        }
        else
        {
            return Session["img"].ToString();
        }

        #endregion
    }

    #endregion
}