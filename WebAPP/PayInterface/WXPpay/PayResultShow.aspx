<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayResultShow.aspx.cs" Inherits="PayResultShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,viewport-fit=cover">
    <title>支付结果确认</title>
    <script src="../../NGWeiXinRoot/Js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <link href="../../NGWeiXinRoot/WeUICss/style/weui.min.css" rel="stylesheet" type="text/css" />
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
    <div class="page">
    <div class="weui-msg">
        <div class="weui-msg__icon-area" id="div_success_1"><i class="weui-icon-success weui-icon_msg"></i></div>
        <div class="weui-msg__icon-area" id="div_warn_1" style="display:none"><i class="weui-icon-warn weui-icon_msg"></i></div>
        <div class="weui-msg__text-area">
            <h2 class="weui-msg__title" id="div_success_2">支付成功</h2>
            <h2 class="weui-msg__title" id="div_warn_2" style="display:none">支付未完成</h2>
            <p class="weui-msg__desc" style="display:none">展现<a href="javascript:void(0);">链接</a></p>
        </div>
        <div class="weui-msg__opr-area">
            <p class="weui-btn-area">
                <a href="javascript:go_mycourse();" class="weui-btn weui-btn_primary" id="div_success_3">确 定</a>
                <a href="javascript:go_listcourse();" class="weui-btn weui-btn_primary" id="div_warn_3" style="display:none">确 定</a>
            </p>
        </div>
        
    </div>
</div>
    </form>
</body>
</html>
<script language="javascript">
    var orderid = "<%=Request.QueryString["out_trade_no"] %>";
    function checkapy() {
        $.ajax({
            type: "get",
            url: "WeiXinPayCheck.ashx?orderId="+orderid,
            dataType: "text",
            async: true,
            success: function (data) {
                if(data=="SUCCESS"){
                    document.getElementById("div_success_1").style.display="";
                    document.getElementById("div_success_2").style.display="";
                    document.getElementById("div_success_3").style.display="";
                    document.getElementById("div_warn_1").style.display="none";
                    document.getElementById("div_warn_2").style.display="none";
                    document.getElementById("div_warn_3").style.display="none";
                }else{
                    document.getElementById("div_success_1").style.display="none";
                    document.getElementById("div_success_2").style.display="none";
                    document.getElementById("div_success_3").style.display="none";
                    document.getElementById("div_warn_1").style.display="";
                    document.getElementById("div_warn_2").style.display="";
                    document.getElementById("div_warn_3").style.display="";
                }
            }
        });
    }
    checkapy();
    //window.setInterval("checkapy()",5000);

    function go_mycourse(){
        location.href="/NGWeiXinRoot/YqxkjMemberCourse.aspx";
    }

    function go_listcourse(){
        location.href= "<%=this.back_url %>";
    }
</script>