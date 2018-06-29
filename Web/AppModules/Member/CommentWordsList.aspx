<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommentWordsList.aspx.cs" Inherits="AppModules_Member_MemberList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>评价词管理</title>
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
            width: 190px;
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
    <div class="PosiBar">
        <p>
            评价词管理</p>
    </div>
    <div class="SosoBar Left">
        <table>
            <tr>
                <td style="width: 80%">
                    <div class="divSearch" style='width: 500px'>
                        <span class="spanSearch">评价词语：</span>
                        <input style="width: 100px;" id="txtWords" />
                    </div>
                </td>
                <td style="width: 20%">
                    <div class="divSearchUC">
                        <input type="button" class="InputClass" value="添&nbsp;加" title="添加" onclick="add()"
                            style="padding: 0px 5px" />
                        <input type="button" class="Red InputClass" value="批量删除" title="批量删除" onclick="deleteAll()"
                            style="padding: 0px 5px" />
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
        url: 'CommentWordsHandler.ashx?flag=wordslist',
        datatype: "json",
        colNames: ['编号', '评价词语'],
        colModel: [
            { name: 'id', index: 'id', hidden: true },
            { name: 'word', index: 'word', width: 60, align: 'center' }
   	    ],
        rowList: [10],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'id',
        viewrecords: true,
        sortorder: "asc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "id");
            }
        },
        gridComplete: function () {
            
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //刷新当前页面
    function freshCurrentPage() {
        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
    }

    //查询
    function grid_search() {
        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "CommentWordsHandler.ashx?flag=wordslist", page: 1 }).trigger("reloadGrid");
    }


    //批量删除
    function deleteAll() {
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
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'id') + "|"; //获取所有选中的id值
                }
                var str = $.ajax({ url: "CommentWordsHandler.ashx?flag=deletewords&id=" + id + "", async: false }).responseText;
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                if (str == "1") {
                    alert("删除成功!");
                }
                else {
                    alert("删除失败!");
                }
            }
        }
    }

    function add() {
        var words = $('#txtWords').val();

        if (!words) {
            alert('请填写词语');
            $('#txtWords').focus();
        } else {
            var isTop = $('#txtIsTop').val();
            var str = $.ajax({ url: "CommentWordsHandler.ashx?flag=addwords&words=" + words + "", async: false }).responseText;
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            if (str == "1") {
                alert("添加成功!");
            }
            else {
                alert("添加失败!");
            }
        }
    }
</script>
