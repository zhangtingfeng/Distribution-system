Gitom = {
    //版本
    version: 'v0.5',
    //命名空间
    namespace: function (str) {
        var arr = str.split("."), w = window;
        for (i = 0; i < arr.length; i++) {
            w[arr[i]] = w[arr[i]] || {};
            w = w[arr[i]];
        }
    },
    //继承
    extend: function () {
        // inline overrides  
        var io = function (o) {
            for (var m in o) {
                this[m] = o[m];
            }
        };
        var oc = Object.prototype.constructor;

        return function (sb, sp, overrides) {
            if (typeof sp == 'object') {
                overrides = sp;
                sp = sb;
                sb = overrides.constructor != oc ? overrides.constructor : function () { sp.apply(this, arguments); };
            }
            var F = function () { }, sbp, spp = sp.prototype;
            F.prototype = spp;
            sbp = sb.prototype = new F();
            sbp.constructor = sb;
            sb.superclass = spp;
            if (spp.constructor == oc) {
                spp.constructor = sp;
            }
            sb.override = function (o) {
                Gitom.override(sb, o);
            };
            sbp.override = io;
            Gitom.override(sb, overrides);
            sb.extend = function (o) { Gitom.extend(sb, o); };
            return sb;
        };
    } (),
    //重写
    override: function (origclass, overrides) {
        if (overrides) {
            var p = origclass.prototype;
            for (var method in overrides) {
                p[method] = overrides[method];
            }
        }
    },
    apply: function (o, c, defaults) {
        if (defaults) {
            // no "this" reference for friendly out of scope calls  
            Gitom.apply(o, defaults);
        }
        if (o && c && typeof c == 'object') {
            for (var p in c) {
                o[p] = c[p];
            }
        }
        return o;
    }
}
Gitom.namespace("Gitom.Config");
Gitom.Config = {
    widgetCdn: "/widgets/", //widgets文件夹的cdn地址
    stylesCdn: "/Styles/<%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityTemplet")%>/", //styles文件夹的cdn地址
    cdnImagesUrl: "/images/"//images文件夹的cdn地址
}
Gitom.namespace("Gitom.Position");
Gitom.Position = {
    getScrollPos: function (win) {
        if (win == null)
            win = window;
        var doc = win.document, pos;
        pos = { left: 0, top: 0 };
        try {
            if (typeof win.pageYOffset != "undefined")
                pos = {
                    left: win.pageXOffset,
                    top: win.pageYOffset
                };
            if (pos.left == 0 && pos.top == 0 && typeof doc.compatMode != "undefined" && doc.compatMode != "BackCompat")
                pos = {
                    left: doc.documentElement.scrollLeft,
                    top: doc.documentElement.scrollTop
                };
            if (pos.left == 0 && pos.top == 0 && typeof doc.body != "undefined")
                pos = {
                    left: doc.body.scrollLeft,
                    top: doc.body.scrollTop
                };
        } catch (e) {
        }
        return pos;
    }
};
Gitom.namespace("Gitom.Dialog");
Gitom.Dialog = {
    openFrameWindow: function (url, title, width, height) {
        width = width || 500;
        height = height || 450;
        //添加随机参数，防止iframe的缓存
        if (url && url.length > 0) {
            url = url + (url.indexOf("?") == -1 ? "?" : "&") + "_=" + new Date().getTime();
        }
        //var win = window.top._window || window._window;
        var win = window._window;
        return win.Open("[url]" + url, title, "isModal=yea,width=" + width + ",height=" + height);
    },
    openContentWindow: function (content, title, width, height) {
        width = width || 500;
        height = height || 450;
        //        var win = window.top._window || window._window;
        var win = window._window;
        return win.Open(content, title, "isModal=yea,width=" + width + ",height=" + height);
    },
    openModalWindow: function (content, title, width, height) {
        width = width || 500;
        height = height || 450;
        //        var win = window.top._window || window._window;
        var win = window._window;
        return win.Open(content, title, "width=" + width + ",height=" + height);
    }
};
Gitom.namespace("Gitom.CallService");
Gitom.CallService =
{
    callMethod: function (method, args, methodcallback, async) {
        if (method == null || method == undefined || method == '') {
            alert('必须提供方法名！');
        }
        var url = '/Jsonp.htm?t=' + Math.random();
        args = args || {};
        $.extend(args, { method: method });
        $.ajax({
            type: 'post',
            url: url,
            async: async || true,
            cache: false,
            datatype: args.contentType || "json",
            data: args,
            error: function (httpRequest, textStatus, errorThrown) {
                var reqMsg = '未知错误';
                if (textStatus == 'timeout')
                    reqMsg = '请求超时';
                if (textStatus == 'error' && httpRequest != null) {
                    if (httpRequest.status == 401)
                        reqMsg = '访问被拒绝。';
                    if (httpRequest.status == 401.1)
                        reqMsg = '登录失败。请重新登录...';
                    if (httpRequest.status == 413)
                        reqMsg = '请求实体太大。';
                    if (httpRequest.status == 417)
                        reqMsg = '执行失败。';
                    if (httpRequest.status == 500)
                        reqMsg = '内部服务器错误。';
                    if (httpRequest.status == 500.12)
                        reqMsg = '服务重新启动中,请稍候再试。';
                    if (httpRequest.status == 500.13) { reqMsg = '服务器太忙。'; } else {
                        reqMsg = '未知的请求错误';
                    }
                }
                if (textStatus == 'parsererror' && errorThrown != null) {
                    if (errorThrown.name == 'SyntaxError') { reqMsg = errorThrown.message; }
                }
                console.log('错误:',reqMsg);
            },
            success: function (result) {
                if (methodcallback != null) {
                    methodcallback(result); //回调函数，根据自己的业务写在相应的业务处理js函数内
                }
            }
        });
    }
}

