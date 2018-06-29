<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CopyMenu.aspx.cs" Inherits="AppModules_IACenter_CopyMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="/css/style.css" type="text/css" rel="stylesheet" />
    <style>
    .btn_copy{ background:url("/Images/SubmitA.png"); border:none; outline:none; font-size:16px; line-height:32px; font-family:微软雅黑;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="tableList" style="width: 800px; margin: 0 auto; font-family: 微软雅黑; font-size: 16px;
        line-height: 35px;">
        <caption style="font-size: 20px; height: 30px; line-height: 30px; padding-top: 30px;
            padding-bottom: 20px; border-bottom: 2px solid #ccc">
            菜单复制
        </caption>
        <tr>
            <td style="text-align: right; padding-top: 10px;">
                源MenuId：
            </td>
            <td>
                <asp:TextBox runat="server" ID="ltSourceId" AutoPostBack="true" OnTextChanged="ltSourceId_TextChanged">0</asp:TextBox>[<asp:Literal
                    ID="Literal2" runat="server"></asp:Literal>]
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                目标MenuId：
            </td>
            <td>
                <asp:Literal ID="ltTargetId" runat="server"></asp:Literal>[<asp:Literal ID="Literal1"
                    runat="server"></asp:Literal>]<br />
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                说明：
            </td>
            <td>
                把“源MenuId”的子菜单copy到“目标MenuId”的子菜单中。
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="padding-top: 30px; text-align: center">
                <asp:Button ID="btnCopy" runat="server" Text="复 制" OnClick="btnCopy_Click" Height="32px"
                    Width="135px" CssClass="btn_copy" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
