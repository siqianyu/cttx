<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_Forms_Resource.aspx.cs" Inherits="AppModules_Goods_T_Forms_Resource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>任务图片</title>
    
    <style>
    a{
	    text-decoration:underline;
	    color:#ff0000;
    }
    </style>

<script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
<script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
<script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
<script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
<script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
<script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
<script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript">
       function checkForm(){
            document.getElementById('btnSave').style.display="none";
            document.getElementById('btnUploading').style.display="";
            return true;
        }

        //关闭当前层并刷新列表页(保存按钮)
        function layer_close_refresh() {
            alert("!");
            parent.GetSlide(); //执行列表页的搜索事件
            var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
            parent.layer.close(layer_index);

        }
        
        function deleteOne(str){
            if(confirm('确定要删除吗？')==false){return false}
//            var resultObj = AppModules_Goods_T_Forms_Resource.Ajax_Delete(str);
//            if(resultObj.value=="1"){
//                alert('删除成功');
//                toClose();
//            }else{
//                alert('删除失败');
//            }


            $.ajax({
                url: "T_Forms_Resource.aspx?del=" + str,
                dataType: "text",
                success: function (data) {
                    if (data == "success") {
                        alert('删除成功');
                        $("tr[did=" + str + "]").remove();
                        //toClose();
                    } else {
                        alert('删除失败');
                    }
                }


            });
        }
        
        function toClose(){
            window.returnValue="1";
            window.close();
        }
    </script>
    <base target="_self"></base>
</head>
<body style="font-size:12px; margin:0px">
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
    <div style="width:580px; height:450px; overflow:auto">
    <table width="100%" border="1" cellspacing="0" cellpadding="2" class="table_1" style="font-size: 12px;
            border-collapse: collapse" bordercolor="#E4F6FF">
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <tr did='<%#Eval("Sysnumber").ToString() %>'><td height="18">
                        <a style="text-decoration: underline"
                            target="_blank">
                            <img src='<%#Eval("picpath").ToString() %>' height="100" />
                        </a>
                    </td>
                    <td width="70" align="center"><a href="javascript:deleteOne('<%#Eval("Sysnumber").ToString() %>');void(0)">[删除]</a></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        </div>
        <asp:Panel ID="panUpload" runat="server">
        <div style="line-height:5px; height:5px;"></div>
            <table width="98%" border="1" cellspacing="0" cellpadding="2" class="table_1" style="font-size: 12px;
                border-collapse: collapse" bordercolor="#E4F6FF">
                <tr>
                    <td>
                        <div>
                            排序：<input name="Check" type="text" class="input_5" id="txtOrder1" runat="server"
                                value="1" style="width: 50px" />
                            图片：
                            <input type="file" class="file" name="file_1" style="width: 200px;" />
                            是否添加水印:
                            <input type="checkbox" class="ck" name="ck_1" id='Checkbox1' style="width: 40px;" runat="server"/>
                        </div>
                                                <div>
                            排序：<input name="input3" type="text" class="input_5" id="txtOrder2" runat="server"
                                value="2" style="width: 50px" />
                            图片：
                            <input type="file" class="file" name="file_2" style="width: 200px;" />
                            是否添加水印:
                            <input type="checkbox" class="ck" name="ck_1" id='Checkbox2' style="width: 40px;" runat="server"/>
                        </div>
                                                <div>
                            排序：<input name="input3" type="text" class="input_5" id="txtOrder3" runat="server"
                                value="3" style="width: 50px" />
                            图片：
                            <input type="file" class="file" name="file_3" style="width: 200px;" />
                            是否添加水印:
                            <input type="checkbox" class="ck" name="ck_1" id='Checkbox3' style="width: 40px;" runat="server"/>
                        </div>
                                                <div>
                            排序：<input name="input3" type="text" class="input_5" id="txtOrder4" runat="server"
                                value="4" style="width: 50px" />
                            图片：
                            <input type="file" class="file" name="file_4" style="width: 200px;" />
                            是否添加水印:
                            <input type="checkbox" class="ck" name="ck_1" id='Checkbox4' style="width: 40px;" runat="server"/>

                        </div>
                    </td>
                    <td width="70" align="center">
                        <asp:Button runat="server" ID="btnSave" Text="上传" OnClick="btnSave_Click" OnClientClick="return checkForm()" />
                        <input id="btnUploading" type="button" disabled="disabled" style="display: none"
                            value="上传中" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
    </form>
</body>
</html>
