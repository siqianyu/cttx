// added by 胡晓威 2008.10.15

//判断是否是图片,如果是则显示预览 
function showPic(obj, img)
{
    var url = obj.value;
    //strFilter必须是小写列举
    var strFilter=".jpeg|.jpg|.bmp|.gif|"
    if(url.indexOf(".")>-1)
    {
        var p = url.lastIndexOf(".");
        var strPostfix=url.substring(p,url.length) + '|';        
        strPostfix = strPostfix.toLowerCase();
        if(strFilter.indexOf(strPostfix)>-1)
        {
    	    document.getElementById(img).src = url;
    	    return;
        }
    }
    alert('上传图片格式不正确！');
    document.getElementById(img).src = '../Images/nopic.jpg';
}