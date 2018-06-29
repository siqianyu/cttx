$(document).ready(function(){
		
		$('.footer-menu li').click(function(){
			$(this).addClass('current').siblings('li').removeClass('current');
		})
		
	})