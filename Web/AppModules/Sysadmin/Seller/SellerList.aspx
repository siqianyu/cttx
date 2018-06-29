<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellerList.aspx.cs" Inherits="AppModules_Sysadmin_Seller_SellerList" %>

<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实体卖家信息列表</title>
    <link href="/Style/List.css" rel="stylesheet" type="text/css" />
    <link href="/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="/js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="/js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="/js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <style>
        #ddlOpen
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">
                <span>公司名称：</span><input style="width: 150px;" id="txtCompanyName" />
                <span>店铺名称：</span><input style="width: 150px;" id="txtShopName" />
                <span>联系人：</span><input style="width: 150px;" id="txtLinkMan" />
                <span>账户状态：</span>
                <asp:DropDownList ID="ddlAccount" runat="server">
                    <asp:ListItem Value="">--全部--</asp:ListItem>
                    <asp:ListItem Value="Normal">正常</asp:ListItem>
                    <asp:ListItem Value="Disable">禁用</asp:ListItem>
                </asp:DropDownList>
                <%--<span>店铺状态：</span>--%>
                <asp:DropDownList ID="ddlOpen" runat="server">
                    <asp:ListItem Value="">--全部--</asp:ListItem>
                    <asp:ListItem Value="1">开启</asp:ListItem>
                    <asp:ListItem Value="0">关闭</asp:ListItem>
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
        url: 'SellerService.ashx?flag=list&type=<%=type %>',
        datatype: "json",
        colNames: ['编号', '所在区域', '农贸市场', '公司名称', '店铺名称', '出售类型', '联系人', '联系电话', '账户状态', '店铺状态', '操作'],
        colModel: [
            { name: 'ShopId', hidden: true },
            { name: 'areaname', index: 'areaname', width: 50, align: 'center' },
            { name: 'marketname', index: 'marketname', width: 60, align: 'center' },
            { name: 'CompanyName', index: 'CompanyName', width: 80 },
   		    { name: 'ShopName', index: 'ShopName' },
   		    { name: 'CategoryName', index: 'CategoryName', width: 150, align: 'center' },
            { name: 'LinkMan', index: 'LinkMan', width: 40, align: 'center' },
            { name: 'Phone', index: 'Phone', width: 50, align: 'center' },
            { name: 'AccoutsState', index: 'AccoutsState', width: 50, align: 'center' },
            { name: 'isOpen', index: 'openStatue', width: 50, align: 'center' },
            { name: 'cmd_col', align: 'center', width: 100 }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: ' applytime ',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "ShopId");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.ShopId;
                    var isOpen = { isOpen: "" };
                    if (rowData["isOpen"] && rowData["isOpen"].toString() == "1") {
                        isOpen = { isOpen: "<font style='color:green'>开启</font>" };
                    }
                    else {
                        isOpen = { isOpen: "<font style='color:red'>关闭</font>" };
                    }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], isOpen);

                    if ("<%=type %>" === "1" || "<%=type %>" === "4") {
                        //正常运营的店铺（账户正常 店铺开启）
                        var writeData = { cmd_col: "<input type='button' class='CommonButon' value='设置' onclick=\"button_actions('set','" + id + "')\"> <input type='button' class='CommonButon' value='重置密码' onclick=\"button_actions('init','" + id + "')\">" }
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                    }
                    else if ("<%=type %>" === "2") {
                        // 关闭的店铺
                        var writeData = { cmd_col: "<input type='button' class='CommonButon' value='开启' onclick=\"button_actions('open','" + id + "')\">" }
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                    }
                    else if ("<%=type %>" === "0") {
                        //未审核的店铺
                        var writeData = { cmd_col: "<input type='button' class='CommonButon' value='审核' onclick=\"button_actions('check','" + id + "')\">" }
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                    }
                    else if ("<%=type %>" === "3") {
                        //审核不通过的店铺
                        var writeData = { cmd_col: "<input type='button' class='CommonButon' value='重新审核' onclick=\"button_actions('check','" + id + "')\">" }
                        $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
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
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "check") {
            check_method(id); //审核卖家信息
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "set") {
            set_method(id); //设置卖家信息
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag === "init" || flag === "open") {
            init_method(id, flag); //重置密码 开启店铺
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "delete") {
            delete_method(id);
        }
    }

    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['商家信息添加', true],
            maxmin: true,
            iframe: { src: 'AddSeller.aspx' },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {

                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //查询
    function grid_search() {
        var _CompanyName = $("#txtCompanyName").val();
        var _ShopName = $("#txtShopName").val();
        var _LinkMan = $("#txtLinkMan").val();
        var _isOpen = $("#ddlOpen").val();
        var _accoutsState = $("#ddlAccount").val();

        var _searchfilter = "CompanyName$$" + _CompanyName + "_$$_ShopName$$" + _ShopName + "_$$_LinkMan$$" + _LinkMan + "_$$_isOpen$$" + _isOpen + "_$$_AccoutsState$$" + _accoutsState;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "SellerService.ashx?flag=list&type=<%=type %>&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    //删除
    function delete_method(id) {
        layer.confirm('确定要删除该配送员的个人信息吗?', function () {
            var str = $.ajax({ url: "SellerService.ashx?flag=delete&id=" + id + "", async: false }).responseText;
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            var index = layer.index; //获取当前弹出窗索引
            layer.close(index); //关闭弹出窗
        });
    }

    //设置
    function set_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['设置', true],
            maxmin: true,
            iframe: { src: 'CheckSeller.aspx?rd=1&id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {

                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    function check_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['审核', true],
            maxmin: true,
            iframe: { src: 'CheckSeller.aspx?rd=1&id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {

                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //初始化密码
    function init_method(id, flag) {
        if (flag === "init") {
            layer.confirm('确定重置该商家密码为"111111"吗？', function () {
                var str = $.ajax({ url: "SellerService.ashx?flag=" + flag + "&id=" + id + "", async: false }).responseText;
                var index = layer.index; //获取当前弹出窗索引
                layer.close(index); //关闭弹出窗
                layer.alert(str, 1, "密码重置");
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            });

        }
        else if (flag === "open") {
            layer.confirm('确定开启该店铺吗？', function () {
                var str = $.ajax({ url: "SellerService.ashx?flag=" + flag + "&id=" + id + "", async: false }).responseText;
                var index = layer.index; //获取当前弹出窗索引
                layer.close(index); //关闭弹出窗
                layer.alert(str, 1, "店铺开启");
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
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
            if (confirm('确定要批量删除这些配送员的个人信息吗?')) {
                var rowData = jQuery('#startech_table_jqgrid').jqGrid('getGridParam', 'selarrrow');
                var id = "";
                for (var i = 0; i < rowData.length; i++) {
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'postman_id') + ","; //获取所有选中的id值
                }
                var str = $.ajax({ url: "SellerService.ashx?flag=delete&id=" + id + "", async: false }).responseText;
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
