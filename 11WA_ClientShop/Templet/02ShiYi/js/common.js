﻿$(document).ready(function(){$("#iwant").click(function(){$("#iwant").removeClass("draw_mine_black").addClass("draw_mine_green");$("#amine").addClass("colorgreen");var b=parseInt($("#divIwant").css("bottom"));$("#divIwant").css("right","0px");if(b<0){var b=$("#divIwant").height()+$("#bottom_navs").height()+28*5-15;$("#divIwant").css("bottom",b);$("#divIwant").css("display","block");$("#divContact").css("bottom",-100);$("#icontact").removeClass("draw_price_green").addClass("draw_price_black");$("#acon").removeClass("colorgreen")}else{$("#amine").removeClass("colorgreen");$("#divIwant").css("bottom",-100);$("#iwant").removeClass("draw_mine_green").addClass("draw_mine_black")}});$("#icontact").click(function(){$("#icontact").removeClass("draw_price_black").addClass("draw_price_green");$("#acon").addClass("colorgreen");var b=parseInt($("#divContact").css("bottom"));$("#divContact").css("left","50%");if(b<0){var b=$("#divContact").height()+$("#bottom_navs").height()-15;$("#divContact").css("bottom",b);$("#divContact").css("display","block");(function(){$(".open_nav_conter .phone").click(function(){var a=popupShow();var f=$(".m-phoneCode").val();a.innerHTML=f;var e=$(".m-phone");$.transitionEnd&&e[0].offsetWidth;e.addClass("on");e.find(".closet").click(function(){e.removeClass("on");e.one($.transitionEnd,function(){popupRemove()})});return false})})();$("#divIwant").css("bottom",-100);$("#iwant").removeClass("draw_mine_green").addClass("draw_mine_black");$("#amine").removeClass("colorgreen")}else{$("#acon").removeClass("colorgreen");$("#icontact").removeClass("draw_price_green").addClass("draw_price_black");$("#divContact").css("bottom",-100)}});$("#iphone").click(function(){var b=parseInt($("#divContact").css("bottom"));$("#divContact").css("left","50%");if(b<0){var b=$("#divContact").height()+$("#bottom_navs").height()-60;$("#divContact").css("bottom",b);$("#divContact").css("display","block");(function(){$(".open_nav_conter .phone").click(function(){var a=popupShow();var f=$(".m-phoneCode").val();a.innerHTML=f;var e=$(".m-phone");$.transitionEnd&&e[0].offsetWidth;e.addClass("on");e.find(".closet").click(function(){e.removeClass("on");e.one($.transitionEnd,function(){popupRemove()})});return false})})()}else{$("#divContact").css("bottom",-100)}})});function animateBottomFinished(){$("#divIwant").css("display","none")}function animateBottomFinished(){$("#divContact").css("display","none")}var ls;if(window.localStorage){ls=localStorage}var cartJson={};var ua=navigator.userAgent.toLowerCase();$.transitionEnd=(function(){var b=(function(){var e=document.createElement("ceshi"),a={WebkitTransition:"webkitTransitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd otransitionend",transition:"transitionend"},f;for(f in a){if(e.style[f]!==undefined){return a[f]}}}());return b})();var removeTouch=function(c){var d=c.target;node=document.querySelector(".m-popup");if(orientation!=90||orientation!=-90){c.preventDefault()}};var delPopup=function(c){var d=c.target;if(d.className=="m-popup"){popupRemove()}};var updateOrientation=function(){var b=window.orientation;if(b==90||b==-90){document.body.removeEventListener("touchmove",removeTouch,false)}else{document.body.addEventListener("touchmove",removeTouch,false)}};var popupShow=(function(){return function(d){document.documentElement.style.overflow="hidden";document.body.style.overflow="hidden";document.body.addEventListener("click",delPopup,false);var c=document.createElement("div");c.className="m-popup";document.body.appendChild(c);return c}})();var popupRemove=(function(){return function(){var b=document.querySelector(".m-popup");b.parentNode.removeChild(b);document.documentElement.style.overflow="auto";document.body.style.overflow="auto";document.body.removeEventListener("click",delPopup,false)}})();function wxEmail(f){if(ua.match(/MicroMessenger/i)=="micromessenger"){if(document.querySelector(".m-popup")){popupRemove()}var d=popupShow();var e;if(ua.indexOf("iphone")>-1||ua.indexOf("ipad")>-1||!!ua.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/)){e='<img src="'+fweb.path+'img/m_v2/emailMsg-ap.jpg" alt="" width="100%" />'}else{e='<img src="'+fweb.path+'img/m_v2/emailMsg-ad.jpg" alt="" width="100%" />'}d.innerHTML=e;d.onclick=function(){popupRemove()};return}else{return f()}}(function(b){(function(){b(".m-ft .phone").click(function(){var a=popupShow();var f=b(".m-phoneCode").val();a.innerHTML=f;var e=b(".m-phone");b.transitionEnd&&e[0].offsetWidth;e.addClass("on");e.find(".close").click(function(){e.removeClass("on");e.one(b.transitionEnd,function(){popupRemove()})});return false})})()})(Zepto);