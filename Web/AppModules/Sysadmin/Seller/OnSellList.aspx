<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnSellList.aspx.cs" Inherits="AppModules_Sysadmin_Seller_SellerList" %>

<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实体卖家信息列表</title>
    <link href="/Style/List.css" rel="stylesheet" type="text/css" />
    <link href="/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="/js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="/js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="/js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <style>
        #ddlOpen
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>店铺名称：</span><input style="width: 150px;" id="txtShopName" />
                <span>任务名称：</span><input style="width: 150px;" id="txtProName" />
                <span>农贸市场：</span>
                <select id="ddlTopArea" onchange="loadSecArea()">
                </select>
                <select id="ddlSecArea" onchange="loadThirdArea()" style="display: none;">
                </select>
                <select id="ddlThirdArea" onchange="loadMarket()" style="display: none;">
                </select>
                <select id="txtMarket">
                </select>
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
        url: 'OnSell.ashx?flag=list',
        datatype: "json",
        colNames: ['编号', '所在区域', '农贸市场', '店铺名称', '任务编号', '任务名称', '库存', '报价', '上架时间'],
        colModel: [
            { name: 'shopgoods_id', hidden: true },
            { name: 'areaname', index: 'areaname', width: 50, align: 'center' },
            { name: 'marketname', index: 'marketname', width: 80, align: 'center' },
            { name: 'shopname', index: 'shopname', width: 80 },
            { name: 'goodscode', index: 'goodscode', hidden: true },
   		    { name: 'goodsname', index: 'goodsname' },
   		    { name: 'shopgoods_amount', index: 'shopgoods_amount', width: 50, align: 'center' },
            { name: 'shopgoods_selfprice', index: 'shopgoods_selfprice', width: 50, align: 'center' },
            { name: 'shopgoods_addtime', index: 'shopgoods_addtime', align: 'center' }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: ' goodscode,shopgoods_addtime ',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "shopgoods_id");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {

                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });


    //查询
    function grid_search() {

        var _ShopName = $("#txtShopName").val();

        var _Areaid = $("#ddlThirdArea").val();

        var _Marketid = $("#txtMarket").val();
        var _GoodsName = $("#txtProName").val();



        var _searchfilter = "_$$_ShopName$$" + _ShopName + "_$$_Areaid$$" + _Areaid + "_$$_Marketid$$" + _Marketid + "_$$_GoodsName$$" + _GoodsName;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "OnSell.ashx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }
</script>
<script>
    (function () {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=data",
            success: function (res) {
                if (res != "") {
                    $("#ddlTopArea").html(res);
                }
            },
            error: function (error) {
                //  alert(error);
            }
        });


    })();

    function loadSecArea() {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=data&areaid=" + document.getElementById("ddlTopArea").value,
            success: function (res) {
                if (res != "") {
                    $("#ddlSecArea").html(res);
                    document.getElementById("ddlSecArea").style.display = "inline-block";

                }
            },
            error: function (error) {
                //  alert(error);
            }
        });
    }

    function loadThirdArea() {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=data&areaid=" + document.getElementById("ddlSecArea").value,
            success: function (res) {
                if (res != "") {
                    $("#ddlThirdArea").html(res);
                    document.getElementById("ddlThirdArea").style.display = "inline-block";
                }
            },
            error: function (error) {
                //  alert(error);
            }
        });
    }

    function loadMarket() {
        $.ajax({
            type: "GET",
            url: "SellerService.ashx",
            dataType: "text",
            data: "flag=market&areaid=" + document.getElementById("ddlThirdArea").value,
            success: function (res) {
                if (res != "") {
                    $("#txtMarket").html(res);
                    // document.getElementById("txtMarket").innerHTML = res;
                    // document.getElementById("tr").style.display = "table-row";
                }
            },
            error: function (error) {
                //  alert(error);
            }
        });
    }
</script>
