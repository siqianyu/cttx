<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SavePic.aspx.cs" Inherits="NGWeiXinRoot_SavePic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    MediaId:<asp:TextBox ID="TextBox1" runat="server" Height="30px" 
            Width="890px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="保存图片" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
