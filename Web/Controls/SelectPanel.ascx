<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectPanel.ascx.cs" Inherits="Components_SelectKM" %>
<script language="javascript">  
    //选择人员
	function <%=this.txtFnumberAndName.ClientID %>_ShowUsers()
    {           
        //选择人员
	    //var URL="/Controls/frame.aspx?url=<%=this.url %>&r="+Math.random(9999)+"";
        var URL="<%=this.url %>&r="+Math.random(9999)+"";
        window.open(URL,self,"height=600, width=800");				
	}

    function windowOpenReturnValueShowInfo(){
        var returnValue = document.getElementById("windowOpenReturnValue").value;
        //alert(returnValue);
		    if(returnValue!=null){
	              var arr = returnValue.split('$');
	              var valueStr = arr[0];
	              var textStr=(arr.length>1)?arr[1]:arr[0];
	          
	              var oldhidSysId=document.getElementById("<%=this.hidSysId.ClientID %>").value;
	              var oldtxtFnumberAndName=document.getElementById("<%=this.txtFnumberAndName.ClientID %>").value;
	              if(oldhidSysId == ""){
		              document.getElementById("<%=this.hidSysId.ClientID %>").value=valueStr; 
		              document.getElementById("<%=this.txtFnumberAndName.ClientID %>").value=textStr;   
		          }else{
		              if(document.getElementById("<%=this.hidTextMode.ClientID %>").value=="MultiLine"){  
		                  document.getElementById("<%=this.hidSysId.ClientID %>").value=oldhidSysId+","+valueStr; 
		                  document.getElementById("<%=this.txtFnumberAndName.ClientID %>").value=oldtxtFnumberAndName+","+textStr; 
		              }else{
		                  document.getElementById("<%=this.hidSysId.ClientID %>").value=valueStr; 
		                  document.getElementById("<%=this.txtFnumberAndName.ClientID %>").value=textStr;   
		              }
		          }
                  }
    }
	
	function <%=this.txtFnumberAndName.ClientID %>_UnShowUsers()
    {
        document.getElementById("<%=this.txtFnumberAndName.ClientID %>").value="";
        document.getElementById("<%=this.hidSysId.ClientID %>").value="";
        
        //回调函数
		try{
		<%=this.CallJsFunctionByReturnVoid %>
		}catch(e){}		
    }
    
    function <%=this.txtFnumberAndName.ClientID %>_GetId()
    {
        return "<%=this.txtFnumberAndName.ClientID %>";
    }
</script>
<input type="hidden" id="windowOpenReturnValue" />
<asp:HiddenField ID="hidTextMode" runat="server" />
<asp:HiddenField ID="hidSysId" runat="server" />
<asp:TextBox ID="txtFnumberAndName" Width="120px" Height="16px" BorderStyle="Groove"
    runat="server" MaxLength="20"></asp:TextBox>
<span id="enabled_img" runat="server">
    <img id="img1" alt='选择' border="0" runat="server" style="cursor: pointer; margin: 0px;"
        align="absMiddle">
    <img id="img2" alt='清除' border="0" runat="server" style="cursor: pointer; margin: 0px;"
        align="absMiddle">
</span><span id="disabled_img" runat="server">
    <img id="img3" border="0" runat="server" style="margin: 0px;" align="absMiddle" />
    <img id="img4" border="0" runat="server" style="margin: 0px;" align="absMiddle" />
</span>