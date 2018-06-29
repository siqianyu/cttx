<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPayRecord.aspx.cs" Inherits="AppModules_Sysadmin_Member_AddMemberRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员充值信息添加</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script>
        function CheckForm() {
            var name = $("#txtName");
            if (name.val() == null || name.val() == "") {
                alert("请选择会员名称！");
                name.focus();
                return false;
            }
            var money = $("#txtMoney");
            if (money.val() == null || money.val() == "") {
                alert("请填写充值金额！");
                money.focus();
                return false;
            }

            var montype = $("#ddlMoneyType");
            if (montype.val() == null || montype.val() == "") {
                alert("请选择充值类型！");
                montype.focus();
                return false;
            }
            return true;
        }
        $(document).ready(function () {
            var css = parent.document.all.cssfile.href;
            document.all.ChildcssFile.href = css;
            var js = parent.document.all.jsCss.src;
            document.all.ChildJsCss.src = js;
        });

        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }

        function Select_Member() {
            var URL = "ChooseMember.aspx?r=" + Math.random();
            var returnValue = window.showModalDialog(URL, self, "edge:raised:1;help:0;resizable:1;dialogWidth:800px;dialogHeight:500px;");
            if (returnValue != null) {
                document.getElementById("<%=txtName.ClientID %>").value = returnValue;
                document.getElementById("<%=txtMemberId.ClientID %>").value = returnValue;
            }
        }
    </script>
</head>
<body runat="server">
    <form id="Form1" method="post" runat="server">
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                <span style="color: #ff0000">*</span> 会员名称：
            </td>
            <td class="Rtd">
                <input type="hidden" id="txtMemberId" runat="server" />
                <asp:TextBox ID="txtName" ReadOnly="true" runat="server" Width="150px" CssClass="input_add"
                    MaxLength="20" />
                <img id="btnChoice" src="../../../Images/checkmember.jpg" style="cursor: pointer;"
                    align="middle" onclick="Select_Member();" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                <span style="color: #ff0000">*</span> 充值金额：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtMoney" runat="server" Width="300px" CssClass="input_add" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                <span style="color: #ff0000">*</span> 充值类型：
            </td>
            <td class="Rtd">
                <asp:DropDownList runat="server" ID="ddlMoneyType">
                    <asp:ListItem Value="" Selected="True">-请选择-</asp:ListItem>
                    <asp:ListItem Value="XJ">现金</asp:ListItem>
                    <asp:ListItem Value="ZS">赠送</asp:ListItem>
                    <asp:ListItem Value="XFQ">消费券</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                备注说明：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtRemarks" runat="server" Width="300px" Height="120px" CssClass="input_add"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                添加时间：
            </td>
            <td class="Rtd">
                <%-- <asp:TextBox ID="txtReleaseDate" runat="server" Width="200px" onFocus="this.blur()"
                    CssClass="input_add"></asp:TextBox>
                <img onclick="meizz_calendar(document.getElementById('txtReleaseDate'))" style="cursor: pointer;"
                    src="../image/calendar.gif" align="middle" /><br />
                --%>
                <input id="txtReleaseDate" runat="server" onclick="WdatePicker()" class="input_add"
                    style="width: 200px" />&nbsp;
                <img style="cursor: pointer;" src="../../../Images/calendar.gif" />
                <span style="color: #ff0000">注：发布时间请不要超出当前时间。</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                添加人：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtAddPerson" runat="server" Width="300px" CssClass="input_add"
                    ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <asp:Panel runat="server" ID="palSH" Visible="false">
            <tr>
                <td class="Ltd">
                    审核状态：
                </td>
                <td class="Rtd">
                    <asp:Literal runat="server" ID="ltlshFlag"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    审核人：
                </td>
                <td class="Rtd">
                    <asp:Literal runat="server" ID="ltlshPerson"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    审核时间：
                </td>
                <td class="Rtd">
                    <asp:Literal runat="server" ID="ltlshTime"></asp:Literal>
                </td>
            </tr>
        </asp:Panel>
        <tr class="ButBox">
            <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                align="center">
                <%--  <asp:Button runat="server" ID="btnSubmit" CssClass="bottom_btn" ToolTip="确定" OnClientClick="return CheckForm();"
                    OnClick="btnSubmit_Click" Text="确定" BorderWidth="0" />
                <asp:Button runat="server" ID="Button1" CssClass="bottom_btn" ToolTip="返回" OnClientClick="javascript:history.go(-1); return false;"
                    Text="返回" BorderWidth="0" />--%>
                <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="确定" OnClientClick="return CheckForm();"
                    OnClick="btnSubmit_Click" Text="确定" BorderWidth="0" Width="135px" Height="32px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="Return" value="返回" style="border: 0px; width: 135px;
                    height: 32px;" onclick="layer_close()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
