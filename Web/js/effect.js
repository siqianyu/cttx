// JavaScript Document
function backchange(a){
	a.style.background = "#E4F6FF";
}
function restore(b){
	b.style.background = "#fff";
}
function green_backchange(a){
	a.style.background = "#D9F4D6";
}
function green_restore(b){
	b.style.background = "#fff";
}
function red_backchange(a){
	a.style.background = "#FADCDC";
}
function red_restore(b){
	b.style.background = "#fff";
}
function change1(){
	if(document.getElementById("based_on1").style.display == "block"){
		document.getElementById("based_on1").style.display = "none";
		document.getElementById("based1").style.background = "url(../skin/blue/images/button_left01.jpg)";
	}
	else{
		document.getElementById("based_on1").style.display = "block";
		document.getElementById("based1").style.background = "url(../skin/blue/images/button_left02.jpg)";
	}
}