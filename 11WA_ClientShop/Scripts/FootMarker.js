


var jsvarUserID; var jsvarVisitInfo;

function getLocation(varUserID, varVisitInfo) {
    if (navigator.geolocation) {

        jsvarUserID = varUserID;
        jsvarVisitInfo = varVisitInfo;
        testAsync(jsvarUserID, 111, 222, jsvarVisitInfo)
        navigator.geolocation.getCurrentPosition(showMap, handleError, { enableHighAccuracy: true, maximumAge: 3600000 });//3600000  一个小时
    } else {
        alert('您的浏览器不支持使用HTML 5来获取地理位置服务');
    }

    ///maximumAge 允许返回指定时间内的缓存结果。如果此值为0，则浏览器将立即获取新定位结果。
}

function showMap(value) {
    var longitude = value.coords.longitude;
    var latitude = value.coords.latitude;

    testAsync(jsvarUserID, longitude, latitude, jsvarVisitInfo)
}

function handleError(value) {
    switch (value.code) {
        case 1:
            alert('位置服务被拒绝');
            break;
        case 2:
            //alert('暂时获取不到位置信息');
            break;
        case 3:
            alert('获取信息超时');
            break;
        case 4:
            alert('未知错误');
            break;
    }
}



function testAsync(userid, longitude, latitude, varneedSetVisitInfo) {

    var url555 = "userid=" + userid + "&longitude=" + longitude + "&latitude=" + latitude + "&needSetVisitInfo=" + escape(varneedSetVisitInfo);

    var url = "https://service.eggsoft.cn/User/FootMark.asmx/_FootMark_Html5_Save?" + url555;

    //alert(url);
    var result = -1;
    $.ajax({
        type: "get",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp7Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = parseInt(json.ErrorCode);
            longitude = parseFloat(json.BaiDugpsY);
            latitude = parseFloat(json.BaiDugpsX);
            /*
            var map = new BMap.Map('map');
            var point = new BMap.Point(longitude, latitude);    // 创建点坐标
            map.centerAndZoom(point, 22);
            var marker = new BMap.Marker(new BMap.Point(longitude, latitude));  // 创建标注
            map.addOverlay(marker);              // 将标注添加到地图中
            */
            //alert("ok  insert");
            //$.alerts.okButton = "确定";
            ////jAlert("此商品已下架！", '提示', callBackvardoT);

            //if (result == -1) {
            //    jAlert("购物车添加失败！", '提示');
            //    // alert ("购物车添加失败！");
            //}
            //else if (result == -2) {
            //    jAlert("购物车添加失败！购买限制，在订单中已存在！", '提示');
            //    // alert ("购物车添加失败！购买限制，在订单中已存在");
            //}
            //else if (result == -3) {
            //    jAlert("购物车添加失败！购物券已被使用", '提示');
            //    //alert ("购物车添加失败！购物券已被使用");
            //}
            return;
            
        },
        error: function () {
            // debugger;
            // alert('fail');
        }
    });


    return result;
}
