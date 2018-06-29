var jobType = '';
function getPriceList() {
    var gid = getQueryString('gid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/OfferPriceHandler.ashx",
        data: { flag: "list", gid: gid },
        dataType: "text",
        async: false,
        success: function (data) {
            var dataInfo = eval(data);
            if (dataInfo.length > 0) {
                var chosenContent = '';
                var unchosenContent = '';
                var chosenCount = 0, unchosenCount = 0;

                for (var i = 0; i < dataInfo.length; i++) {
                    var price = parseInt(dataInfo[i].MemberPrice);
                    var priceUnit = location.href.indexOf('People') > -1 ? '元/天' : '元';
                    if (dataInfo[i].IsEmployerSelect == '1') {
                        chosenContent += '<li>' +
                            '<div class="IconBox" onclick="location.href=\'ResumeInfo.html?mid=' + dataInfo[i].MemberId + '\'">' +
                            '<img src="' + dataInfo[i].HeadImg + '">' +
                            '</div>' +
                            '<div class="WorkerInfo Sizing">' +
                            '<b>' + dataInfo[i].TrueName + '<i style=\'display:none;\'>（' + dataInfo[i].LevelName + '）</i></b>' +
                            '<span>' + dataInfo[i].MemberMobile + '</span>' +
                            '<p>&nbsp;</p><p>' +
                            getUserStars(dataInfo[i].MemberId) +
                            '</p>' +
                            '</div>' +
                            '<div class="WorkerPrice">' +
                            '<b>¥' + price + priceUnit + '</b>' +
                            '<p>' +
                            '签到次数：<i>' + dataInfo[i].signCount + '</i> 次<br>未签到次数：<i>' + dataInfo[i].unsignCount + '</i> 次' +
                            '</p>' +
                            '</div>' +
                            '</li>';
                        chosenCount++;
                    } else {
                        unchosenContent += '<li id="' + dataInfo[i].PriceId + '">' +
                            '<div class="IconBox" onclick="location.href=\'ResumeInfo.html?mid=' + dataInfo[i].MemberId + '\'">' +
                            '<img src="' + dataInfo[i].HeadImg + '">' +
                            '</div>' +
                            '<div class="WorkerInfo Sizing">' +
                            '<b>' + dataInfo[i].TrueName + '<i style=\'display:none;\'>（' + dataInfo[i].LevelName + '）</i></b>' +
                            '<span>' + dataInfo[i].MemberMobile + '</span>' +
                            '<p>' +
                            getUserStars(dataInfo[i].MemberId) +
                            '</p>' +
                            '</div>' +
                            '<div class="WorkerPrice">' +
                            '<b>¥' + price + priceUnit + (dataInfo[i].IsLast == '1' ? '' : '') + '</b>' +
                            '<span>TA的报价</span>' +
                            '</div>' +
                            '</li>';
                        unchosenCount++;
                    }
                }
                $('#priceCount').html('竞拍人数：' + dataInfo.length);
                $('#chosenUl').html(chosenContent);
                $('#unchosenUl').html(unchosenContent);
                $('#chosenP').html('已选工人（' + chosenCount.toString() + '人）');
                $("#unchosenUl li").bind("click", function () {
                    if (this.className.indexOf('chosenLi') > -1) {
                        $(this).removeClass('chosenLi');
                    } else {
                        $(this).addClass('chosenLi');
                    }
                });
            }
        }
    });
}

function getStars(starNum) {
    var stars = '';
    for (var i = 0; i < 5; i++) {
        if (i < starNum) {
            stars += '<img src="Images/StarY.png">';
        } else {
            stars += '<img src="Images/StarG.png">';
        }
    }
    return stars;
}

var topPrice = 0;
function getPriceInfo() {
    var gid = getQueryString('gid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/OfferPriceHandler.ashx",
        data: { flag: "info", gid: gid },
        dataType: "text",
        async: false,
        success: function (data) {
            if (data == 'fill resume') {
                if (window.confirm("您需要完善简历，才可进行报价，是否去完善")) {
                    location.href = 'FillResume.html?returnUrl=OfferPrice.html?gid=' + gid;
                }
            }else {
                var dataInfo = eval(data);
                if (dataInfo.length > 0) {
                    $('#goodsName').html(dataInfo[0].GoodsName);

                    var sDate = new Date(dataInfo[0].AddTime);
                    $('#pubTime').html('发布时间：' + getFormatDate(sDate));

                    $('#offCount').html('竞拍人数：' + dataInfo[0].offCount);

                    jobType = dataInfo[0].JobType;
                    topPrice = dataInfo[0].SalePrice;
                }
            }
        }
    });
}

function SendPrice() {
    var gid = getQueryString('gid');
    var phoneNo = $('#phoneNo').val();
    var priceInfo = $('#offPriceTxt').val();
    if (!parseFloat(priceInfo) || parseFloat(priceInfo) <= 0) {
        alert('请输入正确的报价格式'); return;
    }

    if (parseFloat(topPrice) != 0 && parseFloat(priceInfo) > parseFloat(topPrice)) {
        alert('您的报价已高于最高限价，请重新报价');
        return;
    }

    if (phoneNo && priceInfo) {

        $.ajax({
            type: "post",
            url: "/ServiceInterface/OfferPriceHandler.ashx",
            data: { flag: "add", gid: gid, mobile: phoneNo, price: priceInfo, jobType: jobType },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data == 'fill resume') {
                    location.href = 'FillResume.html?returnUrl=OfferPrice.html?gid=' + gid;
                } else if (data == '1') {
                    if (jobType == 'QB') {
                        location.href = 'TaskInfo.html?gid=' + gid;
                    } else {
                        location.href = 'TaskPeopleInfo.html?gid=' + gid;
                    }
                } else if (data == "0") {
                    alert('提交失败');
                } else {
                    alert(data);
                }
            }
        });
    } else {
        alert('请输入报价和手机号码');
    }
}