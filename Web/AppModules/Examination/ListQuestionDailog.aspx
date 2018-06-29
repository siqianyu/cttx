<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListQuestionDailog.aspx.cs" Inherits="Admin_AppModules_Question_ListQuestionDailog"  ValidateRequest="false" %>

<%@ Register Src="~/Controls/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>案例管理</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
      //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            //parent.freshCurrentPage(); //执行当前列表页的刷新事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }

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
            parent.open_al_end(s);
            layer_close_refresh();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="PosiBar">
        <p>
            案例列表</p>
    </div>

    <div class="SosoBar Left">
        <table>
            <tr>
                <td style="width: 80%">
                    <div class="divSearch">
                        <div class="spanSearch">
                            标题：</div>
                        <asp:TextBox ID="txtTitle" runat="server" Width="200px" CssClass="input_add"></asp:TextBox>
                    </div>
                </td>
              
                    <td style="width: 20%">
                        <div class="divSearchUC">
                            <asp:Button ID="Button2" runat="server" Text="搜索" OnClick="Button2_Click" /></div>
                    </td>
            </tr>
        </table>
    </div>       
                     
    <div class="TableBox Left">
        <!--搜索面板-->
        <div class="List">
            <table cellpadding="3" cellspacing="3" border="1" id="table_datalist">
                <tbody>
                    <asp:Repeater ID="gvArticleList" runat="server">
                        <ItemTemplate>
                            <tr>
                            <td align="center" style="margin:5px;">
                                 <input type="button" value="选择" onclick="to_select('@$@<%#Eval("sysnumber").ToString() %>@$@<%#Eval("categoryflag").ToString() %>')" />
                                 
                                 </td>
                                <td class="text_left" style="text-align: left;">
                                    <%# Eval("questionTitle").ToString()%>
                                </td>
                                
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
                    <asp:Button ID="Button1" runat="server" Text="保存案例" OnClick="Button1_Click" 
                        Width="70px" Height="30px" />
                </div>
            </div>
        </div>
    </div>
                  
    </form>
</body>
</html>
    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
    <script language="javascript">
        $(function () {
            var editor = CKEDITOR.replace("al", { skin: "kama", width: document.body.scrollWidth - 300, height: 180 });
        });
    </script>