//设置jQuery的全局错误事件捕获
//Genius Zhang
//$.ajaxSetup({
//    cache: false, //禁止缓存
//    async: true, //异步提交数据
//    global: true, //全局受控
//    //处理AJAX完成事件，主要为了处理返回的错误是JSON格式的
//    complete: function (r, t) {
//        //确定是JSON数据
//        if (t == "success" && r.responseText && r.responseText.indexOf("\"success\"") != -1 && r.responseText.indexOf("\"message\"") != -1) {
//            var data = eval("(" + r.responseText + ")");
//            //后台返回失败
//            if (!data.success) {
//                alert("服务器发生错误！错误信息为：\n\n" + data.message);
//            }
//        }
//    },
//    //全局处理AJAX错误
//    error: function () {
//        alert("服务器发生错误！请重试！");
//    }
//});

jQuery.inputListInit = function (option) {
    if (option.pID.toLowerCase().indexOf("checkbox") == 0) {//checkboxlist start
        var reg = /\[[0-9i]\]/;
        $("#" + option.pID + ">input:checkbox").each(function () {
            $(this).bind("click", { option: option }, function (event) {
                var option = event.data.option;
                //事件增加
                if (option.clickExtend) {
                    option.clickExtend({ self: $(this) });
                }
                if ($(this).prop("checked")) {
                    $(this).prop("checked", "true");
                    $(this).after("<input type=\"hidden\" value=\"" + $(this).val() + "\" id=\"hid" + $(this).attr("id") + "\" name=\"" + option.tagName + "\" />")
                    //设置值、用于validate
                    var checkAllValue = $("#hid" + option.tagID).val();
                    if (!checkAllValue) {
                        checkAllValue = $(this).val();
                    }
                    else {
                        checkAllValue += "," + $(this).val();
                    }
                    $("#hid" + option.tagID).val(checkAllValue);
                    //设置name索引
                    $("#" + option.pID + ">input:checkbox:checked").each(function (i) {
                        var ckname = option.tagName; //$(this).attr("name");
                        if (ckname.indexOf("[") > 0) {
                            ckname = ckname.replace(reg, "[" + i + "]");
                            //$(this).attr("name", ckname);
                            $("#hid" + $(this).attr("id")).attr("name", ckname);
                        }
                        else {
                            ckname = ckname + "[" + i + "]";
                            //$(this).attr("name", ckname);
                            $("#hid" + $(this).attr("id")).attr("name", ckname);
                        }
                    })
                }
                else {
                    $(this).prop("checked", "");
                    $("#hid" + $(this).attr("id")).remove();
                    //设置值、用于validate
                    var checkAllValue = "," + $("#hid" + option.tagID).val() + ",";
                    if (checkAllValue) {
                        checkAllValue = checkAllValue.replace("," + $(this).val() + ",", "");
                    }
                    $("#hid" + option.tagID).val(checkAllValue);
                    //设置name索引
                    $("#" + option.pID + ">input:checkbox:checked").each(function (i) {
                        var ckname = option.tagName; // $(this).attr("name");
                        if (ckname.indexOf("[") > 0) {
                            ckname = ckname.replace(reg, "[" + i + "]");
                            //$(this).attr("name", ckname);
                            $("#hid" + $(this).attr("id")).attr("name", ckname);
                        }
                        else {
                            ckname = ckname + "[" + i + "]";
                            //$(this).attr("name", ckname);
                            $("#hid" + $(this).attr("id")).attr("name", ckname);
                        }
                    })
                }
            })
        })
    } //checkboxlist end
    else if (option.pID.toLowerCase().indexOf("radio") == 0) { //radiolist start
        var id = option.pID.split('.');
        if (id.length > 1) {
            id = id[0] + "\\." + id[1];
        }
        var tagID = option.tagID.split('.');
        if (tagID.length > 1) {
            tagID = tagID[0] + "\\." + tagID[1];
        }
        $("#text" + tagID).val($("#" + id + ">input:radio:checked").next().html());
        $("#" + id + ">input:radio").each(function () {
            $(this).bind("click", { option: option }, function (event) {
                var option = event.data.option;
                //事件增加
                if (option.clickExtend) {
                    option.clickExtend({ self: $(this) });
                }
                if ($(this).attr("checked")) {
                    $("#" + tagID).val($(this).val());
                    $("#text" + tagID).val($(this).next().html());
                }
            })
        });
    } //radiolist end
}

