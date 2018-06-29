<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddGroup.aspx.cs" Inherits="AppModules_IACenter_AddGroup" %>


<%@ Register Src="~/Controls/SelectPanel.ascx" TagName="SelectPanel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户组名称</title>
    <link href="/skin/blue/css/style.css" type="text/css" rel="stylesheet" />
    <style>
        .file
        {
            font-size: 12px;
            line-height: 16px;
            height: 20px;
            width: 500px;
        }
    </style>
    <style>
        #addGroup_table
        {
            margin: 0 auto;
            line-height: 50px;
            font-size: 12px;
            font-family: 微软雅黑;
        }
        #addGroup_table .td_title
        {
            text-align: right;
            padding-right: 5px;
        }
        #addGroup_table .td_info1
        {
            text-align: left;
            padding-left: 5px;
        }
    </style>
    <script type="text/javascript" language="javascript" src="/js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" language="javascript" src="/js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="/js/calendar.js"></script>

    <!--div层父窗口交互控制_start(代码的位置必须在body前面)-->
    <script language="javascript">
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
    <!--div层父窗口交互控制_end-->


    <script language="javascript">
        function checkForm() {
            

            if ($("#txtTitle").val() == "") {
                alert('请输入用户组名称');
                return false;
            }

            
            return true;
        }

        function setFilesNumber() {
            var i = parseInt($("#filecount").val());
            if (!i) { i = 1; }
            var len = $("#table_files tr").length;
            //alert(len);
            if (i == len) { return false; }
            if (i > len) {
                for (var n = len + 1; n <= i; n++) {
                    $("#table_files").append("<tr><td style='text-align:left'><input type='file' name='file_" + n + "' id='file_" + n + "' class='file' /></td></tr>");
                }
            } else {
                while (true) {
                    if (i < $("#table_files tr").length) {
                        $("#table_files tr:last").remove();
                    } else { break; }
                }
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
    <div class="div_right">
        <%--<h2>
            <span class="title_left"></span><b>用户组名称</b><span class="title_right"></span></h2>--%>
        <table width="805" cellspacing="0" cellpadding="0" class="table_1" id="addGroup_table">
            <caption style="font-size: 18px; margin: 20px auto 10px; border-bottom: 2px solid #ccc">
                用户组名称</caption>
            <tr>
                <td class="td_title">
                    用户组名称：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txtTitle" runat="server" style="height: 16px;
                        width: 500px;" /><span class="gray">&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    用户组成员：
                </td>
                <td class="td_info1" colspan="3">
                    <uc1:SelectPanel ID="SelectPanel1" TextMode="MultiLine" DialogUrl="/AppModules/IACenter/DepartUser.aspx?SelectMode=1"
                        Width="500" Height="50" runat="server" />
                </td>
            </tr>
        </table>
        <center style="padding-top: 30px">
            <asp:ImageButton ID="btnSave" ImageUrl="~/Images/skin/SubmitA1.png" runat="server"
                CssClass="save" OnClick="btnSave_Click"  OnClientClick="return  checkForm()" />
            &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/skin/ReturnA1.png" runat="server"
                CssClass="save" OnClientClick="layer_close_refresh()" />
        </center>
    </div>
    <div class="clear">
    </div>
    </form>
</body>
</html>
