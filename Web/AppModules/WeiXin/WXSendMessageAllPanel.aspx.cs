using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using StarTech.DBUtility;
using System.Data;
using System.Collections;

public partial class AppModules_WXSendMessagePanel : StarTech.Adapter.StarTechPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllOpenIds();
        }
    }


    public void GetAllOpenIds()
    {
        string html = "";
        DataTable dtUsers = GetUsers();
        DataTable dtLogs = GetDaySendLogs();
        string atoken = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string strUrl = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + atoken;
        string datas = NG.WeiXin.NGWeiXinPubTools.SendGetHttpRequest(strUrl, "GET");

        JObject ja = (JObject)JsonConvert.DeserializeObject(datas);
        int totalnum = int.Parse(ja["count"].ToString());
        for (int i = 0; i < totalnum; i++)
        {
            string openid = ja["data"]["openid"][i].ToString();

            //本地库
            DataRow[] rowUser = dtUsers.Select("WXOpenId='" + openid + "'");
            string localName = rowUser.Length > 0 ? rowUser[0]["TrueName"].ToString() : "";
            string showName = localName == "" ? "【未知关注】" : localName;
            int count = dtLogs.Select("WXOpenId='" + openid + "'").Length;
            string log = count == 0 ? "（今日未发送）" : "（今日已发送" + count + "次）";
            html += "<input type=\"checkbox\" name=\"cb_user\" id=\"cb_" + openid + "\" value=\"" + openid + "\">" + showName + log + "";
        }
        this.ltHtml.Text = html;
    }

    protected DataTable GetUsers()
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        //string sql = "select * from [T_Member_Info] where MemberId in(select MemberId from T_Order_Info)";
        string sql = "select * from [T_Member_Info]";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
        return dt;
    }

    protected DataTable GetDaySendLogs()
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        //string sql = "select * from [T_Member_Info] where MemberId in(select MemberId from T_Order_Info)";
        string sql = "select * from [T_WXSendMessage_Log] where createTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
        return dt;
    }



    protected int WXSend(string touser, string msgTitle, string msgContent)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        object objMemberId = adoHelper.ExecuteSqlScalar("select MemberId from T_Member_Info where WXOpenId='" + touser + "'");

        System.Threading.Thread.Sleep(10);
        int total = 0;

        //time
        string msgTime = this.txtTime.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : this.txtTime.Text.Trim();
        //remarks
        string msgRemarks = this.txtRemarks.Text.Trim() == "" ? "★才通天下吧，详情内容请点击请进入微信公众号★" : this.txtRemarks.Text.Trim();
        //url
        string msgUrl = this.txtUrl.Text.Trim() == "" ? "http://www.yiqixkj.com/NGWeiXinRoot/YqxkjHome.aspx" : this.txtUrl.Text.Trim();

        //发送到微信
        string token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string first = msgTitle;   //标题
        string keyword1 = msgContent; //描述
        string keyword2 = msgTime;   //字段2
        string urlPage = msgUrl;    //详情页面url
        string template_id = System.Configuration.ConfigurationManager.AppSettings["notice_template_id"];   //通知模板
        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token;
        string temp = "{\"touser\": \"" + touser + "\"," +
                      "\"template_id\": \"" + template_id + "\", " +
                      "\"topcolor\": \"#FF0000\", " +
                      "\"url\":\"" + urlPage + "\"," +
                      "\"data\": " +
                      "{\"first\": {\"value\": \"" + first + "\",\"color\":\"#173177\"}," +
                      "\"keyword1\": { \"value\": \"" + keyword1 + "\",\"color\":\"#173177\"}," +
                      "\"keyword2\": { \"value\": \"" + keyword2 + "\",\"color\":\"#173177\"}," +
                      "\"remark\": {\"value\": \"" + msgRemarks + "\",\"color\":\"#FF0000\"}}}";
        string result = NG.WeiXin.NGWeiXinPubTools.GetPage(url, temp);
        //errcode
        JObject ja1 = (JObject)JsonConvert.DeserializeObject(result);
        string errcode = ja1["errcode"].ToString();
        if (errcode == "0")
        {
            total++;
            //创建日志
            AddMsg(touser, msgTitle, msgContent);
        }
        else
        {
            NG.WeiXin.Log.Debug(this.GetType().ToString(), "【WXSendMessagePanel.aspx-->WXSend】" + result);
        }

        return total;
    }

    protected int AddMsg(string WXOpenId, string title, string remark)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string sql = "INSERT INTO [NG_WeiXinKJ].[dbo].[T_WXSendMessage_Log]([sysnumber],[WXOpenId],[type],[createTime],[title],[remark],[url],[isRead]) ";
        sql += "VALUES('" + Guid.NewGuid().ToString() + "','" + WXOpenId + "','消息提醒',getdate(),'" + title + "','" + remark + "','',0)";
        return adoHelper.ExecuteSqlNonQuery(sql);
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        int total = 0;
        if (this.txtTitle.Text.Trim() != "" && this.txtMsg.Text.Trim() != "")
        {
            DataTable dtLogs = GetDaySendLogs();
            foreach (string openid in this.hidSelectIds.Value.Split('|'))
            {
                int count = dtLogs.Select("WXOpenId='" + openid + "'").Length;
                if (count < 1)
                {
                    if (openid != "")
                    {
                        total += WXSend(openid, this.txtTitle.Text.Trim(), this.txtMsg.Text.Trim());
                    }
                }
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('发送成功(" + total + ")条消息');location.href='WXSendMessageAllPanel.aspx';</script>");
    }


    protected void btnSend2_Click(object sender, EventArgs e)
    {
        int total = 0;
        if (this.txtTitle.Text.Trim() != "" && this.txtMsg.Text.Trim() != "")
        {
            foreach (string openid in this.hidSelectIds.Value.Split('|'))
            {
                if (openid != "")
                {
                    total += WXSend(openid, this.txtTitle.Text.Trim(), this.txtMsg.Text.Trim());
                }
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('发送成功(" + total + ")条消息');location.href='WXSendMessageAllPanel.aspx';</script>");
    }
    protected void btnTest_Click(object sender, EventArgs e)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        //string sql = "select * from [T_Member_Info] where MemberId in(select MemberId from T_Order_Info)";
        string sql = "select * from [T_Member_Info] where tel='"+this.txtTel.Text.Trim()+"'";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            int total = WXSend(dt.Rows[0]["WXOpenId"].ToString(), this.txtTitle.Text.Trim(), this.txtMsg.Text.Trim());
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('发送成功(" + total + ")条消息');location.href='WXSendMessageAllPanel.aspx';</script>");
        }
    }
}