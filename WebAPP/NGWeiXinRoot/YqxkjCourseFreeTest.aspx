<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjCourseFreeTest.aspx.cs" Inherits="NGWeiXinRoot_YqxkjCourseFreeTest" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧</title>
	<link href="style/common.css?v=0310" rel="stylesheet">
	<link href="style/index.css?v=0310" rel="stylesheet">
	<link href="style/iconfont.css?v=0310" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <script src="js/video.js" type="text/javascript"></script>
    <link href="js/video-js.css" rel="stylesheet" type="text/css" />
    <script>
        videojs.options.flash.swf = "video-js.swf";
    </script>
</head>
<body class="bottom-pd">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidMemberId" runat="server" />
    <asp:HiddenField ID="hid_goodsId" runat="server" />
    <asp:HiddenField runat="server" ID="hidVideoPath" />
    <asp:HiddenField runat="server" ID="hidVideoPic" />
    <asp:HiddenField runat="server" ID="hidGoodsId" />
    <asp:HiddenField runat="server" ID="hidMorePropertys" />
		<%--<div class="banner">
        
        </div>--%>
		<!--课程简介开始-->
<%--		<ul class="course-list left course-info-top">
			<li style="display:none">
				<div class="course-img left"><img src="" runat="server" id="img_goods"></div>
				<p>
					<b>会计入门基础</b>
					<span>任务：<i class="update" runat="server" id="span_taskcount">0</i> 个</span>
					<span>习题：<i class="update" runat="server" id="span_questioncout">0</i> 道</span>
					<span>学员：<i class="update" runat="server" id="span_totalsale">0</i> 名</span>
					<span>价格：<i class="update" runat="server" id="span_price">0</i> 元</span>
				</p>
				<button class="consult-btn">咨询</button><button class="buy-btn">购买</button>
			</li>
		</ul>--%>
		<!--课程简介结束-->

        <div class="video left" id="div_video_panel">
			<!--<video  id="example_video_1" class="video-js" webkit-playsinline="" playsinline="" x5-playsinline="" x-webkit-airplay="allow"
				width="100%" height="100%" src="http://player.youku.com/player.php/sid/XMzQ0MDE5MDYyNA==/v.swf" preload="true">
			</video>-->
			<%--<iframe height=auto width=100% src='http://player.youku.com/embed/XMzQ0MDE5MDYyNA==' frameborder=0 'allowfullscreen'></iframe>--%>
            <!--视频播放start-->
        <video id="index_video" class="video-js vjs-default-skin vjs-big-play-centered" controls
            preload="auto" width="100%"  poster="<%=this.pic %>" data-setup="{}">
                        <source src="<%=this.path %>" type='video/mp4' />
                        <source src="<%=this.path %>" type='video/webm' />
                        <!--<source src="index_video.ogv" type='video/ogg' />-->
                    </video>
        <script type="text/javascript">
            var myPlayer = videojs('index_video');
            videojs("index_video").ready(function () {
                var myPlayer = this;
                myPlayer.play();
            });
        </script>
        <!--视频播放end-->

		</div>
		<P class="lesson-info left" id="div_goodsdesc" runat="server">
			--
		</P>
        <!--课后练习开始-->
		<div class="exercise left" id="div_khlx" style="display:none">
			<p class="title"><i></i><b>课后练习</b></p>
			<p class="exercise-info left" id="div_computer1">
				--
			</p>
			<button class="take-exercise" onclick="return to_question();">练习</button>
		</div>
		<!--课后练习结束-->
        <!--模拟试题开始-->
		<div class="exercise left" id="div_mnst" style="display:none">
			<p class="title"><i></i><b>模拟练习</b></p>
			<p class="exercise-info left" id="div_computer2">
				--
			</p>
			<button class="take-exercise" onclick="return to_question();">答题</button>
		</div>
		<!--模拟试题结束-->
		<!--课程评价开始-->
		<div class="course-info">
			<p class="evaluate-title left">评价（<asp:Literal runat="server" ID="ltCommnetCount"></asp:Literal>条）<a id="makeCommentBtn" style="display:none"><i class="icon iconfont icon-comment"></i>评论</a></p>
			<ul class="evaluate-list" runat="server" id="ul_commnet_list">
			</ul>
		</div>
		<!--课程评价结束-->
		<!--底部菜单开始-->
		<uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
    	<!--弹窗开始-->
		<div class="cover" id="makeComment">
			<div class="model">
				<p class="write-comment">
					<b>评分：</b>
					<i class="icon iconfont icon-pingfen on"></i><i class="icon iconfont icon-pingfen on"></i>
					<i class="icon iconfont icon-pingfen on"></i><i class="icon iconfont icon-pingfen on"></i>
					<i class="icon iconfont icon-pingfen on"></i>
				</p>
				<p class="write-comment">
					<b>评论：</b>
					<textarea>挺好的</textarea>
				</p>
				<p class="btn-bar">
					<button class="submit" id="submitComment">确定</button>
					<button class="cancel" id="cancelComment">取消</button>
				</p>
			</div>
		</div>
		<!--弹窗结束-->
