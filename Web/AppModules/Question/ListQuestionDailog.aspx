<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListQuestionDailog.aspx.cs" Inherits="Admin_AppModules_Question_ListQuestionDailog" %>

<%@ Register Src="~/Controls/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>案例管理</title>
    <link href="../../admin.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript" src="../../js/checkboxValue.js"></script>

    <script type="text/javascript" language="javascript" src="../../js/jquery-1.3.2.js"></script>

    <script type="text/javascript" language="javascript" src="../../js/jquery.mouseevent.js"></script>

    <script language="javascript" type="text/javascript">
        function buttonAction(s) {
            var ids = "";
            var count = 0;
            $("#table_datalist input[name='ids']").each(function () {
                if ($(this).attr("checked") == true) {
                    count++;
                    ids += $(this).val() + ",";
                }
            })
            if (s == "add") {
                location.href = "AddQuestions.aspx";
                return false;
            }
            if (s == "edit") {
                if (count == 0) {
                    alert("请选择要操作的项");
                    return false;
                }
                if (count > 1) {
                    alert("只能选择一项操作");
                    return false;
                }
                if (count == 1) {
                    location.href = "AddQuestions.aspx?Nid=" + ids.split(",")[0] + "&PageIndex="+<%=this.PageIndex %>;
                }
                return false;
            }
              if(s=="delete")
            {
              if (count == 0) {
                alert("请选择要操作的项");
               return false;}
              if(confirm("确认删除"))
             { var value=Admin_AppModules_Question_ListQuestion.Delete(ids).value;
              alert("成功删除"+value+"条数据");}
              location.href=location.href;
              return false;
            }
            if(s=="sh")
            {
               var url = "QuestionAddBat.aspx?r="+Math.random(9999);
               var returnValue= window.showModalDialog(url,"KHLX","dialogWidth=620px;dialogHeight=300px;scroll:1;status:0;help:0;resizable:1;");
               return false;
            } 
             if(s=="nsh")
            {
               if (count == 0) {
                alert("请选择要操作的项");
               return false;}
               window.showModalDialog("AddToChapter.aspx?questionSys="+ids+"&r="+Math.random(9999),window,"DialogHeight:300px,DialogWeight:100px;");
            }   
            return true;
        }
        
        function to_select(s){
            window.returnValue=s;
            window.close();
        }
        
        window.name="kxm371";
    </script>
<base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server" name="kxm371" target="kxm371">
        <div>
            <div class="body">
                <div class="main">
                    <div class="Wrapper" style="margin-left: 0px;">
                        <div class="content">
                            <!--begin-->
                            <div class="query">
                                <!--按钮组-->
                                <div style="display: none">
                                   
                                    <div style="line-height: 5px; height: 5px">
                                        <!--blank-->
                                    </div>
                                </div>
                                <div class="wrapper">
                                    <div>
                                        <label>
                                            标题：<asp:TextBox ID="txtTitle" runat="server" Width="200px" CssClass="input_add"></asp:TextBox>
                                        </label>
                                        <label>
                                            分类：
                                            <asp:DropDownList ID="ddlPCategory" runat="server" Width="260">
                                            </asp:DropDownList>
                                        </label>
                                        <asp:Button ID="Button2" runat="server" Text="搜索" OnClick="Button2_Click" />
                                    </div>
                                </div>
                            </div>
                            <!--搜索面板-->
                            <div class="List">
                                <table cellpadding="0" cellspacing="0" border="0" id="table_datalist">
                                    <thead>
                                        <tr>
                                            <th width="75%">
                                                标题
                                            </th>
                                            <th>
                                                分类
                                            </th>
                                            <th width="8%">
                                                操作</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="gvArticleList" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text_left" style="text-align: left;">
                                                        <%# Eval("questionTitle").ToString()%>
                                                    </td>
                                                    <td>
                                                        <%#categoryname(Eval("categoryflag").ToString(), Eval("ifCourseQuestion").ToString())%>
                                                    </td>
                                                    <td>
                                                        <input type="button" value="选择" onclick="to_select('<%#Eval("questionTitle").ToString()%>@$@<%#Eval("sysnumber").ToString() %>@$@<%#Eval("categoryflag").ToString() %>')" /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <!--分页-->
                            <div>
                                <p>
                                    <uc1:Pagination ID="pageBar" runat="server" PageSize="10" OnPageIndexChanged="pageBar_PageIndexChanged" />
                                </p>
                            </div>
                            <div class="query">
                                <div class="wrapper">
                                    <div>
                                        <label>
                                            <b>新增案例：</b><br />
                                            <textarea id="al" runat="server" style="width: 754px; height: 49px;"></textarea>
                                        </label>
                                        <br />
                                        <label>
                                            分类：
                                            <asp:DropDownList ID="ddlPCategory2" runat="server" Width="260">
                                            </asp:DropDownList>
                                        </label>
                                        <br />
                                        <label>
                                            层级：
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                                Width="350" Style="margin: 3px;">
                                            </asp:CheckBoxList>
                                        </label>
                                        <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Button1_Click" Width="53px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
