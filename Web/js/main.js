// JavaScript Document
$(document).ready(function(){
	$(".b_blue").hover(
	   function () {
		 $(".ind_img img").hide();
		 $(".ind_img img").eq(0).show();
		 $(".ind_img").css("left","9px");
		 $(".ind_img").fadeIn("slow"); 
	   },
	   function () {
		 $(".ind_img").fadeOut("slow"); 
	});
	$(".b_green").hover(
	   function () {
		 $(".ind_img img").hide();
		 $(".ind_img img").eq(1).show();
		 $(".ind_img").css("left","18px");
		 $(".ind_img").fadeIn("slow"); 
	   },
	   function () {
		 $(".ind_img").fadeOut("slow"); 
	});
	$(".b_orange").hover(
	   function () {
		 $(".ind_img img").hide();
		 $(".ind_img img").eq(2).show();
		 $(".ind_img").css("left","27px");
		 $(".ind_img").fadeIn("slow"); 
	   },
	   function () {
		 $(".ind_img").fadeOut("slow"); 
	});
	$(".b_red").hover(
	   function () {
		 $(".ind_img img").hide();
		 $(".ind_img img").eq(3).show();
		 $(".ind_img").css("left","36px");
		 $(".ind_img").fadeIn("slow"); 
	   },
	   function () {
		 $(".ind_img").fadeOut("slow"); 
	});
});
function sh1(id){
    var val=AppModules_CXPlatform_Index_FJ.getnewsinfo(id).value;
        if(val[0]!="")
        {
            $("#newstitle",window.parent.document).text(val[0]);
            $("#newsly",window.parent.document).text(val[1]);
            $("#newsfbsj",window.parent.document).text(val[2]);
            $("#newsnr",window.parent.document).html(val[3]);
            $("#newsly1",window.parent.document).text(val[1]);
            $("#newszz",window.parent.document).text(val[4]);
	        var sWidth = $(window.parent.document).width();
	        var sTop = (sWidth-657)/2;
	        $(".over_news",window.parent.document).css("left",sTop+"px")
        	
	        var sHeight = $(window.parent.document).height();
	        $("#CoverDiv",window.parent.document).height(sHeight);
	        $("#CoverDiv",window.parent.document).show();
        	
	        $(".over_news",window.parent.document).slideDown("slow"); 
	    }
	}
function sh11(id){
var val=AppModules_CXPlatform_Index_SJ.getnewsinfo(id).value;
    if(val[0]!="")
    {
        $("#newstitle",window.parent.document).text(val[0]);
        $("#newsly",window.parent.document).text(val[1]);
        $("#newsfbsj",window.parent.document).text(val[2]);
        $("#newsnr",window.parent.document).html(val[3]);
        $("#newsly1",window.parent.document).text(val[1]);
        $("#newszz",window.parent.document).text(val[4]);
        var sWidth = $(window.parent.document).width();
        var sTop = (sWidth-657)/2;
        $(".over_news",window.parent.document).css("left",sTop+"px")
    	
        var sHeight = $(window.parent.document).height();
        $("#CoverDiv",window.parent.document).height(sHeight);
        $("#CoverDiv",window.parent.document).show();
    	
        $(".over_news",window.parent.document).slideDown("slow"); 
    }
}
function sh12(id){
var val=AppModules_Government_ListNotice.getnewsinfo(id).value;
    if(val[0]!="")
    {
            $("#newstitle",window.parent.document).text(val[0]);
            $("#newsly",window.parent.document).text(val[1]);
            $("#newsfbsj",window.parent.document).text(val[2]);
            $("#newsnr",window.parent.document).html(val[3]);
            $("#newsly1",window.parent.document).text(val[1]);
            $("#newszz",window.parent.document).text(val[4]);
	        var sWidth = $(window.parent.document).width();
	        var sTop = (sWidth-657)/2;
	        $(".over_news",window.parent.document).css("left",sTop+"px")
        	
	        var sHeight = $(window.parent.document).height();
	        $("#CoverDiv",window.parent.document).height(sHeight);
	        $("#CoverDiv",window.parent.document).show();
        	
	        $(".over_news",window.parent.document).slideDown("slow"); 
    }
}