define("zenjs/backbone/base_view", ["require", "backbone", "../core/trigger_method"],
function (e) {
    var t = e("backbone"),
    i = e("../core/trigger_method");
    return t.View.extend({
        clean: function () {
            return this.stopListening(),
            this
        },
        triggerMethod: i
    })
}),
define("bower_components/zenlist/list", ["require", "exports", "module", "zenjs/backbone/base_view"],
function (e, t, i) {
    var n = function () { },
    o = e("zenjs/backbone/base_view");
    i.exports = o.extend({
        initialize: function (e) {
            return this.options = e = e || {},
            this.items = [],
            this.itemView = e.itemView,
            this.itemOptions = e.itemOptions || {},
            this.collection = e.collection,
            this.onAfterListChange = e.onAfterListChange || n,
            this.onAfterListLoad = e.onAfterListLoad || n,
            this.onAfterListDisplay = e.onAfterListDisplay || n,
            this.onListEmpty = e.onListEmpty || e.onEmptyList || this._onListEmpty,
            this.onItemClick = e.onItemClick || n,
            this.onViewItemAdded = e.onViewItemAdded || n,
            this.displaySize = e.displaySize || -1,
            this.emptyHTML = e.emptyHTML || "",
            this.emptyText = e.emptyText || "列表为空",
            this
        },
        render: function (e) {
            return this.displaySize = -1 == (e || {}).displaySize ? -1 : this.displaySize,
            this.clean(),
            this._setupListeners(),
            this.addAll(),
            this.onAfterListDisplay({
                list: this.collection
            }),
            this
        },
        fetchRender: function (e) {
            return this.collection.fetch({
                data: e,
                success: _(function (e, t) {
                    this.render(),
                    this.onAfterListLoad(this.collection, t),
                    this.onFetchSuccess && this.onFetchSuccess()
                }).bind(this),
                error: _.bind(function () {
                    this.onAfterListLoad(this.collection, response)
                },
                this)
            }),
            this
        },
        _setupListeners: function () {
            this.collection && (this.stopListening(this.collection), this.listenTo(this.collection, "add", this.addItem, this), this.listenTo(this.collection, "reset sort", this.render, this), this.listenTo(this.collection, "remove", this.onItemRemoved, this))
        },
        addItemListeners: function (e) {
            var t = this;
            this.listenTo(e, "all",
            function () {
                var t = "item:" + arguments[0],
                i = _.toArray(arguments);
                i.splice(0, 1),
                i.unshift(t, e),
                this.trigger.apply(this, i),
                "item:click" == t && this.onItemClick()
            }),
            this.listenTo(e.model, "change",
            function () {
                t.onAfterListChange({
                    list: this.collection
                })
            })
        },
        addAll: function () {
            0 === this.collection.length ? this.fetching || this.triggerMethod("list:empty") : this.collection.each(function (e) {
                this.addItem(e)
            },
            this)
        },
        removeAll: function () {
            for (var e = this.items.length - 1; e >= 0; e--) this.removeView(this.items[e]);
            this.onAfterListChange({
                list: this.collection
            })
        },
        addItem: function (e, t, i) {
            if (!(this.displaySize >= 0 && this.items.length >= this.displaySize)) {
                1 == this.collection.length && (this.listEl || this.$el).html("");
                var n = new this.itemView(_.extend({},
                this.options.itemOptions, {
                    model: e,
                    index: this.collection.indexOf(e)
                }));
                this.items.push(n),
                this.addItemListeners(n),
                n.render(),
                this.onViewItemAdded({
                    list: this.items,
                    viewItem: n,
                    model: e
                });
                var o = (i || {}).at;
                return 0 === o ? (this.listEl || this.$el).prepend(n.el) : (this.listEl || this.$el).append(n.el),
                n
            }
        },
        removeItem: function (e) {
            var t = this.getViewByModel(e);
            t && this.removeView(t)
        },
        removeView: function (e) {
            var t;
            this.stopListening(e),
            e && (this.stopListening(e.model), e.remove(), t = this.items.indexOf(e), this.items.splice(t, 1)),
            0 === this.collection.length && (this.fetching || this.triggerMethod("list:empty"))
        },
        onItemRemoved: function (e) {
            this.onAfterListChange({
                list: this.collection,
                action: "remove",
                model: e
            }),
            this.removeItem(e)
        },
        getViewByModel: function (e) {
            return _.find(this.items,
            function (t, i) {
                return t.model === e
            })
        },
        dispatchEventToAllViews: function (e, t) {
            for (var i = this.items.length - 1; i >= 0; i--) this.items[i].trigger(e, t)
        },
        remove: function () {
            o.prototype.remove.call(this, arguments),
            this.removeAll(),
            this.collection.reset(),
            delete this.collection
        },
        clean: function () {
            o.prototype.clean.call(this, arguments),
            this.removeAll(),
            (this.listEl || this.$el).html(""),
            this.stopListening(this.collection)
        },
        _onListEmpty: function () {
            this.$el.html(this.emptyHTML || (this.emptyText ? '<p style="text-align:center;line-height:60px;">' + this.emptyText + "</p>" : ""))
        }
    })
}),
define("zenjs/util/number", [],
function () {
    var e = {
        makeRandomString: function (e) {
            var t = "",
            i = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            e = e || 10;
            for (var n = 0; e > n; n++) t += i.charAt(Math.floor(Math.random() * i.length));
            return t
        }
    };
    return e
}),
define("bower_components/pop/pop", ["require", "zenjs/events", "zenjs/util/number"],
function (e) {
    var t = function () { },
    i = e("zenjs/events"),
    n = e("zenjs/util/number");
    window.zenjs = window.zenjs || {};
    var o = i.extend({
        init: function (e) {
            this._window = $(window);
            var i = n.makeRandomString();
            $("body").append('<div id="' + i + '"                 style="display:none; height: 100%;                 position: fixed; top: 0; left: 0; right: 0;                background-color: rgba(0, 0, 0, ' + (e.transparent || ".9") + ');z-index:1000;opacity:0;transition: opacity ease 0.2s;"></div>'),
            this.nBg = $("#" + i),
            this.nBg.on("click", $.proxy(function () {
                this.isCanNotHide || this.hide()
            },
            this));
            var o = n.makeRandomString();
            $("body").append('<div id="' + o + '" class="' + (e.className || "") + '" style="overflow:hidden;visibility: hidden;"></div>'),
            this.nPopContainer = $("#" + o),
            this.nPopContainer.hide(),
            this.nPopContainer.css({
                opacity: 0,
                position: "absolute",
                "z-index": 1e3
            }),
            e.contentViewClass && (this.contentViewClass = e.contentViewClass, this.contentViewOptions = $.extend({
                el: this.nPopContainer
            },
            e.contentViewOptions || {}), this.contentView = new this.contentViewClass($.extend({
                onHide: $.proxy(this.hide, this)
            },
            this.contentViewOptions)), this.contentView.onHide = $.proxy(this.hide, this)),
            this.animationTime = e.animationTime || 300,
            this.isCanNotHide = e.isCanNotHide,
            this.doNotRemoveOnHide = e.doNotRemoveOnHide || !1,
            this.onShow = e.onShow || t,
            this.onHide = e.onHide || t,
            this.onFinishHide = e.onFinishHide || t,
            this.html = e.html
        },
        render: function (e) {
            return this.renderOptions = e || {},
            this.contentViewClass ? this.contentView.render(this.renderOptions) : this.html && this.nPopContainer.html(this.html),
            this
        },
        show: function () {
            return this.nBg.show().css({
                opacity: "1",
                "transition-property": "none"
            }),
            this.nPopContainer.show(),
            this.trigger("pop:show:before"),
            setTimeout($.proxy(function () {
                this.trigger("pop:show:after"),
                this.nPopContainer.show().css("visibility", "visible"),
                this._doShow && this._doShow(),
                this.onShow()
            },
            this), 200),
            this
        },
        hide: function (e) {
            e = e || {};
            var t = e.doNotRemove || this.doNotRemoveOnHide || !1;
            this._doHide && this._doHide(),
            this.trigger("pop:hide:before"),
            setTimeout($.proxy(function () {
                this.nBg.css({
                    opacity: 0,
                    "transition-property": "opacity"
                }),
                this.trigger("pop:hide:after"),
                setTimeout($.proxy(function () {
                    this.nBg.hide(),
                    this.nPopContainer.hide(),
                    t || this.destroy()
                },
                this), 200)
            },
            this), this.animationTime),
            this.onHide()
        },
        destroy: function () {
            return this.nPopContainer.remove(),
            this.nBg.remove(),
            this.contentView && this.contentView.remove(),
            this
        }
    });
    return o
}),
define("bower_components/pop/pop_forbid_scroll", ["require", "./pop"],
function (e) {
    window.zenjs = window.zenjs || {};
    var t = e("./pop"),
    i = t.extend({
        init: function (e) {
            this._super(e),
            this.on("pop:show:before", _(this.onBeforePopShow).bind(this)),
            this.on("pop:show:after", _(this.onAfterPopShow).bind(this)),
            this.on("pop:hide:after", _(this.onAfterPopHide).bind(this))
        },
        onBeforePopShow: function () {
            this.top = this._window.scrollTop()
        },
        onAfterPopShow: function () {
            this._window.scrollTop(0),
            this.startShow()
        },
        onAfterPopHide: function () {
            var e, t = function (i) {
                return e !== this._window.scrollTop() && i > 0 ? (this._window.scrollTop(e), void setTimeout($.proxy(t, this, i - 1))) : void setTimeout($.proxy(this.onFinishHide, this), 50)
            };
            return function () {
                this.startHide(),
                e = this.top,
                this._window.scrollTop(e),
                $.proxy(t, this)(2),
                setTimeout($.proxy(function () {
                    window.zenjs.popList.length < 1 && $("html").css("position", this.htmlPosition)
                },
                this), 200)
            }
        }(),
        startShow: function () {
            var e = window.zenjs.popList;
            if (e || (e = window.zenjs.popList = []), 0 === e.length) {
                var t = $("body"),
                i = $("html");
                this.htmlPosition = i.css("position"),
                i.css("position", "relative"),
                this.bodyCss = (t.prop("style") || {}).cssText,
                this.htmlCss = (i.prop("style") || {}).cssText,
                $("body,html").css({
                    overflow: "hidden",
                    height: this._window.height()
                })
            }
            e.indexOf(this) < 0 && e.push(this)
        },
        startHide: function () {
            var e = window.zenjs.popList,
            t = e.indexOf(this);
            t > -1 && e.splice(t, 1),
            e.length < 1 && ($("html").attr("style", this.htmlCss || ""), $("body").attr("style", this.bodyCss || ""))
        }
    });
    return i
}),
define("bower_components/pop/popup", ["require", "./pop_forbid_scroll"],
function (e) {
    var t = e("./pop_forbid_scroll"),
    i = t.extend({
        init: function (e) {
            this._super(e),
            this.onClickBg = e.onClickBg ||
            function () { },
            this.onBeforePopupShow = e.onBeforePopupShow ||
            function () { },
            this.onAfterPopupHide = e.onAfterPopupHide ||
            function () { },
            this.nPopContainer.css(_.extend({
                left: 0,
                right: 0,
                bottom: 0,
                background: "white"
            },
            e.containerCss || {})),
            this.nPopContainer.css("opacity", "0"),
            this.nPopContainer.on("focus", "input, textarea",
            function (e) {
                var t = $(this),
                i = $(window),
                n = i.scrollTop(),
                o = t.offset().top; (0 > o - n || o - n > 100) && setTimeout(function () {
                    var e = o - 70;
                    i.scrollTop(e)
                },
                150)
            })
        },
        _doShow: function () {
            this.contentView && this.contentView.height ? this.height = this.contentView.height() : this.contentView || (this.height = this.nPopContainer.height()),
            this.onBeforePopupShow(),
            $(".js-close").click($.proxy(function (e) {
                this.hide()
            },
            this)),
            this.nPopContainer.addClass("popup"),
            this.nPopContainer.css({
                height: this.height + "px",
                transform: "translate3d(0,100%,0)",
                "-webkit-transform": "translate3d(0,100%,0)"
            }),
            this.bodyPadding = $("body").css("padding"),
            $("body").css("padding", "0px"),
            setTimeout($.proxy(function () {
                this.nPopContainer.css({
                    transform: "translate3d(0,0,0)",
                    "-webkit-transform": "translate3d(0,0,0)",
                    "-webkit-transition": "all ease " + this.animationTime + "ms",
                    transition: "all ease " + this.animationTime + "ms",
                    opacity: 1
                })
            },
            this)),
            setTimeout($.proxy(function () {
                this.contentView && this.contentView.onAfterPopupShow && this.contentView.onAfterPopupShow()
            },
            this), this.animationTime)
        },
        _doHide: function (e) {
            this.nPopContainer.css({
                transform: "translate3d(0,100%,0)",
                "-webkit-transform": "translate3d(0,100%,0)",
                opacity: 0
            }),
            setTimeout($.proxy(function () {
                $("body").css("padding", this.bodyPadding),
                this.onAfterPopupHide()
            },
            this), this.animationTime)
        }
    });
    return i
}),
define("text!wap/components/pay/templates/wapwxpay.html", [],
function () {
    return '<div class="header center">微信支付确认</div>\n<div class="content font-size-12">如您已使用微信支付完成付款，请点击“我已支付成功”查看订单；如付款遇到问题，请尝试使用其他方式付款。</div>\n<div class="action-container">\n    <div class="btn-2-1"><button class="btn btn-l btn-green js-ok">我已支付成功</button></div>\n    <div class="btn-2-1"><button class="btn btn-l btn-white js-cancel">使用其他支付方式</button></div>\n</div>\n'
}),
define("text!wap/components/pay/templates/pay_item.html", [],
function () {
    return '<button type="button" data-pay-type="<%= data.code %>" class="btn-pay btn btn-block btn-large btn-<%= data.code %> <%= getBtnClass(data.code) %>">\n	<%= data.name %>\n</button>\n'
}),
define("wap/components/loading", ["bower_components/pop/pop"],
function (e) {
    var t, i = e.extend({
        init: function (e) {
            e = e || {},
            this._super(e),
            this.css = $.extend({
                position: "fixed",
                opacity: 1,
                top: "50%",
                left: "50%",
                "-webkit-transform": "translate3d(-50%, -50%, 0)",
                transform: "translateY(-50%, -50%, 0)"
            }),
            this.nPopContainer.css(this.css)
        }
    });
    return {
        show: function () {
            t = new i({
                html: '<div class="loader-container"><div class="loader center">处理中</div></div>',
                isCanNotHide: !0,
                transparent: ".6"
            }).render().show()
        },
        hide: function () {
            t.hide()
        }
    }
}),
window.Zepto &&
function (e) {
    e.fn.serializeArray = function () {
        var t, i, n = [],
        o = function (e) {
            return e.forEach ? e.forEach(o) : void n.push({
                name: t,
                value: e
            })
        };
        return this[0] && e.each(this[0].elements,
        function (n, s) {
            i = s.type,
            t = s.name,
            t && "fieldset" != s.nodeName.toLowerCase() && !s.disabled && "submit" != i && "reset" != i && "button" != i && "file" != i && ("radio" != i && "checkbox" != i || s.checked) && o(e(s).val())
        }),
        n
    },
    e.fn.serialize = function () {
        var e = [];
        return this.serializeArray().forEach(function (t) {
            e.push(encodeURIComponent(t.name) + "=" + encodeURIComponent(t.value))
        }),
        e.join("&")
    },
    e.fn.submit = function (t) {
        if (0 in arguments) this.bind("submit", t);
        else if (this.length) {
            var i = e.Event("submit");
            this.eq(0).trigger(i),
            i.isDefaultPrevented() || this.get(0).submit()
        }
        return this
    }
}(Zepto),
define("vendor/zepto/form",
function () { }),
window.Utils = window.Utils || {},
$.extend(window.Utils, {
    needConfirm: function (e, t, i) {
        var n = window.confirm(e);
        n ? t && "function" == typeof t && t.apply() : i && "function" == typeof i && i.apply()
    }
}),
define("wap/components/util/confirm",
function () { }),
define("bower_components/pop/popout", ["require", "./pop_forbid_scroll"],
function (e) {
    var t = e("./pop_forbid_scroll"),
    i = t.extend({
        init: function (e) {
            e = e || {},
            this._super(e),
            this.css = $.extend({
                transition: "opacity ease " + this.animationTime + "ms",
                top: "50%",
                left: "50%",
                "-webkit-transform": "translate3d(-50%, -50%, 0)",
                transform: "translateY(-50%, -50%, 0)"
            },
            e.css || {}),
            this.nPopContainer.css(this.css)
        },
        _doShow: function () {
            $(".js-popout-close").click($.proxy(function (e) {
                this.hide()
            },
            this)),
            this.nPopContainer.css("opacity", 1),
            this.nPopContainer.show()
        },
        _doHide: function (e) {
            this.nPopContainer.css({
                opacity: 0
            })
        }
    });
    return i
}),
define("bower_components/pop/popout_box", ["require", "./popout"],
function (e) {
    var t = function () { },
    i = e("./popout"),
    n = i.extend({
        init: function (e) {
            this._super(e),
            this._onOKClicked = e.onOKClicked || t,
            this._onCancelClicked = e.onCancelClicked || t,
            this.preventHideOnOkClicked = e.preventHideOnOkClicked || !1,
            this.width = e.width,
            this.setEventListener()
        },
        setEventListener: function () {
            this.nPopContainer.on("click", ".js-ok", $.proxy(this.onOKClicked, this)),
            this.nPopContainer.on("click", ".js-cancel", $.proxy(this.onCancelClicked, this))
        },
        _doShow: function () {
            this.boxCss = {
                "border-radius": "4px",
                background: "white",
                width: this.width || "270px",
                padding: "15px"
            },
            this.nPopContainer.css(this.boxCss).addClass("popout-box"),
            this._super()
        },
        _doHide: function (e) {
            this._super()
        },
        onOKClicked: function (e) {
            this._onOKClicked(e),
            !this.preventHideOnOkClicked && this.hide()
        },
        onCancelClicked: function (e) {
            this._onCancelClicked(e),
            this.hide()
        }
    });
    return n
}),
define("text!wap/components/pay/templates/password.html", [],
function () {
    return '<div class="header">\n	安全验证\n</div>\n<span class="js-cancel close"></span>\n<div class="popout-content content">\n	<p class="font-size-12">为保证支付账户安全，请输入手机账户<%= account %>的登录密码</p>\n	<p>\n		<input type="password" class="js-password password" placeholder="请输入账户密码"/>\n	</p>\n</div>\n<div class="action-container">\n	<button class="js-ok btn btn-green btn-block">付款</button>\n</div>\n<!--\n<p class="tips clearfix">\n	<a class="font-size-12 c-blue pull-right" href="<%- changePwdUrl %>">忘记登录密码</a>\n</p>\n-->'
}),
define("wap/components/pay/views/password", ["require", "text!wap/components/pay/templates/password.html"],
function (e) {
    var t = function () { },
    i = e("text!wap/components/pay/templates/password.html"),
    n = _.template(i);
    return Backbone.View.extend({
        initialize: function (e) {
            this.account = e.account || "",
            this.onConfirm = e.onConfirm || t
        },
        events: {
            "click .js-ok": "onOKClicked"
        },
        render: function () {
            function e(e) {
                return e = e.toString(),
                e.slice(0, 3) + "****" + e.slice(-3)
            }
            return this.$el.html(n({
                account: e(this.account),
                changePwdUrl: window._global.url.wap + "/buyer/auth/changePassword?redirect_uri=" + encodeURIComponent(window.location.href)
            })),
            this.nPassword = this.$(".js-password"),
            this
        },
        onOKClicked: function (e) {
            var t = this.nPassword.val();
            return t ? void this.onConfirm({
                password: t
            }) : void motify.log("请输入密码")
        }
    })
}),
define("wap/components/pay/pay_item", ["require", "jquery", "underscore", "backbone", "text!wap/components/pay/templates/wapwxpay.html", "text!wap/components/pay/templates/pay_item.html", "wap/components/loading", "vendor/zepto/form", "wap/components/util/confirm", "bower_components/pop/popout_box", "wap/components/pay/views/password", "zenjs/util/ua"],
function (e) {
    var t = e("jquery"),
    i = e("underscore"),
    n = e("backbone"),
    o = e("text!wap/components/pay/templates/wapwxpay.html"),
    s = e("text!wap/components/pay/templates/pay_item.html"),
    r = e("wap/components/loading");
    e("vendor/zepto/form"),
    e("wap/components/util/confirm");
    var a = e("bower_components/pop/popout_box"),
    c = e("wap/components/pay/views/password"),
    l = e("zenjs/util/ua"),
    d = function () { },
    h = n.View.extend({
        template: i.template(s),
        initialize: function (e) {
            this.onOtherPayClicked = e.onOtherPayClicked || d,
            this.payUrl = e.payUrl,
            this.kdt_id = e.kdt_id,
            this.order_no = e.order_no,
            this.account = e.account || "",
            this.wxPayResultUrl = e.wxPayResultUrl,
            this.getPayDataExtr = e.getPayDataExtr,
            this.onPayOrderCreated = e.onPayOrderCreated,
            this.model.on("change", i(this.render).bind(this)),
            this.beforeWxPayRender = e.beforeWxPayRender || d,
            this.onPayBtnClicked = e.onPayBtnClicked || d,
            this.onPayError = e.onPayError || d,
            this.onWxPayError = e.onWxPayError || d,
            this.wxWapPaySuccess = e.wxWapPaySuccess
        },
        events: {
            "click button": "onButtonClick"
        },
        onButtonClick: function (e) {
            if (!this.isClickProcessing) {
                this.isClickProcessing = !0;
                var t = this.$("button"),
                n = t.data("pay-type"),
                o = "";
                return this.onPayBtnClicked(n, t),
                "other" === n ? (this.onOtherPayClicked(), void (this.isClickProcessing = !1)) : "codpay" === n ? (o = "下单提醒：您正在选择货到付款，下单后由商家发货，快递送货上门并收款。", this.model && "到店付款" === this.model.get("name") && (o = "下单提醒：您正在选择到店付款，下单后请自行到店领取并付款。"), (Utils || {}).needConfirm && Utils.needConfirm(o, i(function () {
                    this.doPay(n)
                }).bind(this)), void (this.isClickProcessing = !1)) : "ecard" === n ? void this.getPasswordBeforePay(n) : void this.doPay(n)
            }
        },
        getPasswordBeforePay: function (e) {
            new a({
                contentViewClass: c,
                contentViewOptions: {
                    account: this.account,
                    onConfirm: i(function (t) {
                        this.doPay(e, t.password)
                    }).bind(this)
                },
                isCanNotHide: !0,
                className: "pay-popout",
                preventHideOnOkClicked: !0
            }).render().show(),
            this.isClickProcessing = !1
        },
        doPay: function (e, t) {
            var n = this.getPayDataExtr(e);
            if (!n) return void (this.isClickProcessing = !1);
            var o = {
                order_no: this.order_no,
                kdt_id: this.kdt_id,
                buy_way: e
            };
            t && (o.password = t),
            this.submitPay(i.extend(n, o), e)
        },
        submitPay: function (e, n) {
            var s = this;
            t._ajax({
                url: this.payUrl,
                type: "POST",
                dataType: "json",
                timeout: 15e3,
                data: e,
                cache: !1,
                beforeSend: function () {
                    "wxpay" != n && r.show(),
                    s.$("button").addClass("btn-pay-loading")
                },
                success: function (t) {
                    var r = t.code;
                    switch (s.isClickProcessing = !1, r) {
                        case 0:
                            s.onPayOrderCreated(e);
                            var c = t.data.pay_data,
                            l = t.data.redirect_url,
                            d = t.data.pay_return_url,
                            h = t.data.pay_return_data;
                            switch (n) {
                                case "wxapppay":
                                    s.doFinishWxAppPay(c, h);
                                    break;
                                case "wxpay":
                                    s.doFinishWxPay(c, l, d, h);
                                    break;
                                case "couponpay":
                                case "presentpay":
                                    window.location = c.submit_url;
                                    break;
                                case "ecard":
                                    s.doFinishECardPay(c, l);
                                    break;
                                case "wxwappay":
                                    return s.wapPayPopout = new a({
                                        html: o,
                                        width: "290px",
                                        className: "pay-popout",
                                        onOKClicked: function () {
                                            s.wxWapPaySuccess(t, s),
                                            location.reload()
                                        },
                                        onCancelClicked: function () {
                                            location.reload()
                                        }
                                    }).render().show(),
                                    void (window.location = c.deeplink);
                                default:
                                    if (!c || !c.submit_url) return void motify.log("支付过程出错，请联系客服！");
                                    s.doFinishOtherPay(c)
                            }
                            break;
                        case 11022:
                        case 11023:
                            window.wxReady && window.wxReady(function () {
                                window.WeixinJSBridge && window.WeixinJSBridge.invoke("closeWindow", {})
                            });
                            break;
                        case 11010:
                            window.Utils.needConfirm(t.msg,
                            function () {
                                e.accept_price = 1,
                                s.submitPay(e, n)
                            },
                            function () {
                                motify.log("支付已取消", 0),
                                window.location.reload()
                            });
                            break;
                        case 11012:
                        case 11024:
                        case 11026:
                        case 11027:
                            motify.log("正在跳转...");
                            var p = "wxpay" != n ? window._global.url.trade + "/trade/order/result?order_no=" + s.order_no + "&kdt_id=" + s.kdt_id + "#wechat_webview_type=1" : s.wxPayResultUrl;
                            window.location.href = p;
                            break;
                        case 21e3:
                            window.location.reload();
                            break;
                        case 90001:
                            var u = t.data.item_url,
                            f = i.template(['<div class="content">矮油，动作太慢了，已被抢光了</div>', '<div class="action-container">', '<div class="btn-2-1"><button class="btn btn-l btn-white js-ok">放弃</button></div>', '<div class="btn-2-1"><a href="<%= data.buyUrl %>" class="btn btn-l btn-orange-dark">我要买</a></div>', "</div>"].join(""));
                            s.errorPopout || (s.errorPopout = new a({
                                doNotRemoveOnHide: !0,
                                className: "pay-popout",
                                html: f({
                                    data: {
                                        buyUrl: u
                                    }
                                })
                            }).render()),
                            s.errorPopout.show();
                            break;
                        default:
                            s.render(),
                            motify.log(t.msg)
                    }
                    0 !== r && s.onPayError(r, t.msg || "", t)
                },
                error: function (e, t, i) {
                    s.isClickProcessing = !1,
                    motify.log("生成支付单失败。"),
                    s.render()
                },
                complete: function (e, t) {
                    s.isClickProcessing = !1,
                    s.$("button").removeClass("btn-pay-loading"),
                    "wxpay" != n && r.hide(),
                    s.render()
                }
            })
        },
        doFinishOtherPay: function (e) {
            if (!this.isSubmitting) {
                this.isSubmitting = !0;
                var n = '<form method="post" action="' + e.submit_url + '">';
                delete e.submit_url,
                i(e).map(function (e, t) {
                    n += '<input type="hidden" name="' + t + '" value="' + e + '" />'
                }),
                n += "</form>";
                var o = t(n);
                o.submit(),
                this.isSubmitting = !1
            }
        },
        doFinishWxPay: function (e, n, o, s) {
            return this.wxpayed ? void motify.log("支付数据处理中，请勿重复操作") : (this.wxpayed = !0, "string" == typeof e && (e = t.parseJSON(e)), window.WeixinJSBridge ? (this.beforeWxPayRender(), void window.WeixinJSBridge.invoke("getBrandWCPayRequest", e, i(function (e) {
                var i = e.err_msg;
                this.wxpayed = !1,
                "get_brand_wcpay_request:ok" === i ? (motify.log("支付成功，正在处理订单...", 0), t._ajax({
                    url: o,
                    type: "POST",
                    dataType: "json",
                    timeout: 15e3,
                    data: s,
                    cache: !1,
                    success: function (e) {
                        window.location.href = n
                    }
                })) : "get_brand_wcpay_request:cancel" === i ? l.isIOS() ? this.render() : this.onWxPayError ? this.onWxPayError({
                    payReturnData: s,
                    model: this.model
                }) : this.render() : (this.onWxPayError ? this.onWxPayError({
                    payReturnData: s,
                    model: this.model
                }) : motify.log(i), t._ajax({
                    url: "/v2/pay/api/recordwxjsfailmsg.json",
                    type: "POST",
                    dataType: "json",
                    data: t.extend({
                        wxpay_fail_order_url: n
                    },
                    e)
                }))
            }).bind(this))) : void (this.wxpayed = !1))
        },
        doFinishWxAppPay: function () {
            function e(e, t) {
                t || (t = "weixin"),
                l.isIOS() ? (e = encodeURIComponent(e), document.location.hash = "#func=appWXPay&params=" + e) : l.isAndroid() && window.android && window.android.appWXPay(e)
            }
            return function (t, i) {
                l.isWxd() && l.getPlatformVersion() >= "1.5.0" ? window.YouzanJSBridge && window.YouzanJSBridge.doCall("doAction", {
                    action: "appWXPay",
                    kdt_id: t.kdt_id,
                    order_no: i.order_no,
                    inner_order_no: t.order_no
                }) : (e("kdt_id=" + t.kdt_id + "&order_no=" + t.order_no), window.YouzanJSBridge && window.YouzanJSBridge.doCall("appWXPay", {
                    kdt_id: t.kdt_id,
                    order_no: t.order_no
                }))
            }
        }(),
        doFinishECardPay: function (e, i) {
            e = e || {};
            var n = e.pay_return_url,
            o = e.pay_return_data;
            t._ajax({
                url: n,
                type: "POST",
                dataType: "json",
                timeout: 15e3,
                data: o,
                cache: !1,
                complete: function () {
                    window.location.href = i
                }
            })
        },
        render: function () {
            var e = this,
            t = this.model.toJSON();
            return this.$el.html(this.template(i.extend({
                data: t
            },
            {
                getBtnClass: function (t) {
                    return window.parseInt(e.model.get("order")) > 0 ? " btn-white" : " btn-green"
                }
            }))),
            this.$el.css("margin-bottom", "10px"),
            this
        }
    });
    return h
}),
define("wap/components/pay/pop_pay_list", ["bower_components/pop/popout", "vendor/zepto/form", "wap/components/util/confirm"],
function (e, t, i) {
    var n = Backbone.View.extend({
        className: "pay-way-opts",
        initialize: function (e) {
            this.collection = e.collection,
            $("body").append('<div                 id="confirm-pay-way-opts-popup-bg"                 style="display:none; width: 100%; height: 100%;                 position: fixed; top:0; left:0;                 background-color: rgba(0, 0, 0, .5);"></div>'),
            this.bg = $("#confirm-pay-way-opts-popup-bg"),
            this.bg.on("click", _(this.hide).bind(this)),
            this.listOpt = {
                el: this.$el,
                itemView: PayItemView,
                collection: this.collection,
                itemOptions: e.itemOptions
            }
        },
        events: {
            "click #confirm-pay-way-opts-popup-bg": "hide"
        },
        render: function () {
            return this.payWayListView = new ListView(this.listOpt).render(),
            this
        },
        show: function () {
            this.$el.addClass("active"),
            this.bg.show()
        },
        hide: function () {
            this.$el.removeClass("active"),
            this.bg.hide()
        }
    });
    return n
}),
define("wap/components/pay/pay", ["require", "bower_components/zenlist/list", "bower_components/pop/popup", "wap/components/pay/pay_item", "wap/components/pay/pop_pay_list", "backbone", "underscore"],
function (e) {
    var t = function () { },
    i = e("bower_components/zenlist/list"),
    n = e("bower_components/pop/popup"),
    o = e("wap/components/pay/pay_item"),
    s = (e("wap/components/pay/pop_pay_list"), e("backbone")),
    r = e("underscore");
    return s.View.extend({
        initialize: function (e) {
            this.collection = new s.Collection,
            this.nPayTips = e.nPayTips,
            this.itemOptions = e.itemOptions || {},
            this.pagePayWaySize = e.pagePayWaySize || 3,
            this.payUrl = e.payUrl || window._global.url.trade + "/trade/order/pay.json",
            this.order_no = e.order_no || window._global.order_no,
            this.kdt_id = e.kdt_id || window._global.kdt_id,
            this.orderPrice = e.orderPrice,
            this.account = e.account || "",
            this.getPayDataExtr = e.getPayDataExtr ||
            function () {
                return {}
            },
            this.wxPayResultUrl = e.wxPayResultUrl,
            this.onPayOrderCreated = e.onPayOrderCreated || t,
            this.otherPayText = e.otherPayText,
            this.beforeWxPayRender = e.beforeWxPayRender || t,
            this.wxWapPaySuccess = e.wxWapPaySuccess || t;
            var i = e.payWays,
            n = new s.Collection;
            r.each(i,
            function (e, t) {
                e.id = t,
                e.order = t,
                n.add(new s.Model(e))
            }),
            this.listOpt = {
                el: this.$el,
                itemView: o,
                collection: this.collection,
                itemOptions: r.extend({},
                this.itemOptions, {
                    onOtherPayClicked: r(this.onOtherPayClicked).bind(this),
                    payUrl: this.payUrl,
                    order_no: this.order_no,
                    kdt_id: this.kdt_id,
                    account: this.account,
                    getPayDataExtr: this.getPayDataExtr,
                    wxPayResultUrl: this.wxPayResultUrl,
                    onPayOrderCreated: this.onPayOrderCreated,
                    beforeWxPayRender: this.beforeWxPayRender,
                    wxWapPaySuccess: this.wxWapPaySuccess
                }),
                emptyHTML: e.emptyHTML || " "
            },
            this.allPayWayCollection = n,
            this.initPagePayWay()
        },
        render: function () {
            return this.payWayListView = new i(this.listOpt).render(),
            this
        },
        initPagePayWay: function () {
            if (0 === this.allPayWayCollection.length) return this.nPayTips && this.nPayTips.html("无可用的支付方式"),
            this;
            if (this.allPayWayCollection.length <= this.pagePayWaySize) for (var e = 0; e < this.allPayWayCollection.length; e++) this.collection.add(this.allPayWayCollection.get(e));
            else {
                for (var e = 0; e < this.pagePayWaySize - 1; e++) this.collection.add(this.allPayWayCollection.get(e));
                var t = this.allPayWayCollection.findWhere({
                    code: "aliwap"
                });
                t && this.collection.add(t),
                this.collection.add(new s.Model({
                    code: "other",
                    name: this.otherPayText || "其他支付方式",
                    order: e
                }))
            }
        },
        addHotIcon: function (e) {
            var t = this.allPayWayCollection.findWhere({
                code: e
            });
            if (t) {
                var i = t.get("name") + '<span class="hot"></span>';
                t.set("name", i);
                var n = this.collection.findWhere({
                    code: e
                });
                if (n) return void n.set("name", i);
                var o = this.collection.findWhere({
                    code: "other"
                });
                o && o.set("name", o.get("name") + '<span class="hot"></span>')
            }
        },
        initPopPayWayListView: function () {
            var e = r.clone(this.listOpt);
            delete e.el,
            e.collection = new s.Collection(this.allPayWayCollection.filter(function (e) {
                return e.get("hide") !== !0
            })),
            this.popPayWayListView = new n({
                contentViewClass: i,
                className: "confirm-pay-way-opts-popup",
                contentViewOptions: e,
                containerCss: {
                    padding: "10px"
                },
                doNotRemoveOnHide: !0
            }).render().show()
        },
        onOtherPayClicked: function () {
            this.initPopPayWayListView()
        },
        updatePayWay: function (e, t) {
            var i = this.allPayWayCollection.find(function (e) {
                return e.get("code") == t.code
            });
            if (i) for (var n in t) t.hasOwnProperty(n) && i.set(n, t[n])
        },
        updatePayWaySelffetch: function (e) {
            this.isSelffetch = e
        },
        updateValue: function (e) {
            if (e) {
                var t = this,
                i = ["payUrl", "order_no", "kdt_id"];
                r.each(e,
                function (e, n, o) {
                    t[n] = e,
                    i.indexOf(n) > -1 && (t.listOpt.itemOptions[n] = e, t.payWayListView && r.each(t.payWayListView.items,
                    function (t) {
                        t[n] = e
                    }), t.popPayWayListView && r.each(t.popPayWayListView.items,
                    function (t) {
                        t[n] = e
                    }))
                })
            }
        }
    })
}),
define("text!wap/components/keyboard/templates/index.html", [],
function () {
    return '<div class="ui-keyboard js-keyboard">\n    <ul class="ui-keyboard-numbers js-num">\n        <li>1</li><li>2</li><li>3</li>\n        <li>4</li><li>5</li><li>6</li>\n        <li>7</li><li>8</li><li>9</li>\n        <li class="zero">0</li><li>.</li>\n    </ul>\n    <ul class="ui-keyboard-buttons">\n        <li class="btn-del js-del"><</li>\n        <li class="btn-ok js-ok">完成</li>\n    </ul>\n</div>'
}),
define("wap/components/keyboard/main", ["text!wap/components/keyboard/templates/index.html"],
function (e) {
    return Backbone.View.extend({
        initialize: function (t) {
            this.$el.html(e),
            t = t || {},
            this.$container = $(t.container),
            this.$trigger = $(t.trigger),
            this.rules = t.rules || /./,
            this.cacheValue = t.defaultValue || "",
            delete t.container,
            delete t.trigger,
            delete t.rules,
            delete t.defaultValue
        },
        render: function (e) {
            return this.$container.append(this.$el),
            this.$keyboard = this.$el.find(".js-keyboard"),
            this.$el.delegate(".js-num", "touchstart", this.numberHandler.bind(this)),
            this.$el.delegate(".js-ok", "touchstart", this.okHandler.bind(this)),
            this.$el.delegate(".js-del", "touchstart", this.deleteHandler.bind(this)),
            this
        },
        show: function () {
            return this.$keyboard.hasClass("on") || (this.$keyboard.addClass("on"), this.trigger("show")),
            this
        },
        hide: function () {
            return this.$keyboard.removeClass("on"),
            this.trigger("hide"),
            this
        },
        numberHandler: function (e) {
            var t = $(e.target).html(),
            i = this;
            this.rules.test(this.cacheValue + t) && ("0" === this.cacheValue && "." !== t ? this.cacheValue = "" : "" === this.cacheValue && "." === t && (this.cacheValue = "0"), this.cacheValue = this.cacheValue + t, this.numTimer && clearTimeout(this.timer), this.numTimer = setTimeout(function () {
                var e = i.isFormElements(i.$trigger[0]) ? "val" : "html";
                i.$trigger[e](i.cacheValue)
            },
            15))
        },
        okHandler: function () {
            setTimeout(this.hide.bind(this), 0)
        },
        deleteHandler: function () {
            var e = this.cacheValue.length - 1,
            t = this;
            this.cacheValue = this.cacheValue.substring(0, e),
            this.delTimer && clearTimeout(this.timer),
            this.delTimer = setTimeout(function () {
                var e = t.isFormElements(t.$trigger[0]) ? "val" : "html";
                t.$trigger[e](t.cacheValue)
            },
            15)
        },
        isFormElements: function (e) {
            if (!e) return !1;
            var t = e.tagName.toLowerCase();
            return ["input", "textarea", "select"].indexOf(t) > -1
        }
    })
}),
define("text!wap/trade/cashier/decrease/tpl/decrease.html", [],
function () {
    return '<ul class="form">\n  <li class="block-item">\n    <label>\n        每满<%= base %>减<%= decrease %>元\n        <% if (limit && limit > 0) { %>\n            （最高优惠<%= limit %>）\n        <% } %>\n    </label>\n    <span class="text-right">-<%= real_decrease %></span>\n  </li>\n  <li class="block-item">\n    <label>实际支付</label>\n    <span class="text-right total-input">￥<%= total %></span>\n  </li>\n</ul>\n'
}),
define("wap/trade/cashier/decrease/main", ["wap/components/keyboard/main", "text!wap/trade/cashier/decrease/tpl/decrease.html"],
function (e, t) {
    var i = Backbone.View.extend({
        template: _.template(t),
        initialize: function (t) {
            var i = this,
            n = $("#js-cashier-cursor"),
            o = $(".js-cashier-tip"),
            s = $("#cashier-price");
            if (i.el = t.el, i.trigger = t.trigger, i.price = t.price, s.length > 0) {
                var r = new e({
                    container: ".container",
                    trigger: "#cashier-price",
                    rules: /^\d*\.?\d{0,2}$/
                }).render();
                setTimeout(function () {
                    r.show()
                },
                100),
                r.on("show",
                function () {
                    n.show(),
                    o.addClass("keyboard-on")
                }).on("hide",
                function () {
                    n.hide(),
                    o.removeClass("keyboard-on"),
                    i.render(i.trigger.html())
                }),
                i.trigger.parent().on("click",
                function (e) {
                    r.show()
                })
            }
        },
        getDecreaseData: function () {
            return this.decrease_id
        },
        render: function (e) {
            var t = this;
            e = e || t.price,
            e && $._ajax({
                url: _global.url.trade + "/trade/cashier/scanreduce.json",
                type: "POST",
                data: {
                    kdt_id: _global.kdt_id,
                    real_pay: e
                },
                success: function (e) {
                    if (0 !== e.code) return void motify.log(e.msg);
                    var i, n = e.data;
                    n.has_scan_reduce ? (i = t.template({
                        base: n.meet_quota,
                        decrease: n.reduce_quota,
                        real_decrease: n.reduce,
                        limit: n.limit_quota,
                        total: n.real_pay
                    }), $(t.el).html(i)) : $(t.el).html(""),
                    t.decrease_id = n.scan_reduce_id
                },
                error: function (e) {
                    motify.log("收银台支付信息获取失败")
                }
            })
        }
    });
    return i
}),
define("bower_components/aes/aes", ["require", "exports", "module"],
function (e, t, i) {
    var n = n ||
    function (e, t) {
        var i = {},
        n = i.lib = {},
        o = function () { },
        s = n.Base = {
            extend: function (e) {
                o.prototype = this;
                var t = new o;
                return e && t.mixIn(e),
                t.hasOwnProperty("init") || (t.init = function () {
                    t.$super.init.apply(this, arguments)
                }),
                t.init.prototype = t,
                t.$super = this,
                t
            },
            create: function () {
                var e = this.extend();
                return e.init.apply(e, arguments),
                e
            },
            init: function () { },
            mixIn: function (e) {
                for (var t in e) e.hasOwnProperty(t) && (this[t] = e[t]);
                e.hasOwnProperty("toString") && (this.toString = e.toString)
            },
            clone: function () {
                return this.init.prototype.extend(this)
            }
        },
        r = n.WordArray = s.extend({
            init: function (e, i) {
                e = this.words = e || [],
                this.sigBytes = i != t ? i : 4 * e.length
            },
            toString: function (e) {
                return (e || c).stringify(this)
            },
            concat: function (e) {
                var t = this.words,
                i = e.words,
                n = this.sigBytes;
                if (e = e.sigBytes, this.clamp(), n % 4) for (var o = 0; e > o; o++) t[n + o >>> 2] |= (i[o >>> 2] >>> 24 - 8 * (o % 4) & 255) << 24 - 8 * ((n + o) % 4);
                else if (65535 < i.length) for (o = 0; e > o; o += 4) t[n + o >>> 2] = i[o >>> 2];
                else t.push.apply(t, i);
                return this.sigBytes += e,
                this
            },
            clamp: function () {
                var t = this.words,
                i = this.sigBytes;
                t[i >>> 2] &= 4294967295 << 32 - 8 * (i % 4),
                t.length = e.ceil(i / 4)
            },
            clone: function () {
                var e = s.clone.call(this);
                return e.words = this.words.slice(0),
                e
            },
            random: function (t) {
                for (var i = [], n = 0; t > n; n += 4) i.push(4294967296 * e.random() | 0);
                return new r.init(i, t)
            }
        }),
        a = i.enc = {},
        c = a.Hex = {
            stringify: function (e) {
                var t = e.words;
                e = e.sigBytes;
                for (var i = [], n = 0; e > n; n++) {
                    var o = t[n >>> 2] >>> 24 - 8 * (n % 4) & 255;
                    i.push((o >>> 4).toString(16)),
                    i.push((15 & o).toString(16))
                }
                return i.join("")
            },
            parse: function (e) {
                for (var t = e.length,
                i = [], n = 0; t > n; n += 2) i[n >>> 3] |= parseInt(e.substr(n, 2), 16) << 24 - 4 * (n % 8);
                return new r.init(i, t / 2)
            }
        },
        l = a.Latin1 = {
            stringify: function (e) {
                var t = e.words;
                e = e.sigBytes;
                for (var i = [], n = 0; e > n; n++) i.push(String.fromCharCode(t[n >>> 2] >>> 24 - 8 * (n % 4) & 255));
                return i.join("")
            },
            parse: function (e) {
                for (var t = e.length,
                i = [], n = 0; t > n; n++) i[n >>> 2] |= (255 & e.charCodeAt(n)) << 24 - 8 * (n % 4);
                return new r.init(i, t)
            }
        },
        d = a.Utf8 = {
            stringify: function (e) {
                try {
                    return decodeURIComponent(escape(l.stringify(e)))
                } catch (t) {
                    throw Error("Malformed UTF-8 data")
                }
            },
            parse: function (e) {
                return l.parse(unescape(encodeURIComponent(e)))
            }
        },
        h = n.BufferedBlockAlgorithm = s.extend({
            reset: function () {
                this._data = new r.init,
                this._nDataBytes = 0
            },
            _append: function (e) {
                "string" == typeof e && (e = d.parse(e)),
                this._data.concat(e),
                this._nDataBytes += e.sigBytes
            },
            _process: function (t) {
                var i = this._data,
                n = i.words,
                o = i.sigBytes,
                s = this.blockSize,
                a = o / (4 * s),
                a = t ? e.ceil(a) : e.max((0 | a) - this._minBufferSize, 0);
                if (t = a * s, o = e.min(4 * t, o), t) {
                    for (var c = 0; t > c; c += s) this._doProcessBlock(n, c);
                    c = n.splice(0, t),
                    i.sigBytes -= o
                }
                return new r.init(c, o)
            },
            clone: function () {
                var e = s.clone.call(this);
                return e._data = this._data.clone(),
                e
            },
            _minBufferSize: 0
        });
        n.Hasher = h.extend({
            cfg: s.extend(),
            init: function (e) {
                this.cfg = this.cfg.extend(e),
                this.reset()
            },
            reset: function () {
                h.reset.call(this),
                this._doReset()
            },
            update: function (e) {
                return this._append(e),
                this._process(),
                this
            },
            finalize: function (e) {
                return e && this._append(e),
                this._doFinalize()
            },
            blockSize: 16,
            _createHelper: function (e) {
                return function (t, i) {
                    return new e.init(i).finalize(t)
                }
            },
            _createHmacHelper: function (e) {
                return function (t, i) {
                    return new p.HMAC.init(e, i).finalize(t)
                }
            }
        });
        var p = i.algo = {};
        return i
    }(Math); !
    function () {
        var e = n,
        t = e.lib.WordArray;
        e.enc.Base64 = {
            stringify: function (e) {
                var t = e.words,
                i = e.sigBytes,
                n = this._map;
                e.clamp(),
                e = [];
                for (var o = 0; i > o; o += 3) for (var s = (t[o >>> 2] >>> 24 - 8 * (o % 4) & 255) << 16 | (t[o + 1 >>> 2] >>> 24 - 8 * ((o + 1) % 4) & 255) << 8 | t[o + 2 >>> 2] >>> 24 - 8 * ((o + 2) % 4) & 255, r = 0; 4 > r && i > o + .75 * r; r++) e.push(n.charAt(s >>> 6 * (3 - r) & 63));
                if (t = n.charAt(64)) for (; e.length % 4;) e.push(t);
                return e.join("")
            },
            parse: function (e) {
                var i = e.length,
                n = this._map,
                o = n.charAt(64);
                o && (o = e.indexOf(o), -1 != o && (i = o));
                for (var o = [], s = 0, r = 0; i > r; r++) if (r % 4) {
                    var a = n.indexOf(e.charAt(r - 1)) << 2 * (r % 4),
                    c = n.indexOf(e.charAt(r)) >>> 6 - 2 * (r % 4);
                    o[s >>> 2] |= (a | c) << 24 - 8 * (s % 4),
                    s++
                }
                return t.create(o, s)
            },
            _map: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/="
        }
    }(),
    function (e) {
        function t(e, t, i, n, o, s, r) {
            return e = e + (t & i | ~t & n) + o + r,
            (e << s | e >>> 32 - s) + t
        }
        function i(e, t, i, n, o, s, r) {
            return e = e + (t & n | i & ~n) + o + r,
            (e << s | e >>> 32 - s) + t
        }
        function o(e, t, i, n, o, s, r) {
            return e = e + (t ^ i ^ n) + o + r,
            (e << s | e >>> 32 - s) + t
        }
        function s(e, t, i, n, o, s, r) {
            return e = e + (i ^ (t | ~n)) + o + r,
            (e << s | e >>> 32 - s) + t
        }
        for (var r = n,
        a = r.lib,
        c = a.WordArray,
        l = a.Hasher,
        a = r.algo,
        d = [], h = 0; 64 > h; h++) d[h] = 4294967296 * e.abs(e.sin(h + 1)) | 0;
        a = a.MD5 = l.extend({
            _doReset: function () {
                this._hash = new c.init([1732584193, 4023233417, 2562383102, 271733878])
            },
            _doProcessBlock: function (e, n) {
                for (var r = 0; 16 > r; r++) {
                    var a = n + r,
                    c = e[a];
                    e[a] = 16711935 & (c << 8 | c >>> 24) | 4278255360 & (c << 24 | c >>> 8)
                }
                var r = this._hash.words,
                a = e[n + 0],
                c = e[n + 1],
                l = e[n + 2],
                h = e[n + 3],
                p = e[n + 4],
                u = e[n + 5],
                f = e[n + 6],
                m = e[n + 7],
                y = e[n + 8],
                w = e[n + 9],
                g = e[n + 10],
                v = e[n + 11],
                b = e[n + 12],
                _ = e[n + 13],
                x = e[n + 14],
                k = e[n + 15],
                C = r[0],
                P = r[1],
                j = r[2],
                F = r[3],
                C = t(C, P, j, F, a, 7, d[0]),
                F = t(F, C, P, j, c, 12, d[1]),
                j = t(j, F, C, P, l, 17, d[2]),
                P = t(P, j, F, C, h, 22, d[3]),
                C = t(C, P, j, F, p, 7, d[4]),
                F = t(F, C, P, j, u, 12, d[5]),
                j = t(j, F, C, P, f, 17, d[6]),
                P = t(P, j, F, C, m, 22, d[7]),
                C = t(C, P, j, F, y, 7, d[8]),
                F = t(F, C, P, j, w, 12, d[9]),
                j = t(j, F, C, P, g, 17, d[10]),
                P = t(P, j, F, C, v, 22, d[11]),
                C = t(C, P, j, F, b, 7, d[12]),
                F = t(F, C, P, j, _, 12, d[13]),
                j = t(j, F, C, P, x, 17, d[14]),
                P = t(P, j, F, C, k, 22, d[15]),
                C = i(C, P, j, F, c, 5, d[16]),
                F = i(F, C, P, j, f, 9, d[17]),
                j = i(j, F, C, P, v, 14, d[18]),
                P = i(P, j, F, C, a, 20, d[19]),
                C = i(C, P, j, F, u, 5, d[20]),
                F = i(F, C, P, j, g, 9, d[21]),
                j = i(j, F, C, P, k, 14, d[22]),
                P = i(P, j, F, C, p, 20, d[23]),
                C = i(C, P, j, F, w, 5, d[24]),
                F = i(F, C, P, j, x, 9, d[25]),
                j = i(j, F, C, P, h, 14, d[26]),
                P = i(P, j, F, C, y, 20, d[27]),
                C = i(C, P, j, F, _, 5, d[28]),
                F = i(F, C, P, j, l, 9, d[29]),
                j = i(j, F, C, P, m, 14, d[30]),
                P = i(P, j, F, C, b, 20, d[31]),
                C = o(C, P, j, F, u, 4, d[32]),
                F = o(F, C, P, j, y, 11, d[33]),
                j = o(j, F, C, P, v, 16, d[34]),
                P = o(P, j, F, C, x, 23, d[35]),
                C = o(C, P, j, F, c, 4, d[36]),
                F = o(F, C, P, j, p, 11, d[37]),
                j = o(j, F, C, P, m, 16, d[38]),
                P = o(P, j, F, C, g, 23, d[39]),
                C = o(C, P, j, F, _, 4, d[40]),
                F = o(F, C, P, j, a, 11, d[41]),
                j = o(j, F, C, P, h, 16, d[42]),
                P = o(P, j, F, C, f, 23, d[43]),
                C = o(C, P, j, F, w, 4, d[44]),
                F = o(F, C, P, j, b, 11, d[45]),
                j = o(j, F, C, P, k, 16, d[46]),
                P = o(P, j, F, C, l, 23, d[47]),
                C = s(C, P, j, F, a, 6, d[48]),
                F = s(F, C, P, j, m, 10, d[49]),
                j = s(j, F, C, P, x, 15, d[50]),
                P = s(P, j, F, C, u, 21, d[51]),
                C = s(C, P, j, F, b, 6, d[52]),
                F = s(F, C, P, j, h, 10, d[53]),
                j = s(j, F, C, P, g, 15, d[54]),
                P = s(P, j, F, C, c, 21, d[55]),
                C = s(C, P, j, F, y, 6, d[56]),
                F = s(F, C, P, j, k, 10, d[57]),
                j = s(j, F, C, P, f, 15, d[58]),
                P = s(P, j, F, C, _, 21, d[59]),
                C = s(C, P, j, F, p, 6, d[60]),
                F = s(F, C, P, j, v, 10, d[61]),
                j = s(j, F, C, P, l, 15, d[62]),
                P = s(P, j, F, C, w, 21, d[63]);
                r[0] = r[0] + C | 0,
                r[1] = r[1] + P | 0,
                r[2] = r[2] + j | 0,
                r[3] = r[3] + F | 0
            },
            _doFinalize: function () {
                var t = this._data,
                i = t.words,
                n = 8 * this._nDataBytes,
                o = 8 * t.sigBytes;
                i[o >>> 5] |= 128 << 24 - o % 32;
                var s = e.floor(n / 4294967296);
                for (i[(o + 64 >>> 9 << 4) + 15] = 16711935 & (s << 8 | s >>> 24) | 4278255360 & (s << 24 | s >>> 8), i[(o + 64 >>> 9 << 4) + 14] = 16711935 & (n << 8 | n >>> 24) | 4278255360 & (n << 24 | n >>> 8), t.sigBytes = 4 * (i.length + 1), this._process(), t = this._hash, i = t.words, n = 0; 4 > n; n++) o = i[n],
                i[n] = 16711935 & (o << 8 | o >>> 24) | 4278255360 & (o << 24 | o >>> 8);
                return t
            },
            clone: function () {
                var e = l.clone.call(this);
                return e._hash = this._hash.clone(),
                e
            }
        }),
        r.MD5 = l._createHelper(a),
        r.HmacMD5 = l._createHmacHelper(a)
    }(Math),
    function () {
        var e = n,
        t = e.lib,
        i = t.Base,
        o = t.WordArray,
        t = e.algo,
        s = t.EvpKDF = i.extend({
            cfg: i.extend({
                keySize: 4,
                hasher: t.MD5,
                iterations: 1
            }),
            init: function (e) {
                this.cfg = this.cfg.extend(e)
            },
            compute: function (e, t) {
                for (var i = this.cfg,
                n = i.hasher.create(), s = o.create(), r = s.words, a = i.keySize, i = i.iterations; r.length < a;) {
                    c && n.update(c);
                    var c = n.update(e).finalize(t);
                    n.reset();
                    for (var l = 1; i > l; l++) c = n.finalize(c),
                    n.reset();
                    s.concat(c)
                }
                return s.sigBytes = 4 * a,
                s
            }
        });
        e.EvpKDF = function (e, t, i) {
            return s.create(i).compute(e, t)
        }
    }(),
    n.lib.Cipher ||
    function (e) {
        var t = n,
        i = t.lib,
        o = i.Base,
        s = i.WordArray,
        r = i.BufferedBlockAlgorithm,
        a = t.enc.Base64,
        c = t.algo.EvpKDF,
        l = i.Cipher = r.extend({
            cfg: o.extend(),
            createEncryptor: function (e, t) {
                return this.create(this._ENC_XFORM_MODE, e, t)
            },
            createDecryptor: function (e, t) {
                return this.create(this._DEC_XFORM_MODE, e, t)
            },
            init: function (e, t, i) {
                this.cfg = this.cfg.extend(i),
                this._xformMode = e,
                this._key = t,
                this.reset()
            },
            reset: function () {
                r.reset.call(this),
                this._doReset()
            },
            process: function (e) {
                return this._append(e),
                this._process()
            },
            finalize: function (e) {
                return e && this._append(e),
                this._doFinalize()
            },
            keySize: 4,
            ivSize: 4,
            _ENC_XFORM_MODE: 1,
            _DEC_XFORM_MODE: 2,
            _createHelper: function (e) {
                return {
                    encrypt: function (t, i, n) {
                        return ("string" == typeof i ? m : f).encrypt(e, t, i, n)
                    },
                    decrypt: function (t, i, n) {
                        return ("string" == typeof i ? m : f).decrypt(e, t, i, n)
                    }
                }
            }
        });
        i.StreamCipher = l.extend({
            _doFinalize: function () {
                return this._process(!0)
            },
            blockSize: 1
        });
        var d = t.mode = {},
        h = function (t, i, n) {
            var o = this._iv;
            o ? this._iv = e : o = this._prevBlock;
            for (var s = 0; n > s; s++) t[i + s] ^= o[s]
        },
        p = (i.BlockCipherMode = o.extend({
            createEncryptor: function (e, t) {
                return this.Encryptor.create(e, t)
            },
            createDecryptor: function (e, t) {
                return this.Decryptor.create(e, t)
            },
            init: function (e, t) {
                this._cipher = e,
                this._iv = t
            }
        })).extend();
        p.Encryptor = p.extend({
            processBlock: function (e, t) {
                var i = this._cipher,
                n = i.blockSize;
                h.call(this, e, t, n),
                i.encryptBlock(e, t),
                this._prevBlock = e.slice(t, t + n)
            }
        }),
        p.Decryptor = p.extend({
            processBlock: function (e, t) {
                var i = this._cipher,
                n = i.blockSize,
                o = e.slice(t, t + n);
                i.decryptBlock(e, t),
                h.call(this, e, t, n),
                this._prevBlock = o
            }
        }),
        d = d.CBC = p,
        p = (t.pad = {}).Pkcs7 = {
            pad: function (e, t) {
                for (var i = 4 * t,
                i = i - e.sigBytes % i,
                n = i << 24 | i << 16 | i << 8 | i,
                o = [], r = 0; i > r; r += 4) o.push(n);
                i = s.create(o, i),
                e.concat(i)
            },
            unpad: function (e) {
                e.sigBytes -= 255 & e.words[e.sigBytes - 1 >>> 2]
            }
        },
        i.BlockCipher = l.extend({
            cfg: l.cfg.extend({
                mode: d,
                padding: p
            }),
            reset: function () {
                l.reset.call(this);
                var e = this.cfg,
                t = e.iv,
                e = e.mode;
                if (this._xformMode == this._ENC_XFORM_MODE) var i = e.createEncryptor;
                else i = e.createDecryptor,
                this._minBufferSize = 1;
                this._mode = i.call(e, this, t && t.words)
            },
            _doProcessBlock: function (e, t) {
                this._mode.processBlock(e, t)
            },
            _doFinalize: function () {
                var e = this.cfg.padding;
                if (this._xformMode == this._ENC_XFORM_MODE) {
                    e.pad(this._data, this.blockSize);
                    var t = this._process(!0)
                } else t = this._process(!0),
                e.unpad(t);
                return t
            },
            blockSize: 4
        });
        var u = i.CipherParams = o.extend({
            init: function (e) {
                this.mixIn(e)
            },
            toString: function (e) {
                return (e || this.formatter).stringify(this)
            }
        }),
        d = (t.format = {}).OpenSSL = {
            stringify: function (e) {
                var t = e.ciphertext;
                return e = e.salt,
                (e ? s.create([1398893684, 1701076831]).concat(e).concat(t) : t).toString(a)
            },
            parse: function (e) {
                e = a.parse(e);
                var t = e.words;
                if (1398893684 == t[0] && 1701076831 == t[1]) {
                    var i = s.create(t.slice(2, 4));
                    t.splice(0, 4),
                    e.sigBytes -= 16
                }
                return u.create({
                    ciphertext: e,
                    salt: i
                })
            }
        },
        f = i.SerializableCipher = o.extend({
            cfg: o.extend({
                format: d
            }),
            encrypt: function (e, t, i, n) {
                n = this.cfg.extend(n);
                var o = e.createEncryptor(i, n);
                return t = o.finalize(t),
                o = o.cfg,
                u.create({
                    ciphertext: t,
                    key: i,
                    iv: o.iv,
                    algorithm: e,
                    mode: o.mode,
                    padding: o.padding,
                    blockSize: e.blockSize,
                    formatter: n.format
                })
            },
            decrypt: function (e, t, i, n) {
                return n = this.cfg.extend(n),
                t = this._parse(t, n.format),
                e.createDecryptor(i, n).finalize(t.ciphertext)
            },
            _parse: function (e, t) {
                return "string" == typeof e ? t.parse(e, this) : e
            }
        }),
        t = (t.kdf = {}).OpenSSL = {
            execute: function (e, t, i, n) {
                return n || (n = s.random(8)),
                e = c.create({
                    keySize: t + i
                }).compute(e, n),
                i = s.create(e.words.slice(t), 4 * i),
                e.sigBytes = 4 * t,
                u.create({
                    key: e,
                    iv: i,
                    salt: n
                })
            }
        },
        m = i.PasswordBasedCipher = f.extend({
            cfg: f.cfg.extend({
                kdf: t
            }),
            encrypt: function (e, t, i, n) {
                return n = this.cfg.extend(n),
                i = n.kdf.execute(i, e.keySize, e.ivSize),
                n.iv = i.iv,
                e = f.encrypt.call(this, e, t, i.key, n),
                e.mixIn(i),
                e
            },
            decrypt: function (e, t, i, n) {
                return n = this.cfg.extend(n),
                t = this._parse(t, n.format),
                i = n.kdf.execute(i, e.keySize, e.ivSize, t.salt),
                n.iv = i.iv,
                f.decrypt.call(this, e, t, i.key, n)
            }
        })
    }(),
    function () {
        for (var e = n,
        t = e.lib.BlockCipher,
        i = e.algo,
        o = [], s = [], r = [], a = [], c = [], l = [], d = [], h = [], p = [], u = [], f = [], m = 0; 256 > m; m++) f[m] = 128 > m ? m << 1 : m << 1 ^ 283;
        for (var y = 0,
        w = 0,
        m = 0; 256 > m; m++) {
            var g = w ^ w << 1 ^ w << 2 ^ w << 3 ^ w << 4,
            g = g >>> 8 ^ 255 & g ^ 99;
            o[y] = g,
            s[g] = y;
            var v = f[y],
            b = f[v],
            _ = f[b],
            x = 257 * f[g] ^ 16843008 * g;
            r[y] = x << 24 | x >>> 8,
            a[y] = x << 16 | x >>> 16,
            c[y] = x << 8 | x >>> 24,
            l[y] = x,
            x = 16843009 * _ ^ 65537 * b ^ 257 * v ^ 16843008 * y,
            d[g] = x << 24 | x >>> 8,
            h[g] = x << 16 | x >>> 16,
            p[g] = x << 8 | x >>> 24,
            u[g] = x,
            y ? (y = v ^ f[f[f[_ ^ v]]], w ^= f[f[w]]) : y = w = 1
        }
        var k = [0, 1, 2, 4, 8, 16, 32, 64, 128, 27, 54],
        i = i.AES = t.extend({
            _doReset: function () {
                for (var e = this._key,
                t = e.words,
                i = e.sigBytes / 4,
                e = 4 * ((this._nRounds = i + 6) + 1), n = this._keySchedule = [], s = 0; e > s; s++) if (i > s) n[s] = t[s];
                else {
                    var r = n[s - 1];
                    s % i ? i > 6 && 4 == s % i && (r = o[r >>> 24] << 24 | o[r >>> 16 & 255] << 16 | o[r >>> 8 & 255] << 8 | o[255 & r]) : (r = r << 8 | r >>> 24, r = o[r >>> 24] << 24 | o[r >>> 16 & 255] << 16 | o[r >>> 8 & 255] << 8 | o[255 & r], r ^= k[s / i | 0] << 24),
                    n[s] = n[s - i] ^ r
                }
                for (t = this._invKeySchedule = [], i = 0; e > i; i++) s = e - i,
                r = i % 4 ? n[s] : n[s - 4],
                t[i] = 4 > i || 4 >= s ? r : d[o[r >>> 24]] ^ h[o[r >>> 16 & 255]] ^ p[o[r >>> 8 & 255]] ^ u[o[255 & r]]
            },
            encryptBlock: function (e, t) {
                this._doCryptBlock(e, t, this._keySchedule, r, a, c, l, o)
            },
            decryptBlock: function (e, t) {
                var i = e[t + 1];
                e[t + 1] = e[t + 3],
                e[t + 3] = i,
                this._doCryptBlock(e, t, this._invKeySchedule, d, h, p, u, s),
                i = e[t + 1],
                e[t + 1] = e[t + 3],
                e[t + 3] = i
            },
            _doCryptBlock: function (e, t, i, n, o, s, r, a) {
                for (var c = this._nRounds,
                l = e[t] ^ i[0], d = e[t + 1] ^ i[1], h = e[t + 2] ^ i[2], p = e[t + 3] ^ i[3], u = 4, f = 1; c > f; f++) var m = n[l >>> 24] ^ o[d >>> 16 & 255] ^ s[h >>> 8 & 255] ^ r[255 & p] ^ i[u++],
                y = n[d >>> 24] ^ o[h >>> 16 & 255] ^ s[p >>> 8 & 255] ^ r[255 & l] ^ i[u++],
                w = n[h >>> 24] ^ o[p >>> 16 & 255] ^ s[l >>> 8 & 255] ^ r[255 & d] ^ i[u++],
                p = n[p >>> 24] ^ o[l >>> 16 & 255] ^ s[d >>> 8 & 255] ^ r[255 & h] ^ i[u++],
                l = m,
                d = y,
                h = w;
                m = (a[l >>> 24] << 24 | a[d >>> 16 & 255] << 16 | a[h >>> 8 & 255] << 8 | a[255 & p]) ^ i[u++],
                y = (a[d >>> 24] << 24 | a[h >>> 16 & 255] << 16 | a[p >>> 8 & 255] << 8 | a[255 & l]) ^ i[u++],
                w = (a[h >>> 24] << 24 | a[p >>> 16 & 255] << 16 | a[l >>> 8 & 255] << 8 | a[255 & d]) ^ i[u++],
                p = (a[p >>> 24] << 24 | a[l >>> 16 & 255] << 16 | a[d >>> 8 & 255] << 8 | a[255 & h]) ^ i[u++],
                e[t] = m,
                e[t + 1] = y,
                e[t + 2] = w,
                e[t + 3] = p
            },
            keySize: 8
        });
        e.AES = t._createHelper(i)
    }(),
    n.pad.Iso10126 = {
        pad: function (e, t) {
            var i = 4 * t,
            i = i - e.sigBytes % i;
            e.concat(n.lib.WordArray.random(i - 1)).concat(n.lib.WordArray.create([i << 24], 1))
        },
        unpad: function (e) {
            e.sigBytes -= 255 & e.words[e.sigBytes - 1 >>> 2]
        }
    },
    i.exports = n
}),
define("bower_components/aes/main", ["require", "./aes"],
function (e) {
    var t = e("./aes"),
    i = t.enc.Utf8.parse("youzan.com.aesiv"),
    n = t.enc.Utf8.parse("youzan.com._key_");
    return {
        encrypt: function (e) {
            return e = t.enc.Utf8.parse(e),
            t.AES.encrypt(e, n, {
                mode: t.mode.CBC,
                padding: t.pad.Iso10126,
                iv: i
            }).toString()
        }
    }
}),
define("text!bower_components/login/templates/init.html", [],
function () {
    return '<form class="js-login-form popout-login" method="GET" action="">\n    <div class="header c-green center">\n        <h2>请填写您的手机号码</h2>\n    </div>\n    <fieldset class="wrapper-form font-size-14">\n        <div class="form-item">\n            <label for="phone">手机号</label>\n            <input id="phone" name="phone" type="tel" maxlength="11" autocomplete="off" placeholder="" value="<%= phone %>">\n        </div>\n        <div class="js-help-info font-size-12 error c-orange"></div>\n    </fieldset>\n    <div class="action-container">\n        <input type="submit" class="js-confirm btn btn-green btn-block font-size-14" value="确认手机号码" />\n    </div>\n</form>\n'
}),
define("text!bower_components/login/templates/login.html", [],
function () {
    return '<form class="js-login-form popout-login" method="GET" action="">\n    <div class="header c-green center">\n        <h2>该号码注册过，请直接登录</h2>\n    </div>\n    <fieldset class="wrapper-form font-size-14">\n        <div class="form-item">\n            <label for="phone">手机号</label>\n            <input id="phone" name="phone" type="tel" maxlength="11" autocomplete="off" placeholder="请输入你的手机号" disabled="disabled" value="<%= phone %>">\n        </div>\n        <div class="form-item">\n            <label for="password">密码</label>\n            <input id="passsword" name="password"  type="password" autocomplete="off" placeholder="请输入登录密码">\n        </div>\n        <div class="js-help-info font-size-12 error c-orange"></div>\n    </fieldset>\n    <div class="action-container">\n        <button type="button" class="js-confirm btn btn-green btn-block font-size-14">\n            <%= showBindText ? \'登录并绑定\' : \'确认\' %>\n        </button>\n    </div>\n    <div class="bottom-tips font-size-12">\n        <span class="c-orange">如果您忘了密码，请</span><a href="javascript:;" class="js-change-pwd c-blue">点此找回密码</a>\n        <a href="javascript:;" class="js-change-phone c-blue pull-right">更换手机号</a>\n    </div>\n</form>\n'
}),
define("text!bower_components/login/templates/register.html", [],
function () {
    return '<form class="js-login-form popout-login" method="GET" action="">\n    <div class="header c-green center">\n        <h2>注册有赞帐号</h2>\n    </div>\n    <fieldset class="wrapper-form font-size-14">\n        <div class="form-item">\n            <label for="phone">手机号</label>\n            <input id="phone" name="phone" type="tel" maxlength="11" autocomplete="off" placeholder="请输入你的手机号" disabled="disabled" value="<%= phone %>">\n        </div>\n        <div class="form-item js-image-verify hide">\n            <label for="verifycode">身份校验</label>\n            <input id="verifycode" name="verifycode" class="js-verify-code item-input"  type="tel" style="width:178px" maxlength="6" autocomplete="off" placeholder="输入右侧数字">\n            <img class="js-verify-image verify-image" src="">\n        </div>\n        <div class="form-item">\n            <label for="code">验证码</label>\n            <input id="code" name="code"  type="text" style="width:178px" maxlength="6" autocomplete="off" placeholder="输入短信验证码">\n            <button type="button" class="js-auth-code tag btn-auth-code tag-green font-size-12" data-text="获取验证码">\n                获取验证码\n            </button>\n        </div>\n        <div class="form-item">\n            <label for="password">密码</label>\n            <input id="passsword" name="password"  type="password" autocomplete="off" maxlength="20" placeholder="请输入8-20位数字和字母组合">\n        </div>\n        <div class="js-help-info font-size-12 error c-orange"></div>\n    </fieldset>\n    <div class="action-container">\n        <button type="button" class="js-confirm btn btn-green btn-block font-size-14">\n            <%= showBindText ? \'注册并绑定\' : \'确认\' %>\n        </button>\n    </div>\n    <div class="bottom-tips font-size-12">\n        <span class="c-orange">如果您忘了密码，请</span><a href="javascript:;" class="js-change-pwd c-blue">点此找回密码</a>\n        <a href="javascript:;" class="js-change-phone c-blue pull-right">更换手机号</a>\n    </div>\n</form>\n'
}),
define("text!bower_components/login/templates/change_pwd.html", [],
function () {
    return '<form class="js-login-form popout-login" method="GET" action="">\n    <div class="header c-green center">\n        <h2><%if(isSetting){%>设定<%}else{%>找回<%}%>帐号密码</h2>\n    </div>\n    <fieldset class="wrapper-form font-size-14">\n        <div class="form-item">\n            <label for="phone">手机号</label>\n            <input id="phone" name="phone" type="tel" maxlength="11" autocomplete="off" placeholder="请输入你的手机号" disabled="disabled" value="<%= phone %>">\n        </div>\n        <div class="form-item js-image-verify hide">\n            <label for="verifycode">身份校验</label>\n            <input id="verifycode" name="verifycode" class="js-verify-code item-input"  type="tel" style="width:178px" maxlength="6" autocomplete="off" placeholder="输入右侧数字">\n            <img class="js-verify-image verify-image" src="">\n        </div>\n        <div class="form-item">\n            <label for="code">验证码</label>\n            <input id="code" name="code"  type="text" style="width:178px" maxlength="6" autocomplete="off" placeholder="输入短信验证码">\n            <button type="button" class="js-auth-code tag btn-auth-code font-size-12 tag-green" data-text="获取验证码">\n                获取验证码\n            </button>\n        </div>\n        <div class="form-item">\n            <label for="password">密码</label>\n            <input id="passsword" name="password"  type="password" autocomplete="off" placeholder="设置新的8-20位数字和字母组合密码">\n        </div>\n        <div class="js-help-info font-size-12 error c-orange"></div>\n    </fieldset>\n    <div class="action-container">\n        <button type="button" class="js-confirm btn btn-green btn-block font-size-14">确定</button>\n    </div>\n    <div class="bottom-tips pull-right">\n        <a href="javascript:;" class="js-login inline-item c-blue">已有帐号登录</a>\n        <a href="javascript:;" class="js-register inline-item c-blue">注册新帐号</a>\n    </div>\n</form>\n'
}),
define("zenjs/util/valid", [],
function () {
    window.zenjs = window.zenjs || {};
    var e = {
        validMobile: function (e) {
            return e = "" + e,
            /^((\+86)|(86))?(1)\d{10}$/.test(e)
        },
        validPhone: function (e) {
            return e = "" + e,
            /^0[0-9\-]{10,13}$/.test(e)
        },
        validNumber: function (e) {
            return /^\d+$/.test(e)
        },
        validEmail: function (e) {
            return /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(e)
        },
        validPostalCode: function (e) {
            return e = "" + e,
            /^\d{6}$/.test(e)
        }
    };
    return window.zenjs.Valid = e,
    e
}),
define("zenjs/util/form", ["require", "exports", "module", "vendor/zepto/form", "jquery"],
function (e, t, i) {
    e("vendor/zepto/form");
    var n = e("jquery");
    window.zenjs = window.zenjs || {};
    var o = {
        getFormData: function (e) {
            var t = e.serializeArray(),
            i = {};
            return n.map(t,
            function (e) {
                i[e.name] = e.value
            }),
            i
        }
    };
    window.zenjs.Form = o,
    i.exports = o
}),
define("zenjs/sms_fetch/main", ["require", "exports", "module", "bower_components/ajax/ajax", "jquery"],
function (e, t, i) {
    function n() {
        this.loadingLock = !1,
        this.isUsed = void 0
    }
    function o(e) {
        e = e || {},
        this.$el = s(e.el || e.$el || "<div></div>"),
        this.el = this.$el[0],
        this.$ = function (e) {
            return this.$el.find(e)
        },
        this.initialize && this.initialize(e)
    }
    e("bower_components/ajax/ajax");
    var s = e("jquery"),
    r = function () { };
    n.prototype = {
        fetch: function () {
            if (this.isUsed !== !1) {
                var e = this;
                this.loadingLock = !0,
                s._ajax({
                    url: window._global.url.www.replace(/^https?:\/\//, "//") + "/common/token/token.jsonp",
                    type: "get",
                    dataType: "jsonp"
                }).done(function (t) {
                    0 == t.code ? (e.token = t.data, e.loadingLock = !1, e.isUsed = !1) : motify.log(t.msg)
                }).fail(function () {
                    motify.log("token 获取失败")
                })
            }
        },
        get: function () {
            return this.isUsed = !0,
            this.token
        }
    };
    var a = new n;
    s.extend(o.prototype, {
        initialize: function (e) {
            this.duration = e.time || 60,
            this.step = e.step || 1100,
            this.codeVerifyClass = e.codeVerifyClass || "js-verify-code",
            this.verifyType = "smsFetch";
            var t = window._global.url.www.replace(/^https?:\/\//, "//");
            this.smsFetchUrl = t + "/common/sms/captcha.jsonp",
            this.imgUrl = t + "/common/sms/imgcaptcha",
            this.imgVerifyUrl = t + "/common/sms/imgcaptcha.jsonp",
            this.biz = e.biz || "kdt_account_captcha",
            this.onTimeChange = e.onTimeChange || r,
            this.onTimeEnd = e.onTimeEnd || r,
            this.onTimerStart = e.onTimerStart || r,
            this.onTimerClose = e.onTimerClose || r,
            this.onVerifyPictureShow = e.onVerifyPictureShow || r,
            this.onGetCodeError = e.onGetCodeError || r,
            this.onVerifyPictureSuccess = e.onVerifyPictureSuccess || r,
            this.onVerifyPictureError = e.onVerifyPictureError || r,
            this.platform = e.platform || "",
            this.subFrom = e.subFrom || "",
            a.fetch()
        },
        setMobile: function (e) {
            e && (this.mobile = e)
        },
        getImageCode: function () {
            return s.trim(this.$("." + this.codeVerifyClass).val())
        },
        getSms: function (e) {
            var t = this;
            if (a.loadingLock) return void motify.log("数据加载中，稍后再试");
            if (e = e || {},
            e.mobile && (this.mobile = e.mobile), !this.mobile) return !1;
            var i = {
                smsFetch: t.onSmsFetchHandler,
                image: t.onImageHandler
            };
            return t.startTimer.call(t),
            (i[t.verifyType] || r).call(t),
            this
        },
        startTimer: function () {
            this.onTimerStart(),
            this.btnCountdown(this.duration)
        },
        stopTimer: function () {
            clearTimeout(this.timer),
            this.onTimerClose()
        },
        btnCountdown: function (e) {
            var t = this;
            this.onTimeChange({
                second: e
            }),
            --e >= 0 ? this.timer = setTimeout(function () {
                t.btnCountdown(e)
            },
            this.step) : (this.onTimeEnd(), this.timer = "")
        },
        onVerifyImageShow: function (e) {
            this.$(".js-image-verify").removeClass("hide"),
            this.$(".js-verify-image").attr("src", e)
        },
        onVerifyImageHide: function () {
            this.$(".js-image-verify").addClass("hide")
        },
        onSmsFetchHandler: function () {
            var e = 1;
            return function () {
                var t = this,
                i = {
                    verifyTimes: e,
                    mobile: this.mobile,
                    biz: this.biz,
                    token: a.get()
                };
                this.platform && (i.platform = this.platform),
                this.subFrom && (i.sub_from = this.subFrom),
                s._ajax({
                    url: this.smsFetchUrl,
                    dataType: "jsonp",
                    data: i,
                    success: function (i) {
                        return 0 == i.code ? void e++ : (t.stopTimer.call(t), t.onGetCodeError.call(t), void (10111 === i.code ? (t.verifyType = "image", t.onVerifyImageShow(t.imgUrl), t.onVerifyPictureShow()) : (e++, motify.log(i.msg))))
                    },
                    error: function (i, n, o) {
                        e++,
                        t.stopTimer.call(t),
                        t.onGetCodeError.call(t),
                        motify.log("获取验证码失败，请稍后再试")
                    },
                    complete: function (e, t) { }
                }).always(function () {
                    a.fetch()
                })
            }
        }(),
        onImageHandler: function () {
            var e = this,
            t = this.mobile;
            s._ajax({
                url: this.imgVerifyUrl,
                dataType: "jsonp",
                data: {
                    mobile: t,
                    captcha_code: this.getImageCode()
                },
                success: function (i) {
                    return 0 === i.code ? (e.verifyType = "smsFetch", e.mobile = t, e.onVerifyImageHide(), e.onVerifyPictureSuccess(), void e.onSmsFetchHandler()) : (e.stopTimer.call(e), e.onVerifyPictureError.call(e), void (10100 === i.code ? (motify.log(i.msg), e.$el.find(".js-verify-image").attr("src", e.imgUrl)) : motify.log(i.msg)))
                },
                error: function (t, i, n) {
                    e.stopTimer.call(e),
                    e.onVerifyPictureError.call(e),
                    motify.log("图形验证失败，重试一下吧~"),
                    e.$el.find(".js-verify-image").attr("src", e.imgUrl)
                },
                complete: function (e, t) { }
            })
        }
    }),
    i.exports = o
}),
define("zenjs/util/url_helper", [],
function () {
    var e = {
        site: function (e, t) {
            var i = window._global.env,
            n = window._global.url;
            "static" === t && "online" === i && (t = "cdn_static");
            var o = e;
            return t = t ? n[t] || "" : "",
            -1 == e.search(/^http[s]?\:\/\//) && ("/" !== e[0] && (e = "/" + e), o = t + e),
            o
        },
        getCdnImageUrl: function (e, t) {
            if (!e) return window._global.url.cdn_static + "/image/wap/no_pic.png";
            if (t = e.match(/.+\!\d+x\d+.+/) ? "" : t && t.length > 0 ? t : "!100x100.jpg", e.match(/^(https?:)?\/\//i)) {
                for (var i = [/^(https?:)?\/\/imgqn.koudaitong.com/, /^(https?:)?\/\/kdt-img.koudaitong.com/, /^(https?:)?\/\/img.yzcdn.cn/, /^(https?:)?\/\/dn-kdt-img.qbox.me/], n = 0; n < i.length; n++) e = e.replace(i[n], window._global.url.imgqn);
                return e + t
            }
            return window._global.url.imgqn + "/" + e + t
        }
    };
    return e
}),
define("bower_components/login/main", ["require", "bower_components/ajax/ajax", "jquery", "backbone", "bower_components/aes/main", "text!./templates/init.html", "text!./templates/login.html", "text!./templates/register.html", "text!./templates/change_pwd.html", "zenjs/util/valid", "zenjs/util/form", "zenjs/sms_fetch/main", "zenjs/util/url_helper", "zenjs/util/ua"],
function (e) {
    e("bower_components/ajax/ajax");
    var t = e("jquery"),
    i = e("backbone"),
    n = e("bower_components/aes/main"),
    o = e("text!./templates/init.html"),
    s = e("text!./templates/login.html"),
    r = e("text!./templates/register.html"),
    a = e("text!./templates/change_pwd.html"),
    c = e("zenjs/util/valid"),
    l = e("zenjs/util/form"),
    d = e("zenjs/sms_fetch/main"),
    h = e("zenjs/util/url_helper"),
    p = e("zenjs/util/ua"),
    u = ["youzanmars"],
    f = i.View.extend({
        events: {
            "click .js-confirm": "onConfirmClicked",
            "click .js-change-phone": "onChangePhoneClicked",
            "click .js-change-pwd": "onChangePwdClicked",
            "click .js-login": "onLoginClicked",
            "click .js-register": "onRegisterClicked",
            "click .js-auth-code": "onAuthcodeClicked",
            "submit .js-login-form": "onConfirmClicked"
        },
        initialize: function (e) {
            e = e || {};
            var i = p.getPlatform();
            if (u.indexOf(i) > -1) return void (window.location.href = h.site("/buyer/kdtunion?redirect_uri=" + encodeURIComponent(window.location.href), "wap"));
            var n = this,
            c = _global.url;
            this.tpl_map = {
                init: _.template(e.initTpl || o),
                login: _.template(e.loginTpl || s),
                register: _.template(e.registerTpl || r),
                changePwd: _.template(e.changePwdTpl || a)
            },
            this.valid_map = {
                checkPhone: _(this.checkPhone).bind(this),
                checkPwd: _(this.checkPwd).bind(this),
                checkCode: _(this.checkCode).bind(this)
            },
            this.renderOpt = e.renderOpt || {
                type: "init",
                phone: ""
            },
            this.renderOpt.showBindText = p.isWeixin() || p.isQQ(),
            this.urlMap = {
                login: e.loginUrl || c.wap + "/buyer/auth/authlogin.json",
                register: e.registerUrl || c.wap + "/buyer/auth/authRegister.json",
                changePwd: e.changePwdUrl || c.wap + "/buyer/auth/changePassword.json",
                confirm: e.confirmUrl || c.wap + "/buyer/auth/authConfirm.json"
            },
            this.source = e.source || 2,
            this.ajaxType = e.ajaxType || "POST",
            this.afterLogin = e.afterLogin ||
            function () { };
            var l, f;
            p.isMobile() ? (l = "app", f = "wsc", p.isWxd() ? f = "wxd" : p.isWsc() ? f = "wsc" : p.isPf() ? f = "pf" : l = "wap") : l = "pc",
            this.sms = new d({
                el: this.$el,
                onTimeChange: function (e) {
                    var i = e.second;
                    t(n.nAuthCode).text("等待 " + i + "秒")
                },
                onTimeEnd: function () {
                    n.nAuthCode.text("再次获取"),
                    n.nAuthCode.prop("disabled", !1),
                    n.nAuthCode.removeClass("disabled"),
                    n.nCodeInput.attr("placeholder", "没有收到验证码？")
                },
                onTimerStart: function () {
                    n.nAuthCode.prop("disabled", !0),
                    n.nAuthCode.addClass("disabled")
                },
                onVerifyPictureError: function () {
                    n.nAuthCode.removeAttr("disabled"),
                    n.nAuthCode.removeClass("disabled"),
                    n.nAuthCode.text("再次获取")
                },
                onGetCodeError: function () {
                    n.nAuthCode.removeAttr("disabled"),
                    n.nAuthCode.removeClass("disabled"),
                    n.nAuthCode.text("再次获取")
                },
                onVerifyPictureShow: function () {
                    n.nHelpInfo.html("操作过于频繁，请先输入图像验证码再获取")
                },
                onVerifyPictureSuccess: function () {
                    n.nHelpInfo.html("")
                },
                platform: l,
                subFrom: f
            })
        },
        render: function () {
            return this.appLogined ? this : (this.$el.html(this.tpl_map[this.renderOpt.type](this.renderOpt)), this.nForm = this.$(".js-login-form"), this.nHelpInfo = this.$(".js-help-info"), this.nPhone = this.$('input[name="phone"]'), this.nPwd = this.$('input[name="password"]'), this.nCodeInput = this.$('input[name="authcode"]'), this.nAuthCode = this.$(".js-auth-code"), this)
        },
        show: function (e, t) {
            "changePwd" == e ? this.sms.biz = "reset_account_passwd" : this.sms.biz = "kdt_account_captcha",
            _.extend(this.renderOpt, {
                type: e
            },
            t || {
                isSetting: !1
            }),
            this.render(this.renderOpt),
            this.$el.show(this.animationTime)
        },
        onConfirmClicked: function (e) {
            e.preventDefault();
            var i = this,
            o = t(e.target),
            s = l.getFormData(i.nForm);
            if (s = _.extend(i.renderOpt, s), !i.validate(s)) return !1;
            s.source = this.source,
            s.password && (s.password = n.encrypt(s.password));
            var r = o.html();
            if ("init" === i.renderOpt.type) i.renderOpt.phone = s.phone,
            t._ajax({
                url: this.urlMap.confirm,
                type: "POST",
                dataType: "json",
                timeout: 15e3,
                data: s,
                xhrFields: {
                    withCredentials: !0
                },
                beforeSend: function () {
                    o.html("确认中..."),
                    o.prop("disabled", !0)
                },
                success: function (e) {
                    switch (+e.code) {
                        case 0:
                            i.show("login");
                            break;
                        case 200:
                            i.show("register");
                            break;
                        case 300:
                            i.show("changePwd", {
                                isSetting: !0
                            });
                        default:
                            i.nHelpInfo.html(e.msg)
                    }
                },
                error: function () {
                    i.nHelpInfo.html("出错啦，请重试")
                },
                complete: function () {
                    o.html(r),
                    o.prop("disabled", !1)
                }
            });
            else {
                var a = i.renderOpt.type;
                t._ajax({
                    url: this.urlMap[a],
                    type: this.ajaxType,
                    dataType: "json",
                    timeout: 15e3,
                    data: s,
                    xhrFields: {
                        withCredentials: !0
                    },
                    beforeSend: function () {
                        o.html("确认中..."),
                        o.prop("disabled", !0)
                    },
                    success: function (e) {
                        0 === e.code ? i.afterLogin(e, {
                            type: a
                        }) : i.nHelpInfo.html(e.msg)
                    },
                    error: function () {
                        i.nHelpInfo.html("出错啦，请重试")
                    },
                    complete: function () {
                        o.html(r),
                        o.prop("disabled", !1)
                    }
                })
            }
        },
        onAuthcodeClicked: function (e) {
            e.preventDefault();
            var t = this,
            i = l.getFormData(t.nForm);
            i = _.extend(t.renderOpt, i);
            var n = i.phone;
            this.sms.setMobile(n),
            this.sms.getSms()
        },
        onChangePhoneClicked: function (e) {
            e.preventDefault(),
            this.show("init")
        },
        onChangePwdClicked: function (e) {
            e.preventDefault(),
            this.sms.stopTimer(),
            this.show("changePwd")
        },
        onLoginClicked: function (e) {
            e.preventDefault(),
            this.show("init")
        },
        onRegisterClicked: function (e) {
            e.preventDefault(),
            this.show("init")
        },
        validate: function () {
            var e = {
                init: ["checkPhone"],
                login: ["checkPwd"],
                register: ["checkCode", "checkPwd"],
                changePwd: ["checkCode", "checkPwd"]
            };
            return function (t) {
                return _.every(e[t.type], _(function (e) {
                    return this.valid_map[e](t)
                }).bind(this))
            }
        }(),
        checkPhone: function (e) {
            return "" === e.phone ? (this.nPhone.focus(), this.nHelpInfo.html("请填写您的手机号码"), !1) : c.validMobile(e.phone) ? !0 : (this.nPhone.focus(), this.nHelpInfo.html("请填写11位手机号码"), !1)
        },
        checkPwd: function (e) {
            function t(e) {
                var t = /[a-zA-Z]/g,
                i = /[0-9]/g,
                n = 0,
                o = t.test(e),
                s = i.test(e);
                return o && s && (n = 1),
                n
            }
            if ("" === e.password) return this.nPwd.focus(),
            this.nHelpInfo.html("请输入您的密码"),
            !1;
            if ("login" !== this.renderOpt.type && e.password.length < 8) return this.nPwd.focus(),
            this.nHelpInfo.html("亲，密码最短为8位"),
            !1;
            if ("login" !== this.renderOpt.type && e.password.length > 20) return this.nPwd.focus(),
            this.nHelpInfo.html("亲，密码最长为20位"),
            !1;
            if ("login" !== this.renderOpt.type) {
                var i = t(e.password);
                if (!i) return this.nPwd.focus(),
                this.nHelpInfo.html("亲，密码为8-20位数字和字母组合"),
                !1
            }
            return !0
        },
        checkCode: function (e) {
            return c.validPostalCode(e.code) ? !0 : (this.nCodeInput.focus(), this.nHelpInfo.html("请填写6位短信验证码"), !1)
        }
    });
    return f
}),
define("text!wap/trade/cashier/pf/pf_follow.html", [],
function () {
    return '<div class="pf-follow-pop js-follow-pop">\n	<p class="pf-follow-text font-size-14">关注我的店铺，享受优惠并了解新款商品</p>\n	<a href="javascript:void(0)" class="btn follow-btn js-cancel">暂不关注</a>\n	<a href="javascript:void(0)" class="btn btn-green js-ok">立即关注</a>\n</div>'
}),
require(["zenjs/class", "bower_components/pop/popout_box", "bower_components/login/main", "text!wap/trade/cashier/pf/pf_follow.html", "zenjs/util/args"],
function (e, t, i, n, o) {
    var s = e.extend({
        init: function () {
            var e = window._global,
            i = e.mp_data.team_type,
            s = e.isTeamFollow,
            r = e.fixedMoney,
            a = this;
            if (this.redirectUrl = e.url.pf + "/wholesale/follow?qr_id=" + o.get("qr_id") + "&kdt_id=" + o.get("kdt_id"), this.buyerId = e.buyer_id, "pf" === i && !s && !r) {
                new t({
                    html: n,
                    animationTime: 300,
                    transparent: ".8",
                    onOKClicked: function () {
                        a.loginFunc()
                    }
                }).render().show()
            }
        },
        loginFunc: function () {
            var e = this;
            if (this.buyerId <= 0) {
                var n = new t({
                    contentViewClass: i,
                    contentViewOptions: {
                        afterLogin: function (t) {
                            n.hide(),
                            e.followStore()
                        }
                    }
                }).render(),
                o = $(n.contentView.el).find(".action-container");
                o.addClass("pf-only"),
                o.find(".js-confirm").removeClass("btn-block"),
                o.prepend('<input type="button" class="js-cancel btn btn-cancel font-size-14" value="关闭">'),
                n.show()
            } else e.followStore()
        },
        followStore: function () {
            var e = this;
            $._ajax({
                type: "POST",
                url: window._global.url.pf + "/wholesale/follow/storeAdd.json",
                data: {
                    kdt_id: window._global.kdt_id
                },
                xhrFields: {
                    withCredentials: !0
                },
                success: function (t) {
                    0 === t.code ? motify.log("关注成功", 200,
                    function () {
                        window.location.href = e.redirectUrl
                    }) : motify.log(t.msg)
                },
                error: function (e, t, i) {
                    motify.log("关注失败，请稍后再试")
                }
            })
        }
    });
    new s
}),
define("wap/trade/cashier/pf/pf_follow",
function () { }),
require(["wap/components/pay/pay", "wap/trade/cashier/decrease/main", "wap/trade/cashier/pf/pf_follow"],
function (e, t) {
    var i = window._global,
    n = i.payWays,
    o = i.url.trade,
    s = $("#js-cashier-action"),
    r = $("#cashier-price"),
    a = $("#js-payer-name"),
    c = new t({
        el: ".js-decrease-container",
        price: $.trim($(".js-avatar-price").data("price")),
        trigger: r
    });
    c.render(),
    new e({
        payWays: n,
        el: s,
        payUrl: o + "/trade/cashier/pay.json",
        wxReturnUrl: o + "/pay/wxpay/return.json",
        wxPayResultUrl: o + "/trade/order/result?order_no=" + i.order_no + "&kdt_id=" + i.kdt_id + "&order_paid=1#wechat_webview_type=1&refresh",
        order_no: i.order_no,
        kdt_id: i.kdt_id,
        pagePayWaySize: 2,
        otherPayText: "其他支付方式",
        getPayDataExtr: function () {
            var e, t, n = {};
            if (n.qr_id = i.qr_id, r.length > 0) {
                if (e = parseFloat(r.html()), _.isNaN(e)) return motify.log("输入金额错误"),
                !1;
                if (.01 > e) return motify.log("至少支付0.01元"),
                !1;
                n.qr_price = e.toFixed(2)
            } else n.qr_price = 0;
            return a.length > 0 && (t = $.trim(a.val()), "匿名" == t && (t = ""), n.payer_name = t),
            c && c.getDecreaseData() && (n.active_id = c.getDecreaseData()),
            n
        }
    }).render()
}),
define("main",
function () { });