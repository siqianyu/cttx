<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMsgTemp.aspx.cs" Inherits="AppModules_Sysadmin_ShortMsg_AddMsgTemp" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加供应</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/effect.js" type="text/javascript"></script>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/Validator.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function Check() {
            var txtCode = document.getElementById("<%=txtCode.ClientID %>");
        }



        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.grid_search(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post">
        <div id="right" style="width: 100%; margin: 0 auto; font-family: 微软雅黑;">
            <div class="box" style="font-size: 18px; font-family: 微软雅黑; text-align: center; padding-bottom: 20px;">
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox" id="table_datalist" style="width: 100%; line-height: 30px;">
                     <tr>
                        <td class="Ltd">供应商：
                        </td>
                        <td class="Rtd">
                            <asp:DropDownList ID="ddlSupplierCategory" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">标识：
                        </td>
                        <td class="Rtd">
                            <asp:DropDownList ID="ddlParentCategory" runat="server">
                                <asp:ListItem Value="reg" Text="reg"></asp:ListItem>
                                <asp:ListItem Value="login" Text="login"></asp:ListItem>
                                <asp:ListItem Value="findpwd" Text="findpwd"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span class="red">*</span>code：
                        </td>
                        <td class="Rtd">
                            <asp:TextBox ID="txtCode" runat="server" Width="200px" CssClass="input_add"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span class="red">*</span>Param：
                        </td>
                        <td class="Rtd">
                            <asp:TextBox ID="txtParam" TextMode="MultiLine" Rows="5" runat="server" Width="200px" CssClass="input_add"></asp:TextBox>
                        </td>
                    </tr>

                    <%--
                        
                
                <tr>
                    <td class="Ltd">
                        添加时间：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtAddDate" runat="server" Width="100px" onFocus="WdatePicker()" ReadOnly="true"
                            CssClass="input_add"></asp:TextBox>&nbsp; <span class="red">注：发布时间请不要超出当前时间。</span>
                    </td>
                </tr>--%>
                    <tr class="ButBox">
                        <td colspan="2" style="height: 30px; padding-top: 4px; background-color: #f6f6f6;">
                            <asp:Button runat="server" ID="btnSave" CssClass="Submit" ToolTip="确定" Text="确定"
                                BorderWidth="0" Width="135px" Height="32px" OnClick="btnSave_Click" OnClientClick="return Check()" />
                            &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="Button1" CssClass="Return" ToolTip="返回" OnClientClick="layer_close_refresh()" Text="返回" BorderWidth="0" Width="135px" Height="32px" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
