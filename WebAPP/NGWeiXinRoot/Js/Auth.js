function fileAdd(fileName,formName) {
    var isFront = fileName.indexOf('1') > 0;
    $("#" + formName).ajaxSubmit({
        success: function (redata) {
            var result = redata.split('_$$_');
            if (result.length == 1) {
                alert(result[0]);
            }
            else if (result.length == 2) {
                $('#img' + fileName).html('<a href="javascript:delFile(\'' + fileName + '\',' + isFront + ')" class="Delete"></a><img id="imgFile' + (isFront ? "1" : "2") + '" src="' + result[1] + '">');
            } else {
                alert('添加失败');
            }
        },
        error: function (error) {
            document.write(error.responseText);
        },
        url: '/ServiceInterface/AuthHandler.ashx?flag=upload&isfront=' + isFront, /*设置post提交到的页面*/
        type: "post", /*设置表单以post方法提交*/
        dataType: "text", /*设置返回值类型为文本*/
        uploadProgress: function (event, position, total, percentComplete) {

        },
        complete: function (xhr) {

        }
    });

}

function delFile(fileName, isFront) {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/AuthHandler.ashx",
        data: { flag: "delpic", isfront: isFront },
        dataType: "text",
        async: false,
        success: function (data) {
            $('#img' + fileName).html('');
            $('#' + fileName).val('');
        }
    });
}

function getInfo() {
    $.ajax({
        type: "post",
        url: "/ServiceInterface/authHandler.ashx",
        data: { flag: 'info' },
        dataType: "text",
        async: false,
        success: function (data) {
            try {
                var authInfo = eval(data);
                if (authInfo.length > 0) {
                    $('#trueName').val(authInfo[0].TrueName);
                    $('#idCard').val(authInfo[0].PersonIDCode);

                    $('#imgupFile1').html('<a href="javascript:delFile(\'upFile1\',true)" class="Delete"></a><img id="imgFile1" src="' + authInfo[0].applyPic + '">');
                    $('#imgupFile2').html('<a href="javascript:delFile(\'upFile2\',false)" class="Delete"></a><img id="imgFile2" src="' + authInfo[0].applyPic2 + '">');
                }
            }
            catch (ex) { }
        }
    });
}

function addAuthInfo() {
    if (!$('#imgFile1')[0].src || !$('#imgFile2')[0].src) {
        alert('请选择图片');
        return;
    }
    if ($('#trueName').val() && $('#idCard').val()) {
        $.ajax({
            type: "post",
            url: "/ServiceInterface/AuthHandler.ashx",
            data: { flag: "addinfo", trueName: $('#trueName').val(), idCard: $('#idCard').val() },
            dataType: "text",
            async: false,
            success: function (data) {
                if (data == '1') {
                    alert('提交成功！');
                    location.href = "index.html";
                }
                else if (data == '0') {
                    alert('提交失败');
                }
                else {
                    alert(data);
                }

            }
        });
    } else {
        alert('请填写完整信息');
    }
}


