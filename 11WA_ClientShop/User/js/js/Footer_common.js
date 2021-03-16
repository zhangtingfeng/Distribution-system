$(document)
		.ready(
				function() {
				    $("#iwant").click(
							function() {
							    $("#iwant").removeClass("draw_mine_black")
										.addClass("draw_mine_green");
							    $("#amine").addClass("colorgreen");

							    var iwantbottom = parseInt($("#divIwant").css(
										"bottom"));

							    $("#divIwant").css("right", "0px");

							    if (iwantbottom < 0) {
							        var iwantbottom = $("#divIwant").height()
											+ $("#bottom_navs").height() + 38
											* 6 - 15; // 大概每个 24高度

							        var iwantbottom = 180;

							        $("#divIwant").css("bottom", iwantbottom);
							        $("#divIwant").css("display", "block");
							        $("#divContact").css("bottom", -100);

							        $("#icontact").removeClass(
											"draw_price_green").addClass(
											"draw_price_black");
							        $("#acon").removeClass("colorgreen");
							    } else {
							        $("#amine").removeClass("colorgreen");
							        $("#divIwant").css("bottom", -100);
							        $("#iwant").removeClass("draw_mine_green")
											.addClass("draw_mine_black");

							    }
							});
				    $("#icontact")
							.click(
									function() {
									    $("#icontact").removeClass(
												"draw_price_black").addClass(
												"draw_price_green");
									    $("#acon").addClass("colorgreen");
									    var icontactbottom = parseInt($(
												"#divContact").css("bottom"));

									    $("#divContact").css("left", "50%");

									    if (icontactbottom < 0) {
									        var icontactbottom = $(
													"#divContact").height()
													+ $("#bottom_navs")
															.height() - 15;
									        icontactbottom = 180-38*3;//第四个按钮的6个少3个 所以减去3
									        $("#divContact").css("bottom",
													icontactbottom);
									        $("#divContact").css("display",
													"block");

									        (function() {
									            $(".open_nav_conter .phone")
														.click(
																function() {
																    var _popup = popupShow();
																    var _phone = $(
																			".m-phoneCode")
																			.val();
																    _popup.innerHTML = _phone;

																    var $phoneBox = $(".m-phone");

																    $.transitionEnd
																			&& $phoneBox[0].offsetWidth;
																    $phoneBox
																			.addClass("on")

																    $phoneBox
																			.find(
																					".closet")
																			.click(
																					function() {
																					    $phoneBox
																								.removeClass("on");
																					    $phoneBox
																								.one(
																										$.transitionEnd,
																										function() {
																										    popupRemove();
																										})
																					})

																    return false;
																})
									        })();
									        $("#divIwant").css("bottom", -100);
									        $("#iwant")
													.removeClass(
															"draw_mine_green")
													.addClass("draw_mine_black");
									        $("#amine").removeClass(
													"colorgreen");
									    } else {
									        $("#acon")
													.removeClass("colorgreen");
									        $("#icontact").removeClass(
													"draw_price_green")
													.addClass(
															"draw_price_black");
									        $("#divContact")
													.css("bottom", -100);
									    }
									});
				    $("#iphone")
							.click(function() {

							    var icontactbottom = parseInt($(
												"#divContact").css("bottom"));

							    $("#divContact").css("left", "50%");

							    if (icontactbottom < 0) {
							        var icontactbottom = $(
													"#divContact").height()
													+ $("#bottom_navs")
															.height() - 60; // 90;大概每个
							        // 24高度
							        // alert($("#divContact").height());
							        $("#divContact").css("bottom",
													icontactbottom);
							        $("#divContact").css("display",
													"block");

							        (function() {
							            $(".open_nav_conter .phone")
														.click(
																function() {
																    var _popup = popupShow();
																    var _phone = $(
																			".m-phoneCode")
																			.val();
																    _popup.innerHTML = _phone;

																    var $phoneBox = $(".m-phone");

																    $.transitionEnd
																			&& $phoneBox[0].offsetWidth;
																    $phoneBox
																			.addClass("on")

																    $phoneBox
																			.find(
																					".closet")
																			.click(
																					function() {
																					    $phoneBox
																								.removeClass("on");
																					    $phoneBox
																								.one(
																										$.transitionEnd,
																										function() {
																										    popupRemove();
																										})
																					})

																    return false;
																})
							        })();

							    } else {
							        $("#divContact")
													.css("bottom", -100);
							    }
							});

				});

