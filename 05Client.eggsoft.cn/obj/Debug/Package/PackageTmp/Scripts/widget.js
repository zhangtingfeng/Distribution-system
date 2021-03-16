function AddCart(siteId, shopId, proId, price, Store_Sum) {
    if (proId < 1) {
        $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("抱歉当前产品不存在!");
        $("#addCartSuccess").show();
        return;
    }
    if (Store_Sum < 1) {
        $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("抱歉当前库存不足!");
        $("#addCartSuccess").show();
        return;
    }

    var loginShopId = GetLoginShopID(siteId);
    if (loginShopId != shopId && loginShopId != 0) {
        if (!confirm("此产品不属于当前店铺的，是否继续购买")) {
            return;
        }
    }

    var proStr = proId + "_" + shopId; //如果有颜色及销售属性并接到此处
    var quantity = 1;
    var property = "";
    var PropertyId = "";
    try {
        $.Cart.addCart(proStr, proId, quantity, property, PropertyId, price, siteId, shopId);
        $("#addCartMsg").removeClass("jt_ts_err").addClass("jt_ts");
    }
    catch (e) {
        $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("加入购物车失败!");
    }
    $("#addCartSuccess").show();
}

function closeMsg() {
    $("#addCartSuccess").hide();
}

var Sku;
var total;
var price;
$(document).ready(function () {
    Sku = $("#hidSku").val();
    total = $("#PrototalHid").val();
    price = $("#PropriceHid").val();

    var proinfo = new ProInfo();
    proinfo.init();
});
function ProInfo() { };

