<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBToM.aspx.cs" Inherits="AppModules_Sysadmin_Base_AddBToM" %>

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
            var Market = $("#<%=ddlMarket.ClientID %> option:selected").val();
            var Sort = $("#txtSort").val();
            if (Market == "0") {
                alert("请选择农贸市场");
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


        function selectBuilding() {
            var Building = $("#txtBuildingName").val();
            var str = $.ajax({ url: "BuildingHandler.ashx?flag=selectBuilding&Buildingname=" + escape(Building) + "&r=" + Math.random(), async: false }).responseText;
            $("#ddlBuilding").html(str);

        }

        function selectMarket() {
            var Market = $("#txtMarketName").val();
            var str = $.ajax({ url: "MarketHandler.ashx?flag=selectMarket&Marketname=" + escape(Market) + "&r=" + Math.random(), async: false }).responseText;
            $("#ddlMarket").html(str);

        }

        function LoadBuilding() {
            var w = document.body.scrollWidth-100;
            var h = document.body.scrollHeight - 770;
            var url = "BuildingListToR.aspx?r=" + Math.random() + "";
            var ids = window.showModalDialog(url, window, 'resizable:no;scroll:yes;status:no;dialogWidth=' + w + 'px;dialogHeight=' + h + 'px;')
            if (ids == undefined) {
                ids = window.returnValue;
            }
            if (ids) {
                $("#hidBuildingsId").val(ids);
                var str = $.ajax({ url: "BToMHandler.ashx?flag=SBN&buildingsId=" + escape(ids) + "&r=" + Math.random(), async: false }).responseText;
                if (str) {
                    $("#txtBuilding").val(str);
                }
                
            }
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
                <asp:HiddenField ID="hidBuildingsId" runat="server" />
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                    <td class="Ltd">选择小区：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtBuilding" runat="server" Width="200px" onclick="LoadBuilding()" CssClass="Enter" ReadOnly="true" MaxLength="500" ></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlBuilding" runat="server" Width="204px">
                        <asp:ListItem Value="0" Selected="True" Text="--请选择--"></asp:ListItem>
                    </asp:DropDownList>
                    <input style="width: 65px;" id="txtBuildingName" /><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="selectBuilding()"  />--%>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">选择农贸市场：</td>
                    <td class="Rtd">
                        <asp:DropDownList ID="ddlMarket" runat="server" Width="204px">
                        <asp:ListItem Value="0" Selected="True" Text="--请选择--"></asp:ListItem>
                    </asp:DropDownList>
                    <input style="width: 65px;" id="txtMarketName" /><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="selectMarket()"  />
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
