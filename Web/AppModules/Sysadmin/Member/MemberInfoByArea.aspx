<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberInfoByArea.aspx.cs"
    Inherits="AppModules_Sysadmin_Member_MemberTotal" %>

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
    <!--search-->
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>会员类别：</span><asp:DropDownList ID="ddlMemberType" runat="server">
                    <asp:ListItem Value="" Selected="True" Text="--请选择--"></asp:ListItem>
                    <asp:ListItem Value="QY">企业</asp:ListItem>
                    <asp:ListItem Value="XH">协会</asp:ListItem>
                    <asp:ListItem Value="XZ">行政</asp:ListItem>
                </asp:DropDownList>
                <span>注册类型：</span><asp:DropDownList ID="ddlRegType" runat="server">
                    <asp:ListItem Value="" Selected="True" Text="--请选择--"></asp:ListItem>
                    <asp:ListItem Value="PT">普通</asp:ListItem>
                    <asp:ListItem Value="XFQ">消费券</asp:ListItem>
                </asp:DropDownList>
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                <%-- <input type="button" class="QueryBut" value="添&nbsp;加" title="添加" onclick="button_actions('add','')" />--%>
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
        url: 'MemberCountHandler.ashx?flag=list&area=<%=area %>',
        datatype: "json",
        colNames: ['编号', '用户名', '单位名称', '组织机构代码', '单位地址', '联系电话', '会员类型', '注册类别', '注册时间', '操作'],
        colModel: [
            { name: 'memberId', hidden: true },
            { name: 'memberName', index: 'memberName', width: 50 },
   		    { name: 'memberCompanyName', index: 'memberCompanyName', width: 140 },
            { name: 'memberCompanyCode', index: 'memberCompanyCode', width: 50 },
   		    { name: 'address', index: 'address', width: 50 },
   		    { name: 'tel', index: 'tel', width: 60 },
            { name: 'memberType', index: 'memberType', width: 40 },
             { name: 'RegType', index: 'RegType', width: 60 },
             { name: 'regTime', index: 'regTime', hidden: true },
            { name: 'cmd_col', align: 'center', hidden: true }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'regTime',
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
                    var writeData = { cmd_col: "<input type='button' class='CommonButon' value='审核' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">" }
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

    //查询
    function grid_search() {
        var _MemberType = $("#ddlMemberType").val();
        var _RegType = $("#ddlRegType").val();

        var _searchfilter = "MemberType$$" + _MemberType + "_$$_RegType$$" + _RegType;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MemberCountHandler.ashx?flag=list&area=<%=area %>&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
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

    
</script>
