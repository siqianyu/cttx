<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Push.aspx.cs" Inherits="NGWeiXinRoot_Push" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="推送消息" onclick="Button1_Click" />
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="284px" TextMode="MultiLine" 
            Width="551px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
