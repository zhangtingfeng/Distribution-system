$(document).ready(function() {
	$("#iqr").click(function() {
		var _popup = popupShow_Head();
//		$qrBox.addClass("on")

//		$qrBox.find(".closet").click(function() {
//			$qrBox.removeClass("on");
//			$qrBox.one($.transitionEnd, function() {
//				popupRemove();
//			});
//		});

//		return false;
	});
});


// 添加蒙版 & 删除模版
var removeTouch = function(event) {
	var target = event.target;
	node = document.querySelector('.m-popup');
	if (orientation != 90 || orientation != -90) {
		event.preventDefault();
	}

}

var delPopup = function(event) {
	var target = event.target;
	if (target.className == 'm-popup') {
		popupRemove();

	}
}

// 判断横屏
var updateOrientation = function() {
	var orientation = window.orientation;
	if (orientation == 90 || orientation == -90) {
	    document.body.removeEventListener('touchmove', removeTouch, false);
	} else {
		document.body.addEventListener('touchmove', removeTouch, false);
	}
}

var popupShow_Head = (function() {
	return function(type) {
		document.documentElement.style.overflow = 'hidden';
		document.body.style.overflow = 'hidden';
		document.body.addEventListener('click', delPopup, false);
		document.body.addEventListener('touchmove', delPopup, false);
		document.body.addEventListener('touchstart', delPopup, false);
		var _popup = document.createElement("div");
		_popup.innerHTML = "<img width='300' height='300' src='/Templet/02ShiYi/skin/images/jan140328202201.jpg'>";
		_popup.className = 'm-popup';
		document.body.appendChild(_popup);
		return _popup;
	}
})();

var popupRemove = (function() {
	return function() {
		var node = document.querySelector('.m-popup');
		node.parentNode.removeChild(node);

		document.documentElement.style.overflow = 'auto';
		document.body.style.overflow = 'auto';
		document.body.removeEventListener('click', delPopup, false);
		document.body.removeEventListener('touchmove', delPopup, false);
		document.body.removeEventListener('touchstart', delPopup, false);


	}
})();


