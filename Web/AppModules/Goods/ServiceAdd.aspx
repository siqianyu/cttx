<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceAdd.aspx.cs" Inherits="AppModules_Goods_ServiceAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Pragma" content="no-cache">
    <%
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    %>
    <title>任务添加</title>
            
    <script language="javascript">

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
    </script>


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

</head>
<body>
    <form id="form1" runat="server">
    <div class="Mebmid">
        <!--main_conten_start-->
        <div class="MebmidR">
            
            <div class="AddcomCT" id="AddcomCT" style="margin-top: 0px; float: left; width: 100px;">
            </div>
            <div id="Addcom_Bas">
                <div class="AddcomT1">
                </div>
                <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist ViewBox">
                    <tr>
                        <td class="Ltd">
                            服务名称：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            服务说明：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:TextBox ID="txtContext" runat="server"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            服务选项：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox><span style='color:Red'>要选择的服务方式之间以逗号隔开入：杀,不杀，注意：请慎重更新</span>

                        </td>
                    </tr>

                                        <tr>
                        <td class="Ltd">
                            收费标准：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox><span style='color:Red'>要收取的费用之间以逗号隔开入并和服务选项对应：1,0，</span>

                        </td>
                    </tr>

                                        <tr>
                        <td class="Ltd">
                            默认选择：
                        </td>
                        <td class="Rtd" colspan='3'>
                           <asp:TextBox ID="txtDefault" runat="server"></asp:TextBox><span style='color:Red'>要要设为默认的选项之间以逗号隔开入并和服务选项对应：1,0，1表示该选项为默认项</span>

                        </td>
                    </tr>

                                           <tr>
                        <td class="Ltd">
                            排序：
                        </td>
                        <td class="Rtd" colspan='3'>
                           <asp:TextBox ID="txtOrder" runat="server" Text='99'></asp:TextBox>

                        </td>
                    </tr>                 
                                        <tr>
                        <td class="Ltd">
                            备注：
                        </td>
                        <td class="Rtd" colspan='3'>
                           <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    
                    
                    
                    


                    

                    <tr>
                        <td class='Rtd ButBox' colspan='4'>
                            <asp:Button Text="提交" OnClientClick='return checkForm()' ID="btnSave" Style='border-width: 0px;
                                height: 32px; width: 135px;' CssClass="Submit" runat="server" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
        </div>
    </div>
    </form>
</body>
<script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
<script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
<script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
<script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
<script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
<script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

</html>

