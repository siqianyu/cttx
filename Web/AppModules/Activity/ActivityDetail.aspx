<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityDetail.aspx.cs" Inherits="AppModules_Member_MemberDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Pragma" content="no-cache">
    <%
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    %>
    <title>编辑任务</title>

    <style type="text/css">
        #cke_1_bottom {
            display: none;
        }

        .cateSelect {
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
                <h4></h4>
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>活动标题：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 100px;" type="text" id="activity_title" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>活动介绍：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <textarea style="width: 400px;" rows="5" id="activity_desc" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>教练：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 100px;" type="text" id="manager_id" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>岗位分配：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 100px;" type="text" id="job_ids" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>总人数：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 100px;" type="text" id="activity_person" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>活动状态：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:RadioButtonList ID="activity_status" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Text="未开始" Value="0"></asp:ListItem>
                                <asp:ListItem Text="已开始" Value="1"></asp:ListItem>
                                <asp:ListItem Text="报名中" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已结束" Value="-1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>审核：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:RadioButtonList ID="if_approved" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Text="已审核" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>备注：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <input style="width: 200px;" type="text" id="remarks" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class='Rtd ButBox' colspan='4'>
                            <input type="hidden" id="activity_id" runat="server" />
                            <asp:Button Text="提交" OnClientClick='return checkForm()' ID="btnSave" Style='border-width: 0px; height: 32px; width: 135px;'
                                CssClass="Submit" runat="server" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">

    function checkForm() {
        var ids = ['activity_title', 'manager_id','activity_person'];
        for (var i = 0; i < ids.length; i++) {
            if (!$('#' + ids[i]).val()) {
                $('#' + ids[i]).focus();
                return false;
            }
        }
        return true;
    }
</script>
</html>
