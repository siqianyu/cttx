<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Postman_ListInfo.aspx.cs"
    Inherits="AppModules_Postman_Postman_ListInfo" %>

<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>派送员信息列表</title>
    <link href="/Style/List.css" rel="stylesheet" type="text/css" />
    <link href="/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="/js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="/js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="/js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--search-->
    <div class="PosiBar">
        <p>
            派送员信息列表</p>
    </div>
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>用户名：</span><input style="width: 100px;" id="txtName" />
                <span>姓名：</span><input style="width: 100px;" id="txtTrueName" />
                <span>状&nbsp;态：</span><asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="" Selected="True" Text="--全部--"></asp:ListItem>
                    <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                    <asp:ListItem Value="1" Text="正常"></asp:ListItem>
                </asp:DropDownList>
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
        url: 'PostmanHandler.ashx?flag=list',
        datatype: "json",
        colNames: ['编号', '地区', '市场', '用户名', '姓名', '联系方式', '绩效分数', '状态', '添加时间', '操作'],
        colModel: [
            { name: 'postman_id', hidden: true },
            { name: 'areaname', index: 'areaname', width: 50, align: 'center' },
            { name: 'marketname', index: 'marketname', width: 60, align: 'center' },
            { name: 'postman_username', index: 'postman_username', width: 80, align: 'center' },
   		    { name: 'postman_trueName', index: 'postman_trueName', align: 'center' },
   		    { name: 'postman_tel', index: 'postman_tel', width: 50, align: 'center' },
            { name: 'postman_score', index: 'postman_score', width: 40, align: 'center' },
            { name: 'postman_status', index: 'postman_status', width: 40, align: 'center' },
            { name: 'postman_addtime', index: 'postman_addtime', width: 50, align: 'center' },
            { name: 'cmd_col', align: 'center', width: 120 }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'postman_addtime',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "postman_id");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.postman_id;
                    //修改时间显示格式
                    if (rowData["postman_addtime"].toString() != "") {
                        var postman_addtime = { postman_addtime: rowData["postman_addtime"].substring(0, rowData["postman_addtime"].toString().indexOf(' ')).replace(/\//g, "-") };
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], postman_addtime);
                    }

                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">" }
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
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "delete") {
            delete_method(id);
        }
    }

    //查询
    function grid_search() {
        var _postman_usernamee = $("#txtName").val();
        var _postman_trueName = $("#txtTrueName").val();
        var _postman_status = $("#ddlStatus").val();

        var _searchfilter = "postman_username$$" + _postman_usernamee + "_$$_postman_trueName$$" + _postman_trueName + "_$$_postman_status$$" + _postman_status;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "PostmanHandler.ashx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    //添加
    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['添加', true],
            maxmin: true,
            iframe: { src: 'AddPostman.aspx?r=' + Math.random() },
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
            title: ['修改', true],
            maxmin: true,
            iframe: { src: 'AddPostman.aspx?id=' + id },
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
        layer.confirm('确定要删除该配送员的个人信息吗?', function () {
            var str = $.ajax({ url: "PostmanHandler.ashx?flag=delete&id=" + id + "", async: false }).responseText;
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
            iframe: { src: 'AddPostman.aspx?rd=1&id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
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
            if (confirm('确定要批量删除这些配送员的个人信息吗?')) {
                var rowData = jQuery('#startech_table_jqgrid').jqGrid('getGridParam', 'selarrrow');
                var id = "";
                for (var i = 0; i < rowData.length; i++) {
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'postman_id') + ","; //获取所有选中的id值
                }
                var str = $.ajax({ url: "PostmanHandler.ashx?flag=delete&id=" + id + "", async: false }).responseText;
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
