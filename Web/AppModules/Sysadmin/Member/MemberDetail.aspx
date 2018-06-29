<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberDetail.aspx.cs" Inherits="MemberDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员详细信息</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script>
        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                会员名：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litUserName"></asp:Literal>
            </td>
            <td class="Ltd" width="100">
                会员类别：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litMemberType"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                现金账户：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litbuyMoneyAccount"></asp:Literal>&nbsp;
            </td>
            <td class="Ltd">
                赠送账户：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litfreeMoenyAccount"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                所在地区：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litAreaName"></asp:Literal>
            </td>
            <td class="Ltd">
                企业类别：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litmemberCompanyType"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                单位名称：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litmemberCompanyName"></asp:Literal>
            </td>
            <td class="Ltd">
                机构代码：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litmemberCompanyCode"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                真实姓名：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litmemberTrueName"></asp:Literal>
            </td>
            <td class="Ltd">
                性别：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litSex"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                联系地址：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litaddress"></asp:Literal>&nbsp;
            </td>
            <td class="Ltd">
                密码：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litPassword"></asp:Literal>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                联系电话：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litPhone"></asp:Literal>&nbsp;
            </td>
            <td class="Ltd">
                手机号码：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litMobile"></asp:Literal>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                传真号码：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litFax"></asp:Literal>&nbsp;
            </td>
            <td class="Ltd">
                邮政编码：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litPost"></asp:Literal>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                E-mail：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litEmail"></asp:Literal>&nbsp;
            </td>
            <td class="Ltd">
                注册时间：
            </td>
            <td class="Rtd">
                <asp:Literal runat="server" ID="litRegDate"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                企业类型：
            </td>
            <td class="Rtd" colspan="3">
                <asp:CheckBoxList ID="ddlMemberType" runat="server" RepeatDirection="Horizontal"
                    CssClass="input_add" RepeatColumns="5">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                审核标示：
            </td>
            <td class="Rtd">
                <asp:RadioButtonList runat="server" ID="rdlshFlag" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">通过</asp:ListItem>
                    <asp:ListItem Value="0">未通过</asp:ListItem>
                </asp:RadioButtonList>
                &nbsp;
            </td>
            <td class="Ltd">
                会员状态：
            </td>
            <td class="Rtd">
                <asp:RadioButtonList runat="server" ID="rdlmemberStatus" RepeatDirection="Horizontal">
                    <asp:ListItem Value="ZC">正常</asp:ListItem>
                    <asp:ListItem Value="JY">禁用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                审核时间：
            </td>
            <td class="Rtd">
                <%--  <asp:TextBox ID="txtshTime" runat="server" Width="100px" onFocus="this.blur()" CssClass="input_add"></asp:TextBox>&nbsp;--%>
                <input id="txtshTime" runat="server" onclick="WdatePicker()" class="input_add" style="width: 200px" />&nbsp;
                <img style="cursor: pointer;" src="../../../Images/calendar.gif" /><br />
                <span style="color: #ff0000">注：审核时间请不要超出当前时间。</span>
            </td>
            <td class="Ltd">
                审核人：
            </td>
            <td class="Rtd">
                <asp:TextBox runat="server" ID="txtshPerson"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                未通过审核原因：
            </td>
            <td class="Rtd" colspan="3">
                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtunPassReason" Width="317px"></asp:TextBox>
            </td>
        </tr>
        <tr class="ButBox">
            <td style="height: 30px; padding-top: 4px; background-color: #f6f6f6;" align="center"
                colspan="4">
                <%--  <asp:Button runat="server" ID="Button2" CssClass="bottom_btn" ToolTip="确定" Text="确定"
                    BorderWidth="0" OnClick="Button2_Click" />
                <asp:Button runat="server" ID="Button1" CssClass="bottom_btn" ToolTip="返回" OnClientClick="javascript:history.go(-1); return false;"
                    Text="返回" BorderWidth="0" />--%>
                <asp:Button runat="server" ID="Button2" CssClass="Submit" ToolTip="确定" OnClientClick="return Check();"
                    OnClick="Button2_Click" Text="确定" BorderWidth="0" Width="135px" Height="32px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="Return" value="返回" style="border: 0px; width: 135px;
                    height: 32px;" onclick="layer_close()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
