<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddWTO.aspx.cs" Inherits="AppModules_InfoManage_AddArticle" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文章查看页面</title>
    <link href="../../Style/Common.css" rel="stylesheet" />
    <link href="../../Style/PopUp.css" rel="stylesheet" />
    <script type="text/javascript" src="../../My97DatePicker/WdatePicker.js"></script>
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
    <form id="form1" runat="server">
    <table class="ModTab" cellpadding="0" cellspacing="1">
        <tr>
            <td class="Ltd">
                共享平台：
            </td>
            <td class="Rtd">
                <asp:CheckBoxList ID="ckPlatFormList" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>

        <tr>
            <td class="Ltd">
                所属专题共享 ：
            </td>
            <td class="Rtd">
                <asp:CheckBoxList ID="ckSubjectFormList" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>

        <tr>
            <td class="Ltd">
                目标市场共享 ：
            </td>
            <td class="Rtd">
                <asp:CheckBoxList ID="ckMarketFormList" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
         <tr>
            <td class="Ltd">
                所属体系 ：
            </td>
            <td class="Rtd" colspan="3">
                <asp:DropDownList ID="ddlTxPlatform" runat="server">
                </asp:DropDownList>
            </td>
        </tr>

        <asp:Panel ID="palCheck" runat="server" Visible="false">
            <tr>
                <td class="Ltd">
                    审核状态：
                </td>
                <td colspan="3" class="ButBox">
                    <asp:RadioButtonList ID="radioApproved" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">通过</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">未通过</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td colspan="2" class="ButBox">
                <p>
                    <asp:LinkButton ID="LinkButton1" runat="server" class="Submit" title="保存" OnClick="LinkButton1_Click">保 存</asp:LinkButton><a
                        href="Javascript:layer_close();void(0);" class="Return" title="返回">返&nbsp;回</a></p>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfanalyse_is_certify" runat="server" />
    </form>
    <script>
        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.grid_search(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
</body>
</html>
