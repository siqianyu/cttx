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
            DataRow[] rowUser = dtUsers.Select("WXOpenId='" + openid + "'");
            if (rowUser.Length > 0)
            {
                int count = dtLogs.Select("WXOpenId='" + openid + "'").Length;
                string log = count == 0 ? "（今日未发送）" : "（今日已发送" + count + "次）";
                html += "<input type=\"checkbox\" name=\"cb_user\" id=\"cb_" + rowUser[0]["MemberId"] + "\" value=\"" + openid + "\">" + rowUser[0]["TrueName"] + log + "";
            }
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


    /// <summary>
    /// 获取下个学习任务(支持多课程)
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    protected ArrayList GetNextGoodsInfo(string memberId)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        ArrayList list = new ArrayList();
        //获取目前所有的课程
        DataTable dtGroup = adoHelper.ExecuteSqlDataset("select GoodsToTypeId from V_Member_StudyRecord where MemberId='" + memberId + "' group by GoodsToTypeId").Tables[0];
        foreach (DataRow row in dtGroup.Rows)
        {
            //获取最新的任务
            DataTable curGoods = adoHelper.ExecuteSqlDataset("select top 1 * from V_Member_StudyRecord where GoodsToTypeId='" + row["GoodsToTypeId"] + "' and MemberId='" + memberId + "' order by Orderby desc").Tables[0];
            if (curGoods.Rows.Count > 0)
            {
                //当前已完成的课程
                if (curGoods.Rows[0]["IfReachedTotalTime"].ToString() == "1" && curGoods.Rows[0]["IfPassExamination"].ToString() == "1")
                {
                    DataTable dtNext = adoHelper.ExecuteSqlDataset("select * from T_Goods_Info where GoodsToTypeId='" + row["GoodsToTypeId"] + "' and Orderby>" + curGoods.Rows[0]["Orderby"] + " order by Orderby asc").Tables[0];
                    if (dtNext.Rows.Count > 0)
                    {
                        string GoodsDesc = PubFunction.CheckStr(dtNext.Rows[0]["GoodsDesc"].ToString());
                        list.Add(dtNext.Rows[0]["GoodsId"] + "$" + dtNext.Rows[0]["GoodsName"] + "$" + GoodsDesc);
                    }
                }
                else
                {
                    //当前未完成的课程
                    string GoodsDesc = PubFunction.CheckStr(curGoods.Rows[0]["GoodsDesc"].ToString());
                    list.Add(curGoods.Rows[0]["GoodsId"] + "$" + curGoods.Rows[0]["GoodsName"] + "$" + GoodsDesc);
                }
            }
        }
        return list;
    }

    protected int WXSend(string touser)
    {
        AdoHelper adoHelper = AdoHelper.CreateHelper("DB_Instance");
        object objMemberId = adoHelper.ExecuteSqlScalar("select MemberId from T_Member_Info where WXOpenId='" + touser + "'");
        ArrayList list = GetNextGoodsInfo(objMemberId.ToString());
        System.Threading.Thread.Sleep(100);
        int total = 0;
        foreach (string taskInfo in list)
        {
            string[] taskInfoArr = taskInfo.Split('$');
            //发送到微信
            string token = NG.WeiXin.NGAccessTokenTools.GetExistAccess_Token();
            string first = "您好！您已完成本阶段任务，即将进入下个阶段任务。";   //标题
            string keyword1 = "《" + taskInfoArr[1] + "》" + taskInfoArr[2]; //描述
            string keyword2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //字段2
            string urlPage = "http://www.yiqixkj.com/NGWeiXinRoot/YqxkjNewTask.aspx?goodsId=" + taskInfoArr[0] + "";    //详情页面url
            string template_id = System.Configuration.ConfigurationManager.AppSettings["study_template_id"];
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token;
            string temp = "{\"touser\": \"" + touser + "\"," +
                          "\"template_id\": \"" + template_id + "\", " +
                          "\"topcolor\": \"#FF0000\", " +
                          "\"url\":\"" + urlPage + "\"," +
                          "\"data\": " +
                          "{\"first\": {\"value\": \"" + first + "\",\"color\":\"#173177\"}," +
                          "\"keyword1\": { \"value\": \"" + keyword1 + "\",\"color\":\"#173177\"}," +
                          "\"keyword2\": { \"value\": \"" + keyword2 + "\",\"color\":\"#173177\"}," +
                          "\"remark\": {\"value\": \"★才通天下吧，更多学习内容请进入微信公众号个人中心★\",\"color\":\"#FF0000\"}}}";
            string result = NG.WeiXin.NGWeiXinPubTools.GetPage(url, temp);
            //errcode
            JObject ja1 = (JObject)JsonConvert.DeserializeObject(result);
            string errcode = ja1["errcode"].ToString();
            if (errcode == "0")
            {
                total++;
                //创建日志
                AddMsg(touser, taskInfoArr[1], taskInfoArr[2]);
            }
            else
            {
                NG.WeiXin.Log.Debug(this.GetType().ToString(), "【WXSendMessagePanel.aspx-->WXSend】" + result);
            }
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
        DataTable dtLogs = GetDaySendLogs();
        foreach (string openid in this.hidSelectIds.Value.Split('|'))
        {
            int count = dtLogs.Select("WXOpenId='" + openid + "'").Length;
            if (count < 1)
            {
                if (openid != "")
                {
                    total += WXSend(openid);
                }
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('发送成功(" + total + ")条消息');location.href='WXSendMessagePanel.aspx';</script>");
    }


    protected void btnSend2_Click(object sender, EventArgs e)
    {
        int total = 0;
        foreach (string openid in this.hidSelectIds.Value.Split('|'))
        {
            if (openid != "")
            {
                total += WXSend(openid);
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('发送成功(" + total + ")条消息');location.href='WXSendMessagePanel.aspx';</script>");
    }
}