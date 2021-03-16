define([], function() {

    var Router = Backbone.Router.extend({
        container:'#site-container',
        initialize: function() {
            Router.__super__.initialize();
            Backbone.history.start();

        },
        routes: {
            "" : "p1",
            "p1" : "p1",
            "p2": "p2",
            "p3": "p3",
            "sharepage?:type": "sharepage",
            "p4": "p4"
        },
        p1 : function(){
            this._loadView("p1",true);
        },
        p2 : function(){
            this._loadView("p2",true);
        },
        p3 : function(){
            this._loadView("p3",true);
        },
        sharepage : function(type){
            this._loadView("sharepage",true,{type:type});
        },
        p4 : function(){

            this._loadView("p4",true);
        },
        _loadView:function(view_name,clear,args){
            var self = this;
            require( ["../../js/app/"+view_name], function( LoadedView ) {
                var v = new LoadedView(args);

                console.log(args);

                self._fillContent(v.el,clear);
                
            });
        },
        _fillContent:function(c,clear){
            if(clear)
                $(this.container).empty();
            $(this.container).append(c);
        }
    });

    return Router;
});