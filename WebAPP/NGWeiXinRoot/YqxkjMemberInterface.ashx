<%@ WebHandler Language="C#" Class="YqxkjMemberInterface" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using StarTech.DBUtility;
using System.Text;
using StarTech.Order.Order;
using StarTech.Order;
using System.Collections.Generic;

/// <summary>
/// 基于session验证类，防止越权访问和读取订单、视频等数据
/// </summary>
/// 
public class YqxkjMemberInterface : IHttpHandler, IRequiresSessionState
{

    #region 用户和权限.....................
    /// <summary>
    /// 权限控制
    /// </summary>
    /// <param name="context"></param>
    protected void UserCheck(HttpContext context)
    {
        if (context.Session["MemberId"] == null || context.Session["MemberId"].ToString() == "")
        {
            context.Response.Write("无权访问");
            context.Response.End();
        }
    }

    /// <summary>
    /// 用户id
    /// </summary>
    /// <param name="context"></param>
    protected string GetMemberId(HttpContext context)
    {
        if (context.Session["MemberId"] == null || context.Session["MemberId"].ToString() == "")
        {
            return "";
        }
        else
        {
            return context.Session["MemberId"].ToString();
        }
    }
    #endregion

    AdoHelper ado = AdoHelper.CreateHelper("DB_Instance");
    public string memberId;
    public void ProcessRequest(HttpContext context)
    {
        //权限验证
        UserCheck(context);

        //用户Id
        this.memberId = GetMemberId(context);

        //接收参数
        string flag = Common.NullToEmpty(context.Request["flag"]);
        string goodsid = Common.NullToEmpty(context.Request["goodsid"]);
        string couponid = Common.NullToEmpty(context.Request["couponid"]);
        int studytime = Common.NullToZero(context.Request["studytime"]);
        string email = Common.NullToEmpty(context.Request["email"]);
        string truename = HttpContext.Current.Server.UrlDecode(Common.NullToEmpty(context.Request["truename"]));
        string phone = Common.NullToEmpty(context.Request["phone"]);
        string areacode = Common.NullToEmpty(context.Request["areacode"]);
        string wxopenid = Common.NullToEmpty(context.Request["wxopenid"]);
        string pwd = Common.NullToEmpty(context.Request["pwd"]);
        string code = Common.NullToEmpty(context.Request["code"]);
        string msg = HttpContext.Current.Server.UrlDecode(Common.NullToEmpty(context.Request["msg"]));

        context.Response.ContentType = "text/plain";

        switch (flag.ToLower())
        {
            case "buy_course"://购买课程
                context.Response.Write(BuyCourse(goodsid, couponid));
                break;
            case "list_course_my"://我的课程
                context.Response.Write(ListCourseMy(this.memberId));
                break;
            case "list_course_task"://我的课程任务
                context.Response.Write(ListCourseTaskMy(this.memberId, goodsid));
                break;
            case "list_study_record"://我的学习进度
                context.Response.Write(ListStudyRecordMy(this.memberId, goodsid));
                break;
            case "write_study_record"://更新学习进度
                context.Response.Write(WriteStudyRecordMy(this.memberId, goodsid, studytime));
                break;
            case "get_coupon": //领取优惠券
                context.Response.Write(GetCoupon(this.memberId));
                break;
            case "get_coupon_indexbat": //领取优惠券首页一键领取
                context.Response.Write(GetCouponIndexBat(this.memberId));
                break;
            case "get_qrcode": //获取推广二维码
                context.Response.Write(GetQRCode(this.memberId));
                break;
            case "add_comment": //添加评论
                context.Response.Write(AddComment(this.memberId, truename, msg, goodsid));
                break; 
                
        }
    }


    /// <summary>
    /// 获取推广二维码
    /// </summary>
    /// <param name="MemberId"></param>
    /// <returns></returns>
    protected string GetQRCode(string MemberId)
    {
        string url = "http://www.yiqixkj.com/AppClient/ShareScanByMemberId.aspx?flag=Share&MemberId=" + MemberId;
        string img = QRCodeHelper.CreateQRCodeImg(url, "/Source/QRCode");
        return img;
    }


