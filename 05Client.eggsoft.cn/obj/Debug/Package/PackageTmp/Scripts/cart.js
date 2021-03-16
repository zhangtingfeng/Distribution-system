jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie
        if (value == "-1") {
            options = options || {};
            var path = options.path ? '; path=' + options.path : '/';
            var domain = ";domain=.3g.gitom.com"; //options.domain ? '; domain=' + options.domain : '; domain=.gitom.com';
            var secure = ""; //options.secure ? '; secure' : '';
            var date = new Date();
            date.setTime(date.getTime() - (2 * 24 * 60 * 60 * 1000));
            var expires = ';expires=' + date.toUTCString();
            document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
        }
        else {
            options = options || {};
            if (value === null) {
                value = '';
                options.expires = -1;
            }
            var expires = '';
            if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
                var date;
                if (typeof options.expires == 'number') {
                    date = new Date();
                    date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
                }
                else {
                    date = options.expires;
                }
                expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
            }
            var path = options.path ? '; path=' + options.path : '/';
            var domain = ";domain=.3g.gitom.com"; //options.domain ? '; domain=' + options.domain : '; domain=.gitom.com';
            var secure = ""; //options.secure ? '; secure' : '';
            document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
        }
    }
    else { // only name given, get cookie
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

jQuery.Cart = {
    delCart: function (ar, n) {
        if (n < 0) //如果n<0，则不进行任何操作。 
            return ar;
        else
            return ar.slice(0, n).concat(ar.slice(n + 1, ar.length));
    },
    //添加至购物车 
    //(产品id_颜色id_销售属性id,产品id,数量,颜色销售属性名称,颜色销售熟悉id,价格,站点id,店铺id)
    addCart: function (proStr, proid, quantity, Property, PropertyId, price, siteId, shopId) {
        try {
            if (proStr != "" && proid > 0) {
                var ProIDList = $.cookie("CartList"); //车内商品ID列表 
                if (ProIDList != null && ProIDList != "" && ProIDList != "null") {
                    var hasPro = $.Cart.hasCart(proStr);
                    if (hasPro == 0) {
                        ProIDList += "&" + proStr + "=" + proid + "|" + quantity + "|" + encodeURI(Property) + "|" + PropertyId + "|" + price + "|" + siteId + "|" + shopId;
                        $.cookie("CartList", ProIDList, { expires: 2, path: "/" }); //更新购物车清单
                    }
                    else {
                        $.Cart.updateCart(proStr, hasPro + quantity); //存在则增加相应的数量
                    }
                }
                else {
                    ProIDList = proStr + "=" + proid + "|" + quantity + "|" + encodeURI(Property) + "|" + PropertyId + "|" + price + "|" + siteId + "|" + shopId;
                    $.cookie("CartList", ProIDList, { expires: 2, path: "/" }); //更新购物车清单
                }
            }
            else {
                alert("产品id不能为空");
            }
        }
        catch (err) {
            $.Cart.clearCart();
            alert("浏览器发生错误，请重新订购，给您照成的麻烦，敬请原谅。");
        }
    },
    //添加物品结束 
    //检验购物车内是否已经含有该商品 
    hasCart: function (proStr) {
        ProIDList = $.cookie("CartList"); //车内商品ID列表 
        if (ProIDList.lastIndexOf("&") != -1) {
            var arr = ProIDList.split("&");
            for (i = 0; i < arr.length; i++) {
                if (arr[i].substring(0, arr[i].indexOf("=")) == proStr) {
                    var arr2 = arr[i].split("|");
                    return 1 * arr2[1];
                }
            }
        }
        else
            if (ProIDList != null && ProIDList != "" && ProIDList != "null") {
                if (ProIDList.substring(0, ProIDList.indexOf("=")) == proStr) {
                    var arr2 = ProIDList.split("|");
                    return 1 * arr2[1];
                }
            }
        return 0;
    },
    //检测结束 
    //移除某商品 
    removeCart: function (proStr) {
        try {
            if ($.Cart.hasCart(proStr) != 0) {
                ProIDList = $.cookie("CartList");
                if (ProIDList.lastIndexOf("&") != -1) {
                    var arr = ProIDList.split("&");
                    for (i = 0; i < arr.length; i++) {
                        if (arr[i].substring(0, arr[i].indexOf("=")) == proStr) {
                            var temparr = arr[i].split('|');
                            var arr2 = $.Cart.delCart(arr, i);
                            var tempStr = arr2.join("&"); //由数组重组字符串 
                            $.cookie("CartList", tempStr, { expires: 2, path: "/" }); //更新购物车清单 
                            return;
                        }
                    }
                }
                else {
                    if (ProIDList != "null" && ProIDList != "") {
                        $.cookie('CartList', -1, { path: "/" });
                    }
                }
            }
        }
        catch (err) {
            $.Cart.clearCart();
            alert("浏览器发生错误，请重新订购，给您照成的麻烦，敬请原谅。");
        }
    },
    //移除物品结束
    //修改商品数量 
    updateCart: function (proStr, quantity) {
        if (quantity <= 0) {
            $.Cart.removeCart(proStr);
        }
        else {
            ProIDList = $.cookie("CartList"); //车内商品ID列表
            if (ProIDList.lastIndexOf("&") != -1) {
                var arr = ProIDList.split("&");
                var sub = $.Cart.getSubPlace(ProIDList, proStr); //获取该物品在COOKIE数组中的下标位置 
                var arr2 = arr[sub].split("|");
                arr2[1] = quantity;
                var tempStr = arr2.join("|"); //由数组重组字符串 
                arr[sub] = tempStr;
                var newProList = arr.join("&"); //由数组重组字符串 
                $.cookie("CartList", newProList, { expires: 2, path: "/" }); //更新购物车清单
            }
            else {
                var arr = ProIDList.split("|");
                arr[1] = quantity;
                var newProList = arr.join("|");
                $.cookie("CartList", newProList, { expires: 2, path: "/" }); //更新购物车清单
            }
        }
    },
    //修改物品结束 proStr=10816_21_1010"
    //返回指定物品所在数组的下标位置 
    getSubPlace: function (list, proStr) {
        var arr = list.split("&");
        for (i = 0; i < arr.length; i++) {
            if (arr[i].substring(0, arr[i].indexOf("=")) == proStr) {
                return i;
            }
        }
    }, 
    //返回下标结束
    clearCart: function () {
        $.cookie('CartList', -1, { path: "/" });
    },
    //统计总价
    Total: function () {
        var cartList = $.cookie("CartList");
        if (!cartList) {
            $.Cart.clearCart();
            return { TotalPro: 0, TotalPrice: 0.00 };
        }
        var arr = cartList.split("&");
        if (arr.length == 0) {
            return { TotalPro: 0, TotalPrice: 0.00 };
        }
        else {
            var totalPrice = 0.00;
            var taotalPro = 0;
            for (i = 0; i < arr.length; i++) {
                var pro = arr[i].substring(arr[i].indexOf("="), arr[i].length);
                var proInfo = pro.split('|');
                taotalPro += 1;
                totalPrice += (1.00 * proInfo[1] * proInfo[4]);
            }
            return { TotalPro: taotalPro, TotalPrice: totalPrice.toFixed(2) };
        }
    }
};