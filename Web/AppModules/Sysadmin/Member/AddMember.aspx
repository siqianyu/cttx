<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMember.aspx.cs" Inherits="AddMember" %>

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
            if ($("#txtMemberName").val().trim() == "") {
                alert("请输入会员账号！");
                return false;
            }
            if ($("#txtName").val() == "") {
                alert("请输入用户名！");
                return false;
            }
            if ($("#txtPwd").val() == "") {
                alert("请输入密码！");
                return false;
            }
            if ($("#txtTel").val() == "") {
                alert("请输入联系电话！");
                return false;
            }
            if ($("#txtXHName").val() == "") {
                alert("请输入协会名称！");
                return false;
            }
            if ($("#txtTrueName").val() == "") {
                alert("请输入联系人！");
                return false;
            }
            if ($("#txtAddress").val() == "") {
                alert("请输入地址！");
                return false;
            }
            if ($("#ddlMemberType").val() == "0" || $("#ddlMemberType").val() == "") {
                alert("请选择所属行业！");
                return false;
            }
        }

        function CheckXZForm() {
            if ($("#txtXZMember").val().trim() == "") {
                alert("请输入会员账号！");
                return false;
            }
            if ($("#txtXZName").val() == "") {
                alert("请输入用户名！");
                return false;
            }
            if ($("#txtXZPwd").val() == "") {
                alert("请输入密码！");
                return false;
            }
            if ($("#txtXZTel").val() == "") {
                alert("请输入联系电话！");
                return false;
            }
            if ($("#txtXZType").val() == "") {
                alert("请输入部门名称！");
                return false;
            }
            if ($("#txtXZTrueName").val() == "") {
                alert("请输入联系人！");
                return false;
            }
        }
        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <asp:Panel ID="panel_xh" runat="server" Visible="False">
        <table cellpadding="0" cellspacing="1" class="ViewBox">
            <tr>
                <td class="Ltd">
                    会员账号：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtMemberName" runat="server" CssClass="input_add" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    协会名称：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXHName" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    初始密码：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtPwd" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    联系人：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtTrueName" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    联系电话：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtTel" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    地址：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    所属行业：
                </td>
                <td class="Rtd">
                    <%--<asp:TextBox ID="txtMemberType" runat="server" CssClass="input_add" Width="200px" />--%>

                     <asp:CheckBoxList ID="ddlMemberType" runat="server" RepeatDirection="Horizontal"  CssClass="input_add" RepeatColumns="5">
                        </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    备注：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="input_add" Width="200px" />
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
    </asp:Panel>
    <asp:Panel ID="panel_xz" runat="server" Visible="False">
        <table cellpadding="0" cellspacing="1" class="ViewBox">
            <tr>
                <td class="Ltd">
                    会员账号：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXZMember" runat="server" CssClass="input_add" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    单位名称：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXZName" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    部门名称：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXZType" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    初始密码：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXZPwd" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    联系人：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXZTrueName" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    联系电话：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXZTel" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    备注：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtXZRemark" runat="server" CssClass="input_add" Width="200px" />
                </td>
            </tr>
            <tr class="ButBox">
                <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                    align="center">
                    <asp:Button runat="server" ID="Button2" CssClass="Submit" ToolTip="确定" OnClientClick="return CheckXZForm();"
                        OnClick="btnSubmit_Click" Text="确定" BorderWidth="0" Width="135px" Height="32px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" class="Return" value="返回" style="border: 0px; width: 135px;
                        height: 32px;" onclick="layer_close()" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
