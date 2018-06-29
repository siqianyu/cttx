<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLink.aspx.cs" Inherits="AppModules_Link_AddLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加友情链接</title>
     <link href="../../Style/Common.css" rel="stylesheet" />
    <link href="../../Style/PopUp.css" rel="stylesheet" />

    <script  type="text/javascript" src="../../My97DatePicker/WdatePicker.js"></script>

    <!--div层父窗口交互控制_start(代码的位置必须在body前面)-->
    <script type="text/javascript">
        //关闭当前层(返回按钮)
        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }

        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.grid_search(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
    <!--div层父窗口交互控制_end-->
</head>

<body>
    <form id="form1" runat="server" >
 <table class="ModTab" cellpadding="0" cellspacing="1">
        <tr>
            <td class="Ltd">显示方式 ：</td>
            <td class="Rtd">
                  <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1" onclick="changeType(this.value)">文字</asp:ListItem>
                                <asp:ListItem Value="0" onclick="changeType(this.value)">图片</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="lbDisplayMode" runat="server" />
            </td>

            <td class="Ltd"> 上传图片 ：</td>
            <td class="Rtd">
                                          <input id="picUpload" runat="server" name="picUpload" type="file" onpropertychange="showPic(this,'uploadimage')" />
                            <asp:Image ID="uploadimage" runat="server" ImageUrl="~/Sysadmin/image/nopic.jpg" Height="50px"
                                Width="90px" />
                           <%-- <span style="color: #ff0000">上传文件类型仅限 jpg | jpeg | bmp | gif 格式</span>--%>
                            <asp:Label ID="lbHideLink" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="Ltd">类别名称：</td>
            <td class="Rtd">

                   <asp:DropDownList ID="DrCategory" runat="server"> </asp:DropDownList>
            </td>
            <td class="Ltd">链接名称 ：</td>
            <td class="Rtd">
                <asp:TextBox ID="txtTitle" runat="server" Width="300px" CssClass="input_add" MaxLength="35" />
            </td>
        </tr>


        <tr>
            <td class="Ltd">链接地址 ：</td>
            <td class="Rtd">
                <asp:TextBox ID="txtLink" runat="server" Width="300px" Text="http://"  CssClass="input_add" MaxLength="100"/>
            </td>
            <td class="Ltd">排序 ：</td>
            <td class="Rtd">
                <asp:TextBox ID="txtSort" runat="server" Width="60px" CssClass="input_add" MaxLength="10" />
            </td>
        </tr>


        <tr>
            <td colspan="4" class="ButBox">
                <p>
                    <asp:LinkButton ID="LinkButton1" runat="server" class="Submit" title="保存" 
                        onclick="LinkButton1_Click" >保 存</asp:LinkButton><a
                        href="Javascript:layer_close_refresh();void(0);" class="Return" title="返回">返&nbsp;回</a></p>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfanalyse_is_certify" runat="server" />
    </form>
</body>
</html>