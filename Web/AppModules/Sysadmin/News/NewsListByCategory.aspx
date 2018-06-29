<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsListByCategory.aspx.cs"
    Inherits="Admin_AppModules_NewsListByCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <script type="text/javascript" language="javascript" src="../../../js/jquery-1.2.6.pack.js"></script>
    <script type="text/javascript" language="javascript" src="../../../js/jquery.mouseevent.js"></script>
    <script type="text/javascript" language="javascript" src="../../../js/calendar.js"></script>
    <script type="text/javascript" language="javascript" src="../../../js/TreeView_CheckBox.js"></script>
    <script language="javascript">
        function setHeight() {
            var iframeWin = window.frames['mainList'];
            var iframeEl = window.document.getElementById ? window.document.getElementById('mainList') : document.all ? document.all['mainList'] : null;
            if (iframeEl && iframeWin) {
                var docHei = getDocHeight(iframeWin.document);
                var menuHeight = document.all['Menu'].scrollHeight;
                if (docHei < menuHeight) docHei = menuHeight;
                if (docHei < 400) docHei = 400;
                docHei = docHei + 100;
                if (docHei != iframeEl.style.height) iframeEl.style.height = docHei + 'px';
            }
            else if (iframeEl) {
                var docHei = getDocHeight(iframeEl.contentDocument);
                var menuHeight = document.all['Menu'].scrollHeight;
                if (docHei < menuHeight) docHei = menuHeight;
                if (docHei < 400) docHei = 400;
                docHei = docHei + 100;
                if (docHei != iframeEl.style.height) iframeEl.style.height = docHei + 'px';
            }
        }

        function getDocHeight(doc) {
            //在IE中doc.body.scrollHeight的可信度最高
            //在Firefox中，doc.height就可以了
            var docHei = 0;
            var scrollHei; //scrollHeight
            var offsetHei; //offsetHeight，包含了边框的高度

            if (doc.height) {
                //Firefox支持此属性，IE不支持
                docHei = doc.height;
            }
            else if (doc.body) {
                //在IE中，只有body.scrollHeight是与当前页面的高度一致的，
                //其他的跳转几次后就会变的混乱，不知道是依照什么取的值！
                //似乎跟包含它的窗口的大小变化有关
                if (doc.body.offsetHeight) docHei = offsetHei = doc.body.offsetHeight;
                if (doc.body.scrollHeight) docHei = scrollHei = doc.body.scrollHeight;
            }
            else if (doc.documentElement) {
                if (doc.documentElement.offsetHeight) docHei = offsetHei = doc.documentElement.offsetHeight;
                if (doc.documentElement.scrollHeight) docHei = scrollHei = doc.documentElement.scrollHeight;
            }
            /*
            docHei = Math.max(scrollHei,offsetHei);//取最大的值，某些情况下可能与实际页面高度不符！
            */
            return docHei;
        }
    </script>
    <script type="text/javascript">
        function buttonAction(s) {
            var ids = Components_TopButtons_GetCheckBoxValues(document.form1.ids);
            var selectNumer = ids.split(',').length;
            if (s == "add") {
                location.href = "AddGoodsCategory.aspx";
                return false;
            }
            else if (s == "edit") {
                //if(Components_TopButtons_CheckUpdate(document.form1.ids) == false){return false;}
            }
            else if (s == "delete") {
                if (confirm('确定要删除吗?') == false) { return false; }
            }
            else if (s == "search") {

            }
            else if (s == "show") {
                //if(Components_TopButtons_CheckUpdate(document.form1.ids) == false){return false;}
            }
            return true;
        }
        window.setInterval("window.parent.setHeight()",1500);
        
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="3" cellspacing="3" id="Menu">
        <tr>
            <td style="text-align: left; width: 190px; vertical-align: top; background-color: #eff7fc;">
                <div style="height: 800px; overflow: auto">
                    <asp:TreeView Width="100%" ID="tvModuleTree" runat="server" BorderColor="#E0E0E0"
                        ShowLines="True" Style="padding: 0px; margin: 0px;" ExpandDepth="2" SelectedNodeStyle-ForeColor="red"
                        SelectedNodeStyle-Font-Bold="true" Font-Size="12px">
                    </asp:TreeView>
                </div>
            </td>
            <td>
                <iframe id="mainList" name="mainList" src="NewsList.aspx" style="width: 100%; height: 900px;"
                    frameborder="0" scrolling="no"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
