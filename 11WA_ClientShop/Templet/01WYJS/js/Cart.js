function loadBuySelectType_Multi() {
    ShowCartAuto(varUserID, 0, 0);
    autoClickOneKeyQuickPay();/////启用一键快捷支付（客户点击商品页面的立即购买，可快捷弹出微信支付（如果条件具备、譬如收货地址已存在的情况下））
}

function CartAdd(varID_ShopingCart) {
    var f = document.getElementById("valuesCart" + varID_ShopingCart);
    var e = f.value;
    e++;
    document.getElementById("valuesCart" + varID_ShopingCart).value = e;
    ShowCartAuto(varUserID, varID_ShopingCart, e);
}
function CartJian(varID_ShopingCart) {
    var e = document.getElementById("valuesCart" + varID_ShopingCart);
    var d = e.value;
    if (d > 0) {
        d--;
        document.getElementById("valuesCart" + varID_ShopingCart).value = d;
        ShowCartAuto(varUserID, varID_ShopingCart, d)
    }
}


function ShowCartAuto(varUserID, varID_ShopingCart, eGoodCount) {
    //var city = $("#Text1").val();
    $.ajax({
        type: 'GET',
        url: '/Handler/MultiButton_Cart.ashx',
        async: false,
        dataType: 'text',
        data: 'strQureyUserID=' + varUserID + '&strQureyID_ShopingCart=' + varID_ShopingCart + '&strQureyGoodCount=' + eGoodCount,
        beforeSend: function () {
            //$("#WaitPayList").html("");
            //debugger;
            document.body.style.cursor = "wait";

            var varlength = $("#WaitPayList").html().length;
            //alert(varlength);
            //alert($("#WaitPayList").html());

            if (varlength == 0) {
                $("#WaitPayList").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
            }
        },
        success: function (msg) {
            //alert(msg);
            document.body.style.cursor = "default";
            $("#WaitPayList").html(msg);
            if (typeof (varintHowToGetProductDoOnlyAfterloadCart) != 'undefined') {////购物车是空的 这个 不会出现 。
                intInThisChoiceClick(varintHowToGetProductDoOnlyAfterloadCart);
            }
        },
        error: function (data) {
            //alert(data);
        }
    })

}




function to_change() {
    var obj = document.getElementsByName('RadioButtonList_Address');
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked == true) {
            var varXiangXiDiZhiID = obj[i].value;

            changeaddressCart(varUserID, varXiangXiDiZhiID);
            ShowCartAuto(varUserID, 0, 0);
            break;
        }
    }
}



function changeaddressCart(varUserID, varXiangXiDiZhiID) {
    var url = varServicesURL + "/Order/DoOrder.asmx/Change_User_Address?strpub_Int_Session_CurUserID=" + varUserID + "&strXiangXiDiZhiID=" + varXiangXiDiZhiID;

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        async: false,//必须同步等待
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp1Callback98348787", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            //result = parseInt(json.ErrorCode);
            //ShowCartAuto(varUserID, 0, 0);
            return;
        },
        error: function () {
        }
    });
    return result;
}


function checkPayWay() {///支付方式检测
    if (varglobalcheckPayWay == "V3_js_API") {
        payAction();
        return false;
    }
    else if (varglobalcheckPayWay == "Oldjs_APIPay") {
        return true;//继续旧版的支付
    }
    else {
        return true;//继续旧版的支付
    }
}

