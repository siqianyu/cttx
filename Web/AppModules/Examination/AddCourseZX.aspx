<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCourseZX.aspx.cs" Inherits="Admin_AppModules_Question_AddCourseZX" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <style>
        #rdLX
        {
            width: 200px;
            margin-left: 10px;
        }
        #rdZJFS
        {
            margin-left: 10px;
        }
    </style>
  
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
    <script language="javascript" type="text/javascript">
        function Check() {
            if ($("#txtTitle").val() == "") { alert("标题不能为空"); return false; }
            if ($("#txtReleaseDate").val() == "") { alert("时间不能为空"); return false; }
            if($("#txtStarTime").val()==""){alert("考试开始时间不能为空");return false;}
              if($("#txtEndTime").val()==""){alert("考试结束时间不能为空");return false;}
        }
        function deleteQuestion(question) {
            var value = Admin_AppModules_Question_AddCourseZX.DeleteQues('<%=this._Nid%>', question).value;
            if (value != null && value != "") {
                alert("操作成功");
                window.location.href = window.location.href;
            }
        }
        
        function add(){
            var url="AddQuestion.aspx?sjid=<%=this._Nid %>&r="+Math.random();
            //alert(url);
            var returnValue= window.showModalDialog(url,"KHLX","dialogWidth=800px;dialogHeight=620px;scroll:1;status:0;help:0;resizable:1;");
            return false;
        }        
        function add2(){
            var url="AddQuestionExcel.aspx?sjid=<%=this._Nid %>&r="+Math.random();
            //alert(url);
            var returnValue= window.showModalDialog(url,"KHLX","dialogWidth=800px;dialogHeight=620px;scroll:1;status:0;help:0;resizable:1;");
            return false;
        }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <input type="hidden" runat="server" id="hid_goodsId" />
         <div class="Mebmid">
        <!--main_conten_start-->
        <div class="MebmidR">
            <div id="Addcom_Bas">
                <div class="AddcomT1">
                </div>
                                        <table border="0" cellpadding="0" cellspacing="1" class="Addcomlist ViewBox">
                                            <tr>
                                                <th   class="Ltd">
                                                    组卷方式：
                                                </th>
                                                <td class="Rtd"  style="text-align: left">
                                                    <asp:RadioButtonList runat="server" ID="rdZJFS" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdZJFS_SelectedIndexChanged">
                                                    
                                                    <asp:ListItem Value="Auto" Selected="True">系统自动组卷（系统自动抽取题目）</asp:ListItem>
                                                    <asp:ListItem Value="Person">人工组卷（管理员手动抽取题目）</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <th   class="Ltd">
                                                    标题：
                                                </th>
                                                <td class="Rtd">
                                                    <asp:TextBox ID="txtTitle" runat="server" Width="390" CssClass="input_add" Height="15" />
                                                    <asp:HiddenField runat="server" ID="hidQuestions" />
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr runat="server" id="tr_level">
                                                <th   class="Ltd">
                                                    难度：
                                                </th>
                                                <td class="Rtd" style="text-align: left">
                                                    <asp:DropDownList runat="server" ID="ddlLevel">
                                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                        <asp:ListItem Selected="True" Text="0.5" Value="0.5"></asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                              <tr runat="server" id="tr1">
                                                <th   class="Ltd">
                                                    审核：
                                                </th>
                                                <td class="Rtd" style="text-align: left">
                                                   <asp:DropDownList runat="server" ID="ddlSH">
                                <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="已审核" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td class="td_left" style="text-align: right">
                                                    试卷类型：
                                                </td>
                                                <td class="td_right"  style="text-align: left">
                                                    <asp:RadioButtonList runat="server" ID="rdLX" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="3" Selected="True">正式考试</asp:ListItem>
                                                    <asp:ListItem Value="4">模拟练习</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                               <th   class="Ltd">
                                                    备注：
                                                </th>
                                                <td class="Rtd">
                                                    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Rows="5" Width="390"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td class="td_right" style="text-align: right">
                                                    添加时间：
                                                </td>
                                                <td class="td_right">
                                                    <asp:TextBox ID="txtReleaseDate" runat="server" Width="70px" onFocus="this.blur()"
                                                        CssClass="input_add"></asp:TextBox>&nbsp;
                                                    <img onclick="meizz_calendar(document.getElementById('txtReleaseDate'))" style="cursor: pointer;"
                                                        src="/Admin/skin/calendar.gif" />
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td class="td_right" style="text-align: right">
                                                    开始时间：
                                                </td>
                                                <td class="td_right">
                                                    <asp:TextBox ID="txtStarTime" runat="server" Width="70px"  CssClass="input_add"></asp:TextBox>&nbsp;
                                                    <img onclick="meizz_calendar(document.getElementById('txtStarTime'))" style="cursor: pointer;"
                                                        src="/Admin/skin/calendar.gif" />
                                                    <asp:DropDownList ID="ddlStarH" runat="server">
                                                        <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                        <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                        <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                        <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    </asp:DropDownList>时
                                                    <asp:DropDownList ID="ddlStarM" runat="server">
                                                        <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                        <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                    </asp:DropDownList>分
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td class="td_right" style="text-align: right">
                                                    结束时间：
                                                </td>
                                                <td class="td_right">
                                                    <asp:TextBox ID="txtEndTime" runat="server" Width="70px"  CssClass="input_add"></asp:TextBox>&nbsp;
                                                    <img onclick="meizz_calendar(document.getElementById('txtEndTime'))" style="cursor: pointer;"
                                                        src="/Admin/skin/calendar.gif" />
                                                        <asp:DropDownList ID="ddlEndH" runat="server">
                                                        <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                        <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                        <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                        <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    </asp:DropDownList>时
                                                    <asp:DropDownList ID="ddlEndM" runat="server">
                                                        <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                        <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                    </asp:DropDownList>分
                                                </td>
                                            </tr>
                                            <tr>
                        <td class='Rtd ButBox' colspan='4'>
                            <asp:Button Text="提交"  ID="btnSave" Style='border-width: 0px;
                                height: 32px; width: 135px;' CssClass="Submit" runat="server" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                                        </table>
                                 
                                
                                <div style="height: 500px; width: 98%; overflow: scroll;" id="div_questions" runat="server" visible="false">
                                    <div style="text-align: right">
                                        <a href="javascript:add();void(0);" style="font-size: 14px; color: Blue">[新增题目]</a>、
                                        <a href="javascript:add2();void(0);" style="font-size: 14px; color: Blue">[批量导入]</a>&nbsp;&nbsp;
                                    </div>
                                    <table cellpadding="0" cellspacing="0" class="table_1" style="width: 96%;">
                                        <th style="width: 80%">
                                            题目
                                        </th>
                                        <th style="width: 10%">
                                            类型
                                        </th>
                                        <th>
                                            操作
                                        </th>
                                        <asp:Repeater runat="server" ID="rpt">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%#Eval("questionTitle")%>
                                                    </td>
                                                    <td>
                                                        <%#GetQuesType(Eval("questionType").ToString())%>
                                                    </td>
                                                    <td>
                                                        <a href='javascript:deleteQuestion("<%#Eval("sysnumber") %>");void(0)'>[删除]</a>
                                                        <a href='AddQuestion.aspx?sjid=<%=this._Nid %>&Nid=<%#Eval("sysnumber") %>'>[修改]</a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