ProInfo.prototype = {
    init: function () {
        var context = this;
        context.ClickColor();       //颜色
        context.ClickSale();        //销售
        context.OnlyOne();          //只有一个颜色或销售属性时
        context.btnBuy();
    },
    ClickColor: function () {
        var context = this;
        $("#Pro_color>i").click(function () {
            if ($(this).hasClass("disabled"))//库存小于1
            {
                return false;
            }
            $("#pro_property>i.disabled").removeClass("disabled");
            if ($(this).hasClass("sel"))//如果是已经选中了的
            {
                $(this).removeClass("sel");
                $("#colorImg").hide();
                $("#proPrice").find("em.red").text(price);
                $("#proPrice").find("em.total").text(total);
            }
            else {
                //如果有图片却换左边的图片
                var imgPath = $(this).attr("imgPath");
                if (typeof (imgPath) != "undefined" && imgPath.length > 0) {
                    $("#colorImg").show();
                    $("#colorImgSrc").attr("src", imgPath);
                }
                else {
                    $("#colorImg").hide();
                }
                $(this).addClass("sel").siblings("i.sel").removeClass("sel");
                context.ColorClickToStoreSum();
            }
            context.GetSelectedColorAndSale();
        });
    },
    ClickSale: function () {
        var context = this;
        $("#pro_property>i").click(function () {
            if ($(this).hasClass("disabled"))//库存小于1
            {
                return false;
            }
            $("#Pro_color>i.disabled").removeClass("disabled");
            if ($(this).hasClass("sel"))//如果是已经选中了的
            {
                $(this).removeClass("sel");
                $("#proPrice").find("em.red").text(price);
                $("#proPrice").find("em.total").text(total);
            }
            else {
                $(this).addClass("sel").siblings("i.sel").removeClass("sel");
                context.SaleClickToStoreSum();
            }
            context.GetSelectedColorAndSale();
        });
    },
    ColorClickToStoreSum: function ()//点击颜色时触发
    {
        var T = total;
        var P = price;
        var ColorSeled = $("#Pro_color>i.sel");
        var Cid = ColorSeled.attr("data-value");
        //alert("T:"+T+"P:"+P+"ColorSeled:"+ColorSeled+"Cid:"+Cid);
        if ($("#pro_property").is("div"))//如果销售属性存在
        {
            var SaleSeled = $("#pro_property>i.sel");
            if (SaleSeled.length > 0) {
                var Sid = SaleSeled.attr("data-value");
                var pattern = eval("/" + Cid + ";" + Sid + ",([0-9.]+),([0-9]+)/");
                var _match = Sku.match(pattern);
                if (_match) {
                    //alert(matchs[1]);
                    //alert(matchs[2]);
                    P = 1.00 * _match[1];
                    T = 1 * _match[2];
                    P = P.toFixed(2);
                }
            }
            //循环销售属性
            var patterns = eval("/" + Cid + ";([0-9]+):([0-9]+),([0-9.]+),([0-9]+)/g");
            var matchss = Sku.match(patterns);
            if (matchss) {
                for (var i = 0; i < matchss.length; i++) {
                    var str = matchss[i];
                    var pattern = eval("/([0-9]+:[0-9]+);([0-9]+:[0-9]+),([0-9.]+),([0-9]+)/");
                    var _match = str.match(pattern);
                    if (_match) {
                        if (_match[4] < 1) {
                            //alert(_match[2]);
                            $("#pro_property>i[data-value='" + _match[2] + "']").addClass("disabled");
                            //alert($("#pro_property ul>li[data-value='"+_match[2]+"']").attr("class"));
                        }
                    }
                }
            }
        }
        else {
            var pattern = eval("/" + Cid + ",([0-9.]+),([0-9]+)/");
            var _match = Sku.match(pattern);
            if (_match) {
                //alert(matchs[1]);
                //alert(matchs[2]);
                P = 1.00 * _match[1];
                T = 1 * _match[2];
                P = P.toFixed(2);
                //alert("T:"+T);
            }
        }
        $("#proPrice").find("em.red").text(P);
        $("#proPrice").find("em.total").text(T);
    },
    SaleClickToStoreSum: function ()//点击销售属性是触发
    {
        var T = total;
        var P = price;
        var SaleSeled = $("#pro_property>i.sel");
        var Sid = SaleSeled.attr("data-value");

        //alert("T:"+T+"P:"+P+"SaleSeled:"+SaleSeled+"Sid:"+Sid);
        if ($("#Pro_color").is("div"))//如果颜色存在
        {
            var ColorSeled = $("#Pro_color>i.sel");

            if (ColorSeled.length > 0) {
                var Cid = ColorSeled.attr("data-value");
                var pattern = eval("/" + Cid + ";" + Sid + ",([0-9.]+),([0-9]+)/");
                var _match = Sku.match(pattern);
                //  alert(_match+"------"+Sku);
                if (_match) {
                    //alert(matchs[1]);
                    //alert(matchs[2]);
                    P = 1.00 * _match[1]; //价格
                    T = 1 * _match[2]; //数量
                    // alert("T2:"+T);   
                    P = P.toFixed(2);
                }
            }
            //循环颜色
            var patterns = eval("/([0-9]+:[0-9]+);" + Sid + ",([0-9.]+),([0-9]+)/g");
            var matchss = Sku.match(patterns);
            if (matchss) {
                for (var i = 0; i < matchss.length; i++) {
                    var str = matchss[i];
                    var pattern = eval("/([0-9]+:[0-9]+);([0-9]+:[0-9]+),([0-9.]+),([0-9]+)/");
                    var _match = str.match(pattern);
                    if (_match) {
                        if (_match[4] < 1) {
                            //alert(_match[1]);
                            $("#Pro_color>i[data-value='" + _match[1] + "']").addClass("disabled");
                        }
                    }
                }
            }
        }
        else {
            var pattern = eval("/" + Sid + ",([0-9.]+),([0-9]+)/");
            var _match = Sku.match(pattern);
            if (_match) {
                //alert(matchs[1]);
                //alert(matchs[2]);
                P = 1.00 * _match[1];
                T = 1 * _match[2];
                P = P.toFixed(2);
            }
        }
        $("#proPrice").find("em.red").text(P);
        $("#proPrice").find("em.total").text(T);
    },
    GetSelectedColorAndSale: function ()//请选择
    {
        var showMsg = false;
        var menu = "请选择：";
        var colorName = "";
        var saleName = "";
        if ($("#Pro_color").is("div"))//如果颜色存在
        {
            if ($("#pro_property").is("div"))//如果销售存在
            {
                colorName = "未选择";
            }
            colorName += $("#Pro_color").attr("title");
            if ($("#Pro_color>i.sel").length > 0) {
                menu = "已选择：";
                colorName = "\"" + $("#Pro_color>i.sel").text() + "\"";
                showMsg = true;
            }
            $("#colorAndSaleSeled em.color").text(colorName);
        }

        if ($("#pro_property").is("div"))//如果销售存在
        {
            if ($("#Pro_color").is("div"))//如果颜色存在
            {
                saleName = "未选择";
            }
            saleName += $("#pro_property").attr("title");
            if ($("#pro_property>i.sel").length > 0) {
                menu = "已选择：";
                saleName = "\"" + $("#pro_property>i.sel").text() + "\"";
                showMsg = true;
            }
            $("#colorAndSaleSeled em.sale").text(saleName);
        }
        if (showMsg) {
            $("#colorAndSaleSeled").show();
            $("#colorAndSaleSeled h").text(menu);
            $("#proPrice").show();
        }
        else {
            $("#colorAndSaleSeled").hide();
            $("#proPrice").hide();
        }
    },
    OnlyOne: function () {
        if ($("#Pro_color>i").length == 1) {
            $("#Pro_color>i:first").click();
        }

        if ($("#pro_property>i").length == 1) {
            $("#pro_property>i:first").click();
        }
    },
    btnBuy: function () {
        var context = this;
        $("#btnBuy").click(function () {
            var proId = $("#ProductID").val();
            if (proId < 1) {
                $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("抱歉当前产品不存在!");
                $("#addCartSuccess").show();
                setTimeout(closeMsg, 2000);
                return;
            }
            var loginShopId = GetLoginShopID($("#siteId").val());
            var shopId = $("#shopId").val();
            if (loginShopId != shopId) {
                if (!confirm("此产品不属于当前店铺的，是否继续购买")) {
                    return;
                }
            }
            //var p = $("#sku-all");
            //var position = p.position();
            var proStr = proId; //如果有颜色及销售属性并接到此处
            var colorPidVid = "";
            var salePidVid = "";
            var propertyArr = [];
            var propertyIdArr = [];
            if ($("#Pro_color").is("div"))//如果颜色存在
            {
                if ($("#Pro_color>i.sel").length < 1) {
                    $("#warnMsgColorSale").css("top", document.body.scrollTop + 210).show();
                    $("#showColorAndSale").removeClass("down").addClass("up"); //显示出 销售属性
                    $("div.dsm-p").show();
                    setTimeout(function () { $("#warnMsgColorSale").hide(); }, 2000);
                    return;
                }
                else if ($("#Pro_color>i.sel").length == 1) {
                    proStr = proStr + "_" + $("#Pro_color>i.sel").attr("id");
                    colorPidVid = $("#Pro_color>i.sel").attr("data-value");
                    propertyIdArr.push(colorPidVid);
                    propertyArr.push($("#Pro_color").attr("title") + ":" + $("#Pro_color>i.sel").text());
                }
            }
            if ($("#pro_property").is("div"))//如果销售存在
            {
                if ($("#pro_property>i.sel").length < 1) {
                    $("#warnMsgColorSale").css("top", document.body.scrollTop + 210).show();
                    $("#showColorAndSale").removeClass("down").addClass("up"); //显示出 销售属性
                    $("div.dsm-p").show();
                    setTimeout(function () { $("#warnMsgColorSale").hide(); }, 2000);
                    return;
                }
                else if ($("#pro_property>i.sel").length == 1) {
                    proStr = proStr + "_" + $("#pro_property>i.sel").attr("id");
                    salePidVid = $("#pro_property>i.sel").attr("data-value");
                    propertyIdArr.push(salePidVid);
                    propertyArr.push($("#pro_property").attr("title") + ":" + $("#pro_property>i.sel").text());
                }
            }
            $("#warnMsgColorSale").hide();

            var price = 0;
            var Store_Sum = 0;
            var property = "";
            var propertyId = "";
            if ($("#Pro_color").is("div") || $("#pro_property").is("div"))//如果颜色存在
            {
                price = 1.00 * $("#proPrice").find("em.red").text();
                Store_Sum = 1 * $("#proPrice").find("em.total").text();
                property = propertyArr.join(",");
                propertyId = propertyIdArr.join(";");
            }
            else {
                price = 1.00 * $("#PropriceHid").val();
                Store_Sum = 1 * $("#PrototalHid").val();
            }
            context.AddCart(proId, proStr, property, propertyId, price, Store_Sum);
        });
    },
    AddCart: function (proId, proStr, property, propertyId, price, store_sum) {
        if (proId < 1) {
            $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("抱歉当前产品不存在!");
            $("#addCartSuccess").show();
            setTimeout(closeMsg, 2000);
            return;
        }
        if (store_sum < 1) {
            $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("抱歉当前库存不足!");
            $("#addCartSuccess").show();
            setTimeout(closeMsg, 2000);
            return;
        }
        var siteId = $("#siteId").val();
        if (siteId < 1) {
            $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("抱歉站点信息丢失，请刷新后重试!");
            $("#addCartSuccess").show();
            setTimeout(closeMsg, 2000);
            return;
        }
        var shopId = $("#shopId").val();
        proStr = proStr + "_" + shopId;
        var quantity = 1;
        try {
            $.Cart.addCart(proStr, proId, quantity, property, propertyId, price, siteId, shopId);
            $("#addCartMsg").removeClass("jt_ts_err").addClass("jt_ts");
        }
        catch (e) {
            $("#addCartMsg").removeClass("jt_ts").addClass("jt_ts_err").html("加入购物车失败!");
        }
        $("#addCartSuccess").show();
        setTimeout(closeMsg, 2000);
    }
};

