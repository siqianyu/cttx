<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Send.aspx.cs" Inherits="NGWeiXinRoot_Send" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Width="505px"></asp:TextBox>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" Text="发送消息给单个用户" onclick="Button1_Click" />
        <br />
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Height="248px" TextMode="MultiLine" 
            Width="655px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
