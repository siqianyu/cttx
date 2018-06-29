<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjMemberCoupon.aspx.cs" Inherits="NGWeiXinRoot_YqxkjMemberCoupon" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧-我的优惠券</title>
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
		<!--待使用开始-->
		<p class="title left" style="background:#fff;"><i></i><b>待使用</b></p>
		<div class="coupon-box left">
			<ul>
            <asp:Repeater runat="server" ID="rptListUse">
            <ItemTemplate>
			 <li class="to-use">
			 	<p class="coupon-money">
			 		<span>￥<b><%#Eval("CouponValue")%></b> </span>
			 		<i>满<%#Eval("minPrice")%>可用</i>
			 	</p>
			 	<p class="coupon-info left">
			 		<span class="use"><%#Eval("Remark")%></span>
			 		<span class="use-btn">
			 			<i><%#Eval("StartTime", "{0:yyyy-MM-dd}").ToString()%>~<%#Eval("EndTime", "{0:yyyy-MM-dd}").ToString()%></i>
			 			<button onclick="location.href='YqxkjCourseList.aspx?dirId=1029';return false;">点击使用</button>
			 		</span>
			 	</p>
			 	<i class="attach"></i>
			 </li>
              </ItemTemplate>
             </asp:Repeater>
			 
			</ul>
		</div>
		<!--待使用结束-->

		<!--已失效开始-->
		<p class="title left" style="background:#fff;display:none;"><i></i><b>已失效</b></p>
		<div class="coupon-box left" style="display:none">
			<ul>
			 <li class="outof-date">
			 	<p class="coupon-money">
			 		<span>￥<b>100</b> </span>
			 		<i>满999可用</i>
			 	</p>
			 	<p class="coupon-info left">
			 		<span class="use">全部课程可用</span>
			 		<span class="use-btn">
			 			<i>2018.3.1~2018.3.15</i>
			 			<button>已失效</button>
			 		</span>
			 	</p>
			 	<i class="attach"></i>
			 </li>
			 <li class="outof-date">
			 	<p class="coupon-money">
			 		<span>￥<b>50</b> </span>
			 		<i>满99可用</i>
			 	</p>
			 	<p class="coupon-info left">
			 		<span class="use">全部课程可用</span>
			 		<span class="use-btn">
			 			<i>2018.3.1~2018.3.15</i>
			 			<button>已失效</button>
			 		</span>
			 	</p>
			 	<i class="attach"></i>
			 </li>
			</ul>
		</div>
		<!--已失效结束-->
		<!--底部菜单开始-->
		<uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
</body>
</html>
