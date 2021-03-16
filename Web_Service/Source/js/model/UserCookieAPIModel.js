(function (global, undefined) {
    'use strict';

    var UserCookieAPIModel = function () {
        //用户ID
        this.userID = cookieGet('userID');
        //用户账号
        this.userAccount = cookieGet('userAccount');
        //用户名
        this.userName = cookieGet('userName');

        //渠道发展编码
        this.DevelopmentCode = cookieGet('DevelopmentCode');
        return this;
    };

    UserCookieAPIModel.prototype = {

    };

    global.UserCookieAPIModel = UserCookieAPIModel;
})(typeof window === 'undefined' ? this : window);