<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopicList.aspx.cs" Inherits="AppModules_InfoManage_TopicList" %>

<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>荣灿企业信息及票据采集系统</title>
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
            标准专题列表</p>
    </div>
    <!--search-->
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>专题标题：</span><input style="width: 200px;" id="input_name" />
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                <uc1:Add ID="Add1" runat="server" />
                <uc3:Edit ID="Edit1" runat="server" />
                <uc6:Show ID="Show1" runat="server" />
                <uc4:Delete ID="Delete1" runat="server" />
            </p>
        </div>
        <%--<span class="Dele" title="批量删除">
            <input type="button" value="批量删除" onclick="all_deleterow()" /></span> <span class="Add"
                title="新增">
                <input type="button" value="添加" onclick="add_method()" />
            </span>--%>
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
<!--表格数据源绑定start-->
<script type="text/javascript">
    jQuery("#startech_table_jqgrid").jqGrid({
        url: 'NewsList.ashx?flag=topic',
        datatype: "json",
        colNames: ['编号', '专题名称', '专题描述', '链接地址', '共享平台',  '添加日期', '操作'],
        colModel: [
            { name: 'ID', hidden: true },
   		    { name: 'Title', index: 'Title', width: 100 },
   		    { name: 'TopicContent', index: 'TopicContent', align: 'left', width: 280 },
   		    { name: 'Url', index: 'Url', align: 'left', width: 100 },
            { name: 'ShareToPlatform', index: 'ShareToPlatform', align: 'left', width: 80 },
            { name: 'AddTime', index: 'AddTime', align: 'center', width: 80 },
            { name: 'cmd_col', align: 'center', width: 120 }
   	    ],
        rowNum: 15,
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'AddTime',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox', //选择框
        //caption: '分析方法管理',
        multiselect: true,
        onSelectRow: function (rowid) {//通过rowsid获取到列中的值
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "ID");
            }
        },
        gridComplete: function () {//当表格所有数据都加载完成而且其他的处理也都完成时触发此事件，排序，翻页同样也会触发此事件
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.ID;
                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='修改' onclick=\"button_actions('edit','" + id + "')\">  <input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "')\">  <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">" }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);

                    //修改时间显示格式
                    if (rowData["AddTime"].toString() != "") {
                        var AddTime = { AddTime: rowData["AddTime"].substring(0, rowData["AddTime"].toString().indexOf(' ')).replace(/\//g, "-") };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], AddTime);
                    }
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
        else if (flag == "delete") {
            delete_method(id);
        }
    }

    //查询
    function grid_search() {
        var _Title = $("#input_name").val();

        var _searchfilter = "Title$$" + _Title;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "NewsList.ashx?flag=topic&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
    }

    //添加
    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['添加', true],
            maxmin: true,
            iframe: { src: 'AddTopic.aspx?r=' + Math.random() },
            area: ['1000px', '550px'],
            offset: ['0px', ''],
            close: function (index) {
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
            title: ['修改', true],
            maxmin: true,
            iframe: { src: 'AddTopic.aspx?id=' + id },
            area: ['1000px', '550px'],
            offset: ['0px', ''],
            close: function (index) {
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //删除
    function delete_method(id) {
        layer.confirm('确定要删除吗?', function () {
            var str = $.ajax({ url: "NewsList.ashx?flag=deltopic&id=" + id + "", async: false }).responseText;
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
            iframe: { src: 'AddTopic.aspx?rd=1&id=' + id },
            area: ['1000px', '550px'],
            offset: ['0px', ''],
            close: function (index) {

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
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'ID') + "|"; //获取所有选中的id值
                }
                var str = $.ajax({ url: "NewsList.ashx?flag=deltopic&id=" + id + "", async: false }).responseText;
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                if (str == "true") {
                    alert("删除成功!");
                }
                else {
                    alert("删除失败!");
                }
            }
        }
    }
</script>
<!--表格数据源绑定end-->
