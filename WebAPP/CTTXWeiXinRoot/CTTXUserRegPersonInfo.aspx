<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTTXUserRegPersonInfo.aspx.cs" Inherits="CTTXWeiXinRoot_CTTXUserRegPersonInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>个人信息--才通天下</title>
    <link href="style/common.css" rel="stylesheet" />
    <link href="style/login.css" rel="stylesheet" />
    <link href="style/animate.min.css" rel="stylesheet" />
    <script src="http://libs.baidu.com/jquery/1.10.2/jquery.min.js"></script>
    <script src="js/radio.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidHeadImg" runat="server" />
        <!--header start-->
        <div class="header">
            <a onclick="history.back()">
                <img alt="" src="img/return_icon.png" /></a>
            <span>个人信息</span>
        </div>
        <!--header end-->
        <!--icon-bar start-->
        <div class="icon-bar wow fadeInDown">
            <div class="icon-box">
                <img  style="width: 120px; height: 120px; border-radius: 50%" src="<%=this.HeadImg%>" />
                <input type="button" id="prevIcon" style="position: absolute; left: 0; top: 0" />
            </div>
        </div>
        <!--icon-bar end-->
        <!--personal start-->
        <div class="personal left wow fadeInDown">
            <div class="personal-item left">
                <b>姓名</b><span><input placeholder="请输入姓名" value="<%=Session["wx_nickname"]%>" id="trueName" /></span>
            </div>
            <div class="personal-item left">
                <b>性别</b>
                <span id="sex">
                    <label class="radio-blue right" style="margin-left: .8rem;">
                        <i></i>
                        <input type="radio" name="role" value="女" />女
                    </label>
                    <label class="radio-blue right">
                        <i class="checked"></i>
                        <input type="radio" name="role" value="男" checked="checked" />男
                    </label>
                </span>
            </div>
            <div class="personal-item left">
                <b>出生日期</b><span><input id="birthDay" placeholder="请选择出生日期" value="1989-01-01" type="date" style="width: auto" /></span>
            </div>
            <div class="personal-item left">
                <b>职历</b>
                <span style="margin-right: 2rem; height: 25px; line-height: 25px; overflow: hidden;" onclick="location.href='CTTXUserCareer.aspx'">职历,求职术语,简约地说,就是作为社会的人一生或一个阶段的职业经历和历史。职历的深浅,是一个人素质因岁月因经验因后天的努力与成熟而形成的特</span>
                <a></a>
            </div>
            <div class="personal-item left">
                <b>我的画像</b>
                <span style="margin-right: 2rem;" onclick="location.href='CTTXUserPersonTag.aspx'">幽默，专业</span>
                <a></a>
            </div>
        </div>
        <div class="login-item wow fadeInUp" style="animation-delay: .4s; -webkit-animation-delay: .4s; margin-top: 3rem">
            <button class="login-btn" id='register-btn'>完成提交</button>
        </div>
        <!--personal end-->
        <div class="prev-icon" id="overlay">
            <div>
                <p>
                    <img src="<%=Session["wx_headimgurl"]%>" />
                </p>
            </div>
        </div>
        <!--错误提示-->
        <div class="cover" id="error">
            <p class="alert" id="Tip">错误提示</p>
        </div>
        <!--错误提示-->
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
        $("#form1").removeAttr("action");
        var winHt = $(window).height();
        $('.login-content').css('height', winHt - 86);
        $('#prevIcon').click(function () {
            $('#overlay').show();
        });
        //点击空白区域隐藏弹出框
        $('#overlay').click(function (e) {
            var target = $(e.target);
            if (!target.is('#overlay img')) {
                if ($('#overlay').is(':visible')) {
                    $('#overlay').hide();
                }
            }
        });
        $("#register-btn").on("click", function () {
            var txtMemberId = "<%=this.MemberId%>";
            var txtHeadImg = $("#hidHeadImg").val();
            var txtTrueName = $("#trueName").val();
            var txtSex = $("#sex input[type='radio']:checked").val();
            var txtBirthDay = $("#birthDay").val();
            if (!txtTrueName) {
                $('#error').show().delay(3000).fadeOut();
                $("#Tip").html("请填写姓名");
                $("#trueName").focus();
                return false;
            }
            else { $('#error').hide(); }
            var reg = /^(19|20)\d{2}-(1[0-2]|0?[1-9])-(0?[1-9]|[1-2][0-9]|3[0-1])$/;
            if (!reg.test(txtBirthDay)) {
                $('#error').show().delay(3000).fadeOut();
                $("#Tip").html("生日格式不正确");
                return false;
            }
            else { $('#error').hide(); }
            //修改基础信息
            if (confirm("是否确认提交")) {
                $.ajax({
                    type: "post",
                    url: "/CTTXServerInterface/CTTXUser.ashx",
                    data: { flag: "user_changeInfo", memberId: txtMemberId, trueName: txtTrueName, sex: txtSex, birthDay: txtBirthDay },
                    dataType: "json",
                    async: false,
                    beforeSend: function () {
                    },
                    success: function (data) {
                        if (data.status == 1) {
                            window.location.href = "CTTXHome.aspx";
                        }
                        else {
                            alert(data.msg);
                        }
                    },
                    complete: function () {
                        //console.log("提交完成......");
                    }
                });
            }
            return false;
        })

    })
</script>
