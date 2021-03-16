var OFFSET = 5;
var page = 1;
var PAGESIZE = 4;

var myScroll,
	pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;
var maxScrollY = 0;

var hasMoreData = false;

document.addEventListener('touchmove', function (e) {
    e.preventDefault();
}, false);

document.addEventListener('DOMContentLoaded', function () {
    $(document).ready(function () {
        loaded();
    });
}, false);

function loaded() {
    //debugger;
    //pullDownEl = document.getElementById('pullDown');
    //pullDownOffset = pullDownEl.offsetHeight;
    pullUpEl = document.getElementById('pullUp');
    pullUpOffset = pullUpEl.offsetHeight;

    hasMoreData = false;
    // $("#thelist").hide();
    $("#pullUp").hide();

    //pullDownEl.className = 'loading';
    //pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Loading...';
    $("#thelist").append('<img style="margin: 0px auto;display:block;" src="/images/loading.gif"/>');
    page = 1;
    $.post(
		"/Handler/ProductSearchGoods.ashx?strUserID=" + pub_Int_Session_CurUserID + "&SearchKey=" + strSearchKey, {
		    "page": page,
		    "pagesize": PAGESIZE
		},
		function (response, status) {
		    if (status == "success") {
		        $("#thelist").show();

		        if (response.list.length < PAGESIZE) {
		            hasMoreData = false;
		            $("#pullUp").hide();
		        } else {
		            hasMoreData = true;
		            $("#pullUp").show();
		        }

		        // document.getElementById('wrapper').style.left = '0';

		        myScroll = new iScroll('wrapper', {
		            useTransition: true,
		            topOffset: pullDownOffset,
		            onRefresh: function () {
		                //if (pullDownEl.className.match('loading')) {
		                //	pullDownEl.className = 'idle';
		                //	pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Pull down to refresh...';
		                //	this.minScrollY = -pullDownOffset;
		                //}
		                if (pullUpEl.className.match('loading')) {
		                    pullUpEl.className = 'idle';
		                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
		                }
		            },
		            onScrollMove: function () {
		                //if (this.y > OFFSET && !pullDownEl.className.match('flip')) {
		                //	pullDownEl.className = 'flip';
		                //	pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Release to refresh...';
		                //	this.minScrollY = 0;
		                //} else if (this.y < OFFSET && pullDownEl.className.match('flip')) {
		                //	pullDownEl.className = 'idle';
		                //	pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Pull down to refresh...';
		                //	this.minScrollY = -pullDownOffset;
		                //} 
		                if (this.y < (maxScrollY - pullUpOffset - OFFSET) && !pullUpEl.className.match('flip')) {
		                    if (hasMoreData) {
		                        this.maxScrollY = this.maxScrollY - pullUpOffset;
		                        pullUpEl.className = 'flip';
		                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '松开刷新...';
		                    }
		                } else if (this.y > (maxScrollY - pullUpOffset - OFFSET) && pullUpEl.className.match('flip')) {
		                    if (hasMoreData) {
		                        this.maxScrollY = maxScrollY;
		                        pullUpEl.className = 'idle';
		                        pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
		                    }
		                }
		            },
		            onScrollEnd: function () {
		                //if (pullDownEl.className.match('flip')) {
		                //	pullDownEl.className = 'loading';
		                //	pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Loading...';
		                //	// pullDownAction(); // Execute custom function (ajax call?)
		                //	refresh();
		                //}
		                if (hasMoreData && pullUpEl.className.match('flip')) {
		                    pullUpEl.className = 'loading';
		                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '加载中...';
		                    // pullUpAction(); // Execute custom function (ajax call?)
		                    nextPage();
		                }
		            }
		        });

		        $("#thelist").empty();

		        if (response.list.length > 0) {
		            $.each(response.list, function (key, value) {
		                $("#thelist").append(addThisLine(value));
		            });
		        } else {
		            $("#thelist").append("暂无数据");
		        }
		        myScroll.refresh(); // Remember to refresh when contents are loaded (ie: on ajax completion)

		        if (hasMoreData) {
		            myScroll.maxScrollY = myScroll.maxScrollY + pullUpOffset;
		        } else {
		            myScroll.maxScrollY = myScroll.maxScrollY;
		        }
		        maxScrollY = myScroll.maxScrollY;
		    };
		},
		"json");
}

function refresh() {
    page = 1;
    $.post(
		"/Handler/ProductSearchGoods.ashx?strUserID=" + pub_Int_Session_CurUserID + "&SearchKey=" + strSearchKey, {
		    "page": page,
		    "pagesize": PAGESIZE
		},
		function (response, status) {
		    if (status == "success") {
		        $("#thelist").empty();

		        myScroll.refresh();

		        if (response.list.length < PAGESIZE) {
		            hasMoreData = false;
		            $("#pullUp").hide();
		        } else {
		            hasMoreData = true;
		            $("#pullUp").show();
		        }

		        $.each(response.list, function (key, value) {
		            $("#thelist").append(addThisLine(value));

		            //$("#thelist").append('<li>' + value.name + '\t' + value.time + '</li>');
		        });
		        // $("#thelist").listview("refresh");
		        myScroll.refresh(); // Remember to refresh when contents are loaded (ie: on ajax completion)

		        if (hasMoreData) {
		            myScroll.maxScrollY = myScroll.maxScrollY + pullUpOffset;
		        } else {
		            myScroll.maxScrollY = myScroll.maxScrollY;
		        }
		        maxScrollY = myScroll.maxScrollY;
		    };
		},
		"json");
}

function nextPage() {
    page++;
    $.post(
		"/Handler/ProductSearchGoods.ashx?strUserID=" + pub_Int_Session_CurUserID + "&SearchKey=" + strSearchKey, {
		    "page": page,
		    "pagesize": PAGESIZE
		},
		function (response, status) {
		    if (status == "success") {
		        if (response.list.length < PAGESIZE) {
		            hasMoreData = false;
		            $("#pullUp").hide();
		        } else {
		            hasMoreData = true;
		            $("#pullUp").show();
		        }

		        $.each(response.list, function (key, value) {
		            $("#thelist").append(addThisLine(value));

		            //$("#thelist").append('<li>' + value.name + '\t' + value.time + '</li>');
		        });
		        // $("#thelist").listview("refresh");
		        myScroll.refresh(); // Remember to refresh when contents are loaded (ie: on ajax completion)
		        if (hasMoreData) {
		            myScroll.maxScrollY = myScroll.maxScrollY + pullUpOffset;
		        } else {
		            myScroll.maxScrollY = myScroll.maxScrollY;
		        }
		        maxScrollY = myScroll.maxScrollY;
		    };
		},
		"json");
}




function btnSearch() {;
    var varSearchKey = document.getElementById("oliversearch-input_s_1447079356776").value;
    window.location.href = '/ProductSearchGoods.aspx?SearchKey=' + varSearchKey;
};
