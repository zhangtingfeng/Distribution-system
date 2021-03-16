
function showMyInfoNum(varHtmlID, varInfomarkerNeedShow, varShopClientID, varNetShopClientSafeCode, varServiceURL) {
    if (document.getElementById(varHtmlID) == null) return;


    var varURL = varServiceURL + "/User/WS_UserInfo.asmx/getshowMyInfoNum_ByShopClient?ShopClientID=" + varShopClientID + "&Infomarker=" + varInfomarkerNeedShow;
    var TSign = hex_md5_8(String(varShopClientID) + varNetShopClientSafeCode + varInfomarkerNeedShow);
    varURL += "&TSign=" + TSign;

    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        //jsonpCallback: "jsonp320170827Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            if (parseInt(json.CountShow) == 0) {
                document.getElementById(varHtmlID).parentNode.parentNode.style.display = "none";
            }
            else if (parseInt(json.CountShow) > 0) {
                document.getElementById(varHtmlID).parentNode.parentNode.style.display = "block";
                document.getElementById(varHtmlID).innerHTML = "" + json.CountShow;
            }
        },
        error: function () {
            // debugger;
            // alert('fail');
        }
    });

}