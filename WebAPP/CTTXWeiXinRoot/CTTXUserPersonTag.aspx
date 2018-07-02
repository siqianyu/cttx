<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTTXUserPersonTag.aspx.cs" Inherits="CTTXWeiXinRoot_CTTXUserPersonTag" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>我的画像--才通天下</title>
    <link href="style/common.css" rel="stylesheet" />
    <link href="style/login.css" rel="stylesheet" />
    <link href="style/animate.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--header start-->
        <div class="header">
            <a onclick="history.back()">
                <img src="img/return_icon.png"></a>
            <span>我的画像</span>
        </div>
        <!--header end-->
        <!--personTag start-->
        <div class="personTag">
            <div class="tag-box">
                <div class="tag-btn less"><span class="tag-name">幽默</span><span class="tag-num">2</span></div>
                <div class="tag-btn less"><span class="tag-name">活泼开朗</span><span class="tag-num">3</span></div>
                <div class="tag-btn more"><span class="tag-name">职业化</span><span class="tag-num">11</span></div>
                <div class="tag-btn middle"><span class="tag-name">标签1</span><span class="tag-num">6</span></div>
                <div class="tag-btn most"><span class="tag-name">标签1</span><span class="tag-num">15</span></div>
                <div class="tag-btn less"><span class="tag-name">标签1</span><span class="tag-num">3</span></div>
            </div>
            <div class="tag-icon">
                <div>
                    <img src="img/icon_bg.jpg">
                </div>
                <span>ALICE·董事长</span>
            </div>
            <div class="tag-box">
                <div class="tag-btn less"><span class="tag-name">幽默</span><span class="tag-num">2</span></div>
                <div class="tag-btn less"><span class="tag-name">活泼开朗</span><span class="tag-num">3</span></div>
                <div class="tag-btn more"><span class="tag-name">职业化</span><span class="tag-num">11</span></div>
            </div>
        </div>
        <!--personTag end-->
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
        var ht = $(window).height();
        $('.tag-box').css('min-height', (ht - 240) / 2);
        $('.tag-btn').eq(2).css('margin', '.2rem 20%')
    })
</script>
