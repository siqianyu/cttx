

var price = 0;
var jobNeeds = '';
var gid = '';
var commentMid = '';
function showPrice(priceType) {
    if (priceType == 0) {
        $('#priceNumber').hide();
        $('#priceHigh').show();
        $('#priceZero')[0].style.backgroundColor = '#339efe';
        $('#priceZero')[0].style.color = 'white';
        price = 0;
    }
    else {
        $('#priceNumber').show();
        $('#priceHigh').hide();
        $('#priceZero')[0].style.backgroundColor = 'white';
        $('#priceZero')[0].style.color = '#339efe';
        price = 1;
    }
}

var g_comment_page = 0;
function getBossCommentList() {
    var mid = commentMid;
    $.ajax({
        type: "get",
        url: "/ServiceInterface/ResumeHandler.ashx",
        data: { flag: 'comment', mid: mid, isWorker: '0', page: g_comment_page },
        dataType: "text",
        async: false,
        success: function (data) {
            var dataInfo = eval(data);
            var htmlStr = '';
            for (var i = 0; i < dataInfo.length; i++) {
                htmlStr += '<li>' +
                    '<img src="' + dataInfo[i].HeadImg + '" style=" height:50px; width:50px; left:10px; top:10px;">' +
                    '<span class="resumeLiName">' + dataInfo[i].TrueName + '</span>' +
                    '<span class="resumeStars">' + getStars(parseInt(dataInfo[i].commentType)) + '</span>' +
                    '<span class="resumeLiDate">' + dataInfo[i].createTime.split(' ')[0] + '</span>' +
                    '<span class="resumeLiWord">' + dataInfo[i].context + '</span>' +
                '</li>';
            }
            $('#commentUl').append(htmlStr);
            g_comment_page++;
            if (htmlStr == "" || dataInfo.length < 5) { $("#more_comment").hide(); }
        }
    });
}

function opNeeds(objId, value) {
    jobNeeds = value;
    $('#jobNeeds a').html('包工');
    $('#jobNeeds a').next().html('包工包料');
    $('#jobNeeds a').next().next().html('包工及工具');

    $('#' + objId).html(value + '<i class="Selected">');
}

function showCate() {
    $("#cateUl").show();
}

function fileAdd() {
    if ($('#divImgs').children().length >= 7) {
        alert('最多只能上传6张图片');
    } else {
        $("#taskForm").ajaxSubmit({
            success: function (redata) {
                var result = redata.split('_$$_');
                if (result.length == 1) {
                    alert(result[0]);
                }
                else if (result.length == 2) {
                    $('#divImgs').prepend('<span id="' + result[0] + '"><img src="' + result[1] + '"><a href="javascript:delFile(\'' + result[0] + '\')" class="Delete"></a></span>');
                } else {
                    alert('添加失败');
                }
            },
            error: function (error) {
                document.write(error.responseText);
            },
            url: '/ServiceInterface/GoodsHandler.ashx?flag=uppic', /*设置post提交到的页面*/
            type: "post", /*设置表单以post方法提交*/
            dataType: "text", /*设置返回值类型为文本*/
            uploadProgress: function (event, position, total, percentComplete) {

            },
            complete: function (xhr) {

            }
        });
    }
}

function delFile(fileid) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/GoodsHandler.ashx",
        data: { flag: "delpic", id: fileid },
        dataType: "text",
        async: false,
        success: function (data) {
            $('#' + fileid).remove();
        }
    });
}

function commonCategory() {
    var dataInfo = '';
    $.ajax({
        type: "post",
        url: "/ServiceInterface/GoodsHandler.ashx",
        data: { flag: "getCate" },
        dataType: "text",
        async: false,
        success: function (data) {
            dataInfo = data;
        }
    });
    return dataInfo;
}

