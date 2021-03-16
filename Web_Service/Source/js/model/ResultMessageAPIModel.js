(function (global, undefined) {
    'use strict';

    var ResultMessageAPIModel = function () {
        //返回代码
        this.code = 200;
        //信息
        this.message = '';
        //错误代码
        this.errorCode = 0;
        //错误信息
        this.errorMessage = '';
        return this;
    };

    ResultMessageAPIModel.prototype = {
       
    };

    global.ResultMessageAPIModel = ResultMessageAPIModel;
})(typeof window === 'undefined' ? this : window);