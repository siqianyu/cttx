<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberTypeList.aspx.cs" Inherits="AppModules_Sysadmin_Member_MemberTypeList" %>



<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>才通天下微信公号平台</title>
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
        <p>会员类型列表</p>
    </div>

     <!--search-->
      <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>会员类别名称：</span><input style="width: 200px;" id="input_name" />

                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />

                <uc1:add ID="Add1" runat="server" />
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


<!--表格数据源绑定start-->
<script  type="text/javascript">
    jQuery("#startech_table_jqgrid").jqGrid({
        url: 'MemberType.ashx?flag=list',
        datatype: "json",
        colNames: ['编号', '会员类别名称', '排序', '操作'],
        colModel: [
            { name: 'TypeCode', hidden: false },
   		    { name: 'TypeName', index: 'TypeName', width: 300 },
   		    { name: 'orderBy', index: 'orderBy', align: 'center', width: 200 },
            { name: 'act', index: 'TypeCode', width: 150, sortable: false },
   	    ],
        rowNum: 15,
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'TypeCode',
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
                var method_id = $("#startech_table_jqgrid").getCell(gr, "TypeCode");
                var method_name = $("#hyName").getCell(gr, "TypeName");
                var method_is_in_use = $("#orderBy").getCell(gr, "orderBy");



            }
        },
        gridComplete: function () {//当表格所有数据都加载完成而且其他的处理也都完成时触发此事件，排序，翻页同样也会触发此事件
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {


                    var ed = "<input class='CommonButon' type='button' value='修改' onclick=\"editrow('" + rowData.TypeCode + "');\"  />";
                    //var de = "<input class='CommonButon' type='button' value='删除' onclick=\"deleterow('" + rowData.TypeCode + "');\"  />";
                    // var sle = "<input class='CommonButon' type='button' value='查看' onclick=\"sle_method('" + rowData.hyCode + "');\"  />";
                    var writeData =
                     {

                         act: ed 
                     }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false, search: false, refresh: false });
   



    //button_actions
    function button_actions(flag, id, lang) {
        if (flag == "add") {
            add_method();
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "edit") {
            editrow(index)
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        } else if (flag == "show") {
            show_method(id);
        }
        else if (flag == "delete") {
            deleterow(index)
        }
    }
    //search搜索
    function grid_search() {
        var _Title = $("#input_name").val();
        var _searchfilter = "TypeName$$" + _Title + "";
        var _searchfilter = escape(_searchfilter); //escape处理中文编码
        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MemberType.ashx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }
    //修改
    function editrow(index) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['修改', true],
            maxmin: true,
            iframe: { src: 'AddMemberType.aspx?TypeId=' + index },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }
    //添加
    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['添加', true],
            maxmin: true,
            iframe: { src: 'AddMemberType.aspx?r=' + Math.random() },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }
    //查看详细
    function sle_method(index) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['查看', true],
            maxmin: true,
            iframe: { src: 'AddLink.aspx?rd=1&LinkId=' + index },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                //jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }
    //删除
    function deleterow(index) {
        layer.confirm('确定要删除吗?', function () {
            var str = $.ajax({ url: "../../../AppModules/HyManage/Del.ashx?flag=delete&TypeId=" + index + "", async: false }).responseText;
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            var CurrentIndex = layer.index; //获取当前弹出窗索引
            layer.close(CurrentIndex); //关闭弹出窗
        });
    }
    //批量删除
    function all_deleterow() {
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
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'TypeCode') + "|"; //获取所有选中的id值
                }
                var str = $.ajax({ url: "../../../AppModules/HyManage/Del.ashx?flag=delete&TypeId=" + id + "", async: false }).responseText;
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                if (str == "true") {
                    alert("删除成功!");
                }
                //                else {
                //                    alert("删除失败!");
                //                }
            }
        }
    }

    function ref() {
        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
    }
</script>
<!--表格数据源绑定end-->
