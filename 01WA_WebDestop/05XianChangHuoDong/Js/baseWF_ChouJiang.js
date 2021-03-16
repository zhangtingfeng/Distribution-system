//var PATH_ACTIVITY = "/data/scene/";

function getType(o) {
    var _t; return ((_t = typeof (o)) == "object" ? o == null && "null" || Object.prototype.toString.call(o).slice(8, -1) : _t).toLowerCase();
}

if (!Array.prototype.indexOf) {
    //debugger;
    Array.prototype.indexOf = function (b) {
        debugger;
        var aWindowlength = this.length;
        var c = Number(arguments[1]) || 0;
        c = (c < 0) ? Math.ceil(c) : Math.floor(c);
        if (c < 0) {
            c += aWindowlength
        }
        for (; c < aWindowlength; c++) {
            if (c in this && this[c] === b) {
                return c
            }
        }
        return -1
    }
} (function (aWindow, bjQuery, tttt) {
    var varA = getType(aWindow); var varB = getType(bjQuery);
    //debugger;
    bjQuery.fn.scroll_subtitle = function () {
        return this.each(function () {
            var c = bjQuery(this);
            if (c.children().length > 1) {
                aWindow.setInterval(function () {
                    var ewindow = c.children(),
                    d = bjQuery(ewindow[0]);
                    d.slideUp(2000,
                    function () {
                        d.remove().appendTo(c).show()
                    })
                },
                5000)
            }
        })

    };
    bjQuery.preloadImages = function (c, j) {
        if (bjQuery.isArray(c)) {
            var e = c.length;
            if (e > 0) {
                var h = 0,
                d = function () {
                    h++;
                    if (h >= e) {
                        if (typeof j == "function") {
                            aWindow.setTimeout(j, 100)
                        }
                    }
                };
                for (var g = 0; g < e; g++) {
                    var f = new Image();
                    f.onload = d;
                    f.onerror = d;
                    f.src = c[g]
                }
            }
        }
    };
    bjQuery.getUrlParam = function (c) {
        var d = new RegExp("(^|&)" + c + "=([^&]*)(&|$)");
        var e = aWindow.location.search.substr(1).match(d);
        if (e != null) {
            return unescape(e[2])
        }
        return null
    };
    bjQuery.fn.toFillText = function () {
        return this.each(function () {
            var c = bjQuery(this),
            e = c.html(),
            f = c.height();
            c.html("");
            var g = bjQuery("<div>" + e + "</div>").appendTo(c);
            g.css("font-size", "12px");
            for (var d = 12; d < 200; d++) {
                if (g.height() > f) {
                    c.css("font-size", (d - 2) + "px").html(e);
                    break
                } else {
                    g.css("font-size", d + "px")
                }
            }
        })
    };
    bjQuery.fillText = function (c) {
        var e = c.html(),
        f = c.height();
        c.html("");
        var g = bjQuery("<div>" + e + "</div>").appendTo(c);
        g.css("font-size", "12px");
        for (var d = 12; d < 200; d++) {
            if (g.height() > f) {
                c.css("font-size", (d - 2) + "px").html(e);
                break
            } else {
                g.css("font-size", d + "px")
            }
        }
    };

    bjQuery.showPage = function (cwinner_list) {
        var gcwinner_list = bjQuery('<div class="frame-dialog"><iframe frameborder="0" src="' + cwinner_list + '"></iframe><div class="closebutton"></div></div>');
        var e;
        gcwinner_list.appendTo("body").show().on("click", ".closebutton",
        function () {
            gcwinner_list.hide(function () {
                gcwinner_list.remove();
                gcwinner_list = null
            });
            clearInterval(e)
        });
        var d = bjQuery(".frame-dialog").height(),
        f = d - 200;

        e = setInterval(function () {
            bjQuery("iframe").contents().find(".member-list").ready(function () {
                bjQuery("iframe").contents().find(".member-list").css({
                    height: f + "px"
                })
            })
        },
        1000)
    };

    aWindow.WBActivity = {

        showLoginForm: function () {
            bjQuery(".loginform").fadeIn()
        },
        hideLoginForm: function () {
            bjQuery(".loginform").fadeOut()
        },
        showLoading: function () {
            bjQuery(".loader").fadeIn()
        },
        hideLoading: function () {
            bjQuery(".loader").fadeOut()
        }
    };
    bjQuery(function () {
        bjQuery(".top_title").scroll_subtitle();

        bjQuery(".mp_account_codeimage").on("click",
        function () {
            debugger;
            bjQuery(".bigmpcodebar").slideDown()
        });
        bjQuery(".bigmpcodebar .closebutton").on("click",
        function () {
            bjQuery(".bigmpcodebar").slideUp()
        });
        bjQuery(".navbaritem.fullscreen").on("click",
        function () {
            bjQuery.toggleFullScreen()
        });
        //var c = "_wb_islogin" + scene_id,
        dloginkeyIfNeedPassword = aWindow.sessionStorage.getItem("loginkeyIfNeedPassword");
        //debugger;
        if (!dloginkeyIfNeedPassword) {
            var varJump = '/05XianChangHuoDong/WF_YaoYiYao-' + varShopClientID + '-' + var_ShopClient_XianChangHuoDongID + '.aspx';
            window.location.href = varJump;
            //aWindow.WBActivity.hideLoading();
            //aWindow.WBActivity.showLoginForm()
        } else {
            aWindow.WBActivity.start();
        }
    });
    bjQuery(aWindow).on("resize",
    function () {
        aWindow.WBActivity.resize();
    })
})(window, jQuery);

