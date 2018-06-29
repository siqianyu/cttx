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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


public partial class SubSite_CreateImage : System.Web.UI.Page
{
    private const float PI = 3.14159265358979f;
    private const float PI2 = 6.28318530717959f;
    private const int VCODENUM = 4; //验证码位数
    private const int FONTSIZE = 14; //验证码字体大小
    //private const string VCHAR = "3,3,5,5,6,6,7,7,9,9,8,8,E,F,G,H,K,L,M,N,P,R,T,X,Y,Z"; //定义验证码字符及出现频次
    private const string VCHAR = "1,2,3,4,5,6,7,8,9"; 

    private void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); //不缓存
        float x, y, x1, y1;
        int PenWidth1, PenWidth2, VerifyVharFont;
        string VerifyChar = RndChar(); //RndChar是一个自定义函数
        System.Drawing.Bitmap Img;
        Graphics g;
        Brush backBrush = Brushes.DimGray;
        Brush textBrush = Brushes.Black;
        VerifyVharFont = FONTSIZE;  //验证码字符字体大小
        Font textFont = new Font("Arial", VerifyVharFont, FontStyle.Strikeout);  //验证码字体
        MemoryStream ms;
        int gWidth = Convert.ToInt32(VerifyChar.Length) * VerifyVharFont + VerifyVharFont;  //验证区宽度。如果字符都为W，不加宽是不行的
        Img = new System.Drawing.Bitmap(gWidth, 20); //验证区高度

        //生成随机背景颜色
        int nRed, nGreen, nBlue; // 背景的三元色
        Random rd = new Random();
        nRed = rd.Next(255) % 128 + 128;
        nGreen = rd.Next(255) % 128 + 128;
        nBlue = rd.Next(255) % 128 + 128;

        //在图片框picCanvas上面建立一个新的空白Graphics
        g = Graphics.FromImage(Img);

        //填充位图背景
        g.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(nRed, nGreen, nBlue)), 0, 0, Img.Width, Img.Height);

        //随机输出噪音线
        for (int i = 0; i < 1; i++)
        {
            Random rnd = new Random();
            x = Img.Width * (float)rnd.NextDouble();
            y = Img.Height * (float)rnd.NextDouble();
            x1 = Img.Width * (float)rnd.NextDouble();
            y1 = Img.Height * (float)rnd.NextDouble();
            PenWidth1 = Convert.ToInt32(2 * rnd.NextDouble()); //修改参数可获得不同的效果
            g.DrawLine(new Pen(backBrush, PenWidth1), x, y, x1, y1);
        }

        //随机输出噪点
        PenWidth2 = 2; //修改参数可获得不同的效果
        for (int i = 0; i < 1; i++)
        {
            Random rnd = new Random();
            x = Img.Width * (float)rnd.NextDouble();
            y = Img.Height * (float)rnd.NextDouble();
            nRed = rd.Next(255) % 128 + 128;
            nGreen = rd.Next(255) % 128 + 128;
            nBlue = rd.Next(255) % 128 + 128;
            g.DrawRectangle(new Pen(Color.FromArgb(nRed, nGreen, nBlue), PenWidth2), x, y, 1, 1);
        }

        //文字的位置
        Random rnd2 = new Random();
        x = 16 * (float)rnd2.NextDouble() - 6; //随机产生X轴位置，增加程序识别难度
        y = 0;

        //随机画3D背景
        Random rnd3d = new Random();
        double S3d = rnd3d.NextDouble();
        if (S3d > 0.9)
        {
            for (int i = 1; i > 0; i--)
            {
                g.DrawString(VerifyChar, textFont, backBrush, x - i, y + i);
            }
        }

        //将全局变换平移(x, y)，也就是使画布上将要画的所有内容向左边移动x，向下移动y
        g.TranslateTransform(1.5f, 1);

        //做切变，将原始矩形的下边缘水平移动矩形高度的0.2倍
        Matrix textTransform = g.Transform;
        textTransform.Shear(0.2f, 0);
        g.Transform = textTransform;

        //画出文字
        g.DrawString(VerifyChar, textFont, textBrush, x, y);
        this.Session["VerifyChar"] = VerifyChar; //将验证字符写入Session，供前台调用

        //扭曲验证字符。TwistImage参数可自行修改
        int Twist1, Twist2;
        if (S3d > 0.9)
        {
            Twist1 = 0;
            Twist2 = 0;
        }
        else
        {
            Random rnd4 = new Random();
            Twist1 = Convert.ToInt32(rnd4.NextDouble() * 1);  //扭曲参数随机生成
            Twist2 = Convert.ToInt32(rnd4.NextDouble() * 1); //扭曲参数随机生成
        }

        Img = TwistImage(Img, true, -Twist1, -Twist2);
        Img = TwistImage(Img, false, Twist1, Twist2); //多扭曲几次也没关系，只是消耗服务器资源多些
        ms = new MemoryStream();
        Img.Save(ms, ImageFormat.Png);
        Response.ClearContent();    //需要输出图象信息 要修改HTTP头 
        Response.ContentType = "image/Png";
        Response.BinaryWrite(ms.ToArray());
        g.Dispose();
        Img.Dispose();
        Response.End();
    }

    //函数名称:RndChar 
    //VCODENUM--设定返回随机字符串的位数 
    //函数功能:产生指定长度的由数字和字符组成的随机字符串 
    private string RndChar()
    {
        string[] VcArray = VCHAR.Split(','); //将字符串生成数组 
        int VCHARLen = VcArray.Length;
        string vvchar = "";
        Random rnd5 = new Random();
        for (int i = 0; i < VCODENUM; i++) //确保最少2个字符，最多VCODENUM+1个字符
        {
            vvchar += VcArray[rnd5.Next(0, VCHARLen)]; //数组一般从0开始读取，所以这里为25*Rnd 
        }
        return vvchar;
    }

    //函数名称:TwistImage 
    //函数参数: dMultValue－波形的幅度倍数；dPhase波形的起始相位，取值区间[0-2*PI)；bXDir－扭曲方式
    //函数功能:正弦曲线Wave扭曲图片。函数可以迭加使用，以获得不同方式不同程度的效果
    //这个天才的函数，已经无法考证出处了。感谢原作者！
    private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
    {
        Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
        double dBaseAxisLen = bXDir ? Convert.ToDouble(destBmp.Height) : Convert.ToDouble(destBmp.Width);
        for (int i = 0; i < destBmp.Width; i++)
        {
            for (int j = 0; j < destBmp.Height; j++)
            {
                double dx = 0;
                dx = bXDir ? PI2 * Convert.ToDouble(j) / dBaseAxisLen : PI2 * Convert.ToDouble(i) / dBaseAxisLen;
                dx += dPhase;
                double dy = Math.Sin(dx);

                //取得当前点的颜色
                int nOldX = 0;
                int nOldY = 0;
                nOldX = bXDir ? i + Convert.ToInt32(dy * dMultValue) : i;
                nOldY = bXDir ? j : j + Convert.ToInt32(dy * dMultValue);
                System.Drawing.Color color = srcBmp.GetPixel(i, j);
                if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                {
                    destBmp.SetPixel(nOldX, nOldY, color);
                }
            }
        }
        return destBmp;
    }
    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion
}
