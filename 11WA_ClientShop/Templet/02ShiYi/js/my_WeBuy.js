


function showMyInfoNum(varHtmlID, varInfomarkerNeedShow, varUserID, varShopClientID, varNetUserSafeCode, varServiceURL) {
    try {
        if (document.getElementById(varHtmlID) == null) return;


        var varURL = varServiceURL + "/User/WS_UserInfo.asmx/getshowMyInfoNum?strUserID=" + varUserID + "&ShopClientID=" + varShopClientID + "&Infomarker=" + varInfomarkerNeedShow;
        var TSign = hex_md5_8(String(varUserID) + String(varShopClientID) + varNetUserSafeCode + varInfomarkerNeedShow);
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
                  
                    //var varmeHtmlID = ("#" + varHtmlID + "");
                    //console.log(varmeHtmlID + "1 " + getNowFormatDate());
                    //$(varmeHtmlID).fadeOut(0);///jquery自带淡入淡出效果，我建议你用jq来实现此效果。而且兼容性也很好。
                    //console.log(varmeHtmlID + "2 " + getNowFormatDate());
                    //setTimeout('$("' + varmeHtmlID + '").fadeIn(2000);', 1000)////执行 消息提醒
                    //console.log(varmeHtmlID + "3 " + getNowFormatDate());

                }
            },
            error: function () {
                // debugger;
                // alert('fail');
            }
        });


    }
    catch (error) {

    }


}


function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds() + " 从 1970/01/01 至今已过去 " + date.getTime() + " 毫秒";
    return currentdate;
}




function jsonp320170827Callback() {

}