<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePwd.aspx.cs" Inherits="AppModules_Sysadmin_Member_UpdatePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>密码修改</title>
    <link href="../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="PosiBar" style="background: url(../../../Images/PosiBg.png) repeat; margin-bottom:5px;">
        <p style="background: url(../../../Images/PosIco.png) 13px no-repeat">
            密码修改
        </p>
    </div>
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                原始密码：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtOrignalPwd" runat="server" CssClass="input_add" Width="200px"
                    TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                新密码：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtNewPwd" runat="server" CssClass="input_add" Width="200px" TextMode="Password" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                确认密码：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtReNewPwd" runat="server" CssClass="input_add" Width="200px" TextMode="Password" />
            </td>
        </tr>
        <tr class="ButBox">
            <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                align="center">
                <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="确定" OnClientClick="return CheckForm();"
                    OnClick="btnSubmit_Click" Text="确定" BorderWidth="0" Width="135px" Height="32px" />
            </td>
        </tr>
    </table>
    </form>
    <script>
        function CheckForm() {
            var txtNewPwd = document.getElementById("txtNewPwd");
            var txtReNewPwd = document.getElementById("txtReNewPwd");
            var txtOrignalPwd = document.getElementById("txtOrignalPwd");
            if (txtOrignalPwd.value === "") {
                alert("原始密码不能为空");
                return false;
            }
            if (txtNewPwd.value === "") {
                alert("新密码不能为空");
                return false;
            }
            else {
                if (txtNewPwd.value.length < 6) {
                    alert("新密码长度不能少于6位");
                    return false;
                }
            }
            if (txtReNewPwd.value === "") {
                alert("确认密码不能为空");
                return false;
            }
            else {
                if (txtReNewPwd.value != txtNewPwd.value) {
                    alert("确认密码与新密码不一致");
                    return false;
                }
            }
            return true;
        }
    </script>
</body>
</html>
