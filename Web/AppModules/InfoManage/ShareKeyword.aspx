<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShareKeyword.aspx.cs" Inherits="AppModules_InfoManage_ShareKeyword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/PopUp.css" rel="stylesheet" type="text/css" />
</head>
<body style='1140px'>
    <form id="form1" runat="server">
    <div class="PosiBar">
        <p>新闻分享设置</p>
    </div>
    <br />
    <p>&nbsp;</p>
    <table class='ModTab' cellpadding='0' cellspacing='1' style='width:800px;'>
<%--    <tr style=''><td class='Ltd'  <%--style='width:250px;padding:5px;border-bottom-style:solid;border-color:#BBB;border-width:1px;'>分享类型 </td> <td class='Rtd' <%--style='width:350px;border-bottom-style:solid;border-color:#BBB;border-width:1px;'>要匹配的关键字<span style='color:Red'>（以逗号隔开）</span></td><td class='Rtd' <%-- style='width:100px;border-bottom-style:solid;border-color:#BBB;border-width:1px;'>同步</td></tr>--%>
        <tr style=''><td class='Ltd' style='width:300px;'>分享类型 </td> <td class='Rtd' style='width:300px;'>要匹配的关键字<span style='color:Red'>（以逗号隔开）</span></td><td class='Rtd' style='width:100px;'>同步</td></tr>

    <tr><td colspan='3' class='Ltd' style='text-align:left;'>分享到平台</td></tr>

		

    <tr><td class='Ltd'>服务业标准化</td><td class='Rtd'><asp:TextBox ID="TextBox1" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button1" runat="server" Text="同  步" onclick="Button1_Click" /></td></tr>
    
    <tr><td class='Ltd'>工业标准化</td><td class='Rtd'><asp:TextBox ID="TextBox2" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button2" runat="server" Text="同  步" onclick="Button2_Click" /></td></tr>
    
    <tr><td class='Ltd'>农业标准化</td><td class='Rtd'><asp:TextBox ID="TextBox3" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button3" runat="server" Text="同  步" onclick="Button3_Click" /></td></tr>

    <tr><td class='Ltd'>社会公共管理</td><td class='Rtd'><asp:TextBox ID="TextBox4" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button4" runat="server" Text="同  步" onclick="Button4_Click" /></td></tr>

        <tr><td colspan='3' class='Ltd' style='border-style:solid;border-width:1px;border-color:#BBB;text-align:left;'>分享到专题</td></tr>


    <tr><td class='Ltd'>LED</td><td class='Rtd'><asp:TextBox ID="TextBox5" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button5" runat="server" Text="同  步" onclick="Button5_Click" /></td></tr>

    <tr><td class='Ltd'>纺织服装</td><td class='Rtd'><asp:TextBox ID="TextBox6" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button6" runat="server" Text="同  步" onclick="Button6_Click" /></td></tr>

    <tr><td class='Ltd'>纺织服装标签</td><td class='Rtd'><asp:TextBox ID="TextBox7" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button7" runat="server" Text="同  步" onclick="Button7_Click" /></td></tr>

    <tr><td class='Ltd'>食品与农产品</td><td class='Rtd'><asp:TextBox ID="TextBox8" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button8" runat="server" Text="同  步" onclick="Button8_Click" /></td></tr>

    <tr><td class='Ltd'>食品标签</td><td class='Rtd'><asp:TextBox ID="TextBox9" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button9" runat="server" Text="同  步" onclick="Button9_Click" /></td></tr>

    <tr><td class='Ltd'>玩具</td><td class='Rtd'><asp:TextBox ID="TextBox10" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button10" runat="server" Text="同  步" onclick="Button10_Click" /></td></tr>

    <tr><td class='Ltd'>电动工具</td><td class='Rtd'><asp:TextBox ID="TextBox11" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button11" runat="server" Text="同  步" onclick="Button11_Click" /></td></tr>

    <tr><td class='Ltd'>通讯设备</td><td class='Rtd'><asp:TextBox ID="TextBox12" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button12" runat="server" Text="同  步" onclick="Button12_Click" /></td></tr>

    <tr><td class='Ltd'>电动汽车</td><td class='Rtd'><asp:TextBox ID="TextBox13" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button13" runat="server" Text="同  步" onclick="Button13_Click" /></td></tr>

        <tr><td colspan='3' class='Ltd' style='border-style:solid;border-width:1px;border-color:#BBB;text-align:left;'>分享到市场</td></tr>


    <tr><td class='Ltd'>北美</td><td class='Rtd'><asp:TextBox ID="TextBox14" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button14" runat="server" Text="同  步" onclick="Button14_Click" /></td></tr>

    <tr><td class='Ltd'>欧洲</td><td class='Rtd'><asp:TextBox ID="TextBox15" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button15" runat="server" Text="同  步" onclick="Button15_Click" /></td></tr>

    <tr><td class='Ltd'>日韩</td><td class='Rtd'><asp:TextBox ID="TextBox16" runat="server" Width='320px'></asp:TextBox></td><td class='Rtd'>        
        <asp:Button ID="Button16" runat="server" Text="同  步" onclick="Button16_Click" /></td></tr>


    </table>

    </form>
</body>
</html>
