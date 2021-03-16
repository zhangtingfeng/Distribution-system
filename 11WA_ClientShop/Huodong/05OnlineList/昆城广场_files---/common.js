var _sessionStorageName = "KunChengGuangChangNew";//值为会员卡卡号
var _sessionStoragePhone = "KunChengGuangChangPhoneNew";//曾登录过的手机号码
var _sessionStorageOpenid = "KunChengGuangChangOpenid";//关联openid

var _openid;//openid

var _source;//设备来源
var _time;//时间戳
var _token;//签名

//域名地址
var _domainUrl = "http://kunchensquare.kunshankunshan.com/admin";// 正式
//var _domainUrl = "http://kunchensq.imagchina.com/admin";// 测试

var _api = _api || {};
_api = {
    //跳转链接
    jumpUrl: "http://kunshankunshan.com/kunchensquare/website/",// 正式
    //jumpUrl: "http://kunshankunshan.com/kunchensquare/website-test/",// 测试
	
    CheckUserInfoByOpenid: _domainUrl + '/member/information:CheckUserInfoByOpenid',//获取微信用户绑定或登录的状态
    getCode: _domainUrl + '/member/register:sendsms',//获取验证码
    queryCode: _domainUrl + '/member/register:querycode',//查询验证码
    register: _domainUrl + '/member/register',//会员卡申请
    updateInformation: _domainUrl + '/member/information:Updateinformation',//完善用户详细信息
    getInformation: _domainUrl + '/member/information',//获取用户详细信息


    information: {type: "POST", url: _domainUrl + "/member/information"},// 个人资料
    getEventSC: {type: "POST", url: _domainUrl + "/apps/Mallpromotion/api"},// 商场促销
    getEventDetailSC: {type: "POST", url: _domainUrl + "/apps/Mallpromotion/api:FindSingle"},// 商场促销详情
    getEventSH: {type: "POST", url: _domainUrl + "/apps/Merchantspromotion/api"},// 商户促销
    getEventDetailSH: {type: "POST", url: _domainUrl + "/apps/Merchantspromotion/api:FindSingle"},// 商户促销详情
    getMembership: {type: "POST", url: _domainUrl + "/apps/Membership/api"},// 活动列表
    getMembershipOver: {type: "POST", url: _domainUrl + "/apps/Membership/api:over"},// 活动回顾列表
    getQueryById: {type: "POST", url: _domainUrl + "/apps/Membership/api:QueryById"},// 活动详情
    saveApply: {type: "POST", url: _domainUrl + "/apps/Membership/api:apply"},// 报名
    saveSign: {type: "POST", url: _domainUrl + "/apps/Membership/api:Sign"},// 签到
    completion: {type: "POST", url: _domainUrl + "/member/information:completion"},// 个人资料完成度
    updatePwd: {type: "POST", url: _domainUrl + "/member/information:UpdatePwd"},//更新交易密码
   
	
};

/**
 * 获取地址栏参数
 * @param name
 * @returns {null}
 */
function getParameter(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}

/**
 * 是否是微信
 * @returns {boolean}
 */
function isWeChat() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
        return true;
    } else {
        return false;
    }
}

/**
 * 设备来源
 */
function browserRedirect() {
    var sUserAgent = navigator.userAgent.toLowerCase();
    var bIsIpad = sUserAgent.match(/ipad/i) == "ipad";
    var bIsIphoneOs = sUserAgent.match(/iphone os/i) == "iphone os";
    var bIsMidp = sUserAgent.match(/midp/i) == "midp";
    var bIsUc7 = sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4";
    var bIsUc = sUserAgent.match(/ucweb/i) == "ucweb";
    var bIsAndroid = sUserAgent.match(/android/i) == "android";
    var bIsCE = sUserAgent.match(/windows ce/i) == "windows ce";
    var bIsWM = sUserAgent.match(/windows mobile/i) == "windows mobile";
    if (bIsIpad) {
        _source = "ipad";
    } else if (bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM) {
        _source = "phone";
    } else {
        _source = "pc";
    }
}

