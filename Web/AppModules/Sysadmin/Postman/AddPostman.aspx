<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPostman.aspx.cs" Inherits="AppModules_Sysadmin_Postman_AddPostman" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>派送员添加</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
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
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script>
        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }

        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.grid_search(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="hideMarketid" type="hidden" />
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                派送员账号：
            </td>
            <td class="Rtd">
                <input id="name" onchange="checkname()" class="input_add" /><span class="red" id="txtAlert"></span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                密码：
            </td>
            <td class="Rtd">
                <input id="pwd" class="input_add" /><span class="red" id="txtPwdAlert" style="display: none">若需要修改密码则输入新密码，不输入则密码不变</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                真实姓名：
            </td>
            <td class="Rtd">
                <input id="tname" class="input_add" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                手机号码：
            </td>
            <td class="Rtd">
                <input id="tel" class="input_add" maxlength="11" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                农贸市场：
            </td>
            <td class="Rtd">
                <select id="ddlTopArea" onchange="loadSecArea()">
                </select>
                <select id="ddlSecArea" onchange="loadThirdArea()" style="display: none;">
                </select>
                <select id="ddlThirdArea" onchange="loadMarket()" style="display: none;">
                </select>
                <select id="marketid" runat="server">
                </select>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                账户状态：
            </td>
            <td class="Rtd">
                <input id="rbt_zc" type="radio" name="status" value="1" checked />
                <label for="rbt_zc">
                    正常</label>
                <input id="rbt_jy" type="radio" name="status" value="0" />
                <label for="rbt_jy">
                    禁用</label>
            </td>
        </tr>
        <tr class="ButBox">
            <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                align="center">
                <input type="button" id="btnPostmanApply" value="确定" onclick="postmanApply()" class="Submit"
                    style="width: 135px; height: 32px; border: 0; cursor: pointer;" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="Return" value="返回" style="border: 0px; width: 135px;
                    height: 32px;" onclick="layer_close()" />
            </td>
        </tr>
    </table>
    </form>
    <script>
        (function loadTopArea() {
            $.ajax({
                type: "GET",
                url: "PostmanHandler.ashx",
                dataType: "text",
                data: "flag=data",
                success: function (res) {
                    if (res != "") {
                        // document.getElementById("ddlTopArea").innerHTML = res; //IE下不能用innerHTML修改select
                        $("#ddlTopArea").html(res);
                    }
                },
                error: function (error) {
                    //  alert(error);
                }
            });
        })();

        function checkname() {
            var name = document.getElementById("name").value;
            $.ajax({
                type: "POST",
                url: "PostmanHandler.ashx",
                dataType: "text",
                data: { flag: "check", name: escape(name) },
                success: function (res) {
                    if (res === "1") {
                        document.getElementById("txtAlert").innerHTML = "该用户已存在!";
                        document.getElementById("btnPostmanApply").style.display = "none";
                    }
                    else if (res === "0") {
                        document.getElementById("txtAlert").innerHTML = "";
                        document.getElementById("btnPostmanApply").style.display = "inline-block";
                    }
                },
                error: function (error) {
                    // alert(error);
                }
            });
        }

        function loadSecArea() {
            $.ajax({
                type: "GET",
                url: "PostmanHandler.ashx",
                dataType: "text",
                data: "flag=data&areaid=" + document.getElementById("ddlTopArea").value,
                success: function (res) {
                    if (res != "") {
                        // document.getElementById("ddlSecArea").innerHTML = res;
                        $("#ddlSecArea").html(res);
                        document.getElementById("ddlSecArea").style.display = "inline-block";
                        if (GetQueryString("id") && GetQueryString("id") !== "") {
                            //  document.getElementById("marketid").innerHTML = "";
                            $("#marketid").html("");

                        }
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
                url: "PostmanHandler.ashx",
                dataType: "text",
                data: "flag=data&areaid=" + document.getElementById("ddlSecArea").value,
                success: function (res) {
                    if (res != "") {
                        //   document.getElementById("ddlThirdArea").innerHTML = res;
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
                url: "PostmanHandler.ashx",
                dataType: "text",
                data: "flag=market&areaid=" + document.getElementById("ddlThirdArea").value,
                success: function (res) {
                    if (res != "") {
                        $("#marketid").html(res);
                        // document.getElementById("marketid").innerHTML = res;
                    }
                },
                error: function (error) {
                    //  alert(error);
                }
            });
        }

        function postmanApply() {

            var name = document.getElementById("name").value.replace(/(^\s+)|(\s+$)/, "");
            var pwd = document.getElementById("pwd").value.replace(/(^\s+)|(\s+$)/, "");
            var tname = document.getElementById("tname").value.replace(/(^\s+)|(\s+$)/, "");
            var tel = document.getElementById("tel").value.replace(/(^\s+)|(\s+$)/, "");
            var marketid = document.getElementById("marketid").value.replace(/(^\s+)|(\s+$)/, "");
            var status = document.getElementById("rbt_zc").checked ? "1" : "0";


            if (name === "") {
                alert("派送员账号不能为空");
                return false;
            }

            if (!GetQueryString("id") && pwd === "") {
                alert("密码不能为空");
                return false;
            }

            if (tname === "") {
                alert("真实姓名不能为空");
                return false;
            }

            if (tel === "") {
                alert("手机号码不能为空");
                return false;
            }
            else {
                var reg = /^1\d{10}$/;
                if (!reg.test(tel)) {
                    alert("手机号码格式不正确，请重新输入");
                    return false;
                }
            }

            if (marketid === "0" || marketid === "") {
                alert("所属农贸市场不能为空");
                return false;
            }

            if (GetQueryString("id") && GetQueryString("id") !== "") {
                $.ajax({
                    type: "POST",
                    url: "PostmanHandler.ashx",
                    dataType: "text",
                    data: { flag: "update", postmanid: GetQueryString("id"), name: escape(name), pwd: escape(pwd), tname: escape(tname), tel: escape(tel), marketid: escape(marketid), status: status },
                    success: function (res) {
                        alert(res);
                        layer_close_refresh();
                    },
                    error: function (error) {

                    }
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "PostmanHandler.ashx",
                    dataType: "text",
                    data: { flag: "add", name: escape(name), pwd: escape(pwd), tname: escape(tname), tel: escape(tel), marketid: escape(marketid), status: status },
                    success: function (res) {
                        alert(res);
                        layer_close_refresh();
                    },
                    error: function (error) {
                        // alert(error);
                    }
                });
            }
        }

        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        //加载派送员信息
        function loadinfo() {
            if (GetQueryString("id") && GetQueryString("id") !== "") {
                $.ajax({
                    type: "POST",
                    url: "PostmanHandler.ashx",
                    dataType: "json",
                    data: { flag: "info", id: GetQueryString("id") },
                    success: function (res) {
                        if (res && res.length > 0) {
                            res = res[0];
                            document.getElementById("name").value = res.name;
                            document.getElementById("tname").value = res.tname;
                            document.getElementById("tel").value = res.tel;
                            document.getElementById("marketid").innerHTML = "<option value='" + res.marketid + "'>" + res.mname + "</option>";
                            document.getElementById("marketid").style.display = "inline-block";
                            document.getElementById("txtPwdAlert").style.display = "inline-block";
                            document.getElementById("hideMarketid").value = res.marketid;
                            if (res.status === "1") {
                                document.getElementById("rbt_zc").checked = true;
                            }
                            else {
                                document.getElementById("rbt_jy").checked = true;
                            }

                        }
                    },
                    error: function (error) {
                        // alert(error);
                    }
                });
            }
        }

        loadinfo();

    </script>
</body>
</html>
