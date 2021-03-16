define(["./baseView","./shareContent"],function(BaseView,ShareContent){
    var v = BaseView.extend({
        initialize:function(){
            v.__super__.initialize();

            this.loadTemplate("p2");

            $(".mask").hide();

        },
        onTemplateLoaded:function(){
            
            var self = this;

            this.shareObj = new ShareContent();

            console.log(location.hash.substr(1))


            // $(".wz_txt1").removeClass('out_txt');
            // $(".wz_txt2").removeClass('show_txt2');
            // $(".wz_txt3").removeClass('show_txt3');
            // $(".show_box").removeClass('show_box');

            
            window.addEventListener('resize',self.onAppesize,this);
            self.onAppesize();
            
            self.prohibitScroll();

            self.onClickTemplate();
            if(location.hash.substr(1) == "p2"){
                self.myCanvasTemplate();
            }
            
        },
        onClickTemplate : function(){
            var self = this;

            $(".wht_box").on('touchstart',function() {



                $("#navContent").fadeOut(1000,function(){
                    self.redirect("p3");
                });
            });

        },
        myCanvasTemplate : function(){
            var pageWidth = $(window).width();
            var addHeight = $(window).height();

            var renderer = PIXI.autoDetectRenderer(pageWidth, addHeight,{transparent: true });
            $(".quan_box").append(renderer.view);

            // create the root of the scene graph
            var stage = new PIXI.Container();

            var fruits = [
                'images/quan_1.png',
                'images/quan_2.png',
                'images/quan_3.png',
                'images/quan_4.png',
                'images/quan_5.png',
                'images/quan_6.png',
                'images/quan_7.png',
                
            ];

            var items = [];

            for(var i=0;i<fruits.length;i++){
                var item = PIXI.Sprite.fromImage(fruits[i]);
                item.position.x = pageWidth/2;
                item.position.y = addHeight/2 - 120;

                item.anchor.set(0.5);

                stage.addChild(item);

                items.push(item);
            }

            var number = 0,count = 0,_scale = 0,_alpha = 1,angle = 0.01;

            requestAnimationFrame(animate);

            function animate() {

                number++;

                // console.log(number);

                if(number>50){

                    angle += 0.01;

                    count += angle;

                    if(angle > 2)angle=2;

                    
                    
                    for(var a =0;a<fruits.length;a++){
                        if(a%2){
                            items[a].rotation = -count * Math.PI / 180;
                        }else{
                            items[a].rotation = count * Math.PI / 180;
                        }
                    }

                    
                }

                if(number>150){
                    $('.wz_txt1').addClass('out_txt');
                    $('.wz_txt2').addClass('show_txt2');
                }

                if(number>350){
                    $('.wz_txt2').addClass('out_txt');
                    _scale += 0.02;
                    for(var a =0;a<fruits.length;a++){
                        items[a].scale.x = items[a].scale.y = 1 + _scale;
                    }
                }
                if(number>450){
                    _alpha -=0.01;
                    for(var a =0;a<fruits.length;a++){
                        items[a].alpha = _alpha;
                    }
                    $(".wht_box").show();
                    $(".wht_box").addClass('show_box');
                    $(".wz_txt3").addClass('show_txt3');
                }

                if(number > 550) return;
                renderer.render(stage);
                requestAnimationFrame(animate);
            }
        },
        getRandom : function(n){
            return "JZ" + Math.floor(Math.random()*n+1)
        },
        onAppesize : function(){
            var pageWidth = $(window).width();
            var addHeight = $(window).height();
            var setHeight = parseInt(165 / (638 / pageWidth));
        }

    });

    return v;
});