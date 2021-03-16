﻿(function(I,Q){var Z=Math,F=Q.createElement("div").style,N=(function(){var c="t,webkitT,MozT,msT,OT".split(","),b,d=0,a=c.length;for(;d<a;d++){b=c[d]+"ransform";if(b in F){return c[d].substr(0,c[d].length-1)}}return false})(),X=N?"-"+N.toLowerCase()+"-":"",L=af("transform"),ab=af("transitionProperty"),P=af("transitionDuration"),M=af("transformOrigin"),S=af("transitionTimingFunction"),ae=af("transitionDelay"),W=(/android/gi).test(navigator.appVersion),J=(/iphone|ipad/gi).test(navigator.appVersion),H=(/hp-tablet/gi).test(navigator.appVersion),aa=af("perspective") in F,R="ontouchstart" in I&&!H,T=N!==false,Y=af("transition") in F,O="onorientationchange" in I?"orientationchange":"resize",V=R?"touchstart":"mousedown",ah=R?"touchmove":"mousemove",ag=R?"touchend":"mouseup",ac=R?"touchcancel":"mouseup",G=(function(){if(N===false){return false}var a={"":"transitionend",webkit:"webkitTransitionEnd",Moz:"transitionend",O:"otransitionend",ms:"MSTransitionEnd"};return a[N]})(),ad=(function(){return I.requestAnimationFrame||I.webkitRequestAnimationFrame||I.mozRequestAnimationFrame||I.oRequestAnimationFrame||I.msRequestAnimationFrame||function(a){return setTimeout(a,1)}})(),U=(function(){return I.cancelRequestAnimationFrame||I.webkitCancelAnimationFrame||I.webkitCancelRequestAnimationFrame||I.mozCancelRequestAnimationFrame||I.oCancelRequestAnimationFrame||I.msCancelRequestAnimationFrame||clearTimeout})(),m=aa?" translateZ(0)":"",K=function(c,b){var d=this,a;d.wrapper=typeof c=="object"?c:Q.getElementById(c);d.wrapper.style.overflow="hidden";d.scroller=d.wrapper.children[0];d.options={hScroll:true,vScroll:true,x:0,y:0,bounce:true,bounceLock:false,momentum:true,lockDirection:true,useTransform:true,useTransition:false,topOffset:0,checkDOMChanges:false,handleClick:true,hScrollbar:true,vScrollbar:true,fixedScrollbar:W,hideScrollbar:J,fadeScrollbar:J&&aa,scrollbarClass:"",zoom:false,zoomMin:1,zoomMax:4,doubleTapZoom:2,wheelAction:"scroll",snap:false,snapThreshold:1,onRefresh:null,onBeforeScrollStart:function(e){e.preventDefault()},onScrollStart:null,onBeforeScrollMove:null,onScrollMove:null,onBeforeScrollEnd:null,onScrollEnd:null,onTouchEnd:null,onDestroy:null,onZoomStart:null,onZoom:null,onZoomEnd:null};for(a in b){d.options[a]=b[a]}d.x=d.options.x;d.y=d.options.y;d.options.useTransform=T&&d.options.useTransform;d.options.hScrollbar=d.options.hScroll&&d.options.hScrollbar;d.options.vScrollbar=d.options.vScroll&&d.options.vScrollbar;d.options.zoom=d.options.useTransform&&d.options.zoom;d.options.useTransition=Y&&d.options.useTransition;if(d.options.zoom&&W){m=""}d.scroller.style[ab]=d.options.useTransform?X+"transform":"top left";d.scroller.style[P]="0";d.scroller.style[M]="0 0";if(d.options.useTransition){d.scroller.style[S]="cubic-bezier(0.33,0.66,0.66,1)"}if(d.options.useTransform){d.scroller.style[L]="translate("+d.x+"px,"+d.y+"px)"+m}else{d.scroller.style.cssText+=";position:absolute;top:"+d.y+"px;left:"+d.x+"px"}if(d.options.useTransition){d.options.fixedScrollbar=true}d.refresh();d._bind(O,I);d._bind(V);if(!R){if(d.options.wheelAction!="none"){d._bind("DOMMouseScroll");d._bind("mousewheel")}}if(d.options.checkDOMChanges){d.checkDOMTime=setInterval(function(){d._checkDOMChanges()},500)}};K.prototype={enabled:true,x:0,y:0,steps:[],scale:1,currPageX:0,currPageY:0,pagesX:[],pagesY:[],aniTime:null,wheelZoomCount:0,handleEvent:function(b){var a=this;switch(b.type){case V:if(!R&&b.button!==0){return}a._start(b);break;case ah:a._move(b);break;case ag:case ac:a._end(b);break;case O:a._resize();break;case"DOMMouseScroll":case"mousewheel":a._wheel(b);break;case G:a._transitionEnd(b);break}},_checkDOMChanges:function(){if(this.moved||this.zoomed||this.animating||(this.scrollerW==this.scroller.offsetWidth*this.scale&&this.scrollerH==this.scroller.offsetHeight*this.scale)){return}this.refresh()},_scrollbar:function(b){var a=this,c;if(!a[b+"Scrollbar"]){if(a[b+"ScrollbarWrapper"]){if(T){a[b+"ScrollbarIndicator"].style[L]=""}a[b+"ScrollbarWrapper"].parentNode.removeChild(a[b+"ScrollbarWrapper"]);a[b+"ScrollbarWrapper"]=null;a[b+"ScrollbarIndicator"]=null}return}if(!a[b+"ScrollbarWrapper"]){c=Q.createElement("div");if(a.options.scrollbarClass){c.className=a.options.scrollbarClass+b.toUpperCase()}else{c.style.cssText="position:absolute;z-index:100;"+(b=="h"?"height:7px;bottom:1px;left:2px;right:"+(a.vScrollbar?"7":"2")+"px":"width:7px;bottom:"+(a.hScrollbar?"7":"2")+"px;top:2px;right:1px")}c.style.cssText+=";pointer-events:none;"+X+"transition-property:opacity;"+X+"transition-duration:"+(a.options.fadeScrollbar?"350ms":"0")+";overflow:hidden;opacity:"+(a.options.hideScrollbar?"0":"1");a.wrapper.appendChild(c);a[b+"ScrollbarWrapper"]=c;c=Q.createElement("div");if(!a.options.scrollbarClass){c.style.cssText="position:absolute;z-index:100;background:rgba(0,0,0,0.5);border:1px solid rgba(255,255,255,0.9);"+X+"background-clip:padding-box;"+X+"box-sizing:border-box;"+(b=="h"?"height:100%":"width:100%")+";"+X+"border-radius:3px;border-radius:3px"}c.style.cssText+=";pointer-events:none;"+X+"transition-property:"+X+"transform;"+X+"transition-timing-function:cubic-bezier(0.33,0.66,0.66,1);"+X+"transition-duration:0;"+X+"transform: translate(0,0)"+m;if(a.options.useTransition){c.style.cssText+=";"+X+"transition-timing-function:cubic-bezier(0.33,0.66,0.66,1)"}a[b+"ScrollbarWrapper"].appendChild(c);a[b+"ScrollbarIndicator"]=c}if(b=="h"){a.hScrollbarSize=a.hScrollbarWrapper.clientWidth;a.hScrollbarIndicatorSize=Z.max(Z.round(a.hScrollbarSize*a.hScrollbarSize/a.scrollerW),8);a.hScrollbarIndicator.style.width=a.hScrollbarIndicatorSize+"px";a.hScrollbarMaxScroll=a.hScrollbarSize-a.hScrollbarIndicatorSize;a.hScrollbarProp=a.hScrollbarMaxScroll/a.maxScrollX}else{a.vScrollbarSize=a.vScrollbarWrapper.clientHeight;a.vScrollbarIndicatorSize=Z.max(Z.round(a.vScrollbarSize*a.vScrollbarSize/a.scrollerH),8);a.vScrollbarIndicator.style.height=a.vScrollbarIndicatorSize+"px";a.vScrollbarMaxScroll=a.vScrollbarSize-a.vScrollbarIndicatorSize;a.vScrollbarProp=a.vScrollbarMaxScroll/a.maxScrollY}a._scrollbarPos(b,true)},_resize:function(){var a=this;setTimeout(function(){a.refresh()},W?200:0)},_pos:function(a,b){if(this.zoomed){return}a=this.hScroll?a:0;b=this.vScroll?b:0;if(this.options.useTransform){this.scroller.style[L]="translate("+a+"px,"+b+"px) scale("+this.scale+")"+m}else{a=Z.round(a);b=Z.round(b);this.scroller.style.left=a+"px";this.scroller.style.top=b+"px"}this.x=a;this.y=b;this._scrollbarPos("h");this._scrollbarPos("v")},_scrollbarPos:function(c,d){var e=this,a=c=="h"?e.x:e.y,b;if(!e[c+"Scrollbar"]){return}a=e[c+"ScrollbarProp"]*a;if(a<0){if(!e.options.fixedScrollbar){b=e[c+"ScrollbarIndicatorSize"]+Z.round(a*3);if(b<8){b=8}e[c+"ScrollbarIndicator"].style[c=="h"?"width":"height"]=b+"px"}a=0}else{if(a>e[c+"ScrollbarMaxScroll"]){if(!e.options.fixedScrollbar){b=e[c+"ScrollbarIndicatorSize"]-Z.round((a-e[c+"ScrollbarMaxScroll"])*3);if(b<8){b=8}e[c+"ScrollbarIndicator"].style[c=="h"?"width":"height"]=b+"px";a=e[c+"ScrollbarMaxScroll"]+(e[c+"ScrollbarIndicatorSize"]-b)}else{a=e[c+"ScrollbarMaxScroll"]}}}e[c+"ScrollbarWrapper"].style[ae]="0";e[c+"ScrollbarWrapper"].style.opacity=d&&e.options.hideScrollbar?"0":"1";e[c+"ScrollbarIndicator"].style[L]="translate("+(c=="h"?a+"px,0)":"0,"+a+"px)")+m},_start:function(f){var d=this,e=R?f.touches[0]:f,c,h,a,b,g;if(!d.enabled){return}if(d.options.onBeforeScrollStart){d.options.onBeforeScrollStart.call(d,f)}if(d.options.useTransition||d.options.zoom){d._transitionTime(0)}d.moved=false;d.animating=false;d.zoomed=false;d.distX=0;d.distY=0;d.absDistX=0;d.absDistY=0;d.dirX=0;d.dirY=0;if(d.options.zoom&&R&&f.touches.length>1){b=Z.abs(f.touches[0].pageX-f.touches[1].pageX);g=Z.abs(f.touches[0].pageY-f.touches[1].pageY);d.touchesDistStart=Z.sqrt(b*b+g*g);d.originX=Z.abs(f.touches[0].pageX+f.touches[1].pageX-d.wrapperOffsetLeft*2)/2-d.x;d.originY=Z.abs(f.touches[0].pageY+f.touches[1].pageY-d.wrapperOffsetTop*2)/2-d.y;if(d.options.onZoomStart){d.options.onZoomStart.call(d,f)}}if(d.options.momentum){if(d.options.useTransform){c=getComputedStyle(d.scroller,null)[L].replace(/[^0-9\-.,]/g,"").split(",");h=+(c[12]||c[4]);a=+(c[13]||c[5])}else{h=+getComputedStyle(d.scroller,null).left.replace(/[^0-9-]/g,"");a=+getComputedStyle(d.scroller,null).top.replace(/[^0-9-]/g,"")}if(h!=d.x||a!=d.y){if(d.options.useTransition){d._unbind(G)}else{U(d.aniTime)}d.steps=[];d._pos(h,a);if(d.options.onScrollEnd){d.options.onScrollEnd.call(d)}}}d.absStartX=d.x;d.absStartY=d.y;d.startX=d.x;d.startY=d.y;d.pointX=e.pageX;d.pointY=e.pageY;d.startTime=f.timeStamp||Date.now();if(d.options.onScrollStart){d.options.onScrollStart.call(d,f)}d._bind(ah,I);d._bind(ag,I);d._bind(ac,I)},_move:function(i){var b=this,g=R?i.touches[0]:i,e=g.pageX-b.pointX,f=g.pageY-b.pointY,c=b.x+e,j=b.y+f,d,h,k,a=i.timeStamp||Date.now();if(b.options.onBeforeScrollMove){b.options.onBeforeScrollMove.call(b,i)}if(b.options.zoom&&R&&i.touches.length>1){d=Z.abs(i.touches[0].pageX-i.touches[1].pageX);h=Z.abs(i.touches[0].pageY-i.touches[1].pageY);b.touchesDist=Z.sqrt(d*d+h*h);b.zoomed=true;k=1/b.touchesDistStart*b.touchesDist*this.scale;if(k<b.options.zoomMin){k=0.5*b.options.zoomMin*Math.pow(2,k/b.options.zoomMin)}else{if(k>b.options.zoomMax){k=2*b.options.zoomMax*Math.pow(0.5,b.options.zoomMax/k)}}b.lastScale=k/this.scale;c=this.originX-this.originX*b.lastScale+this.x;j=this.originY-this.originY*b.lastScale+this.y;this.scroller.style[L]="translate("+c+"px,"+j+"px) scale("+k+")"+m;if(b.options.onZoom){b.options.onZoom.call(b,i)}return}b.pointX=g.pageX;b.pointY=g.pageY;if(c>0||c<b.maxScrollX){c=b.options.bounce?b.x+(e/2):c>=0||b.maxScrollX>=0?0:b.maxScrollX}if(j>b.minScrollY||j<b.maxScrollY){j=b.options.bounce?b.y+(f/2):j>=b.minScrollY||b.maxScrollY>=0?b.minScrollY:b.maxScrollY}b.distX+=e;b.distY+=f;b.absDistX=Z.abs(b.distX);b.absDistY=Z.abs(b.distY);if(b.absDistX<6&&b.absDistY<6){return}if(b.options.lockDirection){if(b.absDistX>b.absDistY+5){j=b.y;f=0}else{if(b.absDistY>b.absDistX+5){c=b.x;e=0}}}b.moved=true;b._pos(c,j);b.dirX=e>0?-1:e<0?1:0;b.dirY=f>0?-1:f<0?1:0;if(a-b.startTime>300){b.startTime=a;b.startX=b.x;b.startY=b.y}if(b.options.onScrollMove){b.options.onScrollMove.call(b,i)}},_end:function(o){if(R&&o.touches.length!==0){return}var i=this,k=R?o.changedTouches[0]:o,b,e,f={dist:0,time:0},g={dist:0,time:0},p=(o.timeStamp||Date.now())-i.startTime,c=i.x,j=i.y,l,n,d,a,h;i._unbind(ah,I);i._unbind(ag,I);i._unbind(ac,I);if(i.options.onBeforeScrollEnd){i.options.onBeforeScrollEnd.call(i,o)}if(i.zoomed){h=i.scale*i.lastScale;h=Math.max(i.options.zoomMin,h);h=Math.min(i.options.zoomMax,h);i.lastScale=h/i.scale;i.scale=h;i.x=i.originX-i.originX*i.lastScale+i.x;i.y=i.originY-i.originY*i.lastScale+i.y;i.scroller.style[P]="200ms";i.scroller.style[L]="translate("+i.x+"px,"+i.y+"px) scale("+i.scale+")"+m;i.zoomed=false;i.refresh();if(i.options.onZoomEnd){i.options.onZoomEnd.call(i,o)}return}if(!i.moved){if(R){if(i.doubleTapTimer&&i.options.zoom){clearTimeout(i.doubleTapTimer);i.doubleTapTimer=null;if(i.options.onZoomStart){i.options.onZoomStart.call(i,o)}i.zoom(i.pointX,i.pointY,i.scale==1?i.options.doubleTapZoom:1);if(i.options.onZoomEnd){setTimeout(function(){i.options.onZoomEnd.call(i,o)},200)}}else{if(this.options.handleClick){i.doubleTapTimer=setTimeout(function(){i.doubleTapTimer=null;b=k.target;while(b.nodeType!=1){b=b.parentNode}if(b.tagName!="SELECT"&&b.tagName!="INPUT"&&b.tagName!="TEXTAREA"){e=Q.createEvent("MouseEvents");e.initMouseEvent("click",true,true,o.view,1,k.screenX,k.screenY,k.clientX,k.clientY,o.ctrlKey,o.altKey,o.shiftKey,o.metaKey,0,null);e._fake=true;b.dispatchEvent(e)}},i.options.zoom?250:0)}}}i._resetPos(400);if(i.options.onTouchEnd){i.options.onTouchEnd.call(i,o)}return}if(p<300&&i.options.momentum){f=c?i._momentum(c-i.startX,p,-i.x,i.scrollerW-i.wrapperW+i.x,i.options.bounce?i.wrapperW:0):f;g=j?i._momentum(j-i.startY,p,-i.y,(i.maxScrollY<0?i.scrollerH-i.wrapperH+i.y-i.minScrollY:0),i.options.bounce?i.wrapperH:0):g;c=i.x+f.dist;j=i.y+g.dist;if((i.x>0&&c>0)||(i.x<i.maxScrollX&&c<i.maxScrollX)){f={dist:0,time:0}}if((i.y>i.minScrollY&&j>i.minScrollY)||(i.y<i.maxScrollY&&j<i.maxScrollY)){g={dist:0,time:0}}}if(f.dist||g.dist){d=Z.max(Z.max(f.time,g.time),10);if(i.options.snap){l=c-i.absStartX;n=j-i.absStartY;if(Z.abs(l)<i.options.snapThreshold&&Z.abs(n)<i.options.snapThreshold){i.scrollTo(i.absStartX,i.absStartY,200)}else{a=i._snap(c,j);c=a.x;j=a.y;d=Z.max(a.time,d)}}i.scrollTo(Z.round(c),Z.round(j),d);if(i.options.onTouchEnd){i.options.onTouchEnd.call(i,o)}return}if(i.options.snap){l=c-i.absStartX;n=j-i.absStartY;if(Z.abs(l)<i.options.snapThreshold&&Z.abs(n)<i.options.snapThreshold){i.scrollTo(i.absStartX,i.absStartY,200)}else{a=i._snap(i.x,i.y);if(a.x!=i.x||a.y!=i.y){i.scrollTo(a.x,a.y,a.time)}}if(i.options.onTouchEnd){i.options.onTouchEnd.call(i,o)}return}i._resetPos(200);if(i.options.onTouchEnd){i.options.onTouchEnd.call(i,o)}},_resetPos:function(b){var d=this,c=d.x>=0?0:d.x<d.maxScrollX?d.maxScrollX:d.x,a=d.y>=d.minScrollY||d.maxScrollY>0?d.minScrollY:d.y<d.maxScrollY?d.maxScrollY:d.y;if(c==d.x&&a==d.y){if(d.moved){d.moved=false;if(d.options.onScrollEnd){d.options.onScrollEnd.call(d)}}if(d.hScrollbar&&d.options.hideScrollbar){if(N=="webkit"){d.hScrollbarWrapper.style[ae]="300ms"}d.hScrollbarWrapper.style.opacity="0"}if(d.vScrollbar&&d.options.hideScrollbar){if(N=="webkit"){d.vScrollbarWrapper.style[ae]="300ms"}d.vScrollbarWrapper.style.opacity="0"}return}d.scrollTo(c,a,b||0)},_wheel:function(d){var b=this,e,c,f,g,a;if("wheelDeltaX" in d){e=d.wheelDeltaX/12;c=d.wheelDeltaY/12}else{if("wheelDelta" in d){e=c=d.wheelDelta/12}else{if("detail" in d){e=c=-d.detail*3}else{return}}}if(b.options.wheelAction=="zoom"){a=b.scale*Math.pow(2,1/3*(c?c/Math.abs(c):0));if(a<b.options.zoomMin){a=b.options.zoomMin}if(a>b.options.zoomMax){a=b.options.zoomMax}if(a!=b.scale){if(!b.wheelZoomCount&&b.options.onZoomStart){b.options.onZoomStart.call(b,d)}b.wheelZoomCount++;b.zoom(d.pageX,d.pageY,a,400);setTimeout(function(){b.wheelZoomCount--;if(!b.wheelZoomCount&&b.options.onZoomEnd){b.options.onZoomEnd.call(b,d)}},400)}return}f=b.x+e;g=b.y+c;if(f>0){f=0}else{if(f<b.maxScrollX){f=b.maxScrollX}}if(g>b.minScrollY){g=b.minScrollY}else{if(g<b.maxScrollY){g=b.maxScrollY}}if(b.maxScrollY<0){b.scrollTo(f,g,0)}},_transitionEnd:function(b){var a=this;if(b.target!=a.scroller){return}a._unbind(G);a._startAni()},_startAni:function(){var d=this,b=d.x,a=d.y,e=Date.now(),f,g,c;if(d.animating){return}if(!d.steps.length){d._resetPos(400);return}f=d.steps.shift();if(f.x==b&&f.y==a){f.time=0}d.animating=true;d.moved=true;if(d.options.useTransition){d._transitionTime(f.time);d._pos(f.x,f.y);d.animating=false;if(f.time){d._bind(G)}else{d._resetPos(0)}return}c=function(){var i=Date.now(),j,h;if(i>=e+f.time){d._pos(f.x,f.y);d.animating=false;if(d.options.onAnimationEnd){d.options.onAnimationEnd.call(d)}d._startAni();return}i=(i-e)/f.time-1;g=Z.sqrt(1-i*i);j=(f.x-b)*g+b;h=(f.y-a)*g+a;d._pos(j,h);if(d.animating){d.aniTime=ad(c)}};c()},_transitionTime:function(a){a+="ms";this.scroller.style[P]=a;if(this.hScrollbar){this.hScrollbarIndicator.style[P]=a}if(this.vScrollbar){this.vScrollbarIndicator.style[P]=a}},_momentum:function(c,b,f,j,g){var d=0.0006,i=Z.abs(c)/b,e=(i*i)/(2*d),h=0,a=0;if(c>0&&e>f){a=g/(6/(e/i*d));f=f+a;i=i*f/e;e=f}else{if(c<0&&e>j){a=g/(6/(e/i*d));j=j+a;i=i*j/e;e=j}}e=e*(c<0?-1:1);h=i/d;return{dist:e,time:Z.round(h)}},_offset:function(a){var c=-a.offsetLeft,b=-a.offsetTop;while(a=a.offsetParent){c-=a.offsetLeft;b-=a.offsetTop}if(a!=this.wrapper){c*=this.scale;b*=this.scale}return{left:c,top:b}},_snap:function(i,a){var d=this,f,b,c,e,g,h;c=d.pagesX.length-1;for(f=0,b=d.pagesX.length;f<b;f++){if(i>=d.pagesX[f]){c=f;break}}if(c==d.currPageX&&c>0&&d.dirX<0){c--}i=d.pagesX[c];g=Z.abs(i-d.pagesX[d.currPageX]);g=g?Z.abs(d.x-i)/g*500:0;d.currPageX=c;c=d.pagesY.length-1;for(f=0;f<c;f++){if(a>=d.pagesY[f]){c=f;break}}if(c==d.currPageY&&c>0&&d.dirY<0){c--}a=d.pagesY[c];h=Z.abs(a-d.pagesY[d.currPageY]);h=h?Z.abs(d.y-a)/h*500:0;d.currPageY=c;e=Z.round(Z.max(g,h))||200;return{x:i,y:a,time:e}},_bind:function(a,b,c){(b||this.scroller).addEventListener(a,this,!!c)},_unbind:function(a,b,c){(b||this.scroller).removeEventListener(a,this,!!c)},destroy:function(){var a=this;a.scroller.style[L]="";a.hScrollbar=false;a.vScrollbar=false;a._scrollbar("h");a._scrollbar("v");a._unbind(O,I);a._unbind(V);a._unbind(ah,I);a._unbind(ag,I);a._unbind(ac,I);if(!a.options.hasTouch){a._unbind("DOMMouseScroll");a._unbind("mousewheel")}if(a.options.useTransition){a._unbind(G)}if(a.options.checkDOMChanges){clearInterval(a.checkDOMTime)}if(a.options.onDestroy){a.options.onDestroy.call(a)}},refresh:function(){var c=this,e,f,b,d,a=0,g=0;if(c.scale<c.options.zoomMin){c.scale=c.options.zoomMin}c.wrapperW=c.wrapper.clientWidth||1;c.wrapperH=c.wrapper.clientHeight||1;c.minScrollY=-c.options.topOffset||0;c.scrollerW=Z.round(c.scroller.offsetWidth*c.scale);c.scrollerH=Z.round((c.scroller.offsetHeight+c.minScrollY)*c.scale);c.maxScrollX=c.wrapperW-c.scrollerW;c.maxScrollY=c.wrapperH-c.scrollerH+c.minScrollY;c.dirX=0;c.dirY=0;if(c.options.onRefresh){c.options.onRefresh.call(c)}c.hScroll=c.options.hScroll&&c.maxScrollX<0;c.vScroll=c.options.vScroll&&(!c.options.bounceLock&&!c.hScroll||c.scrollerH>c.wrapperH);c.hScrollbar=c.hScroll&&c.options.hScrollbar;c.vScrollbar=c.vScroll&&c.options.vScrollbar&&c.scrollerH>c.wrapperH;e=c._offset(c.wrapper);c.wrapperOffsetLeft=-e.left;c.wrapperOffsetTop=-e.top;if(typeof c.options.snap=="string"){c.pagesX=[];c.pagesY=[];d=c.scroller.querySelectorAll(c.options.snap);for(f=0,b=d.length;f<b;f++){a=c._offset(d[f]);a.left+=c.wrapperOffsetLeft;a.top+=c.wrapperOffsetTop;c.pagesX[f]=a.left<c.maxScrollX?c.maxScrollX:a.left*c.scale;c.pagesY[f]=a.top<c.maxScrollY?c.maxScrollY:a.top*c.scale}}else{if(c.options.snap){c.pagesX=[];while(a>=c.maxScrollX){c.pagesX[g]=a;a=a-c.wrapperW;g++}if(c.maxScrollX%c.wrapperW){c.pagesX[c.pagesX.length]=c.maxScrollX-c.pagesX[c.pagesX.length-1]+c.pagesX[c.pagesX.length-1]}a=0;g=0;c.pagesY=[];while(a>=c.maxScrollY){c.pagesY[g]=a;a=a-c.wrapperH;g++}if(c.maxScrollY%c.wrapperH){c.pagesY[c.pagesY.length]=c.maxScrollY-c.pagesY[c.pagesY.length-1]+c.pagesY[c.pagesY.length-1]}}}c._scrollbar("h");c._scrollbar("v");if(!c.zoomed){c.scroller.style[P]="0";c._resetPos(400)}},scrollTo:function(h,a,d,f){var e=this,g=h,b,c;e.stop();if(!g.length){g=[{x:h,y:a,time:d,relative:f}]}for(b=0,c=g.length;b<c;b++){if(g[b].relative){g[b].x=e.x-g[b].x;g[b].y=e.y-g[b].y}e.steps.push({x:g[b].x,y:g[b].y,time:g[b].time||0})}e._startAni()},scrollToElement:function(c,d){var b=this,a;c=c.nodeType?c:b.scroller.querySelector(c);if(!c){return}a=b._offset(c);a.left+=b.wrapperOffsetLeft;a.top+=b.wrapperOffsetTop;a.left=a.left>0?0:a.left<b.maxScrollX?b.maxScrollX:a.left;a.top=a.top>b.minScrollY?b.minScrollY:a.top<b.maxScrollY?b.maxScrollY:a.top;d=d===undefined?Z.max(Z.abs(a.left)*2,Z.abs(a.top)*2):d;b.scrollTo(a.left,a.top,d)},scrollToPage:function(a,b,d){var c=this,e,f;d=d===undefined?400:d;if(c.options.onScrollStart){c.options.onScrollStart.call(c)}if(c.options.snap){a=a=="next"?c.currPageX+1:a=="prev"?c.currPageX-1:a;b=b=="next"?c.currPageY+1:b=="prev"?c.currPageY-1:b;a=a<0?0:a>c.pagesX.length-1?c.pagesX.length-1:a;b=b<0?0:b>c.pagesY.length-1?c.pagesY.length-1:b;c.currPageX=a;c.currPageY=b;e=c.pagesX[a];f=c.pagesY[b]}else{e=-c.wrapperW*a;f=-c.wrapperH*b;if(e<c.maxScrollX){e=c.maxScrollX}if(f<c.maxScrollY){f=c.maxScrollY}}c.scrollTo(e,f,d)},disable:function(){this.stop();this._resetPos(0);this.enabled=false;this._unbind(ah,I);this._unbind(ag,I);this._unbind(ac,I)},enable:function(){this.enabled=true},stop:function(){if(this.options.useTransition){this._unbind(G)}else{U(this.aniTime)}this.steps=[];this.moved=false;this.animating=false},zoom:function(e,f,a,c){var b=this,d=a/b.scale;if(!b.options.useTransform){return}b.zoomed=true;c=c===undefined?200:c;e=e-b.wrapperOffsetLeft-b.x;f=f-b.wrapperOffsetTop-b.y;b.x=e-e*d+b.x;b.y=f-f*d+b.y;b.scale=a;b.refresh();b.x=b.x>0?0:b.x<b.maxScrollX?b.maxScrollX:b.x;b.y=b.y>b.minScrollY?b.minScrollY:b.y<b.maxScrollY?b.maxScrollY:b.y;b.scroller.style[P]=c+"ms";b.scroller.style[L]="translate("+b.x+"px,"+b.y+"px) scale("+a+")"+m;b.zoomed=false},isReady:function(){return !this.moved&&!this.zoomed&&!this.animating}};function af(a){if(N===""){return a}a=a.charAt(0).toUpperCase()+a.substr(1);return N+a}F=null;if(typeof exports!=="undefined"){exports.iScroll=K}else{I.iScroll=K}})(window,document);