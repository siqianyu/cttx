<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopButtons.ascx.cs" Inherits="Components_TopButtons" %>
<table cellpadding="0" cellspacing="0" id="Components_TopButtons_Table_Id">
    <tr class="pad">
        <td id="Components_TopButtons_Table_Id_Buttons">
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnAdd" ImageUrl="~/Images/skin/add.jpg" OnClick="btnAdd_Click"
                Visible="false" ToolTip="添加" />

            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnEdit" ImageUrl="~/Images/skin/modify.jpg" Visible="false"
                OnClick="btnEdit_Click" ToolTip="修改" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnDelete" ImageUrl="~/Images/skin/delete.jpg" Visible="false"
                OnClick="btnDelete_Click" ToolTip="删除" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnSH" ImageUrl="~/skin/button/blue/audit.jpg" Visible="false"
                OnClick="btnSH_Click" ToolTip="审核" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnNSH" ImageUrl="~/skin/button/blue/cancel.jpg" Visible="false"
                OnClick="btnNSH_Click" ToolTip="取消审核" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnSC" ImageUrl="~/skin/blue/images/shoucang.jpg" Visible="false"
                OnClick="btnSC_Click" ToolTip="收藏" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver" ToolTip="查看"
                BorderWidth="0" ID="btnShow" ImageUrl="~/Images/skin/show.jpg" Visible="false"
                OnClick="btnShow_Click" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="imgtnPatrol" ImageUrl="~/skin/button/red/checkresult.jpg"
                Visible="false" OnClick="btnPatrol_Click" ToolTip="检查结果" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnZS" ImageUrl="~/skin/button/red/ZS.jpg" Visible="false"
                OnClick="btnZS_Click" ToolTip="证书管理" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="imgbtnSB" ImageUrl="~/skin/button/red/btnSendSJ.jpg" Visible="false"
                OnClick="btnSB_Click" ToolTip="发送市局" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnSearch" ImageUrl="~/Images/skin/search.jpg" Visible="false"
                OnClick="btnSearch_Click" ToolTip="搜索" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnBack" ImageUrl="~/Images/skin/search.gif" Visible="false"
                OnClick="btnBack_Click" ToolTip="返回" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnSendMessage" ImageUrl="~/skin/button/red/btnDXTX.jpg"
                Visible="false" OnClick="btnSendMessage_Click" ToolTip="短信提醒" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnOverTime" ImageUrl="~/skin/button/red/btnDQTX.jpg" Visible="false"
                OnClick="btnOverTime_Click" ToolTip="到期提醒" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnYearCheck" ImageUrl="~/skin/button/red/btnNSTX.jpg" Visible="false"
                OnClick="btnYearCheck_Click" ToolTip="年审提醒" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnSet" ImageUrl="~/skin/button/red/btnSZ.jpg" Visible="false"
                OnClick="btnSet_Click" ToolTip="设置" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnPersonSet" ImageUrl="~/skin/button/red/btnCSZ.jpg" Visible="false"
                OnClick="btnPersonSet_Click" ToolTip="人员设置" />
            <asp:ImageButton CssClass="jquery_button_image" runat="server" SkinID="ibtnSkinMouseOver"
                BorderWidth="0" ID="btnConpanySet" ImageUrl="~/skin/button/red/btnPSZ.jpg" Visible="false"
                OnClick="btnConpanySet_Click" ToolTip="企业设置" />
        </td>
        <td id="Components_TopButtons_Table_Id_Images" style="display: none">
        </td>
        <td style="display: none">
            <img src="<%=this.upImg %>" alt='展开' align="absmiddle" onclick="Components_Up_Down_Control_UpDown('<%=this.ControlPanelID %>')"
                id="Components_Up_Down_Control_UpImg" style="display: none; cursor: hand;" />
            <img src="<%=this.downImg %>" alt='收起' align="absmiddle" onclick="Components_Up_Down_Control_UpDown('<%=this.ControlPanelID %>')"
                id="Components_Up_Down_Control_DownImg" style="cursor: hand;" />
        </td>
        <td>
            <span>
                <asp:Button ID="btnHid" runat="server" OnClientClick="return false" Width="1" Height="1"
                    Style="background-color: White; border: 0pt solid #ffffff" /></span>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidCheckBoxValues" runat="server" />

