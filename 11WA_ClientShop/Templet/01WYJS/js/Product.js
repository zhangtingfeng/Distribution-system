function ShareGoodsFunction() {///商品分享朋友圈的回调事件
    //varGoodID = varGoodID;///在主页面上已定义变量

    var url = varServiceURL + "/Pub/doVisitGood.asmx/doShareGoodAction?strIntGoodID=" + varGoodID + "&strUserID=" + varUserID + "&strShopClientID=" + varShopClientID;
    //alert("url=" + url);
    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201601072055Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            ///result = parseInt(json.ErrorCode);
            return;
        },
        error: function () {
        }
    });


    return result;
}

