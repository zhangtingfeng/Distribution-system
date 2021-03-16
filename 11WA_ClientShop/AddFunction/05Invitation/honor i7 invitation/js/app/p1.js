define(["./baseView","./shareContent"],function(BaseView,ShareContent){
    var v = BaseView.extend({
        shareObj:null,
        initialize:function(){
            v.__super__.initialize();

            this.loadTemplate("p1");

            $(".mask").hide();
        },

        onTemplateLoaded:function(){

            var self = this;

            this.shareObj = new ShareContent();
            self.prohibitScroll();
            window.addEventListener('resize', self.onrAppesize,this);
            self.onrAppesize();

            self.onClickTemplate();

        },
        onClickTemplate : function(){

            var self = this;

            $(".home_btn").on('touchstart',function(event) {

                $(".sp_box").addClass('out_sp').removeClass('show_sp');



                setTimeout(function(){
                    $("#navContent").fadeOut(1000,function(){
                        self.redirect("p2");
                    });
                },1000)
            });

            
        },
        animatePage : function(){
            var self = this;

            $(".home").addClass('sec_show').removeClass('sec_out');

        },
        getRandom : function(n){
            return "JZ" + Math.floor(Math.random()*n+1)
        },
        getQueryString: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        },
        onrAppesize:function(){
            
            var pageWidth = $(window).width();
            var addHeight = $(window).height();

        }

    });

    return v;
});