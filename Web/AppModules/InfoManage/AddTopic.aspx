<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTopic.aspx.cs" Inherits="AppModules_InfoManage_AddTopic" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文章查看页面</title>
    <link href="../../Style/Common.css" rel="stylesheet" />
    <link href="../../Style/PopUp.css" rel="stylesheet" />
    <script type="text/javascript" src="../../My97DatePicker/WdatePicker.js"></script>
    <!--div层父窗口交互控制_start(代码的位置必须在body前面)-->
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

        function checkForm() {
            var title = document.getElementById("txtTitle");
            var url = document.getElementById("txtUrl");
            var content = document.getElementById("txtContent");
            var img = document.getElementById("topicImg");
            var file = document.getElementById("picUpload");
            var sort = document.getElementById("txtSort");

            if (title.value.replace(/\s+/g, "") == "") {
                alert("请填写标准专题");
                return false;
            }
            if (url.value.replace(/\s+/g, "") == "") {
                alert("请填写专题链接");
                return false;
            }
            if (content.value.replace(/\s+/g, "") == "") {
                alert("请填写专题描述");
                return false;
            }
            if (file.value == "" && (img.src.toString().indexOf("nopic") != -1)) {
                alert("请上传专题图片");
                return false;
            }
            if (sort.value != "" && !parseInt(sort.value)) {
                alert("排序值应为数字");
                return false;
            }
        }

        function showImg() {

            var pic = document.getElementById("topicImg");
            var file = document.getElementById("picUpload");
            var ext = file.value.substring(file.value.lastIndexOf(".") + 1).toLowerCase();
            // gif在IE浏览器暂时无法显示
            if (ext != 'png' && ext != 'jpg' && ext != 'jpeg') {
                return;
            }
            var isIE = !!window.ActiveXObject; // 判断是否IE浏览器
            var ie6 = isIE && !window.XMLHttpRequest; //判断是否IE6
            if (window.FileReader) {
                // 如果支持HTML5的FileReader接口（chrome,firefox7+,opera,IE10,IE9）

                var file = file.files[0];
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var pic = document.getElementById("topicImg");
                    pic.src = this.result;
                }
            }
            else if (isIE) {
                // 不支持HTML5（IE），IE6特殊处理，IE9(移除焦点，解决因安全限制拒绝访问的问题)

                file.select();
                document.getElementById("td_imgPreview").focus(); //解决上传到服务器上后，特殊情况下ie9还是拒绝访问的问题
                var reallocalpath = document.selection.createRange().text;
                if (ie6) {
                    // IE6浏览器设置img的src为本地路径可以直接显示图片
                    pic.src = reallocalpath;
                } else {
                    // 非IE6版本的IE由于安全问题直接设置img的src无法显示本地图片，但是可以通过滤镜来实现
                    pic.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src=\"" + reallocalpath + "\")";
                    // 设置img的src为base64编码的透明图片 取消显示浏览器默认图片
                    pic.src = 'data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==';
                }
            }
            else if (file.files) {
                // firefox6
                if (file.files.item(0)) {
                    url = file.files.item(0).getAsDataURL();
                    pic.src = url;
                }
            }
        }
    </script>
    <!--div层父窗口交互控制_end-->
</head>
<body>
    <form id="form1" runat="server">
    <table class="ModTab" cellpadding="0" cellspacing="1">
        <tr>
            <td class="Ltd">
                专题名称：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtTitle" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td class="Ltd">
                链接地址：
            </td>
            <td class="Rtd">
                <asp:TextBox ID="txtUrl" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                添加图片：
            </td>
            <td class="Rtd" colspan="3" id="td_imgPreview">
                <img runat="server" id="topicImg" width="150" name="topicImg" height="100" src="../../images/nopic.jpg" />
                <input type="file" id="picUpload" name="picUpload" runat="server" onchange="showImg();" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                专题描述：
            </td>
            <td class="Rtd" colspan="3">
                <asp:TextBox ID="txtContent" runat="server" Width="600px" Height="90px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                排序：
            </td>
            <td class="Rtd" colspan="3">
                <asp:TextBox ID="txtSort" runat="server" Width="250px" Text="99"></asp:TextBox><span
                    style="text-align: left; color: Red;">&nbsp;&nbsp; 注意：数字越小越靠前，默认为99</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                是否为新专题：
            </td>
            <td class="Rtd" colspan="3">
                <asp:RadioButton ID="rbtnYes" runat="server" Text="是" GroupName="sort" />
                &nbsp;
                <asp:RadioButton ID="rbtnNo" runat="server" Text="否" Checked="True" GroupName="sort" />
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                共享平台：
            </td>
            <td class="Rtd" colspan="3">
                <asp:CheckBoxList ID="ckPlatFormList" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                专题新闻关键字：
            </td>
            <td class="Rtd" colspan="3">
                <asp:TextBox ID="txtKeyWord" runat="server" Width="250px"></asp:TextBox>
                <span style="padding-left:20px; color:Gray">关键字用于相关体系平台新闻数据筛选，多个关键字请用中文或英文分号分隔</span>
            </td>
        </tr>
        <tr>
            <td class="Ltd">
                所属体系 ：
            </td>
            <td class="Rtd" colspan="3">
                <asp:DropDownList ID="ddlTxPlatform" runat="server">
                </asp:DropDownList>
                <span style="padding-left:20px; color:Gray">选择该专题对应的体系平台，便于根据专题关键字筛选数据</span>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="ButBox">
                <p>
                    <asp:LinkButton ID="LinkButton1" runat="server" class="Submit" title="保存" OnClientClick="return checkForm();"
                        OnClick="LinkButton1_Click">保 存</asp:LinkButton><a href="Javascript:layer_close();void(0);"
                            class="Return" title="返回">返&nbsp;回</a></p>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfanalyse_is_certify" runat="server" />
    </form>
</body>
<!--div层父窗口交互控制_end-->
</html>
