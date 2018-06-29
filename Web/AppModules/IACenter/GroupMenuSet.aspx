<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupMenuSet.aspx.cs" Inherits="AppModules_IACenter_GroupMenuSet" %>

<%@ Register Src="../../Controls/TopButtons.ascx" TagName="TopButtons" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户组菜单设置</title>
    <link href="/skin/blue/css/style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="/js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" language="javascript" src="/js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="/js/calendar.js"></script>
    <style>
        #table_datalist
        {
            line-height: 25px;
            font-size: 16px;
            font-family: 微软雅黑;
        }
        #table_datalist a
        {
            text-decoration: none;
            color: #000;
        }
        #table_datalist a:hover
        {
            text-decoration: underline;
        }
    </style>
    <script language="javascript">
        function buttonAction(s) {
            var ids = Components_TopButtons_GetCheckBoxValues(document.form1.ids);
            var selectNumer = ids.split(',').length;
            if (s == "add") {
                location.href = "AddProduct.aspx";
                return false;
            }
            else if (s == "edit") {
                if (Components_TopButtons_CheckUpdate(document.form1.ids) == false) { return false; }
            }
            else if (s == "delete") {
                if (Components_TopButtons_CheckDelete(document.form1.ids) == false) { return false; }
            }
            else if (s == "search") {

            }
            else if (s == "show") {
                if (Components_TopButtons_CheckUpdate(document.form1.ids) == false) { return false; }
            }
            return true;
        }
        window.setInterval("window.parent.setHeight()", 1000);
    </script>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
    <div class="div_right">
        <h2>
            <span class="title_left"></span><b>[<asp:Literal runat="server" ID="ltGroupName"></asp:Literal>]用户组菜单设置</b><span
                class="title_right"></span></h2>
        <div class="clear">
        </div>
        <!--数据源-->
        <table width="805" border="0" cellspacing="0" cellpadding="0" id="table_datalist">
            <tr>
                <td width="300" style=" vertical-align:top">
                        <asp:TreeView Width="100%" ID="tvModuleTree" runat="server" BorderColor="#E0E0E0"
                            ShowLines="True" Style="padding: 0px; margin: 0px;" ExpandDepth="1">
                        </asp:TreeView>
                </td>
                <td valign="top">
                    <iframe id="mainList" name="mainList" src="about:blank" width="100%" height="300"
                        frameborder="0" scrolling="no"></iframe>
                </td>
            </tr>
        </table>
        <!--//数据源-->
        <div class="clear">
        </div>
    </div>
    </form>
</body>
</html>
