<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMenuType.aspx.cs" Inherits="AppModules_Menu_AddMenuType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/Controls/CategorySelectMenu.ascx" TagName="cselect" TagPrefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="/skin/blue/css/style.css" type="text/css" rel="stylesheet" />
<%--    <script language="javascript" type="text/javascript" src="../js/jquery-1.3.2.js"></script>
    <script language="javascript" type="text/javascript" src="../../skin/blue/js/effect.js"        id="ChildJsCss"></script>--%>

    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

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

    <script type="text/javascript">
        function openWinDialog(url, arg) {
            return window.showModalDialog(url, arg, 'edge:raised;scroll:0;status:0;help:0;resizable:1;dialogWidth:420px;dialogHeight:205px;')
        }
        function selectPic(showReturnValueObj) {
            openWinDialog('../menu/selecticon.aspx', $(showReturnValueObj));
        }
        $(document).ready(function () {
            var css = parent.document.all.cssfile.href;
            document.all.ChildcssFile.href = css;
            var js = parent.document.all.jsCss.src;
            document.all.ChildJsCss.src = js;
        });

        function checkForm() {
            var txtMenuName = document.getElementById("txtMenuName").value;
            var txtSort = document.getElementById("txtSort").value;
            if (txtMenuName == "") {
                alert("菜单名称不能为空");
                return false;
            }
            else if (txtSort == "") {
                alert("排列序号不能为空");
                return false;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        //Check All
        function chkAll() {
            var f; //define & find the form object
            var isFound = false;
            for (var i = 0; i < document.forms.length; i++) {
                f = document.forms[i];
                if (f.checkall)	//key item : named 'checkall'
                {
                    isFound = true;
                    break;
                }
            }
            if (!isFound) return;

            var isAllChecked = f.checkall.checked;
            for (var i = 0; i < f.elements.length; i++) {
                if (f.elements[i].type.toLowerCase() == 'checkbox') {
                    f.elements[i].checked = isAllChecked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server" method="post">

    <div id="right">

            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>分类名称：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtMenuName" runat="server" Width="200px" CssClass="Enter" MaxLength="100" ></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>上级分类：</td>
                    <td class="Rtd">
                        
                    <uc2:cselect id="cSelect" runat="server"></uc2:cselect>
                        </td>
                </tr>

                <tr>
                    <td class="Ltd">排列序号：</td>
                    <td class="Rtd">

                                            <asp:TextBox ID="txtSort" runat="server" Width="80px" CssClass="input_add"></asp:TextBox><span
                            style="color: Red; font-size: 16px; line-height: 30px; padding-left: 10px">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="字段不能为空"
                            ControlToValidate="txtSort"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSort"
                            ErrorMessage="* 请输入正整数" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">可见性：</td>
                    <td class="Rtd">
                        <asp:CheckBox ID="cbIsVisible" runat="server" Text="在菜单中显示" Checked="true" />
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">分类图片：</td>
                    <td class="Rtd">
                        <asp:FileUpload ID="fuImg" runat="server" />&nbsp;&nbsp;
                            <span style='color:#F00;'>说明：图片建议尺寸：300×300,建议使用PNG格式</span>
                        <br />
                        <%=imgUrl %>
                        </td>
                </tr>
               <tr class="ButBox">
                  	<td colspan="2" style="height:30px; padding-top:4px; background-color:#f6f6f6;" align="center">
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/skin/SubmitA1.png" runat="server"
                            CssClass="save" OnClick="btnSave_Click" OnClientClick="return checkForm()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/skin/ReturnA1.png" runat="server"
                            CssClass="save" OnClientClick="layer_close_refresh()" />

                    </td>
                </tr>
            </table>
            </div>
        </div>


    <div id="right2" style="width: 800px; margin: 0 auto; font-family: 微软雅黑;display:none">
        <div class="box" style="font-size: 18px; font-family: 微软雅黑; text-align: center; margin: 30px auto 20px;
            padding-bottom: 20px; border-bottom: 2px solid #ccc">
            <div class="box_top">
                <b class="box_b1"></b><b class="box_b2"></b><span>
                    <%=_pageTitle %></span>
            </div>
        </div>
        <div class="applica_di">
            <table cellpadding="0" cellspacing="0" class="table_2" id="table_datalist" style="width: 100%;
                line-height: 30px;">
<%--                <tr>
                    <td align="right">
                        菜单名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtMenuName" runat="server" Width="500px" CssClass="input_add"></asp:TextBox><span
                            style="color: Red; font-size: 16px; line-height: 30px; padding-left: 10px">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="字段不能为空"
                            ControlToValidate="txtMenuName" />
                    </td>
                </tr>--%>

<%--                <tr>
                    <td align="right">
                        上级菜单：
                    </td>
                    <td align="left">
                    <uc2:cselect id="cSelect" runat="server"></uc2:cselect>
                    </td>
                </tr>--%>

<%--                <tr>
                    <td align="right">
                        排列序号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSort" runat="server" Width="80px" CssClass="input_add"></asp:TextBox><span
                            style="color: Red; font-size: 16px; line-height: 30px; padding-left: 10px">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="字段不能为空"
                            ControlToValidate="txtSort"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSort"
                            ErrorMessage="* 请输入正整数" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>--%>
<%--                <tr>
                    <td align="right" style="height: 15px">
                        可 见 性：
                    </td>
                    <td align="left" style="height: 15px">
                        <asp:CheckBox ID="cbIsVisible" runat="server" Text="在菜单中显示" Checked="true" />
                    </td>
                </tr>--%>
<%--                <tr align="center">
                    <td colspan="2" style="padding-top: 30px;" align="center">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/skin/SubmitA1.png" runat="server"
                            CssClass="save" OnClick="btnSave_Click" OnClientClick="return checkForm()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/skin/ReturnA1.png" runat="server"
                            CssClass="save" OnClientClick="layer_close_refresh()" />
                    </td>
                </tr>--%>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
