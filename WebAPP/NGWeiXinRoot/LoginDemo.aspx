<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginDemo.aspx.cs" Inherits="AppClient_Tools_LoginDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="Js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <!--用户登录start-->
    <script language="javascript" type="text/javascript">
        function user_login() {
            var _txtMobile = $.trim($("#txtMobile").val());
            var _txtPwd = $.trim($("#txtPwd").val());
            if (_txtMobile == "" || _txtMobile.length != 11) { layer.msg("请输入手机号"); return false; }
            if (_txtPwd == "") { layer.msg("请输入密码"); return false; }
           
            layer.load(2); //加载时显示加载效果
            $("#btnLogin").hide();
            $.ajax({
                type: "post",
                url: "LoginDemo.ashx",
                data: { flag: "user_login", phone: _txtMobile, pwd: _txtPwd },
                dataType: "text",
                async: true,
                success: function (data) {
                    layer.closeAll('loading'); //关闭所有加载效果
                    $("#btnLogin").show();
                    if (data == '1') {
                        location.href = "YqxkjMemberCenter.aspx";
                    } else {
                        alert(data);
                    }
                }
            });
        }
    </script>
    <!--用户登录end-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
    手机：<input id="txtMobile" type="text" />
    密码：<input id="txtPwd" type="password"/>
    <input id="btnLogin" type="button" value="登录" onclick="user_login()" />
    </div>
    </form>
</body>
</html>
