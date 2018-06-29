function DrawImage(ImgD,sw,sh)

{
	var _width=sw;
	var _height=sh;
    var image = new Image();
    image.src = ImgD.src;
    if (image.width > 0 && image.height > 0){
        flag2 = true;
        if (image.width / image.height >= _width / _height){
            if (image.width > _width){
                ImgD.width = _width;
                ImgD.height = (image.height * _width) / image.width;
            }else{
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        }else{     
            if (image.height > _height){
                ImgD.height =_height;
                ImgD.width = (image.width * _height) / image.height;
            }else{
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        }
    }
}


function showDiv(leftWidth,topHeight,gooestype){
var Sys = {};
var ua = navigator.userAgent.toLowerCase();
if (window.ActiveXObject)
 Sys.ie = ua.match(/msie ([\d.]+)/)[1]
else if (document.getBoxObjectFor)
 Sys.firefox = ua.match(/firefox\/([\d.]+)/)[1]
else if (window.MessageEvent && !document.getBoxObjectFor)
 Sys.chrome = ua.match(/chrome\/([\d.]+)/)[1]
else if (window.opera)
 Sys.opera = ua.match(/opera.([\d.]+)/)[1]
else if (window.openDatabase)
 Sys.safari = ua.match(/version\/([\d.]+)/)[1];


if(Sys.ie)      //IE
{
    if(Sys.ie == '7.0' || Sys.ie == "8.0")
    {
      if(gooestype=="rm"){//热卖任务
        document.write("<div style='position:relative;left:0px;top:"+(topHeight - 30)+"px; '><div style='position:absolute;z-index:999;left:0px;top:0px; '><img src='/images/rm.gif' border='0' /></div></div> ");
      }else if(gooestype=="xp"){//新品
        document.write("<div style='position:relative;left:"+(leftWidth-44)+"px;top:0px;'><div style='position:absolute;z-index:999;left:0px;top:0px; '><img src='/images/xp.gif' border='0'/></div></div> ");
      }else if(gooestype=="cx"){
        document.write("<div style='position:relative;left:"+(leftWidth-41)+"px;top:"+ (topHeight - 42) +"px;'><div style='position:absolute;z-index:999;left:0px;top:0px; '><img src='/images/cx.gif' border='0'/></div></div> ");
      }
    }
    else if(Sys.ie == '6.0')
    {
        if(gooestype=="rm"){//热卖任务
        document.write("<div style='position:relative;left:0px;top:"+(topHeight - 30)+"px; '><div style='position:absolute;z-index:999;left:0px;top:0px; '><img src='/images/rm.gif' border='0' /></div></div> ");
      }else if(gooestype=="xp"){//新品
        document.write("<div style='position:relative;left:"+(leftWidth-44)+"px;top:0px;'><div style='position:absolute;z-index:999;left:0px;top:0px; '><img src='/images/xp.gif' border='0'/></div></div> ");
      }else if(gooestype=="cx"){
        document.write("<div style='position:relative;left:"+(leftWidth-41)+"px;top:"+ (topHeight - 42) +"px;'><div style='position:absolute;z-index:999;left:0px;top:0px; '><img src='/images/cx.gif' border='0'/></div></div> ");
      }
    }
}
if(Sys.firefox)         //火狐
{
    
}
if(Sys.chrome)          //谷歌
{
     
}
if(Sys.opera)
{
    
}
if(Sys.safari)          //谷歌
{
    
}
}
