function register() {
    var phone = $('#txtPhoneNo').val();
    var pwd = $('#txtPwd').val();
    var code = $('#txtCode').val();
    if (phone && pwd && code) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/RegisterHandler.ashx",
            data: { flag: 'reg', phone: phone, pwd: pwd, code: code },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data == '1') {
                    location.href = "area.html";
                } else {
                    alert(data);
                }
            }
        });
    } else {
        alert("请完整填写信息");
    }
}


function findpwd() {
    var phone = $('#txtPhoneNo').val();
    var pwd = $('#txtPwd').val();
    var code = $('#txtCode').val();
    if (phone && pwd && code) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/RegisterHandler.ashx",
            data: { flag: 'findpwd', phone: phone, pwd: pwd, code: code },
            dataType: "text",
            async: false,
            success: function (data) {
                alert(data);
                location.href = "Login.html";
            }
        });
    } else {
        alert("请完整填写信息");
    }
}

function login() {
    var phone = $('#txtPhoneNo').val();
    var pwd = $('#txtPwd').val();
    var _url = getQueryString("url");
    if (_url) {
        _url = (_url.indexOf("?") > -1) ? _url + "&uid=" + phone : _url + "?uid=" + phone;
    }
    var backUrl = getQueryString("url") == null ? "MyHome.html?uid=" + phone : unescape(_url); //登录跳转页面
    $.ajax({
        type: "post",
        url: "/ServiceInterface/RegisterHandler.ashx",
        data: { flag: 'login', phone: phone, pwd: pwd },
        dataType: "text",
        async: false,
        success: function (data) {
            if (data == '1') {
                location.href = backUrl;
            } else {
                alert(data);
            }
        }
    });
}

var interCount;
var sec = 60;
function sendCode(flag) {
    var phone = $('#txtPhoneNo').val();
    if (phone) {
        $.ajax({
            type: "post",
            url: "/MsgInterface/SendMsg.ashx",
            data: { flag: flag, tel: phone },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data == "0") {
                    alert("验证码发送失败");
                } else {
                    $('#btnSendCode')[0].disabled = true;
                    $('#btnSendCode')[0].style.backgroundColor = "#adadad";
                    interCount = setInterval(setButtonStr, 1000);
                }
            }
        });
    } else {
        alert("请输入手机号");
    }
}

function setButtonStr() {
    if (sec == 0) {
        sec = 60;
        $('#btnSendCode')[0].disabled = false;
        $('#btnSendCode')[0].style.backgroundColor = "#339efe";
        $('#btnSendCode').html('获取验证码(60秒)');
        clearInterval(interCount);
    } else {
        $('#btnSendCode').html('获取验证码(' + sec + '秒)');
        sec--;
    }
}