<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frame.aspx.cs" Inherits="Controls_frame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script language="javascript" type="text/javascript">
        function lo() {
            document.getElementById("fram").style.width = window.dialogWidth;
            document.getElementById("fram").style.height = document.getElementById("fram").contentWindow.document.documentElement.scrollHeight > window.dialogHeight ? document.getElementById("fram").contentWindow.document.documentElement.scrollHeight : window.dialogHeight;
            //       var url=location.search;
            //       var pos=url.indexOf("=");
            //       var pos1=url.indexOf("&");
            //       var src=url.substring(pos+1,pos1-pos-1);
            document.getElementById("fram").src = "<%=url %>";
        }
    </script>
</head>
<body onload="lo()">
    <form id="form1" runat="server">
    <iframe frameborder="0" id="fram" onload="document.title=this.contentWindow.document.title"
        scrolling="auto" style="width: 100%; min-height: 550px"></iframe>
    </form>
</body>
</html>
