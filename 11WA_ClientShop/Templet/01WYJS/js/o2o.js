function intInThisChoiceClick(intInThisChoiceClick) {


    if (intInThisChoiceClick == 0) {
        $("#HowToGetProduct a:first").click();
    }
    else {
        $("#HowToGetProduct a:last").click();
    }

}

$(function () {




    $("#HowToGetProduct a").click(function () {
        $(this).addClass("style_select"); //添加样式
        $(this).addClass('current').siblings().removeClass('current');
        //alert();
        $(this).siblings().removeClass(); //移除样式

        var var_HowToGetProduct = $(this).children("span").attr("title");

        doClickThis_HowToGetProduct(var_HowToGetProduct);

        if (var_HowToGetProduct == "0") {
            $("#ShouHuoDizhiList").show();
            $("#o2oShop_AddressList").hide();

            $("#StaticCartselfFright").show();
            $("#StaticCartselfNoFright").hide();
        }
        else if (var_HowToGetProduct == "1") {
            $("#ShouHuoDizhiList").hide();
            $("#o2oShop_AddressList").show();

            $("#StaticCartselfFright").hide();
            $("#StaticCartselfNoFright").show();
            doMakeHtml_idGeto2oShop_Address_List();
        }

        Changeo2oChildList(var_HowToGetProduct);
    });



    function Changeo2oChildList(var0Or1) {
        var arrPrePrice = $('.Cart_pro_list_name_EachGoods');
        for (var i = 0; i < arrPrePrice.length; i++) {
            if (var0Or1 == "1") {
                $(arrPrePrice[i]).children(".FullMoney").hide();
                $(arrPrePrice[i]).children(".outDecimal_My_Freight").show();
            }
            else {
                $(arrPrePrice[i]).children(".FullMoney").show();
                $(arrPrePrice[i]).children(".outDecimal_My_Freight").hide();
            }
        }

        var arrPrePrice = $('.YJJ_inputs_OneLine');
        for (var i = 0; i < arrPrePrice.length; i++) {
            if (var0Or1 == "1") {
                $(arrPrePrice[i]).children(".YJJ_inputs_Line_Left").hide();
            }
            else {
                $(arrPrePrice[i]).children(".YJJ_inputs_Line_Left").show();
            }
        }

    }



    function doMakeHtml_idGeto2oShop_Address_List() {
        var idGeto2oShop_Address = $("#idGeto2oShop_AddressNeedHandleList").html();
        if (idGeto2oShop_Address == "") {//空的才调用 因为可能已经加载过聊

            $.ajax({
                type: 'GET',
                url: '/Handler/Default_Get_NestShopNameListByo2o.ashx',
                dataType: 'text',
                data: 'strUser=' + varUserID,
                beforeSend: function () {
                    $("#idGeto2oShop_AddressNeedHandleList").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
                },
                success: function (msg) {
                    $("#idGeto2oShop_AddressNeedHandleList").html(msg);



                    //js获取日期：前天、昨天、今天、明天、后天
                    //昨天："+GetDateStr(-1)
                    //今天："+GetDateStr(0)
                    //明天："+GetDateStr(1)

                    var dd = new Date();
                    dd.setDate(dd.getDate() + 3);//获取AddDayCount天后的日期    
                    var y = dd.getFullYear();
                    var m = dd.getMonth() + 1;//获取当前月份的日期   
                    if (m < 10) {
                        m = "0" + m;
                    }
                    var d = dd.getDate();
                    if (d < 10) {
                        d = "0" + d;
                    }

                    var ddH = dd.getHours();
                    var ddm = dd.getMinutes();
 




                    document.getElementById("ZitiRenDate").value = y + "-" + m + "-" + d;
                    document.getElementById("ZitiRenTime").value = ddH + ":" + ddm;
                },
                error: function (data) {
                    //alert(data);
                }
            });
        }
    }



    function doClickThis_HowToGetProduct(varpHowToGetProduct) {


        var url = varServicesURL + "/Pub/doClickThis_HowToGetProduct.asmx/doSelectThis?strIntUserID=" + varUserID + "&strpHowToGetProduct=" + varpHowToGetProduct;

        var result = -1;
        $.ajax({
            type: "POST",
            url: url,
            dataType: "jsonp",
            jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
            jsonpCallback: "jsonp81Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                ///result = parseInt(json.ErrorCode);
                return;
            },
            error: function () {
            }
        });


        return result;
    }

    function jsonp81Callback() {
            
    }


});