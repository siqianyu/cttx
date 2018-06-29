<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JSSDKDemo.aspx.cs" Inherits="NGWeiXinRoot_JSSDKDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微信jssdk</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=0">    
    <!--微信jssdk类库-->
    <script language="javascript" src="http://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    <script src="../AppClient/Js/jquery-1.9.0.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hidOpenId" Value="oE12M0QZlHtBINwsgyFo26rUWpAA" />
    <div>
    <input type="button" value="扫一扫接口" onclick="scanQRCode()" /><br /><br />
    <input type="button" value="地理位置接口" onclick="getLocation()" /><br /><br />
    <input type="button" value="网络类型接口" onclick="getNetworkType()" />
    
    <br /><br /><br /><br />-------------------------------------------<br />
    <a href="https://mp.weixin.qq.com/debug/cgi-bin/sandbox?t=jsapisign" target="_blank">签名验证</a>
    </div>
    </form>
</body>
</html>
    <!--微信jssdk初始化-->
    <script language="javascript">
        //后台服务生成签名
        var targetUrl = location.href.split('#')[0];
        var configInfo = $.ajax({ url: "WXJSSDKInterface.ashx?flag=jsapi_token&targetUrl=" + targetUrl, async: false }).responseText;
        //alert(configInfo);
        var configArr = configInfo.split('$');
        //获取签名
        wx.config({
            debug: false,
            appId: 'wx68e162b98725824f', //申请的公众号id
            timestamp: configArr[1],
            nonceStr: configArr[0],
            signature: configArr[2],
            jsApiList: [
            'checkJsApi',
            'onMenuShareTimeline',
            'onMenuShareAppMessage',
            'onMenuShareQQ',
            'onMenuShareWeibo',
            'onMenuShareQZone',
            'hideMenuItems',
            'showMenuItems',
            'hideAllNonBaseMenuItem',
            'showAllNonBaseMenuItem',
            'translateVoice',
            'startRecord',
            'stopRecord',
            'onVoiceRecordEnd',
            'playVoice',
            'onVoicePlayEnd',
            'pauseVoice',
            'stopVoice',
            'uploadVoice',
            'downloadVoice',
            'chooseImage',
            'previewImage',
            'uploadImage',
            'downloadImage',
            'getNetworkType',
            'openLocation',
            'getLocation',
            'hideOptionMenu',
            'showOptionMenu',
            'closeWindow',
            'scanQRCode',
            'chooseWXPay',
            'openProductSpecificView',
            'addCard',
            'chooseCard',
            'openCard'
           ]
        });

        //加载完成
        wx.ready(function () {
            alert("wx.ready");
            var shareData = {//自定义分享数据
                title: '一起学会计吧',
                desc: '一起学会计吧，关注微信公众号',
                link: 'http://www.yiqixkj.com/NGWeiXinRoot/YqxkjShare.aspx?wx_openid=' + $("#hidOpenId").val() + '', //链接地址
                imgUrl: 'http://www.yiqixkj.com/qrcode_yqxkj.jpg'
            };

            wx.onMenuShareAppMessage(shareData);
            wx.onMenuShareTimeline(shareData);
            wx.onMenuShareQQ(shareData);
            wx.onMenuShareQZone(shareData);
            wx.onMenuShareWeibo(shareData);
        });
        
        //错误处理
        wx.error(function (res) {
            alert("[error]:" + res.errMsg);
        });

        //获取地理位置
        function getLocation() {
            wx.getLocation({
                success: function (res) {
                    alert(JSON.stringify(res));
                },
                cancel: function (res) {
                    alert('用户拒绝授权获取地理位置');
                }
            });
        }

        //扫一扫
        function scanQRCode(){
            wx.scanQRCode();
        }

        //网络类型
        function getNetworkType() {
            wx.getNetworkType({
                success: function (res) {
                    alert(res.networkType);
                },
                fail: function (res) {
                    alert(JSON.stringify(res));
                }
            });
        }
    </script>