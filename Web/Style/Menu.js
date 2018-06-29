// JavaScript Document

function Hidden(){
  var S=document.getElementById("MenuS");
  var V=document.getElementById("MenuH");
  if(S.style.display=="block"){
	  S.style.display="none"
	  V.style.display="block";
	  }
	  else{
		  S.style.display="none"
		  V.style.display="block";
		  }
}

function Visible(){
  var S=document.getElementById("MenuS");
  var V=document.getElementById("MenuH");
  if(V.style.display=="block"){
	  V.style.display="none";
	  S.style.display="block";
	  }
	  else{
		  V.style.display="none"
		  }
   }

