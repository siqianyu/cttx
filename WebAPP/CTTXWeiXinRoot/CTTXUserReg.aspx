<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTTXUserReg.aspx.cs" Inherits="NGWeiXinRoot_CTTXUserReg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html charset=UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>注册--才通天下</title>
    <link href="style/common.css" rel="stylesheet" />
    <link href="style/login.css" rel="stylesheet" />
    <link href="style/animate.min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://libs.baidu.com/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" src="js/radio.js"></script>
</head>
<body class="login-body">
    <form id="form1" runat="server">
        <asp:HiddenField ID="WXOpenId" runat="server" />
        <asp:HiddenField ID="HeadImg" runat="server" />
        <!--header start-->
        <div class="header">
            <a onclick="history.back()">
                <img alt="" src="img/return_icon.png" /></a>
            <span>注册</span>
        </div>
        <!--header end-->
        <!--login-content start-->
        <div class="login-content">
            <div class="login">
                <div class="login-item wow fadeInUp login-cell">
                    <span>
                        <i></i>
                        <input placeholder="请输入手机号" type="number" id='cell' />
                    </span>
                </div>
                <div class="login-item wow fadeInUp login-cell" style="animation-delay: .1s; -webkit-animation-delay: .1s;">
                    <span>
                        <i></i>
                        <input placeholder="请输入验证码" type="number" id="code" />
                    </span>
                    <button class="getcode" id="get-code">获取验证码</button>
                </div>
                <div class="login-item wow fadeInUp login-password" style="animation-delay: .3s; -webkit-animation-delay: .3s;">
                    <span>
                        <i></i>
                        <input placeholder="请输入密码" type="password" id='password' />
                    </span>
                </div>
                <div class="login-item wow fadeInUp login-password" style="animation-delay: .4s; -webkit-animation-delay: .4s;">
                    <span>
                        <i></i>
                        <input placeholder="请再次输入密码" type="password" id='re-password' />
                    </span>
                </div>
                <div class="login-role wow fadeInUp" style="animation-delay: .5s; -webkit-animation-delay: .5s;">
                    <label class="radio-check left">
                        <i class="checked"></i>
                        <input type="radio" checked="checked" name="role" value="0" />我是学员
                    </label>
                    <label class="radio-check right">
                        <i></i>
                        <input type="radio" name="role" value="1" />我是观众
                    </label>
                </div>
                <div class="login-item wow fadeInUp" style="animation-delay: .6s; -webkit-animation-delay: .6s;">
                    <button class="login-btn" id='register-btn'>注册</button>
                </div>
                <a class="wow fadeInUp disclaimer" style="animation-delay: .8s; -webkit-animation-delay: .8s;">免责条款</a>
            </div>
        </div>
        <!--login-content end-->
        <div class="cover" id="overlay">
            <p class="alert" id="Tip"></p>
        </div>
    </form>
</body>
</html>
<script type="text/javascript" src="js/wow.min.js"></script>
<script type="text/javascript">
    if (!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
        new WOW().init();
    };
</script>
<script type="text/javascript">
    $(document).ready(function () {
        var winHt = $(window).height();
        $('.login-content').css('height', winHt - 86);
    })
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#form1").removeAttr("action");
        $('#register-btn').click(function () {
            var OpenId = $("#WXOpenId").val();
            var HeadImg = $("#HeadImg").val();
            var CellNum = $('#cell').val();
            var Password = $('#password').val();
            var RePassword = $('#re-password').val();
            var Code = $('#code').val();
            var MemberFlag = $("input[type='radio']:checked").val();
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
            if (RePassword != '' && RePassword != Password) {
                $('#overlay').show().delay(3000).fadeOut();
                $('#Tip').html('两次输入密码不一致！');
                return false;
            } else {
                $('#overlay').hide();
            }
            if (Code == "") {
                $('#overlay').show().delay(3000).fadeOut();
                $('#Tip').html('验证码不能为空！');
                return false;
            }
            else {
                $('#overlay').hide();
            }
            //注册
            $.ajax({
                type: "post",
                url: "/CTTXServerInterface/CTTXUser.ashx",
                data: { flag: "user_reg", action: "reg", phone: CellNum, pwd: Password, code: Code, wxopenid: OpenId, headImg: HeadImg, memberFlag: MemberFlag },
                dataType: "json",
                async: true,
                beforeSend: function () {
                    //console.log("提交中......");
                },
                success: function (data) {
                    if (data.status == 1) {
                        //alert("注册成功并跳转");
                        window.location.href = "CTTXUserRegPersonInfo.aspx";
                    }
                    else {
                        $('#overlay').show().delay(3000).fadeOut();
                        $('#Tip').html(data.msg);
                    }
                    return false;
                },
                complete: function () {
                    //console.log("提交完成......");
                }
            });
            return false;
        });
        //点击空白区域隐藏弹出框
        $('#overlay').click(function (e) {
            var target = $(e.target);
            if (!target.is('#register-btn') && !target.is('#Tip')) {
                if ($('#overlay').is(':visible')) {
                    $('#overlay').hide();
                }
            }
        });
        $('#get-code').on("click", function () {
            var CellNum = $('#cell').val();
            if (!/^[1][3,4,5,7,8][0-9]{9}$/.test(CellNum)) {
                $('#overlay').show().delay(3000).fadeOut();
                $('#Tip').html('请输入正确的手机号！');
                return false;
            } else {
                $('#overlay').hide();
            }
            //发送验证码
            $.ajax({
                type: "post",
                url: "/CTTXServerInterface/CTTXUser.ashx",
                data: { flag: "user_reg", action: "sendcode", phone: CellNum },
                dataType: "json",
                async: true,
                beforeSend: function () {
                    //console.log("开始发送......");
                },
                success: function (data) {
                    if (data.status == 1) {
                        TimeDown();
                    }
                    else {
                        $('#overlay').show().delay(3000).fadeOut();
                        $('#Tip').html(data.msg);
                    }
                    return false;
                },
                complete: function () {
                    // console.log("发送完成......");
                }
            });
            return false;
        })
        return false;
    })

    //倒计时
    function TimeDown() {
        var time_num = 60;
        var timer = setInterval(function () {
            if (time_num > 0) {
                $('#get-code').html(time_num + "秒后重新发送").attr("disabled", "disabled").addClass('getcodeA');
                time_num--;
            } else {
                clearInterval(timer);
                $('#get-code').html("免费获取验证码").removeAttr("disabled").removeClass('getcodeA');
            }
        }, 1000)
    }
</script>
