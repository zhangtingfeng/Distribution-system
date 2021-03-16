
//写cookies函数 作者：翟振凯
function SetCookie(name, value)//两个参数，一个是cookie的名子，一个是值
{
    var Days = 30; //此 cookie 将被保存 30 天
    var exp = new Date();    //new Date("December 31, 9998");
    //exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    exp.setTime(exp.getTime() + 60000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
function getCookie(name)//取cookies函数
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;

}
function delCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}

function getUserID()//删除cookie
{
    //debugger;

    var varGetUseid = localStorage.getItem('CurUserID201709121928_Open_0609');

    var vargetUserID = parseInt(varGetUseid); //returns 1234
    if (isNaN(vargetUserID)) {
        var myvarparentagentadid = GameRequestString('parentagentadid');
        var varINTparentagentadid = parseInt(myvarparentagentadid); //
        var myvarparentagentid = GameRequestString('parentagentid');
        var varINTparentagentid = parseInt(myvarparentagentid); //

        var varLinkURL = "/game/dogameuserid.aspx?";
        if (!isNaN(varINTparentagentadid)) {
            varLinkURL += "parentagentadid=" + varINTparentagentadid;
        }
        else if (!isNaN(varINTparentagentid)) {
            varLinkURL += "parentagentid=" + varINTparentagentid;
        }

        var gamehref = window.location.href;//获取页面完整地址
        var gamehost = "https://" + window.location.host;//获取域名
        var gamecallbackurl = gamehref.substring(gamehost.length, gamehref.length);
        gamecallbackurl = encodeURIComponent(encodeURIComponent(gamecallbackurl));
        //debugger;
        var varJumpURL = varLinkURL + "&gamecallbackurl=" + gamecallbackurl;
        //alert("GameUserID.js window.location.href=" + varJumpURL);
        window.location.href = varJumpURL;
        return -1;
    } else {
        return vargetUserID;
    }
}



function GameRequestString(name) {
    new RegExp("(^|&)" + name + "=([^&]*)").exec(window.location.search.substr(1));
    return RegExp.$2
}
//alert(RequestString("id"));



function do_GetAjaxShareWeiXin(ShopClientID, arghttpURL, WeiXin_shareAppAllPageTitle, WeiXin_descAppPageContent, WeiXin_imgAllPageUrl, ShareShopFunction) {
    WeiXin_shareAppAllPageTitle = WeiXin_shareAppAllPageTitle.replace(/null/g, "");
    WeiXin_descAppPageContent = WeiXin_descAppPageContent.replace(/null/g, "");

    var url = varServiceURL + "/Pub/doWS_GetWeiXinSign.asmx/_GetWeiXinSign";
    //var url = "http://localhost/14WcfS/Pub/doWS_GetWeiXinSign.asmx/_GetWeiXinSign";

    var pathNeedSign = window.location.href;
    //pathNeedSign = pathNeedSign.replace(new RegExp(":", "gm"), "("); //g全局('', '*');
    //pathNeedSign = pathNeedSign.replace("?", ")"); //g全局('', '*');
    //pathNeedSign = pathNeedSign.replace(new RegExp("&", "gm"), "*"); //g全局('', '*');
    //pathNeedSign = pathNeedSign.replace(new RegExp("/", "gm"), "@"); //g全局('', '*');//varPath.Replace('/', '@');///他自己有许多&  ，将来替换回来
    //pathNeedSign = pathNeedSign.replace(new RegExp("=", "gm"), "^"); //g全局('', '*');//varPath.Replace('=', '^');
    pathNeedSign = encodeURIComponent(pathNeedSign);

    url += "?httpURL=" + pathNeedSign;
    url += "&ShopClientID=" + ShopClientID;


    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp145Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = parseInt(json.ErrorCode);
            if (result == 0) {


                wx.config({
                    debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                    appId: json.appId, // 'wxb97da79b8bad5e74',
                    timestamp: json.timestamp, //1439108495,
                    nonceStr: json.nonceStr, //'E07413354875BE01A996DC560274708E',
                    signature: json.signature, //'42a6b5d303034aff11de80c9e67802258b7ab4dc',
                    jsApiList: [
                        'onMenuShareTimeline', 'getLatestAddress', 'editAddress',
                'onMenuShareAppMessage', 'chooseWXPay'
                    ]
                });

                wx.ready(function () {
                    // 在这里调用 API
                    var shareData = {
                        title: WeiXin_shareAppAllPageTitle, ///这些变量 调用的地方有
                        desc: WeiXin_descAppPageContent,
                        link: arghttpURL,
                        imgUrl: WeiXin_imgAllPageUrl
                    };
                    wx.onMenuShareAppMessage(shareData);

                    if (!ShareShopFunction) {///有回掉事件
                        wx.onMenuShareTimeline({
                            title: WeiXin_shareAppAllPageTitle,
                            desc: WeiXin_descAppPageContent,
                            link: arghttpURL,
                            imgUrl: WeiXin_imgAllPageUrl,
                            success: function () { ShareShopFunction },
                            cancel: function () { }
                        });
                    }
                    else {
                        wx.onMenuShareTimeline(shareData);
                    }
                });
                wx.error(function (res) {
                    alert(res.errMsg);
                });
            }
            return;
        },
        error: function () {
        }
    });
    return result;
}

function QueryString() {
    var name, value, i;
    var str = location.search;
    var num = str.indexOf("?")
    str = str.substr(num + 1);
    var arrtmp = str.split("&");
    for (i = 0; i < arrtmp.length; i++) {
        num = arrtmp[i].indexOf("=");
        if (num > 0) {
            name = arrtmp[i].substring(0, num);
            value = arrtmp[i].substr(num + 1);
            this[name] = value;
        }
    }
}


function SendMoneyAddAsync(intpInt_Session_CurUserID, ShopClientID, intHowMany, gameid) {
    var url = "https://Service.eggsoft.cn/User/Game_Vouchers.asmx/_GameSend_VouchersSave";
    //var url = "http://localhost/14WcfS/User/Game_Vouchers.asmx/_GameSend_VouchersSave";



    url += "?UserID=" + intpInt_Session_CurUserID;
    url += "&ShopClientID=" + ShopClientID;
    url += "&gameid=" + gameid;
    url += "&BoolConsumeOrRecharge=" + "true";
    url += "&intHowMany=" + intHowMany;


    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp1Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = parseInt(json.ErrorCode);
            if (result == 0) {
                if (confirm(json.OkDesc)) {
                    var varJumpURL = json.JumpURL;
                    window.location = (varJumpURL);
                }
                else {
                }
            }
            return;
        },
        error: function () {
        }
    });
    return result;
}


function doVisitThisPageName(varGameInffo, varparentID, varGetUseid, varGetShopClientID) {
    //var url = "http://localhost/14WcfS/Pub/doVisitGames.asmx/dodoVisitGameAction";
    var url = "https://Service.eggsoft.cn/Pub/doVisitGames.asmx/dodoVisitGameAction";

    url += "?strpGameInfo=" + escape(varGameInffo) + "&strpInt_QueryString_ParentID=" + varparentID + "&strpub_Int_Session_CurUserID=" + varGetUseid + "&strpub_Int_ShopClientID=" + varGetShopClientID + "";

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp1Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = parseInt(json.ErrorCode);
            return;
        },
        error: function () {
        }
    });
    return result;
}