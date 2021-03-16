define("zenjs/util/ua", [],
function () {
    var e = navigator.userAgent.toLowerCase(),
    t = {
        isIOS: function () {
            return "ios" == window._global.mobile_system
        },
        getIOSVersion: function () {
            return parseFloat(("" + (/CPU.*OS ([0-9_]{1,5})|(CPU like).*AppleWebKit.*Mobile/i.exec(navigator.userAgent) || [0, ""])[1]).replace("undefined", "3_2").replace("_", ".").replace("_", "")) || !1
        },
        isAndroid: function () {
            return "android" == window._global.mobile_system
        },
        isAndroidOld: function () {
            return /android 2.3/gi.test(e) || /android 2.2/gi.test(e)
        },
        getAndroidVersion: function () {
            var t = e.match(/android\s([0-9\.]*)/);
            return t ? t[1] : !1
        },
        isWeixin: function () {
            return "weixin" == window._global.platform
        },
        isQQ: function () {
            return "qq" == window._global.platform
        },
        isIPad: function () {
            return /ipad/gi.test(e)
        },
        isMobile: function () {
            return window._global.is_mobile
        },
        isSafari: function () {
            return /safari/gi.test(e) && !/chrome/gi.test(e)
        },
        getSafariVersion: function () {
            var t = /safari\/(\S*)/i;
            return t.test(e) ? e.match(t)[1] : "0"
        },
        isChrome: function () {
            return /chrome/gi.test(e)
        },
        getChromeVersion: function () {
            var t = /chrome\/(\S*)/i;
            return t.test(e) ? e.match(t)[1] : "0"
        },
        isUC: function () {
            return /ucbrowser/gi.test(e)
        },
        getUCVersion: function () {
            var t = /ucbrowser\/(\S*)/i;
            return t.test(e) ? e.match(t)[1] : "0"
        },
        isWxd: function () {
            return "youzanwxd" === _global.platform
        },
        isWsc: function () {
            return "youzanwsc" === _global.platform
        },
        isPf: function () {
            return "younipf" === _global.platform
        },
        getPlatformVersion: function () {
            return _global.platform_version
        },
        getPlatform: function () {
            return _global.platform
        }
    };
    return t
}),
define("bower_components/wap_common/base/fullguide", ["require", "zenjs/util/ua", "Zepto"],
function (e) {
    var t = e("zenjs/util/ua"),
    n = e("Zepto"),
    o = window._global,
    i = n("body"),
    r = t.isWeixin() && o.mp_data && +o.mp_data.quick_subscribe && o.mp_data.quick_subscribe_url,
    a = {
        fav: function () {
            return '<div id="js-fav-guide" class="js-fullguide fullscreen-guide fav-guide hide"><span class="guide-close">&times;</span><span class="guide-arrow"></span><div class="guide-inner"><div class="step step-1"></div><div class="step step-2"></div></div></div>'
        },
        share: function () {
            return '<div id="js-share-guide" class="js-fullguide fullscreen-guide hide" style="font-size: 16px; line-height: 35px; color: #fff; text-align: center;"><span class="js-close-guide guide-close">&times;</span><span class="guide-arrow"></span><div class="guide-inner">请点击右上角<br/>通过【发送给朋友】功能<br>或【分享到朋友圈】功能<br>把消息告诉小伙伴哟～</div></div>'
        },
        browser: function (e) {
            var t = e || {},
            n = t.isIOS ? '<div id="js-share-guide" class="js-fullguide fullscreen-guide hide" style="font-size: 16px; line-height: 35px; color: #fff; text-align: center;"><span class="js-close-guide guide-close">&times;</span><span class="guide-arrow"></span><div class="guide-inner">请点击右上角<br/>在Safari中打开～</div></div>' : '<div id="js-share-guide" class="js-fullguide fullscreen-guide hide" style="font-size: 16px; line-height: 35px; color: #fff; text-align: center;"><span class="js-close-guide guide-close">&times;</span><span class="guide-arrow"></span><div class="guide-inner">请点击右上角<br/>在浏览器中打开～</div></div>';
            return n
        },
        follow: function (e) {
            var t = e || {},
            n = ['<div id="js-follow-guide" class="js-fullguide fullscreen-guide follow-guide hide"><span class="js-close-guide guide-close">&times;</span><div class="guide-inner"><div class="step step-2"></div><div class="wxid"><strong>', t.mp_weixin, '</strong></div><div class="step step-3"></div></div></div>'];
            return n.join("")
        },
        goodsFollow: function (e) {
            var t = e || {},
            n = ['<div id="js-follow-guide" class="js-fullguide fullscreen-guide follow-guide hide"><span class="js-close-guide guide-close">&times;</span><div class="guide-inner"><h3 class="guide-inner-title">你需要关注后才能购买</h3><div class="step step-2"></div><div class="wxid"><strong>', t.mp_weixin, '</strong></div><div class="step step-3"></div></div></div>'];
            return n.join("")
        },
        goodsQuickSubscribe: function (e) {
            var t = e || {},
            n = ['<div id="js-follow-guide" class="js-fullguide fullscreen-guide follow-guide hide"><div class="quick-subscribe js-quick-subscribe"><h2>请先关注后再购买，享受更好的服务~</h2><div><a class="btn" href="', t.quick_subscribe_url, '">去关注</a ></div></div></div>'];
            return n.join("")
        },
        pc: function (e) {
            var t = e || {},
            n = ['<div id="js-share-guide" class="js-fullguide fullscreen-guide hide" style="font-size: 20px; line-height: 30px; color: #fff; text-align: center;"> <span class="js-close-guide guide-close">&times;</span> <div class="guide-inner"> 通过微信【扫一扫】功能<br/>扫描二维码关注我们<img style="width:160px; height: 160px;margin-top: 20px;" src="https://open.weixin.qq.com/qr/code/?username=', t.mp_weixin, '" alt="', t.mp_weixin, '"> </div> </div> '];
            return n.join("")
        }
    },
    s = {
        follow: "#js-follow-guide",
        fav: "#js-fav-guide",
        share: "#js-share-guide"
    },
    l = function (e, t) {
        var o, i;
        n(s[e]).length ? i = n(s[e]) : (o = a[e](t || {}), i = n(o).appendTo("body")),
        i.removeClass("hide")
    },
    c = {
        fav: function () {
            l("fav")
        },
        share: function () {
            window._global && window._global.wuxi1_0_0 && window.shareHook ? window.shareHook() : window.YouzanJSBridge ? window.YouzanJSBridge.doShare() : l("share")
        },
        follow: function (e) {
            var t = o.mp_data;
            if (t) return !(e || {}).goods && r ? void (window.location.href = t.quick_subscribe_url) : void l("follow", t)
        },
        browser: function (e) {
            t.isWeixin() && l("browser", e)
        }
    },
    u = function (e, t) {
        c[e](t)
    };
    o.is_mobile ? o && ["Showcase_Goods_Controller", "Ump_Groupon_Controller"].indexOf(o.controller) > -1 && (r ? a.follow = a.goodsQuickSubscribe : a.follow = a.goodsFollow) : a.follow = a.pc,
    i.on("click", ".wxid",
    function (e) {
        e.stopPropagation()
    }),
    i.on("click", ".js-open-follow",
    function (e) {
        e.preventDefault(),
        u("follow")
    }),
    i.on("click", ".js-open-browser",
    function (e) {
        e.preventDefault(),
        u("browser")
    }),
    i.on("click", ".js-open-fav",
    function (e) {
        e.preventDefault(),
        u("fav")
    }),
    i.on("click", ".js-open-share",
    function (e) {
        e.preventDefault(),
        u("share")
    }),
    n(document).on("click", ".js-fullguide",
    function () {
        n(this).addClass("hide")
    }),
    i.on("click", ".js-quick-subscribe",
    function (e) {
        e.stopPropagation()
    }),
    window.showGuide = u
}),
define("zenjs/util/args", ["require", "jquery"],
function (e) {
    var t = e("jquery"),
    n = {
        getParameterByName: function (e, t) {
            e = e.replace(/[[]/, "\\[").replace(/[]]/, "\\]"),
            t = t ? "?" + t.split("#")[0].split("?")[1] : window.location.search;
            var n = RegExp("[?&]" + e + "=([^&#]*)").exec(t);
            return n ? decodeURIComponent(n[1].replace(/\+/g, " ")) : ""
        },
        removeParameter: function (e, t) {
            var n = e.split("?");
            if (n.length >= 2) {
                for (var o = encodeURIComponent(t) + "=", i = n[1].split(/[&;]/g), r = i.length; r-- > 0;) -1 !== i[r].lastIndexOf(o, 0) && i.splice(r, 1);
                return e = n[0] + "?" + i.join("&")
            }
            return e
        },
        addParameter: function () {
            var e = function (e) {
                var n = "";
                for (var o in e) "" !== e[o] && (n += t.trim(o) + "=" + e[o] + "&");
                return n ? "?" + n.slice(0, n.length - 1) : ""
            };
            return function (n, o) {
                if (!n || 0 === n.length || 0 === t.trim(n).indexOf("javascript")) return "";
                var i = n.split("#"),
                r = i[0].split("?"),
                a = {};
                return r[1] && t.each(r[1].split("&"),
                function (e, t) {
                    var n;
                    n = t.split("="),
                    a[n[0]] = n.slice(1).join("=")
                }),
                t.each(o || {},
                function (e, n) {
                    a[t.trim(e)] = encodeURIComponent(n)
                }),
                n = r[0] + e(a),
                i[1] ? n += "#" + i[1] : n
            }
        }()
    };
    return n.get = n.getParameterByName,
    n.remove = n.removeParameter,
    n.add = n.addParameter,
    n
}),
define("bower_components/wap_common/base/logv2", ["require", "bower_components/ajax/ajax", "zenjs/util/args"],
function (e) {
    e("bower_components/ajax/ajax");
    var t = e("zenjs/util/args"),
    n = window.Zepto || window.jQuery || n,
    o = _global.kdt_id,
    i = function (e) {
        var t = _global.spm || {},
        i = {
            kdt_id: o,
            sf: a(),
            spm: (t.logType || "") + (t.logId || "")
        };
        e = n.extend(i, e);
        for (var r in e) "" === e[r] && delete e[r];
        "share" === e.fm && (e.url = window.location.href),
        n._ajax({
            url: "//tj.koudaitong.com/v1/fm",
            xhrFields: {
                withCredentials: !0
            },
            data: e,
            defaultError: !1
        })
    },
    r = function (e) {
        var t = e.attr("class") || "";
        t.indexOf("js-") < 0 && (e = e.closest("[class*=js-]"));
        var n = /js-(\S+)/,
        o = e.attr("class").match(n),
        r = e.data("title") || e.text().trim().replace(/[\s|\r|\n]/g, "");
        r.length > 20 && (r = r.substring(0, 20)),
        o && o.length > 0 && i({
            fm: "click",
            ck: o[1],
            title: r
        })
    },
    a = function () {
        return t.get("sf")
    };
    return {
        log: i,
        logClick: r
    }
}),
define("zenjs/util/cookie", [],
function () {
    var e = function () {
        var e = new Date,
        t = +e,
        n = 864e5,
        o = function (e) {
            var t = document.cookie,
            n = "\\b" + e + "=",
            o = t.search(n);
            if (0 > o) return "";
            o += n.length - 2;
            var i = t.indexOf(";", o);
            return 0 > i && (i = t.length),
            t.substring(o, i) || ""
        },
        i = function (e, t, n) {
            if (!e) return "";
            var o = [];
            for (var i in e) o.push(encodeURIComponent(i) + "=" + (n ? encodeURIComponent(e[i]) : e[i]));
            return o.join(t || ",")
        };
        return function (r, a) {
            if (void 0 === a) return o(r);
            if ("string" == typeof a || a instanceof String) {
                if (a) return document.cookie = r + "=" + a + ";",
                a;
                a = {
                    expires: -100
                }
            }
            a = a || {};
            var s = r + "=" + (a.value || "") + ";";
            delete a.value,
            void 0 !== a.expires && (e.setTime(t + a.expires * n), a.expires = e.toGMTString()),
            s += i(a, ";"),
            document.cookie = s
        }
    }();
    return e
}),
define("zenjs/util/image", ["require", "./cookie"],
function (e) {
    var t = e("./cookie"),
    n = {};
    return n.toWebp = function () {
        function e(e, t) {
            var n, o = /(\?imageView2\/\d\/w\/\d+\/h\/\d+\/q\/\d+\/format\/)(\w+)/;
            if (n = e, o.test(e)) {
                var i = e.match(o)[2];
                t ? "gif" !== i && "webp" !== i && (n = e.replace(o, "$1webp")) : "webp" === i && (n = e.replace(o, "$1jpg"))
            }
            return n
        }
        var t = /\.([^.!]+)\!([0-9]{1,4})x([0-9]{1,4})(\+2x)?\..+/,
        n = !1;
        try {
            n = "ok" === window.localStorage.getItem("canwebp")
        } catch (o) { }
        return function (o) {
            var i = o,
            r = 1,
            a = i.match(t);
            return n ? a && a.length >= 4 ? ("+2x" == a[4] && (r = 2), i = i.replace(t, ".") + a[1] + "?imageView2/2/w/" + parseInt(a[2], 10) * r + "/h/" + parseInt(a[3], 10) * r + "/q/75/format/" + ("gif" == a[1] ? "gif" : "webp")) : i = e(i, !0) : a && a.length >= 4 ? ("+2x" == a[4] && (r = 2), i = i.replace(t, ".") + a[1] + "?imageView2/2/w/" + parseInt(a[2], 10) * r + "/h/" + parseInt(a[3], 10) * r + "/q/75/format/" + ("webp" === a[1] ? "jpg" : a[1])) : i = e(i, !1),
            i
        }
    }(),
    n.checkCanWebp = function () {
        var e = function (e) {
            var t = new Image;
            t.onload = t.onerror = function () {
                e(2 == t.height)
            },
            t.src = "data:image/webp;base64,UklGRjoAAABXRUJQVlA4IC4AAACyAgCdASoCAAIALmk0mk0iIiIiIgBoSygABc6WWgAA/veff/0PP8bA//LwYAAA"
        };
        return function (n) {
            if ("object" == typeof window.localStorage) try {
                var o = localStorage.getItem("canwebp");
                "ok" == o ? t("_canwebp", {
                    value: "1",
                    path: "/",
                    domain: location.hostname,
                    expires: 3650
                }) : "no" != o && e(function (e) {
                    localStorage.setItem("canwebp", e ? "ok" : "no"),
                    e && t("_canwebp", {
                        value: "1",
                        path: "/",
                        domain: location.hostname,
                        expires: 3650
                    })
                })
            } catch (i) { }
        }
    }(),
    n
}),
define("bower_components/wap_common/base/webpinfo", ["require", "zenjs/util/ua", "zenjs/util/cookie"],
function (e) {
    function t() {
        if (1 === parseInt(r("_canwebp"), 10)) return l;
        if (2 === parseInt(r("_canwebp"), 10)) return s;
        var e;
        if (window.localStorage) try {
            "ok" === localStorage.getItem("canwebp") ? e = l : "no" === localStorage.getItem("canwebp") && (e = s)
        } catch (t) {
            e = a
        }
        return e
    }
    function n() {
        return i.isWeixin() ? "weixin" : i.isWxd() ? "wxd" : i.isUC() ? "uc-" + i.getUCVersion() : i.isChrome() ? "chrome-" + i.getChromeVersion() : i.isSafari() ? "safari-" + i.getSafariVersion() : "unknow"
    }
    function o() {
        return i.isIOS() ? "ios-" + i.getIOSVersion() : i.isAndroid() ? "android-" + i.getAndroidVersion() : void 0
    }
    var i = e("zenjs/util/ua"),
    r = e("zenjs/util/cookie"),
    a = 2,
    s = 1,
    l = 0;
    return {
        canWebp: t(),
        browser: n(),
        system: o()
    }
}),
define("bower_components/wap_common/base/log", ["zenjs/util/args", "./webpinfo"],
function (e, t) {
    var n = window.Zepto || window.jQuery || n,
    o = {};
    _global.spm = _global.spm || {};
    var i = function () {
        var t = function () {
            return _global.spm.logType + _global.spm.logId || "fake" + _global.kdt_id
        };
        return function () {
            var o = e.get("spm");
            if (o = n.trim(o), "" !== o) {
                var i = o.split("_");
                i.length > 2 && (o = i[0] + "_" + i[i.length - 1]),
                o += "_" + t()
            } else o = t();
            return o
        }
    }(),
    r = function (t, o, i) {
        var r = new Image,
        a = Math.floor(2147483648 * Math.random()).toString(36),
        s = "log_" + a,
        l = new n.Deferred;
        return window[s] = r,
        r.onload = r.onerror = r.onabort = function () {
            r.onload = r.onerror = r.onabort = null,
            window[s] = null,
            r = null,
            l.resolve()
        },
        o.link = window.location.href,
        o.time = (new Date).getTime(),
        r.src = e.add(t, o),
        window.setTimeout(l.resolve, 1500),
        l.promise()
    },
    a = function (e) {
        e = e || "default";
        var t = {
            wxd: "//tj.koudaitong.com/fx.gif",
            wxdapp: "//app.tj.koudaitong.com/1.gif",
            "default": "//tj.koudaitong.com/1.gif",
            ua: "//tj.koudaitong.com/v1/ua"
        };
        return t[e]
    };
    o.log = function (e, t) {
        e.spm || (e.spm = o.getSpm()),
        e.referer_url || (e.referer_url = encodeURIComponent(document.referrer)),
        e.title || (e.title = _global.title || n.trim(document.title));
        var i = a(e.target);
        return delete e.target,
        r(i, e, t)
    },
    o.uaLog = function () {
        r(a("ua"), t)
    },
    o.getSpm = function () {
        return o.spm || (o.spm = i()),
        o.spm
    },
    window.Logger = o;
    var s = window.__logs;
    return s && s.length > 0 && s.forEach(o.log),
    o
}),
function (e) {
    e.onReady = function (t, n) {
        if (n) {
            var o = function () {
                e[t] ? n() : setTimeout(function () {
                    o(t, n)
                },
                500)
            };
            o(t, n)
        }
    };
    var t = /complete|loaded/;
    e.afterLoad = function (n) {
        t.test(document.readyState) && document.body ? setTimeout(n) : e.addEventListener("load", n, !1)
    }
}(window),
define("zenjs/inline_script/onready",
function () { }),
define("bower_components/wap_common/base/lazy_load", ["require", "./logv2", "zenjs/util/image", "./log", "zenjs/inline_script/onready", "bower_components/ajax/ajax"],
function (e) {
    var t = e("./logv2"),
    n = e("zenjs/util/image");
    e("./log"),
    e("zenjs/inline_script/onready"),
    e("bower_components/ajax/ajax");
    var o = window.Zepto || window.jQuery || o,
    i = o(window),
    r = Logger && Logger.getSpm() || "";
    o.fn.lazyload = function (e) {
        function t() {
            var e = 0;
            a.each(function () {
                var t = o(this);
                if (!s.skip_invisible || t.is(":visible")) if (o.abovethetop(this, s) || o.leftofbegin(this, s));
                else if (o.belowthefold(this, s) || o.rightoffold(this, s)) {
                    if (++e > s.failure_limit) return !1
                } else t.trigger("appear"),
                e = 0
            })
        }
        var r, a = this,
        s = {
            threshold: 400,
            failure_limit: 0,
            event: "scroll",
            effect: "show",
            container: window,
            data_attribute: "src",
            skip_invisible: !1,
            appear: null,
            load: null,
            placeholder: null
        };
        return e && (void 0 !== e.failurelimit && (e.failure_limit = e.failurelimit, delete e.failurelimit), void 0 !== e.effectspeed && (e.effect_speed = e.effectspeed, delete e.effectspeed), o.extend(s, e)),
        r = void 0 === s.container || s.container === window ? i : o(s.container),
        0 === s.event.indexOf("scroll") && r.bind(s.event,
        function () {
            return t()
        }),
        this.each(function () {
            var e = this,
            t = o(e),
            i = t[0].nodeName.toLowerCase();
            e.loaded = !1,
            "img" === i && (void 0 === t.attr("src") || t.attr("src") === !1) && t.is("img") && s.placeholder && t.attr("src", s.placeholder),
            t.one("appear",
            function () {
                if (!this.loaded) {
                    if (s.appear) {
                        var r = a.length;
                        s.appear.call(e, r, s)
                    }
                    if ("img" === i) {
                        var l = t.attr("data-" + s.data_attribute);
                        l = n.toWebp(l),
                        l = l.replace(/http:\/\/imgqn.koudaitong.com/gi, "https://img.yzcdn.cn"),
                        l = l.replace(/http:\/\/dn-kdt-img.qbox.me/gi, "https://dn-kdt-img.qbox.me"),
                        o("<img />").bind("load",
                        function () {
                            t.hide(),
                            t.is("img") ? t.attr("src", l) : t.css("background-image", 'url("' + l + '")'),
                            t[s.effect](),
                            e.loaded = !0;
                            var n = o(e).parent();
                            n.hasClass("photo-block") && n.css("background-color", "#fff");
                            var i = o.grep(a,
                            function (e) {
                                return !e.loaded
                            });
                            if (a = o(i), s.load) {
                                var r = a.length;
                                s.load.call(e, r, s)
                            }
                        }).attr("src", l)
                    } else if ("textarea" === i) {
                        var c = t.parent(),
                        u = t.val() || "";
                        t.after(u).remove(),
                        o(".js-lazy", c).lazyload(),
                        o(".js-richtext-img-lazy", c).lazyload(),
                        o(".js-goods-lazy", c).goodsLazyLoad(),
                        s.load && s.load.call(e, c, s)
                    } else if (t.hasClass("js-lazy-container")) o(".js-lazy, .js-richtext-img-lazy, .js-goods-lazy", t).trigger("appear"),
                    t.loaded = !0;
                    else if ("iframe" === i) t.attr("src", t.data("src")),
                    t.loaded = !0;
                    else {
                        var d = t.data("src");
                        d.length > 0 && o._ajax({
                            url: d,
                            dataType: "json",
                            type: "get",
                            timeout: 3e3
                        }).done(function (n) {
                            0 === +n.code ? (t.append(n.data), t.loaded = !0, o(".js-lazy", t).lazyload(), s.load && s.load.call(e, t, s)) : motify.log(n.msg)
                        }).fail(function () {
                            motify.log("网络错误")
                        })
                    }
                }
            }),
            0 !== s.event.indexOf("scroll") && t.bind(s.event,
            function () {
                e.loaded || t.trigger("appear")
            })
        }),
        i.bind("resize",
        function () {
            t()
        }),
        /(?:iphone|ipod|ipad).*os 5/gi.test(navigator.appVersion) && i.bind("pageshow",
        function (e) {
            e.originalEvent && e.originalEvent.persisted && a.each(function () {
                o(this).trigger("appear")
            })
        }),
        o(document).ready(function () {
            t()
        }),
        this
    },
    o.fn.goodsLazyLoad = function () {
        this.lazyload({
            appear: function () {
                var e, n = o(this).parents(".js-goods").first().data("goods-id");
                e = r.lastIndexOf("_") === r.length - 1 ? r + "SI" + n : r + "_SI" + n,
                window.Logger && Logger.log({
                    spm: e,
                    fm: "display"
                }),
                t.log({
                    fm: "view",
                    display_goods: n
                })
            }
        })
    },
    o.belowthefold = function (e, t) {
        var n;
        return n = void 0 === t.container || t.container === window ? (window.innerHeight ? window.innerHeight : i.height()) + i.scrollTop() : o(t.container).offset().top + o(t.container).height(),
        n <= o(e).offset().top - t.threshold
    },
    o.rightoffold = function (e, t) {
        var n;
        return n = void 0 === t.container || t.container === window ? i.width() + i.scrollLeft() : o(t.container).offset().left + o(t.container).width(),
        n <= o(e).offset().left - t.threshold
    },
    o.abovethetop = function (e, t) {
        var n;
        return n = void 0 === t.container || t.container === window ? i.scrollTop() : o(t.container).offset().top,
        n >= o(e).offset().top + t.threshold + o(e).height()
    },
    o.leftofbegin = function (e, t) {
        var n;
        return n = void 0 === t.container || t.container === window ? i.scrollLeft() : o(t.container).offset().left,
        n >= o(e).offset().left + t.threshold + o(e).width()
    },
    o.inviewport = function (e, t) {
        return !(o.rightoffold(e, t) || o.leftofbegin(e, t) || o.belowthefold(e, t) || o.abovethetop(e, t))
    },
    window.afterLoad(function () {
        o(".js-lazy").lazyload(),
        o(".js-richtext-img-lazy").lazyload({
            load: function () {
                o(this).closest(".custom-richtext").css("min-height", 0)
            }
        }),
        o(".js-lazy-container").lazyload(),
        o(".js-goods-lazy").goodsLazyLoad()
    })
}),
define("zenjs/util/str/unescape", [],
function () {
    var e = function (e) {
        var t = {
            "&amp;": "&",
            "&lt;": "<",
            "&gt;": ">",
            "&quot;": '"',
            "&#x27;": "'"
        },
        n = /(\&amp;|\&lt;|\&gt;|\&quot;|\&#x27;)/g;
        return ("" + e).replace(n,
        function (e) {
            return t[e]
        })
    };
    return e
}),
define("zenjs/util/str", ["require", "./str/unescape"],
function (e) {
    var t = e("./str/unescape"),
    n = function (e, t) {
        e && t && $(e).each(function (e, n) {
            for (var o = $(n), i = o.find(t), r = i.text(), a = -1, s = 0; i.height() > o.height() && 100 > s;) {
                s++;
                var l = r.slice(0, a--);
                l = l.replace(/([\S\s])$/, "..."),
                i.text(l)
            }
        })
    };
    return {
        multiEllipsis: n,
        unescape: t
    }
}),
define("bower_components/wap_common/base/share", ["require", "zenjs/util/ua", "zenjs/util/args", "zenjs/util/str"],
function (e) {
    var t = e("zenjs/util/ua"),
    n = e("zenjs/util/args"),
    o = e("zenjs/util/str"),
    i = window.Zepto || window.jQuery || i,
    r = window._global || {},
    a = r.share || {},
    s = function (e) {
        return e = e.replace(/imgqn\.koudaitong\.com/i, "img.yzcdn.cn"),
        -1 !== e.indexOf("imageView2") ? e = e.replace(/(imageView2\/2\/w\/)\d+(\/h\/)\d+/, "$1200$2200") : (-1 !== e.indexOf("img.yzcdn.cn") || -1 !== e.indexOf("imgqntest.koudaitong.com") || -1 !== e.indexOf("dn-kdt-img.qbox.me") || -1 !== e.indexOf("dn-kdt-img-test.qbox.me")) && (e = e.replace(/\!\d+x\d+.+/, "!200x200.jpg"), -1 === e.indexOf("!200x200") && (e += "!200x200.jpg")),
        e = e.replace("https://", "http://").replace("/format/webp", "/format/jpg")
    },
    l = function () {
        var e = "/AddFunction/03CustomPay/Images/youzan_mall_logo.jpg",
        t = i("#wxcover"),
        n = null;
        return t && t.length > 0 ? (n = t.data("wxcover"), n && 0 !== n.length || (n = t.css("background-image"), n && "none" != n ? (n = /^url\((['"]?)(.*)\1\)$/.exec(n), n = n ? n[2] : null) : n = null)) : (t = null, i(".content img").each(function (e, n) {
            return i(n).hasClass("js-not-share") ? void 0 : (t = i(n), !1)
        }), t && t.length > 0 && (n = t[0].getAttribute("data-src") || t[0].getAttribute("src"))),
        n || (_global.mp_data || {}).logo || e
    },
    c = function (e) {
        e = e || document.documentURI;
        var t, o = Number(_global.kdt_id) || 0,
        i = [2737501, 618192, 618242, 371189, 1],
        r = _global.youzan_share,
        a = Math.floor(9e3 * Math.random()) + 1e3;
        return i.indexOf(o) >= 0 && (o = 0),
        r ? (t = a + "." + r, e = e.replace(/:\/\/.*\.koudaitong\.com/g, "://" + t + ".koudaitong.com")) : (t = 0 === o ? "192168-" + a : 192168 + o, e = e.replace("://wap.", "://shop" + t + ".")),
        e = n.remove(e, "redirect_count")
    },
    u = function (e, t) {
        var n = e || i("#wxdesc").val() || i("#wxdesc").text() || i(".custom-richtext").text();
        if (!n || 0 == n.length) {
            var o = i(".content").clone();
            o.find(".hide, .js-add-wish").remove(),
            n = (o.text() || t) + "",
            n = i("<div>" + n + "</div>").text()
        }
        return n.replace(/\s+/g, " ").trim()
    },
    d = function () {
        var e = a.title || _global.title || i("#wxtitle").text() || document.title,
        t = a.link || c(),
        n = s(a.cover || l()),
        r = u(a.desc, e);
        return function () {
            e = window.__title || e,
            t = window.__link || t,
            n = window.__cover || n,
            r = window.__desc || r;
            var s, l = i(".time-line-title"),
            c = a.otherShareData || {};
            return s = l.length > 0 ? l.val() || l.text() : a.timeline_title,
            i.extend({
                title: o.unescape(e),
                link: t,
                img_url: n,
                desc: o.unescape(r).substring(0, 80),
                timeLineTitle: o.unescape((s || "").trim())
            },
            c)
        }
    }(),
    f = function () {
        var e = d();
        if (t) if (t.isIOS()) {
            var n = "#func=sharePlatsAction&content=" + e.title + e.desc + "&content_url=" + e.link + "&pic=" + e.img_url;
            window.location.hash = "",
            window.location.href = n
        } else t.isAndroid() && window.android && window.android.sharePlatsAction && window.android.sharePlatsAction(e.title, e.link, e.img_url)
    };
    window.shareHook = f,
    window.getShareLink = c,
    window.getShareData = window.getShareData || d
}),
define("zenjs/class", ["require", "exports", "module"],
function (e, t, n) {
    var o = !1,
    i = /\b_super\b/,
    r = function () { };
    r.extend = function (e) {
        function t() {
            !o && this.init && this.init.apply(this, arguments)
        }
        var n = this.prototype;
        o = !0;
        var r = new this;
        o = !1;
        for (var a in e) r[a] = "function" == typeof e[a] && "function" == typeof n[a] && i.test(e[a]) ?
        function (e, t) {
            return function () {
                var o = this._super;
                this._super = n[e];
                var i = t.apply(this, arguments);
                return this._super = o,
                i
            }
        }(a, e[a]) : e[a];
        return t.prototype = r,
        t.prototype.constructor = t,
        t.extend = arguments.callee,
        t
    },
    n.exports = r
}),
define("zenjs/core/trigger_method", [],
function () {
    var e = function () {
        function e(e, t, n) {
            return n.toUpperCase()
        }
        function t(e, t, n) {
            return [].slice.call(e, null == t || n ? 1 : t)
        }
        var n = /(^|:)(\w)/gi;
        return function (o) {
            var i = "on" + o.replace(n, e),
            r = this[i];
            return "function" == typeof this.trigger && this.trigger.apply(this, arguments),
            "function" == typeof r ? r.apply(this, t(arguments)) : void 0
        }
    }();
    return e
}),
define("zenjs/events", ["require", "exports", "module", "./class", "./core/trigger_method"],
function (e, t, n) {
    var o = e("./class"),
    i = e("./core/trigger_method");
    n.exports = o.extend({
        on: function (e, t, n) {
            return this._events = this._events || {},
            this._events[e] = this._events[e] || [],
            this._events[e].push({
                callback: t,
                context: n,
                ctx: n || this
            }),
            this
        },
        off: function (e, t, n) {
            var o, i, r, a, s, l, c, u;
            if (!e && !t && !n) return this._events = {},
            this;
            for (a = e ? [e] : Object.keys(this._events), s = 0, l = a.length; l > s; s++) if (e = a[s], r = this._events[e]) {
                if (this._events[e] = o = [], t || n) for (c = 0, u = r.length; u > c; c++) i = r[c],
                (t && t !== i.callback && t !== i.callback._callback || n && n !== i.context) && o.push(i);
                o.length || delete this._events[e]
            }
            return this
        },
        trigger: function (e) {
            if (!this._events) return this;
            var t = [].slice.call(arguments, 1),
            n = this._events[e];
            if (n) for (var o, i = -1; ++i < n.length;) (o = n[i]).callback.apply(o.ctx, t)
        },
        triggerMethod: i
    })
}),
define("zenjs/enhanced_events", ["require", "exports", "module", "./events"],
function (e, t, n) {
    function o(e) {
        var t, n = !1;
        return function () {
            return n ? t : (n = !0, t = e.apply(this, arguments), e = null, t)
        }
    }
    var i = e("./events"),
    r = /\s+/,
    a = function (e, t, n, o, i) {
        var s, l = 0;
        if (n && "object" == typeof n) {
            void 0 !== o && "context" in i && void 0 === i.context && (i.context = o);
            for (s = _.keys(n) ; l < s.length; l++) t = a(e, t, s[l], n[s[l]], i)
        } else if (n && r.test(n)) for (s = n.split(r) ; l < s.length; l++) t = e(t, s[l], o, i);
        else t = e(t, n, o, i);
        return t
    },
    s = function (e, t, n, o) {
        if (n) {
            var i = e[t] || (e[t] = []),
            r = o.context,
            a = o.ctx,
            s = o.listening;
            s && s.count++,
            i.push({
                callback: n,
                context: r,
                ctx: r || a,
                listening: s
            })
        }
        return e
    },
    l = function (e, t, n, o, i) {
        if (e._events = a(s, e._events || {},
        t, n, {
            context: o,
            ctx: e,
            listening: i
        }), i) {
            var r = e._listeners || (e._listeners = {});
            r[i.id] = i
        }
        return e
    },
    c = i.extend({
        once: function (e, t, n) {
            var i = this,
            r = o(function () {
                i.off(e, r),
                t.apply(this, arguments)
            });
            return r._callback = t,
            this.on(e, r, n)
        },
        listenTo: function (e, t, n) {
            if (!e) return this;
            var o = e._listenId || (e._listenId = _.uniqueId("l")),
            i = this._listeningTo || (this._listeningTo = {}),
            r = i[o];
            if (!r) {
                var a = this._listenId || (this._listenId = _.uniqueId("l"));
                r = i[o] = {
                    obj: e,
                    objId: o,
                    id: a,
                    listeningTo: i,
                    count: 0
                }
            }
            return l(e, t, n, this, r),
            this
        },
        stopListening: function (e, t, n) {
            var o = this._listeningTo;
            if (!o) return this;
            for (var i = e ? [e._listenId] : _.keys(o), r = 0; r < i.length; r++) {
                var a = o[i[r]];
                if (!a) break;
                a.obj.off(t, n, this)
            }
            return _.isEmpty(o) && (this._listeningTo = void 0),
            this
        }
    });
    n.exports = c
}),
define("bower_components/youzanjsbridge/core", ["require", "Zepto", "zenjs/enhanced_events", "zenjs/util/args", "zenjs/util/ua"],
function (e) {
    var t = e("Zepto"),
    n = e("zenjs/enhanced_events"),
    o = e("zenjs/util/args"),
    i = e("zenjs/util/ua"),
    r = function () {
        return {}
    },
    a = n.extend({
        init: function (e) {
            var n = this;
            this.on("share", this.doShare),
            this.doCall("webReady"),
            e.check_login && (this.on("userInfoReady",
            function (o) {
                o && o.user_id && o.user_id != e.fans_token && t.ajax({
                    type: "POST",
                    url: e.kdtunionUrl || window._global.url.wap + "/buyer/kdtunion/index.json",
                    xhrFields: {
                        withCredentials: !0
                    },
                    data: o,
                    success: function (t) {
                        t && 0 === t.code ? e.redirectUrl ? window.location.href = e.redirectUrl : (window._global.buyer_id = t.data.fans_id, n.trigger("loginSuccess", t.data.fans_id)) : alert("登录失败请重试！")
                    }
                })
            }), this.on("userInfoFail",
            function () {
                history.back(),
                setTimeout(function () {
                    n.doCall("doAction", {
                        action: "back"
                    })
                },
                500)
            }), setTimeout(function () {
                n.doCall("getData", {
                    datatype: "userInfo"
                }),
                n.doCall("getUserInfo")
            },
            100))
        },
        doCall: function (e, n, r) {
            console.log(e, n);
            var a = this;
            if (this.lastCallTime && Date.now() - this.lastCallTime < 100) return void setTimeout(function () {
                a.doCall(e, n, r)
            },
            100);
            if (this.lastCallTime = Date.now(), n = n || {},
            r && t.extend(n, {
                callback: r
            }), i.isIOS()) {
                t.each(n,
                function (e, o) {
                    (t.isPlainObject(o) || t.isArray(o)) && (n[e] = JSON.stringify(o))
                });
                var s = o.addParameter("youzanjs://" + e, n),
                l = document.createElement("iframe");
                l.style.width = "1px",
                l.style.height = "1px",
                l.style.display = "none",
                l.src = s,
                document.body.appendChild(l),
                setTimeout(function () {
                    l.remove()
                },
                100)
            } else i.isAndroid() ? window.androidJS && window.androidJS[e] && window.androidJS[e](JSON.stringify(n)) : console.error("未获取platform信息，调取api失败")
        },
        call: function (e) {
            return this.lastTriggerTime && Date.now() - this.lastTriggerTime < 100 ? void setTimeout(function () {
                _this.call(e)
            },
            100) : (this.lastTriggerTime = Date.now(), void (e && i.isIOS() ? this.doiOSCall(e) : e && i.isAndroid() ? window.YZAndroidJS && window.YZAndroidJS.doCall && window.YZAndroidJS.doCall(e) : console.error("未获取platform信息，调取api失败")))
        },
        doiOSCall: function (e) {
            var t = document.createElement("iframe");
            t.style.display = "none",
            t.style.width = "1px",
            t.style.height = "1px",
            t.src = e,
            document.body.appendChild(t),
            setTimeout(function () {
                t.remove()
            },
            100)
        },
        getData: function () {
            this.trigger.apply(this, arguments)
        },
        doShare: function (e) {
            e = e || (window.getShareData || r)() || {},
            e.datatype = "commonShareInfo",
            this.doCall("returnShareData", e),
            this.doCall("putData", e)
        }
    });
    window.onReady("isReadyForYouZanJSBridge",
    function () {
        var e = window.YouzanJSBridgeOptions || {};
        window.YouzanJSBridge = new a({
            check_login: _global.ajax_acl_check || e.isNeedCheckLogin,
            fans_token: _global.fans_token,
            redirectUrl: e.redirectUrl,
            kdtunionUrl: _global.kdt_union_url
        })
    })
}),
define("bower_components/wap_common/base/wx", ["require", "./logv2", "zenjs/util/args", "./share"],
function (e) {
    var t = e("./logv2"),
    n = e("zenjs/util/args"),
    o = window._global;
    e("./share");
    var i = function (e) {
        if (window.WeixinJSBridge) e && e();
        else {
            var t = setTimeout(function () {
                window.WeixinJSBridge && e && e()
            },
            1e3);
            document.addEventListener("WeixinJSBridgeReady",
            function () {
                clearTimeout(t),
                e && e()
            })
        }
    },
    r = window._global || {},
    a = r.share || {},
    s = function (e) {
        e.fm = "share",
        t.log(e)
    },
    l = function (e, t) {
        window.Logger && window.Logger.log({
            fm: "share",
            title: e.title,
            link: encodeURIComponent(e.link),
            from: t
        })
    },
    c = function (e, t) {
        e.link = n.add(e.link, {
            sf: t
        })
    };
    i(function () {
        var e = window.WeixinJSBridge;
        e && e.on && (e.call(a.notShare ? "hideOptionMenu" : "showOptionMenu"), e.on("menu:share:timeline",
        function () {
            if (!a.notShare) {
                window.doWhileShare && window.doWhileShare();
                var t = window.getShareData();
                t.timeLineTitle && (t.title = t.timeLineTitle);
                var n = "wx_tl";
                c(t, n),
                e.invoke("shareTimeline", t,
                function (e) {
                    window.__onShareTimeline && window.__onShareTimeline(e)
                }),
                l(t, "timeline"),
                s({
                    sf: n
                })
            }
        }), e.on("menu:share:appmessage",
        function () {
            if (!a.notShare) {
                window.doWhileShare && window.doWhileShare();
                var t = window.getShareData(),
                n = "wx_sm";
                c(t, n),
                e.invoke("sendAppMessage", t,
                function () { }),
                l(t, "appmessage"),
                s({
                    sf: n
                })
            }
        }), e.on("menu:share:qq",
        function () {
            if (!a.notShare) {
                var t = "qq_sm",
                n = window.getShareData();
                c(n, t),
                e.invoke("shareQQ", n,
                function () { }),
                s({
                    sf: t
                })
            }
        }), e.on("menu::share:qzone",
        function () {
            if (!a.notShare) {
                var t = "qq_zone",
                n = window.getShareData();
                c(n, t),
                e.invoke("shareQZone", n,
                function () { }),
                s({
                    sf: t
                })
            }
        }))
    });
    var u = {
        openAddress: function (e, t) {
            window.WeixinJSBridge && window.WeixinJSBridge.invoke("editAddress", o.address_token,
            function (n) {
                var o = n.err_msg || n.errMsg;
                "edit_address:ok" === o ? e(n) : t(n)
            })
        }
    }; !
    function () {
        var e = {};
        e.on = function () { },
        window.wx = e,
        window.wxReady = i,
        window.wxBridge = u
    }()
}),
function (e, t) {
    function n() {
        return o ? o : (o = e('<div class="motify"><div class="motify-inner"></div></div>'), e("body").append(o), o)
    }
    var o, i, r = t.motify = t.motify || {};
    r.log = function (e, o, r) {
        var a = n(),
        s = this;
        "number" != typeof o && (o = 2e3),
        a.show().find(".motify-inner").html(e || " "),
        o > 0 && (t.clearTimeout(i), i = t.setTimeout(function () {
            r && r.apply(null),
            s.clear()
        },
        "function" != typeof r ? o : o + 300))
    },
    r.clear = function () {
        var e = n();
        e.hide()
    }
}(window.Zepto || window.jQuery || $, window),
define("bower_components/wap_common/base/motify",
function () { }),
window.Zepto &&
function (e) {
    ["width", "height"].forEach(function (t) {
        var n = t.replace(/./,
        function (e) {
            return e[0].toUpperCase()
        });
        e.fn["outer" + n] = function (e) {
            var n = this;
            if (n && n.length > 0) {
                var o = n[t](),
                i = {
                    width: ["left", "right"],
                    height: ["top", "bottom"]
                };
                return i[t].forEach(function (t) {
                    e && (o += parseInt(n.css("margin-" + t), 10))
                }),
                o
            }
            return null
        }
    })
}(Zepto),
define("vendor/zepto/outer",
function () { }),
define("bower_components/wap_common/base/footer_auto", ["require", "vendor/zepto/outer"],
function (e) {
    e("vendor/zepto/outer");
    var t = navigator.userAgent,
    n = ["MI", "NX507J", "SM701", "Coolpad"],
    o = function () {
        for (var e = n.length - 1; e >= 0; e--) if (t.indexOf(n[e]) > -1) return !0;
        return !1
    }(),
    i = 0 === $(".auto-footer-off").length ? !1 : !0;
    if (!i && !o) {
        var r = $(window).height(),
        a = $(".container"),
        s = $(".footer").length && $(".footer").outerHeight(!0) || 0,
        l = $(".js-footer-auto-ele"),
        c = r;
        if (0 === a.length) return;
        c -= s,
        l.length > 0 && (c -= l.outerHeight(!0)),
        a.css("min-height", c + "px")
    }
}),
function () {
    function e(t, o) {
        function i(e, t) {
            return function () {
                return e.apply(t, arguments)
            }
        }
        var r;
        if (o = o || {},
        this.clickEvents = {},
        this.trackingClick = !1, this.trackingClickStart = 0, this.targetElement = null, this.touchStartX = 0, this.touchStartY = 0, this.lastTouchIdentifier = 0, this.touchBoundary = o.touchBoundary || 10, this.layer = t, this.tapDelay = o.tapDelay || 200, this.tapTimeout = o.tapTimeout || 700, !e.notNeeded(t)) {
            for (var a = ["onMouse", "onClick", "onTouchStart", "onTouchMove", "onTouchEnd", "onTouchCancel"], s = this, l = 0, c = a.length; c > l; l++) s[a[l]] = i(s[a[l]], s);
            n && (t.addEventListener("mouseover", this.onMouse, !0), t.addEventListener("mousedown", this.onMouse, !0), t.addEventListener("mouseup", this.onMouse, !0)),
            t.addEventListener("click", this.onClick, !0),
            t.addEventListener("touchstart", this.onTouchStart, !1),
            t.addEventListener("touchmove", this.onTouchMove, !1),
            t.addEventListener("touchend", this.onTouchEnd, !1),
            t.addEventListener("touchcancel", this.onTouchCancel, !1),
            Event.prototype.stopImmediatePropagation || (t.removeEventListener = function (e, n, o) {
                var i = Node.prototype.removeEventListener;
                "click" === e ? i.call(t, e, n.hijacked || n, o) : i.call(t, e, n, o)
            },
            t.addEventListener = function (e, n, o) {
                var i = Node.prototype.addEventListener;
                "click" === e ? i.call(t, e, n.hijacked || (n.hijacked = function (e) {
                    e.propagationStopped || n(e)
                }), o) : i.call(t, e, n, o)
            }),
            "function" == typeof t.onclick && (r = t.onclick, t.addEventListener("click",
            function (e) {
                r(e)
            },
            !1), t.onclick = null);
            var u = this;
            window.addEventListener("scroll",
            function () {
                for (var e in u.clickEvents) clearTimeout(e);
                u.clickEvents = {}
            })
        }
    }
    var t = navigator.userAgent.indexOf("Windows Phone") >= 0,
    n = navigator.userAgent.indexOf("Android") > 0 && !t,
    o = /iP(ad|hone|od)/.test(navigator.userAgent) && !t,
    i = o && /OS 4_\d(_\d)?/.test(navigator.userAgent),
    r = o && /OS [6-7]_\d/.test(navigator.userAgent),
    a = navigator.userAgent.indexOf("BB10") > 0;
    e.prototype.needsClick = function (e) {
        switch (e.nodeName.toLowerCase()) {
            case "button":
            case "select":
            case "textarea":
                if (e.disabled) return !0;
                break;
            case "input":
                if (o && "file" === e.type || e.disabled) return !0;
                break;
            case "label":
            case "iframe":
            case "video":
                return !0
        }
        return /\bneedsclick\b/.test(e.className)
    },
    e.prototype.needsFocus = function (e) {
        switch (e.nodeName.toLowerCase()) {
            case "textarea":
                return !0;
            case "select":
                return !n;
            case "input":
                switch (e.type) {
                    case "button":
                    case "checkbox":
                    case "file":
                    case "image":
                    case "radio":
                    case "submit":
                        return !1
                }
                return !e.disabled && !e.readOnly;
            default:
                return /\bneedsfocus\b/.test(e.className)
        }
    },
    e.prototype.sendClick = function (e, t) {
        var n, o;
        document.activeElement && document.activeElement !== e && document.activeElement.blur(),
        o = t.changedTouches[0],
        n = document.createEvent("MouseEvents"),
        n.initMouseEvent(this.determineEventType(e), !0, !0, window, 1, o.screenX, o.screenY, o.clientX, o.clientY, !1, !1, !1, !1, 0, null),
        n.forwardedTouchEvent = !0,
        e.dispatchEvent(n)
    },
    e.prototype.determineEventType = function (e) {
        return n && "select" === e.tagName.toLowerCase() ? "mousedown" : "click"
    },
    e.prototype.focus = function (e) {
        var t;
        o && e.setSelectionRange && 0 !== e.type.indexOf("date") && "time" !== e.type && "month" !== e.type ? (t = e.value.length, e.setSelectionRange(t, t)) : e.focus()
    },
    e.prototype.updateScrollParent = function (e) {
        var t, n;
        if (t = e.fastClickScrollParent, !t || !t.contains(e)) {
            n = e;
            do {
                if (n.scrollHeight > n.offsetHeight) {
                    t = n,
                    e.fastClickScrollParent = n;
                    break
                }
                n = n.parentElement
            } while (n)
        }
        t && (t.fastClickLastScrollTop = t.scrollTop)
    },
    e.prototype.getTargetElementFromEventTarget = function (e) {
        return e.nodeType === Node.TEXT_NODE ? e.parentNode : e
    },
    e.prototype.onTouchStart = function (e) {
        var t, n, r;
        if (e.targetTouches.length > 1) return !0;
        if (t = this.getTargetElementFromEventTarget(e.target), n = e.targetTouches[0], o) {
            if (r = window.getSelection(), r.rangeCount && !r.isCollapsed) return !0;
            if (!i) {
                if (n.identifier && n.identifier === this.lastTouchIdentifier) return e.preventDefault(),
                !1;
                this.lastTouchIdentifier = n.identifier,
                this.updateScrollParent(t)
            }
        }
        return this.trackingClick = !0,
        this.trackingClickStart = e.timeStamp,
        this.targetElement = t,
        this.touchStartX = n.pageX,
        this.touchStartY = n.pageY,
        e.timeStamp - this.lastClickTime < this.tapDelay && e.preventDefault(),
        !0
    },
    e.prototype.touchHasMoved = function (e) {
        var t = e.changedTouches[0],
        n = this.touchBoundary;
        return Math.abs(t.pageX - this.touchStartX) > n || Math.abs(t.pageY - this.touchStartY) > n ? !0 : !1
    },
    e.prototype.onTouchMove = function (e) {
        return this.trackingClick ? ((this.targetElement !== this.getTargetElementFromEventTarget(e.target) || this.touchHasMoved(e)) && (this.trackingClick = !1, this.targetElement = null), !0) : !0
    },
    e.prototype.findControl = function (e) {
        return void 0 !== e.control ? e.control : e.htmlFor ? document.getElementById(e.htmlFor) : e.querySelector("button, input:not([type=hidden]), keygen, meter, output, progress, select, textarea")
    },
    e.prototype.onTouchEnd = function (e) {
        var t, a, s, l, c, u = this.targetElement;
        if (!this.trackingClick) return !0;
        if (e.timeStamp - this.lastClickTime < this.tapDelay) return this.cancelNextClick = !0,
        !0;
        if (e.timeStamp - this.trackingClickStart > this.tapTimeout) return !0;
        if (this.cancelNextClick = !1, this.lastClickTime = e.timeStamp, a = this.trackingClickStart, this.trackingClick = !1, this.trackingClickStart = 0, r && (c = e.changedTouches[0], u = document.elementFromPoint(c.pageX - window.pageXOffset, c.pageY - window.pageYOffset) || u, u.fastClickScrollParent = this.targetElement.fastClickScrollParent), s = u.tagName.toLowerCase(), "label" === s) {
            if (t = this.findControl(u)) {
                if (this.focus(u), n) return !1;
                u = t
            }
        } else if (this.needsFocus(u)) return e.timeStamp - a > 100 || o && window.top !== window && "input" === s ? (this.targetElement = null, !1) : (this.focus(u), this.sendClick(u, e), o && "select" === s || (this.targetElement = null, e.preventDefault()), !1);
        if (o && !i && (l = u.fastClickScrollParent, l && l.fastClickLastScrollTop !== l.scrollTop)) return !0;
        if (!this.needsClick(u)) {
            var d, f = this;
            e.preventDefault(),
            d = setTimeout(function () {
                f.sendClick(u, e),
                delete f.clickEvents[d]
            },
            0),
            this.clickEvents[d] = d
        }
        return !1
    },
    e.prototype.onTouchCancel = function () {
        this.trackingClick = !1,
        this.targetElement = null
    },
    e.prototype.onMouse = function (e) {
        return this.targetElement ? e.forwardedTouchEvent ? !0 : e.cancelable && (!this.needsClick(this.targetElement) || this.cancelNextClick) ? (e.stopImmediatePropagation ? e.stopImmediatePropagation() : e.propagationStopped = !0, e.stopPropagation(), e.preventDefault(), !1) : !0 : !0
    },
    e.prototype.onClick = function (e) {
        var t;
        return this.trackingClick ? (this.targetElement = null, this.trackingClick = !1, !0) : "submit" === e.target.type && 0 === e.detail ? !0 : (t = this.onMouse(e), t || (this.targetElement = null), t)
    },
    e.prototype.destroy = function () {
        var e = this.layer;
        n && (e.removeEventListener("mouseover", this.onMouse, !0), e.removeEventListener("mousedown", this.onMouse, !0), e.removeEventListener("mouseup", this.onMouse, !0)),
        e.removeEventListener("click", this.onClick, !0),
        e.removeEventListener("touchstart", this.onTouchStart, !1),
        e.removeEventListener("touchmove", this.onTouchMove, !1),
        e.removeEventListener("touchend", this.onTouchEnd, !1),
        e.removeEventListener("touchcancel", this.onTouchCancel, !1)
    },
    e.notNeeded = function (e) {
        var t, o, i, r;
        if ("undefined" == typeof window.ontouchstart) return !0;
        if (o = +(/Chrome\/([0-9]+)/.exec(navigator.userAgent) || [, 0])[1]) {
            if (!n) return !0;
            if (t = document.querySelector("meta[name=viewport]")) {
                if (-1 !== t.content.indexOf("user-scalable=no")) return !0;
                if (o > 31 && document.documentElement.scrollWidth <= window.outerWidth) return !0
            }
        }
        if (a && (i = navigator.userAgent.match(/Version\/([0-9]*)\.([0-9]*)/), i[1] >= 10 && i[2] >= 3 && (t = document.querySelector("meta[name=viewport]")))) {
            if (-1 !== t.content.indexOf("user-scalable=no")) return !0;
            if (document.documentElement.scrollWidth <= window.outerWidth) return !0
        }
        return "none" === e.style.msTouchAction || "manipulation" === e.style.touchAction ? !0 : (r = +(/Firefox\/([0-9]+)/.exec(navigator.userAgent) || [, 0])[1], r >= 27 && (t = document.querySelector("meta[name=viewport]"), t && (-1 !== t.content.indexOf("user-scalable=no") || document.documentElement.scrollWidth <= window.outerWidth)) ? !0 : "none" === e.style.touchAction || "manipulation" === e.style.touchAction ? !0 : !1)
    },
    e.attach = function (t, n) {
        return new e(t, n)
    },
    window.FastClick = e
}(),
define("vendor/fastclick_release",
function () { }),
define("bower_components/wap_common/base/base", ["require", "zenjs/util/ua", "zenjs/util/args", "Zepto", "zenjs/util/cookie", "vendor/fastclick_release", "zenjs/inline_script/onready"],
function (e) {
    var t = e("zenjs/util/ua"),
    n = (e("zenjs/util/args"), e("Zepto")),
    o = e("zenjs/util/cookie");
    e("vendor/fastclick_release"),
    e("zenjs/inline_script/onready"),
    document.addEventListener("click",
    function () { },
    !0),
    t.isWeixin() && n(".js-mp-info").on("click", ".js-follow-mp",
    function () {
        return window.showGuide && window.showGuide("follow"),
        !1
    }),
    window.afterLoad(function () {
        document.getElementsByClassName("disable-fastclick").length || FastClick && FastClick.attach(document.body)
    });
    var i = function (e) {
        var t = new Image;
        t.onload = t.onerror = function () {
            e(2 == t.height)
        },
        t.src = "data:image/webp;base64,UklGRjoAAABXRUJQVlA4IC4AAACyAgCdASoCAAIALmk0mk0iIiIiIgBoSygABc6WWgAA/veff/0PP8bA//LwYAAA"
    };
    if (window.localStorage) try {
        var r = localStorage.getItem("canwebp");
        "ok" == r ? o("_canwebp", {
            value: "1",
            path: "/",
            domain: "koudaitong.com",
            expires: 3650
        }) : "no" != r && i(function (e) {
            localStorage.setItem("canwebp", e ? "ok" : "no"),
            e && o("_canwebp", {
                value: "1",
                path: "/",
                domain: "koudaitong.com",
                expires: 3650
            })
        })
    } catch (a) { }
}),
define("bower_components/wap_common/base/make_url_log", ["require", "zenjs/util/args", "./log"],
function (e) {
    var t = e("zenjs/util/args");
    e("./log");
    var n = function (e) {
        return "" === e ? (new Date).getTime() : e.indexOf("_") < 0 ? e + "_" + (new Date).getTime() : (e = e.split("_"), e[1] + "_" + (new Date).getTime())
    },
    o = function (e) {
        if ("" === e) return "";
        var e = e.split("."),
        t = (new Date).getTime(),
        n = _global.spm.logType + _global.spm.logId || "fake" + _global.kdt_id;
        switch (e.length) {
            case 1:
                return "";
            case 2:
                e.push(t);
            case 3:
                e.push(n);
                break;
            case 4:
                e.pop(),
                e.push(n)
        }
        return e.join(".")
    },
    i = t.get("mf"),
    r = t.get("sf"),
    a = t.get("reft") || "",
    s = o(t.get("tr")),
    l = t.get("source"),
    c = t.get("kdtfrom"),
    u = t.get("from"),
    d = t.get("promote"),
    f = t.get("fpd"),
    g = "";
    return window.Logger && (g = window.Logger.getSpm()),
    function (e) {
        var o = t.get("source", e);
        return f || (f = t.get("fpd", e)),
        l = o ? o : l,
        t.add(e, {
            reft: n(a),
            spm: g,
            sf: r,
            mf: i,
            tr: s,
            source: l,
            kdtfrom: c,
            form: u,
            promote: d,
            fpd: f
        })
    }
}),
define("bower_components/youzanjsbridge/api", ["Zepto", "zenjs/util/ua", "zenjs/util/args"],
function (e, t, n) {
    var o = "",
    i = Date.now();
    return {
        doCall: function (e, t, n) {
            console.log("YouzanJSBridge API doCall method = " + e),
            console.log(t),
            window.onReady("YouzanJSBridge",
            function () {
                if (n) {
                    var o = "_" + (new Date).getTime() + Math.random().toString().slice(2, 7);
                    window[o] = n;
                    var i = "window." + o;
                    window.YouzanJSBridge.doCall(e, t, i)
                } else window.YouzanJSBridge.doCall(e, t)
            })
        },
        call: function (o, i, r) {
            var a = {
                method: o
            },
            s = arguments;
            window.onReady("YouzanJSBridge",
            function () {
                var o = "_" + (new Date).getTime() + Math.random().toString().slice(2, 7);
                if (window.YouzanJSBridge.callbacks = window.YouzanJSBridge.callbacks || {},
                console.log(s.length), 2 === s.length && e.isPlainObject(i) ? a.data = i : 2 === s.length && e.isFunction(i) ? (a.callback_id = o, window.YouzanJSBridge.callbacks[o] = i) : 3 === s.length && (a.data = i, a.callback_id = o, window.YouzanJSBridge.callbacks[o] = r), t.isIOS()) {
                    var l = "youzan://" + a.method;
                    a.data && (l = n.addParameter(l, {
                        data: JSON.stringify(a.data)
                    })),
                    a.callback_id && (l = n.addParameter(l, {
                        callback_id: a.callback_id
                    }))
                } else if (t.isAndroid()) var l = JSON.stringify(a);
                window.YouzanJSBridge.call(l)
            })
        },
        on: function () {
            var e = [].slice.call(arguments, 0);
            window.onReady("YouzanJSBridge",
            function () {
                window.YouzanJSBridge.on.apply(window.YouzanJSBridge, e)
            })
        },
        configNative: function (e) {
            this.doCall("configNative", e)
        },
        getData: function (e, t, n) {
            t.datatype = e,
            window.onReady("YouzanJSBridge",
            function () {
                window.YouzanJSBridge.once("dataReady:" + e, n)
            }),
            this.doCall("getData", t)
        },
        putData: function (e, t) {
            t.datatype = e,
            this.doCall("putData", t)
        },
        gotoWebview: function (e) {
            o === e.url && Date.now() - i < 100 ? i = Date.now() : this.doCall("gotoWebview", e)
        },
        gotoNative: function (e, t) {
            this.doCall("gotoNative", e, t)
        },
        doAction: function (e) {
            this.doCall("doAction", e)
        }
    }
}),
define("bower_components/wap_common/base/gotoApp", ["require", "zenjs/class", "./make_url_log", "bower_components/youzanjsbridge/api"],
function (e) {
    var t = e("zenjs/class"),
    n = e("./make_url_log"),
    o = e("bower_components/youzanjsbridge/api"),
    i = t.extend({
        init: function () {
            this.doRedirectToApp()
        },
        doRedirectToApp: function () {
            var e = this,
            t = _global.platform;
            $("html").on("click", "a",
            function (o) {
                var i = $(this),
                r = i.attr("href"),
                a = /^https?:\/\//,
                s = a.test(r);
                return "youzanmars" === t && s && !i.closest(".js-no-webview-block").length ? (o.preventDefault(), o.stopPropagation(), e.goToWebView(n(r)), !1) : void 0
            })
        },
        goToWebView: function (e) {
            return o.gotoWebview({
                page: "web",
                url: e
            }),
            !1
        }
    });
    new i
}),
define("bower_components/wap_common/base/logcollection", ["require", "./log", "./logv2"],
function (e) {
    var t = e("./log"),
    n = e("./logv2");
    return {
        log: t,
        logV2: n
    }
}),
define("bower_components/wap_common/base/page_type", ["require", "exports", "module"],
function (e, t, n) {
    function o(e) {
        return void 0 !== r[e]
    }
    function i() {
        return a ? "groupon" : c ? s || l ? "tuan_discount" : "tuan" : u ? "downprice" : d ? g ? "limit_discount_3" : "limit_discount" : s || l ? g ? "discount_3" : "discount" : f ? g ? "normal_3" : "normal" : "unknown"
    }
    var r = window._global,
    a = o("group_on"),
    s = o("meetReduce"),
    l = o("cashBack"),
    c = o("tuan"),
    u = o("auction"),
    d = o("timelimitedDiscount"),
    f = o("goods_id"),
    g = $(".js-add-gift").length > 0,
    p = i();
    n.exports = {
        getPageType: function () {
            return p
        }
    }
}),
define("bower_components/wap_common/base/page_logger", ["require", "exports", "module", "Zepto", "./logcollection", "./make_url_log", "./page_type"],
function (e, t, n) {
    var o = e("Zepto"),
    i = e("./logcollection"),
    r = i.log,
    a = i.logV2,
    s = e("./make_url_log"),
    l = e("./page_type");
    o("body").on("click", "[class*=js-]",
    function (e) {
        a.logClick(o(e.target))
    }),
    o(document).on("click", "a",
    function (e) {
        var t = o(this),
        n = t.attr("href");
        if (n.indexOf("m.iishang.com") > -1) return void e.preventDefault();
        var i = t.data("goods-id"),
        a = t.prop("title") || t.text(),
        l = o.trim(n);
        if (0 !== n.indexOf("#") && "" !== l && 0 !== l.indexOf("javascript") && 0 !== l.indexOf("tel") && !t.hasClass("js-no-follow")) {
            var c = n;
            if (n.match(/^https?:\/\/\S*\.?(koudaitong\.com|kdt\.im|youzan\.com)/)) c = s(n);
            else {
                var u = {
                    fm: "click",
                    url: n,
                    title: o.trim(a)
                };
                e.fromMenu && o.extend(u, {
                    click_type: "menu"
                }),
                null !== i && void 0 !== i && o.extend(u, {
                    click_id: i
                }),
                r.log(u)
            }
            return window.location.href = c,
            !1
        }
    }),
    r.log({
        fm: "display",
        act_name: l.getPageType(),
        act_ver: _global.page_version || "unknown",
        platform: _global.platform
    }),
    r.uaLog()
}),
define("bower_components/wap_common/base/prefetch", ["require"],
function (e) {
    return function (e, t) {
        setTimeout(function () {
            var n = 0,
            o = (new Date).getTime();
            try {
                n = window.localStorage.getItem(e)
            } catch (i) { }
            if (!(n > 0 && 864e5 > o - n)) {
                var r = $("<iframe>").css("display", "none");
                r.attr("src", t).appendTo(document.body);
                try {
                    window.localStorage.setItem(e, o)
                } catch (i) { }
            }
        },
        3e3)
    }
}),
define("bower_components/wap_common/base/main", ["require", "./fullguide", "./lazy_load", "./share", "bower_components/youzanjsbridge/core", "./wx", "./motify", "./footer_auto", "./base", "./gotoApp", "./page_logger", "./prefetch"],
function (e) {
    e("./fullguide"),
    e("./lazy_load"),
    e("./share"),
    e("bower_components/youzanjsbridge/core"),
    e("./wx"),
    e("./motify"),
    e("./footer_auto"),
    e("./base"),
    e("./gotoApp"),
    e("./page_logger");
    var t = e("./prefetch");
    t("staticIframeOpenTime", window._global.url.wap + "/common/prefetching")
}),
require(["bower_components/wap_common/base/main"],
function () { }),
define("main",
function () { });