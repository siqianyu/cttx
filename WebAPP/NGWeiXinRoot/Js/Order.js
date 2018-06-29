function addOrder() {
    if (location.href.indexOf('MyTaskInfo.html') > -1 && $('#chosenUl li').length > 0) {
        alert('已选择承包人');
        return false;
    }

    var priceId = '';
    var objList = $('.chosenLi');
    for (var i = 0; i < objList.length; i++) {
        priceId += objList[i].id + ',';
    }

    if (location.href.indexOf('MyTaskInfo.html') > -1 && objList.length > 1) {
        alert('只能选择一人进行承包');
    }
    else if (priceId == '') {
        alert('请选择报价');
    } else {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/CreateOrder.ashx",
            data: { flag: "add", priceId: priceId },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data == '1') {
                    alert('下单成功');
                    //location.reload();
                    location.href = "MyOrder.html"
                } else {
                    alert('下单失败');
                }
            }
        });
    }
}

function getOrderList(status) {
    nTabs($('#myTab0 span')[parseInt(status)], parseInt(status));
    $.ajax({
        type: "post",
        url: "/ServiceInterface/CreateOrder.ashx",
        data: { flag: "list", status: status },
        dataType: "text",
        async: false,
        success: function (data) {
            var dataInfo = eval(data);
            switch (status) {
                case '0':
                    showList0(dataInfo); break;
                case '1':
                    showList1(dataInfo); break;
                case '2':
                    showList2(dataInfo); break;
            }
        }
    });
}

function showList0(dataInfo) {
    var htmlStr = '';
    for (var i = 0; i < dataInfo.length; i++) {
        var type = '';
        var priceUnit = '';
        if (dataInfo[i].OrderType == 'QB') {
            type = dataInfo[i].JobNeeds;
            priceUnit = '元';
        } else {
            type = '点工';
            priceUnit = '元';
        }
        var typeStr = dataInfo[i].OrderType == 'DG' ? 'People' : '';
        var btn = '';
        if (dataInfo[i].IsMemberConfirm == '1') {
            btn += '<input type="button" value="支付" class="Pay" onclick="location.href=\'PayOrder.html?orderId=' + dataInfo[i].OrderId + '\'">';
        } else {
            btn += '<input type="button" value="编辑" class="Edit" onclick=\"location.href=\'AddTask' + typeStr + '.html?gid=' + dataInfo[i].GoodsId + '\'\">';
        }
        btn += '<input type="button" value="取消订单" class="CancelOrder" onclick="deleteOrderByGoodsId(\'' + dataInfo[i].GoodsId + '\',this)">';

        var priceStr = '';
        if (dataInfo[i].MemberPrice == '') {
            if (dataInfo[i].SalePrice == 0) {
                priceStr = '不限价';
            } else {
                priceStr = '￥' + parseInt(dataInfo[i].SalePrice) + priceUnit;
            }
        } else {
            priceStr = '￥' + parseInt(dataInfo[i].MemberPrice) + priceUnit;
        }

        var _url = "MyTask" + typeStr + "Info.html?gid=" + dataInfo[i].GoodsId + "";

        htmlStr += '<li>' +
            '<p class="OrderName"><a href="MyTask' + typeStr + 'Info.html?gid=' + dataInfo[i].GoodsId + '">' + dataInfo[i].GoodsName + '</a><span>' + priceStr + '</span></p>' +
            '<p class="Status"><span class="dkg">' + dataInfo[i].JobStatus + '</span><b>' + type + '</b><b>' + dataInfo[i].CategoryName + '</b></p>' +
            '<p class="OrderAdd" onclick="location.href=\''+ _url + '\'">' +
                '<span>承接人：' + (dataInfo[i].TrueName == '' ? '无' : dataInfo[i].TrueName) + '</span><br /><span>' + dataInfo[i].JobAddress + '</span><br /><span>起止日期：' + getFormatDate(new Date(dataInfo[i].JobStartTime)) + '至' + getFormatDate(new Date(dataInfo[i].JobEndTime)) + '</span>' +
            '</p>' +
            '<p class="OrderBtn">' +
                btn +
            '</p>' +
        '</li>';
    }

    $('#myTab0_Content0').html(htmlStr);
}