//添加页面
function getCategory() {
    var data = commonCategory();
    if (data != '0') {
        var categoryInfo = eval(data);
        $('#cateUl').html('');
        for (var i = 0; i < categoryInfo.length; i++) {
            $('#cateUl').html($('#cateUl').html() + '<li title="' + categoryInfo[i].CategoryId + '">' + categoryInfo[i].CategoryName + '</li>');
            if (i == 0) {
                $("#categoryId").val(categoryInfo[i].CategoryId);
                $("#categoryName").val(categoryInfo[i].CategoryName);
            }
        }

        $("#cateUl li").bind("click", function () {
            $("#categoryId").val($(this)[0].title);
            $("#categoryName").val($(this).html());
            All.style.position = 'relative';
            PopBg.style.display = 'none';
        });
    }
}

//列表页面
function getCategoryList() {
    var data = commonCategory();
    if (data != '0') {
        var categoryInfo = eval(data);
        $('#cateSel').html('<option value="" selected="selected">请选择</option>');
        for (var i = 0; i < categoryInfo.length; i++) {
            $('#cateSel').html($('#cateSel').html() + '<option value="' + categoryInfo[i].CategoryId + '">' + categoryInfo[i].CategoryName + '</option>');
        }
    }
}

function commonGoods() {
    gid = getQueryString("gid");
    var dataInfo = '';
    if (gid) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/GoodsHandler.ashx",
            data: { flag: 'infodetail', gid: gid },
            dataType: "text",
            async: false,
            success: function (data) {
                dataInfo = data;
            }
        });
    } else {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success, error, options);
        }
    }
    return dataInfo;
}

//添加页面
function getGoods() {
    var data = commonGoods();
    try {
        if (data) {
            var t = $('#titleP').html();
            t = replaceAll(t, '添加', '编辑');
            $('#titleP').html('编辑订单');
            $('title').html('编辑订单');

            var infoArray = data.split('_$$_');
            var goodsInfo = eval(infoArray[0])[0];
            var goodsPic = eval(infoArray[1]);

            goodsId = goodsInfo.GoodsId;
            $("#jobDay").val(goodsInfo.JobDay);
            $("#jobArea").val(goodsInfo.JobArea);
            $("#categoryName").val(goodsInfo.CategoryName);
            $("#goodsName").val(goodsInfo.GoodsName);
            $("#goodsDesc").val(goodsInfo.GoodsDesc);
            $("#jobAddress").val(goodsInfo.JobAddress);

            //日期
            var sDate = new Date(goodsInfo.JobStartTime);
            $("#jobStartTime").val(getFormatDate(sDate));

            //价格
            price = parseInt(goodsInfo.SalePrice);
            price = isNaN(price) ? 0 : price;
            if (price == 0) {
                showPrice(0);
            } else {
                showPrice(price);
                $('#priceNumber').val(parseInt(goodsInfo.SalePrice));
            }

            //工程需求
            jobNeeds = goodsInfo.JobNeeds;
            switch (goodsInfo.JobNeeds) {
                case '包工': opNeeds('jobNeeds1', '包工'); break;
                case '包工包料': opNeeds('jobNeeds2', '包工包料'); break;
                case '包工及工具': opNeeds('jobNeeds3', '包工及工具'); break;
            }

            //图片
            for (var i = 0; i < goodsPic.length; i++) {
                $('#divImgs').prepend('<span id="' + goodsPic[i].sysnumber + '"><img src="' + goodsPic[i].picPath + '"><a href="javascript:delFile(\'' + goodsPic[i].sysnumber + '\')" class="Delete"></a></span>');
            }
        } else {
            var sDate = new Date();
            //$("#jobStartTime").val(getFormatDate(sDate, true));

            var tomorrow = $.ajax({ url: "/ServiceInterface/MoreOthers.ashx?flag=gettimebytomorrow", async: false }).responseText;
            $("#jobStartTime").val(tomorrow);
        }
    }
    catch (ex) { }
}

