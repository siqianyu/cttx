<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionListDialog.aspx.cs" Inherits="AppModules_Examination_QuestionListDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>所有习题</title>
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
        .divSearch{ float:left; width:220px}
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
    .InputClass{background:url(../../../Images/Opera.png) repeat-x;border:1px solid #d0d0d0;float:left;margin:3px 0 0 7px;display:inline;padding:0 0;line-height:22px; height:22px; overflow:hidden;}
    .InputClass:hover{text-decoration:underline}
    .Gray{color:#5a5a5a;}
    .LightGray{color:#969696}
    .Green{color:#0f7e00;}
    .LightGr{color:#2aa21a;}
    .DarkRed{color:#b22300}
    .Red{color:#f00}
    .goodsimgstyle img{padding:3px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidCourseId" runat="server"/>
    <asp:HiddenField ID="hidTestSysnumber" runat="server"/>
<!--select-->
        <div class="PosiBar">
        <p>
            已选择</p>
    </div>
    <div class="SosoBar Left">
        <table>
            <tr>
                <td style=" width:80%">
                    <div class="divSearch">

                    <div class="spanSearch" >题目：</div>
                    <input style="width: 150px;" id="questionTitle2" />
                    </div>
                    <div class="divSearch"><span class="spanSearch">类型：</span>
                    <asp:DropDownList ID="questionType2" runat="server" Width="104px">
                        <asp:ListItem Value="" Selected="True" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Enabled="true" Text="单选题" Value="单选题"></asp:ListItem>
                                <asp:ListItem Text="多选题" Value="多选题"></asp:ListItem>
                                <asp:ListItem Text="不定项选择题" Value="不定项选择题"></asp:ListItem>
                                <asp:ListItem Text="判断题" Value="判断题"></asp:ListItem>
                                <asp:ListItem Text="简答题" Value="简答题"></asp:ListItem>
                                <asp:ListItem Text="计算分析题" Value="计算分析题"></asp:ListItem>
                                <asp:ListItem Text="综合题" Value="综合题"></asp:ListItem>
                    </asp:DropDownList>
                    
                    </div>
                </td>
                <td style=" width:20%">
                    <div class="divSearchUC">
                    
                    <input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="grid_search2()" />
                    
<%--                     <asp:Button ID="btnAdd" runat="server" Text=" 添 加 " OnClientClick="button_actions('add'); return false;" CssClass="Green InputClass"/>
                     <asp:Button ID="Button2" runat="server" Text="Excel导入" OnClientClick="button_actions('excel'); return false;" CssClass="Green InputClass"/>
                     <asp:Button ID="Button1" runat="server" Text="案例管理" OnClientClick="button_actions('al'); return false;" CssClass="Green InputClass"/>--%>
                    <asp:Button ID="Button1" runat="server" Text="移除试卷" OnClientClick="delsj_method(); return false;" CssClass="Green InputClass"/>
                    <asp:Button ID="Button2" runat="server" Text="显示全部试题" OnClientClick="grid_search2_all(); return false;" CssClass="Green InputClass"/>
                    <asp:Button ID="Button4" runat="server" Text="预览" OnClientClick="yl_test(); return false;" CssClass="Green InputClass"/>
                    
                    </div>
                </td>
            </tr>
        </table>
    </div>
    
        <div class="TableBox Left" style="height:350px; overflow:scroll">
        <table id="startech_table_jqgrid2">
        </table><div id="startech_table_jqgrid_pager2">
        </div>
    </div>
    <!--no select-->
    <div class="PosiBar">
        <p>
            未选择</p>
    </div>
<div class="SosoBar Left">
        <table>
            <tr>
                <td style=" width:80%">
                    <div class="divSearch">

                    <div class="spanSearch" >题目：</div>
                    <input style="width: 150px;" id="questionTitle" />
                    </div>
                    <div class="divSearch"><span class="spanSearch">类型：</span>
                    <asp:DropDownList ID="questionType" runat="server" Width="104px">
                        <asp:ListItem Value="" Selected="True" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Enabled="true" Text="单选题" Value="单选题"></asp:ListItem>
                                <asp:ListItem Text="多选题" Value="多选题"></asp:ListItem>
                                <asp:ListItem Text="不定项选择题" Value="不定项选择题"></asp:ListItem>
                                <asp:ListItem Text="判断题" Value="判断题"></asp:ListItem>
                                <asp:ListItem Text="简答题" Value="简答题"></asp:ListItem>
                                <asp:ListItem Text="计算分析题" Value="计算分析题"></asp:ListItem>
                                <asp:ListItem Text="综合题" Value="综合题"></asp:ListItem>
                    </asp:DropDownList>
                    
                    </div>
                </td>
                <td style=" width:20%">
                    <div class="divSearchUC"><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                    
<%--                     <asp:Button ID="btnAdd" runat="server" Text=" 添 加 " OnClientClick="button_actions('add'); return false;" CssClass="Green InputClass"/>
                     <asp:Button ID="Button2" runat="server" Text="Excel导入" OnClientClick="button_actions('excel'); return false;" CssClass="Green InputClass"/>
                     <asp:Button ID="Button1" runat="server" Text="案例管理" OnClientClick="button_actions('al'); return false;" CssClass="Green InputClass"/>--%>
                    <asp:Button ID="Button3" runat="server" Text="加入试卷" OnClientClick="button_actions('addsj'); return false;" CssClass="Green InputClass"/>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="TableBox Left" style="height:350px; overflow:scroll">
        <table id="startech_table_jqgrid">
        </table>
        <div id="startech_table_jqgrid_pager">
        </div>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
jQuery("#startech_table_jqgrid2").jqGrid({
    url: 'QuestionList.ashx?flag=listdialog_select&testSysnumber=' + $("#hidTestSysnumber").val() + '&categoryId=' + $("#hidCourseId").val() + '',
        datatype: "json",
        colNames: ['序号', '所属课程/任务', '题目类型', '题目标题', '案例ID', '难度系数', '所属任务', '时间', '审核', '操作'],
        colModel: [
            { name: 'sysnumber', hidden: true },
            { name: 'GoodsName', index: 'GoodsName', width: 60 },
            { name: 'questionType', index: 'questionType', width: 40, align: 'center' },
            { name: 'questionTitle', index: 'questionTitle', width: 160 },
            { name: 'mainQuestionSysnumber', index: 'mainQuestionSysnumber', width: 40 },
   		    { name: 'levelPoint', index: 'levelPoint', width: 40, align: 'center' },
            { name: 'courseId', index: 'courseId', width: 60, align: 'left', hidden: true },
            { name: 'createTime', index: 'createTime', width: 60, align: 'center', hidden: true },
            { name: 'shFlag', index: 'shFlag', width: 30, align: 'center', hidden: true },
            { name: 'cmd_col', align: 'center', width: 60, hidden: true },
   	    ],
        rowList: [1000],
        rowNum:1000,
        pager: '#startech_table_jqgrid_pager2',
        sortname: 'courseId,questionType,createTime',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid2").getGridParam("selrow");
                var id = $("#startech_table_jqgrid2").getCell(gr, "sysnumber");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid2').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid2").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.sysnumber;
                    var writeData = {
                        cmd_col: "<input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">"
                    }
                    $('#startech_table_jqgrid2').jqGrid('setRowData', ids[i], writeData);
                    //<input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "')\">
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid2").jqGrid('navGrid', '#startech_table_jqgrid_pager2', { edit: false, add: false, del: false });

    //刷新当前页面
    function freshCurrentPage() {
        jQuery("#startech_table_jqgrid2").trigger("reloadGrid");
    }
        
    //查询
    function grid_search2() {
        var _questionTitle = $("#questionTitle2").val();
        var _questionType = $("#questionType2").val();

        var _searchfilter = "questionTitle$$" + _questionTitle + "_$$_questionType$$" + _questionType; //+ "_$$_Pid$$" + _Pid;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid2").jqGrid('setGridParam', { url: "QuestionList.ashx?flag=listdialog_select&testSysnumber="+$("#hidTestSysnumber").val()+"&categoryId=" + $("#hidCourseId").val() + "&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    //all
    function grid_search2_all() {
        jQuery("#startech_table_jqgrid2").jqGrid('setGridParam', { url: "QuestionList.ashx?flag=listdialog_select&testSysnumber=" + $("#hidTestSysnumber").val() + "&categoryId=&searchfilter=", page: 1 }).trigger("reloadGrid");
    }

    //show
    function yl_test() {
        var url = "TestDetailZX.aspx?Nid=" + $("#hidTestSysnumber").val() + "&view=1";
        window.open(url);
    }

    //移除试卷
    function delsj_method() {
        var IsCheck = false;
        var inputList = document.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var oInput = inputList[i];
            if (oInput.type == "checkbox" && oInput.checked) {
                IsCheck = true;
            }
        }
        if (!IsCheck) {
            alert("对不起，您尚未选择要移除的选项");
            return IsCheck;
        }
        else {
            if (confirm('确定要批量移除这些吗?')) {
                var rowData = jQuery('#startech_table_jqgrid2').jqGrid('getGridParam', 'selarrrow');
                if (rowData.length > 100) { alert("对不起，每次选择不能超过100条数据"); return false; }
                var id = "";
                for (var i = 0; i < rowData.length; i++) {
                    id += jQuery('#startech_table_jqgrid2').jqGrid('getCell', rowData[i], 'sysnumber') + "|"; //获取所有选中的id值
                }
                //alert(id);
                var str = $.ajax({ url: "QuestionList.ashx?flag=delsj&testSysnumber=" + $("#hidTestSysnumber").val() + "&id=" + id + "", async: false }).responseText;
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                if (str == "true") {
                    jQuery("#startech_table_jqgrid2").trigger("reloadGrid");
                    alert("移除成功!");
                }
                else {
                    alert("移除失败!");
                }
            }
        }
    }
</script>


<script language="javascript" type="text/javascript">
    //sysnumber,questionType,questionTitle,questionAnswer,description,
    jQuery("#startech_table_jqgrid").jqGrid({
        url: 'QuestionList.ashx?flag=listdialog&testSysnumber=' + $("#hidTestSysnumber").val() + '&categoryId=' + $("#hidCourseId").val() + '',
        datatype: "json",
        colNames: ['序号','所属课程/任务', '题目类型', '题目标题','案例ID', '难度系数', '所属任务','时间', '审核','操作'],
        colModel: [
            { name: 'sysnumber', hidden: true },
            { name: 'GoodsName', index: 'GoodsName', width: 60 },
            { name: 'questionType', index: 'questionType', width: 40, align: 'center' },
            { name: 'questionTitle', index: 'questionTitle', width: 160 },
            { name: 'mainQuestionSysnumber', index: 'mainQuestionSysnumber', width: 40 },
   		    { name: 'levelPoint', index: 'levelPoint', width: 40, align: 'center' },
            { name: 'courseId', index: 'courseId', width: 60, align: 'left', hidden: true },
            { name: 'createTime', index: 'createTime', width: 60, align: 'center', hidden: true },
            { name: 'shFlag', index: 'shFlag', width: 30, align: 'center', hidden: true },
            { name: 'cmd_col', align: 'center', width: 60, hidden: true },
   	    ],
        rowList: [1000],
        rowNum:1000,
        pager: '#startech_table_jqgrid_pager',
        sortname: 'courseId,questionType,createTime',
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
                        cmd_col: "<input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">"
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
    
    //查询
    function grid_search() {
        var _questionTitle = $("#questionTitle").val();
        var _questionType = $("#questionType").val();
        var _searchfilter = "questionTitle$$" + _questionTitle+ "_$$_questionType$$" + _questionType; // + "_$$_Pid$$" + _Pid;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "QuestionList.ashx?flag=listdialog&testSysnumber="+$("#hidTestSysnumber").val()+"&categoryId=" + $("#hidCourseId").val() + "&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
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
        else if (flag == "al") {
            //download_method(id, lang);
            location.href = "ListQuestionDailog.aspx?courseId=" + $("#hidCategoryId").val();
        }
        else if (flag == "delete") {
            delete_method(id);
        }
        else if (flag == "approve") {
            approve_method(id);
        }
        else if (flag == "approveAll") {
            approveAll();
            return false;
        }
        else if (flag == "deleteAll") {
            deleteAction();
            return false;
        } else if (flag == "excel") {
            excel_method($("#hidCategoryId").val());
            return false;
        } else if (flag == "addsj") {
            addsj_method();
            return false;
        }
    }

    //加入试卷
    function addsj_method() {
        var IsCheck = false;
        var inputList = document.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var oInput = inputList[i];
            if (oInput.type == "checkbox" && oInput.checked) {
                IsCheck = true;
            }
        }
        if (!IsCheck) {
            alert("对不起，您尚未选择要加入的选项");
            return IsCheck;
        }
        else {
            if (confirm('确定要批量加入这些吗?')) {
                var rowData = jQuery('#startech_table_jqgrid').jqGrid('getGridParam', 'selarrrow');
                if (rowData.length > 100) { alert("对不起，每次选择不能超过100条数据"); return false; }
                var id = "";
                for (var i = 0; i < rowData.length; i++) {
                    id += jQuery('#startech_table_jqgrid').jqGrid('getCell', rowData[i], 'sysnumber') + "|"; //获取所有选中的id值
                }
                //alert(id);
                var str = $.ajax({ url: "QuestionList.ashx?flag=addsj&testSysnumber=" + $("#hidTestSysnumber").val() + "&id=" + id + "", async: false }).responseText;
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                if (str == "true") {
                    jQuery("#startech_table_jqgrid2").trigger("reloadGrid");
                    alert("加入成功!");
                }
                else {
                    alert("加入失败!");
                }
            }
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

    //excel
    function excel_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['Excel导入', true],
            maxmin: true,
            iframe: { src: 'QuestionAddBat.aspx??flag=course&courseId=' + id },
            area: ['600px', '400px'],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }



    //添加
    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['添加', true],
            maxmin: true,
            iframe: { src: 'AddQuestions.aspx?courseId=' + $("#hidCategoryId").val() + '&r=' + Math.random() },
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
            iframe: { src: 'AddQuestions.aspx?courseId=' + $("#hidCategoryId").val() + '&nid=' + id },
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
            var str = $.ajax({ url: "QuestionList.ashx?flag=delete&id=" + id + "&r=" + Math.random(), async: false }).responseText;
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