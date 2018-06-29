<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupMenuSetIframe.aspx.cs" Inherits="AppModules_IACenter_GroupMenuSetIframe" %>

<%@ Register Src="../../Controls/TopButtons.ascx" TagName="TopButtons" TagPrefix="uc2" %>

<%@ Register Src="../../Controls/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户组菜单设置</title>
    <link href="/skin/blue/css/style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="/js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" language="javascript" src="/js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="/js/calendar.js"></script>
    <script language="javascript">
        function buttonAction(s){
            var ids = Components_TopButtons_GetCheckBoxValues(document.form1.ids);
            var selectNumer = ids.split(',').length;
            if(s=="add"){
                location.href="AddProduct.aspx";
                return false;
            }
            else if(s=="edit"){
                if(Components_TopButtons_CheckUpdate(document.form1.ids) == false){return false;}
            }
            else if(s=="delete"){
                if(Components_TopButtons_CheckDelete(document.form1.ids) == false){return false;}
            }
            else if(s=="search"){
                
            }
            else if(s=="show"){
                if(Components_TopButtons_CheckUpdate(document.form1.ids) == false){return false;}
            }
            return true;
        }
        
        function cbxAllClick(){
            if($("#cbxAll").attr('checked')==true){
                $("input[@type=checkbox]","#cbxButtons").each(
                    function(){
                        $(this).attr('checked',true);
                    }
                );
            }else{
                $("input[@type=checkbox]","#cbxButtons").each(
                    function(){
                        $(this).attr('checked',false);
                    }
                );
            }
        }
    </script>
</head>
<body style="margin:0px; padding:0px;">
    <form id="form1" runat="server">
        <div class="div_right">
            <!--数据源-->
            <table width="405" border="0" cellspacing="0" cellpadding="0" id="table_datalist"
                class="table_2">
                <tr>
                    <td width="100">
                        菜单名称：
                    </td>
                    <td class="text_left" style="color:Red">
                        <asp:CheckBox ID="cbxMenu" runat="server" /><asp:Literal runat="server" ID="ltMenuName"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        菜单功能：
                    </td>
                    <td class="text_left">
                    <input type="checkbox" id="cbxAll" onclick="cbxAllClick()" />选择全部
                        <asp:CheckBoxList ID="cbxButtons" runat="server" RepeatColumns="4">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                        应用到子类：
                    </td>
                    <td class="text_left">
                        <asp:CheckBox ID="cbxToSub" runat="server" Checked="true" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text=" 保 存 " OnClick="btnSave_Click" /></td>
                </tr>
            </table>
            <!--//数据源-->
            <div class="clear">
            </div>
        </div>
    </form>
</body>
</html>
