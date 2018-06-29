<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionAddBat.aspx.cs" Inherits="MemberCenter_QuestionAddBat" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Excel导入</title>
   <script language="javascript">
        function checkForm(){
          return true;
        }
   </script>
</head>
 
<body>
<form runat="server" style="margin:8px;">

        <table cellpadding="2" cellspacing="2" style="font-size:12px;">
            
            <tr>
            	<td class="bg" style="width: 130px">Excel导入文件</td>
            	<td width="85%" class="td_l"><asp:FileUpload ID="FileUpload1" runat="server" Width="320px"  />
            	<span style="color:#666666">(注意导入文件后缀名为.xls)</span>
            	</td>
            </tr>
            <tr>
            	<td class="bg" style="width: 130px">所属课件编号</td>
            	<td width="85%" class="td_l"><input type="text" id="txtCourseId" runat="server" />
            	<span style="color:#666666">(非必填，课后练习导入时需填写)</span>
            	</td>
            </tr>
            <tr>
            	<td class="bg" style="width: 130px">审核</td>
            	<td width="85%" class="td_l"><asp:DropDownList runat="server" ID="ddlSH">
                                <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="已审核" Value="1"></asp:ListItem>
                            </asp:DropDownList>
            	<span style="color:#666666">(默认审核)</span>
            	</td>
            </tr>
            
            
            <tr>
            	<td colspan="2" align="center"><asp:Button ID="btnAdd" Text=" 导 入 " runat="server" CssClass="button1"  OnClientClick="return checkForm();" OnClick="btnAdd_Click"/></td>
            </tr>
        </table>
        <div style="color:Red; font-size:12px;">
        1、导入excel格式为.xls<br />
        2、Excel底部的页签名称必须为Sheet1<br />
        3、模块参考《<a href="mb.xls">导入模板.xls</a>》
        </div>
<!--尾部-->
</form>
</body>
</html>