//详情页面
function getGoodsDetail() {
    var data = commonGoods();
    try {
        var infoArray = data.split('_$$_');
        var goodsInfo = eval(infoArray[0])[0];
        var goodsPic = eval(infoArray[1]);

        goodsId = goodsInfo.GoodsId;
        $("#infoTitle").html(goodsInfo.GoodsName);

        var pDate = new Date(goodsInfo.AddTime);
        $("#infoTime").html('发布时间：' + getFormatDate(pDate));

        price = parseInt(goodsInfo.SalePrice);
        $("#infoPrice").html(isNaN(price) || price == '0' ? '不限价' : price.toString());

        $("#infoCate").html('招工意向：' + goodsInfo.CategoryName);
        $("#infoDays").html('工期：' + goodsInfo.JobDay + '天');
        $("#infoArea").html('面积：' + goodsInfo.JobArea + 'm<sup>2</sup>');
        $("#infoMemo").html(goodsInfo.GoodsDesc);
        $("#infoNeeds").html('工程需求：'+goodsInfo.JobNeeds);

        var sDate = new Date(goodsInfo.JobStartTime);
        var eDate = new Date(goodsInfo.JobEndTime);
        $("#infoStartTime").html('<b>起止日期：</b>' + getFormatDate(sDate) + '至' + getFormatDate(eDate));

        $("#infoAddress").html('<b>地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;址：</b>' + goodsInfo.JobAddress);
        $("#infoMember").html('<b>联系人：</b>' + goodsInfo.TrueName);
        $("#userStar").html(getUserStars(goodsInfo.MemberId));

        var tel = (goodsInfo.DataFrom == null || goodsInfo.DataFrom == "") ? goodsInfo.Mobile : goodsInfo.DataFrom;
        $("#infoMobile").html('<b>手&nbsp;&nbsp;&nbsp;&nbsp;机：</b>' + tel);

        //图片
        $('#ulImgs').html('');
        for (var i = 0; i < goodsPic.length; i++) {
            $('#ulImgs').append('<li><img src="' + goodsPic[i].picPath + '" style="height:150px;"></li>');
        }
        commentMid = goodsInfo.MemberId;
    }
    catch (ex) { }
}

function commonGoodsList(jobType) {
    var dataInfo = '';
    $.ajax({
        type: "post",
        url: "/ServiceInterface/GoodsHandler.ashx",
        data: { flag: 'list', categoryId: $('#cateSel').val(), jobType: jobType },
        dataType: "text",
        async: false,
        success: function (data) {
            dataInfo = data;
        }
    });

    return dataInfo;
}

function getGoodsList() {
    $('#Scn').hide();
    var data = commonGoodsList("QB");
    //alert(data);
    $('#goodsListUl').html('');
    var infoArray = eval(data);
    for (var i = 0; i < infoArray.length; i++) {
        var sDate = new Date(infoArray[i].JobStartTime);
        var eDate = new Date(infoArray[i].JobEndTime);

        $('#goodsListUl').html($('#goodsListUl').html() + '<li onclick="location.href=\'TaskInfo.html?gid=' + infoArray[i].GoodsId + '\'">' +
            '<div class="ItemImg"><img src="' + infoArray[i].GoodsSmallPic + '"></div>' +
            '<div class="ItemInfo Sizing">' +
                '<b>' + infoArray[i].GoodsName + '</b>' +
                '<span>' + infoArray[i].CategoryName + '<i style="font-size:1.2em">' + (parseInt(infoArray[i].SalePrice) == 0 ? '不限价' : (parseInt(infoArray[i].SalePrice).toString() + ' 元')) + '</i></span><span>工期：' + getFormatDate(sDate) + '至' + getFormatDate(eDate) + '</span>' +
                '<p class="Sizing">' + infoArray[i].JobAddress + '</p>' +
            '</div>' +
        '</li>');
    }
}

