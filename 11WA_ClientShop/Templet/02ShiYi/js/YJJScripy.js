function add() {
    var f = document.getElementById("values");
    var e = f.value;
    e++;
    var d = document.getElementById("MaxOrderNum").value;
    if (e > d) {
        document.getElementById("spans").innerHTML = "商品最大购买数量" + d;
        e = d
    } else {
        document.getElementById("spans").innerHTML = " "
    }
    document.getElementById("values").value = e;
    Show_ReCountRedWalletAndMoneyAuto();
}
function jian() {
    var e = document.getElementById("values");
    var d = e.value;
    d--;
    var f = document.getElementById("MinOrderNum").value;
    if (d < f) {
        document.getElementById("spans").innerHTML = "商品起卖数量" + f;
        d = f
    } else {
        document.getElementById("spans").innerHTML = " "
    }
    document.getElementById("values").value = d;
    Show_ReCountRedWalletAndMoneyAuto();
}
function getBuyCount() {
    var b = document.getElementById("values");
    return b
};


function APPCODE_GetGoodErWeiMaImage(strUploadHttp, strType, strShopID, strhttpUrl, strParentID, strGoodsID, strOperationID) {
    //var url = "http://localhost:26208/PubFile/GoodP_QR.asmx/APPCODE_GetGoodErWeiMaImage";
    var url = strUploadHttp + "/PubFile/GoodP_QR.asmx/APPCODE_GetGoodErWeiMaImage";
    url += "?strType=" + strType + "&strShopID=" + strShopID + "&strhttpUrl=" + strhttpUrl + "&strParentID=" + strParentID + "&strGoodsID=" + strGoodsID + "&strOperationID=" + strOperationID;
     var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        async: false,//如果想同步 async设置为false就可以（默认是true）
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        //jsonpCallback: "jsonp148566Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            var bigimg = document.getElementById("ErWeiMaSao");
            bigimg.src = strUploadHttp + json.GetGoodErWeiMaImage;
            bigimg.style.display = 'block';
            var test = document.getElementById('ErWeiMaSaoHref');
            test.href = strUploadHttp + json.GetGoodErWeiMaImage;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        },
        complete: function (XMLHttpRequest, textStatus) {
            //this; // 调用本次AJAX请求时传递的options参数
        }
    });



    return result;
}
