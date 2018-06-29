<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadImg.aspx.cs" Inherits="AppModules_Menu_UploadImg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    
    <link href="../../../css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/redmond/jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../../js/iframe_height_reset.js" type="text/javascript"></script>
    <script src="../../../js/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../../js/layer-v1-8-3/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
                //关闭当前层(返回按钮)
            function layer_close() {
                var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
                parent.layer.close(layer_index);
            }

            //关闭当前层并刷新列表页(保存按钮)
            function layer_close_refresh() {
                parent.grid_search(); //执行列表页的搜索事件
                var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
                parent.layer.close(layer_index);
            }


    </script>
</head>
<body>

    <form id="form1" runat="server">
    <div>

    <table>
    <tr>
    <td>
            
        <asp:Image ID="Image1" runat="server" Width='130' Height='130' ImageUrl='/Images/notupload.jpg' /><br /><asp:FileUpload style='width:130px' ID="FileUpload1" runat="server" />
        </td>

    <td>
            
        <asp:Image ID="Image2" runat="server" Width='130' Height='130' ImageUrl='/Images/notupload.jpg' /><br /><asp:FileUpload style='width:130px' ID="FileUpload2" runat="server" />
        
    </td>
    


        
    <td>
            
        <asp:Image ID="Image3" runat="server" Width='130' Height='130' ImageUrl='/Images/notupload.jpg' /><br /><asp:FileUpload style='width:130px' ID="FileUpload3" runat="server" />
    </td>
</tr>
<tr>
    <td>
           
        <asp:Image ID="Image4" runat="server" Width='130' Height='130' ImageUrl='/Images/notupload.jpg' /><br /> <asp:FileUpload style='width:130px' ID="FileUpload4" runat="server" />
   </td>

<%--    </tr>
    <tr>--%>
    <td>        
    
        <asp:Image ID="Image5" runat="server" Width='130' Height='130' ImageUrl='/Images/notupload.jpg' /><br />  <asp:FileUpload style='width:130px' ID="FileUpload5" runat="server" />
  
        
   </td>

    <td>        
            <asp:Image ID="Image6" runat="server" Width='130' Height='130' ImageUrl='/Images/notupload.jpg' /><br />  <asp:FileUpload style='width:130px' ID="FileUpload6" runat="server" />

         </td>
    </tr>
    <tr>
    <td colspan='3' align="center">
        <asp:HiddenField ID="hfImgList" runat="server" />
        
    <span style='color:#F00;'>说明：图片宽度建议：300px,高度不限,建议使用PNG格式</span>
        <br />
        <asp:Button ID="btUpload" runat="server" Text="上  传" onclick="btUpload_Click"  style='margin:10px;' />
        <input id="btSend" type="button" value="提  交" style='margin:10px;'  />
    </td>

</tr>
    </table>







    </div>
    </form>
</body>
<script type="text/javascript">
    var num=<%=num %>;
    var menuId='<%=menuId %>';
    $("#btSend").click(function () {
        var hfV=$("#hfImgList").val();
        if(hfV!=""){
        parent.GetImage(num,menuId,hfV);
        }
        //parent.grid_search(); //执行列表页的搜索事件
        var layer_index = parent.layer.getFrameIndex(window.name); //获取当前窗口索引
        parent.layer.close(layer_index);
        
    });
</script>
</html>
