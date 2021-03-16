$(document).ready(function () {
    resetAll();
    $("#icontact1").click(function () {
        icontactClick(1)
    });
    $("#icontact2").click(function () {
        icontactClick(2)
    });
    $("#icontact0").click(function () {
        icontactClick(0)
    })
});

function resetAll() {
    var l = parseInt($("#divContact0").css("bottom"));
    var m = parseInt($("#divContact1").css("bottom"));
    var j = parseInt($("#divContact2").css("bottom"));
    if ((l > 0)) {
        $("#divContact0").css("bottom", -100)
    }
    if ((m > 0)) {
        $("#divContact1").css("bottom", -100)
    }
    if ((j > 0)) {
        $("#divContact2").css("bottom", -100)
    }

}
function icontactClick(o) {
    var l = parseInt($("#divContact0").css("bottom"));
    var m = parseInt($("#divContact1").css("bottom"));
    var j = parseInt($("#divContact2").css("bottom"));
    if ((l > 0) && (o != 0)) {
        $("#divContact0").css("bottom", -100)
    }
    if ((m > 0) && (o != 1)) {
        $("#divContact1").css("bottom", -100)
    }
    if ((j > 0) && (o != 2)) {
        $("#divContact2").css("bottom", -100)
    }
    var n = parseInt($("#divContact" + o + "").css("bottom"));
    var k = $("#line_Right" + o + "").offset().left;
    var i = $("#line_Right" + o + "").css("width");
    $("#divContact" + o + "").css("width", i);
    $("#divContact" + o + "").css("left", k);
    if (o == 0) {
        var p = varButton0
    } else {
        if (o == 1) {
            var p = varButton1
        } else {
            if (o == 2) {
                var p = varButton2
            }
        }
    }
    if (n < 0) {
        //n = 180 - 38 * (6 - p);
        n = 48 * p - 50;
        $("#divContact" + o + "").css("bottom", n);
        $("#divContact" + o + "").css("display", "block")
    } else {
        $("#divContact" + o + "").css("bottom", -100)
    }
}
function animateBottomFinished() { }
function animateBottomFinished() { }
var ls;
if (window.localStorage) {
    ls = localStorage
}
var cartJson = {};
var ua = navigator.userAgent.toLowerCase();
$.transitionEnd = (function () {
    var b = (function () {
        var e = document.createElement("ceshi"),
        a = {
            WebkitTransition: "webkitTransitionEnd",
            MozTransition: "transitionend",
            OTransition: "oTransitionEnd otransitionend",
            transition: "transitionend"
        },
        f;
        for (f in a) {
            if (e.style[f] !== undefined) {
                return a[f]
            }
        }
    }());
    return b
})();
$(function () {
    $("input[type=text]").focus(function () {
        $("#foot777er").hide()
    });
    $("input[type=text]").focusout(function () {
        $("#foot777er").show()
    });
    $("textarea").focus(function () {
        $("#foot777er").hide()
    });
    $("textarea").focusout(function () {
        $("#foot777er").show()
    });
    $("input[type=number]").focus(function () {
        $("#foot777er").hide()
    });
    $("input[type=number]").focusout(function () {
        $("#foot777er").show()
    })
});

// JavaScript Document
function loadjscssfile(filename, filetype) {

    if (filetype == "js") {
        var fileref = document.createElement('script');
        fileref.setAttribute("type", "text/javascript");
        fileref.setAttribute("src", filename);
    } else if (filetype == "css") {

        var fileref = document.createElement('link');
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", filename);
    }
    if (typeof fileref != "undefined") {
        document.getElementsByTagName("head")[0].appendChild(fileref);
    }

}

function callBackvardoFJalert() { ////jalert 的回掉函数
}

function doInfoAlert_Msg(varUserID) {
    var varHeadURL = "";
    if ((varServiceURL == null) || varServiceURL == "") {
        varHeadURL = "https://Service.eggsoft.cn";
    }
    else {
        varHeadURL = varServiceURL;
    }
    var url = varHeadURL + "/Pub/doInfoAlert_Msg.asmx/doInfoAlert_MsgAction?strpub_Int_Session_CurUserID=" + varUserID;

    var variable1result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp",
        //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp9Callback",
        //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            variable1result = decodeURIComponent(json.InfoMsg);
            if (variable1result == '') { } else {
                loadjscssfile("/Templet/01WYJS/js/jquery.alert.v1.2/jquery.alert.css?version=css201709121928", "css");
                loadjscssfile("/Templet/01WYJS/js/jquery.alert.v1.2/jquery.easydrag.js?version=js201709121928", "js");
                loadjscssfile("/Templet/01WYJS/js/jquery.alert.v1.2/jquery.alert.js?version=js201709121928", "js");

                loadjscssfile("/Templet/01WYJS/js/layer2.0.js?version=js201709121928", "js");
                loadjscssfile("/Templet/01WYJS/Css/layer2.0.css?version=css201709121928", "css");


                //信息框
                setTimeout("layer.open({ content: '" + variable1result + "', btn: 'OK', time: 10 })", 1000);
                ////动态的加载的js 会延迟加载 无奈出此下策 定时执行
                //setTimeout("if ($.alerts != null) $.alerts.okButton = '确定';jAlert('"+variable1result+"', '提示', callBackvardoFJalert);", 1000)


            }

            return;
        },
        error: function () { }
    });

    return 1;
}