

function ShareShopFunction() {///商城分享朋友圈的回调事件  大部分页面通用
    var url = varServiceURL + "/Pub/doVisiDefault.asmx/doShareDefaultAction?strUserID=" + varUserID + "&strShopClientID=" + varShopClientID;

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201601070639Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
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



function QueryString() {
    var name, value, i;
    var str = location.search;
    var num = str.indexOf("?")
    str = str.substr(num + 1);
    var arrtmp = str.split("&");
    for (i = 0; i < arrtmp.length; i++) {
        num = arrtmp[i].indexOf("=");
        if (num > 0) {
            name = arrtmp[i].substring(0, num);
            value = arrtmp[i].substr(num + 1);
            this[name] = value;
        }
    }
}



function doMakeHtml__Pub_03Footer(varGetUseid, varDestionnation_HTML_ID) {
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的

    var url = varServiceURL + "/Pub/doClickThis_HowToGetProduct.asmx/doGet_Pub_03Footer?strvarGetUseid=" + varGetUseid + "&strShopClientID=" + varShopClientID;

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201601070639Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $(varDestionnation_HTML_ID).append('<div style=\"margin:0 auto;text-align:center;\" class=\"wx_loading_icon\"></div>');
        },
        success: function (json) {
            if (json.ErrorCode == "0") {
                //alert(msg);
                $(varDestionnation_HTML_ID).html(decodeURIComponent(json.Pub_03Footer));
                ///如何在js文件中动态加载另一个js文件？
                var oHead = document.getElementsByTagName('HEAD').item(0);
                var oScript = document.createElement("script");
                oScript.type = "text/javascript";
                oScript.src = "/Templet/02ShiYi/js/Footer_common.js?version=js201709121928";
                oHead.appendChild(oScript);
            }
            ///result = parseInt(json.ErrorCode);
            return;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });


}

///页面数字符号提醒 设置偏移量  开始//////////////////////////////////////////////////////////////////////////////////////////////////
///1.获取相对与document的偏移量
function getOffsetSum(ele) {
    var top = 0, left = 0;
    while (ele) {
        top += ele.offsetTop;
        left += ele.offsetLeft;
        ele = ele.offsetParent;
    }
    return {
        top: top,
        left: left
    }
}

///2.获取相对与视口的偏移量(viewpoint)加上页面的滚动量(scroll)
function getOffsetRect(ele) {
    var box = ele.getBoundingClientRect();
    var body = document.body,
      docElem = document.documentElement;
    //获取页面的scrollTop,scrollLeft(兼容性写法)
    var scrollTop = window.pageYOffset || docElem.scrollTop || body.scrollTop,
      scrollLeft = window.pageXOffset || docElem.scrollLeft || body.scrollLeft;
    var clientTop = docElem.clientTop || body.clientTop,
      clientLeft = docElem.clientLeft || body.clientLeft;
    var top = box.top + scrollTop - clientTop,
      left = box.left + scrollLeft - clientLeft;
    return {
        //Math.round 兼容火狐浏览器bug
        top: Math.round(top),
        left: Math.round(left)
    }
}

//兼容性写法

//获取元素相对于页面的偏移
function getOffset(ele) {
    if (ele.getBoundingClientRect) {
        return getOffsetRect(ele);
    } else {
        return getOffsetSum(ele);
    }
}



//$(document).ready(function () {
//    setLocation("Button16_16CheckModifyParento_Board", "Button16_16CheckModifyParent");
//})
function setLocation(varChlid, varBoard) {

    var varPos = getOffset(document.getElementById(varBoard));

    var varSetvarChlid = document.getElementById(varChlid);

    //if (document.getElementById("Label1intInfoAlertMessageExistsCount").innerText == "0") {
    //    varButton16_16CheckModifyParento_Board.style.left = -100;
    //    varButton16_16CheckModifyParento_Board.style.top = varPos.top;
    //}
    //else {
    varSetvarChlid.style.left = varPos.left + "px";
    varSetvarChlid.style.top = varPos.top + "px";
    //}

}
///页面数字符号提醒 设置偏移量  结束//////////////////////////////////////////////////////////////////////////////////////////////////


function isNull(arg1) {
    return !arg1 && arg1 !== 0 && typeof arg1 !== "boolean" ? true : false;
}