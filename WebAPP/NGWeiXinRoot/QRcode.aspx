<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRcode.aspx.cs" Inherits="NGWeiXinRoot_QRcode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="微信推广二维码" 
            onclick="Button1_Click" />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="202px" TextMode="MultiLine" 
            Width="363px"></asp:TextBox>
    </div>
             <div runat="server" id="div_img"></div>
    </form>
</body>
</html>
