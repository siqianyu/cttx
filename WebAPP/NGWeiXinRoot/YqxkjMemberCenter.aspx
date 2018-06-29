<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjMemberCenter.aspx.cs" Inherits="NGWeiXinRoot_YqxkjMemberCenter" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧-我的会员中心</title>
	<link href="style/common.css?v=0310" rel="stylesheet">
	<link href="style/user.css?v=0310" rel="stylesheet">
	<link href="style/iconfont.css?v=0310" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
</head>
<body class="bottom-pd user-body">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_memberId" runat="server" />
	    <!--个人信息开始-->
		<div class="user-info left"  onclick="location.href='#'">
			<div class="icon-box"><img runat="server" id="img_header" src="images/banner.jpg"/></div>
			<p>
				<%=this.TrueName %><span>登录账号：<%=this.MemberName %></span>
			</p>
			<i class="icon iconfont icon-jiantou"></i>
		</div>
		<!--个人信息结束-->
		<!--菜单列表开始-->
		<ul class="user-box left">
			<li onclick="location.href='YqxkjMemberCourse.aspx'"><p><i class="icon iconfont icon-shu"></i><span>我的课程</span></p></li> 
		</ul>
		<ul class="user-box left">
			<li class="middle orange" onclick="location.href='YqxkjMemberCoupon.aspx'"><p><i class="icon iconfont icon-youhuiquan"></i><span>优惠券</span></p></li> 
             <li onclick="location.href='YqxkjNewsList2.aspx?dirId=<%=this.BZZX_CategoryID %>'"><p><i class="icon iconfont icon-custom-service"></i><span>客服咨询</span></p></li>
			
		</ul>
		<ul class="user-box left">
            <li onclick="location.href='YqxkjShareQRCode.aspx'"><p><i class="icon iconfont icon-msnui-sns"></i><span>我的分享</span></p></li>
            <li class="middle orange" onclick="location.href='YqxkjMemberCash.aspx'"><p><i class="icon iconfont icon-youhuiquan"></i><span>分享佣金</span></p></li>
			
		</ul>
		<!--菜单列表结束-->
		<!--底部菜单开始-->
		<uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
</body>
</html>
