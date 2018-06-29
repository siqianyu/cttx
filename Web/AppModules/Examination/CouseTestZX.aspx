<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouseTestZX.aspx.cs" Inherits="Admin_AppModules_Question_CouseTestZX" %>


<%@ Register Src="~/Controls/Pagination.ascx" TagName="Pagination" TagPrefix="uc11" %>
<%@ Register Src="~/Controls/TopButtons.ascx" TagName="TopButtons" TagPrefix="uc21" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
       <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .divSearch
        {
            float: left;
            width: 220px;
        }
        .divSearch span
        {
            color: #2871a4;
            float: left;
            font-weight: bold;
        }
        .divSearch input
        {
            background: none repeat scroll 0 0 #fff;
            border: 1px solid #ccc;
            height: 20px;
            line-height: 20px;
            float: left;
        }
        .divSearchUC
        {
            float: left;
            width: 325px;
            height: 30px;
        }
        .divSearch select
        {
            background: none repeat scroll 0 0 #fff;
            border: 1px solid #ccc;
            height: 22px;
            float: left;
        }
        .divSearchUC .SelectBut
        {
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
        .divSearchUC .SelectBut:hover
        {
            background: url(../../../Images/LogOutA.png);
        }
        .spanSearch
        {
            padding: 0 0 0 10px;
            font-weight: bold;
            color: #2871a4;
            float: left;
            line-height: 25px;
        }
        .InputClass
        {
            background: url(../../../Images/Opera.png) repeat-x;
            border: 1px solid #d0d0d0;
            float: left;
            margin: 3px 0 0 7px;
            display: inline;
            padding: 0 0;
            line-height: 22px;
            height: 22px;
            overflow: hidden;
        }
        .InputClass:hover
        {
            text-decoration: underline;
        }
        .Gray
        {
            color: #5a5a5a;
        }
        .LightGray
        {
            color: #969696;
        }
        .Green
        {
            color: #0f7e00;
        }
        .LightGr
        {
            color: #2aa21a;
        }
        .DarkRed
        {
            color: #b22300;
        }
        .Red
        {
            color: #f00;
        }
        .goodsimgstyle img
        {
            padding: 3px;
        }
    </style>
    
</head>
<body>
        <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hid_goodsId" />
        <asp:HiddenField ID="hid_pgoodsid" runat="server" />
        <asp:HiddenField ID="hid_categoryId" runat="server" />
           <div class="PosiBar">
        <p>
            组卷管理</p>
    </div>
       <div class="SosoBar Left">
        <table>
            <tr>
            <td style=" width:80%"></td>
                <td style=" width:20%">
                    <div class="divSearchUC"><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                     <asp:Button ID="btnAdd" runat="server" Text=" 添 加 " OnClientClick="button_actions('add');return false;" CssClass="Green InputClass"/>
                     <asp:Button ID="BtnApprove" runat="server"  Text=" 返 回 "  OnClientClick="return button_actions('back');"  CssClass="DarkRed InputClass" />
                    </div>
                </td>

             </tr>
        </table>
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
    //Sysnumber, Title, levelFlag, Addtime, shFlag
    jQuery("#startech_table_jqgrid").jqGrid({
        url: 'CouseTestZX.ashx?flag=list&goodsid=' + $("#hid_goodsId").val() + '',
        datatype: "json",
        colNames: ['序号', '标题', '难度系数', '组卷方式', '时间','是否审核', '操作'],
        colModel: [
            { name: 'sysnumber', hidden: true },
            { name: 'Title', index: 'Title', width: 190, align: 'center' },
            { name: 'levelFlag', index: 'levelFlag', width: 60 },
            { name: 'ZjMethod', index: 'ZjMethod', width: 40, align: 'center' },
   		    { name: 'Addtime', index: 'Addtime', width: 80, align: 'center' },
            { name: 'shFlag', index: 'shFlag', width: 40, align: 'center' },
            { name: 'cmd_col', align: 'center', width: 120 },
   	    ],
        rowList: [10],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'Addtime',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "sysnumber");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.sysnumber;
                    var writeData = {
                        cmd_col: "<input type='button' class='CommonButon' value='试题管理' onclick=\"button_actions('shiti','" + id + "','" + rowData.ZjMethod + "')\"> <input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">"
                    }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                    //<input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "')\">
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
            add_method();
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "edit") {
            edit_method(id);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        } else if (flag == "show") {
            show_method(id);
        }
        else if (flag == "shiti") {
            //download_method(id, lang);
        if (lang == "人工") {
            var _url = "QuestionListDialogType.aspx?taskId=" + $("#hid_goodsId").val() + "&courseId=" + $("#hid_pgoodsid").val() + "&testSysnumber=" + id;
            window.open(_url);
        } else {
            location.href = "AddCourseZXQuestionAuto.aspx?pgoodsId=" + $("#hid_pgoodsid").val() + "&goodsId=" + $("#hid_goodsId").val() + "&Nid=" + id; 
        }
        }
        else if (flag == "delete") {
            delete_method(id);
        } 
        else if (flag == "back") {
            location.href = "../Goods/SubGoodsList.aspx?categoryId=" + $("#hid_categoryId").val() + "&goodsId=" + $("#hid_goodsId").val();
            return false;
        } 
        else if (flag == "approve") {
            approve_method(id);
        }
        else if (flag == "approveAll") {
            approveAll();
            return false;
        }
        if (flag == "deleteAll") {
            deleteAction();
            return false;
        }
    }

    //审核
    function approve_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['请选择审核结果', true],
            maxmin: true,
            iframe: { src: 'Approve.aspx?Qid=' + id },
            area: ['175px', '80px'],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //查询
    function grid_search() {
        var _questionTitle = $("#questionTitle").val();

        var _searchfilter = "questionTitle$$" + _questionTitle; //+ "_$$_AreaId$$" + _AreaId + "_$$_Pid$$" + _Pid;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "CouseTestZX.ashx?flag=list&goodsid=" + $("#hid_goodsId").val() + "&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    //添加
    function add_method() {
        //location.href = "AddCourseZX.aspx?goodsId="+$("#hid_goodsId").val();
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['添加', true],
            maxmin: true,
            iframe: { src: 'AddCourseZX.aspx?goodsId=' + $("#hid_goodsId").val() + '&r=' + Math.random() },
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
            iframe: { src: 'AddCourseZX.aspx?nid=' + id },
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
            var str = $.ajax({ url: "CouseTestZX.ashx?flag=delete&id=" + id + "&r=" + Math.random(), async: false }).responseText;
            if (str == "true") {
                alert("删除成功！");
            }
            else {
                alert("删除失败！");
            }
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
            iframe: { src: 'AddPeriodical.aspx?rd=1&id=' + id },
            area: ['1000px', '550px'],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                //jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //查看文件
    function download_method(id, lang) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['查看', true],
            maxmin: true,
            iframe: { src: 'StandardHandle.ashx?lang=' + lang + '&id=' + id },
            area: ['1000px', '550px'],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }


    //批量审核
    function approveAll() {
        var IsCheck = false;
        var inputList = document.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var oInput = inputList[i];
            if (oInput.type == "checkbox" && oInput.checked) {
                IsCheck = true;
            }
        }
        if (!IsCheck) {
            alert("对不起，您尚未选择要审核的选项");
            return IsCheck;
        } else {
            var rowData = jQuery('#startech_table_jqgrid').jqGrid('getGridParam', 'selarrrow');
            var id = "";
            for (var i = 0; i < rowData.length; i++) {
                id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'IDX') + "|"; //获取所有选中的id值
            }
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['请选择审核结果', true],
                maxmin: true,
                iframe: { src: 'Approve.aspx?Qid=' + id },
                area: ['175px', '80px'],
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
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'area_id') + "|"; //获取所有选中的id值
                }
                var str = $.ajax({ url: "QuestionList.ashx?flag=delete&id=" + id + "", async: false }).responseText;
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