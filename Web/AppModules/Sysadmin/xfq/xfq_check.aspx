<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xfq_check.aspx.cs" Inherits="AppModules_xfq_xfq_check" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>消费券验证</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Check() {

        }
        $(document).ready(function () {
            var css = parent.document.all.cssfile.href;
            document.all.ChildcssFile.href = css;
            var js = parent.document.all.jsCss.src;
            document.all.ChildJsCss.src = js;
            window.setInterval("window.parent.setHeight()", 1000);
        });
        function detail() {
            window.open("/sysadmin/member/tcinfo.aspx?levid=" + $("#stc").val());
        }
        function CheckForm() {
            //var val=$("input[name=RadioButtonList1][checked]").val();
            var val = $("input[name='RadioButtonList1']:checked").val();
            if (val == undefined) {
                alert("请先选择套餐！");
                return false;
            }
            if (!/^[0-9]*[0-9][0-9]*$/.test($("#txttg").val().trim())) {
                alert("请输入正确的可托管标准数！");
                return false;
            }
            if (!/^[0-9]*[0-9][0-9]*$/.test($("#txtxz").val().trim())) {
                alert("请输入正确的可下载标准数！");
                return false;
            }
            if ($("#txtStart").val().trim() == "") {
                alert("请选择有效期!");
                return false;
            }
            if ($("#txtEnd").val().trim() == "") {
                alert("请选择有效期!");
                return false;
            }
            var arry = val.split("$");
            if (parseInt($("#txttg").val().trim()) > parseInt(arry[0])) {
                alert("可托管标准数超过了套餐上限，请选择更大的套餐！");
                return false;
            }
            if (parseInt($("#txtxz").val().trim()) > parseInt(arry[1])) {
                alert("可下载标准数超过了套餐上限，请选择更大的套餐！");
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
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                消费券编号：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtid" runat="server" CssClass="input_add"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                消费券密码：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtpw" runat="server" CssClass="input_add" />
            </td>
        </tr>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <tr>
                <td class="Ltd">
                    是否已使用：
                </td>
                <td class="Rtd">
                    <asp:Literal ID="lituse" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    消费券过期时间：
                </td>
                <td class="Rtd">
                    <asp:Literal ID="litgqsj" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    金额：
                </td>
                <td class="Rtd">
                    <asp:Literal ID="litje" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    会员名：
                </td>
                <td class="Rtd">
                    <asp:Literal ID="lithym" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    公司名称：
                </td>
                <td class="Rtd">
                    <asp:Literal ID="litgsmc" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    会员有效期：
                </td>
                <td class="Rtd">
                    <asp:Literal ID="lityx" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    套餐：
                </td>
                <td class="Rtd">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    可托管标准数：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txttg" runat="server" CssClass="input_add"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    可下载标准数：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtxz" runat="server" CssClass="input_add"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    会员有效期：
                </td>
                <td class="Rtd">
                 <%--   <asp:TextBox ID="txtStart" Columns="10" onFocus="this.blur();" runat="server" CssClass="input_add"></asp:TextBox>&nbsp;<img
                        onclick="meizz_calendar(document.getElementById('txtStart'))" style="cursor: pointer;"
                        src="../image/calendar.gif" />--%>

                        <input id="txtStart" runat="server" onclick="WdatePicker()" class="input_add" style="width: 200px" />&nbsp;
                <img  style="cursor: pointer;" src="../../../Images/calendar.gif" />
                    到
                   <%-- <asp:TextBox ID="txtEnd" Columns="10" onFocus="this.blur();" runat="server" CssClass="input_add"></asp:TextBox>&nbsp;<img
                        onclick="meizz_calendar(document.getElementById('txtEnd'))" style="cursor: pointer;"
                        src="../image/calendar.gif" />--%>
                        <input id="txtEnd" runat="server" onclick="WdatePicker()" class="input_add" style="width: 200px" />&nbsp;
                <img  style="cursor: pointer;" src="../../../Images/calendar.gif" />
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Visible="false">
            <tr>
                <td class="Ltd">
                    &nbsp;
                </td>
                <td class="Rtd">
                    <span style="color: red;">无此消费券！</span>
                </td>
            </tr>
        </asp:Panel>
        <tr class="ButBox">
            <td colspan="2" style="height: 30px; padding-top: 14px; background-color: #f6f6f6;"
                align="center">
                <%--<asp:Button runat="server" ID="btnSubmit" CssClass="bottom_btn" OnClick="btnSubmit_Click"
                    CommandArgument="0" Text="下一步" BorderWidth="0" />--%>
                <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="下一步" OnClick="btnSubmit_Click" CommandArgument="0"
                    Text="下一步" BorderWidth="0" Width="135px" Height="32px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
