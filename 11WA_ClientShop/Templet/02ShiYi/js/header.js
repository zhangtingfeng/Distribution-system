$(document).ready(function () {
    $("#iqr").click(function () {
        var b = popupShow_Head()
    })
});
var removeTouch = function (c) {
    var d = c.target;
    node = document.querySelector(".m-popup");
    if (orientation != 90 || orientation != -90) {
        c.preventDefault()
    }
};
var delPopup = function (c) {
    var d = c.target;
    if (d.className == "m-popup") {
        popupRemove()
    }
};
var updateOrientation = function () {
    var b = window.orientation;
    if (b == 90 || b == -90) {
        document.body.removeEventListener("touchmove", removeTouch, false)
    } else {
        document.body.addEventListener("touchmove", removeTouch, false)
    }
};
var popupShow_Head = (function () {
    return function (d) {
        document.documentElement.style.overflow = "hidden";
        document.body.style.overflow = "hidden";
        document.body.addEventListener("click", delPopup, false);
        document.body.addEventListener("touchmove", delPopup, false);
        document.body.addEventListener("touchstart", delPopup, false);
        var c = document.createElement("div");
        c.innerHTML = "<img width='300' height='300' src='/Templet/02ShiYi/skin/images/jan140328202201.jpg'>";
        c.className = "m-popup";
        document.body.appendChild(c);
        return c
    }
})();
var popupRemove = (function () {
    return function () {
        var b = document.querySelector(".m-popup");
        b.parentNode.removeChild(b);
        document.documentElement.style.overflow = "auto";
        document.body.style.overflow = "auto";
        document.body.removeEventListener("click", delPopup, false);
        document.body.removeEventListener("touchmove", delPopup, false);
        document.body.removeEventListener("touchstart", delPopup, false)
    }
})();
function WeiXin_shareFriend() {
    WeixinJSBridge.invoke("sendAppMessage", {
        appid: WeiXin_appidAllPage,
        img_url: WeiXin_imgAllPageUrl,
        img_width: "640",
        img_height: "640",
        link: WeiXin_lineAllPageLink,
        desc: WeiXin_descAppPageContent,
        title: WeiXin_shareAppAllPageTitle
    },
    function (b) {
        _report("send_msg", b.err_msg)
    })
}
function weixin_ShareTimeline() {
    WeixinJSBridge.invoke("shareTimeline", {
        appid: WeiXin_appidAllPage,
        img_url: WeiXin_imgAllPageUrl,
        img_width: "640",
        img_height: "640",
        link: WeiXin_lineAllPageLink,
        desc: WeiXin_descAppPageContent,
        title: WeiXin_shareAppAllPageTitle
    },
    function (b) {
        _report("timeline", b.err_msg)
    })
}
function weixin_shareWeibo() {
    WeixinJSBridge.invoke("shareWeibo", {
        content: WeiXin_descAppPageContent,
        url: WeiXin_lineAllPageLink
    },
    function (b) {
        _report("weibo", b.err_msg)
    })
}
document.addEventListener("WeixinJSBridgeReady",
function onBridgeReady() {
    WeixinJSBridge.on("menu:share:appmessage",
    function (b) {
        WeiXin_shareFriend()
    });
    WeixinJSBridge.on("menu:share:timeline",
    function (b) {
        weixin_ShareTimeline()
    });
    WeixinJSBridge.on("menu:share:weibo",
    function (b) {
        weixin_shareWeibo()
    })
},
false);
window.shareData = {
    imgUrl: WeiXin_imgAllPageUrl,
    tImgUrl: WeiXin_imgAllPageUrl,
    fImgUrl: WeiXin_imgAllPageUrl,
    wImgUrl: WeiXin_imgAllPageUrl,
    timeLineLink: WeiXin_lineAllPageLink,
    sendFriendLink: WeiXin_lineAllPageLink,
    weiboLink: WeiXin_lineAllPageLink,
    tTitle: WeiXin_shareAppAllPageTitle,
    tContent: WeiXin_descAppPageContent,
    fTitle: WeiXin_shareAppAllPageTitle,
    fContent: WeiXin_descAppPageContent,
    wContent: WeiXin_descAppPageContent
};