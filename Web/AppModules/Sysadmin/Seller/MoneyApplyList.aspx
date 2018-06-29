<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoneyApplyList.aspx.cs" Inherits="AppModules_Sysadmin_Seller_SellerList" %>

<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>保证金缴纳申请列表</title>
    <link href="/Style/List.css" rel="stylesheet" type="text/css" />
    <link href="/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="/js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="/js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="/js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>店铺名称：</span><input style="width: 150px;" id="txtShopName" />
                <span>账户状态：</span>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="">--全部--</asp:ListItem>
                    <asp:ListItem Value="1">已审核</asp:ListItem>
                    <asp:ListItem Value="0">未审核</asp:ListItem>
                </asp:DropDownList>
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                <uc1:Add ID="Add1" runat="server" />
                <uc3:Edit ID="Edit1" runat="server" />
                <uc6:Show ID="Show1" runat="server" />
                <uc4:Delete ID="Delete1" runat="server" />
            </p>
        </div>
    </div>
    <div class="TableBox Left">
        <table id="startech_table_jqgrid">
        </table>
        <div id="startech_table_jqgrid_pager">
        </div>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    jQuery("#startech_table_jqgrid").jqGrid({
        url: 'MoneyHandler.ashx?flag=list',
        datatype: "json",
        colNames: ['编号', '店铺编号', '店铺名称', '申请人', '金额', '审核状态', '申请时间', '操作'],
        colModel: [
            { name: 'apply_id', hidden: true },
             { name: 'shopid', hidden: true },
            { name: 'shopname', index: 'shopname', width: 80, align: 'center' },
   		    { name: 'apply_name', index: 'apply_name', align: 'center' },
   		    { name: 'apply_money', index: 'apply_money', width: 50, align: 'center' },
            { name: 'apply_status', index: 'apply_status', width: 40, align: 'center' },
            { name: 'apply_time', index: 'apply_time', width: 40, align: 'center' },
            { name: 'cmd_col', align: 'center', width: 100 }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'apply_id',
        viewrecords: true,
        sortorder: "asc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "apply_id");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.apply_id;
                    if (rowData["apply_time"].toString() != "") {
                        var apply_time = { apply_time: rowData["apply_time"].substring(0, rowData["apply_time"].toString().indexOf(' ')).replace(/\//g, "-") };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], apply_time);
                    }

                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='审核' onclick=\"button_actions('check','" + id + "','" + rowData.shopid + "')\">" }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //button_actions
    function button_actions(flag, id, sid) {
        if (flag == "add") {
            add_method();
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "show") {
            show_method(id);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "check") {
            check_method(id, sid); //审核保证金
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "delete") {
            delete_method(id);
        }
    }

    //查询
    function grid_search() {
        var _ShopName = $("#txtShopName").val();
        var _ApplyStatus = $("#ddlStatus").val();

        var _searchfilter = "ShopName$$" + _ShopName + "_$$_ApplyStatus$$" + _ApplyStatus;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MoneyHandler.ashx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }



    //设置
    function set_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['设置', true],
            maxmin: true,
            iframe: { src: 'CheckSeller.aspx?rd=1&id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {

                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    function check_method(id, sid) {
        layer.confirm('确定收到该商家的保证金了吗？', function () {
            var str = $.ajax({ url: "MoneyHandler.ashx?flag=check&id=" + id + "&sid=" + sid + "", async: false }).responseText;
            var index = layer.index; //获取当前弹出窗索引
            layer.close(index); //关闭弹出窗
            layer.alert(str, 1, "保证金申请审核");
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        });
 
    }
</script>
