function ChangeMoney(varARGClickButton) {
    varClickButtonNum = varARGClickButton;///记录 单击了 哪一个 兑换按钮  分享后 做相应运算
    //AfterShareContinuesAskChangeMoney();

    layer.open({
        type: 2,
        content: "分享朋友圈,进行兑换",
        time: 2,
        end: function (layer) {
            $("#mcoverChangeMoney").css("display", "block");  // 分享给好友圈按钮触动函数
        }
    });
}

//分享之后的继续调用
function AfterShareContinuesAskChangeMoney() {
    //debugger;
    //alert(varClickButtonNum);
    ShareShopFunction();///每日分享商铺的奖励事件 异步回调
    weChat();
    ChangeMoneyFunction(varClickButtonNum);
}

function weChat() {
    $("#mcoverChangeMoney").css("display", "none");  // 点击弹出层，弹出层消失
}



function ChangeMoneyFunction(varClickButtonNum) {///兑换事件  
    var url = varServiceURL + "/Pub/doVisiRedBag.asmx/_ChangeMoney?strUserID=" + varUserID + "&strShopClientID=" + varShopClientID + "&GouWuQuan2XianJInEtcID=" + varClickButtonNum;

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201601070640Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = parseInt(json.ErrorCode);

            if (result == -1) {
                layer.open({

                    content: "通讯错误",
                    time: 2
                });
            }
            else if (result == -2) {
                layer.open({

                    content: "余额不足 无权兑现",
                    time: 2
                });
            }
            else if (result == -3) {
                layer.open({

                    content: "未关注者不能兑换",
                    time: 2
                });
            }
            else if (result == 2) {
                layer.open({

                    content: "兑现成功",
                    time: 2
                });
            }
            else if (result == 3) {
                layer.open({
                    content: "申请成功,商城将24小时内处理",
                    time: 2
                });
            }
            return;
        },
        error: function () {
        }
    });


    return result;
}
