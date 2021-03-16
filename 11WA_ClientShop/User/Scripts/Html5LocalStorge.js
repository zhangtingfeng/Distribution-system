function JSuserAgent() {
    //return;

    //alert("isWeiXin=" + isWeiXin());

    var varuserAgent = window.navigator.userAgent;
    //alert("varuserAgent=" + varuserAgent);
    var isChrome = window.navigator.userAgent.indexOf("Chrome") !== -1
    //alert("isChrome=" + isChrome);
}


function isWeiXin() {
    /*
    你好，乐意为你解答此题：
如何判断微信内置浏览器，首先需要获取微信内置浏览器的User Agent，经过在 iPhone 上微信的浏览器的检测，它的 User Agent 是：
Mozilla/5.0 (iPhone; CPU iPhone OS 6_1_3 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Mobile/10B329 MicroMessenger/5.0.1
andriod上检测
Mozilla/5.0 (linux;u; android 4.0.3;zh-cn;htc t328w build/iml74k)applewebkit/534.30(kmtml,like gecko)version/4.0 mobile safari/534.30 MicroMessenger/5.0.3.354
所以通过识别 MicroMessenger 这个关键字来确定是否微信内置的浏览器了。
    */


    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == 'micromessenger') {
        return true;
    } else {
        return false;
    }
}


function isAPPCan() {
    /*
    非常感谢你的回答，2和3解答了我的疑惑，谢谢！

对于useragent，我通过在线打包得到了这样的useragent信息：AppleWebKit/537.36 (KHTML, ** Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36 Appcan/3.1 

其中有个appcan/3.1字样，不知道这个能否添加用户自己设置的字符串和版本号？
    */


    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/Appcan/i) == 'appcan') {
        return true;
    } else if (ua.match(/firefox/i) == 'firefox') {
        return true;
    } else {
        return false;
    }
}



function isAlipay_TianMao_TaoBao_() {
    /*
    非常感谢你的回答，2和3解答了我的疑惑，谢谢！

对于useragent，我通过在线打包得到了这样的useragent信息：AppleWebKit/537.36 (KHTML, ** Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36 Appcan/3.1 

其中有个appcan/3.1字样，不知道这个能否添加用户自己设置的字符串和版本号？
    */


    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/AlipayClient/i) == 'alipayclient') {//支付宝
        return true;
    } else if (ua.match(/AliApp\(TM/i) == 'aliapp\(tm') {////天猫
        return true;
    } else if (ua.match(/AliApp\(TB/i) == 'aliapp\(tb') {////天猫
        return true;
    } else {
        return false;
    }
}