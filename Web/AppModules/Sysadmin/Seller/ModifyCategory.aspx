<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyCategory.aspx.cs" Inherits="AddMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员添加</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <style>
        .red
        {
            color: Red;
            padding-left: 10px;
        }
        #btnYes
        {
            width: 135px;
            height: 32px;
            border: 0;
            cursor: pointer;
        }
        .Ltd
        {
            width:8%;
        }
    </style>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script>
        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                任务类别：
            </td>
            <td class="Rtd">
                <asp:CheckBoxList ID="catelist" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr class="ButBox">
            <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                align="center">
                <asp:Button ID="btnYes" runat="server" Text="确定" OnClick="Button1_Click" CssClass="Submit" />
                <%--<input type="button" value="确定" id="btnYes" onclick="addSeller()" class="Submit"
                    style="width: 135px; height: 32px; border: 0; cursor: pointer;" />--%>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="Return" value="返回" style="border: 0px; width: 135px;
                    height: 32px;" onclick="layer_close()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
