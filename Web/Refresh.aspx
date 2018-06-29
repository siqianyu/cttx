<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Refresh.aspx.cs" Inherits="Refresh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>刷新页面--用于不操作待机的情况</title>
    <script language="javascript">
        window.setTimeout("location.href='Refresh.aspx'",60000);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
    当前用户信息：
    <asp:Literal ID="ltUser" runat="server"></asp:Literal>
    </div>
    </div>
    </div>
    </form>
</body>
</html>
