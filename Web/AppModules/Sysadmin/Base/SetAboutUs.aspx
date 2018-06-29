<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAboutUs.aspx.cs" Inherits="AppModules_Sysadmin_Base_SetBToMPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #Save{ width:135px; height:32px; font-size:16px; color:#ab0b0b;text-shadow:0 1px #ffe8bb;background:url(../../../Images/SubmitN.png); position:relative; left:430px;top:-10px;}
    .Ltd{ width:20%;}
    .SelectBut{border:none;width:65px;height:25px;background:url(../../../Images/LogOutN.png);color:#9b0700;text-shadow:0 1px #ffe191;margin:0 10px;padding:0;text-align:center;cursor:pointer; }
    </style>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div id="right">
            <div class="applica_title">
            <br />
                <h4 style="display:none;">
                    关于我们
                </h4>
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                    <td class="Ltd">版本：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtVersion" runat="server" Width="200px" CssClass="Enter" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">内容：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtAboutUs" runat="server" TextMode="MultiLine" Width="600px" Height="200px" CssClass="Enter"></asp:TextBox>
                        </td>
                </tr>
               <tr class="ButBox">
                  	<td colspan="2" style="height:30px; padding-top:4px; background-color:#f6f6f6;" align="center">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="确定"
                             Text="确定" BorderWidth="0" Width="135px" Height="32px" 
                            onclick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
            </div>
        </div>
    </form>
</body>
</html>
