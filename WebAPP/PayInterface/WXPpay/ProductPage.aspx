<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPage.aspx.cs" Inherits="demo_ProductPage" %>


<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/> 
    <title>微信支付跳转-一起学会计吧</title>
</head>

       <script type="text/javascript">
       /*
          //获取共享地址
          function editAddress()
          {
             WeixinJSBridge.invoke(
                 'editAddress',
                 <%=wxEditAddrParam%>,//josn串
                   function (res)
                   {
                       var addr1 = res.proviceFirstStageName;
                       var addr2 = res.addressCitySecondStageName;
                       var addr3 = res.addressCountiesThirdStageName;
                       var addr4 = res.addressDetailInfo;
                       var tel = res.telNumber;
                       var addr = addr1 + addr2 + addr3 + addr4;
                       alert(addr + ":" + tel);
                   }
               );
         }

           window.onload = function ()
           {
               if (typeof WeixinJSBridge == "undefined")
               {
                   if (document.addEventListener)
                   {
                       document.addEventListener('WeixinJSBridgeReady', editAddress, false);
                   }
                   else if (document.attachEvent)
                   {
                       document.attachEvent('WeixinJSBridgeReady', editAddress);
                       document.attachEvent('onWeixinJSBridgeReady', editAddress);
                   }
               }
               else
               {
                   editAddress();
               }
           };
           */
	    </script>

<body>
    <form id="Form1" runat="server">
        <asp:HiddenField ID="out_trade_no" runat="server" />
        <br/><br/><br/>
        <div align="center" style="text-align:center"><img src="1.gif" /></div>
        <div align="center" style="display:none">
            <asp:Label ID="Label1" runat="server" style="color:#00CD00;"><b>商品一：价格为<span style="color:#f00;font-size:50px">1分</span>钱</b></asp:Label><br/><br/>
            <asp:Button ID="Button1" runat="server" Text="立即购买" style="width:210px; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;" OnClick="Button1_Click" />
	    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function to_pay_page() {
        var _url = "<%=this.pay_url %>";
        //alert(_url);
        if (_url && _url != "") {location.href = _url;}
    }
    window.setTimeout("to_pay_page()", 1500);

    
    function checkapy() {
        var orderid = "<%=Request.QueryString["out_trade_no"] %>";
        $.ajax({
            type: "get",
            url: "WeiXinPayCheck.ashx?orderId="+orderid,
            dataType: "text",
            async: true,
            success: function (data) {
                if(data=="SUCCESS"){
                    alert("订单已支付，请不要重复支付");
                    location.href="/NGWeiXinRoot/YqxkjMemberCourse.aspx";
                }
            }
        });
    }
    checkapy();
</script>