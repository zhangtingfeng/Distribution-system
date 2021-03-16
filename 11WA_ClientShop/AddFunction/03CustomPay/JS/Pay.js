//var tNumberInput = document.getElementById("NumberInput");
//tNumberInput.onclick = function tst() {
//    alert('999');
//}

function load_Multi() {
    var varReturn = getUserID();
    if (varReturn > -1) {
        do_GetGameInfo();
        doShareWeiXin();
    }
}

function do_GetGameInfo() {
    var url = varServiceURL + "/Order/DoSelf_51.asmx/_GetGameInfo";
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的

    var Request = new QueryString();
    var version = Request["version"];
    //alert(version);
    url += "?ShopClientID=" + ShopClientID;
    //alert("do_GetGameInfo");
    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        async: false,//如果想同步 async设置为false就可以（默认是true）
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp148566Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = parseInt(json.ErrorCode);
            if (result == 0) {
                //  varThisGame = json.GameName;
                document.title = decodeURIComponent(json.ShopName) + document.title;
                document.getElementById("ShopclientICON").src = json.ShopLogoImage;
                document.getElementById("ShopclientICON").style.display = "block";
                //document.getElementById("ShopLogoImage").src = json.ShopLogoImage;
            }

            //alert(varThisGame);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });



    return result;
}


$(function () {
    //var ul = document.getElementById('NumberInput');
    //var lis = ul.getElementsByTagName('li');
    //for (var i = 0; i < lis.length; i++) {
    //    lis[i].onclick = function () {
    //        alert(this.innerHTML);
    //    }
    //}

    $("#NumberInput li").click(function () {
        $("#cashier-price").html($("#cashier-price").html() + this.innerHTML);

    });

    $(".btn-del").click(function () {
        var varcashierPrice = $("#cashier-price").html();
        if (varcashierPrice.length > 0) {
            varcashierPrice = varcashierPrice.substring(0, varcashierPrice.length - 1);

        }
        $("#cashier-price").html(varcashierPrice);
    });

    $(".js-ok").click(function () {

        payMoney();
    });

    $(".btn-pay").click(function () {

        payMoney();
    });
});


function payMoney() {
    var varMoney = $("#cashier-price").html();
    var varTrueOrFalse = isPriceNumber(varMoney);
    if (varTrueOrFalse == false) {
        layer.open({
            type: 4,
            content: "&#x91D1;&#x989D;&#x8F93;&#x5165;&#x9519;&#x8BEF;",///中文转UTF8  http://tool.chinaz.com/tools/utf-8.aspx
            time: 3
        });
        return false;
    }
    ASKPayAsync_51(varMoney)
}

