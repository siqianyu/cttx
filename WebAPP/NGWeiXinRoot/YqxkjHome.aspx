<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjHome.aspx.cs" Inherits="NGWeiXinRoot_YqxkjHome" %>
<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧</title>
	<link href="style/common.css?v=0330" rel="stylesheet">
	<link href="style/index.css?v=0330" rel="stylesheet">
	<link href="style/iconfont.css?v=0330" rel="stylesheet" />
        <script src="js/jquery.min.js"></script>
		<script src="js/iconfont.js"></script>
		<script src="js/banner.js"></script>
    <script src="Js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    
</head>
<body class="bottom-pd">
    <form id="form1" runat="server">
    <asp:HiddenField ID="WXOpenId" runat="server" />
		<!--banner开始-->
		<div class="banner">
			<div class="new_banner">
    			<ul class="rslides f426x240">		
       				<%=this.ListAD()%>
    			</ul>
			</div>
		</div>
		<!--banner结束-->
		<!--课程分类开始-->
		<ul class="course-nav left">
			<li class="nav1">
            <%--<a href="YqxkjCourseList.aspx?dirId=<%=this.KJJC_ID %>">--%>
                <a href="/NGWeiXinRoot/News/NewsList.aspx">
				<b><i class="icon iconfont icon-jisuanqi"></i></b><span>会计基础</span>
			</a></li>
			<li class="nav2"><a href="YqxkjCourseList.aspx?dirId=<%=this.SFKS_ID %>">
				<b><i class="icon iconfont icon-sifakaoshi"></i></b><span>职称考试</span>
			</a></li>
			<li class="nav3">
            <%--<a href="YqxkjCourseList.aspx?dirId=<%=this.SCKC_ID %>">--%>
                <a href="/NGWeiXinRoot/News/NewsList.aspx">
				<b><i class="icon iconfont icon-kechengbiao"></i></b><span>实操课程</span>
			</a></li>
			<li class="nav4">
            <%--<a href="YqxkjCourseList.aspx?dirId=<%=this.GLKJ_ID %>">--%>
                <a href="/NGWeiXinRoot/News/NewsList.aspx">
				<b><i class="icon iconfont icon-guanli"></i></b><span>管理会计</span>
			</a></li>
			<li class="nav5"><a href="YqxkjNewsList.aspx?dirId=<%=this.CWZX_ID %>">
				<b><i class="icon iconfont icon-caiwuguanli"></i></b><span>财务咨询</span>
			</a></li>
			<li class="nav6"><a href="YqxkjNewsList.aspx?dirId=<%=this.GLZX_ID %>">
				<b><i class="icon iconfont icon-guanli1"></i></b><span>管理咨询</span>
			</a></li>
			<li class="nav7">
            <%--<a href="YqxkjCourseList.aspx?dirId=<%=this.SWKJ_ID %>">--%>
                <a href="/NGWeiXinRoot/News/NewsList.aspx">
				<b><i class="icon iconfont icon-icon-test"></i></b><span>税务会计</span>
			</a></li>
			<li class="nav8"><a href="#">
				<b><i class="icon iconfont icon-dianpu1"></i></b><span>微 店</span>
			</a></li>
		</ul>
		<!--课程分类结束-->
		<!--推荐课程开始-->
		<div class="recommend-course left">
			<p class="title"><i></i><b>推荐课程</b><a>换一批</a></p>
			<ul id="courseList">
				<%=this.TopCourse()%>
			</ul>
		</div>
		<!--推荐课程结束-->
        <div class="banner banner1" onclick="location.href='YqxkjCourseList.aspx'"><img src="images/ad.png"></div>
		<div class="banner banner1" onclick="get_coupon_index()"><img src="images/banner1.jpg"></div>
		<!--帮助中心开始-->
		<div class="help-bar left"><ul>
			<li class="help1">
				<p class="help-title">帮助中心</p>
				<img src="images/arrow.png" class="help-arrow" />
				<b class="shadow"></b>
				<span>
					<%=this.ListHelp(this.BZZX_CategoryID)%>
				</span>
			</li>
			<li class="help2">
				<p class="help-title">客服&售后</p>
				<img src="images/arrow.png" class="help-arrow" />
				<b class="shadow"></b>
				<span>
					<%=this.ListHelp(this.KFSH_CategoryID)%>
				</span>
			</li>
			<li class="help3">
				<p class="help-title">商务合作</p>
				<img src="images/arrow.png" class="help-arrow" />
				<b class="shadow"></b>
				<span>
					<%=this.ListHelp(this.SWHZ_CategoryID)%>
				</span>
			</li>
		</ul></div>
		<!--帮助中心结束-->
		<!--底部菜单开始-->
        <uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        //设置推荐课程里面图片长款比例为：10:6
        var item = $('#courseList li b');
        var wd = item.width();
        item.height(wd * .6);
    })
</script>
<script language="javascript">
    //一键领取优惠券
    function get_coupon_index() {
        $.ajax({
            type: "post",
            url: "YqxkjMemberInterface.ashx",
            data: { flag: "get_coupon_indexbat" },
            dataType: "text",
            async: true,
            success: function (data) {
                if (data == '0') {
                    layer.msg("已经领取，不能重复领取"); return false;
                }
                else if (data == '-1') {
                    layer.msg("领取失败"); return false;
                } 
                else if (data == '无权访问') {
                    layer.msg("请先登录"); return false;
                }
                else {
                    layer.msg("领取成功"); return false;
                }
            }
        });
    }
</script>