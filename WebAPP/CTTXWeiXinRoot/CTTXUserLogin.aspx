<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTTXUserLogin.aspx.cs" Inherits="CTTXWeiXinRoot_CTTXUserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html charset=UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>登录--才通天下</title>
    <link href="style/common.css" rel="stylesheet" />
    <link href="style/login.css" rel="stylesheet" />
    <link href="style/animate.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
</head>
<body class="login-body">
    <form id="form1" runat="server">
        <asp:HiddenField ID="WXOpenId" runat="server" />
        <!--header start-->
        <div class="header">
            <a onclick="history.back()">
                <img alt="" src="img/return_icon.png" /></a>
            <span>登录</span>
        </div>
        <!--header end-->
        <!--login-content start-->
        <div class="login-content">
            <div class="login">
                <div class="login-item wow fadeInUp login-cell">
                    <span>
                        <i></i>
                        <input placeholder="请输入手机号" type="number" id="cell" />
                    </span>
                </div>
                <div class="login-item wow fadeInUp login-password" style="animation-delay: .1s; -webkit-animation-delay: .1s;">
                    <span>
                        <i></i>
                        <input placeholder="请输入密码" type="password" id='password' />
                    </span>
                </div>
                <div class="login-item wow fadeInUp" style="animation-delay: .4s; -webkit-animation-delay: .4s;">
                    <button class="login-btn" id="login-btn">登录</button>
                </div>
                <div class="login-item wow fadeInUp" style="animation-delay: .6s; -webkit-animation-delay: .6s;">
                    <button class="register-btn" id="reg-btn">注册</button>
                </div>
            </div>
        </div>
        <!--login-content end-->
        <!--错误提示-->
        <div class="cover" id="overlay">
            <p class="alert" id="Tip">错误提示</p>
        </div>
        <!--错误提示-->
    </form>
</body>
</html>
<script src="js/wow.min.js"></script>
<script type="text/javascript">
    if (!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
        new WOW().init();
    };
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#form1").removeAttr("action");
        var winHt = $(window).height();
        $('.login-content').css('height', winHt - 86);
        $("#reg-btn").on("click", function () {
            window.location.href = "CTTXUserReg.aspx?r=" + Math.random();
            return false;
        });
        $('#login-btn').on("click", function () {
            var OpenId = $("#WXOpenId").val();
            var CellNum = $('#cell').val();
            var Password = $('#password').val();
            if (!/^[1][3,4,5,7,8][0-9]{9}$/.test(CellNum)) {
                $('#overlay').show().delay(3000).fadeOut();
                $('#Tip').html('请输入正确的手机号！');
                return false;
            } else {
                $('#overlay').hide();
            }
            if (!/^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,21}$/.test(Password)) {
                $('#overlay').show().delay(3000).fadeOut();
                $('#Tip').html('密码需由6~20位数字和字母组成！');
                return false;
            } else {
                $('#overlay').hide();
            }
            //return false;
            //验证登陆
            $.ajax({
                type: "post",
                url: "/CTTXServerInterface/CTTXUser.ashx",
                data: { flag: "user_login", phone: CellNum, pwd: Password, wxopenid: OpenId },
                dataType: "json",
                async: true,
                beforeSend: function () {
                    $(this).text("登陆中");
                },
                success: function (data) {
                    if (data.status == 1) {
                        // alert("登陆成功了即将为你跳转");
                        window.location.href = "CTTXHome.aspx";
                    }
                    else {
                        $('#overlay').show().delay(3000).fadeOut();
                        $('#Tip').html(data.msg);
                        return false;
                    }
                },
                complete: function () {
                    $(this).text("登陆");
                }
            });
            return false;
        })
        //点击空白区域隐藏弹出框
        $('#overlay').click(function (e) {
            var target = $(e.target);
            if (!target.is('#register-btn') && !target.is('#Tip')) {
                if ($('#overlay').is(':visible')) {
                    $('#overlay').hide();
                }
            }
        });
    })
</script>
