<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="AppModules_Sysadmin_Base_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
   
</head>
<body>
    <form id="form1" runat="server">
    <div class="BigPic" style='position: relative; overflow: hidden; text-overflow: ellipsis;
        white-space: nowrap;'>
        <img alt="Test" src="../../../Images/0925570762.jpg"></img>
        <span id='chooseSet' style='background-color: Gray; display: none; border: thin solid #C0C0C0;
            filter: alpha(opacity=50); -moz-opacity: 0.50; opacity: 0.50; position: absolute;
            width: 200px; height: 200px;'></span>
    </div>
    <div id='showBigImg' style='background-color: white; width: 400px; height: 400px;
        position: fixed; overflow: hidden; display: none'>
        <div id='showBigImg2' style='position: absolute; display: none;'>
        </div>
    </div>
    <input type="button" value="点击" id="btn" />
    </form>
</body>
 <script src="../../../js/singleGoods.js" type="text/javascript"></script>
</html>
