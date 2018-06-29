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

public partial class AppModules_WXSendMessageLog : StarTech.Adapter.StarTechPage
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
        DataTable dtUsers = GetUsers();
        string atoken = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string strUrl = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + atoken;
        string datas = NG.WeiXin.NGWeiXinPubTools.SendGetHttpRequest(strUrl, "GET");

        JObject ja = (JObject)JsonConvert.DeserializeObject(datas);
        int totalnum = int.Parse(ja["count"].ToString());
        for (int i = 0; i < totalnum; i++)
        {
            string openid = ja["data"]["openid"][i].ToString();

            //string str = " https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + atoken + "&openid=" + openid + "&lang=zh_CN";
            //string data = NG.WeiXin.NGWeiXinPubTools.SendGetHttpRequest(str, "GET");
            //JObject ja1 = (JObject)JsonConvert.DeserializeObject(data);
            //string UserName = ja1["nickname"].ToString();
            //string remark = ja1["remark"].ToString();

            DataRow[] rowUser = dtUsers.Select("WXOpenId='" + openid + "'");
            if (rowUser.Length > 0)
            {
                string UserName = rowUser[0]["TrueName"].ToString();
                ListItem item = new ListItem(UserName, openid);
                this.ddlUsers.Items.Add(item);
            }
        }
    }

    protected DataTable GetUsers()
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        //string sql = "select * from [T_Member_Info] where MemberId in(select MemberId from T_Order_Info)";
        string sql = "select * from [T_Member_Info]";
        DataTable dt = adoHelper.ExecuteSqlDataset(sql).Tables[0];
        return dt;
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string touser = this.ddlUsers.SelectedValue;
        string token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
        string var1 = "任务2：会计基础知识";
        string var2 = "会计基础知识包括了：基础知识、会计知识、高级会计、中级会计知识等";
        string template_id = "SW06RKvnwY9ZLxIp16hX_SNkEMElCskYRcumW0ggxcM";
        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token;
        string temp = "{\"touser\": \"" + touser + "\"," +
                      "\"template_id\": \"" + template_id + "\", " +
                      "\"topcolor\": \"#FF0000\", " +
                      "\"url\":\"http://www.yiqixkj.com/NGWeiXinRoot/YqxkjNewTask.aspx\"," +
                      "\"data\": " +
                      "{\"var1\": {\"value\": \"" + var1 + "\",\"color\":\"#173177\"}," +
                      "\"var2\": { \"value\": \"" + var2 + "\",\"color\":\"#173177\"}," +
                      "\"var3\": { \"value\": \"" + DateTime.Now + "\",\"color\":\"#173177\"}," +
                      "\"var4\": {\"value\": \"请任务完成任务！\",\"color\":\"#FF0000\"}}}";
        string result = NG.WeiXin.NGWeiXinPubTools.GetPage(url, temp);
        //errcode
        JObject ja1 = (JObject)JsonConvert.DeserializeObject(result);
        string errcode = ja1["errcode"].ToString();
        if (errcode == "0")
        {
            //创建日志
            AddMsg(touser, var1, var2);
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('发送成功');freshCurrentPage();</script>");
        }
    }

    protected int AddMsg(string WXOpenId, string title, string remark)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        string sql = "INSERT INTO [NG_WeiXinKJ].[dbo].[T_WXSendMessage_Log]([sysnumber],[WXOpenId],[type],[createTime],[title],[remark],[url],[isRead]) ";
        sql += "VALUES('" + Guid.NewGuid().ToString() + "','" + WXOpenId + "','消息提醒',getdate(),'" + title + "','" + remark + "','',0)";
        return adoHelper.ExecuteSqlNonQuery(sql);
    }
}