/* 自助支付 特殊商品 1533编号吧 */
function ASKPayAsync_51(GoodPrice) {
    var varGetUseid = getUserID();
    var varQueryStringList = new QueryString();
    var str_PayGoodID = '1X5X3X3X';
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的

    varURL = varServiceURL + "/Order/DoSelf_51.asmx/_Service_AddToCart_51PaySelf?str_PayGoodID=" + str_PayGoodID + "&strUserID=" + varGetUseid + "&ShopClientID=" + ShopClientID + "&GoodPrice=" + GoodPrice;

    //alert(varURL);
    var result = -1;
    $.ajax({
        type: "get",
        url: varURL,
        dataType: "jsonp", async: false,
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201606250744Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        beforeSend: function () { //加载条
            layer.open({ type: 2, time: 2 });
        },
        success: function (data) {

            if (data.ErrorCode == 82) {///
                layer.open({
                    type: 2,
                    content: "自助付款商品编号不能为0" + decodeURIComponent(data.ErrorDescription),
                    time: 4
                });
            }
            else if (data.ErrorCode == 81) {///都准备好了，可以开始支付
                layer.open({
                    type: 2,
                    content: "正在申请微信支付,支付成功后请展示您的支付凭证给相关工作人员" + decodeURIComponent(data.ErrorDescription),
                    time: 1
                });
                payActionCartGood(data.OrderINT, ShopClientID);
            }
            else if (data.ErrorCode == -44) {
                layer.open({
                    type: 2,
                    content: "购物车添加失败！库存不足！",
                    time: 1
                });
            }
            return;

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });


    return result;
}


function payActionCartGood(data_OrderINT, ShopClientID) {

    var url = varServiceURL + "/Pub/doWS_GetWeiXinSign.asmx/_GetWeiXinPay?OrderINT=" + data_OrderINT + "&ShopClientID=" + ShopClientID;
    //alert("payActionCartGood=" + url);
    $.ajax({
        url: url,
        dataType: "jsonp",
        jsonpCallback: "person",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        beforeSend: function () {//加载层    // Handle the beforeSend event
            layer.open({
                type: 2,
                content: '正在为你做支付准备',
                time: 4///不知道 是否能加2秒
            });
        },
        success: function (data) {
            ///v3  pay



            wx.ready(function () {
                //alert("data.timestamp" + data.timestamp);
                wx.chooseWXPay({
                    debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                    timestamp: data.timestamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                    nonceStr: data.nonceStr, // 支付签名随机串，不长于 32 位
                    package: data.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                    signType: 'MD5', // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                    paySign: data.paySign, // 支付签名
                    cancel: function (res) {
                        layer.open({
                            content: "取消支付",
                            type: 4,
                            time: 1
                        });
                        //self.location = '/cart_good.aspx';////   到待付款去 用户可以继续支付

                    },
                    fail: function (res) {
                        alert(res.errMsg);
                        alert(res.err_msg);

                        for (i in res) {
                            alert(i);           //获得属性 
                            alert(test[i]);  //获得属性值
                        }
                        layer.open({
                            content: "支付失败",
                            type: 4,
                            time: 1
                        });
                        //self.location = '/cart_good.aspx';////   到待付款去 用户可以继续支付
                    },
                    success: function (res) {
                        // 支付成功后的回调函数
                        layer.open({
                            content: "支付成功,请展示您的支付凭证给相关工作人员",
                            type: 2,
                            time: 5
                        });
                        //alert("data_OrderINT  ShopClientID=" + data_OrderINT + "   " + ShopClientID);
                        doCheckPayAcyion_SelfGetMoney(data_OrderINT, ShopClientID);
                        //self.location = '/v3pay_weixin/CheckIfGetWinXinMoney.aspx?OrderNum=' + out_trade_no;////   到这里去检测 微信支付发送给我们的数据。。更新数据库的状态
                    }
                });

            });

            wx.error(function (res) {
                alert(res.errMsg);
            });
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });
}


/*自助得到money  自助发货处理*/
function doCheckPayAcyion_SelfGetMoney(data_OrderINT, varThispub_Int_ShopClientID) {
    var url_SelfGetMoney = varServiceURL + "/Order/DoOrder.asmx/CheckPayAcyion?OrderINT=" + data_OrderINT + "&ShopClientID=" + varThispub_Int_ShopClientID;
    //alert("url_SelfGetMoney=" + url_SelfGetMoney);
    $.ajax({
        url: url_SelfGetMoney,
        async: false,
        dataType: "jsonp",
        jsonpCallback: "pe201607172003rson",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        beforeSend: function () {//加载层    // Handle the beforeSend event
            layer.open({
                type: 2,
                content: '正在处理订单',
                time: 1///不知道 是否能加2秒
            });
        },
        error: function (e) {
            alert(e);
        },
        success: function (data) {
            //alert("data.ErrorCode == \"0\"");
            if (data.ErrorCode == "0") {///请先输入收货地址
                doCheckPayAcyion_SelfGetMoney_AutoFaHuo(data_OrderINT, varThispub_Int_ShopClientID)
            }
        }
    });

}



/*自助得到money  自助发货处理*/
function doCheckPayAcyion_SelfGetMoney_AutoFaHuo(data_OrderINT, varThispub_Int_ShopClientID) {
    varURL = varServiceURL + "/Order/DoSelf_51.asmx/_Service_AddToCart_51PaySelf_AutoFaHuo?OrderINT=" + data_OrderINT + "&ShopClientID=" + varThispub_Int_ShopClientID;
    //alert("varURL == \"" + varURL + "\"");
    //var url = varServicesURL + "/Order/DoOrder.asmx/CheckPayAcyion?OrderINT=" + data_OrderINT + "&ShopClientID=" + varThispub_Int_ShopClientID;
    $.ajax({
        url: varURL,
        async: false,
        dataType: "jsonp",
        jsonpCallback: "pe201607172022rson",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        beforeSend: function () {//加载层    // Handle the beforeSend event
            layer.open({
                type: 2,
                content: '正在处理订单',
                time: 1///不知道 是否能加2秒
            });
        },
        error: function (e) {
            alert(e);
        },
        success: function (data) {

            if (data.ErrorCode == 0) {///
                layer.open({
                    type: 2,
                    content: "订单处理成功,请展示您的支付凭证给工作人员",//+ decodeURIComponent(data.ErrorDescription),
                    time: 4
                });
            }
            else if (data.ErrorCode == -44) {
                layer.open({
                    type: 2,
                    content: "购物车添加失败！库存不足！",
                    time: 1
                });
            }
        }
    });

}


function doShareWeiXin() {
    var varGetUseid = getUserID();
    var varParentID = 0;
    var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
    if ((varDB_ParentID != null) && (varDB_ParentID != undefined) && (varDB_ParentID != '')) {
        varParentID = varDB_ParentID;
    }
    else {
        var varparentagentid = varQueryStringList["parentagentid"];////转发ID
        var varparentagentadid = varQueryStringList["parentagentadid"];////转发ID
        if ((varparentagentadid != null) && (varparentagentadid = undefined)) {
            varParentID = varparentagentadid;
        }
        else if ((varparentagentid != null) && (varparentagentid = undefined)) {
            varParentID = varparentagentid;
        }
    }
    var host = window.location.host;
    //var varMasterUserID = varQueryStringList["masteruserid"];////没有就是 没有 没有的话只能发起
    var varJURL = "https://" + host + "/03custompay.aspx?1=1";
    varJURL = varJURL + "&parentagentid=" + varParentID;

    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
    var varimg = localStorage.getItem('GetUser_MyDisk_HeadImage201709121928_Open_0609');
    var varUserNickName = localStorage.getItem('GetUserNickName201709121928_Open_0609');

    var vardesc = varGetShopClientName + "自助支付活动." + varUserNickName;
    var varShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');
    //alert("varJURL=" + varJURL);
    //alert("varShopClientID=" + varShopClientID);


    do_GetAjaxShareWeiXin(varShopClientID, varJURL, document.title, vardesc, varimg, ShareShopFunction);
}



function isPriceNumber(_keyword) {
    if (_keyword == "0" || _keyword == "0." || _keyword == "0.0" || _keyword == "0.00") {
        _keyword = "0"; return true;
    } else {
        var index = _keyword.indexOf("0");
        var length = _keyword.length;
        if (index == 0 && length > 1) {//0寮€澶寸殑鏁板瓧涓?/
            var reg = /^[0]{1}[.]{1}[0-9]{1,2}$/;
            if (!reg.test(_keyword)) {
                return false;
            } else {
                return true;
            }
        }
        else {/*闈?寮€澶寸殑鏁板瓧*/
            var reg = /^[1-9]{1}[0-9]{0,10}[.]{0,1}[0-9]{0,2}$/;
            if (!reg.test(_keyword)) {
                return false;
            } else {
                return true;
            }
        }
        return false;
    }
}