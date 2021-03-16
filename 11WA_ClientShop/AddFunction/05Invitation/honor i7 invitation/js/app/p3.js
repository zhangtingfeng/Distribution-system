define(["./baseView","./shareContent"],function(BaseView,ShareContent){
    var v = BaseView.extend({
        type : "",
        initialize:function(e){
            v.__super__.initialize(),this.type = e.type;

            this.loadTemplate("p3");

            $(".mask").hide();

        },
        onTemplateLoaded:function(){

            var self = this;

            this.shareObj = new ShareContent();
            self.prohibitScroll();

            window.addEventListener('resize',self.onrAppesize,this);
            self.onrAppesize();

            $('section').eq(0).show();

            self.onClickTemplate();
            self.onTouchsTemplate();
        },
        onClickTemplate : function(){

            var self = this;
            
            for(var i=1;i<=4;i++){
                $(".i_btn" + i).on('touchstart',function() {
                    mask_loader();

                });
            }


            function mask_loader(){
                $(".mask_loader").fadeIn(500, function() {
                    setTimeout(function(){
                        $('#navContent').fadeOut(1000,function(){
                            self.redirect("p4");
                        });
                    },2000)
                });
            }


        },
        onTouchsTemplate : function(){

            var self = this;

            $('.btn_touch').on('touchstart',function(e) {
                var touch = e.originalEvent.targetTouches[0];
                e.preventDefault();
                startY = touch.pageY;
            });

            $('.btn_touch').on('touchmove',function(e) {
                var touch = e.originalEvent.targetTouches[0];
                e.preventDefault();
                onMove(touch,e);
            });

            var i = 0;

            function onMove (e, oe) {
                var touch = oe.originalEvent.changedTouches[0];
                var endY = touch.pageY;
                oe.preventDefault();
            }

            $('.btn_touch').on('touchend',function(e) {
                e.preventDefault();
                var touch = e.originalEvent.changedTouches[0];
                var endY = touch.pageY;

                console.log(i + "code");




                if(endY<470){
                    if(i<$('section').length){
                        i++;
                        console.log(i);
                        $(".btn_touch").hide();

                        for(var j =0;j<$('section').length;j++){
                            $('section').eq(j).find('.setp_pic').removeClass('show_out');
                            $('section').eq(j).find('.yuan').removeClass('show_touch');
                        }

                        $('section').eq(i-1).find('.setp_pic').addClass('show_out');
                        $('section').eq(i-1).find('.yuan').addClass('show_touch');

                        setTimeout(function(){
                            $('section').eq(i-1).fadeOut(1000,function(){
                                $(".btn_touch").show();
                            });
                            $('section').eq(i).fadeIn(1000);
                        },500)
                        

                        if(i == $('section').length){
                            // $('section').eq(0).find('.setp_pic').removeClass('show_out');
                            // $('section').eq(0).find('.yuan').removeClass('show_touch');

                            // setTimeout(function(){
                            //     $('section').eq($('section').length -1).fadeOut(1000,function(){
                            //         $(".btn_touch").show();
                            //     });
                            //     $('section').eq(0).fadeIn(1000);
                            // },500);

                            // i = 0;
                            console.log("跳转页面")
                            $(".mask_loader").fadeIn(500, function() {
                                setTimeout(function(){
                                    $('#navContent').fadeOut(1000,function(){
                                        self.redirect("p4");
                                    });
                                },2000)
                            });

                        }
                    }
                    
                }
                
            });

        },
        onrAppesize : function(){
            var pageWidth = $(window).width();
            var addHeight = $(window).height();
            
        },
        getRandom : function(n){
            return "JZ" + Math.floor(Math.random()*n+1)
        },
        getQueryString:function (name,link) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            // var r = window.location.search.substr(1).match(reg);
            var r = link.match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

    });

    return v;
});