function showList1(dataInfo) {
    var htmlStr = '';
    for (var i = 0; i < dataInfo.length; i++) {
        var type = '';
        var priceUnit = '';
        var btn = '';
        if (dataInfo[i].OrderType == 'QB') {
            type = dataInfo[i].JobNeeds;
            priceUnit = '元';
        } else {
            type = '点工';
            priceUnit = '元';
            btn += '<input type="button" value="续聘" class="Pay" onclick="location.href=\'Renew.html?priceId=' + dataInfo[i].PriceId + '&uName=' + escape(dataInfo[i].TrueName) + '\'">';
        }
        var typeStr = dataInfo[i].OrderType == 'DG' ? 'People' : '';
        btn += '<input type="button" value="查看完工详情" class="Edit" onclick="location.href=\'FinishedInfo.html?mid=' + dataInfo[i].mid + '&gid=' + dataInfo[i].GoodsId + '&oid=' + dataInfo[i].OrderId + '\';">';
        var _url = "MyTask" + typeStr + "Info.html?gid=" + dataInfo[i].GoodsId + "";
        htmlStr += '<li>' +
                '<p class="OrderNumber"><span class="Sizing">订单号：' + dataInfo[i].OrderId + '</span></p>' +
                '<p class="OrderName"><a href="MyTask' + typeStr + 'Info.html?gid=' + dataInfo[i].GoodsId + '">' + dataInfo[i].GoodsName + '</a><span>￥' + parseInt(dataInfo[i].OrderAllMoney) + priceUnit + '</span></p>' +
                '<p class="Status"><span class="dkg">' + dataInfo[i].JobStatus + '</span><b>' + type + '</b><b>' + dataInfo[i].CategoryName + '</b></p>' +
                '<p class="OrderAdd" onclick="location.href=\'' + _url + '\'">' +
                    '<span>承接人：' + dataInfo[i].TrueName + '</span><br /><span>' + dataInfo[i].JobAddress + '</span><br /><span>起止日期：' + getFormatDate(new Date(dataInfo[i].JobStartTime)) + '至' + getFormatDate(new Date(dataInfo[i].JobEndTime)) + '</span>' +
                '</p>' +
                '<p class="OrderBtn">' +
                    btn +
                '</p>' +
            '</li>';
    }

    $('#myTab0_Content1').html(htmlStr);
}

function showList2(dataInfo) {
    var htmlStr = '';
    for (var i = 0; i < dataInfo.length; i++) {

        var type = '';
        var priceUnit = '';
        if (dataInfo[i].OrderType == 'QB') {
            type = dataInfo[i].JobNeeds;
            priceUnit = '元';
        } else {
            type = '点工';
            priceUnit = '元';
        }
        var typeStr = dataInfo[i].OrderType == 'DG' ? 'People' : '';
        var btn = '';
        if (dataInfo[i].commentId) {
            btn = '<input type="button" value="已评价" class="Edit" />';
        } else {
            btn = '<input type="button" value="评价" class="Comment" onclick="location.href=\'Evaluate.html?evalWorker=1&flag=' + dataInfo[i].OrderType + '&orderId=' + dataInfo[i].OrderId + '&goodsId=' + dataInfo[i].GoodsId + '\'">';
        }
        var _url = "MyTask" + typeStr + "Info.html?gid=" + dataInfo[i].GoodsId + "";
        htmlStr += '<li>' +
            '<p class="OrderNumber"><span class="Sizing">订单号：' + dataInfo[i].OrderId + '</span></p>' +
            '<p class="OrderName"><a href="MyTask' + typeStr + 'Info.html?gid=' + dataInfo[i].GoodsId + '">' + dataInfo[i].GoodsName + '</a><span>￥' + parseInt(dataInfo[i].OrderAllMoney) + priceUnit + '</span></p>' +
            '<p class="Status"><span class="dkg">' + dataInfo[i].JobStatus + '</span><b>' + type + '</b><b>' + dataInfo[i].CategoryName + '</b></p>' +
            '<p class="OrderAdd" onclick="location.href=\'' + _url + '\'">' +
                '<span>承接人：' + dataInfo[i].TrueName + '</span><br /><span>' + dataInfo[i].JobAddress + '</span><br /><span>起止日期：' + getFormatDate(new Date(dataInfo[i].JobStartTime)) + '至' + getFormatDate(new Date(dataInfo[i].JobEndTime)) + '</span>' +
            '</p>' +
            '<p class="OrderBtn">' +
                btn +
            '</p>' +
        '</li>';
    }

    $('#myTab0_Content2').html(htmlStr);
}

function getOrderDetail() {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/CreateOrder.ashx",
        data: { flag: "getdetail", orderId: orderId },
        dataType: "text",
        async: false,
        success: function (data) {

        }
    });
}

function deleteOrder(orderId, obj) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/CreateOrder.ashx",
        data: { flag: "delete", orderId: orderId },
        dataType: "text",
        async: false,
        success: function (data) {
            $(obj).parent().parent().hide();
        }
    });
}

function deleteOrderByGoodsId(goodsId, obj) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/CreateOrder.ashx",
        data: { flag: "deletebygoods", goodsId: goodsId },
        dataType: "text",
        async: false,
        success: function (data) {
            $(obj).parent().parent().hide();
        }
    });
}

function ReHire() {
    var priceId = getQueryString('priceId');
    var reJobStartTime = $('#reJobStartTime').val();
    var reJobDay = $('#reJobDay').val();
    var rePrice = $('#rePrice').val();

    if (priceId && reJobStartTime && reJobDay && rePrice) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/CreateOrder.ashx",
            data: { flag: "rehire", priceId: priceId, reJobStartTime: reJobStartTime, reJobDay: reJobDay, rePrice: rePrice },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data == '1') {
                    alert('续聘成功');
                    location.href = 'MyOrder.html';
                } else if (data == '0') {
                    alert('续聘失败');
                } else {
                    alert(data);
                }
            }
        });
    } else {
        alert('请填写续聘信息');
    }
}

function payOrder(orderId) {
    location.href = 'PayOrder.html?orderId='
}