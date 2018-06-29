<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCategory.aspx.cs" Inherits="Sysadmin_Article_AddCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加文章类别</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/effect.js" type="text/javascript"></script>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/calendar.js" type="text/javascript"></script>
    <script src="../../../js/Validator.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function Check()
        {
            var txtTitle=document.getElementById("<%=txtTitle.ClientID %>");
            var txtSort=document.getElementById("<%=txtSort.ClientID %>");
            if(txtTitle.value==""){
                alert("类别名称不能为空！");
                txtTitle.focus();
                return false;
            }
            if(txtSort.value=="")
            {
                alert('排序不能为空！');
                txtSort.focus();
                return false;
            }
            else if(isNaN(txtSort.value))
            {
                alert('输入有误！只能是数字！');
                txtSort.focus();
                return false;
            }
        }

        $(document).ready(function () {
            var css = parent.document.all.cssfile.href;
            document.all.ChildcssFile.href = css;
            var js = parent.document.all.jsCss.src;
            document.all.ChildJsCss.src = js;
        });

        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.grid_search(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post">
    <div id="right" style="width: 100%; margin: 0 auto; font-family: 微软雅黑;">
        <div class="box" style="font-size: 18px; font-family: 微软雅黑; text-align: center; 
            padding-bottom: 20px;">
        </div>
        <div class="applica_di">
            <table cellpadding="0" cellspacing="1" class="ViewBox" id="table_datalist" style="width: 100%;
                line-height: 30px;">
                <tr>
                    <td class="Ltd">
                        <span class="red">*</span>类别名称：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtTitle" runat="server" Width="200px" CssClass="input_add"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        上级菜单：
                    </td>
                    <td class="Rtd">
                        <asp:DropDownList ID="ddlParentCategory" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span class="red">*</span>排序：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtSort" runat="server" Width="200px" CssClass="input_add"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td class="Ltd">
                        添加时间：
                    </td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtAddDate" runat="server" Width="100px" onFocus="WdatePicker()" ReadOnly="true"
                            CssClass="input_add"></asp:TextBox>&nbsp; <span class="red">注：发布时间请不要超出当前时间。</span>
                    </td>
                </tr>
                <tr class="ButBox">
                    <td colspan="2" style="height: 30px; padding-top: 4px; background-color: #f6f6f6;">
                        <asp:Button runat="server" ID="btnSave" CssClass="Submit" ToolTip="确定" Text="确定"
                                BorderWidth="0" Width="135px" Height="32px" OnClick="btnSave_Click" OnClientClick="return Check()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="Button1" CssClass="Return" ToolTip="返回" OnClientClick="layer_close_refresh()" Text="返回" BorderWidth="0" Width="135px" Height="32px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