function getGoodsPeopleList() {
    $('#Scn').hide();
    var data = commonGoodsList("DG");
    try {
        $('#goodsListUl').html('');
        var infoArray = eval(data);
        for (var i = 0; i < infoArray.length; i++) {

            var sDate = new Date(infoArray[i].JobStartTime);
            var eDate = new Date(infoArray[i].JobEndTime);

            $('#goodsListUl').html($('#goodsListUl').html() + '<li onclick="location.href=\'TaskPeopleInfo.html?gid=' + infoArray[i].GoodsId + '\'">' +
            '<div class="ItemImg"><img src="' + infoArray[i].GoodsSmallPic + '"></div>' +
            '<div class="ItemInfo Sizing">' +
                '<b>' + infoArray[i].GoodsName + '</b>' +
                '<span>' + infoArray[i].CategoryName + '<i style="font-size:1.2em">' + (parseInt(infoArray[i].SalePrice) == 0 ? '不限价' : (parseInt(infoArray[i].SalePrice).toString() + ' 元/天')) + '</i></span><span>工期：' + getFormatDate(sDate) + '至' + getFormatDate(eDate) + '</span>' +
                '<p class="Sizing">' + infoArray[i].JobAddress + '</p>' +
            '</div>' +
        '</li>');
        }
    } catch (ex) { }
}

function MyGoodsList(jobType) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/GoodsHandler.ashx",
        data: { flag: 'mylist', jobType: jobType },
        dataType: "text",
        async: false,
        success: function (data) {
            var typeStr = jobType == 'DG' ? 'People' : '';
            var dataInfo = eval(data);
            var htmlStr = '';
            for (var i = 0; i < dataInfo.length; i++) {
                var aDate = new Date(dataInfo[i].AddTime);

                var statusClass = '';
                var editBtn = '';
                switch (dataInfo[i].JobStatus) {
                    case '未发布':
                        statusClass = 'dkg';
                        editBtn = '<span class="dzf" onclick="location.href=\'' + (jobType == 'QB' ? 'AddTask.html?gid=' + dataInfo[i].GoodsId : 'AddTaskPeople.html?gid=' + dataInfo[i].GoodsId) + '\'">编辑</span>';
                        break;
                    case '竞价中':
                        statusClass = 'jjz';
                        break;
                    case '进行中':
                        statusClass = 'sgz';
                        break;
                    case '已完成':
                        statusClass = 'ywg';
                        break;
                }

                htmlStr += '<li onclick="location.href=\'MyTask' + typeStr + 'Info.html?gid=' + dataInfo[i].GoodsId + '\'">' +
                '<p class="OrderName"><a href="javascript:Task' + typeStr + 'Info.html?gid=' + dataInfo[i].GoodsId + '">' + dataInfo[i].GoodsName + '</a></p>' +
                '<p class="Status"><span class="' + statusClass + '">' + dataInfo[i].JobStatus + '</span><b>报价人数：<i>' + dataInfo[i].priceCount + '</i></b></p>' +
                '<p class="OrderPubTime">' + getFormatDate(aDate) + '</p>' +
                '</li>';
            }
            $('#goodsListUl').html(htmlStr);
        }
    });
}

function addGoods(isSale, jobType) {
    var flag = '';
    if (gid)
        flag = 'update';
    else
        flag = 'add';



    if (price != 0) {
        if (!parseFloat($('#priceNumber').val()) || parseFloat($('#priceNumber').val()) <= 0) { alert("请输入正确的价格"); return false; }
        price = $('#priceNumber').val();
    }

    var picIds = '';
    for (var i = 0; i < $('#divImgs').children().length; i++) {
        picIds += $('#divImgs').children()[i].id + ',';
    }

    if ($('#jobStartTime').val() && $('#jobDay').val() && $('#categoryId').val() && $('#goodsName').val() && $('#jobAddress').val()) {
        $("#btnPubTask").hide();
        $.ajax({
            type: "post",
            url: "/ServiceInterface/GoodsHandler.ashx",
            data: { flag: flag, gid: gid, jobStartTime: $('#jobStartTime').val(), jobDay: $('#jobDay').val(), jobArea: $('#jobArea').val(), jobType: jobType, categoryId: $('#categoryId').val(), goodsName: $('#goodsName').val(), goodsDesc: $('#goodsDesc').val(), salePrice: price, jobNeeds: jobNeeds, jobAddress: $('#jobAddress').val(), isSale: isSale, picIds: picIds, tel: $("#tel").val(), Lat: $("#hidLat").val(), Lng: $("#hidLon").val() },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data == '0') {
                    alert('提交失败');
                }
                else if (data.indexOf('错误：') > -1) {
                    alert(data);
                }
                else {
                    if (gid) {
                        alert("修改成功");
                        location.href = 'MyOrder.html';
                    }
                    else {
                        location.href = 'NoticeInfo.html?msgid=' + data;
                    }
                }

            }
        });
    } else {
        alert('请填写完整信息');
    }
}


