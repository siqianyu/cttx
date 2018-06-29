<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JsApiPayPage.aspx.cs" Inherits="demo_JsApiPayPage" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/> 
    <title>微信支付确认-一起学会计吧</title>
	<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
</head>

           <script type="text/javascript">

               //调用微信JS api 支付
               function jsApiCall()
               {
                   WeixinJSBridge.invoke(
                   'getBrandWCPayRequest',
                   <%=wxJsApiParam%>,//josn串
                    function (res)
                    {
                        //alert(res.err_msg);
                        location.href="PayResultShow.aspx?r="+Math.random()+"&out_trade_no=<%=Request.QueryString["out_trade_no"] %>";
                     }
                    );
               }

               function callpay()
               {
                   if (typeof WeixinJSBridge == "undefined")
                   {
                       if (document.addEventListener)
                       {
                           document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                       }
                       else if (document.attachEvent)
                       {
                           document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                           document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                       }
                   }
                   else
                   {
                       jsApiCall();
                   }
               }
               
     </script>

<body>
    <form id="Form1" runat="server">
        <br/>
	    <div align="center">
		    <br/>
            <asp:Button ID="submit" runat="server" Text="立即支付" OnClientClick="callpay();return false;" style="width:300px; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:18px;" />
	    </div>
    </form>
</body>
</html>
<script src="../../AppClient/Js/jquery.min.js" type="text/javascript"></script>

