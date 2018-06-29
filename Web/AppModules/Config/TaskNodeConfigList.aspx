<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskNodeConfigList.aspx.cs" Inherits="AppModules_Member_MemberList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>环节配置</title>
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
        .divSearch{ float:left; width:190px}
        .divSearch span{color: #2871a4; float:left; font-weight:bold;}
        .divSearch input{background: none repeat scroll 0 0 #fff;
    border: 1px solid #ccc;
    height: 20px;
    line-height: 20px; float:left;}
    .divSearchUC{float:left; width:325px; height:30px;}
    .divSearch select{background: none repeat scroll 0 0 #fff;
    border: 1px solid #ccc;
    height: 22px;float:left;}
    .divSearchUC .SelectBut{border:none;width:65px;height:25px;background:url(../../../Images/LogOutN.png);color:#9b0700;text-shadow:0 1px #ffe191;margin:0 10px;padding:0;text-align:center;cursor:pointer;float:left;}
    .divSearchUC .SelectBut:hover{background:url(../../../Images/LogOutA.png);}
    .spanSearch{padding:0 0 0 10px;font-weight:bold;color:#2871a4;float:left; line-height:25px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    

    <div class="PosiBar">
        <p>
            环节配置列表</p>
    </div>

    
    <div class="SosoBar Left">
        <table>
            <tr>
                <td style=" width:80%">
                    <div class="divSearch" style='width: 600px'>
                        <span class="spanSearch">环节名称：</span>
                        <input style="width: 100px;" id="node_name" />
                        <span class="spanSearch">是否启用：</span>
                        <select id="if_use">
                            <option value=""></option>
                            <option value="1">是</option>
                            <option value="0">否</option>
                        </select>
                    </div>
                </td>
                <td style=" width:20%">
                    <div class="divSearchUC"><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                    <asp:Button ID="btnAdd" runat="server" Text=" 添 加 " OnClientClick="button_actions('add'); return false;" CssClass="Green InputClass"/>
                    <asp:Button ID="btnDelete" runat="server" Text="批量删除" OnClientClick="return button_actions('deleteAll')" CssClass="Red InputClass"/>
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
        url: 'TaskNodeConfigList.aspx?flag=list&task_id=<%=task_id%>',
        datatype: "json",
        colNames: ['环节ID', '环节编码', '环节序号', '环节名称', '备注', '是否启用', '操作'],
        colModel: [
            { name: 'node_id', index: 'node_id', width: 60 },
            { name: 'node_code', index: 'node_code', width: 60, align: 'center' },
            { name: 'node_sn', index: 'node_sn', width: 60, align: 'center', },
   		    { name: 'node_name', index: 'node_name', width: 60, align: 'center' },
            { name: 'remarks', index: 'remarks', width: 90, align: 'center' },
            { name: 'if_use', index: 'if_use', width: 60, align: 'center' },
            { name: 'cmd_col', align: 'center', width: 80 }
   	    ],
        rowList: [10],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'node_id',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "node_id");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.node_id;
                    var if_use = rowData.if_use == 1 ? "是" : "否";
                    var writeData = {
                        if_use: if_use,
                        cmd_col: "<input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\">"
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
        else if (flag == "deleteAll") {
            deleteAction();
            return false;
        }

    }


    //查询
    function grid_search() {
        var node_name = $("#node_name").val();
        var if_use = $("#if_use").val();
        var _searchfilter = "node_name$$" + node_name + "_$$_if_use$$" + if_use;  //+ "_$$_Pid$$" + _Pid;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "TaskNodeConfigList.aspx?flag=list&task_id=<%=task_id%>&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
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
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'node_id') + "|"; //获取所有选中的id值
                }
                var str = $.ajax({ url: "TaskNodeConfigList.aspx?flag=delete&id=" + id + "", async: false }).responseText;
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
            iframe: { src: 'TaskNodeConfigDetail.aspx?task_id=<%=task_id%>&id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

</script>
