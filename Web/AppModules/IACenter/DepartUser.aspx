<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartUser.aspx.cs" Inherits="AppModules_Mail_DepartUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门人员列表</title>
    <base target="_self" />
    
    <script type="text/javascript" language="javascript"> 
        //获取js参数
        function getQueryStringRegExp(name) {
            var reg = new RegExp("(^|\\?|&)"+ name +"=([^&]*)(\\s|&|$)", "i");
            if (reg.test(location.href)) return unescape(RegExp.$2.replace(/\+/g, " ")); 
            return "0"; 
        }
       var SelectMode=getQueryStringRegExp('SelectMode');//0:单选 1:多选
       
       function SetDefaultValue()
        {
            var DefaultObject = window.dialogArguments;
            if (DefaultObject != undefined && DefaultObject != null)
            {
                var DefaultValue=DefaultObject.name;
                if(DefaultValue !='' && DefaultValue.indexOf('|') !=-1)
                {
                   var listID= DefaultValue.split('|')[0].split(',');
                   var listName= DefaultValue.split('|')[1].split('，');
                   if(listID.length==listName.length)
                   {
                        for(var i=0;i<listID.length;i++)
                        {
                            var id=listID[i];
                            var name=listName[i];
                            if(id !='' && name !='')
                            {AddTableFromTree(name,id);}
                        }
                   }
                }
            }
        }

        function GetStrValue() {


            var deptNames = "", deptIDs = "";
            var nodeList = document.getElementById("selectList").childNodes;
            for (var i = 0; i < nodeList.length; i++) {
                if (nodeList[i].tagName && nodeList[i].tagName.toLowerCase() == "table") {
                    deptIDs += nodeList[i].id + ",";
                    deptNames += nodeList[i].childNodes[0].childNodes[0].childNodes[0].innerHTML + ",";
                }
            }
            if (deptNames.length > 0)
                deptNames = deptNames.substring(0, deptNames.length - 1);
            if (deptIDs.length > 0)
                deptIDs = deptIDs.substring(0, deptIDs.length - 1);


            if (window.opener) {
                window.opener.document.getElementById("windowOpenReturnValue").value = deptIDs + "$" + deptNames;
                window.opener.windowOpenReturnValueShowInfo();
             }
            else { window.returnValue = deptIDs + "$" + deptNames; }
            window.close();
        }

        function RemoveTable(tableName)
        {
            var divObj = document.getElementById("selectList");
            var tableObj = document.getElementById(tableName);
            divObj.removeChild(tableObj);
        }
        
        function AddTableFromTree(nodeName,nodeValue)
        {
            if (!FilterTable(nodeValue))
            {
                var divObj = document.getElementById("selectList");
                //table
                var table = document.createElement("table");
                table.setAttribute("id",nodeValue);
                table.setAttribute("width","250px");
                table.setAttribute("cellpadding","4");
                table.setAttribute("title","点击上移");
                table.onmouseover = new Function("this.className='supover'");
                table.onmouseout = new Function("this.className='supout'");
                table.onclick = new Function("MoveUpTable(this)");
                //tbody
                var tbody = document.createElement("tbody");
                //tr    
                var row = document.createElement("tr");    
                //td1
                var cell_1 = document.createElement("td");
                cell_1.setAttribute("width","90%");
                //td2
                var cell_2 = document.createElement("td");
                cell_2.setAttribute("width","10%");
                //colseBtn
                var colseBtn = document.createElement("input");
                colseBtn.type = "button";
                colseBtn.setAttribute("value","×");
                colseBtn.style.width = "20px";
                colseBtn.style.height = "20px";
                colseBtn.className = "input_1";
                colseBtn.onclick = new Function("RemoveTable('" + nodeValue + "')");
                colseBtn.setAttribute("title","点击删除");

                //文本节点
                var textNode = document.createTextNode(nodeName);
                cell_1.appendChild(textNode);
                cell_2.appendChild(colseBtn);
                row.appendChild(cell_1);
                row.appendChild(cell_2);
                tbody.appendChild(row);
                cell_2.appendChild(colseBtn);
                table.appendChild(tbody);
                divObj.appendChild(table);
            }
        }
        function FilterTable(nodeValue)
        {
            var findFlag = false;
            var nodeList = document.getElementById("selectList").childNodes;
            
            //单选模式
            if(SelectMode=="0")
            {
                var divObj = document.getElementById("selectList");
                divObj.innerHTML="";
                return findFlag;
            }
                
            for(var i = 0; i < nodeList.length; i++) 
            {
                if (nodeList[i].id == nodeValue)
                {
                    findFlag = true;
                }
            }
            return findFlag;
        }
        
        function MoveUpTable(tableObj)
        {
            if (tableObj.parentNode.firstChild != tableObj)
            {
                tableObj.className = "supout";
                tableObj.parentNode.insertBefore(tableObj, tableObj.previousSibling);
            }
        }
    </script>
    
    <style type="text/css">
    .supman{ color:black}
    .supman:hover { color: white; background-color:ActiveCaption ;}
    .supout{ color:black}
    .supover { color: white; background-color:ActiveCaption ;}
    .input_1 {border:1px solid #98B9C0;}
    </style>
</head>
<body onload="SetDefaultValue();">
    <form id="form1" runat="server" defaultfocus="DeptTree">
        <table>
            <tr>
                <td style="vertical-align: top; width: 250px;">
                    <table class="input_1">
                        <tr>
                            <td>
                                <div style="height: 450px; width: 248px; overflow: auto; overflow-x: hidden">
                                   <%-- <asp:TreeView ID="DeptTree" runat="server" ExpandDepth="1" Width="100%" CollapseImageUrl="~/skin/WebResourcedown.gif"
                                        ExpandImageUrl="~/skin/WebResource.gif" NoExpandImageUrl="~/skin/WebResourceExpand.gif" style="font-size:12px; line-height:150%;">
                                    </asp:TreeView>--%>
                                     <asp:TreeView ID="DeptTree" runat="server" ExpandDepth="1" Width="100%" CollapseImageUrl="~/Images/skin/WebResourcedown.gif"
                                        ExpandImageUrl="~/Images/skin/WebResourceExpand.gif" NoExpandImageUrl="~/Images/skin/WebResource.gif" style="font-size:12px; line-height:150%;">
                                    </asp:TreeView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 40px; text-align: center; vertical-align: middle">
                    <img id="arrowImg" src="../../Images/skin/arrow_left.png" />
                </td>
                <td style="vertical-align: top">
                    <div id="selectList" class="input_1" style="height: 450px; width: 270px; overflow: auto;font-size:12px; line-height:150%;">
                    </div>
                </td>
            </tr>
            <tr style="height : 40px; vertical-align: middle;">
                <td align="right">
                    <img src="../../Images/skin/Yes.png" style="cursor: hand;" 
                        onclick="GetStrValue()" alt="确 定"  width="30" height="30"/></td>
                <td>
                </td>
                <td>
                    <img src="../../Images/skin/No.png" style="cursor: hand;" 
                        onclick="window.close()" alt="关 闭" width="30" height="30"/></td>
            </tr>
        </table>
    </form>
</body>
</html>