</body>
</html>
<%--<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        //设置课程列表里面图片长款比例为：10:6
        var item = $('.course-img');
        var wd = item.width();
        item.height(wd * .6);
        $("#div_goodsdesc img").css("width","100%");
    })
</script>--%>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        var ht = $(window).Height;
        $('.cover').Height = ht;

        $('#makeCommentBtn').click(function () {
            $('#makeComment').show();
            $('body').css('position', 'fixed');
        })
        $('#submitComment').click(function () {
            $('#makeComment').hide();
            $('body').css('position', 'relative');
        })
        $('#cancelComment').click(function () {
            $('#makeComment').hide();
            $('body').css('position', 'relative');
        })

    })
</script>
<script language="javascript" type="text/javascript">
    $(function () {
        /*
        * 鼠标点击，该元素包括该元素之前的元素获得样式,并给隐藏域input赋值
        * 鼠标移入，样式随鼠标移动
        * 鼠标移出，样式移除但被鼠标点击的该元素和之前的元素样式不变
        * 每次触发事件，移除所有样式，并重新获得样式
        * */
        var stars = $('.write-comment');
        var Len = stars.length;
        //遍历每个评分的容器
        for (i = 0; i < Len; i++) {
            //每次触发事件，清除该项父容器下所有子元素的样式所有样式
            function clearAll(obj) {
                obj.parent().children('i').removeClass('on');
            }
            stars.eq(i).find('i').click(function () {
                var num = $(this).index();
                clearAll($(this));
                //当前包括前面的元素都加上样式
                $(this).addClass('on').prevAll('i').addClass('on');

            });
            stars.eq(i).find('i').mouseover(function () {
                var num = $(this).index();
                clearAll($(this));
                //当前包括前面的元素都加上样式
                $(this).addClass('on').prevAll('i').addClass('on');
            });
        }
    })
</script>

<script language="javascript" type="text/javascript">
    function to_question() {
        var mid = $("#hidMemberId").val();
        if ($("#hidMorePropertys").val() == "模拟试题") {
            location.href = 'YqxkjQuestionMN.aspx?memberId=' + mid + '&fromfree=free&source=mnlx&courseId=' + $("#hid_goodsId").val() + '&r=' + Math.random();
        } else {
            location.href = 'YqxkjQuestion.aspx?memberId=' + mid + '&fromfree=free&source=khlx&courseId=' + $("#hid_goodsId").val() + '&r=' + Math.random();
        }
        return false;
    }

    function list_subclass() {
        layer.load(2); //加载时显示加载效果
        $.ajax({
            type: "get",
            url: "YqxkjCourseInterface.ashx",
            data: { flag: "list_subclass", dirid: $("#hid_dirId").val() },
            dataType: "text",
            async: true,
            success: function (data) {
                layer.closeAll('loading'); //关闭所有加载效果
                $("#ul_subclass").html(data);
                if ($("#ul_subclass").text() == "") { $("#ul_subclass").hide(); }
            }
        });
    }
    list_subclass();

    function list_course() {
        layer.load(2); //加载时显示加载效果
        $.ajax({
            type: "get",
            url: "YqxkjCourseInterface.ashx",
            data: { flag: "list_course", dirid: $("#hid_dirId").val() },
            dataType: "text",
            async: true,
            success: function (data) {
                layer.closeAll('loading'); //关闭所有加载效果
                $("#courseList").html(data);
            }
        });
    }
    list_course();

    function to_select_questions() {
        if ($("#hidMorePropertys").val() == "模拟试题") {
            $("#div_mnst").show();
            $("#div_video_panel").hide(); 
        } else {
            $("#div_khlx").show();
        }
    }
    to_select_questions();

    //得分计算
    function computer_record() {
        var mytype = "";
        htmlobj = $.ajax({ url: "YqxkjCourseInterface.ashx?flag=computer_record&courseId=" + $("#hid_goodsId").val() + "&mid=" + $("#hidMemberId").val() + "&mytype=" + escape(mytype) + "", async: false });
        var str = htmlobj.responseText;
        //return str;

        var info = "答对<b>" + str.split('|')[1] + "</b>题，共<b>" + str.split('|')[0] + "</b>题，正确率<b>" + str.split('|')[2] + "%</b>";
        $("#div_computer1").html(info);
        $("#div_computer2").html(info);
    }
    computer_record();
    </script>