/**
 * 生成时间戳与签名
 */
function timestampSignature() {
    _time = new Date().getTime();
    _token = $.md5("imagchina" + _time);
}

/**
 * 获取微信用户绑定或登录的状态
 */
function prefsession() {
    /* 无需验证是否登录 */
    if (getParameter("f") == 'index' || _curPage == '商场促销商户促销'||_curPage == '非会员电子券详情'||_curPage == '积分兑换会员'||_curPage == '个人礼券详情') return;

    $.ajax({
        type: 'POST',
        url: _api.CheckUserInfoByOpenid,
        data: {
            time: _time,
            token: _token,
            membercode: saveInformation2.get(),
            openid: saveInformation3.get()
        },
        cache: false,
        success: function (data) {
            data = eval('(' + data + ')');
            console.log("获取微信用户绑定或登录的状态", data);
            if (data.err == 0) {
                saveInformation.set(data.membercode);
                saveInformation2.set(data.tel);
                if (data.isBind) {
                    //已经绑定微信openid
                } else {
                    //跳转至登陆页
                    if (_curPage != "loginClubcard") window.location.href = _api.jumpUrl + 'loginClubcard.html';
                    return;
                }
                if (data.islogged) {
                    //已登陆
                    if (_curPage == "usercenter") {
                        $("#header, .wrap").show();
                        $("body").removeClass("body");
                        userCenterInit();
                        return;
                    }
                    if (_curPage == "index") {
                        window.location.href = _api.jumpUrl + 'usercenter.html';
                        return;
                    }
                    if (_curPage == "loginClubcard") {
                        window.location.href = _api.jumpUrl + 'usercenter.html';
                        return;
                    }
                    if (_curPage == "passwordGet") {
                        window.location.href = _api.jumpUrl + 'usercenter.html';
                        return;
                    }
                    if (_curPage == "cardBinding") {
                        window.location.href = _api.jumpUrl + 'usercenter.html';
                        return;
                    }
                    if (_curPage == "专享活动") {
                        openVipEvent();
                    }
                    if (_curPage == "积分兑换非会员") {
                        window.location.href = _api.jumpUrl + 'score_exchange.html';
                        return;
                    }
                } else {
                    //未登陆
                    if (_curPage == "index") {
                        window.location.href = _api.jumpUrl + 'loginClubcard.html';
                        return;
                    }
                    if (_curPage == "passwordChange") {
                        window.location.href = _api.jumpUrl + 'loginClubcard.html';
                        return;
                    }
                    if (_curPage == "passwordGet") {
                        return;
                    }
                    if (_curPage == "usercenter") {
                        window.location.href = _api.jumpUrl + 'loginClubcard.html';
                        return;
                    }
                    if (_curPage == "information") {
                        window.location.href = _api.jumpUrl + 'loginClubcard.html';
                        return;
                    }
                    if (_curPage == "cardBinding") {
                        $("#cardBindingWrap").show();
                        $("#loadingGray").hide();
                        return;
                    }
                    if (_curPage == "专享活动" || _curPage == "专享活动详情") {
                        //window.location.href = _api.jumpUrl + 'loginClubcard.html';
                        //return;
                        openVipEvent();
                    }

					
                }
            } else {
                //未登录过任何信息跳转至注册页面
                if (_curPage != "index" && _curPage != "cardBinding" && _curPage != "主题活动" && _curPage != "主题活动详情" && _curPage != "专享活动" && _curPage != "专享活动详情") {
                    window.location.href = _api.jumpUrl + 'index.html';
                }else{
                    //展开注册页面内容
                    $("#loadingGray").hide();
                    $("#indexWrap").show();
                    $("#cardBindingWrap").show();
                }
            }
        }
    });

    //显示专享活动页面元素
    function openVipEvent(){
        $("#loadingGray").hide();
        $("#header, .viewport-theme").show();
    }
}
function prefsession2() {
    /* 无需验证是否登录 */
    $.ajax({
        type: 'POST',
        url: _api.CheckUserInfoByOpenid,
        data: {
            time: _time,
            token: _token,
            membercode: saveInformation2.get(),
            openid: saveInformation3.get()
        },
        cache: false,
        success: function (data) {
            data = eval('(' + data + ')');
            console.log("获取微信用户绑定或登录的状态", data);
            if (data.err == 0) {
                saveInformation.set(data.membercode);
                saveInformation2.set(data.tel);
                if (data.isBind) {
                    //已经绑定微信openid
                } else {
                    //跳转至登陆页
                }
                if (data.islogged) {
                    //已登陆
                     window.location.href = _api.jumpUrl + 'score_exchange.html';
                } else {
                    //未登陆
                }
            }
        }
    });

}

