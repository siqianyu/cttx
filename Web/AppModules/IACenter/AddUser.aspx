<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AppModules_IACenter_AddUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户添加</title>
    <link href="/skin/blue/css/style.css" type="text/css" rel="stylesheet" />
    <!--div层父窗口交互控制_start(代码的位置必须在body前面)-->
    <style>
        #addUser_table
        {
            margin: 0 auto;
            line-height: 30px;
            font-size: 12px;
            font-family: 微软雅黑;
        }
        #addUser_table, #addUser_table td
        {
            border: 1px solid #CCC;
            border-collapse: collapse;
        }
        #addUser_table .td_title
        {
            text-align: right;
            padding-right: 5px;
            width: 100px;
        }
        #addUser_table .td_info1
        {
            text-align: left;
            padding-left: 5px;
        }
    </style>
    <script type="text/javascript" language="javascript" src="/js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" language="javascript" src="/js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="/js/calendar.js"></script>
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
    <script language="javascript" type="text/javascript">
        function checkForm() {
            if (document.getElementById("Sbm").value == "") {
                alert("请选择所属部门！");
                return false;
            }
            if (document.getElementById("txttruename").value == "") {
                alert("请输入真实姓名！");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
    <div class="div_right">
        <%--<h2>
            <span class="title_left"></span><b></b><span class="title_right"></span></h2>--%>
        <table width="805" class="table_1" id="addUser_table">
            <caption style="font-size: 18px; padding-bottom: 20px;">
                用户添加</caption>
            <tr>
                <td class="td_title">
                    所属部门：
                </td>
                <td class="td_info1" colspan="3">
                    <select id="Sbm" runat="server">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    用户名：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txtusername" runat="server"
                        style="height: 16px" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    真实姓名：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txttruename" runat="server"
                        style="height: 16px" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    性别：
                </td>
                <td class="td_info1" colspan="3">
                    <select id="Ssex" runat="server">
                        <option value="">请选择</option>
                        <option value="男">男</option>
                        <option value="女">女</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    年龄：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txtage" runat="server" style="height: 16px"
                        value="0" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    电话：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txttel" runat="server" style="height: 16px" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    手机：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txtsj" runat="server" style="height: 16px" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    传真：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txtcz" runat="server" style="height: 16px" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    工作描述：
                </td>
                <td class="td_info1" colspan="3">
                    <asp:TextBox runat="server" ID="txtJob" TextMode="MultiLine" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    排序：
                </td>
                <td class="td_info1" colspan="3">
                    <input name="input3" type="text" class="input_5" id="txtsort" runat="server" style="height: 16px"
                        value="0" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    启用：
                </td>
                <td class="td_info1" colspan="3">
                    <asp:CheckBox ID="chkqy" runat="server" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="td_title">
                    设为超级用户：
                </td>
                <td class="td_info1" colspan="3">
                    <asp:CheckBox ID="chkadmin" runat="server" Checked="false" />
                </td>
            </tr>
        </table>
        <center style="padding-top: 20px;">
            <asp:ImageButton ID="btnSave" ImageUrl="~/Images/skin/SubmitA1.png" runat="server"
                CssClass="save" OnClick="btnSave_Click" OnClientClick="return checkForm()" />
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
