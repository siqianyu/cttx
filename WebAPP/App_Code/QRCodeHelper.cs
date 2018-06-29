using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using System.IO;

/// <summary>
///二维码生成辅助类
/// </summary>
public class QRCodeHelper
{

    /// <summary>
    /// 生成二维码图片
    /// </summary>
    /// <param name="URL"></param>
    /// <param name="WebPath"></param>
    /// <returns></returns>
    public static string CreateQRCodeImg(string URL, string WebPath)
    {
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
        qrCodeEncoder.QRCodeScale = 40;//大小(值越大生成的二维码图片像素越高)
        qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)

        //System.Drawing.Image image = qrCodeEncoder.Encode("4408810820 深圳－广州 小江");
        System.Drawing.Image image = qrCodeEncoder.Encode(URL);
        string filename = Guid.NewGuid().ToString() + ".png";

        //物理路径
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(WebPath)))//判断文件夹路径是否存在
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(WebPath));
        }
        string filepath = HttpContext.Current.Server.MapPath(WebPath + "\\" + filename);
        System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
        image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
        fs.Close();
        image.Dispose();

        return WebPath + "/" + filename;
    }

}