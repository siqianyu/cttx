<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestShortMessage.aspx.cs" Inherits="AppModules_Sysadmin_Base_TestShortMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table cellpadding="0" cellspacing="1" class="ViewBox">
            <tr>
                <td class="Ltd">
                    <span style="color: #ff0000">*</span>手机号码：</td>
                <td class="Rtd">
                    <asp:TextBox ID="txtPhone"  runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" />

                </td>
            </tr>
                        <tr>
                <td class="Ltd">
                    <span style="color: #ff0000"></td>
                <td class="Rtd">
                    <asp:Button ID="btSend" runat="server" Text="发送短信" onclick="btSend_Click" />

                </td>
            </tr>

           <tr>
<%--                <td class="Ltd">
                    <span style="color: #ff0000">*</span>验证码：</td>
                <td class="Rtd">
                    <asp:TextBox ID="txtYZM" ReadOnly="true" runat="server" Width="200px" 
                        CssClass="Enter" style="color:Gray;" MaxLength="100" TextMode="MultiLine" 
                        Height="113px" />
                </td>--%>
           </tr>
    </table>
    </div>
    </form>
</body>
</html>
