using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

/// <summary>
///Create2dbCode 的摘要说明
/// </summary>
public class QRCode
{
    public string context="";
    public QRCodeEncoder.ENCODE_MODE mode = QRCodeEncoder.ENCODE_MODE.BYTE;
    QRCodeEncoder.ERROR_CORRECTION level = QRCodeEncoder.ERROR_CORRECTION.L;
    int size = 4;
    int version =10;

    public QRCode()
    {

    }

    public QRCode(string _context)
	{
        context = _context;
    }

    

    /// <summary>
    /// 创建二维码
    /// </summary>
    /// <returns></returns>
    public string CreateQRCode()
    {
        QRCodeEncoder enc = new QRCodeEncoder();
        string fileName = Guid.NewGuid().ToString()+".jpg";
        string filePath = "/upload/qrcode/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
        Image img;
        enc.QRCodeEncodeMode = mode;
        enc.QRCodeErrorCorrect = level;
        enc.QRCodeScale = size;
        enc.QRCodeVersion = version;
        
        img = enc.Encode(context);

        if(!Directory.Exists(HttpContext.Current.Server.MapPath(filePath)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath));
        }
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(filePath+fileName), FileMode.OpenOrCreate,FileAccess.Write);
        img.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
        fs.Close();
        img.Dispose();
        return filePath+fileName;
    }

    /// <summary>
    /// 创建二维码
    /// </summary>
    /// <param name="_context">要生成的文字</param>
    /// <returns></returns>
    public string CreateQRCode(string _context)
    {
        context = _context;
        return CreateQRCode();
    }
     
}