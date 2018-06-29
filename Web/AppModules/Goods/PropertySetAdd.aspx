<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PropertySetAdd.aspx.cs" Inherits="AppModules_Goods_PropertySetAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
<table class="AddBox Left ViewBox" cellpadding="0" cellspacing="1" border="0"> 
                <tr>
                    <td class="Ltd">
                        属性名称：</td>
                    <td class="Rtd">
                        <input name="input3" type="text"  class="Enter" id="txtName" runat="server" style="width:429px; height:20px;"  /><span
                            class="gray">&nbsp;</span></td>
                </tr>
                <tr>
                    <td class="Ltd">
                        属性显示形式：</td>
                    <td class="Rtd" >
                        <asp:RadioButtonList ID="rdFlag" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="仅显示给客户" Value="input"  runat="server" Selected=True></asp:ListItem>
                        <asp:ListItem Text="可供客户选择规格" runat="server"  Value="select"></asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                
                <tr>
                    <td class="Ltd">
                        属性值：</td>
                    <td class="Rtd" >
                        <asp:TextBox runat="server" ID="txtValues" TextMode="MultiLine" Height="105px" Width="429px" CssClass="TxtArea"></asp:TextBox>
                        多个属性值用","号隔开</td>
                </tr>
                <tr>
                    <td class="Ltd">
                        排序：</td>
                    <td class="Rtd" >
                        <input name="input3" type="text"  class="Enter" id="txtOrder" runat="server" value="0" style="height:20px" /><span
                            class="gray">&nbsp;</span></td>
                </tr>
                <tr>
                    <td class="Ltd">
                        备注：</td>
                    <td class="Rtd" >
                        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="55px" Width="429px" CssClass="TxtArea"></asp:TextBox>
                        </td>
                </tr>
                   <tr><td colspan="2" style="position:relative;height:50px;" class="Ltd">
                   <p class="AddBut">
                <asp:Button ID="btnSave"  BorderWidth="1" BorderColor="#d25938" runat="server" Width="120" Height="36"  CssClass="AddSubmit" Text="提 交" style="font-size:14px; font-weight:bold;" OnClick="btnSave_Click" OnClientClick="return checkForm()"  />
                <asp:Button ID="Button1" BorderWidth="1" BorderColor="#cfcfcf" runat="server" Width="120" Height="36"  CssClass="AddCancel" Text="返 回" style="font-size:14px; font-weight:bold;" OnClientClick="history.go(-1);return false;"  />
                 </p></td></tr>
            </table>
    </form>
</body>
</html>
