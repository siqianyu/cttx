<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTTXHome.aspx.cs" Inherits="NGWeiXinRoot_CTTXHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html charset=UTF-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <title>首页--才通天下</title>
    <link href="style/common.css" rel="stylesheet" />
    <link href="style/index.css" rel="stylesheet" />
    <link href="style/animate.min.css" rel="stylesheet" />
    <script type="text/javascript" src="js/zepto.js"></script>
    <script type="text/javascript" src="js/carousel-image.js"></script>
    <!--此页面由于焦点图不支持jquery，所以必须将jquery引用放在最下面-->
</head>
<body class="bottom-pd">
    <form id="form1" runat="server">
        <asp:HiddenField ID="WXOpenId" runat="server" />
        <!--index-header start-->
        <div class="index-header">
            <div class="logo">
                <img src="img/logo.png">
            </div>
            <ul class="index-info-menu">
                <li><span class="index-menu1">战友档案</span></li>
                <li><span class="index-menu2">公司信息</span></li>
                <li style="border: none;"><span class="index-menu3">远鉴王者</span></li>
            </ul>
        </div>
        <!--index-header end-->
        <!--首页广告焦点图开始-->
        <div class="carousel-image-box">
            <div class="carousel-image">
                <div>
                    <a style="background: url('img/index_ad.jpg') center no-repeat; background-size: cover;"></a>
                    <a style="background: url('img/index_ad.jpg') center no-repeat; background-size: cover;"></a>
                </div>
                <span class="carousel-num"></span>
            </div>
        </div>
        <!--首页广告焦点图结束-->
        <!--表格开始-->
        <!--<div class="index-chart left">
			<div class="sub-title">
				<span><i></i>江湖排名</span>
				<b class="orange"><i></i></b>
				<b class="green"><i></i>才商 </b>
				<b class="red"><i></i>Y币</b>
			</div>
			<div class="index-chart-box">
				<img src="img/chart.jpg" class="chart"/>
				<img src="img/index_share.png" class="chart-share" />
			</div>
		</div>-->
        <!--表格结束-->
        <!--排行开始-->
        <div class="index-rank left">
            <div class="index-rank-title left">
                <span class="active">Y币排名</span><span>红旗排名</span><span>人气排名</span>
                <span>才商排名</span><span>财富排名</span>
            </div>
            <table class="index-rank-table">
                <tbody>
                    <tr>
                        <td><span class="rank-num">1</span></td>
                        <td>
                            <div class="rank-icon">
                                <img src="img/icon.png">
                            </div>
                        </td>
                        <td class="rank-name">马云</td>
                        <td class="rank-item-num">100000.00</td>
                    </tr>
                    <tr>
                        <td><span class="rank-num">2</span></td>
                        <td>
                            <div class="rank-icon">
                                <img src="img/icon.png">
                            </div>
                        </td>
                        <td class="rank-name">马云</td>
                        <td class="rank-item-num">100000.00</td>
                    </tr>
                    <tr>
                        <td><span class="rank-num">3</span></td>
                        <td>
                            <div class="rank-icon">
                                <img src="img/icon.png">
                            </div>
                        </td>
                        <td class="rank-name">马云</td>
                        <td class="rank-item-num">100000.00</td>
                    </tr>
                    <tr>
                        <td><span class="rank-num">4</span></td>
                        <td>
                            <div class="rank-icon">
                                <img src="img/icon.png">
                            </div>
                        </td>
                        <td class="rank-name">马云</td>
                        <td class="rank-item-num">100000.00</td>
                    </tr>
                    <tr>
                        <td><span class="rank-num">5</span></td>
                        <td>
                            <div class="rank-icon">
                                <img src="img/icon.png">
                            </div>
                        </td>
                        <td class="rank-name">马云</td>
                        <td class="rank-item-num">100000.00</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--排行结束-->
        <!--底部菜单开始-->
        <div class="footer">
            <ul>
                <li><span class="menu1 active">阅贤阁</span></li>
                <li><span class="menu2">上林苑</span></li>
                <li><span class="menu3">上书房</span></li>
                <li><span class="menu4">朕宫</span></li>
            </ul>
        </div>
        <!--底部菜单结束-->
    </form>
</body>
</html>

<script type="text/javascript">
    var Sum = $('.carousel-image > div > a').length;
    var Allwidth = $('body').width();
    var item = Sum * 12;
    //		alert(item);
    $('.carousel-image').css('width', Allwidth * Sum);
    $('.carousel-image > div > a').css('width', Allwidth);
    $('.carousel-num').css('left', (Allwidth - item) / 2);

    $('.carousel-image').CarouselImage({
        num: $('.carousel-num')
    });
</script>
<script type="text/javascript" src="js/jquery-3.2.1.min.js"></script>
<script type="text/javascript" src="js/wow.min.js"></script>
<script type="text/javascript" src="js/footer.js"></script>
<script type="text/javascript">
    if (!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
        new WOW().init();
    };
</script>

