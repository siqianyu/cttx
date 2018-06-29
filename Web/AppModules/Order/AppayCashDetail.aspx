﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppayCashDetail.aspx.cs" Inherits="AppModules_Order_AppayCashDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="Expires" CONTENT="0">  
    <meta http-equiv="Cache-Control" CONTENT="no-cache">  
    <meta http-equiv="Pragma" CONTENT="no-cache"> 
    <%
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        %>
    <title>提现管理</title>

    <style type="text/css">
        #cke_1_bottom
        {
            display: none;
        }
        .cateSelect
        {
            width: 100px;
        }
        
    </style>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
        <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        //关闭当前层(返回按钮)
        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }

        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.grid_search(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
        function checkapply() {
            return confirm('确定操作？操作后信息不能更改、撤销！');
        }

        function check() {
            return confirm('确定已完成支付？操作后信息不能更改、撤销！');
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server" method="post">
<div id="right">
            <div class="applica_title">
            <br />
                <h4>
                </h4>
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>会员号：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbMemberId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>会员账户余额：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbYE" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>银行卡信息：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbCardNumber" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>申请时间：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbApplayTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>金额：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>平台扣费：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbKuoMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>处理状态：
                        </td>
                        <td class="Rtd" colspan='3'>
    
                            <asp:DropDownList ID="ddlIfDeal" runat="server">
                            <asp:ListItem Value="0">未处理</asp:ListItem>
                            <asp:ListItem Value="1">通过审核</asp:ListItem>
                            <asp:ListItem Value="-1">不通过审核</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                   <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>处理备注：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:TextBox TextMode="MultiLine" ID="lbDealRemarks" runat="server" 
                                Height="100px" Width="600px"></asp:TextBox>

                        </td>
                    </tr>
                      <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>处理人：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbDealAdmin" runat="server"></asp:Label>
                        </td>
                    </tr> 
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>处理时间：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbDealTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                <tr>
                        <td class='Rtd ButBox' colspan='4'>
                            <asp:Button Text="确定申请"  ID="btnSave" Style='border-width: 0px;
                                height: 32px; width: 135px;' CssClass="Submit" runat="server" OnClick="btnSave_Click" OnClientClick="return checkapply();" />

                               &nbsp;&nbsp;
                                <asp:Button Text="确定已完成支付" OnClientClick="return check();"  ID="btnCheck" Style='border-width: 0px;
                                height: 32px; width: 135px;' CssClass="Submit" runat="server" OnClick="btnCheck_Click" Visible="false" />
                        </td>
                    </tr>
            
            </table>
            </div>
        </div>

        </form>
        </body>
</html>