//传入当前站点siteId
//返回当前进入的店铺id
function GetLoginShopID(siteId) {
    var CmsShop = $.cookie("Gitom3GCMSShop"); //车内商品ID列表
    if (CmsShop != null) {
        //CmsShop = escape(CmsShop);
        var cmsShopArr = CmsShop.split('|');
        if (cmsShopArr.length > 3) {
            if (parseInt(cmsShopArr[3]) != siteId) {//当然登录的站点与产品购买产品的站不是同一个站点的返回0
                return 0;
            }
            if (typeof cmsShopArr[2] != "undefined") {
                return cmsShopArr[2];
            }
        }
    }
    return 0;
}

function showAndhideArr() {
    if ($("#showColorAndSale").hasClass("up")) {
        $("#showColorAndSale").removeClass("up").addClass("down");
        $("div.dsm-p").hide();
    }
    else {
        $("#showColorAndSale").removeClass("down").addClass("up");
        $("div.dsm-p").show();
        //,p.dsm-sel,p.dsm-lap
    }
}

/******************************多图滑动******************/
$(function () {
    if ($("#detailContainer").is("div")) {
        var cur_width = $(window).width();
        $('#picTank').width(cur_width * $("#photoCountNumHid").val());
        $('#picTank').height(cur_width);
        var mySwiper = $('.type_show').swiper({
            mode: 'horizontal',
            loop: false
        });
//        $(".swiper-slide>img").each(function () {
//            var img_h = $(this).height();
//            var m_height = (cur_width - img_h) / 2 - 5;
//            $(this).css("margin-top", m_height + "px");
//        });
    }
});

function showZoom() {
    $("#bigColorImgSrc").attr("src", $("#colorImgSrc").attr("src").replace("_120_120",""));
    $("div.Enlarge").show();
    //setTimeout(closeBigImg, 8000);
}

function closeBigImg() {
    $("div.Enlarge").hide();
}