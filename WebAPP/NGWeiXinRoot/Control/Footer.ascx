<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="NGWeiXinRoot_Control_Footer" %>
		<ul class="footer-menu">
			<li id="li_footer_index" class="current" onclick="location.href='YqxkjHome.aspx'">
				<b><i class="icon iconfont icon-shouye"></i></b><span>首页</span>
			</li>
			<li id="li_footer_list" onclick="location.href='YqxkjCourseList.aspx'">
				<b><i class="icon iconfont icon-fenlei"></i></b><span>全部课程</span>
			</li>
			<li id="li_footer_my" onclick="location.href='YqxkjMemberCenter.aspx'">
				<b><i class="icon iconfont icon-user"></i></b><span>我的</span>
			</li>
		</ul>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        var url = location.href;
        if (url.indexOf("YqxkjMember") > -1) {
            $("#li_footer_my").addClass('current').siblings('li').removeClass('current');
        } else if (url.indexOf("YqxkjHome") > -1) {
            $("#li_footer_index").addClass('current').siblings('li').removeClass('current');
        } else {
            $("#li_footer_list").addClass('current').siblings('li').removeClass('current');
        }
    });
</script>