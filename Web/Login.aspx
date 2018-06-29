<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>才通天下微信公号平台</title>
    <link href="Style/Common.css" rel="stylesheet" />
    <link href="Style/Login.css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="js/jquery-1.9.0.min.js"></script>

    <script language="javascript">
        function checkForm(){
            if($("#txtUsername").val() == ""){alert('请输入用户名!');return false;}
            if($("#txtPassword").val() == ""){alert('请输入密码!');return false;}
            //if($("#txtYZM").val() == ""){alert('请输入验证码!');return false;}
            return true;
        }
        
        $(document).ready(
            function(){
                $("#txtUsername").focus();
            }
        );
    </script>

</head>
<body bgcolor="#F3FDFF">
    <form id="form1" runat="server">
         <div class="LoginBox Left">
          <p><span>用户名：</span><input runat="server" id="txtUsername" class="Enter"/></p>
          <p><span>密&nbsp;&nbsp;&nbsp;&nbsp;码：</span><input runat="server" id="txtPassword" type="password" class="Enter"/></p>
          <p style="display:none"><span>验证码：</span><input  name="txtYZM" id="txtYZM" class="Enter" style="width:120px;"/>
          <img src="CreateImage.aspx" alt="看不清,换一张" id="Img_CreateImage" width="68" height="26"
                        align="absMiddle" border="0" class="CheckCode" onclick="changeCreateImage()" />

                    <script language="javascript">
                        function changeCreateImage() {
                            document.getElementById("Img_CreateImage").src = "";
                            document.getElementById("Img_CreateImage").src = "CreateImage.aspx?&r=" + Math.random(9999) + "";
                        }
                    </script>
          
          
          
          </p>
          <div class="Remember"><label><input type="checkbox"/><span>记住我的登录状态</span></label></div>
          <div class="LoginBar Left">
          <asp:LinkButton ID="btnLogin" CssClass="LoginBut" runat="server" 
                  onclick="btnLogin_Click" OnClientClick="return checkForm();"></asp:LinkButton>
          <a href="javascript:location.href='Login.aspx';void(0);" class="CancelBut"></a>
          </div>
        </div>
    </form>
</body>
</html>
