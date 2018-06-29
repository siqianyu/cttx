<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YqxkjFlower.aspx.cs" Inherits="NGWeiXinRoot_YqxkjShare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,viewport-fit=cover">
    <title>一起学会计吧-分享</title>
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
        <h1 class="page__title">用户分享</h1>
        <p class="page__desc">来自“<asp:Literal runat="server" ID="ltUser"></asp:Literal>”的分享！</p>
    </div>
    <div class="page__bd">
        <div class="weui-cells__title">基本信息</div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">姓名</label></div>
                <div class="weui-cell__bd">
                    <input id="txtTruename" class="weui-input" type="text" placeholder="请输入姓名"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机号</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="txtMobile" class="weui-input" type="tel" placeholder="请输入手机号"/>
                </div>
            </div>
        </div>

<div class="weui-cells__title">性别</div>

<div class="weui-cells weui-cells_radio">
            <label class="weui-cell weui-check__label" for="x11">
                <div class="weui-cell__bd">
                    <p>男</p>
                </div>
                <div class="weui-cell__ft">
                    <input type="radio" class="weui-check" name="radio1" id="x11" checked="checked"/>
                    <span class="weui-icon-checked"></span>
                </div>
            </label>
            <label class="weui-cell weui-check__label" for="x12">

                <div class="weui-cell__bd">
                    <p>女</p>
                </div>
                <div class="weui-cell__ft">
                    <input type="radio" name="radio1" class="weui-check" id="x12"/>
                    <span class="weui-icon-checked"></span>
                </div>
            </label>
        </div>

        <div class="weui-cells__tips">提示：请填写正确的手机号，方便我们联系</div>

        <label for="weuiAgree" class="weui-agree">
            <input id="weuiAgree" type="checkbox" class="weui-agree__checkbox"/>
            <span class="weui-agree__text">
                阅读并同意<a href="javascript:void(0);">《相关条款》</a>
            </span>
        </label>

        <div class="weui-btn-area">
            <a class="weui-btn weui-btn_primary" href="javascript:user_reg();void(0);" id="btnSave">确定</a>
        </div>
    </div>
    <div class="page__ft">
        <a href="javascript:home()"><img src="Images/yqxkj.png" /></a>
    </div>
</div>
    </form>
</body>
</html>
    <!--用户注册start-->
    <script language="javascript" type="text/javascript">
        function user_reg() {
            var _txtWXOpenId = $("#WXOpenId").val() + "|" + $.trim($("#txtMobile").val());
            var _txtMobile = $.trim($("#txtMobile").val());
            var _txtTruename = escape($.trim($("#txtTruename").val()));
            var _txtEmail = "";// $.trim($("#txtEmail").val());
            var _ddlArea3 = ""; //$.trim($("#ddlArea3").val());
            if (_txtTruename == "") { layer.msg("请输入姓名"); return false; }
            if (_txtMobile == "" || _txtMobile.length != 11) { layer.msg("请输入手机号"); return false; }
            //if (_txtEmail == "") { layer.msg("请输入邮箱"); return false; }
            //if (_ddlArea3 == "") { layer.msg("请选择地区"); return false; }
            layer.load(2); //加载时显示加载效果
            $("#btnSave").hide();
            $.ajax({
                type: "post",
                url: "YqxkjUserRegAndLogin.ashx",
                data: { flag: "user_reg", phone: _txtMobile, email: _txtEmail, truename: _txtTruename, wxopenid: _txtWXOpenId, areacode: _ddlArea3 },
                dataType: "text",
                async: true,
                success: function (data) {
                    layer.closeAll('loading'); //关闭所有加载效果
                    $("#btnSave").show();
                    if (data == '1') {
                        alert('注册成功，恭喜您成为一起学会计吧平台会员！');
                        user_login();
                    } else {
                        alert(data);
                    }
                }
            });
        }
    </script>
    <!--用户注册end-->
    <!--用户登录start-->
    <script language="javascript" type="text/javascript">
        function user_login() {
            var _txtMobile = $.trim($("#txtMobile").val());
            var _txtPwd = "";
            layer.load(2); //加载时显示加载效果
            $.ajax({
                type: "post",
                url: "YqxkjUserRegAndLogin.ashx",
                data: { flag: "user_login", phone: _txtMobile, pwd: _txtPwd },
                dataType: "text",
                async: true,
                success: function (data) {
                    layer.closeAll('loading'); //关闭所有加载效果
                    if (data == '1') {
                        location.href = "YqxkjHome.aspx";
                    } else {
                        alert(data);
                    }
                }
            });
        }
    </script>
    <!--用户登录end-->