/**
 * 获取验证码
 */
function testGetCode() {
    var getCodeBox = $("#get_code");
    getCodeBox.unbind("click").bind("click", function () {
        if (getCodeBox.attr("class").indexOf("off") < 0) {
            var _n = 90;

            //验证手机号码
            var _userPhoneVal = "000000";
            if(_curPage == 'tradersPassword'){
                _userPhoneVal = parseInt(saveInformation2.get());
            }else{
                var _userPhone = $("#userPhone");
                _userPhoneVal = _userPhone.val();
            }
            if (_userPhoneVal == "" || _userPhoneVal.length < 11) {
                publicPopupBox("请输入正确的手机号码");
                return;
            }
            var phone = /^1[3|4|5|7|8][0-9]\d{4,8}$/;
            if (!phone.test(_userPhoneVal)) {
                publicPopupBox('请输入正确的手机号码！');
                return;
            }

            //倒计时
            getCodeBox.addClass("off");
            function checkTime() {
                getCodeBox.html(_n + "S");
                _n--;
                if (_n != -1) {
                    setTimeout(checkTime, 1000);
                } else {
                    getCodeBox.html("获取动态密码");
                    getCodeBox.removeClass("off");
                }
            }

            checkTime();

            //来源
            var _action = "";
            if(_curPage == "index"){
                _action = "申请";
            }
            if(_curPage == "loginClubcard"){
                _action = "登陆";
            }
            if(_curPage == "cardBinding"){
                _action = "绑定";
            }
            if(_curPage == "tradersPassword"){
                _action = "交易密码";
            }

            //发送短信验证码
            $.ajax({
                type: 'POST',
                url: _api.getCode,
                data: {
                    time: _time,
                    token: _token,
                    tel: _userPhoneVal,
                    action: _action,
                    openid: saveInformation3.get()
                },
                cache: false,
                success: function (data) {
                    data = eval('(' + data + ')');
                    if (data.err == 0) {
                        //验证码发送成功
                        publicPopupBox('验证码已经发送到您的手机，请注意查收，验证码有效时间为15分钟。失效后请重新获取动态密码。');
                    } else {
                        publicPopupBox('验证码发送失败，请重试！');
                    }
                }
            });
        }
    });
}

/**
 * 验证验证码
 */
function checkoutGetCode() {
    //验证手机号码
    var _userPhone = $("#userPhone");
    if (_userPhone.val() == "" || _userPhone.val().length < 11) {
        return;
    }
    var phone = /^1[3|4|5|7|8][0-9]\d{4,8}$/;
    if (!phone.test(_userPhone.val())) {
        return;
    }
    //验证码
    var _userCode = $("#userCode");
    if (_userCode.val() == "") {
        return;
    }
    $.ajax({
        type: 'POST',
        url: _api.queryCode,
        data: {
            time: _time,
            token: _token,
            tel: _userPhone.val(),
            code: _userCode.val()
        },
        cache: false,
        success: function (data) {
            data = eval('(' + data + ')');
        }
    });
}

/**
 * 基本提示窗口
 * @param font
 */
