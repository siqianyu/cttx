using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StarTech.DBUtility;
using System.Web.Configuration;

public partial class NGWeiXinRoot_YqxkjCourseDetail : System.Web.UI.Page
{
    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string DGZX_CategoryID;//订购咨询问答
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DGZX_CategoryID = WebConfigurationManager.AppSettings["DGZX_CategoryID"];//订购咨询问答
        this.hid_goodsId.Value = Common.NullToZero(Request["id"]).ToString();
        if (!IsPostBack)
        {
            BindCourse();
            BindComment(this.hid_goodsId.Value);
            if (Session["MemberId"] != null && Session["MemberId"].ToString() != "")
            {
                this.hidMemberId.Value = Session["MemberId"].ToString();

                //跳转到我购买的课程里
                if (CheckIsVipBuy(this.hid_goodsId.Value, this.hidMemberId.Value) == true)
                {
                    Response.Redirect("YqxkjMemberCourseDetail.aspx?id=" + this.hid_goodsId.Value + "", true);
                }                
                
                //同步支付订单
                this.hidNoPayOrderId.Value = CheckNoPayOrder(this.hid_goodsId.Value, this.hidMemberId.Value);

                //支付页面跳转回
                Session["PayPage_BackUrl"] = "/NGWeiXinRoot/YqxkjCourseDetail.aspx?id=" + this.hid_goodsId.Value;
            }
        }
    }

    /// <summary>
    /// 判断是否已经购买
    /// </summary>
    /// <param name="goodsId"></param>
    /// <returns></returns>
    public bool CheckIsVipBuy(string goodsId, string memberId)
    {
        string sql = "select GoodsId from T_Order_InfoDetail where orderId in(select orderId from T_Order_Info where MemberId='" + memberId + "' and IsPay=1 and OrderType='Goods')";
        DataTable dtGoods = ado.ExecuteSqlDataset(sql).Tables[0];
        foreach (DataRow row in dtGoods.Rows)
        {
            if (row["GoodsId"].ToString() == goodsId) { return true; }
        }
        return false;
    }

    /// <summary>
    /// 已经下单未支付的订单
    /// </summary>
    /// <param name="goodsId"></param>
    /// <returns></returns>
    public string CheckNoPayOrder(string goodsId, string memberId)
    {
        string sql = "select a.orderId from T_Order_InfoDetail b,T_Order_Info a where b.orderId=a.orderId and a.MemberId='" + memberId + "' and a.IsPay=0 and a.OrderType='Goods' and b.GoodsId='" + goodsId + "' order by a.OrderTime desc";
        DataTable dtGoods = ado.ExecuteSqlDataset(sql).Tables[0];
        return dtGoods.Rows.Count > 0 ? dtGoods.Rows[0]["orderId"].ToString() : "";

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

                string freeTest = "";
                string href = "";
                if (decimal.Parse(row["SalePrice"].ToString()) < 0)
                {
                    freeTest = "icon-jiantou";
                    href = "onclick=\"location.href='YqxkjCourseFreeTest.aspx?id=" + row["GoodsId"].ToString() + "'\"";
                }

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

    public string ListAD()
    {
        DataTable dt = ado.ExecuteSqlDataset("SELECT [PicPath] FROM [T_Goods_Pic] where GoodsId='"+this.hid_goodsId.Value+"' order by Orderby asc").Tables[0];
        string html = "";
        string css = "";
        foreach (DataRow row in dt.Rows)
        {
            html += "<li " + css + "><img src=\"" + row["PicPath"] + "\"></li>";
            if (css == "") { css = "style=\"display:none\""; }
        }
        return html;
    }
}