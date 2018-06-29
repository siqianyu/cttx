using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using StarTech.DBUtility;

/*
 微信接口：
 * 1、原理就是监听微信发来的数据：接收到微信post上来数据（xml格式），然后处理后，再返回给微信
 */


public partial class NGWeiXinRoot_MainInterface : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 请求方式大写
        string httpMethod = Request.HttpMethod.ToUpper();
        if (httpMethod == "GET")
        {
            string signature = Request["signature"] == null ? "" : Request["signature"];
            string timestamp = Request["timestamp"] == null ? "" : Request["timestamp"];
            string nonce = Request["nonce"] == null ? "" : Request["nonce"];
            string echoStr = Request["echoStr"] == null ? "" : Request["echoStr"];
            //log
            string log = "【" + httpMethod + "】" + DateTime.Now.ToString() + "|signature(" + signature + ")|timestamp(" + timestamp + ")|nonce(" + nonce + ")|echoStr(" + echoStr + ")";
           // WriteLog(log);
            WXPushLog("收到消息0GET", Request.Url.Query);
            Response.Write(echoStr);
            Response.End();
        }
        else if (httpMethod == "POST")
        {
            if (Request.InputStream != null)
            {
                System.IO.Stream sm = Request.InputStream;//获取post正文
                int len = (int)sm.Length;//post数据长度
                byte[] inputByts = new byte[len];//字节数据,用于存储post数据
                sm.Read(inputByts, 0, len);//将post数据写入byte数组中
                sm.Close();//关闭IO流
                string data = Encoding.GetEncoding("utf-8").GetString(inputByts);//转为unicode编码
                data = Server.UrlDecode(data);//下面解释一下Server.UrlDecode和Server.UrlEncode的作用

                //WriteLog("【" + DateTime.Now + "收到消息】" + data);
                WXPushLog("收到消息0", data);
                SendMsgToUser(data);
            }
        }


    }


    /*
     用户发的图片消息
     * 
     <xml><ToUserName><![CDATA[gh_384b77007ac6]]></ToUserName>
        <FromUserName><![CDATA[oKiw2t92-9nwoTj4QXgMiAb1fsnQ]]></FromUserName>
        <CreateTime>1481527997</CreateTime>
        <MsgType><![CDATA[image]]></MsgType>
        <PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz_jpg/yWrFx0bFsMNVQmGsld4lH4HVZRrmInIiaWvdiaEkFwNJ7aXicoW2PjWamUp2UJYZyIibeibgFShKZrIfT23hCUGDMiaA/0]]></PicUrl>
        <MsgId>6363114295708982107</MsgId>
        <MediaId><![CDATA[Jrm1xwqmtl3XnmAWTj7QPwdvXTbYXHV3_OtXKIW3YSaZZo881SHcmmLkkxv4Q1F3]]></MediaId>
     </xml>
     */


    /// <summary>
    /// 微信自动回复接口
    /// </summary>
    /// <returns></returns>
    public void SendMsgToUser(string userMsg)
    {
        #region 首先读取用户发送过来的信息
        byte[] array = Encoding.UTF8.GetBytes(userMsg);
        MemoryStream stream = new MemoryStream(array);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(stream);
        XmlNodeList xmlList = xmlDoc.ChildNodes;


        string ToUserName = "";
        string FromUserName = "";
        string MsgType = "";
        string MediaId = "";
        string Content = "";

        ToUserName = xmlDoc.SelectNodes("/xml/ToUserName")[0].InnerText;
        FromUserName = xmlDoc.SelectNodes("/xml/FromUserName")[0].InnerText;
        MsgType = xmlDoc.SelectNodes("/xml/MsgType")[0].InnerText;
        
        Content = "";

        //保存图片到本地服务器
        if (MsgType == "image")
        {
            MediaId = xmlDoc.SelectNodes("/xml/MediaId")[0].InnerText;
            string atoken = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
            string path = HttpContext.Current.Server.MapPath("Source/" + MediaId + ".jpg");
            NG.WeiXin.NGWeiXinPubTools.DownloadPic(atoken, MediaId, path);
        }
        #endregion

        #region 我的推广记录
        if (MsgType == "event")
        {
            BindUser(MsgType, userMsg);
        }
        #endregion

        #region 然后回复用户发来的信息


        string msg = "<xml>";
        msg += "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>";
        msg += "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";
        msg += "<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>";

        string welcomeMsg = "“一起学会计吧”服务指南\n【资源】汇集会计、税务、管理等方面的实用资源，你工作、学习的轻松帮手，只需搜索关键字即可到你要的信息。\n【课程】知识更新、职称考试、技能提升，一起学会计吧将助你轻松愉快学习！我们的初级职称考试课程已经上线，注册即可免费开放20套全真模拟试题。\n【我的】欢迎分享给你身边一起学习的朋友，你将会得到意想不到的好礼哦。\n生意人的账簿，记录收入与支出，两数相减，便是盈利。人生的账簿，记录爱与被爱，两数相加，就是成就。我们一起学会计吧！";
        if (MsgType == "text")
        {
            msg += "<MsgType><![CDATA[text]]></MsgType>";
            msg += "<Content><![CDATA[" + welcomeMsg + "]]></Content>";
        } 
        else if (MsgType == "image")
        {
            msg += "<MsgType><![CDATA[text]]></MsgType>";
            msg += "<Content><![CDATA[" + welcomeMsg + "]]></Content>";
        }
        else
        {
            msg += "<MsgType><![CDATA[text]]></MsgType>";
            msg += "<Content><![CDATA[" + welcomeMsg + "]]></Content>";
        
            /*
            msg += "<MsgType><![CDATA[news]]></MsgType>";
            msg += "<ArticleCount>3</ArticleCount>";
            msg += "<Articles>";
            msg += "<item>";
            msg += "<Title><![CDATA[一起学会计吧！]]></Title> ";
            msg += "<Description><![CDATA[一起学会计吧！]]></Description>";
            msg += "<PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz_jpg/iaNBEqjMsH1Sp6853661FKSH1yFIzjzYdZgaO8PThdwr4xGib4bmEAM1QxJUHFY76icmqic1gUOHGzRf2yHqtMdDjg/0]]></PicUrl>";
            msg += "<Url><![CDATA[https://mp.weixin.qq.com/mp/homepage?__biz=MzUxNTYzNzAxMQ==&hid=1&sn=da30d1036978fd2fabea459e771a91fc&scene=18&uin=&key=&devicetype=Windows+7&version=6206014b&lang=zh_CN&ascene=7&winzoom=1]]></Url>";
            msg += "</item>";
            msg += "<item>";
            msg += "<Title><![CDATA[2018《初级会计实务》必备知识点汇总（持续更新）]]></Title> ";
            msg += "<Description><![CDATA[2018《初级会计实务》必备知识点汇总（持续更新）]]></Description>";
            msg += "<PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz_jpg/iaNBEqjMsH1TPEnDEPvPtxXEYiaIialFegyVwuxbh2P5Lra9ibpO2nmSmuZ4eHwgkxcW7au6msv03Q6m7RFiaDBiaGwg/640?wx_fmt=jpeg&wxfrom=5&wx_lazy=1]]></PicUrl>";
            msg += "<Url><![CDATA[http://mp.weixin.qq.com/s/fA-B0Ul7zFy9mpSBtrzyOA]]></Url>";
            msg += "</item>";
            msg += "<item>";
            msg += "<Title><![CDATA[2018《初级会计实务》必须熟悉的会计科目]]></Title> ";
            msg += "<Description><![CDATA[2018《初级会计实务》必须熟悉的会计科目]]></Description>";
            msg += "<PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz_jpg/iaNBEqjMsH1TPEnDEPvPtxXEYiaIialFegyVwuxbh2P5Lra9ibpO2nmSmuZ4eHwgkxcW7au6msv03Q6m7RFiaDBiaGwg/640?wx_fmt=jpeg&wxfrom=5&wx_lazy=1]]></PicUrl>";
            msg += "<Url><![CDATA[https://mp.weixin.qq.com/s/viIoEC23WD8L6XvzQgWekQ]]></Url>";
            msg += "</item>";
            msg += "</Articles>";
             */


        }
        msg += "</xml>";

        //<xml>
        //<ToUserName><![CDATA[toUser]]></ToUserName>
        //<FromUserName><![CDATA[fromUser]]></FromUserName>
        //<CreateTime>12345678</CreateTime>
        //<MsgType><![CDATA[news]]></MsgType>
        //<ArticleCount>2</ArticleCount>
        //<Articles>
        //<item>
        //<Title><![CDATA[title1]]></Title> 
        //<Description><![CDATA[description1]]></Description>
        //<PicUrl><![CDATA[picurl]]></PicUrl>
        //<Url><![CDATA[url]]></Url>
        //</item>
        //<item>
        //<Title><![CDATA[title]]></Title>
        //<Description><![CDATA[description]]></Description>
        //<PicUrl><![CDATA[picurl]]></PicUrl>
        //<Url><![CDATA[url]]></Url>
        //</item>
        //</Articles>
        //</xml> 

        //WriteLog("【" + DateTime.Now + "自动接口回复】" + msg);
        WXPushLog("接口回复1", msg);
        Response.Write(msg);
        Response.End();
        #endregion
    }

    /// <summary>
    /// 邦定推广用户的公众号
    /// </summary>
    /// <param name="MsgType"></param>
    /// <param name="wxMsg"></param>
    /// <returns></returns>
    public void BindUser(string MsgType, string wxMsg)
    {
        /*
        <xml>
        <ToUserName><![CDATA[gh_384b77007ac6]]></ToUserName> 
        <FromUserName><![CDATA[oKiw2t92-9nwoTj4QXgMiAb1fsnQ]]></FromUserName> 
        <CreateTime>1519282100</CreateTime> 
        <MsgType><![CDATA[event]]></MsgType> 
        <Event><![CDATA[subscribe]]></Event> 
        <EventKey><![CDATA[qrscene_oKiw2t92-9nwoTj4QXgMiAb1fsnQ]]></EventKey> 
        <Ticket><![CDATA[gQGJ8TwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAyTWJFbzFrR0k4U1QxMDAwMGcwNy0AAgT4Zo5aAwQAAAAA]]></Ticket> 
        </xml>
         */

        if (MsgType == "event")
        {
            string Event = GetMsgField(wxMsg, "Event");
            //if (Event == "subscribe" || Event == "SCAN")
            if (Event == "subscribe")
            {
                //邦定推广用户的公众号
                string myOpenId = GetMsgField(wxMsg, "EventKey").Replace("qrscene_", "");
                string firendNewOpenId = GetMsgField(wxMsg, "FromUserName");

                JObject userObj = NG.WeiXin.NGWeiXinUser.GetUserInfoByOpenId(firendNewOpenId);
                string nickname = userObj == null ? "" : userObj["nickname"].ToString();
                string sex = userObj == null ? "" : userObj["sex"].ToString();
                string headimgurl = userObj == null ? "" : userObj["headimgurl"].ToString();

                //入库
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into T_WXQRCodeShare_Log(");
                strSql.Append("sysnumber,logTime,myOpenId,firendNewOpenId,firendNewNickname,firendNewSex,firendNewHeader,scanInfo,remarks)");
                strSql.Append(" values (");
                strSql.Append("@sysnumber,@logTime,@myOpenId,@firendNewOpenId,@firendNewNickname,@firendNewSex,@firendNewHeader,@scanInfo,@remarks)");
                SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@logTime", SqlDbType.DateTime),
					new SqlParameter("@myOpenId", SqlDbType.VarChar,50),
					new SqlParameter("@firendNewOpenId", SqlDbType.VarChar,50),
                    new SqlParameter("@firendNewNickname", SqlDbType.VarChar,50),
                    new SqlParameter("@firendNewSex", SqlDbType.VarChar,50),
                    new SqlParameter("@firendNewHeader", SqlDbType.VarChar,500),
                    new SqlParameter("@scanInfo", SqlDbType.VarChar,4000),
                    new SqlParameter("@remarks", SqlDbType.VarChar,500),
                                    };
                parameters[0].Value = Guid.NewGuid().ToString();
                parameters[1].Value = DateTime.Now;
                parameters[2].Value = myOpenId;
                parameters[3].Value = firendNewOpenId;
                parameters[4].Value = nickname;
                parameters[5].Value = sex;
                parameters[6].Value = headimgurl;
                parameters[7].Value = wxMsg;
                parameters[8].Value = "我的二维码推广";

                StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
                adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);

                //消息通知分享推广人
                if (myOpenId != "")
                {
                    WXSendShare(myOpenId, nickname);

                }

                //赠送优惠券
                if (myOpenId != "")
                {
                    object objMemberId = adoHelper.ExecuteSqlScalar("select MemberId from T_Member_Info where WXOpenId='" + myOpenId + "'");
                    if (objMemberId != null) { GetCouponShareBat(objMemberId.ToString()); }
                }
            }
        }
    }


    /*
     19ziuKJhRshfoArJrEUEwkg9gt6McIg5Grpnfb1q1Xk
     */
    protected int WXSendShare(string touser, string firendNewNickname)
    {
        //发送到微信
        string token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string first = "您好，有用户通过您的分享进入平台！";   //标题
        string keyword1 = firendNewNickname;   //字段1
        string keyword2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //字段2
        string urlPage = "http://www.yiqixkj.com/NGWeiXinRoot/YqxkjShareList.aspx?wx_openid=" + touser + "";    //详情页面url
        string template_id = System.Configuration.ConfigurationManager.AppSettings["share_template_id"];
        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token;
        string temp = "{\"touser\": \"" + touser + "\"," +
                      "\"template_id\": \"" + template_id + "\", " +
                      "\"topcolor\": \"#FF0000\", " +
                      "\"url\":\"" + urlPage + "\"," +
                      "\"data\": " +
                      "{\"first\": {\"value\": \"" + first + "\",\"color\":\"#173177\"}," +
                      "\"keyword1\": { \"value\": \"" + keyword1 + "\",\"color\":\"#173177\"}," +
                      "\"keyword2\": { \"value\": \"" + keyword2 + "\",\"color\":\"#173177\"}," +
                      "\"remark\": {\"value\": \"★点击详情查看我分享的用户统计★\",\"color\":\"#FF0000\"}}}";
        string result = NG.WeiXin.NGWeiXinPubTools.GetPage(url, temp);
        NG.WeiXin.Log.Debug(this.GetType().ToString(), "【MainInterface.aspx-->WXSendShare】" + result);
        return 1;
    }


    /// <summary>
    /// 微信分享赠送（批量领取）
    /// </summary>
    /// <param name="MemberId"></param>
    /// <returns></returns>
    protected int GetCouponShareBat(string MemberId)
    {
        AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
        //config
        DataTable dtConfig = ado.ExecuteSqlDataset("select * from T_Base_Coupon where GetPlaceInfo='微信分享赠送' and CouponType='抵用券' and  isEffect=1 ").Tables[0];
        if (dtConfig.Rows.Count == 0) { return -1; }

        //add
        int result = 0;
        int flag = 10;
        foreach (DataRow row in dtConfig.Rows)
        {
            List<SqlParameter> plist = new List<SqlParameter>();
            string guid = Guid.NewGuid().ToString();
            string CouponId = MemberId + DateTime.Now.ToString("yyMMddHHmmss") + flag;
            plist.Add(new SqlParameter("@Sysnumber", guid));
            plist.Add(new SqlParameter("@MemberId", MemberId));
            plist.Add(new SqlParameter("@CouponId", CouponId));
            plist.Add(new SqlParameter("@CouponType", "抵用券"));
            plist.Add(new SqlParameter("@CouponValue", row["CouponValue"]));
            plist.Add(new SqlParameter("@StartTime", row["StartTime"]));
            plist.Add(new SqlParameter("@EndTime", row["EndTime"]));
            plist.Add(new SqlParameter("@IsUsed", "0"));
            plist.Add(new SqlParameter("@Remark", row["Context"]));
            plist.Add(new SqlParameter("@GetPlaceInfo", "微信分享赠送"));
            plist.Add(new SqlParameter("@minPrice", row["minPrice"]));
            plist.Add(new SqlParameter("@maxPrice", row["maxPrice"]));
            SqlParameter[] p = plist.ToArray();
            result += StarTech.DBCommon.InsertData("T_Member_Coupon", p);
            flag++;
        }
        return result;
    }


    public void WriteLog(string text)
    {
        string mailText = text;
        string fileName = DateTime.Now.ToString("yyyyMMdd") + "log.txt";
        string dir = HttpContext.Current.Server.MapPath("/log/");
        if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
        string path = dir + fileName;
        if (!System.IO.File.Exists(path))
        {
            using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
            {
                sw.WriteLine("-----------------记录开始----------------");
            }
        }
        System.IO.StreamWriter sr = System.IO.File.AppendText(path);
        sr.WriteLine(mailText);
        sr.Close();
    }

    /// <summary>
    /// ServicesLog
    /// </summary>
    /// <param name="actionType"></param>
    /// <param name="logInfo"></param>
    public void WXPushLog(string actionType, string logInfo)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into T_WXServicePush_Log(");
        strSql.Append("sysnumber,logTime,actionType,logInfo,MsgType,Event)");
        strSql.Append(" values (");
        strSql.Append("@sysnumber,@logTime,@actionType,@logInfo,@MsgType,@Event)");
        SqlParameter[] parameters = {
					new SqlParameter("@sysnumber", SqlDbType.VarChar,50),
					new SqlParameter("@logTime", SqlDbType.DateTime),
					new SqlParameter("@actionType", SqlDbType.VarChar,50),
					new SqlParameter("@logInfo", SqlDbType.Text),
                    new SqlParameter("@MsgType", SqlDbType.VarChar,50),
                    new SqlParameter("@Event", SqlDbType.VarChar,50),
                                    };
        parameters[0].Value = Guid.NewGuid().ToString();
        parameters[1].Value = DateTime.Now;
        parameters[2].Value = actionType;
        parameters[3].Value = logInfo;
        parameters[4].Value = GetMsgField(logInfo, "MsgType");
        parameters[5].Value = GetMsgField(logInfo, "Event");

        StarTech.DBUtility.AdoHelper adoHelper = StarTech.DBUtility.AdoHelper.CreateHelper("DB_Instance");
        adoHelper.ExecuteSqlNonQuery(strSql.ToString(), parameters);
    }





    /*
    <xml>
    <ToUserName><![CDATA[gh_384b77007ac6]]></ToUserName> 
    <FromUserName><![CDATA[oKiw2t92-9nwoTj4QXgMiAb1fsnQ]]></FromUserName> 
    <CreateTime>1519282100</CreateTime> 
    <MsgType><![CDATA[event]]></MsgType> 
    <Event><![CDATA[subscribe]]></Event> 
    <EventKey><![CDATA[qrscene_oKiw2t92-9nwoTj4QXgMiAb1fsnQ]]></EventKey> 
    <Ticket><![CDATA[gQGJ8TwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAyTWJFbzFrR0k4U1QxMDAwMGcwNy0AAgT4Zo5aAwQAAAAA]]></Ticket> 
    </xml>
     */
    public string GetMsgField(string logInfo,string field)
    {
        if (logInfo.IndexOf("<xml>") == -1) 
        {
            return "";
        }

        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(logInfo));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(stream);
        XmlNodeList xmlList = xmlDoc.ChildNodes;
        if (field == "MsgType") {
            //event
            if (logInfo.IndexOf("MsgType") > -1) {return  xmlDoc.SelectNodes("/xml/MsgType")[0].InnerText; }
        }
        else if (field == "Event")
        {
            //subscribe
            if (logInfo.IndexOf("Event") > -1) { return xmlDoc.SelectNodes("/xml/Event")[0].InnerText; }
        }
        else if (field == "EventKey")
        {
            //qrscene_
            if (logInfo.IndexOf("EventKey") > -1) { return xmlDoc.SelectNodes("/xml/EventKey")[0].InnerText; }
        }
        else if(field == "ToUserName"){
            if (logInfo.IndexOf("ToUserName") > -1) { return xmlDoc.SelectNodes("/xml/ToUserName")[0].InnerText; }
        }
        else if (field == "FromUserName")
        {
            if (logInfo.IndexOf("FromUserName") > -1) { return xmlDoc.SelectNodes("/xml/FromUserName")[0].InnerText; }
        }

        
        return "-";
    }

}

