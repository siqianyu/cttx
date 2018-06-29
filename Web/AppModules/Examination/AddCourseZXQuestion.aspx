<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCourseZXQuestion.aspx.cs" Inherits="Admin_AppModules_Question_AddCourseZXQuestion" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会计学习</title>
    <link href="../../admin.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../../js/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../../js/jquery-1.3.2.js"></script>

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
        <div class="B">
         
            <div class="body">
                <div class="main">
                    <div class="Wrapper">
                        <div class="sitemap">
                            当前位置: 考试管理 &gt; 组卷管理</div>
                        <!--c-->
                        <div class="content">
                            <!--begin-->
                            <div class="Detail">
                                <div id="right">
                                    <div class="applica_title" style="display: none">
                                        <h4>
                                            <%=_pageTitle %>
                                        </h4>
                                    </div>
                                    <div class="applica_di">
                                        <table cellpadding="0" cellspacing="0" class="table_1">
                                            <tr>
                                                <td class="td_left" style="text-align: right" width="120">
                                                    考试标题：
                                                </td>
                                                <td class="td_right">
                                                    <asp:Label ID="txtTitle" runat="server" />
                                                    <asp:HiddenField runat="server" ID="hidQuestions" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_left" style="text-align: right" width="120">
                                                    层级：
                                                </td>
                                                <td class="td_right">
                                                    <asp:Label ID="txtLevel" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_left" style="text-align: right" width="120">
                                                    题目数量：
                                                </td>
                                                <td class="td_right">
                                                    <asp:Label ID="txtQuestionTotalNum" runat="server" />
                                                </td>
                                            </tr>
                                           <tr>
                                                <td class="td_left" style="text-align: right" width="120">
                                                    组卷管理：
                                                </td>
                                                <td class="td_right">
                                                    <input type="button" value="抽取题目" onclick="add2()" />
                                                    <input type="button" value=" 返 回 " onclick="history.go(-1)" />
                                                    <a href="YL.aspx?Nid=<%=this._Nid %>" target="_blank">【打印试卷】</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                   
                                </div>
                                <%=this.CreateHtml(this._Nid)%>
                                
                            </div>
                        </div>
                        <!--//c-->
                    </div>
                </div>
               
            </div>
          
        </div>
    </form>
</body>
</html>
