// JavaScript Document

function nTabs(thisObj,Num){
if(thisObj.className == "active")return;
var tabObj = thisObj.parentNode.id;
var tabList = document.getElementById(tabObj).getElementsByTagName("span");
for(i=0; i <tabList.length; i++)
{
  if (i == Num)
  {
   thisObj.className = "SlideA"; 
      document.getElementById(tabObj+"_Content"+i).style.display = "inline-block";
  }else{
   tabList[i].className = ""; 
   document.getElementById(tabObj+"_Content"+i).style.display = "none";
  }
} 
}