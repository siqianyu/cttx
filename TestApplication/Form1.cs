using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void TimeElapse(object source, System.Timers.ElapsedEventArgs e)
        {
            string html = HttpPost(this.textBox1.Text, "r=1");
            FileStream fs = new FileStream(@"d:/Logs/timetick_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine("" + DateTime.Now.ToString() + "------------------->" + html);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
        }

        private string HttpPost(string Url, string postDataStr)
        {
            byte[] dataArray = Encoding.UTF8.GetBytes(postDataStr);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataArray.Length;
            //request.CookieContainer = cookie;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                String res = reader.ReadToEnd();
                reader.Close();
                return res;
            }
            catch (Exception e)
            {
                return e.Message + e.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为10000毫秒； 
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapse);//到达时间的时候执行事件； 
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            this.button1.Enabled = false;
        }

    }
}
