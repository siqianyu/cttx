<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateOrderFree.aspx.cs" Inherits="AppModules_Order_CreateOrderFree" %>

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
    <title>人工下单</title>

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

        function add_member_info(mid, truename) {
            //alert(mid + truename);
            var ids = $("#hidMemberIds").val();
            var truenames = $("#div_truenames").html();
            if (ids.indexOf(mid) == -1) {
                $("#hidMemberIds").val(ids + "," + mid);
                $("#div_truenames").html(truenames + "," + truename);
            }
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server" method="post">
    <asp:HiddenField runat="server" ID="hidGoodsIds" />
    <asp:HiddenField runat="server" ID="hidMemberIds" />
<div id="right">
            <div class="applica_title">
            <br />
                <h4>
                </h4>
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                        <td class="Ltd" colspan='4'>
                            <asp:Button ID="btnCreate" runat="server" Text="创建订单" Height="25px" 
                                Width="70px" onclick="btnCreate_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>下单给：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <div id="div_truenames"></div>
                        </td>
                    </tr>
                    <tr> 
                    <td class="Ltd">
                            <span style="color: #ff0000"></span>请选择：
                        </td>
                        <td class="Rtd" colspan='4'>
                            <iframe src="../Member/MemberListDialog.aspx" width="500" height="500" scrolling="no" frameborder="0"></iframe>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>详情：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Literal ID="llGoods" runat="server"></asp:Literal>
                        </td>
                    </tr>
            </table>
            </div>
        </div>

        </form>
        </body>
</html>
