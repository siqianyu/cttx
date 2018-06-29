<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberTotal.aspx.cs" Inherits="AppModules_Sysadmin_Member_MemberTotal" %>

<%@ Register Src="~/Controls/OutputExcel.ascx" TagName="OutputExcel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员统计</title>
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
        url: 'MemberCountHandler.ashx?flag=listcount',
        datatype: "json",
        colNames: ['城区', '企业', '政府', '个人', '普通注册', '消费券注册', '总数', '操作'],
        colModel: [
            { name: 'areaName', index: 'areaName', width: 50 },
            { name: 'com', index: 'com', width: 50 },
   		    { name: 'gov', index: 'gov', width: 140 },
            { name: 'person', index: 'person', width: 50 },
   		    { name: 'formreg', index: 'formreg', width: 50 },
   		    { name: 'xfqreg', index: 'xfqreg', width: 60 },
             { name: 'total', index: 'total', width: 60 },
            { name: 'cmd_col', align: 'center' }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: '',
        viewrecords: true,
        sortorder: "",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "areaName");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.areaName;
                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "')\">" }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //button_actions
    function button_actions(flag, id, lang) {
        if (flag == "show") {
            show_method(id);
        }
    }
    //查看
    function show_method(id) {
        window.location.href = "MemberInfoByArea.aspx?Area=" + id;
//        $.layer({
//            type: 2,
//            shade: [0.1, '#000'],
//            fix: false,
//            title: ['查看会员信息', true],
//            maxmin: true,
//            iframe: { src: 'MemberInfoByArea.aspx?Area=' + id },
//            area: ['900px', '600px'],
//            offset: ['0px', ''],
//            close: function (index) {
//            }
//        });
    }
</script>
