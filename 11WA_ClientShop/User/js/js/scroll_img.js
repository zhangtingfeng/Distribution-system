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
        //������Ҫ�Ĳ����ĳ�ʼֵ�����ϲ�options����set����
        var set = $.extend({
            url: "data-url",
            fadeIn: 0
        }, options || {});
        var cache = [];

        $(this).each(function () {
            var nodeName = this.nodeName.toLowerCase();
            var url = $(this).attr(set.url);
            //��ȡÿ��Ԫ�ص���Ϣ
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
                    var winHeight = $(window).height(); //��ǰ���ڸ߶�
                    var scrolltop = $(window).scrollTop(); //������ƫ�Ƹ߶�
                    var oTop = obj.offset().top; //ͼƬ��Ը߶�
                    //�ж��Ƿ��ڵ�ǰ������
                    if ((oTop - scrolltop) > 0 && (oTop - scrolltop) < winHeight) {
                        if (tag === "img") {
                            if (set.fadeIn) {
                                //����Ч��
                                obj.fadeIn(set.fadeIn);
                            }
                            //��src���Ը�ֵ
                            obj.attr("src", url);
                        } else {
                            return false;
                        }
                        e.obj = null;
                    }
                }
            });
        }
        //���غ�����ִ��
        lazyLoading();
        //ִ�й����������¼�
        $(window).bind("scroll", lazyLoading);
    };
    $(".lazyLoading").imgLazyLoading({
        url: "data-url",
        fadeIn: 400
    });
});