//js,alert提示语
var AlertMessageObj = new Object();
AlertMessageObj.UnEditAlert = "不能对已经审核的数据进行修改操作";
AlertMessageObj.UnCancelAlert = "不能对已经审核的数据进行作废操作";
AlertMessageObj.UnApproveAlert = "不能对已经审核的数据进行审核操作";

 /* 全选/反选 */
  function Common_CheckboxChoose(choosed)
	{
		var elementOnes=document.form1.elements;
		var boolAll=true;
		var controlAll; 
		
		for(var i=0;i<elementOnes.length;i++)
		{
			if(choosed.id.indexOf('chkAll')!=-1)
			{
				elementOnes[i].checked=choosed.checked;
				controlAll=elementOnes[i];
			}
			else if(choosed.id.indexOf('chk')!=-1)
			{
				if(elementOnes[i].id.indexOf('chkAll')!=-1)
				{
					controlAll=elementOnes[i];
					controlAll.checked=true;
				}
				if(elementOnes[i].checked==false)
				{
					boolAll=false;
				} 
			}
		}	
		controlAll.checked=boolAll;	
	}
	
	
	/* 全选/反选 */
	function Common_CheckAll(checkAllBox)
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

/*
通用表单验证类
输入：
Common_CheckFormObj.NoEmptyCheckStr="domId||ErrorAlert_$$_domId2||ErrorAlert2";
输出：
true|false
*/
var Common_CheckFormObj = new Object();

//批量判断是否为空
//参数emptyCheckStr="domId||ErrorAlert_$$_domId2||ErrorAlert2";
Common_CheckFormObj.BatCheckIsEmptyAndAlert = function(emptyCheckStr){
    if(emptyCheckStr !=""){
       var arr = emptyCheckStr.split("_$$_");
       for(var i=0;i<arr.length;i++){
           var arrItem = arr[i].split("||");
            var obj = document.getElementById(arrItem[0]);
            if(obj){
                if(Common_CheckFormObj.ClearSpace(obj.value) == ""){
                    alert(arrItem[1]);
                    Common_CheckFormObj.FocusObj(obj);
                    return false;
                }
          }
       }
    }
    return true;
}

//是否为空
Common_CheckFormObj.CheckIsEmptyAndAlert = function(obj,errorAlert){
    if(Common_CheckFormObj.ClearSpace(obj.value) == ""){
        alert(errorAlert);
        Common_CheckFormObj.FocusObj(obj);
        return false;
    }
    return true;
}

//是否为价格
Common_CheckFormObj.CheckIsPriceAndAlert = function(obj,errorAlert){
    if(Common_CheckFormObj.CheckIsPrice(Common_CheckFormObj.ClearSpace(obj.value))==false){
        alert(errorAlert);
        Common_CheckFormObj.FocusObj(obj);
        return false;
    }
    return true;
}

//是否为全部数字（包括小数）
Common_CheckFormObj.CheckIsAllNumberAndAlert = function(obj,errorAlert){
    if(Common_CheckFormObj.CheckIsAllNumber(Common_CheckFormObj.ClearSpace(obj.value))==false){
        alert(errorAlert);
        Common_CheckFormObj.FocusObj(obj);
        return false;
    }
    return true;
}


