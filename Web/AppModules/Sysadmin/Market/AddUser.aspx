<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AppModules_IACenter_AddUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>市场管理员添加</title>
    <link href="/skin/blue/css/style.css" type="text/css" rel="stylesheet" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <style>
        .ViewBox .Ltd
        {
            width: 10%;
        }
    </style>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script language="javascript">
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
    <!--div层父窗口交互控制_end-->
</head>
<body>
    <form runat="server">
    <table cellpadding="0" cellspacing="1" class="ViewBox">
       
        <tr>
            <td class="Ltd">
                用户名：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txtusername" runat="server"
                    style="height: 16px" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                密码：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txtpwd" runat="server" style="height: 16px" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                真实姓名：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txttruename" runat="server"
                    style="height: 16px" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                性别：
            </td>
            <td class="Rtd">
                <select id="Ssex" runat="server">
                    <option value="">请选择</option>
                    <option value="男">男</option>
                    <option value="女">女</option>
                </select>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                年龄：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txtage" runat="server" style="height: 16px"
                    value="0" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                电话：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txttel" runat="server" style="height: 16px" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                手机：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txtsj" runat="server" style="height: 16px" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                传真：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txtcz" runat="server" style="height: 16px" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                工作描述：
            </td>
            <td class="Rtd">
                <asp:TextBox runat="server" ID="txtJob" TextMode="MultiLine" Width="324px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                排序：
            </td>
            <td class="Rtd">
                <input name="input3" type="text" class="input_5" id="txtsort" runat="server" style="height: 16px"
                    value="0" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                启用：
            </td>
            <td class="Rtd">
                <asp:CheckBox ID="chkqy" runat="server" Checked="true" />
            </td>
        </tr>
        <tr style="display: none">
            <td class="Ltd">
                设为超级用户：
            </td>
            <td class="Rtd">
                <asp:CheckBox ID="chkadmin" runat="server" Checked="false" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
            </td>
            <td class="Rtd">
                <asp:ImageButton ID="btnSave" ImageUrl="~/Images/skin/SubmitA1.png" runat="server"
                    CssClass="save" OnClick="btnSave_Click" OnClientClick="return checkForm()" />
                &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/skin/ReturnA1.png" runat="server"
                    CssClass="save" OnClientClick="layer_close_refresh()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