<script language="javascript">
    var Global_Components_Up_Down_Control_Status = "down"; //全局变量
    
    function Components_Up_Down_Control_UpDown(panelId){
        var objPanel = document.getElementById(panelId);
        var objUp = document.getElementById('Components_Up_Down_Control_UpImg');
        var objDown = document.getElementById('Components_Up_Down_Control_DownImg');
        if(objPanel){
            if(Global_Components_Up_Down_Control_Status == "down"){
                Global_Components_Up_Down_Control_Status = "up";
                objPanel.style.display = "none";
                objUp.style.display = "block";
                objDown.style.display = "none";
            }else{
                Global_Components_Up_Down_Control_Status = "down";
                objPanel.style.display ="block";
                objUp.style.display = "none";
                objDown.style.display = "block";
            }
        }
    }
    
    
   //供引用页面调用,返回checkbox当前选择的值
    function Components_TopButtons_GetCheckBoxValues(checkBoxObjs){
        var returnStr="";
        var objs = checkBoxObjs;
        if(objs!=null){
                if(objs.length){
                for(var i=0;i<objs.length;i++){
                    if(objs[i].checked && objs[i].value!=""){returnStr+=objs[i].value+",";}
                }
                if(returnStr!=""){returnStr=returnStr.substring(0,returnStr.length-1);}
            }else{
                if(objs.checked && objs.value!=""){returnStr=objs.value;}
            }
            document.getElementById("<%=this.hidCheckBoxValues.ClientID %>").value=returnStr;        
        }
        
        return returnStr
    }
    
    
    //供引用页面调用(全选/反选)
	function Components_TopButtons_CheckAll(checkAllBox)
    {
        var frm = document.all.tags('input');
        var ChkState=checkAllBox.checked;
        var i;
        for(i=0;i< frm.length;i++)
        {
            if(frm(i).type=='checkbox')
            { 
                frm(i).checked = ChkState;
            }
        }
    }
    
    //供引用页面调用(删除前确认)
    function Components_TopButtons_CheckDelete(checkBoxObjs){
        var allIds = Components_TopButtons_GetCheckBoxValues(checkBoxObjs);
        if(allIds == ""){
            alert("请选择要操作的项");
            return false;
        }
        if(!confirm("确定要进行删除吗？")){return false;}
        return true;
    }
     //供引用页面调用(审核前确认)
    function Components_TopButtons_CheckSH(checkBoxObjs){
        var allIds = Components_TopButtons_GetCheckBoxValues(checkBoxObjs);
        if(allIds == ""){
            alert("请选择要操作的项");
            return false;
        }
        if(!confirm("确定要修改状态吗？")){return false;}
        return true;
    }
    //供引用页面调用(修改前确认)
    function Components_TopButtons_CheckUpdate(checkBoxObjs){
        var allIds = Components_TopButtons_GetCheckBoxValues(checkBoxObjs);
        if(allIds == ""){
            alert("请选择要操作的项");
            return false;
        }
        if(allIds.split(',').length>1){
            alert("只能选择一项进行操作");
            return false;
        }
        return true;
    }
    
 
    //供引用页面调用(批量修改前确认+提示信息)
    function Components_TopButtons_CheckUpdateBat(checkBoxObjs,confirmMsg){
        var allIds = Components_TopButtons_GetCheckBoxValues(checkBoxObjs);
        if(allIds == ""){
            alert("请选择要操作的项");
            return false;
        }
        if((confirmMsg) && (typeof(confirmMsg)=="string")){if(!confirm(confirmMsg)){return false;}}
        return true;
    }
    
    function Components_TopButtons_HiddenTopButtions(){
    	var newTableHtml = "";
	    $(".jquery_button_image","table[id='Components_TopButtons_Table_Id']").each(
	        function(){
	            newTableHtml += "<img style='filter:alpha(opacity=70)' src='"+$(this).attr("src")+"'> ";
	        }
	    );
	    if(newTableHtml != ""){
	        $("#Components_TopButtons_Table_Id_Images").html(newTableHtml);
	        $("#Components_TopButtons_Table_Id_Images").css("display","block");
            $("#Components_TopButtons_Table_Id_Buttons").css("display","none");
        }
    }
    
    //供引用页面调用(显示当前条件的状态以及屏蔽用户重复提交)
    function Components_TopButtons_ShowProcessStatus(content){
        if(!content){content='提交处理中，请稍等...'};
	    $("#Div_Components_TopButtons_Bg_Content").css("display","block");
	    $("#Span_Components_TopButtons_Bg_Content").text(content);
	    
	    //屏蔽用户重复提交
	    Components_TopButtons_HiddenTopButtions();
	}
	
    //供引用页面调用(不显示当前条件的状态以及屏蔽用户重复提交)
    function Components_TopButtons_UnShowProcessStatus(){
        $("#Div_Components_TopButtons_Bg_Content").css("display","none");
    }
    
</script>


