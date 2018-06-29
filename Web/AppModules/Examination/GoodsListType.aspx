<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsListType.aspx.cs" Inherits="AppModules_Examination_GoodsListType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
<script type="text/javascript">
//    function GetList(url) {
//        alert(url);
//        $("#categoryGoodsList").attr("src", url);

//    }
</script>

<style type="text/css">
.treeSize a
{
    font-size:13px;
}
</style>

<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style='width: 100%;'>
        <tr>
            <td style="width: 230px" valign="top" class='treeSize' bgcolor="#f6fcff">
            <div style="overflow:auto; width:260px; height:800px;">
                <asp:TreeView ID="treeMenu" runat="server" ShowLines="True" SelectedNodeStyle-ForeColor="Red"
                    SelectedNodeStyle-Font-Bold="true" ForeColor="#3f71a9" OnSelectedNodeChanged="treeMenu_SelectedNodeChanged"
                    ExpandDepth="3">
                </asp:TreeView>
                </div>
            </td>
            <td valign="top">
                <iframe frameborder="0" scrolling="auto" name="categoryGoodsList" style='width: 100%;
                    height: 1000px;' id='categoryGoodsList' src='QuestionList.aspx<%=categoryId %>'>
                </iframe>
            </td>
        </tr>
    </table>
    </form>
</body>

</html>
