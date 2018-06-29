using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;

public partial class NGWeiXinRoot_YqxkjCourseFreeTest : System.Web.UI.Page
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string path;
    public string pic;
    public int total_questions = 0; //总题目数量
    public int record_questions = 0; //记录数量
    public int record_percent = 0;//百分比

    protected void Page_Load(object sender, EventArgs e)
    {
        this.hidMemberId.Value = Session["MemberId"] != null ? Session["MemberId"].ToString() : Guid.NewGuid().ToString();
        this.hid_goodsId.Value = Common.NullToZero(Request["id"]).ToString();
        if (!IsPostBack)
        {
            BindCourse();
            BindComment(this.hid_goodsId.Value);
            BindQuestionRecord();
        }
    }

    /// <summary>
    /// 练习进度
    /// </summary>
    protected void BindQuestionRecord()
    {
        //总数
        DataTable obj_total_questions = null;
        if (this.hidMorePropertys.Value == "模拟试题")
        {
            obj_total_questions = ado.ExecuteSqlDataset("select Questions from T_Test_day where courseType = '" + this.hid_goodsId.Value + "' and shFlag=1").Tables[0];
            if (obj_total_questions.Rows.Count > 0)
            {
                this.total_questions = obj_total_questions.Rows[0]["Questions"].ToString().TrimEnd(',').TrimStart(',').Split(',').Length;
            }
        }
        if (total_questions == 0)
        {
            obj_total_questions = ado.ExecuteSqlDataset("select sysnumber from T_Test_Queston where courseId = '" + this.hid_goodsId.Value + "' and shFlag=1").Tables[0];
            this.total_questions = obj_total_questions.Rows.Count;
        }
        //已回答正确
        DataTable dtRecord = ado.ExecuteSqlDataset("select QuestionId from T_Test_ErrorRecord where IfPass=1 and [MemberId]='" + this.hidMemberId.Value + "' and [CourseId]='" + this.hid_goodsId.Value + "'").Tables[0];
        this.record_questions = dtRecord.Rows.Count;

        if (this.total_questions > 0)
        {
            this.record_percent = this.record_questions * 100 / this.total_questions;
        }
    }


    public void BindCourse()
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Goods_Info where GoodsId = '" + this.hid_goodsId.Value + "' and IsSale=1 and SalePrice<0").Tables[0];
      
        foreach (DataRow row in dt.Rows)
        {
            //this.span_price.InnerText = row["SalePrice"].ToString();
            //this.span_questioncout.InnerText = "1243";
            //this.span_taskcount.InnerText = "10";
            //this.span_totalsale.InnerText = row["TotalSaleCount"].ToString();
            //this.img_goods.Src = row["GoodsSmallPic"].ToString();
            this.div_goodsdesc.InnerHtml = row["GoodsSampleDesc"].ToString();

            this.path = this.hidVideoPath.Value = row["BookInfo"].ToString();
            this.pic = this.hidVideoPic.Value = row["GoodsSmallPic"].ToString();
            this.hidMorePropertys.Value = row["MorePropertys"].ToString();
            if (this.hidMorePropertys.Value == "模拟试题")
            {
                this.path = "";
            }
        }
    }


    public void BindComment(string goodsId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Goods_PingLun where isShow=1 and goodsId= '" + this.hid_goodsId.Value + "' order by addTime desc").Tables[0];
        this.ltCommnetCount.Text = dt.Rows.Count.ToString();
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<li>";
            html += "<b class=\"user-icon\"><i class=\"icon iconfont icon-user\"></i></b>";
            html += "<p>";
            html += "<span>" + row["memberName"].ToString() + "<i class=\"time\">" + PubFunction.TimeComputer(DateTime.Now, DateTime.Parse(row["addTime"].ToString())) + "</i></span>";
            html += "<span class=\"course-mark\">课程评分：";
            html += "<i class=\"icon iconfont icon-pingfen\"></i><i class=\"icon iconfont icon-pingfen\"></i>";
            html += "<i class=\"icon iconfont icon-pingfen\"></i><i class=\"icon iconfont icon-pingfen\"></i>";
            html += "<i class=\"icon iconfont icon-pingfen\"></i>";
            html += "</span>";
            html += "<span>" + row["memberContent"].ToString() + "</span>";
            html += "</p>";
            html += "</li>";
        }
        this.ul_commnet_list.InnerHtml = html;
    }
}