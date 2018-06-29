<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuestions.aspx.cs" Inherits="Admin_AppModules_Question_AddQuestions" ValidateRequest="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会计学习</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script language="javascript">
        function checkForm(){
            if($("#txtTitle").val() == ""){
                alert('请输入题目');
                return false;
            }
            return true;
        }
       
       function selectType(){
           if ($("#ddlType").val() == "不定项选择题") {
                $("#tr_category").hide();
                $("#tr_level").hide();
            } else if ($("#ddlType").val() == "判断题") {
                $("#txt1").val("对");
                $("#txt2").val("错");
            } else {
                
            }
       }

        //案例
        function open_al() {
            var url = "ListQuestionDailog.aspx?courseId=<%=this.courseId %>&questionType=" + escape($("#ddlType").val()) + "&r=" + Math.random();
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['案例', true],
                maxmin: true,
                iframe: { src: url },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }

        function open_al_end(s) {
            var arr = s.split('@$@');
            if (arr && arr.length == 3) {
                var title = $.ajax({url: "QuestionList.ashx?flag=get_al&id=" + arr[1], async: false}).responseText;
                $("#div_al").html(title);
                $("#mainSysnumber").val(arr[1]);
                $("#ddlPCategory").val(arr[2]);
                $("#div_al").css("height", "100px");
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
    <asp:HiddenField ID="hidCategoryId" runat="server"/>
    <div class="Mebmid">
        <!--main_conten_start-->
        <div class="MebmidR">
            <div id="Addcom_Bas">
                <div class="AddcomT1">
                </div>
                <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist ViewBox">
                    <tr>
                        <th   class="Ltd">
                            题目类型：
                        </th>
                        <td  class="Rtd" >
                            <asp:DropDownList runat="server" ID="ddlType" onchange="selectType()">
                                <asp:ListItem Enabled="true" Text="单选题" Value="单选题"></asp:ListItem>
                                <asp:ListItem Text="多选题" Value="多选题"></asp:ListItem>
                                <asp:ListItem Text="不定项选择题" Value="不定项选择题"></asp:ListItem>
                                <asp:ListItem Text="判断题" Value="判断题"></asp:ListItem>
                                <asp:ListItem Text="简答题" Value="简答题"></asp:ListItem>
                                <asp:ListItem Text="计算分析题" Value="计算分析题"></asp:ListItem>
                                <asp:ListItem Text="综合题" Value="综合题"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="gray">&nbsp;</span>
                        </td>
                        <th   class="Ltd">
                            难度系数：
                        </th>
                        <td  class="Rtd" >
                            <asp:DropDownList runat="server" ID="ddlLevelPoint">
                                <asp:ListItem Enabled="true" Text="0.5" Value="0.5"></asp:ListItem>
                                <asp:ListItem Text="0.0" Value="0.0"></asp:ListItem>
                                <asp:ListItem Text="1.0" Value="1.0"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="gray">&nbsp;</span>
                        </td>
                    </tr>
                    <tr id="tr_al">
                        <th  class="Ltd">
                            所属案例：<br /><input id="btn_al" runat="server" type="button" value="选择案例" onclick="open_al()" />
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <div id="div_al" runat="server" style="height:20px; overflow:auto">
                            </div>
                            
                            <asp:HiddenField runat="server" ID="mainSysnumber" />
                        </td>
                    </tr>
                    <tr id="tr_category" style="display:none">
                        <th  class="Ltd">
                            分类：
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <asp:Literal runat="server" ID="ddlPCategory_Show"></asp:Literal>&nbsp;&nbsp;<input
                                type="button" value="设置" onclick="$('#div_ddlPCategory').show()" />
                            <div id="div_ddlPCategory" style="display: none">
                                <asp:ListBox ID="ddlPCategory" runat="server" Width="360" Height="220" SelectionMode="Multiple">
                                </asp:ListBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="tr_level" style="display:none">
                        <th  class="Ltd">
                            层级：
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                Width="350" Style="margin: 3px;">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <th  class="Ltd">
                            题目：
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <asp:TextBox ID="txtTitle" runat="server" Width="600px" CssClass="input_add" Height="120px"
                                TextMode="MultiLine" Columns="3" />
                            <asp:HiddenField runat="server" ID="hidAnswer" />
                            <!--案例题子题_start-->
                            <asp:Repeater runat="server" ID="rptSubQuestions">
                                <ItemTemplate>
                                    <div>
                                        【案例题】<a href="AddQuestions.aspx?courseId=<%=this.courseId %>&nid=<%#Eval("sysnumber")%>"><%#Eval("questionTitle")%></a></div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <!--案例题子题_end-->
                        </td>
                    </tr>
                    
                    <tr id="tr_options">
                        <th  class="Ltd">
                            题目选项：
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <table runat="server" id="tb" style="margin-left: 0px;">
                                <tr>
                                    <td>
                                        A、<input type="checkbox" runat="server" id="ck1" /><asp:TextBox ID="txt1" runat="server"
                                            Width="450px" Height="30px" CssClass="input_add"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        B、<input type="checkbox" runat="server" id="ck2" /><asp:TextBox ID="txt2" runat="server"
                                            Width="450px" Height="30px" CssClass="input_add"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        C、<input type="checkbox" runat="server" id="ck3" /><asp:TextBox ID="txt3" runat="server"
                                            Width="450px" Height="30px" CssClass="input_add"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        D、<input type="checkbox" runat="server" id="ck4" /><asp:TextBox ID="txt4" runat="server"
                                            Width="450px" Height="30px" CssClass="input_add"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        E、<input type="checkbox" runat="server" id="ck5" /><asp:TextBox ID="txt5" runat="server"
                                            Width="450px" Height="30px" CssClass="input_add"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        解析：
                                        <asp:TextBox ID="txtDescription" runat="server" Width="550px" CssClass="input_add"
                                             TextMode="MultiLine" Height="80"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="tr_sh">
                        <th  class="Ltd">
                            排序：
                        </th>
                        <td  class="Rtd" >
                            <asp:TextBox ID="hidOrderBy" runat="server" CssClass="input_add" Text="0" />
                        </td>
                   
                        <th  class="Ltd">
                            审核状态：
                        </th>
                        <td  class="Rtd" >
                            <asp:DropDownList runat="server" ID="ddlSH">
                                <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="已审核" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <th  class="Ltd">
                            审核备注：
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <asp:TextBox TextMode="MultiLine" ID="txtSHRemarks" runat="server" CssClass="input_add"
                                Height="46px" Width="401px" />
                        </td>
                    </tr>
                    <tr style="display:none">
                        <th  class="Ltd">
                            审核人：
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <asp:Label runat="server" ID="txtSHPerson"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <th  class="Ltd">
                            审核时间：
                        </th>
                        <td  colspan="3" class="Rtd" >
                            <asp:Label runat="server" ID="txtSHTime"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class='Rtd ButBox'  colspan="4">
                            <asp:Button Text="提交"  OnClientClick="return checkForm()" ID="btnSave" Style='border-width: 0px;
                                height: 32px; width: 135px;' CssClass="Submit" runat="server" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
