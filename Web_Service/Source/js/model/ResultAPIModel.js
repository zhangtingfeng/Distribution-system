(function (global, undefined) {
    'use strict';

    var ResultAPIModel = function () {
        //数据
        this.data = null;
        //分页信息
        this.paging = new PagingAPIModel();
        //返回信息
        this.resultMessage = new ResultMessageAPIModel();
        //用户cookie
        this.userCookie = new UserCookieAPIModel();

        return this;
    };

    ResultAPIModel.prototype = {

    };

    global.ResultAPIModel = ResultAPIModel;
})(typeof window === 'undefined' ? this : window);