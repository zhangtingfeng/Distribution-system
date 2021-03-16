/*!
 * jQuery Transit - CSS3 transitions and transformations
 * (c) 2011-2012 Rico Sta. Cruz <rico@ricostacruz.com>
 * MIT Licensed.
 *
 * http://ricostacruz.com/jquery.transit
 * http://github.com/rstacruz/jquery.transit
 */
(function(k){k.transit={version:"0.9.9",propertyMap:{marginLeft:"margin",marginRight:"margin",marginBottom:"margin",marginTop:"margin",paddingLeft:"padding",paddingRight:"padding",paddingBottom:"padding",paddingTop:"padding"},enabled:true,useTransitionEnd:false};var d=document.createElement("div");var q={};function b(v){if(v in d.style){return v}var u=["Moz","Webkit","O","ms"];var r=v.charAt(0).toUpperCase()+v.substr(1);if(v in d.style){return v}for(var t=0;t<u.length;++t){var s=u[t]+r;if(s in d.style){return s}}}function e(){d.style[q.transform]="";d.style[q.transform]="rotateY(90deg)";return d.style[q.transform]!==""}var a=navigator.userAgent.toLowerCase().indexOf("chrome")>-1;q.transition=b("transition");q.transitionDelay=b("transitionDelay");q.transform=b("transform");q.transformOrigin=b("transformOrigin");q.transform3d=e();var i={transition:"transitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd",WebkitTransition:"webkitTransitionEnd",msTransition:"MSTransitionEnd"};var f=q.transitionEnd=i[q.transition]||null;for(var p in q){if(q.hasOwnProperty(p)&&typeof k.support[p]==="undefined"){k.support[p]=q[p]}}d=null;k.cssEase={_default:"ease","in":"ease-in",out:"ease-out","in-out":"ease-in-out",snap:"cubic-bezier(0,1,.5,1)",easeOutCubic:"cubic-bezier(.215,.61,.355,1)",easeInOutCubic:"cubic-bezier(.645,.045,.355,1)",easeInCirc:"cubic-bezier(.6,.04,.98,.335)",easeOutCirc:"cubic-bezier(.075,.82,.165,1)",easeInOutCirc:"cubic-bezier(.785,.135,.15,.86)",easeInExpo:"cubic-bezier(.95,.05,.795,.035)",easeOutExpo:"cubic-bezier(.19,1,.22,1)",easeInOutExpo:"cubic-bezier(1,0,0,1)",easeInQuad:"cubic-bezier(.55,.085,.68,.53)",easeOutQuad:"cubic-bezier(.25,.46,.45,.94)",easeInOutQuad:"cubic-bezier(.455,.03,.515,.955)",easeInQuart:"cubic-bezier(.895,.03,.685,.22)",easeOutQuart:"cubic-bezier(.165,.84,.44,1)",easeInOutQuart:"cubic-bezier(.77,0,.175,1)",easeInQuint:"cubic-bezier(.755,.05,.855,.06)",easeOutQuint:"cubic-bezier(.23,1,.32,1)",easeInOutQuint:"cubic-bezier(.86,0,.07,1)",easeInSine:"cubic-bezier(.47,0,.745,.715)",easeOutSine:"cubic-bezier(.39,.575,.565,1)",easeInOutSine:"cubic-bezier(.445,.05,.55,.95)",easeInBack:"cubic-bezier(.6,-.28,.735,.045)",easeOutBack:"cubic-bezier(.175, .885,.32,1.275)",easeInOutBack:"cubic-bezier(.68,-.55,.265,1.55)"};k.cssHooks["transit:transform"]={get:function(r){return k(r).data("transform")||new j()},set:function(s,r){var t=r;if(!(t instanceof j)){t=new j(t)}if(q.transform==="WebkitTransform"&&!a){s.style[q.transform]=t.toString(true)}else{s.style[q.transform]=t.toString()}k(s).data("transform",t)}};k.cssHooks.transform={set:k.cssHooks["transit:transform"].set};if(k.fn.jquery<"1.8"){k.cssHooks.transformOrigin={get:function(r){return r.style[q.transformOrigin]},set:function(r,s){r.style[q.transformOrigin]=s}};k.cssHooks.transition={get:function(r){return r.style[q.transition]},set:function(r,s){r.style[q.transition]=s}}}n("scale");n("translate");n("rotate");n("rotateX");n("rotateY");n("rotate3d");n("perspective");n("skewX");n("skewY");n("x",true);n("y",true);function j(r){if(typeof r==="string"){this.parse(r)}return this}j.prototype={setFromString:function(t,s){var r=(typeof s==="string")?s.split(","):(s.constructor===Array)?s:[s];r.unshift(t);j.prototype.set.apply(this,r)},set:function(s){var r=Array.prototype.slice.apply(arguments,[1]);if(this.setter[s]){this.setter[s].apply(this,r)}else{this[s]=r.join(",")}},get:function(r){if(this.getter[r]){return this.getter[r].apply(this)}else{return this[r]||0}},setter:{rotate:function(r){this.rotate=o(r,"deg")},rotateX:function(r){this.rotateX=o(r,"deg")},rotateY:function(r){this.rotateY=o(r,"deg")},scale:function(r,s){if(s===undefined){s=r}this.scale=r+","+s},skewX:function(r){this.skewX=o(r,"deg")},skewY:function(r){this.skewY=o(r,"deg")},perspective:function(r){this.perspective=o(r,"px")},x:function(r){this.set("translate",r,null)},y:function(r){this.set("translate",null,r)},translate:function(r,s){if(this._translateX===undefined){this._translateX=0}if(this._translateY===undefined){this._translateY=0}if(r!==null&&r!==undefined){this._translateX=o(r,"px")}if(s!==null&&s!==undefined){this._translateY=o(s,"px")}this.translate=this._translateX+","+this._translateY}},getter:{x:function(){return this._translateX||0},y:function(){return this._translateY||0},scale:function(){var r=(this.scale||"1,1").split(",");if(r[0]){r[0]=parseFloat(r[0])}if(r[1]){r[1]=parseFloat(r[1])}return(r[0]===r[1])?r[0]:r},rotate3d:function(){var t=(this.rotate3d||"0,0,0,0deg").split(",");for(var r=0;r<=3;++r){if(t[r]){t[r]=parseFloat(t[r])}}if(t[3]){t[3]=o(t[3],"deg")}return t}},parse:function(s){var r=this;s.replace(/([a-zA-Z0-9]+)\((.*?)\)/g,function(t,v,u){r.setFromString(v,u)})},toString:function(t){var s=[];for(var r in this){if(this.hasOwnProperty(r)){if((!q.transform3d)&&((r==="rotateX")||(r==="rotateY")||(r==="perspective")||(r==="transformOrigin"))){continue}if(r[0]!=="_"){if(t&&(r==="scale")){s.push(r+"3d("+this[r]+",1)")}else{if(t&&(r==="translate")){s.push(r+"3d("+this[r]+",0)")}else{s.push(r+"("+this[r]+")")}}}}}return s.join(" ")}};function m(s,r,t){if(r===true){s.queue(t)}else{if(r){s.queue(r,t)}else{t()}}}function h(s){var r=[];k.each(s,function(t){t=k.camelCase(t);t=k.transit.propertyMap[t]||k.cssProps[t]||t;t=c(t);if(k.inArray(t,r)===-1){r.push(t)}});return r}function g(s,v,x,r){var t=h(s);if(k.cssEase[x]){x=k.cssEase[x]}var w=""+l(v)+" "+x;if(parseInt(r,10)>0){w+=" "+l(r)}var u=[];k.each(t,function(z,y){u.push(y+" "+w)});return u.join(", ")}k.fn.transition=k.fn.transit=function(z,s,y,C){var D=this;var u=0;var w=true;if(typeof s==="function"){C=s;s=undefined}if(typeof y==="function"){C=y;y=undefined}if(typeof z.easing!=="undefined"){y=z.easing;delete z.easing}if(typeof z.duration!=="undefined"){s=z.duration;delete z.duration}if(typeof z.complete!=="undefined"){C=z.complete;delete z.complete}if(typeof z.queue!=="undefined"){w=z.queue;delete z.queue}if(typeof z.delay!=="undefined"){u=z.delay;delete z.delay}if(typeof s==="undefined"){s=k.fx.speeds._default}if(typeof y==="undefined"){y=k.cssEase._default}s=l(s);var E=g(z,s,y,u);var B=k.transit.enabled&&q.transition;var t=B?(parseInt(s,10)+parseInt(u,10)):0;if(t===0){var A=function(F){D.css(z);if(C){C.apply(D)}if(F){F()}};m(D,w,A);return D}var x={};var r=function(H){var G=false;var F=function(){if(G){D.unbind(f,F)}if(t>0){D.each(function(){this.style[q.transition]=(x[this]||null)})}if(typeof C==="function"){C.apply(D)}if(typeof H==="function"){H()}};if((t>0)&&(f)&&(k.transit.useTransitionEnd)){G=true;D.bind(f,F)}else{window.setTimeout(F,t)}D.each(function(){if(t>0){this.style[q.transition]=E}k(this).css(z)})};var v=function(F){this.offsetWidth;r(F)};m(D,w,v);return this};function n(s,r){if(!r){k.cssNumber[s]=true}k.transit.propertyMap[s]=q.transform;k.cssHooks[s]={get:function(v){var u=k(v).css("transit:transform");return u.get(s)},set:function(v,w){var u=k(v).css("transit:transform");u.setFromString(s,w);k(v).css({"transit:transform":u})}}}function c(r){return r.replace(/([A-Z])/g,function(s){return"-"+s.toLowerCase()})}function o(s,r){if((typeof s==="string")&&(!s.match(/^[\-0-9\.]+$/))){return s}else{return""+s+r}}function l(s){var r=s;if(k.fx.speeds[r]){r=k.fx.speeds[r]}return o(r,"ms")}k.transit.getTransitionValue=g})(jQuery);
/*!
 * 一切动画入口
 */
