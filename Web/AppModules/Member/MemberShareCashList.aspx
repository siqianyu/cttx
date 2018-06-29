<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberShareCashList.aspx.cs" Inherits="AppModules_Member_MemberShareCashList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员获取代金券</title>
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
    
    

    <div class="PosiBar">
        <p>
            会员获取代金券</p>
    </div>

    
    <div class="SosoBar Left">
        <table>
            <tr>
                <td style=" width:80%">
                    <div class="divSearch" style='width:500px'>

                    <span class="spanSearch" >会员名：</span>
                    <input style="width: 100px;" id="txtMemberName" />

                                        <span  class="spanSearch" >手机号码：</span>
                    <input style="width: 100px;" id="txtTel" />


                    </div>
                    
                </td>
                <td style=" width:20%">
                    <div class="divSearchUC"><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                    
                    <%--<asp:Button ID="btnAdd" runat="server" Text=" 添 加 " OnClientClick="button_actions('add'); return false;" CssClass="Green InputClass"/>--%>
<%--                    <asp:Button ID="BtnApprove" runat="server" Visible="false"  Text="批量审核"  OnClientClick="return button_actions('approveAll')"  CssClass="DarkRed InputClass" />
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
    //Sysnumber,TrueName,Tel,CouponId,CouponType,CouponValue,IsUsed,Remark,ShareFirendTrueName,CreateTime
    jQuery("#startech_table_jqgrid").jqGrid({
        url: 'MemberHandler.ashx?flag=sharecashlist<%=categoryId %>',
        datatype: "json",
        colNames: ['Sysnumber', '真实姓名', '手机号码', '代金券', 'CouponType', '金额', '是否使用', '备注', '来自人员', '时间', '操作'],
        colModel: [
            { name: 'Sysnumber', index: 'Sysnumber',hidden:true},
            { name: 'TrueName', index: 'TrueName', width: 60, align: 'center' },
            { name: 'Tel', index: 'Tel', width: 60, align: 'center' },
   		    { name: 'CouponId', index: 'CouponId', width: 90, align: 'center' },
            { name: 'CouponType', index: 'CouponType', width: 90, align: 'center', hidden: true },
            { name: 'CouponValue', index: 'CouponValue', width: 40, align: 'center' },
            { name: 'IsUsed', index: 'IsUsed', width: 40, align: 'center' },
            { name: 'Remark', index: 'Remark', width: 120, align: 'center' },
            { name: 'ShareFirendTrueName', index: 'ShareFirendTrueName', width: 60, align: 'center' },
            { name: 'CreateTime', index: 'CreateTime', width: 60, align: 'center' },
            { name: 'cmd_col', align: 'center', width: 80, hidden: true }
   	    ],
        rowList: [10],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'CreateTime',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
        edittype: 'checkbox',
        multiselect: true,
        onSelectRow: function (rowid) {
            if (rowid) {
                
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var id = rowData.Sysnumber;
                    var writeData = {
                        cmd_col: "<input type='button' class='CommonButon' value='分享明细' onclick=\"button_actions('detail','" + id + "')\">"
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
        else if (flag == "download") {
            //download_method(id, lang);
            var str = $.ajax({ url: "StandardHandle.ashx?flag=delete&lang='" + lang + "'id=" + id + "", async: false }).responseText;
        }
        else if (flag == "delete") {
            //delete_method(id);
        }
        else if (flag == "approve") {
            approve_method(id);
        }
        else if (flag == "detail") {
            detail_method(id);
        }
        else if (flag == "approveAll") {
            approveAll();
            return false;
        } else if (flag == "use0") {
            delete_method(flag,id);
        } else if (flag == "use1") {
            delete_method(flag,id);
        }
        if (flag == "deleteAll") {
            deleteAction();
            return false;
        }

    }


    function Recharge(id, name) {
        $("#czname").text(name);
        $("#btCZ").attr("czid", id);
        $("#czDiv").show();
    }

    $("#closeCZ").click(function () {
        $("#czDiv").hide();
    });

    $("#btCZ").click(function () {
        var id = $(this).attr("czid");
        var money = $("#txtMoney").val();
        var name = $("#czname").text();
        if (confirm("确定要给用户‘" + name + "’充值" + money + "元吗？")) {
            $.ajax({
                url: "MemberHandler.ashx?flag=cz&money=" + money + "&memberId=" + id,
                type: "get",
                dataType: "text",
                success: function (data) {
                    if (data == "success") {
                        alert("充值成功！");
                        $("#czDiv").hide();
                    }
                    else {
                        alert("充值失败！");
                    }
                }

            });
        }
    });


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
        //var _AreaId = $("#txtAreaId").val();
        var memberName = $("#txtMemberName").val();
        var tel = $("#txtTel").val();
        var _searchfilter = "MemberName$$" + memberName + "_$$_Tel$$" + tel;  //+ "_$$_Pid$$" + _Pid;
        var _searchfilter = escape(_searchfilter);

        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "MemberHandler.ashx?flag=sharecashlist<%=categoryId %>&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }

    
   function detail_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['详情', true],
            maxmin: true,
            iframe: { src: 'MemberShareListDetail.aspx?memberId=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
            }
        });
    }

    function add_method() {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['添加', true],
            maxmin: true,
            iframe: { src: 'MemberHandler.aspx?r=' + Math.random() },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }
    //详情
    function edit_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['详情', true],
            maxmin: true,
            iframe: { src: 'MemberDetail.aspx?id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }

    //禁用或启用
    function delete_method(flag, id) {
        var context = "";
        var state="";
        if (flag == "use0"){
            context = "启用";
            state=1;
            }
        if (flag == "use1"){
            context = "禁用";
            state=0;
            }
        layer.confirm('确定要' + context + '吗?', function () {
            var str = $.ajax({ url: "MemberHandler.ashx?flag=use&id=" + id + "&statu="+state+"&r=" + Math.random(), async: false }).responseText;
            if (str == "true") {
                alert(context+"成功！");
            }
            else {
                alert(context+"失败！");
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
                var str = $.ajax({ url: "GoodsHandler.ashx?flag=delete&id=" + id + "", async: false }).responseText;
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
