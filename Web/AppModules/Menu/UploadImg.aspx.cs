using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StarTech.DBUtility;
using System.Data;
using StarTech;
using System.IO;

public partial class AppModules_Menu_UploadImg : StarTech.Adapter.StarTechPage
{
    protected int num = 1;
    protected string menuId="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["num"] != null)
            int.TryParse(Request.QueryString["num"], out num);

        if (!IsPostBack)
        {
            GetNowImg();
        }
    }

    /// <summary>
    /// 获取现在的图片
    /// </summary>
    protected void GetNowImg()
    {
        string menuId="";
        if (Request.QueryString["menuId"] != null)
            menuId = KillSqlIn.Form_ReplaceByString(Request.QueryString["menuId"], 10);
        else
            return;

        string strSQL = "select stepText from T_Menu_Step where menuId='"+menuId+"' and stepNo="+num+";";
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            return;
        string imgList = ds.Tables[0].Rows[0][0].ToString();
        if (imgList.Contains("_"))
        {
            imgList = imgList.Split('_')[1];
        }
        else
            return;
        for (int i = 0; i < imgList.Split('^').Length; i++)
        {
            string src=imgList.Split('^')[i];
            if(src=="")
                continue;
            if (i == 0)
            {
                Image1.ImageUrl = src;
            }
            if (i == 1)
            {
                Image2.ImageUrl = src;
            }
            if (i == 2)
            {
                Image3.ImageUrl = src;
            }
            if (i == 3)
            {
                Image4.ImageUrl = src;
            }
            if (i == 4)
            {
                Image5.ImageUrl = src;
            }
            if (i == 5)
            {
                Image6.ImageUrl = src;
            }
        }
    }


    protected void btUpload_Click(object sender, EventArgs e)
    {
        string iList = "";
        int index = 0;
        string url = "/upload/menu/step/" + DateTime.Now.Year + "/" + DateTime.Now.Month+"/";
        if (!Directory.Exists(Server.MapPath("~"+url)))
        {
            Directory.CreateDirectory(Server.MapPath("~"+url));
        }
        iList += GetImg(url, FileUpload1, Image1, ref index);
        iList += GetImg(url, FileUpload2, Image2, ref index);
        iList += GetImg(url, FileUpload3, Image3, ref index);
        iList += GetImg(url, FileUpload4, Image4, ref index);
        iList += GetImg(url, FileUpload5, Image5, ref index);
        iList += GetImg(url, FileUpload6, Image6, ref index);
        hfImgList.Value = iList;



    }


    string GetImg(string url,FileUpload fu, Image img, ref int index)
    {
        string iList = ""; ;
        if (fu.FileName != null && fu.FileName != "")
        {
            string name = Guid.NewGuid().ToString();
            string tzm = fu.FileName.Substring(fu.FileName.LastIndexOf(".")).ToLower();
            if (tzm != ".jpg" && tzm != ".png" && tzm != ".gif" && tzm != ".bmp")
            {
                return "";
            }
            if (index != 0)
            {
                iList += "|";
            }
            index++;
            iList += url + name+tzm;
            fu.SaveAs(Server.MapPath("~"+url + name + tzm));
            img.ImageUrl = url + name + tzm;
        }
        else
        {
            string src =img.ImageUrl.Substring(img.ImageUrl.LastIndexOf('/')+1);
            if (src != "" && src != "notupload.jpg")
            {
                if (index != 0)
                {
                    iList += "|";
                }
                index++;
                iList += img.ImageUrl;
            }
        }
        
        return iList;

    }

}