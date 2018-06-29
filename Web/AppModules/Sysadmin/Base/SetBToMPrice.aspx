<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetBToMPrice.aspx.cs" Inherits="AppModules_Sysadmin_Base_SetBToMPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
    #Save{ width:135px; height:32px; font-size:16px; color:#ab0b0b;text-shadow:0 1px #ffe8bb;background:url(../../../Images/SubmitN.png); position:relative; left:430px;top:-10px;}
    .Ltd{ width:20%;}
    .SelectBut{border:none;width:65px;height:25px;background:url(../../../Images/LogOutN.png);color:#9b0700;text-shadow:0 1px #ffe191;margin:0 10px;padding:0;text-align:center;cursor:pointer; }
    </style>
    <script type="text/javascript">

        function check() {
            var minPrice = $("#txtMinPrice").val();
            var maxPrice = $("#txtMaxPrice").val();
            var price = $("#txtPrice").val();
            var distance = $("#txtDistance").val();
            var reg = /^\d+(\.\d+)?$/; //非负浮点数
            var reg1 = /^\d+$/;  //非负整数
            if (minPrice != null || minPrice != "") {
                if (!reg.test(minPrice) && !reg1.test(minPrice)) {
                    alert("起步价格式不正确!");
                    return false;
                }
            }
            if (maxPrice != null || maxPrice != "") {
                if (!reg.test(maxPrice) && !reg1.test(maxPrice)) {
                    alert("封顶价格式不正确!");
                    return false;
                }
            }
            if (price != null || price != "") {
                if (!reg.test(price) && !reg1.test(price)) {
                    alert("每500g单价格式不正确!");
                    return false;
                }
            }
            if (distance != null || distance != "") {
                if (!reg.test(distance) && !reg1.test(distance)) {
                    alert("距离格式不正确!");
                    return false;
                }
            }
        }
        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.freshCurrentPage(); //执行当前列表页的刷新事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
        //js获取地址栏中的参数
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }


    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div id="right">
            <div class="applica_title">
            <br />
                <h4 style="display:none;">
                    <%=_pageTitle %>
                </h4>
            </div>
            <div class="applica_di">
                <asp:HiddenField ID="hidBuildingsId" runat="server" />
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                    <td class="Ltd">小区名称：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtBuilding" runat="server" Width="200px" CssClass="Enter" ReadOnly="true" MaxLength="500" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">农贸市场名称：</td>
                    <td class="Rtd">
                    <asp:TextBox ID="txtMarket" runat="server" Width="200px" CssClass="Enter" ReadOnly="true" MaxLength="500" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">起步价：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtMinPrice" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">封顶价：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtMaxPrice" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">每500g单价：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtPrice" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">距离：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtDistance" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
               <tr class="ButBox">
                  	<td colspan="2" style="height:30px; padding-top:4px; background-color:#f6f6f6;" align="center">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="确定"
                             Text="确定" BorderWidth="0" Width="135px" Height="32px" 
                             OnClientClick="return check()" onclick="btnSubmit_Click" />
                        <asp:Button runat="server" ID="Button1" CssClass="Return" ToolTip="返回"
                            OnClientClick="layer_close_refresh()" Text="返回" BorderWidth="0" Width="135px" Height="32px" />
                    </td>
                </tr>
            </table>
            </div>
        </div>
    </form>
</body>
</html>
