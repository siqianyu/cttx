<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjShareQRCode.aspx.cs" Inherits="NGWeiXinRoot_YqxkjShareQRCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,viewport-fit=cover">
    <title>一起学会计吧-分享二维码</title>
    <link href="WeUICss/style/weui.min.css" rel="stylesheet" type="text/css" />
    <script src="Js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="Js/layer-v3.0.3/layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
    body,html{height:100%;-webkit-tap-highlight-color:transparent}
    body{font-family:-apple-system-font,Helvetica Neue,Helvetica,sans-serif}
    ul{list-style:none}
    .page,body{background-color:#f8f8f8}
    .link{color:#1aad19}
    .page__hd{padding:20px}
    .page__bd_spacing{padding:0 15px}
    .page__ft{padding-top:40px;padding-bottom:10px;text-align:center}
    .page__ft img{height:50px}
    .page__ft.j_bottom{position:absolute;bottom:0;left:0;right:0}
    .page__title{text-align:left;font-size:20px;font-weight:400}
    .page__desc{margin-top:5px;color:#888;text-align:left;font-size:14px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="WXOpenId" runat="server" />
<div class="page">
    <div class="page__hd">
        <h1 class="page__title">分享二维码</h1>
        <p class="page__desc">来自“<asp:Literal runat="server" ID="ltUser"></asp:Literal>”的分享！</p>
    </div>
    <div class="page__bd">
        <div class="weui-cells__title">扫一扫关注《一起学会计吧》微信公众号</div>
        <div class="weui-cells weui-cells_form" style="text-align:center">
            <div class="weui-cell" style="text-align:center">
                <img id="img_qrcode" runat="server" style="width:80%; margin-left:10%" />
            </div>
        </div>
        <div class="weui-cells__tips" style="display:none">提示：扫一扫关注公众号</div>
    </div>
    <div class="page__ft">
        <a href="javascript:home()"><img src="Images/yqxkj.png" /></a>
    </div>
</div>
    </form>
</body>
</html>
