<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTTXUserCareer.aspx.cs" Inherits="CTTXWeiXinRoot_CTTXUserCareer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html charset=UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>职位信息--才通天下</title>
    <link href="style/common.css" rel="stylesheet" />
    <link href="style/login.css" rel="stylesheet" />
    <link href="style/animate.min.css" rel="stylesheet" />
    <script src="http://libs.baidu.com/jquery/1.10.2/jquery.min.js"></script>
    <script src="js/radio.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidMemberId" Value="100433" />
        <!--header start-->
        <div class="header">
            <a onclick="history.back()">
                <img alt="" src="img/return_icon.png" /></a>
            <span>职位信息</span>
        </div>
        <!--header end-->
        <!--career-content start-->
        <div class="career-box">
            <div id="careerList">
                <div class="career-content wow fadeInDown">
                    <div class="career-item">
                        <b>公司</b>
                        <span>
                            <input name="companyName" placeholder="请输入公司名称" /></span>
                    </div>
                    <div class="career-item">
                        <b>最高职位</b>
                        <span>
                            <input name="jobName" placeholder="请输入职位" /></span>
                    </div>
                </div>
            </div>
            <div class="login-item wow fadeInUp" style="margin-top: 1rem;">
                <button class="register-btn" id="addCareer">新增</button>
            </div>
        </div>
        <div class="login-item wow fadeInUp">
            <button class="login-btn" id="login-btn">保存</button>
        </div>
        <!--career-content end-->
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
        $('.career-box').css('min-height', winHt - 200);
    })
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#form1").removeAttr("action");
        $('#addCareer').on("click", function () {
            var last = $("#careerList .career-content:last");
            var comName = $(last).find("input[name='companyName']");
            var jobName = $(last).find("input[name='jobName']");
            if (comName.val().length == 0 || jobName.val().length == 0) {
                $('#overlay').show().delay(3000).fadeOut();
                $('#Tip').html('公司名称和职位都要填写！');
                return false;
            }
            else {
                $('#careerList').append("<div class='career-content wow fadeInDown'><div class='career-item'><b>公司</b><span><input name='companyName' placeholder='请输入公司名称'></span></div><div class='career-item'><b>最高职位</b><span><input name='jobName' placeholder='请输入职位'></span></div></div>");
            }
            return false;
        })
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
    $("#login-btn").on("click", function () {
        var careers = $("#careerList .career-content");
        var list = "";
        careers.each(function () {
            var comName = $(this).find("input[name='companyName']");
            var jobName = $(this).find("input[name='jobName']");
            if (comName.val().length >= 0 && jobName.val().length > 0) {
                list = list + comName.val().replace(",", " ").replace("|", " ") + "|" + jobName.val().replace(",", " ").replace("|", " ") + ",";
            }
        })
        if (list.length > 0) {
            list = list.substr(0, list.length - 1);
            if (confirm("请确认保存您的数据")) {
                //保存数据
                var MemberId = $("#hidMemberId").val();
                alert(MemberId);
                $.ajax({
                    type: "post",
                    url: "/CTTXServerInterface/CTTXUser.ashx",
                    data: { flag: "user_changecareer", memberId: MemberId, careers: list },
                    dataType: "json",
                    async: false,
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
                    },
                    complete: function () {
                        //console.log("提交完成......");
                    }
                });
            }
        }
        else {
            $('#overlay').show().delay(3000).fadeOut();
            $('#Tip').html('您似乎什么都没写！');
        }
        return false;
    })

</script>
