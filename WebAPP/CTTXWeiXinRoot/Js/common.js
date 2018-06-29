function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function getFormatDate(sDate,isEight) {
    var str = sDate.getFullYear().toString();
    str += '/';
    str += (sDate.getMonth() + 1 < 10 ? '0' + (sDate.getMonth() + 1).toString() : (sDate.getMonth() + 1).toString());
    str += '/';
    if(isEight){
        str += (sDate.getDate()+1 < 10 ? '0' + (sDate.getDate()+1).toString() : (sDate.getDate()+1).toString());
        str +=' 08:00';
    }else{
        str += (sDate.getDate() < 10 ? '0' + (sDate.getDate()).toString() : (sDate.getDate()).toString());
        str += ' ' + (sDate.getHours()< 10 ? '0' + sDate.getHours().toString():sDate.getHours().toString());
        str += ':' + (sDate.getMinutes()< 10 ? '0' + sDate.getMinutes().toString():sDate.getMinutes().toString());
    }
    return str;
}

function replaceAll(str, oldStr, newStr) {
    str = str.replace(new RegExp(oldStr, 'gm'), newStr);
    return str;
}

Date.prototype.addDay = function (num) {
    this.setDate(this.getDate() + num);
    return this;
}

function getUserStars(userId) {
    var starStr = '';
    $.ajax({
        type: "post",
        url: "/ServiceInterface/UserHandler.ashx",
        data: { flag: "stars", userId: userId },
        dataType: "text",
        async: false,
        success: function (data) {
            var dataInfo = eval(data);
            if (dataInfo && dataInfo.length > 0) {
                for (var i = 1; i <= 5; i++) {
                    if (i <= parseInt(dataInfo[0].star)) {
                        starStr += '<img src="Images/StarY.png">';
                    } else {
                        starStr += '<img src="Images/StarG.png">';
                    }
                }
                starStr += dataInfo[0].num + '人评价';
            }
        }
    });
    return starStr;
}

var isEr = true;
function showHome(divId) {
    if (divId == '#eeDiv') {
        $('#erDiv').hide();
        $('#erSpan')[0].style.backgroundColor = 'lightgray';
        $('#eeSpan')[0].style.backgroundColor = '#ff9500';
        isEr = false;
        $.cookie('USER_ROLE', "isEe", { expires: 700, path: '/' });
    } else if (divId == '#erDiv') {
        $('#eeDiv').hide();
        $('#erSpan')[0].style.backgroundColor = '#3fb5dc';
        $('#eeSpan')[0].style.backgroundColor = 'lightgray';
        isEr = true;
        $.cookie('USER_ROLE',  "isEr", { expires: 700, path: '/' });
    }
    $(divId).show();
}

function AddMenu() {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/RecordHandler.ashx",
        data: { flag: "unread" },
        dataType: "text",
        async: true,
        success: function (data) {
            var redBall = '';
            if (data != '0' && data != 'please login') {
                redBall = '<font style="color:red;">●</font>';
            }

            var menuStr = '';
            if (isEr) {
                menuStr = '<a href="MyOrder.html?r=' + Math.random() + '" class="Order">订单</a>';
            } else {
                menuStr = '<a href="FillResume.html?r=' + Math.random() + '" class="Resume">简历</a>';
            }

            $('#menuDiv').html('<span>' +
                    '<a href="index.html?r=' + Math.random() + '" class="Home">首页</a>' +
                '</span>' +
                '<span>' +
                    '<a href="MyNotice.html?r=' + Math.random() + '" class="Message">' + redBall + '消息</a>' +
                '</span>' +
                '<span>' +
                    menuStr +
                '</span>' +
                '<span>' +
                    '<a href="MyHome.html?r=' + Math.random() + '" class="Mine">我的</a>' +
                '</span>');
        }
    });

}
AddMenu();
setInterval(AddMenu, 1000);

/*
*用户是否登录验证（kxm）
*/
function CheckUserLogin() {
    var s = $.cookie('memberInfo');
    //alert(s);
    if (s == null || s.indexOf("$$") == -1) {
        location.href = 'Login.html?r=' + Math.random();
        return false;
    }
    return true;
}

/*
*用户id（kxm）
*/
function GetMemberId() {
    var s = $.cookie('memberInfo');
    if (s != null && s.indexOf("$$") > -1) { return s.split('$$')[0]; }
    return "";
}

/*
*用户真实姓名（kxm）
*/
function GetUserTruename() {
    var s = $.cookie('memberInfoMore');
    if (s != null && s.indexOf("$$") > -1) { return s.split('$$')[0]; }
    return "";
}

/*
*用户手机号（kxm）
*/
function GetUserMobile() {
    var s = $.cookie('memberInfoMore');
    if (s != null && s.indexOf("$$") > -1) { return s.split('$$')[1]; }
    return "";
}