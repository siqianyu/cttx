using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech;
using StarTech.ELife.Goods;
using StarTech.DBUtility;
using System.Data;
using System.IO;

public partial class AppModules_Goods_AddGoodsType : StarTech.Adapter.StarTechPage
{
    protected string _pageTitle = "添加分类";
    private string id;
    protected GoodsTypeBll bll=new GoodsTypeBll();
    protected string imgUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            id = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"],10);
        }
        if (!IsPostBack)
        {
            
            if (this.id != "")
            {
                GoodsTypeModel mod = bll.GetModel(this.id);
                if (mod != null)
                {
                    //this.ddlType.SelectedValue = mod.CategoryToTypeId;
                    //this.ddlPCategory.SelectedValue = mod.PCategoryId;
                    this.cSelect.categoryID = mod.PCategoryId;
                    this.txtMenuName.Text = mod.CategoryName;
                    this.txtSort.Text = mod.Orderby.ToString();
                    this.cbIsVisible.Checked = (mod.CategoryFlag == "Course") ? true : false;
                    if (mod.Url != null)
                    {
                        imgUrl = "<img src='"+mod.Url+"' width='100' height='100' />";
                    }
                    ViewState["Old_PId"] = mod.PCategoryId;
                }
            }
            else
            {
                //this.ddlPCategory.SelectedValue = this.pCatatoryId;
                //this.cSelect.categoryID = this.pCatatoryId;
            }
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {

        GoodsTypeModel mod;
        if (this.id != "" && this.id!=null)
            mod = bll.GetModel(this.id);
        else
            mod = new GoodsTypeModel();
        string pPath = "";
        string[] codeList = this.cSelect.hfCode.Split('|');
        StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");



        if (codeList.Length < 1 || cSelect.hfCode=="")
        {
            mod.PCategoryId = "";
            mod.CategoryLevel = 1;
            mod.CategoryPath = mod.CategoryId;
        }
        else
        {
            //if(this.cSelect.categoryID)
            mod.PCategoryId = codeList[codeList.Length - 1];

            DataSet ds = adoHelper.ExecuteSqlDataset("select * from T_Info_Category where categoryid='" + KillSqlIn.Form_ReplaceByString(mod.PCategoryId, 20) + "';");
            mod.CategoryLevel = Convert.ToInt32(ds.Tables[0].Rows[0]["CategoryLevel"].ToString())+1;
            pPath = ds.Tables[0].Rows[0]["CategoryPath"].ToString();
        }

        if (pPath != "")
            pPath += ",";

        mod.CategoryName = this.txtMenuName.Text.Trim();
        //mod.CategoryPath=
        //mod.Remarks = this.txtRemarks.Text.Trim();
        int orderdy=0;
        int.TryParse(this.txtSort.Text.Trim(),out orderdy);
        mod.Orderby =orderdy ;
        mod.CategoryFlag = (this.cbIsVisible.Checked == true) ? "Course" : "Category";

        if (fuImg.FileName != null && fuImg.FileName != "")
        {
            //mod.Url = fuImg.FileName;
            string nowUrl= Guid.NewGuid().ToString();
            string tzm = fuImg.FileName.Substring(fuImg.FileName.LastIndexOf("."));
            string url="/upload/Category/"+nowUrl+""+tzm;
            fuImg.SaveAs(Server.MapPath("~"+url));
            mod.Url = url;
        }

        if (this.id != "" && this.id != null)
        {
            if (this.id == mod.PCategoryId) {
                ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('上级分类不能选择当前分类');layer_close_refresh();</script>");
                return;
            }
            mod.CategoryPath = pPath + mod.CategoryId;
            if (bll.Update(mod))
            {
                LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "修改任务分类《" + mod.CategoryName + "》", "修改", "", "", HttpContext.Current.Request.Url.ToString());
                ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
            }
        }
        else
        {

            mod.CategoryId = IdCreator.CreateId("T_Info_Category", "CategoryId");

            mod.CategoryPath = pPath + mod.CategoryId;
            bll.Add(mod);
            LogAdd.CreateLog(HttpContext.Current.Session["UserId"].ToString(), "添加任务分类《" + mod.CategoryName + "》;", "添加", "", "", HttpContext.Current.Request.Url.ToString());
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('添加成功');layer_close_refresh();</script>");
            //JSUtility.ReplaceOpenerParentWindow("menuTree.aspx");
        }

        //ModMenu detail = new ModMenu();
        //detail.menuName = this.txtMenuName.Text.Trim();
        //detail.menuTarget = this.txtMenuLink.Text.Trim();
        //detail.isShow = (this.cbIsVisible.Checked == true ? 1 : 0);
        //detail.orderIndex = Convert.ToInt32(this.txtSort.Text.Trim());
        //detail.parentMenuId = Convert.ToInt32(this.cSelect.categoryID);
        //if (this._menuId != null)
        //{
        //    int menuId = Convert.ToInt32(this._menuId);
        //    detail.uniqueId = menuId;
        //    this._menu.Update(detail);
        //    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('修改成功');layer_close_refresh();</script>");
        //}
        //else
        //{
        //    this._menu.Add(detail);
        //    ClientScript.RegisterStartupScript(this.GetType(), "close", "<script>alert('添加成功');layer_close_refresh();</script>");
        //    //JSUtility.ReplaceOpenerParentWindow("menuTree.aspx");
        //}
    }


}