function getMyAccpetList(jobType, status) {
    nTabs($('#myTab0 span')[parseInt(status)], parseInt(status));
    $.ajax({
        type: "post",
        url: "/ServiceInterface/GoodsHandler.ashx",
        data: { flag: "myacceptlist", status: status, jobType: jobType },
        dataType: "text",
        async: false,
        success: function (data) {
            var dataInfo = eval(data);
            switch (status) {
                case 0:
                    showAcList0(dataInfo); break;
                case 1:
                    showAcList1(dataInfo); break;
            }
        }
    });
}

function showAcList0(dataInfo) {
    var htmlStr = '';
    for (var i = 0; i < dataInfo.length; i++) {
        var type = '';
        var priceUnit = '';
        if (dataInfo[i].JobType == 'QB') {
            type = dataInfo[i].JobNeeds;
            priceUnit = '元';
        } else {
            type = '点工';
            priceUnit = '元/天';
        }
        var typeStr = dataInfo[i].JobType == 'DG' ? 'People' : '';

        var btn = '';
        if (dataInfo[i].IsEmployerSelect == '1') {
            if (dataInfo[i].IsMemberConfirm == '1' && dataInfo[i].IsPay == '1') {
                btn += '<input type="button" value="完工" class="CancelOrder" onclick="location.href=\'Finished.html?oid=' + dataInfo[i].OrderId + '&gid=' + dataInfo[i].GoodsId + '\'">';
                btn += '<input type="button" value="签到" class="Renew" onclick="signRecord(\'' + dataInfo[i].GoodsId + '\')">';
            } else if (dataInfo[i].IsMemberConfirm != '1') {
                btn += '<input type="button" value="拒绝" class="CancelOrder" onclick="DeletePrice(\'' + dataInfo[i].PriceId + '\',this)")"><input type="button" value="接受" class="Pay" onclick="AcceptOrder(\'' + dataInfo[i].PriceId + '\', \'1\')">';
            }
        } else {
            btn += '<input type="button" value="取消" class="CancelOrder" onclick="DeletePrice(\'' + dataInfo[i].PriceId + '\',this)">';
            btn += '<input type="button" value="再次报价" class="Renew" onclick="location.href = \'OfferPrice.html?gid=' + dataInfo[i].GoodsId + '\'">';
        }

        htmlStr += '<li>' +
            '<p class="OrderName"><a href="Task' + typeStr + 'Info.html?gid=' + dataInfo[i].GoodsId + '">' + dataInfo[i].GoodsName + '</a><span>￥' + parseInt(dataInfo[i].MemberPrice) + priceUnit + '</span></p>' +
            '<p class="Status"><span class="dkg">' + dataInfo[i].JobStatus + '</span><b>' + dataInfo[i].CategoryName + '</b></p>' +
            '<p class="OrderAdd">' +
                '<span>' + dataInfo[i].JobAddress + '</span><span>起止日期：' + getFormatDate(new Date(dataInfo[i].JobStartTime)) + '至' + getFormatDate(new Date(dataInfo[i].JobEndTime)) + '</span>' +
            '</p>' +
            '<p class="OrderBtn">' +
                btn +
            '</p>' +
        '</li>';
    }

    $('#myTab0_Content0').html(htmlStr);
}

