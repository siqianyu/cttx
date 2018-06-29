using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;

public partial class NGWeiXinRoot_YqxkjMemberCourseDetail : StarTech.Adapter.BasePage
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hid_goodsId.Value = Common.NullToZero(Request["id"]).ToString();
        if (!IsPostBack)
        {
            if (CheckIsVipBuy(this.hid_goodsId.Value) == false)
            {
                Response.Write("<script>alert('请先购买该课程');location.href='YqxkjHome.aspx';</script>"); 
                Response.End();
            }

            BindCourse();
            BindComment(this.hid_goodsId.Value);
            this.hidMemberId.Value = this.MemberId;
        }
    }

    /// <summary>
    /// 判断是否已经购买
    /// </summary>
    /// <param name="goodsId"></param>
    /// <returns></returns>
    public bool CheckIsVipBuy(string goodsId)
    {
        string sql = "select GoodsId from T_Order_InfoDetail where orderId in(select orderId from T_Order_Info where MemberId='" + MemberId + "' and IsPay=1 and OrderType='Goods')";
        DataTable dtGoods = ado.ExecuteSqlDataset(sql).Tables[0];
        foreach (DataRow row in dtGoods.Rows)
        {
            if (row["GoodsId"].ToString() == goodsId) { return true; }
        }
        return false;
    }

    public void BindCourse()
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Goods_Info where GoodsId = '" + this.hid_goodsId.Value + "' and IsSale=1").Tables[0];
        DataSet dsCount = ado.ExecuteSqlDataset("select count(1) from T_Goods_Info where [JobType]='SubGoods' and GoodsToTypeId = '" + this.hid_goodsId.Value + "' and IsSale=1;select count(1) from [T_Test_Queston] where [courseId] in(select GoodsId from T_Goods_Info where [JobType]='SubGoods' and GoodsToTypeId = '" + this.hid_goodsId.Value + "' and IsSale=1);");
        foreach (DataRow row in dt.Rows)
        {
            this.span_name.InnerText = row["GoodsName"].ToString();
            this.span_price.InnerText = row["SalePrice"].ToString();
            this.span_questioncout.InnerText = dsCount.Tables[1].Rows[0][0].ToString();
            this.span_taskcount.InnerText = dsCount.Tables[0].Rows[0][0].ToString();
            this.span_totalsale.InnerText = row["TotalSaleCount"].ToString();
            this.img_goods.Src = row["GoodsSmallPic"].ToString();
            this.div_goodsdesc.InnerHtml = row["GoodsDesc"].ToString();
        }
    }

    public string BindTask()
    {
        DataTable dt = ado.ExecuteSqlDataset("select MorePropertys,GoodsId,GoodsName,GoodsSmallPic,GoodsSampleDesc,SalePrice,TotalSaleCount,DataFrom from T_Goods_Info where GoodsToTypeId = '" + this.hid_goodsId.Value + "' and JobType='SubGoods' and IsSale=1 order by Orderby").Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            string[] questionInfoArr = QuestionHelper.ComputerQuestionRecord("", row["GoodsId"].ToString(), this.hidMemberId.Value).Split('|');

            if (row["MorePropertys"].ToString() == "外部课程" && row["DataFrom"].ToString() != "")
            {
                html += "<li onclick=\"location.href='YqxkjCourseDetail.aspx?id=" + row["DataFrom"].ToString() + "'\">";
                html += "<b>" + row["GoodsName"].ToString() + "</b>";
                html += "<span><i class=\"icon iconfont icon-jiantou\"></i></span>";
                html += "</li>";
            }
            else
            {
                string freeTest = "icon-jiantou";
                string href = "onclick=\"location.href='YqxkjMemberCoursePlay.aspx?id=" + row["GoodsId"].ToString() + "'\"";


                html += "<li " + href + "\">";
                html += "<b>" + row["GoodsName"].ToString() + "";
                html += "<a>已做<i>" + questionInfoArr[1] + "</i>题，共<i>" + questionInfoArr[0] + "</i>题，正确率<i>" + questionInfoArr[2] + "%</i></a></b>";
                html += "<span><i class=\"icon iconfont " + freeTest + "\"></i></span>";
                html += "</li>";
            }
        }
        return html;
    }

    public void BindComment(string goodsId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Goods_PingLun where isShow=1 and goodsId in (select goodsId from T_Goods_Info where JobType='SubGoods' and GoodsToTypeId = '" + this.hid_goodsId.Value + "' and IsSale=1) order by addTime desc").Tables[0];
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