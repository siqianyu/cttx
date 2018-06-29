<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMember_Year.aspx.cs" Inherits="AddMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员添加</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function CheckForm() {
            if ($("#txtjgdm").val().trim() == "") {
                alert("请输入组织机构代码！");
                return false;
            }
            if ($("#txtname").val() == "") {
                alert("请输入用户名！");
                return false;
            }
            if ($("#txtpw").val() == "") {
                alert("请输入密码！");
                return false;
            }
            if (Sysadmin_Member_addmember.checkUsername($("#txtname").val().trim()).value == "1") {
                alert("该用户已存在！");
                return false;
            }
        }

        //        $(document).ready(function () {
        //            var css = parent.document.all.cssfile.href;
        //            document.all.ChildcssFile.href = css;
        //            var js = parent.document.all.jsCss.src;
        //            document.all.ChildJsCss.src = js;
        //            window.setInterval("window.parent.setHeight()", 1000);
        //        });

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
                组织机构代码：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtjgdm" runat="server" CssClass="input_add" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                用户名：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtname" runat="server" CssClass="input_add" Width="200px" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                密码：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtpw" TextMode="Password" runat="server" CssClass="input_add" Width="200px" />
            </td>
        </tr>
        <tr class="ButBox">
            <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                align="center">
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