function showAcList1(dataInfo) {
    var htmlStr = '';
    for (var i = 0; i < dataInfo.length; i++) {
        var type = '';
        var priceUnit = '';
        if (dataInfo[i].JobType == 'QB') {
            type = dataInfo[i].JobNeeds;
            priceUnit = '元';
        } else {
            type = '点工';
            priceUnit = '元/天';
        }
        var typeStr = dataInfo[i].JobType == 'DG' ? 'People' : '';
        var btn = '';
        if (dataInfo[i].commentId) {
            btn = '<input type="button" value="已评价" class="Edit" />';
        } else {
            btn = '<input type="button" value="评价" class="Comment" onclick="location.href=\'Evaluate.html?evalWorker=0&flag=' + dataInfo[i].JobType + '&orderId=' + dataInfo[i].OrderId + '&goodsId=' + dataInfo[i].GoodsId + '\'">';
        }

        htmlStr += '<li>' +
            '<p class="OrderName"><a href="Task' + typeStr + 'Info.html?gid=' + dataInfo[i].GoodsId + '">' + dataInfo[i].GoodsName + '</a><span>￥' + parseInt(dataInfo[i].MemberPrice) +priceUnit+ '</span></p>' +
            '<p class="Status"><span class="dkg">' + dataInfo[i].JobStatus + '</span><b>' + dataInfo[i].CategoryName + '</b></p>' +
            '<p class="OrderAdd">' +
                '<span>' + dataInfo[i].JobAddress + '</span><span>起止日期：' + getFormatDate(new Date(dataInfo[i].JobStartTime)) + '至' + getFormatDate(new Date(dataInfo[i].JobEndTime)) + '</span>' +
            '</p>' +
            '<p class="OrderBtn">' +
                btn +
            '</p>' +
        '</li>';
    }

    $('#myTab0_Content1').html(htmlStr);
}


function goodsRecordList() {
    var goodsId = getQueryString('gid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/RecordHandler.ashx",
        data: { flag: "listbygoods",goodsId:gid },
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
            $('#signListUl').html(htmlStr);
        }
    });
}

function DeletePrice(priceId,obj) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/OfferPriceHandler.ashx",
        data: { flag: "delete", priceId: priceId },
        dataType: "text",
        async: false,
        success: function (data) {
            $(obj).parent().parent().hide();
        }
    });
}

function AcceptOrder(priceId, confirm) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/OfferPriceHandler.ashx",
        data: { flag: "confirm", priceId: priceId, confirm: confirm },
        dataType: "text",
        async: false,
        success: function (data) {
            location.href = location.href;
        }
    });
}

function OfferPrice() {
    gid = getQueryString("gid");
    if (CheckUserLogin() == false) {
        location.href = "Login.html?url=" + escape("OfferPrice.html?gid=" + gid);
        return; 
    }
    location.href = 'OfferPrice.html?gid=' + gid;
}

function showFilter() {
    $('#Scn').show();
}

function cutStringByInput(id, len) {
    var inputStr = $("#"+id).val();
    if (inputStr.length > len) {
        $("#" + id).val(inputStr.substring(0,len));
    }
}

function getAuthStep() {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/AuthHandler.ashx",
        data: { flag: "isauth" },
        dataType: "text",
        async: false,
        success: function (data) {
            if (data != "1") {
                location.href = 'Identification.html';
            }
        }
    });
}

//LBS定位
var addressDetail = '';
function success(position) {
    showPosition(position);
}

function error() {

}

var options = {};

var showPosition = function (position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    //var map = new BMap.Map("container");   // 创建Map实例
    var point = new BMap.Point(lon, lat); // 创建点坐标
    var gc = new BMap.Geocoder();
    gc.getLocation(point, function (rs) {
        var addComp = rs.addressComponents;
        addressDetail = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        $('#jobAddress').val(addressDetail);
    });
};
