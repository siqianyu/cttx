<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FreeCut.aspx.cs" Inherits="AppModules_Sysadmin_Article_FreeCut" ValidateRequest="false" %>

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
            var Market = $("#<%=ddlMarket.ClientID %> option:selected").val();
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
            var editor = CKEDITOR.replace("fckBody", { skin: "kama", width: document.body.scrollWidth - 150, height: 380
            });
        });

        function selectMarket() {
            var Market = $("#txtMarketName").val();
            var str = $.ajax({ url: "/AppModules/Sysadmin/Base/MarketHandler.ashx?flag=selectMarket&Marketname=" + escape(Market) + "&r=" + Math.random(), async: false }).responseText;
            $("#ddlMarket").html(str);

        }
    </script>
    <style type="text/css">
        .UnNull
        {
            color: Red;
        }
        #Save
        {
            width: 135px;
            height: 32px;
            font-size: 16px;
            color: #ab0b0b;
            text-shadow: 0 1px #ffe8bb;
            background: url(../../../Images/SubmitN.png);
            position: relative;
            left: 350px;
            top: -10px;
        }.SelectBut{border:none;width:65px;height:25px;background:url(../../../Images/LogOutN.png);color:#9b0700;text-shadow:0 1px #ffe191;margin:0 10px;padding:0;text-align:center;cursor:pointer; }
        .Ltd
        {
            width: 20%;
        }
    </style>
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
                <tr>
                    <td class="Ltd">
                        标&nbsp;&nbsp;&nbsp;&nbsp;题：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtTitle" runat="server" Width="80%" ReadOnly="true" Text="APP客户端自由分割区" /><span class="UnNull">*</span>
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
                    <td class="Ltd">选择农贸市场：</td>
                    <td class="Rtd">
                        <asp:DropDownList ID="ddlMarket" runat="server" Width="204px">
                        <asp:ListItem Value="0" Selected="True" Text="--请选择--"></asp:ListItem>
                    </asp:DropDownList>
                    <input style="width: 65px;" id="txtMarketName" /><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="selectMarket()"  />
                        </td>
                </tr>
                <tr style=" display:none;">
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
                        内&nbsp;&nbsp;&nbsp;&nbsp;容：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="fckBody" runat="server" TextMode="MultiLine" ></asp:TextBox>
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
                                Text="返回" Visible="false" BorderWidth="0" Width="135px" Height="32px" />
                        </td>
                    </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
