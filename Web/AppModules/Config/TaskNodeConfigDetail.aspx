<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskNodeConfigDetail.aspx.cs" Inherits="AppModules_Member_MemberDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Pragma" content="no-cache">
    <%
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    %>
    <title>环节配置</title>

    <style type="text/css">
        #cke_1_bottom {
            display: none;
        }

        .cateSelect {
            width: 100px;
        }
    </style>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        //关闭当前层(返回按钮)
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
<body onload="onload()">
    <form id="Form1" runat="server" method="post">
        <div id="right">
            <div class="applica_title">
                <br />
                <h4></h4>
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>环节序号：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 50px;" type="text" id="node_sn" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>环节名称：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 200px;" type="text" id="node_name" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>Y币值：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 200px;" type="text" id="yb_value" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>经验值和Y币的转化：
                        </td>
                        <td class="Rtd" colspan='3'>
                            Y币 * <input style="width: 50px;" type="text" id="jyz_value_set" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>才商指数和Y币的转化：
                        </td>
                        <td class="Rtd" colspan='3'>
                            Y币 * <input style="width: 50px;" type="text" id="cs_value_set" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>人气指数和Y币的转化：
                        </td>
                        <td class="Rtd" colspan='3'>
                            Y币 * <input style="width: 50px;" type="text" id="rq_value_set" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>参数配置：
                        </td>
                        <td class="Rtd" colspan='3' id="configTD">
                            <input type="hidden" id="node_para" runat="server" />
                            <input type="button" value="新增参数" onclick="AddParam()" />
                            <table id="para">
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>备注：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 200px;" type="text" id="remarks" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>是否启用：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:RadioButtonList ID="if_use" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class='Rtd ButBox' colspan='4'>
                            <input type="hidden" id="task_id" runat="server" />
                            <input type="hidden" id="node_id" runat="server" />
                            <asp:Button Text="提交" OnClientClick='return checkForm()' ID="btnSave" Style='border-width: 0px; height: 32px; width: 135px;'
                                CssClass="Submit" runat="server" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>


            </div>
        </div>

    </form>
</body>
<script type="text/javascript">
    function onload() {
        var jsonStr = $('#node_para').val();
        var json = eval('(' + jsonStr + ')');
        for (var key in json) {
            $('#para').append('<tr><td><input type="text" style="width:100px" name="paramKey" value="' + key + '" /> = <input type="text" style="width:100px" name="paramValue" value="' + json[key] + '" /></td></tr>');
        }
    }

    function checkForm() {
        var ids = ['node_sn','node_name'];
        for (var i = 0; i < ids.length; i++) {
            if (!$('#' + ids[i]).val()) {
                $('#' + ids[i]).focus();
                return false;
            }
        }

        var keys = $('input[name="paramKey"]');
        var values = $('input[name="paramValue"]');
        var json = {};
        for (var i = 0; i < keys.length; i++) {
            if (keys[i].value) {
                if (validJson(keys[i].value) && validJson(values[i].value)) {
                    json[keys[i].value] = values[i].value;
                } else {
                    alert("参数配置中请不要包含单引号、双引号、冒号、中括号、大括号");
                    return false;
                }
            }
        }
        $('#node_para').val(JSON.stringify(json));
        return true;
    }

    function validJson(val) {
        if (val.indexOf('\'') > -1 || val.indexOf('"') > -1 || val.indexOf(':') > -1 || val.indexOf('[') > -1 || val.indexOf(']') > -1 || val.indexOf('{') > -1 || val.indexOf('}') > -1) {
            return false;
        }
        return true;
    }

    function AddParam() {
        $('#para').append('<tr><td><input type="text" style="width:100px" name="paramKey" /> = <input type="text" style="width:100px" name="paramValue" /></td></tr>');
    }
</script>
</html>
