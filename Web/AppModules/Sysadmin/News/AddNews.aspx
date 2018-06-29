<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNews.aspx.cs" Inherits="AppModules_Sysadmin_News_AddNews" validateRequest="false" %>

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
            var editor = CKEDITOR.replace("fckBody", { skin: "kama", width: document.body.scrollWidth - 150, height: 380 });
            var editorFooter = CKEDITOR.replace("fckBodyFooter", { skin: "kama", width: document.body.scrollWidth - 150, height: 100 });
        });
    </script>
    <style type="text/css">
    .UnNull
    {
        color:Red;
    }
    #Save{ width:135px; height:32px; font-size:16px; color:#ab0b0b;text-shadow:0 1px #ffe8bb;background:url(../../../Images/SubmitN.png); position:relative; left:350px;top:-10px;}
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
                    标题：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtTitle" runat="server" Width="80%" /><span class="UnNull">*</span>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    所属分类：
                </td>
                <td class="Rtd">
                    <asp:DropDownList ID="ddlType" runat="server" Width="20%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    副标题：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtSubHead" runat="server" Width="80%" /><span class="UnNull">*</span>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    作 者：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtUnit" runat="server" Width="80%" /><span class="UnNull">*</span>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    来源：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtFromSouce" runat="server" Width="80%" /><span class="UnNull">*</span>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    上传图片：
                </td>
                <td class="Rtd">
                    <input type="file" id="picUpload" name="picUpload" runat="server" onpropertychange="showPic(this,'uploadimage')"
                        style="width: 20%" class="input" /><img id="uploadimage" runat="server" src="../../../Images/nopic.jpg"
                            width="100" height="119" /><asp:Button ID="btnDeleteImg" runat="server" 
                        onclick="btnDeleteImg_Click" Text="删除图片" Visible="False" />
&nbsp;</td>
            </tr>
            <tr>
                <td class="Ltd">
                    发布时间：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtReleaseDate" runat="server" Width="200px" onFocus="WdatePicker()"
                        CssClass="input_add"></asp:TextBox>
                    <span style="color: #ff0000">注：发布时间请不要超出当前时间。</span>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    排序：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="txtSort" runat="server" Width="200px"
                        CssClass="input_add">99</asp:TextBox>&nbsp;            
                    <span style="color: #ff0000">注：数字越小，排序越靠前，默认为99。</span>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    新闻内容：
                </td>
                <td class="Rtd">
                    <asp:TextBox ID="fckBody" runat="server" TextMode="MultiLine" ></asp:TextBox>
                    <span style="color: #ff0000">*</span><font color="red">【建议排版操作规范】：<br />
                            （1）首先打开记事本，把文字内容复制到记事本里<br />
                            （2）然后在记事本里把段落、空格等编辑好后将内容复制进来<br />
      
                            【本编辑器里的首行空格问题】：<br />
                            半角空格（空格键按4下）；全角空格（空格键按2下，建议使用全角空格，复制引号里的全角空格“　”）
                            </font>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    文章尾部信息：
                </td>
                <td class="Rtd">
                    <asp:CheckBox ID="cbFooter" Checked="false" Text="自动添加新闻底部信息" runat="server" /><br />
                    <asp:TextBox ID="fckBodyFooter" runat="server" TextMode="MultiLine" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    是否推荐：
                </td>
                <td class="Rtd">
                    <asp:RadioButtonList ID="radioIndexCommentList" runat="server" RepeatDirection="Horizontal"
                        Height="20px">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    是否置顶：
                </td>
                <td class="Rtd">
                    <asp:RadioButtonList ID="rbtIstop" runat="server" RepeatDirection="Horizontal" Height="20px">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="Ltd">
                    设为热点：
                </td>
                <td class="Rtd">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td width="200px">
                                <asp:RadioButtonList ID="rbtHot" runat="server" RepeatDirection="Horizontal" Height="20px">
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                默认天数：<asp:TextBox ID="txtHotDays" runat="server" Width="20px" CssClass="input_add">3</asp:TextBox>天
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <asp:Panel ID="palCheck" runat="server" Visible="false">
                <tr>
                    <td class="Ltd">
                        审核状态：
                    </td>
                    <td class="Rtd">
                        <asp:RadioButtonList ID="radioApproved" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">通过</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">未通过</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </asp:Panel>
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
