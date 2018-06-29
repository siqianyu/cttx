<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuTypeList.aspx.cs" Inherits="AppModules_Menu_MenuTypeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/Controls/Edit.ascx" TagName="Edit" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/Delete.ascx" TagName="Delete" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/Search.ascx" TagName="Search" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/Show.ascx" TagName="Show" TagPrefix="uc6" %>
<%@ Register Src="~/Controls/Add.ascx" TagName="Add" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function buttonAction(s) {
            var ids = Components_TopButtons_GetCheckBoxValues(document.form1.ids);
            var selectNumer = ids.split(',').length;
            if (s == "add") {
                location.href = "AddMenu.aspx";
                return false;
            }
            else if (s == "edit") {
                if (global_count == 0) { alert("请选择一个要编辑的节点!"); return false; }
                if (global_count > 1) { alert("只能选择一个要编辑的节点!"); return false; }
            }
            else if (s == "delete") {
                if (global_count == 0) { alert("请选择要删除的页节点!"); return false; }
                else { return confirm("您确定要删除该节点吗?"); }
            }
            else if (s == "add2") {
                if (global_count == 0) { alert("请选择节点!"); return false; }
            }
            return true;
        }

    </script>

    <script type="text/javascript">


        //添加
        function add_method() {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['添加', true],
                maxmin: true,
                iframe: { src: 'AddMenuType.aspx?r=' + Math.random() },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }


        //修改
        function edit_method(id) {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['修改', true],
                maxmin: true,
                iframe: { src: 'AddMenuType.aspx?id=' + id },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }

        //查看
        function show_method(id) {
            $.layer({
                type: 2,
                shade: [0.1, '#000'],
                fix: false,
                title: ['查看', true],
                maxmin: true,
                iframe: { src: 'AddMenuType.aspx?id=' + id },
                area: [document.body.scrollWidth - 20, $(document).height()],
                offset: ['0px', ''],
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#name', index).val(), 3, 1);
                    //jQuery("#startech_table_jqgrid").trigger("reloadGrid");
                }
            });
        }


        //查询
        function grid_search() {
            var _StdID = $("#txtstdid").val();

            var _searchfilter = "StdID$$" + _StdID;
            var _searchfilter = escape(_searchfilter);
            jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "ListGroupHandler.ashx?flag=list&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
            location.replace(location);

        }
        

    </script>


</head>
<body>
    <form id="form1" runat="server">
    <!--按钮组-->
    <div class="PosiBar">
        <p>
            菜谱分类管理</p>
    </div>
    
    <div class="SosoBar Left">
        <div class="Soso">
            <p class="Query">

                <uc1:Add ID="Add1" runat="server" />
                <uc3:Edit ID="Edit1" runat="server" />
                <uc6:Show ID="Show1" runat="server" />
                <uc4:Delete ID="Delete1" runat="server" />
            </p>
            <div style="height: 3px; line-height: 3px;">
            </div>
        </div>
    </div>
    <div id="div_right" style="font-size: 16px; font-family: 微软雅黑; color: Black">
        <table cellspacing="2" cellpadding="2" width="98%" align="center" border="0">
            <asp:TreeView ID="treeMenu" runat="server" ExpandDepth="1" ShowLines="True">
            </asp:TreeView>
        </table>
    </div>
    </form>
</body>
</html>

