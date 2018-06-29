function getUserInfo() {
    var dataInfo;
    $.ajax({
        type: "post",
        url: "/ServiceInterface/UserHandler.ashx",
        data: { flag: 'info' },
        dataType: "text",
        async: false,
        success: function (data) {
            dataInfo = eval(data);
        }
    });
    return dataInfo;
}

function logout() {
    $.cookie('memberInfo', '', { expires: -1, path: '/' });
    $.cookie('memberInfoMore', '', { expires: -1, path: '/' });
    location.href = 'Login.html';
}

function homeInfo() {
    var dataInfo = getUserInfo();
    if (dataInfo.length > 0) {
        $('#nameB').html(dataInfo[0].TrueName);
        $('#levelNameI').html(dataInfo[0].LevelName);
        $('#mobileA').html(dataInfo[0].Mobile);
        $('#headImg')[0].src = dataInfo[0].HeadImg;
        $('#addressSpan').html(dataInfo[0].AddressDetail);
    }
}

function userInfo() {
    var dataInfo = getUserInfo();
    if (dataInfo.length > 0) {
        $('#nameTxt').val(dataInfo[0].TrueName);
        $('#addressTxt').val(dataInfo[0].AddressDetail);
        $('#mobileTxt').val(dataInfo[0].Mobile);
        $('#headImg')[0].src = dataInfo[0].HeadImg;
    }
}

function fillUserInfo() {
    var trueName = $('#nameTxt').val();
    var phone = $('#mobileTxt').val();
    var address = $('#addressTxt').val();
    $.ajax({
        type: "post",
        url: "/ServiceInterface/UserHandler.ashx",
        data: { flag: 'fill', trueName: trueName, phone: phone, address: address },
        dataType: "text",
        async: false,
        success: function (data) {
            if (data == '1') {
                location.href = 'MyHome.html';
            } else {
                alert('保存失败');
            }
        }
    });
}

function fileAdd() {
    $("#authForm").ajaxSubmit({
        success: function (redata) {
            var result = redata.split('_$$_');
            if (result.length == 1) {
                alert(result[0]);
            }
            else if (result.length == 2) {
                $('#headImg')[0].src = result[1];
            } else {
                alert('上传失败');
            }
        },
        error: function (error) {
            document.write(error.responseText);
        },
        url: '/ServiceInterface/UserHandler.ashx?flag=upload', /*设置post提交到的页面*/
        type: "post", /*设置表单以post方法提交*/
        dataType: "text", /*设置返回值类型为文本*/
        uploadProgress: function (event, position, total, percentComplete) {

        },
        complete: function (xhr) {

        }
    });
}