(function (ewindow, gjQuery, ttt) {
    //debugger; 初始化 jQuery
    var vare = getType(ewindow);
    var varg = getType(gjQuery);
    var h = {
        supportsFullScreen: false,
        isFullScreen: function () {
            return false
        },
        requestFullScreen: function () { },
        cancelFullScreen: function () { },
        fullScreenEventName: "",
        prefix: ""
    },
    c = "webkit moz o ms khtml".split(" ");
    if (typeof document.cancelFullScreen != "undefined") {
        h.supportsFullScreen = true
    } else {
        for (var b = 0,
        aWindow = c.length; b < aWindow; b++) {
            h.prefix = c[b];
            if (typeof document[h.prefix + "CancelFullScreen"] != "undefined") {
                h.supportsFullScreen = true;
                break
            }
        }
    }
    if (h.supportsFullScreen) {
        h.fullScreenEventName = h.prefix + "fullscreenchange";
        h.isFullScreen = function () {
            switch (this.prefix) {
                case "":
                    return document.fullScreen;
                case "webkit":
                    return document.webkitIsFullScreen;
                default:
                    return document[this.prefix + "FullScreen"]
            }
        };
        h.requestFullScreen = function (i) {
            return (this.prefix === "") ? i.requestFullScreen() : i[this.prefix + "RequestFullScreen"]()
        };
        h.cancelFullScreen = function (i) {
            return (this.prefix === "") ? document.cancelFullScreen() : document[this.prefix + "CancelFullScreen"]()
        }
    }
    if (typeof jQuery != "undefined") {
        jQuery.fn.requestFullScreen = function () {
            return this.each(function () {
                var i = jQuery(this);
                if (h.supportsFullScreen) {
                    h.requestFullScreen(i)
                }
            })
        }
    }
    ewindow.fullScreenApi = h;
    gjQuery.toggleFullScreen = function () {
        if (h.isFullScreen()) {
            h.cancelFullScreen(document.documentElement)
        } else {
            h.requestFullScreen(document.documentElement)
        }
    };
    gjQuery.djax = function (idataScenePath, jgetOrPost, kObject, lFunctionVar) {
        //debugger;
        gjQuery.ajax({
            url: idataScenePath,
            type: jgetOrPost,
            dataType: "jsonp",
            jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
            data: kObject,
            success: function (m) {
                debugger;
                lFunctionVar(m)
            },
            error: function () {
                debugger;
                alert("连接服务器失败，请检查网络!  idataScenePath=“" + idataScenePath + "”, jgetOrPost=“" + jgetOrPost + "”, kObject=“" + kObject + "”, lFunctionVar=“" + lFunctionVar + "”")
            }
        })
    };
    gjQuery.setRandom = function (o, j, m, p) {
        if ((((j - o) + 1) < m || o > j)) {
            return ""
        }
        if (typeof p === "undefined") {
            p = true
        }
        var l = [],
        n = 0;
        function k() {
            if (n < m) {
                var i = Math.floor(Math.random() * j + o);
                if (p) {
                    l.push(i);
                    n++
                } else {
                    if (l.indexOf(i) < 0) {
                        l.push(i);
                        n++
                    }
                }
                k()
            } else {
                return false
            }
        }
        k();
        return l
    };
    var d = 0,
    maxUserHeadimg = 0;
    //gjQuery.getUser = function (jAjaxURL, i) {
    //    ///alert("jAjaxURL=" + jAjaxURL);

    //    /////debugger;正在圆环上显示用户      $.joinUser.show(varServicesURL_HelpMachine+"/Pub/doWeiXianChang.asmx/doshake_parter",
    //    //gjQuery.djax(jAjaxURL, "get", {
    //    //    from_id: i,
    //    //    scene_id: varSceenID
    //    //},
    //    gjQuery.djax(jAjaxURL, "get", {
    //        ShopClientID: varShopClientID,
    //        SceenXianChangHuoDongNumber: varSceenXianChangHuoDongNumber
    //    },
    //    function (lresponse) {
    //        ///debugger;   正在圆环上显示用户  varServicesURL_HelpMachine + idataScenePath /Pub/doWeiXianChang.asmx/doshake_parter
    //        if (lresponse.ret == "0") {
    //            if (lresponse.data && lresponse.data.parters) {

    //                gjQuery("#XianChangHuoDongNum").html("编号" + varSceenXianChangHuoDongNumber + "的活动");

    //                gjQuery(".join_num em").html(lresponse.data.count);
    //                var mpartersList = lresponse.data.parters,
    //                k = mpartersList.length;
    //                if (k) {
    //                    var n = '<div id="' + mpartersList[0].UserID + '" class="user-item"><div class="user-img"><img src="' + mpartersList[0].avatar + '" /></div><p>' + mpartersList[0].nick_name + "</p></div>";
    //                    maxUserHeadimg++;
    //                    if (maxUserHeadimg > 10) {
    //                        gjQuery(".users").html("");
    //                        maxUserHeadimg = 1
    //                    }
    //                    gjQuery(n).appendTo(".users").animate({
    //                        opacity: 0.7
    //                    },
    //                    500);
    //                    i = mpartersList[0].UserID
    //                }
    //                if (join_status == 0) {
    //                    //0还没有倒计时开始  1表示 开始聊
    //                    setTimeout(function () {////请求下一张 图片 进行圆环上的展示
    //                        gjQuery.getUser(jAjaxURL, i)
    //                    },
    //                    1001);
    //                    //循环显示图像1001   
    //                } else { }
    //            }
    //        } else {
    //            alert(lresponse.msg)
    //        }
    //    })
    //};
    //gjQuery.joinUser = {
    //    show: function (jAjaxURL, i) {
    //        switch (join_status) {
    //            case "-1":
    //                gjQuery.joinUser.hide();
    //                break;
    //            case "0": //0还没有倒计时开始  1表示 开始聊
    //                gjQuery.joinUser.showRader();
    //                d = 1;
    //                gjQuery.getUser(jAjaxURL, 0);
    //                break;
    //            case "1": //0还没有倒计时开始  1表示 开始聊
    //                gjQuery("body").addClass("sameRound");
    //                gjQuery(".line").removeClass("roundMove");
    //                break;
    //            default:
    //                break
    //        }
    //        i()
    //    },
    //    hide: function (i) {
    //        if (gjQuery(".join_user").size()) {
    //            gjQuery(".join_user").animate({
    //                top: "-91%"
    //            },
    //            1500,
    //            function () {
    //                if (gjQuery.isFunction(i)) {
    //                    i()
    //                }
    //            })
    //        }
    //    },
    //    showRader: function () {
    //        if (gjQuery(".join_user").size()) {
    //            var i = gjQuery(ewindow).width();
    //            gjQuery(".join_user").animate({
    //                top: "0"
    //            },
    //            500);
    //            gjQuery(".radar").fadeIn(1).animate({
    //                left: i / 2 + 220
    //            },
    //            500,
    //            function () {
    //                gjQuery(".line").addClass("roundMove")
    //            });
    //            gjQuery(".codeImg").animate({
    //                left: i / 2 - 220
    //            },
    //            500)
    //        }
    //    }
    //};
    gjQuery.getObjNum = function (k) {
        var l = 0;
        if (!gjQuery.isEmptyObject(k)) {
            for (var j in k) {
                l++
            }
        }
        return l
    }
})(window, jQuery, 999);
