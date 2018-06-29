function getPayInfo() {
    var orderId = getQueryString('orderId');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/PayHandler.ashx",
        data: { flag: "info", orderId: orderId },
        dataType: "text",
        async: false,
        success: function (data) {
            var info = eval(data);
            if (info.length > 0) {
                $("#infoTitle").html(info[0].GoodsName);
                $("#infoCate").html('招工意向：' + info[0].CategoryName);
                $("#infoDays").html('工期：' + info[0].JobDay + '天');
                var sDate = new Date(info[0].JobStartTime);
                var eDate = new Date(info[0].JobEndTime);
                $("#infoStartTime").html('<b>起止日期：</b>' + getFormatDate(sDate) + '至' + getFormatDate(eDate));

                var unitPriceStr = '';
                var sumPriceStr = '';
                if (info[0].JobType == 'QB') {
                    unitPriceStr = '¥' + parseInt(info[0].MemberPrice) + '元';
                    sumPriceStr = '¥' + parseInt(info[0].MemberPrice) + '元';
                } else {
                    unitPriceStr = '¥' + parseInt(info[0].MemberPrice) + '元/天';
                    var sumPrice = parseFloat(info[0].MemberPrice) * parseInt(info[0].JobDay);
                    sumPriceStr = '¥' + parseInt(sumPrice.toString()) + '元';
                }

                var htmlStr = '<li class="Sizing">' +
            '<img src="Images/ChooseIco.png">' +
            '<span style="color: #717171" class="Sizing"><b>竞聘人</b>' + info[0].TrueName + '</span> <span style="color: #999"' +
                'class="Sizing"><b>竞聘价格</b>' + unitPriceStr + '</span> </li>';

                $('#workUl').html(htmlStr);

                $('#sumPriceB').html(sumPriceStr);
                $('#payPriceB').html(sumPriceStr);
            }
        }
    });
}

function payOrder() {
    var orderId = getQueryString('orderId');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/PayHandler.ashx",
        data: { flag: "pay", orderId: orderId },
        dataType: "text",
        async: false,
        success: function (data) {
            location.href = 'MyOrder.html?listtype=1';
        }
    });
}

