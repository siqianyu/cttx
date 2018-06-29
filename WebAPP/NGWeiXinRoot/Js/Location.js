//必须先引入百度的api
//<script src="http://api.map.baidu.com/api?v=1.4" type="text/javascript"></script>

var city = '北京';
//定位经纬度并根据经纬度获取城市信息
var getLocation = function (successFunc, errorFunc) { //successFunc获取定位成功回调函数，errorFunc获取定位失败回调

    //默认城市
    //$.cookie('VPIAO_MOBILE_CURRENTCITY', city, { expires: 1, path: '/' });

    // LBS兼容性 begin
    if (navigator.userAgent.indexOf("Html5Plus")<0){
        //TODO 使用浏览器自带的HTML5 的方式获取地理位置
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                        showPosition(position);
                    },
                function (error) {
                    switch (error.code) {
                        case 1:
                            alert("位置服务被拒绝。");
                            break;
                        case 2:
                            alert("暂时获取不到位置信息。");
                            break;
                        case 3:
                            alert("获取位置信息超时。");
                            break;
                        default:
                            alert("未知错误。");
                            break;
                    }
                    city = '北京';

                    //默认城市
                    //$.cookie('VPIAO_MOBILE_CURRENTCITY', city, { expires: 1, path: '/' });
                    if (errorFunc != undefined)
                        errorFunc(error);
                }, { timeout: 5000, enableHighAccuracy: true });
            } else {
                alert("你的浏览器不支持获取地理位置信息。");
                if (errorFunc != undefined)
                    errorFunc("你的浏览器不支持获取地理位置信息。");
            }
    } else {
        document.addEventListener('plusready', function(){
            plus.geolocation.getCurrentPosition(function (p){
                //alert( "Geolocation\nLatitude:" + p.coords.latitude + "\nLongitude:" + p.coords.longitude + "\nAltitude:" + p.coords.altitude );
                showPosition(p);
            }, function(){
                alert( "Geolocation error: " + e.message );
            });
        }, false);
    }
    // LBS兼容性 end
   
    
};

//根据经纬度获取城市信息
var showPosition = function (position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    //var map = new BMap.Map("container");   // 创建Map实例
    var point = new BMap.Point(lon, lat); // 创建点坐标
    var gc = new BMap.Geocoder();
    gc.getLocation(point, function (rs) {
        var addComp = rs.addressComponents;
        city = addComp.city;
        if (city.indexOf('市') == city.length - 1) {
            city=city.substr(0,city.length-1)
        }
        $('#cuCity').html(city);

        //当前定位城市
        $.cookie('VPIAO_MOBILE_CURRENTCITY', city, { expires: 7, path: '/' });
        //alert(addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street);
    });
};

var showPositionError = function (error) {
    switch (error.code) {
        case 1:
            alert("位置服务被拒绝。");
            break;
        case 2:
            alert("暂时获取不到位置信息。");
            break;
        case 3:
            alert("获取位置信息超时。");
            break;
        default:
            alert("未知错误。");
            break;
    }
    city = '北京';
    //默认城市
    //$.cookie('VPIAO_MOBILE_CURRENTCITY', city, { expires: 1, path: '/' });
};


