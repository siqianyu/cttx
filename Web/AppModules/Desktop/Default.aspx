<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AppModules_Desktop_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <title></title>
    <link href="Style/Common.css" rel="stylesheet" />

    <style type="text/css">

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Default">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="width: 275px; padding: 0 6px">
                    <div class="LeftTd Left">
                        <div class="Info Left">
                            <img src="Images/Icon.png" /><p>
                                您好，<b class="Blue"><%=this.TrueName %></b>,欢迎回来！<br />
                                上次登录时间：<br />
                                <span>
                                    <%=DateTime.Now %></span></p>
                        </div>
                        <div class="LeftBar Left">
                            <div class="LeftBarT Left">
                                <p class="Title">
                                    登录日志</p>
                                <ul>
                                   <%=GetLogData()%>
                                </ul>
                            </div>
                            <div class="LeftBarB Left">
                            </div>
                        </div>
                    </div>
                </td>
                <td style="vertical-align: top; padding: 0 6px">
                    <div class="TaskBox Left" style="display:none">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <div class="Task_div">
                                        <a href="#" class="Tk1">新增任务(0)</a></div>
                                </td>
                                <td>
                                    <div class="Task_div">
                                        <a href="#" class="Tk2">新增订单(0)</a></div>
                                </td>
                                <td>
                                    <div class="Task_div">
                                        <a href="#" class="Tk4">新增施工(0)</a></div>
                                </td>
                                <td>
                                    <div class="Task_div">
                                        <a href="#" class="Tk5">新增会员(0)</a></div>
                                </td>
                               <%-- <td style="background: none">
                                    <div class="Task_div">
                                        <a href="#" class="Tk8">新增支付(2)</a></div>
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                    <!--TaskBox close-->
                    <div class="Psnal Left">
                        <div class="PsnalT Left">
                            <p>
                                最新信息</p>
                            <div class="Slide" style="display: none">
                                <a href="#" class="Prev" title="上周"></a><span>本周</span><a href="#" class="Next" title="下周"></a></div>
                        </div>
                        <table cellpadding="0" cellspacing="1" border="0" class="TkBoard">
                            <tr class="Ttr">
                                <td>
                                    <div class="PosiBar" style='margin-bottom: 3px;'>
                                        <p>
                                            最新订单
                                        </p>
                                        <a href='/AppModules/Order/OrderList.aspx' style='float: right; color: #3f71a9;'></a>
                                    </div>
                                    <div class='ui-jqgrid'>
                                        <table class="ui-jqgrid-htable" role="grid" aria-labelledby="gbox_startech_table_jqgrid"
                                            cellspacing="0" cellpadding="0" border="0">
                                            <thead>
                                                <tr class="ui-jqgrid-labels" role="rowheader">
                                                    <th id="startech_table_jqgrid_orderId" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="jqgh_startech_table_jqgrid_orderId" class="ui-jqgrid-sortable">
                                                            订单号 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="startech_table_jqgrid_memberName" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="jqgh_startech_table_jqgrid_memberName" class="ui-jqgrid-sortable">
                                                            用户名 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="startech_table_jqgrid_orderTime" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="jqgh_startech_table_jqgrid_orderTime" class="ui-jqgrid-sortable">
                                                            下单时间 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="startech_table_jqgrid_orderAllMoney" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="jqgh_startech_table_jqgrid_orderAllMoney" class="ui-jqgrid-sortable">
                                                            金额 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="startech_table_jqgrid_goodsList" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="jqgh_startech_table_jqgrid_goodsList" class="ui-jqgrid-sortable">
                                                            内容 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="startech_table_jqgrid_cmd_col" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="font-size: 12px; line-height: 15px;width:90px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span><div
                                                            id="jqgh_startech_table_jqgrid_cmd_col" class="ui-jqgrid-sortable">
                                                            操作 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <%=strOrderTable %>
                                        </table>
                                    </div>
                                    <div class="PosiBar" style='margin: 3px 0px;'>
                                        <p>
                                            最新加入会员
                                        </p>
                                        <a href='/AppModules/member/MemberList.aspx' style='float: right; color: #3f71a9;'></a>
                                    </div>
                                    <div class='ui-jqgrid'>
                                        <table class="ui-jqgrid-htable" role="grid" aria-labelledby="gbox_startech_table_jqgrid"
                                            cellspacing="0" cellpadding="0" border="0">
                                            <thead>
                                                <tr class="ui-jqgrid-labels" role="rowheader">
                                                    <th id="Th1" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="Div1" class="ui-jqgrid-sortable">
                                                            会员编号 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="Th2" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="Div2" class="ui-jqgrid-sortable">
                                                            会员名 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="Th3" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="Div3" class="ui-jqgrid-sortable">
                                                            手机号码 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="Th4" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="width: 138px; font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="Div4" class="ui-jqgrid-sortable">
                                                            真实姓名 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="Th5" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="font-size: 12px; line-height: 15px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span>
                                                        <div id="Div5" class="ui-jqgrid-sortable">
                                                            注册时间 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                    <th id="Th6" role="columnheader" class="ui-state-default ui-th-column ui-th-ltr"
                                                        style="font-size: 12px; line-height: 15px;width:90px;">
                                                        <span class="ui-jqgrid-resize ui-jqgrid-resize-ltr" style="cursor: col-resize;">&nbsp;</span><div
                                                            id="Div6" class="ui-jqgrid-sortable">
                                                            操作 <span class="s-ico" style="display: none"><span sort="asc" class="ui-grid-ico-sort ui-icon-asc ui-state-disabled ui-icon ui-icon-triangle-1-n ui-sort-ltr">
                                                            </span><span sort="desc" class="ui-grid-ico-sort ui-icon-desc ui-state-disabled ui-icon ui-icon-triangle-1-s ui-sort-ltr">
                                                            </span></span>
                                                        </div>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <%=strMemberTable %>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr class="odd">
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--Psnal close-->
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">

    function button_actions(flag, id) {
        if (flag == "detail") {
            detail_method(id);
        } else if (flag == "detail2") {
            detail_method2(id);
        }
    }


    //订单详情
    function detail_method(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['订单详情', true],
            maxmin: true,
            iframe: { src: '/AppModules/Order/orderDetail.aspx?orderid=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }
    //会员详情
    function detail_method2(id) {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['详情', true],
            maxmin: true,
            iframe: { src: '/AppModules/Member/MemberDetail.aspx?id=' + id },
            area: [document.body.scrollWidth - 20, $(document).height()],
            offset: ['0px', ''],
            close: function (index) {
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
            }
        });
    }
</script>
</html>
