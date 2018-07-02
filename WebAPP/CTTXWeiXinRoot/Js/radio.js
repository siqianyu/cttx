$(document).ready(function(){
	$('.radio-check').click(function(){
		if($(this).children('i').is('.checked')){
			
		}else{
			$(this).children('i').addClass('checked');
			$(this).children('input').attr('checked',true);
			$(this).siblings('.radio-check').children('i').removeClass('checked');
			$(this).siblings('.radio-check').children('input').attr('checked', false);
		}
	});
	$('.radio-blue').click(function(){
		if($(this).children('i').is('.checked')){
			
		}else{
			$(this).children('i').addClass('checked');
			$(this).children('input').attr('checked',true);
			$(this).siblings('.radio-blue').children('i').removeClass('checked');
			$(this).siblings('.radio-blue').children('input').attr('checked', false);
		}
	});
})