function animateBottomFinished() {
	$("#divIwant").css("display", "none");
}
function animateBottomFinished() {
	$("#divContact").css("display", "none");
}
/* $("#idBottom").floatdiv("middlebottom"); */


var ls;
if(window.localStorage){
	ls = localStorage
}
var cartJson = {};
var ua = navigator.userAgent.toLowerCase();


$.transitionEnd = (function () {
	var transitionEnd = (function () {
		var el = document.createElement('ceshi') //创建一个自定义标签做测试
		, transEndEventNames = {  //检测CSS3 transition结束时的回调名   
			'WebkitTransition' : 'webkitTransitionEnd'
			,  'MozTransition'    : 'transitionend'
			,  'OTransition'      : 'oTransitionEnd otransitionend'
			,  'transition'       : 'transitionend'
		}
		, name

		for (name in transEndEventNames){
			if (el.style[name] !== undefined) {
				return transEndEventNames[name]
			}
		}
	}())
	return transitionEnd;
})();




/*
// 添加蒙版 & 删除模版

var removeTouch = function (event) {
	var target = event.target;
	node = document.querySelector('.m-popup');
	// alert(node.contains(target))
	// if(node.contains(target)){
	// 	return
	// }
	if(orientation != 90 || orientation != -90){
		event.preventDefault();
	}
	
}

var delPopup = function(event){
	var target = event.target;
	if(target.className == 'm-popup'){
		popupRemove();
		
	}
}

// 判断横屏
var updateOrientation = function () {
	 var orientation = window.orientation; 
	 if(orientation == 90 || orientation == -90){
	 	document.body.removeEventListener('touchmove', removeTouch, false);
	 } else {
	 	document.body.addEventListener('touchmove', removeTouch, false);
	 }
}

var popupShow = (function(){
	return function(type){
		// if(!type || type != 'none'){
			document.documentElement.style.overflow = 'hidden';
			document.body.style.overflow = 'hidden';
		// 	document.body.addEventListener('touchmove', removeTouch, false);
			document.body.addEventListener('click', delPopup, false); 
		// 	window.addEventListener("orientationchange",updateOrientation,false);
		// }
		var _popup = document.createElement("div");
		_popup.className = 'm-popup';
		document.body.appendChild(_popup);

		return _popup;
	}
})();

var popupRemove = (function(){
	return function(){
		var node = document.querySelector('.m-popup');
		node.parentNode.removeChild(node);

		document.documentElement.style.overflow = 'auto';
		document.body.style.overflow = 'auto';
		// document.body.removeEventListener('touchmove', removeTouch, false); 
		document.body.removeEventListener('click', delPopup, false); 
		// window.removeEventListener("orientationchange",updateOrientation,false);
	}
})();


*/


function wxEmail (callback) {
	if(ua.match(/MicroMessenger/i)=="micromessenger"){
		if(document.querySelector('.m-popup')){
			popupRemove();
		}
		var _popup = popupShow();
		var html;
		if (ua.indexOf("iphone") > -1 || ua.indexOf("ipad") > -1 || !!ua.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/) ){
			html = '<img src="'+fweb.path+'img/m_v2/emailMsg-ap.jpg" alt="" width="100%" />'
		} else{
			html = '<img src="'+fweb.path+'img/m_v2/emailMsg-ad.jpg" alt="" width="100%" />'
		}
		_popup.innerHTML = html;
		_popup.onclick = function  () {
			popupRemove();
		}
		return;
	} else {
		return callback();
	}
}



(function($){


	// phone
	(function () {
		$(".m-ft .phone").click(function () {
			var _popup = popupShow();
			var _phone = $(".m-phoneCode").val();
			_popup.innerHTML = _phone;

			var $phoneBox = $(".m-phone");

			$.transitionEnd && $phoneBox[0].offsetWidth;
			$phoneBox.addClass("on")

			$phoneBox.find(".close").click(function () {
				$phoneBox.removeClass("on");
				$phoneBox.one($.transitionEnd, function(){
					popupRemove();
				})
			})

			return false;
		})
	})();



})