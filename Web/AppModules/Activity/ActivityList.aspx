<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityList.aspx.cs" Inherits="AppModules_Member_MemberList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>活动列表</title>
    <link href="/Style/List.css" rel="stylesheet" type="text/css" />
    <link href="/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="/js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="/js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="/js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <style type="text/css">
        .divSearch {
            float: left;
            width: 190px
        }

            .divSearch span {
                color: #2871a4;
                float: left;
                font-weight: bold;
            }

            .divSearch input {
                background: none repeat scroll 0 0 #fff;
                border: 1px solid #ccc;
                height: 20px;
                line-height: 20px;
                float: left;
            }

        .divSearchUC {
            float: left;
            width: 325px;
            height: 30px;
        }

        .divSearch select {
            background: none repeat scroll 0 0 #fff;
            border: 1px solid #ccc;
            height: 22px;
            float: left;
        }

        .divSearchUC .SelectBut {
            border: none;
            width: 65px;
            height: 25px;
            background: url(../../../Images/LogOutN.png);
            color: #9b0700;
            text-shadow: 0 1px #ffe191;
            margin: 0 10px;
            padding: 0;
            text-align: center;
            cursor: pointer;
            float: left;
        }

            .divSearchUC .SelectBut:hover {
                background: url(../../../Images/LogOutA.png);
            }

        .spanSearch {
            padding: 0 0 0 10px;
            font-weight: bold;
            color: #2871a4;
            float: left;
            line-height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">



        <div class="PosiBar">
            <p>
                活动列表
            </p>
        </div>


        <div class="SosoBar Left">
            <table>
                <tr>
                    <td style="width: 80%">
                        <div class="divSearch" style='width: 600px'>
                            <span class="spanSearch">活动标题：</span>
                            <input style="width: 100px;" id="activity_title" />
                            <span class="spanSearch">活动状态：</span>
                            <select id="activity_status">
                                <option value=""></option>
                                <option value="0">未开始</option>
                                <option value="1">已开始</option>
                                <option value="2">报名中</option>
                                <option value="-1">已结束</option>
                            </select>
                            <span class="spanSearch">审核状态：</span>
                            <select id="if_approved">
                                <option value=""></option>
                                <option value="0">未审核</option>
                                <option value="1">已审核</option>
                            </select>
                        </div>
                    </td>
                    <td style="width: 20%">
                        <div class="divSearchUC">
                            <input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                            <asp:Button ID="btnAdd" runat="server" Text=" 添 加 " OnClientClick="button_actions('add'); return false;" CssClass="Green InputClass" />
                            <asp:Button ID="btnDelete" runat="server" Text="批量删除" OnClientClick="return button_actions('deleteAll')" CssClass="Red InputClass" />
                            <%--<asp:Button ID="btnAdd" runat="server" Text=" 添 加 " OnClientClick="button_actions('add'); return false;" CssClass="Green InputClass"/>--%>
                            <%--<asp:Button ID="BtnApprove" runat="server" Visible="false"  Text="批量审核"  OnClientClick="return button_actions('approveAll')"  CssClass="DarkRed InputClass" />
                    <asp:Button ID="btnDelete" runat="server" Text="批量删除" OnClientClick="return button_actions('deleteAll')" CssClass="Red InputClass"/>--%>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
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
        url: 'ActivityList.aspx?flag=list',
        datatype: "json",
        colNames: ['活动ID', '活动标题', '活动状态', '审核状态', '教练', '总人数', '创建人', '创建时间', '备注', '操作'],
        colModel: [
            { name: 'activity_id', index: 'activity_id', width: 60 },
            { name: 'activity_title', index: 'activity_title', width: 60, align: 'center' },
            { name: 'activity_status', index: 'activity_status', width: 60, align: 'center', },
            { name: 'if_approved', index: 'if_approved', width: 60, align: 'center' },
            { name: 'manager_id', index: 'manager_id', width: 60, align: 'center' },
            { name: 'activity_person', index: 'activity_person', width: 60, align: 'center' },
            { name: 'create_person', index: 'create_person', width: 60, align: 'center' },
            { name: 'create_time', index: 'create_time', width: 60, align: 'center' },
            { name: 'remarks', index: 'remarks', width: 60, align: 'center' },
            { name: 'cmd_col', align: 'center', width: 80 }
        ],
        rowList: [10],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'activity_id',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "activity_id");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.activity_id;
                    var activity_status = "未开始";
                    switch (rowData.activity_status) {
                        case "0": activity_status = "未开始"; break;
                        case "1": activity_status = "已开始"; break;
                        case "2": activity_status = "报名中"; break;
                        case "-1": activity_status = "已结束"; break;
                    }
                    var if_approved = "未审核";
                    switch (rowData.if_approved) {
                        case "0": if_approved = "未审核"; break;
                        case "1": if_approved = "已审核"; break;
                    }

                    var writeData = {
                        activity_status: activity_status,
                        if_approved:if_approved,
                        cmd_col: "<input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='任务配置' onclick=\"button_actions('edittask','" + id + "')\">"
                    }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //刷新当前页面
    function freshCurrentPage() {
        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
    }

    //button_actions
    function button_actions(flag, id, lang) {
        // id = safeReplace(id);
        if (flag == "add") {
            edit_method('');
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "edit") {
            edit_method(id);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "edittask") {
            task_method(id);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "deleteAll") {
            deleteAction();
            return false;
        }

    }


    //查询
    function grid_search() {
        var activity_title = $("#activity_title").val();
        var activity_status = $("#activity_status").val();
        var if_approved = $("#if_approved").val();
        var _searchfilter = "activity_title$$" + activity_title + "_$$_activity_status$$" + activity_status+ "_$$_if_approved$$" + if_approved;  //+ "_$$_Pid$$" + _Pid;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "ActivityList.aspx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
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
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'activity_id') + "|"; //获取所有选中的id值
                }
                var str = $.ajax({ url: "ActivityList.aspx?flag=delete&id=" + id + "", async: false }).responseText;
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

    //编辑
    function edit_method(id) {
        var title = '添加';
        if (id) {
            title = '编辑';
        }

        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: [title, true],
            maxmin: true,
            iframe: { src: 'ActivityDetail.aspx?id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //任务配置
    function task_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['任务配置', true],
            maxmin: true,
            iframe: { src: 'ActivityTaskList.aspx?activity_id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

</script>
