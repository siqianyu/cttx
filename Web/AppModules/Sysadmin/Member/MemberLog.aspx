<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberLog.aspx.cs" Inherits="MemberList" %>

<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/OutputExcel.ascx" TagName="OutputExcel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员日志-会员管理</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="PosiBar">
        <p>
        </p>
    </div>
    <!--search-->
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>操作人：</span><input style="width: 100px;" id="txtPerson" runat="server" />
                <span>会员名称：</span><input style="width: 100px;" id="txtMemName" runat="server" />&nbsp;&nbsp;
                <span>操作关键字：</span><input style="width: 100px;" id="txtKeyWord" runat="server" />&nbsp;&nbsp;
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                <%-- <input type="button" class="QueryBut" value="添&nbsp;加" title="添加" onclick="button_actions('add','')" />--%>
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
        url: 'MemberHandler.ashx?flag=log',
        datatype: "json",
        colNames: ['编号', '操作人', '操作对象', '关键字', '操作简介', '操作时间'],
        colModel: [
            { name: 'memberId', hidden: true },
            { name: 'person', index: 'person', width: 60 },
            { name: 'member', index: 'member', width: 100 },
   		    { name: 'operation', index: 'operation', width: 80 },
            { name: 'decoration', index: 'decoration', width: 350 },
   		    { name: 'time', index: 'time', width: 50 }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'memberId',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "memberId");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.memberId;


                    //修改时间显示格式
                    if (rowData["time"].toString() != "") {
                        var time = { time: rowData["time"].substring(0, rowData["time"].toString().indexOf(' ')).replace(/\//g, "-") };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], time);
                    }
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //button_actions
    function button_actions(flag, id, type) {
        if (flag == "add") {
            add_method(type);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "edit") {
            edit_method(id, type);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        } else if (flag == "show") {
            show_method(id, type);
        }
        else if (flag == "check") {
            check_xfq(id);
        }
        else if (flag == "download") {
            //download_method(id, lang);
            var str = $.ajax({ url: "MemberHandler.ashx?flag=delete&lang='" + lang + "'id=" + id + "", async: false }).responseText;
        }
        else if (flag == "delete") {
            delete_method(id);
        }
        else if (flag == "levelup") {
            levelup_method(id);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
    }

    //查询
    function grid_search() {
        var _person = $("#txtPerson").val();
        var _operation = $("#txtKeyWord").val();
        var _member = $("#txtMemName").val();


        var _searchfilter = "person$$" + _person + "_$$_operation$$" + _operation + "_$$_member$$" + _member;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MemberHandler.ashx?flag=log&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

</script>
