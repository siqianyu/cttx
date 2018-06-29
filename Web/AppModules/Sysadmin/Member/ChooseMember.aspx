<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseMember.aspx.cs" Inherits="MemberList" %>

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
    <script>
        function CheckBoxValue(mid) {
            var str = mid; //debugger;
            window.returnValue = str;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--search-->
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>用户名：</span><input style="width: 100px;" id="txtMemberName" />
                <span>真实姓名：</span><input style="width: 100px;" id="txtTrueName" />
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
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
        url: 'MemberHandler.ashx?flag=list',
        datatype: "json",
        colNames: ['编号', '用户名', '性别', '真实姓名', '会员类型', '审核标示', '会员状态', '注册时间', '操作'],
        colModel: [
            { name: 'memberId', hidden: true },
            { name: 'memberName', index: 'memberName', width: 80 },
   		    { name: 'sex', index: 'sex', width: 50 },
            { name: 'memberTrueName', index: 'memberTrueName', width: 50 },
   		    { name: 'memberType', index: 'memberType', width: 50 },
   		    { name: 'shFlag', index: 'shFlag', width: 60 },
            { name: 'memberStatus', index: 'memberStatus', width: 40 },
             { name: 'regTime', index: 'regTime', width: 80 },
            { name: 'cmd_col', align: 'center' }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'levelServiceStartTime',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "memberName");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var memberName = rowData.memberName;
                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='选择' onclick=\"CheckBoxValue('" + memberName + "')\">" }
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
    }

    //查询
    function grid_search() {
        var _memberName = $("#txtMemberName").val();
        var _memberTrueName = $("#txtTrueName").val();

        var _searchfilter = "memberName$$" + _memberName + "_$$_memberTrueName$$" + _memberTrueName;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MemberHandler.ashx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    //添加
    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['会员添加', true],
            maxmin: true,
            iframe: { src: 'AddMember_Year.aspx?r=' + Math.random() },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //会员升级
    function edit_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['会员升级', true],
            maxmin: true,
            iframe: { src: 'MemberLevelUp.aspx?memberid=' + id },
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
            var str = $.ajax({ url: "MemberHandler.ashx?flag=delete&id=" + id + "", async: false }).responseText;
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
            title: ['查看会员信息', true],
            maxmin: true,
            iframe: { src: 'MemberDetail.aspx?userid=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                //jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //验证消费券
    //    function check_xfq(id) {
    //        $.layer({
    //            type: 2,
    //            shade: [0.1, '#000'],
    //            fix: false,
    //            title: ['验证消费券', true],
    //            maxmin: true,
    //            iframe: { src: '../xfq/xfq_check.aspx?memberid=' + id },
    //            area: [document.body.scrollWidth - 20, $(document).height()],
    //            offset: ['0px', ''],
    //            close: function (index) {
    //                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
    //                //jQuery("#startech_table_jqgrid").trigger("reloadGrid");
    //            }
    //        });
    //    }

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
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'memberId') + "|"; //获取所有选中的id值
                }
                var str = $.ajax({ url: "MemberHandler.ashx?flag=delete&id=" + id + "", async: false }).responseText;
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
<%--<script language="javascript">
    //-----------div层父窗口交互控制------------//
    function layer_close() {
        var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
        parent.layer.close(layer_index);
    }
</script>--%>
