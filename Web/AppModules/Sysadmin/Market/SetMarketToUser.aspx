<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetMarketToUser.aspx.cs"
    Inherits="AppModules_IACenter_AddUser" %>

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
                <asp:DropDownList ID="ddlMarket" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                管理员：
            </td>
            <td class="Rtd">
                <asp:DropDownList ID="ddlMarketUser" runat="server" Width="200px">
                </asp:DropDownList>
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
