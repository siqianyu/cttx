<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditGoodsFormate.aspx.cs" Inherits="ShopSeller_EditGoodsFormate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>任务规格信息</title>
    
    <link type="text/css" href="MebSty/order.css" rel="stylesheet" />
    <link type="text/css" href="MebSty/bottom.css" rel="stylesheet" />
    <style>
    .file{
	    FONT-SIZE: 12px;	
	    LINE-HEIGHT: 16px;	
	    HEIGHT: 20px;
	    width:500px;
	    
    }
    </style>
<%--    <script type="text/javascript" language="javascript" src="../../js/jquery-1.2.6.pack.js"></script>--%>
<%--    <script src="../JS/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="../../js/calendar.js"></script>--%>
        <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript">
        function checkForm(){
            if($("#div_formate_list").text() == ""){
                alert('请选择规格信息');
                return false;
            }
            return true;
        }
        
        function delOne(sysnumber){
            if(confirm('确定要删除吗?')==false){return false;}
            //var r = ShopSeller_EditGoodsFormate.Ajax_Delete(sysnumber).value;
            $.ajax({
                url: "EditGoodsFormate.aspx?flag=del&sysnumber=" + sysnumber ,
                dataType: 'text',
                success: function (data) {
                    //if (data == 1) { alert('保存成功'); listInfo(); }
                    if (data == 1) { alert('删除成功'); listInfo(); }
                }
            });
        }
        
        function saveOne(sysnumber,flag){
            var info = document.getElementById("GoodsCode_" + flag).value + "$" + document.getElementById("price_" + flag).value + "$" + document.getElementById("stock_" + flag).value + "$" + document.getElementById("vipprice1_" + flag).value  + "$" + document.getElementById("vipprice2_" + flag).value;
            //alert(info);
            if(document.getElementById("GoodsCode_"+flag).value.indexOf('$')>-1){alert('存在系统字符$,请用其他字符代替');return false;}
            //var r = ShopSeller_EditGoodsFormate.Ajax_Save(sysnumber, info).value;
            $.ajax({
                url: "EditGoodsFormate.aspx?flag=save&sysnumber="+sysnumber+"&info="+info,
                dataType: 'text',
                success: function (data) {
                    if (data == 1) { alert('保存成功'); listInfo(); }
                }
            });
//            if(r==1){alert('保存成功');listInfo();}
        }
        
        function listInfo(){
            //            var s = ShopSeller_EditGoodsFormate.Ajax_ListFormate2("<%=this.goodsId %>").value;
            //               $("#div_formate_list").html(s);
            $.ajax({
                url: "EditGoodsFormate.aspx?flag=list&goodsid=<%=this.goodsId %>",
                dataType: 'text',
                success: function (data) {
                    $("#div_formate_list").html(data);
                }
            });
        }
       
        window.name="mydialog";
        
        $(document).ready(
            function(){
                listInfo();
            }
        );
    </script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server" target="mydialog">
        <div style="font-size:12px">
            <div class="AddcomT1">
                        <p class="AddcomT1P">
                            任务规格</p>
                    </div>
            <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist ViewBox">
                
                <tr>
                    <td class="Ltd" style='text-align:left;' colspan="3">
                    <div><b style="padding:5px;padding-right:80px; font-size:12px;">[货号]</b><b style="padding:5px; padding-right:95px;font-size:12px;">[规格组合信息]</b><b style="padding:5px;font-size:12px;padding-right: 18px;">&nbsp;&nbsp;&nbsp;[销售价格]</b>&nbsp;<b style='padding:5px;font-size:12px;display:none;'>[金牌会员价]</b>&nbsp;<b style='padding:5px;font-size:12px;display:none;'>[银牌会员价]</b>&nbsp;<b style="padding:5px;font-size:12px;">[库存数量]</b></div>
                    <div id="div_formate_list"></div>
                    </td>
                </tr>
            </table>
            <center style="display:none">
                <asp:ImageButton ID="btnSave" ImageUrl="imgs/sure.gif" runat="server"
                    CssClass="save" OnClick="btnSave_Click" OnClientClick="return checkForm()" />
                &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ImageButton1" ImageUrl="imgs/Cancel.gif" runat="server"
                    CssClass="save" OnClientClick="window.close();return false;" />
            </center>
        </div>
	<div class="clear"></div>
    <input id='hfVip1' type="hidden" value='<%=vipDs1 %>' />
    <input id='hfVip2' type="hidden" value='<%=vipDs2 %>' />
    </form>
</body>
<script type="text/javascript">
    $(".price").live("blur", function () {
        var pr = parseFloat($(this).val());
        var ds1 = parseFloat($("#hfVip1").val());
        var ds2 = parseFloat($("#hfVip2").val());
        var v1 = (pr * ds1).toFixed(2);
        var v2 = (pr * ds2).toFixed(2);
        $(this).parent().find(".vipPrice1").val(v1);
        $(this).parent().find(".vipPrice2").val(v2);
    });
</script>

</html>
