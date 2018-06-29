function jquery_mouse_hover(tableId){
    if(tableId && typeof(tableId)=="string"){
        $("tr","table[id='"+tableId+"']").each(
            function(){
		            $(this).hover(
                      function(){$(this).css("color","red");},
                      function(){$(this).css("color","");}
                    ); 
            }
        );
    }else{
        $(".jquery_mouse_hover").each(
            function(){
		            $(this).hover(
                      function(){$(this).css("color","red");$(this).css("cursor","hand");},
                      function(){$(this).css("color","");$(this).css("cursor","");}
                    ); 
            }
        );
    }
}

function jquery_mouse_hover_tbody(tableId,bgColor){
    if(!bgColor)(bgColor="#def0fa");
    
    $("tbody tr","table[id='"+tableId+"']").each(
        function(){
	            $(this).hover(
                  function(){$(this).css("backgroundColor",bgColor);},
                  function(){$(this).css("backgroundColor","");}
                ); 
        }
    );
}

function jquery_mouse_move(flagType,flagId,bgColor){
    if(!bgColor)(bgColor="#def0fa");
    
    if(flagType=="onmouseover"){
        $("#"+flagId).css("backgroundColor",bgColor);
    }else{
        var checked = $("input[@type=checkbox]","tr[id='"+flagId+"']").attr('checked');
        if(!checked){$("#"+flagId).css("backgroundColor","");}
        if(checked==false){
            $("#"+flagId).css("backgroundColor","");
        }
    }
}

function jquery_mouse_checkbox_click(flagId,bgColor){
    if(!bgColor)(bgColor="#def0fa");
 
    var checked = $("input[@type=checkbox]","tr[id='"+flagId+"']").attr('checked');
    //alert(checked);
    if(checked==true){
        $("#"+flagId).css("backgroundColor",bgColor);
    }else{
        $("#"+flagId).css("backgroundColor","");
    }
    
    

}


function jquery_init_tr_class(){
    if(document.getElementById('table_list_repeater'))
    {
        var i=2;
        $("#table_list_repeater tr").each(
            function(){
                if(i%2==0){
                    $(this).attr("class","tr_even");
                }
                i++;
            }
        );
    }
}
$(document).ready(function(){jquery_init_tr_class();});