define([],function(){
    var Loader = Backbone.View.extend({
        initialize:function(){
            
            Loader.__super__.initialize();

            var self = this;
            var fileList = [
                "images/logo.png",
                "images/diantong.png",
                "images/home_btn.png",
                "images/txt_home1.png",
                "images/wht_pic.png",
                "images/music1.png",
                "images/music2.png",
                "images/quan_1.png",
                "images/quan_2.png",
                "images/quan_3.png",
                "images/quan_4.png",
                "images/quan_5.png",
                "images/quan_6.png",
                "images/quan_7.png",
                "images/sp1.png",
                "images/sp2.png",
                "images/sp3.png",
                "images/sp4.png",
                "images/sp5.png",
                "images/sp6.png",
                "images/wz_txt1.png",
                "images/wz_txt2.png",
                "images/wz_txt3.png",
                "images/wz_txt4.png",
                "images/wz_txt5.png",
                "images/xian.png",
                "images/xian1.png",
                "images/xian2.png",
                "images/xian3.png",
                "images/yuan.png",
                "images/yuan1.png",
                "images/yuan2.png",
                "images/yuan3.png",
                "images/setp_1_1.png",
                "images/setp_2_1.png",
                "images/setp_3_1.png",
                "images/setp_4_1.png",
                "images/setp_1.jpg",
                "images/setp_2.jpg",
                "images/setp_3.jpg",
                "images/setp_4.jpg",
                "images/i_btn1.png",
                "images/i_btn2.png",
                "images/i_btn3.png",
                "images/i_btn4.png",
                "images/setp_wz1.png",
                "images/setp_wz2.png",
                "images/setp_wz3.png",
                "images/setp_wz4.png",
                "images/btn_share.png",
                "images/btn_taking.png",
                "images/mask_loader.png",
                "images/mask_loading.jpg",
                "images/mask.png",
                "images/pixi_i.png",
                "images/record_txt.png",
                "images/set_pic.jpg",
                "images/share_jt.png",
                "images/wz_flashing.png",
            ]

            self.showLoading();
            self.loadImages(fileList);
        },
        loadImages:function(imgs,onComplete,onProgress){

            var self = this;
            var basePath = "";

            var total = imgs.length,loadedCount = 0;

            for(var i in imgs){
                var tempImg = new Image();
                tempImg.onload = function(){

                    loadedCount++;

                    var percent = Math.round((loadedCount / total) * 100);

                    // console.log(percent);

                    $("#loading p").text(percent + "%");

                    $("#loading .progress").css({'width':percent+"%"});

                    if(loadedCount == total){
                        //加载完成
                        onComplete && onComplete.apply(self);
                        self.imgComplete();

                    }
                    onProgress && onProgress.apply(self,[loadedCount,total]);
                };
                tempImg.onerror = function(){
                    //即使出错也显示进度防止页面停住不动
                    loadedCount++;

                    var percent = Math.round((loadedCount / total) * 100);
                    
                    // console.log(percent);
                    $("#loading p").text(percent + "%");
                    $("#loading .progress").css({'width':percent+"%"});

                    if(loadedCount == total){
                        //加载完成
                        onComplete && onComplete.apply(self);
                        self.imgComplete();
                    }
                    onProgress && onProgress.apply(self,[loadedCount,total]);
                };
                tempImg.src = imgs[i];
            }

        },
        imgComplete : function(){
            var self = this;
            self.hideLoading();
            self.onImagesCallback();
        },
        showLoading:function(){
            $("#loading").show();
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
        onImagesCallback:function(){
            // alert("回调");
            
        }

    });

    return Loader;
});