    /// <summary>
    /// 添加评论
    /// </summary>
    /// <param name="MemberId"></param>
    /// <param name="TrueName"></param>
    /// <param name="msg"></param>
    /// <param name="goodsId"></param>
    /// <returns></returns>
    protected string AddComment(string MemberId, string TrueName, string msg, string goodsId)
    {
        List<SqlParameter> plist = new List<SqlParameter>();
        string guid = Guid.NewGuid().ToString();
        plist.Add(new SqlParameter("@sysnumber", guid));
        plist.Add(new SqlParameter("@memberId", MemberId));
        plist.Add(new SqlParameter("@goodsId", goodsId));
        plist.Add(new SqlParameter("@memberName", TrueName));
        plist.Add(new SqlParameter("@memberContent", msg));
        plist.Add(new SqlParameter("@addTime", DateTime.Now));
        plist.Add(new SqlParameter("@isShow", "0"));
        SqlParameter[] p = plist.ToArray();
        int result = StarTech.DBCommon.InsertData("T_Goods_PingLun", p);
        return result.ToString();
    }

    /// <summary>
    /// 显示全部课程信息(我的)
    /// </summary>
    /// <returns></returns>
    protected string ListCourseMy(string MemberId)
    {
        string sql = "select * from T_Order_InfoDetail where orderId in(select orderId from T_Order_Info where MemberId='" + MemberId + "' and IsPay=1 and OrderType='Goods')";
        DataTable dtGoods = ado.ExecuteSqlDataset(sql).Tables[0];
        string goodsIds = "";
        foreach (DataRow row in dtGoods.Rows)
        {
            goodsIds += row["GoodsId"].ToString() + ",";
        }
        if (goodsIds == "") { return ""; }
        goodsIds = goodsIds.TrimEnd(',');
        
        string html = "";
        DataTable dt = ado.ExecuteSqlDataset("select GoodsId,GoodsName,GoodsSmallPic,GoodsSampleDesc,SalePrice,TotalSaleCount,(select count(1) from T_Goods_Info where GoodsToTypeId=a.GoodsId and IsSale=1)TaskCount from V_Goods_Info a where GoodsId in('" + goodsIds.Replace(",", "','") + "') and JobType='Goods'").Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            html += "<li onclick=\"location.href='YqxkjMemberCourseDetail.aspx?id=" + row["GoodsId"].ToString() + "'\">";
            html += "<div class=\"course-img left\"><img src=\"" + row["GoodsSmallPic"].ToString() + "\"></div>";
            html += "<p>";
            html += "<b>" + row["GoodsName"].ToString() + "</b>";
            html += "<span class=\"course-des\">" + row["GoodsSampleDesc"].ToString() + "</span>";
            html += "<span>" + row["TaskCount"].ToString() + "讲  |  <i class=\"over\">" + row["TotalSaleCount"].ToString() + "人学习中</i></span>";
            html += "</p>";
            html += "</li>";
        }
        return html;
    }


    /// <summary>
    /// 显示课程学习任务列表(我的)
    /// </summary>
    /// <returns></returns>
    protected string ListCourseTaskMy(string MemberId, string PGoodsId)
    {
        string sql = "select * from T_Goods_Info where GoodsToTypeId='" + PGoodsId + "' and JobType='SubGoods'";
        DataTable dt = ado.ExecuteSqlDataset(sql).Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            string GoodsSmallPic = "<img width='80' height='60' src='" + row["GoodsSmallPic"].ToString() + "'>";
            string PlayButton = "<input id='btnOrder' type='button' value='播放' onclick=\"play_course_task('" + row["GoodsId"].ToString() + "')\" />";
            html += "<div>" + GoodsSmallPic + row["GoodsName"].ToString() + PlayButton + "</div>";
        }
        return html;
    }


    /// <summary>
    /// 显示课程学习记录(我的)
    /// </summary>
    /// <returns></returns>
    protected string ListStudyRecordMy(string MemberId, string PGoodsId)
    {
        string sql = "select a.*,b.GoodsSmallPic,b.GoodsName from T_Member_StudyRecord a,T_Goods_Info b where a.MemberId='" + MemberId + "' and a.GoodsId=b.GoodsId and b.GoodsToTypeId='" + PGoodsId + "' and b.JobType='SubGoods'";
        DataTable dt = ado.ExecuteSqlDataset(sql).Tables[0];
        string html = "";
        foreach (DataRow row in dt.Rows)
        {
            string GoodsSmallPic = "<img width='80' height='60' src='" + row["GoodsSmallPic"].ToString() + "'>";
            string PlayButton = "<input id='btnOrder' type='button' value='继续播放' onclick=\"play_course_task('" + row["GoodsId"].ToString() + "')\" />";
            string Bar = row["TotalStudyTime"].ToString() + "/" + row["GoodsStudyTime"].ToString();
            html += "<div>" + GoodsSmallPic + row["GoodsName"].ToString() + Bar + PlayButton + "</div>";
        }
        return html;
    }


    /// <summary>
    /// 跟踪学习进度(我的)
    /// </summary>
    /// <returns></returns>
    protected int WriteStudyRecordMy(string MemberId, string goodsId, int appendTime)
    {
        string sql = "select * from T_Member_StudyRecord where GoodsId='" + goodsId + "' and MemberId='" + MemberId + "'";
        DataTable dt = ado.ExecuteSqlDataset(sql).Tables[0];

        if (dt.Rows.Count > 0)
        {
            int goodsStudyTime = dt.Rows[0]["GoodsStudyTime"].ToString() == "" ? 0 : int.Parse(dt.Rows[0]["GoodsStudyTime"].ToString());
            int preTotalStudyTime = dt.Rows[0]["TotalStudyTime"].ToString() == "" ? 0 : int.Parse(dt.Rows[0]["TotalStudyTime"].ToString());
            int curTotalStudyTime = preTotalStudyTime + appendTime;
            string updateInfo = "LastRecordTime=getdate(),TotalStudyTime=" + curTotalStudyTime + "";
            if (curTotalStudyTime >= goodsStudyTime) { updateInfo += ",IfReachedTotalTime=1"; }
            string sqlupdate = "update T_Member_StudyRecord set " + updateInfo + " where RecordId='" + dt.Rows[0]["RecordId"].ToString() + "'";

            int result = ado.ExecuteSqlNonQuery(sqlupdate);
            return result;
        }
        else
        {
            List<SqlParameter> plist = new List<SqlParameter>();
            string guid = Guid.NewGuid().ToString();
            plist.Add(new SqlParameter("@RecordId", guid));
            plist.Add(new SqlParameter("@RecordTime", DateTime.Now));
            plist.Add(new SqlParameter("@MemberId", MemberId));
            plist.Add(new SqlParameter("@GoodsId", goodsId));
            plist.Add(new SqlParameter("@RecordInfo", "任务学习记录"));
            plist.Add(new SqlParameter("@GoodsStudyTime", "20"));
            plist.Add(new SqlParameter("@TotalStudyTime", appendTime));
            plist.Add(new SqlParameter("@IfReachedTotalTime", "0"));
            plist.Add(new SqlParameter("@IfPassExamination", "0"));
            SqlParameter[] p = plist.ToArray();
            int result = StarTech.DBCommon.InsertData("T_Member_StudyRecord", p);
            return result;
        }
    }

    /// <summary>
    /// 更新总销售量
    /// </summary>
    /// <param name="goodsId"></param>
    /// <returns></returns>
    protected int UpdateTotalSaleCount(string goodsId)
    {
       return ado.ExecuteSqlNonQuery("update T_Goods_Info set TotalSaleCount=TotalSaleCount+1  where GoodsId='" + goodsId + "'");
    }

    /// <summary>
    /// 订购课程
    /// </summary>
    /// <param name="goodsId"></param>
    /// <returns></returns>
    protected string BuyCourse(string goodsId,string couponId)
    {
        //更新总销售量
        UpdateTotalSaleCount(goodsId);
        
        if (couponId == "")
        {
            //（重要）订单价格，优惠券使用条件判断
            DataTable dtPrice = ado.ExecuteSqlDataset("select SalePrice from T_Goods_Info where GoodsId='" + goodsId + "'").Tables[0];
            if (dtPrice.Rows.Count == 0) { return "error|数据异常"; }
            decimal OrderAllMoney = decimal.Parse(dtPrice.Rows[0]["SalePrice"].ToString());
            
            //自动获取优惠券，如果有多张，优选最高优惠的一张
            object objCouponId = ado.ExecuteSqlScalar("select top 1 CouponId from T_Member_Coupon where MemberId='" + this.memberId + "' and minPrice<=" + OrderAllMoney + " and isnull(IsUsed,0)=0 and EndTime>getdate() and CouponType='抵用券' order by CouponValue desc");
            couponId = objCouponId == null ? "" : objCouponId.ToString();
        }
        return AddOrder(this.memberId, goodsId, couponId);
    }


    /// <summary>
    /// 领取优惠券
    /// </summary>
    /// <param name="MemberId"></param>
    /// <returns></returns>
    protected int GetCoupon(string MemberId)
    {
        object objCouponId = ado.ExecuteSqlScalar("select CouponId from T_Member_Coupon where MemberId='" + MemberId + "' and isnull(IsUsed,0)=0 and EndTime>getdate() and CouponType='抵用券'");
        if (objCouponId != null) { return 2; }
        
        //config
        DataTable dtConfig = ado.ExecuteSqlDataset("select * from T_Base_Coupon where CouponType='抵用券' and StartTime<getdate() and EndTime>getdate() and isEffect=1 ").Tables[0];
        if (dtConfig.Rows.Count == 0) { return -1; }
        
        //add
        List<SqlParameter> plist = new List<SqlParameter>();
        string guid = Guid.NewGuid().ToString();
        string CouponId = MemberId + DateTime.Now.ToString("yyMMddHHmmss");
        int endDay = int.Parse(dtConfig.Rows[0]["CouponDay"].ToString());
        plist.Add(new SqlParameter("@Sysnumber", guid));
        plist.Add(new SqlParameter("@MemberId", MemberId));
        plist.Add(new SqlParameter("@CouponId", CouponId));
        plist.Add(new SqlParameter("@CouponType", "抵用券"));
        plist.Add(new SqlParameter("@CouponValue", dtConfig.Rows[0]["CouponValue"]));
        plist.Add(new SqlParameter("@StartTime", DateTime.Now));
        plist.Add(new SqlParameter("@EndTime", DateTime.Now.AddDays(endDay)));
        plist.Add(new SqlParameter("@IsUsed", "0"));
        plist.Add(new SqlParameter("@Remark", dtConfig.Rows[0]["Context"]));
        SqlParameter[] p = plist.ToArray();
        int result = StarTech.DBCommon.InsertData("T_Member_Coupon", p);
        return result;
    }


    /// <summary>
    /// 首页一键领取优惠券（批量领取）
    /// </summary>
    /// <param name="MemberId"></param>
    /// <returns></returns>
    protected int GetCouponIndexBat(string MemberId)
    {
        object objCouponId = ado.ExecuteSqlScalar("select CouponId from T_Member_Coupon where MemberId='" + MemberId + "' and GetPlaceInfo='首页一键领取'");
        if (objCouponId != null) { return 0; }

        //config
        DataTable dtConfig = ado.ExecuteSqlDataset("select * from T_Base_Coupon where GetPlaceInfo='首页一键领取' and CouponType='抵用券' and  isEffect=1 ").Tables[0];
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
            plist.Add(new SqlParameter("@GetPlaceInfo", "首页一键领取"));
            plist.Add(new SqlParameter("@minPrice", row["minPrice"]));
            plist.Add(new SqlParameter("@maxPrice", row["maxPrice"]));
            SqlParameter[] p = plist.ToArray();
            result += StarTech.DBCommon.InsertData("T_Member_Coupon", p);
            flag++;
        }
        return result;
    }
    
    /// <summary>
    /// 订单主表T_Order_Info
    /// </summary>
    protected string AddOrder(string MemberId, string GoodsId, string CouponId)
    {
        //判断重复下单
        if (ado.ExecuteSqlScalar("select orderId from T_Order_Info where  MemberId='" + MemberId + "' and IsPay=1 and orderId in (select orderId from T_Order_InfoDetail where [GoodsId]='" + GoodsId + "')") != null)
        {
            return "error|重复下单";
        }

        //读取报价表和其他信息
        DataTable dtPrice = ado.ExecuteSqlDataset("select * from T_Goods_Info where GoodsId='" + GoodsId + "'").Tables[0];
        if (dtPrice.Rows.Count == 0) { return "error|数据异常"; }
        decimal OrderAllMoney = decimal.Parse(dtPrice.Rows[0]["SalePrice"].ToString());     //（重要）订单价格
        string JobType = dtPrice.Rows[0]["JobType"].ToString();

        //会员表
        DataTable dtMember = ado.ExecuteSqlDataset("select * from T_Member_Info where MemberId='" + MemberId + "'").Tables[0];
        if (dtMember.Rows.Count == 0) { return "error|数据异常"; }
        string MemberName = dtMember.Rows[0]["MemberName"].ToString();
        string TrueName = dtMember.Rows[0]["TrueName"].ToString();
        string AddressCode = dtMember.Rows[0]["AddressCode"].ToString();
        string AddressDetail = dtMember.Rows[0]["AddressDetail"].ToString();
        string PostCode = dtMember.Rows[0]["PostCode"].ToString();

        //优惠券信息
        decimal TicketMoney = 0;
        if (CouponId != "")
        {
            object objCouponValue = ado.ExecuteSqlScalar("select CouponValue from T_Member_Coupon where MemberId='" + MemberId + "' and CouponId='" + CouponId + "' and isnull(IsUsed,0)=0 and EndTime>getdate()");
            TicketMoney = objCouponValue == null ? 0 : decimal.Parse(objCouponValue.ToString());
        }

        //订单主表（订单主信息，包括人员信息和价格信息等）
        ModOrder mod = new ModOrder();

        //订单基本信息
        mod.OrderId = DateTime.Now.ToString("yyyyMMddHHmmss") + MemberId + new Random().Next(1000, 9999).ToString();
        mod.OrderType = JobType;                                         //（重要）订单类型，和工作类型关联：全包（QB）、钟点工（DG）
        mod.OrderTime = DateTime.Now;
        mod.SendMethod = "off";
        mod.PayMethod = "在线支付";


        //雇员信息
        mod.MemberId = MemberId;                                        //（重要）雇员编号
        mod.MemberName = MemberName;
        mod.MemberOrderRemarks = "";                                    //订单备注（备用）
        mod.ReceivePerson = TrueName;
        mod.ReceiveAddressCode = AddressCode;
        mod.ReceiveAddressDetail = AddressDetail;
        mod.PostCode = PostCode;


        
        //优惠券信息
        mod.couponId = CouponId;                                        //（重要）优惠券编号
        mod.oldMoney = OrderAllMoney;                                   //（重要）订单原始价格（优惠券抵用前）
        mod.TicketMoney = TicketMoney;                                  //（重要）优惠券金额
        
        //价格信息
        mod.OrderAllMoney = OrderAllMoney - TicketMoney;                              //（重要）订单价格
        mod.GoodsAllMoney = OrderAllMoney - TicketMoney;
        mod.OrderAllWeight = 0;
        
        
        //支付信息
        mod.IsPay = 0;                                                  //（重要）是否支付
        mod.PayInterfaceCode = "";                                      //第三方支付接口提交的编号，查账用
        mod.PayMethod = "";                                             //支付方式（支付宝ZFB，微信WX等）
        mod.PayTime = DateTime.Parse("1900-01-01");                     //支付时间


        //创建订单
        if (new BllOrder().Add(mod) > 0)
        {
            //明细信息
            AddOrderDetail(mod.OrderId, mod.OrderType, GoodsId);
            
            //如果是组合课程再添加子课程
            DataTable dtSub = ado.ExecuteSqlDataset("select MorePropertys,DataFrom from T_Goods_Info where GoodsToTypeId = '" + GoodsId + "' and JobType='SubGoods' and IsSale=1").Tables[0];
            foreach (DataRow row in dtSub.Rows)
            {
                if (row["MorePropertys"].ToString() == "外部课程" && row["DataFrom"].ToString() != "")
                {
                    AddOrderDetail(mod.OrderId, mod.OrderType, row["DataFrom"].ToString());
                }
            }
        }

        return mod.OrderId;
    }


    /// <summary>
    /// 订单明细T_Order_InfoDetail
    /// </summary>
    protected int AddOrderDetail(string OrderId, string OrderType, string GoodsId)
    {
        DataTable dt = ado.ExecuteSqlDataset("select * from T_Goods_Info where GoodsId='" + GoodsId + "'").Tables[0];
        BllOrderDetail bllDetail = new BllOrderDetail();
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            ModOrderDetail mod = new ModOrderDetail();
            mod.OrderId = OrderId;
            mod.OrderType = OrderType;
            mod.GoodsId = row["GoodsId"].ToString();
            mod.GoodsName = row["GoodsName"].ToString();
            mod.GoodsCode = row["GoodsCode"].ToString();
            mod.GoodsFormate = "";
            mod.Price = decimal.Parse(row["SalePrice"].ToString());
            mod.GoodsPic = row["GoodsSmallPic"].ToString();
            mod.ProviderInfo = row["ProviderInfo"].ToString();
            mod.DataFrom = row["DataFrom"].ToString();
            mod.Quantity = 1;
            mod.AllMoney = mod.Price * mod.Quantity;
            mod.AllWeight = mod.OneWeight * mod.Quantity;
            i += bllDetail.Add(mod);
        }
        return i;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}