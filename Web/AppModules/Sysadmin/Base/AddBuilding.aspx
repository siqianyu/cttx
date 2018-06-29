<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBuilding.aspx.cs" Inherits="AppModules_Sysadmin_Base_AddBuilding" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        function check() {
            var id = $("#txtBuildingId").val();
            var name = $("#txtBuildingName").val();
            var Sort = $("#txtSort").val();
            if (id == "" || id == null) {
                alert("市场编号不能为空!");
                return false;
            }
            else {
                var oldId = GetQueryString("id");
                if (!/[@#\$%\^&\*<\>[\]{\}!,\;]+/g.test(id)) {
                    if (oldId == "" || oldId == null || id != oldId) {//是进行添加或编号被修改
                        var str = $.ajax({ url: "BuildingHandler.ashx?flag=isexist&id=" + id + "", async: false }).responseText;
                        if (str === "true") {
                            alert("此编号已经存在!");
                            return false;
                        }
                    }
                }
                else {
                    alert("不能包含非法字符");
                    return false;
                }
            }
            if (name == "" || name == null) {
                alert("市场名称不能为空!");
                return false;
            }
            if (Sort != "" && Sort != null) {
                var reg = /^\d+$/;
                if (!reg.test(Sort)) {
                    alert("排序只能为数字!");
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
                <h4>
                    <%=_pageTitle %>
                </h4>
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>小区/大厦编号：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtBuildingId" runat="server" ReadOnly="true" style="color:Gray;" Width="200px" CssClass="Enter" MaxLength="100" >自动生成</asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>小区/大厦名称：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtBuildingName" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">所属区域：</td>
                    <td class="Rtd">
                        <asp:DropDownList ID="ddlAreaId" runat="server" Width="204px">
                        <asp:ListItem Value="0" Selected="True" Text="--请选择--"></asp:ListItem>
                    </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">详细地址：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtAddressDetail" runat="server" Width="200px" TextMode="MultiLine" Rows="3" CssClass="Enter" MaxLength="250" ></asp:TextBox>
                        (地址前不用手工输入区域信息)
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">经度：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtMapX" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">维度：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtMapY" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">排序：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtSort" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
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
