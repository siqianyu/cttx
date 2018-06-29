<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>才通天下微信公号平台</title>
    <link href="Style/Common.css" rel="stylesheet" />
    <script src="Style/Menu.js" language="javascript" type="text/javascript"></script>
    <script src="Style/Menu.js" language="javascript" type="text/javascript"></script>
    <script src="js/jquery-1.9.0.min.js" language="javascript" type="text/javascript"></script>
    <script language="javascript">
        /*提供iframe内页引用iframe_height_reset.js*/
        function setHeight(docHei) {
            var iframeWin = window.frames['Main'];
            var iframeEl = window.document.getElementById ? window.document.getElementById('Main') : document.all ? document.all['Main'] : null;
            if (iframeEl && iframeWin) {
                iframeEl.style.height = docHei + 'px';
            }
            else if (iframeEl) {
                iframeEl.style.height = docHei + 'px';
            }
        }

        function selectTab(id, title) {
            var tabs = $("#hidRootMenu").val().split(',');
            for (var i = 0; i < tabs.length; i++) {
                $("#" + tabs[i]).attr("class", "Normal");
                $("#" + id).attr("class", "Active");
            }
            $("#menu_title").text(title);
            //loadMenus(id);
            var menus = $.ajax({ url: "LeftMenuTree.ashx?userid=<%=this.UserId %>&pid=" + id.replace("tab_", "") + "&r=" + Math.random() + "", async: false }).responseText;
            //alert(menus);
            $("#div_menus").html(menus);
            initleftmenu();
            loadDefaultPage(id);
            if (id == "tab_106") { Hidden(); } else {Visible();}
        }


        /*==顶部菜单切换的时候，默认显示页面==*/
        function loadDefaultPage(tabId) {
            $("#Main").attr("src", "about:blank");
            var firstPage = $("#div_menus li:eq(0) a").attr("href");
            //alert(firstPage);
            $("#Main").attr("src", firstPage);
        }

        function initleftmenu() {
            $("#div_menus ul:eq(0)").show();
//            $("#div_menus p.Act").click(function () {
//                $(this).addClass("current").next("ul").slideToggle(300).siblings("ul").slideUp("slow");
//                $(this).siblings().removeClass("current");
//            });
        }
        function leftmenuclick(id) {
            $("#" + id).addClass("current").next("ul").slideToggle(300).siblings("ul").slideUp("slow");
            $("#" + id).siblings().removeClass("current");
        }

        $(document).ready(function () {
            initleftmenu();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Header Left">
        <div class="LogoBar Left">
            <img src="Images/Logo.png" class="Logo Left" />
            <p style="display:none">
                <img src="Images/Skin.png" class="Skin" /><span><a href="#">回到首页</a>|<a href="#">个人定制</a>|<a
                    href="#">帮助手册</a></span></p>
        </div>
        <!--LogoBar end-->
        <div class="MenuBar Left">
            <p class="Menu">
                <asp:Literal runat="server" ID="ltRootMenu"></asp:Literal>
            </p>
            <asp:HiddenField runat="server" ID="hidRootMenu" />
            <div class="Member">
                <input onclick="location.href='Default.aspx'" value="退出登录" />
                <p>
                    【<span id="span_truename" runat="server">--</span>】，您好！今天是<asp:Literal
                        runat="server" ID="ltTime"></asp:Literal></p>
                <img src="Images/MemIco.png" />
            </div>
        </div>
        <!--MenuBar end-->
    </div>
    <!--Header end-->
    <div class="Main Left">
        <table class="Frame" cellpadding="0" cellspacing="3">
            <tr>
                <td class="PartL" id="MenuS">
                    <div class="MenuLT Left">
                        <p id="menu_title">
                            --</p>
                        <a href="#" title="点击收起左侧菜单" onclick="Hidden();">
                            <img src="Images/Close.png" /></a></div>
                    <div class="MenuLC Left" id="div_menus">
                    </div>
                </td>
                <td class="OpenBar" id="MenuH" onclick="Visible();" title="点击展开左侧菜单">
                    <a href="#">
                        <img src="Images/Open.png" /></a>
                </td>
                <td class="PartM">
                </td>
                <td class="PartR">
                    <iframe id="Main" name="Main" src="AppModules/Desktop/Default.aspx" width="100%" height="650"
                        frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe>
                </td>
            </tr>
        </table>
        <!--Frame end-->
    </div>
    <!--Main end-->
    <div class="Footer Left">
        <p>
            技术支持：<a href="#">才通天下微信公号平台</a></p>
    </div>
    <div style="display: none">
        <iframe src="Refresh.aspx"></iframe>
    </div>
    </form>
</body>
</html>
