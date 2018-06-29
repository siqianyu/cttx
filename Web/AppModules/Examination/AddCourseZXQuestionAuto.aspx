<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCourseZXQuestionAuto.aspx.cs" Inherits="Admin_AppModules_Question_AddCourseZXQuestionAuto" %>


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

    <script language="javascript" type="text/javascript">
        function Check() {
            if ($("#txtTitle").val() == "") { alert("标题不能为空"); return false; }
            if ($("#txtReleaseDate").val() == "") { alert("时间不能为空"); return false; }
            if($("#txtStarTime").val()==""){alert("考试开始时间不能为空");return false;}
              if($("#txtEndTime").val()==""){alert("考试结束时间不能为空");return false;}
        }
        function deleteQuestion(question,groupid) {
            if(confirm('确定要删除吗?')==false){return false;}
            var value = Admin_AppModules_Question_AddCourseZXQuestion.DeleteQues('<%=this._Nid%>', question,groupid).value;
            if (value != null && value != "") {
                alert("操作成功");
                window.location.href = window.location.href;
            }
        }
        function showQuestion(sysnumber){
            var url="AddQuestions.aspx?Nid="+sysnumber+"&isshow=1&r="+Math.random();
            //alert(url);
            var returnValue= window.showModalDialog(url,"q123","dialogWidth=800px;dialogHeight=620px;scroll:1;status:0;help:0;resizable:1;");
            return false;
        }
        
        function deleteItems(id,cid){
             if(confirm('确定要删除吗?')==false){return false;}
            var value = Admin_AppModules_Question_AddCourseZXQuestion.DeleteItems(id, cid).value;
            if (value != null && value != "") {
                alert("操作成功");
                window.location.href = window.location.href;
            }
        }
        function addQuestion(sysnumber){
            var url="ListQuestionItemDialog.aspx?sysnumber="+sysnumber+"&personFlag=<%=this.personFlag %>&r="+Math.random();
            //alert(url);
            var returnValue= window.showModalDialog(url,"KHLX","dialogWidth=800px;dialogHeight=620px;scroll:1;status:0;help:0;resizable:1;");
            if(returnValue)
            {
                //alert(returnValue);
                if(confirm('确定要新增吗?')==false){return false;}
                var value = Admin_AppModules_Question_AddCourseZXQuestion.InsertQues(returnValue,sysnumber).value;
                if (value != null && value != "") {
                    alert("操作成功");
                    window.location.href = window.location.href;
                }
            }
            return false;
        }        
        function add2(){
            var url="AddQuestionRnd.aspx?zjmethod=<%=this.zjmethod %>&sjid=<%=this._Nid %>&r="+Math.random();
            //alert(url);
            var returnValue= window.showModalDialog(url,"KHLX","dialogWidth=900px;dialogHeight=700px;scroll:1;status:0;help:0;resizable:1;");
            location.href=location.href;
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
                                                    所属任务：
                                                </th>
                                                <td  class="Rtd">
                                                    <asp:Label ID="txtGoods" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                 <th   class="Ltd">
                                                    标题：
                                                </th>
                                                <td  class="Rtd">
                                                    <asp:Label ID="txtTitle" runat="server" />
                                                    <asp:HiddenField runat="server" ID="hidQuestions" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <th   class="Ltd">
                                                    题目数量：
                                                </th>
                                                <td  class="Rtd">
                                                    <asp:Label ID="txtQuestionTotalNum" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                 <th   class="Ltd">
                                                    规则配置：
                                                </th>
                                                <td  class="Rtd">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                来源
                                                            </td>
                                                            <td>
                                                                判断题
                                                            </td>
                                                            <td>
                                                                单选题
                                                            </td>
                                                            <td>
                                                                多选题
                                                            </td>
                                                            <td>
                                                                不定项选择题
                                                            </td>
                                                            <td>
                                                                简答题
                                                            </td> 
                                                            <td style="display: none">
                                                                计算分析题
                                                            </td>
                                                        </tr>
                                                        <tr id="tr1">
                                                            <td>
                                                                《所属课程》：
                                                                <asp:TextBox Width="60" runat="server" ID="ly1" Visible="false">Current</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="sf1">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a11">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a21">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a3a41">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="x1">5</asp:TextBox>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:TextBox Width="60" runat="server" ID="al1">0</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr2" style="display: none">
                                                            <td>
                                                                大专业
                                                                <asp:TextBox Width="60" runat="server" ID="ly2" Visible="false">Parent</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="sf2"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a12"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a22"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a3a42"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="x2"></asp:TextBox>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:TextBox Width="60" runat="server" ID="al2"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr3">
                                                            <td>
                                                                《所属任务》：
                                                                <asp:TextBox Width="60" runat="server" ID="ly3" Visible="false">Course</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="sf3">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a13">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a23">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a3a43">5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="x3">5</asp:TextBox>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:TextBox Width="60" runat="server" ID="al3">0</asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr4" style="display: none">
                                                            <td>
                                                                公共
                                                                <asp:TextBox Width="60" runat="server" ID="ly4" Visible="false">Common</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="sf4"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a14"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a24"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="a3a44"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Width="60" runat="server" ID="x4"></asp:TextBox>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:TextBox Width="60" runat="server" ID="al4"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="7">
                                                                <asp:Button ID="btnSave" runat="server" Text="保存配置" OnClick="btnSave_Click" 
                                                                    Height="30px" Width="88px" />

                                                                     <asp:Button ID="Button1" runat="server" Text="返 回"  
                                                                    Height="30px" Width="88px" onclick="Button1_Click1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                           <tr>
                                                <th   class="Ltd">
                                                    预览试题：
                                                </th>
                                                <td class="Rtd">
                                                    <a href="YL.aspx?Nid=<%=this._Nid %>" target="_blank">【点击预览】</a>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <%=this.CreateHtml(this._Nid)%>
                    
                </div>
              
            </div>
          
        </div>
    </form>
</body>
</html>
