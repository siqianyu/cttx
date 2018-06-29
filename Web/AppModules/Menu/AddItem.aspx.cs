using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using Startech.Utils;
using System.IO;

public partial class AppModules_Menu_AddItem : StarTech.Adapter.StarTechPage
{
    protected  string itemid = "";
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
    protected bool ifShowImg = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["itemId"] != null)
        {
            itemid = StarTech.KillSqlIn.Form_ReplaceByString(Request.QueryString["itemId"], 10);
            if (!IsPostBack)
            {

                GetItem();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void GetItem()
    {
        string strSQL = "select * from T_Menu_Item where itemid='"+itemid+"';";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtItemName.Text = ds.Tables[0].Rows[0]["itemname"].ToString();
            txtOrderby.Text = ds.Tables[0].Rows[0]["orderby"].ToString();
            txtRemark.Text = ds.Tables[0].Rows[0]["remark"].ToString();
            txtUnit.Text = ds.Tables[0].Rows[0]["unit"].ToString();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ifbuy"]) == 1)
            {
                rbChoose.Checked = true;
                hfGoodsId.Value = ds.Tables[0].Rows[0]["goodsid"].ToString();
            }
            //ddlItemType.SelectedValue = ds.Tables[0].Rows[0]["itemType"].ToString();
            imgItem.ImageUrl = ds.Tables[0].Rows[0]["itemImgSrc"].ToString();
            ifShowImg = true;
            hfImgSrc.Value = ds.Tables[0].Rows[0]["itemImgSrc"].ToString();

        }

    }


    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        string itemName = KillSqlIn.Form_ReplaceByString(txtItemName.Text,20);
        //string itemType = KillSqlIn.Form_ReplaceByString(ddlItemType.SelectedValue,10);
        string itemType = "";
        string remark = KillSqlIn.Form_ReplaceByString(txtRemark.Text,500);
        string goodsid = "";
        int ifbuy = 0;
        int orderby = 99;
        string imgsrc = "";
        string unit = KillSqlIn.Form_ReplaceByString(txtUnit.Text, 20);
         if (!rbWrite.Checked && hfGoodsId.Value != "")
        {
            ifbuy = 1;
            goodsid = KillSqlIn.Form_ReplaceByString(hfGoodsId.Value, 20);
            //imgsrc = imgItem.ImageUrl;
            imgsrc = hfImgSrc.Value;

        }
        else
        {
            ifbuy = 0;
            if (fuImg.FileName != null && fuImg.FileName != "")
            {
                if (fuImg.FileName.ToLower().Contains(".jpg") || fuImg.FileName.ToLower().Contains(".png") || fuImg.FileName.ToLower().Contains(".bmp") || fuImg.FileName.ToLower().Contains(".gif"))
                {
                    string filePath = "/Upload/MenuItem/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    if (!Directory.Exists(Server.MapPath(filePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(filePath));
                    }
                    string fileName = DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + new Random().Next(10, 99);
                    string tzm = fuImg.FileName.Substring(fuImg.FileName.LastIndexOf("."));
                    fuImg.SaveAs(Server.MapPath(filePath + fileName + tzm));
                    imgsrc = filePath + fileName + tzm;
                }
                else
                {

                }
            }
            else
            {
                //imgsrc = imgItem.ImageUrl;
                imgsrc = hfImgSrc.Value;
            }
        }
        if (!int.TryParse(txtOrderby.Text, out orderby))
        {
            orderby = 99;
        }
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('"+itemid+"')</script>");


        if (itemid==null || itemid == "")
        {
            string strSQL = "insert T_Menu_Item values('" + itemName + "','" + imgsrc + "','" + itemType + "','" + ifbuy + "','" + goodsid + "'," + orderby + ",'" + remark + "','"+unit+"'); ";
            int row = adoHelper.ExecuteSqlNonQuery(strSQL);
            if (row > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('添加成功');layer_close_refresh()</script>");
            }
        }
        else
        {
            string strSQL = "update T_Menu_Item set itemName='" + itemName + "',itemImgSrc='" + imgsrc + "',itemType='" + itemType + "',ifBuy='" + ifbuy + "',GoodsId='" + goodsid + "',orderBy=" + orderby + ",remark='" + remark + "',unit='"+unit+"' where itemId='"+itemid+"'; ";
            int row = adoHelper.ExecuteSqlNonQuery(strSQL);
            if (row > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('修改成功');layer_close_refresh()</script>");
            }
        }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }
}