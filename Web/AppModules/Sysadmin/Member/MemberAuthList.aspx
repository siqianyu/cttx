<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberAuthList.aspx.cs" Inherits="MemberList" %>

<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/OutputExcel.ascx" TagName="OutputExcel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员列表</title>
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
            权限设置
        </p>
    </div>
    <!--search-->
    <div class="SosoBar Left" style="display: none">
        <div class="Soso">
            <p class="Query">
                <span>用户名：</span><input style="width: 100px;" id="txtMemberName" runat="server" />
                <span>单位名称：</span><input style="width: 100px;" id="txtCompanyName" runat="server" />
                <span>真实姓名：</span><input style="width: 100px;" id="txtTrueName" runat="server" />
                <span>会员级别：</span><asp:DropDownList ID="ddlMemLevel" runat="server" Width="100px">
                </asp:DropDownList>
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                <uc2:Add ID="Add1" runat="server" />
                <uc3:Edit ID="Edit1" runat="server" />
                <uc6:Show ID="Show1" runat="server" />
                <uc4:Delete ID="Delete1" runat="server" />
                <uc1:OutputExcel ID="OutputExcel1" runat="server" />
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
        url: 'MemberAuth.ashx?flag=list',
        datatype: "json",
        colNames: ['编号', '对象', '权限', '操作'],
        colModel: [
            { name: 'id', hidden: true },
            { name: 'flag', index: 'flag', width: 60 },
   		    { name: 'subinfo', index: 'subinfo' },
            { name: 'cmd_col', align: 'center', width: 100 }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'id',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "id");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.id;
                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='设置' onclick=\"button_actions('edit','" + id + "')\"> " }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);

                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //button_actions
    function button_actions(flag, id, type) {
        if (flag == "edit") {
            edit_method(id, type);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
    }

    //修改
    function edit_method(id ) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['权限设置', true],
            maxmin: true,
            iframe: { src: 'SetMemAuth.aspx?id=' + id },
            area: [800, 400],
            offset: ['0px', ''],
            close: function (index) {

                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    function flash() {
        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
    }

</script>
