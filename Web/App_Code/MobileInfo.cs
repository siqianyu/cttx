using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net;
using System.IO;
using StarTech.DBUtility;
using System.Text;

/// <summary>
///MobileInfo 的摘要说明
/// </summary>
public class MobileInfo
{
    AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");

	public MobileInfo()
	{
    }

    /// <summary>
    /// 获取短信验证码
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public string GetMess(string mobile,string info)
    {
        //return "{\"result\":\"canUse\"}";
        mobile=StarTech.KillSqlIn.Form_ReplaceByString(mobile,20);
        string strSQL = "select outSendTime from T_ShortMessage_Log where tel='"+mobile+"' order by sendtime desc;";
        DataSet ds = adoHelper.ExecuteSqlDataset(strSQL);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
          DateTime outTime=Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
          if (outTime >= DateTime.Now)
              return "{\"result\":\"wait\"}";
        }
        if (ds == null)
        {
            return "{\"result\":\"dataError\"}";
        }
        string json = "";
        //string yzm = new Random().Next(0, 9999).ToString().PadLeft(4,'0');
        //string message = "才通天下微信公号验证码为"+yzm+"，请不要将此短信泄露给其他人";
        //string message = yzm+"（才通天下微信公号验证码），感谢您使用才通天下微信公号平台，咨询电话0571-88930567【才通天下微信公号】";
        string message = info + "【才通天下微信公号】";
        //string userName = "eshenghuo";
        //string pwd = "Hzyk20150428hzsd";
        string userName = "api";
        string pwd = "key-c984e096bc3d8480df72e20a5216a7e4";
        string url = "https://sms-api.luosimao.com/v1/send.json";
        
        byte[] byteArray = Encoding.UTF8.GetBytes("mobile=" + mobile + "&message=" + message);
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
        
        string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(userName + ":" + pwd));
        
        webRequest.Headers.Add("Authorization", auth);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.ContentLength = byteArray.Length;

        Stream newStream = webRequest.GetRequestStream();
        newStream.Write(byteArray, 0, byteArray.Length);
        newStream.Close();

        HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
        StreamReader php = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string Message = php.ReadToEnd();
        string statu = "fail";
        if (Message.Contains("error\":0"))
            statu = "success";

        return statu;
        //string gid = Guid.NewGuid().ToString();
        //strSQL = "insert T_ShortMessage_Log values('"+gid+"','"+mobile+"','"+yzm+"','"+statu+"','"+DateTime.Now+"','"+DateTime.Now.AddSeconds(30)+"','');";
        //int rows = adoHelper.ExecuteSqlNonQuery(strSQL);
        //json = "{\"result\":\""+statu+"\",\"yzm\":\""+yzm+"\",\"message\":["+Message+"],\"sendTime\":\""+DateTime.Now+"\"}";
        
        //return json;
    

	}
}