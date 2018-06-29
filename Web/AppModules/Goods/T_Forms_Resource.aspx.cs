using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using StarTech.ELife.Goods;
using StarTech.DBUtility;
using StarTech;

public partial class AppModules_Goods_T_Forms_Resource : StarTech.Adapter.StarTechPage
{
    public string goodsId;
    public static int imgNum;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Ajax方法
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AppModules_Goods_T_Forms_Resource));
        if (Request.QueryString["del"]!=null)
        {
            string strSQL = "delete T_Goods_Pic where sysnumber='"+KillSqlIn.Form_ReplaceByString(Request.QueryString["del"],100)+"';";
            AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
            int row = adohelper.ExecuteSqlNonQuery(strSQL);

            Response.Clear();

            if (row > 0)
                Response.Write("success");
            else
                Response.Write("fail");
            Response.End();
        }
        this.goodsId = (Request["goodsId"] == null) ? "" : StarTech.KillSqlIn.Url_ReplaceByString(Request["goodsId"], 50);
        
        if (!IsPostBack)
        {
            InitForm();
            BindVideoAndPic();
        }
    }

    [AjaxPro.AjaxMethod]
    public int Ajax_Delete(string id)
    {
        //return new TableObject("T_Goods_Pic").Util_DeleteBat("Sysnumber='" + id + "'");
        return 1;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        //ModGoodsPic modPic = new ModGoodsPic();
        //modPic.GoodsId = goodsId;

        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);

        ArrayList list = batUpload();
        if (list == null || list.Count == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请选择图片');</script>");
            return;
        }
        int i = 1;
        string strSQL = "";
        foreach (object obj in list)
        {
            if (imgNum + i > 6)
            {
                if (i == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('最多可上传6张');toClose();</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('已上传"+(i-1)+"张，最多可上传6张');toClose();</script>");
                }
                break;
            }
            //modPic.Sysnumber = Guid.NewGuid().ToString();
            string sysnumber = Guid.NewGuid().ToString();
            int orderby = 1;
            string picPath = "";
            try
            {
                Control control = Page.FindControl("txtOrder" + i);
                orderby = Int32.Parse(((HtmlInputText)(control)).Value.Trim());
            }
            catch 
            { 
                orderby = 0; 
            }
            i++;
            
            string[] arr = obj.ToString().Split('|');
            picPath = arr[2];
            strSQL += "insert into T_Goods_Pic([Sysnumber],[GoodsId],[PicPath],[IsDefault],[Orderby]) values('"+sysnumber+"','"+goodsId+"','"+picPath+"',0,"+orderby+");";
            //modPic.PicPath = arr[2];
            //new BllGoodsPic().Add(modPic);
        }
        int row = adohelper.ExecuteSqlNonQuery(strSQL);
        if (row > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('上传成功');toClose();</script>");
            BindVideoAndPic();
        }
    }

    protected void InitForm()
    {

    }

    public ArrayList batUpload()
    {

        //string shopName = "";
        //string strSQL = "select shopName from T_Shop_User where shopid=(select ProviderInfo from T_Goods_info where goodsid='" + goodsId + "' and Datafrom='ShopSeller');select memberName from T_member_info where memberId=(select ProviderInfo from T_Goods_info where goodsid='" + goodsId + "' and Datafrom='PersonSeller');";
        
        //DataSet ds = adohelper.ExecuteSqlDataset(strSQL);
        //if (ds == null || ds.Tables.Count < 2)
        //    return null;

        //if (ds.Tables[0].Rows.Count > 0)
        //    shopName = ds.Tables[0].Rows[0][0].ToString();
        //else
        //    shopName = ds.Tables[1].Rows[0][0].ToString();

        ArrayList list = new ArrayList();
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        //搜集表单中的file元素
        HttpFileCollection files = Request.Files;

        //遍历file元素
        for (int i = 0; i < files.Count; i++)
        {
            HttpPostedFile postedFile = files[i];
            HtmlInputCheckBox []ck={Checkbox1,Checkbox2,Checkbox3,Checkbox4};
            if (postedFile.FileName != "")
            {
                //文件大小
                int fileSize = postedFile.ContentLength / 1024;
                if (fileSize == 0) { fileSize = 1; }

                //提取文件名
                string oldFileName = Path.GetFileName(postedFile.FileName);

                //提取文件扩展名
                string oldFileExt = Path.GetExtension(oldFileName);

                //重命名文件
                string newFileName = Guid.NewGuid().ToString()+oldFileExt;

                //设置保存目录
                string webDirectory = "/upload/goodsadmin/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string saveDirectory = Server.MapPath(webDirectory);
                if (!Directory.Exists(saveDirectory)) { Directory.CreateDirectory(saveDirectory); }

                //设置保存路径
                string savePath = saveDirectory + newFileName;
                //保存
                postedFile.SaveAs(savePath);

                string savePath2 = savePath;
                string newFileName2 = newFileName;
                if (ck[i].Checked)
                {
                    //System.Drawing.Image nowImg = System.Drawing.Image.FromFile(savePath);
                    System.Drawing.Bitmap nowImg2 = new System.Drawing.Bitmap(savePath);
                    System.Drawing.Bitmap nowImg = new System.Drawing.Bitmap(nowImg2.Width,nowImg2.Height);
                    
                    float x = nowImg.Width-50;
                    float y = nowImg.Height-30;

                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(nowImg);
                    g.DrawImage(nowImg2, 0, 0);
                    System.Drawing.Font f = new System.Drawing.Font("华文彩云", 12);
                    System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    g.DrawString("才通天下微信公号", f, b, x, y);
                    f = new System.Drawing.Font("华文琥珀", 12);
                    b = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                    g.DrawString("才通天下微信公号·", f, b, x, y);
                    f.Dispose();
                    b.Dispose();
                    g.Dispose();
                    nowImg2.Dispose();
                    //savePath2 = webDirectory + newFileName.Replace(oldFileExt, "" + oldFileExt);
                    string nGuid = Guid.NewGuid().ToString();
                    nGuid= nGuid.Replace(nGuid[new Random().Next(1,9)],nGuid[new Random().Next(10,15)]);
                    newFileName2 = nGuid + oldFileExt;
                    savePath2 = webDirectory + newFileName2;
                    MemoryStream ms = new MemoryStream(); 
                    nowImg.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imgData = ms.ToArray();
                    File.Delete(savePath);
                    FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite);
                    if (fs != null)
                    {
                        fs.Write(imgData, 0, imgData.Length);
                        fs.Close();
                    }
                    nowImg.Dispose();
                }



                //缩略图
                MakeSmallPic(Server.MapPath(webDirectory + newFileName), Server.MapPath(webDirectory + newFileName.Replace(oldFileExt, ".jpg" )));
                string goodsSmallPic =webDirectory+newFileName.Replace(oldFileExt, "" + oldFileExt);

                list.Add(oldFileName + "|" + fileSize + "|" + goodsSmallPic);
            }
        }
        return list;
    }

    protected void BindVideoAndPic()
    {
        //DataTable dt = new TableObject("T_Goods_Pic").Util_GetList("*", "goodsId=" + this.goodsId + " ", "orderBy asc");
        AdoHelper adohelper = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
        DataSet ds = adohelper.ExecuteSqlDataset("select * from T_Goods_Pic where goodsid='"+goodsId+"' order by orderby asc;");
        this.Repeater2.DataSource = ds;
        this.Repeater2.DataBind();
        imgNum = ds.Tables[0].Rows.Count;
    }

    protected void MakeSmallPic(string oldPicPath, string smallPicPath)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            WLWang.MakeThumbnail.MakeToThumbnail(oldPicPath, smallPicPath, 400, 300, "Cut");
            // 生成缩略图方法 
            //生成的缩略图稍微比前台显示的图片大点，这样显示的清晰度高点（如前台显示100*150，则生成200*300）
        }
        catch {  }
    }
}
