<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormateGoodsList.aspx.cs" Inherits="AppModules_Goods_FormateGoodsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Controls/CategorySelect.ascx" TagName="cselect" TagPrefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
        <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="PosiBar" style='margin:0px 0px 8px 0px;'>
        <p>
            属性绑定</p>
    </div>
                    <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist ViewBox">
                     <tr>
                        <td class="Ltd">
                            系统分类：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <%--<asp:DropDownList ID="ddlPCategory" runat="server" Width="260px" onchange="selectDefaultType()"></asp:DropDownList>--%>
                            <uc2:cselect ID='cselect' runat="server"  />
                        </td>
                    </tr> 
                    <tr>
                       <td class="Ltd">
                            规格参数：
                        </td>
                        <td class="Rtd" colspan='3' id='checklist'>
                            <%=strTypeList %>
                        </td>
                    </tr>
                    <tr class='ButBox'>
                        <td colspan='4' style='text-align:center;'>
                            <input id="btSend" type="button" class='Submit' value="提  交" style='border-width: 0px;height: 32px;width: 135px;'/>
                            <%--<input id="btCancel" class='Return' type="button" value="返  回"  style='border-width: 0px;height: 32px;width: 135px;'/>--%>
                        </td>
                    </tr>
                    </table>

    </form>
</body>
<script type="text/javascript">
    var url = 'FormateGoodsList.aspx?';
    var nowCate = '-1';
    $("body").on("change", ".cateSelect", function () {
        if ($(this).val() == "-1")
            return;
        var typeid = $(this).val();
        nowCate = typeid;
        $.ajax({
            url: url + "flag=select&typeid=" + typeid + "&r=" + Math.random(),
            dataType: "text",
            success: function (data) {
                $("#checklist").html(data);
            }
        });
    });


    $("#btSend").on("click", function () {
        if (nowCate == "-1") {
            alert("未选择分类！");
            return;
        }
        var categoryList = "";
        var index = 0;
        $("#checklist input").each(function () {

            if ($(this).prop('checked')) {
                if (index != 0)
                    categoryList += ",";
                categoryList += $(this).attr("tid");
                index++;
            }

        });

        $.ajax({
            url: url + "flag=add&typeid=" + nowCate + "&catelist="+categoryList+"&r=" + Math.random(),
            dataType: "text",
            success: function (data) {
                if (data == "success") {
                    alert("绑定属性成功！");
                } else
                    alert("绑定属性失败！");
            }
        });
    });



</script>
</html>
