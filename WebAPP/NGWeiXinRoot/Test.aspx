<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="NGWeiXinRoot_Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="获取通用AccessToken" 
            onclick="Button1_Click" />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="1208px" Height="39px"></asp:TextBox>
    
    <br />
            <asp:Button ID="Button2" runat="server" Text="获取通用NGJSApiTicket" 
            onclick="Button2_Click"  />
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="1208px" Height="39px"></asp:TextBox>

            <br />
            <asp:Button ID="Button3" runat="server" Text="生成JSApiTicket签名" 
            onclick="Button3_Click"  />
        <br />
        <asp:TextBox ID="TextBox3" runat="server" Width="1208px" Height="193px" 
            TextMode="MultiLine"></asp:TextBox>
    </div>
    </form>
</body>
</html>
