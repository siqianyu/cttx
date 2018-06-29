<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjShareList.aspx.cs" Inherits="NGWeiXinRoot_YqxkjShareList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,viewport-fit=cover">
    <title>一起学会计吧-分享统计</title>
    <link href="WeUICss/style/weui.min.css" rel="stylesheet" type="text/css" />
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
        <h1 class="page__title">一起学会计吧</h1>
        <p class="page__desc">分享信息统计。</p>
    </div>
    <div class="page__bd">
        <div class="weui-panel">
            <div class="weui-panel__hd">用户列表</div>
            <div class="weui-panel__bd">
                <div class="weui-media-box weui-media-box_small-appmsg">
                    <div class="weui-cells">
                    <asp:Repeater runat="server" ID="rptList">
                    <ItemTemplate>
                        <a class="weui-cell weui-cell_access" href="javascript:;">
                            <div class="weui-cell__hd"><img src="<%#Eval("firendNewHeader") %>" alt="" style="width:40px;margin-right:5px;display:block"></div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p><%#Eval("firendNewNickname")%></p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="page__ft">
        <a href="javascript:home()"><img src="Images/yqxkj.png" /></a>
    </div>
</div>
    </form>
</body>
</html>
