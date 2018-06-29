function signRecord(goodsId) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/RecordHandler.ashx",
        data: { flag: "sign", goodsId: goodsId, addressDetail: addressDetailRecord },
        dataType: "text",
        async: false,
        success: function (data) {
            alert(data);
        }
    });
}

function recordList() {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/RecordHandler.ashx",
        data: { flag: "list" },
        dataType: "text",
        async: false,
        success: function (data) {
            var info = eval(data);
            var htmlStr = '';
            for (var i = 0; i < info.length; i++) {
                htmlStr += '<li>' +
            '<p class="SignTime">' +
                '<b>' + getFormatDate(new Date(info[i].RecordTime)) + '&nbsp;&nbsp;&nbsp;&nbsp;</b> <span>' + info[i].TrueName + '</span>' +
            '</p>' +
            '<p class="SignAdd">' +
                '<b>' + info[i].GoodsName + '&nbsp;&nbsp;&nbsp;&nbsp;</b> <span>' + info[i].JobAddress + '</span>' +
            '</p>' +
        '</li>';
            }
            $('#recordUl').html(htmlStr);
        }
    });
}

var global_msglist_day = 7;
function msgList() {
    $.ajax({
        type: "get",
        url: "/ServiceInterface/RecordHandler.ashx",
        data: { flag: "msglist", day: global_msglist_day },
        dataType: "text",
        async: false,
        success: function (data) {
            var info = eval(data);
            var htmlStr = '';
            for (var i = 0; i < info.length; i++) {
                var _url = "NoticeInfo.html?msgid=" + info[i].sysnumber + "";
                htmlStr += '<li  onclick="location.href=\'' + _url + '\'">' +
                           '<p><a class="Sizing">' + (info[i].isRead.toString() == '1' ? '' : '<font style="color:red">●</font>') + info[i].title + '</a><b>' + getFormatDate(new Date(info[i].createTime)) + '</b></p>' +
                           '<p><span style="color:blue;text-decoration:underline;">' + info[i].remark + '</span> </p>' +
                           (info[i].url ? '<p style="display:none"><input  onclick="location.href=\'' + info[i].url + '\';"/></p>' : '') +
                          '</li>';
            }

            $('#noticeUl').html(htmlStr + $('#noticeUl').html());
            global_msglist_day = global_msglist_day + 30;
        }
    });
}

function msgDetail() {
    var msgId = getQueryString('msgid');
    if (msgId) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/RecordHandler.ashx",
            data: { flag: "msgdetail", msgid: msgId },
            dataType: "text",
            async: false,
            success: function (data) {
                var info = eval(data);
                var htmlStr = '';
                for (var i = 0; i < info.length; i++) {
                    var _url = info[i].url;
                    var _css = info[i].url.length > 5 ? ' style="color:blue;text-decoration:underline;"' : '';
                    htmlStr += '<li  onclick="location.href=\'' + _url + '\'">' +
                    '<p><a href="javascript:;" class="Sizing">' + info[i].title + '</a><b>' + getFormatDate(new Date(info[i].createTime)) + '</b></p>' +
                    '<p ' + _css + '>' + info[i].remark + '</p>' +
                    (info[i].url ? '<p style="display:none"><input type="button" value="查看页面" style="padding: 0 1.5em;color: #fff;float: right;margin-left: 10px;border: none;border-radius: 4px;line-height: 2.5em;background: #339efe;" onclick="location.href=\'' + info[i].url + '\';"/></p>' : '') +
                    '</li>';
                }
                $('#noticeUl').html(htmlStr);
            }
        });
    }
}


//LBS定位
var addressDetailRecord = '';
function successRecord(position) {
    showPositionRecord(position);
}

function errorRecord() {

}

var optionsRecord = {};

var showPositionRecord = function (position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    //var map = new BMap.Map("container");   // 创建Map实例
    var point = new BMap.Point(lon, lat); // 创建点坐标
    var gc = new BMap.Geocoder();
    gc.getLocation(point, function (rs) {
        var addComp = rs.addressComponents;
        addressDetailRecord = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
    });
};