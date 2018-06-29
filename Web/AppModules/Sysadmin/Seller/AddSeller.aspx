<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSeller.aspx.cs" Inherits="AddMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员添加</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/uploadify.css" rel="stylesheet" type="text/css" />
    <style>
        .red
        {
            color: Red;
            padding-left: 10px;
        }
        #category label
        {
            font-size: 15px;
            padding: 10px;
        }
    </style>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script>

        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }

        //获取数据的URL地址
        function createObjectURL(blob) {
            if (window.URL) {
                return window.URL.createObjectURL(blob);
            } else if (window.webkitURL) {
                return window.webkitURL.createObjectURL(blob);
            } else {
                return null;
            }
        }


        //实时显示图片
        function showImg() {

            var pic = document.getElementById("topicImg");
            var file = document.getElementById("txtCharter");
            var ext = file.value.substring(file.value.lastIndexOf(".") + 1).toLowerCase();
            // gif在IE浏览器暂时无法显示
            if (ext != 'png' && ext != 'jpg' && ext != 'jpeg') {
                return;
            }
            var isIE = !!window.ActiveXObject; // 判断是否IE浏览器
            var ie6 = isIE && !window.XMLHttpRequest; //判断是否IE6
            if (window.FileReader) {
                // 如果支持HTML5的FileReader接口（chrome,firefox7+,opera,IE10,IE9）

                var file = file.files[0];
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var pic = document.getElementById("topicImg");
                    pic.src = this.result;
                }
            }
            else if (isIE) {
                // 不支持HTML5（IE），IE6特殊处理，IE9(移除焦点，解决因安全限制拒绝访问的问题)

                file.select();
                document.getElementById("td_imgPreview").focus(); //解决上传到服务器上后，特殊情况下ie9还是拒绝访问的问题
                var reallocalpath = document.selection.createRange().text;
                if (ie6) {
                    // IE6浏览器设置img的src为本地路径可以直接显示图片
                    pic.src = reallocalpath;
                } else {
                    // 非IE6版本的IE由于安全问题直接设置img的src无法显示本地图片，但是可以通过滤镜来实现
                    pic.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src=\"" + reallocalpath + "\")";
                    // 设置img的src为base64编码的透明图片 取消显示浏览器默认图片
                    pic.src = 'data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==';
                }
            }
            else if (file.files) {
                // firefox6
                if (file.files.item(0)) {
                    url = file.files.item(0).getAsDataURL();
                    pic.src = url;
                }
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                用户名：
            </td>
            <td class="Rtd">
                <input type="text" id="txtUserName" onchange="checkUserName();" /><span class="red">*</span>
                <span class="red" id="txtAlert"></span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                密码：
            </td>
            <td class="Rtd">
                <input type="text" id="txtPwd" /><span class="red">*</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                公司名称：
            </td>
            <td class="Rtd">
                <input type="text" id="txtCompany" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                店铺名称：
            </td>
            <td class="Rtd">
                <input type="text" id="txtShopName" /><span class="red">*</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                联系人：
            </td>
            <td class="Rtd">
                <input type="text" id="txtLinkMan" /><span class="red">*</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                手机号码：
            </td>
            <td class="Rtd">
                <input type="text" id="txtPhone" maxlength="11" /><span class="red">*</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                所在区域：
            </td>
            <td class="Rtd">
                <select id="ddlTopArea" onchange="loadSecArea()">
                </select>
                <select id="ddlSecArea" onchange="loadThirdArea()" style="display: none;">
                </select>
                <select id="ddlThirdArea" onchange="loadMarket()" style="display: none;">
                </select>
                <span class="red">*</span>
            </td>
        </tr>
        <tr id="tr" style="display: none;">
            <td class="Ltd">
                所在农贸市场：
            </td>
            <td class="Rtd">
                <select id="txtMarket">
                </select>
                <span class="red">*</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                任务分类：
            </td>
            <td class="Rtd" id="category">
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                详细地址：
            </td>
            <td class="Rtd">
                <input type="text" id="txtAddress" /><span class="red">*</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                是否为直营商家：
            </td>
            <td class="Rtd">
                <input id="rbYes" type="radio" name="own" checked />
                <label for="rbYes">
                    是</label>
                <input id="rbNo" type="radio" name="own" />
                <label for="rbNo">
                    否</label>
            </td>
        </tr>
        <tr class="ButBox">
            <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                align="center">
                <input type="button" id="btnYes" value="确定" onclick="addSeller()" class="Submit"
                    style="width: 135px; height: 32px; border: 0; cursor: pointer;" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="Return" value="返回" style="border: 0px; width: 135px;
                    height: 32px;" onclick="layer_close()" />
            </td>
        </tr>
    </table>
    </form>
</body>
<script>

    (function () {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=data",
            success: function (res) {
                if (res != "") {
                    $("#ddlTopArea").html(res);
                }
            },
            error: function (error) {
                //  alert(error);
            }
        });

        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "json",
            data: "flag=cate",
            success: function (res) {
                if (res && res.length > 0) {
                    var html = "";
                    for (var i = 0; i < res.length; i++) {
                        var p = res[i];
                        html += "<input type='checkbox' id='" + p.id + "'><label for='" + p.id + "'>" + p.name + "</label>";
                    }
                    document.getElementById("category").innerHTML = html;

                }
            },
            error: function (error) {
                //  alert(error);
            }
        });
    })();

    function checkUserName() {
        var name = document.getElementById("txtUserName").value.replace(/(^\s+)|(\s+$)/, "");
        if (name !== "") {
            $.ajax({
                type: "GET",
                url: "SellerService.ashx",
                dataType: "text",
                data: "flag=checkname&name=" + name,
                success: function (res) {
                    if (res === "1") {
                        document.getElementById("txtAlert").innerHTML = "该用户已存在";
                        document.getElementById("btnYes").style.display = "none";
                    }
                    else {
                        document.getElementById("txtAlert").innerHTML = "";
                        document.getElementById("btnYes").style.display = "inline-block";
                    }
                },
                error: function (error) {
                    //  alert(error);
                }
            });
        }
    }

    function loadSecArea() {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=data&areaid=" + document.getElementById("ddlTopArea").value,
            success: function (res) {
                if (res != "") {
                    $("#ddlSecArea").html(res);
                    document.getElementById("ddlSecArea").style.display = "inline-block";

                }
            },
            error: function (error) {
                //  alert(error);
            }
        });
    }

    function loadThirdArea() {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=data&areaid=" + document.getElementById("ddlSecArea").value,
            success: function (res) {
                if (res != "") {
                    $("#ddlThirdArea").html(res);
                    document.getElementById("ddlThirdArea").style.display = "inline-block";
                }
            },
            error: function (error) {
                //  alert(error);
            }
        });
    }

    function loadMarket() {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=market&areaid=" + document.getElementById("ddlThirdArea").value,
            success: function (res) {
                if (res != "") {
                    $("#txtMarket").html(res);
                   // document.getElementById("txtMarket").innerHTML = res;
                    document.getElementById("tr").style.display = "table-row";
                }
            },
            error: function (error) {
                //  alert(error);
            }
        });
    }

    function addSeller() {
        var name = document.getElementById("txtUserName").value.replace(/(^\s+)|(\s+$)/, "");
        if (name === "") {
            alert("用户名不能为空");
            return false;
        }
        var pwd = document.getElementById("txtPwd").value.replace(/(^\s+)|(\s+$)/, "");
        if (pwd === "") {
            alert("密码不能为空");
            return false;
        }

        var cname = document.getElementById("txtCompany").value.replace(/(^\s+)|(\s+$)/, "");
        if (cname === "") {
            alert("公司名称不能为空");
            return false;
        }

        var sname = document.getElementById("txtShopName").value.replace(/(^\s+)|(\s+$)/, "");
        if (sname === "") {
            alert("店铺名称不能为空");
            return false;
        }
        var man = document.getElementById("txtLinkMan").value.replace(/(^\s+)|(\s+$)/, "");
        if (man === "") {
            alert("联系人不能为空");
            return false;
        }
        var phone = document.getElementById("txtPhone").value.replace(/(^\s+)|(\s+$)/, "");
        if (phone === "") {
            alert("手机号码不能为空");
            return false;
        }
        else {
            var reg = /^1\d{10}$/;
            if (!reg.test(phone)) {
                alert("手机号码格式不正确，请重新输入");
                return false;
            }
        }

        var area = document.getElementById("ddlThirdArea").value.replace(/(^\s+)|(\s+$)/, "");
        if (area === "0" || area === "") {
            alert("所在区域不能为空");
            return false;
        }

        var market = document.getElementById("txtMarket").value.replace(/(^\s+)|(\s+$)/, "");
        if (market === "0" || market === "") {
            alert("所属农贸市场不能为空");
            return false;
        }

        var ckList = "";
        var list = document.getElementById("category").getElementsByTagName("input");
        for (var i = 0; i < list.length; i++) {
            var p = list[i];
            if (p.checked) {
                ckList += p.id + "$";
            }
        }

        if (ckList === "") {
            alert("请选择任务分类");
            return false;
        }

        var address = document.getElementById("txtAddress").value.replace(/(^\s+)|(\s+$)/, "");
        if (address === "") {
            alert("详细地址不能为空");
            return false;
        }

        var isOwn = document.getElementById("rbYes").checked ? "1" : "0";
        $.ajax({
            type: "POST",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=apply&name=" + escape(name) + "&pwd=" + escape(pwd) + "&cname=" + escape(cname) + "&sname=" + escape(sname) + "&man=" + escape(man) + "&phone=" + phone + "&area=" + escape(area) + "&market=" + document.getElementById("txtMarket").value + "&address=" + escape(address) + "&clist=" + escape(ckList) + "&isown=" + isOwn,
            success: function (res) {
                alert(res);
                layer_close();
            },
            error: function (error) {

            }
        });
    }

</script>
</html>
