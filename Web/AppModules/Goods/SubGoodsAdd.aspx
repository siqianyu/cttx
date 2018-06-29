<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubGoodsAdd.aspx.cs" Inherits="ShopSeller_SubGoodsAdd"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/CategorySelect.ascx" TagName="cselect" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hid_pgoodsid" runat="server" />
    <div class="Mebmid">
        <!--main_conten_start-->
        <div class="MebmidR">
            <div class="AddcomT" style='width: 500px; height: 24px; padding-top: 5px; display:none'>
                <%--                <p class="AddcomTP" style='float:left;width:100px;'>
                    任务信息管理</p>--%>
                <p id="BaseInfo" style='cursor: pointer; float: left; width: 95px; padding: 5px 1px;
                    background: url(/Images/MenuA.png) no-repeat;'>
                    任务基本信息</p>
              <%--  <p style="cursor: pointer; color: White; float: left; width: 95px; padding: 5px 1px;
                    background: url(/Images/MenuN.png) no-repeat;" id="FormateInfo">
                    任务属性规格</p>--%>
            </div>
            <div class="AddcomCT" id="AddcomCT" style="margin-top: 0px; float: left; width: 100px;">
            </div>
            <div id="Addcom_Bas">
                <div class="AddcomT1">
                </div>
                <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist ViewBox">
                    <tr style="display:none">
                        <td class="Ltd">
                            系统分类：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <uc2:cselect ID='cselect' runat="server" />
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="Ltd">
                            任务标签：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Literal ID="ltSign" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfSign" runat="server" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="Ltd">
                            任务名称：
                        </td>
                        <td class="Rtd">
                           <select runat="server" id="ddlJobType" style="display:none"><option value="SubGoods">视频</option></select> 
                           <input style="width: 300px;" type="text" id="txtName" runat="server" />
                           <asp:CheckBox ID="cbFree" Text="支持试听" runat="server" />
                        </td>
                        <td class="Ltd">
                            任务编码：
                        </td>
                        <td class="Rtd">
                            <asp:HiddenField runat="server" ID="hidOldGoodsCode" />
                            <input style="width: 100px;" type="text" id="txtGoodsCode" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            任务小图片：
                        </td>
                        <td class="Rtd">
                            <asp:FileUpload ID="FileUpload1" runat="server" Height="18px" Width="220px" />
                            &nbsp;
                            <br />
                            <span style='color:#F00;'>说明：图片建议尺寸：160×120,建议使用PNG格式</span>
                            <div
                                runat="server" id="div_img" visible="false">
                                <asp:Image ID="Image1" runat="server" Width="80" Height="60" /></div>
                        </td>
                        <td class="Ltd">
                            是否发布：
                        </td>
                        <td class="Rtd">
                            <asp:RadioButtonList ID="rdIsSale" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="否" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="Ltd">
                            视频文件：
                        </td>
                        <td class="Rtd">
                            <asp:FileUpload ID="FileUpload2" runat="server" Height="18px" Width="220px" />
                            <asp:HiddenField runat="server" ID="hidVideoPath" />
                            &nbsp;
                            <br />
                            <span style='color:#F00;'>说明：必须使用MP4格式</span>
                            
                        </td>
                        <td class="Ltd">
                            预览视频：
                        </td>
                        <td class="Rtd">
                            <input id="btnPlay" type='button' class='CommonButon' value='预览播放' onclick="play_video()" /><asp:Literal runat="server" ID="ltVideoSize"></asp:Literal>
                        </td>
                    </tr>
                    
                    <tr style="display:none">
                        <td class="Ltd">
                            销售价格：
                        </td>
                        <td class="Rtd">
                            <span>
                                <input style="width: 80px; margin-left: 0px;" type="text" id="txtSalePrice" value="1.00"
                                    runat="server" /></span><span style="color: #b9b8b8">(显示的最终交易价格)</span>
                        </td>
                        <td class="Ltd">
                            原始价格：
                        </td>
                        <td class="Rtd" style="text-align: left">
                            <span>
                                <input style="width: 80px; margin-left: 0px;" type="text" id="txtMarketPrice" value="1.00"
                                    runat="server" /></span><span style="color: #b9b8b8">(显示的优惠前的价格)</span>
                        </td>
                    </tr>


                    <tr>
                       
                        <td class="Ltd">
                            播放次数：
                        </td>
                        <td class="Rtd" style="text-align: left">
                            <span>
                                <input style="width: 80px; margin-left: 0px;" type="text" id="txtTotalSaleCount" 
                                    runat="server" value="506" /></span>
                        </td> 
                        <td class="Ltd">
                            排 序：
                        </td>
                        <td class="Rtd" style="text-align: left">
                            <span>
                                <input style="width: 50px; margin-left: 0px;" type="text" id="txtOrder" runat="server"
                                    value="1" /></span><span style="color: #b9b8b8">正序(1比2靠前)</span>
                        </td>
                    </tr>
                    <tr>
                     <td class="Ltd">
                            任务属性：
                        </td>
                        <td class="Rtd" style="text-align: left;">
                            <asp:DropDownList ID="ddlMorePropertys" runat="server">
                            <asp:ListItem Value="视频和练习">视频和练习</asp:ListItem>
                            <asp:ListItem Value="模拟试题">模拟试题</asp:ListItem>
                            <asp:ListItem Value="外部课程">外部课程</asp:ListItem>
                            </asp:DropDownList>
                        <br />
                            <span style='color:#F00;'>说明：视频和练习（只需上传视频，课后练习题库自动抽取）；</span>
                            <br />
                            <span style='color:#F00;'>模拟试题（无需上传视频，需要人工组卷附加到任务里）；</span>
                        </td>
                        <td class="Ltd">
                            任务标识：
                        </td>
                        <td class="Rtd" style="text-align: left;">
                            <asp:CheckBox ID="cbIsNew" Text="最新" runat="server" />&nbsp;
                            <asp:CheckBox ID="cbIsHot" Text="热门 " runat="server" />&nbsp;
                            <asp:CheckBox ID="cbIsRec" Text="推荐" runat="server" />&nbsp;
                           
                        </td>
                       
                    </tr>

                    <tr>
                     <td class="Ltd">
                            外部课程编号：
                        </td>
                        <td class="Rtd" colspan="3">
                            <asp:HiddenField runat="server" ID="hidOutGoodsId" />
                            <input style="width: 100px;" type="text" id="txtOutGoodsId" runat="server" />
                            <span style='color:#0000ff;'>组合的课程编号,只适用于组合课程,普通课程不填。</span>
                        </td>
                    </tr>

                    <tr style="display: none">
                        <td class="Addcomlist_Con1">
                            备 注：
                        </td>
                        <td class="Addcomlist_Con2" colspan="3" style="text-align: left">
                            &nbsp;<asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Height="45px"
                                Width="280px"></asp:TextBox>后台备注，前台不显示
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class='Ltd'>
                            任务幻灯图片：
                        </td>
                        <td class='Rtd' colspan='3'>
                            <% if (this.id != "")
                               { %>
                            <%--<a href="T_Forms_Resource.aspx?goodsid=<%=this.id %>" target="_blank" style="color:Blue">【添加幻灯片】</a>--%>
                            <a href="javascript:void(0)"  style="color:Blue" id='addSlide'>【添加幻灯片】</a>                        
                            <span style='color:#F00;'>说明：图片建议尺寸：400×400,建议使用PNG格式</span>

                            <%}
                               else
                               { %>
                            <a href="javascript:void(0)" style="color:#dddddd">请先保存任务基本信息</a>
                            <%} %>
                            <br />
                            <div id='slideImgList'>
                            
                            </div>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class='Ltd'>
                            任务简介：
                        </td>
                        <td class='Rtd' colspan='3'>
                            <asp:TextBox TextMode="MultiLine" ID="txtSampleDesc" runat="server" Visible="true"
                                Style='width: 700px; height: 60px;'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='Ltd'>
                            任务详情：
                        </td>
                        <td class='Rtd' colspan='3'>
                            <asp:TextBox TextMode="MultiLine" ID="hzst_ckeditor" runat="server" Visible="true"
                                Style='width: 700px; height: 100px;'></asp:TextBox>
                        </td>
                    </tr>
                     
                    <tr>
                        <td class='Rtd ButBox' colspan='4'>
                            <asp:Button Text="提交" OnClientClick='return checkForm()' ID="btnSave" Style='border-width: 0px;
                                height: 32px; width: 135px;' CssClass="Submit" runat="server" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <script language="javascript">
                    (function () {
                        if ("<%=this.id %>" == "") {
                            $("#tr_pic_1").show();
                            $("#tr_pic_2").hide();

                        }
                        else {
                            $("#tr_pic_1").hide();
                            $("#tr_pic_2").show();
                            var ajaxStr = $.ajax({ url: "GoodsInfo.ashx?method=goodspics&goodsId=<%=this.id %>&r=" + Math.random() + "", async: false }).responseText;
                            $("#td_pics").html(ajaxStr);
                        }
                    } ());
                </script>
                <!--任务幻灯图片end-->
            </div>
            <div id="Addcom_Pro" style="display: none">
                <!--任务扩展属性start-->
                <div class="AddcomT1">
                    <p class="AddcomT1P ViewBox" style="font-size: 14px; background-color: #f4f4f4; height: 36px;
                        color: #ab0b0b; font-weight: bold">
                        任务属性管理</p>
                </div>
                <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist ViewBox" id="table_moreproperty">
                </table>
                <script>
                    (function () {
                        if ("<%=this.id %>" != "") {
                            var ajaxStr = $.ajax({ url: "GoodsInfo.ashx?method=moreproperty&goodsId=<%=this.id %>&r=" + Math.random() + "", async: false }).responseText;
                            ajaxStr += "<tr><td class='Rtd ButBox' colspan='4' style='width:98%;padding:5px;text-align:center;height:45px;'><input style='width: 135px;' type='button' class='CommonButon' value='编辑任务属性' onclick='editMore()'  /></td></tr>";
                            $("#table_moreproperty").html(ajaxStr);
                        }
                    } ());
                </script>
                <!--任务扩展属性end-->
                <!--任务规格信息start-->
                <div class="AddcomT1">
                    <p class="AddcomT1P ViewBox" style="font-size: 14px; background-color: #f4f4f4; height: 36px;
                        color: #ab0b0b; font-weight: bold">
                        任务规格管理</p>
                </div>
                <table border='0' cellpadding='0' cellspacing='1' class='Addcomlist ViewBox' id="div_goods_formate">
                </table>
                <script language="javascript">
                    (function () {
                        if ("<%=this.id %>" != "") {
                            var ajaxStr = $.ajax({ url: "GoodsInfo.ashx?method=listformate&goodsId=<%=this.id %>&r=" + Math.random() + "", async: false }).responseText;
                            var buttonStr = "<tr><td class='Rtd ButBox'  colspan='4'  style='width:98%;padding:5px;text-align:center;height:45px;'><input style='width: 135px;' type='button' class='CommonButon' value='创建规格' onclick='editeFormate(1)' />&nbsp;&nbsp;<input type='button' class='CommonButon' style='width: 135px;' value='编辑规格' onclick='editeFormate(2)' /></td></tr>";
                            ajaxStr += buttonStr;
                            $("#div_goods_formate").html(ajaxStr);

                        }
                    } ());
                </script>
                <!--任务规格信息end-->
            </div>
        </div>
    </div>
    <!--main_conten_end-->
    <input type="hidden" id='hfVip1' value='<%=vipDs1  %>' />
    <input type="hidden" id='hfVip2' value='<%=vipDs2  %>' />
    <%--<uc3:Bottom ID="Bottom1" runat="server" />--%>
    </form>