//去除前后空格
Common_CheckFormObj.ClearSpace = function(str){
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

//获取焦点
Common_CheckFormObj.FocusObj = function(obj){
    try{
        obj.focus();
        obj.select();
    }catch(e){}
}

//判断正整数
Common_CheckFormObj.CheckNumber = function(str){
    var re = /^\d+?$/;
    return re.test(str);
}

//判断正负整数
Common_CheckFormObj.CheckNumberWidthAll = function(str){
    var re = /^-?[0-9]+?$/;
    return re.test(str);
}    

//判断价格
Common_CheckFormObj.CheckIsPrice = function(str){
    var re = /^\d+(\.\d+)?$/;
    return re.test(str);
}  

//限制TextArea的字数
Common_CheckFormObj.CutTextArea = function(obj,length){
    if(obj.value.length > length){
        obj.value=obj.value.substring(0,length);
    }
}

//判断数字（包括小数）
Common_CheckFormObj.CheckIsAllNumber = function(str){
    var re = new RegExp("^([+-]?)\\d*\\.?\\d+$");
    return re.test(str);
} 


//ie6,ie7版本下打开window.showModalDialog
function Common_ShowModalDialogInIe6Ie7(url,width,height){
    var v = jQuery.browser.version;
    //alert(v);
    if(v == "6.0"){
        width += 60;
        height += 60;
    }
    window.showModalDialog(url,self,"edge:raised:1;help:0;resizable:1;dialogWidth:"+width+"px;dialogHeight:"+height+"px;");	
}

    function jquery_validate(){
        var validateReslut = true;
        $(".jquery_validate_empty").each(
            function(){
                var id = $(this).attr("id");
                var flag = id.split('$')[0].toLowerCase();
                var flagId = id.split('$')[1];
                if(flag == "input"){
                    if($("input[id='"+flagId+"']").val() == ""){
                        alert($(this).text());
                        $("input[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }else if(flag == "select"){
                    if($("select[id='"+flagId+"']").val() == ""){
                        alert($(this).text());
                        $("select[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }else if(flag == "radio"){
                    
                }   
            }
        );
        
        if(validateReslut == false){return false;}
        
        $(".jquery_validate_regx").each(
            function(){
                var id = $(this).attr("id");
                var flagId = id.split('$')[0];
                var regxType = id.split('$')[1].toLowerCase();
                var str = $("input[id='"+flagId+"']").val();
                    str = $.trim(str);
                if(regxType == "int_"){
                    var re = new RegExp("^[0-9]+?$");
                    if(re.test(str) == false){
                        alert($(this).text());
                        $("input[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }else if(regxType == "_int"){
                    var re = new RegExp("^[-][0-9]+?$");
                    if(re.test(str) == false){
                        alert($(this).text());
                        $("input[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }else if(regxType == "int"){
                    var re = new RegExp("^-?[0-9]+?$");
                    if(re.test(str) == false){
                        alert($(this).text());
                        $("input[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }else if(regxType == "float_"){
                    var re = new RegExp("^\\d*\\.?\\d+$");
                    if(re.test(str) == false){
                        alert($(this).text());
                        $("input[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }else if(regxType == "_float"){
                    var re = new RegExp("^[-]\\d*\\.?\\d+$");
                    if(re.test(str) == false){
                        alert($(this).text());
                        $("input[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }else if(regxType == "float"){
                    var re = new RegExp("^([+-]?)\\d*\\.?\\d+$");
                    if(re.test(str) == false){
                        alert($(this).text());
                        $("input[id='"+flagId+"']").focus();
                        validateReslut = false;
                        return false;
                    }
                }
            }
        );
        return validateReslut;
    }
    
//获取checkbox选中行的中某个td的值    
function Common_GetCheckBoxTdText(tableId,tdIndex){
    var returnArr= new Array();
    $("input[@type='checkbox']","table[id='"+tableId+"']").each(
        function(){
            if($(this).attr('checked') == true){
                var tr = $(this).parent().parent();
                returnArr.push(tr.children().eq(tdIndex).text());
            }
        }
    );
    return returnArr;
}
//获取checkbox选中行的中某个td的值    
function Common_GetAllCheckBoxTdText(tableId,tdIndex){
    var returnArr= new Array();
    $("input[@type='checkbox']","table[id='"+tableId+"']").each(
        function(){
                var tr = $(this).parent().parent();
                returnArr.push(tr.children().eq(tdIndex).text());
        }
    );
    return returnArr;
}


//控制iframe内的列表显示模式（包含独立窗口时）
$(document).ready(
    function(){
        if($("#div_list").length>0){
            var w = window.screen.width-30;
            var w2 = window.screen.width-200;
            if(window.self == window.top){
                $("#div_list").css("width",w);
            }else{
                $("#div_list").css("width",w2);
            }
            var h = window.screen.height-500;
            if(h>500){$("#div_list").css("height",h);}
        }
   }
);