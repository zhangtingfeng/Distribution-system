function RegisterNow() {
    var varQueryStringList = new QueryString();
    var varLocalStorgeCallbackURL = varQueryStringList["LocalStorgeCallbackURL"];

    window.location.href = "/User/Register.aspx" + "?LocalStorgeCallbackURL=" + varLocalStorgeCallbackURL;
}

function btnLoginNow() {
    if (!checkPhone('uid')) {
        alert("手机号码有误，请重填"); return;
    }
    if (isNull(document.getElementById('upwd').value)) {
        alert("密码有误，请重填"); return;
    }
    LoginMSGCode()

}


function LoginMSGCode() {
    var varuserMobilePhone = document.getElementById('uid').value;
    var varuserpwd = document.getElementById('upwd').value;

    var url = varGetAppConfiugServicesURL + "/Other/CheckCode/WSCheck.asmx/doGameInfo_CheckPhonePWD_Login";
    url += "&PhoneNum=" + varuserMobilePhone
    url += "&userpwd=" + varuserpwd
    url += "&strShopClientID=" + varpub_Int_ShopClientID;
    url += "&strUserID=" + varpub_Int_Session_CurUserID;


    layer.open({ content: '正在验证账户', type: 2, time: 1 });


    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp20161229Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            var result = json.ErrorCode;
            if (result == 1) {
                //提示
                layer.open({
                    type: 2
                    , content: '登陆成功', time: 2 //2秒后自动关闭
                });
                var varQueryStringList = new QueryString();
                var varLocalStorgeCallbackURL = varQueryStringList["LocalStorgeCallbackURL"];
                if (isNull(varLocalStorgeCallbackURL) == false) {
                    window.location.href = varLocalStorgeCallbackURL;////跳回
                }
                else {
                    window.location.href = "/mywebuy.aspx";
                }
            }
            else if (result == 2) {
                //提示
                layer.open({
                    type: 2
                    , content: '手机号码已被其他微信号绑定.本平台不支持一个手机多个微信号', time: 2 //2秒后自动关闭
                });
            }
            else if (result == -2) {
                //提示
                layer.open({
                    type: 2
                    , content: '密码错误，请重新录入，也可找回或者重置密码', time: 2 //2秒后自动关闭
                });
            }
            else if (result == -3) {
                layer.open({
                    type: 2, content: '手机账户不存在'
                  , time: 2 //2秒后自动关闭
                });
            }
            return;
        },
        error: function () {
        }
    });
}


function checkPhone(varID) {
    var phone = document.getElementById(varID).value;
    if (!(/^1[34578]\d{9}$/.test(phone))) {

        return false;
    }
    return true;
}


function isNull(arg1) {
    return !arg1 && arg1 !== 0 && typeof arg1 !== "boolean" ? true : false;
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
