define([],function(){
    var Share = Backbone.View.extend({
        initialize:function(){

            this.loadToken();

        },
        loadToken : function(){

        	console.log("微信分享");

        	$.ajax({
				url: "wx/share.php",
				type: "GET",
				dataType: 'json',
				success: function (json) {
					// var json = eval('(' + msg + ')');
					// console.log("appid:"+json.appId);
					// console.log("time:"+json.timestamp);
					// console.log("Str:"+json.nonceStr);
					// console.log("Ture:"+json.signature);

					wx.config({
			            // debug: true,
						appId: json.appId,
						timestamp: json.timestamp,
						nonceStr: json.nonceStr,
						signature: json.signature,
						rawString: json.rawString,
			            jsApiList: ['onMenuShareTimeline','onMenuShareAppMessage']
			        });

					wx.error(function (res) {
						// alert(JSON.stringify(res));
					});
					
				}
			});
        }
    });

    return Share;
});