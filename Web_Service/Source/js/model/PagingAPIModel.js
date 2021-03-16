(function (global, undefined) {
    'use strict';

    var PagingAPIModel = function () {
        //分页对象
        this.pages = [{ pageNumber: '1', isActive: 'active' }];
        //数据量
        this.dataCount = 0;
        //数据量格式化
        this.dataCountFormatter = "";
        //当前页号
        this.pageNumber = 1;
        //每页记录数
        this.pageSize = 10;
        this.dataAllPageCountFormatter = "";
        return this;
    };

    PagingAPIModel.prototype = {
        //最大页数
        maxPageNumber: function () {
            var maxPageNumber = Math.ceil(this.dataCount / this.pageSize);

            this.dataAllPageCountFormatter = maxPageNumber <= 0 ? 1 : maxPageNumber;
            return this.dataAllPageCountFormatter;
        },
        //分页查询用，起始行号
        startRowNumber: function () {
            var val = (this.pageNumber - 1) * this.pageSize;
            if (val < 0) val = 0;
            return val + 1;
        },
        //分页查询用，结束行号
        endRowNumber: function () {
            var val = this.pageNumber * this.pageSize;
            if (val < 0) val = 0;
            return val;
        },
        getPrevPageNumber: function () {
            this.pageNumber--;
            if (this.pageNumber - 1 <= 0) {
                this.pageNumber = 1;
            }
            return this.pageNumber;
        },
        getNextPageNumber: function () {
            this.pageNumber++;
            if (this.pageNumber >= this.maxPageNumber()) {
                this.pageNumber = this.maxPageNumber();
            }
            return this.pageNumber;
        },
        //计算分页
        calPages: function () {
            this.pages = [];

            //1 1~10
            //2 11~20
            //3 21~30
            //...
            //9 81~90
            //10 91~100
            //11 101~110
            var maxPage = 10;
            //判断当前页是否已经超出最大数据页
            this.pageNumber = this.pageNumber > this.maxPageNumber() ? this.maxPageNumber() : this.pageNumber;

            var startPageNumber = this.pageNumber - maxPage / 2;
            startPageNumber = startPageNumber < 0 ? 0 : startPageNumber;
            for (var i = 1; i <= maxPage; i++) {
                var p = startPageNumber + i;
                if (p > this.maxPageNumber()) {
                    break;
                }
                this.pages.push({ pageNumber: p, isActive: p == this.pageNumber ? 'active btn-primary' : '' });
            }
            //格式化显示总记录数
            this.dataCountFormatter = this.dataCount.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
            
        }
    };

    global.PagingAPIModel = PagingAPIModel;
})(typeof window === 'undefined' ? this : window);