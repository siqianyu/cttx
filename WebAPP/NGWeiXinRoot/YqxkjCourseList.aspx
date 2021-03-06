﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjCourseList.aspx.cs" Inherits="NGWeiXinRoot_YqxkjCourseList" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧-主页</title>
	<link href="style/common.css?v=0310" rel="stylesheet">
	<link href="style/index.css?v=0310" rel="stylesheet">
	<link href="style/iconfont.css?v=0310" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
</head>
<body class="bottom-pd">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_dirId" runat="server" />
		<div class="banner"><img src="images/banner2.jpg"></div>
		<!--课程分类开始-->
		<ul class="course-class left" id="ul_subclass">

		</ul>
		<!--课程分类结束-->
		<!--课程列表开始-->
		<ul class="course-list left" id='courseList'>
			
		</ul>
		<!--课程列表结束-->
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

    })
</script>
<script language="javascript" type="text/javascript">

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
            data: { flag: "list_course", dirid: $("#hid_dirId").val(), r: Math.random() },
            dataType: "text",
            async: true,
            success: function (data) {
                layer.closeAll('loading'); //关闭所有加载效果
                $("#courseList").html(data);
            }
        });
    }
    list_course();
    </script>