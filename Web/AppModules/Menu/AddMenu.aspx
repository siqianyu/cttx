<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMenu.aspx.cs" Inherits="AppModules_Menu_AddMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/Controls/CategorySelectMenu.ascx" TagName="cselect" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="Expires" CONTENT="0">  
    <meta http-equiv="Cache-Control" CONTENT="no-cache">  
    <meta http-equiv="Pragma" CONTENT="no-cache"> 
    <%
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1); 
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        %>
    <title>菜谱添加</title>

    <style type="text/css">
        #cke_1_bottom
        {
            display: none;
        }
        .cateSelect
        {
            width: 100px;
        }
        .item
        {
            background:#B2DFEE;
            float:left;
            padding:1px;
            margin:1px;
        }
        .itemC
        {
            cursor:pointer;
        }
        .itemV
        {
            width:60px;
        }
        .itemB
        {
            width:30px;
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
                        <span style="color: #ff0000">*</span>菜名：
                        </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtMenuName" runat="server" Width="150px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                        <td class="Ltd">
                        <span style="color: #ff0000">*</span>烹饪工艺：

                        </td>
                        <td class="Rtd">
                                                <asp:TextBox ID="txtTechnology" runat="server" Width="150px" CssClass="Enter" MaxLength="100" ></asp:TextBox>

                            <%--<asp:dropdownlist id='ddlTechnology' runat="server">
                            <asp:ListItem>炒</asp:ListItem>
                            <asp:ListItem>炸</asp:ListItem>
                            <asp:ListItem>蒸</asp:ListItem>
                            <asp:ListItem>炖</asp:ListItem>
                            </asp:dropdownlist>--%>
                        </td>
                </tr>
                <tr>
                <td class="Ltd">菜谱标签：</td>
                <td class="Rtd" colspan='3'>
                            <asp:Literal ID="ltSign" runat="server"></asp:Literal>
                            <asp:HiddenField ID="hfSign" runat="server" />
                </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>分类：
                    </td>
                    <td class="Rtd" >
                    <uc2:cselect ID='selectMenu' runat="server" />
                    </td> 
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>烹饪时间：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtCookieTime" runat="server"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>口味：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtFlavor" runat="server"></asp:TextBox>
                        </td>
                                            <td class="Ltd">
                                                卡路里(cal)：</td>
                    <td class="Rtd">
                        
                        <asp:TextBox ID="txtCalorie" runat="server" Width="150px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>烹饪步骤：</td>
                    <td class="Rtd" colspan="3">
                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                        <input id="btStepAdd" type="button" value="增加步骤+" />
                        <input id="btStepAdd2" type="button" value="批量生成步骤+" />生成<input id="txtStepNum" type="text" style='width:50px;' />步（重新生成）
                        <div id='stepList'>
                        <%=stepHtml %>
                        </div>
                        <asp:HiddenField ID="hfStepHtml" runat="server" />
                        <asp:HiddenField ID="hfStepList" runat="server" />
                    </td>

                </tr>

                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>食材：</td>
                    <td class="Rtd" colspan="3">
                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                        <input id="btItemAdd" type="button" value="添加样品+" /><span style='color:#FF0000'>说明：样品的分量根据食材的用料，作简单说明，购买数量输入数字，代表需要购买的任务份数</span>
                        <div id='itemList'>
                        <div style='overflow: hidden;'><div>主料：</div>
                        <div id='itemListZ'><%=itemHtmlZ %></div></div>
                        <div style='overflow: hidden;'><div>辅料：</div>
                        <div id='itemListF'><%=itemHtmlF %></div></div>
                        <div style='overflow: hidden;'><div>调料：</div>
                        <div id='itemListT'><%=itemHtmlT %></div></div>
                        </div>
                        <asp:HiddenField ID="hfItemHtml" runat="server" />
                        <asp:HiddenField ID="hfItemList" runat="server" />
                    </td>

                </tr>

                <tr>

                    <td class="Ltd">
                        <span style="color: #ff0000"></span>烹饪技巧：</td>
                    <td class="Rtd" colspan='3'>
                        <asp:TextBox ID="txtCookingSkill" runat="server" TextMode="MultiLine" 
                            Height="80px" Width="600px"></asp:TextBox>
                    </td>


                </tr>


                <tr>
                     <td class="Ltd">
                        <span style="color: #ff0000"></span>是否推荐到首页：</td>
                    <td class="Rtd" colspan='3'>
                        <asp:CheckBox ID="cbTop" runat="server" />
                     </td>
                </tr>

               <tr>
                     <td class="Ltd">
                        <span style="color: #ff0000"></span>是否显示：</td>
                    <td class="Rtd" colspan='3'>
                        <asp:CheckBox ID="cbShow" runat="server" />
                     </td>
                </tr>


                 <tr>
                     <td class="Ltd">
                        <span style="color: #ff0000"></span>展示图：</td>
                    <td class="Rtd">
                        <asp:FileUpload ID="fuBigImg" runat="server" />
                        <br />
                            <span style='color:#F00;'>说明：图片建议尺寸：400×400,建议使用PNG格式</span>

                        <br />
                        <asp:Literal ID="llBigImg" runat="server"></asp:Literal>
                        </td>
                                            <td class="Ltd">
                        <span style="color: #ff0000"></span>预览图：</td>
                    <td class="Rtd">
                        
                        <asp:FileUpload ID="fuSmallImg" runat="server" />
                      <br />
                            <span style='color:#F00;'>说明：图片建议尺寸：150×150,建议使用PNG格式</span>

                        <br />
                        <asp:Literal ID="llSmallImg" runat="server"></asp:Literal>
                        </td>
                </tr>

               <tr class="ButBox">
                  	<td colspan="4" style="height:30px; padding-top:4px; background-color:#f6f6f6;" align="center">
                                            <asp:ImageButton ID="btSend" ImageUrl="~/Images/skin/SubmitA1.png" runat="server"
                            CssClass="save" OnClick="btnSave_Click" OnClientClick="return checkForm()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/skin/ReturnA1.png" runat="server"
                            CssClass="save" OnClientClick="layer_close_refresh()" onclick="ImageButton2_Click" />

                    </td>
                </tr>
            </table>
            </div>
            <div style="height:100px;"></div>
        </div>
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

            

    </script>

        <script type="text/javascript">

            var signlist = "";
            if ($("#hfSign").val() != "") {
                var strlist = $("#hfSign").val().split(',');

                signlist = $("#hfSign").val();
                for (var i = 0; i < strlist.length; i++) {
                    $("input[signid=" + strlist[i] + "]").attr("checked=checked");

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
            });


            function checkForm() {
                $("#hfStepHtml").val(espaceHtmll($("#stepList").text()));
                $("#hfItemHtml").val(espaceHtmll($("#itemList").html()));
                var strStep = "";
                $(".stepText").each(function (i) {
                    if (i != 0)
                        strStep += "|";
                    strStep += $(this).val();
                    $(".stepImgSpan:eq(" + i + ") img").each(function (j) {
                        if (j != 0) {
                            strStep += "^";
                        } else {
                            strStep += "_";
                        }
                        strStep += $(".stepImgSpan:eq(" + i + ")").find("img:eq("+j+")").attr("src");
                    });
                });
                $("#hfStepList").val(espaceHtmll(strStep));

                var strItem = "";

                $("#itemListZ .itemV").each(function (i) {
                    if (i != 0) {
                        strItem += ",";
                    }
                    strItem += $(this).attr("index");
                    strItem += "_" + $(this).val();
                    var ib = $(this).parent().parent().find(".itemB").val();
                    strItem += "_" + ib;
                    strItem += "_" + $(this).attr("goodscode");

                });
                strItem += "@";
                $("#itemListF .itemV").each(function (i) {
                    if (i != 0) {
                        strItem += ",";
                    }
                    strItem += $(this).attr("index");
                    strItem += "_" + $(this).val();
                    var ib = $(this).parent().parent().find(".itemB").val();
                    strItem += "_" + ib;
                    strItem += "_" + $(this).attr("goodscode");
                });
                strItem += "@";

                $("#itemListT .itemV").each(function (i) {
                    if (i != 0) {
                        strItem += ",";
                    }
                    strItem += $(this).attr("index");
                    strItem += "_" + $(this).val();
                    var ib = $(this).parent().parent().find(".itemB").val();
                    strItem += "_" + ib;
                    strItem += "_" + $(this).attr("goodscode");
                });

                $("#hfItemList").val(strItem);

                return true;

            }

            function espaceHtmll(str) {
                var strs = str.replace(/</g, "&lt");
                strs = strs.replace(/>/g, "&rt");
                return strs;
            }


            function GetItem(id, name, type,code) {
                var indexZ = $("#itemListZ span").index($("span[index=" + id + "]"));
                //var indexZ = $("span[index=" + id + "]").index($("#itemListZ"))
                var indexF = $("#itemListF span").index($("span[index=" + id + "]"));
                var indexT = $("#itemListT span").index($("span[index=" + id + "]"));
                //                if (index < 0) {
                if (type == "主料") {
                    if (indexZ >= 0) {
                        $("#itemListZ .itemV[index=" + id + "]").attr("goodscode", code);
                        
                    } else {
                        $("#itemListZ").append("<span class='item'><span index='" + id + "' goodscode='" + code + "' class='itemT'>" + name + "</span><span> 分量<input  index='" + id + "' class='itemV'  goodscode='" + code + "'  type='text' value=''/></span><span> 购买量<input  index='" + id + "' class='itemB'  type='text' value='1'/></span><span class='itemC'>×</span></span>");
                    }
                }
                else if (type == "辅料") {
                    if (indexF >= 0) {
                        $("#itemListF .itemV[index=" + id + "]").attr("goodscode", code);
                    } else {
                        $("#itemListF").append("<span class='item'><span index='" + id + "' goodscode='" + code + "'  class='itemT'>" + name + "</span><span> 分量<input  index='" + id + "' class='itemV'  goodscode='" + code + "'  type='text' value=''/></span><span> 购买量<input  index='" + id + "' class='itemB' type='text' value='1'/></span><span class='itemC'>×</span></span>");
                    }
                }
                else if (type == "调料") {
                    if (indexT >= 0) {
                        $("#itemListT .itemV[index=" + id + "]").attr("goodscode", code);
                    } else {
                        $("#itemListT").append("<span class='item'><span index='" + id + "' goodscode='" + code + "'  class='itemT'>" + name + "</span><span> 分量<input  index='" + id + "' class='itemV'  goodscode='" + code + "'  type='text' value=''/></span><span> 购买量<input  index='" + id + "' class='itemB' type='text' value='1'/></span><span class='itemC'>×</span></span>");
                    } 
                }

                $("#hfItemHtml").val($("#itemList").html());
                
            }


            $("#btStepAdd").click(function () {
                var table = "<div class='stepDiv'><div style='width:600px;'><b><font class='stepBz'></font></b><a href='javascript:void(0)' style='float:right;color:blue;' class='stepImg'>加入图片</a><span style='float:right;color:blue;' class=''>&nbsp;</span><a href='javascript:void(0)' style='float:right;color:blue;' class='stepDel'>删除</a></div>"
                + "<textarea class='stepText' style='width:600px;height:50px'></textarea><br/>"
                +"<span class='stepImgSpan'></span></div>";
                $("#stepList").append(table);
                $("#hfStepHtml").val($("#stepList").html());

                $(".stepBz").each(function (i) {
                    $(this).text("步骤" + (i+1));
                });
            });

            $("#btStepAdd2").click(function () {
                var table1 = "<div class='stepDiv'><div style='width:600px;'><b><font class='stepBz'></font></b><a href='javascript:void(0)' style='float:right;color:blue;' class='stepImg'>加入图片</a><span style='float:right;color:blue;' class=''>&nbsp;</span><a href='javascript:void(0)' style='float:right;color:blue;' class='stepDel'>删除</a></div>"
                + "<textarea class='stepText' style='width:600px;height:50px'></textarea><br/>"
                + "<span class='stepImgSpan'></span></div>";
                var table = table1;
                var num = parseInt($("#txtStepNum").val());
                if (num == 0)
                    return;
                if (num > 1) {
                    for (var i = 0; i < num - 1; i++) {
                        table += table1;
                    }
                }
                $("#stepList").append(table);
                $("#hfStepHtml").val($("#stepList").html());

                $(".stepBz").each(function (i) {
                    $(this).text("步骤" + (i + 1));
                });
            });



            $("#stepList").on("click", ".stepDel", function () {
                $(this).parent().parent().remove();
                $(".stepBz").each(function (i) {
                    $(this).text("步骤" + (i + 1));
                });
            });

            $("#itemList").on("click", ".itemC", function () {
                $(this).parent().remove();
            });


            $("#btItemAdd").click(function () {
                
                $.layer({
                    type: 2,
                    shade: [0.1, '#000'],
                    fix: false,
                    title: ['添加', true],
                    maxmin: true,
                    iframe: { src: 'ItemListMenu.aspx?r=' + Math.random() },
                    area: [800, 600],
                    offset: ['0px', ''],
                    close: function (index) {
                        //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                    }
                });
            });


            $("body").on("click", ".stepImg", function () {
                var num = $(".stepImg").index($(this)) + 1;
                var menuId = "";
                if ($(this).attr("menuId") != undefined)
                    menuId = $(this).attr("menuId");
                $.layer({
                    type: 2,
                    shade: [0.1, '#000'],
                    fix: false,
                    title: ['添加', true],
                    maxmin: true,
                    iframe: { src: 'UploadImg.aspx?r=' + Math.random() + "&num=" + num + "&menuId=" + menuId },
                    area: [450, 420],
                    offset: ['0px', ''],
                    close: function (index) {
                        //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                    }
                });
            });


            function GetImage(index, menuId, imgList) {
                index = index - 1;
                var imgs = imgList.toString().split('|');
                var img = "";
                for (var i = 0; i < imgs.length; i++) {
                    img += "<img src='"+imgs[i]+"'  height='50' style='margin:0px 1px;' />";
                }
                    $(".stepImgSpan:eq(" + index + ")").html(img);
            }

        </script>
        
        </form>
        </body>

</html>
        

