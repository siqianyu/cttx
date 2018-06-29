$(document).ready(function(){
	$('.footer ul li span').click(function(){
		$(this).addClass('active').parent('li').siblings('li').find('span').removeClass('active');
	})
	})