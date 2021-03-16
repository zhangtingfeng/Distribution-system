define([],function(){
    var Screen = Backbone.View.extend({
        initialize:function(){
            Screen.__super__.initialize();

            this.init();

        },
        init : function(){
        	var self = this;

        	//===== 判断客户端
        	self.equipment = new self.Env();

            // window.addEventListener('resize', isAPP, this);

            if(self.equipment.isAppMobile){
                console.log(self.equipment.isAppMobile + ":移动端");
            }else{

                // window.location.href= "./pc.html";

                // console.log(self.equipment.isAppMobile + ":网页端");
                // $("#loading").show();
                // // $("#fullscreen_tips").show();
                // $("#site-container").css("opacity","0");
            }
        	//===== 判断客户端

        	//===== 判断是否转屏
        	self.flipEquipment();

        	$(window).bind( 'orientationchange', function(e){
				self.flipEquipment();
			});
			//===== 判断是否转屏

        },
        flipEquipment : function(){
        	if (window.orientation == 0 || window.orientation == 180) {
				orientation = 'portrait';
				$("#fullscreen_tips").hide();
                $("#site-container").css("opacity","1");
				return false;
			}else if (window.orientation == 90 || window.orientation == -90) {
				orientation = 'landscape';
  				$("#fullscreen_tips").show();
                $("#site-container").css("opacity","0");
				return false;
			}
        },
        Env :function(){
            var f = "";
            var ua=navigator.userAgent.toLowerCase();

            function check(r){
                return r.test(ua);
            }

            var DOC = document;
            return {

              //判断环境，操作系统、浏览器、是否是https连接等
                isStrict : DOC.compatMode == "CSS1Compat",
                isOpera : check(/opera/),
                isChrome : check(/\bchrome\b/),
                isWebKit : check(/webkit/),
                isFirefox : check(/firefox/),
                isAppMobile : check(/iphone|ipod|android.*mobile|windows.*phone|blackberry.*mobile/i),
                isApple : check(/iphone|ipod|ipad/i),
                // isApple : check(/iphone/i),
                // isIpad : check(/ipad/i),
                isAndroid : check(/android/i),
                isSafari : !this.isChrome && check(/safari/),
                isSafari2 : this.isSafari && check(/applewebkit\/4/),
                isSafari3 : this.isSafari && check(/version\/3/),
                isSafari4 : this.isSafari && check(/version\/4/),
                isIE : !this.isOpera && check(/msie/),
                isIE7 : this.isIE && check(/msie 7/),
                isIE8 : this.isIE && check(/msie 8/),
                isIE6 : this.isIE && !isIE7 && !isIE8,
                isGecko : !this.isWebKit && check(/gecko/),
                isGecko2 : this.isGecko && check(/rv:1\.8/),
                isGecko3 : this.isGecko && check(/rv:1\.9/),
                isBorderBox : this.isIE && !isStrict,
                isWindows : check(/windows|win32/),
                isMac : check(/macintosh|mac os x/),
                isAir : check(/adobeair/),
                isLinux : check(/linux/),
                isSecure : /^https/i.test(window.location.protocol),
               /**
                 * 是否为空，如果允许allowBlank=true，则当v=''时返回true
                 */
                isEmpty : function(v, allowBlank){
                    return v === null || v === undefined || ((this.isArray(v) && !v.length)) || (!allowBlank ? v === '' : false);
                },

                /**
                 * 是否为数组类型
                 */
                isArray : function(v){
                    return toString.apply(v) === '[object Array]';
                },

                /**
                 * 是否为日期类型
                 */
                isDate : function(v){
                    return toString.apply(v) === '[object Date]';
                },

                /**
                 * 是否为Object类型
                 */
                isObject : function(v){
                    return !!v && Object.prototype.toString.call(v) === '[object Object]';
                },

                /**
                 * 判断是否是函数
                 */
                isFunction : function(v){
                    return toString.apply(v) === '[object Function]';
                },

                /**
                 * 判断是否为数字
                 */
                isNumber : function(v){
                    return typeof v === 'number' && isFinite(v);
                },

                /**
                 * 判断字符串类型
                 */
                isString : function(v){
                    return typeof v === 'string';
                },

                /**
                 * 判断布尔类型
                 */
                isBoolean : function(v){
                    return typeof v === 'boolean';
                },

                /**
                 * 判断是否为dom元素
                 */
                isElement : function(v) {
                    return !!v && v.tagName;
                },

                /**
                 * 判断是否已定义
                 */
                isDefined : function(v){
                    return typeof v !== 'undefined';
                }
            }
        }
    });

    return Screen;
});