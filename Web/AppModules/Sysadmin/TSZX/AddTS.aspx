<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTS.aspx.cs" Inherits="AppModules_Sysadmin_Base_AddTS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../../../ckfinder/ckfinder.js" type="text/javascript"></script>
    <script type="text/javascript">

        function check() {
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
            var IsPic = "<%=disImg %>";
            if (IsPic == "1") {
                $("#trPic").show();
            } else {
                $("#trPic").hide();
            }
        });
        $(function () {
            var editor = CKEDITOR.replace("fckBody", { skin: "kama", width: 900, height: 380
            });
        });
    </script>
    <style type="text/css">
    .UnNull
    {
        color:Red;
    }
    #Save{ width:135px; height:32px; font-size:16px; color:#ab0b0b;text-shadow:0 1px #ffe8bb;background:url(../../../Images/SubmitN.png); position:relative; left:430px;top:-10px;}
    .Ltd{ width:20%;}
    </style>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div id="right">
        <div class="applica_title">
            <br />
            <h4>
            <asp:Button runat="server" ID="Save" Text="保存" OnClientClick="return check()"
                            OnClick="btnSubmit_Click" BorderWidth="0" Width="135px" Height="32px" />
            </h4>
        </div>
        <div class="applica_di">
            <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                    <td class="Ltd">
                        投诉主题：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtSubHead" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        申诉人姓名：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtUnit" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        申诉人地址：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtFromSouce" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        申诉人邮编：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtCode" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        申诉人电话：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtteleph" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        申诉人电子邮件：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtEmail" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        申诉人产品名称：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtProduct" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        详细内容：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtContent" runat="server" Width="80%" /><span class="UnNull">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        回复时间：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtReleaseDate" runat="server" Width="200px" onclick="WdatePicker()"
                            CssClass="input_add"></asp:TextBox>&nbsp; <span style="color: #ff0000">注：发布时间请不要超出当前时间。</span>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        回复信息：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="fckBody" runat="server" TextMode="MultiLine" ></asp:TextBox>
                        <span style="color: #ff0000">*</span> <font color="red">注：请尽量不要从Word中复制内容，以免发生不兼容的情况</font>
                    </td>
                </tr>
                <tr>
                    <tr class="ButBox">
                        <td colspan="2" style="height: 30px; padding-top: 4px; background-color: #f6f6f6;"
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
