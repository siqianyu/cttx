function getWords() {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: 'getwords' },
        dataType: "text",
        async: false,
        success: function (data) {
            var dataInfo = eval(data);
            var htmlStr = '';
            for (var i = 0; i < dataInfo.length; i++) {
                htmlStr += '<a href="javascript:;" class="Impression ImNo">' + dataInfo[i].word + '</a>';
            }
            $('#liWords').html(htmlStr);
        }
    });
}


function finish() {
    if (confirm('确定验收通过吗？') == false) {return false;}
    var orderId = getQueryString('oid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: 'finish', orderId: orderId },
        dataType: "text",
        async: false,
        success: function (data) {
            location.href = 'MyOrder.html?listtype=2';
        }
    });
}

function getFinished() {
    var goodsId = getQueryString('gid');
    var orderId = getQueryString('oid');
    var mid = getQueryString('mid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: 'getinfo', goodsId: goodsId,orderId:orderId, mid: mid },
        dataType: "text",
        async: false,
        success: function (data) {
            var infoArray = data.split('_$$_');
            var finishInfo = eval(infoArray[0])[0];
            var finishPic = eval(infoArray[1]);

            $('#infoTxt').val(finishInfo.CompleteInfo);
            for (var i = 0; i < finishPic.length; i++) {
                $('#divImgs').prepend('<span id="' + finishPic[i].sysnumber + '"><img src="' + finishPic[i].picPath + '"><a href="javascript:delFile(\'' + finishPic[i].sysnumber + '\')" class="Delete"></a></span>');
            }
        }
    });
}

function getFinishInfo() {
    var goodsId = getQueryString('gid');
    var orderId = getQueryString('oid');
    var mid = getQueryString('mid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: 'getinfo', goodsId: goodsId,orderId:orderId, mid: mid },
        dataType: "text",
        async: false,
        success: function (data) {
            var infoArray = data.split('_$$_');
            var finishInfo = eval(infoArray[0])[0];
            var finishPic = eval(infoArray[1]);

            $('#infoTxt').val(finishInfo.CompleteInfo);
            for (var i = 0; i < finishPic.length; i++) {
                $('#divImgs').prepend('<span id="' + finishPic[i].sysnumber + '"><img src="' + finishPic[i].picPath + '"></span>'); 
            }
        }
    });
}

function fillFinishInfo() {
    var isback = getQueryString('isback');
    if (isback) {
        alert('该订单已被验收');
        return false;
    }

    var infoTxt = $('#infoTxt').val();
    var orderId = getQueryString('oid');
    var goodsId = getQueryString('gid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: 'complete', info: infoTxt, goodsId: goodsId, orderId: orderId },
        dataType: "text",
        async: false,
        success: function (data) {
            if (data == '1') {
                location.href = 'MyHome.html';
            } else {
                alert('提交失败');
            }
        }
    });
}

function fileAdd() {
    var goodsId = getQueryString('gid');
    var orderId = getQueryString('oid');
    $("#taskForm").ajaxSubmit({
        success: function (redata) {
            var result = redata.split('_$$_');
            if (result.length == 1) {
                alert(result[0]);
            }
            else if (result.length == 2) {
                $('#divImgs').prepend('<span id="' + result[0] + '"><img src="' + result[1] + '"><a href="javascript:delFile(\'' + result[0] + '\')" class="Delete"></a></span>');
            } else {
                alert('上传失败');
            }
        },
        error: function (error) {
            document.write(error.responseText);
        },
        url: '/ServiceInterface/FinishHandler.ashx?flag=upload&orderId=' + orderId + '&goodsId=' + goodsId, /*设置post提交到的页面*/
        type: "post", /*设置表单以post方法提交*/
        dataType: "text", /*设置返回值类型为文本*/
        uploadProgress: function (event, position, total, percentComplete) {

        },
        complete: function (xhr) {

        }
    });
}

function delFile(fileid) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: "delpic", id: fileid },
        dataType: "text",
        async: false,
        success: function (data) {
            $('#' + fileid).remove();
        }
    });
}

function evalUserInfo() {
    var evalWorker = getQueryString('evalWorker');
    var orderId = getQueryString('orderId');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: "userinfo", orderId: orderId, evalWorker: evalWorker },
        dataType: "text",
        async: false,
        success: function (data) {
            var dataInfo = eval(data);
            if (dataInfo.length > 0) {
                $('#nameB').html(dataInfo[0].TrueName);
                $('#levelNameI').html(dataInfo[0].LevelName);
                $('#headImg')[0].src = dataInfo[0].HeadImg;
                $('#addressSpan').html(dataInfo[0].AddressDetail);
            }
        }
    });
}

function fillComment(isPub) {
    var evalWorker = getQueryString('evalWorker');
    var flag = getQueryString('flag'); 
    var orderId = getQueryString('orderId');
    var goodsId = getQueryString('goodsId');
    var words = $('#commentTxt').val();
    if (words.length > 10) {
        alert('请不要超过十个汉字');
        return false;
    }

    $.ajax({
        type: "post",
        url: "/ServiceInterface/FinishHandler.ashx",
        data: { flag: "comment", orderId: orderId, goodsId: goodsId, isPub: isPub, words: words, stars: starNum, evalWorker: evalWorker },
        dataType: "text",
        async: false,
        success: function (data) {
            if (data == '1') {
                if (evalWorker == "0") {
                    if (flag == "DG") {
                        location.href = 'MyAcceptTaskPeopleList.html';
                    } else {
                        location.href = 'MyAcceptTaskList.html';
                    }
                } else {
                    location.href = 'MyOrder.html';
                }
            } else {
                alert("保存失败");
            }
        }
    });
}

var starNum = 5;
function AddStars(num) {
    starNum = num;
    $('#starSpan').html('');
    for (var i = 1; i <= 5; i++) {
        if (i <= num) {
            $('#starSpan').append('<img src="Images/StarY.png" onclick="AddStars('+i+')">');
        } else {
            $('#starSpan').append('<img src="Images/StarG.png" onclick="AddStars(' + i + ')">');
        }
    }
}