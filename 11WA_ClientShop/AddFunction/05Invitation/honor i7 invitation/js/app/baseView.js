define([],function(){
    var v = Backbone.View.extend({

        _loading:null,
        _curLoadingImgIndex:0,
        _needLoadedImages:null,
        _loadNeededImagesComplete:null,

        loadedImages:[],

        shareObj:null,

        scalePercent:1,//APP为适应屏幕缩放参数

        initialize:function(){
            v.__super__.initialize();

            // this.fitContentSize();//调整程序内容适应屏幕大小,一些项目不需要可以禁用掉
            return this;
        },

        fitContentSize:function(){
            window.addEventListener('resize', $.proxy(this._fitAppSize,this));
            this._fitAppSize();
        },

        _fitAppSize:function(){
            var winh = window.innerHeight * window.devicePixelRatio; //设备像素比devicePixelRatio
            var winw = window.innerWidth * window.devicePixelRatio;

            var percent = (winh / 1008) ;
            if(winw > 640){
                percent *= 640/winw;
            }
            if(percent > 1) percent = 1;

            this.scalePercent = percent;

            this.vScreenSize = {width:winw*percent/window.devicePixelRatio,height: winw*percent * (1008/640)/window.devicePixelRatio};

            $("#site-container").css({'width':this.vScreenSize.width+'px','height':this.vScreenSize.height+'px'});

            // $("#fullscreen_tips").css({'width':this.vScreenSize.width+'px','height':this.vScreenSize.height+'px'});

            if($("#site-container").width() == 640){
                $("#site-container").css({'height':'1009px'});
            }

            $("#site-container").css({'height':'1009px'});

            this.trigger("update_app_size"); //回调函数  更新APP尺寸

        },
        loadTemplate: function(name,animated){
            animated = (animated === false ? false : true);

            var self = this;
            // self.showLoading();

            self.$el.load('template/'+name+'.html',function(e){

                if(animated) {
                    TweenMax.to(self.$el,0,{opacity:0});
                }

                TweenMax.to(self.$el,2,{opacity:1});

                self.onTemplateLoaded();
            });
            
        },
        onTemplateLoaded:function(){
            //subview to do something
        },

        showLoading:function(){
            $("#loading").show();
            // TweenMax.to($("#loading"),1,{opacity:1});
        },

        hideLoading : function(){
            var self = this;
            TweenMax.to(self,0,{onComplete:function(){
                TweenMax.to(self.$el,2,{opacity:1,delay:0.5});
                TweenMax.to($("#loading"),1,{opacity:0,onComplete:function(){
                    $("#loading").hide();
                }});
            }});
            
        },
        picLoading : function(){
            $("#loadImages").show();
            TweenMax.to($("#loadImages"),0,{opacity:0});
            TweenMax.to($("#loadImages"),1,{opacity:0.4});
        },
        hidePicLoading : function(){
            TweenMax.to($("#loadImages"),1,{opacity:0,onComplete:function(){
                $("#loadImages").hide();
            }})
        },

        /* 静止滚动 */
        prohibitScroll : function(){

            this.preventDefault();
            
        },
        preventDefault:function(){
            document.addEventListener('touchmove', this.setDefault, false);
        },
        setDefault:function(e){
            e.preventDefault();
        },
        removeMyScroll : function(e){
            document.removeEventListener('touchmove',this.setDefault, false);
        },

        /* 获取汉字的长度(两个字母算一个字) */
        getStrCNLen : function(str){
            var count = 0;
            for(var i=0;i<str.length;i++){
                var c = str.charCodeAt(i);
                if(c >= 0x4e00 && c <= 0x9fa5) count++;//中文
                else count += 0.5;
            }

            return count;
        },

        redirect:function(fragment){
            Backbone.history.navigate(fragment,{trigger:true});
        },

        analysisTrack : function(name){
            _hmt.push(['_trackEvent', 'button_click', "分享成功_"+name]);
            ga('send', 'event', 'button_click', '分享成功_'+name, 'my_bwc');
        }

    });
    return v;
});

$.fn.extend({
    animateCss: function (animationName) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        $(this).addClass('animated ' + animationName).one(animationEnd, function() {
            $(this).removeClass('animated ' + animationName);
        });
    }
});