;$(function() {
	var endTransition = 'webkitTransitionEnd transitionend',
		endAnimation = 'webkitAnimationEnd animationend',
		wait = function(fn, delay) {
			if(typeof fn != 'function') {
				throw(' Parameter is not a function ');
			}

			setTimeout(fn, delay || 2e3);
		},
		transit = function (object, position, time, method) {
			// 动画一
			if( !method ) {
				object.transit({x: 4, y: - 20 / 400 * position,  opacity: 1},                   time *  5  / 100, 'linear')
					  .transit({x: 2, y: - 40 / 400 * position,  rotate: '10deg'},              time * 10  / 100, 'linear')
					  .transit({x: 7, y: - 80 / 400 * position,  rotate: '14deg', scale: 0.7},  time * 20  / 100, 'linear')
					  .transit({x: 5, y: -120 / 400 * position,  rotate: '15deg'},              time * 30  / 100, 'linear')
					  .transit({x: 1, y: -160 / 400 * position,  rotate: '20deg'},              time * 40  / 100, 'linear')
					  .transit({x: 2, y: -200 / 400 * position,  rotate: '18deg', scale:1.1},   time * 50  / 100, 'linear')
					  .transit({x: 3, y: -240 / 400 * position,  rotate: '22deg', opacity:0.4}, time * 60  / 100, 'linear')
					  .transit({x: 0, y: -400 / 400 * position,  opacity: 1},                   time * 100 / 100, 'linear');
			} else {
				object.transit({x: 2, y: - 20 / 200 * position, opacity: 1},  time * 13  / 100, 'linear')
					  .transit({x: 5, y: - 40 / 200 * position, scale: 1.2},  time * 20  / 100, 'linear')
					  .transit({x: 0, y: - 60 / 200 * position},              time * 30  / 100, 'linear')
					  .transit({x: 8, y: - 80 / 200 * position},              time * 40  / 100, 'linear')
					  .transit({x: 3, y: -100 / 200 * position},   			  time * 50  / 100, 'linear')
					  .transit({x: 8, y: -120 / 200 * position}, 			  time * 60  / 100, 'linear')
					  .transit({opacity: 0.4},                   			  time * 65  / 100, 'linear')
					  .transit({x: 0, y: -200 / 200 * position, opacity: 1},  time * 80  / 100, 'linear');
			}
		},
		fadeText = function(object, sTime, delay) {
			delay = delay || 2e3;

			object.each(function(i) {
				var $this = $(this);
				wait(function() {
					$this.transit({'opacity': 1});
				}, sTime + delay * i)
			})
			.on(endAnimation, function(){
				$(this).removeClass('fadeIn animated');
			});
		},
		smoothScroll,
		page1Start, wordsBegin,
		page2Start, bubbleBengin,
		page3Start,
		page4Start,
		page5Start,
		readyDelay = 2000;

	/*
	 * Smooth scrolling page
	 */
	;(function() {
		if(navigator.userAgent.toLowerCase().indexOf('iphone') >= 0) {
			$('.pages').addClass('ios')
		} else {
			$('.pages').addClass('android')
		}

		// 1. Page scrollbar is prohibited
		$(document).on('touchmove', function() {
			return false;
		});
		
		// 2. Computational pages total height
		var $pages = $('.pages');
		var height = $(document).height(); // height bug
		var page = $pages.find('>div').css('height', height).length;
		var number = 0; // 第几页
		var heights = [];
		var count = 0;

		// Statistical total height
		for( var i = 0; i < page; i++ ) {
			count += height;
			heights.push(height);
		}
		
		// long screen height fix
		heights[1] = Math.floor(440 / 1165 * height + height);
		count = count - height + heights[1];
		$('.page2').css('height', heights[1]);

		// pages
		$pages.css({
			'height': count,
			// '-webkit-transform': 'transition(0, '+ - height * number + 'px',
			'y': - scrollTop()
		});
		
		function scroll( callback ) {
			$pages.transition({ y: - scrollTop()}, 5000, 'linear');
			callback && callback();
		}
		
		function scrollTop() {
			return count = count - heights[number++]
		}

		smoothScroll = scroll;
	})();

	/*
	 * The first page of the animation
	 */
	;(function() {
		// To the page1 binding touchmove events
		var touchY, moving = false;

		// 新增点击可以触发效果
		$('.page1').on('touchstart', function(e) {
			touchY = e.originalEvent.touches[0].clientY;
		})
		.on('touchmove', function(e) {
			// 如果监听有拉动感，
			moving = true;
		})
		.on('touchend', function(e) {
			if( moving ) {
				if( Math.abs(e.originalEvent.changedTouches[0].clientY - touchY) > 50 ) { 
					next();
				}
				moving = false
			} else {
				next();
			}
		});

		function next() {
			// 播放背景音乐
			try {
				$('#bgmusic').get(0).play();
				$('#music').css({'visibility': 'visible', opacity: .8});
				$('#music .music').addClass('play');
			} catch(e) {}
			// 触发统计
			try{
				Tracker.addAll(window.inPage.start)
			} catch(e) {}

			smoothScroll(function() {
				page2Start()
			});
			$('.page1').off('touchstart touchmove touchend');
		}

		function play() {
			$('.page1').addClass('on');
			wordsBegin();
			penBegin();
			$('.typer').addClass('on');
			$('.timing').addClass('on');
			wait(function() {
				$('.logo').addClass('on');
			}, 350);
		}

		page1Start = play;
	})();

	/*
	 * Typewriter animation
	 */
	;(function() {
		// document.location.search
		function getQueryString() {
	    	var result = {}, queryString = window.location.search.substring(1),
	       		re = /([^&=]+)=([^&]*)/g, m;
		    while (m = re.exec(queryString)) {
		        result[decodeURIComponent(m[1])] = decodeURIComponent(m[2]);
		    }
	    	return result;
		}

		var target = getQueryString();
		var app = {
			text: '致 ' + ( target['name'] ? target['name'] : '小丸子' ),
			index: 0,
			chars: 0,
			speed: 650,
			container: '.typer .content',
			init: function() {
			  	this.chars = this.text.length;
			  	return this.write();
			},
			write: function() {
				var blanks = this.text[this.index];
				blanks = (blanks == ' ') ? '<i></i>' : blanks;
		  		$(this.container).append( blanks );
		  		if (this.index < this.chars) {
		    		this.index++;
		    		return window.setTimeout(function() {
		      			return app.write();
		    		}, this.speed);
		  		}
			}
		};

		penBegin = function() {
			app.init();
		}
	})();

	/*
	 * Loaded words
	 */
	;(function() {
		var words = document.getElementsByClassName('word');
		var wordArray = [];
		var currentWord = 0;

		words[currentWord].style.opacity = 1;
		for (var i = 0; i < words.length; i++) {
			splitLetters(words[i]);
		}

		function changeWord() {
			var cw = wordArray[currentWord];
			var nw = currentWord == words.length-1 ? wordArray[0] : wordArray[currentWord+1];
			for (var i = 0; i < cw.length; i++) {
				animateLetterOut(cw, i);
			}
			
			for (var i = 0; i < nw.length; i++) {
				nw[i].className = 'letter behind';
				nw[0].parentElement.style.opacity = 1;
				animateLetterIn(nw, i);
			}

			currentWord = (currentWord == wordArray.length-1) ? 0 : currentWord+1;

		}

		function animateLetterOut(cw, i) {
		  	setTimeout(function() {
				cw[i].className = 'letter out';
		  	}, i*80);
		}

		function animateLetterIn(nw, i) {
		  	setTimeout(function() {
				nw[i].className = 'letter in';
		  	}, 340+(i*80));
		}

		function splitLetters(word) {
		  	var content = word.innerHTML;
		  	word.innerHTML = '';
		  	var letters = [];
		  	for (var i = 0; i < content.length; i++) {
		    	var letter = document.createElement('span');
		    	letter.className = 'letter';
		    	letter.innerHTML = content.charAt(i);
		    	word.appendChild(letter);
		    	letters.push(letter);
		  	}
		  
		  	wordArray.push(letters);
		}

		function loop(fn, count) {
			if(count <= 0) return;

			fn();
			setTimeout(function() {
				loop(fn, --count)
			}, 4e3);
		}

		/* throw out */
		
		wordsBegin = function() {
			loop(changeWord, 2)
		}
	})();

	/*
	 * The second page of the animation
	 */
	;(function() {
		function play() {
			$('.page2').addClass('on');
			// 气泡
			bubbleBengin();
			// 遮罩取消 -- 添加on 无效果
			$('.deer').addClass('on');
			// 用户
			wait(function(){
				$('.lines').addClass('on');
				$('.user').addClass('on');
			}, 1e3);
			wait(function() {
				// 气泡往上走，引出下一个场景
				cohesive();
			}, 13e3);
			// 线引导
			// wait(function(){
			// 	$('.links').addClass('on')
			// 	.on(endAnimation, function() {
			// 		// link animate bug
			// 		$('.links').off(endAnimation);
			// 		// 气泡往上走，引出下一个场景
			// 		cohesive();
			// 	})
			// }, 5e3);
			// 文字
			fadeText($('.text1>div'), 7e3);
			// 播放下一页动画
			wait(function(){
				smoothScroll(function() {
					page3Start();
				});
			}, 14e3);
		}

		function cohesive() {
			var height = $(document).height(),
				b3 = $('.b3');
			// 衔接动画
			// transit(b3, height * (1 - 10 / 100) - b3.outerHeight(), 2e3);
			b3.addClass('zoomIn animated on');
			wait(function() {
				// b3 停止不动bug
				b3.removeClass('zoomIn animated');
				transit(b3, height - b3.outerHeight(), 16e2);
				// 这里的.1是解决transform-originBug问题 
				b3.transit({scale: .1, opacity: 0});
			}, 1e3);
		}

		page2Start = play;
	})();

	/*
	 * Bubble animation
	 */
	;(function() {
		function animate() {
			var height = $(document).height();
			var b1 = $('.b1'), b2 = $('.b2'), b3 = $('.b3'), b4 = $('.b4'), b5 = $('.b5'), b6 = $('.b6');

			transit(b1, height * (1 -  4.606 / 100) - b1.outerHeight(), 2e3);
			transit(b2, height * (1 -  7.067 / 100) - b2.outerHeight(), 15e2, true);
			// transit(b3, height * (1 - 18.455 / 100) - b3.outerHeight(), 2e3);
			transit(b4, height * (1 + 10.893 / 100) - b4.outerHeight(), 1e3, true);
			transit(b5, height * (1 +  0.004 / 100) - b5.outerHeight(), 3e3);
			transit(b6, height * (1 -  1.923 / 100) - b6.outerHeight(), 15e2, true);
		}

		bubbleBengin = function() {
			wait(animate, 500)
		};
	})();

	/*
	 * The third page of the animation
	 */
	;(function() {
		function play() {
			$('.page3').addClass('on');
			wait(function(){
				$('.hand').addClass('on');
			}, 55e2);
			wait(function() {
				$('.butterflys, .butterfly').addClass('zoomIn animated on')
				.on(endAnimation, function() {
					$(this).removeClass('zoomIn animated')
				});
			}, 6e3);
			// // 单个蝴蝶
			// wait(function() {
			// 	$('.butterfly').addClass('on');
			// }, 7e3);
			// 文字2
			fadeText($('.text2>div'), 65e2);
			wait(function(){
				$('.butterfly').addClass('animate');
			}, 12e3)
			wait(function(){
				smoothScroll(function() {
					page4Start()
				});
			}, 14e3);
		}

		page3Start = play;
	})();

	/*
	 * The fourth page of the animation
	 */
	;(function() {
		function play() {
			// 衔接动画
			var bfly = $('#butterfly'),
				position = 600,
				time = 2000;
			bfly.transit({x: 4, y: - 20 / 400 * position,  scale: 0.5},  	  time *  5  / 100, 'linear')
				.transit({x: 2, y: - 40 / 400 * position,  rotate: '10deg'},  time * 10  / 100, 'linear')
				.transit({x: 7, y: - 80 / 400 * position,  rotate: '14deg'},  time * 20  / 100, 'linear')
				.transit({x: 5, y: -120 / 400 * position,  rotate: '15deg'},  time * 30  / 100, 'linear')
				.transit({x: 1, y: -160 / 400 * position,  rotate: '20deg'},  time * 40  / 100, 'linear')
				.transit({x: 2, y: -200 / 400 * position,  rotate: '18deg'},  time * 50  / 100, 'linear')
				.transit({x: 3, y: -240 / 400 * position,  rotate: '22deg'},  time * 60  / 100, 'linear')
				.transit({x: 0, y: -400 / 400 * position,  scale: 0.5, opacity: 0},		  time * 100 / 100, 'linear')

			wait(function() {
				$('.hub').addClass('on');
			}, 7500);
			// 文字2
			fadeText($('.text3>div'), 8e3);

			wait(function() {
				page5Start()
			}, 11e3);
		}

		page4Start = play;
	})();

	/*
	 * Finally ended
	 */
	;(function() {
		function play() {
			wait(function() {
				// 加载最后一页
				$('.page-invite').addClass('on');
				$('.pages').addClass('off');

				wait(function() {
					$('.pages').hide();
				}, 1000)
			}, 6000);
			wait(function() {
				// 加载文字
				fadeText( $('.text4>div:lt(3)'), 1e3, 3e3 );
				wait(function() {$('.open-btn').addClass('on'); $('.music').removeClass('play');}, 7e3);
				// 弹层
				$('.page-invite').on('click', function(e) {
					if( !$(e.target).is('.location') ){
						if( !$('.location').is('.open') ) {
							// 触发统计
							try{
								Tracker.addAll(window.inPage.open)
							} catch(e) {}
						}
						
						$('.location, .mask').addClass('open');
					}
				});
				$('.location, .mask').on('click', function() {
					$('.location, .mask').removeClass('open');
					return false;
				});
			}, 7500);
		}

		page5Start = play;
	})();

	window.inPage = window.inPage || {};
	window.inPage.onStart = page1Start;
});

