function doLoadMultiDocumnet() {

    try {
        var varGetUseid = getUserID();
        //alert(varGetUseid);
        if (varGetUseid > -1) {
            var varQueryStringList = new QueryString();
            var varZCID = varQueryStringList["zcid"];

            setTimeout("doAjaxLoadZCContent(" + varZCID + ", " + varGetUseid + ");///轮播图  众筹原因  众筹详情 承诺与回报", 3000);

            doAjaxLoadZCSpeedBar(varZCID, varGetUseid);///进度图形
            doAjaxLoadZCIWanttoSupportList(varZCID, varGetUseid);///我要支持
            doShareWeiXin(varZCID);

            doMakeHtml__Pub_03Footer(varGetUseid, "#idPub_03Footer_html_lsTuanGou");

        }
    }
    catch (e)
    { alert('语句异常：' + e.message) }
}


///轮播图 倒计时  众筹原因  众筹详情 承诺与回报
function doAjaxLoadZCContent(varZCID, varGetUseid) {
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    var varURLGameInfo_ZC = varServiceURL + "/Pub/doZC.asmx/doGameInfo_ZC_Content?strZCID=" + varZCID + "&strUseid=" + varGetUseid + "&strShopClientID=" + ShopClientID;
    //alert("varURL=" + varURL);

    var result = -1;
    $.ajax({
        type: "get",
        url: varURLGameInfo_ZC,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201608090722Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        beforeSend: function () {
            $("#Anounce").html('<div class=\"wx_loading_icon\"></div>');
            $("#content_ZCReason").html('<div class=\"wx_loading_icon\"></div>');
            $("#content_GoodInfo").html('<div class=\"wx_loading_icon\"></div>');
            $("#content_ZCPromiseAndReturn").html('<div class=\"wx_loading_icon\"></div>');
        },
        success: function (jsonReturn) {
            result = jsonReturn.ErrorCode;
            var json = jsonReturn.ThisZhongChouGoodInfo;
            if (result == 0) {
                varSourceGoodID = json.SourceGoodID;

                document.title = "众筹" + decodeURIComponent(json.GoodName);
                $("#content_LongInfo").html(decodeURIComponent(json.LongInfo));
                $("#content_ZCReason").html(decodeURIComponent(json.ZCReason));
                $("#content_ZCDescribe").html(decodeURIComponent(json.ZCDescribe));
                $("#content_ZCPromiseAndReturn").html(decodeURIComponent(json.ZCPromiseAndReturn));




                doMakeHtml_AnnouncePic_GoodList(json.SourceGoodID)


            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });


    return result;

}

///众筹进度
function doAjaxLoadZCSpeedBar(varZCID, varGetUseid) {
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    var varURLGameInfo_ZC = varServiceURL + "/Pub/doZC.asmx/doGameInfo_ZC_SpeedBar?strZCID=" + varZCID + "&strUseid=" + varGetUseid + "&strShopClientID=" + ShopClientID;
    //alert("varURL=" + varURL);

    var result = -1;
    $.ajax({
        type: "get",
        url: varURLGameInfo_ZC,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201608181429Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        beforeSend: function () {
            document.getElementById("wxBoxSpeedBar").style.display = "none";
        },
        success: function (jsonReturn) {
            result = jsonReturn.ErrorCode;
            var json = jsonReturn.ThisZhongChouGoodInfo;
            if (result == 0) {
                document.getElementById("wxBoxSpeedBar").style.display = "block";

                var varShowPercent = json.SpeendPercent;
                if (varShowPercent > 100) varShowPercent = 100;
                if (varShowPercent < 10) varShowPercent = 10;///保证百分数可见
                var varWidth = varShowPercent + "%";


                $("#wxBoxSpeedBar .progressBar").width(varWidth);
                //document.getElementsByClassName("wxBoxSpeedBar progressBar").width(varWidth);
                var intDiff = json.doubleMaxLengthSeconds;
                if (intDiff > 0) {
                    var uiidTuanZhangTeam_Countdown = document.getElementById("Countdown");
                    uiidTuanZhangTeam_Countdown.style.display = "block";
                    timer(intDiff);
                } else if (intDiff < 0) {
                    //$("#TuanHowManyPeople").html("本活动已终止");
                    //$("#buyZuTuan").removeAttr("onclick");////移除参团购买事件
                }
                document.getElementById("progressNum").innerHTML = json.SpeendPercent + "%";

                document.getElementById("supportsNum").innerHTML = json.AllPeopleNum + "人";
                document.getElementById("moneyAmount").innerHTML = json.AllSalesMoney + "元";

                var strShowremainDays = "";
                if (json.doubleMaxLengthDay > 0) {
                    strShowremainDays = json.doubleMaxLengthDay + "天";
                }
                else if (json.doubleMaxLengthHour > 0) {
                    strShowremainDays = json.doubleMaxLengthHour + "小时";
                }

                else if (json.doubleMaxLengthMinute > 0) {
                    strShowremainDays = json.doubleMaxLengthMinute + "分钟";
                }
                else if (json.doubleMaxLengthSeconds > 0) {
                    strShowremainDays = json.doubleMaxLengthSeconds + "秒";
                }
                else {
                    strShowremainDays = "已结束";
                }
                document.getElementById("remainDays").innerHTML = strShowremainDays;
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });


    return result;

}



//////我要支持
function doAjaxLoadZCIWanttoSupportList(varZCID, varGetUseid) {
    var ShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');///运行DoGameUserID.aspx得到的
    var varURLGameInfo_ZC = varServiceURL + "/Pub/doZC.asmx/doGameInfo_ZC_IWanttoSupportList?strZCID=" + varZCID + "&strUseid=" + varGetUseid + "&strShopClientID=" + ShopClientID;
    //alert("varURL=" + varURL);

    var result = -1;
    $.ajax({
        type: "get",
        url: varURLGameInfo_ZC,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201608190542Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json;charset=utf-8",//有这条报“请求格式无效：application/json;charset=utf-8。”错误，注释掉就正常
        beforeSend: function () {
            $("#content_BuyList").html('<div class=\"wx_loading_icon\"></div>');
        },
        success: function (jsonReturn) {
            result = jsonReturn.ErrorCode;
            //var jsonThisZhongSupportListInfo = jsonReturn.ThisZhongSupportListInfo;
            if (result == 0) {
                if (jsonReturn.ThisZhongSupportListInfo.length > 0) {
                    $("#content_BuyList").html('');

                    $.each(jsonReturn.ThisZhongSupportListInfo, function (key, value) {
                        $("#content_BuyList").append(addThisLineZCList(value, jsonReturn.GoodICON));
                    });
                }
                else {
                    $("#LoadTuanGouGoodList").append("暂无团购商品");
                }
                //var varShowPercent = json.SpeendPercent;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });


    return result;

}


function addThisLineZCList(ZhongChouSupportInfo, varGoodICON) {
  
    var varOneLine = "";
    varOneLine += "<dl  class=\"dlddStyle\">";
    //varOneLine += "                    <a href=\"/addfunction/02pingtuan/03goods.html?tuangouid=" + TuanGouGoodInfo.TuanGouID + "\">";
    varOneLine += "                        <dt>";
    varOneLine += "                            <div class=\"ddconTitle\">";
    varOneLine += "                                <span class=\"ddconTitleIcon\">";
    varOneLine += "                                  <img src=\"" + varGoodICON + "\">";
    varOneLine += "                               </span>";
    varOneLine += "                                <span class=\"ddconTitleIconDesc\"><span class=\"wrap\"><span class=\"subwrap\"><span class=\"content\">";
    varOneLine += "                                  " + decodeURIComponent(ZhongChouSupportInfo.DescMemo) + "";
    varOneLine += "                               </span></span></span></span>";
    varOneLine += "                            </div>";
    varOneLine += "                        </dt>";

    varOneLine += "                        <dd>";
    varOneLine += "                            <div class=\"ddconContent\">";
    varOneLine += "                                  " + decodeURIComponent(ZhongChouSupportInfo.SalesPricePromiseAndReturn) + "";
    varOneLine += "                            </div>";
    varOneLine += "                        </dd>";

    varOneLine += "                        <dd style=\"display:block;height:40px;height:66px;margin-bottom:4px\">";
    varOneLine += "                            <div class=\"ddClickBuy\">";
    varOneLine += "                                <span class=\"ddconSaySomething\">";
    //varOneLine += "                                 <input type=\"text\"  maxlength=\"250\" placeholder=\"如您支持，请说些什么吧,最多250字.\" style=\"width:75%;margin-top:4px;\"  name=\"usrSaySomething" + ZhongChouSupportInfo.SupportID + "\" id=\"usrSaySomething" + ZhongChouSupportInfo.SupportID + "\">";

    varOneLine += "<textarea name=\"usrSaySomething" + ZhongChouSupportInfo.SupportID + "\" id=\"usrSaySomething" + ZhongChouSupportInfo.SupportID + "\" maxlength=\"250\" placeholder=\"如您支持，请说些什么吧,最多250字.\" style=\"width:75%;margin-top:4px;height:64px;\"></textarea>";
    varOneLine += "                               </span>";
    varOneLine += "                                <span class=\"ddconBuyClick\" onclick=\"BuyAndSupportThis(" + ZhongChouSupportInfo.SupportID + ")\">";
    varOneLine += "                                     <span class=\"Buttontg1\">";
    varOneLine += "                                     " + ZhongChouSupportInfo.SalesPrice + "¥</span><br />";
    varOneLine += "                                     <span class=\"Buttontg2\">";
    varOneLine += "                                     我要支持</span>";
    varOneLine += "                               </span>";
    varOneLine += "                            </div>";
    varOneLine += "                        </dd>";

    varOneLine += "</dl>";

    return varOneLine;

}


function doMakeHtml_AnnouncePic_GoodList(varstrGoodID) {
    //var city = $("#Text1").val();
    varURL = varServiceURL + "/Pub/doClickThis_HowToGetProduct.asmx/doGetProduct_newTmpletAnnouncePic_GoodList";
    $.ajax({
        type: 'GET',
        url: varURL,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        data: 'strGoodID=' + varstrGoodID,
        jsonpCallback: "jsonp201608160357Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#Anounce").append('<img style=\"margin: 0px auto;display:block;\" src="/images/loading.gif"/>');
        },
        success: function (json) {
            //alert(msg);
            result = parseInt(json.ErrorCode);
            if (result == 0) {
                //alert(msg);
                $("#Anounce").html(decodeURIComponent(json.msg));
            };
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    })


}


function doShareWeiXin(varZCID) {
    var varQueryStringList = new QueryString();

    var varGetUseid = getUserID();
    var varParentID = 0;
    var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
    //alert("varDB_ParentID11=" + varDB_ParentID);
    if ((varDB_ParentID != null) && (varDB_ParentID != undefined) && (varDB_ParentID != '')) {
        //alert("varDB_ParentID1=" + varDB_ParentID);
        varParentID = varDB_ParentID;
    }
    else {
        var varparentagentid = varQueryStringList["parentagentid"];////转发ID
        var varparentagentadid = varQueryStringList["parentagentadid"];////转发ID
        if ((varparentagentadid != null) && (varparentagentadid = undefined)) {
            varParentID = varparentagentadid;
            //alert("varDB_ParentID2=" + varDB_ParentID);
        }
        else if ((varparentagentid != null) && (varparentagentid = undefined)) {
            varParentID = varparentagentid;
            //alert("varDB_ParentID3=" + varDB_ParentID);
        }
        //alert("varDB_ParentID4=" + varDB_ParentID);
    }
    //var varTuanGouIDNumber = varQueryStringList["TuanGouIDNumber"];////没有就是 没有 没有的话只能发起
    //varTuanGouIDNumber=

    var varMasterUserID = varQueryStringList["masteruserid"];////没有就是 没有 没有的话只能发起
    var host = window.location.host;
    var varJURL = "https://" + host + "/addfunction/04ZC_project/03ZC.html?zcid=" + varQueryStringList["zcid"];
    varJURL = varJURL + "&parentagentid=" + varParentID ;
    
    var varGetShopClientName = localStorage.getItem('GetShopClientName201709121928_Open_0609');
    var varimg = localStorage.getItem('GetUser_MyDisk_HeadImage201709121928_Open_0609');
    var varUserNickName = localStorage.getItem('GetUserNickName201709121928_Open_0609');

    var vardesc = varGetShopClientName + "微众筹活动." + varUserNickName + ".微云基石微众筹。";
    var varShopClientID = localStorage.getItem('GetShopClientID201709121928_Open_0609');
    //alert(varJURL);
    do_GetAjaxShareWeiXin(varShopClientID, varJURL, "微众筹" + " " + document.title + varGetShopClientName, vardesc, varimg, ShareShopFunction);
}

function BuyAndSupportThis(varbuySingleSupportID) {
    var varQueryStringList = new QueryString();
    var varZCID = varQueryStringList["zcid"];
    layer.open({
        type: 2,
        content: "请稍等,正在为您准备购物车",
        time: 3
    });

    var varDB_ParentID = localStorage.getItem('GetPub_Link_ParentID201709121928_Open_0609');
    var varGetURLBuy = "";
    varGetURLBuy += "UserID=" + varUserID;
    varGetURLBuy += "&goodid=" + varSourceGoodID;
    varGetURLBuy += "&parentid=" + varDB_ParentID;
    varGetURLBuy += "&buycount=" + 1;
    varGetURLBuy += "&ZCID=" + varZCID;
    varGetURLBuy += "&multibuytype=3";///众筹订单
    varGetURLBuy += "&SupportID=" + varbuySingleSupportID;///

    var varusrSaySomething_SingleSupportID = "usrSaySomething" + varbuySingleSupportID;
    var xusrSay = document.getElementById(varusrSaySomething_SingleSupportID).value;///这个x的值就是获取到的内容
    varGetURLBuy += "&usrSay=" + encodeURI(encodeURI(xusrSay));///

    //众筹订购买的
    var url = varServiceURL + "/Order/DoZhongChou.asmx/_Service_AddToCart_ZC?" + varGetURLBuy; //众筹订购买的
    var result = -1;
    $.ajax({
        type: "get",
        url: url,
        dataType: "jsonp", async: false,
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201608211535Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = parseInt(json.ErrorCode);
            if (result == -1) {
                layer.open({
                    type: 2,
                    content: "购物车添加失败！",
                    time: 1
                });
            }
            else if (result == -22) {
                layer.open({
                    type: 2,
                    content: "购物车添加失败！购买限制，在订单中已存在！",
                    time: 2,
                    end: function (layer) {
                        self.location = '/cart_good.aspx';////转到订单表
                    }
                });
            }
            else if (result == -44) {
                layer.open({
                    type: 2,
                    content: "购物车添加失败！库存不足！",
                    time: 2,
                    end: function (layer) {
                        self.location = '/cart_good.aspx';////转到订单表
                    }
                });
            }
            else if (result == -23) {
                layer.open({
                    type: 2,
                    content: "购物车添加失败！购买限制，在购物车中已存在！",
                    time: 2,
                    end: function (layer) {
                        self.location = '/cart.aspx';////转到购物车
                    }
                });
            }
            else if (result == 1) {
                self.location = '/cart.aspx';////   转入购物车
            }
            //self.location = '/cart.aspx';////   转入购物车
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
    });

}



///倒计时
function timer(intDiff) {
    if (intDiff > 0) {
        window.setInterval(function () {
            var day = 0,
                hour = 0,
                minute = 0,
                second = 0;//时间默认值
            if (intDiff > 0) {
                day = Math.floor(intDiff / (60 * 60 * 24));
                hour = Math.floor(intDiff / (60 * 60)) - (day * 24);
                minute = Math.floor(intDiff / 60) - (day * 24 * 60) - (hour * 60);
                second = Math.floor(intDiff) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
            }
            if (minute <= 9) minute = '0' + minute;
            if (second <= 9) second = '0' + second;
            $('#day_show').html(day);
            $('#hour_show').html('<s id="h"></s>' + hour);
            $('#minute_show').html('<s></s>' + minute);
            $('#second_show').html('<s></s>' + second);
            intDiff--;
        }, 1000);

        console.log("用于输出普通信息intDiff=" + intDiff);
        //console.info("用于输出提示性信息");
        //console.error("用于输出错误信息");
        //console.warn("用于输出警示信息");
    }
}



$(".ShowTitleInfo").click(function () {
    open__win("5555");
});

function open__win(varShow) {

    layer.open({
        content: varShow,
        btn: '我知道了'
    });
}

