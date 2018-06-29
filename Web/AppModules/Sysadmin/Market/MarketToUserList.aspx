<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MarketToUserList.aspx.cs"
    Inherits="AppModules_IACenter_ListUser" %>

<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标准列表</title>
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
            农贸市场配置信息列表</p>
    </div>
    <!--search-->
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>农贸市场名称：</span><input style="width: 100px;" id="txtMarketName" />
                <span>真实姓名：</span><input style="width: 100px;" id="txtstdname" />
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                <%-- <input type="button" class="QueryBut" value="添&nbsp;加" title="添加" onclick="button_actions('add','')" />--%>
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
        url: 'MarketToUserHandler.ashx?flag=list',
        datatype: "json",
        colNames: ['序号', '所在区域', '农贸市场编号', '农贸市场名称', '管理员姓名', '手机号码', '配置时间', '操作'],
        colModel: [
            { name: 'id', index: 'id', hidden: true },
            { name: 'market_id', index: 'market_id', width: 80, align: 'center' },
             { name: 'area_name', index: 'area_name', width: 200, align: 'center' },
            { name: 'market_name', index: 'market_name', width: 200, align: 'center' },
            { name: 'truename', index: 'truename', width: 100, align: 'center' },
            { name: 'mobile', index: 'mobile', width: 100, align: 'center' },
            { name: 'addtime', index: 'addtime', width: 80, align: 'center' },
            { name: 'cmd_col', align: 'center' }
        ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'addtime',
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

                    if (rowData["addtime"].toString() != "") {
                        var addtime = { addtime: rowData["addtime"].substring(0, rowData["addtime"].toString().indexOf(' ')).replace(/\//g, "-") };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], addtime);
                    }

                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">" }
                    //  var writeData = { cmd_col: "<input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">" }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //button_actions
    function button_actions(flag, id, lang) {
        if (flag == "add") {
            add_method();
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "edit") {
            edit_method(id);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        } else if (flag == "show") {
            show_method(id);
        }
        else if (flag == "download") {
            //download_method(id, lang);
            var str = $.ajax({ url: "MarketToUserHandler.ashx?flag=delete&lang='" + lang + "'id=" + id + "", async: false }).responseText;
        }
        else if (flag == "delete") {
            delete_method(id);
        }
    }

    //查询
    function grid_search() {

        var _searchfilter = "marketname$$" + $("#txtMarketName").val() + "_$$_truename$$" + $("#txtstdname").val();
        var _searchfilter = escape(_searchfilter);


        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MarketToUserHandler.ashx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    //添加
    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['配置农贸市场管理员', true],
            maxmin: true,
            iframe: { src: 'SetMarketToUser.aspx?r=' + Math.random() },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //修改
    function edit_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['修改农贸市场管理员', true],
            maxmin: true,
            iframe: { src: 'SetMarketToUser.aspx?id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //删除
    function delete_method(id) {
        layer.confirm('确定要删除吗?', function () {
            var str = $.ajax({ url: "MarketToUserHandler.ashx?flag=delete&id=" + id + "", async: false }).responseText;
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            var index = layer.index; //获取当前弹出窗索引
            layer.close(index); //关闭弹出窗
        });
    }
    //查看
    function show_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['查看', true],
            maxmin: true,
            iframe: { src: 'SetMarketToUser.aspx?rd=1&id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                //jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //查看文件
    function download_method(id, lang) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['查看', true],
            maxmin: true,
            iframe: { src: 'MarketToUserHandler.ashx?lang=' + lang + '&id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                //jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //批量删除
    function deleteAction() {
        var IsCheck = false;
        var inputList = document.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var oInput = inputList[i];
            if (oInput.type == "checkbox" && oInput.checked) {
                IsCheck = true;
            }
        }
        if (!IsCheck) {
            alert("对不起，您尚未选择要删除的选项");
            return IsCheck;
        }
        else {
            if (confirm('确定要批量删除这些吗?')) {
                var rowData = jQuery('#startech_table_jqgrid').jqGrid('getGridParam', 'selarrrow');
                var id = "";
                for (var i = 0; i < rowData.length; i++) {
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'id') + ","; //获取所有选中的id值
                }
                var str = $.ajax({ url: "MarketToUserHandler.ashx?flag=delete&id=" + id + "", async: false }).responseText;
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                if (str === "true") {
                    alert("删除成功!");
                }
                else {
                    alert("删除失败!");
                }
            }
        }
    }

</script>
<%--<script language="javascript">
    //-----------div层父窗口交互控制------------//
    function layer_close() {
        var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
        parent.layer.close(layer_index);
    }
</script>--%>
