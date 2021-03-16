require.config( {

	baseUrl: "./js/lib/",
    urlArgs:"v=1.3",
	paths: {
		"Urlapp": "../../js/app"
	}

});

require(["Urlapp/Router","Urlapp/screen","Urlapp/loader","Urlapp/share"], function (Router,Screen,Loader,Share) {
	
	var self = this;
	var newMusic;
	var isBooleans = true;

	self.loader = new Loader();
	self.turnscreen = new Screen();

	self.loader.onImagesCallback = function(){


		if(self.turnscreen.Env().isAppMobile){
	        setTimeout(function(){
				self.router = new Router();
				self.share = new Share();
			},500);
		}
	}
    newMusic = $("#sound_button")[0]
    newMusic.play();

	// var queue = new createjs.LoadQueue();
 //    queue.installPlugin(createjs.Sound);
 //    queue.on("complete", handleComplete, this);
 //    queue.loadFile({id:"sound", src:"images/small.mp3"});
 //    function handleComplete() {
 //        newMusic = createjs.Sound.play("sound");
 //        newMusic.setLoop(1000)
 //        console.log("播放音乐");
 //    }

    $("#music").on('touchstart',function() {
    	if(isBooleans){
    		// createjs.Sound.stop("sound");
            newMusic.pause();
    		isBooleans = false;
    		$("#music img").attr('src', 'images/music2.png');
    	}else{
            newMusic.play();
    		// newMusic = createjs.Sound.play("sound");
            // newMusic.setLoop(1000)
    		isBooleans = true;
    		$("#music img").attr('src', 'images/music1.png');
    	}

    });

    FastClick.attach(document.body);

});
