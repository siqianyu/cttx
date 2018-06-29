<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjMemberCourseDetail.aspx.cs" Inherits="NGWeiXinRoot_YqxkjMemberCourseDetail" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧-主页</title>
	<link href="style/common.css?v=0410" rel="stylesheet">
	<link href="style/index.css?v=0410" rel="stylesheet">
	<link href="style/iconfont.css?v=0410" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $(".course-slide span").each(function (i) {
                $(this).click(function () {

                    $(this).addClass("active").siblings().removeClass("active");
                    $(".course-info:eq(" + i + ")").show().siblings(".course-info").hide();
                    var l = $(this).offset().left;
                    var w = $(this).width();
                    //	  alert(l);
                    $(".course-slide i").css("left", l + w / 2.2)
                })
            })
        }) 
</script>
</head>
<body class="bottom-pd">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_goodsId" runat="server" />
    <asp:HiddenField ID="hidMemberId" runat="server" />
    
		<div class="banner" style="display:none"><img src="images/banner2.jpg"></div>
		<!--课程简介开始-->
		<ul class="course-list left course-info-top">
			<li>
				<div class="course-img left"><img src="" runat="server" id="img_goods"></div>
				<p>
					<b runat="server" id="span_name"></b>
					<span>任务：<i class="update" runat="server" id="span_taskcount">0</i> 个</span>
					<span>习题：<i class="update" runat="server" id="span_questioncout">0</i> 道</span>
					<span>学员：<i class="update" runat="server" id="span_totalsale">0</i> 名</span>
					<span style="display:none">价格：<i class="update" runat="server" id="span_price">0</i> 元</span>
				</p>
				<button class="consult-btn" style="display:none">咨询</button><button class="buy-btn" onclick="return buy_course()" style="display:none">购买</button>
			</li>
		</ul>
		<!--课程简介结束-->
		<!--页签开始-->
		<div class="course-slide left"><p>
			<span>课程简介</span><span class="active">课程目录</span><span>课程评价</span>
			<i style="left:48%"></i>
		</p></div>
		<!--页签结束-->
		<!--课程简介开始-->
		<div class="course-info" style="display: none;">
			<div class="course-introduct" id="div_goodsdesc" runat="server">

			</div>
		</div>
		<!--课程简介结束-->
		<!--课程目录开始-->
		<div class="course-info">
			<ul class="chapter-list" id="ul_list" runat="server">
				<%=this.BindTask()%>
			</ul>
		</div>
		<!--课程目录结束-->
		<!--课程评价开始-->
		<div class="course-info" style="display: none;">
			<p class="evaluate-title left">评价（<asp:Literal runat="server" ID="ltCommnetCount"></asp:Literal>条）</p>
			<ul class="evaluate-list" runat="server" id="ul_commnet_list">
			</ul>
		</div>
		<!--课程评价结束-->
		<!--底部菜单开始-->
		<uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        //设置课程列表里面图片长款比例为：10:6
        var item = $('.course-img');
        var wd = item.width();
        item.height(wd * .6);
        $("#div_goodsdesc img").css("width","100%");
    })
</script>
<script language="javascript" type="text/javascript">
    function buy_course(id) {
        if ($("#hidMemberId").val() == "") { layer.msg("请先登录"); return false; }
        layer.load(2); //加载时显示加载效果
        $.ajax({
            type: "get",
            url: "YqxkjMemberInterface.ashx",
            data: { flag: "buy_course", goodsid: $("#hid_goodsId").val() },
            dataType: "text",
            async: true,
            success: function (data) {
                layer.closeAll('loading'); //关闭所有加载效果
                if (data.indexOf('error') > -1) { layer.msg(data); }
                else {
                    layer.msg("订购成功，前往支付界面..."); 
                    to_paypage(data);
                }
            }
        });
        return false;
    }

    function to_paypage(orderId) {
        location.href = "../PayInterface/WXPpay/ProductPage.aspx?out_trade_no=" + orderId + "&r=" + Math.random();
    }
</script>