function publicPopupBox(font) {
    var getCodeBox = $("#getCode");
    var updateOkBox = $("#updateOk");
    var grayBox = $("#gray_box");
    getCodeBox.find(".popupText").html(font);
    getCodeBox.find(".popupText").css("text-align", "center");
    getCodeBox.fadeIn();
    updateOkBox.find(".popupText").css("text-align", "center");
    grayBox.fadeIn();
}

/**
 * 保存用户登录的相关信息
 */
var saveInformation = {
    islocalStorage: function () {
        if (window.localStorage) {
            //浏览支持localStorage
            return true;
        } else {
            //浏览暂不支持localStorage
            return false;
        }
    },
    set: function (o) {
        if (saveInformation.islocalStorage()) {
            localStorage.setItem(_sessionStorageName, o);
        } else {
            var date = new Date();
            date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000));
            $.cookie(_sessionStorageName, o, {path: '/', expires: date});
        }
    },
    get: function () {
        if (saveInformation.islocalStorage()) {
            return localStorage.getItem(_sessionStorageName);
        } else {
            return $.cookie(_sessionStorageName);
        }
    },
    clear: function () {
        if (saveInformation.islocalStorage()) {
            localStorage.clear();
        } else {
            $.cookie(_sessionStorageName, null, {path: '/'});
        }
    }
};
var saveInformation2 = {
    islocalStorage: function () {
        if (window.localStorage) {
            //浏览支持localStorage
            return true;
        } else {
            //浏览暂不支持localStorage
            return false;
        }
    },
    set: function (o) {
        if (saveInformation2.islocalStorage()) {
            localStorage.setItem(_sessionStoragePhone, o);
        } else {
            var date = new Date();
            date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000));
            $.cookie(_sessionStoragePhone, o, {path: '/', expires: date});
        }
    },
    get: function () {
        if (saveInformation2.islocalStorage()) {
            return localStorage.getItem(_sessionStoragePhone);
        } else {
            return $.cookie(_sessionStoragePhone);
        }
    },
    clear: function () {
        if (saveInformation2.islocalStorage()) {
            localStorage.removeItem(_sessionStoragePhone)
        } else {
            $.cookie(_sessionStoragePhone, null, {path: '/'});
        }
    }
};
var saveInformation3 = {
    islocalStorage: function () {
        if (window.localStorage) {
            //浏览支持localStorage
            return true;
        } else {
            //浏览暂不支持localStorage
            return false;
        }
    },
    set: function (o) {
        if (saveInformation3.islocalStorage()) {
            localStorage.setItem(_sessionStorageOpenid, o);
        } else {
            var date = new Date();
            date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000));
            $.cookie(_sessionStorageOpenid, o, {path: '/', expires: date});
        }
    },
    get: function () {
        if (saveInformation3.islocalStorage()) {
            return localStorage.getItem(_sessionStorageOpenid);
        } else {
            return $.cookie(_sessionStorageOpenid);
        }
    },
    clear: function () {
        if (saveInformation3.islocalStorage()) {
            localStorage.clear();
        } else {
            $.cookie(_sessionStorageOpenid, null, {path: '/'});
        }
    }
};

$(function () {
    //初始化遮罩图层
    var grayBox = $("#gray_box");
    grayBox.width($(window).width());
    grayBox.height($(window).height());

    //更新时间戳与签名
    timestampSignature();

    //设备来源
    browserRedirect();

    if (isWeChat()) {
        _source += "_____isWeChat";

        //个人资料页面不进入授权流程
        if (_curPage == "information") {
            prefsession();
            return;
        }
		
		
        //微信打开进入授权流程
        var w = getParameter('wxid');
        if (!w) {
            var _href = window.location.href;
            _href = _href.replace("http://", "");
            _href = _href.replace("#rd", "");
            var loc = encodeURIComponent(_href);
            window.location.href = _domainUrl + '/api/auth?tempURL=' + loc;
        } else {
            //保存用户openid
            _openid = w;
            saveInformation3.set(_openid);
            if (_curPage == "积分兑换非会员") {
            prefsession2();
            return;
            }
            prefsession();
        }
    }
});