</body>
<script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
<script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
<script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
<script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
<script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
<script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
<script type="text/javascript">

    <% if(this.id!=""){ %>
        GetSlide();
    <%} %>
    
    var signlist = "";
    var servicelist="";
    if ($("#hfSign").val() != "") {
        var strlist = $("#hfSign").val().split(',');

        signlist = $("#hfSign").val();
        for (var i = 0; i < strlist.length; i++) {
            $("input[signid=" + strlist[i] + "]").attr("checked=checked");

        }
    }

//    var servicelist = "";
//    if ($("#hfService").val() != "") {
//        var strlist = $("#hfService").val().split(',');

//        servicelist = $("#hfService").val();
//        for (var i = 0; i < strlist.length; i++) {
//            $("input[serviceid=" + strlist[i] + "]").attr("checked=checked");

//        }
//    }



    $("#FormateInfo").click(function () {
        var goodsId = "<%=this.id %>";
        if (goodsId == "")
            return;
        $("#Addcom_Pro").show().css({ "color": "#000", "background": "url(/Images/MenuA.png) no-repeat" });
        $("#Addcom_Bas").hide().css({ "color": "#FFF", "background": "url(/Images/MenuN.png) no-repeat" });
        $("#FormateInfo").css({ "color": "#000", "background": "url(/Images/MenuA.png) no-repeat" });
        $("#BaseInfo").css({ "color": "#FFF", "background": "url(/Images/MenuN.png) no-repeat" });
    });

    $("#BaseInfo").click(function () {
        $("#Addcom_Pro").hide().css({ "color": "#FFF", "background": "url(/Images/MenuN.png) no-repeat" });
        $("#Addcom_Bas").show().css({ "color": "#000", "background": "url(/Images/MenuA.png) no-repeat" });
        $("#FormateInfo").css({ "color": "#FFF", "background": "url(/Images/MenuN.png) no-repeat" });
        $("#BaseInfo").css({ "color": "#000", "background": "url(/Images/MenuA.png) no-repeat" });
    });


    $("body").on("click", "#addSlide", function () {
        $.layer({
            type: 2,
            shade: [0.1, '#000'],
            fix: false,
            title: ['上传幻灯片', true],
            maxmin: true,
            iframe: { src: 'T_Forms_Resource.aspx?goodsid=<%=this.id %>&r='+Math.random()},
            area: ['570px', '700px'],
            offset: ['0px', ''],
            close: function (index) {
                //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                GetSlide();
            }
        });
    });

    function GetSlide() {
        //alert("=.=");
        $("#slideImgList").html("幻灯片加载中");
        $.ajax({
            url: "GoodsHandler.ashx?flag=slide&goodsId=<%=this.id %>",
            dataType: "text",
            success: function (data) {
                $("#slideImgList").html(data);
            }
        });
    }

    function editMore() {
        var goodsId = "<%=this.id %>";
        if (goodsId == "") { alert('请先保存任务信息，再进行属性编辑'); return false; }
        var url = "EditGoodsPropertyIframe.aspx?goodsId=" + goodsId + "&r=" + Math.random(9999) + "";
        var returnValue = window.showModalDialog(url, "", "dialogWidth=500px;dialogHeight=350px;scroll:1;status:0;help:0;resizable:0;");
        if (returnValue != null) {
            var ajaxStr = $.ajax({ url: "GoodsInfo.ashx?method=moreproperty&goodsId=" + goodsId + "&r=" + Math.random() + "", async: false }).responseText;
            ajaxStr += "<tr><td class='Rtd ButBox' colspan='4' style='width:98%;padding:5px;text-align:center;height:45px;'><input type='button' class='CommonButon' value='编辑任务属性' onclick='editMore()'  /></td></tr>";
            $("#table_moreproperty").html(ajaxStr);
        }
        return false;
    }

    //任务规格
    function editeFormate(flag) {
        var goodsId = "<%=this.id %>";
        if (goodsId == "") { alert('请先保存任务信息，再进行规格编辑'); return false; }
        var url = "AddGoodsFormate.aspx?goodsId=" + goodsId + "&r=" + Math.random(9999) + "";
        if (flag == 2) { url = "EditGoodsFormate.aspx?goodsId=" + goodsId + "&r=" + Math.random(9999) + ""; }
        var returnValue = window.showModalDialog(url, "", "dialogWidth=830px;dialogHeight=600px;scroll:1;status:0;help:0;resizable:0;");
        var buttonStr = "<tr><td class='Rtd ButBox' colspan='4' style='width:98%;padding:5px;text-align:center;height:45px;'><input type='button' class='CommonButon' value='创建规格' onclick='editeFormate(1)' />&nbsp;&nbsp;<input type='button' class='CommomButtonStyle' value='编辑规格' onclick='editeFormate(2)' /></td></tr>";
        if (flag == 2) {
            var ajaxStr = $.ajax({ url: "GoodsInfo.ashx?method=listformate&goodsId=<%=this.id %>&r=" + Math.random() + "", async: false }).responseText;
            ajaxStr += buttonStr;
            $("#div_goods_formate").html(ajaxStr);
        } else {
            if (returnValue != null && typeof (returnValue) != undefined && returnValue != "") {
                var ajaxStr = $.ajax({ url: "GoodsInfo.ashx?method=listformate&goodsId=<%=this.id %>&r=" + Math.random() + "", async: false }).responseText;
                ajaxStr += buttonStr;
                $("#div_goods_formate").html(ajaxStr);
            }
        }
    }

    $("td").on("click", ".ckSign", function () {
        if ($(this).prop('checked')) {
            if (signlist == "") {
                signlist += $(this).attr("signid");
            } else {
                signlist += "," + $(this).attr("signid");
            }
        } else {
            if (signlist.indexOf("," + $(this).attr("signid")) != -1) {
                signlist = signlist.replace("," + $(this).attr("signid"), "");
            } else if (signlist.indexOf($(this).attr("signid") + ",") != -1) {
                signlist = signlist.replace($(this).attr("signid") + ",", "");
            } else {
                signlist = signlist.replace($(this).attr("signid"), "");
            }
        }
        $("#hfSign").val(signlist);
        //alert(signlist);
    });


//    $("td").on("click", ".ckService", function () {
//        if ($(this).prop('checked')) {
//            if (servicelist == "") {
//                servicelist += $(this).attr("serviceid");
//            } else {
//                servicelist += "," + $(this).attr("serviceid");
//            }
//        } else {
//            if (servicelist.indexOf("," + $(this).attr("serviceid")) != -1) {
//                servicelist = servicelist.replace("," + $(this).attr("serviceid"), "");
//            } else if (servicelist.indexOf($(this).attr("serviceid") + ",") != -1) {
//                servicelist = servicelist.replace($(this).attr("serviceid") + ",", "");
//            } else {
//                servicelist = servicelist.replace($(this).attr("serviceid"), "");
//            }
//        }
//        $("#hfService").val(servicelist);
//    });


      function  checkForm(){
        return true;
      }

      function play_video(){
          var path=$("#hidVideoPath").val();
          if(path==""){alert('无视频文件');return;}
           window.open("/Controls/MP4Player.aspx?path="+escape(path));
     }
</script>
</html>
    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
    <script language="javascript">
        $(function () {
            var editor = CKEDITOR.replace("hzst_ckeditor", { skin: "kama", width: document.body.scrollWidth - 300, height: 280 });
        });
    </script>