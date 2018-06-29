<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditGoodsPropertyIframe.aspx.cs" Inherits="ShopSeller_EditGoodsPropertyIframe" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>任务属性</title>
    

    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
        <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />


    <script language="javascript" src="../JS/jquery-1.4.2.js"></script>
    <style>
    .file{
	    FONT-SIZE: 12px;	
	    LINE-HEIGHT: 16px;	
	    HEIGHT: 20px;
	    width:500px;
	    
    }
    </style>
    <script type="text/javascript" language="javascript" src="../../js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" language="javascript" src="../../js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="../../js/calendar.js"></script>
    <base target="_self"></base>
    <script language="javascript">
    window.name="myDialog";
    </script>
</head>
<body>
    <form id="form1" runat="server" target="myDialog">
        <div style="font-size:12px">
            <div class="AddcomT1" style="width:450px">
                        <p class="AddcomT1P">
                            任务属性</p>
                    </div>
            <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist" style="width:450px">
                    <asp:Literal ID="ltHtml"  runat="server"></asp:Literal></table>
            <center runat="server" class='ButBox' id="btn_1">
            <asp:Button ID="btnSave"  runat="server" style='border-width: 0px;height: 32px;width: 135px;' CssClass="Submit"
                     OnClick="btnSave_Click" Text='提交' />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="ImageButton1" runat="server" style='border-width: 0px;height: 32px;width: 135px;' CssClass="Return"
                     OnClientClick="window.close();return false;" Text='返回' />
            </center>
            <center runat="server" id="btn_2" visible="false">
                <font color="#cccccc">此任务分类暂无设置任务扩展属性</font>
            </center>
        </div>
	<div class="clear"></div>
    </form>
</body>
</html>
