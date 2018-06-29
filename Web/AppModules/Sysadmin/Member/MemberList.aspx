<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberList.aspx.cs" Inherits="MemberList" %>

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
            <%=title %>
        </p>
    </div>
    <!--search-->
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>用户名：</span><input style="width: 100px;" id="txtMemberName" runat="server" />
                <span>单位名称：</span><input style="width: 100px;" id="txtCompanyName" runat="server" />
                <span>真实姓名：</span><input style="width: 100px;" id="txtTrueName" runat="server" />
                <span>会员级别：</span><asp:DropDownList ID="ddlMemLevel" runat="server" Width="100px">
                </asp:DropDownList>
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                <%-- <input type="button" class="QueryBut" value="添&nbsp;加" title="添加" onclick="button_actions('add','')" />--%>
                <uc2:Add ID="Add1" runat="server" />
                <uc3:Edit ID="Edit1" runat="server" />
                <uc6:Show ID="Show1" runat="server" />
                <uc4:Delete ID="Delete1" runat="server" />
                <uc1:OutputExcel ID="OutputExcel1" runat="server" />
               <%-- <asp:Button ID="btn" runat="server" Text="下载" OnClick="btn_ExprotMemListByFilter" />--%>
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
        url: 'MemberHandler.ashx?flag=list&type=<%=type %>',
        datatype: "json",
        colNames: ['编号', '账号', '单位名称', '真实姓名', '会员级别', '审核标示', '会员状态', '注册时间', '操作'],
        colModel: [
            { name: 'memberId', hidden: true },
            { name: 'memberName', index: 'memberName', width: 60 },
   		    { name: 'memberCompanyName', index: 'memberCompanyName', width: 120 },
            { name: 'memberTrueName', index: 'memberTrueName', width: 50 },
   		    { name: 'levelname', index: 'levelname', width: 50 },
            { name: 'shFlag', index: 'shFlag', width: 60 },
            { name: 'memberStatus', index: 'memberStatus', width: 40 },
            { name: 'regTime', index: 'regTime', width: 50 },
            { name: 'cmd_col', align: 'center' }
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
                    if ('<%=type %>' == 'QY' || '<%=type %>'.toLocaleUpperCase() == 'COM') {
                        var writeData = { cmd_col: "<input type='button' class='CommonButon' value='审核' onclick=\"button_actions('edit','" + id + "','<%=type %>')\"> <input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "','<%=type %>')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\"> <input type='button' class='CommonButon' value='会员升级' onclick=\"button_actions('levelup','" + id + "')\">" }
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                    }
                    else {
                        var writeData = { cmd_col: "<input type='button' class='CommonButon' value='修改' onclick=\"button_actions('edit','" + id + "','<%=type %>')\"> <input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "','<%=type %>')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">" }
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                    }
                    //显示审核中文结果
                    if (rowData["shFlag"].toString() == "1") {
                        var shFlag = { shFlag: "<font style='color:green'>通过</font>" };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], shFlag);
                    }
                    else {
                        var shFlag = { shFlag: "<font style='color:red;'>未通过</font>" };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], shFlag);
                    }
                    //修改时间显示格式
                    if (rowData["regTime"].toString() != "") {
                        var regTime = { regTime: rowData["regTime"].substring(0, rowData["regTime"].toString().indexOf(' ')).replace(/\//g, "-") };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], regTime);
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
        var _memberName = $("#txtMemberName").val();
        var _memberTrueName = $("#txtTrueName").val();
        var _memberCompanyName = $("#txtCompanyName").val();
        var _memberLevel = $("#ddlMemLevel").val();

        var _searchfilter = "memberName$$" + _memberName + "_$$_memberCompanyName$$" + _memberCompanyName + "_$$_memberTrueName$$" + _memberTrueName + "_$$_memberLevel$$" + _memberLevel;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MemberHandler.ashx?flag=list&type=<%=type %>&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    //添加
    function add_method(type) {
        if ("<%=type %>" == "Com" || "<%=type %>" == "QY") {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['会员添加', true],
                maxmin: true,
                iframe: { src: 'AddMember_Year.aspx?type=<%=type %>&r=' + Math.random() },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }
        else {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['会员添加', true],
                maxmin: true,
                iframe: { src: 'AddMember.aspx?type=<%=type %>&r=' + Math.random() },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }
    }

    //修改
    function edit_method(id, type) {
        if ("<%=type %>" == "Com" || "<%=type %>" == "QY") {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['会员信息修改', true],
                maxmin: true,
                iframe: { src: 'MemberDetail.aspx?userid=' + id },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }
        else {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['会员信息修改', true],
                maxmin: true,
                iframe: { src: 'AddMember.aspx?id=' + id + '&type=' + type + '&r=' + Math.random() },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }
    }

    function levelup_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['会员升级', true],
            maxmin: true,
            iframe: { src: 'MemberLevelUp.aspx?memberid=' + id + '&r=' + Math.random() },
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
    function show_method(id, type) {
        if ("<%=type %>" == "Com" || "<%=type %>" == "QY") {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['会员信息修改', true],
                maxmin: true,
                iframe: { src: 'MemberDetail.aspx?rd=1&userid=' + id },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }
        else {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['会员信息修改', true],
                maxmin: true,
                iframe: { src: 'AddMember.aspx?rd=1&id=' + id + '&type=' + type + '&r=' + Math.random() },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }
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
