var OFFSET = 5;
var page = 1;
var PAGESIZE = 3;

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
		"/Handler/MultiButton_agent.ashx?strUserID=" + pub_Int_Session_CurUserID + "&LevelShow=" + LevelShowNum, {
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
		        $.each(response.list, function (key, value) {
		            //$("#thelist").append('<li  ms-repeat=\"items\">' + value.name + '\t' + value.time + '</li>');
		            $("#thelist").append(addThisLine(value));


		        });
		        // $("#thelist").listview("refresh");
		        myScroll.refresh(); // Remember to refresh when contents are loaded (ie: on ajax completion)
		        // pullDownEl.className = 'idle';
		        // pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Pull down to refresh...';
		        // this.minScrollY = -pullDownOffset;

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
		"/Handler/MultiButton_agent.ashx?strUserID=" + pub_Int_Session_CurUserID + "&LevelShow=" + LevelShowNum, {
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
		"/Handler/MultiButton_agent.ashx?strUserID=" + pub_Int_Session_CurUserID + "&LevelShow=" + LevelShowNum, {
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



function addThisLine(varLine) {

    var varLineContent = "<li style=\"height: 170px;\"  ms-repeat=\"items\">";
    varLineContent += "			<div class=\"ul_li\">\n";
    varLineContent += "				<div class=\"ul_li_div_img\">\n";
    varLineContent += "					<div class=\"div_img_center\"><img alt=\"图片\" width=\"90px\" height=\"90px\"\n";
    varLineContent += "						src=\"" + varLine.strHeadIMG + "\"></div>\n";
    varLineContent += "					<div class=\"ul_li_div_name\">" + varLine.strNickname + "</div>\n";
    varLineContent += "				</div>\n";
    varLineContent += "				<div class=\"ul_li_trade\"><ul class=\"OliverModi\">\n";
    varLineContent += "					<li>代理店铺名:" + varLine.strShopName + "</li>\n";
    varLineContent += "						<li>联系人:" + varLine.UserRealName + "</li>\n";
    varLineContent += "						<li>电话:<a href=\"tel:" + varLine.tel + "\">" + varLine.tel + "</a></li>\n";
    //strBody += "						<li>店铺更新时间:" + strUpdateTime + "</li>\n";
    varLineContent += "						<li>代理商品总数:" + varLine.GoodsCounts + "</li>\n";
    //strBody += "				<div class=\"ul_li_money_Percent\"><ul class=\"OliverModi\">\n";
    varLineContent += "						<li>代理销售商品数:" + varLine.SalesGoodsCounts + "%</li>\n";
    varLineContent += "						<li>所得收入:" + varLine.GetSalesMoney + "￥(T+7)</li>\n";

    if (varLine.boolIfShowMuSonMoney == "True") {
        varLineContent += "<li><a style=\"color:blue;\" href='" + varLine.strPub_Agent_Path + "/multibutton_customer.aspx?lookmysonid=" + varLine._GetAgent_UserID + "&agentlevelnum=" + (varLine.intAgentLevelNum+1) + "'>直接收入(" + varLine.GetmySalesMoney + "￥)</a></li>";
        varLineContent += "<li><a style=\"color:blue;\" target=\"_blank\" href='" + varLine.strPub_Agent_Path + "/multibutton_agent.aspx?lookmysonid=" + varLine._GetAgent_UserID + "&agentlevelnum=" + (varLine.intAgentLevelNum ) + "'>下级" + varLine._GetAgent_FenXiaoOrDaili + "商(" + varLine.intFenXiaoOrDailiCount + "个)</a></li>";
    }
    else if (varLine.boolIfShowMuSonMoney == "False") {
        varLineContent += "<li>直接收入(" + varLine.GetmySalesMoney + "￥)</li>";
        varLineContent += "<li>下级" + varLine._GetAgent_FenXiaoOrDaili + "商(" + varLine.intFenXiaoOrDailiCount + "个)</li>";
    }
    varLineContent += "				</ul></div>\n";
    varLineContent += "			</div></li>\n";

    return varLineContent;
}