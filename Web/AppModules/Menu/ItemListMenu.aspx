<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemListMenu.aspx.cs" Inherits="AppModules_Menu_ItemListMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>所有食材</title>
    <link href="../../../Style/List.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .divSearch{ float:left; width:190px}
        .divSearch span{color: #2871a4; float:left; font-weight:bold;}
        .divSearch input{background: none repeat scroll 0 0 #fff;
    border: 1px solid #ccc;
    height: 20px;
    line-height: 20px; float:left;}
    .divSearchUC{float:left; width:325px; height:30px;}
    .divSearch select{background: none repeat scroll 0 0 #fff;
    border: 1px solid #ccc;
    height: 22px;float:left;}
    .divSearchUC .SelectBut{border:none;width:65px;height:25px;background:url(../../../Images/LogOutN.png);color:#9b0700;text-shadow:0 1px #ffe191;margin:0 10px;padding:0;text-align:center;cursor:pointer;float:left;}
    .divSearchUC .SelectBut:hover{background:url(../../../Images/LogOutA.png);}
    .spanSearch{padding:0 0 0 10px;font-weight:bold;color:#2871a4;float:left; line-height:25px;}
    .InputClass{background:url(../../../Images/Opera.png) repeat-x;border:1px solid #d0d0d0;float:left;margin:3px 0 0 7px;display:inline;padding:0 0;line-height:22px; height:22px; overflow:hidden;}
    .InputClass:hover{text-decoration:underline}
    .Gray{color:#5a5a5a;}
    .LightGray{color:#969696}
    .Green{color:#0f7e00;}
    .LightGr{color:#2aa21a;}
    .DarkRed{color:#b22300}
    .Red{color:#f00}
    .goodsimgstyle img{padding:3px;}
    </style>
</head>
<body style=" width:99%">
    <form id="form1" runat="server">
    <div class="PosiBar">
        <p>
            食材列表</p>
    </div>

    
    <div class="SosoBar Left">
        <table>
            <tr>
                <td style=" width:80%">
                    <div class="divSearch">

                    <div class="spanSearch" >食材名：</div>
                    <input style="width: 100px;" id="txtItemName" />

                    </div>

                     <div class="divSearch">

                    <div class="spanSearch" >是否支持购买：</div>
                    <select id='slBuy'>
                        <option value='-1'>----</option>
                        <option value='1'>支持</option>
                        <option value='0'>不支持</option>
                    </select>
                    </div>
                </td>
                <td style=" width:20%">
                    <div class="divSearchUC"><input type="button" class="SelectBut" value="查&nbsp;询" title="查询" onclick="grid_search()" />
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div class="TableBox Left">
        <table id="startech_table_jqgrid">
        </table>
        <div id="startech_table_jqgrid_pager">
        </div>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var cindex=<% =index %>;
    jQuery("#startech_table_jqgrid").jqGrid({
        url: 'Menu.ashx?flag=ilist',
        datatype: "json",
        colNames: ['食材Id', '预览图', '食材名', '食材类型', '支持购买', '备注', '排序','','','', '操作'],
        //menuId,imgSrc,menuName,Technology,Flavor,CookingTime,Calorie,UserId,AddTime,isShow,orderby
        colModel: [
            { name: 'itemId', index: 'itemId', hidden: true },
            { name: 'itemImgSrc', index: 'itemImgSrc', width: 60, align: 'center', classes: 'goodsimgstyle' },
            { name: 'itemName', index: 'itemName',  align: 'left' },
   		    { name: 'itemType', index: 'itemType', width: 50, align: 'left', hidden: true },
            { name: 'ifBuy', index: 'ifBuy', width: 40, align: 'center' },
            { name: 'remark', index: 'remark', width: 70, align: 'left' },
            { name: 'orderBy', index: 'orderBy', width: 30, align: 'center', hidden: true },
            { name: 'unit', index: 'unit', width: 30, align: 'center', hidden: true },
            { name: 'goodsId', index: 'unit', width: 30, align: 'center', hidden: true },
            { name: 'goodsFormate', index: 'unit', width: 30, align: 'center', hidden: true },
            { name: 'cmd_col', align: 'center', width: 100 }
   	    ],
        rowList: [10, 15, 20],
        pager: '#startech_table_jqgrid_pager',
        sortname: 'orderby',
        viewrecords: true,
        sortorder: "desc",
        height: "100%",
        autowidth: true,
//        edittype: 'checkbox',
//        multiselect: true,


        onSelectRow: function (rowid) {
            if (rowid) {
                var gr = $("#startech_table_jqgrid").getGridParam("selrow");
                var id = $("#startech_table_jqgrid").getCell(gr, "itemId");
            }
        },
        gridComplete: function () {
            var ids = $('#startech_table_jqgrid').jqGrid('getDataIDs');  //得到行id数组行号
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#startech_table_jqgrid").getRowData(ids[i]);
                if (rowData) {
                    var iname=rowData.itemName;
                    var unit=rowData.unit;
                    var id = rowData.itemId;
                    var imgSrc = rowData.itemImgSrc;
                    var ifb = rowData.ifBuy == "1" ? "支持" : "<font color='red'>不支持</font>";
                    var name=rowData.itemName;

                    var goodsf = rowData.itemName +"<div>"+ rowData.goodsFormate+"</div>";


                    var writeData = {
                        itemName:goodsf,
                        itemImgSrc: "<img src='" + imgSrc + "' width='70' height='70'/>",
                        ifBuy: ifb,
                        //cmd_col: "<input type='button' class='CommonButon' value='编辑' onclick=\"button_actions('edit','" + id + "')\"> <input type='button' class='CommonButon' value='删除' onclick=\"button_actions('delete','" + id + "')\">"
                        cmd_col: "<input type='button' class='CommonButon' value='主料' onclick=\"usingItem('" + id + "',1,'主料','"+iname+"')\"> "
                        + "<input type='button' class='CommonButon' value='辅料' onclick=\"usingItem('" + id + "',1,'辅料','"+iname+"')\"> "
                        + "<input type='button' class='CommonButon' value='调料' onclick=\"usingItem('" + id + "',1,'调料','"+iname+"')\"> "
                    }
                    $('#startech_table_jqgrid').jqGrid('setRowData', ids[i], writeData);
                    //<input type='button' class='CommonButon' value='查看' onclick=\"button_actions('show','" + id + "')\">
                }
            }
        }
    });
    jQuery("#startech_table_jqgrid").jqGrid('navGrid', '#startech_table_jqgrid_pager', { edit: false, add: false, del: false });

    //刷新当前页面
    function freshCurrentPage() {
        jQuery("#startech_table_jqgrid").trigger("reloadGrid");
    }

    function usingItem(id,index,type,name)
    {
        var code="";
        $(".gf"+id).each(function(i){
            if($(this).attr("checked")){
            code=$(this).attr("code");
            }
        });
       //parent.grid_search(); //执行列表页的搜索事件
       parent.GetItem(id,name,type,code);
       var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
       parent.layer.close(layer_index);
    }

    //button_actions
    function button_actions(flag, id, lang) {
        // id = safeReplace(id);
        if (flag == "add") {
            add_method();
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        }
        else if (flag == "edit") {
            edit_method(id);
            jQuery("#startech_table_jqgrid").trigger("reloadGrid");
        } else if (flag == "show") {
            show_method(id);
        }
        else if (flag == "download") {
            //download_method(id, lang);
            var str = $.ajax({ url: "StandardHandle.ashx?flag=delete&lang='" + lang + "'id=" + id + "", async: false }).responseText;
        }
        else if (flag == "delete") {
            delete_method(id);
        }
        else if (flag == "approve") {
            approve_method(id);
        }
        else if (flag == "approveAll") {
            approveAll();
            return false;
        }
        if (flag == "deleteAll") {
            deleteAction();
            return false;
        }
    }


    //查询
    function grid_search() {
        //var _AreaId = $("#txtAreaId").val();
        var _AreaName = $("#txtItemName").val();
        var buy = $("#slBuy").val();
        var _searchfilter = "itemName$$" + _AreaName+ "_$$_ifbuy$$" + buy; // + "_$$_Pid$$" + _Pid;
        
        var _searchfilter = escape(_searchfilter);
        jQuery("#startech_table_jqgrid").jqGrid('setGridParam', { url: "Menu.ashx?flag=ilist&searchfilter=" + _searchfilter + "", page: 1 }).trigger("reloadGrid");
    }













</script>
