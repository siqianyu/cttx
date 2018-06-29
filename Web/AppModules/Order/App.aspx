<%@ Page Language="C#" AutoEventWireup="true" CodeFile="App.aspx.cs" Inherits="AppModules_Order_App" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    用户手机号：<asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
    用户ID：<asp:TextBox ID="txtMemberId" runat="server"></asp:TextBox>
    市场ID：<asp:TextBox ID="txtMarketId" runat="server" value='1000000001'></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="用户下单前确认" onclick="Button1_Click" 
        style="height: 21px" />


    <br />
    <br />
    <asp:Button ID="Button2" runat="server" Text="首页数据接口" onclick="Button2_Click" />
    <br />
    <br />
    <br />
    <asp:Button ID="Button3" runat="server" Text="市场全部任务TOP" 
        onclick="Button3_Click" />
    <br />
    <br />
    <asp:Button ID="Button4" runat="server" Text="某个任务详情" onclick="Button4_Click" />任务ID：<asp:TextBox 
        ID="txtGoodsID" runat="server" Width="101px"></asp:TextBox>
&nbsp;<br />


    </form>
</body>
</html>