function payAction() {
    //debugger;
    var varRadioButtonList_Address = null;
    var obj = document.getElementsByName("RadioButtonList_Address")
    for (var i = 0; i < obj.length; i++) { //遍历Radio 
        if (obj[i].checked) {
            varRadioButtonList_Address = obj[i].value;
            break;
        }
    }
    var ZitiRenName = document.getElementById("ZitiRenName").value;
    var ZitiRenMobile = document.getElementById("ZitiRenMobile").value;
    var ZitiRenDate = document.getElementById("ZitiRenDate").value;
    var ZitiRenTime = document.getElementById("ZitiRenTime").value;
    var varGeto2oShop = null;
    var obj = document.getElementsByName("RadioButtonList_dGeto2oShop")
    for (var i = 0; i < obj.length; i++) { //遍历Radio 
        if (obj[i].checked) {
            varGeto2oShop = obj[i].value;
            break;
        }
    }
    var varZiti = "&ZitiRenName=" + ZitiRenName + "&ZitiRenMobile=" + ZitiRenMobile + "&ZitiRenDate=" + ZitiRenDate + "&ZitiRenTime=" + ZitiRenTime + "&varGeto2oShop=" + varGeto2oShop;

    var url = varServicesURL + "/Order/DoOrder.asmx/CartbuyNow?pub_Int_Session_CurUserID=" + varUserID + "&RadioButtonList_Address=" + varRadioButtonList_Address + "&pub_Int_ShopClientID=" + varpub_Int_ShopClientID + varZiti;
    $.ajax({
        url: url,
        async: false,
        dataType: "jsonp",
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        jsonpCallback: "per985675w4son",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        beforeSend: function () {//加载层    // Handle the beforeSend event
            layer.open({
                type: 2,
                content: '正在生成订单',
                time: 6///不知道 是否能加2秒
            });
        },
        error: function (e) {
            alert(e);
        },
        success: function (data) {
            if (data.ErrorCode == "-3") {///请先输入收货地址
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 3,
                    end: function (layer) {
                        self.location = '/cart_self.aspx?paymoney=paymoneymusthaveaddress';
                    }
                });

            }
            else if (data.ErrorCode == "-4") {///输入自取人姓名
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 1
                });
                document.getElementById("ZitiRenName").focus();
                //self.location = '/cart_self.aspx?paymoney=paymoneymusthaveaddress';
            }
            else if ((data.ErrorCode == "-5") || (data.ErrorCode == "-6")) {///输入手机号格式不正确   手机号长度必须是11位
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 1
                });
                document.getElementById("ZitiRenMobile").focus();
            }
            else if (data.ErrorCode == "-7") {///购物车清空   本月已有购买限制  是否已经完全出局 完全出局也不能买  不想更新js  所以 这里 公用一个-7
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 1
                });

            }
            else if (data.ErrorCode == "81") {///都准备好了，可以开始支付
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 1
                });
                doPayAcyion(data.OrderINT, varpub_Int_ShopClientID);
            }
            else if (data.ErrorCode == "82") {///自己的余额  购物券支付，不用付钱
                layer.open({
                    type: 2,
                    content: decodeURIComponent(data.ErrorDescription),
                    time: 2
                });
                self.location = '/cart_good2.aspx';////   自己的余额  购物券支付，不用付钱
            }
        }
    });
}

function doPayAcyion(data_OrderINT, varThispub_Int_ShopClientID) {
    var url = varServicesURL + "/Pub/doWS_GetWeiXinSign.asmx/_GetWeiXinPay?OrderINT=" + data_OrderINT + "&ShopClientID=" + varThispub_Int_ShopClientID;
    $.ajax({
        url: url,
        async: false,
        dataType: "jsonp",
        jsonpCallback: "pe76486781256rson",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        beforeSend: function () {//加载层    // Handle the beforeSend event
            layer.open({
                type: 2,
                content: '正在申请支付',
                time: 4///不知道 是否能加2秒
            });
        },
        error: function (e) {
            alert(e);
        },
        success: function (data) {
            ///debugger;
            wx.ready(function () {
                wx.chooseWXPay({
                    timestamp: data.timestamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                    nonceStr: data.nonceStr, // 支付签名随机串，不长于 32 位
                    package: data.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                    signType: 'MD5', // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                    paySign: data.paySign, // 支付签名
                    cancel: function (res) {
                        layer.open({
                            type: 2,
                            content: "亲,取消支付,你可联系我们或继续支付",
                            time: 2,
                            end: function (layer) {
                                self.location = '/cart_good.aspx';////   到待付款去 用户可以继续支付
                            }
                        });


                    },
                    fail: function (res) {
                        layer.open({
                            type: 2,
                            content: "支付失败,联系商家或继续支付",
                            time: 2,
                            end: function (layer) {
                                self.location = '/cart_good.aspx';////   到待付款去 用户可以继续支付
                            }
                        });
                        //self.location = '/cart_good.aspx';////   到待付款去 用户可以继续支付
                    },
                    success: function (res) {
                        // 支付成功后的回调函数
                        layer.open({
                            type: 2,
                            content: "支付成功",
                            time: 1,
                        });
                        doCheckPayAcyion(data_OrderINT, varThispub_Int_ShopClientID)
                    }
                });
            });

            wx.error(function (res) {
                alert("wx.error" + res.errMsg);
                // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

            });
        }
    });

}


function doCheckPayAcyion(data_OrderINT, varThispub_Int_ShopClientID) {
    //debugger;
    var url = varServicesURL + "/Order/DoOrder.asmx/CheckPayAcyion?OrderINT=" + data_OrderINT + "&ShopClientID=" + varThispub_Int_ShopClientID;
    //alert(url);
    $.ajax({
        url: url,
        async: false,
        dataType: "jsonp",
        jsonpCallback: "pe7648675674856rson",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        beforeSend: function () {//加载层    // Handle the beforeSend event
            layer.open({
                type: 2,
                content: '正在处理订单',
                time: 2///不知道 是否能加2秒
            });
        },
        error: function (e) {
            alert(e);
        },
        success: function (data) {
            if (data.ErrorCode == "0") {///请先输入收货地址
                layer.open({
                    type: 2,
                    content: "订单处理成功,请等待发货",
                    time: 1,
                    end: function (layer) {
                        self.location = '/cart_good2.aspx';
                    }
                });
            }
            else if (data.ErrorCode == "-1") {///请先输入收货地址
                layer.open({
                    type: 2,
                    content: "订单处理失败,可能是网络连接失败",
                    time: 1,
                    end: function (layer) {
                        self.location = '/mywebuy.aspx';
                    }
                });
            }
        }
    });

}