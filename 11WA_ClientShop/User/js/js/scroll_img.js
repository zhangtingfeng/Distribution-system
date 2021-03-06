$(function () {
    var slider = Swipe(document.getElementById('scroll_img'), {
        auto: 3000,
        continuous: true,
        callback: function (pos) {
            var i = bullets.length;
            while (i--) {
                bullets[i].className = ' ';
            }
            bullets[pos].className = 'on';
        }
    });
    var slider = Swipe(document.getElementById('scroll_img1'), {
        auto: 3400,
        continuous: true,
        callback: function (pos) {
            var i = bullets.length;
            while (i--) {
                bullets[i].className = ' ';
            }
            bullets[pos].className = 'on';
        }
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() > 100) {
            $("#gotop").fadeIn(1500);
        }
        else {
            $("#gotop").fadeOut(1500);
        }
    });
    $("#gotop").click(function () {
        $('body,html').animate({ scrollTop: 0 }, 1000);
        return false;
    });
    $.fn.imgLazyLoading = function (options) {
        //定义需要的参数的初始值，并合并options对象到set对象
        var set = $.extend({
            url: "data-url",
            fadeIn: 0
        }, options || {});
        var cache = [];

        $(this).each(function () {
            var nodeName = this.nodeName.toLowerCase();
            var url = $(this).attr(set.url);
            //获取每个元素的信息
            var data = {
                obj: $(this),
                url: url,
                tag: nodeName
            }
            cache.push(data);
        });

        var lazyLoading = function () {
            $.each(cache, function (i, e) {
                var obj = e.obj,
		          url = e.url,
		          tag = e.tag;
                if (obj) {
                    var winHeight = $(window).height(); //当前窗口高度
                    var scrolltop = $(window).scrollTop(); //滚动条偏移高度
                    var oTop = obj.offset().top; //图片相对高度
                    //判断是否在当前窗口内
                    if ((oTop - scrolltop) > 0 && (oTop - scrolltop) < winHeight) {
                        if (tag === "img") {
                            if (set.fadeIn) {
                                //渐出效果
                                obj.fadeIn(set.fadeIn);
                            }
                            //给src属性赋值
                            obj.attr("src", url);
                        } else {
                            return false;
                        }
                        e.obj = null;
                    }
                }
            });
        }
        //加载后立即执行
        lazyLoading();
        //执行滚动，触发事件
        $(window).bind("scroll", lazyLoading);
    };
    $(".lazyLoading").imgLazyLoading({
        url: "data-url",
        fadeIn: 400
    });
});