//var pCreate = function () { };
//$("#pagemain").live('pagebeforeshow', function (event) {
//    pCreate();
//});
//pCreate = function () {
//    var url = document.location.href, re = /widgetid=([\d]+)/i;
//    if (re.test(url)) {
//        var widgetid = RegExp.$1;
//        $.ajax({
//            url: '/MAdmin/WidgetSet/RenewWidget.htm?_=' + new Date().getTime(),
//            type: 'POST',
//            data: "sitePageWidgetId=" + widgetid + "&loadCss=1&SitePageFlag=1&pageId=@(ViewBag.PageId)",
//            context: this,
//            cache: false,
//            success: function (data) {
//                _updateWidget("Widget" + widgetid, data);
//            }
//        });
//    }
//};
var myScroll;
jQuery.selectListInit = function (option) {
    var id = option.pID;
    var tagID = option.tagID;
    $("#" + (option.selectContainerId || "businessCon")).append(eval(option.tagID + 'Container'));
    $("#target_list_" + tagID).click(function () {
        var targetPos = $("#target_list_" + tagID).offset();
        $("#prompt_target_list_" + tagID).hide(); //隐藏提示
        //$("#" + id).css({ left: targetPos.left });
        //alert(targetPos.top);
        $("#" + id).show();
        $("#" + id).css({ top: targetPos.top, left: targetPos.left });
        if (!myScroll) {
            myScroll = new iScroll('select_list_ddlCategory');
        }
        return false;
    });
    $("#" + id + " ul li .ui-btn-text a").each(function () {
        $(this).bind("click", { option: option }, function (event) {
            var option = event.data.option;
            //事件增加
            if (option.clickExtend) {
                option.clickExtend({ self: $(this) });
            }
            $("input[name='" + tagID + "']").val($(this).attr("value"));
            $("#target_list_" + tagID + " .ui-btn-text span").html($(this).html());
            $("#" + id + " ul li").removeClass("ui-btn-active");
            $(this).parent().parent().parent().addClass("ui-btn-active");
            $("#" + id).hide();

            return false;
        })
    });
}