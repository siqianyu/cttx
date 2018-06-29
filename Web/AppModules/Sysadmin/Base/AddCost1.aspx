<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCost1.aspx.cs" Inherits="AppModules_Sysadmin_Base_AddCost1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function check() {
            var name = $("#txtValue").val();
            if (name == "" || name == null) {
                alert("项目不能为空!");
                return false;
            }
        }

        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.freshCurrentPage(); //执行当前列表页的刷新事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
        //js获取地址栏中的参数
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }   
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div id="right">
        <div class="applica_title">
            <br />
            <h4>
                <%-- <%=_pageTitle %>--%>
            </h4>
        </div>
        <div class="applica_di">
            <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                    <td class="Ltd">
                        名称：
                    </td>
                    <td class="Rtd">
                        <asp:Literal ID="txtname" runat="server"></asp:Literal>
                    </td>
                </tr>
               <%-- <tr>
                 <td class="Ltd">
                        <span style="color: #ff0000">*</span>项目序号：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtsysnumber" runat="server" Width="200px" CssClass="Enter" MaxLength="100" Visible="true"></asp:TextBox>
                        <asp:Literal ID="txtID" runat="server" Visible="false"> </asp:Literal>
                    </td></tr>--%>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>项目：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtValue" runat="server" Width="200px" CssClass="Enter" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>价格：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtPrice" runat="server" Width="200px" CssClass="Enter" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>是否默认：
                    </td>
                    <td class="Rtd">
                     <asp:CheckBoxList runat="server" ID="CheckDefault">
                     <asp:ListItem Selected="True" Text="是" Value="1">是</asp:ListItem>
                     <asp:ListItem Text="否" Value="0">否</asp:ListItem>
                     </asp:CheckBoxList>
                    </td>
                </tr>
                <tr class="ButBox">
                    <td colspan="2" style="height: 30px; padding-top: 4px; background-color: #f6f6f6;"
                        align="center">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="确定" Text="确定"
                            BorderWidth="0" Width="135px" Height="32px" OnClientClick="return check()" OnClick="btnSubmit_Click" />
                        <asp:Button runat="server" ID="Button1" CssClass="Return" ToolTip="返回" OnClientClick="layer_close_refresh()"
                            Text="返回" BorderWidth="0" Width="135px" Height="32px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
