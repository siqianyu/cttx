<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddGoodsFormate.aspx.cs" Inherits="AppModules_Goods_AddGoodsFormate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>任务规格信息</title>
    
    <style>
    .file{
	    FONT-SIZE: 12px;	
	    LINE-HEIGHT: 16px;	
	    HEIGHT: 20px;
	    width:500px;
	    
    }
        .save
        {
            width: 26px;
        }
    </style>
        <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />

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
<%--<script src="../JS/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="../../js/calendar.js"></script>--%>
    <script language="javascript">
        function checkForm(){
            if($("#div_formate_list").text() == ""){
                alert('请选择规格信息');
                return false;
            }
            return true;
        }
        
        function selectPropertyValue(pid,id,s){
            var obj = document.getElementById(pid);
            if(document.getElementById(id).checked==true){
                if(obj.value.indexOf(","+s+",")==-1){obj.value+=","+s+",";}
            }else{
                obj.value=obj.value.replace(","+s+",","");
            }
            //alert(obj.value);
        }
        
        function createLists(){
            if((!document.getElementById('MorePropertyInfo')) || $("#MorePropertyInfo").val() == ""){alert('请选择规格信息');return false;}
            var infoArr = $("#MorePropertyInfo").val().split(',');
            var s = "";
            var hasSub = false;
            for(var i=0;i<infoArr.length;i++){
                if(document.getElementById(infoArr[i].split('$')[0]).value != "")
                {
                    s+=infoArr[i].split('$')[1]+"$"+document.getElementById(infoArr[i].split('$')[0]).value+"|";
                    hasSub=true;
                }
            }
            if(hasSub == false){alert('请选择规格信息');return false;}

            $.ajax({
                url: "AddGoodsFormate.aspx?flag=1&goodsid=<%=this.goodsId %>&iteminfo="+escape(s),
                dataType: 'text',
                success: function (data) {
                    $("#div_formate_list").html(data);
                }
            });
        }
        window.name="mydialog"
    </script>
    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server" target="mydialog">
        <div style="font-size:12px;">
            <div class="AddcomT1">
      <%--                  <p class="AddcomT1P">
                            任务规格</p>--%>
                    </div>
            <table width="805" border="1" cellspacing="0" cellpadding="0" class="Addcomlist ViewBox">
                <tr>
                    <td class="Ltd">
                        规格选择：</td>
                    <td class="Rtd" colspan="3" id="td_formate">
                       <asp:Literal runat="server" ID="ltHtml"></asp:Literal>
                       <div><input type="button" value="创建规格列表" onclick="createLists()" class="CommomButtonStyleSmall" /></div>
                       </td>
                </tr>
                <script language="javascript">if(!document.getElementById('MorePropertyInfo')){$("#td_formate").text('此任务分类暂无设置任务规格')}</script>
                <tr>
                    <td class="Ltd">
                        规格列表：</td>
                    <td class="Rtd" colspan="3">
                    <div><b style="padding:5px;padding-right:80px;">[货号]</b>
                    <b style="padding:5px; padding-right:110px;">[规格组合信息]</b>
                    <b style="padding:5px;">[销售价格]</b>&nbsp;
<%--                    <b style="padding:5px;">[金牌会员价]</b>
                    &nbsp;
                    <b style="padding:5px;">[银牌会员价]</b>
                    &nbsp;--%>
                    <b style="padding:5px;">[库存数量]</b></div>
                    <div id="div_formate_list"></div>
                    </td>
                </tr>
            <tr>
            <td colspan='4' class='Ltd'>
            <center class='ButBox' style='padding:0px;'>
                <asp:Button ID="btnSave" ImageUrl="imgs/sure.gif" runat="server" style='border-width: 0px;height: 32px;width: 135px;' CssClass="Submit"
                     OnClick="btnSave_Click" OnClientClick="return checkForm()" Text='提交' />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="ImageButton1" ImageUrl="imgs/Cancel.gif" runat="server" style='border-width: 0px;height: 32px;width: 135px;' CssClass="Return"
                    text='返回' OnClientClick="window.close();return false;" />
            </center>
            </td></tr>
            </table>
        </div>
	<div class="clear"></div>
    <input type="hidden"  value='<%=vipDs1 %>' id='hfVip1'/>
    <input type="hidden"  value='<%=vipDs2 %>' id='hfVip2'/>
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
