function commonResume() {
    var dataInfo = '';
    $.ajax({
        type: "post",
        url: "/ServiceInterface/ResumeHandler.ashx",
        data: { flag: 'info' },
        dataType: "text",
        async: false,
        success: function (data) {
            dataInfo = data;
        }
    });
    return dataInfo;
}

var g_comment_page = 0;
function getWorkerCommentList() {
    var mid = getQueryString('mid');
    $.ajax({
        type: "get",
        url: "/ServiceInterface/ResumeHandler.ashx",
        data: { flag: 'comment', mid: mid, isWorker: '1', page: g_comment_page },
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

function getResume() {
    var data = commonResume();
    try {
        var infoArray = data.split('_$$_');
        var resumeInfo = eval(infoArray[0])[0];
        var resumePic = eval(infoArray[1]);

        $("#workYearSel").val(resumeInfo.Specialty);
        $("#categoryName").val(resumeInfo.Skill);
        $("#addressTxt").val(resumeInfo.AddressDetail);
        $("#addressCode").val(resumeInfo.AddressCode);
        $("#selfIntroTxt").val(resumeInfo.SelfIntroduction);

        //图片
        for (var i = 0; i < resumePic.length; i++) {
            $('#divImgs').prepend('<span id="' + resumePic[i].applyId + '"><img src="' + resumePic[i].applyPic + '"><a href="javascript:delResumeFile(\'' + resumePic[i].applyId + '\')" class="Delete"></a></span>');
        }

        var aid = getQueryString('aid');
        var aname = unescape(getQueryString('aname'));
        if (aid) {
            getTempResume();
            $("#addressTxt").val(aname);
            $("#addressCode").val(aid);
        } else {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(successResume, errorResume, optionsResume);
                $("#addressCode").val('');
            }
        }
    }
    catch (ex) { }
}

function getResumeDetail() {
    var dataInfo = '';
    var mid = getQueryString('mid');
    $.ajax({
        type: "post",
        url: "/ServiceInterface/ResumeHandler.ashx",
        data: { flag: 'detail', mid: mid },
        dataType: "text",
        async: false,
        success: function (data) {
            dataInfo = data;
        }
    });
    try {
        var infoArray = dataInfo.split('_$$_');
        var resumeInfo = eval(infoArray[0])[0];
        var resumePic = eval(infoArray[1]);

        $("#headImg")[0].src = resumeInfo.headimg;
        $("#nameB").html(resumeInfo.TrueName + '<i style=\'display:none;\'>（' + resumeInfo.LevelName + '）</i>');
        $("#citySpan").html('现居 ' + resumeInfo.city);
        $("#ageI").html(resumeInfo.Age);
        $("#workYearSel").val(resumeInfo.Specialty);
        $("#skillTxt").val(resumeInfo.Skill);
        $("#selfIntroTxt").val(resumeInfo.SelfIntroduction);

        if (resumeInfo.Skill) {
            $('#skillBtn').show();
        }
        if (resumeInfo.ifpass && resumeInfo.ifpass == '1') {
            $('#authBtn').show();
        }

        //图片
        $('#picLi').html('');
        for (var i = 0; i < resumePic.length; i++) {
            $('#picLi').append('<img src="' + resumePic[i].applyPic + '" class="WorkPieceImg">');
        }
    }
    catch (ex) { }
}


function addResume() {

    if ($('#addressTxt').val() && $('#selfIntroTxt').val()) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/ResumeHandler.ashx",
            data: { flag: 'add', workYear: $('#workYearSel').val(), skill: $('#categoryName').val(), addressCode: $('#addressCode').val(), addressTxt: $('#addressTxt').val(), selfIntro: $('#selfIntroTxt').val() },
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
                    var returnUrl = getQueryString('returnUrl');
                    if (returnUrl) {
                        location.href = returnUrl;
                    } else {
                        location.href = 'ResumeInfo.html';
                    }
                }

            }
        });
    } else {
        alert('请填写地址信息和自我评价');
    }
}

function addResumeFile() {
    if ($('#divImgs').children().length >= 7) {
        alert('最多只能上传6张图片');
    } else {
        $("#resumeForm").ajaxSubmit({
            success: function (redata) {
                var result = redata.split('_$$_');
                if (result.length == 1) {
                    alert(result[0]);
                }
                else if (result.length == 2) {
                    $('#divImgs').prepend('<span id="' + result[0] + '"><img src="' + result[1] + '"><a href="javascript:delResumeFile(\'' + result[0] + '\')" class="Delete"></a></span>');
                } else {
                    alert('添加失败');
                }
            },
            error: function (error) {
                document.write(error.responseText);
            },
            url: '/ServiceInterface/ResumeHandler.ashx?flag=uppic', /*设置post提交到的页面*/
            type: "post", /*设置表单以post方法提交*/
            dataType: "text", /*设置返回值类型为文本*/
            uploadProgress: function (event, position, total, percentComplete) {

            },
            complete: function (xhr) {

            }
        });
    }
}

function delResumeFile(fileid) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/ResumeHandler.ashx",
        data: { flag: "delpic", id: fileid },
        dataType: "text",
        async: false,
        success: function (data) {
            $('#' + fileid).remove();
        }
    });
}

function chooseCity() {
    var tempVal = $('#workYearSel').val() + '_$$_' + $('#categoryName').val() + '_$$_' + $('#selfIntroTxt').val();
    $.cookie('tempResume', tempVal, { expires: 1, path: '/' });
    location.href = 'CityList.html';
}

function getTempResume() {
    var tempVal = $.cookie('tempResume');
    var infoArray = tempVal.split('_$$_');
    if (infoArray.length >= 3) {
        $('#workYearSel').val(infoArray[0]);
        $('#categoryName').val(infoArray[1]);
        $('#selfIntroTxt').val(infoArray[2]);
    }
}


//LBS定位
var addressDetailResume = '';
function successResume(position) {
    showPositionResume(position);
}

function errorResume() {
}

var optionsResume = {};

var showPositionResume = function (position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    //var map = new BMap.Map("container");   // 创建Map实例
    var point = new BMap.Point(lon, lat); // 创建点坐标
    var gc = new BMap.Geocoder();
    gc.getLocation(point, function (rs) {
        var addComp = rs.addressComponents;
        addressDetailResume = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        $('#addressTxt').val(addressDetailResume);
    });
};