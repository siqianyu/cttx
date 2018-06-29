using StarTech.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using System.Data;

/// <summary>
/// Common 的摘要说明
/// </summary>
public static class Common
{
    private static AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public static bool CheckUser(string cookieInfo)
    {
        string[] info = cookieInfo.Split(new string[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
        if (info.Length == 2)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[] {
                    new SqlParameter("@memberid",info[0]),
                    new SqlParameter("@password",info[1])
                };

                return ado.ExecuteSqlDataset("select * from t_member_info where memberid=@memberid and password=@password", p).Tables[0].Rows.Count > 0;
            }
            catch
            { return false; }
        }
        else
        {
            return false;
        }
    }

    public static string SaveFile(string table, string key, string value, string prop, HttpPostedFile uploadFile)
    {
        try
        {
            if (uploadFile.FileName != ".jpeg" && uploadFile.FileName != ".jpg" && uploadFile.FileName != ".png" && uploadFile.FileName != ".bmp" && uploadFile.FileName != ".gif")
            {
                return "图片格式不正确";
            }
            //if (uploadFile.ContentLength > 1024000)
            //{
            //    return "图片不能大于1M";
            //}

            //保存文件
            string path = "/upload/";
            switch (table.ToLower())
            {
                case "t_member_pic":
                    path += "seller";
                    break;
                case "t_member_info":
                    path += "headimg";
                    break;
                case "t_goods_pic":
                    path += "goods";
                    break;
                default:
                    path += "default";
                    break;
            }
            path += "/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMdd") + "/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            path += DateTime.Now.ToString("HHmmssffff") + uploadFile.FileName;
            uploadFile.SaveAs(HttpContext.Current.Server.MapPath(path));

            //修改数据库
            string strSql = string.Format("update {0} set {1}='{2}' where {3}='{4}'", table, prop, path, key, value);
            return ado.ExecuteSqlNonQuery(strSql).ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    //获取任务类别
    public static string GetJobTypeByGoodsId(string goodsId)
    {
        SqlParameter[] p = new SqlParameter[] { 
            new SqlParameter("@goodsId",goodsId)
        };
        return Common.NullToEmpty(ado.ExecuteSqlScalar("select top 1 JobType from T_Goods_Info where goodsId=@goodsId", p));
    }

    //获取任务类别
    public static string GetJobTypeByOrderId(string orderId)
    {
        SqlParameter[] p = new SqlParameter[] { 
            new SqlParameter("@orderId",orderId)
        };
        return Common.NullToEmpty(ado.ExecuteSqlScalar("select top 1 JobType from T_Goods_Info a left join T_Order_InfoDetail b on a.GoodsId=b.GoodsId where b.OrderId=@orderId", p));
    }

    //消息日志
    public static string AddLogMsg(string memberId, string title, string remark,string url,int isRead)
    {
        string sysnumber = Guid.NewGuid().ToString();
        string sql = " INSERT INTO [T_Log_Message]([sysnumber],[MemberId],[type],[createTime],[title],[remark],[url],[isread])";
        sql += "VALUES('" + sysnumber + "','" + memberId + "','log',getdate(),'" + title + "','" + remark + "','"+url+"','"+isRead.ToString()+"')";
        //短信提醒
        DataTable dt = ado.ExecuteSqlDataset("select tel,mobile from t_member_info where memberid='"+memberId+"'").Tables[0];
        string tel="";
        if (dt.Rows.Count > 0)
        {
            tel = NullToEmpty(dt.Rows[0][0]);
            if (string.IsNullOrEmpty(tel))
            {
                tel = NullToEmpty(dt.Rows[0][1]);
            }
        }
        try
        {
            ToSendMsg(tel, "logmsg", memberId, "【才通天下微信公号】" + remark);
        }
        catch { }

        ado.ExecuteSqlNonQuery(sql).ToString();
        return sysnumber;
    }

    private static string ToSendMsg(string tel, string msgFlag,string memberId,string msg)
    {
        string sysnumber = Guid.NewGuid().ToString();
        string sql = " INSERT INTO [T_ShortMessage_Log]([sysnumber],[tel],[yzm],[statu],[sendTime],[outSendTime],[remark])";
        sql += "VALUES('" + sysnumber + "','" + tel + "','m" + memberId + "','sending',getdate(),DATEADD(n,5, getdate()),'" + msgFlag + "')";
        ado.ExecuteSqlNonQuery(sql);

        if (tel.Trim().Length == 11 && msg != "")
        {
            string r = Send(tel.Trim(), msg);
            if (r == "1")
            {
                ado.ExecuteSqlNonQuery("update T_ShortMessage_Log set statu='ok' where sysnumber='" + sysnumber + "'");
                return "1";
            }
            else
            {
                ado.ExecuteSqlNonQuery("update T_ShortMessage_Log set statu='error',remark='" + r + "' where sysnumber='" + sysnumber + "'");
                return "0";
            }
        }
        return "0";
    }

    private static string Send(string mobile, string msg)
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

    private static string HttpPost(string Url, string postDataStr)
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

    public static string GetBossIdByGoodsId(string goodsId)
    {
        string strSql = "select top 1 ProviderInfo from T_Goods_Info where GoodsId=@goodsId";
        SqlParameter[] p1 = { new SqlParameter("@goodsId", goodsId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetBossNameByGoodsId(string goodsId)
    {
        string strSql = @"select top 1 TrueName from T_Goods_Info a
            left join T_Member_Info b on a.ProviderInfo=b.MemberId
            where GoodsId=@goodsId";
        SqlParameter[] p1 = { new SqlParameter("@goodsId", goodsId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetBossIdByPriceId(string priceId)
    {
        string strSql = @"select top 1 ProviderInfo from T_Goods_MemberPrice a
            left join T_Goods_Info b on a.GoodsId=b.GoodsId
            where priceId=@priceId";
        SqlParameter[] p1 = { new SqlParameter("@priceId", priceId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetBossNameByPriceId(string priceId)
    {
        string strSql = @"select top 1 TrueName from T_Goods_MemberPrice a
            left join T_Goods_Info b on a.GoodsId=b.GoodsId 
            left join T_Member_Info c on b.ProviderInfo=c.MemberId
            where priceId=@priceId";
        SqlParameter[] p1 = { new SqlParameter("@priceId", priceId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetBossIdByOrderId(string orderId)
    {
        string strSql = @"select top 1 SellerId from T_Order_Info 
            where orderId=@orderId";
        SqlParameter[] p1 = { new SqlParameter("@orderId", orderId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetBossNameByOrderId(string orderId)
    {
        string strSql = @"select top 1 TrueName from T_Order_Info a
            left join T_Member_Info b on a.SellerId=b.MemberId
            where orderId=@orderId";
        SqlParameter[] p1 = { new SqlParameter("@orderId", orderId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetWorkerIdByPriceId(string priceId)
    {
        string strSql = @"select top 1 MemberId from T_Goods_MemberPrice where priceId=@priceId";
        SqlParameter[] p1 = { new SqlParameter("@priceId", priceId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetWorkerNameByPriceId(string priceId)
    {
        string strSql = @"select top 1 TrueName from T_Goods_MemberPrice a
            left join T_Member_Info b on a.MemberId=b.MemberId
            where priceId=@priceId";
        SqlParameter[] p1 = { new SqlParameter("@priceId", priceId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetWorkerIdByOrderId(string orderId)
    {
        string strSql = @"select top 1 MemberId from T_Order_Info 
            where orderId=@orderId";
        SqlParameter[] p1 = { new SqlParameter("@orderId", orderId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetWorkerNameByOrderId(string orderId)
    {
        string strSql = @"select top 1 TrueName from T_Order_Info a
            left join T_Member_Info b on a.MemberId=b.MemberId
            where orderId=@orderId";
        SqlParameter[] p1 = { new SqlParameter("@orderId", orderId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetGoodsNameByGoodsId(string goodsId)
    {
        string strSql = @"select top 1 GoodsName from T_Goods_Info 
            where goodsId=@goodsId";
        SqlParameter[] p1 = { new SqlParameter("@goodsId", goodsId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetGoodsNameByPriceId(string priceId)
    {
        string strSql = @"select top 1 GoodsName from T_Goods_Info a
            left join T_Goods_MemberPrice b on a.goodsId=b.goodsId
            where priceId=@priceId";
        SqlParameter[] p1 = { new SqlParameter("@priceId", priceId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string GetGoodsNameByOrderId(string orderId)
    {
        string strSql = @"select top 1 GoodsName from T_Goods_Info a
            left join T_Goods_MemberPrice b on a.goodsId=b.goodsId
            left join T_Order_Info c on b.PriceId=c.PriceId
            where orderId=@orderId";
        SqlParameter[] p1 = { new SqlParameter("@orderId", orderId) };
        return ado.ExecuteSqlScalar(strSql, p1).ToString();
    }

    public static string NullToEmpty(object o)
    {
        try { return o.ToString(); }
        catch { return string.Empty; }
    }

    public static string NullToEmpty(object o, string str)
    {
        try { return o.ToString(); }
        catch { return str; }
    }

    public static int NullToZero(object o)
    {
        try { return Convert.ToInt32(o); }
        catch { return 0; }
    }

    public static int NullToZero(object o, int num)
    {
        try { return Convert.ToInt32(o); }
        catch { return num; }
    }
}