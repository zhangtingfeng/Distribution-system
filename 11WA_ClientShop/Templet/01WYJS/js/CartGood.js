function payActionCartGood(data_OrderINT, varpub_Int_ShopClientID) {

    var url = varServicesURL + "/Pub/doWS_GetWeiXinSign.asmx/_GetWeiXinPay?OrderINT=" + data_OrderINT + "&ShopClientID=" + varpub_Int_ShopClientID;
    $.ajax({
        url: url,
        dataType: "jsonp",
        jsonpCallback: "person",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        beforeSend: function () {//加载层    // Handle the beforeSend event
            layer.open({
                type: 2,
                content: '正在为你做支付准备',
                time: 1///不知道 是否能加2秒
            });
        },
        success: function (data) {
            ///v3  pay

            wx.ready(function () {
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
                            type: 2,
                            time: 1
                        });
                        self.location = '/cart_good.aspx';////   到待付款去 用户可以继续支付

                    },
                    fail: function (res) {
                        layer.open({
                            content: "支付失败",
                            type: 2,
                            time: 1
                        });
                        self.location = '/cart_good.aspx';////   到待付款去 用户可以继续支付
                    },
                    success: function (res) {
                        // 支付成功后的回调函数
                        layer.open({
                            content: "支付成功",
                            type: 2,
                            time: 1
                        });
                        doCheckPayAcyion(data_OrderINT, varpub_Int_ShopClientID);
                     }
                });

            });

            wx.error(function (res) {
                alert(res.errMsg);
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
                    time: 2,
                    end: function (layer) {
                        self.location = '/cart_good2.aspx';
                    }
                });
            }
            else if (data.ErrorCode == "-1") {///请先输入收货地址
                layer.open({
                    type: 2,
                    content: "订单处理失败,可能是网络连接失败",
                    time: 2,
                    end: function (layer) {
                        self.location = '/mywebuy.aspx';
                    }
                });
            }
        }
    });

}

//页面加载完成有两种事件，一是ready，表示文档结构已经加载完成（不包含图片等非文字媒体文件），二是onload，指示页 面包含图片等文件在内的所有元素都加载完成。(可以说：ready 在onload 前加载！！！)
//我的理解： 一般样式控制的，比如图片大小控制放在onload 里面加载;
//而：jS事件触发的方法，可以在ready 里面加载;
function loadBuySelectType_Multi() {
    doCheckPayAcyion_WeiKanJia();

}

function doCheckPayAcyion_WeiKanJia() {
    var varQueryStringList = new QueryString();
    var vartype = varQueryStringList["type"];
    var buythisorderid = varQueryStringList["buythisorderid"];
    if (vartype == "weikanjiafirstorderpay") {
        ///alert(buythisorderid);
        buyThis(buythisorderid);//////微砍价需要支付的事情   闭环完成  就是 需要测试聊
    }
}