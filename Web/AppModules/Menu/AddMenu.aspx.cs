using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Startech.Utils;
using StarTech.ELife.Menu;
using System.IO;
using StarTech.DBUtility;
using System.Data;

public partial class AppModules_Menu_AddMenu : StarTech.Adapter.StarTechPage
{
    protected string menuId = "";
    protected string stepHtml = "";
    protected string itemHtmlZ = "";
    protected string itemHtmlF = "";
    protected string itemHtmlT = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null)
        {
            menuId = KillSqlIn.Form_ReplaceByString(Request.QueryString["id"], 10);
            if (!IsPostBack)
            {

                GetMenu();
            }
        }
        if (!IsPostBack)
        {
            bindSign();
        }
    }


    protected void bindSign()
    {
        string sign = hfSign.Value;
        string strSQL = "";
        strSQL += "select * from T_Menu_Sign;";
        //strSQL += "select * from T_Goods_SignBind where goodsid='" + id + "';";
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        List<string> signList = new List<string>();

        if (sign != "" && sign != null)
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

    /// <summary>
    /// 获取菜单
    /// </summary>
    protected void GetMenu()
    {
        MenuModel model= new MenuBll().GetModel(menuId);
        txtMenuName.Text = model.menuName;
        txtFlavor.Text = model.Flavor;
        txtTechnology.Text = model.Technology;
        txtFlavor.Text = model.Flavor;
        txtCookieTime.Text = model.CookingTime;
        txtCookingSkill.Text = model.CookingSkill;
        txtCalorie.Text = (model.Calorie).ToString();
        cbShow.Checked = model.isShow.Value == 1;
        cbTop.Checked = model.isTop == 1;
        selectMenu.categoryID = model.categoryId;
        hfSign.Value = model.signId;
        if (model.imgSrc != null && model.imgSrc != "")
        {
            llBigImg.Text = "<img src='"+model.imgSrc+"' width='100px' height='100px'/>";
        }
        if (model.smallImgSrc != null && model.smallImgSrc != "")
        {
            llSmallImg.Text = "<img src='" + model.smallImgSrc + "' width='100px' height='100px'/>";
        }
        GetItemInfo(menuId);
    }

    /// <summary>
    /// 保存信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        MenuModel model = new MenuModel();
        if (menuId!=null && menuId != "")
        {
            model = new MenuBll().GetModel(menuId);
            if (model == null)
                model = new MenuModel();
        }
         model.menuName = txtMenuName.Text;
         model.Flavor=txtFlavor.Text;
        //model.Technology=ddlTechnology.SelectedValue;
         model.Technology = txtTechnology.Text;
         model.Flavor=txtFlavor.Text;
         model.CookingTime=txtCookieTime.Text;
         model.CookingSkill=txtCookingSkill.Text;
         string[] strCode = selectMenu.hfCode.Split('|');
         if(strCode.Length>0)
         model.categoryId = strCode[strCode.Length-1];
         if (strCode[strCode.Length - 1] == "")
         {
             ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('未选择分类');</script>");

         }
         if (hfSign.Value.Length > 0)
             model.signId = hfSign.Value;
         else
             model.signId = "";

         decimal d = 0;
         if (decimal.TryParse(txtCalorie.Text, out d))
         {
             model.Calorie = d;
         }
         model.isShow = cbShow.Checked ? 1 : 0;
         model.isTop = cbTop.Checked ? 1 : 0;

         if (fuBigImg.FileName.ToLower().Contains(".jpg") || fuBigImg.FileName.ToLower().Contains(".png") || fuBigImg.FileName.ToLower().Contains(".bmp") || fuBigImg.FileName.ToLower().Contains(".gif"))
         {
             string filePath = "/Upload/Menu/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
             if(!Directory.Exists(Server.MapPath(filePath)))
             {
                 Directory.CreateDirectory(Server.MapPath(filePath));
             }
             string fileName = DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + new Random().Next(10, 99);
             string tzm = fuBigImg.FileName.Substring(fuBigImg.FileName.LastIndexOf("."));
             fuBigImg.SaveAs(Server.MapPath(filePath+fileName + tzm));
             model.imgSrc = filePath + fileName + tzm;
             if (model.imgSrc != null && model.imgSrc != "")
             {
                 llBigImg.Text = "<img src='" + model.imgSrc + "' width='100px' height='100px'/>";
             }

         }
         else if (fuBigImg.FileName != null && fuBigImg.FileName != "")
         {
             ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('展示图图片格式不正确');</script>");
             return;
         }

         if (fuSmallImg.FileName.ToLower().Contains(".jpg") || fuSmallImg.FileName.ToLower().Contains(".png") || fuSmallImg.FileName.ToLower().Contains(".bmp") || fuSmallImg.FileName.ToLower().Contains(".gif"))
         {
             string filePath = "/Upload/Menu/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
             string fileName = DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + new Random().Next(10, 99);
             string tzm = fuSmallImg.FileName.Substring(fuSmallImg.FileName.LastIndexOf("."));
             fuSmallImg.SaveAs(Server.MapPath(filePath + fileName + tzm));
             model.smallImgSrc = filePath + fileName + tzm;
             if (model.smallImgSrc != null && model.smallImgSrc != "")
             {
                 llSmallImg.Text = "<img src='" + model.smallImgSrc + "' width='100px' height='100px'/>";
             }
         }
         else if(fuSmallImg.FileName!=null && fuSmallImg.FileName!="")
         {
             ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('预览图图片格式不正确');</script>");
             return;
         }

         if (menuId == null || menuId == "")
         {
             model.menuId = IdCreator.CreateId("T_Menu_Info", "menuId");
             model.AddTime = DateTime.Now;
             if (new MenuBll().Add(model))
             {
                 BindItemInfo(model.menuId);
                 LogAdd.CreateLog(Session["UserId"].ToString(), "添加菜谱《" + model.menuName + "》", "添加", "", "", Request.Url.ToString());
                 ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加成功');layer_close_refresh();</script>");
             }
         }
         else
         {
             //model.menuId = IdCreator.CreateId("T_Menu_Info", "menuId");
             if (new MenuBll().Update(model))
             {
                 BindItemInfo(model.menuId);
                 LogAdd.CreateLog(Session["UserId"].ToString(), "修改菜谱《" + model.menuName + "》", "修改", "", "", Request.Url.ToString());
                 ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改成功');layer_close_refresh();</script>");

             }
         }
        

    }

    /// <summary>
    /// 获取步骤和用料
    /// </summary>
    protected void GetItemInfo(string id)
    {
        string strSQL = "select * from T_Menu_Step where menuid='"+id+"' order by stepNo asc;";
        strSQL += "select b.*,i.itemName,i.unit from T_Menu_ItemBind b,T_Menu_Item i where b.itemid=i.itemid and menuid='" + id + "';";
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string html = HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["stepText"].ToString()).Replace("&rt", ">").Replace("&lt", "<");
                string imgList = "";
                if (html.Contains("_"))
                {
                    string[] imgLists = html.Split('_')[1].Split('^');
                    html = html.Split('_')[0];
                    for (int j = 0; j < imgLists.Length; j++)
                    {
                        imgList += "<img src='" + imgLists[j] + "'  height='50' style='margin:0px 1px;' />";
                    }
                }
                stepHtml += "<div class=\"stepDiv\"><div style=\"width:600px;\"><b><font class=\"stepBz\">步骤" + (i + 1) + "</font></b><a href='javascript:void(0)' style='float:right;' class='stepImg' menuId='"+menuId+"'>加入图片</a><span style='float:right;' class=''>&nbsp;</span><a href=\"javascript:void(0)\" style=\"float:right;\" class=\"stepDel\">删除</a></div><textarea class=\"stepText\" style=\"width:600px;height:50px\">" + html + "</textarea><br/><span class='stepImgSpan'>"+imgList+"</span></div>";
            }
        }
        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            DataRow[] drz = ds.Tables[1].Select("itemType='主料'");
            DataRow[] drf = ds.Tables[1].Select("itemType='辅料'");
            DataRow[] drt = ds.Tables[1].Select("itemType='调料'");
            
            for (int i = 0; i < drz.Count(); i++)
            {
                itemHtmlZ += "<span class=\"item\"><span goodsCode='" + drz[i]["goodsCode"] + "' index=\"" + drz[i]["itemId"] + "\" class=\"itemT\">" + drz[i]["itemName"] + "</span><span> 分量<input  index='" + drz[i]["itemId"] + "' class='itemV' type='text'  goodsCode='" + drz[i]["goodsCode"] + "'  value='" + drz[i]["context"] + "'/></span><span> 购买量<input  index='" + drz[i]["itemId"] + "' class='itemB' type='text' value='" + drz[i]["weight"] + "'/></span><span class=\"itemC\">×</span></span>";
            }
            for (int i = 0; i < drf.Count(); i++)
            {
                itemHtmlF += "<span class=\"item\"><span goodsCode='" + drf[i]["goodsCode"] + "' index=\"" + drf[i]["itemId"] + "\" class=\"itemT\">" + drf[i]["itemName"] + "</span><span> 分量<input  index='" + drf[i]["itemId"] + "' class='itemV' type='text'  goodsCode='" + drf[i]["goodsCode"] + "'  value='" + drf[i]["context"] + "'/></span><span> 购买量<input  index='" + drf[i]["itemId"] + "' class='itemB' type='text' value='" + drf[i]["weight"] + "'/></span><span class=\"itemC\">×</span></span>";
            }
            for (int i = 0; i < drt.Count(); i++)
            {
                itemHtmlT += "<span class=\"item\"><span goodsCode='" + drt[i]["goodsCode"] + "' index=\"" + drt[i]["itemId"] + "\" class=\"itemT\">" + drt[i]["itemName"] + "</span><span> 分量<input  index='" + drt[i]["itemId"] + "' class='itemV' type='text'  goodsCode='" + drt[i]["goodsCode"] + "'  value='" + drt[i]["context"] + "'/></span><span> 购买量<input  index='" + drt[i]["itemId"] + "' class='itemB' type='text' value='" + drt[i]["weight"] + "'/></span><span class=\"itemC\">×</span></span>";
            }
        }

    }

    /// <summary>
    /// 绑定步骤和用料
    /// </summary>
    /// <param name="id"></param>
    protected void BindItemInfo(string id)
    {
        string steps = hfStepList.Value;
        string items = hfItemList.Value;
        string strSQLStep="delete T_Menu_Step where menuid='"+id+"';";
        string strSQLItem = "delete T_Menu_ItemBind where menuid='"+id+"';";
        if (steps != "")
        {
            string[] stepList = steps.Split('|');
            for (int i = 0; i < stepList.Length; i++)
            {
                if (stepList[i] != "")
                {
                    string text = HttpUtility.HtmlEncode(stepList[i]);
                    strSQLStep += "insert T_Menu_Step values("+(i+1)+",'"+text+"','','"+id+"');";
                }
            }
        }
        if (items != "")
        {
            string[] itemListAll = items.Split('@');

            string[] itemList = itemListAll[0].Split(',');
            string[] itemList2 = itemListAll[1].Split(',');
            string[] itemList3 = itemListAll[2].Split(',');

            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i] != "")
                {
                    string text = HttpUtility.HtmlEncode(itemList[i]);
                    string itemId = text.Split('_')[0];
                    string context = text.Split('_')[1];
                    string weight = text.Split('_')[2];
                    string code = text.Split('_')[3];
                    strSQLItem += "insert T_Menu_ItemBind values('" + id + "','" + itemId + "'," + i + ",'主料','"+weight+"','"+context+"','"+code+"');";
                }
            }

            for (int i = 0; i < itemList2.Length; i++)
            {
                
                if (itemList2[i] != "")
                {
                    string text = HttpUtility.HtmlEncode(itemList2[i]);
                    string itemId = text.Split('_')[0];
                    string context = text.Split('_')[1];
                    string weight = text.Split('_')[2];
                    string code = text.Split('_')[3];
                    strSQLItem += "insert T_Menu_ItemBind values('" + id + "','" + itemId + "'," + i + ",'辅料','" + weight + "','" + context + "','"+code+"');";
                }
            }

            for (int i = 0; i < itemList3.Length; i++)
            {
                if (itemList3[i] != "")
                {
                    string text = HttpUtility.HtmlEncode(itemList3[i]);
                    string itemId = text.Split('_')[0];
                    string context = text.Split('_')[1];
                    string weight = text.Split('_')[2];
                    string code = text.Split('_')[3];
                    strSQLItem += "insert T_Menu_ItemBind values('" + id + "','" + itemId + "'," + i + ",'调料','" + weight + "','" + context + "','"+code+"');";
                }
            }
        }

        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        int row = adoHelper.ExecuteSqlNonQuery(strSQLStep + "" + strSQLItem);
        if (row > 0)
        {
            GetItemInfo(id);
        }
    }
    

    /// <summary>
    /// 取消保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }
}