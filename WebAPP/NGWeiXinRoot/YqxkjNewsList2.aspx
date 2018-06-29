<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjNewsList2.aspx.cs" Inherits="NGWeiXinRoot_YqxkjNewsList2" %>

<%@ Register src="Control/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>一起学会计吧</title>
	<link href="style/common.css?v=0313" rel="stylesheet">
	<link href="style/user.css?v=0313" rel="stylesheet">
	<link href="style/iconfont.css?v=0313" rel="stylesheet" />
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/iconfont.js"></script>
    <script src="js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
</head>
<body class="bottom-pd  list-body">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_dirId" runat="server" />
		<p class="title" style="background: #f8f9fb;"><i></i><b><%=this.dirName%></b></p>
		<!--课程列表开始-->
        <ul class="words-list left" id='courseList'>
		 	
		 </ul>
		<!--课程列表结束-->
		<!--底部菜单开始-->
		<uc1:Footer ID="Footer1" runat="server" />
		<!--底部菜单结束-->
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    function list_news2() {
        layer.load(2); //加载时显示加载效果
        $.ajax({
            type: "get",
            url: "YqxkjCourseInterface.ashx",
            data: { flag: "list_news2", dirid: $("#hid_dirId").val() },
            dataType: "text",
            async: true,
            success: function (data) {
                layer.closeAll('loading'); //关闭所有加载效果
                $("#courseList").html(data);
            }
        });
    }
    list_news2();
    </script>