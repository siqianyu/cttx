<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberDetail.aspx.cs" Inherits="AppModules_Member_MemberDetail" %>

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
                            <span style="color: #ff0000"></span>会员编号：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbMemberId" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>会员名：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbMemberName" runat="server"></asp:Label>&nbsp;&nbsp;
                            <asp:Button ID="btnResetPwd" runat="server" Text="重置登录密码" 
                                onclick="btnResetPwd_Click" />

                                <asp:Button ID="btnResetPwd2" runat="server" Text="重置交易密码" onclick="btnResetPwd2_Click" 
                                 />
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>账户余额：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbMoney" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            &nbsp;<input type="button" value="查看明细" onclick="if(document.getElementById('div_money').style.display=='none'){$('#div_money').show();}else{$('#div_money').hide();}" />
                            <div id="div_money" style="display:none; overflow:scroll; height:300px;" runat="server"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>头像：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbHeadImg" runat="server"></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>真实姓名：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbTrueName" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>身份证号：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbPersonIDCode" runat="server"></asp:Label>
                            <asp:Literal ID="lbPersonIDCodePass" runat="server"></asp:Literal>&nbsp;
                            <asp:Button ID="btnCheck" runat="server" Text="审核通过" onclick="btnCheck_Click" />&nbsp;
                            <asp:Button ID="btnCheck2" runat="server" Text="取消通过" 
                                onclick="btnCheck2_Click" />
                        </td>
                    </tr>

                     <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>身份证照片：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Literal ID="ltPersonIDCodePhoto" runat="server"></asp:Literal>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>手机号码：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbTel" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>年龄：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbAge" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr style="display:none">
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>电子邮件：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbEmail" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr style="display:none">
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>QQ：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbQQ" runat="server"></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>性别：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbSex" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>生日：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbBirthDay" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>地址：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbAddress" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>特长：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbSpecialty" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>技能：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbSkill" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>自我介绍：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbSelfIntroduction" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>工程照片：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Literal ID="ltPics" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>注册时间：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbRegisterTime" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr style="display:none">
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>最后登陆：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbLastLoginTime" runat="server"></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td class="Ltd">
                            <span style="color: #ff0000"></span>启用状态：
                        </td>
                        <td class="Rtd" colspan='3'>
                            <asp:Label ID="lbIsUse" runat="server"></asp:Label>
                        </td>
                    </tr>

            </table>


            </div>
        </div>

        </form>
        </body>
</html>
