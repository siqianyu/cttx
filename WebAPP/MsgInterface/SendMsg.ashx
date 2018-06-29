<%@ WebHandler Language="C#" Class="SendMsg" %>

using System;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using StarTech.DBUtility;

public class SendMsg : IHttpHandler
{
    AdoHelper ado = AdoHelper.CreateHelper(StarTech.Util.AppConfig.DBInstance);
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string flag = context.Request["flag"] != null ? context.Request["flag"] : "";
        string tel = context.Request["tel"] != null ? context.Request["tel"] : "";
        
        if (flag == "reg" || flag == "findpwd")
        {
            context.Response.Write(ToSendMsg(tel, flag));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public string ToSendMsg(string tel, string msgFlag)
    {
        string yzm = new Random().Next(1000, 9999).ToString();
        string sysnumber = Guid.NewGuid().ToString();
        string sql = " INSERT INTO [T_ShortMessage_Log]([sysnumber],[tel],[yzm],[statu],[sendTime],[outSendTime],[remark])";
        sql += "VALUES('" + sysnumber + "','" + tel + "','" + yzm + "','sending',getdate(),DATEADD(n,5, getdate()),'" + msgFlag + "')";
        ado.ExecuteSqlNonQuery(sql);

        string msg = "";
        if (msgFlag == "reg") { msg = "【易开工】欢迎注册，当前操作的验证码为：" + yzm + "，请在五分钟内使用！"; }
        if (msgFlag == "findpwd") { msg = "【易开工】找回密码，当前操作的验证码为：" + yzm + "，请在五分钟内使用！"; }
        if (tel.Trim().Length == 11 && msg != "")
        {
            string r = Send(tel.Trim(), msg);
            if (r == "1")
            {
                ado.ExecuteSqlNonQuery("update T_ShortMessage_Log set statu='ok' where sysnumber='" + sysnumber + "'");
                return yzm;
            }
            else
            {
                ado.ExecuteSqlNonQuery("update T_ShortMessage_Log set statu='error',remark='" + r + "' where sysnumber='" + sysnumber + "'");
                return "0";
            }
        }
        return "0";
    }


    public string Send(string mobile, string msg)
    {
        // 设置为您的apikey(https://www.yunpian.com)可查
        string apikey = "c5fb96334fd28051f42624662ded4dd9";

        // 发送内容
        string text = msg;

        // 智能模板发送短信url
        string url_send_sms = "https://sms.yunpian.com/v1/sms/send.json";
        string data_send_sms = "apikey=" + apikey + "&mobile=" + mobile + "&text=" + text;

        string result = "";

        result += HttpPost(url_send_sms, data_send_sms);

        //{"code":0,"msg":"OK","result":{"count":1,"fee":1,"sid":9339732980}}

        if (result.IndexOf("OK") > -1)
        {
            return "1";
        }
        else
        {
            return "0|" + result;
        }
    }

    public string HttpPost(string Url, string postDataStr)
    {
        byte[] dataArray = Encoding.UTF8.GetBytes(postDataStr);
        // Console.Write(Encoding.UTF8.GetString(dataArray));

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
}