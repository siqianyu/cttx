<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckSeller.aspx.cs" Inherits="AppModules_Sysadmin_Seller_AddSeller" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商家信息审核</title>
    <link href="../../../Style/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../Style/PopUp.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/uploadify.css" rel="stylesheet" type="text/css" />
    <style>
        .ViewBox .Ltd
        {
            width: 8%;
        }
    </style>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="/js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#file_upload").uploadify({
                //指定swf文件
                'swf': '/js/uploadify.swf',
                //后台处理的页面
                'uploader': 'SellerService.ashx',
                //按钮显示的文字
                'buttonText': '选择图片',
                //在浏览窗口底部的文件类型下拉菜单中显示的文本
                'fileTypeDesc': 'Image Files',
                //允许上传的文件后缀
                'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png',
                //设置是否自动上传
                'auto': false,
                //设置为true将允许多文件上传
                'multi': false,
                'fileTypeDesc': '支持的格式：',
                'fileSizeLimit': '5MB',
                'removeTimeout': 1,
                
                //返回一个错误，选择文件的时候触发  
                'onSelectError': function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                            break;
                        case -110:
                            alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                            break;
                        case -120:
                            alert("文件 [" + file.name + "] 大小异常！");
                            break;
                        case -130:
                            alert("文件 [" + file.name + "] 类型不正确！");
                            break;
                    }
                },
                //检测FLASH失败调用  
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                'onUploadStart': function (file) {  
                 //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。 
                 // 后台使用 context.Request.Form 接收参数
                    $("#file_upload").uploadify("settings", "formData", { 'shopid': <%=id %>,"method":"charter"});  
                    
                } ,
                //上传到服务器，服务器返回相应信息到data里  
                'onUploadSuccess': function (file, data, response) {
                    //                addSeller(); // 文件上传以后再添加商家数据
                }
            });
        });

        function doUplaod() {
            $('#file_upload').uploadify('upload', '*');

        }

        function closeLoad() {
            $('#file_upload').uploadify('cancel', '*');
        }

        function showTab() {
            $("#tab_info").hide();
            $("#tab_upload").show();
        }

        function layer_close() {
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }

        function layer_close_flesh() {
            parent.grid_search(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
</head>
<body style="text-align: left;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" class="ViewBox">
        <tr>
            <td class="Ltd">
                用户名：
            </td>
            <td class="Rtd">
                <asp:Label ID="txtUserName" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="Ltd">
                保证金：
            </td>
            <td class="Rtd">
                <input id='txtBZJ' type="text" runat="server" />(元)
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                公司名称：
            </td>
            <td class="Rtd">
                <asp:Literal ID="CompanyName" runat="server"></asp:Literal>
            </td>
            <td class="Ltd">
                店铺名称：
            </td>
            <td class="Rtd">
                <asp:Literal ID="ShopName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                地区：
            </td>
            <td class="Rtd">
                <asp:Literal ID="area" runat="server"></asp:Literal>
            </td>
            <td class="Ltd">
                详细地址：
            </td>
            <td class="Rtd">
                <asp:Literal ID="address" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                联系人：
            </td>
            <td class="Rtd">
                <asp:Literal ID="Contact" runat="server"></asp:Literal>
            </td>
            <td class="Ltd">
                手机号码：
            </td>
            <td class="Rtd">
                <asp:Literal ID="Phone" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                联系邮箱：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="email" runat="server"></asp:TextBox>
                <%-- <asp:Literal ID="email" runat="server"></asp:Literal>--%>
            </td>
            <td class="Ltd">
                QQ号：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="qq" runat="server"></asp:TextBox>
                <%-- <asp:Literal ID="qq" runat="server"></asp:Literal>--%>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                出售类型：
            </td>
            <td class="Rtd">
                <asp:Literal ID="categroytype" runat="server"></asp:Literal>
                <a style="cursor: pointer;color:red; padding-left:35px;" onclick="showbox()">修改</a>
            </td>
            <td class="Ltd">
                排序：
            </td>
            <td class="Rtd" colspan="3">
                <asp:TextBox ID="txtOrder" runat="server" Width="70px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                营业执照：
            </td>
            <td class="Rtd" colspan="3">
                <div id="tab_info">
                    <asp:Image ID="SmallPic" Width="200" runat="server" />
                    <a style="cursor: pointer" onclick="showTab()">添加|修改</a>
                </div>
                <div id="tab_upload" style="display: none">
                    <input type="file" id="file_upload" runat="server" />
                    <a style="cursor: pointer" onclick="doUplaod()">上传</a> | <a style="cursor: pointer"
                        onclick="closeLoad()">取消上传</a>
                </div>
            </td>
        </tr>
        <tr style="display: none">
            <td class="Ltd">
                备注：
            </td>
            <td class="Rtd" colspan="3">
                <asp:TextBox ID="Mark" TextMode="MultiLine" runat="server" Height="104px" Width="299px"
                    ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                帐号状态：
            </td>
            <td class="Rtd">
                <asp:RadioButtonList ID="rbAccountsState" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Unchecked">未审核</asp:ListItem>
                    <asp:ListItem Value="Unpass">不通过</asp:ListItem>
                    <asp:ListItem Value="Normal">正常</asp:ListItem>
                    <asp:ListItem Value="Disable">禁用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="Ltd">
                店铺状态：
            </td>
            <td class="Rtd">
                <asp:RadioButtonList ID="rbOpen" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">开启</asp:ListItem>
                    <asp:ListItem Value="0">关闭</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="trOwn" runat="server">
            <td class="Ltd" colspan="1">
                数据初始化：
            </td>
            <td class="Rtd" colspan="3">
                <input id="btnInit" type="button" style="padding: 5px 15px" value="初始化任务数据" />
            </td>
        </tr>
        <tr class="ButBox">
            <td colspan="4" style="height: 50px; text-align: center;" class="Ltd">
                <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="保存" OnClick="btnSave_Click"
                    Text="保存" BorderWidth="0" Width="135px" Height="32px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" class="Return" value="返回" style="border: 0px; width: 135px;
                    height: 32px;" onclick="layer_close()" />
            </td>
        </tr>
    </table>
    </form>
    <script>
        var btnInit = document.getElementById("btnInit");
        if (btnInit) {
            btnInit.onclick = function () {
                if (confirm("确定初始化该商家的任务数据吗？")) {
                    $.ajax({
                        type: "GET",
                        url: "GetGoodsHandler.ashx",
                        dataType: "text",
                        data: "flag=goods&shopId=<%=id %>",
                        success: function (res) {
                            if (res === "success") {
                                alert("任务数据初始化成功！");
                            }
                            else {
                                alert("初始化失败！");
                            }
                        },
                        error: function (error) {
                            alert("服务器繁忙，请稍后重试！");
                        }
                    });
                }
            }
        }
    </script>
    <script>
        function showbox() {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['销售类型修改', true],
                maxmin: true,
                iframe: { src: 'ModifyCategory.aspx?id=<%=id %>' },
                area: ['1000', '300'],
                offset: ['0px', ''],
                close: function (index) {

                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }
    </script>
</body>
</html>
