<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberListByHy.aspx.cs" Inherits="MemberList" %>

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
            行业会员统计
        </p>
    </div>
    <!--search-->
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>用户名：</span><input style="width: 100px;" id="txtMemberName" runat="server" />
                <span>单位名称：</span><input style="width: 100px;" id="txtCompanyName" runat="server" />
                <span>真实姓名：</span><input style="width: 100px;" id="txtTrueName" runat="server" />
                <input type="button" class="QueryBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
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
        url: 'MemberHandler.ashx?flag=hy&code=<%=code %>',
        datatype: "json",
        colNames: ['编号', '账号', '单位名称', '真实姓名', '会员级别', '审核标示', '会员状态', '注册时间'],
        colModel: [
            { name: 'memberId', hidden: true },
            { name: 'memberName', index: 'memberName', width: 60 },
   		    { name: 'memberCompanyName', index: 'memberCompanyName', width: 120 },
            { name: 'memberTrueName', index: 'memberTrueName', width: 50 },
   		    { name: 'levelname', index: 'levelname', width: 50 },
            { name: 'shFlag', index: 'shFlag', width: 60 },
            { name: 'memberStatus', index: 'memberStatus', width: 40 },
            { name: 'regTime', index: 'regTime', width: 50 }
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

    //查询
    function grid_search() {
        var _memberName = $("#txtMemberName").val();
        var _memberTrueName = $("#txtTrueName").val();
        var _memberCompanyName = $("#txtCompanyName").val();

        var _searchfilter = "memberName$$" + _memberName + "_$$_memberCompanyName$$" + _memberCompanyName + "_$$_memberTrueName$$" + _memberTrueName;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MemberHandler.ashx?flag=hy&code=<%=code %>&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }
</script>
