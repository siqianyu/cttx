<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetMemAuth.aspx.cs" Inherits="gzs_MemberInfo_AddSubMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>子账户添加</title>
    <link href="../../../css/tableStyle/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/tableStyle/PopUp.css" rel="stylesheet" type="text/css" />
    <style>
        #tableList
        {
            font-size: 14px;
            width: 100%;
            padding: 0px 20px;
            background: #fff;
        }
        #tableList ul > li:first-child
        {
            width: 135px;
        }
        #tableList ul li
        {
            display: inline-block;
        }
        #lbSave
        {
            background: Green;
            width: 115px;
            height: 26px;
            overflow: hidden;
            color: #FFF;
            text-align: center;
            line-height: 26px;
            font-size: 14px;
            margin: 0 0 0 50px;
            border: none;
        }
    </style>
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/json2.js" type="text/javascript"></script>
    <script>
        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            parent.flash(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" class="ViewBox" id="tableList">
        <tr>
            <td class="Rtd" style="margin: 20px;">
                <asp:Repeater ID="Menu" runat="server" OnItemDataBound="Menu_ItemDataBound">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <input type="checkbox" id='<%#Eval("mid") %>' value='<%#Eval("name") %>' name="lickbox" />
                                <label style="font-weight: bold;">
                                    <%#Eval("name") %></label>
                            </li>
                            <li id='<%#Eval("mid")+"_List" %>'>
                                <asp:Repeater ID="SubMenu" runat="server">
                                    <ItemTemplate>
                                        <input type="checkbox" id='<%#Eval("mid") %>' value='<%#Eval("name") %>' name="subbox" />
                                        <label>
                                            <%#Eval("name") %></label>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td class="Rtd">
                <input id="lbSave" type="button" value="保存" onclick="addSubAcc();" style="cursor: pointer;" />
            </td>
        </tr>
    </table>
    </form>
    <script>
        function addSubAcc() {
            var ids = "";
            var names = "";
            var undo = "";
            var undoinfo = "";
            var cklist = document.getElementById("tableList").getElementsByTagName("input");
            for (var j = 0; j < cklist.length; j++) {
                if (cklist[j].type == "checkbox") {
                    if (cklist[j].checked) {
                        ids += cklist[j].id + ";";
                        names += cklist[j].value + ";";
                    }
                    else {
                        undo += cklist[j].id + ";";
                        undoinfo += cklist[j].value + ";";
                    }
                }
            }
            if (ids === "") {
                alert("您尚未给子账户分配权限");
                return false;
            }
            if ("<%=id %>" == "") {
                $.ajax({
                    url: "MemberAuth.ashx?flag=add&r=" + Math.random(),
                    dataType: "text",
                    data: { ids: ids, names: names, undo: undo, undoinfo: undoinfo },
                    success: function (data) {
                        alert(data);
                        layer_close_refresh();
                    }
                });
            }
            else {
                $.ajax({
                    url: "MemberAuth.ashx?flag=update&id=<%=id %>&r=" + Math.random(),
                    dataType: "text",
                    data: { ids: ids, names: names, undo: undo, undoinfo: undoinfo },
                    success: function (data) {
                        alert(data);
                        layer_close_refresh();
                    }
                });
            }


        }

        //父菜单状态变化时 全选 全不选
        function getCheckBox() {
            var lists = document.getElementsByName("lickbox");
            for (var i = 0; i < lists.length; i++) {
                var list = lists[i];
                list.onclick = function () {
                    var itemList = document.getElementById(this.id + "_List").getElementsByTagName("input");
                    if (itemList) {
                        for (var j = 0; j < itemList.length; j++) {
                            if (itemList[j].type == "checkbox") {
                                itemList[j].checked = this.checked;
                            }
                        }
                    }
                }
            }

            //选择子菜单时自动选中父菜单
            var sublists = document.getElementsByName("subbox");
            for (var i = 0; i < sublists.length; i++) {
                var sublist = sublists[i];
                if (sublist.type == "checkbox") {
                    sublist.onclick = function () {
                        var pList = document.getElementById(this.id.toString().substring(0, this.id.toString().lastIndexOf('_')));
                        if (pList) {
                            if (this.checked) {
                                pList.checked = true;
                            }
                            else {
                                var sublist2 = document.getElementById(pList.id + "_List").getElementsByTagName("input");
                                var flag = false;
                                for (var j = 0; j < sublist2.length; j++) {
                                    if (sublist2[j].checked) {
                                        flag = true;
                                    }
                                }
                                pList.checked = flag;
                            }
                        }
                    }
                }
            }

            if ("<%=id %>" != "") {
                $.ajax({
                    url: "MemberAuth.ashx?flag=query&id=<%=id %>&r=" + Math.random(),
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            dataList = JSON.parse(data);
                            if (dataList) {
                                data = dataList[0];
                            }
                            var ids = data.submenu;
                            if (ids) {
                                var menus = ids.split(';');
                                for (var i = 0; i < menus.length; i++) {
                                    var item = document.getElementById(menus[i]);
                                    if (item && item.type == "checkbox") {
                                        item.checked = true;
                                    }
                                }
                            }
                        }
                    }
                });
            }
        }
        getCheckBox();
    </script>
</body>
</html>
