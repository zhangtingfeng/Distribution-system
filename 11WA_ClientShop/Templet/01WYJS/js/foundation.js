(function(f, l, u, r) {
    function i(b) {
        if (typeof b == "string" || b instanceof String) {
            b = b.replace(/^['\\/"]+|(;\s?})+|['\\/"] + $ / g,
            "")
        }
        return b
    }
    var o = function(b) {
        var c = b.length,
        d = f("head");
        while (c--) {
            d.has("." + b[c]).length === 0 && d.append('<meta class="' + b[c] + '" />')
        }
    }; o(["foundation-mq-small", "foundation-mq-medium", "foundation-mq-large", "foundation-mq-xlarge", "foundation-mq-xxlarge", "foundation-data-attribute-namespace"]), f(function() {
        typeof FastClick != "undefined" && typeof u.body != "undefined" && FastClick.attach(u.body)
    });
    var a = function(c, d) {
        if (typeof c == "string") {
            if (d) {
                var b;
                if (d.jquery) {
                    b = d[0];
                    if (!b) {
                        return d
                    }
                } else {
                    b = d
                }
                return f(b.querySelectorAll(c))
            }
            return f(u.querySelectorAll(c))
        }
        return f(c, d)
    },
    n = function(b) {
        var c = [];
        return b || c.push("data"),
        this.namespace.length > 0 && c.push(this.namespace),
        c.push(this.name),
        c.join("-")
    },
    s = function(d) {
        var g = d.split("-"),
        c = g.length,
        b = [];
        while (c--) {
            c !== 0 ? b.push(g[c]) : this.namespace.length > 0 ? b.push(this.namespace, g[c]) : b.push(g[c])
        }
        return b.reverse().join("-")
    },
    e = function(g, c) {
        var b = this,
        d = !a(this).data(this.attr_name(!0));
        if (typeof g == "string") {
            return this[g].call(this, c)
        }
        a(this.scope).is("[" + this.attr_name() + "]") ? (a(this.scope).data(this.attr_name(!0) + "-init", f.extend({},
        this.settings, c || g, this.data_options(a(this.scope)))), d && this.events(this.scope)) : a("[" + this.attr_name() + "]", this.scope).each(function() {
            var h = !a(this).data(b.attr_name(!0) + "-init");
            a(this).data(b.attr_name(!0) + "-init", f.extend({},
            b.settings, c || g, b.data_options(a(this)))),
            h && b.events(this)
        })
    },
    t = function(d, g) {
        function c() {
            g(d[0])
        }
        function b() {
            this.one("load", c);
            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                var j = this.attr("src"),
                h = j.match(/\?/) ? "&": "?";
                h += "random=" + (new Date).getTime(),
                this.attr("src", j + h)
            }
        }
        if (!d.attr("src")) {
            c();
            return
        }
        d[0].complete || d[0].readyState === 4 ? c() : b.call(d)
    }; l.matchMedia = l.matchMedia ||
    function(h) {
        var d, j = h.documentElement,
        c = j.firstElementChild || j.firstChild,
        g = h.createElement("body"),
        b = h.createElement("div");
        return b.id = "mq-test-1",
        b.style.cssText = "position:absolute;top:-100em",
        g.style.background = "none",
        g.appendChild(b),
        function(k) {
            return b.innerHTML = '&shy;<style media="' + k + '"> #mq-test-1 { width: 42px; }</style>',
            j.insertBefore(g, c),
            d = b.offsetWidth === 42,
            j.removeChild(g),
            {
                matches: d,
                media: k
            }
        }
    } (u),
    function(g) {
        function b() {
            j && (d(b), h && jQuery.fx.tick())
        }
        var j, c = 0,
        m = ["webkit", "moz"],
        d = l.requestAnimationFrame,
        k = l.cancelAnimationFrame,
        h = "undefined" != typeof jQuery.fx;
        for (; c < m.length && !d; c++) {
            d = l[m[c] + "RequestAnimationFrame"],
            k = k || l[m[c] + "CancelAnimationFrame"] || l[m[c] + "CancelRequestAnimationFrame"]
        }
        d ? (l.requestAnimationFrame = d, l.cancelAnimationFrame = k, h && (jQuery.fx.timer = function(p) {
            p() && jQuery.timers.push(p) && !j && (j = !0, b())
        },
        jQuery.fx.stop = function() {
            j = !1
        })) : (l.requestAnimationFrame = function(w) {
            var v = (new Date).getTime(),
            q = Math.max(0, 16 - (v - c)),
            p = l.setTimeout(function() {
                w(v + q)
            },
            q);
            return c = v + q,
            p
        },
        l.cancelAnimationFrame = function(p) {
            clearTimeout(p)
        })
    } (jQuery), l.Foundation = {
        name: "Foundation",
        version: "5.2.2",
        media_queries: {
            small: a(".foundation-mq-small").css("font-family").replace(/^[\/\\'"]+|(;\s?})+|[\/\\'"]+$/g, ""),
            medium: a(".foundation-mq-medium").css("font-family").replace(/^[\/\\'"]+|(;\s?})+|[\/\\'"]+$/g, ""),
            large: a(".foundation-mq-large").css("font-family").replace(/^[\/\\'"]+|(;\s?})+|[\/\\'"]+$/g, ""),
            xlarge: a(".foundation-mq-xlarge").css("font-family").replace(/^[\/\\'"]+|(;\s?})+|[\/\\'"]+$/g, ""),
            xxlarge: a(".foundation-mq-xxlarge").css("font-family").replace(/^[\/\\'"]+|(;\s?})+|[\/\\'"]+$/g, "")
        },
        stylesheet: f("<style></style>").appendTo("head")[0].sheet,
        global: {
            namespace: r
        },
        init: function(g, m, k, d, j) {
            var b = [g, k, d, j],
            h = [];
            this.rtl = /rtl/i.test(a("html").attr("dir")),
            this.scope = g || this.scope,
            this.set_namespace();
            if (m && typeof m == "string" && !/reflow/i.test(m)) {
                this.libs.hasOwnProperty(m) && h.push(this.init_lib(m, b))
            } else {
                for (var c in this.libs) {
                    h.push(this.init_lib(c, m))
                }
            }
            return g
        },
        init_lib: function(c, b) {
            return this.libs.hasOwnProperty(c) ? (this.patch(this.libs[c]), b && b.hasOwnProperty(c) ? (typeof this.libs[c].settings != "undefined" ? f.extend(!0, this.libs[c].settings, b[c]) : typeof this.libs[c].defaults != "undefined" && f.extend(!0, this.libs[c].defaults, b[c]), this.libs[c].init.apply(this.libs[c], [this.scope, b[c]])) : (b = b instanceof Array ? b: new Array(b), this.libs[c].init.apply(this.libs[c], b))) : function() {}
        },
        patch: function(b) {
            b.scope = this.scope,
            b.namespace = this.global.namespace,
            b.rtl = this.rtl,
            b.data_options = this.utils.data_options,
            b.attr_name = n,
            b.add_namespace = s,
            b.bindings = e,
            b.S = this.utils.S
        },
        inherit: function(d, g) {
            var c = g.split(" "),
            b = c.length;
            while (b--) {
                this.utils.hasOwnProperty(c[b]) && (d[c[b]] = this.utils[c[b]])
            }
        },
        set_namespace: function() {
            var b = this.global.namespace === r ? f(".foundation-data-attribute-namespace").css("font-family") : this.global.namespace;
            this.global.namespace = b === r || /false/i.test(b) ? "": b
        },
        libs: {},
        utils: {
            S: a,
            throttle: function(d, c) {
                var b = null;
                return function() {
                    var h = this,
                    g = arguments;
                    b == null && (b = setTimeout(function() {
                        d.apply(h, g),
                        b = null
                    },
                    c))
                }
            },
            debounce: function(g, h, b) {
                var c, d;
                return function() {
                    var p = this,
                    m = arguments,
                    j = function() {
                        c = null,
                        b || (d = g.apply(p, m))
                    },
                    k = b && !c;
                    return clearTimeout(c),
                    c = setTimeout(j, h),
                    k && (d = g.apply(p, m)),
                    d
                }
            },
            data_options: function(m) {
                function d(q) {
                    return ! isNaN(q - 0) && q !== null && q !== "" && q !== !1 && q !== !0
                }
                function p(q) {
                    return typeof q == "string" ? f.trim(q) : q
                }
                var k = {},
                g, b, j, c = function(q) {
                    var v = Foundation.global.namespace;
                    return v.length > 0 ? q.data(v + "-options") : q.data("options")
                },
                h = c(m);
                if (typeof h == "object") {
                    return h
                }
                j = (h || ":").split(";"),
                g = j.length;
                while (g--) {
                    b = j[g].split(":"),
                    /true/i.test(b[1]) && (b[1] = !0),
                    /false/i.test(b[1]) && (b[1] = !1),
                    d(b[1]) && (b[1].indexOf(".") === -1 ? b[1] = parseInt(b[1], 10) : b[1] = parseFloat(b[1])),
                    b.length === 2 && b[0].length > 0 && (k[p(b[0])] = p(b[1]))
                }
                return k
            },
            register_media: function(c, b) {
                Foundation.media_queries[c] === r && (f("head").append('<meta class="' + b + '">'), Foundation.media_queries[c] = i(f("." + b).css("font-family")))
            },
            add_custom_rule: function(d, c) {
                if (c === r && Foundation.stylesheet) {
                    Foundation.stylesheet.insertRule(d, Foundation.stylesheet.cssRules.length)
                } else {
                    var b = Foundation.media_queries[c];
                    b !== r && Foundation.stylesheet.insertRule("@media " + Foundation.media_queries[c] + "{ " + d + " }")
                }
            },
            image_loaded: function(d, g) {
                var c = this,
                b = d.length;
                b === 0 && g(d),
                d.each(function() {
                    t(c.S(this),
                    function() {
                        b -= 1,
                        b === 0 && g(d)
                    })
                })
            },
            random_str: function() {
                return this.fidx || (this.fidx = 0),
                this.prefix = this.prefix || [this.name || "F", ( + (new Date)).toString(36)].join("-"),
                this.prefix + (this.fidx++).toString(36)
            }
        }
    },
    f.fn.foundation = function() {
        var b = Array.prototype.slice.call(arguments, 0);
        return this.each(function() {
            return Foundation.init.apply(Foundation, [this].concat(b)),
            this
        })
    }
})(jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.interchange = {
        name: "interchange",
        version: "5.2.2",
        cache: {},
        images_loaded: !1,
        nodes_loaded: !1,
        settings: {
            load_attr: "interchange",
            named_queries: {
                "default": "only screen",
                small: Foundation.media_queries.small,
                medium: Foundation.media_queries.medium,
                large: Foundation.media_queries.large,
                xlarge: Foundation.media_queries.xlarge,
                xxlarge: Foundation.media_queries.xxlarge,
                landscape: "only screen and (orientation: landscape)",
                portrait: "only screen and (orientation: portrait)",
                retina: "only screen and (-webkit-min-device-pixel-ratio: 2),only screen and (min--moz-device-pixel-ratio: 2),only screen and (-o-min-device-pixel-ratio: 2/1),only screen and (min-device-pixel-ratio: 2),only screen and (min-resolution: 192dpi),only screen and (min-resolution: 2dppx)"
            },
            directives: {
                replace: function(b, a, i) {
                    if (/IMG/.test(b[0].nodeName)) {
                        var d = b[0].src;
                        if ((new RegExp(a, "i")).test(d)) {
                            return
                        }
                        return b[0].src = a,
                        i(b[0].src)
                    }
                    var c = b.data(this.data_attr + "-last-path");
                    if (c == a) {
                        return
                    }
                    return /\.(gif|jpg|jpeg|tiff|png)([?#].*)?/i.test(a) ? (g(b).css("background-image", "url(" + a + ")"), b.data("interchange-last-path", a), i(a)) : g.get(a,
                    function(j) {
                        b.html(j),
                        b.data(this.data_attr + "-last-path", a),
                        i()
                    })
                }
            }
        },
        init: function(c, a, b) {
            Foundation.inherit(this, "throttle random_str"),
            this.data_attr = this.set_data_attr(),
            g.extend(!0, this.settings, a, b),
            this.bindings(a, b),
            this.load("images"),
            this.load("nodes")
        },
        get_media_hash: function() {
            var b = "";
            for (var a in this.settings.named_queries) {
                b += matchMedia(this.settings.named_queries[a]).matches.toString()
            }
            return b
        },
        events: function() {
            var a = this,
            b;
            return g(e).off(".interchange").on("resize.fndtn.interchange", a.throttle(function() {
                var c = a.get_media_hash();
                c !== b && a.resize(),
                b = c
            },
            50)),
            this
        },
        resize: function() {
            var c = this.cache;
            if (!this.images_loaded || !this.nodes_loaded) {
                setTimeout(g.proxy(this.resize, this), 50);
                return
            }
            for (var a in c) {
                if (c.hasOwnProperty(a)) {
                    var b = this.results(a, c[a]);
                    b && this.settings.directives[b.scenario[1]].call(this, b.el, b.scenario[0],
                    function() {
                        if (arguments[0] instanceof Array) {
                            var d = arguments[0]
                        } else {
                            var d = Array.prototype.slice.call(arguments, 0)
                        }
                        b.el.trigger(b.scenario[1], d)
                    })
                }
            }
        },
        results: function(c, d) {
            var l = d.length;
            if (l > 0) {
                var a = this.S("[" + this.add_namespace("data-uuid") + '="' + c + '"]');
                while (l--) {
                    var i, b = d[l][2];
                    this.settings.named_queries.hasOwnProperty(b) ? i = matchMedia(this.settings.named_queries[b]) : i = matchMedia(b);
                    if (i.matches) {
                        return {
                            el: a,
                            scenario: d[l]
                        }
                    }
                }
            }
            return ! 1
        },
        load: function(b, a) {
            return (typeof this["cached_" + b] == "undefined" || a) && this["update_" + b](),
            this["cached_" + b]
        },
        update_images: function() {
            var c = this.S("img[" + this.data_attr + "]"),
            d = c.length,
            l = d,
            a = 0,
            i = this.data_attr;
            this.cache = {},
            this.cached_images = [],
            this.images_loaded = d === 0;
            while (l--) {
                a++;
                if (c[l]) {
                    var b = c[l].getAttribute(i) || "";
                    b.length > 0 && this.cached_images.push(c[l])
                }
                a === d && (this.images_loaded = !0, this.enhance("images"))
            }
            return this
        },
        update_nodes: function() {
            var c = this.S("[" + this.data_attr + "]").not("img"),
            d = c.length,
            l = d,
            a = 0,
            i = this.data_attr;
            this.cached_nodes = [],
            this.nodes_loaded = d === 0;
            while (l--) {
                a++;
                var b = c[l].getAttribute(i) || "";
                b.length > 0 && this.cached_nodes.push(c[l]),
                a === d && (this.nodes_loaded = !0, this.enhance("nodes"))
            }
            return this
        },
        enhance: function(a) {
            var b = this["cached_" + a].length;
            while (b--) {
                this.object(g(this["cached_" + a][b]))
            }
            return g(e).trigger("resize")
        },
        parse_params: function(c, a, b) {
            return [this.trim(c), this.convert_directive(a), this.trim(b)]
        },
        convert_directive: function(b) {
            var a = this.trim(b);
            return a.length > 0 ? a: "replace"
        },
        object: function(i) {
            var m = this.parse_data_attr(i),
            a = [],
            c = m.length;
            if (c > 0) {
                while (c--) {
                    var n = m[c].split(/\((.*?)(\))$/);
                    if (n.length > 1) {
                        var d = n[0].split(","),
                        b = this.parse_params(d[0], d[1], n[1]);
                        a.push(b)
                    }
                }
            }
            return this.store(i, a)
        },
        store: function(a, c) {
            var d = this.random_str(),
            b = a.data(this.add_namespace("uuid", !0));
            return this.cache[b] ? this.cache[b] : (a.attr(this.add_namespace("data-uuid"), d), this.cache[d] = c)
        },
        trim: function(a) {
            return typeof a == "string" ? g.trim(a) : a
        },
        set_data_attr: function(a) {
            return a ? this.namespace.length > 0 ? this.namespace + "-" + this.settings.load_attr: this.settings.load_attr: this.namespace.length > 0 ? "data-" + this.namespace + "-" + this.settings.load_attr: "data-" + this.settings.load_attr
        },
        parse_data_attr: function(a) {
            var c = a.attr(this.attr_name()).split(/\[(.*?)\]/),
            d = c.length,
            b = [];
            while (d--) {
                c[d].replace(/[\W\d]+/, "").length > 4 && b.push(c[d])
            }
            return b
        },
        reflow: function() {
            this.load("images", !0),
            this.load("nodes", !0)
        }
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.equalizer = {
        name: "equalizer",
        version: "5.2.2",
        settings: {
            use_tallest: !0,
            before_height_change: g.noop,
            after_height_change: g.noop
        },
        init: function(c, a, b) {
            Foundation.inherit(this, "image_loaded"),
            this.bindings(a, b),
            this.reflow()
        },
        events: function() {
            this.S(e).off(".equalizer").on("resize.fndtn.equalizer",
            function(a) {
                this.reflow()
            }.bind(this))
        },
        equalize: function(d) {
            var p = !1,
            b = d.find("[" + this.attr_name() + "-watch]:visible"),
            o = b.first().offset().top,
            c = d.data(this.attr_name(!0) + "-init");
            if (b.length === 0) {
                return
            }
            c.before_height_change(),
            d.trigger("before-height-change"),
            b.height("inherit"),
            b.each(function() {
                var j = g(this);
                j.offset().top !== o && (p = !0)
            });
            if (p) {
                return
            }
            var a = b.map(function() {
                return g(this).outerHeight()
            }).get();
            if (c.use_tallest) {
                var n = Math.max.apply(null, a);
                b.css("height", n)
            } else {
                var i = Math.min.apply(null, a);
                b.css("height", i)
            }
            c.after_height_change(),
            d.trigger("after-height-change")
        },
        reflow: function() {
            var a = this;
            this.S("[" + this.attr_name() + "]", this.scope).each(function() {
                var b = g(this);
                a.image_loaded(a.S("img", this),
                function() {
                    a.equalize(b)
                })
            })
        }
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.abide = {
        name: "abide",
        version: "5.2.2",
        settings: {
            live_validate: !0,
            focus_on_invalid: !0,
            error_labels: !0,
            timeout: 1000,
            patterns: {
                alpha: /^[a-zA-Z]+$/,
                alpha_numeric: /^[a-zA-Z0-9]+$/,
                integer: /^[-+]?\d+$/,
                number: /^[-+]?\d*(?:\.\d+)?$/,
                card: /^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/,
                cvv: /^([0-9]){3,4}$/,
                email: /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
                url: /^(https?|ftp|file|ssh):\/\/(((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/,
                domain: /^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}$/,
                datetime: /^([0-2][0-9]{3})\-([0-1][0-9])\-([0-3][0-9])T([0-5][0-9])\:([0-5][0-9])\:([0-5][0-9])(Z|([\-\+]([0-1][0-9])\:00))$/,
                date: /(?:19|20)[0-9]{2}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-9])|(?:(?!02)(?:0[1-9]|1[0-2])-(?:30))|(?:(?:0[13578]|1[02])-31))$/,
                time: /^(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){2}$/,
                dateISO: /^\d{4}[\/\-]\d{1,2}[\/\-]\d{1,2}$/,
                month_day_year: /^(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d$/,
                color: /^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$/
            },
            validators: {
                equalTo: function(c, d, a) {
                    var i = h.getElementById(c.getAttribute(this.add_namespace("data-equalto"))).value,
                    b = c.value,
                    l = i === b;
                    return l
                }
            }
        },
        timer: null,
        init: function(c, a, b) {
            this.bindings(a, b)
        },
        events: function(a) {
            var d = this,
            c = d.S(a).attr("novalidate", "novalidate"),
            b = c.data(this.attr_name(!0) + "-init") || {};
            this.invalid_attr = this.add_namespace("data-invalid"),
            c.off(".abide").on("submit.fndtn.abide validate.fndtn.abide",
            function(k) {
                var l = /ajax/i.test(d.S(this).attr(d.attr_name()));
                return d.validate(d.S(this).find("input, textarea, select").get(), k, l)
            }).on("reset",
            function() {
                return d.reset(g(this))
            }).find("input, textarea, select").off(".abide").on("blur.fndtn.abide change.fndtn.abide",
            function(j) {
                d.validate([this], j)
            }).on("keydown.fndtn.abide",
            function(j) {
                b.live_validate === !0 && (clearTimeout(d.timer), d.timer = setTimeout(function() {
                    d.validate([this], j)
                }.bind(this), b.timeout))
            })
        },
        reset: function(a) {
            a.removeAttr(this.invalid_attr),
            g(this.invalid_attr, a).removeAttr(this.invalid_attr),
            g(".error", a).not("small").removeClass("error")
        },
        validate: function(a, i, c) {
            var n = this.parse_patterns(a),
            d = n.length,
            o = this.S(a[0]).closest("[data-" + this.attr_name(!0) + "]"),
            q = o.data(this.attr_name(!0) + "-init") || {},
            b = /submit/.test(i.type);
            o.trigger("validated");
            for (var r = 0; r < d; r++) {
                if (!n[r] && (b || c)) {
                    return q.focus_on_invalid && a[r].focus(),
                    o.trigger("invalid"),
                    this.S(a[r]).closest("[data-" + this.attr_name(!0) + "]").attr(this.invalid_attr, ""),
                    !1
                }
            }
            return (b || c) && o.trigger("valid"),
            o.removeAttr(this.invalid_attr),
            c ? !1 : !0
        },
        parse_patterns: function(c) {
            var a = c.length,
            b = [];
            while (a--) {
                b.push(this.pattern(c[a]))
            }
            return this.check_validation_and_apply_styles(b)
        },
        pattern: function(a) {
            var c = a.getAttribute("type"),
            d = typeof a.getAttribute("required") == "string",
            b = a.getAttribute("pattern") || "";
            return this.settings.patterns.hasOwnProperty(b) && b.length > 0 ? [a, this.settings.patterns[b], d] : b.length > 0 ? [a, new RegExp("^" + b + "$"), d] : this.settings.patterns.hasOwnProperty(c) ? [a, this.settings.patterns[c], d] : (b = /.*/, [a, b, d])
        },
        check_validation_and_apply_styles: function(c) {
            var m = c.length,
            t = [],
            u = this.S(c[0][0]).closest("[data-" + this.attr_name(!0) + "]"),
            b = u.data(this.attr_name(!0) + "-init") || {};
            while (m--) {
                var n = c[m][0],
                d = c[m][2],
                o = n.value,
                s = this.S(n).parent(),
                v = n.getAttribute(this.add_namespace("data-abide-validator")),
                p = n.type === "radio",
                l = n.type === "checkbox",
                r = this.S('label[for="' + n.getAttribute("id") + '"]'),
                a = d ? n.value.length > 0 : !0,
                F,
                i;
                n.getAttribute(this.add_namespace("data-equalto")) && (v = "equalTo"),
                s.is("label") ? F = s.parent() : F = s,
                p && d ? t.push(this.valid_radio(n, d)) : l && d ? t.push(this.valid_checkbox(n, d)) : v ? (i = this.settings.validators[v].apply(this, [n, d, F]), t.push(i), i ? (this.S(n).removeAttr(this.invalid_attr), F.removeClass("error")) : (this.S(n).attr(this.invalid_attr, ""), F.addClass("error"))) : c[m][1].test(o) && a || !d && n.value.length < 1 || g(n).attr("disabled") ? (this.S(n).removeAttr(this.invalid_attr), F.removeClass("error"), r.length > 0 && b.error_labels && r.removeClass("error"), t.push(!0), g(n).triggerHandler("valid")) : (this.S(n).attr(this.invalid_attr, ""), F.addClass("error"), r.length > 0 && b.error_labels && r.addClass("error"), t.push(!1), g(n).triggerHandler("invalid"))
            }
            return t
        },
        valid_checkbox: function(c, a) {
            var c = this.S(c),
            b = c.is(":checked") || !a;
            return b ? c.removeAttr(this.invalid_attr).parent().removeClass("error") : c.attr(this.invalid_attr, "").parent().addClass("error"),
            b
        },
        valid_radio: function(i, m) {
            var a = i.getAttribute("name"),
            c = this.S(i).closest("[data-" + this.attr_name(!0) + "]").find("[name=" + a + "]"),
            n = c.length,
            d = !1;
            for (var b = 0; b < n; b++) {
                c[b].checked && (d = !0)
            }
            for (var b = 0; b < n; b++) {
                d ? this.S(c[b]).removeAttr(this.invalid_attr).parent().removeClass("error") : this.S(c[b]).attr(this.invalid_attr, "").parent().addClass("error")
            }
            return d
        },
        valid_equal: function(c, d, a) {
            var i = h.getElementById(c.getAttribute(this.add_namespace("data-equalto"))).value,
            b = c.value,
            l = i === b;
            return l ? (this.S(c).removeAttr(this.invalid_attr), a.removeClass("error")) : (this.S(c).attr(this.invalid_attr, ""), a.addClass("error")),
            l
        },
        valid_oneof: function(i, m, a, c) {
            var i = this.S(i),
            n = this.S("[" + this.add_namespace("data-oneof") + "]"),
            d = n.filter(":checked").length > 0;
            d ? i.removeAttr(this.invalid_attr).parent().removeClass("error") : i.attr(this.invalid_attr, "").parent().addClass("error");
            if (!c) {
                var b = this;
                n.each(function() {
                    b.valid_oneof.call(b, this, null, null, !0)
                })
            }
            return d
        }
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.dropdown = {
        name: "dropdown",
        version: "5.2.2",
        settings: {
            active_class: "open",
            align: "bottom",
            is_hover: !1,
            opened: function() {},
            closed: function() {}
        },
        init: function(c, a, b) {
            Foundation.inherit(this, "throttle"),
            this.bindings(a, b)
        },
        events: function(a) {
            var b = this,
            c = b.S;
            c(this.scope).off(".dropdown").on("click.fndtn.dropdown", "[" + this.attr_name() + "]",
            function(j) {
                var d = c(this).data(b.attr_name(!0) + "-init") || b.settings;
                if (!d.is_hover || Modernizr.touch) {
                    j.preventDefault(),
                    b.toggle(g(this))
                }
            }).on("mouseenter.fndtn.dropdown", "[" + this.attr_name() + "], [" + this.attr_name() + "-content]",
            function(p) {
                var d = c(this);
                clearTimeout(b.timeout);
                if (d.data(b.data_attr())) {
                    var o = c("#" + d.data(b.data_attr())),
                    m = d
                } else {
                    var o = d;
                    m = c("[" + b.attr_name() + "='" + o.attr("id") + "']")
                }
                var n = m.data(b.attr_name(!0) + "-init") || b.settings;
                c(p.target).data(b.data_attr()) && n.is_hover && b.closeall.call(b),
                n.is_hover && b.open.apply(b, [o, m])
            }).on("mouseleave.fndtn.dropdown", "[" + this.attr_name() + "], [" + this.attr_name() + "-content]",
            function(d) {
                var j = c(this);
                b.timeout = setTimeout(function() {
                    if (j.data(b.data_attr())) {
                        var i = j.data(b.data_attr(!0) + "-init") || b.settings;
                        i.is_hover && b.close.call(b, c("#" + j.data(b.data_attr())))
                    } else {
                        var l = c("[" + b.attr_name() + '="' + c(this).attr("id") + '"]'),
                        i = l.data(b.attr_name(!0) + "-init") || b.settings;
                        i.is_hover && b.close.call(b, j)
                    }
                }.bind(this), 150)
            }).on("click.fndtn.dropdown",
            function(j) {
                var d = c(j.target).closest("[" + b.attr_name() + "-content]");
                if (c(j.target).data(b.data_attr()) || c(j.target).parent().data(b.data_attr())) {
                    return
                }
                if (!c(j.target).data("revealId") && d.length > 0 && (c(j.target).is("[" + b.attr_name() + "-content]") || g.contains(d.first()[0], j.target))) {
                    j.stopPropagation();
                    return
                }
                b.close.call(b, c("[" + b.attr_name() + "-content]"))
            }).on("opened.fndtn.dropdown", "[" + b.attr_name() + "-content]",
            function() {
                b.settings.opened.call(this)
            }).on("closed.fndtn.dropdown", "[" + b.attr_name() + "-content]",
            function() {
                b.settings.closed.call(this)
            }),
            c(e).off(".dropdown").on("resize.fndtn.dropdown", b.throttle(function() {
                b.resize.call(b)
            },
            50)),
            this.resize()
        },
        close: function(b) {
            var a = this;
            b.each(function() {
                a.S(this).hasClass(a.settings.active_class) && (a.S(this).css(Foundation.rtl ? "right": "left", "-99999px").removeClass(a.settings.active_class).prev("[" + a.attr_name() + "]").removeClass(a.settings.active_class), a.S(this).trigger("closed", [b]))
            })
        },
        closeall: function() {
            var a = this;
            g.each(a.S("[" + this.attr_name() + "-content]"),
            function() {
                a.close.call(a, a.S(this))
            })
        },
        open: function(b, a) {
            this.css(b.addClass(this.settings.active_class), a),
            b.prev("[" + this.attr_name() + "]").addClass(this.settings.active_class),
            b.trigger("opened", [b, a])
        },
        data_attr: function() {
            return this.namespace.length > 0 ? this.namespace + "-" + this.name: this.name
        },
        toggle: function(b) {
            var a = this.S("#" + b.data(this.data_attr()));
            if (a.length === 0) {
                return
            }
            this.close.call(this, this.S("[" + this.attr_name() + "-content]").not(a)),
            a.hasClass(this.settings.active_class) ? this.close.call(this, a) : (this.close.call(this, this.S("[" + this.attr_name() + "-content]")), this.open.call(this, a, b))
        },
        resize: function() {
            var b = this.S("[" + this.attr_name() + "-content].open"),
            a = this.S("[" + this.attr_name() + "='" + b.attr("id") + "']");
            b.length && a.length && this.css(b, a)
        },
        css: function(a, c) {
            this.clear_idx();
            if (this.small()) {
                var d = this.dirs.bottom.call(a, c);
                a.attr("style", "").removeClass("drop-left drop-right drop-top").css({
                    position: "absolute",
                    width: "95%",
                    "max-width": "none",
                    top: d.top
                }),
                a.css(Foundation.rtl ? "right": "left", "2.5%")
            } else {
                var b = c.data(this.attr_name(!0) + "-init") || this.settings;
                this.style(a, c, b)
            }
            return a
        },
        style: function(a, d, c) {
            var b = g.extend({
                position: "absolute"
            },
            this.dirs[c.align].call(a, d, c));
            a.attr("style", "").css(b)
        },
        dirs: {
            _base: function(a) {
                var c = this.offsetParent(),
                d = c.offset(),
                b = a.offset();
                return b.top -= d.top,
                b.left -= d.left,
                b
            },
            top: function(c, a) {
                var i = Foundation.libs.dropdown,
                d = i.dirs._base.call(this, c),
                b = c.outerWidth() / 2 - 8;
                return this.addClass("drop-top"),
                (c.outerWidth() < this.outerWidth() || i.small()) && i.adjust_pip(b, d),
                Foundation.rtl ? {
                    left: d.left - this.outerWidth() + c.outerWidth(),
                    top: d.top - this.outerHeight()
                }: {
                    left: d.left,
                    top: d.top - this.outerHeight()
                }
            },
            bottom: function(c, a) {
                var i = Foundation.libs.dropdown,
                d = i.dirs._base.call(this, c),
                b = c.outerWidth() / 2 - 8;
                return (c.outerWidth() < this.outerWidth() || i.small()) && i.adjust_pip(b, d),
                i.rtl ? {
                    left: d.left - this.outerWidth() + c.outerWidth(),
                    top: d.top + c.outerHeight()
                }: {
                    left: d.left,
                    top: d.top + c.outerHeight()
                }
            },
            left: function(c, a) {
                var b = Foundation.libs.dropdown.dirs._base.call(this, c);
                return this.addClass("drop-left"),
                {
                    left: b.left - this.outerWidth(),
                    top: b.top
                }
            },
            right: function(c, a) {
                var b = Foundation.libs.dropdown.dirs._base.call(this, c);
                return this.addClass("drop-right"),
                {
                    left: b.left + c.outerWidth(),
                    top: b.top
                }
            }
        },
        adjust_pip: function(i, m) {
            var a = Foundation.stylesheet;
            this.small() && (i += m.left - 8),
            this.rule_idx = a.cssRules.length;
            var c = ".f-dropdown.open:before",
            n = ".f-dropdown.open:after",
            d = "left: " + i + "px;",
            b = "left: " + (i - 1) + "px;";
            a.insertRule ? (a.insertRule([c, "{", d, "}"].join(" "), this.rule_idx), a.insertRule([n, "{", b, "}"].join(" "), this.rule_idx + 1)) : (a.addRule(c, d, this.rule_idx), a.addRule(n, b, this.rule_idx + 1))
        },
        clear_idx: function() {
            var a = Foundation.stylesheet;
            this.rule_idx && (a.deleteRule(this.rule_idx), a.deleteRule(this.rule_idx), delete this.rule_idx)
        },
        small: function() {
            return matchMedia(Foundation.media_queries.small).matches && !matchMedia(Foundation.media_queries.medium).matches
        },
        off: function() {
            this.S(this.scope).off(".fndtn.dropdown"),
            this.S("html, body").off(".fndtn.dropdown"),
            this.S(e).off(".fndtn.dropdown"),
            this.S("[data-dropdown-content]").off(".fndtn.dropdown")
        },
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.alert = {
        name: "alert",
        version: "5.2.2",
        settings: {
            callback: function() {}
        },
        init: function(c, a, b) {
            this.bindings(a, b)
        },
        events: function() {
            var a = this,
            b = this.S;
            g(this.scope).off(".alert").on("click.fndtn.alert", "[" + this.attr_name() + "] a.close",
            function(d) {
                var i = b(this).closest("[" + a.attr_name() + "]"),
                c = i.data(a.attr_name(!0) + "-init") || a.settings;
                d.preventDefault(),
                "transitionend" in e || "webkitTransitionEnd" in e || "oTransitionEnd" in e ? (i.addClass("alert-close"), i.on("transitionend webkitTransitionEnd oTransitionEnd",
                function(j) {
                    b(this).trigger("close").remove(),
                    c.callback()
                })) : i.fadeOut(300,
                function() {
                    b(this).trigger("close").remove(),
                    c.callback()
                })
            })
        },
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs["magellan-expedition"] = {
        name: "magellan-expedition",
        version: "5.2.2",
        settings: {
            active_class: "active",
            threshold: 0,
            destination_threshold: 20,
            throttle_delay: 30
        },
        init: function(c, a, b) {
            Foundation.inherit(this, "throttle"),
            this.bindings(a, b)
        },
        events: function() {
            var a = this,
            b = a.S,
            c = a.settings;
            a.set_expedition_position(),
            b(a.scope).off(".magellan").on("click.fndtn.magellan", "[" + a.add_namespace("data-magellan-arrival") + '] a[href^="#"]',
            function(p) {
                p.preventDefault();
                var i = g(this).closest("[" + a.attr_name() + "]"),
                r = i.data("magellan-expedition-init"),
                o = this.hash.split("#").join(""),
                d = g("a[name='" + o + "']");
                d.length === 0 && (d = g("#" + o));
                var q = d.offset().top;
                q -= i.outerHeight(),
                g("html, body").stop().animate({
                    scrollTop: q
                },
                700, "swing",
                function() {
                    history.pushState ? history.pushState(null, null, "#" + o) : location.hash = "#" + o
                })
            }).on("scroll.fndtn.magellan", a.throttle(this.check_for_arrivals.bind(this), c.throttle_delay)),
            g(e).on("resize.fndtn.magellan", a.throttle(this.set_expedition_position.bind(this), c.throttle_delay))
        },
        check_for_arrivals: function() {
            var a = this;
            a.update_arrivals(),
            a.update_expedition_positions()
        },
        set_expedition_position: function() {
            var a = this;
            g("[" + this.attr_name() + "=fixed]", a.scope).each(function(b, d) {
                var l = g(this),
                i = l.attr("styles"),
                c;
                l.attr("style", ""),
                c = l.offset().top,
                l.data(a.data_attr("magellan-top-offset"), c),
                l.attr("style", i)
            })
        },
        update_expedition_positions: function() {
            var a = this,
            b = g(e).scrollTop();
            g("[" + this.attr_name() + "=fixed]", a.scope).each(function() {
                var d = g(this),
                i = d.data("magellan-top-offset");
                if (b >= i) {
                    var c = d.prev("[" + a.add_namespace("data-magellan-expedition-clone") + "]");
                    c.length === 0 && (c = d.clone(), c.removeAttr(a.attr_name()), c.attr(a.add_namespace("data-magellan-expedition-clone"), ""), d.before(c)),
                    d.css({
                        position: "fixed",
                        top: 0
                    })
                } else {
                    d.prev("[" + a.add_namespace("data-magellan-expedition-clone") + "]").remove(),
                    d.attr("style", "")
                }
            })
        },
        update_arrivals: function() {
            var a = this,
            b = g(e).scrollTop();
            g("[" + this.attr_name() + "]", a.scope).each(function() {
                var m = g(this),
                i = i = m.data(a.attr_name(!0) + "-init"),
                c = a.offsets(m, b),
                d = m.find("[" + a.add_namespace("data-magellan-arrival") + "]"),
                n = !1;
                c.each(function(l, j) {
                    if (j.viewport_offset >= j.top_offset) {
                        var k = m.find("[" + a.add_namespace("data-magellan-arrival") + "]");
                        return k.not(j.arrival).removeClass(i.active_class),
                        j.arrival.addClass(i.active_class),
                        n = !0,
                        !0
                    }
                }),
                n || d.removeClass(i.active_class)
            })
        },
        offsets: function(b, a) {
            var i = this,
            d = b.data(i.attr_name(!0) + "-init"),
            c = a;
            return b.find("[" + i.add_namespace("data-magellan-arrival") + "]").map(function(n, o) {
                var j = g(this).data(i.data_attr("magellan-arrival")),
                r = g("[" + i.add_namespace("data-magellan-destination") + "=" + j + "]");
                if (r.length > 0) {
                    var q = r.offset().top - d.destination_threshold - b.outerHeight();
                    return {
                        destination: r,
                        arrival: g(this),
                        top_offset: q,
                        viewport_offset: c
                    }
                }
            }).sort(function(l, j) {
                return l.top_offset < j.top_offset ? -1 : l.top_offset > j.top_offset ? 1 : 0
            })
        },
        data_attr: function(a) {
            return this.namespace.length > 0 ? this.namespace + "-" + a: a
        },
        off: function() {
            this.S(this.scope).off(".magellan"),
            this.S(e).off(".magellan")
        },
        reflow: function() {
            var a = this;
            g("[" + a.add_namespace("data-magellan-expedition-clone") + "]", a.scope).remove()
        }
    }
} (jQuery, this, this.document),
function(e, h, j, i) {
    function g(b) {
        var c = /fade/i.test(b),
        a = /pop/i.test(b);
        return {
            animate: c || a,
            pop: a,
            fade: c
        }
    }
    Foundation.libs.reveal = {
        name: "reveal",
        version: "5.2.2",
        locked: !1,
        settings: {
            animation: "fadeAndPop",
            animation_speed: 250,
            close_on_background_click: !0,
            close_on_esc: !0,
            dismiss_modal_class: "close-reveal-modal",
            bg_class: "reveal-modal-bg",
            open: function() {},
            opened: function() {},
            close: function() {},
            closed: function() {},
            bg: e(".reveal-modal-bg"),
            css: {
                open: {
                    opacity: 0,
                    visibility: "visible",
                    display: "block"
                },
                close: {
                    opacity: 1,
                    visibility: "hidden",
                    display: "none"
                }
            }
        },
        init: function(b, c, a) {
            e.extend(!0, this.settings, c, a),
            this.bindings(c, a)
        },
        events: function(b) {
            var c = this,
            a = c.S;
            return a(this.scope).off(".reveal").on("click.fndtn.reveal", "[" + this.add_namespace("data-reveal-id") + "]",
            function(d) {
                d.preventDefault();
                if (!c.locked) {
                    var m = a(this),
                    n = m.data(c.data_attr("reveal-ajax"));
                    c.locked = !0;
                    if (typeof n == "undefined") {
                        c.open.call(c, m)
                    } else {
                        var f = n === !0 ? m.attr("href") : n;
                        c.open.call(c, m, {
                            url: f
                        })
                    }
                }
            }),
            a(j).on("touchend.fndtn.reveal click.fndtn.reveal", this.close_targets(),
            function(f) {
                f.preventDefault();
                if (!c.locked) {
                    var d = a("[" + c.attr_name() + "].open").data(c.attr_name(!0) + "-init"),
                    l = a(f.target)[0] === a("." + d.bg_class)[0];
                    if (l) {
                        if (!d.close_on_background_click) {
                            return
                        }
                        f.stopPropagation()
                    }
                    c.locked = !0,
                    c.close.call(c, l ? a("[" + c.attr_name() + "].open") : a(this).closest("[" + c.attr_name() + "]"))
                }
            }),
            a("[" + c.attr_name() + "]", this.scope).length > 0 ? a(this.scope).on("open.fndtn.reveal", this.settings.open).on("opened.fndtn.reveal", this.settings.opened).on("opened.fndtn.reveal", this.open_video).on("close.fndtn.reveal", this.settings.close).on("closed.fndtn.reveal", this.settings.closed).on("closed.fndtn.reveal", this.close_video) : a(this.scope).on("open.fndtn.reveal", "[" + c.attr_name() + "]", this.settings.open).on("opened.fndtn.reveal", "[" + c.attr_name() + "]", this.settings.opened).on("opened.fndtn.reveal", "[" + c.attr_name() + "]", this.open_video).on("close.fndtn.reveal", "[" + c.attr_name() + "]", this.settings.close).on("closed.fndtn.reveal", "[" + c.attr_name() + "]", this.settings.closed).on("closed.fndtn.reveal", "[" + c.attr_name() + "]", this.close_video),
            !0
        },
        key_up_on: function(b) {
            var a = this;
            return a.S("body").off("keyup.fndtn.reveal").on("keyup.fndtn.reveal",
            function(c) {
                var d = a.S("[" + a.attr_name() + "].open"),
                f = d.data(a.attr_name(!0) + "-init");
                f && c.which === 27 && f.close_on_esc && !a.locked && a.close.call(a, d)
            }),
            !0
        },
        key_up_off: function(a) {
            return this.S("body").off("keyup.fndtn.reveal"),
            !0
        },
        open: function(d, n) {
            var b = this;
            if (d) {
                if (typeof d.selector != "undefined") {
                    var m = b.S("#" + d.data(b.data_attr("reveal-id")))
                } else {
                    var m = b.S(this.scope);
                    n = d
                }
            } else {
                var m = b.S(this.scope)
            }
            var c = m.data(b.attr_name(!0) + "-init");
            if (!m.hasClass("open")) {
                var a = b.S("[" + b.attr_name() + "].open");
                typeof m.data("css-top") == "undefined" && m.data("css-top", parseInt(m.css("top"), 10)).data("offset", this.cache_offset(m)),
                this.key_up_on(m),
                m.trigger("open"),
                a.length < 1 && this.toggle_bg(m),
                typeof n == "string" && (n = {
                    url: n
                });
                if (typeof n == "undefined" || !n.url) {
                    a.length > 0 && this.hide(a, c.css.close),
                    this.show(m, c.css.open)
                } else {
                    var f = typeof n.success != "undefined" ? n.success: null;
                    e.extend(n, {
                        success: function(l, q, k) {
                            e.isFunction(f) && f(l, q, k),
                            m.html(l),
                            b.S(m).foundation("section", "reflow"),
                            a.length > 0 && b.hide(a, c.css.close),
                            b.show(m, c.css.open)
                        }
                    }),
                    e.ajax(n)
                }
            }
        },
        close: function(b) {
            var b = b && b.length ? b: this.S(this.scope),
            c = this.S("[" + this.attr_name() + "].open"),
            a = b.data(this.attr_name(!0) + "-init");
            c.length > 0 && (this.locked = !0, this.key_up_off(b), b.trigger("close"), this.toggle_bg(b), this.hide(c, a.css.close, a))
        },
        close_targets: function() {
            var a = "." + this.settings.dismiss_modal_class;
            return this.settings.close_on_background_click ? a + ", ." + this.settings.bg_class: a
        },
        toggle_bg: function(b) {
            var a = b.data(this.attr_name(!0));
            this.S("." + this.settings.bg_class).length === 0 && (this.settings.bg = e("<div />", {
                "class": this.settings.bg_class
            }).appendTo("body").hide()),
            this.settings.bg.filter(":visible").length > 0 ? this.hide(this.settings.bg) : this.show(this.settings.bg)
        },
        show: function(m, c) {
            if (c) {
                var d = m.data(this.attr_name(!0) + "-init");
                if (m.parent("body").length === 0) {
                    var n = m.wrap('<div style="display: none;" />').parent(),
                    f = this.settings.rootElement || "body";
                    m.on("closed.fndtn.reveal.wrapped",
                    function() {
                        m.detach().appendTo(n),
                        m.unwrap().unbind("closed.fndtn.reveal.wrapped")
                    }),
                    m.detach().appendTo(f)
                }
                var b = g(d.animation);
                b.animate || (this.locked = !1);
                if (b.pop) {
                    c.top = e(h).scrollTop() - m.data("offset") + "px";
                    var a = {
                        top: e(h).scrollTop() + m.data("css-top") + "px",
                        opacity: 1
                    };
                    return setTimeout(function() {
                        return m.css(c).animate(a, d.animation_speed, "linear",
                        function() {
                            this.locked = !1,
                            m.trigger("opened")
                        }.bind(this)).addClass("open")
                    }.bind(this), d.animation_speed / 2)
                }
                if (b.fade) {
                    c.top = e(h).scrollTop() + m.data("css-top") + "px";
                    var a = {
                        opacity: 1
                    };
                    return setTimeout(function() {
                        return m.css(c).animate(a, d.animation_speed, "linear",
                        function() {
                            this.locked = !1,
                            m.trigger("opened")
                        }.bind(this)).addClass("open")
                    }.bind(this), d.animation_speed / 2)
                }
                return m.css(c).show().css({
                    opacity: 1
                }).addClass("open").trigger("opened")
            }
            var d = this.settings;
            return g(d.animation).fade ? m.fadeIn(d.animation_speed / 2) : (this.locked = !1, m.show())
        },
        hide: function(b, a) {
            if (a) {
                var f = b.data(this.attr_name(!0) + "-init"),
                c = g(f.animation);
                c.animate || (this.locked = !1);
                if (c.pop) {
                    var d = {
                        top: -e(h).scrollTop() - b.data("offset") + "px",
                        opacity: 0
                    };
                    return setTimeout(function() {
                        return b.animate(d, f.animation_speed, "linear",
                        function() {
                            this.locked = !1,
                            b.css(a).trigger("closed")
                        }.bind(this)).removeClass("open")
                    }.bind(this), f.animation_speed / 2)
                }
                if (c.fade) {
                    var d = {
                        opacity: 0
                    };
                    return setTimeout(function() {
                        return b.animate(d, f.animation_speed, "linear",
                        function() {
                            this.locked = !1,
                            b.css(a).trigger("closed")
                        }.bind(this)).removeClass("open")
                    }.bind(this), f.animation_speed / 2)
                }
                return b.hide().css(a).removeClass("open").trigger("closed")
            }
            var f = this.settings;
            return g(f.animation).fade ? b.fadeOut(f.animation_speed / 2) : b.hide()
        },
        close_video: function(b) {
            var c = e(".flex-video", b.target),
            a = e("iframe", c);
            a.length > 0 && (a.attr("data-src", a[0].src), a.attr("src", "about:blank"), c.hide())
        },
        open_video: function(b) {
            var c = e(".flex-video", b.target),
            a = c.find("iframe");
            if (a.length > 0) {
                var f = a.attr("data-src");
                if (typeof f == "string") {
                    a[0].src = a.attr("data-src")
                } else {
                    var d = a[0].src;
                    a[0].src = i,
                    a[0].src = d
                }
                c.show()
            }
        },
        data_attr: function(a) {
            return this.namespace.length > 0 ? this.namespace + "-" + a: a
        },
        cache_offset: function(b) {
            var a = b.show().height() + parseInt(b.css("top"), 10);
            return b.hide(),
            a
        },
        off: function() {
            e(this.scope).off(".fndtn.reveal")
        },
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.tooltip = {
        name: "tooltip",
        version: "5.2.2",
        settings: {
            additional_inheritable_classes: [],
            tooltip_class: ".tooltip",
            append_to: "body",
            touch_close_text: "Tap To Close",
            disable_for_touch: !1,
            hover_delay: 200,
            tip_template: function(b, a) {
                return '<span data-selector="' + b + '" class="' + Foundation.libs.tooltip.settings.tooltip_class.substring(1) + '">' + a + '<span class="nub"></span></span>'
            }
        },
        cache: {},
        init: function(c, a, b) {
            Foundation.inherit(this, "random_str"),
            this.bindings(a, b)
        },
        events: function(c) {
            var a = this,
            b = a.S;
            a.create(this.S(c)),
            g(this.scope).off(".tooltip").on("mouseenter.fndtn.tooltip mouseleave.fndtn.tooltip touchstart.fndtn.tooltip MSPointerDown.fndtn.tooltip", "[" + this.attr_name() + "]",
            function(n) {
                var i = b(this),
                m = g.extend({},
                a.settings, a.data_options(i)),
                d = !1;
                if (Modernizr.touch && /touchstart|MSPointerDown/i.test(n.type) && b(n.target).is("a")) {
                    return ! 1
                }
                if (/mouse/i.test(n.type) && a.ie_touch(n)) {
                    return ! 1
                }
                if (i.hasClass("open")) {
                    Modernizr.touch && /touchstart|MSPointerDown/i.test(n.type) && n.preventDefault(),
                    a.hide(i)
                } else {
                    if (m.disable_for_touch && Modernizr.touch && /touchstart|MSPointerDown/i.test(n.type)) {
                        return
                    } ! m.disable_for_touch && Modernizr.touch && /touchstart|MSPointerDown/i.test(n.type) && (n.preventDefault(), b(m.tooltip_class + ".open").hide(), d = !0),
                    /enter|over/i.test(n.type) ? this.timer = setTimeout(function() {
                        var j = a.showTip(i)
                    }.bind(this), a.settings.hover_delay) : n.type === "mouseout" || n.type === "mouseleave" ? (clearTimeout(this.timer), a.hide(i)) : a.showTip(i)
                }
            }).on("mouseleave.fndtn.tooltip touchstart.fndtn.tooltip MSPointerDown.fndtn.tooltip", "[" + this.attr_name() + "].open",
            function(d) {
                if (/mouse/i.test(d.type) && a.ie_touch(d)) {
                    return ! 1
                }
                if (g(this).data("tooltip-open-event-type") == "touch" && d.type == "mouseleave") {
                    return
                }
                g(this).data("tooltip-open-event-type") == "mouse" && /MSPointerDown|touchstart/i.test(d.type) ? a.convert_to_touch(g(this)) : a.hide(g(this))
            }).on("DOMNodeRemoved DOMAttrModified", "[" + this.attr_name() + "]:not(a)",
            function(d) {
                a.hide(b(this))
            })
        },
        ie_touch: function(a) {
            return ! 1
        },
        showTip: function(b) {
            var a = this.getTip(b);
            return this.show(b)
        },
        getTip: function(a) {
            var d = this.selector(a),
            c = g.extend({},
            this.settings, this.data_options(a)),
            b = null;
            return d && (b = this.S('span[data-selector="' + d + '"]' + c.tooltip_class)),
            typeof b == "object" ? b: !1
        },
        selector: function(c) {
            var a = c.attr("id"),
            b = c.attr(this.attr_name()) || c.attr("data-selector");
            return (a && a.length < 1 || !a) && typeof b != "string" && (b = this.random_str(6), c.attr("data-selector", b)),
            a && a.length > 0 ? a: b
        },
        create: function(d) {
            var l = this,
            c = g.extend({},
            this.settings, this.data_options(d)),
            a = this.settings.tip_template;
            typeof c.tip_template == "string" && e.hasOwnProperty(c.tip_template) && (a = e[c.tip_template]);
            var i = g(a(this.selector(d), g("<div></div>").html(d.attr("title")).html())),
            b = this.inheritable_classes(d);
            i.addClass(b).appendTo(c.append_to),
            Modernizr.touch && (i.append('<span class="tap-to-close">' + c.touch_close_text + "</span>"), i.on("touchstart.fndtn.tooltip MSPointerDown.fndtn.tooltip",
            function(j) {
                l.hide(d)
            })),
            d.removeAttr("title").attr("title", "")
        },
        reposition: function(l, c, r) {
            var b, a, s, n, o, d;
            c.css("visibility", "hidden").show(),
            b = l.data("width"),
            a = c.children(".nub"),
            s = a.outerHeight(),
            n = a.outerHeight(),
            this.small() ? c.css({
                width: "100%"
            }) : c.css({
                width: b ? b: "auto"
            }),
            d = function(k, q, p, t, m, j) {
                return k.css({
                    top: q ? q: "auto",
                    bottom: t ? t: "auto",
                    left: m ? m: "auto",
                    right: p ? p: "auto"
                }).end()
            },
            d(c, l.offset().top + l.outerHeight() + 10, "auto", "auto", l.offset().left);
            if (this.small()) {
                d(c, l.offset().top + l.outerHeight() + 10, "auto", "auto", 12.5, g(this.scope).width()),
                c.addClass("tip-override"),
                d(a, -s, "auto", "auto", l.offset().left)
            } else {
                var i = l.offset().left;
                Foundation.rtl && (a.addClass("rtl"), i = l.offset().left + l.outerWidth() - c.outerWidth()),
                d(c, l.offset().top + l.outerHeight() + 10, "auto", "auto", i),
                c.removeClass("tip-override"),
                r && r.indexOf("tip-top") > -1 ? (Foundation.rtl && a.addClass("rtl"), d(c, l.offset().top - c.outerHeight(), "auto", "auto", i).removeClass("tip-override")) : r && r.indexOf("tip-left") > -1 ? (d(c, l.offset().top + l.outerHeight() / 2 - c.outerHeight() / 2, "auto", "auto", l.offset().left - c.outerWidth() - s).removeClass("tip-override"), a.removeClass("rtl")) : r && r.indexOf("tip-right") > -1 && (d(c, l.offset().top + l.outerHeight() / 2 - c.outerHeight() / 2, "auto", "auto", l.offset().left + l.outerWidth() + s).removeClass("tip-override"), a.removeClass("rtl"))
            }
            c.css("visibility", "visible").hide()
        },
        small: function() {
            return matchMedia(Foundation.media_queries.small).matches && !matchMedia(Foundation.media_queries.medium).matches
        },
        inheritable_classes: function(b) {
            var a = g.extend({},
            this.settings, this.data_options(b)),
            i = ["tip-top", "tip-left", "tip-bottom", "tip-right", "radius", "round"].concat(a.additional_inheritable_classes),
            d = b.attr("class"),
            c = d ? g.map(d.split(" "),
            function(j, l) {
                if (g.inArray(j, i) !== -1) {
                    return j
                }
            }).join(" ") : "";
            return g.trim(c)
        },
        convert_to_touch: function(a) {
            var d = this,
            c = d.getTip(a),
            b = g.extend({},
            d.settings, d.data_options(a));
            c.find(".tap-to-close").length === 0 && (c.append('<span class="tap-to-close">' + b.touch_close_text + "</span>"), c.on("click.fndtn.tooltip.tapclose touchstart.fndtn.tooltip.tapclose MSPointerDown.fndtn.tooltip.tapclose",
            function(j) {
                d.hide(a)
            })),
            a.data("tooltip-open-event-type", "touch")
        },
        show: function(b) {
            var a = this.getTip(b);
            b.data("tooltip-open-event-type") == "touch" && this.convert_to_touch(b),
            this.reposition(b, a, b.attr("class")),
            b.addClass("open"),
            a.fadeIn(150)
        },
        hide: function(b) {
            var a = this.getTip(b);
            a.fadeOut(150,
            function() {
                a.find(".tap-to-close").remove(),
                a.off("click.fndtn.tooltip.tapclose touchstart.fndtn.tooltip.tapclose MSPointerDown.fndtn.tapclose"),
                b.removeClass("open")
            })
        },
        off: function() {
            var a = this;
            this.S(this.scope).off(".fndtn.tooltip"),
            this.S(this.settings.tooltip_class).each(function(b) {
                g("[" + a.attr_name() + "]").eq(b).attr("title", g(this).text())
            }).remove()
        },
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.tab = {
        name: "tab",
        version: "5.2.2",
        settings: {
            active_class: "active",
            callback: function() {},
            deep_linking: !1,
            scroll_to_content: !0,
            is_hover: !1
        },
        default_tab_hashes: [],
        init: function(c, a, i) {
            var d = this,
            b = this.S;
            this.bindings(a, i),
            this.handle_location_hash_change(),
            b("[" + this.attr_name() + "] > dd.active > a", this.scope).each(function() {
                d.default_tab_hashes.push(this.hash)
            })
        },
        events: function() {
            var a = this,
            b = this.S;
            b(this.scope).off(".tab").on("click.fndtn.tab", "[" + this.attr_name() + "] > dd > a",
            function(c) {
                var d = b(this).closest("[" + a.attr_name() + "]").data(a.attr_name(!0) + "-init");
                if (!d.is_hover || Modernizr.touch) {
                    c.preventDefault(),
                    c.stopPropagation(),
                    a.toggle_active_tab(b(this).parent())
                }
            }).on("mouseenter.fndtn.tab", "[" + this.attr_name() + "] > dd > a",
            function(c) {
                var d = b(this).closest("[" + a.attr_name() + "]").data(a.attr_name(!0) + "-init");
                d.is_hover && a.toggle_active_tab(b(this).parent())
            }),
            b(e).on("hashchange.fndtn.tab",
            function(c) {
                c.preventDefault(),
                a.handle_location_hash_change()
            })
        },
        handle_location_hash_change: function() {
            var b = this,
            a = this.S;
            a("[" + this.attr_name() + "]", this.scope).each(function() {
                var i = a(this).data(b.attr_name(!0) + "-init");
                if (i.deep_linking) {
                    var c = b.scope.location.hash;
                    if (c != "") {
                        var m = a(c);
                        if (m.hasClass("content") && m.parent().hasClass("tab-content")) {
                            b.toggle_active_tab(g("[" + b.attr_name() + "] > dd > a[href=" + c + "]").parent())
                        } else {
                            var d = m.closest(".content").attr("id");
                            d != f && b.toggle_active_tab(g("[" + b.attr_name() + "] > dd > a[href=#" + d + "]").parent(), c)
                        }
                    } else {
                        for (var n in b.default_tab_hashes) {
                            b.toggle_active_tab(g("[" + b.attr_name() + "] > dd > a[href=" + b.default_tab_hashes[n] + "]").parent())
                        }
                    }
                }
            })
        },
        toggle_active_tab: function(l, c) {
            var a = this.S,
            o = l.closest("[" + this.attr_name() + "]"),
            n = l.children("a").first(),
            s = "#" + n.attr("href").split("#")[1],
            t = a(s),
            d = l.siblings(),
            i = o.data(this.attr_name(!0) + "-init");
            a(this).data(this.data_attr("tab-content")) && (s = "#" + a(this).data(this.data_attr("tab-content")).split("#")[1], t = a(s));
            if (i.deep_linking) {
                var b = g("body,html").scrollTop();
                c != f ? e.location.hash = c: e.location.hash = s,
                i.scroll_to_content ? c == f || c == s ? l.parent()[0].scrollIntoView() : a(s)[0].scrollIntoView() : (c == f || c == s) && g("body,html").scrollTop(b)
            }
            l.addClass(i.active_class).triggerHandler("opened"),
            d.removeClass(i.active_class),
            t.siblings().removeClass(i.active_class).end().addClass(i.active_class),
            i.callback(l),
            t.triggerHandler("toggled", [l]),
            o.triggerHandler("toggled", [t])
        },
        data_attr: function(a) {
            return this.namespace.length > 0 ? this.namespace + "-" + a: a
        },
        off: function() {},
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.clearing = {
        name: "clearing",
        version: "5.2.2",
        settings: {
            templates: {
                viewing: '<a href="#" class="clearing-close">&times;</a><div class="visible-img" style="display: none"><div class="clearing-touch-label"></div><img src="data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D" alt="" /><p class="clearing-caption"></p><a href="#" class="clearing-main-prev"><span></span></a><a href="#" class="clearing-main-next"><span></span></a></div>'
            },
            close_selectors: ".clearing-close",
            touch_label: "",
            init: !1,
            locked: !1
        },
        init: function(a, c, d) {
            var b = this;
            Foundation.inherit(this, "throttle image_loaded"),
            this.bindings(c, d),
            b.S(this.scope).is("[" + this.attr_name() + "]") ? this.assemble(b.S("li", this.scope)) : b.S("[" + this.attr_name() + "]", this.scope).each(function() {
                b.assemble(b.S("li", this))
            })
        },
        events: function(a) {
            var b = this,
            c = b.S;
            g(".scroll-container").length > 0 && (this.scope = g(".scroll-container")),
            c(this.scope).off(".clearing").on("click.fndtn.clearing", "ul[" + this.attr_name() + "] li",
            function(p, r, d) {
                var r = r || c(this),
                d = d || r,
                o = r.next("li"),
                n = r.closest("[" + b.attr_name() + "]").data(b.attr_name(!0) + "-init"),
                q = c(p.target);
                p.preventDefault(),
                n || (b.init(), n = r.closest("[" + b.attr_name() + "]").data(b.attr_name(!0) + "-init")),
                d.hasClass("visible") && r[0] === d[0] && o.length > 0 && b.is_open(r) && (d = o, q = c("img", d)),
                b.open(q, r, d),
                b.update_paddles(d)
            }).on("click.fndtn.clearing", ".clearing-main-next",
            function(d) {
                b.nav(d, "next")
            }).on("click.fndtn.clearing", ".clearing-main-prev",
            function(d) {
                b.nav(d, "prev")
            }).on("click.fndtn.clearing", this.settings.close_selectors,
            function(d) {
                Foundation.libs.clearing.close(d, this)
            }),
            g(h).on("keydown.fndtn.clearing",
            function(d) {
                b.keydown(d)
            }),
            c(e).off(".clearing").on("resize.fndtn.clearing",
            function() {
                b.resize()
            }),
            this.swipe_events(a)
        },
        swipe_events: function(c) {
            var a = this,
            b = a.S;
            b(this.scope).on("touchstart.fndtn.clearing", ".visible-img",
            function(d) {
                d.touches || (d = d.originalEvent);
                var j = {
                    start_page_x: d.touches[0].pageX,
                    start_page_y: d.touches[0].pageY,
                    start_time: (new Date).getTime(),
                    delta_x: 0,
                    is_scrolling: f
                };
                b(this).data("swipe-transition", j),
                d.stopPropagation()
            }).on("touchmove.fndtn.clearing", ".visible-img",
            function(l) {
                l.touches || (l = l.originalEvent);
                if (l.touches.length > 1 || l.scale && l.scale !== 1) {
                    return
                }
                var d = b(this).data("swipe-transition");
                typeof d == "undefined" && (d = {}),
                d.delta_x = l.touches[0].pageX - d.start_page_x,
                typeof d.is_scrolling == "undefined" && (d.is_scrolling = !!(d.is_scrolling || Math.abs(d.delta_x) < Math.abs(l.touches[0].pageY - d.start_page_y)));
                if (!d.is_scrolling && !d.active) {
                    l.preventDefault();
                    var i = d.delta_x < 0 ? "next": "prev";
                    d.active = !0,
                    a.nav(l, i)
                }
            }).on("touchend.fndtn.clearing", ".visible-img",
            function(d) {
                b(this).data("swipe-transition", {}),
                d.stopPropagation()
            })
        },
        assemble: function(a) {
            var d = a.parent();
            if (d.parent().hasClass("carousel")) {
                return
            }
            d.after('<div id="foundationClearingHolder"></div>');
            var n = d.detach(),
            i = "";
            if (n[0] == null) {
                return
            }
            i = n[0].outerHTML;
            var o = this.S("#foundationClearingHolder"),
            c = d.data(this.attr_name(!0) + "-init"),
            n = d.detach(),
            b = {
                grid: '<div class="carousel">' + i + "</div>",
                viewing: c.templates.viewing
            },
            r = '<div class="clearing-assembled"><div>' + b.viewing + b.grid + "</div></div>",
            q = this.settings.touch_label;
            Modernizr.touch && (r = g(r).find(".clearing-touch-label").html(q).end()),
            o.after(r).remove()
        },
        open: function(t, b, a) {
            function l() {
                setTimeout(function() {
                    this.image_loaded(p,
                    function() {
                        p.outerWidth() === 1 && !z ? l.call(this) : i.call(this, p)
                    }.bind(this))
                }.bind(this), 50)
            }
            function i(j) {
                var k = g(j);
                j.css("visibility", "visible"),
                r.css("overflow", "hidden"),
                u.addClass("clearing-blackout"),
                c.addClass("clearing-container"),
                o.show(),
                this.fix_height(a).caption(s.S(".clearing-caption", o), s.S("img", a)).center_and_label(j, d).shift(b, a,
                function() {
                    a.siblings().removeClass("visible"),
                    a.addClass("visible")
                })
            }
            var s = this,
            r = g(h.body),
            u = a.closest(".clearing-assembled"),
            c = s.S("div", u).first(),
            o = s.S(".visible-img", c),
            p = s.S("img", o).not(t),
            d = s.S(".clearing-touch-label", c),
            z = !1;
            p.error(function() {
                z = !0
            }),
            this.locked() || (p.attr("src", this.load(t)).css("visibility", "hidden"), l.call(this))
        },
        close: function(a, i) {
            a.preventDefault();
            var c = function(j) {
                return /blackout/.test(j.selector) ? j: j.closest(".clearing-blackout")
            } (g(i)),
            l = g(h.body),
            d,
            b;
            return i === a.target && c && (l.css("overflow", ""), d = g("div", c).first(), b = g(".visible-img", d), this.settings.prev_index = 0, g("ul[" + this.attr_name() + "]", c).attr("style", "").closest(".clearing-blackout").removeClass("clearing-blackout"), d.removeClass("clearing-container"), b.hide()),
            !1
        },
        is_open: function(a) {
            return a.parent().prop("style").length > 0
        },
        keydown: function(b) {
            var a = g(".clearing-blackout ul[" + this.attr_name() + "]"),
            i = this.rtl ? 37 : 39,
            d = this.rtl ? 39 : 37,
            c = 27;
            b.which === i && this.go(a, "next"),
            b.which === d && this.go(a, "prev"),
            b.which === c && this.S("a.clearing-close").trigger("click")
        },
        nav: function(c, a) {
            var b = g("ul[" + this.attr_name() + "]", ".clearing-blackout");
            c.preventDefault(),
            this.go(b, a)
        },
        resize: function() {
            var b = g("img", ".clearing-blackout .visible-img"),
            a = g(".clearing-touch-label", ".clearing-blackout");
            b.length && this.center_and_label(b, a)
        },
        fix_height: function(c) {
            var a = c.parent().children(),
            b = this;
            return a.each(function() {
                var d = b.S(this),
                j = d.find("img");
                d.height() > j.outerHeight() && d.addClass("fix-height")
            }).closest("ul").width(a.length * 100 + "%"),
            this
        },
        update_paddles: function(b) {
            var a = b.closest(".carousel").siblings(".visible-img");
            b.next().length > 0 ? this.S(".clearing-main-next", a).removeClass("disabled") : this.S(".clearing-main-next", a).addClass("disabled"),
            b.prev().length > 0 ? this.S(".clearing-main-prev", a).removeClass("disabled") : this.S(".clearing-main-prev", a).addClass("disabled")
        },
        center_and_label: function(b, a) {
            return this.rtl ? (b.css({
                marginRight: -(b.outerWidth() / 2),
                marginTop: -(b.outerHeight() / 2),
                left: "auto",
                right: "50%"
            }), a.length > 0 && a.css({
                marginRight: -(a.outerWidth() / 2),
                marginTop: -(b.outerHeight() / 2) - a.outerHeight() - 10,
                left: "auto",
                right: "50%"
            })) : (b.css({
                marginLeft: -(b.outerWidth() / 2),
                marginTop: -(b.outerHeight() / 2)
            }), a.length > 0 && a.css({
                marginLeft: -(a.outerWidth() / 2),
                marginTop: -(b.outerHeight() / 2) - a.outerHeight() - 10
            })),
            this
        },
        load: function(b) {
            if (b[0].nodeName === "A") {
                var a = b.attr("href")
            } else {
                var a = b.parent().attr("href")
            }
            return this.preload(b),
            a ? a: b.attr("src")
        },
        preload: function(a) {
            this.img(a.closest("li").next()).img(a.closest("li").prev())
        },
        img: function(c) {
            if (c.length) {
                var a = new Image,
                b = this.S("a", c);
                b.length ? a.src = b.attr("href") : a.src = this.S("img", c).attr("src")
            }
            return this
        },
        caption: function(c, a) {
            var b = a.attr("data-caption");
            return b ? c.html(b).show() : c.text("").hide(),
            this
        },
        go: function(a, c) {
            var d = this.S(".visible", a),
            b = d[c]();
            b.length && this.S("img", b).trigger("click", [d, b])
        },
        shift: function(i, r, n) {
            var c = r.parent(),
            d = this.settings.prev_index || r.index(),
            s = this.direction(c, i, r),
            o = this.rtl ? "right": "left",
            l = parseInt(c.css("left"), 10),
            t = r.outerWidth(),
            a,
            b = {};
            r.index() !== d && !/skip/.test(s) ? /left/.test(s) ? (this.lock(), b[o] = l + t, c.animate(b, 300, this.unlock())) : /right/.test(s) && (this.lock(), b[o] = l - t, c.animate(b, 300, this.unlock())) : /skip/.test(s) && (a = r.index() - this.settings.up_count, this.lock(), a > 0 ? (b[o] = -(a * t), c.animate(b, 300, this.unlock())) : (b[o] = 0, c.animate(b, 300, this.unlock()))),
            n()
        },
        direction: function(n, a, b) {
            var d = this.S("li", n),
            c = d.outerWidth() + d.outerWidth() / 4,
            i = Math.floor(this.S(".clearing-container").outerWidth() / c) - 1,
            o = d.index(b),
            p;
            return this.settings.up_count = i,
            this.adjacent(this.settings.prev_index, o) ? o > i && o > this.settings.prev_index ? p = "right": o > i - 1 && o <= this.settings.prev_index ? p = "left": p = !1 : p = "skip",
            this.settings.prev_index = o,
            p
        },
        adjacent: function(c, a) {
            for (var b = a + 1; b >= a - 1; b--) {
                if (b === c) {
                    return ! 0
                }
            }
            return ! 1
        },
        lock: function() {
            this.settings.locked = !0
        },
        unlock: function() {
            this.settings.locked = !1
        },
        locked: function() {
            return this.settings.locked
        },
        off: function() {
            this.S(this.scope).off(".fndtn.clearing"),
            this.S(e).off(".fndtn.clearing")
        },
        reflow: function() {
            this.init()
        }
    }
} (jQuery, this, this.document),
!
function(b) {
    "function" == typeof define && define.amd ? define(["jquery"], b) : b(jQuery)
} (function(i) {
    function l(a) {
        return k.raw ? a: encodeURIComponent(a)
    }
    function n(a) {
        return k.raw ? a: decodeURIComponent(a)
    }
    function p(a) {
        return l(k.json ? JSON.stringify(a) : String(a))
    }
    function m(a) {
        0 === a.indexOf('"') && (a = a.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, "\\"));
        try {
            a = decodeURIComponent(a.replace(o, " "))
        } catch(b) {
            return
        }
        try {
            return k.json ? JSON.parse(a) : a
        } catch(b) {}
    }
    function e(c, a) {
        var b = k.raw ? c: m(c);
        return i.isFunction(a) ? a(b) : b
    }
    var o = /\+/g,
    k = i.cookie = function(g, w, a) {
        if (void 0 !== w && !i.isFunction(w)) {
            if (a = i.extend({},
            k.defaults, a), "number" == typeof a.expires) {
                var x = a.expires,
                b = a.expires = new Date;
                b.setDate(b.getDate() + x)
            }
            return document.cookie = [l(g), "=", p(w), a.expires ? "; expires=" + a.expires.toUTCString() : "", a.path ? "; path=" + a.path: "", a.domain ? "; domain=" + a.domain: "", a.secure ? "; secure": ""].join("")
        }
        for (var d = g ? void 0 : {},
        j = document.cookie ? document.cookie.split("; ") : [], u = 0, f = j.length; f > u; u++) {
            var v = j[u].split("="),
            c = n(v.shift()),
            h = v.join("=");
            if (g && g === c) {
                d = e(h, w);
                break
            }
            g || void 0 === (h = e(h)) || (d[c] = h)
        }
        return d
    };
    k.defaults = {},
    i.removeCookie = function(a, b) {
        return void 0 !== i.cookie(a) ? (i.cookie(a, "", i.extend({},
        b, {
            expires: -1
        })), !0) : !1
    }
}),
function(e, h, j, i) {
    var g = g || !1;
    Foundation.libs.joyride = {
        name: "joyride",
        version: "5.2.2",
        defaults: {
            expose: !1,
            modal: !0,
            tip_location: "bottom",
            nub_position: "auto",
            scroll_speed: 1500,
            scroll_animation: "linear",
            timer: 0,
            start_timer_on_click: !0,
            start_offset: 0,
            next_button: !0,
            tip_animation: "fade",
            pause_after: [],
            exposed: [],
            tip_animation_fade_speed: 300,
            cookie_monster: !1,
            cookie_name: "joyride",
            cookie_domain: !1,
            cookie_expires: 365,
            tip_container: "body",
            abort_on_close: !0,
            tip_location_patterns: {
                top: ["bottom"],
                bottom: [],
                left: ["right", "top", "bottom"],
                right: ["left", "top", "bottom"]
            },
            post_ride_callback: function() {},
            post_step_callback: function() {},
            pre_step_callback: function() {},
            pre_ride_callback: function() {},
            post_expose_callback: function() {},
            template: {
                link: '<a href="#close" class="joyride-close-tip">&times;</a>',
                timer: '<div class="joyride-timer-indicator-wrap"><span class="joyride-timer-indicator"></span></div>',
                tip: '<div class="joyride-tip-guide"><span class="joyride-nub"></span></div>',
                wrapper: '<div class="joyride-content-wrapper"></div>',
                button: '<a href="#" class="small button joyride-next-tip"></a>',
                modal: '<div class="joyride-modal-bg"></div>',
                expose: '<div class="joyride-expose-wrapper"></div>',
                expose_cover: '<div class="joyride-expose-cover"></div>'
            },
            expose_add_class: ""
        },
        init: function(b, c, a) {
            Foundation.inherit(this, "throttle random_str"),
            this.settings = this.settings || e.extend({},
            this.defaults, a || c),
            this.bindings(c, a)
        },
        events: function() {
            var a = this;
            e(this.scope).off(".joyride").on("click.fndtn.joyride", ".joyride-next-tip, .joyride-modal-bg",
            function(b) {
                b.preventDefault(),
                this.settings.$li.next().length < 1 ? this.end() : this.settings.timer > 0 ? (clearTimeout(this.settings.automate), this.hide(), this.show(), this.startTimer()) : (this.hide(), this.show())
            }.bind(this)).on("click.fndtn.joyride", ".joyride-close-tip",
            function(b) {
                b.preventDefault(),
                this.end(this.settings.abort_on_close)
            }.bind(this)),
            e(h).off(".joyride").on("resize.fndtn.joyride", a.throttle(function() {
                if (e("[" + a.attr_name() + "]").length > 0 && a.settings.$next_tip) {
                    if (a.settings.exposed.length > 0) {
                        var b = e(a.settings.exposed);
                        b.each(function() {
                            var c = e(this);
                            a.un_expose(c),
                            a.expose(c)
                        })
                    }
                    a.is_phone() ? a.pos_phone() : a.pos_default(!1, !0)
                }
            },
            100))
        },
        start: function() {
            var d = this,
            a = e("[" + this.attr_name() + "]", this.scope),
            b = ["timer", "scrollSpeed", "startOffset", "tipAnimationFadeSpeed", "cookieExpires"],
            c = b.length;
            if (!a.length > 0) {
                return
            }
            this.settings.init || this.events(),
            this.settings = a.data(this.attr_name(!0) + "-init"),
            this.settings.$content_el = a,
            this.settings.$body = e(this.settings.tip_container),
            this.settings.body_offset = e(this.settings.tip_container).position(),
            this.settings.$tip_content = this.settings.$content_el.find("> li"),
            this.settings.paused = !1,
            this.settings.attempts = 0,
            typeof e.cookie != "function" && (this.settings.cookie_monster = !1);
            if (!this.settings.cookie_monster || this.settings.cookie_monster && !e.cookie(this.settings.cookie_name)) {
                this.settings.$tip_content.each(function(n) {
                    var m = e(this);
                    this.settings = e.extend({},
                    d.defaults, d.data_options(m));
                    var f = c;
                    while (f--) {
                        d.settings[b[f]] = parseInt(d.settings[b[f]], 10)
                    }
                    d.create({
                        $li: m,
                        index: n
                    })
                }),
                !this.settings.start_timer_on_click && this.settings.timer > 0 ? (this.show("init"), this.startTimer()) : this.show("init")
            }
        },
        resume: function() {
            this.set_li(),
            this.show()
        },
        tip_template: function(b) {
            var c, a;
            return b.tip_class = b.tip_class || "",
            c = e(this.settings.template.tip).addClass(b.tip_class),
            a = e.trim(e(b.li).html()) + this.button_text(b.button_text) + this.settings.template.link + this.timer_instance(b.index),
            c.append(e(this.settings.template.wrapper)),
            c.first().attr(this.add_namespace("data-index"), b.index),
            e(".joyride-content-wrapper", c).append(a),
            c[0]
        },
        timer_instance: function(b) {
            var a;
            return b === 0 && this.settings.start_timer_on_click && this.settings.timer > 0 || this.settings.timer === 0 ? a = "": a = e(this.settings.template.timer)[0].outerHTML,
            a
        },
        button_text: function(a) {
            return this.settings.next_button ? (a = e.trim(a) || "Next", a = e(this.settings.template.button).append(a)[0].outerHTML) : a = "",
            a
        },
        create: function(d) {
            var a = d.$li.attr(this.add_namespace("data-button")) || d.$li.attr(this.add_namespace("data-text")),
            b = d.$li.attr("class"),
            c = e(this.tip_template({
                tip_class: b,
                index: d.index,
                button_text: a,
                li: d.$li
            }));
            e(this.settings.tip_container).append(c)
        },
        show: function(b) {
            var a = null;
            this.settings.$li === i || e.inArray(this.settings.$li.index(), this.settings.pause_after) === -1 ? (this.settings.paused ? this.settings.paused = !1 : this.set_li(b), this.settings.attempts = 0, this.settings.$li.length && this.settings.$target.length > 0 ? (b && (this.settings.pre_ride_callback(this.settings.$li.index(), this.settings.$next_tip), this.settings.modal && this.show_modal()), this.settings.pre_step_callback(this.settings.$li.index(), this.settings.$next_tip), this.settings.modal && this.settings.expose && this.expose(), this.settings.tip_settings = e.extend({},
            this.settings, this.data_options(this.settings.$li)), this.settings.timer = parseInt(this.settings.timer, 10), this.settings.tip_settings.tip_location_pattern = this.settings.tip_location_patterns[this.settings.tip_settings.tip_location], /body/i.test(this.settings.$target.selector) || this.scroll_to(), this.is_phone() ? this.pos_phone(!0) : this.pos_default(!0), a = this.settings.$next_tip.find(".joyride-timer-indicator"), /pop/i.test(this.settings.tip_animation) ? (a.width(0), this.settings.timer > 0 ? (this.settings.$next_tip.show(), setTimeout(function() {
                a.animate({
                    width: a.parent().width()
                },
                this.settings.timer, "linear")
            }.bind(this), this.settings.tip_animation_fade_speed)) : this.settings.$next_tip.show()) : /fade/i.test(this.settings.tip_animation) && (a.width(0), this.settings.timer > 0 ? (this.settings.$next_tip.fadeIn(this.settings.tip_animation_fade_speed).show(), setTimeout(function() {
                a.animate({
                    width: a.parent().width()
                },
                this.settings.timer, "linear")
            }.bind(this), this.settings.tip_animation_fadeSpeed)) : this.settings.$next_tip.fadeIn(this.settings.tip_animation_fade_speed)), this.settings.$current_tip = this.settings.$next_tip) : this.settings.$li && this.settings.$target.length < 1 ? this.show() : this.end()) : this.settings.paused = !0
        },
        is_phone: function() {
            return matchMedia(Foundation.media_queries.small).matches && !matchMedia(Foundation.media_queries.medium).matches
        },
        hide: function() {
            this.settings.modal && this.settings.expose && this.un_expose(),
            this.settings.modal || e(".joyride-modal-bg").hide(),
            this.settings.$current_tip.css("visibility", "hidden"),
            setTimeout(e.proxy(function() {
                this.hide(),
                this.css("visibility", "visible")
            },
            this.settings.$current_tip), 0),
            this.settings.post_step_callback(this.settings.$li.index(), this.settings.$current_tip)
        },
        set_li: function(a) {
            a ? (this.settings.$li = this.settings.$tip_content.eq(this.settings.start_offset), this.set_next_tip(), this.settings.$current_tip = this.settings.$next_tip) : (this.settings.$li = this.settings.$li.next(), this.set_next_tip()),
            this.set_target()
        },
        set_next_tip: function() {
            this.settings.$next_tip = e(".joyride-tip-guide").eq(this.settings.$li.index()),
            this.settings.$next_tip.data("closed", "")
        },
        set_target: function() {
            var c = this.settings.$li.attr(this.add_namespace("data-class")),
            a = this.settings.$li.attr(this.add_namespace("data-id")),
            b = function() {
                return a ? e(j.getElementById(a)) : c ? e("." + c).first() : e("body")
            };
            this.settings.$target = b()
        },
        scroll_to: function() {
            var a, b;
            a = e(h).height() / 2,
            b = Math.ceil(this.settings.$target.offset().top - a + this.settings.$next_tip.outerHeight()),
            b != 0 && e("html, body").animate({
                scrollTop: b
            },
            this.settings.scroll_speed, "swing")
        },
        paused: function() {
            return e.inArray(this.settings.$li.index() + 1, this.settings.pause_after) === -1
        },
        restart: function() {
            this.hide(),
            this.settings.$li = i,
            this.show("init")
        },
        pos_default: function(c, f) {
            var a = Math.ceil(e(h).height() / 2),
            n = this.settings.$next_tip.offset(),
            b = this.settings.$next_tip.find(".joyride-nub"),
            p = Math.ceil(b.outerWidth() / 2),
            d = Math.ceil(b.outerHeight() / 2),
            o = c || !1;
            o && (this.settings.$next_tip.css("visibility", "hidden"), this.settings.$next_tip.show()),
            typeof f == "undefined" && (f = !1),
            /body/i.test(this.settings.$target.selector) ? this.settings.$li.length && this.pos_modal(b) : (this.bottom() ? (this.rtl ? this.settings.$next_tip.css({
                top: this.settings.$target.offset().top + d + this.settings.$target.outerHeight(),
                left: this.settings.$target.offset().left + this.settings.$target.outerWidth() - this.settings.$next_tip.outerWidth()
            }) : this.settings.$next_tip.css({
                top: this.settings.$target.offset().top + d + this.settings.$target.outerHeight(),
                left: this.settings.$target.offset().left
            }), this.nub_position(b, this.settings.tip_settings.nub_position, "top")) : this.top() ? (this.rtl ? this.settings.$next_tip.css({
                top: this.settings.$target.offset().top - this.settings.$next_tip.outerHeight() - d,
                left: this.settings.$target.offset().left + this.settings.$target.outerWidth() - this.settings.$next_tip.outerWidth()
            }) : this.settings.$next_tip.css({
                top: this.settings.$target.offset().top - this.settings.$next_tip.outerHeight() - d,
                left: this.settings.$target.offset().left
            }), this.nub_position(b, this.settings.tip_settings.nub_position, "bottom")) : this.right() ? (this.settings.$next_tip.css({
                top: this.settings.$target.offset().top,
                left: this.settings.$target.outerWidth() + this.settings.$target.offset().left + p
            }), this.nub_position(b, this.settings.tip_settings.nub_position, "left")) : this.left() && (this.settings.$next_tip.css({
                top: this.settings.$target.offset().top,
                left: this.settings.$target.offset().left - this.settings.$next_tip.outerWidth() - p
            }), this.nub_position(b, this.settings.tip_settings.nub_position, "right")), !this.visible(this.corners(this.settings.$next_tip)) && this.settings.attempts < this.settings.tip_settings.tip_location_pattern.length && (b.removeClass("bottom").removeClass("top").removeClass("right").removeClass("left"), this.settings.tip_settings.tip_location = this.settings.tip_settings.tip_location_pattern[this.settings.attempts], this.settings.attempts++, this.pos_default())),
            o && (this.settings.$next_tip.hide(), this.settings.$next_tip.css("visibility", "visible"))
        },
        pos_phone: function(d) {
            var n = this.settings.$next_tip.outerHeight(),
            b = this.settings.$next_tip.offset(),
            m = this.settings.$target.outerHeight(),
            c = e(".joyride-nub", this.settings.$next_tip),
            a = Math.ceil(c.outerHeight() / 2),
            f = d || !1;
            c.removeClass("bottom").removeClass("top").removeClass("right").removeClass("left"),
            f && (this.settings.$next_tip.css("visibility", "hidden"), this.settings.$next_tip.show()),
            /body/i.test(this.settings.$target.selector) ? this.settings.$li.length && this.pos_modal(c) : this.top() ? (this.settings.$next_tip.offset({
                top: this.settings.$target.offset().top - n - a
            }), c.addClass("bottom")) : (this.settings.$next_tip.offset({
                top: this.settings.$target.offset().top + m + a
            }), c.addClass("top")),
            f && (this.settings.$next_tip.hide(), this.settings.$next_tip.css("visibility", "visible"))
        },
        pos_modal: function(a) {
            this.center(),
            a.hide(),
            this.show_modal()
        },
        show_modal: function() {
            if (!this.settings.$next_tip.data("closed")) {
                var a = e(".joyride-modal-bg");
                a.length < 1 && e("body").append(this.settings.template.modal).show(),
                /pop/i.test(this.settings.tip_animation) ? a.show() : a.fadeIn(this.settings.tip_animation_fade_speed)
            }
        },
        expose: function() {
            var f, a, d, b, l, c = "expose-" + this.random_str(6);
            if (arguments.length > 0 && arguments[0] instanceof e) {
                d = arguments[0]
            } else {
                if (!this.settings.$target || !!/body/i.test(this.settings.$target.selector)) {
                    return ! 1
                }
                d = this.settings.$target
            }
            if (d.length < 1) {
                return h.console && console.error("element not valid", d),
                !1
            }
            f = e(this.settings.template.expose),
            this.settings.$body.append(f),
            f.css({
                top: d.offset().top,
                left: d.offset().left,
                width: d.outerWidth(!0),
                height: d.outerHeight(!0)
            }),
            a = e(this.settings.template.expose_cover),
            b = {
                zIndex: d.css("z-index"),
                position: d.css("position")
            },
            l = d.attr("class") == null ? "": d.attr("class"),
            d.css("z-index", parseInt(f.css("z-index")) + 1),
            b.position == "static" && d.css("position", "relative"),
            d.data("expose-css", b),
            d.data("orig-class", l),
            d.attr("class", l + " " + this.settings.expose_add_class),
            a.css({
                top: d.offset().top,
                left: d.offset().left,
                width: d.outerWidth(!0),
                height: d.outerHeight(!0)
            }),
            this.settings.modal && this.show_modal(),
            this.settings.$body.append(a),
            f.addClass(c),
            a.addClass(c),
            d.data("expose", c),
            this.settings.post_expose_callback(this.settings.$li.index(), this.settings.$next_tip, d),
            this.add_exposed(d)
        },
        un_expose: function() {
            var f, a, d, b, l, c = !1;
            if (arguments.length > 0 && arguments[0] instanceof e) {
                a = arguments[0]
            } else {
                if (!this.settings.$target || !!/body/i.test(this.settings.$target.selector)) {
                    return ! 1
                }
                a = this.settings.$target
            }
            if (a.length < 1) {
                return h.console && console.error("element not valid", a),
                !1
            }
            f = a.data("expose"),
            d = e("." + f),
            arguments.length > 1 && (c = arguments[1]),
            c === !0 ? e(".joyride-expose-wrapper,.joyride-expose-cover").remove() : d.remove(),
            b = a.data("expose-css"),
            b.zIndex == "auto" ? a.css("z-index", "") : a.css("z-index", b.zIndex),
            b.position != a.css("position") && (b.position == "static" ? a.css("position", "") : a.css("position", b.position)),
            l = a.data("orig-class"),
            a.attr("class", l),
            a.removeData("orig-classes"),
            a.removeData("expose"),
            a.removeData("expose-z-index"),
            this.remove_exposed(a)
        },
        add_exposed: function(a) {
            this.settings.exposed = this.settings.exposed || [],
            a instanceof e || typeof a == "object" ? this.settings.exposed.push(a[0]) : typeof a == "string" && this.settings.exposed.push(a)
        },
        remove_exposed: function(b) {
            var c, a;
            b instanceof e ? c = b[0] : typeof b == "string" && (c = b),
            this.settings.exposed = this.settings.exposed || [],
            a = this.settings.exposed.length;
            while (a--) {
                if (this.settings.exposed[a] == c) {
                    this.settings.exposed.splice(a, 1);
                    return
                }
            }
        },
        center: function() {
            var a = e(h);
            return this.settings.$next_tip.css({
                top: (a.height() - this.settings.$next_tip.outerHeight()) / 2 + a.scrollTop(),
                left: (a.width() - this.settings.$next_tip.outerWidth()) / 2 + a.scrollLeft()
            }),
            !0
        },
        bottom: function() {
            return /bottom/i.test(this.settings.tip_settings.tip_location)
        },
        top: function() {
            return /top/i.test(this.settings.tip_settings.tip_location)
        },
        right: function() {
            return /right/i.test(this.settings.tip_settings.tip_location)
        },
        left: function() {
            return /left/i.test(this.settings.tip_settings.tip_location)
        },
        corners: function(c) {
            var f = e(h),
            a = f.height() / 2,
            n = Math.ceil(this.settings.$target.offset().top - a + this.settings.$next_tip.outerHeight()),
            b = f.width() + f.scrollLeft(),
            p = f.height() + n,
            d = f.height() + f.scrollTop(),
            o = f.scrollTop();
            return n < o && (n < 0 ? o = 0 : o = n),
            p > d && (d = p),
            [c.offset().top < o, b < c.offset().left + c.outerWidth(), d < c.offset().top + c.outerHeight(), f.scrollLeft() > c.offset().left]
        },
        visible: function(b) {
            var a = b.length;
            while (a--) {
                if (b[a]) {
                    return ! 1
                }
            }
            return ! 0
        },
        nub_position: function(b, c, a) {
            c === "auto" ? b.addClass(a) : b.addClass(c)
        },
        startTimer: function() {
            this.settings.$li.length ? this.settings.automate = setTimeout(function() {
                this.hide(),
                this.show(),
                this.startTimer()
            }.bind(this), this.settings.timer) : clearTimeout(this.settings.automate)
        },
        end: function(a) {
            this.settings.cookie_monster && e.cookie(this.settings.cookie_name, "ridden", {
                expires: this.settings.cookie_expires,
                domain: this.settings.cookie_domain
            }),
            this.settings.timer > 0 && clearTimeout(this.settings.automate),
            this.settings.modal && this.settings.expose && this.un_expose(),
            this.settings.$next_tip.data("closed", !0),
            e(".joyride-modal-bg").hide(),
            this.settings.$current_tip.hide(),
            typeof a == "undefined" && (this.settings.post_step_callback(this.settings.$li.index(), this.settings.$current_tip), this.settings.post_ride_callback(this.settings.$li.index(), this.settings.$current_tip)),
            e(".joyride-tip-guide").remove()
        },
        off: function() {
            e(this.scope).off(".joyride"),
            e(h).off(".joyride"),
            e(".joyride-close-tip, .joyride-next-tip, .joyride-modal-bg").off(".joyride"),
            e(".joyride-tip-guide, .joyride-modal-bg").remove(),
            clearTimeout(this.settings.automate),
            this.settings = {}
        },
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(i, l, n, p) {
    var m = function() {},
    e = function(j, x) {
        if (j.hasClass(x.slides_container_class)) {
            return this
        }
        var g = this,
        s, h = j,
        a, b, d, f = 0,
        c, r = !1,
        v = h.find("." + x.active_slide_class).length > 0;
        g.cache = {},
        g.slides = function() {
            return h.children(x.slide_selector)
        },
        v || g.slides().first().addClass(x.active_slide_class),
        g.update_slide_number = function(q) {
            x.slide_number && (a.find("span:first").text(parseInt(q) + 1), a.find("span:last").text(g.slides().length)),
            x.bullets && (b.children().removeClass(x.bullets_active_class), i(b.children().get(q)).addClass(x.bullets_active_class))
        },
        g.update_active_link = function(t) {
            var q = i('[data-orbit-link="' + g.slides().eq(t).attr("data-orbit-slide") + '"]');
            q.siblings().removeClass(x.bullets_active_class),
            q.addClass(x.bullets_active_class)
        },
        g.build_markup = function() {
            h.wrap('<div class="' + x.container_class + '"></div>'),
            s = h.parent(),
            h.addClass(x.slides_container_class),
            h.addClass(x.animation),
            x.stack_on_small && s.addClass(x.stack_on_small_class),
            x.navigation_arrows && (s.append(i('<a href="#"><span></span></a>').addClass(x.prev_class)), s.append(i('<a href="#"><span></span></a>').addClass(x.next_class))),
            x.timer && (d = i("<div>").addClass(x.timer_container_class), d.append("<span>"), x.timer_show_progress_bar && d.append(i("<div>").addClass(x.timer_progress_class)), d.addClass(x.timer_paused_class), s.append(d)),
            x.slide_number && (a = i("<div>").addClass(x.slide_number_class), a.append("<span></span> " + x.slide_number_text + " <span></span>"), s.append(a)),
            x.bullets && (b = i("<ol>").addClass(x.bullets_container_class), s.append(b), b.wrap('<div class="orbit-bullets-container"></div>'), g.slides().each(function(t, u) {
                var q = i("<li>").attr("data-orbit-slide", t);
                b.append(q)
            }))
        },
        g._prepare_direction = function(u, y) {
            var w = "next";
            u <= f && (w = "prev"),
            x.animation === "slide" && setTimeout(function() {
                h.removeClass("swipe-prev swipe-next"),
                w === "next" ? h.addClass("swipe-next") : w === "prev" && h.addClass("swipe-prev")
            },
            0);
            var q = g.slides();
            if (u >= q.length) {
                if (!x.circular) {
                    return ! 1
                }
                u = 0
            } else {
                if (u < 0) {
                    if (!x.circular) {
                        return ! 1
                    }
                    u = q.length - 1
                }
            }
            var t = i(q.get(f)),
            C = i(q.get(u));
            return [w, t, C, u]
        },
        g._goto = function(F, A) {
            if (F === null) {
                return ! 1
            }
            if (g.cache.animating) {
                return ! 1
            }
            if (F === f) {
                return ! 1
            }
            typeof g.cache.timer == "object" && g.cache.timer.restart();
            var z = g.slides();
            g.cache.animating = !0;
            var I = g._prepare_direction(F),
            t = I[0],
            u = I[1],
            w = I[2],
            F = I[3];
            if (I === !1) {
                return ! 1
            }
            h.trigger("before-slide-change.fndtn.orbit"),
            x.before_slide_change(),
            f = F,
            u.css("transitionDuration", x.animation_speed + "ms"),
            w.css("transitionDuration", x.animation_speed + "ms");
            var q = function() {
                var B = function() {
                    A === !0 && g.cache.timer.restart(),
                    g.update_slide_number(f),
                    w.addClass(x.active_slide_class),
                    g.update_active_link(F),
                    h.trigger("after-slide-change.fndtn.orbit", [{
                        slide_number: f,
                        total_slides: z.length
                    }]),
                    x.after_slide_change(f, z.length),
                    setTimeout(function() {
                        g.cache.animating = !1
                    },
                    100)
                };
                h.height() != w.height() && x.variable_height ? h.animate({
                    height: w.height()
                },
                250, "linear", B) : B()
            };
            if (z.length === 1) {
                return q(),
                !1
            }
            var y = function() {
                t === "next" && c.next(u, w, q),
                t === "prev" && c.prev(u, w, q)
            };
            w.height() > h.height() && x.variable_height ? h.animate({
                height: w.height()
            },
            250, "linear", y) : y()
        },
        g.next = function(q) {
            q.stopImmediatePropagation(),
            q.preventDefault(),
            g._prepare_direction(f + 1),
            setTimeout(function() {
                g._goto(f + 1)
            },
            100)
        },
        g.prev = function(q) {
            q.stopImmediatePropagation(),
            q.preventDefault(),
            g._prepare_direction(f - 1),
            setTimeout(function() {
                g._goto(f - 1)
            },
            100)
        },
        g.link_custom = function(t) {
            t.preventDefault();
            var u = i(this).attr("data-orbit-link");
            if (typeof u == "string" && (u = i.trim(u)) != "") {
                var q = s.find("[data-orbit-slide=" + u + "]");
                q.index() != -1 && setTimeout(function() {
                    g._goto(q.index())
                },
                100)
            }
        },
        g.link_bullet = function(t) {
            var u = i(this).attr("data-orbit-slide");
            if (typeof u == "string" && (u = i.trim(u)) != "") {
                if (isNaN(parseInt(u))) {
                    var q = s.find("[data-orbit-slide=" + u + "]");
                    q.index() != -1 && setTimeout(function() {
                        g._goto(q.index() + 1)
                    },
                    100)
                } else {
                    setTimeout(function() {
                        g._goto(parseInt(u))
                    },
                    100)
                }
            }
        },
        g.timer_callback = function() {
            g._goto(f + 1, !0)
        },
        g.compute_dimensions = function() {
            var t = i(g.slides().get(f)),
            q = t.height();
            x.variable_height || g.slides().each(function() {
                i(this).height() > q && (q = i(this).height())
            }),
            h.height(q)
        },
        g.create_timer = function() {
            var q = new o(s.find("." + x.timer_container_class), x, g.timer_callback);
            return q
        },
        g.stop_timer = function() {
            typeof g.cache.timer == "object" && g.cache.timer.stop()
        },
        g.toggle_timer = function() {
            var q = s.find("." + x.timer_container_class);
            q.hasClass(x.timer_paused_class) ? (typeof g.cache.timer == "undefined" && (g.cache.timer = g.create_timer()), g.cache.timer.start()) : typeof g.cache.timer == "object" && g.cache.timer.stop()
        },
        g.init = function() {
            g.build_markup(),
            x.timer && (g.cache.timer = g.create_timer(), Foundation.utils.image_loaded(this.slides().children("img"), g.cache.timer.start)),
            c = new k(x, h);
            if (v) {
                var t = h.find("." + x.active_slide_class),
                q = x.animation_speed;
                x.animation_speed = 1,
                t.removeClass("active"),
                g._goto(t.index()),
                x.animation_speed = q
            }
            s.on("click", "." + x.next_class, g.next),
            s.on("click", "." + x.prev_class, g.prev),
            x.next_on_click && s.on("click", "[data-orbit-slide]", g.link_bullet),
            s.on("click", g.toggle_timer),
            x.swipe && h.on("touchstart.fndtn.orbit",
            function(w) {
                if (g.cache.animating) {
                    return
                }
                w.touches || (w = w.originalEvent),
                w.preventDefault(),
                w.stopPropagation(),
                g.cache.start_page_x = w.touches[0].pageX,
                g.cache.start_page_y = w.touches[0].pageY,
                g.cache.start_time = (new Date).getTime(),
                g.cache.delta_x = 0,
                g.cache.is_scrolling = null,
                g.cache.direction = null,
                g.stop_timer()
            }).on("touchmove.fndtn.orbit",
            function(w) {
                Math.abs(g.cache.delta_x) > 5 && (w.preventDefault(), w.stopPropagation());
                if (g.cache.animating) {
                    return
                }
                requestAnimationFrame(function() {
                    w.touches || (w = w.originalEvent);
                    if (w.touches.length > 1 || w.scale && w.scale !== 1) {
                        return
                    }
                    g.cache.delta_x = w.touches[0].pageX - g.cache.start_page_x,
                    g.cache.is_scrolling === null && (g.cache.is_scrolling = !!(g.cache.is_scrolling || Math.abs(g.cache.delta_x) < Math.abs(w.touches[0].pageY - g.cache.start_page_y)));
                    if (g.cache.is_scrolling) {
                        return
                    }
                    var A = g.cache.delta_x < 0 ? f + 1 : f - 1;
                    if (g.cache.direction !== A) {
                        var z = g._prepare_direction(A);
                        g.cache.direction = A,
                        g.cache.dir = z[0],
                        g.cache.current = z[1],
                        g.cache.next = z[2]
                    }
                    if (x.animation === "slide") {
                        var F, y;
                        F = g.cache.delta_x / s.width() * 100,
                        F >= 0 ? y = -(100 - F) : y = 100 + F,
                        g.cache.current.css("transform", "translate3d(" + F + "%,0,0)"),
                        g.cache.next.css("transform", "translate3d(" + y + "%,0,0)")
                    }
                })
            }).on("touchend.fndtn.orbit",
            function(w) {
                if (g.cache.animating) {
                    return
                }
                w.preventDefault(),
                w.stopPropagation(),
                setTimeout(function() {
                    g._goto(g.cache.direction)
                },
                50)
            }),
            s.on("mouseenter.fndtn.orbit",
            function(w) {
                x.timer && x.pause_on_hover && g.stop_timer()
            }).on("mouseleave.fndtn.orbit",
            function(w) {
                x.timer && x.resume_on_mouseout && g.cache.timer.start()
            }),
            i(n).on("click", "[data-orbit-link]", g.link_custom),
            i(l).on("load resize", g.compute_dimensions);
            var u = this.slides().find("img");
            Foundation.utils.image_loaded(u, g.compute_dimensions),
            Foundation.utils.image_loaded(u,
            function() {
                s.prev("." + x.preloader_class).css("display", "none"),
                g.update_slide_number(f),
                g.update_active_link(f),
                h.trigger("ready.fndtn.orbit")
            })
        },
        g.init()
    },
    o = function(g, h, r) {
        var d = this,
        j = h.timer_speed,
        f = g.find("." + h.timer_progress_class),
        s = f && f.css("display") != "none",
        a,
        c,
        b = -1;
        this.update_progress = function(t) {
            var q = f.clone();
            q.attr("style", ""),
            q.css("width", t + "%"),
            f.replaceWith(q),
            f = q
        },
        this.restart = function() {
            clearTimeout(c),
            g.addClass(h.timer_paused_class),
            b = -1,
            s && d.update_progress(0),
            d.start()
        },
        this.start = function() {
            if (!g.hasClass(h.timer_paused_class)) {
                return ! 0
            }
            b = b === -1 ? j: b,
            g.removeClass(h.timer_paused_class),
            s && (a = (new Date).getTime(), f.animate({
                width: "100%"
            },
            b, "linear")),
            c = setTimeout(function() {
                d.restart(),
                r()
            },
            b),
            g.trigger("timer-started.fndtn.orbit")
        },
        this.stop = function() {
            if (g.hasClass(h.timer_paused_class)) {
                return ! 0
            }
            clearTimeout(c),
            g.addClass(h.timer_paused_class);
            if (s) {
                var t = (new Date).getTime();
                b -= t - a;
                var q = 100 - b / j * 100;
                d.update_progress(q)
            }
            g.trigger("timer-stopped.fndtn.orbit")
        }
    },
    k = function(b, a) {
        var c = "webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend";
        this.next = function(g, f, d) {
            Modernizr.csstransitions ? f.on(c,
            function(h) {
                f.unbind(c),
                g.removeClass("active animate-out"),
                f.removeClass("animate-in"),
                a.children().css({
                    transform: "",
                    "-ms-transform": "",
                    "-webkit-transition-duration": "",
                    "-moz-transition-duration": "",
                    "-o-transition-duration": "",
                    "transition-duration": ""
                }),
                d()
            }) : setTimeout(function() {
                g.removeClass("active animate-out"),
                f.removeClass("animate-in"),
                a.children().css({
                    transform: "",
                    "-ms-transform": "",
                    "-webkit-transition-duration": "",
                    "-moz-transition-duration": "",
                    "-o-transition-duration": "",
                    "transition-duration": ""
                }),
                d()
            },
            b.animation_speed),
            a.children().css({
                transform: "",
                "-ms-transform": "",
                "-webkit-transition-duration": "",
                "-moz-transition-duration": "",
                "-o-transition-duration": "",
                "transition-duration": ""
            }),
            g.addClass("animate-out"),
            f.addClass("animate-in")
        },
        this.prev = function(g, f, d) {
            Modernizr.csstransitions ? f.on(c,
            function(h) {
                f.unbind(c),
                g.removeClass("active animate-out"),
                f.removeClass("animate-in"),
                a.children().css({
                    transform: "",
                    "-ms-transform": "",
                    "-webkit-transition-duration": "",
                    "-moz-transition-duration": "",
                    "-o-transition-duration": "",
                    "transition-duration": ""
                }),
                d()
            }) : setTimeout(function() {
                g.removeClass("active animate-out"),
                f.removeClass("animate-in"),
                a.children().css({
                    transform: "",
                    "-ms-transform": "",
                    "-webkit-transition-duration": "",
                    "-moz-transition-duration": "",
                    "-o-transition-duration": "",
                    "transition-duration": ""
                }),
                d()
            },
            b.animation_speed),
            a.children().css({
                transform: "",
                "-ms-transform": "",
                "-webkit-transition-duration": "",
                "-moz-transition-duration": "",
                "-o-transition-duration": "",
                "transition-duration": ""
            }),
            g.addClass("animate-out"),
            f.addClass("animate-in")
        }
    };
    Foundation.libs = Foundation.libs || {},
    Foundation.libs.orbit = {
        name: "orbit",
        version: "5.2.2",
        settings: {
            animation: "slide",
            timer_speed: 10000,
            pause_on_hover: !0,
            resume_on_mouseout: !1,
            next_on_click: !0,
            animation_speed: 500,
            stack_on_small: !1,
            navigation_arrows: !0,
            slide_number: !0,
            slide_number_text: "of",
            container_class: "orbit-container",
            stack_on_small_class: "orbit-stack-on-small",
            next_class: "orbit-next",
            prev_class: "orbit-prev",
            timer_container_class: "orbit-timer",
            timer_paused_class: "paused",
            timer_progress_class: "orbit-progress",
            timer_show_progress_bar: !0,
            slides_container_class: "orbit-slides-container",
            preloader_class: "preloader",
            slide_selector: "*",
            bullets_container_class: "orbit-bullets",
            bullets_active_class: "active",
            slide_number_class: "orbit-slide-number",
            caption_class: "orbit-caption",
            active_slide_class: "active",
            orbit_transition_class: "orbit-transitioning",
            bullets: !0,
            circular: !0,
            timer: !0,
            variable_height: !1,
            swipe: !0,
            before_slide_change: m,
            after_slide_change: m
        },
        init: function(c, a, d) {
            var b = this;
            this.bindings(a, d)
        },
        events: function(a) {
            var b = new e(this.S(a), this.S(a).data("orbit-init"));
            this.S(a).data(self.name + "-instance", b)
        },
        reflow: function() {
            var b = this;
            if (b.S(b.scope).is("[data-orbit]")) {
                var a = b.S(b.scope),
                c = a.data(b.name + "-instance");
                c.compute_dimensions()
            } else {
                b.S("[data-orbit]", b.scope).each(function(g, j) {
                    var f = b.S(j),
                    d = b.data_options(f),
                    h = f.data(b.name + "-instance");
                    h.compute_dimensions()
                })
            }
        }
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.topbar = {
        name: "topbar",
        version: "5.2.2",
        settings: {
            index: 0,
            sticky_class: "sticky",
            custom_back_text: !0,
            back_text: "Back",
            is_hover: !0,
            mobile_show_parent_link: !1,
            scrolltop: !0,
            sticky_on: "all"
        },
        init: function(a, d, c) {
            Foundation.inherit(this, "add_custom_rule register_media throttle");
            var b = this;
            b.register_media("topbar", "foundation-mq-topbar"),
            this.bindings(d, c),
            b.S("[" + this.attr_name() + "]", this.scope).each(function() {
                var n = g(this),
                o = n.data(b.attr_name(!0) + "-init"),
                p = b.S("section", this),
                r = n.children().filter("ul").first();
                n.data("index", 0);
                var q = n.parent();
                q.hasClass("fixed") || b.is_sticky(n, q, o) ? (b.settings.sticky_class = o.sticky_class, b.settings.sticky_topbar = n, n.data("height", q.outerHeight()), n.data("stickyoffset", q.offset().top)) : n.data("height", n.outerHeight()),
                o.assembled || b.assemble(n),
                o.is_hover ? b.S(".has-dropdown", n).addClass("not-click") : b.S(".has-dropdown", n).removeClass("not-click"),
                b.add_custom_rule(".f-topbar-fixed { padding-top: " + n.data("height") + "px }"),
                q.hasClass("fixed") && b.S("body").addClass("f-topbar-fixed")
            })
        },
        is_sticky: function(a, c, d) {
            var b = c.hasClass(d.sticky_class);
            return b && d.sticky_on === "all" ? !0 : b && this.small() && d.sticky_on === "small" ? !0 : b && this.medium() && d.sticky_on === "medium" ? !0 : b && this.large() && d.sticky_on === "large" ? !0 : !1
        },
        toggle: function(c) {
            var a = this;
            if (c) {
                var b = a.S(c).closest("[" + this.attr_name() + "]")
            } else {
                var b = a.S("[" + this.attr_name() + "]")
            }
            var i = b.data(this.attr_name(!0) + "-init"),
            d = a.S("section, .section", b);
            a.breakpoint() && (a.rtl ? (d.css({
                right: "0%"
            }), g(">.name", d).css({
                right: "100%"
            })) : (d.css({
                left: "0%"
            }), g(">.name", d).css({
                left: "100%"
            })), a.S("li.moved", d).removeClass("moved"), b.data("index", 0), b.toggleClass("expanded").css("height", "")),
            i.scrolltop ? b.hasClass("expanded") ? b.parent().hasClass("fixed") && (i.scrolltop ? (b.parent().removeClass("fixed"), b.addClass("fixed"), a.S("body").removeClass("f-topbar-fixed"), e.scrollTo(0, 0)) : b.parent().removeClass("expanded")) : b.hasClass("fixed") && (b.parent().addClass("fixed"), b.removeClass("fixed"), a.S("body").addClass("f-topbar-fixed")) : (a.is_sticky(b, b.parent(), i) && b.parent().addClass("fixed"), b.parent().hasClass("fixed") && (b.hasClass("expanded") ? (b.addClass("fixed"), b.parent().addClass("expanded"), a.S("body").addClass("f-topbar-fixed")) : (b.removeClass("fixed"), b.parent().removeClass("expanded"), a.update_sticky_positioning())))
        },
        timer: null,
        events: function(a) {
            var b = this,
            c = this.S;
            c(this.scope).off(".topbar").on("click.fndtn.topbar", "[" + this.attr_name() + "] .toggle-topbar",
            function(d) {
                d.preventDefault(),
                b.toggle(this)
            }).on("click.fndtn.topbar", '.top-bar .top-bar-section li a[href^="#"],[' + this.attr_name() + '] .top-bar-section li a[href^="#"]',
            function(j) {
                var d = g(this).closest("li");
                b.breakpoint() && !d.hasClass("back") && !d.hasClass("has-dropdown") && b.toggle()
            }).on("click.fndtn.topbar", "[" + this.attr_name() + "] li.has-dropdown",
            function(m) {
                var p = c(this),
                o = c(m.target),
                d = p.closest("[" + b.attr_name() + "]"),
                n = d.data(b.attr_name(!0) + "-init");
                if (o.data("revealId")) {
                    b.toggle();
                    return
                }
                if (b.breakpoint()) {
                    return
                }
                if (n.is_hover && !Modernizr.touch) {
                    return
                }
                m.stopImmediatePropagation(),
                p.hasClass("hover") ? (p.removeClass("hover").find("li").removeClass("hover"), p.parents("li.hover").removeClass("hover")) : (p.addClass("hover"), g(p).siblings().removeClass("hover"), o[0].nodeName === "A" && o.parent().hasClass("has-dropdown") && m.preventDefault())
            }).on("click.fndtn.topbar", "[" + this.attr_name() + "] .has-dropdown>a",
            function(p) {
                if (b.breakpoint()) {
                    p.preventDefault();
                    var r = c(this),
                    d = r.closest("[" + b.attr_name() + "]"),
                    o = d.find("section, .section"),
                    n = r.next(".dropdown").outerHeight(),
                    q = r.closest("li");
                    d.data("index", d.data("index") + 1),
                    q.addClass("moved"),
                    b.rtl ? (o.css({
                        right: -(100 * d.data("index")) + "%"
                    }), o.find(">.name").css({
                        right: 100 * d.data("index") + "%"
                    })) : (o.css({
                        left: -(100 * d.data("index")) + "%"
                    }), o.find(">.name").css({
                        left: 100 * d.data("index") + "%"
                    })),
                    d.css("height", r.siblings("ul").outerHeight(!0) + d.data("height"))
                }
            }),
            c(e).off(".topbar").on("resize.fndtn.topbar", b.throttle(function() {
                b.resize.call(b)
            },
            50)).trigger("resize"),
            c("body").off(".topbar").on("click.fndtn.topbar touchstart.fndtn.topbar",
            function(d) {
                var j = c(d.target).closest("li").closest("li.hover");
                if (j.length > 0) {
                    return
                }
                c("[" + b.attr_name() + "] li.hover").removeClass("hover")
            }),
            c(this.scope).on("click.fndtn.topbar", "[" + this.attr_name() + "] .has-dropdown .back",
            function(o) {
                o.preventDefault();
                var r = c(this),
                d = r.closest("[" + b.attr_name() + "]"),
                q = d.find("section, .section"),
                n = d.data(b.attr_name(!0) + "-init"),
                s = r.closest("li.moved"),
                t = s.parent();
                d.data("index", d.data("index") - 1),
                b.rtl ? (q.css({
                    right: -(100 * d.data("index")) + "%"
                }), q.find(">.name").css({
                    right: 100 * d.data("index") + "%"
                })) : (q.css({
                    left: -(100 * d.data("index")) + "%"
                }), q.find(">.name").css({
                    left: 100 * d.data("index") + "%"
                })),
                d.data("index") === 0 ? d.css("height", "") : d.css("height", t.outerHeight(!0) + d.data("height")),
                setTimeout(function() {
                    s.removeClass("moved")
                },
                300)
            })
        },
        resize: function() {
            var a = this;
            a.S("[" + this.attr_name() + "]").each(function() {
                var i = a.S(this),
                d = i.data(a.attr_name(!0) + "-init"),
                b = i.parent("." + a.settings.sticky_class),
                l;
                if (!a.breakpoint()) {
                    var c = i.hasClass("expanded");
                    i.css("height", "").removeClass("expanded").find("li").removeClass("hover"),
                    c && a.toggle(i)
                }
                a.is_sticky(i, b, d) && (b.hasClass("fixed") ? (b.removeClass("fixed"), l = b.offset().top, a.S(h.body).hasClass("f-topbar-fixed") && (l -= i.data("height")), i.data("stickyoffset", l), b.addClass("fixed")) : (l = b.offset().top, i.data("stickyoffset", l)))
            })
        },
        breakpoint: function() {
            return ! matchMedia(Foundation.media_queries.topbar).matches
        },
        small: function() {
            return matchMedia(Foundation.media_queries.small).matches
        },
        medium: function() {
            return matchMedia(Foundation.media_queries.medium).matches
        },
        large: function() {
            return matchMedia(Foundation.media_queries.large).matches
        },
        assemble: function(b) {
            var a = this,
            i = b.data(this.attr_name(!0) + "-init"),
            d = a.S("section", b),
            c = g(this).children().filter("ul").first();
            d.detach(),
            a.S(".has-dropdown>a", d).each(function() {
                var j = a.S(this),
                n = j.siblings(".dropdown"),
                p = j.attr("href");
                if (!n.find(".title.back").length) {
                    if (i.mobile_show_parent_link && p && p.length > 1) {
                        var o = g('<li class="title back js-generated"><h5><a href="javascript:void(0)"></a></h5></li><li><a class="parent-link js-generated" href="' + p + '">' + j.text() + "</a></li>")
                    } else {
                        var o = g('<li class="title back js-generated"><h5><a href="javascript:void(0)"></a></h5></li>')
                    }
                    i.custom_back_text == 1 ? g("h5>a", o).html(i.back_text) : g("h5>a", o).html("&laquo; " + j.html()),
                    n.prepend(o)
                }
            }),
            d.appendTo(b),
            this.sticky(),
            this.assembled(b)
        },
        assembled: function(a) {
            a.data(this.attr_name(!0), g.extend({},
            a.data(this.attr_name(!0)), {
                assembled: !0
            }))
        },
        height: function(c) {
            var a = 0,
            b = this;
            return g("> li", c).each(function() {
                a += b.S(this).outerHeight(!0)
            }),
            a
        },
        sticky: function() {
            var a = this.S(e),
            b = this;
            this.S(e).on("scroll",
            function() {
                b.update_sticky_positioning()
            })
        },
        update_sticky_positioning: function() {
            var b = "." + this.settings.sticky_class,
            d = this.S(e),
            a = this;
            if (a.settings.sticky_topbar && a.is_sticky(this.settings.sticky_topbar, this.settings.sticky_topbar.parent(), this.settings)) {
                var c = this.settings.sticky_topbar.data("stickyoffset");
                a.S(b).hasClass("expanded") || (d.scrollTop() > c ? a.S(b).hasClass("fixed") || (a.S(b).addClass("fixed"), a.S("body").addClass("f-topbar-fixed")) : d.scrollTop() <= c && a.S(b).hasClass("fixed") && (a.S(b).removeClass("fixed"), a.S("body").removeClass("f-topbar-fixed")))
            }
        },
        off: function() {
            this.S(this.scope).off(".fndtn.topbar"),
            this.S(e).off(".fndtn.topbar")
        },
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.accordion = {
        name: "accordion",
        version: "5.2.2",
        settings: {
            active_class: "active",
            multi_expand: !1,
            toggleable: !0
        },
        init: function(c, a, b) {
            this.bindings(a, b)
        },
        events: function() {
            var b = this,
            a = this.S;
            a(this.scope).off(".fndtn.accordion").on("click.fndtn.accordion", "[" + this.attr_name() + "] dd > a",
            function(d) {
                var s = a(this).closest("[" + b.attr_name() + "]"),
                o = a("#" + this.href.split("#")[1]),
                r = a("dd > .content", s),
                l = g("dd", s),
                c = s.data(b.attr_name(!0) + "-init"),
                t = a("dd > .content." + c.active_class, s),
                i = a("dd." + c.active_class, s);
                d.preventDefault();
                if (!a(this).closest("dl").is(s)) {
                    return
                }
                if (c.toggleable && o.is(t)) {
                    return i.toggleClass(c.active_class, !1),
                    o.toggleClass(c.active_class, !1)
                }
                c.multi_expand || (r.removeClass(c.active_class), l.removeClass(c.active_class)),
                o.addClass(c.active_class).parent().addClass(c.active_class)
            })
        },
        off: function() {},
        reflow: function() {}
    }
} (jQuery, this, this.document),
function(g, e, h, f) {
    Foundation.libs.offcanvas = {
        name: "offcanvas",
        version: "5.2.2",
        settings: {},
        init: function(c, a, b) {
            this.events()
        },
        events: function() {
            var b = this,
            a = b.S;
            a(this.scope).off(".offcanvas").on("click.fndtn.offcanvas", ".left-off-canvas-toggle",
            function(c) {
                b.click_toggle_class(c, "move-right")
            }).on("click.fndtn.offcanvas", ".left-off-canvas-menu a",
            function(c) {
                a(".off-canvas-wrap").removeClass("move-right")
            }).on("click.fndtn.offcanvas", ".right-off-canvas-toggle",
            function(c) {
                b.click_toggle_class(c, "move-left")
            }).on("click.fndtn.offcanvas", ".right-off-canvas-menu a",
            function(c) {
                a(".off-canvas-wrap").removeClass("move-left")
            }).on("click.fndtn.offcanvas", ".exit-off-canvas",
            function(c) {
                b.click_remove_class(c, "move-left"),
                b.click_remove_class(c, "move-right")
            })
        },
        click_toggle_class: function(b, a) {
            b.preventDefault(),
            this.S(b.target).closest(".off-canvas-wrap").toggleClass(a)
        },
        click_remove_class: function(b, a) {
            b.preventDefault(),
            this.S(".off-canvas-wrap").removeClass(a)
        },
        reflow: function() {}
    }
} (jQuery, this, this.document);