<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCoupon.aspx.cs" Inherits="AppModules_Sysadmin_Base_AddCoupon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">



        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.freshCurrentPage(); //执行当前列表页的刷新事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
        //js获取地址栏中的参数
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }   
    

    </script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div id="right">
            <div class="applica_title">
            <br />
                <h4>
                    <%=_pageTitle %>
                </h4>
            </div>
            <div class="applica_di">
                <table cellpadding="0" cellspacing="1" class="ViewBox">

                <tr style="display:none">
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>优惠卷编号：</td>
                    <td class="Rtd">
                        <%--<asp:TextBox ID="txtCouponId" ReadOnly="true" runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" ></asp:TextBox>--%>
                        <asp:Label ID="lbCouponId" runat="server" Text=""></asp:Label>
                        </td>
                </tr>

                 <tr>
                    <td class="Ltd">
                        <span  style="color: #ff0000">*</span>优惠卷类型：</td>
                    <td class="Rtd">
                        <asp:DropDownList ClientIDMode="Static" ID="ddlCouponType" runat="server">
                        <asp:ListItem Value='抵用券'>抵用券</asp:ListItem>
                       <%-- <asp:ListItem Value='ZK'>折扣卷</asp:ListItem>--%>
                        <%--<asp:ListItem Value='DJ'>代金卷</asp:ListItem>--%>
                        </asp:DropDownList>
                        </td>
                </tr>

                <tr class='yh'  <%=isShow %> >
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>使用对象：</td>
                    <td class="Rtd">
                        <asp:DropDownList ClientIDMode="Static" runat="server" ID='ddlMember'>
                        <asp:ListItem  Value='no'>无限制</asp:ListItem>
                        <asp:ListItem Value='new'>新注册用户</asp:ListItem>
                        <asp:ListItem Value='all'>全网用户</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                </tr>

                <tr class='yh' <%=isShow %> >
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>群发短信：</td>
                    <td class="Rtd">
                        <asp:CheckBox ID="cbQF" runat="server" />
                        </td>
                </tr>

                <tr class='yh' <%=isShow %> >
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>使用用户：</td>
                    <td class="Rtd">
                        <asp:Label ID="lbMember" runat="server" Text="lbMember">未提交</asp:Label> 
                        <span style='color:Red'>*提交该类型优惠卷之后，将针对大量用户生成大量优惠卷，每张对应单个用户</span>
                        </td>
                </tr>


                 <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>优惠卷面值：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtValue" runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" >50</asp:TextBox>（元）
                        </td>
                </tr>
                <tr style="display:none">
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>优惠卷有效期：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtDay" runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" >0</asp:TextBox>（天）
                        </td>
                </tr>
                
                                 <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>开始时间：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtStartTime" ClientIDMode="Static" ReadOnly="true" runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" ></asp:TextBox>
                        <asp:HiddenField ID="hfStart" runat="server" />
                        <span style='color:Red'>生效时间(为生效前无法使用)</span>
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>结束时间：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtEndTime" ClientIDMode="Static" ReadOnly="true" runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" ></asp:TextBox>
                        <asp:HiddenField ID="hfEnd" runat="server" />
                        <span style='color:Red'>失效时间(过期无法使用)</span>
                        </td>
                </tr>

                
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>使用门槛：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtMinPrice" runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" >0</asp:TextBox>订单总额不低于（元）
                        </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>用途说明：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtContext" runat="server" Width="200px" CssClass="Enter" style="color:Gray;" MaxLength="100" >可用于购买课程</asp:TextBox>
                        
                        </td>
                </tr>


                <tr style="display:none">
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>二维码：</td>
                    <td class="Rtd">
<%--                        <asp:LinkButton ClientIDMode="Static" ID="btQRCode" runat="server" onclick="btQRCode_Click">点击生成>></asp:LinkButton>(选择分类后必须生成一次，不同分类使用二维码不同)<br />
--%>                        
                        提交后自动生成<br />
                        <asp:Image ID="QRCodeUrl" runat="server" Visible="false" />                        
                    </td>
                </tr>

                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>是否启用：</td>
                    <td class="Rtd">
                        <asp:CheckBox ID="cbEffect" runat="server" />                    
                    </td>
                </tr>
                <tr>
                    <td class="Ltd">
                        <span  style="color: #ff0000">*</span>前台领取位置：</td>
                    <td class="Rtd">
                        <asp:DropDownList ID="ddlGetPlaceInfo" runat="server">
                        <asp:ListItem Value='首页一键领取'>首页一键领取</asp:ListItem>
                        <asp:ListItem Value='微信分享赠送'>微信分享赠送</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                </tr>

                <tr style="display:none">
                    <td class="Ltd">
                        <span style="color: #ff0000">*</span>使用情况：</td>
                    <td class="Rtd">
                        <asp:Label ID="lbUse" runat="server" Text="未使用"></asp:Label>                 
                    </td>
                </tr>

                <tr>
                    <td class="Ltd">
                        <span style="color: #ff0000"></span>备注：</td>
                    <td class="Rtd">
                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="64px" 
                            Width="513px"></asp:TextBox>               
                    </td>
                </tr>
                
               <tr class="ButBox">
                  	<td colspan="2" style="height:30px; padding-top:4px; background-color:#f6f6f6;" align="center">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="Submit" ToolTip="确定"
                             Text="确定" BorderWidth="0" Width="135px" Height="32px" 
                             OnClientClick="return check()" onclick="btnSubmit_Click" />
                        <asp:Button runat="server" ID="Button1" CssClass="Return" ToolTip="返回"
                            OnClientClick="layer_close_refresh()" Text="返回" BorderWidth="0" Width="135px" Height="32px" />
                    </td>
                </tr>
            </table>
            </div>
        </div>

        <script type="text/javascript">

            function check() {
                $("#hfStart").val($("#txtStartTime").val());
                $("#hfEnd").val($("#txtEndTime").val());
            }

            $("#ddlCouponType").change(function () {
                var m = $(this).val();
                if (m == "DJ") {
                    $(".yh").show();
                } else {
                    $(".yh").hide();
                }
            });
        </script>

    </form>
</body>
</html>
