function LoginNow() {
    var varQueryStringList = new QueryString();
    var varLocalStorgeCallbackURL = varQueryStringList["LocalStorgeCallbackURL"];
    window.location.href = "/User/AppLogin.aspx" + "?LocalStorgeCallbackURL=" + varLocalStorgeCallbackURL;
}




var countdown = 120;
function settime(obj) {
    if (!checkPhone('userMobilePhone')) {

        layer.open({
            content: '手机号码有误，请重填'
                 , time: 2 //2秒后自动关闭
        });
        //alert("手机号码有误，请重填"); 
        return;
    }
    if (countdown == 0) {

        obj.removeAttribute("disabled");
        obj.value = "免费获取验证码";
        countdown = 120;
        return;
    } else {
        if (countdown == 120) {
            SendMSGCode(document.getElementById('userMobilePhone').value);
        }
        obj.setAttribute("disabled", true);
        obj.value = "重新发送(" + countdown + ")";
        countdown--;
    }
    setTimeout(function () {
        settime(obj)
    }
        , 1000);

}

function SendMSGCode(varuserMobilePhone) {
    var url = varGetAppConfiugServicesURL + "/Other/CheckCode/WSCheck.asmx/doGameInfo_SendPhoneCode?PhoneNum=" + varuserMobilePhone + "&strShopClientID=" + varpub_Int_ShopClientID;

    layer.open({ content: '正在发送验证码,3小时以内都有效.请勿转告他人', type: 2, time: 3 });


    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp120161006Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            var result = parseInt(json.ErrorCode);
            if (result == 1) {
                localStorage.setItem("ShouldCheckCode", json.SendCheckCode);
                //document.getElementById('ShouldCheckCode').value=json.SendCheckCode;
                //提示
                layer.open({
                    content: '验证码发送成功,请检查短消息,3小时以内都有效.请勿转告他人'
                  , skin: 'msg'
                  , time: 2 //2秒后自动关闭
                });
            }
            else if (result == 2) {
                localStorage.setItem("ShouldCheckCode", json.SendCheckCode);
                //document.getElementById('ShouldCheckCode').value=json.SendCheckCode;
                //提示
                layer.open({
                    content: '3分钟以内都有效,请检查短消息'
                  , skin: 'msg'
                  , time: 2 //2秒后自动关闭
                });
            }
            else if (result == -6) {
                //localStorage.setItem("ShouldCheckCode", json.SendCheckCode);
                //document.getElementById('ShouldCheckCode').value=json.SendCheckCode;
                //提示
                layer.open({
                    content: '短信模板尚未配置'
                  , skin: 'msg'
                  , time: 3 //2秒后自动关闭
                });
            }
            else if (result == -5) {
                //localStorage.setItem("ShouldCheckCode", json.SendCheckCode);
                //document.getElementById('ShouldCheckCode').value=json.SendCheckCode;
                //提示
                layer.open({
                    content: '验证码余额不足'
                  , skin: 'msg'
                  , time: 3 //2秒后自动关闭
                });
            }
            else {
                //信息框
                layer.open({
                    content: '验证码发送失败'
                  , btn: '我知道了'
                });

            }
            return;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.status);
            console.log(XMLHttpRequest.readyState);
            console.log(textStatus);
        }
        //error: function () {
        //}
    });

}

function FunctionSubmit() {
    if (!checkPhone('userMobilePhone')) { alert("手机号码有误，请重填"); return; }
    if (document.getElementById('CheckPassWord').value.length < 4) {
        alert("手机验证码不能为空"); return;
    }


    if (document.getElementById('CheckPassWord').value.length < 4) {
        alert("手机验证码不能为空,请发送手机验证码"); return;
    }

    if (document.getElementById('CheckPassWord').value != localStorage.getItem("ShouldCheckCode")) {
        alert("手机验证码错误，请核对"); return;
    }

    if (document.getElementById('RecommanduserPhone').value != '') {
        if (!checkPhone('RecommanduserPhone')) { alert("推荐人手机号码有误，请重填"); return; }
    }
    if (document.getElementById('userpwd').value.length < 6) {
        alert("密码位数至少6位"); return;
    }
    if (document.getElementById('userpwd').value != document.getElementById('Repeatuserpwd').value) {
        alert("两次输入的密码不一致"); return;
    }

    RegisterMSGCode();

}


function RegisterMSGCode() {
    var varuserMobilePhone = document.getElementById('userMobilePhone').value;
    var varShouldCheckCode = document.getElementById('CheckPassWord').value;
    var varuserpwd = document.getElementById('userpwd').value;
    //var varuserpwd=document.getElementById('userpwd').value;
    var varRecommandPhone = document.getElementById('RecommanduserPhone').value;

    var url = varGetAppConfiugServicesURL + "/Other/CheckCode/WSCheck.asmx/doGameInfo_CheckPhoneCode_Register";
    url += "?RegsiterCode=" + varShouldCheckCode;
    url += "&PhoneNum=" + varuserMobilePhone
    url += "&userpwd=" + varuserpwd
    url += "&RecommandPhone=" + varRecommandPhone
    url += "&strShopClientID=" + varpub_Int_ShopClientID;
    url += "&strUserID=" + varpub_Int_Session_CurUserID;
    layer.open({ content: '正在发送验证码,3小时以内都有效', type: 2, time: 1 });


    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp120161006Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            var result = parseInt(json.ErrorCode);
            if (result == 5) {
                //提示
                layer.open({
                    type: 2
                    , content: '注册成功,重置密码成功', time: 2 //2秒后自动关闭
                });
            }
            else if (result == 55) {
                //提示
                layer.open({
                    type: 2
                    , content: '注册成功,重置密码成功.推荐人设置尚未成功', time: 2 //2秒后自动关闭
                });
            }
            else if (result == 7) {
                //提示
                layer.open({
                    type: 2
                    , content: '注册成功,官方充值成功，请登陆会员中心查看', time: 2 //2秒后自动关闭
                });
            }
            else if (result == 6) {
                layer.open({
                    type: 2, content: '注册成功'
                  , time: 2 //2秒后自动关闭
                });
            } else if (result == 66) {
                layer.open({
                    type: 2, content: '注册成功.推荐人设置尚未成功'
                  , time: 2 //2秒后自动关闭
                });
            }
            else if (result == -3) {
                layer.open({
                    content: '手机号码已被其他微信号捆绑'
                  , time: 2 //2秒后自动关闭
                });
            }
            else if (result == -2) {
                layer.open({
                    content: '验证码错误,验证码3分钟以内都有效'
                  , time: 2 //2秒后自动关闭
                });
            }
            if (result == 6 || result == 5 || result == 55 || result == 7) {
                RegisterLocalState(varuserMobilePhone);
            }
            return;
        },
        error: function () {
        }
    });
}

function RegisterLocalState(VarPhoneNum) {
    var varQueryStringList = new QueryString();
    var varLocalStorgeCallbackURL = varQueryStringList["LocalStorgeCallbackURL"];

    if (isNull(varLocalStorgeCallbackURL) == false) {
        window.location.href = varLocalStorgeCallbackURL;////跳回
    }
    else {
        //var strURL = "/User/WeiXinOpenID.aspx?type=ReadedlocalStorageFromWeiXinOpenID&myjson_OpenID_openid=" + varstrApplicationCheckName;
        //strURL += "&LocalStorgeCallbackURL=" + "/";///回到首页
        //strURL += "&ScopeAccess_token=" + "snsapi_App_Token";
        //strURL += "&scope=" + "snsapi_App_LocalStorge";
        window.location.href = "/mywebuy.aspx";
    }
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
