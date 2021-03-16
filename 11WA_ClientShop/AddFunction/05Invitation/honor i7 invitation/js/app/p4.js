define(["./baseView","./shareContent"],function(BaseView,ShareContent){
    var v = BaseView.extend({
        type : "",
        setStop : 0,
        initialize:function(e){
            v.__super__.initialize(),this.type = e.type;;

            this.loadTemplate("p4");

            $(".mask").hide();
        },
        onTemplateLoaded:function(){

            var self = this;
            this.shareObj = new ShareContent();
            self.onClickTemplate();
            window.addEventListener('resize',self.onAppesize,this);
            self.onAppesize();
            self.pixiCanvasLoaded();

            // $.ajax({
            //     url: "api.php",
            //     type: "GET",
            //     success: function (msg) {
            //         var json = eval('(' + msg + ')'); 
            //         console.log(json.total);
            //         $("#navTxt").text(json.total);
            //     }
            // });
            

        },

        onClickTemplate : function(){
            var self = this;

            var camera;

            var queue = new createjs.LoadQueue();
            queue.installPlugin(createjs.Sound);
            queue.on("complete", handleComplete, this);
            queue.loadFile({id:"camera", src:"images/camera.mp3"});
            function handleComplete() {
                // createjs.Sound.play("sound",{loop:1000});
                camera = createjs.Sound.createInstance("camera");
                // newMusic.play({loop:1000});
            }

            $(".btn_taking").on('touchstart',function() {
                camera.play();



                self.setStop = 1;
                $(".black_color").fadeIn(200);
                setTimeout(function(){
                    // $(".black_color").hide();
                    $(".white_color").show();
                },200)
                setTimeout(function(){
                    $(".black_color").fadeOut(200);
                    $(".white_color").fadeOut(200,function(){
                        setTimeout(function(){
                            $(".taking_box").fadeOut(800);
                            $(".record_box").fadeIn(800);
                        },800)
                    });
                },400)
            });

            $(".btn_share").on('touchstart',function() {


                $(".mask").fadeIn(500,function(){
                    $(".mask .pic").addClass('show_mask');
                });
                

            });

            $(".mask .bg").on('touchstart',function() {
                $(".mask").fadeOut(500,function(){
                    $(".mask .pic").removeClass('show_mask');
                });
                
            });

        },
        pixiCanvasLoaded : function(){
            var pageWidth = $(window).width();
            var addHeight = $(window).height();

            var stage,renderer,background1,background2,pic,mask,speed=0;
            var self = this;

            renderer = PIXI.autoDetectRenderer(pageWidth, addHeight,{transparent: true });
            $("#navCanvas").append(renderer.view);

            stage = new PIXI.Container();

            background1 = PIXI.Sprite.fromImage('images/set_pic.jpg');
            background2 = PIXI.Sprite.fromImage('images/set_pic.jpg');
            background1.position.y = background2.position.y = 210;
            // background1.anchor.set(0.5);
            // background2.anchor.set(0.5);

            mask = PIXI.Sprite.fromImage('images/mask.png');
            // mask.anchor.set(0.5);
            mask.position.x = 0;
            mask.position.y =  210;

            // background1.mask = mask;
            // background2.mask = mask;
            stage.addChild(background1);
            stage.addChild(background2);
            stage.addChild(mask);

            

            requestAnimationFrame(animate);

            function animate() {

                if(self.setStop == 0){
                    speed+=10;

                    background1.position.x = -(speed * 0.6);
                    background1.position.x %= 2706 * 2;
                    if(background1.position.x < 0)
                    {
                        background1.position.x += 2706 * 2;
                    }
                    background1.position.x -= 2706;

                    background2.position.x = -(speed * 0.6) + 2706;
                    background2.position.x %= 2706 * 2;
                    if(background2.position.x < 0)
                    {
                        background2.position.x += 2706 * 2;
                    }
                    background2.position.x -= 2706;
                }

                renderer.render(stage);
                requestAnimationFrame(animate);
            }
        },
        getQueryString:function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        },
        onAppesize : function(){
            var pageWidth = $(window).width();
            var addHeight = $(window).height();

        }

    });

    return v;
});