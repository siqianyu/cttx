<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAd.aspx.cs" Inherits="AppModules_Sysadmin_Base_AddAd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        function check() {
            var title = $("#txtTitle").val();
            var link = $("#txtLink").val();
            var sort = $("#txtSort").val();
            if (title.length > 15) {
                alert("链接名称控制在15字以内!");
                return false;
            }
            if (link == "" || link == null) {
                alert("链接地址不能为空!");
                return false;
            }
            if (sort == "" || sort == null) {
                alert("排序不能为空!");
                return false;
            }
            else{
                var reg = /^\d+$/;
                if (!reg.test(sort)) {
                    alert("排序只能为数字!");
                    return false;
                }
            }
        }
        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.freshCurrentPage(); //执行当前列表页的刷新事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
        //js获取地址栏中的参数
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        function changeType(id) {
            if (id == "0")
                $("#trPic").show();
            else
                $("#trPic").hide();
        }

        $(function () {

                $("#trPic").show();
           
        });
        
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div id="right">
        <div class="applica_title">
            <br />
            <h4>
            </h4>
        </div>
        <div class="applica_di">
            <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr style="display:none">
                    <td class="Ltd">
                        显示方式：
                    </td>
                    <td class="Rtd" colspan="3">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" onclick="changeType(this.value)">文字</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0" onclick="changeType(this.value)">图片</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="lbDisplayMode" runat="server" />
                    </td>
                </tr>
                <tr id="trPic">
                    <td class="Ltd">
                        上传图片：
                    </td>
                    <td class="Rtd" colspan="3">
                        <input id="picUpload" runat="server" name="picUpload" type="file" onpropertychange="showPic(this,'uploadimage')" />
                        <asp:Image ID="uploadimage" runat="server" ImageUrl="../../../Images/nopic.jpg" Height="50px"
                            Width="90px" />
                        <span style="color: #ff0000">上传文件类型仅限 jpg | jpeg | bmp | gif 格式</span>
                        <asp:Label ID="lbHideLink" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        类别名称：
                    </td>
                    <td class="Rtd" colspan="3">
                        <asp:DropDownList ID="DrCategory" runat="server">
                            <asp:ListItem Value="1004">首页</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        图片上传说明：
                    </td>
                    <td class="Rtd" style='color: #F00;' colspan="3">
                        首页幻灯片:首页顶部滚动显示的幻灯广告，建议尺寸为：750×340<br />
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        链接名称：
                    </td>
                    <td class="Rtd" colspan="3">
                        <asp:TextBox ID="txtTitle" runat="server" Width="50%" /><span style='color: #F00;'>控制在15字以内，作简单描述</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        链接地址：
                    </td>
                    <td class="Rtd" colspan="3">
                        <asp:TextBox ID="txtLink" Text="http://" runat="server" Width="80%" /><span style='color: #F00;'>*(格式：http://www.baidu.com)</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        排 序：
                    </td>
                    <td class="Rtd" colspan="3">
                        <asp:TextBox ID="txtSort" runat="server" Width="80%" Text="0" /><span style='color: #F00;'>*</span>
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="Ltd">
                        开始时间：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtStartTime" runat="server" Width="200px" onFocus="WdatePicker()"
                        CssClass="input_add"></asp:TextBox><span style='color: #F00;'>*(注：默认为今天)</span>
                    </td>
                    <td class="Ltd">
                        结束时间：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtEndTime" runat="server" Width="200px" onFocus="WdatePicker()"
                        CssClass="input_add"></asp:TextBox><span style='color: #F00;'>*(注：默认为一周后)</span>
                    </td>
                </tr>
                <tr class="ButBox">
                    <td colspan="4" style="height: 30px; padding-top: 4px; background-color: #f6f6f6;"
                        align="center">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="确定" Text="确定"
                            BorderWidth="0" Width="135px" Height="32px" OnClientClick="return check()" OnClick="btnSubmit_Click" />
                        <asp:Button runat="server" ID="Button1" CssClass="Return" ToolTip="返回" OnClientClick="layer_close_refresh()"
                            Text="返回" BorderWidth="0" Width="135px" Height="32px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>