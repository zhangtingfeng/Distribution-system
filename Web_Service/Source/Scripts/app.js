﻿jQuery.modal = function () {
    var reg = new RegExp("\\[([^\\[\\]]*?)\\]", 'igm');
    var alr = $("#qyb_modal");
    var ahtml = alr.html();

    var _alert = function (options) {
        alr.html(ahtml);    // 复原
        alr.find('.ok').removeClass('btn-success').addClass('btn-primary');
        alr.find('.cancel').hide();
        _dialog(options);

        return {
            on: function (callback) {
                if (callback && callback instanceof Function) {
                    alr.find('.ok').click(function () { callback(true) });
                }
            }
        };
    };

    var _confirm = function (options) {
        alr.html(ahtml); // 复原
        alr.find('.ok').removeClass('btn-primary').addClass('btn-danger');
        alr.find('.cancel').show();
        _dialog(options);

        return {
            on: function (callback) {
                if (callback && callback instanceof Function) {
                    alr.find('.ok').click(function () { callback(true) });
                    alr.find('.cancel').click(function () { callback(false) });
                }
            }
        };
    };

    var _dialog = function (options) {
        var ops = {
            msg: "提示内容",
            title: "操作提示",
            btnok: "确定",
            btncl: "取消"
        };

        $.extend(ops, options);


        var html = alr.html().replace(reg, function (node, key) {
            return {
                Title: ops.title,
                Message: ops.msg,
                BtnOk: ops.btnok,
                BtnCancel: ops.btncl
            }[key];
        });

        alr.html(html);
        alr.modal({
            width: 500,
            backdrop: 'static'
        });
    }

    return {
        alert: _alert,
        confirm: _confirm
    }

}();