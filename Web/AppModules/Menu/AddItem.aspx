<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItem.aspx.cs" Inherits="AppModules_Menu_AddItem" %>

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
    <title>食材添加</title>

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
    </script>
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
                        <span style="color: #ff0000">*</span>是否支持本站购买：
                        </td>
                    <td class="Rtd" colspan='3'>
                        <span id='rbW'><asp:RadioButton ID="rbWrite" runat="server" GroupName='ly' Text='不支持' Checked="true" /></span>
                        <span id='rbC'><asp:RadioButton ID="rbChoose" runat="server" GroupName='ly' Text="支持，并选择任务" /></span>
                        <a id='chooseGoods' href='javascript:void(0)' style='display:none'>选择>></a>
                        <asp:HiddenField ID="hfGoodsId" runat="server" />
                    </td>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>食材名：
                        </td>
                    <td class="Rtd" colspan='3'>
                        <asp:TextBox  ID="txtItemName" ClientIDMode="Static" runat="server" Width="150px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
 



                </tr>

                <tr>
                        <td class="Ltd">
                        <span style="color: #ff0000">*</span>图片：

                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:FileUpload ID="fuImg" runat="server" />
                            <span style='color:#F00;'>说明：图片建议尺寸：150×150,建议使用PNG格式</span>

                            <br />
                            <asp:Image ID="imgItem" runat="server" Width='150px' Height='150px' style='display:none;' />
                            <asp:HiddenField ID="hfImgSrc" runat="server" />
                        </td>

                </tr>
                                
              <tr>
                                    <td class="Ltd">
                        <span style="color: #ff0000"></span>单位：</td>
                    <td class="Rtd" colspan="3">
                        <asp:TextBox ID="txtUnit" runat="server"></asp:TextBox>
                        <span style='color:#F00;'>输入每份的含量和单位，如：500克，1个，1勺</span>
                        </td>

                </tr>

                <tr>
                                    <td class="Ltd">
                        <span style="color: #ff0000"></span>排序：</td>
                    <td class="Rtd" colspan="3">
                        <asp:TextBox ID="txtOrderby" runat="server"></asp:TextBox>
                        </td>

                </tr>



                <tr>

                    <td class="Ltd">
                        <span style="color: #ff0000"></span>备注：</td>
                    <td class="Rtd" colspan='3'>
                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" 
                            Height="182px" Width="379px"></asp:TextBox>
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
        </div>

        <script type="text/javascript">
            <%if(ifShowImg){ %>
                $("#imgItem").show();
            <%} %>

            var goodsName = '';
            var goodsId = "";
            var img = "";

            $("#rbC").on("click", function () {
                $("#chooseGoods").show();
                $("#txtItemName").attr("readonly", "readonly");
                $("#txtUnit").attr("readonly","readonly");
                $("#fuImg").hide();

            });

            $("#rbW").on("click", function () {
                $("#chooseGoods").hide();
                $("#txtItemName").removeAttr("readonly");
                $("#txtUnit").removeAttr("readonly");
                $("#fuImg").show();

            });

            $("#chooseGoods").on("click", function () {
                $.layer({
                    type: 2,
                    shade: [0.1, '#000'],
                    fix: false,
                    title: ['选择任务', true],
                    maxmin: true,
                    iframe: { src: 'GoodsListItem.aspx?r=' + Math.random() },
                    area: [600, $(document).height()],
                    offset: ['0', ''],
                    close: function (index) {

                    }
                });
            });


            function GetData(id, name, pic,unit) {
                $("#hfGoodsId").val("" + id);
                $("#txtItemName").val(name);
                $("#imgItem").attr("src", pic);
                $("#imgItem").show();
                $("#hfImgSrc").val(pic);
                $("#txtUnit").val(unit);
            }

        </script>

        </form>
        </body>
</html>
