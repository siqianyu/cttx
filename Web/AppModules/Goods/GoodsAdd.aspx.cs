using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.ELife.Goods;
using System.Data;
using StarTech.DBUtility;
using Startech.Utils;
using System.Data.SqlClient;

public partial class ShopSeller_GoodsAdd : StarTech.Adapter.StarTechPage
{
    public string id;
    public string isShow;
    public string typeId;
    public string goodsCode;
    public string cateId;
    protected string vipDs1;
    protected string vipDs2;

    public GoodsBll bll = new GoodsBll();

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.cselect.OnlyShop = true;

        this.id = (Request["id"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["id"], 50);

        this.isShow = (Request["isShow"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByNumber(Request["isShow"], 10);
        this.typeId = (Request["typeId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["typeId"], 50);
        this.cateId = (Request["cateId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["cateId"], 50);

        //GetVipPrice();
        if (!IsPostBack)
        {
            BindType();
            InitForm();


            if (this.isShow == "1")
            {

            }
        }

    }

    protected void InitForm()
    {
        //string signList = "";
        string sign = "";
        if (this.id != "")
        {
            GoodsModel mod = bll.GetModel(this.id);
            if (mod != null)
            {
                this.txtName.Value = mod.GoodsName;
                this.txtOrder.Value = mod.Orderby.ToString();
                this.txtRemarks.Text = mod.Remarks;
                this.div_img.Visible = true;
                this.Image1.ImageUrl = mod.GoodsSmallPic;
                this.txtSampleDesc.Text = mod.GoodsSampleDesc;
                this.hzst_ckeditor.Text = mod.GoodsDesc;
                this.cselect.categoryID = mod.CategoryId;

                this.goodsCode = this.txtGoodsCode.Value = mod.GoodsCode;
                if (mod.IsHot == 1) { this.cbIsHot.Checked = true; }
                if (mod.IsNew == 1) { this.cbIsNew.Checked = true; }
                if (mod.IsRec == 1) { this.cbIsRec.Checked = true; }
                //if (mod.IsSpe == 1) { this.cbIsSpe.Checked = true; }
                this.txtSalePrice.Value = mod.SalePrice.ToString();
                this.txtMarketPrice.Value = mod.MarketPrice.ToString();
                if (mod.MorePropertys != "") { this.ddlMorePropertys.SelectedValue = mod.MorePropertys; }
                this.rdIsSale.SelectedValue = mod.IsSale.ToString();
                this.hidOldGoodsCode.Value = mod.GoodsCode;

                sign = mod.signId;
                hfSign.Value = sign;

                //20160605

                //this.txtJobStartTime.Value = mod.JobStartTime.ToString("yyyy-MM-dd HH:mm");
                //this.txtJobEndTime.Value = mod.JobEndTime.ToString("yyyy-MM-dd HH:mm");
                this.ddlJobType.Value = mod.JobType;
                this.txtTotalSaleCount.Value = mod.TotalSaleCount.ToString();

                //this.llRemark.Text = GetMemberPrice(this.id);
            }
        }
        else
        {
            this.txtGoodsCode.Value = IdCreator.CreateId("T_Goods_Info", "GoodsCode");
            if (this.cateId != "") { //this.ddlPCategory.SelectedValue = this.cateId;
                this.cselect.categoryID = this.cateId;
            }
            //this.txtJobStartTime.Value = this.txtJobEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        string strSQL = "";
        strSQL += "select * from T_Goods_Sign;";
        strSQL += "select * from T_Goods_Service order by orderby asc;";
        //strSQL += "select * from T_Goods_SignBind where goodsid='" + id + "';";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        List<string> signList = new List<string>();
        List<string> serviceList = new List<string>();
        if (sign != "" && sign !=null)
            signList = sign.Split(',').ToList();
       
        string llText = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (signList.Contains(ds.Tables[0].Rows[i]["signid"].ToString().Trim()))
            {
                llText += "<input checked='checked' type='checkbox' signname='" + ds.Tables[0].Rows[i]["signname"].ToString() + "' signid='" + ds.Tables[0].Rows[i]["signid"].ToString() + "' class='ckSign' />";
            }
            else
            {
                llText += "<input type='checkbox' signname='" + ds.Tables[0].Rows[i]["signname"].ToString() + "' signid='" + ds.Tables[0].Rows[i]["signid"].ToString() + "' class='ckSign' />";
            }
            llText += ds.Tables[0].Rows[i]["signname"].ToString();
            if ((i + 1) % 6 == 0)
            {
                llText += "<br/>";
            }
            ltSign.Text = llText;
        }



    }


    protected void btnSave_Click(object sender, EventArgs e)
    {


        int updateInt = -1;
        if (this.id == "")
        {
            if (this.FileUpload1.HasFile == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请选择任务图片');</script>");
                return;
            }
           

            GoodsModel mod = new GoodsModel();
            mod.GoodsId = IdCreator.CreateId("T_Goods_Info", "GoodsId");
            mod.AddTime = DateTime.Now;

            if (hfSign.Value.Length > 0)
                mod.signId = hfSign.Value;
            else
                mod.signId = "";
            string fileExt = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();

           


            if (fileExt != ".jpeg" && fileExt != ".jpg" && fileExt != ".png" && fileExt != ".bmp" && fileExt != ".gif")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('图片格式不正确');</script>");
                return;
            }
            if (this.FileUpload1.PostedFile.ContentLength > 1024000)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('图片不能大于1M');</script>");
                return;
            }
            string newFileName = Guid.NewGuid().ToString() + fileExt;
            string dir = "/upload/goodsadmin/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            if (System.IO.Directory.Exists(Server.MapPath(dir)) == false) { System.IO.Directory.CreateDirectory(Server.MapPath(dir)); }
            string newPath = dir + newFileName;
            

            this.FileUpload1.SaveAs(Server.MapPath(newPath));
            //缩略图
            MakeSmallPic(Server.MapPath(newPath), Server.MapPath(newPath.Replace(fileExt, "_s" + fileExt)));
            mod.GoodsSmallPic = newPath.Replace(fileExt, "_s" + fileExt);
            //mod.GoodsSmallPic = newPath.Replace(fileExt, "" + fileExt);
            ViewState["OriginalBigImg"] = mod.GoodsSmallPic;
            GetFormInfo(ref mod);
            if (mod.CategoryId == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('未选择分类')</script>");

            }
            if (bll.Add(mod) > 0)
            {
                string strShop = "select shopId from T_Shop_User where isdefault=1;";

                DataSet ds= StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance).ExecuteSqlDataset(strShop);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string newGuid = Guid.NewGuid().ToString();
                        string shopId = ds.Tables[0].Rows[0][0].ToString();
                        string goodsId = mod.GoodsId;
                        int num = mod.Sotck.Value;
                        decimal price = mod.SalePrice.Value;
                        int isSell = mod.IsSale.Value;
                        DateTime addt = DateTime.Now;
                        string goodsCode = mod.GoodsCode;
                        string strGoods = "insert into T_Shop_Goods values('"+newGuid+"','"+shopId+"','"+goodsId+"',"+num+","+price+","+isSell+",'"+addt+"','"+goodsCode+"',0,0,0,0);";
                        StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance).ExecuteSqlNonQuery(strGoods);
                    }
                }
                string strItem = "insert T_Menu_Item(itemName,itemImgSrc,ifBuy,GoodsId,orderBy,remark,unit) values(@itemName,@itemImgSrc,@ifBuy,@GoodsId,@orderBy,@remark,@unit);";
                SqlParameter[] q = {
                                     //  @itemImgSrc,@ifBuy,@GoodsId,@orderBy,@remark,@unit
                                   new SqlParameter("@itemName",SqlDbType.VarChar),
                                   new SqlParameter("@itemImgSrc",SqlDbType.VarChar),
                                   new SqlParameter("@ifBuy",SqlDbType.Int),
                                   new SqlParameter("@GoodsId",SqlDbType.VarChar),
                                   new SqlParameter("@orderBy",SqlDbType.Int),
                                   new SqlParameter("@remark",SqlDbType.VarChar),
                                   new SqlParameter("@unit",SqlDbType.VarChar)
                                   };
                q[0].Value = mod.GoodsName;
                q[1].Value = mod.GoodsSmallPic;
                q[2].Value = 1;
                q[3].Value = mod.GoodsId;
                q[4].Value = 9999;
                q[5].Value = "任务“"+mod.GoodsName+"”添加";
                q[6].Value = mod.Uint;

                int rows = StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance).ExecuteSqlNonQuery(strItem, q);

                //默认幻灯图片
                //if (ViewState["OriginalBigImg"] != null) { AddDefaultPic(mod.GoodsId, ViewState["OriginalBigImg"].ToString()); }
                LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "添加课程《" + mod.GoodsName + "》", "添加", "", "", HttpContext.Current.Request.Url.ToString());

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>if(confirm('保存成功')){location.href='GoodsAdd.aspx?id=" + mod.GoodsId + "'}else{}</script>");
            }
        }
        else
        {
            updateInt = 0;
            //string strSQL = "select * from T_Goods_Update where goodsid='" + id+"' and updatetime>'"+DateTime.Now.ToShortDateString()+" 00:00:00';";
            string strSQL = "";
            AdoHelper adohelper = StarTech.DBUtility.AdoHelper.CreateHelper(AppConfig.DBInstance);
            //DataSet ds = adohelper.ExecuteSqlDataset(strSQL);

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >= 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 2)
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('编辑成功');</script>");
            //        return;
            //    }
            //    else
            //    {
            //        updateInt = 2 - ds.Tables[0].Rows.Count;
            //    }
            //}



            GoodsModel mod = bll.GetModel(this.id);
            if (this.FileUpload1.HasFile == true)
            {
                string fileExt = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();
                if (fileExt != ".jpeg" && fileExt != ".jpg" && fileExt != ".png" && fileExt != ".bmp" && fileExt != ".gif")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('图片格式不正确');</script>");
                    return;
                }
                if (this.FileUpload1.PostedFile.ContentLength > 10240000)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('图片不能大于10M');</script>");
                    return;
                }
                string newFileName = Guid.NewGuid().ToString() + fileExt;
                string dir = "/upload/goodsadmin/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                if (System.IO.Directory.Exists(Server.MapPath(dir)) == false) { System.IO.Directory.CreateDirectory(Server.MapPath(dir)); }
                string newPath = dir + newFileName;
                
                this.FileUpload1.SaveAs(Server.MapPath(newPath));
                //缩略图
                MakeSmallPic(Server.MapPath(newPath), Server.MapPath(newPath.Replace(fileExt, "_s" + fileExt)));
                mod.GoodsSmallPic = newPath.Replace(fileExt, "_s" + fileExt);
                //mod.GoodsSmallPic = newPath.Replace(fileExt, "" + fileExt);
                ViewState["OriginalBigImg"] = mod.GoodsSmallPic;
            }
            //hfSign.Value = hfSign.Value.Replace("--", "-");
            if (hfSign.Value.Length > 0)
                mod.signId = hfSign.Value;
            else
                mod.signId = "";

          

            GetFormInfo(ref mod);
            if (bll.Update(mod))
            {
                adohelper.ExecuteSqlNonQuery("update T_Goods_Info set JobDay=datediff(day,JobStartTime,JobEndTime) where GoodsId='" + mod.GoodsId + "'");

                //默认图片
                //if (ViewState["OriginalBigImg"] != null) { AddDefaultPic(mod.GoodsId, ViewState["OriginalBigImg"].ToString()); }
                //if (updateInt == -1)
                //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('保存成功');location.href='GoodsList.aspx?id=" + this.cselect.categoryID + "';</script>");
                //else{
                strSQL = "insert T_Goods_Update values('"+mod.GoodsId+"',getdate(),'');";
                adohelper.ExecuteSqlNonQuery(strSQL);
                LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "编辑任务《" + mod.GoodsName + "》", "编辑", "", "", HttpContext.Current.Request.Url.ToString());

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('编辑成功');</script>");
                //}
            }
        }
    }

    protected void GetFormInfo(ref GoodsModel mod)
    {


        mod.GoodsName = this.txtName.Value.Trim();
        mod.Orderby = Convert.ToInt32(this.txtOrder.Value.Trim() == "" ? "0" : this.txtOrder.Value.Trim());
        mod.Remarks = this.txtRemarks.Text;
        mod.GoodsSampleDesc = this.txtSampleDesc.Text;
        mod.GoodsDesc = this.hzst_ckeditor.Text;


        string[] codeList = this.cselect.hfCode.Split('|');
        if (codeList.Length < 1 || cselect.hfCode == "")
        {
            mod.CategoryId = "1";
        }
        else
        {
            mod.CategoryId = codeList[codeList.Length - 1];

        }

        mod.GoodsCode = this.txtGoodsCode.Value.Trim();
        mod.IsHot = this.cbIsHot.Checked == true ? 1 : 0;
        mod.IsNew = this.cbIsNew.Checked == true ? 1 : 0;
        mod.IsSpe = 0;
        mod.IsRec = this.cbIsRec.Checked == true ? 1 : 0;
        //mod.IsSpe = this.cbIsSpe.Checked == true ? 1 : 0;
        mod.SalePrice = Convert.ToDecimal(this.txtSalePrice.Value.Trim());
        mod.IsSale = Int32.Parse(this.rdIsSale.SelectedValue);
        mod.MorePropertys = this.ddlMorePropertys.SelectedValue;
        //20160605
        mod.MarketPrice = Convert.ToDecimal(this.txtMarketPrice.Value.Trim());
        mod.JobStartTime = DateTime.Now;
        mod.JobEndTime = DateTime.Now;
        mod.JobType = this.ddlJobType.Value;
        mod.TotalSaleCount = Int32.Parse(this.txtTotalSaleCount.Value);
        

    }



    protected void BindType()
    {
        //DataTable dtP = new BllGoodsCategory().ListAllCategory();
        //this.ddlPCategory.DataTextField = "categoryName";
        //this.ddlPCategory.DataValueField = "categoryId";
        //this.ddlPCategory.DataSource = dtP;
        //this.ddlPCategory.DataBind();
        //this.ddlPCategory.Items.Insert(0, new ListItem("", ""));

        //DataTable dtB = new TableObject(" T_Provider_Info").Util_GetList("*", "1=1");
        //this.ddlProvider.DataTextField = "providerName";
        //this.ddlProvider.DataValueField = "providerId";
        //this.ddlProvider.DataSource = dtB;
        //this.ddlProvider.DataBind();
        //this.ddlProvider.Items.Insert(0, new ListItem("", ""));

        //DataTable dtB2 = new TableObject("T_Goods_Brand").Util_GetList("*", "tableName='book'");
        //this.ddlBook.DataTextField = "brandName";
        //this.ddlBook.DataValueField = "brandId";
        //this.ddlBook.DataSource = dtB2;
        //this.ddlBook.DataBind();

        //DataTable dtB3 = new TableObject("T_Goods_Brand").Util_GetList("*", "tableName='area'");
        //this.ddlArea.DataTextField = "brandName";
        //this.ddlArea.DataValueField = "brandId";
        //this.ddlArea.DataSource = dtB3;
        //this.ddlArea.DataBind();
        //this.ddlArea.Items.Insert(0, new ListItem("", ""));
    }


    protected void MakeSmallPic(string oldPicPath, string smallPicPath)
    {
        try
        {
            System.Threading.Thread.Sleep(500);
            WLWang.MakeThumbnail.MakeToThumbnail(oldPicPath, smallPicPath, 470, 280, "Cut");
            // 生成缩略图方法 
            //生成的缩略图稍微比前台显示的图片大点，这样显示的清晰度高点（如前台显示100*150，则生成200*300）
        }
        catch {  }
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{

    //}



    /// <summary>
    /// 报价详情
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    protected string GetMemberPrice(string GoodsId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string s = "";
        string sql = @"SELECT [PriceId]
      ,[JobType]
      ,[GoodsId]
      ,[MemberMobile]
      ,[MemberPrice]
      ,[CreateTime]
      ,[IsLast]
      ,[IsEmployerSelect]
      ,[EmployerSelectTime]
      ,[IsMemberConfirm]
      ,[MemberConfirmTime]
      ,b.TrueName
      FROM T_Goods_MemberPrice a,T_Member_Info b
      where a.MemberId=b.MemberId and a.GoodsId='" + GoodsId + "'";
        sql += " order by a.MemberId";

        //Response.Write(sql);
        DataSet ds = adoHelper.ExecuteSqlDataset(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            s += "<table>";
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                s += "<tr><td>" + row["TrueName"] + "</td><td>" + row["CreateTime"] + "</td><td>" + row["MemberPrice"] + "</td><td>" + ((row["IsLast"].ToString() == "1") ? "最新" : "历史") + "</td><td>" + (row["IsEmployerSelect"].ToString() == "1" ? "选择" : "") + "</td></tr>";
            }
            s += "</table>";
        }
        return s;
    }
}